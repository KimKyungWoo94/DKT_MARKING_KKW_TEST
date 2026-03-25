using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{

    public class DKT_MES_MarkingStartInfo
    {
        public DKT_MES_MarkingStartInfo()
        {
            m_strMarkingInfoOrignData = "";
            m_strGuideBarCode = "";
        }

        public DKT_MES_MarkingStartInfo Clone()
        {
            DKT_MES_MarkingStartInfo pRet = new DKT_MES_MarkingStartInfo();
            pRet.m_strMarkingInfoOrignData = this.m_strMarkingInfoOrignData;
            pRet.m_strCommMSG = this.m_strCommMSG;
            pRet.m_strMarkingInfo_Type = this.m_strMarkingInfo_Type;
            pRet.m_strMarkingInfo_ModelName = this.m_strMarkingInfo_ModelName;
            pRet.m_strMarkingInfo_MarkingData = this.m_strMarkingInfo_MarkingData;
            pRet.m_iMarkingInfo_StartNo = this.m_iMarkingInfo_StartNo;
            pRet.m_iMarkingInfo_EndNo = this.m_iMarkingInfo_EndNo;
            pRet.m_iMarkingInfo_Cipher = this.m_iMarkingInfo_Cipher;
            pRet.m_bMarkingInfo_ZeroPadSame = this.m_bMarkingInfo_ZeroPadSame;
            pRet.m_bMarkingInfo_ZeroPadLengthOK = this.m_bMarkingInfo_ZeroPadLengthOK;
            pRet.m_iMarkingInfo_ZeroPad = this.m_iMarkingInfo_ZeroPad;
            pRet.m_strGuideBarCode = this.m_strGuideBarCode;
            pRet.m_pMarkingTargetNumList = this.m_pMarkingTargetNumList.ConvertAll(x => x);
            pRet.m_bTargetDMCstringExist = this.m_bTargetDMCstringExist; //2026.03.23 KKW MES 데이터와 시리얼 다름 현상(BV타입)
            pRet.m_bGuideBarCodeMarking = this.m_bGuideBarCodeMarking;//2026.03.23 KKW MES 데이터와 시리얼 다름 현상(AV타입)
            return pRet;
        }

        public bool SetMarkingInfoData(string a_strData)
        {
            /*
             * [데이터 매핑 참조]
             * * 파라미터 구성:
             * IN      (string)
             * OUT     (string)
             * OUT     (string)
             * OUT     (string)
             *
             * 케이스 1 (V-Type):
             * string val1 = "[V, SDI_R0 REV3.0, P01100070ABVSAYMMDDA, 00001, 000FL, 26]";
             * string status = "OK";
             * string result = "RETURN VALUE : " + P_VALUE;
             *
             * 케이스 2 (BV-Type):
             * string val2 = "[BV, SDI_R0 REV3.0, P01100070ABVSAYMMDDA, 00001, 00002, 00003, 00004, 00005, 00006, 00022, 00123,, 00124,";
             * string status = "OK";
             * string result = "RETURN VALUE : " + P_VALUE;
            */

            // 2026.03.12 KKW
            // MES 통신 테스트 중 데이터 오류 발생 시 차단 로직 추가(데이터가 제대로 다 파싱이 안되는데도 성공이라고 판단함)
            if (!a_strData.Trim().EndsWith("]"))
                return false;

            m_strMarkingInfoOrignData = "";

            m_strMarkingInfoOrignData = a_strData.Clone() as string;
            m_strMarkingInfoOrignData = m_strMarkingInfoOrignData.Trim();
            m_strMarkingInfoOrignData = m_strMarkingInfoOrignData.TrimStart('[');
            m_strMarkingInfoOrignData = m_strMarkingInfoOrignData.TrimEnd(']');
            m_strGuideBarCode = "";
            m_bGuideBarCodeMarking = false;
            m_iMarkingInfo_StartNo = 0;
            m_iMarkingInfo_EndNo = 0;
            string[] strMarkingData = m_strMarkingInfoOrignData.Split(new char[] { ',' }, StringSplitOptions.None);
            if (strMarkingData.Length >= 6)
            {
                m_strMarkingInfo_Type = strMarkingData[0].Trim();
                m_strMarkingInfo_ModelName = strMarkingData[1].Trim();

                if (m_strMarkingInfo_Type.ToUpper().Equals("V"))
                {
                    m_bTargetDMCstringExist = false;
                    m_strMarkingInfo_MarkingData = strMarkingData[2].Trim();
                    m_iMarkingInfo_StartNoLength = strMarkingData[3].Trim().Length;
                    m_iMarkingInfo_EndNoLength = strMarkingData[4].Trim().Length;
                    m_bMarkingInfo_ZeroPadSame = strMarkingData[3].Trim().Length == strMarkingData[4].Trim().Length;
                    m_bMarkingInfo_ZeroPadLengthOK = strMarkingData[3].Trim().Length > 0 && strMarkingData[4].Trim().Length > 0;
                    m_iMarkingInfo_ZeroPad = strMarkingData[3].Trim().Length;

                    int.TryParse(strMarkingData[3].Trim(), out m_iMarkingInfo_StartNo);
                    int.TryParse(strMarkingData[4].Trim(), out m_iMarkingInfo_EndNo);
                    int.TryParse(strMarkingData[5].Trim(), out m_iMarkingInfo_Cipher);
                    return true;
                }
                else
                {
                    if (strMarkingData.Length > 6)
                    {
                        m_bTargetDMCstringExist = true;
                        int.TryParse(strMarkingData[strMarkingData.Length - 1].Trim(), out m_iMarkingInfo_Cipher);

                        int iNumStartIDX = -1;
                        int iNumEndIDX = -1;
                        bool bZeroPadSetOK = false;
                        string strNumData = "";
                        m_pMarkingTargetNumList.Clear();
                        m_bMarkingInfo_ZeroPadSame = true;//지정이라 의미없음
                        m_bMarkingInfo_ZeroPadLengthOK = true;//지정이라 의미 없음

                        if (m_strMarkingInfo_Type.ToUpper().Equals("BV"))
                        {
                            m_strMarkingInfo_MarkingData = strMarkingData[2].Trim();
                            iNumStartIDX = 3;
                            iNumEndIDX = strMarkingData.Length - 2;
                        }
                        else if (m_strMarkingInfo_Type.ToUpper().Equals("AV"))
                        {
                            m_bGuideBarCodeMarking = true;
                            m_strGuideBarCode = strMarkingData[2].Trim();
                            m_strMarkingInfo_MarkingData = strMarkingData[3].Trim();
                            iNumStartIDX = 4;
                            iNumEndIDX = strMarkingData.Length - 2;
                        }
                        if (iNumEndIDX - iNumStartIDX > 0)
                        {
                            for (int i = iNumStartIDX; i <= iNumEndIDX; i++)
                            {
                                strNumData = strMarkingData[i].Trim();
                                if (strNumData.Length > 0 && string.IsNullOrEmpty(strNumData) == false && bZeroPadSetOK == false)
                                {
                                    m_iMarkingInfo_ZeroPad = strNumData.Length;
                                    bZeroPadSetOK = true;
                                }
                                m_pMarkingTargetNumList.Add(strMarkingData[i].Trim());
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void SetCommMSG(string a_strData)
        {
            m_strCommMSG = a_strData;
        }

        private string m_strMarkingInfoOrignData;
        private string m_strMarkingInfo_Type;
        public string strMarkkingInfo_Type
        {
            get { return m_strMarkingInfo_Type; }
        }
        private string m_strMarkingInfo_ModelName;
        public string strMarkingInfo_ModelName
        {
            get { return m_strMarkingInfo_ModelName; }
        }
        private string m_strMarkingInfo_MarkingData;
        public string strMarkingInfo_MarkingData
        {
            get { return m_strMarkingInfo_MarkingData; }
        }
        private int m_iMarkingInfo_StartNo;
        public int iMarkingInfo_StartNo
        {
            get { return m_iMarkingInfo_StartNo; }
        }
        private int m_iMarkingInfo_EndNo;
        public int iMarkingInfo_EndNo
        {
            get { return m_iMarkingInfo_EndNo; }
        }
        private int m_iMarkingInfo_ZeroPad;
        public int iMarkingInfo_ZeroPad
        {
            get { return m_iMarkingInfo_ZeroPad; }
        }
        private bool m_bMarkingInfo_ZeroPadSame;
        public bool bMarkingInfo_ZeroPadSame
        {
            get { return m_bMarkingInfo_ZeroPadSame; }
        }
        private bool m_bMarkingInfo_ZeroPadLengthOK;
        public bool bMarkingInfo_ZeroPadLengthOK
        {
            get { return m_bMarkingInfo_ZeroPadLengthOK; }
        }

        private int m_iMarkingInfo_StartNoLength;
        public int iMarkingInfo_StartNoLength
        {
            get { return m_iMarkingInfo_StartNoLength; }
        }
        private int m_iMarkingInfo_EndNoLength;
        public int iMarkingInfo_EndNoLength
        {
            get { return m_iMarkingInfo_EndNoLength; }
        }
        private int m_iMarkingInfo_Cipher;
        public int iMarkingInfo_Cipher
        {
            get { return m_iMarkingInfo_Cipher; }
        }
        private string m_strCommMSG;
        public string strCommMSG
        {
            get { return m_strCommMSG; }
        }
        private List<string> m_pMarkingTargetNumList = new List<string>();
        public IReadOnlyList<string> pMarkingTargetNumList
        {
            get { return m_pMarkingTargetNumList; }
        }
        private string m_strGuideBarCode;
        public string strGuideBarCode
        {
            get { return m_strGuideBarCode; }
        }
        private bool m_bGuideBarCodeMarking = false;
        public bool bGuideBarCodeMarking
        {
            get { return m_bGuideBarCodeMarking; }
        }
        private bool m_bTargetDMCstringExist = false;
        public bool bTargetDMCstringExist
        {
            get { return m_bTargetDMCstringExist; }
        }

    }
    public class DKT_MES_MarkingEndInfo
    {
        public DKT_MES_MarkingEndInfo()
        {

        }
        public DKT_MES_MarkingEndInfo Clone()
        {
            DKT_MES_MarkingEndInfo pRet = new DKT_MES_MarkingEndInfo();
            pRet.m_strCommMSG = this.m_strCommMSG;
            return pRet;
        }
        public void SetCommMSG(string a_strData)
        {
            m_strCommMSG = a_strData;
        }
        private string m_strCommMSG;
        public string strCommMSG
        {
            get { return m_strCommMSG; }
        }
    }
    public class DKT_MES_MarkingReportInfo
    {
        public DKT_MES_MarkingReportInfo()
        {

        }
        public DKT_MES_MarkingReportInfo Clone()
        {
            DKT_MES_MarkingReportInfo pRet = new DKT_MES_MarkingReportInfo();
            pRet.m_strCommMSG = this.m_strCommMSG;
            return pRet;
        }
        public void SetCommMSG(string a_strData)
        {
            m_strCommMSG = a_strData;
        }
        private string m_strCommMSG;
        public string strCommMSG
        {
            get { return m_strCommMSG; }
        }
    }
}
