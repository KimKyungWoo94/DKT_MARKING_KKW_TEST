using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzIna.Motion;
using EzIna.Motion.AXT.GUI;
using EzInaGui;
namespace EzIna
{
    public partial class FrmInforSetupMotion_SXGA : Form
    {

        enum MOVING_MODE { }
        Timer m_Timer = null;

        Dictionary<int, Button> m_dicButtonList = null;

        bool m_bIsSlow;
        bool m_bIsRel;
        bool m_bFlicker;

        GDMotion.eRepetitiveMotion m_eRepeatStatus;
        int m_nRepeatCurrCnt;
        int m_nRepeatMaximumCnt;
        int m_nRepeatDelay;
        double m_fTarget1;
        double m_fTarget2;
        EzIna.UC.StopWatchTimer m_StopWatchForMotion = null;
        EzIna.UC.StopWatchTimer m_StopWatchForDisplay = null;
        BindingSpeedSetting FastSpeedBinding = new BindingSpeedSetting();
        BindingSpeedSetting SlowSpeedBinding = new BindingSpeedSetting();
        BindingSpeedSetting RunSpeedBinding = new BindingSpeedSetting();
        BindingSpeedSetting userSpeedBinding = new BindingSpeedSetting();

        bool btest = false;
        CMotionAXT_Config_GUI pAXTConfigGUI;
        CMotionAXT_HOMECONFIG_GUI pAXTHomeConfigGUI;


        readonly int m_iTaskID = 1;
        EzIna.Motion.MotionInterface pitem;
        public FrmInforSetupMotion_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();
            m_Timer.Interval = 50;
            m_Timer.Tick += new EventHandler(this.Display);
            m_Timer.Enabled = false;

            btnMinusJog.MouseDown += new MouseEventHandler(JogMoveMouseDown);
            btnMinusJog.MouseUp += new MouseEventHandler(JogMoveMouseUp);

            btnPlusJog.MouseDown += new MouseEventHandler(JogMoveMouseDown);
            btnPlusJog.MouseUp += new MouseEventHandler(JogMoveMouseUp);

            m_dicButtonList = new Dictionary<int, Button>();
            m_dicButtonList.Add(Convert.ToInt32(btnSlowSpeed.Tag.ToString()), btnSlowSpeed);
            m_dicButtonList.Add(Convert.ToInt32(btnFastSpeed.Tag.ToString()), btnFastSpeed);
            m_dicButtonList.Add(Convert.ToInt32(btnRelative.Tag.ToString()), btnRelative);
            m_dicButtonList.Add(Convert.ToInt32(btnAbs.Tag.ToString()), btnAbs);
            m_dicButtonList.Add(Convert.ToInt32(btnMoveToTarget1.Tag.ToString()), btnMoveToTarget1);
            m_dicButtonList.Add(Convert.ToInt32(btnMoveToTarget2.Tag.ToString()), btnMoveToTarget2);
            m_dicButtonList.Add(Convert.ToInt32(btnRepetitiveMotion.Tag.ToString()), btnRepetitiveMotion);

            for (int i = 0; i < m_dicButtonList.Count; i++)
            {
                m_dicButtonList[i].MouseClick += new MouseEventHandler(ClickButtonInTabMovingPanel);
            }

            m_bIsSlow = true;
            m_bIsRel = true;
            m_bFlicker = false;
            m_eRepeatStatus = GDMotion.eRepetitiveMotion.NONE;
            m_nRepeatCurrCnt = 0;
            m_nRepeatMaximumCnt = 0;
            m_nRepeatDelay = 0;
            m_fTarget1 = m_fTarget2 = 0.0;
            m_StopWatchForMotion	= new EzIna.UC.StopWatchTimer();
            m_StopWatchForDisplay	= new EzIna.UC.StopWatchTimer();



            pAXTConfigGUI = new CMotionAXT_Config_GUI();
            pAXTHomeConfigGUI = new CMotionAXT_HOMECONFIG_GUI();

            //pAXTConfigGUI.Parent=tabSetting.TabPages[1];


            
            //pAXTConfigGUI_Page =new TabPage("Config");
            //pAXTConfigGUI_Page.Controls.Add(pAXTConfigGUI);

