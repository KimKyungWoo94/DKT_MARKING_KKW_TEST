using System.Collections.Generic;
using EzIna.FA;

namespace EzIna
{
		public partial class RunningScanner
		{

				#region [ GALVO CAL ]

				public void CreateGalvoMap()
				{
						DeleteGalvoMap();

						m_vecGalvoMap = new List<List<CMapping>>();

						m_nGalvoCal_Columns = FA.RCP.M100_GalvoCalNumOfGridX.AsInt;
						m_nGalvoCal_Rows = FA.RCP.M100_GalvoCalNumOfGridY.AsInt;
						for (int iRow = 0; iRow < m_nGalvoCal_Rows; iRow++)
						{
								m_vecGalvoMap.Add(new List<CMapping>());
								for (int iCol = 0; iCol < m_nGalvoCal_Columns; iCol++)
								{
										m_vecGalvoMap[iRow].Add(new CMapping());
										m_vecGalvoMap[iRow][iCol].Init();

								}
						}

						WinAPIs._PostMessageM(DEF.MSG_GALVO_CAL_CREATE_MAP, m_nGalvoCal_Columns, m_nGalvoCal_Rows);
				}
				public void DeleteGalvoMap()
				{
						if (m_vecGalvoMap?.Count > 0)
								m_vecGalvoMap.Clear();

						m_vecGalvoMap = null;
				}

				public CMapping GetGalvoCalMap(int colidx, int rowidx)
				{
						if (m_vecGalvoMap == null)
								return null;

						if (m_vecGalvoMap.Count == 0)
								return null;

						if (rowidx >= m_vecGalvoMap.Count)
								return null;
						if (m_vecGalvoMap[rowidx].Count >= colidx)
								return null;

						return m_vecGalvoMap?[rowidx][colidx];
				}
				#region [ GALVO CAL ]
				//Galvo_Calibration_Pgm m_Galvo_CAL_PGM = new Galvo_Calibration_Pgm();
				Galvo_Calibration_RTC m_Galvo_CAL_PGM = new Galvo_Calibration_RTC();
				List<List<CMapping>> m_vecGalvoMap = null;

				double m_dGalvoCal_StartX;
				double m_dGalvoCal_StartY;
				int m_nGalvoCal_ColIndex;
				int m_nGalvoCal_RowIndex;
				int m_nGalvoCal_CurrCol;
				int m_nGalvoCal_CurrRow;
				int m_nGalvoCal_Columns;
				int m_nGalvoCal_Rows;


				bool m_bGalvoCal_Pause;
				bool m_bGalvoCal_Continue;
				double m_dGalvoCalATTPos;


				//Manual teaching할 경우 사용되는 변수.
				public double dGalvoCal_ManualTeachingPosX { get; set; }
				public double dGalvoCal_ManualTeachingPosY { get; set; }

				public bool bGalvoCal_Pause
				{
						get { return m_bGalvoCal_Pause; }
						set { m_bGalvoCal_Pause = value; }
				}

				public bool bGalvoCal_continue
				{
						get { return m_bGalvoCal_Pause; }
						set { m_bGalvoCal_Pause = value; }
				}
				public enum eMODULE_SEQ_GALVO_CAL
				{
						Finish = 0
					, Basic_Settings_start = 10
					, variables
					, ChangeLaserMode
					, MakePGM
					, VisionLive_start_prechk = 20
					, VisionLive_finish
					, illumination_start
					, illumination_finish
					, camera_expose_start
					, camera_expose_finish
					, Vacuum
					, Basic_Settings_finish

					, ShutterClose_prechk = 30
					, ShutterClose_start
					, ShutterClose_Finish

					//Move to powermeter position
					, MoveTo_StartZPos_prechk = 40 //Laser Focus Z Pos, ZigHeight, Thickness
					, MoveTo_StartZPos_start
					, MoveTo_StartZPos_finish

					, MoveTo_StartXPos_prechk = 50 //Fine or Coarse
					, MoveTo_StartXPos_start
					, MoveTo_StartXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

					, ShutterOpen_prechk = 60
					, ShutterOpen_start
					, ShutterOpen_Finish

					, DisableExternalGate_prechk = 70 //외부 게이트 제어 사용하지 않음.
					, DisableExternalGate_start
					, DisableExternalGate_finish

					, Calc_Power_prechk = 80 //CSeqScanner::GetInstance()->GetEFreq_ByQFreqAndPower(m_nDefocusCrossHair_Freq, m_dDefocusCrossHair_Power)
					, Calc_Power_start
					, Calc_Power_finish

