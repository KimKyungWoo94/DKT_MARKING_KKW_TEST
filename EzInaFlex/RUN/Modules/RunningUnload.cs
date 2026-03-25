using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzIna.FA.DEF;
using EzIna.FA;
namespace EzIna
{
    public class RunningUnload : RunningBase
    {
        public enum eMODULE_SEQ_INIT_UNLOAD
        {
            Finish = 0
                    , IO_Check_Start = FA.DEF.StartActionOfMain
                    , IO_Check_Finish
                    , Cylinder_chk_start
                    , Cylinder_chk_Finish
                    ,
        }
        public enum eMODULE_STATUS_INIT_UNLOAD
        {
            IO_Check
                , Cylinder_Check
        }

        public enum eMODULE_SEQ_UNLOADING
        {
            Finish = 0
                , JIG_EXIST_CHK_Start = FA.DEF.StartActionOfSub
                , JIG_EXIST_CHK_Finish
                , IO_Check_Start = 20
                , IO_Check_Finish
                //, SEQ_Check_Start = 30
                //, SEQ_Check_Finish
                , Cylinder_chk_PreMoving_Start = 40
                , Cylinder_chk_PreMoving_Finish
                , MOVE_TEACHING_POS_Start = 50
                , MOVE_TEACHING_POS_Finish
                , Cylinder_chk_start = 60
                , Cylinder_chk_Finish
                , MOVE_PINICH_ROLLER_To_Senser_start = 70
                , MOVE_PINICH_ROLLER_To_Senser_Finish
                , DEVICE_IF_Start = 80
                , DEVICE_IF_Finish
                , PINICH_ROLLER_ACT_Start = 90
                , PINICH_ROLLER_ACT_IO_CY_Check
                , PINICH_ROLLER_ACT_Motor_Check
                , PINiCH_ROLLER_ACT_Finish




        }
        public enum eMODULE_SEQ_STATUS_UNLOADING
        {
            JIG_EXIST,
            IO_CHECK,
            Cylinder_check_PreMoveing,
            SEQ_CHECK,
            MOVE_TEACHING_POS,
            Cylinder_Check,
            Move_JIG_To_Sensor,
            DEVICE_IF,
            PINICH_ROLLER_ACT,
            UNLOADING_FINISH

        }

        public enum AUTO_AUTOMATIC_SEQ
        {
            AUTO_SEQ_FINISH_ACTION = 0,
            AUTO_SEQ_FIND_ACTION = 100,
            AUTO_SEQ_PROCESS = 200,
        }
        public Dictionary<eMODULE_STATUS_INIT_UNLOAD, FA.DEF.stRunModuleStatus> Interface_Init { get; set; }
        public Dictionary<eMODULE_SEQ_STATUS_UNLOADING, FA.DEF.stRunModuleStatus> Interface_SEQ { get; set; }
        Stopwatch m_TimeStamp = null;
        RunningScanner m_pRunScanner;
        RunningLoad m_pRunLoad;
        public RunningUnload(string a_strModuleName, int a_iModuleIndex) : base(a_strModuleName, a_iModuleIndex)
        {
            Interface_Init = new Dictionary<eMODULE_STATUS_INIT_UNLOAD, FA.DEF.stRunModuleStatus>();
            Interface_SEQ = new Dictionary<eMODULE_SEQ_STATUS_UNLOADING, FA.DEF.stRunModuleStatus>();
            foreach (eMODULE_STATUS_INIT_UNLOAD item in Enum.GetValues(typeof(eMODULE_STATUS_INIT_UNLOAD)))
            {
                Interface_Init.Add(item, new FA.DEF.stRunModuleStatus());
            }
            foreach (eMODULE_SEQ_STATUS_UNLOADING item in Enum.GetValues(typeof(eMODULE_SEQ_STATUS_UNLOADING)))
            {
                Interface_SEQ.Add(item, new FA.DEF.stRunModuleStatus());
            }
            m_TimeStamp = new Stopwatch();
        }

