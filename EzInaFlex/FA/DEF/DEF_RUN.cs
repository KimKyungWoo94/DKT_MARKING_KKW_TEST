using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    public static partial class DEF //Running definition
    {
        public enum eTasks
        {
            None = -1
          , L_Alignment
          , L_Inspection
          , L_BallGridArraySoldering
          , L_Unloading
          , R_Alignment
          , R_Inspection
          , R_BallGridArraySoldering
          , R_Unloading
          , JobEnd
          , TaskMax
        }

        public enum eWorksInfo
        {
            L_Align,
            R_Align,
            L_Soldering,
            R_Soldering,
            MAX
        }

        public enum eSEQ_Status
        {
            DONE,
            BUSY,
            JAM
        }

        public struct stRunModuleStatus
        {
            public bool bStart;
            public bool bFinish;

            public void Init()
            {
                Clear();
            }

            public void Clear()
            {
                bStart = false;
                bFinish = false;
            }

            public void Reset()
            {
                Clear();
            }

            public void SetStart()
            {
                bStart = true;
                bFinish = false;
            }

            public void SetFinish()
            {
                bStart = true;
                bFinish = true;
            }

            public bool IsDone()
            {
                return bStart & bFinish;
            }
        }



        public struct stPreviousPosition
        {
            double fGantryX; //0,1
            double fGantryY; //2
            double fVisionZ; //3
            double fContactZ;//4
            double fContactR;//5
            double fConvyY;  //6
            double fConvyZ;  //7


            public void Init()
            {
                Clear();
            }

            public void Clear()
            {
                fGantryX = 0.0;
                fGantryY = 0.0;
                fVisionZ = 0.0;
                fContactZ = 0.0;
                fContactR = 0.0;
                fConvyY = 0.0;
                fConvyZ = 0.0;
            }
        }
        public struct stCylinder
        {
            int iCylinder;
            bool bStatus;

            public void Init()
            {
                Clear();
            }

            public void Clear()
            {
                iCylinder = -1;
                bStatus = false;
            }
        }
        public struct stPreviousAction
        {
            public stPreviousPosition stPrevPos; //Motion
            public List<stCylinder> vecPrevCylinder; //Cylinder

            public void Init()
            {
                if (vecPrevCylinder == null)
                    vecPrevCylinder = new List<stCylinder>();

                stPrevPos.Clear();
            }

            public void Clear()
            {
                if (vecPrevCylinder != null)
                    vecPrevCylinder.Clear();

                stPrevPos.Clear();

            }

        }


        #region 1. Running
        public const int StartActionOfMain = 40;
        public const int FinishActionOfMain = 0;

        public const int StartActionOfSub = 10;
        public const int FinishActionOfSub = 0;

        public const int FindAction = 100;
        public const int RepeatActionOfMain = 40;

        public const int SEQ_START = 1;
        public const int SEQ_FINISH = 0;

        public struct stRunInfo
        {
            public bool bBackup;
            public bool bRunStop;
            public bool bHoldOnTimeout;
            public bool bTimeoutNow;

            //main seq.
            public int nMainStep;
            public int nSubStep;
            public int nParallelSeqStep;
            public int nPrevMainStep;
            public int nPrevSubStep;
            public int nRepeatLimit;
            public int nRepeatCurr;

            public EzIna.UC.StopWatchTimer stwatchForMain;
            public EzIna.UC.StopWatchTimer stwatchForSub;

            //이전 동작 확인용 ( 모터의 위치 값과 실린더 동작 위치. )
            public stPreviousAction stPrevAction;

            public void Init()
            {
                bBackup = false;
                bRunStop = true;
                bHoldOnTimeout = false;
                bTimeoutNow = false;

                nMainStep = FA.DEF.FinishActionOfMain;
                nSubStep = FA.DEF.FinishActionOfSub;
                nPrevMainStep = nMainStep;
                nPrevSubStep = nSubStep;

                nParallelSeqStep = FA.DEF.SEQ_FINISH;

                nRepeatLimit = 0;
                nRepeatCurr = 0;

                stwatchForMain = new EzIna.UC.StopWatchTimer();
                stwatchForSub = new EzIna.UC.StopWatchTimer();
                stPrevAction.Init();
            }
            public void Clear()
            {
                bBackup = false;
                bRunStop = true;
                bHoldOnTimeout = false;

                nMainStep = FA.DEF.FinishActionOfMain;
                nSubStep = FA.DEF.FinishActionOfSub;
                nParallelSeqStep = FA.DEF.SEQ_FINISH;
                nPrevMainStep = nMainStep;
                nPrevSubStep = nSubStep;

                nRepeatLimit = 0;
                nRepeatCurr = 0;

                stwatchForMain.TimeStop();
                stwatchForSub.TimeStop();
                stPrevAction.Clear();
            }
            public void HoldOnTimeout()
            {
                bHoldOnTimeout = true;
                bTimeoutNow = false;
            }
            public void TimeoutNow()
            {
                bHoldOnTimeout = false;
                bTimeoutNow = true;
            }
        }
        public static int GetStepOfMain(int a_nStep)
        {
            return (a_nStep / 100) * 100;
        }
        public static int GetStepOfSub(int a_nStep)
        {
            return (a_nStep / 10) * 10;
        }
        public enum eRunMode
        {
            Jam = 0
          , Init
          , Ready
          , ToRun
          , Run
          , ToStop
          , Stop
          , ToManual
          , Manual
          , ToStopManual
          , Max
        }
        public enum eStageSeq
        {
            Finish = 0
          , FindAction = 100
          , PanelAlignment = 200
          , PanelInspection = 300
          , BlockInspection = 400
        }
        public enum eManualMode
        {
            None
          , VISION_CAL            //RunningProcess
          , MAKE_PGM              //RunningProcess
          , MAKE_POWERTABLE       //RunningProcess
          , POWER_CHECK          //RunningProcess

          , GALVO_CAL       //RunningScanner
          , FOCUS_FINDER      //RunningScanner
          , CROSS_HAIR      //RunningScanner
          , FIND_CPU        //RunningScanner

          , STAGE_CAL       //RunningStage
          , ALIGNMENT       //RunningStage
          , AUTOFOCUS       //RunningStage
				  , MANUAL_MOVING

				  , JIG_LOADING
				  , JIG_UNLOADING

					, JIG_MOVING_TEST
					, JIG_MOVING_N_POS_INSP
					, JIG_MOVING_INSP_N_MARKING
					, MANUAL_PROCESS
          , Max,
        }
        #endregion

        #region 2. Timeout
        public const int Timeout_Init = (60 * 1000) * 5;
        public const int Timeout_Ready = (60 * 1000) * 5;
        public const int Timeout_Run = (60 * 1000) * 5;
        #endregion

        #region 3. Error
        public const int Error_Init = 1000;
        public const int Error_Init_Scanner = 1100;
        public const int Error_Init_Stage = 1200;
        public const int Error_Init_Process = 1300;
				public const int Error_Init_Loader	= 1400;
				public const int Error_Init_Unloader = 1500;

        public const int Error_Run_Stage_Mapping_XY = 2000;
        public const int Error_Run_Scanner_PowerTable = 3000;
        public const int Error_Run_Scanner_CrossHair = 4000;
        public const int Error_Run_AutoFocus = 5000;
        public const int Error_Run_Scanner_Cal = 6000;
				public const int Error_Run_Manual_Moving=7000;

				public const int Error_Run_LOADING=8000;
				public const int Error_Run_UNLOADING=9000;
				public const int Error_Run_Process=10000;
				public const int Error_Run_RecipeProcess=10000;

        #endregion

        #region 4. I/O
        public enum eButtonSW
        {
            OFF,
            INIT,
            INIT_FLICKER,
            START,
            STOP,
            RESET,
            START_FLICKER,
            STOP_FLICKER,
            RESET_FLICKER,
        }

        public enum eTowerLamp
        {
            None = -1
          , Run
          , Stop
          , Ready
          , Alarm
          , Error
        }

        public enum eMelody
        {
            None = -1
          , Notification //job finish
          , Alarm
          , Error
        }

        public enum ePneumaticModules
        {
            None = -1
          , StageVacuum
        }
        #endregion


        #region [INIT ENUM]
        //Scanner

        //Stage


        public enum eSEQ_MAIN_INIT
        {
            PRECHECK = 1
          , MODULE_START = 10
          , MODULE_START_COMPLETE
          , MODULE_RUN = 20
          , MODULE_RUN_COMPLETE
          , COMPLETE = 30
          , MAX
        }
        #endregion[INIT ENUM]
        #region [장비 ]
        public enum eSeqReady
        {
            Finish = 0
          , ready_prechk = 1
          , modules_clear_status_start
          , modules_clear_status_finish
          , modules_set_run_status_start
          , modules_set_run_status_finish

        }

        public enum eSeqRunning
        {
            Finish = 0
          , Loop_prechk = 1
          , Loop_start
          , Loop
          , Loop_finish
          , Alarm
        }
        public enum eSeqPanelIns
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , GerberFileOpen
          , DCPowerSupply_set_panel_pwrV_start
          , DCPowerSupply_set_panel_pwrV_finish
          , DCPowerSupply_set_panel_pwrA_start
          , DCPowerSupply_set_panel_pwrA_finish
          , DCPowerSupply_panel_pwr_on_start
          , DCPowerSupply_panel_pwr_on_finish
          , Basic_Settings_finish

          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish

          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          , MoveGantryXYToPanelInspectionPos_prechk = 60
          , MoveGantryXYToPanelInspectionPos_start
          , MoveGantryXYToPanelInspectionPos_finish
          , PowerSignalOn
          , Grab
          , IsGrab
          , BlobRun
          , CreateVisioMap
          , PowerSignalOff

          , DCPowerSupply_set_led_pwrV_start
          , DCPowerSupply_set_led_pwrV_finish
          , DCPowerSupply_set_led_pwrA_start
          , DCPowerSupply_set_led_pwrA_finish
          , DCPowerSupply_pwr_off_start
          , DCPowerSupply_pwr_off_finish

          , DoGerberMapCompareToVisionMap = 80
        }
        public enum eSeqAlign
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , Basic_Settings_finish

          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish


          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          , MoveGantryXYTo1stAlignmentPos_prechk = 60
          , MoveGantryXYTo1stAlignmentPos_start
          , MoveGantryXYTo1stAlignmentPos_finish


          , MoveVisionZToPanelAlignPos_prechk = 70
          , MoveVisionZToPanelAlignPos_start
          , MoveVisionZToPanelAlignPos_finish

          , Grab_Repeat_1st_start
          , Grab_1st
          , IsGrab_1st
          , Match_Run_1st
          , MoveToMatchingPos_1st_start
          , MoveToMatchingPos_1st_finish
          , Grab_Repeat_1st_finish

          , MoveGantryXYTo2ndAlignmentPos_prechk = 80
          , MoveGantryXYTo2ndAlignmentPos_start
          , MoveGantryXYTo2ndAlignmentPos_finish
          , Grab_2ndPos
          , IsGrab_2ndPos
          , Match_Run_2ndPos
          , MoveGantryXYToFiducial1stPos_start
          , MoveGantryXYToFiducial1stPos_finish
        }
        public enum eSeqBlockInspection
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , DCPowerSupply_set_led_pwrV_start
          , DCPowerSupply_set_led_pwrV_finish
          , DCPowerSupply_set_led_pwrA_start
          , DCPowerSupply_set_led_pwrA_finish
          , DCPowerSupply_led_pwr_on_start
          , DCPowerSupply_led_pwr_on_finish
          , Basic_Settings_finish

          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish

          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish
          //ver1.0.0.3
          //{{
          //로컬 정렬 유무 확인
          , Local_Alignment_prechk = 60
          //로컬 정렬 시작
          , Local_Alignment_start
          , Local_Alignment_illumination_slow_start
          , Local_Alignment_illumination_slow_finish
          , Local_Alignment_camera_expose_slow_start
          , Local_Alignment_camera_expose_slow_finish
          , Local_Alignment_MoveToContactZ_UpPos_start
          , Local_Alignment_MoveToContactZ_UpPos_finish
          , Local_Alignment_MoveToContactT_ReadyPos_start
          , Local_Alignment_MoveToContactT_ReadyPos_finish
          , Local_Alignment_MoveToVisionZ_UpPos_start
          , Local_Alignment_MoveToVisionZ_UpPos_finish
          , Local_Alignment_MoveToAlignmentPos_start
          , Local_Alignment_MoveToAlignmentPos_finish
          , Local_Alignment_MoveToVisionZ_AlignmentPos_start
          , Local_Alignment_MoveToVisionZ_AlignmentPos_finish
          , Local_Alignment_Grab
          , Local_Alignment_IsGrab
          , Local_Alignment_Match_Run
          , Local_Alignment_illumination_fast_start
          , Local_Alignment_illumination_fast_finish
          , Local_Alignment_camera_expose_fast_start
          , Local_Alignment_camera_expose_fast_finish
          , Local_Alignment_finish
          //로컬 정렬 종료
          //}}

          , MoveToFindChipPos_prechk = 90
          , MoveToFindChipPos_start
          , MoveToFindChipPos_finish

          , MoveVisionZToBlockInspectionPos_prechk = 100
          , MoveVisionZToBlockInspectionPos_start
          , MoveVisionZToBlockInspectionPos_finish

          , MoveToContactPos_prechk = 110
          , MoveToContactPos_start
          , MoveToContactPos_finish

          , Measurement_loop_prechk = 120
          , Measurement_loop_start
          , pwr_on
          , Grab
          , IsGrab
          , blob
          , Measurement_loop_finish

          , MoveContactZToUpPos_prechk = 130
          , MoveContactZToUpPos_start
          , MoveContactZToUpPos_finish


          , Measure_finish_prechk = 140
          , DCPowerSupply_pwr_off_start
          , DCPowerSupply_pwr_off_finish

          , MoveZAxesToReady_prechk = 150
          , MoveZAxesToReady_start
          , MoveZAxesToReady_finish

          , MoveGantryToLoad_prechk = 160
          , MoveGantryToLoad_start
          , MoveGantryToLoad_finish
        }

        public enum eSeqStageMapping
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , Basic_Settings_finish

          //Ready position
          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish


          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          //Mapping 1st Pos
          , MoveGantryXYTo1stStageMappingPos_prechk = 60
          , MoveGantryXYTo1stStageMappingPos_start
          , MoveGantryXYTo1stStageMappingPos_finish

          , MoveVisionZToMappingPos_prechk = 70
          , MoveVisionZToMappingPos_start
          , MoveVisionZToMappingPos_finish

          , Grab_Repeat_1st_start
          , Grab_1st
          , IsGrab_1st
          , Match_Run_1st
          , MoveToMatchingPos_1st_start
          , MoveToMatchingPos_1st_finish
          , Grab_Repeat_1st_finish

          //Mapping
          , Mapping_prechk = 90
          , Mapping_start
          , Mapping_Rows_loop_prechk = 100
          , Mapping_Rows_loop_start
          , Mapping_Cols_loop_prechk = 110
          , Mapping_Cols_loop_start
          , Mapping_Grab
          , Mapping_IsGrab
          , Mapping_Match_Run
          , Mapping_Cols_loop_finish
          , Mapping_Rows_loop_finish
          , Mapping_finish

          , MoveZAxesToReadyPos_prechk = 120
          , MoveZAxesToReadyPos_start
          , MoveZAxesToReadyPos_finish

          , MoveGantryToUnloadPos_prechk = 130
          , MoveGantryToUnloadPos_start
          , MoveGantryToUnloadPos_finish
        }
        public enum eSeqStageMapping_Check
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , Basic_Settings_finish

          //Ready position
          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish


          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          //Mapping 1st Pos
          , MoveGantryXYTo1stStageMappingPos_prechk = 60
          , MoveGantryXYTo1stStageMappingPos_start
          , MoveGantryXYTo1stStageMappingPos_finish

          , MoveVisionZToMappingPos_prechk = 70
          , MoveVisionZToMappingPos_start
          , MoveVisionZToMappingPos_finish

          , Grab_Repeat_1st_start
          , Grab_1st
          , IsGrab_1st
          , Match_Run_1st
          , MoveToMatchingPos_1st_start
          , MoveToMatchingPos_1st_finish
          , Grab_Repeat_1st_finish

          //Mapping
          , Mapping_prechk = 90
          , Mapping_start
          , Mapping_Rows_loop_prechk = 100
          , Mapping_Rows_loop_start
          , Mapping_Cols_loop_prechk = 110
          , Mapping_Cols_loop_start
          , Mapping_Grab
          , Mapping_IsGrab
          , Mapping_Match_Run
          , Mapping_Cols_loop_finish
          , Mapping_Rows_loop_finish
          , Mapping_finish

          , MoveZAxesToReadyPos_prechk = 120
          , MoveZAxesToReadyPos_start
          , MoveZAxesToReadyPos_finish

          , MoveGantryToUnloadPos_prechk = 130
          , MoveGantryToUnloadPos_start
          , MoveGantryToUnloadPos_finish
        }
        public enum eSeqStageMappingZ
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , Basic_Settings_finish

          //Ready position
          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish


          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          //Mapping 1st Pos
          , MoveGantryXYTo1stStageMappingPos_prechk = 60
          , MoveGantryXYTo1stStageMappingPos_start
          , MoveGantryXYTo1stStageMappingPos_finish

          , MoveVisionZToMappingPos_prechk = 70
          , MoveVisionZToMappingPos_start
          , MoveVisionZToMappingPos_finish

          , DisplacementSensor_AutoZeroOn_start = 80
          , DisplacementSensor_AutoZeroOn_finish
          //Mapping
          , Mapping_prechk = 90
          , Mapping_start
          , Mapping_Rows_loop_prechk = 100
          , Mapping_Rows_loop_start
          , Mapping_Cols_loop_prechk = 110
          , Mapping_Cols_loop_start
          , Mapping_Measure_prechk = 120
          , Mapping_Measure_start
          , Mapping_Measure_Finish
          , Mapping_Match_Run
          , Mapping_Cols_loop_finish
          , Mapping_Rows_loop_finish
          , Mapping_finish

          , MoveZAxesToReadyPos_prechk = 130
          , MoveZAxesToReadyPos_start
          , MoveZAxesToReadyPos_finish

          , MoveGantryToUnloadPos_prechk = 140
          , MoveGantryToUnloadPos_start
          , MoveGantryToUnloadPos_finish
        }
        public enum eSeqStageMappingZ_Check
        {
            Finish = 0
          , Basic_Settings_start = 10
          , variables
          , illumination_start
          , illumination_finish
          , camera_expose_start
          , camera_expose_finish
          , vacuum
          , chk_panel_on_the_table
          , Basic_Settings_finish

          //Ready position
          , MoveConatactZToReadyPos_prechk = 30
          , MoveContactZToReadyPos_start
          , MoveContactZToReadyPos_finish

          , MoveConatactRToReadyPos_prechk = 40
          , MoveContactRToReadyPos_start
          , MoveContactRToReadyPos_finish


          , MoveVisionZToPanelUpPos_prechk = 50
          , MoveVisionZToPanelUpPos_start
          , MoveVisionZToPanelUpPos_finish

          //Mapping 1st Pos
          , MoveGantryXYTo1stStageMappingPos_prechk = 60
          , MoveGantryXYTo1stStageMappingPos_start
          , MoveGantryXYTo1stStageMappingPos_finish

          , MoveVisionZToMappingPos_prechk = 70
          , MoveVisionZToMappingPos_start
          , MoveVisionZToMappingPos_finish

          , DisplacementSensor_AutoZeroOn_start = 80
          , DisplacementSensor_AutoZeroOn_finish
          //Mapping
          , Mapping_prechk = 90
          , Mapping_start
          , Mapping_Rows_loop_prechk = 100
          , Mapping_Rows_loop_start
          , Mapping_Cols_loop_prechk = 110
          , Mapping_Cols_loop_start
          , Mapping_Measure_prechk = 120
          , Mapping_Measure_start
          , Mapping_Measure_Finish
          , Mapping_Match_Run
          , Mapping_Cols_loop_finish
          , Mapping_Rows_loop_finish
          , Mapping_finish

          , MoveZAxesToReadyPos_prechk = 130
          , MoveZAxesToReadyPos_start
          , MoveZAxesToReadyPos_finish

          , MoveGantryToUnloadPos_prechk = 140
          , MoveGantryToUnloadPos_start
          , MoveGantryToUnloadPos_finish
        }

        #endregion

        public const int CommRepeatLimit = 20;
        public const int CommDelay = 500;
        public const int MoveDelay = 100;


        #region Motion

        public enum eGantryXYMotion
        {
            LoadPos
          , UnloadPos
          , PanelAlign1stPos
          , PanelAlign2ndPos
          , BlockCenterPos
          , PanelCenterPos
          , PanelMapping1stPos
        }
        public enum eGantryXMotion
        {
            LoadXPos
          , UnloadXPos
          , PanelAlign1stXPos
          , PanelAlign2ndXPos
          , PanelCenterXPos
          , BlockCenterXPos
          , PanelMapping1stXPos
        }
        public enum eGantryYMotion
        {
            LoadYPos
          , UnloadYPos
          , PanelAlign1stYPos
          , PanelAlign2ndYPos
          , PanelCenterYPos
          , BlockCenterYPos
          , PanelMapping1stYPos

        }
        public enum eVisionZMotion
        {
            UpPos
          , ReadyPos
          , PanelAlignPos
          , PanelInspectionPos
          , BlockAlignPos
          , BlockInspectionPos
          , MappingPos
        }
        public enum eContactRZMotion
        {
            Z_UpPos
          , Z_ReadyPos
          , Z_ContactPos
          , Z_OverDrivePos

          , R_ReadyPos
          , R_ContactPos
          , R_OffsetPos
        }
        public enum eConvyYZMotion
        {
            Z_UpPos
          , Z_ReadyPos
          , Z_DownPos
          , Y_HeightPos
          , Y_ReadyPos
        }

        public const double IN_POS = 0.003; //0.003 ->3 unit : mm
        public const double IN_POS_THETA = 0.010; //0.010 degree

        #endregion
        public static string MappingFileFullNameOfXY
        {
            get
            {
                string strPath = MappingDataFolderPath;
                string strMappingFilePath = Path.GetFullPath(Path.Combine(strPath, @"MappingDataOfXY.csv"));

                FileInfo FileInfor = new FileInfo(strMappingFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }

                return strMappingFilePath;
            }
        }
        public static string MappingFileFullNameOfZ
        {
            get
            {
                string strPath = MappingDataFolderPath;
                string strMappingFilePath = Path.GetFullPath(Path.Combine(strPath, @"MappingDataOfZ.csv"));

                FileInfo FileInfor = new FileInfo(strMappingFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }

                return strMappingFilePath;
            }
        }
        public static string MappingDataFolderPath
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strMappingDataFolderPath = Path.GetFullPath(Path.Combine(strPath, "MappingData"));

                DirectoryInfo DirInfor = new DirectoryInfo(strMappingDataFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();

                return strMappingDataFolderPath;

            }
        }

        public static string ConfigFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = System.IO.Path.GetDirectoryName(strPath);
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\Config"));

                DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();

                return strConfigFolderPath;
            }
        }
        public static string MeasureResultFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = Path.GetDirectoryName(strPath);
                string strMeasureResultfolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\MesureResult"));

                DirectoryInfo DirInfo = new DirectoryInfo(strMeasureResultfolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();

                return strMeasureResultfolderPath;
            }
        }

        public enum eSeries
        {
            Type_ValueVot_Keller
          , Type_ValueVot_1
          , Type_ValueVot_2
          , Type_ValueMotorPos
          , Type_OnOff_SDR
          , Type_OnOff_U_Axis
          , Type_OnOff_FlowSensor
          , Type_OnOff_FiberSensor
          , Type_OnOff_LaserShot
          , Type_Max
        }
        /// <summary>
        /// 타이머 주기 설정값.
        /// </summary>
        public const int nTimerInterval = 50; //20msec


    }//end of class


}
