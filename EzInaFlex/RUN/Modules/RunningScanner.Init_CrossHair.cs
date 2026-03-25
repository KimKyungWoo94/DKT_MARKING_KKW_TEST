using EzIna.FA;
namespace EzIna
{
		public partial class RunningScanner
		{

				//CrossHair_Pgm m_CrossHairPGM = new CrossHair_Pgm();
				CrossHair_RTC m_CrossHairPGM = new CrossHair_RTC();
				double m_dCrossHairTargetXPos = 0.0;
				double m_dCrossHairTargetYPos = 0.0;
				double m_dCrossHairTargetZPos = 0.0;
				double m_dCrossHairStartActXPos = 0.0;
				double m_dCrossHairStartActYPos = 0.0;
				double m_dCrossHairATTPos = 0.0;


				public FA.DEF.eVision m_eSelectedVision { get; set; }
				bool m_bUseDefocus;
				bool m_bUseFocus;



				public void SetDefocus(bool a_bUse)
				{
						m_bUseDefocus = a_bUse;
				}
				public void Setfocus(bool a_bUse)
				{
						m_bUseFocus = a_bUse;
				}


				public enum eMODULE_SEQ_CROSS_HAIR
				{
						Finish = 0
					, Basic_Settings_start = 10
					, variables
					, ChangeLaserMode
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
					, MoveTo_StartXPos_start
					, MoveTo_StartXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

					, ShutterOpen_prechk = 50
					, ShutterOpen_start
					, ShutterOpen_Finish

					, DisableExternalGate_prechk = 60 //외부 게이트 제어 사용하지 않음.
					, DisableExternalGate_start
					, DisableExternalGate_finish

					, Calc_Power_prechk = 70 //CSeqScanner::GetInstance()->GetEFreq_ByQFreqAndPower(m_nDefocusCrossHair_Freq, m_dDefocusCrossHair_Power)
					, Calc_Power_start
					, Calc_Power_finish

					, Set_Laser_EFreq_prechk = 80
					, Set_Laser_EFreq_start
					, Set_Laser_EFreq_finish

					, MoveToAttPos_prechk = 90
					, MoveToAttPos_start
					, MoveToAttPos_finish

					, Set_Laser_QFreq_prechk = 100 // Q-Frequency 값 설정, 파워 설정
					, Set_Laser_QFreq_start
					, Set_Laser_QFreq_finish

					, Set_Laser_GateEnable_prechk = 110
					, Set_Laser_GateEnable_start
					, Set_Laser_GateEnable_finish

					, Set_A3200_prechk = 120
					, Set_A3200_start
					, Set_A3200_finish

					, PGM_Execute_prechk = 130
					, PGM_Execute_start
					, PGM_Execute_Run_Chk
					, PGM_Execute_Finish

					, ShutterClose_2nd_prechk = 140
					, ShutterClose_2nd_start
					, ShutterClose_2nd_Finish

					, Set_Laser_QFreq_To_Zero_prechk = 150 // Q-Frequency 값 설정, 파워 설정
					, Set_Laser_QFreq_To_Zero_start
					, Set_Laser_QFreq_To_Zero_finish

					, MoveTo_FinishZPos_prechk = 160 //Vision Z Pos Vision Focus offset Z Pos, ZigHeight, Thickness
					, MoveTo_FinishZPos_start
					, MoveTo_FinishZPos_finish

					, MoveTo_FinishXPos_prechk = 170 //Fine or Coarse
					, MoveTo_FinishXPos_start
					, MoveTo_FinishXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

				}


