using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Aerotech.A3200.Tasks;
using static EzIna.FA.DEF;
using System.Collections.Concurrent;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EzIna
{
    public partial class RunningScanner
    {
        #region [INIT]
        public enum eMODULE_SEQ_INIT_SCANNER
        {
            Finish = 0
            , IO_Check_Start = FA.DEF.StartActionOfMain
            , IO_Check_Finish
            , CYL_Check_Start = 50
            , CYL_Check_Finish
            , Motor_Home_Start = 60
                            , Motor_Home_Chk
            , Motor_Home_Finish

            , Motor_Home_RA_RB_Start = 70
            , Motor_Home_RA_RB_Finish
            , MAX
        }
        public enum eMODULE_STATUS_INIT_SCANNER
        {
            IO_Check = 0
            , CYL_Check
            , Motor_Robot_Homing_Is_Completed
            , Motor_RA_Homing_Is_Completed
            , Motor_RB_Homing_Is_Completed
            , Max
        }
        #endregion
        public enum eMODULE_SEQ_PROC
        {
            Finish = 0
                        , RECIPE_CHK_START = FA.DEF.StartActionOfSub
                        , RECIPE_CHK_FINISH
                        , JIG_EXIST_CHK_Start = 20
                        , JIG_EXIST_CHK_Finish
                        , IO_Check_Start = 30
                        , IO_Check_Finish
                        , Cylinder_chk_PreMoving_Start = 40
                        , Cylinder_chk_PreMoving_Finish

                        /// For MES JIG Code 

                        , MOVE_JIG_CODE_POS_START = 50
                        , MOVE_JIG_CODE_POS_FINISH
                        , JIG_CODE_INSP_Condition_Check = 60
                        , JIG_CODE_INSP_Condition_CAM_CHECK
                        , JIG_CODE_INSP_Condition_CAM_FINISH
                        , JIG_CODE_INSP_Condition_LIGHT_CHECK
                        , JIG_CODE_INSP_Condition_LIGHT_FINISH
                        , JIG_CODE_INSP_GRAB_START
                        , JIG_CODE_INSP_GRAB_FINISH
                        , JIG_CODE_INSP_RUN_START
                        , JIG_CODE_INSP_RUN_FINISH

                        , MES_MARKING_START_INFO_START = 70
                                                , MES_MARKING_START_PACKET_SEND
                        , MES_MARKING_START_CONDITION_CHECK
                                                , MES_MARKING_START_INFO_FINISH

                        , MOVE_PROC_POS_Start = 80
                        , MOVE_PROC_POS_Precheck
                        , MOVE_PROC_POS_Action
                        , MOVE_PROC_POS_Finish
                        , MARKING_POS_INSP_Condition_Check = 90
                        , MARKING_POS_INSP_Condition_CAM_CHECK
                        , MARKING_POS_INSP_Condition_CAM_FINISH
                        , MARKING_POS_INSP_Condition_LIGHT_CHECK
                        , MARKING_POS_INSP_Condition_LIGHT_FINISH
                        , MARKING_POS_INSP_Condition_PARAM_CHECK
                        , MARKING_POS_INSP_Condition_PARAM_FINISH
                        , MARKING_POS_INSP_Condition_Finish
                        , MARKING_POS_INSP_RUN_GRAB_CHECK = 100
                        , MARKING_POS_INSP_RUN_GRAB_FINISH
                        , MARKING_POS_INSP_RUN_START
                        , MARKING_POS_INSP_RUN_RESULT_CHECK
                        , MARKING_POS_INSP_RUN_FINISH

                        , MARKING_PARAM_CHECK_START = 110
                        , MARKING_LASER_PARAM_CHECK
                        , MARKING_LASER_PARAM_FINSISH
                        , MARKING_LASER_ENABLE_CHECK
                        , MARKING_LASER_ENABLE_FINISH
                        , MARKING_PROCESS_PARAM_CHECK
                        , MARKING_PROCESS_PARAM_FINISH
                        , MARKING_CREATE_DM_CHECK
                        , MARKING_CREATE_DM_FINISH
                        , MARKING_PARAM_CHECK_FINISH
                        , MARKING_LIST_START = 120
                        , MARKING_LIST_LOOP_START
                        , MARKING_LIST_LOOP_FINISH
                        , MARKING_LIST_FINISH
                        , MARKING_INSP_Condition_Check = 130
                        , MARKING_INSP_Condition_CAM_CHECK
                        , MARKING_INSP_Condition_CAM_FINISH
                        , MARKING_INSP_Condition_LIGHT_CHECK
                        , MARKING_INSP_Condition_LIGHT_FINISH
                        , MARKING_INSP_GRAB_START
                        , MARKING_INSP_GRAB_FINISH
                        , MARKING_INSP_RUN_START
                        , MARKING_INSP_RUN_FINISH
                        , PROC_END_Check_Start = 140
                        , PROC_END_Check_Finish
                                                , PROC_MES_Marking_Report_Start = 150
                                                , PROC_MES_Marking_Report_Finish
                        , PROC_END_Start = 160
                        , PROC_END_MES_CHECK
                        , PROC_END_MES_FINISH
                                                , PROC_END_PRODUCT_ERROR_CHECK
                        , PROC_END_Finish




        }
        public enum eMODULE_SEQ_STATUS_PROC
        {
            RECIPE_VAILD_CHECK,
            JIG_EXIST,
            IO_CHECK,
            Cylinder_check_PreMoveing,
            MOVE_PROC_POS,
            MARKING_POS_INSP,
            MARKING,
            MARKING_POS_INSP_Condition_Check,
            MARKING_POS_INSP_CAM_CHECK,
            MARKING_POS_INSP_LIGHT_CHECK,
            MARKING_POS_INSP_PARAM_CHECK,
            MARKING_POS_INSP_RUN,
            MARKING_RUN,
            MARKING_RUN_LASER_ENABLE_CHECK,
            MARKING_RUN_PROC_PARAM_CHECK,
            MARKING_RUN_CREATE_DM,
            MARKING_RUN_LIST,

            MARKING_INSP,
            JOB_END_CHECK,
            JOB_MES_MARKING_REPORT,
            JOB_END,
        }

        public enum AUTO_AUTOMATIC_SEQ
        {
            AUTO_SEQ_FINISH_ACTION = 0,
            AUTO_SEQ_FIND_ACTION = 100,
            AUTO_SEQ_PROCESS = 200,
        }
    }
    public partial class RunningScanner : RunningBase
    {
        public Dictionary<eMODULE_STATUS_INIT_SCANNER, FA.DEF.stRunModuleStatus> Interface_Init { get; set; }
        public Dictionary<eMODULE_SEQ_STATUS_PROC, FA.DEF.stRunModuleStatus> Interface_SEQ { get; set; }
        static int m_iMotionMoveDoneDelay = 1000;
        static int m_iGrabDoneDelay = 150;
        Stopwatch m_TimeStamp = null;
        RunningStage m_pRunningStage = null;
        RunningProcess m_pRunningProcess = null;
        RunningLoad m_pRunningLoad = null;
        RunningUnload m_pRunningUnload = null;
        IniFile m_IniPowerTable = null;
        double m_fSubSeqTargetPosX;
        double m_fSubSeqTargetPosY;
        double m_fSubSeqTargetPosZ;

        double m_fSubSeqRowMovingPitch;
        double m_fSubSeqColMovingPitch;
        int m_iSubSeqRowMovingMax;
        int m_iSubSeqColMovingMax;
        
        bool m_bPreSubSeqMovingLastPerOneLine;
        int m_iSubSeqInspRowCount;
        int m_iSubSeqInspColCount;
        int m_iSubSeqRowMovingExeCount;
        int m_iSubSeqColMovingExeCount;
        int m_iSubSeqInspRowLastCount;

        int m_iSubSeqProductDoneCount;
        int m_iSubSeqProductDoneMaxCount;
        int m_iSubSeqProductDoneRowProgressCount;
        int m_iSubSeqProductDoneColProgressCount;
        bool m_bSusbSeqRowReverseDirEnable;
        //bool m_bSusbSeqColReverseDirEnable;
        int m_iSubSeqMarkingExeCount;
        int m_iSubSeqMarkingExeMax;
        int m_iSubSeqProductIDX;

        Tuple<bool, string> m_pMultiArrayCheckRet;
        List<EzInaVision.GDV.MatchResult> m_pMatchResultList;
        List<EzInaVision.GDV.MatrixCodeResult> m_pMatrixCodeResultList;
        List<EzInaVision.GDV.MatrixCodeResult> m_pJIGCodeMatrixCodeResultList;
        EzCAM_Ver2.Hatch_Option m_pProcessHatchOption;

        List<RunningDataItem> m_pProcessDataList;
        List<RunningDataItem> m_pMarkingReportDataList;
        List<Rectangle> m_pDataMatrixInspRectList;
        List<string> m_pDataMatrixInspNGFileNames;

        List<Tuple<MF.RecipeItemBase, MF.RecipeItemBase>> m_pMarkingOffSetArray;
        RectangleF m_pProcessIDXRectTemp;
        double m_fMarkingCenterX;
        double m_fMarkingCenterY;
        int m_iProcessedJigCount = 0;
        bool m_bProcessEndChecked = false;


        int m_iTotalArrayCount;
        int m_iSubSeqMultiArrayExeCount;
        int m_iSubSeqMultiArrayMaxCount;
        int m_iSubSeqPreRowMovingExeCount;
        int m_iSubSeqPreColMovingExeCount;


        long m_iSubSeqProductStartNo = 0;

        string m_strSubSeqMarkingStingTemp = "";
        double m_fMarkingWidthPixel = 0.0;
        double m_fMarkingHeightPixel = 0.0;
        double m_fMarkingCenterXPixel = 0.0;
        double m_fMarkingCenterYPixel = 0.0;
        double m_fPixelResoltuionX = 0.0;
        double m_fPixelResoltuionY = 0.0;
        double m_fPixelErrorWidth = 0.0;
        double m_fPixelErrorHeight = 0.0;

        double m_fJIGCodePosX = 0.0;
        double m_fJIGCodePosY = 0.0;
        double m_fJIGCodePosZ = 0.0;
        double m_fMarkingOffSetX = 0.0;
        double m_fMarkingOffSetY = 0.0;


        public int m_iSubSEQMES_TestMode_ProductDoneCount = 0;

        
        string strDM_NG_SavePath;
        string strPOSINSP_NG_SavePath;
        DateTime m_SubSeqpProcessStartTime;
        Scanner.ScanlabRTC5.RTC_LIST m_pExecuteListIDX;
        int m_iExecuteListEXECount=0;
        Scanner.ScanlabRTC5.RTC_LIST m_pSubExecuteListIDX;
        int m_iSubExecuteListEXECount=0;
        int m_iRepaintExecuteListCount=1;
        Stopwatch m_pTestStopWatch = new Stopwatch();
        public RunningScanner(string a_strModuleName, int a_iModuleIndex) : base(a_strModuleName, a_iModuleIndex)
        {
            Interface_Init = new Dictionary<eMODULE_STATUS_INIT_SCANNER, FA.DEF.stRunModuleStatus>();
            Interface_SEQ = new Dictionary<eMODULE_SEQ_STATUS_PROC, stRunModuleStatus>();

            foreach (eMODULE_STATUS_INIT_SCANNER item in Enum.GetValues(typeof(eMODULE_STATUS_INIT_SCANNER)))
            {
                Interface_Init.Add(item, new FA.DEF.stRunModuleStatus());
            }
            foreach (eMODULE_SEQ_STATUS_PROC item in Enum.GetValues(typeof(eMODULE_SEQ_STATUS_PROC)))
            {
                Interface_SEQ.Add(item, new FA.DEF.stRunModuleStatus());
            }
            m_pProcessHatchOption = new EzCAM_Ver2.Hatch_Option();
            m_pProcessDataList = new List<RunningDataItem>();
            m_pMarkingReportDataList = new List<RunningDataItem>();
            m_pMatchResultList = new List<EzInaVision.GDV.MatchResult>();
            m_pMatrixCodeResultList = new List<EzInaVision.GDV.MatrixCodeResult>();
            m_pJIGCodeMatrixCodeResultList = new List<EzInaVision.GDV.MatrixCodeResult>();
            m_pDataMatrixInspRectList = new List<Rectangle>();
            m_pDataMatrixInspNGFileNames = new List<string>();
            m_TimeStamp = new Stopwatch();
            m_pProcessIDXRectTemp = new RectangleF();
            m_pMarkingOffSetArray = new List<Tuple<MF.RecipeItemBase, MF.RecipeItemBase>>();
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

        }

        public bool SetSubSeqProcessTargetPosition(double a_PosX, double a_PosY, double a_PosZ)
        {
            if (m_stRun.nSubStep != 0)
                return false;
            m_fSubSeqTargetPosX = a_PosX;
            m_fSubSeqTargetPosY = a_PosY;
            m_fSubSeqTargetPosZ = a_PosZ;
            return true;
        }
        #region override
        public override bool InterfaceInitClear()
        {
            FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
            eMODULE_STATUS_INIT_SCANNER item;
            for (int i = 0; i < Interface_Init.Count; i++)
            {
                item = (eMODULE_STATUS_INIT_SCANNER)i;
                stWork = Interface_Init[item];
                stWork.Clear();
                Interface_Init[item] = stWork;
            }


            return true;
        }

        public override bool InterfaceRunClear()
        {
            FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
            eMODULE_SEQ_STATUS_PROC item;
            for (int i = 0; i < Interface_SEQ.Count; i++)
            {
                item = (eMODULE_SEQ_STATUS_PROC)i;
                stWork = Interface_SEQ[item];
                stWork.Clear();
                Interface_SEQ[item] = stWork;
            }
            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_END, true);
            return true;
        }
        public override void Init()
        {
            try
            {
                switch (CastTo<eMODULE_SEQ_INIT_SCANNER>.From(m_stRun.nMainStep))
                {
                    case eMODULE_SEQ_INIT_SCANNER.Finish:
                        return;

                    case eMODULE_SEQ_INIT_SCANNER.IO_Check_Start:
#if SIM
#else
                        if (FA.DEF.eDI.XYZ_MOTOR_PWR_MC.GetDI().Value == false)
                            break;
                        FA.DEF.eDO.DEBRIS_SOL.GetDO().Value = true;
#endif
                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.IO_Check, false);
                        NextMainStep();
                        break;

                    case eMODULE_SEQ_INIT_SCANNER.IO_Check_Finish:

                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.IO_Check, true);
                        NextMainStep(eMODULE_SEQ_INIT_SCANNER.CYL_Check_Start);
                        break;

                    case eMODULE_SEQ_INIT_SCANNER.CYL_Check_Start:
#if SIM
#else
                        FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
                        FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
#endif
                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.CYL_Check, false);
                        NextMainStep();
                        break;

                    case eMODULE_SEQ_INIT_SCANNER.CYL_Check_Finish:
#if SIM
#else
                        if (!FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Check()) break;
                        if (!FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Check()) break;
                        if (m_pRunningLoad.IsDone_InitStauts(RunningLoad.eMODULE_STATUS_INIT_LOAD.Cylinder_Check) == false)
                            break;
                        if (m_pRunningUnload.IsDone_InitStauts(RunningUnload.eMODULE_STATUS_INIT_UNLOAD.Cylinder_Check) == false)
                            break;
#endif
                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.CYL_Check, true);
                        NextMainStep(eMODULE_SEQ_INIT_SCANNER.Motor_Home_Start);
                        break;

                    case eMODULE_SEQ_INIT_SCANNER.Motor_Home_Start:
                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.Motor_Robot_Homing_Is_Completed, false);

                        AXIS.RZ.ServoOn = true;
                        AXIS.Y.ServoOn = true;
                        AXIS.RX.ServoOn = true;
                        m_stRun.stwatchForSub.SetDelay = 100;

                        NextMainStep();
                        break;

                    case eMODULE_SEQ_INIT_SCANNER.Motor_Home_Chk:
                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.Motor_Robot_Homing_Is_Completed, false);
                        if (!m_stRun.stwatchForSub.IsDone)
                            break;
                        if (!AXIS.RZ.Status().IsServoOn)
                            break;
                        if (!AXIS.RX.Status().IsServoOn)
                            break;
                        if (!AXIS.Y.Status().IsServoOn)
                            break;
                        if (!AXIS.RZ.HomeStart())
                            break;
                        if (!AXIS.RX.HomeStart())
                            break;
                        if (!AXIS.Y.HomeStart())
                            break;

                        m_stRun.stwatchForSub.SetDelay = 1000;
                        NextMainStep();
                        break;


                    case eMODULE_SEQ_INIT_SCANNER.Motor_Home_Finish:
                        if (!m_stRun.stwatchForSub.IsDone)
                            break;
                        if (!AXIS.RZ.Status().IsHomeComplete)
                            break;
                        if (!AXIS.RZ.Status().IsMotionDone)
                            break;
                        if (!AXIS.RX.Status().IsHomeComplete)
                            break;
                        if (!AXIS.RX.Status().IsMotionDone)
                            break;
                        if (!AXIS.Y.Status().IsHomeComplete)
                            break;
                        if (!AXIS.Y.Status().IsMotionDone)
                            break;

                        UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER.Motor_Robot_Homing_Is_Completed, true);
                        IsDone_Init = true;
                        InterfaceRunClear();
                        FinishMainStep();
                        //NextMainStep(eMODULE_SEQ_INIT_SCANNER.Motor_Home_RA_RB_Start);
                        break;

                }

                MainSeqCheckTimeout(FA.DEF.Timeout_Init, FA.DEF.Error_Init_Scanner + m_stRun.nMainStep);
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }

        }
        #endregion
        public eMODULE_SEQ_PROC SubSeqStep
        {
            get { return CastTo<eMODULE_SEQ_PROC>.From(m_stRun.nSubStep); }
        }
        public int SubSeq_RecipeProcessStep
        {
            get { return DEF.Error_Run_RecipeProcess + m_stRun.nSubStep; }
        }
        public bool SubSeq_RecipeProcess(bool a_bPosInsp, bool a_bMarking, bool a_bMarkingInsp, bool a_bAuto = false)
        {
            try
            {
                if (!base.SetRecoveryRunInfo())
                    return false;

                switch (CastTo<eMODULE_SEQ_PROC>.From(m_stRun.nSubStep))
                {
                    case eMODULE_SEQ_PROC.Finish:
                        return true;

                    #region [ Recipe Check ]
                    case eMODULE_SEQ_PROC.RECIPE_CHK_START:
                        {

                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.RECIPE_VAILD_CHECK, false);
#if SIM
                            m_stRun.stwatchForSub.SetDelay = 100;
#endif

                            if (string.IsNullOrWhiteSpace(FA.MGR.RecipeMgr.SelectedModel))
                            {
                                FA.MGR.RunMgr.ModeChange(
                                        eRunMode.Jam,
                                        SubSeq_RecipeProcessStep, eManualMode.None,
                                        "Recipe Not Selected"
                                        );
                                break;
                            }
                            if (RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() <= 0)
                            {
                                FA.MGR.RunMgr.ModeChange(
                                        eRunMode.Jam,
                                        SubSeq_RecipeProcessStep, eManualMode.None,
                                        string.Format("{0} = 0", RCP_Modify.COMMON_PRODUCT_ROW_COUNT.strCaption)
                                        );

                                break;
                            }
                            if (RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>() <= 0)
                            {
                                FA.MGR.RunMgr.ModeChange(
                                        eRunMode.Jam,
                                        SubSeq_RecipeProcessStep, eManualMode.None,
                                        string.Format("{0} = 0", RCP_Modify.COMMON_PRODUCT_COL_COUNT.strCaption)
                                        );
                                break;
                            }
                            if (RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>() <= 0)
                            {
                                FA.MGR.RunMgr.ModeChange(
                                    eRunMode.Jam,
                                    SubSeq_RecipeProcessStep, eManualMode.None,
                                    string.Format("{0} <= 0", RCP_Modify.COMMON_PRODUCT_ROW_PITCH.strCaption)
                                    );
                                break;
                            }
                            if (RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>() <= 0)
                            {
                                FA.MGR.RunMgr.ModeChange(
                                    eRunMode.Jam,
                                    SubSeq_RecipeProcessStep, eManualMode.None,
                                    string.Format("{0} <= 0", RCP_Modify.COMMON_PRODUCT_COL_PITCH.strCaption)
                                    );
                                break;
                            }
                            m_pMultiArrayCheckRet = FA.MGR.RecipeRunningData.CheckMultiArrayVaildation();
                            if (m_pMultiArrayCheckRet.Item1 == false)
                            {
                                FA.MGR.RunMgr.ModeChange(
                                eRunMode.Jam,
                                SubSeq_RecipeProcessStep, eManualMode.None,
                                string.Format($"Multi Array Recipe is wrong Data Check Recipe Param{Environment.NewLine}{Environment.NewLine}{m_pMultiArrayCheckRet.Item2}"));
                                break;
                            }
                            if (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>() > 1)
                            {
                                double fMultiArrayGap = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                                int iMultiArrayCount = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>();
                                int iColCount = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                                double fColPitch = RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>();
                                int iRowCount = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                                double fRowPitch = RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>();


                                switch (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>())
                                {
                                    case eRecieMultiArrayDir.ROW:
                                        {
                                            if ((iColCount * fColPitch * iMultiArrayCount + fMultiArrayGap * (iMultiArrayCount - 1)) < 0)
                                            {
                                                FA.MGR.RunMgr.ModeChange(
                                                eRunMode.Jam,
                                                SubSeq_RecipeProcessStep, eManualMode.None,
                                                string.Format("Multi Array Recipe is wrong Data Check Recipe Param\n Multi array X Stroke < 0"));
                                                break;
                                            }
                                        }
                                        break;
                                    case eRecieMultiArrayDir.COLUMN:
                                        {
                                            if ((iRowCount * fRowPitch * iMultiArrayCount + fMultiArrayGap * (iMultiArrayCount - 1)) < 0)
                                            {
                                                FA.MGR.RunMgr.ModeChange(
                                                eRunMode.Jam,
                                                SubSeq_RecipeProcessStep, eManualMode.None,
                                                string.Format("Multi Array Recipe is wrong Data Check Recipe Param\n Multi array Y Stroke < 0 "));
                                                break;
                                            }
                                        }
                                        break;
                                }


                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", m_stRun.nSubStep), "Recipe Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.RECIPE_CHK_FINISH:
                        {
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#endif
                            m_iSubSeqRowMovingMax = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                            m_iSubSeqRowMovingMax = RCP_Modify.Inspection_RowCount.GetValue<int>() <= 0 ?
                                0 :
                               (int)(Math.Ceiling((double)m_iSubSeqRowMovingMax / RCP_Modify.Inspection_RowCount.GetValue<int>()) - 1);
                            m_iSubSeqColMovingMax = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                            m_iSubSeqColMovingMax = RCP_Modify.Inspection_ColCount.GetValue<int>() <= 0 ?
                                0 :
                                (int)(Math.Ceiling((double)m_iSubSeqColMovingMax / RCP_Modify.Inspection_ColCount.GetValue<int>()) - 1);

                            m_iSubSeqInspRowLastCount = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() % RCP_Modify.Inspection_RowCount.GetValue<int>();
                            m_iSubSeqInspRowLastCount = m_iSubSeqInspRowLastCount == 0 ? RCP_Modify.Inspection_RowCount.GetValue<int>() : m_iSubSeqInspRowLastCount;
                            m_iSubSeqMultiArrayMaxCount = RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>();
                            m_iSubSeqProductDoneMaxCount = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                            m_iSubSeqProductDoneMaxCount *= RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();

                            m_iSubSeqRowMovingExeCount = 0;
                            m_iSubSeqColMovingExeCount = 0;
                            m_iSubSeqMultiArrayExeCount = 0;
                            m_iSubSeqProductDoneCount = 0;
                            m_bPreSubSeqMovingLastPerOneLine = false;
                           
                            m_bSusbSeqRowReverseDirEnable = false;
                            
                            m_iSubSeqProductDoneRowProgressCount = 0;
                            m_iSubSeqProductDoneColProgressCount = 0;
                            m_iSubSeqInspRowCount = RCP_Modify.Inspection_RowCount.GetValue<int>(); ;
                            m_iSubSeqInspColCount = RCP_Modify.Inspection_ColCount.GetValue<int>(); ;
                            FA.MGR.RecipeRunningData.bCurrentInProess = true;
                            //m_bSusbSeqColReverseDirEnable=false;
                            if (m_iSubSeqRowMovingMax < 0)
                                m_iSubSeqRowMovingMax = 0;
                            if (m_iSubSeqColMovingMax < 0)
                                m_iSubSeqColMovingMax = 0;
                            m_bProcessEndChecked = false;


                            FA.MGR.RecipeRunningData.pCurrentProcessData.ClearProcessDataList();
                            FA.MGR.RecipeRunningData.pCurrentProcessData.ClearCodeData();
                            FA.MGR.RecipeRunningData.pCurrentProcessData.iZeroPadCount = RCP_Modify.PROCESS_DATA_MAT_MARKING_NUM_COUNT.GetValue<int>();
#if SIM
                            //DKT (BH FLEX) 지정 Test DB 시 
                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode = "SPS00004MG30307";
#else
                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode =
                            FA.MGR.RecipeRunningData.strLotCardCode;
#endif


                            m_SubSeqpProcessStartTime = DateTime.Now;
                            m_iSubSeqProductStartNo = FA.MGR.RecipeRunningData.lProductStartIDX;
                            FA.MGR.RecipeRunningData.lProductProcessDoneCount = 0;
                            VISION.FINE_LIB.ClearAllMatchResults();
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.RECIPE_VAILD_CHECK, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Recipe Check End");
                            NextSubStep(eMODULE_SEQ_PROC.JIG_EXIST_CHK_Start);
                        }
                        break;
                    #endregion [ Recipe Check ]
                    #region [ Interlock Check  & Run End Check ]
                    case eMODULE_SEQ_PROC.JIG_EXIST_CHK_Start:
                        {
                            if (base.IsRunModeStopped()) break;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JIG_EXIST, false);
#if SIM
                            m_stRun.stwatchForSub.SetDelay = 1000;
#else
                            if (eDI.JIG_FEEDER_JIG_EXIST.GetDI().Value == false)
                            {
                                m_stRun.TimeoutNow();
                                break;
                            }
                            if (ACT.CYL_JIG_FEEDER_L_CLAMP.CurrentStatus() == false || ACT.CYL_JIG_FEEDER_R_CLAMP.CurrentStatus() == false)
                            {
                                break;
                            }
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIG Exist Check Start");
                            NextSubStep();
                        }
                        break;

                    case eMODULE_SEQ_PROC.JIG_EXIST_CHK_Finish:
                        {
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#endif
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JIG_EXIST, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIG Exist Check End");
                            NextSubStep(eMODULE_SEQ_PROC.IO_Check_Start);
                        }
                        break;
                    case eMODULE_SEQ_PROC.IO_Check_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.IO_CHECK, false);
                            //if (eDI.JIG_POS_DETECTED_M.GetDI().Value == true)
                            //		break;
                            //if (eDI.JIG_POS_DETECTED_R.GetDI().Value == true)
                            //		break;
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "IO Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.IO_Check_Finish:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.IO_CHECK, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "IO Check End");
                            NextSubStep(eMODULE_SEQ_PROC.Cylinder_chk_PreMoving_Start);
                        }
                        break;
                    case eMODULE_SEQ_PROC.Cylinder_chk_PreMoving_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.Cylinder_check_PreMoveing, false);
