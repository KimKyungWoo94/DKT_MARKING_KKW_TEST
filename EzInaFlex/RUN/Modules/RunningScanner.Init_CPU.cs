using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.FA;
//using Aerotech.A3200.Tasks;

namespace EzIna
{
		public partial class RunningScanner
		{
				#region [ CPU ]

				CountsPerUnit_Pgm m_CountsPerUnitPGM = new CountsPerUnit_Pgm();
				double m_dCPU_TargetXPos = 0.0;
				double m_dCPU_TargetYPos = 0.0;
				double m_dCPU_TargetZPos = 0.0;
				double m_dCPU_StartActXPos = 0.0;
				double m_dCPU_StartActYPos = 0.0;
				double m_dCPU_ATTPos = 0.0;

				public double GetCPUStartPosX { get { return m_dCPU_StartActXPos; } }
				public double GetCPUStartPosY { get { return m_dCPU_StartActYPos; } }

				public enum eMODULE_SEQ_CPU
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

					, Set_A3200_prechk = 110
					, Set_A3200_start
					, Set_A3200_finish

					, PGM_Execute_prechk = 120
					, PGM_Execute_start
					, PGM_Execute_Run_Chk
					, PGM_Execute_Finish

					, ShutterClose_2nd_prechk = 130
					, ShutterClose_2nd_start
					, ShutterClose_2nd_Finish

					, Set_Laser_QFreq_To_Zero_prechk = 140 // Q-Frequency 값 설정, 파워 설정
					, Set_Laser_QFreq_To_Zero_start
					, Set_Laser_QFreq_To_Zero_finish

					, MoveTo_FinishZPos_prechk = 150 //Vision Z Pos Vision Focus offset Z Pos, ZigHeight, Thickness
					, MoveTo_FinishZPos_start
					, MoveTo_FinishZPos_finish

