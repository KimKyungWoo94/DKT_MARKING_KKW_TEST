using System;
using System.Collections.Generic;
using System.Text;

namespace EzIna
{
    // ─────────────────────────────────────────────────────────────
    //  마킹 코드 타입
    //  MES 연결 모드에서는 중복 검사를 수행하지 않음 (MES 자체 처리)
    //  수동(Manual) 모드에서만 LocalDuplicateManager 활성화
    // ─────────────────────────────────────────────────────────────
    public enum eMarkingCodeType
    {
        VTYPE,      // V타입  : 범위 시리얼 (MES StartNo~EndNo)
        BVTYPE,     // BV타입 : 지정 시리얼 리스트 (MES TargetNumList)
        SHIELD_CAN, // Shield Can QR : 23~35자, Pos21~23 = Serial(001~ZZZ)
        PCA,        // PCA Matrix   : 16~17자, Pos13~16 = Serial(0001~9999)
    }

    // ─────────────────────────────────────────────────────────────
    //  단건 중복 검사 결과
    // ─────────────────────────────────────────────────────────────
    public class DuplicateCheckResult
    {
        /// <summary>중복 여부</summary>
        public bool   IsDuplicate { get; set; }

        /// <summary>검사 대상 마킹 코드 (전체 문자열)</summary>
        public string MarkingData { get; set; }

        /// <summary>검사 대상 시리얼 번호</summary>
        public string SerialNo    { get; set; }

        /// <summary>코드 타입</summary>
        public eMarkingCodeType CodeType { get; set; }

        // ── 이전 마킹 이력 (중복 발견 시 채워짐) ──────────────────
        public DateTime PreviousMarkingTime   { get; set; }
        public string   PreviousModelName     { get; set; }
        public string   PreviousLotNo         { get; set; }
        public string   PreviousMarkingResult { get; set; }

        public DuplicateCheckResult()
        {
            IsDuplicate           = false;
            MarkingData           = string.Empty;
            SerialNo              = string.Empty;
            CodeType              = eMarkingCodeType.VTYPE;
            PreviousMarkingTime   = DateTime.MinValue;
            PreviousModelName     = string.Empty;
            PreviousLotNo         = string.Empty;
            PreviousMarkingResult = string.Empty;
        }

        /// <summary>
        /// 작업자 경고 팝업 메시지 생성
        /// </summary>
        public string ToWarningMessage()
        {
            return string.Format(
                "[중복 마킹 경고]\r\n\r\n" +
                "마킹 데이터 : {0}\r\n" +
                "시리얼 번호 : {1}\r\n" +
                "코드 타입   : {2}\r\n\r\n" +
                "─ 이전 마킹 이력 ──────────────────\r\n" +
                "마킹 일시 : {3:yyyy-MM-dd HH:mm:ss}\r\n" +
                "모 델 명  : {4}\r\n" +
                "LOT No   : {5}\r\n" +
                "결   과   : {6}\r\n" +
                "────────────────────────────────────\r\n\r\n" +
                "계속 진행하시겠습니까?",
                MarkingData,
                SerialNo,
                CodeType.ToString(),
                PreviousMarkingTime,
                PreviousModelName,
                PreviousLotNo,
                PreviousMarkingResult
            );
        }
    }

    // ─────────────────────────────────────────────────────────────
    //  배치(Batch) 중복 검사 결과
    //  마킹 시작 전 로트 전체를 한 번에 조회할 때 사용
    // ─────────────────────────────────────────────────────────────
    public class BatchDuplicateCheckResult
    {
        /// <summary>배치 내 중복 항목 존재 여부</summary>
        public bool HasDuplicate { get; set; }

        /// <summary>중복 감지된 항목 목록</summary>
        public List<DuplicateCheckResult> DuplicateList { get; set; }

        public BatchDuplicateCheckResult()
        {
            HasDuplicate  = false;
            DuplicateList = new List<DuplicateCheckResult>();
        }

        /// <summary>
        /// 배치 중복 경고 요약 메시지 생성 (최대 10건 표시)
        /// </summary>
        public string ToSummaryMessage()
        {
            if (!HasDuplicate)
                return string.Empty;

            var sb = new StringBuilder();
            sb.AppendFormat("[배치 중복 경고]  총 {0}건 중복 발견\r\n\r\n", DuplicateList.Count);

            int showCount = Math.Min(DuplicateList.Count, 10);
            for (int i = 0; i < showCount; i++)
            {
                DuplicateCheckResult r = DuplicateList[i];
                sb.AppendFormat(
                    "  [{0:D2}] {1}  (이전마킹: {2:yyyy-MM-dd HH:mm}  결과: {3})\r\n",
                    i + 1,
                    r.MarkingData,
                    r.PreviousMarkingTime,
                    r.PreviousMarkingResult
                );
            }

            if (DuplicateList.Count > 10)
                sb.AppendFormat("  ... 외 {0}건\r\n", DuplicateList.Count - 10);

            sb.Append("\r\n계속 진행하시겠습니까?");
            return sb.ToString();
        }
    }
}