#if SIM
                            m_stRun.stwatchForSub.SetDelay = 200;
#else
                            if (ACT.CYL_STOPPER_CENTER_DOWN.CurrentStatus() == false)
                            {
                                ACT.CYL_STOPPER_CENTER_DOWN.Run();
                            }
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Cylinder Check premoving Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.Cylinder_chk_PreMoving_Finish:
                        {
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#else
                            if (!ACT.CYL_STOPPER_CENTER_DOWN.Check()) break;
#endif
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.Cylinder_check_PreMoveing, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Cylinder Check premoving End");
                            if (m_iSubSeqProductDoneCount < m_iSubSeqProductDoneMaxCount)
                            {
                                if (FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo == null)
                                {
                                    NextSubStep(eMODULE_SEQ_PROC.MOVE_JIG_CODE_POS_START);
                                }
                                else
                                {
                                    NextSubStep(eMODULE_SEQ_PROC.MOVE_PROC_POS_Start);
                                }
                            }
                            else
                            {
                                if ((m_iSubSeqMultiArrayExeCount + 1) < m_iSubSeqMultiArrayMaxCount)
                                {
                                    m_iSubSeqMultiArrayExeCount++;
                                    m_iSubSeqRowMovingExeCount = 0;
                                    m_iSubSeqColMovingExeCount = 0;
                                    m_iSubSeqProductDoneCount = 0;
                                    m_iSubSeqProductDoneRowProgressCount = 0;
                                    m_iSubSeqProductDoneColProgressCount = 0;
                                    m_bProcessEndChecked = false;
                                    m_bSusbSeqRowReverseDirEnable = false;
                                    m_bPreSubSeqMovingLastPerOneLine = false;
                                    
                                    NextSubStep(eMODULE_SEQ_PROC.MOVE_PROC_POS_Start);
                                }
                                else
                                {
                                    NextSubStep(eMODULE_SEQ_PROC.PROC_END_Start);
                                }
                            }
                        }
                        break;
                    #endregion [ Interlock Check  & Run End Check ]
                    #region For MES ( JIG CODE [ Data Matrix] -> MES Comm -> Marking Process Start
                    case eMODULE_SEQ_PROC.MOVE_JIG_CODE_POS_START:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
                            if (!AXIS.RX.Status().IsMotionDone)
                                break;
                            if (!AXIS.RX.Status().IsInposition)
                                break;
                            if (!AXIS.Y.Status().IsMotionDone)
                                break;
                            if (!AXIS.Y.Status().IsInposition)
                                break;
                            if (!AXIS.RZ.Status().IsMotionDone)
                                break;
                            if (!AXIS.RZ.Status().IsInposition)
                                break;
                            m_fJIGCodePosX = RCP_Modify.COMMON_JIG_CODE_X_POS.GetValue<double>();
                            m_fJIGCodePosY = RCP_Modify.COMMON_JIG_CODE_Y_POS.GetValue<double>();
                            m_fJIGCodePosZ = RCP_Modify.COMMON_JIG_CODE_Z_POS.GetValue<double>();


                            if (AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(m_fJIGCodePosX, 0.01) &&
                                AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(m_fJIGCodePosY, 0.01) &&
                                AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(m_fJIGCodePosZ, 0.005)
                               )
                            {
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JigCode Teaching Pos Moving(inposition) Skip");
                                NextSubStep(eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_Check);
                            }
                            else
                            {

                                if (!AXIS.RX.Move_Absolute(m_fJIGCodePosX, Motion.GDMotion.eSpeedType.RUN)) break;
                                if (!AXIS.Y.Move_Absolute(m_fJIGCodePosY, Motion.GDMotion.eSpeedType.RUN)) break;
                                if (!AXIS.RZ.Move_Absolute(m_fJIGCodePosZ, Motion.GDMotion.eSpeedType.RUN)) break;
                                m_stRun.stwatchForSub.SetDelay = 10;
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JigCode Teaching Pos Moving Start");
                                NextSubStep();
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MOVE_JIG_CODE_POS_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
                            if (!AXIS.RX.Status().IsMotionDone)
                                break;
                            if (!AXIS.RX.Status().IsInposition)
                                break;
                            if (!AXIS.Y.Status().IsMotionDone)
                                break;
                            if (!AXIS.Y.Status().IsInposition)
                                break;
                            if (!AXIS.RZ.Status().IsMotionDone)
                                break;
                            if (!AXIS.RZ.Status().IsInposition)
                                break;
                            m_stRun.stwatchForSub.SetDelay = 100;
                            NextSubStep(eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_Check);
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_Check:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_CAM_CHECK:
                        {
                            VISION.FINE_LIB.ClearMatrixCode1Results();
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_CAM_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else                    
#endif
                            NextSubStep();
                        }


                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_LIGHT_CHECK:
                        {

#if SIM
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Light Check Start");
                            NextSubStep();
#else
                            if (
                                RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>() == LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.LEFT) &&
                                RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>() == LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.RIGHT) &&
                                RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>() == LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.UP) &&
                                RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>() == LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.BOTTOM)
                                )
                            {
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Light Skip(Same Value)");
                                NextSubStep(eMODULE_SEQ_PROC.JIG_CODE_INSP_GRAB_START);
                            }
                            else
                            {
                                LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.LEFT, RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
                                LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.RIGHT, RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
                                LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.UP, RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
                                LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.BOTTOM, RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Light Check Start");
                                NextSubStep();
                            }