					, MoveTo_FinishXPos_prechk = 160 //Fine or Coarse
					, MoveTo_FinishXPos_start
					, MoveTo_FinishXPos_finish        // 현재 포지션 저장 F_Config->m_fCrosshairScannerPos_X = dXPos;

				}
#endregion
				#region [ CPU ]
				public bool SubSeq_Calc_CPU()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eMODULE_SEQ_CPU>.From(m_stRun.nSubStep))
						{
								case eMODULE_SEQ_CPU.Finish:
										FA.LOG.Debug("SEQ", "Focus finder finish");
										return true;
								case eMODULE_SEQ_CPU.Basic_Settings_start:
										FA.LOG.Debug("SEQ", "Focus finder start");
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.variables:
										m_dCrossHairTargetXPos = 0.0;
										m_dCrossHairTargetZPos = 0.0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ChangeLaserMode:
										// set Condition					
										m_CountsPerUnitPGM.SetCPU_distance(FA.RCP.M100_CPUStageFOVSize.AsDouble);
										m_CountsPerUnitPGM.SetGalvoCalPath(FA.RCP.M100_GALVO_CAL_PATH.ToString());
										m_CountsPerUnitPGM.SetGalvoCondition(FA.DEF.eAxesName.RA.ToString(), FA.DEF.eAxesName.RB.ToString(), FA.RCP.M100_CPUMarkingVelocity.AsDouble, FA.RCP.M100_CPUMarkingAccel.AsDouble);
										m_CountsPerUnitPGM.SetGalvoRotate(FA.RCP.M100_CPUGalvoRotate.AsDouble);
										m_CountsPerUnitPGM.SetLaserFreq(FA.RCP.M100_CPULaserFrq.AsDouble * 1000.0, FA.RCP.M100_CPULaserFrq.AsDouble * 1000.0 * 0.5);
										m_CountsPerUnitPGM.SetLaserMode(1);
										m_CountsPerUnitPGM.SetPSOMode(true);
										m_CountsPerUnitPGM.SetStageXYCondition(FA.DEF.eAxesName.RX.ToString(), FA.DEF.eAxesName.Y.ToString(), FA.RCP.M100_CPUStageVelocity.AsDouble, FA.RCP.M100_CPUStageAccel.AsDouble);
										m_CountsPerUnitPGM.SetStageZCondition(FA.DEF.eAxesName.RZ.ToString(), 5, 50);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MakePGM:
										m_CountsPerUnitPGM.Make_Pgm();
										base.NextSubStep();
										break;

								case eMODULE_SEQ_CPU.Basic_Settings_finish:
										base.NextSubStep(eMODULE_SEQ_CPU.ShutterClose_prechk);
										break;
								case eMODULE_SEQ_CPU.ShutterClose_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ShutterClose_start:
										ACT.CYL_LaserShutter_Close.Run();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ShutterClose_Finish:
										if (!ACT.CYL_LaserShutter_Close.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.MoveTo_StartZPos_prechk);
										break;
								case eMODULE_SEQ_CPU.MoveTo_StartZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveTo_StartZPos_start:
										if (FA.OPT.UseAutoFocus)
												m_dCPU_TargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;
										else
												m_dCPU_TargetZPos = FA.RCP.M100_LaserFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;

										ACT.MoveABS(DEF.eAxesName.RZ, m_dCPU_TargetZPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();

										break;
								case eMODULE_SEQ_CPU.MoveTo_StartZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eMODULE_SEQ_CPU.MoveTo_StartXPos_prechk);

										break;
								case eMODULE_SEQ_CPU.MoveTo_StartXPos_prechk:
										if (base.IsRunModeStopped()) break;

										base.NextSubStep();

										break;
								case eMODULE_SEQ_CPU.MoveTo_StartXPos_start:
										if (m_eSelectedVision == DEF.eVision.FINE)
										{

												m_dCPU_StartActXPos = AXIS.RX.Status().m_stPositionStatus.fActPos;
												m_dCPU_StartActYPos = AXIS.Y.Status().m_stPositionStatus.fActPos;
												m_dCPU_TargetXPos = AXIS.RX.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble;
												m_dCPU_TargetYPos = AXIS.Y.Status().m_stPositionStatus.fActPos + FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble;
										}
										else
										{
												m_stRun.TimeoutNow();
												break;
										}


										ACT.MoveABS(DEF.eAxesName.RX, m_dCPU_TargetXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										ACT.MoveABS(DEF.eAxesName.Y, m_dCPU_TargetYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);

										base.NextSubStep();

										break;
								case eMODULE_SEQ_CPU.MoveTo_StartXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.ShutterOpen_prechk);

										break;
								case eMODULE_SEQ_CPU.ShutterOpen_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ShutterOpen_start:
										ACT.CYL_LaserShutter_Open.Run();
										base.NextSubStep();

										break;
								case eMODULE_SEQ_CPU.ShutterOpen_Finish:
										if (!ACT.CYL_LaserShutter_Open.Check())
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.DisableExternalGate_prechk);
										break;
								case eMODULE_SEQ_CPU.DisableExternalGate_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.DisableExternalGate_start:
										FA.LASER.Instance.GateOpen = true;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.DisableExternalGate_finish:
										if (!FA.LASER.Instance.IsGateOpen)
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.Calc_Power_prechk);
										break;
								case eMODULE_SEQ_CPU.Calc_Power_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Calc_Power_start:
										PowerTable PwrTable = new PowerTable(FA.FILE.InitProcPowerTable);
										if (PwrTable.GetPulseWidthByFreqnPower(FA.RCP.M100_CPULaserFrq.AsInt, FA.RCP.M100_CPULaserPwr.AsDouble, out m_dCPU_ATTPos) == -1)
										{
												base.m_stRun.TimeoutNow();
												break;
										}
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Calc_Power_finish:
										base.NextSubStep(eMODULE_SEQ_CPU.Set_Laser_EFreq_prechk);
										break;
								case eMODULE_SEQ_CPU.Set_Laser_EFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_EFreq_start:
										FA.LASER.Instance.RepetitionRate = FA.RCP.M100_CPULaserFrq.AsDouble * 1000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_EFreq_finish:
										if (!FA.LASER.Instance.EPRF.IsSame(FA.RCP.M100_CPULaserFrq.AsDouble * 1000))
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.MoveToAttPos_prechk);
										break;
								case eMODULE_SEQ_CPU.MoveToAttPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveToAttPos_start:
										FA.ATT.LPA.fAngle = m_dCPU_ATTPos;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveToAttPos_finish:
										if (!FA.ATT.LPA.fAngle.IsSame(m_dCPU_ATTPos, 0.02))
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.Set_Laser_QFreq_prechk);
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_start:
										FA.LASER.Instance.RepetitionRate = 0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
												break;

										base.NextSubStep(eMODULE_SEQ_CPU.Set_A3200_prechk);
										break;
								case eMODULE_SEQ_CPU.Set_A3200_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_A3200_start:
										// 					Motion.CMotionA3200.SetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										// 					Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										Motion.CMotionA3200.StopProgram(1);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_A3200_finish:
										//Motion.CMotionA3200.GetGlobalVariables(1, FA.RCP.M100_CrossHairMarkingVelocity);
										//Motion.CMotionA3200.SetGlobalVariables(19, FA.RCP.M100_CrossHairLaserFrq);
										base.NextSubStep(eMODULE_SEQ_CPU.PGM_Execute_prechk);
										break;
								case eMODULE_SEQ_CPU.PGM_Execute_prechk:
										if (base.IsRunModeStopped()) break;
										/*if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete && Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.Idle)
										{
												m_stRun.TimeoutNow();
												break;
										}*/
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.PGM_Execute_start:
										Motion.CMotionA3200.ProgramRun(1, m_CountsPerUnitPGM.Get_FilePath());
										m_stRun.stwatchForSub.SetDelay = 2000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.PGM_Execute_Run_Chk:
										if (!m_stRun.stwatchForSub.IsDone)
												break;

										// 					if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramRunning)
										// 					{
										// 						m_stRun.TimeoutNow();
										// 						break;
										// 					}

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.PGM_Execute_Finish:

										/*if (Motion.CMotionA3200.GetTaskState_Enum(1) != TaskState.ProgramComplete)
										{
												//m_stRun.TimeoutNow();
												break;
										}*/

										base.NextSubStep(eMODULE_SEQ_CPU.ShutterClose_2nd_prechk);
										break;
								case eMODULE_SEQ_CPU.ShutterClose_2nd_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ShutterClose_2nd_start:
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.ShutterClose_2nd_Finish:
										base.NextSubStep(eMODULE_SEQ_CPU.Set_Laser_QFreq_To_Zero_prechk);
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_To_Zero_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_To_Zero_start:
										FA.LASER.Instance.RepetitionRate = 0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.Set_Laser_QFreq_To_Zero_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0))
												break;

										base.NextSubStep(eMODULE_SEQ_CPU.MoveTo_FinishZPos_prechk);
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishZPos_start:
										if (FA.OPT.UseAutoFocus)
												m_dCPU_TargetZPos = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_AutoFocusOffset.AsDouble;
										else
												m_dCPU_TargetZPos = FA.RCP.M100_FineVisionFocusZPos.AsDouble - FA.RCP.M100_JigHeight.AsDouble;
										FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, m_dCPU_TargetZPos, Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishZPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RZ.Status().dTargetCmd, FA.DEF.IN_POS))
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.MoveTo_FinishXPos_prechk);
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishXPos_start:
										ACT.MoveABS(DEF.eAxesName.RX, m_dCPU_StartActXPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										ACT.MoveABS(DEF.eAxesName.Y, m_dCPU_StartActYPos, EzIna.Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CPU.MoveTo_FinishXPos_finish:
										if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
												break;
										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS))
												break;

										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
												break;
										base.NextSubStep(eMODULE_SEQ_CPU.Finish);
										break;
						}
						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Scanner_CrossHair + m_stRun.nSubStep);
						return false;
				}
				#endregion
		}
}
