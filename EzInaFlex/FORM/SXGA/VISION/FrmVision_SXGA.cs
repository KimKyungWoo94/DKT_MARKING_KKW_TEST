using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzInaVision;
using System.Collections.Concurrent;
using EzIna.FA;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EzIna
{

    public partial class FrmVision_SXGA : Form
    {
        string m_strSelectedVision = "FINE";
        Motion.GDMotion.eSpeedType m_eMotionSpeed = Motion.GDMotion.eSpeedType.SLOW;
        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        #region CrossHair
        double m_fCrossHairPitch = 1.0;
        double m_fCrossHairOffsetX = 0.0;
        double m_fCrossHairOffsetY = 0.0;
        #endregion CrossHair
        #region Grid
        uint m_uiX_Grid_ArrayCount = 0;
        uint m_uiY_Grid_ArrayCount = 0;

        double m_fX_Grid_DrawPitch = 0.0;
        double m_fY_Grid_DrawPitch = 0.0;
        double m_fGrid_XPitch = 0.0;
        double m_fGrid_YPitch = 0.0;

        double m_fDM_MarkingOffSetX = 0.0;
        double m_fDM_MarkingOffSetY = 0.0;
        #endregion Grid

        #region DM 
        string m_strDMMarkingText = "";
        #endregion DM



        public FrmVision_SXGA()
        {
            InitializeComponent();

            this.ControlBox = false;

            doubleClickTimer.Interval = 100;
            doubleClickTimer.Tick += new EventHandler(doubleClickTimer_Tick);

            btnSelectFineVision.Click += new EventHandler(ChangeVision);
            btnSelectCoarseVision.Click += new EventHandler(ChangeVision);


            btn_LT.Tag = "LT";
            btn_T.Tag = "T";
            btn_RT.Tag = "RT";

            btn_L.Tag = "L";
            btn_Stop.Tag = "STOP";
            btn_R.Tag = "R";

            btn_LB.Tag = "LB";
            btn_B.Tag = "B";
            btn_RB.Tag = "RB";

            btn_Up.Tag = "UP";
            btn_Dn.Tag = "DN";
            btn_Stop2.Tag = "STOP2";


            btn_LT.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_T.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_RT.MouseDown += new MouseEventHandler(_OnMouseDown);

            btn_L.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_R.MouseDown += new MouseEventHandler(_OnMouseDown);

            btn_LB.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_B.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_RB.MouseDown += new MouseEventHandler(_OnMouseDown);

            btn_Up.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_Dn.MouseDown += new MouseEventHandler(_OnMouseDown);

            btn_LT.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_T.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_RT.MouseUp += new MouseEventHandler(_OnMouseUp);

            btn_L.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_R.MouseUp += new MouseEventHandler(_OnMouseUp);

            btn_LB.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_B.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_RB.MouseUp += new MouseEventHandler(_OnMouseUp);

            btn_Up.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_Dn.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_PinichRoller_Loading_L.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_PinichRoller_Loading_R.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_PinichRoller_Loading_L.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_PinichRoller_Loading_R.MouseUp += new MouseEventHandler(_OnMouseUp);

            btn_PinichRoller_UNLoading_L.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_PinichRoller_UNLoading_R.MouseDown += new MouseEventHandler(_OnMouseDown);
            btn_PinichRoller_UNLoading_L.MouseUp += new MouseEventHandler(_OnMouseUp);
            btn_PinichRoller_UNLoading_R.MouseUp += new MouseEventHandler(_OnMouseUp);

            //btn_Manual_AF_Start.Click += new EventHandler(ManualFuncCall);
            // btn_Manual_AF_Reset.Click += new EventHandler(ManualFuncCall);

            btn_Manual_CrossHair_Start.Click += new EventHandler(ManualFuncCall);
            btn_Grid_Draw.Click += new EventHandler(ManualFuncCall);
            btn_Grid_Rect.Click += new EventHandler(ManualFuncCall);
            btn_Manual_JIGLoading.Click += new EventHandler(ManualFuncCall);
            btn_Manual_JIGUnloading.Click += new EventHandler(ManualFuncCall);
            btn_DM_INSP_MARKING.Click += new EventHandler(ManualFuncCall);
            btn_DM_INSP.Click += new EventHandler(ManualFuncCall); //2026.03.11 KKW
            btn_DM_NO_INSP_MARKING.Click += new EventHandler(ManualFuncCall);
            btn_Save_CalibrationFile.Click += new EventHandler(ManualFuncCall);
            //btn_Manual_CrossHair_Reset.Click += new EventHandler(ManualFuncCall);
            btn_MotorSpeed_SLOW.Click += new EventHandler(Btn_Motor_SPEED_CHANGE);
            btn_MotorSpeed_FAST.Click += new EventHandler(Btn_Motor_SPEED_CHANGE);
            btn_MotorSpeed_RUN.Click += new EventHandler(Btn_Motor_SPEED_CHANGE);

            //lbl_CorssHairEFrqNewValue.Click += new EventHandler(SetValueFunc);
            //lbl_CorssHairPowerNewValue.Click += new EventHandler(SetValueFunc);
            //btn_SaveCrossHairValue.Click += new EventHandler(SetValueFunc);
            //btn_ApplyCrossHairValue.Click += new EventHandler(SetValueFunc);
            ///<see cref="https://link2me.tistory.com/880">소소한 일상 및 업무TIP 다루기]</see>
            ///<see cref="https://msdn.microsoft.com/ko-kr/library/system.windows.forms.formstartposition(v=vs.110).aspx">출처: https://link2me.tistory.com/880 [소소한 일상 및 업무TIP 다루기]</see>
            int screenWidth = System.Windows.Forms.SystemInformation.VirtualScreen.Width; //듀얼 모니터 가로 크기
            int screenHeight = System.Windows.Forms.SystemInformation.VirtualScreen.Height; //듀얼 모니터 세로 크기

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(System.Windows.Forms.SystemInformation.VirtualScreen.X,
                                System.Windows.Forms.SystemInformation.VirtualScreen.Y);

            this.Width = FA.DEF.SXGA_WIDTH;
            this.Height = FA.DEF.SXGA_HEIGHT;
            tmr.Tick += this.Display;

            #region CrossHair
            lb_CrossHair_Pitch.Text = m_fCrossHairPitch.ToString("F3");
            lb_CrossHair_OffsetX.Text = m_fCrossHairOffsetX.ToString("F3");
            lb_CrossHair_OffsetY.Text = m_fCrossHairOffsetY.ToString("F3");
            lb_CrossHair_Pitch.Click += ManualFuncParamSet;
            lb_CrossHair_OffsetX.Click += ManualFuncParamSet;
            lb_CrossHair_OffsetY.Click += ManualFuncParamSet;
            #endregion CrossHair
            #region Grid
            lb_Grid_X_ArrayCount.Text = m_uiX_Grid_ArrayCount.ToString();
            lb_Grid_Y_ArrayCount.Text = m_uiY_Grid_ArrayCount.ToString();
            lb_Gird_X_DrawPitch.Text = m_fX_Grid_DrawPitch.ToString();
            lb_Gird_Y_DrawPitch.Text = m_fY_Grid_DrawPitch.ToString();
            lb_Gird_XPitch.Text = m_fGrid_XPitch.ToString("F3");
            lb_Gird_YPitch.Text = m_fGrid_YPitch.ToString("F3");

            lb_Grid_X_ArrayCount.Click += ManualFuncParamSet;
            lb_Grid_Y_ArrayCount.Click += ManualFuncParamSet;
            lb_Gird_X_DrawPitch.Click += ManualFuncParamSet;
            lb_Gird_Y_DrawPitch.Click += ManualFuncParamSet;
            lb_Gird_XPitch.Click += ManualFuncParamSet;
            lb_Gird_YPitch.Click += ManualFuncParamSet;
            #endregion Grid

            #region DM 
            lb_DM_Text.Text = m_strDMMarkingText;
            lb_DM_Text.Click += ManualFuncParamSet;

            lb_DM_MarkingOffsetX.Click += ManualFuncParamSet;
            lb_DM_MarkingOffsetY.Click += ManualFuncParamSet;
            lb_DM_MarkingOffsetX.Text = string.Format("{0:F3}mm", m_fDM_MarkingOffSetX);
            lb_DM_MarkingOffsetY.Text = string.Format("{0:F3}mm", m_fDM_MarkingOffSetY);
            #endregion DM


        }

        private void Btn_DM_INSP_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn_Frm_Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_Frm_Max_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btn_Frm_Normalize.BringToFront();
        }

        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btn_Frm_Normalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btn_Frm_Max.BringToFront();
        }


        #region [화면(최소,최대) Resize Event]
        ///<see cref ="https://docs.microsoft.com/ko-kr/dotnet/framework/winforms/how-to-distinguish-between-clicks-and-double-clicks"/>

        private Rectangle hitTestRectangle = new Rectangle();
        private Rectangle doubleClickRectangle = new Rectangle();
        private TextBox textBox1 = new TextBox();
        private System.Windows.Forms.Timer doubleClickTimer = new System.Windows.Forms.Timer();

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
                btn_Frm_Max.BringToFront();
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



        private void AttachVision(string a_strName)
        {
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(a_strName.ToUpper(), out item);
            if (item != null)
            {
                if (item.Visible == false)
                {
                    if (panel_Vision.Controls.Contains(item) == false)
                    {
                        panel_Vision.Controls.Add(item);
                        item.Show();

                    }
                }
            }
        }
        private void DetachVisions()
        {
            foreach (EzIna.FrmVisionOfPanel item in panel_Vision.Controls)
            {
                item.Hide();
            }

            if (panel_Vision.Controls.Count > 0)
            {
                panel_Vision.Controls.Clear();
            }
        }
        //private void Add
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            tmr.Enabled = this.Visible;
            if (Visible)
            {
                btnSelectFineVision.ImageIndex = 1;
                btnSelectCoarseVision.ImageIndex = 0;
                AttachVision(m_strSelectedVision.ToUpper());

                Slider_LIGHT_BAR_L.Value = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.LEFT);
                Slider_LIGHT_BAR_R.Value = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.RIGHT);
                Slider_LIGHT_BAR_U.Value = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.UP);
                Slider_LIGHT_BAR_B.Value = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM);

            }
            else
            {
                DetachVisions();
            }
        }

        private void _OnMouseDown(object a_obj, MouseEventArgs e)
        {
            try
            {
                Button btn = (a_obj as Button);
                double fRelTarget;
                double.TryParse(txtbox_StepValue.Text, out fRelTarget);
                if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                {
                    MsgBox.Error(string.Format("Run Mode : {0}\n Stop First", FA.MGR.RunMgr.eRunMode));
                    return;
                }
                switch (btn.Tag.ToString())
                {
                    case "LT":
                        if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget * -1, Motion.GDMotion.eSpeedType.FAST);
                            AXIS.Y.Move_Relative(fRelTarget, Motion.GDMotion.eSpeedType.FAST);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                            AXIS.Y.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "T":
                        if (!AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.Y.Move_Relative(fRelTarget, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.Y.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "RT":
                        if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget, m_eMotionSpeed);
                            AXIS.Y.Move_Relative(fRelTarget, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                            AXIS.Y.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }

                        break;
                    case "L":
                        if (!AXIS.RX.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "R":
                        if (!AXIS.RX.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "LB":
                        if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                            AXIS.Y.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                            AXIS.Y.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "B":
                        if (!AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.Y.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.Y.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "RB":
                        if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RX.Move_Relative(fRelTarget, m_eMotionSpeed);
                            AXIS.Y.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RX.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                            AXIS.Y.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "UP":
                        if (!AXIS.RZ.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RZ.Move_Relative(fRelTarget * -1, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RZ.Move_Jog(false, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "DN":
                        if (!AXIS.RZ.Status().IsMotionDone)
                            break;

                        if (chkbox_UseTheStep.Checked)
                        {
                            AXIS.RZ.Move_Relative(fRelTarget, m_eMotionSpeed);
                        }
                        else
                        {
                            AXIS.RZ.Move_Jog(true, m_eMotionSpeed, trackBar_MotionSpeed.Value / 100.0);
                        }
                        break;
                    case "PR_LOADING_L":
                        {
                            if (!AXIS.PR_L_U.Status().IsMotionDone)
                                break;
                            if (!AXIS.PR_L_B.Status().IsMotionDone)
                                break;

                            AXIS.PR_L_U.Move_Jog(false, m_eMotionSpeed, 1);
                            AXIS.PR_L_B.Move_Jog(false, m_eMotionSpeed, 1);
                        }
                        break;
                    case "PR_LOADING_R":
                        {
                            if (!AXIS.PR_L_U.Status().IsMotionDone)
                                break;
                            if (!AXIS.PR_L_B.Status().IsMotionDone)
                                break;
                            AXIS.PR_L_U.Move_Jog(true, m_eMotionSpeed, 1);
                            AXIS.PR_L_B.Move_Jog(true, m_eMotionSpeed, 1);
                        }
                        break;
                    case "PR_UNLOADING_L":
                        {
                            if (!AXIS.PR_R_U.Status().IsMotionDone)
                                break;
                            if (!AXIS.PR_R_B.Status().IsMotionDone)
                                break;
                            AXIS.PR_R_U.Move_Jog(false, m_eMotionSpeed, 1);
                            AXIS.PR_R_B.Move_Jog(false, m_eMotionSpeed, 1);
                        }
                        break;
                    case "PR_UNLOADING_R":
                        {
                            if (!AXIS.PR_R_U.Status().IsMotionDone)
                                break;
                            if (!AXIS.PR_R_B.Status().IsMotionDone)
                                break;

                            AXIS.PR_R_U.Move_Jog(true, m_eMotionSpeed, 1);
                            AXIS.PR_R_B.Move_Jog(true, m_eMotionSpeed, 1);
                        }
                        break;

                }
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }


        }
        private void _OnMouseUp(object a_obj, MouseEventArgs e)
        {
            try
            {
                if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                {
                    return;
                }
                if (chkbox_UseTheStep.Checked)
                {
                    //AXIS.AllSDStop();
                }
                else
                {
                    AXIS.AllJogStop();
                }

            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }
        private void ChangeVision(object sender, EventArgs a_e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;

            if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                return;




            double dMX = 0.0;
            double dMY = 0.0;
            double dDistFineToLow = FA.RCP.M100_CrossHairDistX_From_F_To_C.AsDouble;
            double dMZ = 0.0;
            switch ((sender as Control).Name)
            {
                case "btnSelectFineVision":
                    DetachVisions();
                    m_strSelectedVision = "FINE";
                    AttachVision(m_strSelectedVision.ToUpper());
                    btnSelectFineVision.ImageIndex = 1;
                    btnSelectCoarseVision.ImageIndex = 0;

                    dMX = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairDistX_From_F_To_C.AsDouble;
                    dMY = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairDistY_From_F_To_C.AsDouble;
                    if (FA.OPT.UseAutoFocus)
                        dMZ = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_VisionFocusOffset.AsDouble;
                    else
                        dMZ = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

                    FA.ACT.MoveABS(FA.DEF.eAxesName.RX, dMX, Motion.GDMotion.eSpeedType.FAST);
                    FA.ACT.MoveABS(FA.DEF.eAxesName.Y, dMY, Motion.GDMotion.eSpeedType.FAST);
                    FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, dMZ, Motion.GDMotion.eSpeedType.FAST);
                    break;
                case "btnSelectCoarseVision":
                    DetachVisions();
                    m_strSelectedVision = "COARSE";
                    AttachVision(m_strSelectedVision.ToUpper());

                    btnSelectFineVision.ImageIndex = 0;
                    btnSelectCoarseVision.ImageIndex = 1;

                    dMX = AXIS.RX.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairDistX_From_F_To_C.AsDouble;
                    dMY = AXIS.Y.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairDistY_From_F_To_C.AsDouble;
                    if (FA.OPT.UseAutoFocus)
                        dMZ = FA.RCP.M100_CoarseVisionFocusZPos.AsDouble - FA.RCP.M100_VisionFocusOffset.AsDouble;
                    else
                        dMZ = FA.RCP.M100_CoarseVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

                    FA.ACT.MoveABS(FA.DEF.eAxesName.RX, dMX, Motion.GDMotion.eSpeedType.FAST);
                    FA.ACT.MoveABS(FA.DEF.eAxesName.Y, dMY, Motion.GDMotion.eSpeedType.FAST);
                    FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, dMZ, Motion.GDMotion.eSpeedType.FAST);
                    break;
            }

        }
        private void FrmVision_SXGA_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (FA.MGR.VisionMgr != null)
            //{
            //	FA.MGR.VisionMgr.Idle();
            //
            //	FA.MGR.VisionMgr.Terminate();
            //}
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbox_UseTheStep.Checked)
                    AXIS.AllSDStop();
                else
                    AXIS.AllJogStop();

            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }
        private void btn_JOGSTOP_Click(object sender, EventArgs e)
        {
            try
            {
                AXIS.AllJogStop();
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }
        private void btn_Stop2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbox_UseTheStep.Checked)
                    AXIS.AllSDStop();
                else
                    AXIS.AllJogStop();

            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }

        private void panel_Vision_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar_LIGHT_ValueChanged(object sender, EventArgs e)
        {
            //channel 1부터 시작함.
            TrackBar pControl = sender as TrackBar;

            int iChannel = 0;
            int.TryParse(pControl.Tag as string, out iChannel);

            FA.DEF.LIGHT_CH Ch = (FA.DEF.LIGHT_CH)iChannel;

            if (Enum.IsDefined(typeof(FA.DEF.LIGHT_CH), Ch) && Ch != DEF.LIGHT_CH.NONE)
            {
                if (FA.MGR.LightMgr[(ushort)FA.DEF.lightSource.BAR] != null)
                {
                    FA.MGR.LightMgr[(ushort)FA.DEF.lightSource.BAR].SetIntensity((ushort)(Ch), pControl.Value);
                    //lbl_LIGHT_BAR_L.Text = pControl.Value.ToString();
                }
            }
        }


        private void FrmVision_SXGA_Load(object sender, EventArgs e)
        {
            tmr.Interval = 50;
            m_eMotionSpeed = Motion.GDMotion.eSpeedType.SLOW;
            btn_MotorSpeed_SLOW.ImageIndex = 1;
            btn_MotorSpeed_FAST.ImageIndex = 0;
            btn_MotorSpeed_RUN.ImageIndex = 0;


        }

        private void Display(object sender, EventArgs e)
        {
            try
            {
                tmr.Stop();
                lbl_LIGHT_BAR_L.Text = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.LEFT).ToString();
                lbl_LIGHT_BAR_R.Text = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.RIGHT).ToString();
                lbl_LIGHT_BAR_B.Text = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM).ToString();
                lbl_LIGHT_BAR_U.Text = FA.LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.UP).ToString();
                LED_EmissionEnable.Value = FA.LASER.Instance.IsEmissionOn;
                lb_MoveSpeedPercent.Text = string.Format("{0}%", trackBar_MotionSpeed.Value);
                LED_JIG_LOCK_STATUS.Value = FA.ACT.CYL_JIG_FEEDER_L_CLAMP.CurrentStatus() & FA.ACT.CYL_JIG_FEEDER_R_CLAMP.CurrentStatus();
                if (FA.DEF.eDO.PINCH_ROLLER_L_U.GetDO().Value && FA.DEF.eDO.PINCH_ROLLER_L_B.GetDO().Value)
                {
                    btn_PR_LOADING_UNCLAMP.BackColor = Color.White;
                    btn_PR_LOADING_CLAMP.BackColor = Color.Green;
                }
                else if (FA.DEF.eDO.PINCH_ROLLER_L_U.GetDO().Value == false && FA.DEF.eDO.PINCH_ROLLER_L_B.GetDO().Value == false)
                {
                    btn_PR_LOADING_UNCLAMP.BackColor = Color.Green;
                    btn_PR_LOADING_CLAMP.BackColor = Color.White;
                }
                else
                {
                    btn_PR_LOADING_UNCLAMP.BackColor = Color.White;
                    btn_PR_LOADING_CLAMP.BackColor = Color.White;
                }
                if (FA.DEF.eDO.PINCH_ROLLER_R_U.GetDO().Value && FA.DEF.eDO.PINCH_ROLLER_R_B.GetDO().Value)
                {
                    btn_PR_UNLOADING_CLAMP.BackColor = Color.Green;
                    btn_PR_UNLOADING_UNCLAMP.BackColor = Color.White;
                }
                else if (FA.DEF.eDO.PINCH_ROLLER_R_U.GetDO().Value == false && FA.DEF.eDO.PINCH_ROLLER_R_B.GetDO().Value == false)
                {
                    btn_PR_UNLOADING_UNCLAMP.BackColor = Color.Green;
                    btn_PR_UNLOADING_CLAMP.BackColor = Color.White;
                }
                else
                {
                    btn_PR_UNLOADING_UNCLAMP.BackColor = Color.White;
                    btn_PR_UNLOADING_CLAMP.BackColor = Color.White;
                }
                if (ACT.CYL_STOPPER_CENTER_UP.CurrentStatus())
                {
                    LED_CENTER_STOPPER.Value = true;
                }
                else if (ACT.CYL_STOPPER_CENTER_DOWN.CurrentStatus())
                {
                    LED_CENTER_STOPPER.Value = false;
                }
                else
                {
                    LED_CENTER_STOPPER.Value = false;
                }
                tmr.Enabled = this.Visible;
            }
            catch (System.Exception ex)
            {
                FA.LOG.Fatal("FATAL", ex.StackTrace, ex.Message);
                tmr.Enabled = this.Visible;
            }
        }

        private void SetValueFunc(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;

            switch ((sender as Control).Name)
            {
                case "lbl_CorssHairEFrqNewValue":
                    {
                        EzIna.GUI.UserControls.NumberKeypad num = new EzIna.GUI.UserControls.NumberKeypad();
                        if (num.ShowDialog(100, 500) == DialogResult.OK)
                            (sender as Control).Text = string.Format("{0:F3}", num.Result);
                    }
                    break;
                case "lbl_CorssHairPowerNewValue":
                    {
                        EzIna.GUI.UserControls.NumberKeypad num = new EzIna.GUI.UserControls.NumberKeypad();
                        if (num.ShowDialog(1, 20) == DialogResult.OK)
                            (sender as Control).Text = string.Format("{0:F3}", num.Result);
                    }
                    break;

                case "btn_ApplyCrossHairValue":
                    {
                        // lbl_MMP_FREQ.Text = string.Format("{0:F2}", FA.RCP.M100_CrossHairLaserFrq);
                        // lbl_MMP_POWER.Text = string.Format("{0:F2}", FA.RCP.M100_CrossHairLaserPwr);

                        // FA.RCP.M100_CrossHairLaserFrq.m_strValue = lbl_CorssHairEFrqNewValue.Text;
                        // FA.RCP.M100_CrossHairLaserPwr.m_strValue = lbl_CorssHairPowerNewValue.Text;
                    }
                    break;
                case "btn_SaveCrossHairValue":
                    {
                        if (MsgBox.Confirm("Would you like to save the file??"))
                        {
                            if (m_strSelectedVision.Contains("FINE"))
                            {

                                double dOldXOffset = FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble;
                                double dOldYOffset = FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble;

                                FA.RCP.M100_CrossHairFineVisionRefPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString("F4");
                                FA.RCP.M100_CrossHairFineVisionRefPosY.m_strValue = AXIS.Y.Status().m_stPositionStatus.fActPos.ToString("F4");

                                FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.m_strValue = string.Format("{0:F4}", dOldXOffset + (FA.RCP.M100_CrossHairScannerRefPosX.AsDouble - FA.RCP.M100_CrossHairFineVisionRefPosX.AsDouble));
                                FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.m_strValue = string.Format("{0:F4}", dOldYOffset + (FA.RCP.M100_CrossHairScannerRefPosY.AsDouble - FA.RCP.M100_CrossHairFineVisionRefPosY.AsDouble));


                            }
                            else
                            {
                                double dOldXOffset = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.AsDouble;
                                double dOldYOffset = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.AsDouble;

                                FA.RCP.M100_CrossHairCoarseVisionRefPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString("F4");
                                FA.RCP.M100_CrossHairCoarseVisionRefPosY.m_strValue = AXIS.Y.Status().m_stPositionStatus.fActPos.ToString("F4");

                                FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.m_strValue = string.Format("{0:F4}", dOldXOffset + (FA.RCP.M100_CrossHairScannerRefPosX.AsDouble - FA.RCP.M100_CrossHairCoarseVisionRefPosX.AsDouble));
                                FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.m_strValue = string.Format("{0:F4}", dOldYOffset + (FA.RCP.M100_CrossHairScannerRefPosY.AsDouble - FA.RCP.M100_CrossHairCoarseVisionRefPosY.AsDouble));


                            }
                            FA.MGR.ProjectMgr.RecipeSave_InitProc();
                        }
                    }
                    break;
            }

        }
        private void ManualFuncCall(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            if (m_bMarkingStart == true)
                return;
            switch ((sender as Control).Name)
            {
                case "btn_Manual_AF_Start":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                        throw new Exception("It is not stop mode");
                    if (MsgBox.Confirm("Would like to execute to auto focusing?"))
                        FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.ToManual, 0, FA.DEF.eManualMode.AUTOFOCUS);
                    break;
                case "btn_Manual_AF_Reset":
                    if (MsgBox.Confirm("Would you like to reset the sequence??"))
                    {
                        FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.RESET);
                        FA.LOG.BTN_Event("BUTTON EVENT", "Reset button click");
                    }
                    break;

                case "btn_Manual_CrossHair_Start":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }

                    if (RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st) == true)
                    {
                        MsgBox.Error("Scanner is working Now");
                        return;
                    }

                    if (MsgBox.Confirm("Would like to execute to cross hair?"))
                    {
                        Manual_CrossHair_Draw();
                    }
                    break;
                case "btn_Grid_Draw":

                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to Draw Grid ?"))
                    {
                        Manual_CrossHairGridMarking();
                    }
                    break;
                case "btn_Grid_Rect":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to Draw Grid ?"))
                    {
                        Manual_CrossHairGridRectMarking();
                    }
                    break;
                case "btn_Manual_JIGLoading":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to JIG Loading?"))
                    {
                        FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.ToManual, 0, FA.DEF.eManualMode.JIG_LOADING);
                    }
                    break;
                case "btn_Manual_JIGUnloading":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to JIG UnLoading?"))
                    {
                        FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.ToManual, 0, FA.DEF.eManualMode.JIG_UNLOADING);
                    }
                    break;
                case "btn_DM_NO_INSP_MARKING":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to Data Matrix Marking without INSP?"))
                    {
                        // Manual_DM_Marking_NO_INSP_Click();

                        System.Threading.Tasks.Task.Run(() =>
                        {
                            try { Manual_DM_Marking_NO_INSP_Click(); }
                            catch (Exception ex)
                            {
                                FA.LOG.InfoJIG("Manual_DM_Marking_NO_INSP Exception: {0}", ex.Message);
                                this.InvokeIfNeeded(() => MsgBox.Error(string.Format("Marking Error:\n{0}", ex.Message)));
                            }
                        });
                    }
                    break;

                case "btn_DM_INSP":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to Data Matrix INSPECTION?"))
                    {
                        // Manual_DM_Marking_INSP_Click();

                        System.Threading.Tasks.Task.Run(() =>
                        {
                            try { Manual_DM_Marking_INSP_Click(); }
                            catch (Exception ex)
                            {
                                FA.LOG.InfoJIG("Manual_DM_Marking_INSP Exception: {0}", ex.Message);
                                this.InvokeIfNeeded(() => MsgBox.Error(string.Format("INSP Error:\n{0}", ex.Message)));
                            }
                        });
                    }
                    break;

                case "btn_DM_INSP_MARKING":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to execute to Data Matrix Marking with INSP?"))
                    {
                        Manual_DM_Marking_INSP_Execute();
                    }
                    break;
                case "btn_Save_CalibrationFile":
                    if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
                    {
                        MsgBox.Error(string.Format("RunMode : {0} , Stop First", FA.MGR.RunMgr.eRunMode));
                        return;
                    }
                    if (MsgBox.Confirm("Would like to save Calibration File?"))
                    {
                        Manual_GridCalibration();
                    }
                    break;
            }
        }
        private void ManualFuncParamSet(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;

            EzIna.GUI.UserControls.NumberKeypad pNKeyPad;
            EzIna.GUI.UserControls.AlphaKeypad pAlphaKeyPad;
            switch ((sender as Control).Name)
            {
                case "lb_CrossHair_Pitch":
                    double dMax = Math.Min(RTC5.Instance.Move_X_PositiveLimit, RTC5.Instance.Move_Y_PositiveLimit);
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0.0, dMax, m_fCrossHairPitch) == DialogResult.OK)
                    {
                        m_fCrossHairPitch = pNKeyPad.Result;
                        (sender as Control).Text = m_fCrossHairPitch.ToString("F3");
                    }
                    break;

                case "lb_CrossHair_OffsetX":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(RTC5.Instance.Move_X_NegativeLimit + m_fCrossHairPitch / 2.0, RTC5.Instance.Move_X_PositiveLimit - m_fCrossHairPitch / 2.0, m_fCrossHairPitch) == DialogResult.OK)
                    {
                        m_fCrossHairOffsetX = pNKeyPad.Result;
                        (sender as Control).Text = m_fCrossHairOffsetX.ToString("F3");
                    }
                    break;
                case "lb_CrossHair_OffsetY":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(RTC5.Instance.Move_Y_NegativeLimit + m_fCrossHairPitch / 2.0, RTC5.Instance.Move_Y_PositiveLimit - m_fCrossHairPitch / 2.0, m_fCrossHairPitch) == DialogResult.OK)
                    {
                        m_fCrossHairOffsetY = pNKeyPad.Result;
                        (sender as Control).Text = m_fCrossHairOffsetY.ToString("F3");
                    }

                    break;
                case "lb_Grid_X_ArrayCount":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 100, m_uiX_Grid_ArrayCount) == DialogResult.OK)
                    {
                        if (pNKeyPad.Result > 0)
                        {
                            m_uiX_Grid_ArrayCount = (uint)pNKeyPad.Result % 2 == 0 ? (uint)pNKeyPad.Result + 1 : (uint)pNKeyPad.Result;
                        }
                        else
                        {
                            m_uiX_Grid_ArrayCount = 0;
                        }
                        // Manual_GridCalibration();
                        (sender as Control).Text = m_uiX_Grid_ArrayCount.ToString();
                    }
                    break;
                case "lb_Grid_Y_ArrayCount":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 100, m_uiY_Grid_ArrayCount) == DialogResult.OK)
                    {
                        if (pNKeyPad.Result > 0)
                        {
                            m_uiY_Grid_ArrayCount = (uint)pNKeyPad.Result % 2 == 0 ? (uint)pNKeyPad.Result + 1 : (uint)pNKeyPad.Result;
                        }
                        else
                        {
                            m_uiY_Grid_ArrayCount = 0;
                        }
                        // Manual_GridCalibration();
                        (sender as Control).Text = m_uiY_Grid_ArrayCount.ToString();
                    }
                    break;

                case "lb_Gird_X_DrawPitch":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 100, m_fX_Grid_DrawPitch) == DialogResult.OK)
                    {
                        m_fX_Grid_DrawPitch = pNKeyPad.Result;
                        (sender as Control).Text = m_fX_Grid_DrawPitch.ToString("F3");
                    }
                    break;
                case "lb_Gird_Y_DrawPitch":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 100, m_fY_Grid_DrawPitch) == DialogResult.OK)
                    {
                        m_fY_Grid_DrawPitch = pNKeyPad.Result;
                        (sender as Control).Text = m_fY_Grid_DrawPitch.ToString("F3");
                    }
                    break;
                case "lb_Gird_XPitch":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 20, m_fGrid_XPitch) == DialogResult.OK)
                    {
                        m_fGrid_XPitch = pNKeyPad.Result;
                        (sender as Control).Text = m_fGrid_XPitch.ToString("F4");
                    }
                    break;
                case "lb_Gird_YPitch":
                    pNKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pNKeyPad.ShowDialog(0, 20, m_fGrid_YPitch) == DialogResult.OK)
                    {
                        m_fGrid_YPitch = pNKeyPad.Result;
                        (sender as Control).Text = m_fGrid_YPitch.ToString("F4");
                    }
                    break;
                case "lb_DM_Text":
                    pAlphaKeyPad = new GUI.UserControls.AlphaKeypad();
                    EzIna.DataMatrix.eDataMatrixSize pRecipeDMSize = RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                    int iMaxLength = EzIna.DataMatrix.DMGenerater.Instance.GetAlphabetMaxCapacity(pRecipeDMSize);
                    if (pAlphaKeyPad.ShowDialog((uint)iMaxLength, m_strDMMarkingText) == DialogResult.OK)
                    {
                        m_strDMMarkingText = pAlphaKeyPad.NewValue;
                        (sender as Control).Text = m_strDMMarkingText;
                    }
                    break;

                case "lb_DM_MarkingOffsetX":
                    {
                        using (GUI.UserControls.NumberKeypad pNumPad = new GUI.UserControls.NumberKeypad())
                        {
                            if (pNumPad.ShowDialog(-10, 10, m_fDM_MarkingOffSetX) == DialogResult.OK)
                            {
                                m_fDM_MarkingOffSetX = pNumPad.Result;
                                (sender as Control).Text = string.Format("{0:F3}mm", m_fDM_MarkingOffSetX);
                            }
                        }
                    }
                    break;
                case "lb_DM_MarkingOffsetY":
                    {
                        using (GUI.UserControls.NumberKeypad pNumPad = new GUI.UserControls.NumberKeypad())
                        {
                            if (pNumPad.ShowDialog(-10, 10, m_fDM_MarkingOffSetY) == DialogResult.OK)
                            {
                                m_fDM_MarkingOffSetY = pNumPad.Result;
                                (sender as Control).Text = string.Format("{0:F3}mm", m_fDM_MarkingOffSetY);
                            }
                        }
                    }
                    break;
            }
        }


        private void Btn_EmssionLockRelease_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.LASER_EM_ENABLE.GetDO().Value = true;
        }

        private void Btn_EmssionLock_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.LASER_EM_ENABLE.GetDO().Value = false;
        }

        private void Btn_Motor_SPEED_CHANGE(object sender, EventArgs e)
        {
            Button pControl = sender as Button;
            if (pControl != null)
            {
                string strSwitch = pControl.Text;
                switch (strSwitch)
                {
                    case "SLOW":
                        {
                            m_eMotionSpeed = Motion.GDMotion.eSpeedType.SLOW;
                            btn_MotorSpeed_SLOW.ImageIndex = 1;
                            btn_MotorSpeed_FAST.ImageIndex = 0;
                            btn_MotorSpeed_RUN.ImageIndex = 0;
                        }
                        break;
                    case "FAST":
                        {
                            m_eMotionSpeed = Motion.GDMotion.eSpeedType.FAST;
                            btn_MotorSpeed_SLOW.ImageIndex = 0;
                            btn_MotorSpeed_FAST.ImageIndex = 1;
                            btn_MotorSpeed_RUN.ImageIndex = 0;
                        }
                        break;
                    case "RUN":
                        {
                            m_eMotionSpeed = Motion.GDMotion.eSpeedType.RUN;
                            btn_MotorSpeed_SLOW.ImageIndex = 0;
                            btn_MotorSpeed_FAST.ImageIndex = 0;
                            btn_MotorSpeed_RUN.ImageIndex = 1;
                        }
                        break;

                }
            }

        }

        private void btn_PR_LOADING_CLAMP_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.PINCH_ROLLER_L_U.GetDO().Value = true;
            FA.DEF.eDO.PINCH_ROLLER_L_B.GetDO().Value = true;
        }

        private void btn_PR_LOADING_UNCLAMP_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.PINCH_ROLLER_L_U.GetDO().Value = false;
            FA.DEF.eDO.PINCH_ROLLER_L_B.GetDO().Value = false;
        }

        private void btn_PR_UNLOADING_CLAMP_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.PINCH_ROLLER_R_U.GetDO().Value = true;
            FA.DEF.eDO.PINCH_ROLLER_R_B.GetDO().Value = true;
        }

        private void btn_PR_UNLOADING_UNCLAMP_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.DEF.eDO.PINCH_ROLLER_R_U.GetDO().Value = false;
            FA.DEF.eDO.PINCH_ROLLER_R_B.GetDO().Value = false;
        }

        private void Btn_JIG_LOCK_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.ACT.CYL_JIG_FEEDER_L_CLAMP.Run();
            FA.ACT.CYL_JIG_FEEDER_R_CLAMP.Run();
        }

        private void Btn_JIG_Release_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
            FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
        }
        private void Btn_CENTER_STOPPER_UP_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.ACT.CYL_STOPPER_CENTER_UP.Run();

        }

        private void Btn_CENTER_STOPPER_DOWN_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            FA.ACT.CYL_STOPPER_CENTER_DOWN.Run();

        }



        public void UpdateBW8SIM_IMG()
        {
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(m_strSelectedVision.ToUpper(), out item);
            item.UpdateImageOfBW8();
        }
        public void UpdateVision()
        {
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(m_strSelectedVision.ToUpper(), out item);
            item.OnMatrixCode1Display(null, null);
        }

        #region Marking / Cross Hair
        bool m_bMarkingStart = false;
        Stopwatch MarkingTacktimer = new Stopwatch();
        private void MarkingTackTimerFucnc()
        {
            while (m_bMarkingStart)
            {
                Thread.Sleep(1);
                if (MarkingTacktimer.IsRunning == false && RTC5.Instance.IsExecuteList_BUSY == true)
                {
                    if (!MarkingTacktimer.IsRunning)
                        MarkingTacktimer.Restart();
                }
                else if (MarkingTacktimer.IsRunning && RTC5.Instance.IsExecuteList_BUSY == false)
                {
                    MarkingTacktimer.Stop();
                    break;
                }
            }
            m_bMarkingStart = false;
        }
        private void Manual_DM_Marking_NO_INSP_Click()
        {
#if SIM
            m_bMarkingStart = false;
#endif
            if (RTC5.Instance.IsExecuteList_BUSY == false && m_bMarkingStart == false && string.IsNullOrEmpty(m_strDMMarkingText) == false)
            {

                m_bMarkingStart = true;
                double fPercentTemp = 0.0;
                double dNewCenterX = 0.0;
                double dNewCenterY = 0.0;
                float SizeX = 0.0f;
                float SizeY = 0.0f;
                EzCAM_Ver2.Hatch_Option pHatchOption = new EzCAM_Ver2.Hatch_Option();
                RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                RTC5.Instance.ConfigData.FreQPulseLength =
                RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();

                RTC5.Instance.ConfigData.LaserOnDelay = RCP_Modify.PROCESS_SCANNER_LASER_ON_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.LaserOffDelay = RCP_Modify.PROCESS_SCANNER_LASER_OFF_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();

                if (FA.MGR.LaserMgr.IsExistFrequencyTable(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                {
                    Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                    if (pData == null)
                        return;
                    pData.GetPercentFromPower(

                            RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                    LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                    EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                            RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                    pHatchOption.Type =
                            (EzCAM_Ver2.HATCH_TYPE)
                            ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE.GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
                    pHatchOption.fPitch = RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
                    pHatchOption.fAngle = RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
                    pHatchOption.fOffset = RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
                    pHatchOption.bOutline = RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();
                    EzIna.DataMatrix.DM pDataMat = FA.MGR.DMGenertorMgr.CreateDataMatrix(m_strDMMarkingText);
                    pDataMat.HatchOption = pHatchOption.Clone();
                    dNewCenterX += RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                    dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;


                    SizeX = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>();
                    SizeY = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>();
                    pDataMat.CreateCodrdinates(new PointF((float)dNewCenterX
                            , (float)dNewCenterY), new SizeF(SizeX, SizeY));
                    RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.MakeListFromGraphicsPath(pDataMat.CodeGraphicsPath, Scanner.ScanlabRTC5.RTC_LIST._1st, false, false);
                    RTC5.Instance.ListEnd();
                    MarkingTacktimer.Reset();
                    MarkingTacktimer.Stop();
                    m_bMarkingStart = true;
                    //Thread pTackThread = new Thread(MarkingTackTimerFucnc);
                    RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    //pTackThread.Start();

                    m_bMarkingStart = false;
                }
            }
        }

        private void Manual_DM_Marking_INSP_Click()
        {
            // ================================================================
            // STEP 2: 마킹 완료 후 검사
            // 실 프로세스의 검사 파라미터(라이트, 카메라, Vision)를 그대로 사용
            // ================================================================
            FA.LOG.InfoJIG("TestMarking INSP Start");

#if SIM
#else
            // 라이트 설정 + 실 프로세스 MARKING_INSP_Condition_LIGHT_FINISH 와 동일:
            // SetIntensity 후 실제값 폴링 확인
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.LEFT,
                    RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.RIGHT,
                    RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.UP,
                    RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.BOTTOM,
                    RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());

            Stopwatch swLight = Stopwatch.StartNew();
            while (true)
            {
                if (LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.LEFT) == RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.RIGHT) == RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.UP) == RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM) == RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                    break;
                System.Threading.Thread.Sleep(10);
                if (swLight.ElapsedMilliseconds > 3000)
                    break; // 조명 컨트롤러 응답 없으면 그냥 진행
            }
            swLight.Stop();