#endif
                        }


                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_Condition_LIGHT_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.LEFT) != RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.RIGHT) != RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.UP) != RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.BOTTOM) != RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                                break;
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Light Check End");
                            NextSubStep();
                        }

                        break;

                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_GRAB_START:
                        {
#if SIM
#else
                            if (!FA.VISION.FINE_CAM.Grab())
                                break;

#endif
                            m_stRun.stwatchForSub.SetDelay = 20;
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Grab Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_GRAB_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
#if SIM
                            Bitmap pJIGCodeBmp = new Bitmap(string.Format("{0}Vision\\JIGCode.bmp", FA.DIR.CFG, m_iSubSeqRowMovingExeCount, m_iSubSeqColMovingExeCount));
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW = pJIGCodeBmp.Width;
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH = pJIGCodeBmp.Height;
                            VISION.FINE_LIB.SetBitmapToEImageBW8((Bitmap)pJIGCodeBmp.Clone());
                            FA.FRM.FrmVision.InvokeIfNeeded(() =>
                            {
                                FA.FRM.FrmVision.UpdateBW8SIM_IMG();
                            });

#else
                            if (!FA.VISION.FINE_CAM.IsGrab())
                            {
                                break;
                            }
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Grab End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_RUN_START:
                        {
                            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(300);
                            strDM_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\JIG_INSP\\{1}_{2}\\",
                                        m_SubSeqpProcessStartTime.ToString("yyyyMMdd"),
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            DateTime.Now.ToString("yyyyMMddHHmmss.ffff")
                                            );
                            if (VISION.FINE_LIB.MatrixCode1MultiRun(
                                    (int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                                    new Rectangle[]
                                    {
                                                            new Rectangle(RCP_Modify.JIGCode_ROI_POS_X.GetValue<int>(),
                                                                                        RCP_Modify.JIGCode_ROI_POS_Y.GetValue<int>(),
                                                                                        RCP_Modify.JIGCode_ROI_POS_WIDTH.GetValue<int>(),
                                                                                        RCP_Modify.JIGCode_ROI_POS_HEIGHT.GetValue<int>()
                                                                               )
                                    },
                                    2.0f, true,
                                    strDM_NG_SavePath) >= 0)
                            {
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "JIGCode Insp Start");
                                NextSubStep();
                            }
                            else
                            {
                                //Alarm
                                m_stRun.TimeoutNow();
                                break;
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.JIG_CODE_INSP_RUN_FINISH:
                        {
                            if (VISION.FINE_LIB.GetMatrixCode1TotalResultCount() == 1)
                            {
                                VISION.FINE_LIB.GetMatrixCode1ResultList(out m_pJIGCodeMatrixCodeResultList);
                                if (m_pJIGCodeMatrixCodeResultList[0].m_bFound)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode = m_pJIGCodeMatrixCodeResultList[0].m_strDecodedString;
                                    FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), string.Format("JIGCode({0}) Insp Start", FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode));
                                    NextSubStep(eMODULE_SEQ_PROC.MES_MARKING_START_INFO_START);
                                }
                                else
                                {
                                    // Alarm
                                    m_stRun.TimeoutNow();
                                    break;
                                }
                            }
                            else
                            {
                                //Alarm
                                m_stRun.TimeoutNow();
                                break;
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MES_MARKING_START_INFO_START:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;

                            if (FA.MGR.MESMgr.IsConnected == false)
                            {
                                FA.MGR.MESMgr.DoConnect();
                                m_stRun.stwatchForSub.SetDelay = 5;
                                break;
                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MES Connect Check OK before MarkingStart");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MES_MARKING_START_PACKET_SEND:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;

                            DKT_MES_MarkingStartInfo pRet = null;
                            if (FA.MGR.MESMgr.SendMarkingStart(
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                            out pRet
                                            ))
                            {
                                FA.MGR.RecipeRunningData.pCurrentProcessData.SetMarkingStartInfoData(pRet);
                                if (OPT.MES_TestModeEnable.m_bState == true)
                                {
                                    m_iSubSeqProductStartNo = m_iSubSEQMES_TestMode_ProductDoneCount + 1;
                                }
                                else
                                {
                                    m_iSubSeqProductStartNo = FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_StartNo;
                                }
                                FA.MGR.RecipeRunningData.lProductStartIDX = (long)m_iSubSeqProductStartNo;

                                FA.MGR.RecipeRunningData.pCurrentProcessData.MES_MarkingStartSendCompleteTime = DateTime.Now;
                                FA.MGR.RecipeRunningData.pCurrentProcessData.bMES_MarkingStartSuccess = true;

                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep),
                                string.Format("MES Marking Start Send {0}_{1}",
                                FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode
                                ));
                                FA.LOG.ProcessEvent("Scanner", string.Format("Marking Start {0}_{1}->{2}_{3}_ {4}",
                                                FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                                FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                                FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                                FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_StartNo,
                                                FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_EndNo
                                                ));
                                //NextSubStep(eMODULE_SEQ_PROC.MOVE_PROC_POS_Start);
                                NextSubStep();
                            }
                            else
                            {
                                ///Alarm 
                                FA.MGR.RunMgr.ModeChange(
                                                eRunMode.Jam,
                                                SubSeq_RecipeProcessStep, eManualMode.None,
                                                string.Format("{0}_{1}\nMES MSG: {2}",
                                                 FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                                 FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                                pRet == null ? "" : pRet.strCommMSG)
                                                );
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MES_MARKING_START_CONDITION_CHECK:
                        {
                            if(FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.bMarkingInfo_ZeroPadSame&&
                               FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.bMarkingInfo_ZeroPadLengthOK
                                )
                            {
                                if ((FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode.Length +
                                 FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_ZeroPad)
                                 > FA.MGR.DMGenertorMgr.GetAlphabetMaxCapacity(RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>())
                                    )
                                {
                                    FA.MGR.RunMgr.ModeChange(eRunMode.Jam,
                                    SubSeq_RecipeProcessStep, eManualMode.None,
                                    string.Format("{0}_{1}\nMES Code: {2}({3})\nMarking Number Digit:{4}\nTotal:{5}(Max:{6})",
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode.Length,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_ZeroPad,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode.Length + FA.MGR.RecipeRunningData.pCurrentProcessData.iZeroPadCount,
                                    FA.MGR.DMGenertorMgr.GetAlphabetMaxCapacity(RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>())
                                    )
                                    );
                                }
                                else
                                {
                                    NextSubStep();
                                }
                            }
                            else
                            {
                                FA.MGR.RunMgr.ModeChange(eRunMode.Jam,
                               SubSeq_RecipeProcessStep, eManualMode.None,
                               $"{FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode}{FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode}\n"+
                               $"MES Code:{FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode}[{FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode.Length}]\n"+
                               $"Total Code Length[MAX]:{FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode.Length + FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_ZeroPad}[{FA.MGR.DMGenertorMgr.GetAlphabetMaxCapacity(RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>())}]\n" +
                               $"Marking Number Digit:{FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_ZeroPad}\n" +
                               $"Marking Number StartNo[Length]:{FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_StartNo}[{FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_StartNoLength}]\n" +
                               $"Marking Number EndNo[Length]:{FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_EndNo}[{FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_EndNoLength}]");                               
                            }                            
                        }
                        break;
                    case eMODULE_SEQ_PROC.MES_MARKING_START_INFO_FINISH:
                        {
                            NextSubStep(eMODULE_SEQ_PROC.MOVE_PROC_POS_Start);
                        }
                        break;
                    #endregion
                    #region [ Target Area Move ]
                    case eMODULE_SEQ_PROC.MOVE_PROC_POS_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MOVE_PROC_POS, false);
                            m_fSubSeqTargetPosX = RCP_Modify.COMMON_INIT_PROC_AREA_X_POS.GetValue<double>();
                            m_fSubSeqTargetPosY = RCP_Modify.COMMON_INIT_PROC_AREA_Y_POS.GetValue<double>();
                            m_fSubSeqTargetPosZ = RCP_Modify.COMMON_INIT_PROC_AREA_Z_POS.GetValue<double>();

                            switch (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>())
                            {
                                case eRecieMultiArrayDir.ROW:
                                    m_fSubSeqTargetPosX += m_iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                                    break;
                                case eRecieMultiArrayDir.COLUMN:
                                    m_fSubSeqTargetPosY += m_iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                                    break;
                            }
                            m_fSubSeqRowMovingPitch = RCP_Modify.Inspection_RowCount.GetValue<int>();
                            m_fSubSeqRowMovingPitch *= RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>();
                            m_fSubSeqRowMovingPitch *= m_iSubSeqRowMovingExeCount;

                            m_fSubSeqColMovingPitch = RCP_Modify.Inspection_ColCount.GetValue<int>();
                            m_fSubSeqColMovingPitch *= RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>();
                            m_fSubSeqColMovingPitch *= m_iSubSeqColMovingExeCount;
                            switch (RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>())
                            {
                                case eRecipeRowProgressDir.UP:
                                    m_fSubSeqTargetPosY += m_fSubSeqRowMovingPitch;
                                    break;
                                case eRecipeRowProgressDir.DOWN:
                                    m_fSubSeqTargetPosY -= m_fSubSeqRowMovingPitch;
                                    break;
                            }
                            switch (RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>())
                            {
                                case eRecipeColProgressDir.LEFT:
                                    m_fSubSeqTargetPosX += m_fSubSeqColMovingPitch;
                                    break;
                                case eRecipeColProgressDir.RIGHT:
                                    m_fSubSeqTargetPosX -= m_fSubSeqColMovingPitch;
                                    break;
                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Move PROC Pos PreCheck Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MOVE_PROC_POS_Precheck:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
                            if (!AXIS.RX.Status().IsMotionDone)
                                break;
                            if (!AXIS.RX.Status().IsInposition)
                                break;
                            if (!AXIS.Y.Status().IsMotionDone)
                                break;
                            if (!AXIS.Y.Status().IsInposition)
                                break;
                            if (!AXIS.RZ.Status().IsMotionDone)
                                break;
                            if (!AXIS.RZ.Status().IsInposition)
                                break;

                            if (AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(m_fSubSeqTargetPosX, 0.01)
                                    && AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(m_fSubSeqTargetPosY, 0.01)
                                    && AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(m_fSubSeqTargetPosZ, 0.01)
                                            )
                            {
                                if (a_bPosInsp)
                                {

                                    m_stRun.stwatchForSub.SetDelay = 10;
                                    NextSubStep(eMODULE_SEQ_PROC.MOVE_PROC_POS_Finish);
                                }
                                else
                                {
                                    NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                                }
                            }
                            else
                            {
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Move PROC Pos Check Start");
                                NextSubStep();
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MOVE_PROC_POS_Action:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MOVE_PROC_POS, false);
                            if (!AXIS.RX.Move_Absolute(m_fSubSeqTargetPosX, Motion.GDMotion.eSpeedType.RUN)) break;
                            if (!AXIS.Y.Move_Absolute(m_fSubSeqTargetPosY, Motion.GDMotion.eSpeedType.RUN)) break;
                            if (!AXIS.RZ.Move_Absolute(m_fSubSeqTargetPosZ, Motion.GDMotion.eSpeedType.RUN)) break;

                            m_stRun.stwatchForSub.SetDelay = 10;
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Move PROC Pos Action Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MOVE_PROC_POS_Finish:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
                            if (!AXIS.RX.Status().IsMotionDone)
                                break;
                            if (!AXIS.RX.Status().IsInposition)
                                break;
                            if (!AXIS.Y.Status().IsMotionDone)
                                break;
                            if (!AXIS.Y.Status().IsInposition)
                                break;
                            if (!AXIS.RZ.Status().IsMotionDone)
                                break;
                            if (!AXIS.RZ.Status().IsInposition)
                                break;


                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MOVE_PROC_POS, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Move PROC Pos Action End");
                            if (a_bPosInsp)
                            {
                                int iForInitValue1 =
                                (m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount) +
                                RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                                int iForLoopCount = bSubSeqMovingLastPerOneLine == false ?
                                 m_iSubSeqInspRowCount :
                                 m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;

                                for (int i = iForInitValue1; i < iForInitValue1 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bInProcess = true;
                                }
                                m_stRun.stwatchForSub.SetDelay = 100;
                                NextSubStep(eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_Check);
                            }
                            else
                            {
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                        }
                        break;
                    #endregion [ Target Area Move ]
                    #region [ Find Marking Target ]
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_Check:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;

                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_CAM_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Cam Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_CAM_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
#if SIM
#else

#endif
                            //VISION.FINE_LIB.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH, false, (k, v) => false);
                            VISION.FINE_LIB.ClearMatchResult(RCP_Modify.MATCH_IMG_IDX.GetValue<EzInaVision.GDV.eGoldenImages>(), RCP_Modify.MATCH_FIND_ROI_IDX.GetValue<EzInaVision.GDV.eRoiItems>());
                            VISION.FINE_LIB.ClearMatrixCode1Results();

                            //FA.FRM.FrmVision.InvokeIfNeeded(() =>
                            //{
                            //		FA.FRM.FrmVision.UpdateVision();
                            //});
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Cam Check End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_LIGHT_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
#if SIM
                            m_stRun.stwatchForSub.SetDelay = 50;
#else
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.LEFT, RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.RIGHT, RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.UP, RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.BOTTOM, RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());
                            m_stRun.stwatchForSub.SetDelay = 10;
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Light Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_LIGHT_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.LEFT) != RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.RIGHT) != RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.UP) != RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.BOTTOM) != RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                                break;
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Light Check End");
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_LIGHT_CHECK, true);
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_PARAM_CHECK:
                        {
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#else
#endif
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Match Param Check Start");
                            EzInaVision.GDV.MatcherConfig MatchConfig = FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_MatchConfig;
                            if (MatchConfig == null)
                                MatchConfig = new EzInaVision.GDV.MatcherConfig();
                            MatchConfig.m_fMinScale = RCP_Modify.Matcher_Minimum_Scale.GetValue<double>() * 100;
                            MatchConfig.m_fMaxScale = RCP_Modify.Matcher_Maximum_Scale.GetValue<double>() * 100;
                            MatchConfig.m_fScore = RCP_Modify.Matcher_Match_Score.GetValue<double>() * 100; ;
                            MatchConfig.m_fAngle = RCP_Modify.Matcher_Match_Angle.GetValue<double>();
                            MatchConfig.m_fMaxPosition = RCP_Modify.Matcher_Match_MaxCount.GetValue<int>();
                            MatchConfig.m_iCorrelationMode = RCP_Modify.Matcher_Match_CorrelationMode.GetValue<int>();
                            MatchConfig.m_iMatchContrastMode = RCP_Modify.Matcher_Match_ContrastMode.GetValue<int>();
                            m_pProcessDataList.Clear();

                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_Condition_PARAM_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_Condition_Check, true);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_PARAM_CHECK, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Match Param Check End");
                            NextSubStep(eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_GRAB_CHECK);
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_GRAB_CHECK:
                        {
                            //	if (base.IsRunModeStopped()) break;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Grab Check Start");
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
                            Bitmap pBmp = new Bitmap(string.Format("{0}Vision\\PosInsp_{1}_{2}.bmp", FA.DIR.CFG, m_iSubSeqRowMovingExeCount, m_iSubSeqColMovingExeCount));
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW = pBmp.Width;
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH = pBmp.Height;
                            VISION.FINE_LIB.SetBitmapToEImageBW8((Bitmap)pBmp.Clone());
                            FA.FRM.FrmVision.InvokeIfNeeded(() =>
                            {
                                FA.FRM.FrmVision.UpdateBW8SIM_IMG();
                            });
                            m_stRun.stwatchForSub.SetDelay = 50;

#else
                            if (!FA.VISION.FINE_CAM.Grab())
                                break;
#endif
                            m_stRun.stwatchForSub.SetDelay = 20;
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_GRAB_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, false);
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM

