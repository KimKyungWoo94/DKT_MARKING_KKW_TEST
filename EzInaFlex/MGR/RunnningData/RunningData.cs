using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public class RunningData
    {
        private List<RunningDataItem> m_pDatalist;
        private RunningDataItem m_pGuideBarData;
        private List<bool> m_pDrawMouseOver;
        private static Font m_pDrawStringFont;
        private string m_strJIGCode = "";
        public string strJIGCode
        {
            get { return m_strJIGCode; }
            set { m_strJIGCode = value; }
        }
        private string m_strLotCardCode = "";
        public string strLotCardCode
        {
            get { return m_strLotCardCode; }
            set { m_strLotCardCode = value; }
        }

        public string strMarkingInfoType
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.strMarkkingInfo_Type : ""; }
        }
        public string strMESCode
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.strMarkingInfo_MarkingData : ""; }
        }
        public string strMESGuideBarCode
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.strGuideBarCode : ""; }
        }
        private string strMarkingInfo
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.strMarkingInfo_MarkingData : ""; }
        }
        public bool bTargetDMCstringExist
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.bTargetDMCstringExist : false; }
        }
        public bool bGuideBarDMCExist
        {
            get { return m_pMES_MarkingStartInfo != null ? m_pMES_MarkingStartInfo.bGuideBarCodeMarking : false; }
        }
        public bool GetTargetDMCstring(int iIDX,out string a_outString)
        {
            a_outString="";
            if (bTargetDMCstringExist&& m_pMES_MarkingStartInfo.pMarkingTargetNumList.Count>0&&
                iIDX >-1 && iIDX < m_pMES_MarkingStartInfo.pMarkingTargetNumList.Count)
            {
                a_outString=m_pMES_MarkingStartInfo.pMarkingTargetNumList[iIDX];
                return true;
            }
            return false;
        }
        private bool m_bMES_MarkingStartSuccess;
        public bool bMES_MarkingStartSuccess
        {
            get { return m_bMES_MarkingStartSuccess; }
            set { m_bMES_MarkingStartSuccess = value; }
        }
        private bool m_bMES_MarkingEndSuccess;
        public bool bMES_MarkingEndSuccess
        {
            get { return m_bMES_MarkingEndSuccess; }
            set { m_bMES_MarkingEndSuccess = value; }
        }
        public DKT_MES_MarkingStartInfo pMES_MarkingStartInfo
        {
            get { return m_pMES_MarkingStartInfo; }
        }

        private DKT_MES_MarkingStartInfo m_pMES_MarkingStartInfo;
        public void SetMarkingStartInfoData(DKT_MES_MarkingStartInfo a_Data)
        {
            m_pMES_MarkingStartInfo = a_Data != null ? a_Data.Clone() : null;
        }
        private DateTime m_MES_MarkingStartSendCompleteTime;
        public DateTime MES_MarkingStartSendCompleteTime
        {
            get
            {
                return m_MES_MarkingStartSendCompleteTime;
            }
            set
            {
                m_MES_MarkingStartSendCompleteTime = value;
            }
        }
        private DKT_MES_MarkingEndInfo m_pMES_MarkingEndInfo;
        public void SetMarkingEndInfoData(DKT_MES_MarkingEndInfo a_Data)
        {
            m_pMES_MarkingEndInfo = a_Data != null ? a_Data.Clone() : null;
        }
        private int m_iZeroPadCount;
        public int iZeroPadCount
        {
            get { return m_iZeroPadCount; }
            set
            {
                m_iZeroPadCount = value;
            }
        }
        private int m_iRepeatMarkingCount;
        public int iRepeatMarkingCount
        {
            get { return m_iRepeatMarkingCount; }
            set
            {
                m_iRepeatMarkingCount = value;
            }
        }
        private FA.eRecipeRowProgressDir m_ePROCRowDir;
        public FA.eRecipeRowProgressDir ePROCRowDir
        {
            get { return m_ePROCRowDir; }
            set { m_ePROCRowDir = value; }
        }
        private FA.eRecipeColProgressDir m_ePROCColDir;
        public FA.eRecipeColProgressDir ePROCColDir
        {
            get { return m_ePROCColDir; }
            set { m_ePROCColDir = value; }
        }
        private float m_fRowPitch;
        public float fRowPitch
        {
            get { return m_fRowPitch; }
            set { m_fRowPitch = value; }
        }
        private float m_fColPitch;
        public float fColPitch
        {
            get { return m_fColPitch; }
            set { m_fColPitch = value; }
        }
        private int m_iRowCount;
        public int iRowCount
        {
            get { return m_iRowCount; }
            set { m_iRowCount = value; }
        }
        private int m_iColCount;
        public int iColCount
        {
            get { return m_iColCount; }
            set { m_iColCount = value; }
        }
        private int m_iMultiArrayCount;
        public int iMultiArrayCount
        {
            get { return m_iMultiArrayCount; }
            set { m_iMultiArrayCount = value; }
        }
        private double m_fMultiArrayGap;
        public double fMultiArrayGap
        {
            get { return m_fMultiArrayGap; }
            set { m_fMultiArrayGap = value; }
        }
        private FA.eRecieMultiArrayDir m_eMultiArrayDIR;
        public FA.eRecieMultiArrayDir eMultiArrayDIR
        {
            get { return m_eMultiArrayDIR; }
            set { m_eMultiArrayDIR = value; }
        }

        private int m_iDrawAreaMargin = 2;
        private int m_iDrawMultiArrayMargin = 2;
        public int iDrawAreaMargin
        {
            get { return m_iDrawAreaMargin; }
            set { m_iDrawAreaMargin = value; }
        }

        private double m_fFirstProductPosX;
        private double m_fFirstProductPosY;
        private double m_fFirstProductPosZ;
        public double fFirstProductPosX
        {
            get { return m_fFirstProductPosX; }
            set { m_fFirstProductPosX = value; }
        }
        public double fFirstProductPosY
        {
            get { return m_fFirstProductPosY; }
            set { m_fFirstProductPosY = value; }
        }
        public double fFirstProductPosZ
        {
            get { return m_fFirstProductPosZ; }
            set { m_fFirstProductPosZ = value; }
        }

        public bool GetMovingPosition(int a_Row, int a_Col,
        out double a_outBufXPos, out double a_outBufYPos, out double a_outBufZPos)
        {
            a_outBufXPos = 0.0;
            a_outBufYPos = 0.0;
            a_outBufZPos = 0.0;

            int TotalRow = m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iRowCount * m_iMultiArrayCount : m_iRowCount;
            int TotalCol = m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iColCount * m_iMultiArrayCount : m_iColCount;



            int iMultiArrayNO = 0;
            int iMultiArrayRow = -1;
            int iMultiArrayCol = -1;
            bool bReverseMultiArrayDir = m_fMultiArrayGap < 0;
            if (a_Row < 0 || a_Col < 0)
                return false;

            if (a_Row >= TotalRow && a_Col >= TotalCol)
                return false;






            a_outBufXPos = m_fFirstProductPosX;
            a_outBufYPos = m_fFirstProductPosY;
            a_outBufZPos = m_fFirstProductPosZ;

            if (a_Row >= m_iRowCount || a_Col >= m_iColCount)
            {
                iMultiArrayNO = m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? a_Col / m_iColCount : a_Row / m_iRowCount;

                switch (m_eMultiArrayDIR)
                {
                    case eRecieMultiArrayDir.ROW:
                        {
                            a_outBufXPos += -m_fMultiArrayGap * iMultiArrayNO + (
                                bReverseMultiArrayDir == true ?
                                m_fColPitch * (m_iColCount - 1) : -m_fColPitch * (m_iColCount - 1));
                            iMultiArrayRow = a_Row;
                            iMultiArrayCol = a_Col % m_iColCount;

                        }

                        break;
                    case eRecieMultiArrayDir.COLUMN:
                        {
                            a_outBufYPos += m_fMultiArrayGap * iMultiArrayNO + (
                                bReverseMultiArrayDir == false ?
                                m_fRowPitch * (m_iRowCount - 1) : -m_fRowPitch * (m_iRowCount - 1));

                            iMultiArrayRow = a_Row % m_iRowCount;
                            iMultiArrayCol = a_Col;
                        }
                        break;
                }
            }
            else
            {
                iMultiArrayRow = a_Row;
                iMultiArrayCol = a_Col;
            }
            double fRowMovingPitch = 0.0;
            double fColMovingPitch = 0.0;

            fRowMovingPitch = m_fRowPitch;
            fRowMovingPitch *= iMultiArrayRow;

            fColMovingPitch = m_fColPitch;
            fColMovingPitch *= iMultiArrayCol;
            switch (m_ePROCRowDir)
            {
                case eRecipeRowProgressDir.UP:
                    a_outBufYPos += fRowMovingPitch;
                    break;
                case eRecipeRowProgressDir.DOWN:
                    a_outBufYPos -= fRowMovingPitch;
                    break;
            }
            switch (RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>())
            {
                case eRecipeColProgressDir.LEFT:
                    a_outBufXPos += fColMovingPitch;
                    break;
                case eRecipeColProgressDir.RIGHT:
                    a_outBufXPos -= fColMovingPitch;
                    break;
            }
            return true;
        }
        public RunningData()
        {
            m_pDatalist = new List<RunningDataItem>();
            m_pDrawMouseOver = new List<bool>();
            if (m_pDrawStringFont == null)
                m_pDrawStringFont = new Font("Century Gothic", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            m_pMES_MarkingStartInfo = null;
            m_pMES_MarkingEndInfo = null;

        }
        public RunningData Clone()
        {
            RunningData pRet = new RunningData();
            m_pGuideBarData=new RunningDataItem();
            pRet.m_pDatalist = this.m_pDatalist.ConvertAll(o => o.Clone());
            pRet.m_pGuideBarData=this.m_pGuideBarData.Clone();
            pRet.m_pDrawMouseOver = this.m_pDrawMouseOver.ConvertAll(o => o);
            pRet.m_strJIGCode = this.m_strJIGCode;
            pRet.m_strLotCardCode = this.m_strLotCardCode;
            pRet.m_ePROCRowDir = this.m_ePROCRowDir;
            pRet.m_ePROCColDir = this.m_ePROCColDir;
            pRet.m_fRowPitch = this.m_fRowPitch;
            pRet.m_fColPitch = this.m_fColPitch;
            pRet.m_iRowCount = this.m_iRowCount;
            pRet.m_iColCount = this.m_iColCount;
            pRet.m_fFirstProductPosX = this.m_fFirstProductPosX;
            pRet.m_fFirstProductPosY = this.m_fFirstProductPosY;
            pRet.m_fFirstProductPosZ = this.m_fFirstProductPosZ;
            pRet.m_pMES_MarkingStartInfo = this.m_pMES_MarkingStartInfo != null ? this.m_pMES_MarkingStartInfo.Clone() : null;
            pRet.m_pMES_MarkingEndInfo = this.m_pMES_MarkingEndInfo != null ? this.m_pMES_MarkingEndInfo.Clone() : null;
            pRet.m_bMES_MarkingStartSuccess = this.m_bMES_MarkingStartSuccess;
            pRet.m_bMES_MarkingEndSuccess = this.m_bMES_MarkingEndSuccess;
            pRet.m_iZeroPadCount = this.m_iZeroPadCount;
            pRet.m_iRepeatMarkingCount = this.m_iRepeatMarkingCount;
            pRet.m_iMultiArrayCount = this.m_iMultiArrayCount;
            pRet.m_fMultiArrayGap = this.m_fMultiArrayGap;
            pRet.m_eMultiArrayDIR = this.m_eMultiArrayDIR;
            return pRet;
        }
        public RunningDataItem pGuideBarDMC
        {
            get { return m_pGuideBarData;}
        }
        public void AddData(RunningDataItem a_Value)
        {
            m_pDatalist.Add(a_Value);
            m_pDrawMouseOver.Add(false);
            m_pDatalist[m_pDatalist.Count - 1].iIDX = m_pDatalist.Count - 1;
        }

        public void ReAllocDataList()
        {
            int iMaxCount = m_iRowCount * m_iColCount * m_iMultiArrayCount;
            ClearDataList();
            for (int i = 0; i < iMaxCount; i++)
            {
                AddData(new RunningDataItem());
                m_pDatalist[m_pDatalist.Count - 1].iIDX = i;
            }
        }
        public void ClearDataList()
        {
            m_pDatalist.Clear();
            m_pDrawMouseOver.Clear();
        }
        public void ClearProcessDataList()
        {
            if (m_pDatalist != null && m_pDatalist.Count > 0)
            {
                for (int i = 0; i < m_pDatalist.Count; i++)
                {
                    m_pDatalist[i].Clear();
                }
            }
        }
        public int iProcessDataListCount
        {
            get { return m_pDatalist != null ? m_pDatalist.Count : 0; }
        }
        /// <summary>
        /// JIG , Lot , MES Code 
        /// </summary>
        public void ClearCodeData()
        {
            m_strLotCardCode = "";
            m_strJIGCode = "";
            m_pMES_MarkingStartInfo = null;
            m_pMES_MarkingEndInfo = null;
            m_bMES_MarkingStartSuccess = false;
            m_bMES_MarkingEndSuccess = false;
            m_iRepeatMarkingCount = 1;
        }
        public bool IsExistError(bool a_bIncludePosINSP = false)
        {
            for (int i = 0; i < m_pDatalist.Count; i++)
            {
                if (m_pDatalist[i].bProcessDone)
                {
                    if (a_bIncludePosINSP)
                    {
                        if (m_pDatalist[i].bMarkingInspExecuted)
                        {
                            if (m_pDatalist[i].pMatchResult == null)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    if (m_pDatalist[i].pMatrixCodeResult != null && m_pDatalist[i].pDataMatrix != null)
                    {
                        if (m_pDatalist[i].pMatrixCodeResult.m_bFound)
                        {
                            if (string.Equals(m_pDatalist[i].pDataMatrix.DatamatrixText,
                                            m_pDatalist[i].pMatrixCodeResult.m_strDecodedString) == false
                                            )
                            {
                                return true;
                            }
                            else
                            {
                                if (m_pDatalist[i].bMarkingInspManualOK == true)
                                    return true;
                            }
                        }
                        else
                        {
                            if (m_pDatalist[i].bMarkingInspManualOK == true)
                                return true;
                        }
                    }
                    else
                    {
                        if (m_pDatalist[i].bMarkingInspManualOK == true)
                            return true;
                    }
                }
            }
            return false;
        }
        public RunningDataItem this[int a_IDX]
        {
            get
            {
                if (m_pDatalist.Count > 0 && a_IDX < m_pDatalist.Count)
                    return m_pDatalist[a_IDX];
                return null;

            }
        }
        public void MouseLeave()
        {
            for (int i = 0; i < m_pDrawMouseOver.Count; i++)
            {
                m_pDrawMouseOver[i] = false;
            }
        }
        public bool MouseClickTest(Control a_Control, Point a_MousePoint, out Point out_Pos, out int out_IDX)
        {
            out_Pos = new Point(-1, -1);
            out_IDX = -1;
            RectangleF pInspTempData = new RectangleF();
            if (m_pDatalist.Count == m_iRowCount * m_iColCount * m_iMultiArrayCount)
            {
                int iRowMultiArrayCount = m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iMultiArrayCount : 1;
                int iColMultiArrayCount = m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iMultiArrayCount : 1;
                int iTotalArrayCount = m_iColCount * m_iRowCount;


                float fProductWidth = m_fColPitch * 0.6f;
                float fProductHeight = m_fRowPitch * 0.6f;
                float fProductRowGap = m_fColPitch * 0.4f;
                float fProductColGap = m_fRowPitch * 0.4f;
                float fRealWidth = m_iDrawAreaMargin * 2 +
                        (m_iColCount * m_fColPitch) * iRowMultiArrayCount +
                        Math.Abs((float)m_fMultiArrayGap) * (iRowMultiArrayCount - 1);
                float fRealHeight = m_iDrawAreaMargin * 2 +
                        (m_iRowCount * m_fRowPitch) * iColMultiArrayCount +
                          Math.Abs((float)m_fMultiArrayGap) * (iColMultiArrayCount - 1);


                float ScaleX = fRealWidth == 0.0 ? 1 : a_Control.Width / fRealWidth;
                float ScaleY = fRealHeight == 0.0 ? 1 : a_Control.Height / fRealHeight;

                float fDrawArrayStartRowPos = 0.0f;
                float fDrawArrayStartColPos = 0.0f;
                float fDrawMultiArrayMarginRow = 0;

                float fDrawMultiArrayMarginCol = 0;
                bool bReverseMultiArrayDir = m_fMultiArrayGap < 0;
                int iArrayIdx = -1;
                float fDrawRowGap = 0;
                float fDrawColGap = 0;
                for (int MultiArray = 0; MultiArray < m_iMultiArrayCount; MultiArray++)
                {

                    if (m_iMultiArrayCount > 1)
                    {
                        switch (m_eMultiArrayDIR)
                        {
                            case eRecieMultiArrayDir.ROW:
                                {
                                    fDrawArrayStartRowPos = bReverseMultiArrayDir == true ?
                                        fRealWidth - m_iDrawAreaMargin - (m_iColCount * m_fColPitch * (MultiArray + 1)) :
                                        m_iDrawAreaMargin + MultiArray * m_iColCount * m_fColPitch;
                                    fDrawArrayStartColPos = m_iDrawAreaMargin;
                                    fDrawMultiArrayMarginRow = (float)m_fMultiArrayGap * MultiArray;
                                    fDrawMultiArrayMarginCol = 0;
                                }
                                break;
                            case eRecieMultiArrayDir.COLUMN:
                                {
                                    fDrawArrayStartRowPos = m_iDrawAreaMargin;
                                    fDrawArrayStartColPos = bReverseMultiArrayDir == false ?
                                         fRealHeight - m_iDrawAreaMargin - (m_iRowCount * m_fRowPitch * (MultiArray + 1)) :
                                        m_iDrawAreaMargin + MultiArray * m_iRowCount * m_fRowPitch;
                                    fDrawMultiArrayMarginRow = 0;
                                    fDrawMultiArrayMarginCol = (float)-m_fMultiArrayGap * MultiArray;
                                }
                                break;
                        }
                    }
                    else
                    {
                        fDrawArrayStartRowPos = m_iDrawAreaMargin;
                        fDrawArrayStartColPos = m_iDrawAreaMargin;
                    }


                    for (int Col = 0; Col < m_iColCount; Col++)
                    {
                        fDrawRowGap = m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? (m_iColCount * MultiArray + Col) * fProductRowGap : Col * fProductRowGap;
                        for (int Row = 0; Row < m_iRowCount; Row++)
                        {
                            fDrawColGap = m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? (m_iRowCount * MultiArray + Row) * fProductColGap : Row * fProductColGap;


                            pInspTempData.X =
                             fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                            (Col * m_fColPitch) + (fProductRowGap / 2);
                            pInspTempData.Y = fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                            (Row * m_fRowPitch) + (fProductColGap / 2);
                            pInspTempData.X *= ScaleX;
                            pInspTempData.Y *= ScaleY;
                            pInspTempData.Width = fProductWidth * ScaleX;
                            pInspTempData.Height = fProductHeight * ScaleY;
                            if (pInspTempData.Contains(a_MousePoint))
                            {
                                if (m_ePROCRowDir == FA.eRecipeRowProgressDir.DOWN)
                                {
                                    if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                    {
                                        out_Pos.X = (m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iRowCount * MultiArray : 0) + Row;
                                        out_Pos.Y = (m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iColCount * MultiArray : 0) + m_iColCount - Col - 1;
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col - 1) + (Row + 1);
                                    }
                                    else
                                    {
                                        out_Pos.X = (m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iRowCount * MultiArray : 0) + Row;
                                        out_Pos.Y = (m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iColCount * MultiArray : 0) + Col;
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * Col + Row + 1;
                                    }
                                }
                                else
                                {
                                    if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                    {
                                        out_Pos.X = (m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iRowCount * MultiArray : 0) + m_iRowCount - Row - 1;
                                        out_Pos.Y = (m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iColCount * MultiArray : 0) + m_iColCount - Col - 1;
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col) - Row;

                                    }
                                    else
                                    {
                                        out_Pos.X = (m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iRowCount * MultiArray : 0) + m_iRowCount - Row - 1;
                                        out_Pos.Y = (m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iColCount * MultiArray : 0) + Col;
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * Col + (m_iRowCount - Row);
                                    }
                                }
                                if (m_pDrawMouseOver != null && (iArrayIdx - 1) < m_pDrawMouseOver.Count)
                                {
                                    m_pDrawMouseOver[iArrayIdx - 1] = true;
                                    out_IDX = iArrayIdx - 1;
                                    //a_Control.Invalidate();
                                    return true;
                                }
                            }
                            else
                            {
                                if (m_ePROCRowDir == FA.eRecipeRowProgressDir.DOWN)
                                {
                                    if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                    {
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col - 1) + (Row + 1);

                                    }
                                    else
                                    {
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * Col + Row + 1;
                                    }
                                }
                                else
                                {
                                    if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                    {
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col) - Row;
                                    }
                                    else
                                    {
                                        iArrayIdx = iTotalArrayCount * MultiArray + m_iRowCount * Col + (m_iRowCount - Row);
                                    }
                                }
                                if (m_pDrawMouseOver != null && (iArrayIdx - 1) < m_pDrawMouseOver.Count)
                                {
                                    m_pDrawMouseOver[iArrayIdx - 1] = false;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }






        private float GetFontSize(string text, Graphics graphics, SizeF rect, string fontName, float maxFontSize = 32)
        {

            while (maxFontSize > 0)
            {
                using (var font = new Font(fontName, maxFontSize))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(
                            text,
                            font.FontFamily,
                            (int)font.Style,
                            font.Size,
                            PointF.Empty,
                            StringFormat.GenericDefault);
                        var calc = path.GetBounds();
                        if (rect.Width > rect.Height)
                        {
                            if (calc.Height > 0 && calc.Height < rect.Height)
                            {
                                maxFontSize -= 1;
                                break;
                            }
                        }
                        else
                        {
                            if (calc.Width > 0 && calc.Width < rect.Width)
                            {
                                //maxFontSize -= 0.3f;
                                break;
                            }
                        }
                    }
                }
                maxFontSize -= 1;
            }
            return maxFontSize;
        }


        public void DrawJIGMap(Control a_Control)
        {
            if (m_pDatalist.Count < m_iRowCount * m_iColCount * m_iMultiArrayCount)
                return;
            using (Graphics graphics = a_Control.CreateGraphics())
            {
                using (BufferedGraphics pDBufferdGraphics = BufferedGraphicsManager.Current.Allocate(graphics, a_Control.DisplayRectangle))
                {

                    using (Brush BackColor = new SolidBrush(a_Control.BackColor))
                    {
                        int iRowMultiArrayCount = m_eMultiArrayDIR == eRecieMultiArrayDir.ROW ? m_iMultiArrayCount : 1;
                        int iColMultiArrayCount = m_eMultiArrayDIR == eRecieMultiArrayDir.COLUMN ? m_iMultiArrayCount : 1;
                        float fProductWidth = m_fColPitch * 0.6f;
                        float fProductHeight = m_fRowPitch * 0.6f;
                        float fProductWidthHalf = fProductWidth / 2.0f;
                        float fProductHeightHalf = fProductHeight / 2.0f;
                        float fProductRowGap = m_fColPitch * 0.4f;
                        float fProductColGap = m_fRowPitch * 0.4f;
                        float fRealWidth = m_iDrawAreaMargin * 2 +
                                (m_iColCount * m_fColPitch) * iRowMultiArrayCount +
                                Math.Abs((float)m_fMultiArrayGap) * (iRowMultiArrayCount - 1);
                        float fRealHeight = m_iDrawAreaMargin * 2 +
                                (m_iRowCount * m_fRowPitch) * iColMultiArrayCount +
                                  Math.Abs((float)m_fMultiArrayGap) * (iColMultiArrayCount - 1);

                        float ScaleX = fRealWidth == 0.0 ? 1 : a_Control.Width / fRealWidth;
                        float ScaleY = fRealHeight == 0.0 ? 1 : a_Control.Height / fRealHeight;
                        float FontScale = ScaleX > ScaleY ? ScaleY : ScaleX;
                        string strDrawString = "";
                        int iArrayIndex = 0;
                        pDBufferdGraphics.Graphics.Clear(a_Control.BackColor);
                        pDBufferdGraphics.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                        //pDBufferdGraphics.Graphics.PageUnit=GraphicsUnit.Display;
                        //using (SolidBrush ProcessDoneBrush = new SolidBrush(Color.LimeGreen))
                        pDBufferdGraphics.Graphics.ScaleTransform(ScaleX, ScaleY);

                        float Fontsize = GetFontSize(string.Format("{0:000}", 1), pDBufferdGraphics.Graphics, new SizeF(fProductWidth, fProductHeight), m_pDrawStringFont.Name);
                        float FontsizeHalf = Fontsize / 2;


                        using (Font pStringDrawFont = new Font(m_pDrawStringFont.Name, Fontsize, FontStyle.Regular, m_pDrawStringFont.Unit))
                        using (SolidBrush ProcessDoneBrush = new SolidBrush(Color.LimeGreen))
                        using (SolidBrush InProcessBrush = new SolidBrush(Color.LightSkyBlue))
                        using (SolidBrush stringbrush = new SolidBrush(Color.Black))
                        using (SolidBrush stringFailbrush = new SolidBrush(Color.Red))
                        using (SolidBrush DMbrush = new SolidBrush(Color.Blue))
                        using (Pen DefaultPen = new Pen(Color.Black, 0.1f))
                        using (Pen MouseOverPen = new Pen(Color.Red, 0.1f))
                        {

                            float fFontOffsetX = fProductWidthHalf - pStringDrawFont.GetHeight() / 2;
                            float fFontOffsetY = fProductHeightHalf - pStringDrawFont.GetHeight() / 2;
                            int iTotalArrayCount = m_iColCount * m_iRowCount;
                            float fDrawArrayStartRowPos = 0;
                            float fDrawArrayStartColPos = 0;
                            bool bReverseMultiArrayDir = m_fMultiArrayGap < 0;
                            float fDrawMultiArrayMarginRow = 0;
                            float fDrawMultiArrayMarginCol = 0;
                            SolidBrush pStatusBrush = null;
                            for (int MultiArray = 0; MultiArray < m_iMultiArrayCount; MultiArray++)
                            {

                                if (m_iMultiArrayCount > 1)
                                {
                                    switch (m_eMultiArrayDIR)
                                    {
                                        case eRecieMultiArrayDir.ROW:
                                            {
                                                fDrawArrayStartRowPos = bReverseMultiArrayDir == true ?
                                                    fRealWidth - m_iDrawAreaMargin - (m_iColCount * m_fColPitch * (MultiArray + 1)) :
                                                    m_iDrawAreaMargin + MultiArray * m_iColCount * m_fColPitch;
                                                fDrawArrayStartColPos = m_iDrawAreaMargin;
                                                fDrawMultiArrayMarginRow = (float)m_fMultiArrayGap * MultiArray;
                                                fDrawMultiArrayMarginCol = 0;
                                            }
                                            break;
                                        case eRecieMultiArrayDir.COLUMN:
                                            {
                                                fDrawArrayStartRowPos = m_iDrawAreaMargin;
                                                fDrawArrayStartColPos = bReverseMultiArrayDir == false ?
                                                     fRealHeight - m_iDrawAreaMargin - (m_iRowCount * m_fRowPitch * (MultiArray + 1)) :
                                                    m_iDrawAreaMargin + MultiArray * m_iRowCount * m_fRowPitch;
                                                fDrawMultiArrayMarginRow = 0;
                                                fDrawMultiArrayMarginCol = (float)-m_fMultiArrayGap * MultiArray;

                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    fDrawArrayStartRowPos = m_iDrawAreaMargin;
                                    fDrawArrayStartColPos = m_iDrawAreaMargin;
                                }
                                for (int Col = 0; Col < m_iColCount; Col++)
                                {
                                    for (int Row = 0; Row < m_iRowCount; Row++)
                                    {
                                        if (m_ePROCRowDir == FA.eRecipeRowProgressDir.DOWN)
                                        {
                                            if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                            {
                                                iArrayIndex = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col - 1) + (Row + 1);
                                            }
                                            else
                                            {
                                                iArrayIndex = iTotalArrayCount * MultiArray + m_iRowCount * Col + Row + 1;
                                            }
                                        }
                                        else
                                        {
                                            if (m_ePROCColDir == FA.eRecipeColProgressDir.LEFT)
                                            {
                                                iArrayIndex = iTotalArrayCount * MultiArray + m_iRowCount * (m_iColCount - Col) - Row;

                                            }
                                            else
                                            {
                                                iArrayIndex = iTotalArrayCount * MultiArray + m_iRowCount * Col + (m_iRowCount - Row);
                                            }
                                        }
                                        if (m_pDrawMouseOver != null && (iArrayIndex - 1) < m_pDrawMouseOver.Count)
                                        {
                                            if (m_pDatalist[iArrayIndex - 1] != null)
                                            {
                                                pStatusBrush = null;
                                                if (m_pDatalist[iArrayIndex - 1].bInProcess == true)
                                                {
                                                    pStatusBrush = InProcessBrush;
                                                }
                                                else
                                                {
                                                    if (m_pDatalist[iArrayIndex - 1].bProcessDone)
                                                    {
                                                        pStatusBrush = ProcessDoneBrush;
                                                    }
                                                }
                                                if (pStatusBrush != null)
                                                {
                                                    pDBufferdGraphics.Graphics.FillRectangle(pStatusBrush,
                                                     fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                                     (Col * m_fColPitch) + (fProductRowGap / 2),
                                                     fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                                     (Row * m_fRowPitch) + (fProductColGap / 2),
                                                    (float)fProductWidth - (fProductWidth * 0.02f),
                                                    (float)fProductHeight - (fProductHeight * 0.02f)
                                                    );
                                                }
                                            }
                                            pDBufferdGraphics.Graphics.DrawRectangle(
                                            m_pDrawMouseOver[iArrayIndex - 1] == false ? DefaultPen : MouseOverPen,
                                            fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                            (Col * m_fColPitch) + (fProductRowGap / 2),
                                            fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                            (Row * m_fRowPitch) + (fProductColGap / 2),
                                            (float)fProductWidth,
                                            (float)fProductHeight
                                            );
                                            strDrawString = string.Format("{0}", iArrayIndex);
                                            if (m_pDatalist[iArrayIndex - 1] != null && m_pDatalist[iArrayIndex - 1].pMatrixCodeResult != null)
                                            {
                                                pDBufferdGraphics.Graphics.DrawString(
                                                strDrawString,
                                                pStringDrawFont,
                                                m_pDatalist[iArrayIndex - 1].pMatrixCodeResult.m_bFound == true ?
                                                DMbrush : stringbrush,
                                                fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                                (Col * m_fColPitch) + (fProductRowGap / 2) + fFontOffsetX,
                                                fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                                (Row * m_fRowPitch) + (fProductColGap / 2) + fFontOffsetY
                                               );
                                            }
                                            else
                                            {

                                                if (m_pDatalist[iArrayIndex - 1] != null && m_pDatalist[iArrayIndex - 1].bPosInspExecuted)
                                                {
                                                    pDBufferdGraphics.Graphics.DrawString(strDrawString, pStringDrawFont,
                                                    m_pDatalist[iArrayIndex - 1].pMatchResult != null ?
                                                    stringbrush :
                                                    stringFailbrush,
                                                    fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                                    (Col * m_fColPitch) + (fProductRowGap / 2) + fFontOffsetX,
                                                    fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                                    (Row * m_fRowPitch) + (fProductColGap / 2) + fFontOffsetY
                                                    );
                                                }
                                                else
                                                {
                                                    pDBufferdGraphics.Graphics.DrawString(strDrawString, pStringDrawFont, stringbrush,
                                                    fDrawMultiArrayMarginRow + fDrawArrayStartRowPos +
                                                    (Col * m_fColPitch) + (fProductRowGap / 2) + fFontOffsetX,
                                                    fDrawMultiArrayMarginCol + fDrawArrayStartColPos +
                                                    (Row * m_fRowPitch) + (fProductColGap / 2) + fFontOffsetY
                                                    );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            pDBufferdGraphics.Render(graphics);
                        }

                    }
                }
            }
        }

    }
}