            //pAXTConfigGUI.Controls.Add(new EzInaMotion.GUI.CMotionAXT_HOMECONFIG_GUI());
            // tabSetting.TabPages.Add(pAXTConfigGUI);
            // pAXTHomeConfigGUI =new EzInaMotion.GUI.CMotionAXT_HOMECONFIG_GUI();
            InitBidingSpeed();
            this.groupBox17.DoubleBuffered(true);
            this.DoubleBuffered(true);
        }
        private void InitBidingSpeed()
        {

            tbCurveDec_User.DataBindings.Add(this.userSpeedBinding.BindingCurveDecel);
            tbCurveAcc_User.DataBindings.Add(this.userSpeedBinding.BindingCurveAccel);
            tbDec_User.DataBindings.Add(this.userSpeedBinding.BindingDecel);
            tbAcc_User.DataBindings.Add(this.userSpeedBinding.BindingAccel);
            tbSpeed_User.DataBindings.Add(this.userSpeedBinding.BindingMaxSpeed);

            tbCurveDec_Run.DataBindings.Add(this.RunSpeedBinding.BindingCurveDecel);
            tbCurveAcc_Run.DataBindings.Add(this.RunSpeedBinding.BindingCurveAccel);
            tbDec_Run.DataBindings.Add(this.RunSpeedBinding.BindingDecel);
            tbAcc_Run.DataBindings.Add(this.RunSpeedBinding.BindingAccel);
            tbSpeed_Run.DataBindings.Add(this.RunSpeedBinding.BindingMaxSpeed);

            tbCurveDec_Fast.DataBindings.Add(this.FastSpeedBinding.BindingCurveDecel);
            tbCurveAcc_Fast.DataBindings.Add(this.FastSpeedBinding.BindingCurveAccel);
            tbDec_Fast.DataBindings.Add(this.FastSpeedBinding.BindingDecel);
            tbAcc_Fast.DataBindings.Add(this.FastSpeedBinding.BindingAccel);
            tbSpeed_Fast.DataBindings.Add(this.FastSpeedBinding.BindingMaxSpeed);
            tbCurveDec_Slow.DataBindings.Add(this.SlowSpeedBinding.BindingCurveDecel);
            tbCurveAcc_Slow.DataBindings.Add(this.SlowSpeedBinding.BindingCurveAccel);
            tbDec_Slow.DataBindings.Add(this.SlowSpeedBinding.BindingDecel);
            tbAcc_Slow.DataBindings.Add(this.SlowSpeedBinding.BindingAccel);
            tbSpeed_Slow.DataBindings.Add(this.SlowSpeedBinding.BindingMaxSpeed);
        }