        public override void ConnectingModule()
        {
            m_pRunScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
            m_pRunLoad = (RunningLoad)FA.MGR.RunMgr.GetItem("Load");
        }

        public int InitSEQStep
        {
            get { return FA.DEF.Error_Init_Unloader + m_stRun.nMainStep; }
        }
        public override void Init()
        {


            switch (CastTo<eMODULE_SEQ_INIT_UNLOAD>.From(m_stRun.nMainStep))
            {
                case eMODULE_SEQ_INIT_UNLOAD.Finish:
                    return;
                case eMODULE_SEQ_INIT_UNLOAD.IO_Check_Start:
                    {
                        UpdateInitStatus(eMODULE_STATUS_INIT_UNLOAD.IO_Check, false);
#if SIM
#else
                        if (eDI.RAIL_MOUTH_R.GetDI().Value == true)
                        {
                            FA.MGR.RunMgr.ModeChange(
                                         eRunMode.Jam,
                                         InitSEQStep, eManualMode.None,
                                         string.Format("Unload RAIL Mouth sensor detected OBJ")
                                         );
                            break;
                        }
                        if (eDI.JIG_POS_DETECTED_R.GetDI().Value == true)
                        {
                            FA.MGR.RunMgr.ModeChange(
                                         eRunMode.Jam,
                                         InitSEQStep, eManualMode.None,
                                         string.Format("JIG POS Detected R sensor detected OBJ")
                                         );
                            break;
                        }
                        eDO.PINCH_ROLLER_R_U.GetDO().Value = false;
                        eDO.PINCH_ROLLER_R_B.GetDO().Value = false;
#endif
                        NextMainStep();
                    }
                    break;
                case eMODULE_SEQ_INIT_UNLOAD.IO_Check_Finish:
                    {
                        UpdateInitStatus(eMODULE_STATUS_INIT_UNLOAD.IO_Check, true);
                        NextMainStep();
                    }
                    break;
                case eMODULE_SEQ_INIT_UNLOAD.Cylinder_chk_start:
                    {
                        UpdateInitStatus(eMODULE_STATUS_INIT_UNLOAD.Cylinder_Check, false);
#if SIM
#else
                        FA.ACT.CYL_STOPPER_R_DOWN.Run();
#endif
                        m_stRun.stwatchForSub.SetDelay = 1000;
                        NextMainStep();
                    }
                    break;
                case eMODULE_SEQ_INIT_UNLOAD.Cylinder_chk_Finish:
                    {
                        if (!m_stRun.stwatchForSub.IsDone)
                            break;
#if SIM
#else
                        if (!FA.ACT.CYL_STOPPER_R_DOWN.Check()) break;
#endif
                        UpdateInitStatus(eMODULE_STATUS_INIT_UNLOAD.Cylinder_Check, true);
                        InterfaceRunClear();
                        FinishMainStep();
                        IsDone_Init = true;
                    }
                    break;
            }

            MainSeqCheckTimeout(FA.DEF.Timeout_Init, FA.DEF.Error_Init_Unloader + m_stRun.nMainStep);
        }
        public eMODULE_SEQ_UNLOADING SubSeqStep
        {
            get { return CastTo<eMODULE_SEQ_UNLOADING>.From(m_stRun.nSubStep); }
        }