#else
                            if (!FA.VISION.FINE_CAM.IsGrab())
                            {
                                break;
                            }
                            //FA.VISION.FINE_CAM.SaveImage(string.Format("D:\\PosInsp_{0}_{1}_{2}.bmp",m_iSubSeqRowMovingExeCount,m_iSubSeqColMovingExeCount,DateTime.Now.ToString("yyyyhhmmss.ffff")));
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Grab Check End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_START:
                        {
                            int iMatchRunStartProductCnt =
                            RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                            RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, false);
                            strPOSINSP_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\{1}_{2}_{3}\\POS_INSP\\",
                                            m_SubSeqpProcessStartTime.ToString("yyyyMMdd"),
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.MES_MarkingStartSendCompleteTime.ToString("yyyyMMddHHmmss.ffff")
                                            );
                            int iForInitValue2 =
                               m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                               RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                               RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                            int iForLoopCount = bSubSeqMovingLastPerOneLine == false ?
                                m_iSubSeqInspRowCount :
                                m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;

                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Condition  Match Run  Start");
                            if (FA.VISION.FINE_LIB.MatchRun(
                                     FA.MGR.ProjectMgr.SelectedModelPath,
                                     RCP_Modify.MATCH_IMG_IDX.GetValue<EzInaVision.GDV.eGoldenImages>(),
                                     RCP_Modify.MATCH_FIND_ROI_IDX.GetValue<EzInaVision.GDV.eRoiItems>(),
                                     true,
                                     strPOSINSP_NG_SavePath,
                                     string.Format("{0}~{1}",
                                     iMatchRunStartProductCnt + 1,
                                     iMatchRunStartProductCnt + m_iSubSeqInspRowCount)
                                 ) > 0)
                            {

                                for (int i = iForInitValue2; i < iForInitValue2 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bPosInspExecuted = true;
                                }
                                NextSubStep();
                            }
                            else
                            {

                                for (int i = iForInitValue2; i < iForInitValue2 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bPosInspExecuted = true;
                                }
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, true);
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_RESULT_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, false);
                            if (FA.VISION.FINE_LIB.GetMatchResultCount(
                                            RCP_Modify.MATCH_IMG_IDX.GetValue<EzInaVision.GDV.eGoldenImages>(),
                                            RCP_Modify.MATCH_FIND_ROI_IDX.GetValue<EzInaVision.GDV.eRoiItems>()
                                            ) > 0)
                            {

#if SIM
                                //	FA.FRM.FrmVision.InvokeIfNeeded(() =>
                                //	{
                                //	FA.FRM.FrmVision.UpdateVision();
                                //	});

#endif
                                List<EzInaVision.GDV.MatchResult> pResultList;
                                FA.VISION.FINE_LIB.MatchResult(
                                                RCP_Modify.MATCH_IMG_IDX.GetValue<EzInaVision.GDV.eGoldenImages>(),
                                                RCP_Modify.MATCH_FIND_ROI_IDX.GetValue<EzInaVision.GDV.eRoiItems>(),
                                                out pResultList
                                                );
                                if (pResultList != null && pResultList.Count > 0)
                                {
                                    m_pMatchResultList = null;
                                    m_pMatchResultList = pResultList.OrderBy(item => item.m_fSensorYPos).ToList();
                                    int iCurrentIDX =
                                             m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                                             RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                             RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                                    for (int i = 0; i < m_pMatchResultList.Count; i++)
                                    {
                                        if (this.GetProcessIDX(out m_iSubSeqProductIDX,
                                        m_pMatchResultList[i].m_fSensorXPos,
                                        m_pMatchResultList[i].m_fSensorYPos,
                                        m_pMatchResultList[i].m_fMatchWidth,
                                        m_pMatchResultList[i].m_fMatchHeight
                                        ) == true)
                                        {
                                            if (m_iSubSeqProductIDX > -1)
                                            {                                             
                                                m_pProcessDataList.Add(FA.MGR.RecipeRunningData.pCurrentProcessData[iCurrentIDX + m_iSubSeqProductIDX]);
                                                m_pProcessDataList[i].pMatchResult = (EzInaVision.GDV.MatchResult)m_pMatchResultList[i].Clone();
                                                m_pProcessDataList[i].iInspOrderIDX = m_iSubSeqProductIDX;                                                
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Match Run  End");
                                    NextSubStep();
                                }
                                else
                                {
                                    int iForInitValue2 =
                                    m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                                    RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                    RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                                    int iForLoopCount = bSubSeqMovingLastPerOneLine == false ?
                                     m_iSubSeqInspRowCount :
                                     m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;
                                    for (int i = iForInitValue2; i < iForInitValue2 + iForLoopCount; i++)
                                    {
                                        FA.MGR.RecipeRunningData.pCurrentProcessData[i].bPosInspExecuted = true;
                                    }
                                    UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, true);
                                    FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Match Run End( No Object )");
                                    NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                                }
                            }
                            else
                            {
                                int iForInitValue2 =
                                    m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                                    RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                    RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                                int iForLoopCount = bSubSeqMovingLastPerOneLine == false ?
                                   m_iSubSeqInspRowCount :
                                   m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;
                                for (int i = iForInitValue2; i < iForInitValue2 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bPosInspExecuted = true;
                                }
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP_RUN, true);
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Pos Insp Match Run End( Inspection Fail )");
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                        }
                        break;

                    case eMODULE_SEQ_PROC.MARKING_POS_INSP_RUN_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_POS_INSP, true);
                            if (a_bMarking)
                            {
                                NextSubStep(eMODULE_SEQ_PROC.MARKING_PARAM_CHECK_START);
                            }
                            else
                            {
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                            //to be Continue Data Struct 	
                        }
                        break;

                    #endregion  [ Find Marking Target ]
                    #region [ Found Target Marking ]
                    case eMODULE_SEQ_PROC.MARKING_PARAM_CHECK_START:
                        {
                            //if (base.IsRunModeStopped()) break;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);

                            //	FA.LOG.Info("Scanner SEQ", "MARKING PARAM CHECK START ");
                            if (m_pMatchResultList == null || m_pMatchResultList.Count < 0)
                            {
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, true);
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                            else
                            {
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Laser PARAM CHECK START ");
                                NextSubStep();
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LASER_PARAM_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;

#if SIM
                            m_stRun.stwatchForSub.SetDelay = 200;
#else
                            if (FA.LASER.Instance.GateMode != Laser.GATE_MODE.EXT)
                            {
                                FA.LASER.Instance.GateMode = Laser.GATE_MODE.EXT;
                                m_stRun.stwatchForSub.SetDelay = 10;
                                break;
                            }
                            if (FA.LASER.Instance.TriggerMode != Laser.TRIG_MODE.EXT)
                            {
                                LASER.Instance.TriggerMode = Laser.TRIG_MODE.EXT;
                                m_stRun.stwatchForSub.SetDelay = 10;
                                break;
                            }
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Laser PARAM CHECK End ");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LASER_PARAM_FINSISH:
                        {
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
#else

#endif
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LASER_ENABLE_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LASER_ENABLE_CHECK, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Laser Enable CHECK Start ");
#if SIM
#else
#endif
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LASER_ENABLE_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;

#if SIM

#else
                            if (FA.OPT.DryRunningEnable.m_bState)
                            {
                                if (LASER.Instance.IsEmissionOn == true)
                                {
                                    eDO.LASER_EM_ENABLE.GetDO().Value = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (LASER.Instance.IsEmissionOn == false)
                                {
                                    eDO.LASER_EM_ENABLE.GetDO().Value = true;
                                    break;
                                }
                            }
#endif
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LASER_ENABLE_CHECK, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Laser Enable CHECK End ");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_PROCESS_PARAM_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_PROC_PARAM_CHECK, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Process Param CHECK Start ");
                            double fPercentTemp = 0.0;
                            Laser.LaserPwrTableData pPowerTable = null;
                            pPowerTable = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                            if(pPowerTable==null || pPowerTable.GetPercentFromPower(
                                    RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp) != 1)
                            {
                                m_stRun.TimeoutNow();
                                break;
                            }
                        

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
                            m_iRepaintExecuteListCount=RCP_Modify.PROCESS_DATA_MAT_REPEAT_MARKING_COUNT.GetValue<int>();
                            //(LASER.Instance as EzIna.Laser.IPG.GLPM).SetPoint = (float)fPercentTemp * 100.0f;
                            LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                            EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                                            RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                            m_pProcessHatchOption.Type =
                                            (EzCAM_Ver2.HATCH_TYPE)
                                            ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE.GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
                            m_pProcessHatchOption.fPitch = RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
                            m_pProcessHatchOption.fAngle = RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
                            m_pProcessHatchOption.fOffset = RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
                            m_pProcessHatchOption.bOutline = RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();

                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_PROCESS_PARAM_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_PROC_PARAM_CHECK, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Process Param CHECK End ");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_CREATE_DM_CHECK:
                        {
                            if (m_pProcessDataList.Count > 0)
                            {
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_CREATE_DM, false);
                                //FA.LOG.Info("Scanner SEQ", "MARKING Create DM Check Start ");
                                for (int i = 0; i < m_pProcessDataList.Count; i++)
                                {

                                    if (a_bAuto)
                                    {

                                        m_strSubSeqMarkingStingTemp = "";
                                        m_pProcessDataList[i].iMarkingIDX = m_iSubSeqProductStartNo + m_pProcessDataList[i].iIDX;
                                        m_pProcessDataList[i].iZeroPadCount = FA.MGR.RecipeRunningData.pCurrentProcessData.pMES_MarkingStartInfo.iMarkingInfo_ZeroPad;
                                        m_pProcessDataList[i].pDataMatrix = FA.MGR.DMGenertorMgr.CreateDataMatrix(
                                        string.Format("{0}{1}", FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode, m_pProcessDataList[i].strMarkingIDX));
                                      
                                        /*if (DKT_ConvertCode.Instance.ConvertSerialNumber_10To32(
                                                DKT_ConvertCode.eCodeType.Variable,
                                                (int)m_pProcessDataList[i].iMarkingIDX,
                                                5,
                                                out m_strSubSeqMarkingStingTemp) == true)
                                                {
                                                m_pProcessDataList[i].pDataMatrix = FA.MGR.DMGenertorMgr.CreateDataMatrix(
                                                string.Format("{0}{1}", FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode, m_strSubSeqMarkingStingTemp));
                                                }
                                                else
                                                {
                                                                //Alarm
                                                                m_stRun.TimeoutNow();
                                                                break;
                                                }*/
                                    }
                                    else
                                    {
                                        if (this.GetProcessIDX(out m_iSubSeqProductIDX,
                                         m_pProcessDataList[i].pMatchResult.m_fSensorXPos,
                                         m_pProcessDataList[i].pMatchResult.m_fSensorYPos,
                                         m_pProcessDataList[i].pMatchResult.m_fMatchWidth,
                                         m_pProcessDataList[i].pMatchResult.m_fMatchHeight
                                        ))
                                        {
                                            m_pProcessDataList[i].pDataMatrix = FA.MGR.DMGenertorMgr.CreateDataMatrix(
                                            string.Format("Test{0}", m_iSubSeqProductDoneMaxCount * m_iProcessedJigCount +
                                            RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                            RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount +
                                            m_iSubSeqProductIDX + 1)
                                            );
                                        }
                                    }
                                    m_pProcessDataList[i].pDataMatrix.HatchOption = m_pProcessHatchOption.Clone();
                                }
                                m_iSubSeqMarkingExeCount = 0;
                                m_iSubSeqMarkingExeMax = m_pProcessDataList.Count;
                                NextSubStep(eMODULE_SEQ_PROC.MARKING_LIST_START);
                            }
                            else
                            {
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, true);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_CREATE_DM, true);
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Finish);
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_CREATE_DM_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_CREATE_DM, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING Create DM Check End ");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LIST_START:
                        {
                            //if (base.IsRunModeStopped()) break;
                            if (m_pProcessDataList.Count > 0)
                            {
                                if (!FA.MGR.VisionToScannerCalb.IsLoad())
                                {
                                    m_stRun.TimeoutNow();
                                    break;
                                }
                                eDO.DEBRIS_SOL.GetDO().Value = true;
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, false);
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep),
                                        "MARKING List Start ");
                                if (m_pProcessDataList.Count > 0)
                                {
                                    if (m_pProcessDataList.Count > 1)
                                    {
                                        FA.LOG.InfoJIG("Marking Start {0}{1}~{2}",
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                        m_pProcessDataList[0].strMarkingIDX,
                                        m_pProcessDataList[m_pProcessDataList.Count - 1].strMarkingIDX
                                        );
                                    }
                                    else
                                    {
                                        FA.LOG.InfoJIG("Marking Start {0}{1}",
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                        m_pProcessDataList[0].strMarkingIDX
                                        );
                                    }
                                }
                                NextSubStep();
                            }
                            else
                            {
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, true);
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING List Empty ");
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LIST_LOOP_START:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING List Loop Start ");
#if SIM
#else
                            if (RTC5.Instance.IsExecuteList_BUSY)
                                break;