        private void ClickButtonInTabMovingPanel(object a_Obj, MouseEventArgs e)
        {

            MotionItem pitemVariable = pitem as MotionItem;
            if (pitem == null || pitemVariable==null ) return;

            if (!pitemVariable.IsMotionDone) return;

          

            //             if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == (int)eSelectedAxis
            //                 || FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis == (int)eSelectedAxis)
            //             {
            //                 if (FA.MGR.MotionMgr.IsActiveGantry() == false
            //                     && FA.MGR.MotionMgr.GetItem(GDMotion.eMotorNameGantry.M_GANTRY_M_Y).bIsServoOn
            //                     && FA.MGR.MotionMgr.GetItem(GDMotion.eMotorNameGantry.M_GANTRY_S_Y).bIsServoOn)
            //                 {
            //                     return;
            //                 }
            //             }

            int iTag = Convert.ToInt32(((Button)a_Obj).Tag.ToString());
            if (iTag < 0 || iTag > 6) return;

            string strMsg = "";
            switch (iTag)
            {
                case 0: //slow
                    m_bIsSlow = true;
                    return;
                case 1: //fast
                    m_bIsSlow = false;
                    return;
                case 2: //Relative motion
                    m_bIsRel = true;
                    return;
                case 3: //Absolute motion
                    m_bIsRel = false;
                    return;
                case 4://Move to target1
                    strMsg = "Would you like to move to the position of target 1";
                    break;
                case 5://Move to target2
                    strMsg = "Would you like to move to the position of target 2";
                    break;
                case 6://Repetitive motion. 
                    strMsg = "Would you like repeat motion from target 1 to target 2";
                    break;
                default:
                    return;
            }

            if (iTag >= m_dicButtonList.Count)
                return;

						double.TryParse(textBoxTargetPosition1.Text,out m_fTarget1);
						double.TryParse(textBoxTargetPosition2.Text,out m_fTarget2);
        						
						if(m_bIsRel==false)
						{
								if (!pitemVariable.IsHomeComplete)
								{
										MsgBox.Error("Absolute Move Require Home Complete , Home First");
								}
						}
            if (MessageBox.Show(strMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (iTag == 4)
                {
                    if (m_bIsRel) //Relative
                    {
                        if (m_bIsSlow)
                        {
                            pitem.Move_Relative(m_fTarget1, GDMotion.eSpeedType.SLOW);
                        }

                        else
                        {
                            pitem.Move_Relative(m_fTarget1, GDMotion.eSpeedType.FAST);
                        }

                    }
                    else
                    {
												
                        if (m_bIsSlow)
                        {
                            pitem.Move_Absolute(m_fTarget1, GDMotion.eSpeedType.SLOW);
                        }

                        else
                        {

                            pitem.Move_Absolute(m_fTarget1, GDMotion.eSpeedType.FAST);
                        }

                    }//
                }
                if (iTag == 5) //absolute
                {
                    if (m_bIsRel)
                    {
                        if (m_bIsSlow)
                        {
                            pitem.Move_Relative(m_fTarget2, GDMotion.eSpeedType.SLOW);
                        }

                        else
                        {
                            pitem.Move_Relative(m_fTarget2, GDMotion.eSpeedType.FAST);
                        }

                    }
                    else
                    {
                        if (m_bIsSlow)
                        {
                            pitem.Move_Absolute(m_fTarget2, GDMotion.eSpeedType.SLOW);
                        }

                        else
                        {
                            pitem.Move_Absolute(m_fTarget2, GDMotion.eSpeedType.FAST);
                        }

                    }
                }

                if (iTag == 6) //Repeat
                {
                    if (m_eRepeatStatus != GDMotion.eRepetitiveMotion.NONE)
                        return;

                    m_eRepeatStatus = GDMotion.eRepetitiveMotion.RDY;
                    m_nRepeatCurrCnt = 0;
                    m_nRepeatMaximumCnt = Convert.ToInt32(textBox_Repeatcount.Text);
                    m_nRepeatDelay = Convert.ToInt32(textBox_Repeatdelay.Text);
                    m_fTarget1 = Convert.ToDouble(textBoxTargetPosition1.Text);
                    m_fTarget2 = Convert.ToDouble(textBoxTargetPosition2.Text);
                }
            }
        }
        private void JogMoveMouseDown(object a_Obj, EventArgs e)
        {
            if (FA.MGR.MotionMgr != null)
            {
                MotionItem item = pitem as MotionItem;

                if (pitem != null)
                {
                    if (!item.IsMotionDone)
                        return;

                    bool bDirPlus = Convert.ToInt32(((Button)a_Obj).Tag.ToString()) == 1 ? true : false;
                    double fPercent = ucDadaTrackBar_JogSpeed.Value / 100.0;
                    pitem.Move_Jog(bDirPlus, GDMotion.eSpeedType.FAST, fPercent);
                }
            }
        }

        private void JogMoveMouseUp(object a_Obj, EventArgs e)
        {
            if (FA.MGR.MotionMgr != null)
            {              
                pitem.JOG_STOP();
            }
        }

        private void Display(object sender, EventArgs e)
        {
            try
            {


                if (FA.MGR.MotionMgr == null)
                    return;
                this.SuspendLayout();
                ReadMotionStatus();
                ReadPositionStatus();
                MotionItem item = pitem as MotionItem;

                if (item == null)
                    return;


                #region GUI

                if (m_dicButtonList.Count > 1)
                {
                    m_dicButtonList[0].BackColor = m_bIsSlow ? GUI.D.clCheckedButton : GUI.D.clUncheckedButton;
                    m_dicButtonList[1].BackColor = !m_bIsSlow ? GUI.D.clCheckedButton : GUI.D.clUncheckedButton;
                }
                if (m_dicButtonList.Count > 3)
                {
                    m_dicButtonList[2].BackColor = m_bIsRel ? GUI.D.clCheckedButton : GUI.D.clUncheckedButton;
                    m_dicButtonList[3].BackColor = !m_bIsRel ? GUI.D.clCheckedButton : GUI.D.clUncheckedButton;
                }
                if (m_StopWatchForDisplay.IsDone)
                {
                    m_bFlicker = !m_bFlicker;
                    m_StopWatchForDisplay.SetDelay = 1000;
                }
                label_MotorName.ForeColor = m_bFlicker ? label_MotorName.BackColor : Color.White;
                #endregion GUI 

                pitem.HomeReadStatus();

                if (!item.IsMotionDone)
                    return;

                switch (m_eRepeatStatus)
                {
                    case GDMotion.eRepetitiveMotion.RDY:
                        {
                            if (m_nRepeatCurrCnt >= m_nRepeatMaximumCnt)
                                m_eRepeatStatus = GDMotion.eRepetitiveMotion.NONE;
                            else
                                m_eRepeatStatus = GDMotion.eRepetitiveMotion.MOV_POS_0;
                        }
                        break;

                    case GDMotion.eRepetitiveMotion.MOV_POS_0:
                        {
                            if (m_bIsRel)
                            {
                                if (m_bIsSlow)
                                    pitem.Move_Relative(m_fTarget1, GDMotion.eSpeedType.SLOW);
                                else
                                    pitem.Move_Relative(m_fTarget1, GDMotion.eSpeedType.FAST);
                            }
                            else
                            {
                                if (m_bIsSlow)
                                    pitem.Move_Absolute(m_fTarget1, GDMotion.eSpeedType.SLOW);
                                else
                                    pitem.Move_Absolute(m_fTarget1, GDMotion.eSpeedType.FAST);
                            }
                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.CHK_POS_0;
                            label_RepeatStatus.Text = "MOV_POS_1";
                        }
                        break;
                    case GDMotion.eRepetitiveMotion.CHK_POS_0:
                        {
                            m_StopWatchForMotion.SetDelay = m_nRepeatDelay;
                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.MOV_DLY_0;

                        }
                        break;
                    case GDMotion.eRepetitiveMotion.MOV_DLY_0:
                        {

                            if (!m_StopWatchForMotion.IsDone)
                                break;

                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.MOV_POS_1;

                        }
                        break;
                    case GDMotion.eRepetitiveMotion.MOV_POS_1:
                        {
                            //Relative , Absolute motion check.
                            if (m_bIsRel) //Relative motion
                            {
                                if (m_bIsSlow)
                                    pitem.Move_Relative(-m_fTarget1, GDMotion.eSpeedType.SLOW);
                                else
                                    pitem.Move_Relative(-m_fTarget1, GDMotion.eSpeedType.FAST);
                            }
                            else  // Absolute motion
                            {
                                if (m_bIsSlow)
                                    pitem.Move_Absolute(m_fTarget2, GDMotion.eSpeedType.SLOW);
                                else
                                    pitem.Move_Absolute(m_fTarget2, GDMotion.eSpeedType.FAST);
                            }
                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.CHK_POS_1;
                            label_RepeatStatus.Text = "MOV_POS_2";

                        }
                        break;
                    case GDMotion.eRepetitiveMotion.CHK_POS_1:
                        {
                            m_StopWatchForMotion.SetDelay = m_nRepeatDelay;
                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.MOV_DLY_1;
                        }
                        break;
                    case GDMotion.eRepetitiveMotion.MOV_DLY_1:
                        {
                            if (!m_StopWatchForMotion.IsDone)
                                break;

                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.DONE;
                        }
                        break;


                    case GDMotion.eRepetitiveMotion.DONE:
                        {
                            //
                            m_nRepeatCurrCnt++;
                            m_eRepeatStatus = GDMotion.eRepetitiveMotion.RDY;
                        }
                        break;

                    case GDMotion.eRepetitiveMotion.NONE:
                        {
                            //
                            m_StopWatchForMotion.TimeStop();
                            label_RepeatStatus.Text = "NONE";
                        }
                        break;
                }
                this.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.ResumeLayout();
            }
            finally
            {
                this.ResumeLayout();
                m_Timer.Enabled = this.Visible;
            }
        }
        private void ReadMotionMasterStatus()
        {

            MotionItem item = pitem as MotionItem;
            if (item == null)
                return;
            Color clBack;
            //-[00001h] Bit 0, +Limit 급정지신호현재상태 o
            clBack = item.m_stMotionInfoStatus.m_bIsLimitP ? Color.Red : SystemColors.Control;
            lbl_LimitPlus.BackColor = clBack;
            //-[00002h] Bit 1, -Limit 급정지신호현재상태 o
            clBack = item.m_stMotionInfoStatus.m_bIsLimitN ? Color.Red : SystemColors.Control;
            lbl_LimitMinus.BackColor = clBack;
            //-[00010h] Bit 4, Alarm 신호신호현재상태    o
            clBack = item.m_stMotionInfoStatus.m_bIsAlarm ? Color.Red : SystemColors.Control;
            lbl_Alarm.BackColor = clBack;
            //-[00020h] Bit 5, InPos 신호현재상태        o
            clBack = item.m_stMotionInfoStatus.m_bIsInPos ? Color.Lime : SystemColors.Control;
            lbl_InPosition.BackColor = clBack;
            //-[00040h] Bit 6, 비상정지신호(ESTOP) 현재상태
            clBack = item.m_stMotionInfoStatus.m_bIsEStop ? Color.Red : SystemColors.Control;
            lbl_EMG.BackColor = clBack;
            //-[00080h] Bit 7, 원점신호헌재상태
            clBack = item.m_stMotionInfoStatus.m_bIsOrg ? Color.Lime : SystemColors.Control;
            lbl_ORG.BackColor = clBack;
            //-[00100h] Bit 8, Z 상입력신호현재상태
            clBack = item.m_stMotionInfoStatus.m_bIsZPhase ? Color.Lime : SystemColors.Control;
            lbl_Z_Phase.BackColor = clBack;

            //m_bMotionDone_Status
            clBack = item.IsMotionDone ? Color.Lime : SystemColors.Control;
            lbl_MotionDone.BackColor = clBack;

        }
        private void ReadMotionSlaveStatus()
        {

            lbl_IsMasterGantry.ImageIndex = 0;
            lbl_IsSlaveGantry.ImageIndex = 0;
            lbl_IsNotGantry.ImageIndex = 0;
            lbl_IsActiveGantry.ImageIndex = 0;
            lbl_IsActiveGantry2.ImageIndex = 0;


            /*
			    if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == m_iSelectedAxis)
				    lbl_IsMasterGantry.ImageIndex = 1;
			    else if (FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis == m_iSelectedAxis)
				    lbl_IsSlaveGantry.ImageIndex = 1;
			    else
				    lbl_IsNotGantry.ImageIndex = 1;

			
			    if (lbl_IsMasterGantry.ImageIndex == 1 || lbl_IsSlaveGantry.ImageIndex == 1)
			    {
				    lbl_IsActiveGantry.ImageIndex = FA.MGR.MotionMgr.IsActiveGantry() ? 1 : 0;
				    lbl_IsActiveGantry2.ImageIndex = FA.MGR.MotionMgr.IsActiveGantry() ? 1 : 0;
			    }

			    if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis != m_iSelectedAxis)
				    return;
			    if (FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis < 0 ||
				    FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis > ( (int)GDMotion.eMotorName.MAX - 1 ) )
				    return;
                
                MotionBaseClass item=FA.MGR.MotionMgr.GetItem(FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis);


                if(item == null)
                    return;

                Color clBack;
			    //-[00001h] Bit 0, +Limit 급정지신호현재상태 o
			    clBack = item.m_stMotionInfoStatus.m_bIsLimitP ? Color.Red : SystemColors.Control;
			    lbl_LimitPlus_Slave.BackColor = clBack;

			    //-[00002h] Bit 1, -Limit 급정지신호현재상태 o
			    clBack = item.m_stMotionInfoStatus.m_bIsLimitN ? Color.Red : SystemColors.Control;
			    lbl_LimitMinus_Slave.BackColor = clBack;

			    //-[00010h] Bit 4, Alarm 신호신호현재상태    o
			    clBack = item.m_stMotionInfoStatus.m_bIsAlarm ? Color.Red : SystemColors.Control;
			    lbl_ServoOn_Slave.BackColor = clBack;

			    //-[00020h] Bit 5, InPos 신호현재상태        o
			    clBack = item.m_stMotionInfoStatus.m_bIsInPos ? Color.Lime : SystemColors.Control;
			    lbl_InPosition_Slave.BackColor = clBack;

			    //-[00040h] Bit 6, 비상정지신호(ESTOP) 현재상태
			    clBack = item.m_stMotionInfoStatus.m_bIsEStop ? Color.Red : SystemColors.Control;
			    lbl_EMG_Slave.BackColor = clBack;

			    //-[00080h] Bit 7, 원점신호헌재상태
			    clBack = item.m_stMotionInfoStatus.m_bIsOrg ? Color.Lime : SystemColors.Control;
			    lbl_ORG_Slave.BackColor = clBack;

			    //-[00100h] Bit 8, Z 상입력신호현재상태
			    clBack = item.m_stMotionInfoStatus.m_bIsZPhase ? Color.Lime : SystemColors.Control;
			    lbl_Z_Phase_Slave.BackColor = clBack;

			    //m_bMotionDone_Status
			    clBack = item.IsMotionDone ? Color.Lime : SystemColors.Control;
			    lbl_MotionDone_Slave.BackColor = clBack;
           
            */
            // 			lbl_IsMasterGantry.ImageIndex = 1;
            // 			lbl_IsSlaveGantry.ImageIndex = 0;
            // 			lbl_IsActiveGantry.ImageIndex = 0;
            // 			lbl_IsActiveGantry2.ImageIndex = 0;          
        }
        private void ReadMotionStatus()
        {
            if (FA.MGR.MotionMgr == null)
                return;


            MotionItem item = pitem as MotionItem;
            if (item == null)
                return;

            ReadMotionMasterStatus();
            ReadMotionSlaveStatus();

            //m_bServoOnOff_Status

            if (item.IsServoOn)
            {
                lbl_ServoOn.BackColor = Color.Lime;
                btnServoOnOff.ImageIndex = 1;
            }
            else
            {
                lbl_ServoOn.BackColor = SystemColors.Control;
                btnServoOnOff.ImageIndex = 0;
            }

            Color clBack;

            //m_bHome_Status


            clBack = item.IsHomeComplete ? Color.Lime : SystemColors.Control;
            lbl_Home.BackColor = clBack;

            labelHomeSearch.Text = item.m_stMotionHomeStatus.m_strHomeStatus_Search;
            labelHomeStepMain32.Text = item.m_stMotionHomeStatus.m_strHomeStatus_StepMain;
            labelHomeStepSub33.Text = item.m_stMotionHomeStatus.m_strHomeStatus_StepSub;
            prgHomeRate.Value = item.m_stMotionHomeStatus.m_nHomeStatus_ProgressBar;
        }

        private void ReadPostionSlaveStatus()
        {
            /*
                if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis != Convert.ToInt32(m_iSelectedAxis))
                    return;
                if (FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis < 0 ||
                    FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis > ((int)GDMotion.eMotorName.MAX - 1))
                    return;
                
                MotionBaseClass item = FA.MGR.MotionMgr.GetItem(FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis);
                if(item == null)
                    return;
                
                lbl_ActPos_Slave.Text = item.m_stPositionStatus.strActPos;
                lbl_CmdPos_Slave.Text = item.m_stPositionStatus.strCmdPos;
                lbl_Speed_Slave .Text = item.m_stPositionStatus.strVelocity;
                lbl_ErrPos_Slave.Text = item.m_stPositionStatus.strErrPos;      
            */
        }

        private void ReadPositionStatus()
        {

            if (FA.MGR.MotionMgr == null)
                return;


            MotionItem item = pitem as MotionItem;

            if (item == null)
                return;

            lbl_ActPos.Text = item.m_stPositionStatus.strActPos;
            lbl_CmdPos.Text = item.m_stPositionStatus.strCmdPos;
            lbl_Speed.Text = item.m_stPositionStatus.strVelocity;
            lbl_ErrPos.Text = item.m_stPositionStatus.strErrPos;
            ReadPostionSlaveStatus();
        }

        private void FrmInforSetupMotion_Load(object sender, EventArgs e)
        {
            if (FA.MGR.MotionMgr != null)
            {
                FA.MGR.MotionMgr.TreeView_Clear(treeView_Motion);
                FA.MGR.MotionMgr.TreeView_Init(treeView_Motion);

                TreeView_Init();
              
            }
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {

                m_Timer.Enabled = true;
                Trace.WriteLine("SetupMotionShow");
            }
            else
            {

                m_Timer.Enabled = false;
                Trace.WriteLine("SetupMotionHide");
            }
            base.OnVisibleChanged(e);
        }
        private void treeView_Motion_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Tag == null)
                return;

