using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    /// <summary>
    /// For DKT
    /// </summary>
    public class RunningDataItem
    {
        private double m_fPosX;
        private double m_fPosY;
        private double m_fPosZ;

        public double fPosX
        {
            get { return m_fPosX; }
            set { m_fPosX = value; }
        }
        public double fPosY
        {
            get { return m_fPosY; }
            set { m_fPosY = value; }
        }

        public double fPosZ
        {
            get { return m_fPosZ; }
            set { m_fPosZ = value; }
        }
        private int m_iIDX = -1;
        public int iIDX
        {
            get { return m_iIDX; }
            set { m_iIDX = value; }
        }
        private long m_iMarkingIDX;

        public long iMarkingIDX
        {
            get { return m_iMarkingIDX; }
            set { m_iMarkingIDX = value; }
        }
        private int m_iInspOrderIDX = 0;
        public int iInspOrderIDX
        {
            get { return m_iInspOrderIDX; }
            set { m_iInspOrderIDX = value; }
        }

        public string strMarkingIDX_TO_32
        {
            get
            {
                string strRet = "";
                if (DKT_ConvertCode.Instance.ConvertSerialNumber_10To32(DKT_ConvertCode.eCodeType.Variable, (int)m_iMarkingIDX, 5, out strRet) == true)
                {

                }
                else
                {
                    strRet = "";
                }
                return strRet;
            }
        }
        public void CraeteMarkingIDX_TO_String()
        {
            m_strMarkingIDX = m_iMarkingIDX.ToString().PadLeft(m_iZeroPadCount <= 0 ? 1 : m_iZeroPadCount, '0');
        }
        private string m_strMarkingIDX="";
         public string strMarkingIDX
        {
            get
            {                
                return m_strMarkingIDX;
            }
            set
            {
                m_strMarkingIDX = value;
            }
        }
        private bool m_bMarkingSKIP;
        public bool bMarkingSKIP
        {
            get { return m_bMarkingSKIP;}
            set { m_bMarkingSKIP=value;}
        }
        private int m_iZeroPadCount;
        public int iZeroPadCount
        {
            get { return m_iZeroPadCount; }
            set { m_iZeroPadCount = value; }
        }

        private DataMatrix.DM m_pDataMatrix = null;
        public DataMatrix.DM pDataMatrix
        {
            get
            {
                return m_pDataMatrix;
            }
            set
            {
                m_pDataMatrix = value;
            }
        }
        private EzInaVision.GDV.MatchResult m_pMatchResult;
        public EzInaVision.GDV.MatchResult pMatchResult
        {
            get
            {
                return m_pMatchResult;
            }
            set
            {
                m_pMatchResult = value;
            }
        }
        private EzInaVision.GDV.MatrixCodeResult m_pMatrixCodeResult;
        public EzInaVision.GDV.MatrixCodeResult pMatrixCodeResult
        {
            get
            {
                return m_pMatrixCodeResult;
            }
            set
            {
                m_pMatrixCodeResult = value;
            }
        }

        private bool m_bInProcess;
        public bool bInProcess
        {
            get { return m_bInProcess; }
            set { m_bInProcess = value; }
        }
        private bool m_bProcessDone;
        public bool bProcessDone
        {
            get { return m_bProcessDone; }
            set { m_bProcessDone = value; }
        }

        private bool m_bPosInspExecuted;
        public bool bPosInspExecuted
        {
            get { return m_bPosInspExecuted; }
            set { m_bPosInspExecuted = value; }
        }
        private bool m_bMarkingDone;
        public bool bMarkingDone
        {
            get { return m_bMarkingDone; }
            set { m_bMarkingDone = true; }
        }
        private bool m_bMarkingInspExecuted;
        public bool bMarkingInspExecuted
        {
            get { return m_bMarkingInspExecuted; }
            set { m_bMarkingInspExecuted = value; }
        }
        public bool m_bMarkingInspManualOK = false;
        public bool bMarkingInspManualOK
        {
            get { return m_bMarkingInspManualOK; }
            set { m_bMarkingInspManualOK = value; }
        }
        private bool m_bMESUnitReport=false;
        public bool bMESUnitReport
        {
            get { return m_bMESUnitReport;}
            set {  m_bMESUnitReport = value; }
        }

        private DKT_MES_MarkingReportInfo m_pMarkingReportInfo;
        public DKT_MES_MarkingReportInfo pMarkingReportInfo
        {
            get { return m_pMarkingReportInfo; }
        }
        public void SetMarkingReportInfo(DKT_MES_MarkingReportInfo a_Data)
        {
            m_pMarkingReportInfo = a_Data != null ? a_Data.Clone() : null;
        }
        private DateTime m_MarkingDoneTime;
        public DateTime MarkingDoneTime
        {
            get { return m_MarkingDoneTime; }
            set { m_MarkingDoneTime = value; }
        }

        public RunningDataItem()
        {
            Clear();
            m_iIDX = -1;
        }
        public void Clear()
        {
            m_pDataMatrix = null;
            m_pMatchResult = null;
            m_bMarkingInspExecuted = false;
            m_bMarkingDone = false;
            m_bPosInspExecuted = false;
            if (m_pMatrixCodeResult != null)
                m_pMatrixCodeResult.Clear();
            m_pMatchResult = null;
            m_bInProcess = false;
            m_bProcessDone = false;
            m_iMarkingIDX = 0;
            m_bMarkingInspManualOK = false;
            MarkingDoneTime = DateTime.MinValue;
            m_iZeroPadCount = 0;
            m_bMESUnitReport=false;
            m_strMarkingIDX="";
            
        }
        public RunningDataItem Clone()
        {
            RunningDataItem pRet = new RunningDataItem();
            pRet.m_pDataMatrix = this.m_pDataMatrix != null ? this.m_pDataMatrix.Clone() : null;
            pRet.m_pMatchResult = this.m_pMatchResult != null ? (EzInaVision.GDV.MatchResult)this.m_pMatchResult.Clone() : null;
            pRet.m_bMarkingInspExecuted = this.m_bMarkingInspExecuted;
            pRet.m_bMarkingDone = this.m_bMarkingDone;
            pRet.m_bPosInspExecuted = this.m_bPosInspExecuted;
            pRet.m_iIDX = this.m_iIDX;
            pRet.m_pMatrixCodeResult = this.m_pMatrixCodeResult != null ? this.m_pMatrixCodeResult.Clone() : null;
            pRet.m_bInProcess = this.m_bInProcess;
            pRet.m_bProcessDone = this.m_bProcessDone;
            pRet.m_bMarkingInspManualOK = this.m_bMarkingInspManualOK;
            pRet.m_MarkingDoneTime = this.m_MarkingDoneTime;
            pRet.m_iZeroPadCount = this.m_iZeroPadCount;
            return pRet;
        }
    }
}