#endif
                            m_fMarkingCenterX = 0.0;
                            m_fMarkingCenterY = 0.0;
                            //m_pExecuteListIDX=Scanner.ScanlabRTC5.RTC_LIST._1st;
                            m_pExecuteListIDX = m_iSubSeqMarkingExeCount % 2 == 0 ?
                                    Scanner.ScanlabRTC5.RTC_LIST._1st : Scanner.ScanlabRTC5.RTC_LIST._2nd;

                           
                            if (m_pProcessDataList[m_iSubSeqMarkingExeCount].pDataMatrix != null &&
                             m_pProcessDataList[m_iSubSeqMarkingExeCount].pDataMatrix.bCreateCoordinate == false
                            )
                            {
                                Tuple<bool, string> pDMPathMakeRet = CreateRecipeDataMatrixPathToScanner(m_iSubSeqMarkingExeCount, m_pExecuteListIDX);
                                //m_pTestStopWatch.Restart();
                                if (pDMPathMakeRet.Item1 == false)
                                {
                                    FA.MGR.RunMgr.ModeChange(
                                             eRunMode.Jam,
                                             SubSeq_RecipeProcessStep, eManualMode.None,
                                             pDMPathMakeRet.Item2
                                             );                                    
                                    break;
                                }
                                //Trace.WriteLine(string.Format("{0} : {1}", m_pTestStopWatch.ElapsedMilliseconds / 1000.0, m_iSubSeqMarkingExeCount));

                            }