				#region [ CROSS HAIR ]
				public bool SubSeq_CrossHair()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eMODULE_SEQ_CROSS_HAIR>.From(m_stRun.nSubStep))
						{
								case eMODULE_SEQ_CROSS_HAIR.Finish:
										FA.LOG.Debug("SEQ", "Cross hair finish");
										return true;
								case eMODULE_SEQ_CROSS_HAIR.Basic_Settings_start:
										FA.LOG.Debug("SEQ", "Cross hair start");
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.variables:
										m_dCrossHairTargetXPos = 0.0;
										m_dCrossHairTargetYPos = 0.0;

										m_dCrossHairTargetZPos = 0.0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ChangeLaserMode:
										// set Condition					
										m_CrossHairPGM.SetCrossLine_distance(FA.RCP.M100_CrossHairMarkingDist.AsDouble);
										m_CrossHairPGM.SetGalvoCalPath(FA.RCP.M100_GALVO_CAL_PATH.ToString());
										m_CrossHairPGM.SetGalvoCondition(FA.DEF.eAxesName.RA.ToString(), FA.DEF.eAxesName.RB.ToString(), FA.RCP.M100_CrossHairMarkingVelocity.AsDouble, FA.RCP.M100_CrossHairMarkingAccel.AsDouble);
										m_CrossHairPGM.SetGalvoRotate(FA.RCP.M100_CPUGalvoRotate.AsDouble);
										m_CrossHairPGM.SetLaserFreq(FA.RCP.M100_CrossHairLaserFrq.AsDouble* 1000.0,0.5);
										m_CrossHairPGM.SetLaserMode(1); // 1 : YAG
										m_CrossHairPGM.SetPSOMode(true); // true : PSO Mode
										m_CrossHairPGM.SetStageXYCondition(FA.DEF.eAxesName.RX.ToString(), FA.DEF.eAxesName.Y.ToString(), FA.RCP.M100_CrossHairStageVelocity.AsDouble, FA.RCP.M100_CrossHairStageAccel.AsDouble);
										m_CrossHairPGM.SetStageZCondition(FA.DEF.eAxesName.RZ.ToString(), 5, 50);

										/// set axis move profile							 
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MakePGM:
										if (m_CrossHairPGM.Make_Pgm() == false)
										{
												m_stRun.bTimeoutNow = true;
												break;
										}

										base.NextSubStep();
										break;

								case eMODULE_SEQ_CROSS_HAIR.Basic_Settings_finish:
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.ShutterClose_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_start:
										ACT.CYL_LaserShutter_Close.Run();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_Finish:
										if (!ACT.CYL_LaserShutter_Close.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.MoveTo_StartZPos_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartZPos_start:
										if (FA.OPT.UseAutoFocus)
												m_dCrossHairTargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;
										else
												m_dCrossHairTargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;


										ACT.MoveABS(DEF.eAxesName.RZ, m_dCrossHairTargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();

										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.MoveTo_StartXPos_prechk);

										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();

										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartXPos_start:
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
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_StartXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.ShutterOpen_prechk);

										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterOpen_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterOpen_start:
										//ACT.CYL_LaserShutter_Open.Run();
										base.NextSubStep();

										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterOpen_Finish:
										//if (!ACT.CYL_LaserShutter_Open.Check())
										//		break;
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.DisableExternalGate_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.DisableExternalGate_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.DisableExternalGate_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.DisableExternalGate_finish:
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Calc_Power_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.Calc_Power_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Calc_Power_start:
										{
												if(FA.MGR.LaserMgr.IsExistFrequencyTable(FA.RCP.M100_CrossHairLaserFrq.AsInt*1000)==false)
												{
														m_stRun.TimeoutNow();
														break;
												}

												Laser.LaserPwrTableData pPwrTable=FA.MGR.LaserMgr.GetPwrTableData(FA.RCP.M100_CrossHairLaserFrq.AsInt*1000);
												if (pPwrTable.GetPercentFromPower(FA.RCP.M100_CrossHairLaserPwr.AsDouble, out m_dCrossHairATTPos) == -1)
												{
														m_stRun.TimeoutNow();
														break;
												} 

												/*	PowerTable PwrTable = new PowerTable(FA.FILE.InitProcPowerTable);
													if (PwrTable.GetPulseWidthByFreqnPower(FA.RCP.M100_CrossHairLaserFrq.AsInt, FA.RCP.M100_CrossHairLaserPwr.AsDouble, out m_dCrossHairATTPos) == -1)
													{
															base.m_stRun.TimeoutNow();
															break;
												}*/
												base.NextSubStep();
										}
										break;
								case eMODULE_SEQ_CROSS_HAIR.Calc_Power_finish:
										  
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Set_Laser_EFreq_prechk);
										break;

								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_EFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_EFreq_start:
										FA.LASER.Instance.EPRF = FA.RCP.M100_CrossHairLaserFrq.AsDouble * 1000;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_EFreq_finish:
										if (!FA.LASER.Instance.EPRF.IsSame(FA.RCP.M100_CrossHairLaserFrq.AsDouble * 1000))
												break;
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.MoveToAttPos_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveToAttPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveToAttPos_start:
										//FA.ATT.LPA.fAngle = m_dCrossHairATTPos;
										   LASER.Instance.SetDiodeCurrent=(float)m_dCrossHairATTPos*100.0f;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveToAttPos_finish:
										//if (!FA.ATT.LPA.fAngle.IsSame(m_dCrossHairATTPos, 0.02))
											//	break;
										if(LASER.Instance.SetDiodeCurrent.IsSame((float)m_dCrossHairATTPos*100.0f,0.2f))
												break;

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_start:
										//FA.LASER.Instance.RepetitionRate = 0;//FA.RCP.M100_CrossHairLaserFrq * 1000;
										DEF.eDO.LASER_EM_ENABLE.GetDO().Value=true;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_finish:
										
