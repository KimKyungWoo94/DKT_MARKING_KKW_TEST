using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Motion;
using System.Collections.Concurrent;
using EzInaVision;
using System.Windows.Forms;
using System.Drawing;
using EzIna.FA;
using static EzIna.FA.DEF;

namespace EzIna
{

    public partial class RunningManager
    {
        public bool IsInitialized { get; set; }
        public FA.DEF.eRunMode eRunMode { get; set; }
        FA.DEF.eManualMode m_eManualMode;
        public FA.DEF.eManualMode eManualMode { get { return m_eManualMode; } }


        FA.DEF.stRunInfo m_stRun;
        Dictionary<string, RunningBase> m_dicRunModules = null;
        object m_lock = null;

        //check digital
        int m_nChattering;
        EzIna.UC.StopWatchTimer stwatch_safetyTeachSW;
        bool m_bSafetyMode;

        //Tasks information
        public ConcurrentDictionary<FA.DEF.eTasks, bool> m_dicTasks;
        //Works information
        public RunningPM PM;

        System.Diagnostics.Stopwatch m_sw = new System.Diagnostics.Stopwatch();


        private double m_fManualMovingXPos;
        public double fManualMovingXPos
        {
            get { return m_fManualMovingXPos; }
            set { m_fManualMovingXPos = value; }
        }

        private double m_fManualMovingYPos;
        public double fManualMovingYPos
        {
            get { return m_fManualMovingYPos; }
            set { m_fManualMovingYPos = value; }
        }

        private double m_fManualMovingZPos;
        public double fManualMovingZPos
        {
            get { return m_fManualMovingZPos; }
            set { m_fManualMovingZPos = value; }
        }
        private bool m_bStartBtnPushed=false;
        private bool m_bStopBtnPushed=false;
        private bool m_bResetBtnPushed=false;
        private int  m_iChatteringMAX=100;
        #region [ Motion Status Delegate ] 
        public delegate void AerotechStatusUtilityHandler();
        event AerotechStatusUtilityHandler _AerotechStatusUtilityEvent;
        public event AerotechStatusUtilityHandler AerotechStatusUtilityEvent
        {
            add
            {
                _AerotechStatusUtilityEvent += value;
            }
            remove
            {
                _AerotechStatusUtilityEvent -= value;
            }
        }

        #endregion [ Motion Status Delegate ]
        public RunningManager()
        {
            #region Running Modules 관리를 위한 Dictionary
            m_dicRunModules = new Dictionary<string, RunningBase>();
            LoadItems();
            #endregion

            m_lock = new object();
            stwatch_safetyTeachSW = new EzIna.UC.StopWatchTimer();

            m_nChattering = 0;
            m_bSafetyMode = false;
            eRunMode = FA.DEF.eRunMode.Stop;
            m_eManualMode = FA.DEF.eManualMode.None;
            IsInitialized = false;

            m_dicTasks = new ConcurrentDictionary<FA.DEF.eTasks, bool>();
            PM = new RunningPM();//Production Manager

            //To do list
            // foreach (FA.DEF.eWorksInfor item in Enum.GetValues(typeof(FA.DEF.eWorksInfor)))
            // {
            //     m_dicWorksInfo.Add(item, new FA.DEF.CWorksInfo(item));
            // }
            // 
            // foreach (FA.DEF.eWorksInfor item in Enum.GetValues(typeof(FA.DEF.eWorksInfor)))
            //     m_dicWorksInfo[item].Read(FA.FILE.WorksInfor);
        }

        ~RunningManager()
        {
            DeleteItems();
        }

        public void LoadItems()
        {
            try
            {
                m_dicRunModules.Add("PROCESS", new RunningProcess("PROCESS", 0));
                m_dicRunModules.Add("SCANNER", new RunningScanner("SCANNER", 1));
                m_dicRunModules.Add("STAGE", new RunningStage("STAGE", 2));
                m_dicRunModules.Add("Load", new RunningLoad("Load", 3));
                m_dicRunModules.Add("Unload", new RunningUnload("Unload", 4));
            }
            catch (Exception ex)
            {

            }

        }

        public void DeleteItems()
        {
            if (m_dicRunModules == null)
                return;

            m_dicRunModules.Clear();
            m_dicRunModules = null;

            //m_dicTasks.Clear();
            m_dicTasks = null;

            //             if(m_dicWorksInfo != null)
            //             {
            //                 foreach (FA.DEF.eWorksInfor item in Enum.GetValues(typeof(FA.DEF.eWorksInfor)))
            //                     m_dicWorksInfo[item].Save(FA.FILE.WorksInfor);
            //             }
        }

        public void ConnectingModules()
        {
            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
            {
                item.Value.ConnectingModule();
            }
        }