#if SIM
                            m_stRun.stwatchForSub.SetDelay = 100;
#else

#endif
                            m_iExecuteListEXECount=0;
                            RTC5.Instance.ListExecute(m_pExecuteListIDX);
                         
                            //m_stRun.stwatchForSub.SetDelay=2;
                            //Trace.WriteLine(string.Format("ScannerList Exe: {0}",m_pExecuteListIDX));
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LIST_LOOP_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, false);
                            if (!m_stRun.stwatchForSub.IsDone) break;
                            if (m_iSubSeqMarkingExeCount + 1 < m_iSubSeqMarkingExeMax)
                            {
                                if (m_pProcessDataList[m_iSubSeqMarkingExeCount + 1].pMatchResult != null &&
                                                                                                m_pProcessDataList[m_iSubSeqMarkingExeCount + 1].pDataMatrix.bCreateCoordinate == false)
                                {
                                    //m_pTestStopWatch.Restart();
                                    m_pSubExecuteListIDX = (m_iSubSeqMarkingExeCount) % 2 == 0 ?
                                    Scanner.ScanlabRTC5.RTC_LIST._2nd : Scanner.ScanlabRTC5.RTC_LIST._1st;
                                    Tuple<bool, string> pSecendDMPathMakeRet = CreateRecipeDataMatrixPathToScanner(m_iSubSeqMarkingExeCount + 1, m_pSubExecuteListIDX);
                                    if (pSecendDMPathMakeRet.Item1 == false)
                                    {
                                        FA.MGR.RunMgr.ModeChange(
                                            eRunMode.Jam,
                                            SubSeq_RecipeProcessStep, eManualMode.None,
                                            pSecendDMPathMakeRet.Item2
                                            );
                                        break;
                                    }
                                    //Trace.WriteLine(string.Format("{0} : {1}", m_pTestStopWatch.ElapsedMilliseconds / 1000.0, m_iSubSeqMarkingExeCount + 1));
                                    //Trace.WriteLine(string.Format("ScannerList Sub : {0}",m_pSubExecuteListIDX));
                                }
                            }

#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#else
                            if (RTC5.Instance.IsExecuteList_BUSY)
                                break;

                           


#endif
                            // For Repeat
                            // SUS 가공
                            if (m_iRepaintExecuteListCount > (m_iExecuteListEXECount+1))
                            {
                                m_iExecuteListEXECount++;
                                RTC5.Instance.ListExecute(m_pExecuteListIDX);
                                break;
                            }
                            // For Repeat
                            // SUS 가공
                            m_pProcessDataList[m_iSubSeqMarkingExeCount].bMarkingDone = true;
                            m_pProcessDataList[m_iSubSeqMarkingExeCount].MarkingDoneTime = DateTime.Now;
                            if (m_iSubSeqMarkingExeCount + 1 >= m_iSubSeqMarkingExeMax)
                            {
                                m_stRun.stwatchForSub.SetDelay = 100;
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, true);
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING List End ");
                                NextSubStep();
                            }
                            else
                            {
                                m_iSubSeqMarkingExeCount++;
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, false);
                                UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_RUN_LIST, false);
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "MARKING List Loop End ");
                                NextSubStep(eMODULE_SEQ_PROC.MARKING_LIST_LOOP_START);
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_LIST_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING, true);
                            if (!m_stRun.stwatchForSub.IsDone) break;

                            if (m_pProcessDataList.Count > 0)
                            {
                                if (m_pProcessDataList.Count > 1)
                                {
                                    FA.LOG.InfoJIG("Marking End {0}{1}~{2}",
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                    m_pProcessDataList[0].strMarkingIDX,
                                    m_pProcessDataList[m_pProcessDataList.Count - 1].strMarkingIDX
                                    );
                                }
                                else
                                {
                                    FA.LOG.InfoJIG("Marking End {0}{1}",
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                    m_pProcessDataList[0].strMarkingIDX
                                    );
                                }
                            }
                            if (a_bMarkingInsp)
                            {
                                NextSubStep(eMODULE_SEQ_PROC.MARKING_INSP_Condition_Check);
                            }
                            else
                            {
                                NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                            }
                        }
                        break;
                    #endregion  [ Found Target Marking ]
                    #region[ Target Marking Inspection ]
                    case eMODULE_SEQ_PROC.MARKING_INSP_Condition_Check:
                        {
                            //if (IsRunModeStopped()) break;
                            if (!m_stRun.stwatchForSub.IsDone) break;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Condition Check Start");
                            NextSubStep();
                        }

                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_Condition_CAM_CHECK:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, false);
                            VISION.FINE_LIB.ClearMatrixCode1Results();
#if SIM
                            m_stRun.stwatchForSub.SetDelay = 100;
#else
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Condition Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_Condition_CAM_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
                            if (!m_stRun.stwatchForSub.IsDone) break;
