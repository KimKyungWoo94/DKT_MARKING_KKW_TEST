using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EzIna
{
    /// <summary>
    /// LocalDuplicateManager 간이 테스트 폼 (개발/디버그 전용)
    /// FrmMain에서 Ctrl+Shift+D 로 열림
    /// </summary>
    public class FrmDuplicateTest : Form
    {
        // ─── 고정 테스트 코드 (저장 / 조회에 공통 사용) ───────────────
        private static readonly string[] TEST_CODES = new[]
        {
            "ABC123456789XYZ01",
            "ABC123456789XYZ02",
            "ABC123456789XYZ03",
            "ABC123456789XYZ04",
            "ABC123456789XYZ05",
        };

        private RichTextBox _log;
        private Label _lblDbPath;

        // ─────────────────────────────────────────────────────────────
        //  생성자 — 컨트롤 전부 코드로 생성 (Designer 파일 없음)
        // ─────────────────────────────────────────────────────────────
        public FrmDuplicateTest()
        {
            Text = "LocalDuplicateManager  간이 TEST  [개발용]";
            Size = new Size(860, 620);
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.WhiteSmoke;

            BuildUI();
        }

        private void BuildUI()
        {
            // ── 상단 DB 경로 라벨 ──────────────────────────────────────
            _lblDbPath = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.LightSkyBlue,
                Font = new Font("Consolas", 9f),
                Text = "DB 경로: (초기화 전)",
                Padding = new Padding(4, 0, 0, 0),
            };

            // ── 로그 창 ────────────────────────────────────────────────
            _log = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.FromArgb(20, 20, 20),
                ForeColor = Color.LightGreen,
                Font = new Font("Consolas", 9.5f),
                BorderStyle = BorderStyle.None,
            };

            // ── 버튼 패널 (우측) ───────────────────────────────────────
            var btnPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 210,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(6),
                BackColor = Color.FromArgb(45, 45, 48),
                AutoScroll = true,
            };

            btnPanel.Controls.AddRange(new Control[]
            {
                MakeHeader("── 초기화 ──"),
                MakeBtn("1. Init (Temp 폴더)",    Color.SteelBlue,        OnInitTemp),
                MakeBtn("2. Init (System 폴더)",  Color.CadetBlue,        OnInitSystem),
                MakeSep(),
                MakeHeader("── 데이터 저장 ──"),
                MakeBtn("3. 저장 5건 (현재 시각)", Color.SeaGreen,         OnSaveCurrent),
                MakeBtn("4. 저장 3건 (10일 전)",  Color.DarkOliveGreen,   OnSaveOld),
                MakeSep(),
                MakeHeader("── 중복 검사 ──"),
                MakeBtn("5. 단건 중복 검사",       Color.MediumPurple,     OnCheckSingle),
                MakeBtn("6. 배치 중복 검사",       Color.SlateBlue,        OnCheckBatch),
                MakeSep(),
                MakeHeader("── 정리 / 조회 ──"),
                MakeBtn("7. Cleanup (7일 초과 삭제)", Color.Tomato,        OnCleanup),
                MakeBtn("8. 전체 레코드 수",        Color.DarkGoldenrod,   OnCount),
                MakeBtn("9. DB 전체 삭제",          Color.Firebrick,       OnClearAll),
                MakeSep(),
                MakeBtn("로그 지우기",              Color.DimGray,         (s, e) => _log.Clear()),
                MakeSep(),
                MakeBtn("닫기",                     Color.FromArgb(60, 60, 60), (s, e) => Close()),
            });

            Controls.Add(_log);
            Controls.Add(btnPanel);
            Controls.Add(_lblDbPath);
        }

        // ─────────────────────────────────────────────────────────────
        //  버튼 핸들러
        // ─────────────────────────────────────────────────────────────

        private void OnInitTemp(object sender, EventArgs e)
        {
            string dir = Path.Combine(Path.GetTempPath(), "EzInaFlex_DupTest");
            Log(">> Init (Temp)", Color.LightSkyBlue);
            Log($"   경로 : {dir}");
            bool ok = LocalDuplicateManager.Instance.Initialize(dir);
            Log(ok ? "   결과 : 성공" : "   결과 : 실패 (로그 확인)", ok ? Color.LightGreen : Color.Tomato);
            _lblDbPath.Text = $"DB 경로: {dir}\\MarkingHistory.db";
        }

        private void OnInitSystem(object sender, EventArgs e)
        {
            Log(">> Init (System 폴더)", Color.LightSkyBlue);
            try
            {
                Log($"   FA.DIR.SYSTEM : {FA.DIR.SYSTEM}");
                bool ok = LocalDuplicateManager.Instance.Initialize();   // baseDirectory = null → FA.DIR.SYSTEM
                Log(ok ? "   결과 : 성공" : "   결과 : 실패 (로그 확인)", ok ? Color.LightGreen : Color.Tomato);
                _lblDbPath.Text = $"DB 경로: {FA.DIR.SYSTEM}LocalDB\\MarkingHistory.db";
            }
            catch (Exception ex)
            {
                Log($"   예외 : {ex.Message}", Color.Tomato);
            }
        }

        private void OnSaveCurrent(object sender, EventArgs e)
        {
            Log(">> 저장 5건 (현재 시각)", Color.Yellow);
            if (!CheckInitialized()) return;

            for (int i = 0; i < TEST_CODES.Length; i++)
            {
                bool ok = LocalDuplicateManager.Instance.SaveMarkingResult(
                    markingData: TEST_CODES[i],
                    serialNo: $"SN{i + 1:D3}",
                    codeType: eMarkingCodeType.VTYPE,
                    modelName: "TEST_MODEL",
                    lotNo: "LOT_TEST_001",
                    markingResult: "OK"
                );
                Log($"   [{i + 1}] {TEST_CODES[i]}  →  {(ok ? "저장 OK" : "저장 실패")}", ok ? Color.LightGreen : Color.Tomato);
            }
        }

        private void OnSaveOld(object sender, EventArgs e)
        {
            Log(">> 저장 3건 (10일 전 — Cleanup 테스트용)", Color.Yellow);
            if (!CheckInitialized()) return;

            DateTime oldTime = DateTime.Now.AddDays(-10);
            for (int i = 0; i < 3; i++)
            {
                string code = $"OLD_CODE_{i + 1:D3}";
                bool ok = LocalDuplicateManager.Instance.SaveMarkingResult(
                    markingData: code,
                    serialNo: $"OLD_SN{i + 1:D3}",
                    codeType: eMarkingCodeType.PCA,
                    modelName: "OLD_MODEL",
                    lotNo: "LOT_OLD_001",
                    markingResult: "OK",
                    markingTime: oldTime
                );
                Log($"   [{i + 1}] {code}  시각={oldTime:yyyy-MM-dd}  →  {(ok ? "저장 OK" : "저장 실패")}", ok ? Color.LightGreen : Color.Tomato);
            }
        }

        private void OnCheckSingle(object sender, EventArgs e)
        {
            Log(">> 단건 중복 검사 (TEST_CODES[0] 기준)", Color.Cyan);
            if (!CheckInitialized()) return;

            string target = TEST_CODES[0];
            var result = LocalDuplicateManager.Instance.CheckDuplicate(target, "SN001", eMarkingCodeType.VTYPE);

            Log($"   조회 코드  : {result.MarkingData}");
            Log($"   중복 여부  : {result.IsDuplicate}");
            if (result.IsDuplicate)
            {
                Log($"   이전 마킹  : {result.PreviousMarkingTime:yyyy-MM-dd HH:mm:ss}", Color.Orange);
                Log($"   모 델 명   : {result.PreviousModelName}");
                Log($"   LOT No    : {result.PreviousLotNo}");
                Log($"   결   과   : {result.PreviousMarkingResult}");
                Log("   ─── 경고 메시지 미리보기 ───────────────", Color.Gray);
                Log(result.ToWarningMessage(), Color.Orange);
            }
            else
            {
                Log("   → 중복 없음 (신규)", Color.LightGreen);
            }
        }

        private void OnCheckBatch(object sender, EventArgs e)
        {
            Log(">> 배치 중복 검사 (TEST_CODES 5건 + 미저장 1건)", Color.Cyan);
            if (!CheckInitialized()) return;

            var items = new List<DuplicateCheckResult>();
            foreach (string code in TEST_CODES)
                items.Add(new DuplicateCheckResult { MarkingData = code, SerialNo = "SN_BATCH", CodeType = eMarkingCodeType.VTYPE });

            // 미저장 코드 1건 추가
            items.Add(new DuplicateCheckResult { MarkingData = "NEW_CODE_NOT_SAVED", SerialNo = "SN_NEW", CodeType = eMarkingCodeType.VTYPE });

            var batch = LocalDuplicateManager.Instance.CheckDuplicateBatch(items);

            Log($"   입력 {items.Count}건  /  중복 감지 {batch.DuplicateList.Count}건  /  HasDuplicate={batch.HasDuplicate}");
            foreach (var dup in batch.DuplicateList)
                Log($"   [중복] {dup.MarkingData}  이전={dup.PreviousMarkingTime:yyyy-MM-dd HH:mm}  결과={dup.PreviousMarkingResult}", Color.Orange);

            if (batch.HasDuplicate)
            {
                Log("   ─── 배치 경고 메시지 미리보기 ──────────", Color.Gray);
                Log(batch.ToSummaryMessage(), Color.Orange);
            }
        }

        private void OnCleanup(object sender, EventArgs e)
        {
            Log(">> CleanupOldRecords (7일 초과 삭제)", Color.Tomato);
            if (!CheckInitialized()) return;

            long before = LocalDuplicateManager.Instance.GetTotalCount();
            int deleted = LocalDuplicateManager.Instance.CleanupOldRecords();
            long after = LocalDuplicateManager.Instance.GetTotalCount();

            Log($"   삭제 전 : {before}건");
            Log($"   삭제 됨 : {deleted}건  (7일 초과 레코드)", deleted > 0 ? Color.LightGreen : Color.Gray);
            Log($"   삭제 후 : {after}건");
        }

        private void OnCount(object sender, EventArgs e)
        {
            Log(">> 전체 레코드 수 조회", Color.Yellow);
            if (!CheckInitialized()) return;

            long count = LocalDuplicateManager.Instance.GetTotalCount();
            Log($"   현재 레코드 수 : {count} 건", Color.LightGreen);
        }

        private void OnClearAll(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "테스트 DB의 모든 레코드를 삭제합니다.\n계속하시겠습니까?",
                "확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            Log(">> DB 전체 삭제", Color.Tomato);
            if (!CheckInitialized()) return;

            // 보관일수를 0으로 주면 전체 삭제됨 (미래 날짜 cutoff)
            bool ok = LocalDuplicateManager.Instance.DeleteAllRecords();
            long count = LocalDuplicateManager.Instance.GetTotalCount();
            Log($"   삭제 결과 : {(ok ? "성공" : "실패")}  /  남은 레코드 : {count}건",
                ok ? Color.LightGreen : Color.Tomato);
        }

        // ─────────────────────────────────────────────────────────────
        //  헬퍼
        // ─────────────────────────────────────────────────────────────

        private bool CheckInitialized()
        {
            if (LocalDuplicateManager.Instance.IsInitialized)
                return true;

            Log("   !! Initialize 가 먼저 호출되어야 합니다.", Color.Tomato);
            return false;
        }

        private void Log(string message, Color? color = null)
        {
            if (_log.InvokeRequired)
            {
                _log.Invoke(new Action(() => Log(message, color)));
                return;
            }

            _log.SelectionStart = _log.TextLength;
            _log.SelectionLength = 0;
            _log.SelectionColor = color ?? Color.LightGreen;
            _log.AppendText(message + "\n");
            _log.ScrollToCaret();
        }

        // ─────────────────────────────────────────────────────────────
        //  UI 팩토리
        // ─────────────────────────────────────────────────────────────

        private Button MakeBtn(string text, Color backColor, EventHandler handler)
        {
            var btn = new Button
            {
                Text = text,
                Width = 192,
                Height = 34,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 2, 0, 2),
                Font = new Font("맑은 고딕", 8.5f),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(4, 0, 0, 0),
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            btn.Click += handler;
            return btn;
        }

        private Label MakeHeader(string text)
        {
            return new Label
            {
                Text = text,
                Width = 192,
                Height = 18,
                ForeColor = Color.Gray,
                Font = new Font("맑은 고딕", 8f),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 4, 0, 0),
            };
        }

        private Label MakeSep()
        {
            return new Label { Width = 192, Height = 4 };
        }
    }
}