#endif

            VISION.FINE_LIB.ClearMatrixCode1Results();

#if SIM
                System.Threading.Thread.Sleep(100);
#else
            // 카메라 그랩
            if (!FA.VISION.FINE_CAM.Grab())
            {
                FA.LOG.InfoJIG("TestMarking INSP: Grab Failed");
                this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Failed"));
                return;
            }

            // 그랩 완료 대기 (최대 5초)
            Stopwatch swGrab = Stopwatch.StartNew();
            while (!FA.VISION.FINE_CAM.IsGrab())
            {
                System.Threading.Thread.Sleep(10);
                if (swGrab.ElapsedMilliseconds > 5000)
                {
                    FA.LOG.InfoJIG("TestMarking INSP: Grab Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Timeout (5s)"));
                    return;
                }
            }
            swGrab.Stop();
#endif

            // ----------------------------------------------------------------
            // ROI 계산: 이미지 중앙 기준
            // 실 프로세스는 위치검사(MatchResult.SensorXPos/Y) 기준으로 ROI를 잡지만,
            // 테스트는 스캐너 센터(이미지 중앙)에 마킹했으므로 이미지 중앙을 ROI 중심으로 사용
            // ----------------------------------------------------------------
            double fPixelResX = RCP.M100_VisionCalFineScaleX.AsDouble / 1000.0;
            double fPixelResY = RCP.M100_VisionCalFineScaleY.AsDouble / 1000.0;
            double fMarkWidthPx = fPixelResX > 0
                    ? RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>() / fPixelResX
                    : 0;
            double fMarkHeightPx = fPixelResY > 0
                    ? RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>() / fPixelResY
                    : 0;

            double fImgCenterX = VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW / 2.0;
            double fImgCenterY = VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH / 2.0;

            Rectangle[] testROIs = new Rectangle[]
            {
                        new Rectangle(
                                (int)(fImgCenterX - fMarkWidthPx),
                                (int)(fImgCenterY - fMarkHeightPx / 1.2),
                                (int)(fMarkWidthPx * 2.0),
                                (int)(fMarkHeightPx * 1.6))
            };

            string strTestSavePath = string.Format(
                    "d:\\PROC_IMG\\TEST\\{0}\\",
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            // DataMatrix 검사 실행 (실 프로세스 MARKING_INSP_RUN_START 와 동일 방식)
            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(30);
            VISION.FINE_LIB.MatrixCode1MultiRun(
                    (int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                    testROIs,
                    4.0f,
                    true,
                    strTestSavePath,
                    new string[] { m_strDMMarkingText });

            // 검사 결과 대기 (최대 35초, 실 프로세스 MARKING_INSP_RUN_FINISH 방식)
            Stopwatch swInsp = Stopwatch.StartNew();
            while (VISION.FINE_LIB.GetMatrixCode1TotalResultCount() <= 0)
            {
                System.Threading.Thread.Sleep(100);
                if (swInsp.ElapsedMilliseconds > 35000)
                {
                    FA.LOG.InfoJIG("TestMarking INSP: Inspection Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Inspection Timeout (35s)"));
                    return;
                }
            }
            swInsp.Stop();

            // 결과 수집
            List<EzInaVision.GDV.MatrixCodeResult> pResultList;
            VISION.FINE_LIB.GetMatrixCode1ResultList(out pResultList);

            string strResult = "No Result";
            if (pResultList != null && pResultList.Count > 0)
            {
                var r = pResultList[0];
                if (r.m_bFound)
                {
                    if (string.Equals(m_strDMMarkingText, r.m_strDecodedString))
                    {
                        strResult = string.Format(
                                "[OK]\nExpected : {0}\nRead     : {1}",
                                m_strDMMarkingText, r.m_strDecodedString);
                    }
                    else
                    {
                        strResult = string.Format(
                                "[MIS MATCH]\nExpected : {0}\nRead     : {1}",
                                m_strDMMarkingText, r.m_strDecodedString);
                    }
                }
                else
                {
                    strResult = string.Format(
                            "[NOT FOUND]\nExpected : {0}", m_strDMMarkingText);
                }
            }

            FA.LOG.InfoJIG("TestMarking INSP Result: {0}",
                    strResult.Replace("\n", " | "));

            this.InvokeIfNeeded(() =>
                    MsgBox.Show(string.Format(
                            "DM Marking + INSP Test Result\n\n{0}", strResult)));

            if (FA.VISION.FINE_CAM.IsGrab())
            {
                FA.VISION.FINE_CAM.Live();
                FA.LOG.InfoJIG("FINE_CAM Switch to Live on Grab");
            }
        }

        private bool Manual_DM_Marking_INSP_Execute()
        {
            List<EzInaVision.GDV.MatchResult> pTemp;
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(m_strSelectedVision, out item);
            FA.MGR.VisionMgr.vecLibraries[0].MatchResult(item.eSelectedGoldenImage, item.eSelectedRoiItem, out pTemp);

            if (pTemp == null)
            {
                MsgBox.Show("Match OBJ Not Found ");
                return false;
            }
            var pList = pTemp.ToList();
            if (pList.Count > 1)
            {
                MsgBox.Show("Match First ,1 OBJ Find");
                return false;
            }

#if SIM
            m_bMarkingStart = false;
#endif
            double dNewCenterX;
            double dNewCenterY;

            if (FA.MGR.VisionToScannerCalb.IsLoad())
            {
                if (RTC5.Instance.IsExecuteList_BUSY == false && m_bMarkingStart == false && string.IsNullOrEmpty(m_strDMMarkingText) == false)
                {
                    m_bMarkingStart = true;
                    double fPercentTemp = 0.0;
                    float SizeX = 0.0f;
                    float SizeY = 0.0f;
                    EzCAM_Ver2.Hatch_Option pHatchOption = new EzCAM_Ver2.Hatch_Option();
                    RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                    RTC5.Instance.ConfigData.FreQPulseLength =
                    RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                    RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();

                    RTC5.Instance.ConfigData.LaserOnDelay = RCP_Modify.PROCESS_SCANNER_LASER_ON_DELAY.GetValue<double>();
                    RTC5.Instance.ConfigData.LaserOffDelay = RCP_Modify.PROCESS_SCANNER_LASER_OFF_DELAY.GetValue<double>();
                    RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                    RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                    RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                    RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();

                    if (FA.MGR.LaserMgr.IsExistFrequencyTable(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                    {
                        Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                        if (pData == null)
                            return false;
                        pData.GetPercentFromPower(
                                RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                        LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                        EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                                RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                        pHatchOption.Type =
                                (EzCAM_Ver2.HATCH_TYPE)
                                ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE.GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
                        pHatchOption.fPitch = RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
                        pHatchOption.fAngle = RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
                        pHatchOption.fOffset = RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
                        pHatchOption.bOutline = RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();
                        FA.MGR.VisionToScannerCalb.CorrectPosUsingCalFile(pList[0].m_fSensorXPos, pList[0].m_fSensorYPos, out dNewCenterX, out dNewCenterY);
                        EzIna.DataMatrix.DM pDataMat = FA.MGR.DMGenertorMgr.CreateDataMatrix(m_strDMMarkingText);
                        pDataMat.HatchOption = pHatchOption.Clone();
                        dNewCenterX += RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                        dNewCenterX += RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_X.GetValue<double>();
                        dNewCenterX += m_fDM_MarkingOffSetX;
                        dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;
                        dNewCenterY += RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_Y.GetValue<double>();
                        dNewCenterY += m_fDM_MarkingOffSetY;

                        SizeX = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>();
                        SizeY = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>();
                        pDataMat.CreateCodrdinates(new PointF((float)dNewCenterX
                                , (float)dNewCenterY), new SizeF(SizeX, SizeY));
                        RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                        RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
                        RTC5.Instance.MakeListFromGraphicsPath(pDataMat.CodeGraphicsPath, Scanner.ScanlabRTC5.RTC_LIST._1st, false, false);
                        RTC5.Instance.ListEnd();
                        MarkingTacktimer.Reset();
                        MarkingTacktimer.Stop();
                        m_bMarkingStart = true;
                        //Thread pTackThread = new Thread(MarkingTackTimerFucnc);
                        RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                        m_bMarkingStart = false;
                        //pTackThread.Start();
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Manual_CrossHair_Draw()
        {
            List<EzInaVision.GDV.MatchResult> pTemp;
            double dNewCenterX = 0.0;
            double dNewCenterY = 0.0;

            if (RTC5.Instance.IsExecuteList_BUSY == false && m_bMarkingStart == false)
            {
                m_bMarkingStart = true;
                double fPercentTemp = 0.0;
                RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                RTC5.Instance.ConfigData.FreQPulseLength =
                RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();
                RTC5.Instance.ConfigData.LaserOnDelay = 0; //RCP_Modify.PROCESS_SCANNER_LASER_ON_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.LaserOffDelay = 0; //RCP_Modify.PROCESS_SCANNER_LASER_OFF_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();

                Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                if (pData == null)
                    return false;
                pData.GetPercentFromPower(

                        RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                dNewCenterX = m_fCrossHairOffsetX;
                dNewCenterX += RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                dNewCenterY = m_fCrossHairOffsetY;
                dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;
                RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
                RTC5.Instance.ListCrossLine(dNewCenterX, dNewCenterY, m_fCrossHairPitch, m_fCrossHairPitch);
                RTC5.Instance.ListEnd();
                RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                m_bMarkingStart = false;
                return true;
            }
            return false;
        }

        private bool Manual_CrossHairGridMarking()
        {
            double dNewCenterX = 0.0;
            double dNewCenterY = 0.0;


            if (RTC5.Instance.IsExecuteList_BUSY == false)
            {
                double fPercentTemp = 0.0;

                RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                RTC5.Instance.ConfigData.FreQPulseLength =
                RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();

                RTC5.Instance.ConfigData.LaserOnDelay = 0;
                RTC5.Instance.ConfigData.LaserOffDelay = 0;
                RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();
                if (FA.MGR.LaserMgr.IsExistFrequencyTable(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                {
                    Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                    if (pData == null)
                        return false;
                    pData.GetPercentFromPower(

                    RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                    LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                    dNewCenterX += RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                    dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;
                    RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);

                    RTC5.Instance.ListCrossGrid(
                        dNewCenterX, dNewCenterY,
                        m_fGrid_XPitch, m_fGrid_YPitch,
                        (int)m_uiY_Grid_ArrayCount, (int)m_uiX_Grid_ArrayCount,
                        m_fX_Grid_DrawPitch, m_fY_Grid_DrawPitch
                        );
                    RTC5.Instance.ListEnd();
                    RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    return true;
                }
            }
            return false;
        }
        private bool Manual_CrossHairGridRectMarking()
        {
            double dNewCenterX = 0.0;
            double dNewCenterY = 0.0;


            if (RTC5.Instance.IsExecuteList_BUSY == false)
            {
                double fPercentTemp = 0.0;

                RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                RTC5.Instance.ConfigData.FreQPulseLength =
                RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();

                RTC5.Instance.ConfigData.LaserOnDelay = 0;
                RTC5.Instance.ConfigData.LaserOffDelay = 0;
                RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();
                if (FA.MGR.LaserMgr.IsExistFrequencyTable(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                {
                    Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                    pData.GetPercentFromPower(

                    RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                    LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                    dNewCenterX += RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                    dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;
                    RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);

                    RTC5.Instance.ListCrossGridOutLineRect(
                        dNewCenterX, dNewCenterY,
                        m_fGrid_XPitch, m_fGrid_YPitch,
                        (int)m_uiY_Grid_ArrayCount, (int)m_uiX_Grid_ArrayCount,
                        m_fX_Grid_DrawPitch, m_fY_Grid_DrawPitch
                        );
                    RTC5.Instance.ListEnd();
                    RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    return true;
                }
            }
            return false;
        }
        private void Manual_GridCalibration()
        {
            try
            {
                string strFilePath = string.Format(@"{0}.csv", "D:\\TestCoordinate");//, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                EzIna.FrmVisionOfPanel item = null;
                EzIna.FA.MGR.dicFrmVisions.TryGetValue(m_strSelectedVision, out item);

                using (FileStream f = new FileStream(strFilePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(f))
                    {
                        EzInaVision.GDV.MatchResult Temp;
                        List<KeyValuePair<int, EzInaVision.GDV.MatchResult>> TempList = VISION.FINE_LIB.m_LibInfo.m_vecMatchResults[(int)item.eSelectedRoiItem][(int)item.eSelectedGoldenImage].ToList();
                        List<KeyValuePair<int, EzInaVision.GDV.MatchResult>> SortTempList;
                        List<KeyValuePair<int, EzInaVision.GDV.MatchResult>> SortRowList;
                        List<KeyValuePair<int, EzInaVision.GDV.MatchResult>> SortTemp;
                        if (TempList.Count < m_uiX_Grid_ArrayCount * m_uiY_Grid_ArrayCount)
                        {
                            MsgBox.Error(string.Format("Match Less Object\nMatch : {0} // {1}", TempList != null ? TempList.Count : 0, m_uiX_Grid_ArrayCount * m_uiY_Grid_ArrayCount));
                            return;
                        }
                        SortTempList = TempList.OrderBy(p => p.Value.m_fSensorYPos).ToList();
                        //// Header
                        sw.WriteLine("Array X Count, Array Y Count,Start X ,Start Y, ResolutionX , ResolutionY");
                        sw.WriteLine(string.Format("{0},{1},{2:F3},{3:F3},{4:F3},{5:F3}", m_uiX_Grid_ArrayCount, m_uiY_Grid_ArrayCount, -((m_uiX_Grid_ArrayCount - 1) * m_fGrid_XPitch) / 2.0, ((m_uiY_Grid_ArrayCount - 1) * m_fGrid_YPitch) / 2.0, m_fGrid_XPitch, -m_fGrid_YPitch));
                        for (int i = 0; i < m_uiX_Grid_ArrayCount; i++)
                        {
                            SortRowList = SortTempList.GetRange(i * (int)m_uiX_Grid_ArrayCount, (int)m_uiY_Grid_ArrayCount).ToList();
                            SortTemp = SortRowList.OrderBy(x => x.Value.m_fSensorXPos).ToList();
                            for (int j = 0; j < m_uiY_Grid_ArrayCount; j++)
                            {
                                sw.Write(string.Format("{0:0000.000},{1:0000.000}", SortTemp[j].Value.m_fSensorXPos, SortTemp[j].Value.m_fSensorYPos));
                                sw.Write("\t");
                            }
                            sw.WriteLine();
                        }
                        sw.Close();
                    }
                    f.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private bool GetProcessIDX(out int a_bufIDX, float a_fSensorX, float a_fSensorY, float a_fWidth, float a_fHeight)
        {
            a_bufIDX = -1;
            float fSensorRowPitchPixel = RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<float>() / RCP.M100_VisionCalFineScaleY.AsSingle * 1000;
            // To do List Parameter -> Recipe  
            float fErrAllowPixel_XPitch = (float)RCP_Modify.INSP_Pixel_Error_Range.GetValue<double>() / RCP.M100_VisionCalFineScaleX.AsSingle * 1000;
            float fErrAllowPixel_YPitch = (float)RCP_Modify.INSP_Pixel_Error_Range.GetValue<double>() / RCP.M100_VisionCalFineScaleY.AsSingle * 1000;
            // To do List Parameter -> Recipe 
            RectangleF m_pProcessIDXRectTemp = RectangleF.Empty;
            int iRowIndexMax = RCP_Modify.Inspection_RowCount.GetValue<int>();
            for (int i = 0; i < iRowIndexMax; i++)
            {
                m_pProcessIDXRectTemp.X = RCP_Modify.INSP_1st_Pixel_Center_X.GetValue<float>() - ((a_fWidth + fErrAllowPixel_XPitch) / 2.0f);
                m_pProcessIDXRectTemp.Y = RCP_Modify.INSP_1st_Pixel_Center_Y.GetValue<float>() - ((a_fHeight + fErrAllowPixel_YPitch) / 2.0f) + i * fSensorRowPitchPixel;
                m_pProcessIDXRectTemp.Width = a_fWidth + fErrAllowPixel_XPitch;
                m_pProcessIDXRectTemp.Height = a_fHeight + fErrAllowPixel_YPitch;
                if (m_pProcessIDXRectTemp.Contains(new PointF(a_fSensorX, a_fSensorY)))
                {
                    if (RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>() == eRecipeRowProgressDir.DOWN)
                    {
                        a_bufIDX = i;
                    }
                    else
                    {
                        a_bufIDX = iRowIndexMax - i - 1;
                    }
                    return true;
                }
            }
            return false;
        }

        List<Rectangle> m_pDataMatrixInspRectList = new List<Rectangle>();
        List<string> m_pDataMatrixInspNGFileNames = new List<string>();
        List<Tuple<MF.RecipeItemBase, MF.RecipeItemBase>> m_pMarkingOffSetArray = new List<Tuple<MF.RecipeItemBase, MF.RecipeItemBase>>();
        private void BTN_DM_TEST_INSP_Click(object sender, EventArgs e)
        {
            List<EzInaVision.GDV.MatchResult> pTemp;
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(m_strSelectedVision, out item);
            FA.MGR.VisionMgr.vecLibraries[0].MatchResult(item.eSelectedGoldenImage, item.eSelectedRoiItem, out pTemp);

            System.Reflection.FieldInfo[] fieldInfos = typeof(FA.RCP_Modify).GetFields(BindingFlags.Public | BindingFlags.Static);
            System.Reflection.FieldInfo pFieldNX;
            System.Reflection.FieldInfo pFieldNY;
            MF.RecipeItemBase pMarkingOff_NX;
            MF.RecipeItemBase pMarkingOff_NY;
            for (int i = 0; i < 10; i++)
            {
                pFieldNX = typeof(FA.RCP_Modify).GetField(string.Format("PROCESS_DATA_MAT_MARKING_OFFSET_E_X_{0}", i), BindingFlags.Public | BindingFlags.Static);
                pFieldNY = typeof(FA.RCP_Modify).GetField(string.Format("PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_{0}", i), BindingFlags.Public | BindingFlags.Static);
                pMarkingOff_NX = null;
                pMarkingOff_NY = null;
                if (pFieldNX != null && pFieldNY != null)
                {
                    pMarkingOff_NX = pFieldNX.GetValue(null) as MF.RecipeItemBase;
                    pMarkingOff_NY = pFieldNY.GetValue(null) as MF.RecipeItemBase;
                    m_pMarkingOffSetArray.Add(new Tuple<MF.RecipeItemBase, MF.RecipeItemBase>
                        (pMarkingOff_NX, pMarkingOff_NY));
                }
            }
            m_pDataMatrixInspRectList.Clear();
            m_pDataMatrixInspNGFileNames.Clear();
            double m_fPixelResoltuionX = (RCP.M100_VisionCalFineScaleX.AsDouble / 1000.0);
            double m_fPixelResoltuionY = (RCP.M100_VisionCalFineScaleY.AsDouble / 1000.0);
            double m_fMarkingWidthPixel = m_fPixelResoltuionX > 0 ? RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>() / m_fPixelResoltuionX : 0;
            double m_fMarkingHeightPixel = m_fPixelResoltuionY > 0 ? RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>() / m_fPixelResoltuionY : 0;
            double m_fPixelErrorWidth = m_fPixelResoltuionX > 0 ? 0.4 / m_fPixelResoltuionX : 0;

            double m_fMarkingOffSetX = 0.0;
            double m_fMarkingOffSetY = 0.0;

            double m_fMarkingCenterXPixel = 0.0;
            double m_fMarkingCenterYPixel = 0.0;
            string strDM_NG_SavePath = @"D:\\";
            int iIDX = 0;

            for (int i = 0; i < pTemp.Count; i++)
            {
                if (GetProcessIDX(out iIDX,
                    pTemp[i].m_fSensorXPos,
                    pTemp[i].m_fSensorYPos,
                    pTemp[i].m_fMatchWidth,
                    pTemp[i].m_fMatchHeight
                   ) == false)
                    continue;
                m_fMarkingOffSetX = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_X.GetValue<double>() + m_pMarkingOffSetArray[iIDX].Item1.GetValue<double>();
                m_fMarkingOffSetY = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_Y.GetValue<double>() + m_pMarkingOffSetArray[iIDX].Item2.GetValue<double>();


                m_fMarkingCenterXPixel = pTemp[i].m_fSensorXPos +
                                                                (m_fPixelResoltuionX > 0 ? m_fMarkingOffSetX / m_fPixelResoltuionX :
                                                                 0);
                m_fMarkingCenterYPixel = pTemp[i].m_fSensorYPos +
                                                                (m_fPixelResoltuionY > 0 ? -1 * m_fMarkingOffSetY / m_fPixelResoltuionY :
                                                                 0);
                m_pDataMatrixInspRectList.Add(
                                new Rectangle(
                                (int)(m_fMarkingCenterXPixel - (m_fMarkingWidthPixel / 1.0)),
                                (int)(m_fMarkingCenterYPixel - m_fMarkingHeightPixel / 1.2),
                                (int)(m_fMarkingWidthPixel * 2.0),
                                (int)(m_fMarkingHeightPixel * 1.6)
                                ));
                m_pDataMatrixInspNGFileNames.Add($"DMTest{i + 1}");
            }
            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(120);

            //if (pTemp.Count > 0)
            //{
            //    DateTime pCurrentTime = DateTime.Now;
            //    strDM_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\{1}_{2}_{3}\\DM_INSP\\{4}~{5}\\",
            //                                                                            m_SubSeqpProcessStartTime.ToString("yyyyMMdd"),
            //                                                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
            //                                                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
            //                                                                            FA.MGR.RecipeRunningData.pCurrentProcessData.MES_MarkingStartSendCompleteTime.ToString("yyyyMMddHHmmss.ffff"),
            //                                                                            m_pProcessDataList[0].strMarkingIDX,//m_pProcessDataList[0].strMarkingIDX_TO_32,
            //                                                                            m_pProcessDataList[m_pProcessDataList.Count - 1].strMarkingIDX//m_pProcessDataList[m_pProcessDataList.Count-1].strMarkingIDX_TO_32																																
            //    );
            //}
            //else
            //{
            //    strDM_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\DM_INSP\\", m_SubSeqpProcessStartTime.ToString("yyyyMMdd"));
            //}
            VISION.FINE_LIB.MatrixCode1MultiRun((int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                    m_pDataMatrixInspRectList.ToArray(),
                    4.0f,
                    true,
                    strDM_NG_SavePath,
                    m_pDataMatrixInspNGFileNames.ToArray()
                    );
        }

        private void btn_IMG_LOAD_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "bmp";
                dlg.Filter = "Image files (*.bmp,*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.tiff) |*.bmp;*.jpg; *.jpeg; *.jpe; *.jfif; *.png;*.tiff";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap pBMP = new Bitmap(dlg.FileName, true))
                    {

                        switch (pBMP.PixelFormat)
                        {
                            case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                                {
                                    VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW = pBMP.Width;
                                    VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH = pBMP.Height;
                                    VISION.FINE_LIB.SetBitmapToEImageBW8(pBMP);
                                    UpdateBW8SIM_IMG();
                                    break;
                                }
                            default:
                                VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW = pBMP.Width;
                                VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH = pBMP.Height;
                                VISION.FINE_LIB.SetColorBitmapToEImageBW8(pBMP);
                                UpdateBW8SIM_IMG();
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
    }
}

