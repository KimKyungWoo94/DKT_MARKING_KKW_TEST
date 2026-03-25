using System;
using System.Collections.Generic;
using EzIna.FA;
using EzIna;
namespace EzIna
{
    #region CFindFocus Class
    public class CFindFocus : IDisposable
    {
        public int m_iRow;
        public int m_iCol;
        public double m_dEncX;//(m_x)
        public double m_dEncY;//(m_y) 
        public double m_dEncZ;//(m_z)
        public double m_dAngleT;//(v_t)
        public bool m_bGoldenFocus;
        public EzIna.GUI.UserControls.eCellStatus m_eStatus;


        public CFindFocus()
        {
            Init();
        }
        public void Init()
        {
            m_iRow = -9999999;
            m_iCol = -9999999;
            m_dEncX = 0.0; //(m_x)
            m_dEncY = 0.0;//(m_y) 
            m_dEncZ = 0.0;
            m_dAngleT = 0.0;//(v_t)
            m_bGoldenFocus = false;
            m_eStatus = EzIna.GUI.UserControls.eCellStatus.Empty;
        }

        public void DataClear()
        {
            m_iRow = -9999;
            m_iCol = -9999;
            m_dEncX = -9999999.0; //(m_x)
            m_dEncY = -9999999.0;//(m_y) 
            m_dEncZ = -9999999.0;
            m_dAngleT = 0.0;//(v_t)
            m_bGoldenFocus = false;
            m_eStatus = EzIna.GUI.UserControls.eCellStatus.Empty;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~CMapping() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    #endregion CFindFocus Class
    public partial class RunningScanner
    {
        #region [ FOCUS FINDER ]

        List<List<CFindFocus>> m_vecFindFocusMap = null;
        //Find_Focus_Pgm m_FocusPGM = new Find_Focus_Pgm();
        Find_Focus_RTC m_FocusPGM = new Find_Focus_RTC();
        double m_dFocusFinderTargetXPos = 0.0;
        double m_dFocusFinderTargetYPos = 0.0;
        double m_dFocusFinderTargetZPos = 0.0;
        double m_dFocusFinderStartActXPos = 0.0;
        double m_dFocusFinderStartActYPos = 0.0;
        double m_dFocusFinderATTPos = 0.0;
        double m_dFocusFinderPowerPerecent = 0.0;
        int m_nFindFocus_Rows = 0;
        int m_nFindFocus_Columns = 0;

        public enum eMODULE_SEQ_FOCUS_FINDER
        {
            Finish = 0
            , Basic_Settings_start = 10
            , variables
            , MakeMap
            , MakePGM_prechk
            , MakePGM_makeReadyStatus
            , MakePGM_makeReadyStatuschk
            , MakePGM
            , Basic_Settings_finish

            , ShutterClose_prechk = 20
            , ShutterClose_start
            , ShutterClose_Finish

            //Move to powermeter position
            , MoveTo_StartZPos_prechk = 30 //Laser Focus Z Pos, ZigHeight, Thickness
            , MoveTo_StartZPos_start
            , MoveTo_StartZPos_finish

            , MoveTo_StartXPos_prechk = 40 //Fine or Coarse
            , MoveTo_StartXPos_prechk_finish
            , MoveTo_StartXPos_start
            , MoveTo_StartXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

            , ShutterOpen_prechk = 50
            , ShutterOpen_start
            , ShutterOpen_Finish

            // Laser Setting Check


            , DisableExternalGate_prechk = 60 //외부 게이트 제어 사용하지 않음.
            , DisableExternalGate_start
            , DisableExternalGate_finish

            , EnableExternalGate_prechk //외부 게이트 제어 사용하지 않음.
            , EnableExternalGate_start
            , EnableExternalGate_finish

  , DisableExternalTrigger_prechk
            , DisableExternalTrigger_start
            , DisableExternalTrigger_finish

            , EnableExternalTrigger_prechk
            , EnableExternalTrigger_start
            , EnableExternalTrigger_finish




            , Calc_Power_prechk = 100 //CSeqScanner::GetInstance()->GetEFreq_ByQFreqAndPower(m_nDefocusCrossHair_Freq, m_dDefocusCrossHair_Power)
            , Calc_Power_start
            , Calc_Power_finish

            , Set_Laser_EFreq_prechk = 110
            , Set_Laser_EFreq_start
            , Set_Laser_EFreq_finish

            , MoveToAttPos_prechk = 120
            , MoveToAttPos_start
            , MoveToAttPos_finish

            , Set_Laser_QFreq_prechk = 130 // Q-Frequency 값 설정, 파워 설정
            , Set_Laser_QFreq_start
            , Set_Laser_QFreq_finish

            , Set_A3200_prechk = 140
            , Set_A3200_start
            , Set_A3200_finish

            , PGM_Execute_prechk = 150
            , PGM_Execute_start
            , PGM_Execute_Run_Chk
            , PGM_Execute_Finish

            , ShutterClose_2nd_prechk = 160
            , ShutterClose_2nd_start
            , ShutterClose_2nd_Finish

            , Set_Laser_QFreq_To_Zero_prechk = 170 // Q-Frequency 값 설정, 파워 설정
            , Set_Laser_QFreq_To_Zero_start
            , Set_Laser_QFreq_To_Zero_finish

            , MoveTo_FinishZPos_prechk = 180 //Vision Z Pos Vision Focus offset Z Pos, ZigHeight, Thickness
            , MoveTo_FinishZPos_start
            , MoveTo_FinishZPos_finish

            , MoveTo_FinishXPos_prechk = 190 //Fine or Coarse
            , MoveTo_FinishXPos_start
            , MoveTo_FinishXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

        }
        #endregion
        #region [SEQ_FIND_FOCUS]

        public enum eSeqFindFocus
        {
            Finish = 0
            , Basic_Settings_start = 10
            , variables
            , Basic_Settings_finish

            //Mapping 1st Pos
            , MoveTo_StartPos_prechk = 30
            , MoveTo_StartPos_start
            , MoveTo_StartPos_finish

            , StartPos_Grab_start
            , StartPos_Grab_finish
            , StartPos_Matching
            , MoveTo_NewStartPos_start
            , MoveTo_NewStartPos_finish

            //Mapping
            , Mapping_prechk = 40
            , Mapping_start

            //X Direction first
            , Mapping_X_Dir_First_Rows_loop_prechk = 50
            , Mapping_X_Dir_First_Rows_loop_start
            , Mapping_X_Dir_First_Cols_loop_prechk = 60
            , Mapping_X_Dir_First_Cols_loop_start
            , Mapping_X_Dir_First_Grab_start
            , Mapping_X_Dir_First_Grab_finish
            , Mapping_X_Dir_First_Matching
            , Mapping_X_DIR_First_ManualTeaching
            , Mapping_X_Dir_First_Cols_loop_finish
            , Mapping_X_Dir_First_Rows_loop_finish

            //Y Direction first
            , Mapping_Y_Dir_First_Cols_loop_prechk = 70
            , Mapping_Y_Dir_First_Cols_loop_start
            , Mapping_Y_Dir_First_Rows_loop_prechk = 80
            , Mapping_Y_Dir_First_Rows_loop_start
            , Mapping_Y_Dir_First_Grab_start
            , Mapping_Y_Dir_First_Grab_finish
            , Mapping_Y_Dir_First_Matching
            , Mapping_Y_DIR_First_ManualTeaching
            , Mapping_Y_Dir_First_Rows_loop_finish
            , Mapping_Y_Dir_First_Cols_loop_finish

            , Mapping_finish
        }
        #endregion

        #region [ FOCUS FINDER]
        public void CreateFindFocusMap()
        {
            DeleteFindFocusMap();

            m_vecFindFocusMap = new List<List<CFindFocus>>();

            int nColunms = 0;

            //무조건 짝수로 만든다.
            if (FA.RCP.M100_FindFocusNumOfShot.AsInt < 4)
                nColunms = 2;
            else
            {

                nColunms = FA.RCP.M100_FindFocusNumOfShot.AsInt / 2;

                if (nColunms % 2 != 0)
                    nColunms++;

                nColunms = nColunms * 2 + 1;

            }
            m_nFindFocus_Columns = nColunms;
            m_nFindFocus_Rows = (int)(FA.RCP.M100_FindFocusZRange.AsDouble / FA.RCP.M100_FindFocusZPitch.AsDouble) * 2 + 1;

            double dMX = FA.AXIS.RX.Status().m_stPositionStatus.fActPos;
            double dMY = FA.AXIS.Y.Status().m_stPositionStatus.fActPos;
            double dMZ = FA.RCP.M100_LaserFocusZPos.AsDouble;
            for (int iRow = 0; iRow < m_nFindFocus_Rows; iRow++)
            {
                m_vecFindFocusMap.Add(new List<CFindFocus>());
                for (int iCol = 0; iCol < m_nFindFocus_Columns; iCol++)
                {
                    m_vecFindFocusMap[iRow].Add(new CFindFocus());
                    m_vecFindFocusMap[iRow][iCol].Init();
                    m_vecFindFocusMap[iRow][iCol].m_dEncX = dMX;
                    m_vecFindFocusMap[iRow][iCol].m_dEncY = dMY + (-m_nFindFocus_Rows / 2 + iRow) * FA.RCP.M100_FindFocusBeamPitchY.AsDouble;
                    m_vecFindFocusMap[iRow][iCol].m_dEncZ = dMZ - FA.RCP.M100_FindFocusZRange.AsDouble + iRow * FA.RCP.M100_FindFocusZPitch.AsDouble;
                    m_vecFindFocusMap[iRow][iCol].m_eStatus = GUI.UserControls.eCellStatus.OK;

                }
            }
            WinAPIs._PostMessageM(DEF.MSG_FIND_FOCUS_CREATE_MAP, m_nFindFocus_Columns, m_nFindFocus_Rows);
        }
        public void DeleteFindFocusMap()
        {
            if (m_vecFindFocusMap?.Count > 0)
                m_vecFindFocusMap.Clear();

            m_vecFindFocusMap = null;
        }

        public CFindFocus GetFindFocusMap(int colidx, int rowidx)
        {
            if (m_vecFindFocusMap == null)
                return null;

            if (m_vecFindFocusMap.Count == 0)
                return null;

            if (rowidx >= m_vecFindFocusMap.Count)
                return null;
            if (m_vecFindFocusMap[rowidx].Count <= colidx)
                return null;

            return m_vecFindFocusMap?[rowidx][colidx];
        }

        public bool SubSeq_FocusFinder()
        {
            if (!base.SetRecoveryRunInfo())
                return false;

            switch (CastTo<eMODULE_SEQ_FOCUS_FINDER>.From(m_stRun.nSubStep))
            {
                case eMODULE_SEQ_FOCUS_FINDER.Finish:
                    FA.LOG.Debug("SEQ", "Focus finder finish");
                    return true;
                case eMODULE_SEQ_FOCUS_FINDER.Basic_Settings_start:
                    FA.LOG.Debug("SEQ", "Focus finder start");
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.variables:
                    m_dFocusFinderTargetXPos = 0.0;
                    m_dFocusFinderTargetYPos = 0.0;
                    m_dFocusFinderTargetZPos = 0.0;
                    m_dFocusFinderStartActXPos = 0.0;
                    m_dFocusFinderStartActYPos = 0.0;
                    m_nFindFocus_Rows = 0;
                    m_nFindFocus_Columns = 0;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MakeMap:
                    CreateFindFocusMap();
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MakePGM_prechk:
                    if (FA.RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st))
                    {
                        base.NextSubStep();
                    }
                    else
                    {
                        base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MakePGM);
                    }
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MakePGM_makeReadyStatus:
                    FA.RTC5.Instance.ListStop();
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MakePGM_makeReadyStatuschk:
                    if (FA.RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st))
                        break;

                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MakePGM:
                    // set Condition					
                    m_FocusPGM.Set_Focus_Condition_BeamPitchXY(FA.RCP.M100_FindFocusBeamPitchX.AsDouble, FA.RCP.M100_FindFocusBeamPitchY.AsDouble);
                    m_FocusPGM.Set_Focus_Condition_Search(AXIS.RZ.Status().m_stPositionStatus.fActPos, FA.RCP.M100_FindFocusZRange.AsDouble, FA.RCP.M100_FindFocusZPitch.AsDouble); // Z 축 위치 설정.
                    m_FocusPGM.Set_NumOfShots(FA.RCP.M100_FindFocusNumOfShot.AsInt);
                    m_FocusPGM.SetGalvoCalPath(FA.RCP.M100_GALVO_CAL_PATH.ToString());
                    m_FocusPGM.SetGalvoCondition(FA.DEF.eAxesName.RA.ToString(), FA.DEF.eAxesName.RB.ToString(), FA.RCP.M100_FindFocusMarkingVelocity.AsDouble, FA.RCP.M100_FindFocusMarkingAccel.AsDouble);
                    m_FocusPGM.SetGalvoRotate(0); //CPU에서 계산된 Angle 값 입력.
                    m_FocusPGM.SetLaserFreq(FA.RCP.M100_FindFocusLaserFrq.AsDouble * 1000.0, 0.5);
                    m_FocusPGM.SetLaserMode(1);
                    //	m_FocusPGM.SetPSOMode(true);
                    //	m_FocusPGM.SetStageXYCondition(FA.DEF.eAxesName.RX.ToString(), FA.DEF.eAxesName.Y.ToString(), FA.RCP.M100_FindFocusStageVelocity.AsDouble, FA.RCP.M100_FindFocusStageAccel.AsDouble);
                    //	m_FocusPGM.SetStageZCondition(FA.DEF.eAxesName.RZ.ToString(), 5, 50);
                    /// set axis move profile
                    m_FocusPGM.Make_Pgm();
                    base.NextSubStep();
                    break;

                case eMODULE_SEQ_FOCUS_FINDER.Basic_Settings_finish:
                    if (!FA.RTC5.Instance.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._1st))
                        //base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.ShutterClose_prechk);
                        base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartZPos_prechk);
                    break;
                #region Shutter isn't Exist ,Shutter SEQ Not Necessary
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_start:
                    ACT.CYL_LaserShutter_Close.Run();
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_Finish:
                    if (!ACT.CYL_LaserShutter_Close.Check())
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartZPos_prechk);
                    break;
                #endregion

                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartZPos_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartZPos_start:
                    m_dCrossHairTargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

                    ACT.MoveABS(DEF.eAxesName.RZ, m_dCrossHairTargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    base.NextSubStep();

                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartZPos_finish:
                    if (!AXIS.RZ.Status().IsMotionDone)
                        break;
                    //if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
                    //		break;

                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartXPos_prechk);

                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartXPos_prechk:
                    if (base.IsRunModeStopped()) break;
                    if (ACT.CYL_STOPPER_L_DOWN.CurrentStatus() == false)
                        ACT.CYL_STOPPER_L_DOWN.Run();

                    if (ACT.CYL_STOPPER_CENTER_DOWN.CurrentStatus() == false)
                        ACT.CYL_STOPPER_CENTER_DOWN.Run();

                    if (ACT.CYL_STOPPER_R_DOWN.CurrentStatus() == false)
                        ACT.CYL_STOPPER_R_DOWN.Run();

                    if (ACT.CYL_JIG_ACC_DOWN.CurrentStatus() == false)
                        ACT.CYL_JIG_ACC_DOWN.Run();

                    // JIG Acc Cylinder Operation Check
                    // Stopper Cylinder Operation Check 
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartXPos_prechk_finish:
                    if (ACT.CYL_STOPPER_L_DOWN.Check() == false)
                        break;

                    if (ACT.CYL_STOPPER_CENTER_DOWN.Check() == false)
                        break;

                    if (ACT.CYL_STOPPER_R_DOWN.Check() == false)
                        break;

                    if (ACT.CYL_JIG_ACC_DOWN.Check() == false)
                        break;

                    base.NextSubStep();
                    break;

                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartXPos_start:

                    m_dFocusFinderStartActXPos = AXIS.RX.Status().m_stPositionStatus.fActPos;
                    m_dFocusFinderStartActYPos = AXIS.Y.Status().m_stPositionStatus.fActPos;

                    m_dFocusFinderTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble;
                    m_dFocusFinderTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble;
                    ACT.MoveABS(DEF.eAxesName.RX, m_dFocusFinderTargetXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    ACT.MoveABS(DEF.eAxesName.Y, m_dFocusFinderTargetYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_StartXPos_finish:
                    if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                        break;

                    //if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
                    //		break;
                    //if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
                    //		break;

                    //base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.ShutterOpen_prechk);
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.EnableExternalGate_prechk);
                    break;
                #region Shutter isn't Exist ,Shutter SEQ Not Necessary
                case eMODULE_SEQ_FOCUS_FINDER.ShutterOpen_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterOpen_start:
                    ACT.CYL_LaserShutter_Open.Run();
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterOpen_Finish:
                    if (!ACT.CYL_LaserShutter_Open.Check())
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.DisableExternalGate_prechk);
                    break;
                #endregion

                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalGate_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalGate_start:
                    if (!FA.LASER.Instance.IsGateOpen)
                    {
                        FA.LASER.Instance.GateOpen = true;
                        base.NextSubStep();
                    }
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalGate_finish:
                    if (!FA.LASER.Instance.IsGateOpen)
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Calc_Power_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalGate_prechk:
                    if (base.IsRunModeStopped()) break;
                    if (FA.LASER.Instance.GateMode == Laser.GATE_MODE.EXT)
                    {
                        base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.EnableExternalGate_finish);
                    }
                    else
                    {
                        base.NextSubStep();
                    }
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalGate_start:
                    FA.LASER.Instance.GateMode = Laser.GATE_MODE.EXT;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalGate_finish:
                    if (FA.LASER.Instance.GateMode != Laser.GATE_MODE.EXT)
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_prechk);
                    break;

                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalTrigger_prechk:
                    FA.LASER.Instance.GateMode = Laser.GATE_MODE.INT;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalTrigger_start:
                    if (FA.LASER.Instance.GateMode == Laser.GATE_MODE.EXT)
                        break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.DisableExternalTrigger_finish:
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_prechk);
                    break;

                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_prechk:
                    if (base.IsRunModeStopped()) break;
                    if (FA.LASER.Instance.TriggerMode == Laser.TRIG_MODE.EXT)
                    {
                        base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_finish);
                    }
                    else
                    {
                        base.NextSubStep();
                    }
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_start:
                    FA.LASER.Instance.TriggerMode = Laser.TRIG_MODE.EXT;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.EnableExternalTrigger_finish:
                    if (FA.LASER.Instance.TriggerMode != Laser.TRIG_MODE.EXT)
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Calc_Power_prechk);
                    break;

                case eMODULE_SEQ_FOCUS_FINDER.Calc_Power_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Calc_Power_start:
                    //To do list : GetEFreq_ByQFreqAndPower() 참고
                    //PowerTable PwrTable = new PowerTable(FA.FILE.InitProcPowerTable);
                    Laser.LaserPwrTableData pPowerTable = FA.MGR.LaserMgr.GetPwrTableData(FA.RCP.M100_FindFocusLaserFrq.AsInt);
                    if (pPowerTable != null)
                    {
                        if (pPowerTable.GetPercentFromPower(FA.RCP.M100_FindFocusLaserPwr.AsDouble, out m_dFocusFinderPowerPerecent) != 1)
                        {
                            base.m_stRun.TimeoutNow();
                            break;
                        }
                    }
                    else
                    {
                        base.m_stRun.TimeoutNow();
                        break;
                    }
                    base.NextSubStep();
                    break;

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                case eMODULE_SEQ_FOCUS_FINDER.Calc_Power_finish:
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Set_Laser_EFreq_prechk);
                    break;


                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_EFreq_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_EFreq_start:
                    //FA.LASER.Instance.EPRF = FA.RCP.M100_FindFocusLaserFrq.AsDouble * 1000.0;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_EFreq_finish:
                    //if (!FA.LASER.Instance.EPRF.IsSame(FA.RCP.M100_FindFocusLaserFrq.AsDouble * 1000.0))
                    //	break;

                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveToAttPos_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveToAttPos_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveToAttPos_start:
                    FA.ATT.LPA.fAngle = m_dFocusFinderATTPos;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveToAttPos_finish:
                    if (!FA.ATT.LPA.fAngle.IsSame(m_dFocusFinderATTPos))
                        break;

                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_start:
                    FA.LASER.Instance.RepetitionRate = 0;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_finish:
                    if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Set_A3200_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_A3200_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_A3200_start:
                    Motion.CMotionA3200.StopProgram(1);
                    // 					Motion.CMotionA3200.SetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
                    // 					Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_A3200_finish:
                    //Motion.CMotionA3200.GetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
                    //Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.PGM_Execute_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.PGM_Execute_prechk:
                    if (base.IsRunModeStopped()) break;
                    //if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete && Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.Idle)
                    //{
                    //		m_stRun.TimeoutNow();
                    //			break;
                    //	}
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.PGM_Execute_start:
                    //Motion.CMotionA3200.ProgramRun(1, m_FocusPGM.Get_FilePath());
                    m_stRun.stwatchForSub.SetDelay = 2000;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.PGM_Execute_Run_Chk:
                    if (!m_stRun.stwatchForSub.IsDone)
                        break;

                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.PGM_Execute_Finish:

                    //if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete)
                    //		break;

                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.ShutterClose_2nd_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_2nd_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_2nd_start:
                    FA.ACT.CYL_LaserShutter_Close.Run();
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.ShutterClose_2nd_Finish:
                    if (!FA.ACT.CYL_LaserShutter_Close.Check())
                        break;

                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_To_Zero_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_To_Zero_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_To_Zero_start:
                    FA.LASER.Instance.RepetitionRate = 0;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.Set_Laser_QFreq_To_Zero_finish:
                    if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishZPos_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishZPos_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishZPos_start:
                    m_dFocusFinderTargetZPos = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;
                    ACT.MoveABS(DEF.eAxesName.RZ, m_dFocusFinderTargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishZPos_finish:
                    if (!AXIS.RZ.Status().IsMotionDone)
                        break;
                    if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishXPos_prechk);
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishXPos_prechk:
                    if (base.IsRunModeStopped()) break;
                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishXPos_start:
                    // 					m_dFocusFinderTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset;
                    // 					m_dFocusFinderTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset;
                    // 					ACT.MoveABS(DEF.eAxesName.RX, m_dFocusFinderTargetXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    // 					ACT.MoveABS(DEF.eAxesName.Y, m_dFocusFinderTargetYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    ACT.MoveABS(DEF.eAxesName.RX, m_dFocusFinderStartActXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
                    ACT.MoveABS(DEF.eAxesName.Y, m_dFocusFinderStartActYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);


                    base.NextSubStep();
                    break;
                case eMODULE_SEQ_FOCUS_FINDER.MoveTo_FinishXPos_finish:
                    if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                        break;
                    if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
                        break;

                    if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
                        break;
                    base.NextSubStep(eMODULE_SEQ_FOCUS_FINDER.Finish);
                    break;
            }
            SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Scanner_CrossHair + m_stRun.nSubStep);
            return false;
        }
        #endregion
    }
}
