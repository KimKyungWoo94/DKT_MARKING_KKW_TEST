//using Aerotech.A3200.Tasks;
using EzIna.FA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmMain : Form
    {



        System.Windows.Forms.Timer m_Timer = new Timer();
        private bool m_bScannerXPowerOn;
        private bool m_bScannerYPowerOn;

        Action<int> actError = (int ErrorNo) =>
        {
            if (FA.FRM.FrmAlarm.Visible)
                FA.FRM.FrmAlarm.Hide();
            FA.FRM.FrmError.ErrNo = ErrorNo;
            FA.FRM.FrmError.Show();
        };
        Action<IntPtr, IntPtr> actError_IntPtr = (IntPtr ErrorNo, IntPtr a_StrMsg) =>
         {
             if (FA.FRM.FrmAlarm.Visible)
                 FA.FRM.FrmAlarm.Hide();
             FA.FRM.FrmError.ErrNo = ErrorNo.ToInt32();
             try
             {
                 FA.FRM.FrmError.Dscr = Marshal.PtrToStringUni(a_StrMsg);
             }
             catch
             {
                 FA.FRM.FrmError.Dscr = "";
             }
             FA.FRM.FrmError.Show();
         };


        Action<int> actAlarm = (int AlarmNo) =>
        {
            if (FA.FRM.FrmError.Visible)
                FA.FRM.FrmError.Hide();
            FA.FRM.FrmAlarm.AlarmNo = AlarmNo;
            FA.FRM.FrmAlarm.Show();
        };
        Action<IntPtr, IntPtr> actAlarm_IntPtr = (IntPtr a_AlarmNo, IntPtr a_StrMsg) =>
         {
             if (FA.FRM.FrmError.Visible)
                 FA.FRM.FrmError.Hide();
             FA.FRM.FrmAlarm.AlarmNo = a_AlarmNo.ToInt32();
             try
             {
                 FA.FRM.FrmAlarm.Msg = Marshal.PtrToStringUni(a_StrMsg);
             }
             catch
             {
                 FA.FRM.FrmAlarm.Msg = "";
             }
             FA.FRM.FrmAlarm.Show();
         };


        Action<int, int> actStage2DCal_CreateMap = (int WParam, int LParam) =>
         {
             FA.FRM.FrmTabInitProcStageCal.ucCellBox_Stage2DCal_Create(WParam, LParam);
         };
        Action<int, int> actStage2DCal_SetMeasuredValue = (int WParam, int LParam) =>
         {
             FA.FRM.FrmTabInitProcStageCal.ucCellBox_Stage2DCal_SetMeasuredValue(WParam, LParam);
         };
        Action<int, int> actGalvoCal_CreateMap = (int WParam, int LParam) =>
        {
            FA.FRM.FrmTabInitProcGalvoCal.ucCellBox_GalvoCal_Create(WParam, LParam);
        };
        Action<int, int> actGalvoCal_SetMeasuredValue = (int WParam, int LParam) =>
         {
             FA.FRM.FrmTabInitProcGalvoCal.ucCellBox_GalvoCal_SetMeasuredValue(WParam, LParam);
         };
        Action<int, int> actFindFocus_CreateMap = (int WParam, int LParam) =>
        {
            FA.FRM.FrmTabInitProcFindFocus.ucCellBox_FindFocus_Create(WParam, LParam);
        };
        Action<int, int> actFindFocus_SetMeasureValue = (int WParam, int LParam) =>
        {
            FA.FRM.FrmTabInitProcFindFocus.ucCellBox_FindFocus_SetMeasuredValue(WParam, LParam);
        };
        public FrmMain()
        {
            InitializeComponent();

            this.Width = FA.DEF.SXGA_WIDTH;
            this.Height = FA.DEF.SXGA_HEIGHT;

            // KKW Ctrl+Shift+D → LocalDuplicateManager 간이 테스트 폼 (개발용)
            this.KeyPreview = true;
            this.KeyDown += FrmMain_KeyDown_DebugTools;



            foreach (ToolStripMenuItem Items in menuStrip_FrmMain.Items)
            {
                foreach (ToolStripItem Item in Items.DropDownItems)
                {
                    Item.Click += OnMenuStripClick;
                }
            }
            this.toolStripMenuItem_Operation_VisionPanel.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_OPERATION_VISION_PANEL;
            this.toolStripMenuItem_Manual_Vision.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_MANUAL_VISION;
            this.toolStripMenuItem_Setup_Init_Process.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_INIT_PROCESS;
            this.toolStripMenuItem_Setup_Motion.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_MOTION;
            this.toolStripMenuItem_Setup_IO.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_IO;
            this.toolStripMenuItem_Setup_Cylinder.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_CYLINDER;
            this.toolStripMenuItem_Setup_Laser.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_LASER;
            this.toolStripMenuItem_Setup_Attenuator.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_ATTENUATOR;
            this.toolStripMenuItem_Setup_Scanner.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_SCANNER;
            this.toolStripMenuItem_Setup_Powermeter.Tag = GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_POWERMETER;
        }
        #region [화면(최소,최대) Resize Event]
        ///<see cref="https://docs.microsoft.com/ko-kr/dotnet/framework/winforms/how-to-distinguish-between-clicks-and-double-clicks"/>

        private Rectangle hitTestRectangle = new Rectangle();
        private Rectangle doubleClickRectangle = new Rectangle();
        private TextBox textBox1 = new TextBox();
        private Timer doubleClickTimer = new Timer();

        private bool isFirstClick = true;
        private bool isDoubleClick = false;
        private int milliseconds = 0;

        private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            WinAPIs.ReleaseCapture();
            WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);

            //             if (!hitTestRectangle.Contains(e.Location))
            //             {
            //                 return;
            //             }

            // This is the first mouse click.
            if (isFirstClick)
            {
                isFirstClick = false;

                // Determine the location and size of the double click
                // rectangle area to draw around the cursor point.
                doubleClickRectangle = new Rectangle(
                    e.X - (SystemInformation.DoubleClickSize.Width / 2),
                    e.Y - (SystemInformation.DoubleClickSize.Height / 2),
                    SystemInformation.DoubleClickSize.Width,
                    SystemInformation.DoubleClickSize.Height);
                //Invalidate();

                // Start the double click timer.
                doubleClickTimer.Start();
            }

            // This is the second mouse click.
            else
            {
                // Verify that the mouse click is within the double click
                // rectangle and is within the system-defined double
                // click period.
                if (doubleClickRectangle.Contains(e.Location) &&
                    milliseconds < SystemInformation.DoubleClickTime)
                {
                    isDoubleClick = true;
                }
            }

            if (isDoubleClick)

            {
                //WindowState = FormWindowState.Maximized;
                //btn_Frm_Normalize.BringToFront();
            }
            else
            {
                WindowState = FormWindowState.Normal;
                btn_Frm_Maximize.BringToFront();
            }

        }

        void doubleClickTimer_Tick(object sender, EventArgs e)
        {
            milliseconds += 100;
            // The timer has reached the double click time limit.
            if (milliseconds >= SystemInformation.DoubleClickTime)
            {
                doubleClickTimer.Stop();
                // Allow the MouseDown event handler to process clicks again.
                isFirstClick = true;
                isDoubleClick = false;
                milliseconds = 0;
            }
        }
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {

            #region [ Version ]
            lbl_Version.Text = Utils.Version;
            #endregion

            menuStrip_FrmMain.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
            SYS.Initialize();
            if (FRM.FrmInitialize.ShowDialog() != DialogResult.OK)
            {
                //FA.MGR.ManagerOfModules.bInit = false;
                //Form_Unload();
                //Environment.Exit(0);Application.Exit();
            }
            SYS.Static_MGR_Memory_LINK();
            //MF.TOWERLAMP.ResistLampIO();


            AddMainForm(FRM.FrmInforOperationAuto, true);
            AddMainForm(FRM.FrmInforOperationSemi, false);



            AddMainForm(FRM.FrmInforRecipeMain, false);
            AddMainForm(FRM.FrmInforManualVision, false);

            AddMainForm(FRM.FrmInforSetupInitialProcess, false);
            AddMainForm(FRM.FrmInforSetupIO, false);
            AddMainForm(FRM.FrmInforSetupMotion, false);
            AddMainForm(FRM.FrmInforSetupCylinder, false);
            AddMainForm(FRM.FrmInforSetupLaser, false);
            AddMainForm(FRM.FrmInforSetupAttenuator, false);
            AddMainForm(FRM.FrmInforSetupScanner, false);
            AddMainForm(FRM.FrmInforSetupPOMeter, false);

            AddMainForm(FRM.FrmInforLogEventLog, false);
            AddMainForm(FRM.FrmInforLogWorkLog, false);

            //AddMainForm(FRM.FrmLogMC, false);
            //AddMainForm(FRM.FrmLogSW, false);
            //AddMainForm(FRM.FrmLaserWarmupAlarm, false);

            AddForm(FRM.FrmVision, false);
            AddForm(FRM.FrmInforOperProcessStatus, false);
            AddForm(FRM.FrmInforOperTactTimeStatus, false);
            MGR.GuiMgr.CreateEvent(ReceiveEventMsgFromOtherForms);

            // Sync Buffer
            MGR.IOMgr.ReadAllIO();
            #region THREAD
            //thread
            MGR.ThreadMgr.AddItem("RUN", new UserThread.ThreadItem("RUN", 1, System.Threading.ThreadPriority.Highest));
            //	MGR.ThreadMgr.AddItem("MES", new UserThread.ThreadItem("MES", 1, System.Threading.ThreadPriority.Normal));
            MGR.ThreadMgr.AddItem("MES_ALIVE", new UserThread.ThreadItem("MES_ALIVE", 3000, System.Threading.ThreadPriority.Normal));
            MGR.ThreadMgr.GetItem("RUN").ExcuteFuncEvent += MGR.RunMgr.Execute;
            //MGR.ThreadMgr.GetItem("MES").ExcuteFuncEvent += MGR.MESMgr.ExecuteQueue;
            MGR.ThreadMgr.GetItem("MES_ALIVE").ExcuteFuncEvent += MGR.MESMgr.GetServoPingTest;

            MGR.ThreadMgr.StartItems();
            #endregion THREAD
            this.Width = 1280;
            this.Height = 1024;
            m_Timer.Interval = 50;
            m_Timer.Tick += new EventHandler(Display);

            Screen[] pMoniotor = Screen.AllScreens;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Screen.PrimaryScreen.Bounds.Location;
            FA.FRM.FrmVision.StartPosition = FormStartPosition.Manual;
            FA.FRM.FrmVision.Location =
                    pMoniotor.Length > 1 ?
                    pMoniotor[1] != Screen.PrimaryScreen ? pMoniotor[1].Bounds.Location : pMoniotor[0].Bounds.Location
                    : Screen.PrimaryScreen.Bounds.Location;
            GUI.UserControls.FlexiableMsgBox.MAX_HEIGHT_FACTOR = 0.5;
            GUI.UserControls.FlexiableMsgBox.FONT = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);

            FA.FRM.FrmInforRecipeMain.OnRecipeChangeEvent += FA.FRM.FrmInforOperationAuto.OnRecipeChanged;
            FA.FRM.FrmInforOperationAuto.OnRecipeChanged();
            FA.MGR.RunMgr.ModeChange(DEF.eRunMode.Stop);
        }
        protected bool AddMainForm(Form frm, bool bShow = false)
        {
            string Formenum = frm.Tag != null ? frm.Tag.ToString() : frm.Tag as string;
            GUI.D.eTagFormID FormID;
            Enum.TryParse(Formenum, out FormID);
            if (Enum.IsDefined(typeof(GUI.D.eTagFormID), FormID))
            {
                FA.MGR.GuiMgr.AddForm((int)FormID, panel_mainview, frm, this, bShow);
                return true;
            }
            return false;
        }
        protected bool AddForm(Form frm, bool bShow = false)
        {
            string Formenum = frm.Tag != null ? frm.Tag.ToString() : frm.Tag as string;
            GUI.D.eTagFormID FormID;
            Enum.TryParse(Formenum, out FormID);
            if (Enum.IsDefined(typeof(GUI.D.eTagFormID), FormID))
            {
                FA.MGR.GuiMgr.AddForm((int)FormID, null, frm, null, bShow);
                return true;
            }
            return false;
        }

        /// <see cref="http://kimstar.kr/1191/"/>
        public void ReceiveEventMsgFromOtherForms(GUI.D.eTagSendMsg a_eTagMsg)
        {
            switch (a_eTagMsg)
            {
                case GUI.D.eTagSendMsg.MSG_FORM_CLOSE:
                    Form_Unload();
                    Application.ExitThread();
                    Environment.Exit(0);
                    //Application.Exit();

                    break;
            }
        }

        private bool Form_Unload()
        {
            bool bRet = false;

            try
            {
                FA.FRM.FrmInitialize.bInit = false;
                VISION.FINE_CAM?.Idle();
                FA.MGR.ThreadMgr.StopItems();
                FA.FRM.FrmInforOperTactTimeStatus.Visible = false;
                FA.FRM.FrmInforOperProcessStatus.Visible = false;
                if (FA.FRM.FrmInitialize.ShowDialog() == DialogResult.OK)
                {
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return bRet;
        }
        private void btn_Frm_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btn_Frm_Normalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btn_Frm_Maximize.BringToFront();
        }
        private void btn_Frm_Maximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btn_Frm_Normalize.BringToFront();
        }

        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Are you  sure you want to quit program??"))
            {
                if (FA.MGR.GuiMgr != null)
                    FA.MGR.GuiMgr.SendMsgToFrmMain(GUI.D.eTagSendMsg.MSG_FORM_CLOSE);
            }
        }


        private void OnMenuStripClick(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Tag == null)
                return;

            GUI.D.eTagFormID eFormID;

            string strFormID = (sender as ToolStripItem).Tag.ToString();
            Enum.TryParse(strFormID, out eFormID);

            if (Enum.IsDefined(typeof(GUI.D.eTagFormID), eFormID))
            {
                FA.MGR.GuiMgr.ChangeForm((int)eFormID);
            }
        }

        private void btn_Title_Bar_Logger_MC_Click(object sender, EventArgs e)
        {
            FA.FRM.FrmLogMC.InvokeIfNeeded(() =>
            {
                if (FA.FRM.FrmLogMC.Visible == false)
                    FA.FRM.FrmLogMC.Show();
                else
                {
                    FA.FRM.FrmLogMC.BringToFront();
                }
            });
        }

        private void btn_Title_Bar_Logger_SW_Click(object sender, EventArgs e)
        {
            FA.FRM.FrmLogSW.InvokeIfNeeded(() =>
            {
                if (FA.FRM.FrmLogSW.Visible == false)
                    FA.FRM.FrmLogSW.Show();
                else
                {
                    FA.FRM.FrmLogSW.BringToFront();
                }

            });

        }
        private void FrmMain_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                m_Timer.Start();
                if (!FA.FRM.FrmVision.Visible)
                {
                    WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_VISION);
                    WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_LASER);

                }

            }
        }
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages

            switch (m.Msg)
            {
                case FA.DEF.MSG_SHOW_ALARM:
                    FA.FRM.FrmAlarm.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmAlarm.Visible)
                            FA.FRM.FrmAlarm.Hide();
                    });

                    //	FA.FRM.FrmAlarm.InvokeIfNeeded<int>(actAlarm, m.WParam.ToInt32());									
                    FA.FRM.FrmAlarm.InvokeIfNeeded<IntPtr>(actAlarm_IntPtr, m.WParam, m.LParam);
                    break;

                case FA.DEF.MSG_SHOW_ERROR:
                    FA.FRM.FrmError.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmError.Visible)
                            FA.FRM.FrmError.Hide();
                    });

                    FA.FRM.FrmError.InvokeIfNeeded<IntPtr>(actError_IntPtr, m.WParam, m.LParam);
                    break;

                case FA.DEF.MSG_HIDE_ERROR:
                    FA.FRM.FrmError.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmError.Visible)
                            FA.FRM.FrmError.Hide();
                    });

                    break;
                case FA.DEF.MSG_HIDE_ALARM:
                    FA.FRM.FrmAlarm.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmAlarm.Visible)
                            FA.FRM.FrmAlarm.Hide();
                    });
                    break;
                case FA.DEF.MSG_FROM_CLOSE_ALL:
                    FA.FRM.FrmError.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmError.Visible)
                            FA.FRM.FrmError.Hide();
                    });
                    FA.FRM.FrmAlarm.InvokeIfNeeded(() =>
                    {
                        if (FA.FRM.FrmAlarm.Visible)
                            FA.FRM.FrmAlarm.Hide();
                    });
                    break;
                case FA.DEF.MSG_SHOW_VISION:
                    FA.FRM.FrmVision.InvokeIfNeeded(() =>
                    {
                        if (!FA.FRM.FrmVision.Visible)
                            FA.FRM.FrmVision.Show();
                    });
                    break;
                case FA.DEF.MSG_SHOW_LASER:
                    FA.FRM.FrmLaserWarmupAlarm.InvokeIfNeeded(() =>
                    {
                        if (!FA.FRM.FrmLaserWarmupAlarm.Visible)
                            FA.FRM.FrmLaserWarmupAlarm.Show();
                    });
                    break;
                case FA.DEF.GUI_AUTO_LOT_CODE_TXT_READONLY:
                    {
                        FA.FRM.FrmInforOperationAuto.InvokeIfNeeded(() =>
                        {
                            FA.FRM.FrmInforOperationAuto.SetLotCodeTxtboxReadonly();
                        });
                    }
                    break;


                //stage 2d calibration
                case FA.DEF.MSG_STAGE_2D_CAL_CREATE_MAP:
                    FA.FRM.FrmTabInitProcStageCal?.InvokeIfNeeded<int>(
                    actStage2DCal_CreateMap, m.WParam.ToInt32(), m.LParam.ToInt32());
                    break;
                case FA.DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE:
                    FA.FRM.FrmTabInitProcStageCal?.InvokeIfNeeded<int>(
                    actStage2DCal_SetMeasuredValue, m.WParam.ToInt32(), m.LParam.ToInt32());
                    break;
                //find focus
                case FA.DEF.MSG_FIND_FOCUS_CREATE_MAP:
                    FA.FRM.FrmTabInitProcFindFocus?.InvokeIfNeeded<int>(
                    actFindFocus_CreateMap, m.WParam.ToInt32(), m.LParam.ToInt32());
                    break;
                case FA.DEF.MSG_FIND_FOCUS_SET_MEASURED_VALUE:
                    FA.FRM.FrmTabInitProcFindFocus?.InvokeIfNeeded<int>(
                    actFindFocus_SetMeasureValue, m.WParam.ToInt32(), m.LParam.ToInt32());
                    break;

            }
            base.WndProc(ref m);

        }

        private void Display(object sender, EventArgs e)
        {
            m_Timer.Stop();
            lbl_RunMode.Text = FA.MGR.RunMgr?.eRunMode.ToString().ToUpper();
            lbl_RunMode.ForeColor = FLICKER.IsOn ? Color.Lime : lbl_RunMode.BackColor;
            ucRoundedPanel_Axis0.clBegin = AXIS.RX.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RX.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis1.clBegin = AXIS.Y.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.Y.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis2.clBegin = AXIS.RZ.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RZ.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis3.clBegin = AXIS.RAIL_ADJUST.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RAIL_ADJUST.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis4.clBegin = RTC5.Instance?.IsSystemOK_HeadA == false ? FA.DEF.MotorAlarmOccurBackColor : RTC5.Instance?.GetHeadStatus(Scanner.ScanlabRTC5.RTC_HEAD.A, Scanner.ScanlabRTC5.HEAD_STATUS.POS_ACK_X) == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor;
            ucRoundedPanel_Axis5.clBegin = RTC5.Instance?.IsSystemOK_HeadA == false ? FA.DEF.MotorAlarmOccurBackColor : RTC5.Instance?.GetHeadStatus(Scanner.ScanlabRTC5.RTC_HEAD.A, Scanner.ScanlabRTC5.HEAD_STATUS.POS_ACK_Y) == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor;

            ucRoundedPanel_Axis0.clEnd = AXIS.RX.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RX.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis1.clEnd = AXIS.Y.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.Y.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis2.clEnd = AXIS.RZ.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RZ.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis3.clEnd = AXIS.RAIL_ADJUST.Status()?.m_stMotionInfoStatus.m_bIsAlarm == true ? FA.DEF.MotorAlarmOccurBackColor : (AXIS.RAIL_ADJUST.Status()?.IsServoOn == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor);
            ucRoundedPanel_Axis4.clEnd = RTC5.Instance?.IsSystemOK_HeadA == false ? FA.DEF.MotorAlarmOccurBackColor : RTC5.Instance?.GetHeadStatus(Scanner.ScanlabRTC5.RTC_HEAD.A, Scanner.ScanlabRTC5.HEAD_STATUS.POS_ACK_X) == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor;
            ucRoundedPanel_Axis5.clEnd = RTC5.Instance?.IsSystemOK_HeadA == false ? FA.DEF.MotorAlarmOccurBackColor : RTC5.Instance?.GetHeadStatus(Scanner.ScanlabRTC5.RTC_HEAD.A, Scanner.ScanlabRTC5.HEAD_STATUS.POS_ACK_Y) == true ? FA.DEF.MotorEnableBackColor : FA.DEF.MotorDisableBackColor;


            ucRoundedPanel_Axis0.Caption = string.Format("{0} : {1:F04} mm", DEF.eAxesName.RX, AXIS.RX.Status()?.m_stPositionStatus.fActPos);
            ucRoundedPanel_Axis1.Caption = string.Format("{0} : {1:F04} mm", DEF.eAxesName.Y, AXIS.Y.Status()?.m_stPositionStatus.fActPos);
            ucRoundedPanel_Axis2.Caption = string.Format("{0} : {1:F04} mm", DEF.eAxesName.RZ, AXIS.RZ.Status()?.m_stPositionStatus.fActPos);
            ucRoundedPanel_Axis3.Caption = string.Format("{0} : {1:F04} mm", "R_ADJ", AXIS.RAIL_ADJUST.Status()?.m_stPositionStatus.fCmdPos);
            ucRoundedPanel_Axis4.Caption = string.Format("{0} : {1:F04} mm", "RA", RTC5.Instance?.GetCommandPositionX(Scanner.ScanlabRTC5.RTC_HEAD.A) * RTC5.Instance?.ConfigData.fScale);
            ucRoundedPanel_Axis5.Caption = string.Format("{0} : {1:F04} mm", "RB", RTC5.Instance?.GetCommandPositionY(Scanner.ScanlabRTC5.RTC_HEAD.A) * RTC5.Instance?.ConfigData.fScale);

            ucRoundedPanel_Axis0.ForeColor = Color.White;
            ucRoundedPanel_Axis1.ForeColor = Color.White;
            ucRoundedPanel_Axis2.ForeColor = Color.White;
            ucRoundedPanel_Axis4.ForeColor = Color.White;
            ucRoundedPanel_Axis5.ForeColor = Color.White;
#if !SIM
            if (RTC5.Instance.GetListStatus_Load(Scanner.ScanlabRTC5.RTC_LIST._1st))
            {
                lbl_TaskStatus_01.BackColor = Color.White;
            }
            else if (RTC5.Instance.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._1st))
            {
                lbl_TaskStatus_01.BackColor = Color.Orange;
            }
            else if (RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st))
            {
                lbl_TaskStatus_01.BackColor = Color.Green;
            }
            else
            {
                lbl_TaskStatus_01.BackColor = Color.Red;
            }

            if (RTC5.Instance.GetListStatus_Load(Scanner.ScanlabRTC5.RTC_LIST._2nd))
            {
                lbl_TaskStatus_02.BackColor = Color.White;
            }
            else if (RTC5.Instance.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._2nd))
            {
                lbl_TaskStatus_02.BackColor = Color.Orange;
            }
            else if (RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._2nd))
            {
                lbl_TaskStatus_02.BackColor = Color.Green;
            }
            else
            {
                lbl_TaskStatus_02.BackColor = Color.Red;
            }