        public RunningBase GetItem(string a_strItemName)
        {
            if (m_dicRunModules == null)
                return null;
            if (m_dicRunModules.Count < 1)
                return null;
            if (m_dicRunModules.ContainsKey(a_strItemName) == false)
                return null;

            return m_dicRunModules[a_strItemName];
        }
        public void Execute(object a_obj)
        {
            try
            {
                lock (m_lock)
                {
                    #region [IO CHECK]
                    if (FA.MGR.IOMgr != null)
                    {
                        FA.MGR.IOMgr.Execute(null);
                        FA.SYS.ButtonSwitchScan();
                        MF.TOWERLAMP.DoThreadProc();
                        CheckDigital();
                    }
                    #endregion [IO CHECK]
                    #region [MOTION CHECK]
                    if (FA.MGR.MotionMgr != null)
                    {
                        FA.MGR.MotionMgr.GetMotionStatus();
                        //Task
                        //EzIna.Motion.CMotionA3200.GetTaskState_Enum()
                        //AerotechStatusUtilityHandlerEvent();
                        CheckMotion();
                    }
                    #endregion[MOTION CHECK]
                    #region [ Scanner CHECK ]
                    if (FA.MGR.RTC5Mgr != null)
                    {
                        FA.MGR.RTC5Mgr.GetAllDeviceStatus();
                    }
                    #endregion [ Scanner CHECK ]
                    switch (eRunMode)
                    {
                        case FA.DEF.eRunMode.Init:
                            Initialize();
                            if (m_stRun.nMainStep == FA.DEF.SEQ_FINISH && eRunMode != FA.DEF.eRunMode.Jam)
                            {
                                IsInitialized = true;
                                UpdateTaskAllClear();
                                //UpdateWorksAllClear();
                                ModeChange(FA.DEF.eRunMode.Stop);
                            }
                            break;

                        case FA.DEF.eRunMode.ToRun:
                            Ready();
                            if (m_stRun.nMainStep == FA.DEF.SEQ_FINISH && eRunMode != FA.DEF.eRunMode.Jam)
                            {
                                ModeChange(FA.DEF.eRunMode.Run);
                            }
                            break;
                        case FA.DEF.eRunMode.Run:
                        case FA.DEF.eRunMode.ToStop:
                            Running();

                            if (m_stRun.nMainStep == FA.DEF.SEQ_FINISH && GetModulesStop() && eRunMode != FA.DEF.eRunMode.Jam)
                            {
                                ModeChange(FA.DEF.eRunMode.Stop);
                            }
                            break;

                        case FA.DEF.eRunMode.ToManual:
                            if (StartManualMode(m_eManualMode))
                                ModeChange(FA.DEF.eRunMode.Manual);
                            break;
                        case FA.DEF.eRunMode.Manual:
                        case FA.DEF.eRunMode.ToStopManual:
                            if (RunManualMode(m_eManualMode))
                            {
                                ModeChange(FA.DEF.eRunMode.Stop);
                            }
                            break;
                    }
                    //Trace.WriteLine("Sequence Execute [ms] : " + m_sw.ElapsedMilliseconds.ToString());
                }//end of lock
            }
            catch (Exception ex)
            {
                //MsgBox.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Motors Check  
        /// </summary>
        public void CheckMotion()
        {
            if (FA.MGR.MotionMgr == null)
                return;

            if (FA.MGR.RunMgr.eRunMode == FA.DEF.eRunMode.Jam)
                return;

            for (int i = 0; i < (int)FA.DEF.eAxesName.Max; i++)
            {
                if (IsInitialized)
                {
#if SIM
									//if (FA.MGR.MotionMgr?.GetItem(i)?.Status().IsServoOn == false)
									//{
									//		ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MOTOR_IS_NOT_SERVO_ON + i,eManualMode.None,
									//				string.Format("{0} Servo Off ",FA.MGR.MotionMgr?.GetItem(i)?.Status().strAxisName));
									//		break;
									//}
#else
                    if (FA.MGR.MotionMgr?.GetItem(i)?.Status().IsServoOn == false)
                    {
                        ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MOTOR_IS_NOT_SERVO_ON + i);
                        IsInitialized = false;
                        break;
                    }
#endif
                    /*if (FA.MGR.MotionMgr?.GetItem(i)?.Status().IsHomeComplete == false)
{
    ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MOTOR_IS_NOT_HOMED + i);
    break;
}*/

                    if (FA.MGR.MotionMgr?.GetItem(i)?.Status().m_stMotionInfoStatus.m_bIsAlarm == true)
                    {
                        ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_AN_ALARM_OCCURRED_ON_THE_MOTOR + i);
                        IsInitialized = false;
                        break;
                    }
                }
            }

        }
        public void CheckDigital()
        {

            #region [check the button switch]

            /* if (!eDI.SLIDE_CHUCK_CLAMPPING_SW_CHECK.GetDI().Value)
{
        FA.ACT.SlideChuckClampingOFF.Run();
}*/


#if SIM
#else
            if (OPT.DoorOpenRunAllow.m_bState == false)
            {
                if (eDI.FRONT_DOOR_OPENED.GetDI().Value || eDI.REAR_DOOR_OPENED.GetDI().Value)
                {
                    if (this.eRunMode == eRunMode.Run)
                        ModeChange(eRunMode.ToStop);

                }
            }
#endif

            if (m_nChattering >= m_iChatteringMAX)
            {
    
                if (eDI.SW_START.GetDI().Value)
                {
                    if(m_bStartBtnPushed==false)
                    {
                        m_bStartBtnPushed=true;
                        if (string.IsNullOrEmpty(FA.MGR.RecipeRunningData.strLotCardCode) == false &&
                        string.IsNullOrWhiteSpace(FA.MGR.RecipeRunningData.strLotCardCode) == false
                           )
                        {
                            if (eRunMode == eRunMode.Stop)
                                ButtonSwitch(FA.DEF.eButtonSW.START);
                        }
                        else
                        {
                            ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_LOT_CODE_EMPTY);
                        }
                    }                                       
                }
                else if (eDI.SW_STOP.GetDI().Value)
                {
                    if(m_bStopBtnPushed==false)
                    {
                        m_bStopBtnPushed=true;
                        ButtonSwitch(FA.DEF.eButtonSW.STOP);
                    }                    
                }

                else if (eDI.SW_RESET.GetDI().Value)
                {
                    if(m_bResetBtnPushed==false)
                    {
                        m_bResetBtnPushed=true;
                        if (eRunMode == FA.DEF.eRunMode.Stop || eRunMode == FA.DEF.eRunMode.Jam)
                            ButtonSwitch(FA.DEF.eButtonSW.RESET);
                    }
                    
                }
                m_nChattering=0;
            }
            else if (IsPressedButtonSwitch())
            {
                if (m_nChattering < m_iChatteringMAX)
                    m_nChattering++;
            }
            else
            {
                m_bStartBtnPushed=false;
                m_bStopBtnPushed=false;
                m_bResetBtnPushed=false;
            }
            #endregion