        public int SubSEQUnloadingStep
        {
            get { return DEF.Error_Run_LOADING + m_stRun.nSubStep; }
        }
        public bool SubSeq_Unloading(bool a_bAuto)
        {
            if (!base.SetRecoveryRunInfo())
                return false;
            switch (CastTo<eMODULE_SEQ_UNLOADING>.From(m_stRun.nSubStep))
            {
                case eMODULE_SEQ_UNLOADING.Finish:
                    return true;

                case eMODULE_SEQ_UNLOADING.JIG_EXIST_CHK_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.JIG_EXIST, false);
#if SIM
#else
                    if (eDI.JIG_FEEDER_JIG_EXIST.GetDI().Value == false)
                    {
                        FA.MGR.RunMgr.ModeChange(
                                         eRunMode.Jam,
                                         InitSEQStep, eManualMode.None,
                                         string.Format("JIG Not detected")
                                         );
                        break;
                    }
                    if (FA.ACT.CYL_JIG_FEEDER_L_CLAMP.CurrentStatus() == false ||
                            FA.ACT.CYL_JIG_FEEDER_R_CLAMP.CurrentStatus() == false)
                    {
                        FA.MGR.RunMgr.ModeChange(
                                         eRunMode.Jam,
                                         InitSEQStep, eManualMode.None,
                                         string.Format("JIG FEEDER Clamp Status NG\nL:{0},R:{1}",
                                         FA.ACT.CYL_JIG_FEEDER_L_CLAMP.CurrentStatus() == true ? "Clamp" : "Unclamp",
                                         FA.ACT.CYL_JIG_FEEDER_R_CLAMP.CurrentStatus() == true ? "Clamp" : "Unclamp")
                                         );
                        break;
                    }
#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.JIG_EXIST_CHK_Finish:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.JIG_EXIST, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.IO_Check_Start);
                    break;
                case eMODULE_SEQ_UNLOADING.IO_Check_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.IO_CHECK, false);
#if SIM
#else
                    eDO.PINCH_ROLLER_R_U.GetDO().Value = false;
                    eDO.PINCH_ROLLER_R_B.GetDO().Value = false;
#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.IO_Check_Finish:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.IO_CHECK, true);
                    eDO.UNLOADER_START_CMD.GetDO().Value = false;
                    NextSubStep(eMODULE_SEQ_UNLOADING.Cylinder_chk_PreMoving_Start);
                    break;
                case eMODULE_SEQ_UNLOADING.Cylinder_chk_PreMoving_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Cylinder_check_PreMoveing, false);
#if SIM
#else
                    FA.ACT.CYL_STOPPER_L_DOWN.Run();
                    FA.ACT.CYL_STOPPER_R_DOWN.Run();
#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.Cylinder_chk_PreMoving_Finish:
#if SIM
#else
                    if (!FA.ACT.CYL_STOPPER_L_DOWN.Check()) break;
                    if (!FA.ACT.CYL_STOPPER_R_DOWN.Check()) break;
#endif
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Cylinder_check_PreMoveing, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.MOVE_TEACHING_POS_Start);
                    break;

                case eMODULE_SEQ_UNLOADING.MOVE_TEACHING_POS_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.MOVE_TEACHING_POS, false);
                    if (!AXIS.RX.Move_Absolute(RCP.M100_RAIL_UNLOADING_X_POS.AsDouble, Motion.GDMotion.eSpeedType.RUN))
                        break;
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.MOVE_TEACHING_POS_Finish:
                    if (!AXIS.RX.Status().IsMotionDone)
                        break;
                    if (!AXIS.RX.Status().IsInposition)
                        break;

                    if (a_bAuto)
                    {
                        m_pRunLoad.UpdateSEQ_Status(RunningLoad.eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH, false);
                    }
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.MOVE_TEACHING_POS, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.Cylinder_chk_start);
                    break;

                case eMODULE_SEQ_UNLOADING.Cylinder_chk_start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Cylinder_Check, false);
#if SIM
#else
                    FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
                    FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
#endif
                    m_stRun.stwatchForSub.SetDelay = 100;
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.Cylinder_chk_Finish:
                    if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
                    if (!FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Check()) break;
                    if (!FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Check()) break;

                    eDO.PINCH_ROLLER_R_U.GetDO().Value = true;
                    eDO.PINCH_ROLLER_R_B.GetDO().Value = true;
#endif
                    if (a_bAuto)
                    {
                        FA.MGR.RunMgr.PM.StopCycleTime();
                        FA.MGR.RunMgr.PM.JIGFinish(
                        FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                        FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                        FA.MGR.RecipeRunningData.lProductStartIDX.ToString(),
                        (FA.MGR.RecipeRunningData.lProductStartIDX + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1).ToString()
                        );
                    }

                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Cylinder_Check, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.MOVE_PINICH_ROLLER_To_Senser_start);
                    break;
                case eMODULE_SEQ_UNLOADING.MOVE_PINICH_ROLLER_To_Senser_start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Move_JIG_To_Sensor, false);