#else
#endif

                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Condition Check End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_Condition_LIGHT_CHECK:
                        {
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Light Check Start");
#if SIM
#else
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.LEFT, RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.RIGHT, RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.UP, RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
                            LIGHTSOURCE.BAR.SetIntensity((int)LIGHT_CH.BOTTOM, RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());
#endif
                            //m_stRun.stwatchForSub.SetDelay = 10;
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_Condition_LIGHT_FINISH:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.LEFT) != RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.RIGHT) != RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.UP) != RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>())
                                break;
                            if (LIGHTSOURCE.BAR.GetIntensity((int)LIGHT_CH.BOTTOM) != RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                                break;
#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Light Check End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_GRAB_START:
                        {
                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
#if SIM
#else

                            if (!FA.VISION.FINE_CAM.Grab())
                                break;

#endif
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Grab  Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_GRAB_FINISH:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, false);

                            if (!m_stRun.stwatchForSub.IsDone)
                                break;
#if SIM
                            Bitmap pMarkInspBmp = new Bitmap(string.Format("{0}Vision\\MatrixInsp_{1}_{2}.bmp",
                                    FA.DIR.CFG,
                                    m_iSubSeqRowMovingExeCount,
                                    m_iSubSeqColMovingExeCount));
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW = pMarkInspBmp.Width;
                            VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH = pMarkInspBmp.Height;
                            VISION.FINE_LIB.SetBitmapToEImageBW8((Bitmap)pMarkInspBmp.Clone());
                            FA.FRM.FrmVision.InvokeIfNeeded(() =>
                            {
                                FA.FRM.FrmVision.UpdateBW8SIM_IMG();
                            });

#else
                            if (!FA.VISION.FINE_CAM.IsGrab())
                            {

                                break;

                            }
#endif
                            int iInspRowCount = m_bSubSeqMovingLastPerOneLine_ForMES == false ?
                                m_iSubSeqInspRowCount :
                                m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;

                            if (m_iSubSeqProductDoneCount + (iInspRowCount * m_iSubSeqInspColCount)
                                < m_iSubSeqProductDoneMaxCount)
                            {


                                m_iSubSeqPreRowMovingExeCount = m_iSubSeqRowMovingExeCount;
                                m_iSubSeqPreColMovingExeCount = m_iSubSeqColMovingExeCount;
                                m_bPreSubSeqMovingLastPerOneLine = m_bSubSeqMovingLastPerOneLine_ForMES;                              
                                RecipeProcessEndCheck();
                                m_bProcessEndChecked = true;

                                m_fSubSeqTargetPosX = RCP_Modify.COMMON_INIT_PROC_AREA_X_POS.GetValue<double>();
                                m_fSubSeqTargetPosY = RCP_Modify.COMMON_INIT_PROC_AREA_Y_POS.GetValue<double>();
                                m_fSubSeqTargetPosZ = RCP_Modify.COMMON_INIT_PROC_AREA_Z_POS.GetValue<double>();

                                switch (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>())
                                {
                                    case eRecieMultiArrayDir.ROW:
                                        m_fSubSeqTargetPosX += m_iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                                        break;
                                    case eRecieMultiArrayDir.COLUMN:
                                        m_fSubSeqTargetPosY += m_iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                                        break;
                                }

                                m_fSubSeqRowMovingPitch = RCP_Modify.Inspection_RowCount.GetValue<int>();
                                m_fSubSeqRowMovingPitch *= RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>();
                                m_fSubSeqRowMovingPitch *= m_iSubSeqRowMovingExeCount;

                                m_fSubSeqColMovingPitch = RCP_Modify.Inspection_ColCount.GetValue<int>();
                                m_fSubSeqColMovingPitch *= RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>();
                                m_fSubSeqColMovingPitch *= m_iSubSeqColMovingExeCount;
                                switch (RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>())
                                {
                                    case eRecipeRowProgressDir.UP:
                                        m_fSubSeqTargetPosY += m_fSubSeqRowMovingPitch;
                                        break;
                                    case eRecipeRowProgressDir.DOWN:
                                        m_fSubSeqTargetPosY -= m_fSubSeqRowMovingPitch;
                                        break;
                                }
                                switch (RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>())
                                {
                                    case eRecipeColProgressDir.LEFT:
                                        m_fSubSeqTargetPosX += m_fSubSeqColMovingPitch;
                                        break;
                                    case eRecipeColProgressDir.RIGHT:
                                        m_fSubSeqTargetPosX -= m_fSubSeqColMovingPitch;
                                        break;
                                }

                                if (!AXIS.RX.Move_Absolute(m_fSubSeqTargetPosX, Motion.GDMotion.eSpeedType.RUN)) break;
                                if (!AXIS.Y.Move_Absolute(m_fSubSeqTargetPosY, Motion.GDMotion.eSpeedType.RUN)) break;
                                if (!AXIS.RZ.Move_Absolute(m_fSubSeqTargetPosZ, Motion.GDMotion.eSpeedType.RUN)) break;

                            }
                            //FA.VISION.FINE_CAM.SaveImage(string.Format("D:\\MatrixInsp_{0}_{1}_{2}.bmp",m_iSubSeqRowMovingExeCount,m_iSubSeqColMovingExeCount,DateTime.Now.ToString("yyyyhhmmss.ffff")));
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Grab End");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_RUN_START:
                        {
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Run Start");
                            m_pDataMatrixInspRectList.Clear();
                            m_pDataMatrixInspNGFileNames.Clear();
                            m_fPixelResoltuionX = (RCP.M100_VisionCalFineScaleX.AsDouble / 1000.0);
                            m_fPixelResoltuionY = (RCP.M100_VisionCalFineScaleY.AsDouble / 1000.0);
                            m_fMarkingWidthPixel = m_fPixelResoltuionX > 0 ? RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>() / m_fPixelResoltuionX : 0;
                            m_fMarkingHeightPixel = m_fPixelResoltuionY > 0 ? RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>() / m_fPixelResoltuionY : 0;
                            m_fPixelErrorWidth = m_fPixelResoltuionX > 0 ? 0.4 / m_fPixelResoltuionX : 0;

                            for (int i = 0; i < m_pProcessDataList.Count; i++)
                            {

                                m_fMarkingOffSetX = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_X.GetValue<double>() + m_pMarkingOffSetArray[m_pProcessDataList[i].iInspOrderIDX].Item1.GetValue<double>();
                                m_fMarkingOffSetY = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_Y.GetValue<double>() + m_pMarkingOffSetArray[m_pProcessDataList[i].iInspOrderIDX].Item2.GetValue<double>();


                                m_fMarkingCenterXPixel = m_pProcessDataList[i].pMatchResult.m_fSensorXPos +
                                                                                (m_fPixelResoltuionX > 0 ? m_fMarkingOffSetX / m_fPixelResoltuionX :
                                                                                 0);
                                m_fMarkingCenterYPixel = m_pProcessDataList[i].pMatchResult.m_fSensorYPos +
                                                                                (m_fPixelResoltuionY > 0 ? -1 * m_fMarkingOffSetY / m_fPixelResoltuionY :
                                                                                 0);
                                m_pDataMatrixInspRectList.Add(
                                                new Rectangle(
                                                (int)(m_fMarkingCenterXPixel - (m_fMarkingWidthPixel / 1.0)),
                                                (int)(m_fMarkingCenterYPixel - m_fMarkingHeightPixel / 1.2),
                                                (int)(m_fMarkingWidthPixel * 2.0),
                                                (int)(m_fMarkingHeightPixel * 1.6)
                                                ));
                                m_pDataMatrixInspNGFileNames.Add(m_pProcessDataList[i].pDataMatrix.DatamatrixText);
                            }
                            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(120);

                            if (m_pProcessDataList.Count > 0)
                            {
                                DateTime pCurrentTime = DateTime.Now;
                                strDM_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\{1}_{2}_{3}\\DM_INSP\\{4}~{5}\\",
                                                                                                        m_SubSeqpProcessStartTime.ToString("yyyyMMdd"),
                                                                                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                                                                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                                                                                        FA.MGR.RecipeRunningData.pCurrentProcessData.MES_MarkingStartSendCompleteTime.ToString("yyyyMMddHHmmss.ffff"),
                                                                                                        m_pProcessDataList[0].strMarkingIDX,//m_pProcessDataList[0].strMarkingIDX_TO_32,
                                                                                                        m_pProcessDataList[m_pProcessDataList.Count - 1].strMarkingIDX//m_pProcessDataList[m_pProcessDataList.Count-1].strMarkingIDX_TO_32																																
                                );
                            }
                            else
                            {
                                strDM_NG_SavePath = string.Format("d:\\PROC_IMG\\{0}\\DM_INSP\\", m_SubSeqpProcessStartTime.ToString("yyyyMMdd"));
                            }
                            VISION.FINE_LIB.MatrixCode1MultiRun((int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                                    m_pDataMatrixInspRectList.ToArray(),
                                    4.0f,
                                    true,
                                    strDM_NG_SavePath,
                                    m_pDataMatrixInspNGFileNames.ToArray()
                                    );

                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, false);
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.MARKING_INSP_RUN_FINISH:
                        {
                            if (VISION.FINE_LIB.GetMatrixCode1TotalResultCount() > 0)
                            {
                                VISION.FINE_LIB.GetMatrixCode1ResultList(out m_pMatrixCodeResultList);
                                for (int i = 0; i < m_pMatrixCodeResultList.Count; i++)
                                {
                                    m_pProcessDataList[i].pMatrixCodeResult = m_pMatrixCodeResultList[i].Clone();
                                    m_pProcessDataList[i].bMarkingInspExecuted = true;

                                }
                            }
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.MARKING_INSP, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "Marking Insp Run End");
                            NextSubStep(eMODULE_SEQ_PROC.PROC_END_Check_Start);
                        }
                        break;
                    #endregion[ Target Marking Inspection ]
                    #region [ Process End Check & Processed MES Report ]
                    case eMODULE_SEQ_PROC.PROC_END_Check_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_END_CHECK, false);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC END Check Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_END_Check_Finish:
                        {
                            m_pMarkingReportDataList.Clear();
                            if (m_bProcessEndChecked == false)
                            {

                                int iForInitValue3 =
                                m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                                RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqColMovingExeCount +
                                RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqRowMovingExeCount;
                                int iForLoopCount = m_bSubSeqMovingLastPerOneLine_ForMES == false ?
                                  m_iSubSeqInspRowCount :
                                  m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;
                                for (int i = iForInitValue3; i < iForInitValue3 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bInProcess = false;
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bProcessDone = true;
                                    //MES Marking Data Report Add
                                    m_pMarkingReportDataList.Add(FA.MGR.RecipeRunningData.pCurrentProcessData[i]);
                                    /*FA.MGR.MESMgr.SendMarkingReport(
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData[i],
                                            string.Format("{0}{1}", FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                            (FA.MGR.RecipeRunningData.lProductStartIDX + i).ToString("00000")),
                                            out pReportResult
                                            );*/
                                }
                                RecipeProcessEndCheck();
                            }
                            else
                            {
                                int iForInitValue3 =
                                m_iSubSeqProductDoneMaxCount * m_iSubSeqMultiArrayExeCount +
                                RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() * m_iSubSeqPreColMovingExeCount +
                                RCP_Modify.Inspection_RowCount.GetValue<int>() * m_iSubSeqPreRowMovingExeCount;
                                int iForLoopCount = m_bSubSeqMovingLastPerOneLine_ForMES == false ?
                                 m_iSubSeqInspRowCount :
                                 m_iSubSeqInspRowLastCount <= 0 ? m_iSubSeqInspRowCount : m_iSubSeqInspRowLastCount;
                                for (int i = iForInitValue3; i < iForInitValue3 + iForLoopCount; i++)
                                {
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bInProcess = false;
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i].bProcessDone = true;
                                    //MES Marking Data Report Add
                                    m_pMarkingReportDataList.Add(FA.MGR.RecipeRunningData.pCurrentProcessData[i]);
                                    /*FA.MGR.MESMgr.SendMarkingReport(
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                    FA.MGR.RecipeRunningData.pCurrentProcessData[i],
                                    string.Format("{0}{1}", FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                    (FA.MGR.RecipeRunningData.lProductStartIDX + i).ToString("00000")),
                                    out pReportResult
                                    );*/
                                }
                            }
                            m_bProcessEndChecked = false;
                            FA.MGR.RecipeRunningData.lProductProcessDoneCount += m_iSubSeqInspRowCount;
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_END_CHECK, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC END Check End");
                            NextSubStep(eMODULE_SEQ_PROC.PROC_MES_Marking_Report_Start);
                        }
                        break;


                    case eMODULE_SEQ_PROC.PROC_MES_Marking_Report_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_MES_MARKING_REPORT, false);
                            if (!m_stRun.stwatchForSub.IsDone) break;
                            if (FA.MGR.MESMgr.IsConnected == false)
                            {
                                FA.MGR.MESMgr.DoConnect();
                                m_stRun.stwatchForSub.SetDelay = 5;
                                break;
                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC Marking Report Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_MES_Marking_Report_Finish:
                        {
                            DKT_MES_MarkingReportInfo pReportResult = null;
                            if (!FA.MGR.MESMgr.SendMarkingReportForUnit(
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strMESCode,
                                            m_pMarkingReportDataList.ToArray(),
                                            out pReportResult
                                            ))
                            {
                                FA.MGR.RunMgr.ModeChange(
                                         eRunMode.Jam,
                                         SubSeq_RecipeProcessStep, eManualMode.None,
                                         pReportResult == null ? "" : pReportResult.strCommMSG
                                         );
                                break;
                            }

                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_MES_MARKING_REPORT, true);
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC Marking Report End");
                            NextSubStep(eMODULE_SEQ_PROC.JIG_EXIST_CHK_Start);
                        }
                        break;
                    #endregion [ Process End Check & Processed MES Report ]
                    #region [ Process End ]
                    case eMODULE_SEQ_PROC.PROC_END_Start:
                        {
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_END, false);
                            FA.MGR.RecipeRunningData.InsertFirstData(FA.MGR.RecipeRunningData.pCurrentProcessData.Clone());
                            if (FA.MGR.RecipeRunningData.iDataListCount >= 30)
                            {
                                FA.MGR.RecipeRunningData.RemoveData(1, FA.MGR.RecipeRunningData.iDataListCount - 1);
                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC END Start");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_END_MES_CHECK:
                        {
                            if (!m_stRun.stwatchForSub.IsDone) break;
                            if (FA.MGR.MESMgr.IsConnected == false)
                            {
                                FA.MGR.MESMgr.DoConnect();
                                m_stRun.stwatchForSub.SetDelay = 5;
                                break;
                            }
                            FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep), "PROC END MES Connect Check OK");
                            NextSubStep();
                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_END_MES_FINISH:
                        {
                            DKT_MES_MarkingEndInfo pEndRet = null;
                            if (FA.MGR.MESMgr.SendMarkingEnd(
                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                            (int)(m_iSubSeqProductStartNo),
                            (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1),
                            out pEndRet) == true)
                            {
                                m_iSubSEQMES_TestMode_ProductDoneCount = (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1);
                                FA.MGR.RecipeRunningData.pCurrentProcessData.SetMarkingEndInfoData(pEndRet);
                                FA.MGR.RecipeRunningData.pCurrentProcessData.bMES_MarkingEndSuccess = true;
                                FA.LOG.SEQ(string.Format("Scanner_{0}", SubSeq_RecipeProcessStep),
                                        string.Format("PROC END MES MarkingEnd {0}_{1}_{2}_{3}",
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                        (int)(m_iSubSeqProductStartNo),
                                        (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1)
                                        )
                                        );
                                NextSubStep();
                            }
                            else
                            {
                                FA.MGR.RunMgr.ModeChange(
                                        eRunMode.Jam,
                                        SubSeq_RecipeProcessStep, eManualMode.None,
                                        string.Format("{0}_{1}_{2}_{3}\nMES MSG:{2}",
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                        FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                        (int)(m_iSubSeqProductStartNo),
                                        (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1),
                                        pEndRet == null ? "" : pEndRet.strCommMSG)
                                        );
                                break;
                            }

                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_END_PRODUCT_ERROR_CHECK:
                        {
                            if (FA.OPT.RunningProductErrorAlarmEnable.m_bState == true)
                            {
                                if (FA.MGR.RecipeRunningData.pCurrentProcessData.IsExistError(true))
                                {
                                    FA.MGR.RunMgr.ModeChange(
                                    eRunMode.Jam,
                                    SubSeq_RecipeProcessStep, eManualMode.None,
                                    ""
                                    );
                                    break;
                                }
                                FA.LOG.SEQ(
                                string.Format("Scanner_{0}", SubSeq_RecipeProcessStep),
                                string.Format("{0}_{1} PROC Product Error Check OK ",
                                         (int)(m_iSubSeqProductStartNo),
                                         (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1))
                                         );
                                NextSubStep();
                            }
                            else
                            {
                                NextSubStep();
                            }
                        }
                        break;
                    case eMODULE_SEQ_PROC.PROC_END_Finish:
                        {
                            FA.MGR.RecipeRunningData.iJIGProcessedCount++;
                            //FA.MGR.RecipeRunningData.lProductStartIDX += FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount;
                            FA.MGR.RecipeRunningData.bCurrentInProess = false;
                            FA.LOG.SEQ(
                            string.Format("Scanner_{0}", SubSeq_RecipeProcessStep),
                            string.Format("{0}_{1} PROC END ",
                                            (int)(m_iSubSeqProductStartNo),
                                            (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1))
                                            );
                            FA.LOG.ProcessEvent("Scanner",
                                            string.Format("PROC END {0}_{1}_{2}_{3}",
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strLotCardCode,
                                            FA.MGR.RecipeRunningData.pCurrentProcessData.strJIGCode,
                                            (int)(m_iSubSeqProductStartNo),
                                            (int)(m_iSubSeqProductStartNo + FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount - 1)
                                            )
                                            );
                            FinishSubStep();
                        }
                        break;
                        #endregion  [ Process End ]
                }
                SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_RecipeProcess + m_stRun.nSubStep);
                return false;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "RunningScanner_{0}:{1}:{2}", m_stRun.nSubStep, exc.StackTrace, exc.Message);
                return false;
            }
        }
        public bool SubSeq_Process(bool a_bPosInsp, bool a_bMarking, bool b_DataMatrixInsp)
        {
            if (!base.SetRecoveryRunInfo())
                return false;

            SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Process + m_stRun.nSubStep);
            return false;
        }
        private bool GetProcessIDX(out int a_bufIDX, float a_fSensorX, float a_fSensorY, float a_fWidth, float a_fHeight)
        {
            a_bufIDX = -1;
            float fSensorRowPitchPixel = RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<float>() / RCP.M100_VisionCalFineScaleY.AsSingle * 1000;
            // To do List Parameter -> Recipe  
            float fErrAllowPixel_XPitch = (float)RCP_Modify.INSP_Pixel_Error_Range.GetValue<double>() / RCP.M100_VisionCalFineScaleX.AsSingle * 1000;
            float fErrAllowPixel_YPitch = (float)RCP_Modify.INSP_Pixel_Error_Range.GetValue<double>() / RCP.M100_VisionCalFineScaleY.AsSingle * 1000;
            // To do List Parameter -> Recipe 
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

        public bool bSubSeqMovingLastPerOneLine
        {
           get { return m_iSubSeqRowMovingExeCount >= m_iSubSeqRowMovingMax; }
        }
        private bool m_bSubSeqMovingLastPerOneLine_ForMES = false;
        private void RecipeProcessEndCheck()
        {

            int iInspRowCount = RCP_Modify.Inspection_RowCount.GetValue<int>();
            int iInspColCount = RCP_Modify.Inspection_ColCount.GetValue<int>();
            int RowCountMax = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
            int ColCountMax = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
            int iRealRowCount = 0;


            if (m_iSubSeqRowMovingExeCount >= m_iSubSeqRowMovingMax)
            {
                m_bSubSeqMovingLastPerOneLine_ForMES = true;
                iRealRowCount = m_iSubSeqInspRowLastCount; 
                m_iSubSeqProductDoneRowProgressCount += iRealRowCount;
                m_iSubSeqProductDoneColProgressCount += iInspColCount;
                m_iSubSeqProductDoneCount += (iRealRowCount * iInspColCount);
            }
            else
            {
                m_bSubSeqMovingLastPerOneLine_ForMES = false;
                iRealRowCount = iInspRowCount; 
                 m_iSubSeqProductDoneRowProgressCount += iRealRowCount;
                m_iSubSeqProductDoneColProgressCount += iInspColCount;
                m_iSubSeqProductDoneCount += (iRealRowCount * iInspColCount);
            }
            if (m_bSusbSeqRowReverseDirEnable == false)
            {
                if (m_iSubSeqRowMovingExeCount + 1 <= m_iSubSeqRowMovingMax)
                {
                    m_iSubSeqRowMovingExeCount++;
                  
                }             
            }
            else
            {
                if (m_iSubSeqRowMovingExeCount - 1 >= 0)
                {
                    m_iSubSeqRowMovingExeCount--;
                }
            }
          
            if (m_iSubSeqProductDoneRowProgressCount >= RowCountMax)
            {
                m_iSubSeqProductDoneRowProgressCount = 0;
                m_iSubSeqColMovingExeCount++;                
                m_bSusbSeqRowReverseDirEnable = !m_bSusbSeqRowReverseDirEnable;
            }
        }


        private void UpdateInitStatus(eMODULE_STATUS_INIT_SCANNER a_eItem, bool a_bCompleted)
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
        public void UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC a_eItem, bool a_bCompleted)
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
        public bool IsDone_SEQStauts(eMODULE_SEQ_STATUS_PROC a_eItem)
        {
            return Interface_SEQ[a_eItem].IsDone();
        }

        public bool IsDone_InitStauts(eMODULE_STATUS_INIT_SCANNER a_eItem)
        {
            return Interface_Init[a_eItem].IsDone();
        }

        private void UpdateWorkStatus(eMODULE_STATUS_INIT_SCANNER a_eItem, bool a_bStart, bool a_bFinish)
        {
            if ((int)a_eItem < 0 || (int)a_eItem >= Interface_Init.Count)
                return;

            FA.DEF.stRunModuleStatus stWork = Interface_Init[a_eItem];
            if (a_bStart)
                stWork.SetStart();
            else if (a_bFinish)
                stWork.SetFinish();

            Interface_Init[a_eItem] = stWork;
        }

        public override void Ready()
        {
            throw new NotImplementedException();
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
                        if (SubSeq_RecipeProcess(true, true, true, true))
                        {
                            base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_FIND_ACTION;
                            base.m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
                            base.SetRecoveryRunInfo();
                            m_pRunningUnload.UpdateSEQ_Status(RunningUnload.eMODULE_SEQ_STATUS_UNLOADING.Cylinder_Check, false);
                            m_pRunningUnload.UpdateSEQ_Status(RunningUnload.eMODULE_SEQ_STATUS_UNLOADING.UNLOADING_FINISH, false);
                            UpdateSeqStatus(eMODULE_SEQ_STATUS_PROC.JOB_END, true);
                            //UnLoad Command 														
                        }
                    }
                    break;
            }
            return bComplete;
        }

        public override void Stop()
        {

        }
        public override bool FindAction()
        {
            if (!base.SetRecoveryRunInfo())
                return false;

            if (
                     this.IsDone_SEQStauts(eMODULE_SEQ_STATUS_PROC.JOB_END) == false &&
                     m_pRunningLoad.IsDone_SEQ_Stauts(RunningLoad.eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH) &&
                     m_pRunningUnload.IsDone_SEQ_Stauts(RunningUnload.eMODULE_SEQ_STATUS_UNLOADING.UNLOADING_FINISH)
                     )
            {
                base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_PROCESS;
            }
            // do to list 
            // Flag , Status 
            // Loading Complate Flag ADD
            return true;
        }
        public override void ConnectingModule()
        {
            m_pRunningStage = (RunningStage)FA.MGR.RunMgr.GetItem("STAGE");
            m_pRunningProcess = (RunningProcess)FA.MGR.RunMgr.GetItem("PROCESS");
            m_pRunningLoad = (RunningLoad)FA.MGR.RunMgr.GetItem("Load");
            m_pRunningUnload = (RunningUnload)FA.MGR.RunMgr.GetItem("Unload");
        }
        private Tuple<bool, string> CreateRecipeDataMatrixPathToScanner(int a_iIDX, Scanner.ScanlabRTC5.RTC_LIST a_ScannerIDX)
        {
            if (RTC5.Instance.GetListStatus_BUSY(a_ScannerIDX) == true)
                return Tuple.Create(false, $"{a_ScannerIDX} List Busy Status");

            if (m_pProcessDataList == null || a_iIDX >= m_pProcessDataList.Count)
                return Tuple.Create(false, $"Run IDX({a_iIDX}) Invalid");


            if (m_pProcessDataList[a_iIDX].pDataMatrix != null &&
                m_pProcessDataList[a_iIDX].pDataMatrix.bCreateCoordinate == false
                )
            {
                if (FA.MGR.VisionToScannerCalb.CorrectPosUsingCalFile(
                 m_pProcessDataList[a_iIDX].pMatchResult.m_fSensorXPos,
                 m_pProcessDataList[a_iIDX].pMatchResult.m_fSensorYPos,
                 out m_fMarkingCenterX,
                 out m_fMarkingCenterY
                 ) != 0)
                {
                    return Tuple.Create(false, "Correction Fail");
                }
                int iInspOrderIDX = m_pProcessDataList[a_iIDX].iInspOrderIDX;
                float fToalMarkingOffsetX = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_X.GetValue<float>();
                float fToalMarkingOffsetY = RCP_Modify.PROCESS_DATA_MAT_MARKING_OFFSET_Y.GetValue<float>();
                float fIdxMarkingOffsetX = m_pMarkingOffSetArray[iInspOrderIDX].Item1.GetValue<float>();
                float fIdxMarkingOffsetY = m_pMarkingOffSetArray[iInspOrderIDX].Item2.GetValue<float>();
                Trace.WriteLine(string.Format(@"Total Offset [{0},{1}] {2}IDX({3})Offset[{4},{5}]",
                                fToalMarkingOffsetX, fToalMarkingOffsetY,
                                iInspOrderIDX, m_iSubSeqMarkingExeCount,
                                fIdxMarkingOffsetX, fIdxMarkingOffsetY));
                if (!m_pProcessDataList[a_iIDX].pDataMatrix.CreateCodrdinates(
                                new System.Drawing.PointF(
                                (float)m_fMarkingCenterX + fToalMarkingOffsetX + fIdxMarkingOffsetX,
                                (float)m_fMarkingCenterY + fToalMarkingOffsetY + fIdxMarkingOffsetY
                                ),
                                new System.Drawing.SizeF(RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>(),
                                RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>()),
                                RCP_Modify.PROCESS_DATA_MAT_ZERO_POS.GetValue<EzIna.DataMatrix.ZeroPosition>(),
                                RCP_Modify.PROCESS_DATA_MAT_ROTATE.GetValue<EzIna.DataMatrix.Rotate>()
                                ))
                {
                    return Tuple.Create(false, "Create DM Coordinate Fail");
                }
                if (!RTC5.Instance.ListReset(a_ScannerIDX))
                    return Tuple.Create(false, "{a_ScannerIDX} List Reset Fail");
                if (!RTC5.Instance.ListBegin(a_ScannerIDX))
                    return Tuple.Create(false, "{a_ScannerIDX} List Begin Fail");

                if (!RTC5.Instance.MakeListFromGraphicsPath(
                                m_pProcessDataList[a_iIDX].pDataMatrix.CodeGraphicsPath,
                                a_ScannerIDX,
                                false,
                                false)
                                )
                    return Tuple.Create(false, "Make DM GraphicsPath Fail");

                RTC5.Instance.ListEnd();
                return Tuple.Create(true, "");
            }

            return Tuple.Create(false, "DM Base Data Not Created");
        }
    }
}

