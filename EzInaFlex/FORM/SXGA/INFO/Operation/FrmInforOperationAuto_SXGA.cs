using EzIna.FA;
using EzIna.GUI.UserControls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmInforOperationAuto_SXGA : Form
    {
        System.Windows.Forms.Timer tmr;
        RunningData m_pDisPlayJIGData;
        bool m_bFirstJIGUpdate = false;
        Color m_ColorCurrentPageColor = Color.White;
        Color m_ColorPastPageColor = SystemColors.ControlDark;
        int m_iCurrentDataPage = -1;
        bool m_bProcessInfoRowAddEnable = false;
        Color m_DisplayOKColor = Color.LimeGreen;
        Color m_DisplayMisMatchColor = Color.Orange;
        Color m_DisplayFailColor = Color.Red;
        Color m_DisplayDefaultColor = Color.Black;

        public FrmInforOperationAuto_SXGA()
        {
            InitializeComponent();

            btnStart.Click += ButtonSwitch_Click;
            btnStop.Click += ButtonSwitch_Click;
            btnRest.Click += ButtonSwitch_Click;
            btnInitailize.Click += ButtonSwitch_Click;
            btnReject.Click += ButtonSwitch_Click;
            Panel_DrawMap.DoubleBuffered(true);
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 100;
            tmr.Enabled = false;
            tmr.Tick += Display;

        }
        private void FrmInforOperationAuto_SXGA_Load(object sender, EventArgs e)
        {
            FA.MGR.RecipeRunningData.InitDGV_DefaultParam(DGV_ProcessInfo);
            DGV_ProcessInfo.RowPostPaint += dgGrid_RowPostPaint;
        }

        private void dgGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.DefaultCellStyle.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        private void FrmInforOperationAuto_SXGA_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {

                tmr.Enabled = true;
            }
        }

        private void Display(object sender, EventArgs e)
        {
            try
            {
                tmr.Stop();
                UpdateDisplayCurrentJIGMAP();
                UpdateDisplayProcessInfo();

                tmr.Enabled = this.Visible;
            }
            catch (System.Exception ex)
            {
                FA.LOG.Fatal("FATAL", ex.StackTrace, ex.Message);
                tmr.Enabled = this.Visible;
            }
        }

        private void UpdateDisplayCurrentJIGMAP()
        {
            try
            {
                if (FA.MGR.RecipeRunningData != null)
                {
                    //lb.Text=FA.MGR.RecipeRunningData.strLotCardCode;
                    /*if (FA.MGR.RecipeRunningData.iDataListCount > 0)
                    {
                                    btn_Past_Prev.Enabled = true;
                                    btn_Past_Next.Enabled = true;
                    }
                    else
                    {
                                    btn_Past_Prev.Enabled = false;
                                    btn_Past_Next.Enabled = false;
                    }*/
                    //tb_PastIDX.Text = string.Format("{0} / {1}", m_iCurrentDataPage + 1, FA.MGR.RecipeRunningData?.iDataListCount);
                    //lb_LotNo_Data_Input.Text = FA.MGR.RecipeRunningData.strLotCardCode;
                    Panel_DrawMap.BackColor = m_pDisPlayJIGData == FA.MGR.RecipeRunningData.pCurrentProcessData ?
                    m_ColorCurrentPageColor : m_ColorPastPageColor;
                }
                if (m_pDisPlayJIGData != null)
                {
                    m_pDisPlayJIGData.DrawJIGMap(Panel_DrawMap);
                }
                else
                {
                    Panel_DrawMap.Invalidate();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void UpdateDisplayProcessInfo()
        {
            try
            {
                lb_Lot_Code_Data.Text = m_pDisPlayJIGData != null ? m_pDisPlayJIGData.strLotCardCode : "";
                lb_JIG_Code_Data.Text = m_pDisPlayJIGData != null ? m_pDisPlayJIGData.strJIGCode : "";
                lb_Marking_Code_data.Text = m_pDisPlayJIGData != null ?
                    $"{m_pDisPlayJIGData.strMESCode}[{m_pDisPlayJIGData.strMarkingInfoType}]" : "";
                if (m_pDisPlayJIGData != null)
                {
                    if (m_pDisPlayJIGData.bGuideBarDMCExist)
                    {
                        lb_Marking_GUIDE_BAR_CODE.Visible = true;
                        lb_Marking_GUIDE_BAR_CODE.Text = m_pDisPlayJIGData.strMESGuideBarCode;
                    }
                    else
                    {
                        lb_Marking_GUIDE_BAR_CODE.Visible = false;
                        lb_Marking_GUIDE_BAR_CODE.Text = "";
                    }
                }


                if (m_pDisPlayJIGData != null)
                {
                    DGV_ProcessInfo.SuspendDrawing();
                    m_bProcessInfoRowAddEnable = false;
                    if (m_pDisPlayJIGData.iProcessDataListCount != DGV_ProcessInfo.RowCount)
                    {
                        DGV_ProcessInfo.Rows.Clear();
                        m_bProcessInfoRowAddEnable = true;
                    }
                    for (int i = 0; i < m_pDisPlayJIGData.iProcessDataListCount; i++)
                    {
                        if (m_bProcessInfoRowAddEnable)
                        {
                            DGV_ProcessInfo.Rows.Add();
                        }
                        /*
                                a_DatagirdView.Columns.AddRange(
                                    CreateDataGridViewTextColumn("MarkingNo","",		(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
                                    CreateDataGridViewTextColumn("PosINSP","",			(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
                                    CreateDataGridViewTextColumn("Marking","" ,			(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
                                    CreateDataGridViewTextColumn("MarkingINSP", "", (int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25))
                                    );
                        */
                        if (m_pDisPlayJIGData != null && m_pDisPlayJIGData[i].pDataMatrix != null)
                        {
                            DGV_ProcessInfo.Rows[i].Cells["MarkingNo"].Value = string.Format("{0}", m_pDisPlayJIGData != null ? string.Format("{0}", m_pDisPlayJIGData[i].strMarkingIDX) : "");
                            //string.Format("{0}", m_pDisPlayJIGData != null ? string.Format("{0}({1})", m_pDisPlayJIGData[i].strMarkingIDX_TO_32, m_pDisPlayJIGData[i].iMarkingIDX.ToString()) : "");
                        }
                        else
                        {
                            DGV_ProcessInfo.Rows[i].Cells["MarkingNo"].Value = "";
                        }
                        #region			POS INSP
                        if (m_pDisPlayJIGData != null && m_pDisPlayJIGData[i].bPosInspExecuted)
                        {
                            if (m_pDisPlayJIGData[i].pMatchResult != null)
                            {
                                DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Style.ForeColor = m_DisplayOKColor;
                                DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Value = "OK";
                            }
                            else
                            {
                                DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Style.ForeColor = m_DisplayFailColor;
                                DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Value = "FAIL";
                            }
                        }
                        else
                        {
                            DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Style.ForeColor = m_DisplayDefaultColor;
                            DGV_ProcessInfo.Rows[i].Cells["PosINSP"].Value = "";
                        }
                        #endregion	POS INSP
                        #region			Makring


                        if (m_pDisPlayJIGData != null && m_pDisPlayJIGData[i].bMarkingSKIP == false)
                        {
                            if (m_pDisPlayJIGData != null && m_pDisPlayJIGData[i].bMarkingDone)
                            {
                                if (m_pDisPlayJIGData[i].pMatchResult != null)
                                {
                                    DGV_ProcessInfo.Rows[i].Cells["Marking"].Style.ForeColor = m_DisplayOKColor;
                                    DGV_ProcessInfo.Rows[i].Cells["Marking"].Value = "DONE";
                                }
                                else
                                {
                                    DGV_ProcessInfo.Rows[i].Cells["Marking"].Style.ForeColor = m_DisplayDefaultColor;
                                    DGV_ProcessInfo.Rows[i].Cells["Marking"].Value = "";
                                }


                            }
                            else
                            {
                                DGV_ProcessInfo.Rows[i].Cells["Marking"].Style.ForeColor = m_DisplayDefaultColor;
                                DGV_ProcessInfo.Rows[i].Cells["Marking"].Value = "";
                            }
                        }
                        else
                        {
                            DGV_ProcessInfo.Rows[i].Cells["Marking"].Style.ForeColor = m_DisplayDefaultColor;
                            DGV_ProcessInfo.Rows[i].Cells["Marking"].Value = "SKIP";
                        }

                        #endregion	Marking
                        #region			Marking INSP
                        if (m_pDisPlayJIGData != null && m_pDisPlayJIGData[i].bMarkingInspExecuted)
                        {
                            if (m_pDisPlayJIGData[i].bMarkingSKIP == false)
                            {
                                if (m_pDisPlayJIGData[i].bMarkingDone)
                                {

                                    if (m_pDisPlayJIGData[i].pMatrixCodeResult != null)

                                    {
                                        if (m_pDisPlayJIGData[i].pMatrixCodeResult.m_bFound == true)
                                        {
                                            if (string.Equals(m_pDisPlayJIGData[i].pDataMatrix.DatamatrixText,
                                                    m_pDisPlayJIGData[i].pMatrixCodeResult.m_strDecodedString))
                                            {
                                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayOKColor;
                                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "OK";
                                            }
                                            else
                                            {
                                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayMisMatchColor;
                                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "MIS Match";
                                            }
                                        }
                                        else
                                        {
                                            DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayFailColor;
                                            DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "Not Found";
                                        }
                                    }
                                    else
                                    {
                                        DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayFailColor;
                                        DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "Not Found";
                                    }
                                }
                                else
                                {
                                    DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayDefaultColor;
                                    DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "";
                                }
                            }
                            else
                            {
                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayDefaultColor;
                                DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "SKIP";
                            }
                        }
                        else
                        {
                            DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Style.ForeColor = m_DisplayDefaultColor;
                            DGV_ProcessInfo.Rows[i].Cells["MarkingINSP"].Value = "";
                        }
                        #endregion	Marking INSP
                    }
                    DGV_ProcessInfo.ResumeDrawing();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ButtonSwitch_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text.ToUpper())
            {
                case "START":
                    {
                        if (string.IsNullOrEmpty(FA.MGR.RecipeRunningData.strLotCardCode) == false &&
                     string.IsNullOrWhiteSpace(FA.MGR.RecipeRunningData.strLotCardCode) == false
                        )
                        {
                            if (FA.MGR.RunMgr.eRunMode == DEF.eRunMode.Stop)
                            {
                                if (MsgBox.Confirm("Would you like Start??", this))
                                {
                                    //tb_LOT_CODE_INPUT.ReadOnly=true;
                                    FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.START);
                                }
                            }
                            else
                            {
                                MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                            }
                        }
                        else
                        {
                            MsgBox.Error("Lot Code Empty , First Input Lot Code");
                        }

                    }
                    break;
                case "STOP":
                    {
                        FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.STOP);
                        FA.LOG.BTN_Event("", "Process Stop BTN Pushed");
                    }

                    break;
                case "RESET":
                    {
                        if (FA.MGR.RunMgr.eRunMode == DEF.eRunMode.Stop ||
                        FA.MGR.RunMgr.eRunMode == DEF.eRunMode.Jam ||
                        FA.MGR.RunMgr.eRunMode == DEF.eRunMode.ToStop
                        )
                        {
                            if (MsgBox.Confirm("Would you like Reset??", this))
                            {
                                FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.RESET);
                            }
                        }
                        else
                        {
                            MsgBox.Error(string.Format("RunMode : {0} , Stop , To Stop Jam Status", FA.MGR.RunMgr.eRunMode));
                        }
                        FA.LOG.BTN_Event("", "Process Reset BTN Pushed");
                    }
                    break;
                case "INIT": //Initialize			
                    {
                        if (FA.MGR.RunMgr.eRunMode == DEF.eRunMode.Stop)
                        {
                            if (MsgBox.Confirm("Would you like initializing??", this))
                            {
                                FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Init);
                            }
                        }
                        else
                        {
                            MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        }
                        FA.LOG.BTN_Event("", "Initialize BTN Pushed");
                    }
                    break;
                case "REJECT":
                    {
                        if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                        {
                            MsgBox.Error(string.Format("Current Run Mode : {0} \n Stop or Reset First", FA.MGR.RunMgr.eRunMode));
                            return;
                        }
                        if (MsgBox.Confirm("Would you like JIG Reject??", this))
                        {
                            FA.MGR.RunMgr.RejectSEQ();
                            FA.MGR.RunMgr.PM.StopCycleTime();
                            DKT_MES_MarkingEndInfo pEndRet = null;
                            if (FA.MGR.RecipeRunningData.bCurrentInProess == true)
                            {
                                if (FA.MGR.MESMgr.IsConnected == false)
                                    FA.MGR.MESMgr.DoConnect();

                                if (FA.MGR.RecipeRunningData.pCurrentProcessData.bMES_MarkingStartSuccess == true &&
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.bMES_MarkingEndSuccess == false)
                                {
                                    var procData = FA.MGR.RecipeRunningData.pCurrentProcessData;
                                    long iStartIDX = procData.pMES_MarkingStartInfo.iMarkingInfo_StartNo;
                                    long iEndIDX = procData.pMES_MarkingStartInfo.iMarkingInfo_EndNo;
                                    if (FA.MGR.MESMgr.SendMarkingEnd(
                                    procData.strLotCardCode,
                                    procData.strJIGCode,
                                    procData.strMarkingInfoType,
                                    procData.strMESCode,
                                    (int)iStartIDX,
                                    (int)iEndIDX,
                                    procData.pMES_MarkingStartInfo?.pMarkingTargetNumList,
                                    out pEndRet) == true) // 2026.03.23 KKW MES END Stored Procedure 변경에 따른 수정
                                    {

                                        FA.MGR.RecipeRunningData.pCurrentProcessData.SetMarkingEndInfoData(pEndRet);
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.bMES_MarkingEndSuccess = true;
                                    }
                                }
                                FA.MGR.RecipeRunningData.bCurrentInProess = false;
                                FA.LOG.BTN_Event("",
                                        string.Format("Reject BTN Pushed\n{0}_{1}",
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode));
                            }
                            else
                            {
                                FA.LOG.BTN_Event("", "Reject BTN Pushed");
                            }
                        }
                    }
                    break;
            }
        }
        private void Btn_WorkTimeReset_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Would you like to reset the run time"))
            {
                FA.MGR.RunMgr.PM.ClearTimes();
                FA.MGR.RunMgr.PM.UpdateTime(FA.MGR.RunMgr.eRunMode);
            }
        }
        private void Panel_DrawMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_pDisPlayJIGData != null)
            {
                Point ArrayCoodinate;
                int iIndex;
                if (m_pDisPlayJIGData.MouseClickTest(Panel_DrawMap, e.Location, out ArrayCoodinate, out iIndex))
                {
                    Trace.WriteLine(string.Format("{0} , {1} , {2} ", ArrayCoodinate.X, ArrayCoodinate.Y, iIndex));
                }
            }
        }

        private void Panel_DrawMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_pDisPlayJIGData != null)
            {
                Point ArrayCoodinate;
                int iIDX;
                if (m_pDisPlayJIGData.MouseClickTest(Panel_DrawMap, e.Location, out ArrayCoodinate, out iIDX))
                {
                    Trace.WriteLine(string.Format("{0} , {1} , {2}", ArrayCoodinate.X, ArrayCoodinate.Y, iIDX));
                }
            }
        }
        private void Panel_DrawMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (FA.MGR.RecipeRunningData.pCurrentProcessData != m_pDisPlayJIGData)
                return;
            if (FA.MGR.RecipeRunningData.pCurrentProcessData != null)
            {
                Point ArrayCoodinate;
                int iIDX;
                if (FA.MGR.RecipeRunningData.pCurrentProcessData.MouseClickTest(Panel_DrawMap, e.Location, out ArrayCoodinate, out iIDX))
                {
                    if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                    {
                        MsgBox.Show(string.Format("Current : {0}\n Run Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }

                    if (MsgBox.Confirm(string.Format("Would like Move {0} Position? ", iIDX + 1)))
                    {
                        double XPos = 0.0;
                        double YPos = 0.0;
                        double ZPos = 0.0;
                        if (FA.MGR.RecipeRunningData.pCurrentProcessData.GetMovingPosition(ArrayCoodinate.X, ArrayCoodinate.Y,
                                out XPos, out YPos, out ZPos
                                ))
                        {
                            FA.MGR.RunMgr.fManualMovingXPos = XPos;
                            FA.MGR.RunMgr.fManualMovingYPos = YPos;
                            FA.MGR.RunMgr.fManualMovingZPos = ZPos;
                            FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.MANUAL_MOVING);
                            FA.LOG.BTN_Event("", string.Format("{0},{1} Manual Moving\nPOS:{2}-{3}-{4}", ArrayCoodinate.X, ArrayCoodinate.Y, XPos, YPos, ZPos));
                            Trace.WriteLine(string.Format("X:{0}\nY:{1}\nZ{2}", XPos, YPos, ZPos));
                        }
                        else
                        {
                            MsgBox.Show(string.Format("Move Fail , Check Recipe Param (MultiArray)"));
                        }
                    }
                }
            }

        }

        private void Panel_DrawMap_MouseLeave(object sender, EventArgs e)
        {
            if (m_pDisPlayJIGData != null)
            {
                m_pDisPlayJIGData.MouseLeave();
            }
        }

        public void OnRecipeChanged()
        {
            if (m_bFirstJIGUpdate == false)
            {
                FA.MGR.RecipeRunningData.pCurrentProcessData.ClearDataList();
                FA.MGR.RecipeRunningData.pCurrentProcessData.ePROCColDir = RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.ePROCRowDir = RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.fRowPitch = RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<float>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.fColPitch = RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<float>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.iRowCount = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.iColCount = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.iMultiArrayCount = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.fMultiArrayGap = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.eMultiArrayDIR = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>();


                FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosX = RCP_Modify.COMMON_FIRST_PRODUCT_X_POS.GetValue<double>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosY = RCP_Modify.COMMON_FIRST_PRODUCT_Y_POS.GetValue<double>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosZ = RCP_Modify.COMMON_FIRST_PRODUCT_Z_POS.GetValue<double>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.iZeroPadCount = RCP_Modify.PROCESS_DATA_MAT_MARKING_NUM_COUNT.GetValue<int>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.iRepeatMarkingCount = RCP_Modify.PROCESS_DATA_MAT_REPEAT_MARKING_COUNT.GetValue<int>();
                FA.MGR.RecipeRunningData.pCurrentProcessData.ReAllocDataList();
                m_pDisPlayJIGData = FA.MGR.RecipeRunningData.pCurrentProcessData;
                m_bFirstJIGUpdate = true;
            }
            else
            {
                if (FA.MGR.RecipeRunningData.bCurrentInProess == false)
                {
                    int iOldRowCount = FA.MGR.RecipeRunningData.pCurrentProcessData.iRowCount;
                    int iOldColCount = FA.MGR.RecipeRunningData.pCurrentProcessData.iColCount;
                    int iOldMultiArrayCount = FA.MGR.RecipeRunningData.pCurrentProcessData.iMultiArrayCount;
                    FA.MGR.RecipeRunningData.pCurrentProcessData.ePROCColDir = RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.ePROCRowDir = RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fRowPitch = RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<float>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fColPitch = RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<float>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.iRowCount = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.iColCount = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosX = RCP_Modify.COMMON_FIRST_PRODUCT_X_POS.GetValue<double>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosY = RCP_Modify.COMMON_FIRST_PRODUCT_Y_POS.GetValue<double>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fFirstProductPosZ = RCP_Modify.COMMON_FIRST_PRODUCT_Z_POS.GetValue<double>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.iMultiArrayCount = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.fMultiArrayGap = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                    FA.MGR.RecipeRunningData.pCurrentProcessData.eMultiArrayDIR = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>();

                    FA.MGR.RecipeRunningData.pCurrentProcessData.iZeroPadCount = RCP_Modify.PROCESS_DATA_MAT_MARKING_NUM_COUNT.GetValue<int>();
                    if (RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() != iOldRowCount ||
                         RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>() != iOldColCount ||
                         FA.MGR.RecipeRunningData.pCurrentProcessData.iMultiArrayCount != iOldMultiArrayCount
                            )
                    {
                        FA.MGR.RecipeRunningData.pCurrentProcessData.ReAllocDataList();
                    }
                }
            }
        }
        private void btnMapCurrent_Click(object sender, EventArgs e)
        {
            m_pDisPlayJIGData = FA.MGR.RecipeRunningData.pCurrentProcessData;
            m_iCurrentDataPage = -1;
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            FA.MGR.RecipeRunningData.InsertFirstData(FA.MGR.RecipeRunningData.pCurrentProcessData.Clone());
        }

        private void btn_Past_Prev_Click(object sender, EventArgs e)
        {
            m_iCurrentDataPage = m_iCurrentDataPage - 1 > -1 ? m_iCurrentDataPage - 1 : -1;
            if (FA.MGR.RecipeRunningData[m_iCurrentDataPage] != null)
            {
                m_pDisPlayJIGData = FA.MGR.RecipeRunningData[m_iCurrentDataPage];
            }
            else
            {
                m_pDisPlayJIGData = FA.MGR.RecipeRunningData.pCurrentProcessData;
                m_iCurrentDataPage = -1;
            }
        }

        private void btn_Past_Next_Click(object sender, EventArgs e)
        {
            m_iCurrentDataPage = m_iCurrentDataPage + 1 < FA.MGR.RecipeRunningData.iDataListCount
                    ? m_iCurrentDataPage + 1 : FA.MGR.RecipeRunningData.iDataListCount - 1;
            if (FA.MGR.RecipeRunningData[m_iCurrentDataPage] != null)
            {
                m_pDisPlayJIGData = FA.MGR.RecipeRunningData[m_iCurrentDataPage];
            }
        }

        private void lb_LotNo_Data_Input_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
            {
                MsgBox.Error(string.Format("RunMode : {0} \n Stop First", FA.MGR.RunMgr.eRunMode));
                return;
            }
            if (FA.MGR.RecipeRunningData.bCurrentInProess == true)
            {
                MsgBox.Error(string.Format("1JIG In Process Check inside"));
                return;
            }
            using (AlphaKeypad pKeyPad = new AlphaKeypad())
            {

                string OldLotCode = FA.MGR.RecipeRunningData.strLotCardCode;
                if (pKeyPad.ShowDialog(200, tb_LOT_CODE_INPUT.Text, false) == DialogResult.OK)
                {
                    if (OldLotCode.Equals(pKeyPad.NewValue) == false)
                    {
                        FA.MGR.RecipeRunningData.strLotCardCode = pKeyPad.NewValue;
                        tb_LOT_CODE_INPUT.Text = FA.MGR.RecipeRunningData.strLotCardCode;
                        FA.LOG.BTN_Event("", string.Format("Lot Code {0}->{1} Change", OldLotCode, FA.MGR.RecipeRunningData.strLotCardCode));
                    }
                }
            }
            /*
            tb_LOT_CODE_INPUT.ReadOnly = !tb_LOT_CODE_INPUT.ReadOnly;
            tb_LOT_CODE_INPUT.BackColor = tb_LOT_CODE_INPUT.ReadOnly == true ? SystemColors.ControlDark : Color.White;
            if (tb_LOT_CODE_INPUT.ReadOnly == true)
            {
                this.Focus();
                string OldLotCode = FA.MGR.RecipeRunningData.strLotCardCode;
                FA.MGR.RecipeRunningData.strLotCardCode = tb_LOT_CODE_INPUT.Text;
                FA.LOG.BTN_Event("", string.Format("Lot Code {0}->{1} Change", OldLotCode, FA.MGR.RecipeRunningData.strLotCardCode));
            }
            else
            {
                if (string.IsNullOrEmpty(tb_LOT_CODE_INPUT.Text) == false ||
             string.IsNullOrWhiteSpace(tb_LOT_CODE_INPUT.Text) == false
             )
                {
                    if (MsgBox.Confirm(string.Format("Would like to clear Exist Lot Code ?\n{0}", tb_LOT_CODE_INPUT.Text)) == true)
                    {
                        tb_LOT_CODE_INPUT.Text = "";
                    }
                }
                tb_LOT_CODE_INPUT.Focus();
            }*/
        }

        private void btn_AddStatus_Click(object sender, EventArgs e)
        {
            if (FRM.FrmInforOperProcessStatus.Visible == false)
            {
                FRM.FrmInforOperProcessStatus.TopMost = true;
                FRM.FrmInforOperProcessStatus.StartPosition = FormStartPosition.Manual;
                FRM.FrmInforOperProcessStatus.Left = FRM.FrmMainForm.Right;
                FRM.FrmInforOperProcessStatus.Top = FRM.FrmMainForm.Top;
                FRM.FrmInforOperProcessStatus.Show();
                FRM.FrmInforOperProcessStatus.TopMost = false;
            }
            else
            {
                FRM.FrmInforOperProcessStatus.Hide();
                FRM.FrmInforOperProcessStatus.TopMost = true;
                FRM.FrmInforOperProcessStatus.StartPosition = FormStartPosition.Manual;
                FRM.FrmInforOperProcessStatus.Left = FRM.FrmMainForm.Right;
                FRM.FrmInforOperProcessStatus.Top = FRM.FrmMainForm.Top;
                FRM.FrmInforOperProcessStatus.Show();
                FRM.FrmInforOperProcessStatus.TopMost = false;
            }
        }
        private void btn_AddTimeStatus_Click(object sender, EventArgs e)
        {
            if (FRM.FrmInforOperTactTimeStatus.Visible == false)
            {
                FRM.FrmInforOperTactTimeStatus.TopMost = true;
                FRM.FrmInforOperTactTimeStatus.StartPosition = FormStartPosition.Manual;
                FRM.FrmInforOperTactTimeStatus.Left = FRM.FrmMainForm.Right;
                FRM.FrmInforOperTactTimeStatus.Top = FRM.FrmMainForm.Bottom - FRM.FrmInforOperTactTimeStatus.Height;
                FRM.FrmInforOperTactTimeStatus.Show();
                FRM.FrmInforOperTactTimeStatus.TopMost = false;
            }
            else
            {
                FRM.FrmInforOperTactTimeStatus.Hide();
                FRM.FrmInforOperTactTimeStatus.TopMost = true;
                FRM.FrmInforOperTactTimeStatus.StartPosition = FormStartPosition.Manual;
                FRM.FrmInforOperTactTimeStatus.Left = FRM.FrmMainForm.Right;
                FRM.FrmInforOperTactTimeStatus.Top = FRM.FrmMainForm.Bottom - FRM.FrmInforOperTactTimeStatus.Height;
                FRM.FrmInforOperTactTimeStatus.Show();
                FRM.FrmInforOperTactTimeStatus.TopMost = false;
            }
        }
        public void SetLotCodeTxtboxReadonly()
        {
            this.tb_LOT_CODE_INPUT.ReadOnly = true;
            tb_LOT_CODE_INPUT.BackColor = tb_LOT_CODE_INPUT.ReadOnly == true ? SystemColors.ControlDark : Color.White;
        }

        private void ucRoundedPanel1_Click(object sender, EventArgs e)
        {
            string str = "P01100120ADUC8WW0ARCV2828       ";
            string strIDX = "06627";
            DataMatrix.DM pDM =
            FA.MGR.DMGenertorMgr.CreateDataMatrix(
                                        string.Format("{0}{1}", str.TrimEnd(), strIDX));
        }
    }
}