#endif

            lbl_Initialized.ForeColor = FA.MGR.RunMgr?.IsInitialized == true ? Color.LimeGreen : Color.Red;
            lbl_MES.ForeColor = FA.MGR.MESMgr?.bConnectionAlive == true ? Color.LimeGreen : Color.Red;
            //lbl_TaskStatus_01.BackColor = EzIna.Motion.CMotionA3200.GetTaskState_Enum(1) == TaskState.Error ? FA.DEF.A3200TaskAlarmIsOccuredColor : FA.DEF.A3200TaskAlarmIsNotOccuredColor;
            //lbl_TaskStatus_02.BackColor = EzIna.Motion.CMotionA3200.GetTaskState_Enum(2) == TaskState.Error ? FA.DEF.A3200TaskAlarmIsOccuredColor : FA.DEF.A3200TaskAlarmIsNotOccuredColor;
            // lbl_TaskStatus_03.BackColor = EzIna.Motion.CMotionA3200.GetTaskState_Enum(3) == TaskState.Error ? FA.DEF.A3200TaskAlarmIsOccuredColor : FA.DEF.A3200TaskAlarmIsNotOccuredColor;
            // lbl_TaskStatus_04.BackColor = EzIna.Motion.CMotionA3200.GetTaskState_Enum(4) == TaskState.Error ? FA.DEF.A3200TaskAlarmIsOccuredColor : FA.DEF.A3200TaskAlarmIsNotOccuredColor;
            m_Timer.Start();
        }

        // ── KKW Ctrl+Shift+D : LocalDuplicateManager 간이 테스트 폼 (개발용) ──
        private void FrmMain_KeyDown_DebugTools(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                using (var frm = new FrmDuplicateTest())
                    frm.ShowDialog(this);
            }
        }
    }
}