					, Set_Laser_EFreq_prechk = 90
					, Set_Laser_EFreq_start
					, Set_Laser_EFreq_finish

					, MoveToAttPos_prechk = 100
					, MoveToAttPos_start
					, MoveToAttPos_finish

					, Set_Laser_QFreq_prechk = 110 // Q-Frequency 값 설정, 파워 설정
					, Set_Laser_QFreq_start
					, Set_Laser_QFreq_finish

					, Set_Laser_GateEnable_prechk = 120
					, Set_Laser_GateEnable_start
					, Set_Laser_GateEnable_finish

					, Set_A3200_prechk = 130
					, Set_A3200_start
					, Set_A3200_finish

					, PGM_Execute_prechk = 140
					, PGM_Execute_start
					, PGM_Execute_Run_Chk
					, PGM_Execute_Finish

					, ShutterClose_2nd_prechk = 150
					, ShutterClose_2nd_start
					, ShutterClose_2nd_Finish

					, Set_Laser_QFreq_To_Zero_prechk = 160 // Q-Frequency 값 설정, 파워 설정
					, Set_Laser_QFreq_To_Zero_start
					, Set_Laser_QFreq_To_Zero_finish

					, MoveTo_FinishZPos_prechk = 170 //Vision Z Pos Vision Focus offset Z Pos, ZigHeight, Thickness
					, MoveTo_FinishZPos_start
					, MoveTo_FinishZPos_finish

					, MoveTo_FinishXPos_prechk = 180 //Fine or Coarse
					, MoveTo_FinishXPos_start
					, MoveTo_FinishXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

					//[Mapping Start]
					//Mapping 1st Pos
					, MoveTo_StartPos_prechk = 190
					, MoveTo_StartPos_start
					, MoveTo_StartPos_finish

					//Mapping
					, Mapping_prechk = 200
					, Mapping_start

					//X Direction first
					, Mapping_X_Dir_First_Rows_loop_prechk = 210
					, Mapping_X_Dir_First_Rows_loop_start
					, Mapping_X_Dir_First_Cols_loop_prechk = 220
					, Mapping_X_Dir_First_Cols_loop_start
					, Mapping_X_Dir_First_Cols_loop_Move_start
					, Mapping_X_Dir_First_Cols_loop_Move_finish
					, Mapping_X_Dir_First_Grab_start
					, Mapping_X_Dir_First_Grab_finish
					, Mapping_X_Dir_First_Matching
					, Mapping_X_DIR_First_ManualTeaching
					, Mapping_X_Dir_First_Cols_loop_finish
					, Mapping_X_Dir_First_Rows_loop_finish

					//Y Direction first
					, Mapping_Y_Dir_First_Cols_loop_prechk = 230
					, Mapping_Y_Dir_First_Cols_loop_start
					, Mapping_Y_Dir_First_Rows_loop_prechk = 240
					, Mapping_Y_Dir_First_Rows_loop_start
					, Mapping_Y_Dir_First_Rows_loop_Move_start
					, Mapping_Y_Dir_First_Rows_loop_Move_finish
					, Mapping_Y_Dir_First_Grab_start
					, Mapping_Y_Dir_First_Grab_finish
					, Mapping_Y_Dir_First_Matching
					, Mapping_Y_DIR_First_ManualTeaching
					, Mapping_Y_Dir_First_Rows_loop_finish
					, Mapping_Y_Dir_First_Cols_loop_finish

