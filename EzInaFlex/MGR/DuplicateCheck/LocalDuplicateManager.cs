using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace EzIna
{
    /// <summary>
    /// 로컬 SQLite 기반 마킹 이력 관리 및 중복 검사
    ///
    /// [사용 조건]
    ///   - MES 미연결 수동(Manual) 모드 전용
    ///   - MES 연결 모드에서는 MES Oracle SP가 중복 검사를 담당하므로 호출하지 않음
    ///
    /// [DB 위치]
    ///   {FA.DIR.SYSTEM}LocalDB\MarkingHistory.db
    ///
    /// [보관 기간]
    ///   7일 (앱 시작 시 + 24시간 주기 자동 정리)
    /// </summary>
    public class LocalDuplicateManager : SingleTone<LocalDuplicateManager>
    {
        // ─────────────────────────────────────────────────────────
        //  상수
        // ─────────────────────────────────────────────────────────
        private const int KEEP_DAYS = 7;
        private const string LOG_HEAD = "LocalDuplicateManager";
        private const string DB_FILE_NAME = "MarkingHistory.db";
        private const string DB_DIR_NAME = "LocalDB";

        // ─────────────────────────────────────────────────────────
        //  내부 상태
        // ─────────────────────────────────────────────────────────
        private string _connectionString;
        private bool _initialized = false;
        private Timer _cleanupTimer;

        private readonly object _lock = new object();

        // ─────────────────────────────────────────────────────────
        //  싱글톤 생성자 (SingleTone<T>가 Activator로 호출)
        // ─────────────────────────────────────────────────────────
        protected LocalDuplicateManager() { }

        protected override void OnCreateInstance()
        {
            base.OnCreateInstance();
        }

        // ─────────────────────────────────────────────────────────
        //  초기화 — 앱 시작 시 1회 호출
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// DB 파일 생성, 테이블/인덱스 준비, 7일 초과 레코드 정리.
        /// baseDirectory 미지정 시 FA.DIR.SYSTEM 하위 LocalDB 폴더 사용.
        /// </summary>
        public bool Initialize(string baseDirectory = null)
        {
            try
            {
                string dir = !string.IsNullOrEmpty(baseDirectory)
                    ? baseDirectory
                    : Path.Combine(FA.DIR.SYSTEM, DB_DIR_NAME);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string dbPath = Path.Combine(dir, DB_FILE_NAME);
                _connectionString = string.Format("Data Source={0};Version=3;", dbPath);

                CreateTableIfNotExists();
                _initialized = true;   // CleanupOldRecords() 호출 전에 true 설정
                CleanupOldRecords();

                // 24시간 주기 자동 정리
                _cleanupTimer = new Timer(
                    _ => CleanupOldRecords(),
                    null,
                    TimeSpan.FromHours(24),
                    TimeSpan.FromHours(24)
                );
                FA.LOG.Debug(LOG_HEAD, string.Format("Initialize OK  db={0}", dbPath));
                return true;
            }
            catch (Exception ex)
            {
                LogError("Initialize", ex);
                return false;
            }
        }

        private void CreateTableIfNotExists()
        {
            const string sql = @"
                CREATE TABLE IF NOT EXISTS MarkingHistory (
                    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                    MarkingTime   TEXT    NOT NULL,
                    MarkingData   TEXT    NOT NULL,
                    CodeType      TEXT,
                    SerialNo      TEXT,
                    ModelName     TEXT,
                    LotNo         TEXT,
                    MarkingResult TEXT
                );
                CREATE INDEX IF NOT EXISTS idx_MarkingData ON MarkingHistory(MarkingData);
                CREATE INDEX IF NOT EXISTS idx_MarkingTime ON MarkingHistory(MarkingTime);";

            using (var conn = OpenConnection())
            using (var cmd = new SQLiteCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────────────────
        //  단건 중복 검사
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// 유닛 마킹 직전 단건 중복 조회.
        /// result.IsDuplicate == true 이면 이전 마킹 정보가 채워짐.
        /// </summary>
        public DuplicateCheckResult CheckDuplicate(
            string markingData,
            string serialNo,
            eMarkingCodeType codeType)
        {
            var result = new DuplicateCheckResult
            {
                MarkingData = markingData,
                SerialNo = serialNo,
                CodeType = codeType
            };

            if (!_initialized || string.IsNullOrEmpty(markingData))
                return result;

            try
            {
                const string sql = @"
                    SELECT MarkingTime, ModelName, LotNo, MarkingResult
                    FROM   MarkingHistory
                    WHERE  MarkingData = @MarkingData
                    ORDER  BY MarkingTime DESC
                    LIMIT  1;";

                lock (_lock)
                    using (var conn = OpenConnection())
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MarkingData", markingData);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result.IsDuplicate = true;
                                result.PreviousMarkingTime = DateTime.Parse(reader["MarkingTime"].ToString());
                                result.PreviousModelName = reader["ModelName"].ToString();
                                result.PreviousLotNo = reader["LotNo"].ToString();
                                result.PreviousMarkingResult = reader["MarkingResult"].ToString();
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                LogError("CheckDuplicate", ex);
            }

            return result;
        }

        // ─────────────────────────────────────────────────────────
        //  배치 중복 검사
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// 로트 시작 전 배치 전체 중복 조회.
        /// items 각 항목의 MarkingData / SerialNo / CodeType 을 미리 채워서 전달.
        /// 중복 발견된 항목에 이전 마킹 정보가 채워져 반환됨.
        /// </summary>
        public BatchDuplicateCheckResult CheckDuplicateBatch(List<DuplicateCheckResult> items)
        {
            var batchResult = new BatchDuplicateCheckResult();

            if (!_initialized || items == null || items.Count == 0)
                return batchResult;

            try
            {
                const string sql = @"
                    SELECT MarkingTime, ModelName, LotNo, MarkingResult
                    FROM   MarkingHistory
                    WHERE  MarkingData = @MarkingData
                    ORDER  BY MarkingTime DESC
                    LIMIT  1;";

                lock (_lock)
                    using (var conn = OpenConnection())
                    {
                        foreach (var item in items)
                        {
                            if (string.IsNullOrEmpty(item.MarkingData)) continue;

                            using (var cmd = new SQLiteCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@MarkingData", item.MarkingData);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        item.IsDuplicate = true;
                                        item.PreviousMarkingTime = DateTime.Parse(reader["MarkingTime"].ToString());
                                        item.PreviousModelName = reader["ModelName"].ToString();
                                        item.PreviousLotNo = reader["LotNo"].ToString();
                                        item.PreviousMarkingResult = reader["MarkingResult"].ToString();

                                        batchResult.HasDuplicate = true;
                                        batchResult.DuplicateList.Add(item);
                                    }
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                LogError("CheckDuplicateBatch", ex);
            }

            return batchResult;
        }

        // ─────────────────────────────────────────────────────────
        //  마킹 결과 저장
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// 마킹 완료 후 이력을 로컬 DB에 저장.
        /// markingTime 미지정 시 현재 시각 사용.
        /// </summary>
        public bool SaveMarkingResult(
            string markingData,
            string serialNo,
            eMarkingCodeType codeType,
            string modelName,
            string lotNo,
            string markingResult,
            DateTime? markingTime = null)
        {
            if (!_initialized || string.IsNullOrEmpty(markingData))
                return false;

            try
            {
                const string sql = @"
                    INSERT INTO MarkingHistory
                        (MarkingTime, MarkingData, CodeType, SerialNo, ModelName, LotNo, MarkingResult)
                    VALUES
                        (@MarkingTime, @MarkingData, @CodeType, @SerialNo, @ModelName, @LotNo, @MarkingResult);";

                DateTime time = markingTime ?? DateTime.Now;

                lock (_lock)
                    using (var conn = OpenConnection())
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MarkingTime", time.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@MarkingData", markingData);
                        cmd.Parameters.AddWithValue("@CodeType", codeType.ToString());
                        cmd.Parameters.AddWithValue("@SerialNo", serialNo ?? string.Empty);
                        cmd.Parameters.AddWithValue("@ModelName", modelName ?? string.Empty);
                        cmd.Parameters.AddWithValue("@LotNo", lotNo ?? string.Empty);
                        cmd.Parameters.AddWithValue("@MarkingResult", markingResult ?? string.Empty);
                        cmd.ExecuteNonQuery();
                    }
                return true;
            }
            catch (Exception ex)
            {
                LogError("SaveMarkingResult", ex);
                return false;
            }
        }

        // ─────────────────────────────────────────────────────────
        //  7일 초과 레코드 정리
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// 보관 기간(7일) 초과 레코드 삭제. 삭제된 건수 반환.
        /// </summary>
        public int CleanupOldRecords()
        {
            if (!_initialized) return 0;

            try
            {
                string cutoff = DateTime.Now.AddDays(-KEEP_DAYS).ToString("yyyy-MM-dd HH:mm:ss");

                const string sql = "DELETE FROM MarkingHistory WHERE MarkingTime < @Cutoff;";

                lock (_lock)
                    using (var conn = OpenConnection())
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cutoff", cutoff);
                        int deleted = cmd.ExecuteNonQuery();

                        if (deleted > 0)
                            FA.LOG.Debug(LOG_HEAD, string.Format("Cleanup {0} records (older than {1})", deleted, cutoff));

                        return deleted;
                    }
            }
            catch (Exception ex)
            {
                LogError("CleanupOldRecords", ex);
                return 0;
            }
        }

        // ─────────────────────────────────────────────────────────
        //  진단용 조회
        // ─────────────────────────────────────────────────────────

        /// <summary>보관 중인 총 레코드 수</summary>
        public long GetTotalCount()
        {
            if (!_initialized) return 0;
            try
            {
                lock (_lock)
                    using (var conn = OpenConnection())
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM MarkingHistory;", conn))
                        return (long)cmd.ExecuteScalar();
            }
            catch { return 0; }
        }

        /// <summary>모든 레코드 삭제 (개발/테스트 전용)</summary>
        public bool DeleteAllRecords()
        {
            if (!_initialized) return false;
            try
            {
                lock (_lock)
                    using (var conn = OpenConnection())
                    using (var cmd = new SQLiteCommand("DELETE FROM MarkingHistory;", conn))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
            }
            catch (Exception ex)
            {
                LogError("DeleteAllRecords", ex);
                return false;
            }
        }

        /// <summary>초기화 완료 여부</summary>
        public bool IsInitialized { get { return _initialized; } }

        // ─────────────────────────────────────────────────────────
        //  내부 헬퍼
        // ─────────────────────────────────────────────────────────

        private SQLiteConnection OpenConnection()
        {
            var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private void LogError(string method, Exception ex)
        {
            try { FA.LOG.Debug(LOG_HEAD, string.Format("[{0}] ERROR: {1}", method, ex.Message)); }
            catch { }
        }
    }
}