										//if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
											//	break;
										if(FA.LASER.Instance.IsEmissionOn==false)
												break;
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Set_Laser_GateEnable_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_GateEnable_prechk:
										if (base.IsRunModeStopped())
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_GateEnable_start:
										//FA.LASER.Instance.GateEnable = true;
										FA.LASER.Instance.GateMode=Laser.GATE_MODE.EXT;
										FA.LASER.Instance.TriggerMode=Laser.TRIG_MODE.EXT;
										(FA.LASER.Instance as EzIna.Laser.IPG.GLPM).Modulation=false;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_GateEnable_finish:
										if (FA.LASER.Instance.GateMode!=Laser.GATE_MODE.EXT)
												break;
										if(FA.LASER.Instance.TriggerMode!=Laser.TRIG_MODE.EXT)
												break;
										if((FA.LASER.Instance as EzIna.Laser.IPG.GLPM).Modulation)
												break;

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Set_A3200_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_A3200_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_A3200_start:
										// Motion.CMotionA3200.SetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										// Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										//Motion.CMotionA3200.StopProgram(1);
										RTC5.Instance.ListStop();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_A3200_finish:
										//Motion.CMotionA3200.GetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										//Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.PGM_Execute_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.PGM_Execute_prechk:
										if (base.IsRunModeStopped()) break;

										if(RTC5.Instance.IsExecuteList_BUSY || RTC5.Instance.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._1st)==false)
										{
												m_stRun.TimeoutNow();
												break;
										}
										//if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete && Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.Idle)
										//{
												//m_stRun.TimeoutNow();
												//break;
										//}
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.PGM_Execute_start:

										//Motion.CMotionA3200.ProgramRun(1, m_CrossHairPGM.Get_FilePath());
										RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
										m_stRun.stwatchForSub.SetDelay = 2000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.PGM_Execute_Run_Chk:
										if (!m_stRun.stwatchForSub.IsDone)
												break;
										if(RTC5.Instance.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st)==true)
										{
												m_stRun.TimeoutNow();
												break;
										}
										// 					if(Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramRunning)
										// 					{
										// 						m_stRun.TimeoutNow();
										// 						break;
										// 					}

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.PGM_Execute_Finish:

										//if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete)
										//{
												//m_stRun.TimeoutNow();
												break;
										//}

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.ShutterClose_2nd_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_2nd_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_2nd_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.ShutterClose_2nd_Finish:
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_To_Zero_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_To_Zero_prechk:
										if (base.IsRunModeStopped()) break;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_To_Zero_start:
										//FA.LASER.Instance.RepetitionRate = 0;
										DEF.eDO.LASER_EM_ENABLE.GetDO().Value=false;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.Set_Laser_QFreq_To_Zero_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
												break;
										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishZPos_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishZPos_prechk:
										if (base.IsRunModeStopped()) break;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishZPos_start:
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
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishXPos_prechk);
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishXPos_start:
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
								case eMODULE_SEQ_CROSS_HAIR.MoveTo_FinishXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;


										base.NextSubStep(eMODULE_SEQ_CROSS_HAIR.Finish);
										break;
						}
						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Scanner_CrossHair + m_stRun.nSubStep);
						return false;
				}
				#endregion
		}
}