					, Mapping_finish = 250

				}
				#endregion
				public bool SubSeq_Galvo_Cal()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eMODULE_SEQ_GALVO_CAL>.From(m_stRun.nSubStep))
						{
								case eMODULE_SEQ_GALVO_CAL.Finish:
										FA.LOG.Debug("SEQ", "Focus finder finish");
										return true;
								case eMODULE_SEQ_GALVO_CAL.Basic_Settings_start:
										FA.LOG.Debug("SEQ", "Focus finder start");
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.variables:
										m_dCrossHairTargetXPos = 0.0;
										m_dCrossHairTargetZPos = 0.0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ChangeLaserMode:
										// set Condition					
										m_Galvo_CAL_PGM.Set_FovSize(FA.RCP.M100_GalvoCalFOVSize.AsDouble);
										m_Galvo_CAL_PGM.Set_GridXY(FA.RCP.M100_GalvoCalNumOfGridX.AsInt, FA.RCP.M100_GalvoCalNumOfGridY.AsInt);
										m_Galvo_CAL_PGM.Set_MarkingOffset(0.5);
										m_Galvo_CAL_PGM.SetGalvoCalPath(FA.RCP.M100_GALVO_CAL_PATH.ToString());
										m_Galvo_CAL_PGM.SetGalvoCondition(FA.DEF.eAxesName.RA.ToString(), FA.DEF.eAxesName.RB.ToString(), FA.RCP.M100_GalvoCalMarkingVelocity.AsDouble, FA.RCP.M100_GalvoCalMarkingAccel.AsDouble);
										m_Galvo_CAL_PGM.SetGalvoRotate(FA.RCP.M100_CPUGalvoRotate.AsDouble);
										m_Galvo_CAL_PGM.SetLaserFreq(FA.RCP.M100_GalvoCalLaserFrq.AsDouble * 1000.0, 0.5);
										m_Galvo_CAL_PGM.SetLaserMode(1);
										m_Galvo_CAL_PGM.SetPSOMode(true);
										m_Galvo_CAL_PGM.SetStageXYCondition(FA.DEF.eAxesName.RX.ToString(), FA.DEF.eAxesName.Y.ToString(), FA.RCP.M100_GalvoCalStageVelocity.AsDouble, FA.RCP.M100_GalvoCalStageAccel.AsDouble);
										m_Galvo_CAL_PGM.SetStageZCondition(FA.DEF.eAxesName.RZ.ToString(), 5, 50);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MakePGM:
										m_Galvo_CAL_PGM.Make_Pgm();
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.VisionLive_start_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.VisionLive_start_prechk:
										{
												VISION.FINE_CAM.Live();
												base.NextSubStep();
												break;
										}
								case eMODULE_SEQ_GALVO_CAL.VisionLive_finish:
										{
												if (!VISION.FINE_CAM.IsLive())
														break;

												base.NextSubStep();
												break;
										}
								case eMODULE_SEQ_GALVO_CAL.illumination_start:
										{
												//[To Do List] 옵션에 따라서 Coarse, Fine 구분할 것.
												FA.LIGHTSOURCE.RING.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Ring_For_Run.AsInt);
												FA.LIGHTSOURCE.SPOT.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Spot_For_Run.AsInt);
												m_stRun.stwatchForSub.SetDelay = 100;
												base.NextSubStep();
												break;
										}

								case eMODULE_SEQ_GALVO_CAL.illumination_finish:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;
												base.NextSubStep();
												break;
										}
								case eMODULE_SEQ_GALVO_CAL.camera_expose_start:
										{
												FA.VISION.FINE_CAM.SetExposeTime(FA.RCP.F_CAM_ExposeTime.AsInt);
												base.NextSubStep();
												break;
										}
								case eMODULE_SEQ_GALVO_CAL.camera_expose_finish:
										{
												if (!VISION.FINE_CAM.GetExpose().IsSame(FA.RCP.F_CAM_ExposeTime.AsInt))
														break;
												base.NextSubStep();
												break;
										}
								case eMODULE_SEQ_GALVO_CAL.Vacuum:
										{
												//ACT.StageVauccmOn.Run(100);
												base.NextSubStep();
										}
										break;

								case eMODULE_SEQ_GALVO_CAL.Basic_Settings_finish:
										//if (!ACT.StageVauccmOn.Check() && !FA.OPT.DryRunningEnable)
										//	break;
										//base.NextSubStep(eMODULE_SEQ_GALVO_CAL.ShutterClose_prechk);
										break;
								#region [ Make Grid]
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_start:
										ACT.CYL_LaserShutter_Close.Run();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_Finish:
										if (!ACT.CYL_LaserShutter_Close.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveTo_StartZPos_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartZPos_start:
										if (FA.OPT.UseAutoFocus)
												m_dCrossHairTargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;
										else
												m_dCrossHairTargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

										ACT.MoveABS(DEF.eAxesName.RZ, m_dCrossHairTargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveTo_StartXPos_prechk);

										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();

										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartXPos_start:
										if (m_eSelectedVision == DEF.eVision.COARSE)
										{
												//스캐너가 카메라 위치로 이동하기 전에 현재의 위치를 기록한다.
												FA.RCP.M100_CrossHairScannerRefPosX.m_strValue = string.Format("{0}", AXIS.RX.Status().m_stPositionStatus.fActPos);
												FA.RCP.M100_CrossHairScannerRefPosY.m_strValue = string.Format("{0}", AXIS.Y.Status().m_stPositionStatus.fActPos);

												m_dCrossHairTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.AsDouble;
												m_dCrossHairTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.AsDouble;
										}
										else if (m_eSelectedVision == DEF.eVision.FINE)
										{
												FA.RCP.M100_CrossHairScannerRefPosX.m_strValue = string.Format("{0}", AXIS.RX.Status().m_stPositionStatus.fActPos);
												FA.RCP.M100_CrossHairScannerRefPosY.m_strValue = string.Format("{0}", AXIS.Y.Status().m_stPositionStatus.fActPos);

												m_dCrossHairTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble;
												m_dCrossHairTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble;
										}
										else
										{
												m_stRun.TimeoutNow();
												break;
										}
										ACT.MoveABS(DEF.eAxesName.RX, m_dCrossHairTargetXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										ACT.MoveABS(DEF.eAxesName.Y, m_dCrossHairTargetYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();

										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.ShutterOpen_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterOpen_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterOpen_start:
										ACT.CYL_LaserShutter_Open.Run();
										base.NextSubStep();

										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterOpen_Finish:
										if (!ACT.CYL_LaserShutter_Open.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.DisableExternalGate_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.DisableExternalGate_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.DisableExternalGate_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.DisableExternalGate_finish:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Calc_Power_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Calc_Power_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Calc_Power_start:
										//To do list : GetEFreq_ByQFreqAndPower() 참고
										PowerTable PwrTable = new PowerTable(FA.FILE.InitProcPowerTable);
										if (PwrTable.GetPulseWidthByFreqnPower(FA.RCP.M100_GalvoCalLaserFrq.AsInt, FA.RCP.M100_GalvoCalLaserPwr.AsDouble, out m_dGalvoCalATTPos) == -1)
										{
												base.m_stRun.TimeoutNow();
												break;
										}
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Calc_Power_finish:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Set_Laser_EFreq_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_EFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_EFreq_start:
										FA.LASER.Instance.EPRF = FA.RCP.M100_GalvoCalLaserFrq.AsDouble * 1000.0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_EFreq_finish:
										if (!FA.LASER.Instance.EPRF.IsSame(FA.RCP.M100_GalvoCalLaserFrq.AsDouble * 1000.0))
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveToAttPos_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveToAttPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveToAttPos_start:
										FA.ATT.LPA.fAngle = m_dGalvoCalATTPos;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveToAttPos_finish:
										if (!FA.ATT.LPA.fAngle.IsSame(m_dGalvoCalATTPos, 0.02))
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_start:
										FA.LASER.Instance.RepetitionRate = 0;//FA.RCP.M100_CrossHairLaserFrq * 1000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Set_Laser_GateEnable_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_GateEnable_prechk:
										if (base.IsRunModeStopped())
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_GateEnable_start:
										FA.LASER.Instance.GateOpen = true;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_GateEnable_finish:
										if (!FA.LASER.Instance.IsGateOpen)
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Set_A3200_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_A3200_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_A3200_start:
										// 					Motion.CMotionA3200.SetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										// 					Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										Motion.CMotionA3200.StopProgram(1);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_A3200_finish:
										//Motion.CMotionA3200.GetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										//Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.PGM_Execute_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.PGM_Execute_prechk:
										if (base.IsRunModeStopped()) break;
										//if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete && Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.Idle)
										//{
										//		break;
										//}
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.PGM_Execute_start:
										Motion.CMotionA3200.ProgramRun(1, m_Galvo_CAL_PGM.Get_FilePath());
										m_stRun.stwatchForSub.SetDelay = 2000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.PGM_Execute_Run_Chk:
										if (!m_stRun.stwatchForSub.IsDone)
												break;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.PGM_Execute_Finish:

										//if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete)
										//{
												//m_stRun.TimeoutNow();
										//		break;
										//}

										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.ShutterClose_2nd_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_2nd_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_2nd_start:
										FA.ACT.CYL_LaserShutter_Close.Run();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.ShutterClose_2nd_Finish:
										if (!FA.ACT.CYL_LaserShutter_Open.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_To_Zero_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_To_Zero_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_To_Zero_start:
										FA.LASER.Instance.RepetitionRate = 0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Set_Laser_QFreq_To_Zero_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveTo_FinishZPos_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishZPos_start:
										if (m_eSelectedVision == DEF.eVision.FINE)
										{
												if (FA.OPT.UseAutoFocus)
														m_dCrossHairTargetZPos = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;
												else
														m_dCrossHairTargetZPos = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;
										}
										else if (m_eSelectedVision == DEF.eVision.COARSE)
										{
												if (FA.OPT.UseAutoFocus)
														m_dCrossHairTargetZPos = FA.RCP.M100_CoarseVisionFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;
												else
														m_dCrossHairTargetZPos = FA.RCP.M100_CoarseVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

										}
										else
										{
												m_stRun.TimeoutNow();
												break;
										}

										ACT.MoveABS(DEF.eAxesName.RZ, m_dCrossHairTargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveTo_FinishXPos_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishXPos_start:
										if (m_eSelectedVision == DEF.eVision.COARSE)
										{
												m_dCrossHairTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.AsDouble;
												m_dCrossHairTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.AsDouble;
										}
										else if (m_eSelectedVision == DEF.eVision.FINE)
										{
												m_dCrossHairTargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble;
												m_dCrossHairTargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos - FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble;
										}
										else
										{
												m_stRun.TimeoutNow();
												break;
										}

										FA.ACT.MoveABS(FA.DEF.eAxesName.RX, m_dCrossHairTargetXPos, Motion.GDMotion.eSpeedType.RUN);
										FA.ACT.MoveABS(FA.DEF.eAxesName.Y, m_dCrossHairTargetYPos, Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_FinishXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.MoveTo_StartPos_prechk);
										break;
								#endregion [ Make Grid]

								#region [Galvo Cal]
								//Mapping 1st Pos
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartPos_prechk:
										if (base.IsRunModeStopped())
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartPos_start:
										{
												double dPitchX = 0.0, dPitchY = 0.0;
												dPitchX = FA.RCP.M100_GalvoCalFOVSize.AsDouble / FA.RCP.M100_GalvoCalNumOfGridX.AsDouble;
												dPitchY = FA.RCP.M100_GalvoCalFOVSize.AsDouble / FA.RCP.M100_GalvoCalNumOfGridY.AsDouble;

												m_dGalvoCal_StartX = ((FA.RCP.M100_GalvoCalNumOfGridX.AsInt / 2.0) * dPitchX) * -1;
												m_dGalvoCal_StartY = ((FA.RCP.M100_GalvoCalNumOfGridX.AsInt / 2.0) * dPitchX) * -1;

												FA.ACT.MoveABS(FA.RCP.M100_FineVisionFocusZPos);
												FA.ACT.MoveABS(FA.DEF.eAxesName.RX, m_dGalvoCal_StartX, Motion.GDMotion.eSpeedType.RUN);
												FA.ACT.MoveABS(FA.DEF.eAxesName.Y, m_dGalvoCal_StartY, Motion.GDMotion.eSpeedType.RUN);
										}
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.MoveTo_StartPos_finish:
										if (!FA.AXIS.RX.Status().IsMotionDone || !FA.AXIS.Y.Status().IsMotionDone || !FA.AXIS.RZ.Status().IsMotionDone) break;
										if (!FA.AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(m_dGalvoCal_StartX, FA.DEF.IN_POS)) break;
										if (!FA.AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(m_dGalvoCal_StartY, FA.DEF.IN_POS)) break;
										if (!FA.ACT.InPostion(FA.RCP.M100_FineVisionFocusZPos)) break;

										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Mapping_prechk);
										break;
								//Mapping
								case eMODULE_SEQ_GALVO_CAL.Mapping_prechk:
										if (base.IsRunModeStopped())
												break;
										//여기서 Galvo Cal 맵데이터를 생성한다.
										CreateGalvoMap();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_start:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Rows_loop_prechk);
										break;
								//X Direction first
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Rows_loop_prechk:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Rows_loop_start:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_prechk);
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_prechk:
										if (base.IsRunModeStopped())
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_Move_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_Move_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Grab_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Grab_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Matching:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_DIR_First_ManualTeaching:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Cols_loop_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_X_Dir_First_Rows_loop_finish:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Cols_loop_prechk);
										break;
								//Y Direction first
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Cols_loop_prechk:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Cols_loop_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Rows_loop_prechk:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Rows_loop_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Rows_loop_Move_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Rows_loop_Move_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Grab_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Grab_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Matching:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_DIR_First_ManualTeaching:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Rows_loop_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_Y_Dir_First_Cols_loop_finish:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_GALVO_CAL.Mapping_finish:
										base.NextSubStep(eMODULE_SEQ_GALVO_CAL.Finish);
										break;
										#endregion [Galvo Cal]
						}
						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Scanner_Cal + m_stRun.nSubStep);
						return false;
				}
				#endregion
		}
}