#if SIM
#else
                    eDO.PINCH_ROLLER_R_U.GetDO().Value = true;
                    eDO.PINCH_ROLLER_R_B.GetDO().Value = true;
#endif
#if SIM
										m_stRun.stwatchForSub.SetDelay = 1000;
#else
                    if (!AXIS.PR_R_U.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN))
                        break;
                    if (!AXIS.PR_R_B.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN))
                        break;
#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.MOVE_PINICH_ROLLER_To_Senser_Finish:
#if SIM
										if (!m_stRun.stwatchForSub.IsDone) break;
#else
                    if (eDI.RAIL_MOUTH_R.GetDI().Value == false)
                        break;
                    if (eDI.JIG_FEEDER_JIG_EXIST.GetDI().Value == true)
                        break;

                    if (!AXIS.PR_R_U.JOG_STOP())
                        break;
                    if (!AXIS.PR_R_B.JOG_STOP())
                        break;
#endif
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Move_JIG_To_Sensor, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.DEVICE_IF_Start);
                    break;
                case eMODULE_SEQ_UNLOADING.DEVICE_IF_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.DEVICE_IF, false);

                    m_stRun.bHoldOnTimeout = true;
#if SIM
#else
                    if (OPT.RunninguUnLoaderIFEnable.m_bState)
                    {
                        if (eDI.UNLOADER_IF_READY.GetDI().Value == false)
                            break;
                        eDO.UNLOADER_START_CMD.GetDO().Value = true;
                    }
#endif


                    // to be continue
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.DEVICE_IF_Finish:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.DEVICE_IF, true);
                    NextSubStep(eMODULE_SEQ_UNLOADING.PINICH_ROLLER_ACT_Start);
                    break;
                case eMODULE_SEQ_UNLOADING.PINICH_ROLLER_ACT_Start:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.PINICH_ROLLER_ACT, false);
#if SIM
#else
                    if (eDI.RAIL_MOUTH_R.GetDI().Value == false)
                        break;

#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.PINICH_ROLLER_ACT_IO_CY_Check:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.PINICH_ROLLER_ACT, false);
#if SIM
										m_stRun.stwatchForSub.SetDelay = 2000;
#else
                    //eDO.PINCH_ROLLER_R_U.GetDO().Value = false;
                    //eDO.PINCH_ROLLER_R_B.GetDO().Value = false;

                    if (!FA.AXIS.PR_R_U.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN))
                    {
                        AXIS.PR_R_U.JOG_STOP();
                        break;
                    }

                    if (!FA.AXIS.PR_R_B.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN))
                    {
                        AXIS.PR_R_B.JOG_STOP();
                        break;
                    }
#endif
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.PINICH_ROLLER_ACT_Motor_Check:
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.PINICH_ROLLER_ACT, false);
#if SIM
										if (!m_stRun.stwatchForSub.IsDone) break;
#else
                    if (eDI.RAIL_MOUTH_R.GetDI().Value == true)
                        break;
                    if (!FA.AXIS.PR_R_U.JOG_STOP()) break;
                    if (!FA.AXIS.PR_R_B.JOG_STOP()) break;
#endif
                    eDO.UNLOADER_START_CMD.GetDO().Value = false;
                    m_stRun.stwatchForSub.SetDelay = 10;
                    NextSubStep();
                    break;
                case eMODULE_SEQ_UNLOADING.PINiCH_ROLLER_ACT_Finish:
                    if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
                    if (!FA.AXIS.PR_R_U.Status().IsMotionDone)
                        break;
                    if (!FA.AXIS.PR_R_B.Status().IsMotionDone)
                        break;
                    eDO.PINCH_ROLLER_R_U.GetDO().Value = false;
                    eDO.PINCH_ROLLER_R_B.GetDO().Value = false;