            if (eRunMode == FA.DEF.eRunMode.Jam)
                return;

#if !SIM
            #region [Interlock]
            //EMO 감지.
            if (eDI.EMERGENCY.GetDI().Value)
            {
                IsInitialized = false;
                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_EMO_HAS_BEEN_DETECTED);
                return;
            }
            if (!eDI.XYZ_MOTOR_PWR_MC.GetDI().Value)
            {
                IsInitialized = false;
                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_SERVO_POWER_OFF);
                return;
            }
            if (!eDI.STEP_DRIVER_PWR_MC.GetDI().Value)
            {
                IsInitialized = false;
                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_STEP_MOTOR_POWER_OFF);
                return;
            }
            //Main Air
            if (!eDI.MAIN_PRESSURE_CHECK.GetDI().Value)
            {
                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MAIN_AIR);
                return;
            }

            #endregion
#endif

        }
        public bool IsPressedButtonSwitch()
        {
            bool bRtn = false;
            bRtn |= eDI.SW_START.GetDI().Value;
            bRtn |= eDI.SW_STOP.GetDI().Value;
            bRtn |= eDI.SW_RESET.GetDI().Value;
            // bRtn |= eDI.SW_STAGE_VACCUM_ONOFF.GetDI().Value;
            // bRtn |= eDI.SW_DOOR_OPEN_CLOSE.GetDI().Value;
            // bRtn |= eDI.FOOT_SW_STAGE_VACCUM.GetDI().Value;
            // bRtn |= eDI.SLIDE_CHUCK_CLAMPPING_SW_CHECK.GetDI().Value;
            return bRtn;
        }
        public void ButtonSwitch(FA.DEF.eButtonSW a_eSW)
        {
            try
            {
                switch (a_eSW)
                {
                    case FA.DEF.eButtonSW.RESET:
                        //강제 타임아웃이 발생했을 경우를 위해서.
                        foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                        {
                            item.Value.ResetTimeout();
                        }

                        /*						M.DigitalMgr.SetMelody(Digital.eMelody.MELODY_NONE);*/
                        if (FA.MGR.VisionMgr != null && FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()) != null)
                            FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).SetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, false);
                        //if (FA.MGR.VisionMgr != null && FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()) != null)
                        //  FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).SetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, false);

                        WinAPIs._PostMessageM(FA.DEF.MSG_FROM_CLOSE_ALL);
                        ModeChange(FA.DEF.eRunMode.Stop);
                        break;

                    case FA.DEF.eButtonSW.STOP:

                        if (eRunMode != FA.DEF.eRunMode.Jam)
                        {
                            if (eRunMode == FA.DEF.eRunMode.Manual)
                            {
                                ModeChange(FA.DEF.eRunMode.ToStopManual);
                            }
                            else //running
                            {
                                ModeChange(FA.DEF.eRunMode.ToStop);
                            }
                        }
                        break;
                    case FA.DEF.eButtonSW.START:
                        if (!IsInitialized)
                        {
                            ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MC_IS_NOT_INITIALIZED);
                            return;
                        }
                        if (eRunMode == FA.DEF.eRunMode.Stop)
                        {
                            WinAPIs._PostMessageM(FA.DEF.GUI_AUTO_LOT_CODE_TXT_READONLY);
                            ModeChange(FA.DEF.eRunMode.ToRun);
                        }
                        break;
                }
                m_nChattering = 0;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }
        public void ModeChange(FA.DEF.eRunMode a_eRunMode, int a_nErrNo = 0, FA.DEF.eManualMode a_eManualMode = FA.DEF.eManualMode.None, string a_strErrDscr = "")
        {
            try
            {
                switch (a_eRunMode)
                {
                    case FA.DEF.eRunMode.Jam:
                        if (eRunMode == FA.DEF.eRunMode.Jam)
                        {
                            FA.FRM.FrmError.InvokeIfNeeded(() =>
                             {
                                 if (FA.FRM.FrmError.Visible)
                                     FA.FRM.FrmError.Hide();
                             });
                            return;
                        }
                        //Laser Interlock : 외부 셧터 클로즈, 게이트 비활성화.
                        FA.SYS.SetButtonSwitch(FA.DEF.eButtonSW.RESET);
                        m_stRun.nMainStep = FA.DEF.SEQ_FINISH;
                        WinAPIs._PostMessageM_IntPtr(FA.DEF.MSG_SHOW_ERROR, WinAPIs.MakeLParam(a_nErrNo, 0), WinAPIs.StringToIntPtr(a_strErrDscr));
                        break;
                    case FA.DEF.eRunMode.Init:
                        IsInitialized = false;
                        FA.SYS.SetButtonSwitch(FA.DEF.eButtonSW.INIT_FLICKER);
                        m_stRun.nMainStep = FA.DEF.SEQ_START;

                        break;
                    case FA.DEF.eRunMode.Ready:
                        break;
                    case FA.DEF.eRunMode.ToRun:
                        if (!IsInitialized)
                        {
                            ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MC_IS_NOT_INITIALIZED);
                            return;
                        }

                        if (OPT.DoorOpenRunAllow.m_bState == false)
                        {
                            if (eDI.FRONT_DOOR_OPENED.GetDI().Value || eDI.REAR_DOOR_OPENED.GetDI().Value)
                            {
                                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_DOOR_OPEN, eManualMode.None,
                                        string.Format("Front : {0} , Rear : {0}",
                                        eDI.FRONT_DOOR_OPENED.GetDI().Value == true ? "Open" : "Close",
                                        eDI.REAR_DOOR_OPENED.GetDI().Value == true ? "Open" : "Close"
                                        )
                                        );
                                return;
                            }
                        }
                        //강제 타임아웃이 발생했을 경우를 위해서.
                        foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                        {
                            item.Value.ResetTimeout();
                        }
                        m_stRun.nMainStep = FA.DEF.SEQ_START;
                        break;
                    case FA.DEF.eRunMode.Run:
                        //M.VisionMgr.GetCam(GDV.eVision.Fine).Idle();
                        //M.VisionMgr.GetCam(GDV.eVision.Coarse).Idle();
                        FA.SYS.SetButtonSwitch(FA.DEF.eButtonSW.START);
                        m_stRun.nMainStep = FA.DEF.SEQ_START;
                        break;
                    case FA.DEF.eRunMode.ToStop:
                        //AXIS.AllSDStop();
                        break;
                    case FA.DEF.eRunMode.Stop:
#if SIM
#else
                        FA.MGR.VisionMgr.GetCam(FA.DEF.eVision.FINE.ToString())?.Live();
                        AXIS.AllStepMotorStop();
                        eDO.LOADER_IF_START_CMD.GetDO().Value = false;
                        eDO.UNLOADER_START_CMD.GetDO().Value = false;
#endif
                        //FA.MGR.VisionMgr.GetCam(FA.DEF.eVision.COARSE.ToString())?.Live();
                        //Laser Interlock : 외부 셧터 클로즈, 게이트 비활성화.
                        //FA.ACT.CYL_LaserShutter_Close.Run();
                        //FA.LASER.TALON.GateEnable = false;

                        FA.SYS.SetButtonSwitch(FA.DEF.eButtonSW.STOP);
                        WinAPIs._PostMessageM(FA.DEF.MSG_HIDE_ALARM);
                        break;
                    case FA.DEF.eRunMode.ToStopManual:


                        break;
                    case FA.DEF.eRunMode.ToManual:
                        m_eManualMode = a_eManualMode;
                        WinAPIs._PostMessageM_IntPtr(
                                FA.DEF.MSG_SHOW_ALARM,
                                WinAPIs.MakeLParam(FA.DEF.ALM_NOTIFY, 0),
                                WinAPIs.StringToIntPtr(string.Format("Manual Mode : {0} Excuting", m_eManualMode))
                                );
                        break;
                    case FA.DEF.eRunMode.Manual:
                        //FA.MGR.VisionMgr.GetCam(FA.DEF.eVision.FINE  .ToString()   ).Idle();
                        //FA.MGR.VisionMgr.GetCam(FA.DEF.eVision.COARSE.ToString()   ).Idle();

                        if (!IsInitialized)
                        {
                            ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MC_IS_NOT_INITIALIZED);
                            return;
                        }
                        if (m_eManualMode == FA.DEF.eManualMode.None || m_eManualMode == FA.DEF.eManualMode.Max)
                            return;
                        FA.SYS.SetButtonSwitch(FA.DEF.eButtonSW.START);
                        break;
                    default:
                        return;
                }

                eRunMode = a_eRunMode;
                UpdateWorksTime(a_eRunMode); // for time check
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        }
        #region [ SEQ - INITIALIZE ]
        void Initialize()
        {
            try
            {
                switch (CastTo<FA.DEF.eSEQ_MAIN_INIT>.From(m_stRun.nMainStep))
                {
                    case FA.DEF.eSEQ_MAIN_INIT.PRECHECK:
                        {
                            InterfaceInitClear();
                            //WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_ALARM, FA.DEF.ALM_INIT);
                            WinAPIs._PostMessageM_IntPtr(FA.DEF.MSG_SHOW_ALARM, WinAPIs.MakeLParam(FA.DEF.ALM_INIT, 0), WinAPIs.StringToIntPtr("Initializing"));
                            NextMainStep(FA.DEF.eSEQ_MAIN_INIT.MODULE_START);
                        }
                        break;
                    case FA.DEF.eSEQ_MAIN_INIT.MODULE_START:
                        {
                            bool bIsCompleted = true;
                            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                            {
                                bIsCompleted &= item.Value.ToInit();
                            }

                            if (!bIsCompleted)
                            {
                                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MC_IS_NOT_INITIALIZED);
                                break;
                            }
                            NextMainStep(FA.DEF.eSEQ_MAIN_INIT.MODULE_START_COMPLETE);
                        }
                        break;
                    case FA.DEF.eSEQ_MAIN_INIT.MODULE_START_COMPLETE:
                        {
                            bool bIsCompleted = true;
                            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                            {
                                bIsCompleted &= item.Value.IsDone_ToInit;
                            }

                            if (!bIsCompleted)
                            {
                                ModeChange(FA.DEF.eRunMode.Jam, FA.DEF.ERR_MC_IS_NOT_INITIALIZED);
                                break;
                            }
                            NextMainStep(FA.DEF.eSEQ_MAIN_INIT.MODULE_RUN);
                        }
                        break;
                    case FA.DEF.eSEQ_MAIN_INIT.MODULE_RUN:
                        {
                            bool bIsCompleted = true;
                            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                            {
                                item.Value.Init();
                                bIsCompleted &= item.Value.IsDone_Init;
                            }
                            if (!bIsCompleted)
                                break;

                            NextMainStep(FA.DEF.eSEQ_MAIN_INIT.MODULE_RUN_COMPLETE);
                        }
                        break;
                    case FA.DEF.eSEQ_MAIN_INIT.MODULE_RUN_COMPLETE:
                        NextMainStep(FA.DEF.eSEQ_MAIN_INIT.COMPLETE);
                        break;
                    case FA.DEF.eSEQ_MAIN_INIT.COMPLETE:
                        NextMainStep(FA.DEF.SEQ_FINISH);
                        WinAPIs._PostMessageM(FA.DEF.MSG_FROM_CLOSE_ALL);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }
        }
        #endregion
        #region [SEND MSG TO MAIN FORM]

        //         public delegate void AsyncDelegateCaller(int LParm, int RParm);
        //         public AsyncDelegateCaller Caller;
        // 
        //         public void CreateEvent(Action<int, int> Func)
        //         {
        //             Caller = new AsyncDelegateCaller(Func);
        //         }
        //         public void AsyncDelegateCallback(IAsyncResult a_ar)
        //         {
        //             AsyncDelegateCaller CallerCallback = a_ar.AsyncState as AsyncDelegateCaller;
        //             CallerCallback.EndInvoke(a_ar);
        //         }
        // 
        //         void SendMsgToFrmMain(int LParam, int RParam)
        //         {
        //             if (Caller == null) return;
        //             Caller.BeginInvoke(LParam, RParam, new AsyncCallback(AsyncDelegateCallback), Caller);
        //         }
        #endregion

        public bool RejectSEQ()
        {
            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
            {
                item.Value.InterfaceRunClear();
                item.Value.ClearRecoveryRunInfo();
                //item.Value.ClearRunInfo();						
            }
            return true;
        }

        #region [TASKS STATUS]
        public bool UpdateTaskAllClear()
        {
            for (int i = 0; i < (int)FA.DEF.eTasks.TaskMax; i++)
            {
                m_dicTasks.AddOrUpdate((FA.DEF.eTasks)i, false, (k, v) => false);
            }
            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
            {
                item.Value.InterfaceRunClear();
            }
            //m_dicRunModules["STAGE"].InterfaceRunClear();
            return true;

        }

        public bool IsUpdateTaskAllClear()
        {
            for (int i = 0; i < (int)FA.DEF.eTasks.TaskMax; i++)
            {
                bool bStatus = false;
                m_dicTasks.TryGetValue((FA.DEF.eTasks)i, out bStatus);
                if (bStatus == true)
                    return false;
            }
            return true;

        }
        public bool UpdateTask(FA.DEF.eTasks a_eTask, bool a_bStatus)
        {
            if (m_dicTasks == null)
                return false;

            m_dicTasks.AddOrUpdate(a_eTask, a_bStatus, (k, v) => a_bStatus);

            return true;
        }
        public bool IsTaskCompleted(FA.DEF.eTasks a_eTask)
        {
            if (m_dicTasks == null)
                return false;
            if (m_dicTasks.Count < 1)
                return false;
            bool bStatus = false;
            m_dicTasks.TryGetValue(a_eTask, out bStatus);
            return bStatus;
        }
        #endregion
        #region [MODULES INTERFACE]
        public void InterfaceInitClear()
        {
            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
            {
                item.Value.InterfaceInitClear();
            }
        }
        public bool GetModulesStop()
        {
            bool bRtn = true;
            foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
            {
                bRtn &= item.Value.IsStop();
            }

            return bRtn;
        }
        #endregion
        #region [MANUAL SEQ]
        public bool StartManualMode(FA.DEF.eManualMode a_eManualMode = FA.DEF.eManualMode.None)
        {
            bool bStart = false;
            switch (a_eManualMode)
            {
                #region [RunningProcess]
                case FA.DEF.eManualMode.VISION_CAL:
                    bStart = m_dicRunModules["PROCESS"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.MAKE_PGM:
                    bStart = m_dicRunModules["PROCESS"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.POWER_CHECK:
                    //bStart = m_dicRunModules["PROCESS"].StartManualMode();
                    break;


                #endregion
                #region [RunningScanner]
                case FA.DEF.eManualMode.GALVO_CAL:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.FOCUS_FINDER:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.CROSS_HAIR:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.FIND_CPU:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;

                case FA.DEF.eManualMode.MAKE_POWERTABLE:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;

                case FA.DEF.eManualMode.JIG_MOVING_TEST:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.JIG_MOVING_N_POS_INSP:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.JIG_MOVING_INSP_N_MARKING:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.MANUAL_PROCESS:
                    bStart = m_dicRunModules["SCANNER"].StartManualMode();
                    break;
                #endregion
                #region [RunningStage]
                case FA.DEF.eManualMode.STAGE_CAL:
                    bStart = m_dicRunModules["STAGE"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.ALIGNMENT:
                    bStart = m_dicRunModules["STAGE"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.AUTOFOCUS:
                    bStart = m_dicRunModules["STAGE"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.MANUAL_MOVING:
                    bStart = m_dicRunModules["STAGE"].StartManualMode();
                    break;


                case FA.DEF.eManualMode.JIG_LOADING:
                    bStart = m_dicRunModules["Load"].StartManualMode();
                    break;
                case FA.DEF.eManualMode.JIG_UNLOADING:
                    bStart = m_dicRunModules["Unload"].StartManualMode();
                    break;


                #endregion

                default:
                    break;
            }

            return bStart;
        }
        public bool RunManualMode(FA.DEF.eManualMode a_eManualMode = FA.DEF.eManualMode.None)
        {
            bool bManualComplete = false;

            switch (a_eManualMode)
            {
                #region [RunningProcess]
                case FA.DEF.eManualMode.VISION_CAL:
                    //bManualComplete = ((RunningProcess)m_dicRunModules["PROCESS"]).
                    break;
                case FA.DEF.eManualMode.MAKE_PGM:
                    //bManualComplete = ((RunningProcess)m_dicRunModules["PROCESS"]).
                    break;
                case FA.DEF.eManualMode.MAKE_POWERTABLE:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_CreatePowerTableWithAttenuator();
                    break;
                case FA.DEF.eManualMode.POWER_CHECK:
                    //bManualComplete = ((RunningProcess)m_dicRunModules["PROCESS"]).
                    break;
                #endregion
                #region [RunningScanner]
                case FA.DEF.eManualMode.GALVO_CAL:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_Galvo_Cal();
                    break;
                case FA.DEF.eManualMode.FOCUS_FINDER:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_FocusFinder();
                    break;
                case FA.DEF.eManualMode.CROSS_HAIR:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_CrossHair();
                    break;
                case FA.DEF.eManualMode.FIND_CPU:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_Calc_CPU();
                    break;
                case FA.DEF.eManualMode.JIG_MOVING_TEST:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_RecipeProcess(false, false, false);
                    break;
                case FA.DEF.eManualMode.JIG_MOVING_N_POS_INSP:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_RecipeProcess(true, false, false);
                    break;
                case FA.DEF.eManualMode.JIG_MOVING_INSP_N_MARKING:
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_RecipeProcess(true, true, false);
                    break;
                case FA.DEF.eManualMode.MANUAL_PROCESS:
#if SIM
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_RecipeProcess(true, true, true, true);
#else
                    bManualComplete = ((RunningScanner)m_dicRunModules["SCANNER"]).SubSeq_RecipeProcess(true, true, true);
#endif
                    break;

                #endregion
                #region [RunningStage]
                case FA.DEF.eManualMode.STAGE_CAL:
                    bManualComplete = ((RunningStage)m_dicRunModules["STAGE"]).SubSeq_StageMapping_XY();
                    break;
                case FA.DEF.eManualMode.ALIGNMENT:
                    //bManualComplete = ((RunningProcess)m_dicRunModules["STAGE"]).
                    break;

                case FA.DEF.eManualMode.AUTOFOCUS:
                    bManualComplete = ((RunningStage)m_dicRunModules["STAGE"]).SubSeq_AutoFocus();
                    break;
                case FA.DEF.eManualMode.JIG_LOADING:
                    bManualComplete = ((RunningLoad)m_dicRunModules["Load"]).SubSeq_Loading(false);
                    break;
                case FA.DEF.eManualMode.JIG_UNLOADING:
                    bManualComplete = ((RunningUnload)m_dicRunModules["Unload"]).SubSeq_Unloading(false);
                    break;
                case FA.DEF.eManualMode.MANUAL_MOVING:
                    bManualComplete = ((RunningStage)m_dicRunModules["STAGE"]).SubSeq_Moving(m_fManualMovingXPos, m_fManualMovingYPos, m_fManualMovingZPos);
                    break;

                #endregion

                default:
                    break;
            }

            return bManualComplete;
        }
        #endregion
        #region [AUTO SEQ]
        private bool Ready()
        {
            switch (m_stRun.nMainStep)
            {
                case (int)FA.DEF.eSeqReady.Finish:
                    m_stRun.nMainStep = FA.DEF.SEQ_FINISH;
                    return true;
                case (int)FA.DEF.eSeqReady.ready_prechk:
                    m_stRun.nMainStep = (int)FA.DEF.eSeqReady.modules_clear_status_start;
                    break;
                case (int)FA.DEF.eSeqReady.modules_clear_status_start:
                    m_stRun.nMainStep = (int)FA.DEF.eSeqReady.modules_clear_status_finish;
                    break;
                case (int)FA.DEF.eSeqReady.modules_clear_status_finish:
                    m_stRun.nMainStep = (int)FA.DEF.eSeqReady.modules_set_run_status_start;
                    break;
                case (int)FA.DEF.eSeqReady.modules_set_run_status_start:
                    m_stRun.nMainStep = (int)FA.DEF.eSeqReady.modules_set_run_status_finish;
                    break;
                case (int)FA.DEF.eSeqReady.modules_set_run_status_finish:
                    m_stRun.nMainStep = FA.DEF.SEQ_FINISH;
                    break;

            }

            return false;
        }
        private bool Running()
        {
            switch (CastTo<FA.DEF.eSeqRunning>.From(m_stRun.nMainStep))
            {
                case FA.DEF.eSeqRunning.Finish:
                    {
                        m_stRun.nMainStep = FA.DEF.SEQ_FINISH;
                        return true;
                    }
                case FA.DEF.eSeqRunning.Loop_prechk:
                    {
                        NextMainStep(FA.DEF.eSeqRunning.Loop_start);
                    }
                    break;
                case FA.DEF.eSeqRunning.Loop_start:
                    {
                        bool bIsCompleted = true;
                        foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                        {
                            bIsCompleted &= item.Value.ToRun();
                        }
                        //bIsCompleted&=GetItem("SCANNER").ToRun();

                        if (bIsCompleted)
                            NextMainStep(FA.DEF.eSeqRunning.Loop);
                        else
                            NextMainStep(FA.DEF.SEQ_FINISH);
                    }
                    break;
                case FA.DEF.eSeqRunning.Loop:
                    {
                        bool bIsCompleted = true;
                        //bIsCompleted&=GetItem("SCANNER").Run();
                        foreach (KeyValuePair<string, RunningBase> item in m_dicRunModules)
                        {
                            bIsCompleted &= item.Value.Run();
                        }
                        if (bIsCompleted)
                        {
                            NextMainStep(FA.DEF.eSeqRunning.Loop_finish);
                        }
                    }
                    break;

                case FA.DEF.eSeqRunning.Loop_finish:
                    {
                        //                         if (IsTaskCompleted(FA.DEF.eTasks.JobEnd))
                        //                         {
                        //                             UpdateTaskAllClear();
                        //                             WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_ALARM, FA.DEF.ALM_JOB_FINISH);
                        //                             NextMainStep(FA.DEF.eSeqRunning.Alarm);
                        //                         }
                        if (eRunMode == FA.DEF.eRunMode.ToStop)
                        {
                            NextMainStep(FA.DEF.SEQ_FINISH);
                        }
                        else
                        {
                            NextMainStep(FA.DEF.eSeqRunning.Loop);
                        }
                    }
                    break;

                case FA.DEF.eSeqRunning.Alarm:
                    break;
            }
            return false;
        }
        #endregion
        #region [WORKS INFO]
        public bool UpdateWorksStart()
        {
            if (PM == null)
                return false;

            //m_WorksInfo.Start();
            return true;
        }
        public bool UpdateWorksFinish()
        {
            if (PM == null)
                return false;

            //m_WorksInfo.Finish();
            return true;
        }
        public bool UpdateWorksTime(FA.DEF.eRunMode a_eRunMode)
        {
            if (PM == null)
                return false;

            PM.UpdateTime(a_eRunMode);
            return true;
        }
        public void DataGridViewWorks_Init(DataGridView a_pDataGridView_Modules)
        {
            if (a_pDataGridView_Modules.RowCount == 0)
            {
                //선택된 모든 셀의 선택해제
                //a_grid.ClearSelection();
                //첫번째 행 선택
                //a_grid.Rows[0].Selected = true;
                //마지막 행 선택
                //a_grid.Rows[a_grid.Rows.Count - 1].Selected = true;
                //선택행에 삽입.
                //a_grid.Rows.Insert(1, "test");
                //마지막행에 삽입.
                //a_grid.Rows.Add("last");
                //첫번째 선택된 행의 인덱스값.
                //a_grid.SelectedRows[0].Index;
                //특정 행 삭제
                //a_grid.Rows.RemoveAt(0);  //삭제
                //선택 행의 색 바꾸기
                //a_grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                //a_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
                //특정 행의 색 바꾸기
                //a_grid.Rows[2].DefaultCellStyle.BackColor = Color.Red;
                //특정 행열의 색 바꾸기
                //a_grid.Rows[i].Cells[col].Style.BackColor = ColorTranslator.FromHtml(123, 123, 123, 123);
                //셀 내용 읽기 0행, 0열
                //a_grid.Rows[0].Cells[0].Value.ToString();
                //행의 총수
                //int lines = a_grid.Rows.Count
                //우측 스크롤바 현재 셀위치를 보여주게 자동 이동
                //a_grid.CurrentCell = a_grid.Rows[행].Cells[열];
                //활성화된 셀의 행 인덱스값
                //int select = a_grid.CurrentCell.RowIndex;
                a_pDataGridView_Modules.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
                a_pDataGridView_Modules.ReadOnly = false;
                a_pDataGridView_Modules.AllowUserToAddRows = false;
                a_pDataGridView_Modules.AllowUserToDeleteRows = false;
                a_pDataGridView_Modules.AllowUserToOrderColumns = false;
                a_pDataGridView_Modules.AllowUserToResizeColumns = false;
                a_pDataGridView_Modules.AllowUserToResizeRows = false;
                a_pDataGridView_Modules.ColumnHeadersVisible = true;
                a_pDataGridView_Modules.RowHeadersVisible = false;
                a_pDataGridView_Modules.MultiSelect = false;
                a_pDataGridView_Modules.EditMode = DataGridViewEditMode.EditOnEnter;
                a_pDataGridView_Modules.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;


                int height = (a_pDataGridView_Modules.Height) / 5;
                int nWidth = (a_pDataGridView_Modules.Width - 30) / 2;

                a_pDataGridView_Modules.Columns.Clear();
                a_pDataGridView_Modules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                a_pDataGridView_Modules.ColumnHeadersHeight = height;
                a_pDataGridView_Modules.RowTemplate.Height = height;
                a_pDataGridView_Modules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                //

                DataGridViewTextBoxColumn Number = new DataGridViewTextBoxColumn();
                Number.HeaderText = "No.";
                Number.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Number.Resizable = DataGridViewTriState.False;
                Number.ReadOnly = true;
                Number.Width = 30;
                Number.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Number.DefaultCellStyle.BackColor = SystemColors.Control;

                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                ItemName.HeaderText = "Item Name";
                ItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ItemName.Resizable = DataGridViewTriState.False;
                ItemName.ReadOnly = true;
                ItemName.Width = nWidth;
                ItemName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ItemName.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionForeColor = Color.Black;

                DataGridViewTextBoxColumn Value = new DataGridViewTextBoxColumn();
                Value.HeaderText = "Value";
                Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Value.Resizable = DataGridViewTriState.False;
                Value.ReadOnly = true;
                Value.Width = nWidth;
                Value.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Value.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_Off.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_Off.DefaultCellStyle.SelectionForeColor = Color.Black;

                a_pDataGridView_Modules.Columns.AddRange(new DataGridViewTextBoxColumn[] {
                                        Number, ItemName, Value});

                //Headers setting
                foreach (DataGridViewColumn col in a_pDataGridView_Modules.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }


                a_pDataGridView_Modules.Rows.Clear();
                for (int i = 0; i < 3; i++)
                {
                    a_pDataGridView_Modules.Columns[i].ReadOnly = true;
                }

                int index = 1;
                //a_pDataGridView_Modules.Rows.Add(index.ToString(), "PANEL ID", m_stWorksInfo.strLotID); index++;
                //a_pDataGridView_Modules.Rows.Add(index.ToString(), "START TIME", m_stWorksInfo.stwStartTime.GetTime()); index++;
                //a_pDataGridView_Modules.Rows.Add(index.ToString(), "STOP TIME", m_stWorksInfo.stwStopTime.GetTime()); index++;
                //a_pDataGridView_Modules.Rows.Add(index.ToString(), "CYCLE TIME", m_stWorksInfo.strCycleTime); index++;

                foreach (DataGridViewColumn col in a_pDataGridView_Modules.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }
        #endregion
        #region Step manager
        public void NextMainStep(int a_iSeq)
        {
            m_stRun.nMainStep = a_iSeq;
        }
        public void FinishMainStep()
        {
            m_stRun.nMainStep = FA.DEF.FinishActionOfSub;
        }
        public void NextMainStep()
        {
            m_stRun.nMainStep++;
        }
        public void NextMainStep<TEnum>(TEnum a_eSeq)
        {
            m_stRun.nMainStep = ConvertToIndex(a_eSeq);
        }
        public void NextSubStep<TEnum>(TEnum a_eSeq)
        {
            m_stRun.nSubStep = ConvertToIndex(a_eSeq);
        }
        int ConvertToIndex<TEnum>(TEnum key)
        {
            return CastTo<int>.From(key);
        }
        #endregion
    }//end of class

}//end of namespace