            //아이콘 초기화.
            foreach (TreeNode pParent in treeView_Motion.Nodes)
            {
                pParent.ImageIndex = 2;
                pParent.SelectedImageIndex = 2;

                foreach (TreeNode pChild in pParent.Nodes)
                {
                    pChild.ImageIndex = 0;
                    pChild.SelectedImageIndex = 0;
                }

            }

            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;

            ChangeAxis((MotionInterface)e.Node.Tag);
        }
        private void ChangeAxis(MotionInterface a_pAxis)
        {
           
            
            if (a_pAxis == null)
                return;
            //m_eSelectedBrand = typeof(item).BaseType;
            MotionItem item = a_pAxis as MotionItem;
            pitem = a_pAxis;

            FastSpeedBinding.Speed = item.m_Fast;
            SlowSpeedBinding.Speed = item.m_Slow;
            userSpeedBinding.Speed = item.m_User;
            RunSpeedBinding.Speed = item.m_Run;

            label_MotorName.Text = item.strAxisName;

            //funcs tabcontrol
            tabControlFuncs.Enabled = true;
            tabSetting.Enabled = true;

            //Config tabcontrol
            Control ctlGantry = tabSetting.Controls[3]; // 0 : config, 1 : Home, 2 : speed, 3 :Gantry
            tabSetting.Controls[0].Enabled = true;
            tabSetting.Controls[1].Enabled = true;
            tabSetting.Controls[2].Enabled = true;
            ctlGantry.Enabled = false;

            //Set Zero button
            btnSetZeroPosition.Enabled = true;
            //Stop , Homing button
            btnStop.Enabled = true;
            btnHoming.Enabled = true;
            btnOpen.Enabled = true;
            btnSave.Enabled = true;

            //
            // 			if (Modules.MotionMgr.m_stGantryInfo.bGantryUse)
            // 				btnActivateGantry.Enabled = true;
            // 			else
            // 				btnActivateGantry.Enabled = false;

            //Functions title
            lable_Funcs_tab_title.Text = "Move Single Axis";

            //master of gantry or slave of gantry

            /*
            if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == m_iSelectedAxis || FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis == m_iSelectedAxis)
            {
                if (FA.MGR.MotionMgr.m_stGantryInfo.bGantryUse)
                    lable_Funcs_tab_title.Text = "Move Axes of gantry";


                if (FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis == m_iSelectedAxis &&
                    FA.MGR.MotionMgr.IsActiveGantry())
                {
                    //tabControl
                    tabControlFuncs.Enabled = false;
                    tabSetting.Enabled = false;

                    //Set Zero button
                    btnSetZeroPosition.Enabled = false;
                    //Stop , Homing button
                    btnStop.Enabled = false;
                    btnHoming.Enabled = false;
                    btnOpen.Enabled = false;
                    btnSave.Enabled = false;
                }

                if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == m_iSelectedAxis)
                {
                    ctlGantry.Enabled = true;
                }
            }*/

        }



        private void UpdateConfigValueInFormFromClass()
        {
            #region updata data in class from form
            /*FA.MGR.MotionMgr.UpdateConfigValueInFormFromClass
			(
				cboPulse, cboEncoder, cboAbsRel, cboProfile, cboZPhaseLev, cboZPhaseUse
				, cboAlarm, cboInp, cboStopMode, cboStopLevel, cboELimitN, cboELimitP, cboServoOn
				, cboAlarmReset, cboSWLimit_Use, cboSWLimit_StopMode, cboSWLimit_Selection, edtSwPosN, edtSwPosP, cboEncoderType, cboHomeSignal, cboHomeLevel
				, cboHomeDir, edtMinVel, edtMaxVel, edtMoveUnit, edtMovePulse, edtHomeClrTime
				, edtHomeOffset, edtVelFirst, edtVelSecond, edtVelThird
				, edtVelLast, edtAccFirst, edtAccSecond, edtPosition, edtVelocity, edtAccel, edtDecel
			);*/
            /*FA.MGR.MotionMgr.UpdateSpeedValueInFormFromClass
            (
				tbSpeed_Slow, tbAcc_Slow, tbDec_Slow, tbCurveAcc_Slow, tbCurveDec_Slow
				, tbSpeed_Fast, tbAcc_Fast, tbDec_Fast, tbCurveAcc_Fast, tbCurveDec_Fast
				, tbSpeed_Run, tbAcc_Run, tbDec_Run, tbCurveAcc_Run, tbCurveDec_Run
				, tbSpeed_User, tbAcc_User, tbDec_User, tbCurveAcc_User, tbCurveDec_User
			);
            */
            /*FA.MGR.MotionMgr.UpdateGantryValueInFormFromClass
			(
			txtb_SlaveHomeOffset
			, txtb_SlaveHomeRange
			, cb_SlaveHomeUse
			, ckb_GantryEnable
			);*/
            #endregion
        }

        private void FrmInforSetupMotion_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                TreeView_Init();
                m_StopWatchForMotion.TimeStop();
                m_StopWatchForDisplay.TimeStop();

                m_eRepeatStatus = GDMotion.eRepetitiveMotion.NONE;

                m_StopWatchForDisplay.SetDelay = 1000;
                m_bFlicker = false;
            }
            m_Timer.Enabled = this.Visible;
        }

        private void TreeView_Init()
        {
            //Ajinextek
            ChangeAxis(null);

            /*
            foreach (TreeNode pParent in treeView_Motion.Nodes)
			{
				pParent.ImageIndex = 2;
				pParent.SelectedImageIndex = 2;
				foreach (TreeNode pChild in pParent.Nodes)
				{
					if (Convert.ToInt32(pChild.Tag) == m_iSelectedAxis)
					{
                        if (pParent.Text.Equals("AJINEXTEK"))
                        {
                            pChild.ImageIndex = 1;
                            pChild.SelectedImageIndex = 1;
                        }
                        else
                        {
                            pChild.ImageIndex = 0;
                            pChild.SelectedImageIndex = 0;
                        }

                    }
					else
					{
						pChild.ImageIndex = 0;
						pChild.SelectedImageIndex = 0;
					}

				}                
			}  
            */
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if
             (
                     MessageBox.Show
                     (
                     "Would you like to save this configuration??"
                     , "Question"
                     , MessageBoxButtons.YesNo
                     , MessageBoxIcon.Question) == DialogResult.Yes
             )
            {
                if (pitem != null)
                {

                    MotionItem ItemVari=pitem as MotionItem;
                    ItemVari.m_Fast = FastSpeedBinding.Speed;
                    ItemVari.m_Slow = SlowSpeedBinding.Speed;
                    ItemVari.m_User = userSpeedBinding.Speed;
                    ItemVari.m_Run = RunSpeedBinding.Speed;
                    ItemVari.SaveMotionSpeedProfile();
                }
            }
            /*
            if(m_eSelectedBrand == GDMotion.eMotionBrand.AJINEXTEK)
            {
                MotionAjin item = (MotionAjin)FA.MGR.MotionMgr.GetItem(m_eSelectedBrand, m_iSelectedAxis);

                if(item == null)
                    return;

                if
                (
                        MessageBox.Show
                        (
                        "Would you like to save this configuration??"
                        , "Question"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question) == DialogResult.Yes
                )
                {

                    UpdateConfigValueInClassFromForm();
                    //모터 설정값 저장.
                    item.SaveMotion();
                    //모터 속도 저장.
                    item.SaveSpeed();
                    //Gantry 값 저장.
                    FA.MGR.MotionMgr.SaveGantry();

                    //ChangeAxis 내에 이 함수가 있음 : UpdateConfigValueInFormFromClass(); 
                    ChangeAxis(m_eSelectedBrand,m_iSelectedAxis);
                }

            }
            else if(m_eSelectedBrand == GDMotion.eMotionBrand.AEROTECH)
            {
                MotionAero item = (MotionAero)FA.MGR.MotionMgr.GetItem(m_eSelectedBrand, m_iSelectedAxis);
                if (item == null)
                    return;

                if
                (
                        MessageBox.Show
                        (
                        "Would you like to save this configuration??"
                        , "Question"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question) == DialogResult.Yes
                )
                {

                    UpdateConfigValueInClassFromForm();
                    //모터 설정값 저장.
                    item.SaveMotion();
                    //모터 속도 저장.
                    item.SaveSpeed(m_iSelectedAxis);
                    //Gantry 값 저장.
                    FA.MGR.MotionMgr.SaveGantry();

                    //ChangeAxis 내에 이 함수가 있음 : UpdateConfigValueInFormFromClass(); 
                    ChangeAxis(m_eSelectedBrand, m_iSelectedAxis);
                }
            }
            */
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

						if
						 (
										 MessageBox.Show
										 (
										 "Would you like to Open this configuration??"
										 , "Question"
										 , MessageBoxButtons.YesNo
										 , MessageBoxIcon.Question) == DialogResult.Yes
						 )
						{
								if (pitem != null)
								{
										
										MotionItem ItemVari   =pitem as MotionItem;
										ItemVari.OpenMotionSpeedProfile();
									  FastSpeedBinding.Speed=ItemVari.m_Fast;
									  SlowSpeedBinding.Speed=ItemVari.m_Slow;
									  userSpeedBinding.Speed=ItemVari.m_User;
								    RunSpeedBinding.Speed =ItemVari.m_Run;										
								}
						}
						/*
            if(m_eSelectedBrand == GDMotion.eMotionBrand.AJINEXTEK)
            {
                MotionAjin item = (MotionAjin)FA.MGR.MotionMgr.GetItem(m_eSelectedBrand, m_iSelectedAxis);
                if(item == null)
                    return;

			    if (item.bIsMotionDone == false)
				    return;

			    if
			    (
					    MessageBox.Show
					    (
					    "Would you like to open the MotionConfig.mot file??"
					    , "Question"
					    , MessageBoxButtons.YesNo
					    , MessageBoxIcon.Question) == DialogResult.Yes
			    )
			    {
				    //모터 설정 값 열기.
				    item.OpenMotion(true);
				    //모터 속도 열기
				    item.OpenSpeed();
				    //Gentry 값 열기
				    FA.MGR.MotionMgr.OpenGantry();

				    ChangeAxis(m_eSelectedBrand, m_iSelectedAxis);

				    //모터 설정 값으로 화면 갱신.
				    //UpdateConfigValueInFormFromClass();

			    }
            }
            else if (m_eSelectedBrand == GDMotion.eMotionBrand.AEROTECH)
            {
                MotionAero item = (MotionAero)FA.MGR.MotionMgr.GetItem(m_eSelectedBrand, m_iSelectedAxis);
                if (item == null)
                    return;

                if (item.GetAxis(m_iSelectedAxis).bIsMotionDone == false)
                    return;

                if
                (
                        MessageBox.Show
                        (
                        "Would you like to open the MotionConfig.mot file??"
                        , "Question"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question) == DialogResult.Yes
                )
                {
                    //모터 설정 값 열기.
                    item.OpenMotion(true);
                    //모터 속도 열기
                    item.OpenSpeed(m_iSelectedAxis);
                    //Gentry 값 열기
                    FA.MGR.MotionMgr.OpenGantry();

                    ChangeAxis(m_eSelectedBrand, m_iSelectedAxis);

                    //모터 설정 값으로 화면 갱신.
                    //UpdateConfigValueInFormFromClass();
                }
            }
            */
				}

        private void btnServoOnOff_Click(object sender, EventArgs e)
        {

            MotionItem  item = pitem as MotionItem;
            if (pitem == null)
                return;

            bool bState = item.IsServoOn;

            pitem.ServoOn = !bState;
            /*
            if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == m_iSelectedAxis)
            {
                if (FA.MGR.MotionMgr.IsActiveGantry())
                {
                   FA.MGR.MotionMgr.GetItem( FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis).ServoOn=!bState;
                   FA.MGR.MotionMgr.GetItem( FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis).ServoOn=!bState;
                }
                else
                {
                     item.ServoOn=!bState;
                }
            }
            else if (FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis == m_iSelectedAxis)
            {
                if (!FA.MGR.MotionMgr.IsActiveGantry())
                    item.ServoOn=!bState;
            }
            else
            {
                item.ServoOn=!bState;
            }
            */

        }

        private void btnSetZeroPosition_Click(object sender, EventArgs e)
        {            
            if (pitem == null)
                return;


            pitem.SetZeroPosition();

            /*
            if (FA.MGR.MotionMgr.IsActiveGantry())
            {
                if (FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis == m_iSelectedAxis)
                {
                    FA.MGR.MotionMgr.GetItem(FA.MGR.MotionMgr.m_stGantryInfo.iMasterAxis).SetZeroPosition();
                    FA.MGR.MotionMgr.GetItem(FA.MGR.MotionMgr.m_stGantryInfo.iSlaveAxis).SetZeroPosition();
                }

            }
            else
            {
                item.SetZeroPosition();
            }           
            */
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            
            if (pitem == null)
                return;
            pitem.HomeStart();
        }



        private void ckb_GantryEnable_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (sender as CheckBox);
            chkbox.ImageIndex = chkbox.Checked ? 1 : 0;
        }

        private void btnActivateGantry_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Would you like to apply to gantry??", "Warning"
            , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //FA.MGR.MotionMgr.ApplayToGantry();
            }
        }

        private void FrmInforSetupMotion_FormClosing(object sender, FormClosingEventArgs e)
        {
			      m_Timer.Stop();
            m_StopWatchForMotion.TimeStop();
            m_StopWatchForDisplay.TimeStop();
            m_StopWatchForMotion = null;
            m_StopWatchForDisplay = null;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
            if (pitem == null)
                return;


            pitem.SD_STOP();
            m_eRepeatStatus = GDMotion.eRepetitiveMotion.NONE;

			


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
          
            if (pitem == null)
                return;
            pitem.SetAlarmReset();
        }

        private void tabSetting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabSetting_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void tabSetting_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (tabSetting.SelectedIndex)
            {
                case 0:
                    {

                    }
                    break;

                case 1:
                    {
                        //tabSetting = pAXTConfigGUI_Page;
                        // tabSetting.SelectedTab = tabSetting.TabPages[1];

                        tabSetting.TabPages[1].Controls.Clear();
                        btest = !btest;
                        if (btest)
                        {
                            pAXTConfigGUI.InitControl(pitem);
                            tabSetting.TabPages[1].Controls.Add(pAXTConfigGUI);
                        }
                            
                        else
                        {
                            tabSetting.TabPages[1].Controls.Add(pAXTHomeConfigGUI);
                        }
                            

                    }
                    break;
                default:
                    {

                    }
                    break;

            }
        }
    }//end of class
}//end of namespace