#endif
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.PINICH_ROLLER_ACT, true);
                    UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.UNLOADING_FINISH, true);
                    FinishSubStep();
                    break;
            }

            SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_LOADING + m_stRun.nSubStep);
            return false;
        }
        private void UpdateInitStatus(eMODULE_STATUS_INIT_UNLOAD a_eItem, bool a_bCompleted)
        {
            if ((int)a_eItem < 0 || (int)a_eItem >= Interface_Init.Count)
                return;

            FA.DEF.stRunModuleStatus stWork = Interface_Init[a_eItem];
            if (a_bCompleted)
                stWork.SetFinish();
            else
                stWork.SetStart();

            Interface_Init[a_eItem] = stWork;
        }
        public void UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING a_eItem, bool a_bCompleted)
        {
            if ((int)a_eItem < 0 || (int)a_eItem >= Interface_SEQ.Count)
                return;

            FA.DEF.stRunModuleStatus stWork = Interface_SEQ[a_eItem];
            if (a_bCompleted)
                stWork.SetFinish();
            else
                stWork.SetStart();

            Interface_SEQ[a_eItem] = stWork;
        }
        public bool IsDone_SEQ_Stauts(eMODULE_SEQ_STATUS_UNLOADING a_eItem)
        {
            return Interface_SEQ[a_eItem].IsDone();
        }
        public bool IsDone_InitStauts(eMODULE_STATUS_INIT_UNLOAD a_eItem)
        {
            return Interface_Init[a_eItem].IsDone();
        }
        public override bool InterfaceRunClear()
        {
            FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
            eMODULE_SEQ_STATUS_UNLOADING item;
            for (int i = 0; i < Interface_SEQ.Count; i++)
            {
                item = (eMODULE_SEQ_STATUS_UNLOADING)i;
                stWork = Interface_SEQ[item];
                stWork.Clear();
                Interface_SEQ[item] = stWork;
            }
            UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.Cylinder_Check, true);
            UpdateSEQ_Status(eMODULE_SEQ_STATUS_UNLOADING.UNLOADING_FINISH, true);
            return true;
        }

        public override bool FindAction()
        {
            if (!base.SetRecoveryRunInfo())
                return false;
            if (m_pRunScanner.IsDone_SEQStauts(RunningScanner.eMODULE_SEQ_STATUS_PROC.JOB_END) &&
                    m_pRunLoad.IsDone_SEQ_Stauts(RunningLoad.eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH))
            {
                base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_PROCESS;
            }
            // do to list 
            // Flag , Status 
            // Loading Complate Flag ADD
            return true;
        }

        public override void Ready()
        {

        }

        public override bool Run()
        {
            bool bComplete = false;

            switch (CastTo<AUTO_AUTOMATIC_SEQ>.From(m_stRun.nMainStep))
            {
                case AUTO_AUTOMATIC_SEQ.AUTO_SEQ_FINISH_ACTION:
                    {
                        bComplete = true;
                        m_stRun.bRunStop = true;
                    }
                    break;
                case AUTO_AUTOMATIC_SEQ.AUTO_SEQ_FIND_ACTION:
                    {
                        if (IsRunModeStopped())
                        {
                            base.m_stRun.nMainStep = FA.DEF.FinishActionOfMain;
                            base.m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
                            break;
                        }
                        else
                        {
                            if (this.FindAction())
                            {
                                m_stRun.nSubStep = FA.DEF.StartActionOfSub;
                            }
                        }

                    }
                    break;

                case AUTO_AUTOMATIC_SEQ.AUTO_SEQ_PROCESS:
                    {
                        if (this.SubSeq_Unloading(true))
                        {
                            base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_FIND_ACTION;
                            base.m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
                            base.SetRecoveryRunInfo();
                            //m_pRunLoad.UpdateSEQ_Status(RunningLoad.eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH, false);
                        }
                    }
                    break;
            }
            return bComplete;
        }
        public override void Stop()
        {

        }
    }
}
