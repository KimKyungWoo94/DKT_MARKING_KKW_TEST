using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzIna.FA.DEF;

namespace EzIna
{
		#region [STAGE MAPPING CLASS]
	
		public class CMapping : IDisposable
		{
				public int m_iRow;
				public int m_iCol;
				public double m_dEncX; //(m_x)
				public double m_dEncY;//(m_y) 
				public double m_dEncZ;
				public double m_dAngleT;//(v_t)
				public double m_dMappingOffsetX;
				public double m_dMappingOffsetY;
				public double m_dMappingOffsetZ;
				public EzIna.GUI.UserControls.eCellStatus m_eStatus;


				public CMapping()
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
						m_dMappingOffsetX = 0.0;
						m_dMappingOffsetY = 0.0;
						m_dMappingOffsetZ = 0.0;
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
						m_dMappingOffsetX = 0.0;
						m_dMappingOffsetY = 0.0;
						m_dMappingOffsetZ = 0.0;
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
		#endregion[STAGE MAPPING]

		public partial class RunningStage
		{
				#region [INTERFACE - INIT]
				public enum eMODULE_SEQ_INIT_STATGE
				{
					Finish = 0
					,IO_Check_Start = 40
					, IO_Check_Finish
					, CYL_Check_Start = 50
					, CYL_Check_Finish
					, Motor_AXIS_Home_PreChk = 60
				  , Motor_AXIS_Home_Start
					, Motor_AXIS_Home_Finish
			    , Motor_AXIS_RAIL_Adjst_MovePreChk
					, Motor_AXIS_RAIL_Adjst_MoveStart
				  , Motor_AXIS_RAIL_Adjst_MoveFinish
					, MAX
				}
				public enum eMODULES_STATUS_INIT_STAGE
				{
						IO_Check = 0
					, CYL_Check
					, Motor_AXIS_Homing_Is_Completed
					, Motor_AXIS_RAIL_Adjust_Is_Completed
					, Max
				}
				#endregion [INTERFACE - INIT]

	
			
			
		}

		public partial class RunningStage : RunningBase
		{
				public Dictionary<eMODULES_STATUS_INIT_STAGE, FA.DEF.stRunModuleStatus> Interface_Init { get; set; }
				System.Diagnostics.Stopwatch m_TimeStamp = null;

				RunningScanner pRunningScanner = null;
				RunningProcess pRunningProcess = null;
				RunningLoad		 pRunningLoad=null;
				RunningUnload  pRunningUnload=null;


				public RunningStage(string a_strModuleName, int a_iModuleIndex) : base(a_strModuleName, a_iModuleIndex)
				{
						Interface_Init = new Dictionary<eMODULES_STATUS_INIT_STAGE, FA.DEF.stRunModuleStatus>();
						foreach (eMODULES_STATUS_INIT_STAGE item in Enum.GetValues(typeof(eMODULES_STATUS_INIT_STAGE)))
						{
								Interface_Init.Add(item, new FA.DEF.stRunModuleStatus());
						}
						m_TimeStamp = new System.Diagnostics.Stopwatch();
				}

				#region override
				public override bool InterfaceInitClear()
				{
						FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
						eMODULES_STATUS_INIT_STAGE item;
						for (int i = 0; i < Interface_Init.Count; i++)
						{
								item = (eMODULES_STATUS_INIT_STAGE)i;
								stWork = Interface_Init[item];
								stWork.Clear();
								Interface_Init[item] = stWork;
						}

						return true;
				}
				public override void Init()
				{
						try
						{													
								switch (CastTo<eMODULE_SEQ_INIT_STATGE>.From(m_stRun.nMainStep))
								{
										case eMODULE_SEQ_INIT_STATGE.Finish:
												return;

										case eMODULE_SEQ_INIT_STATGE.IO_Check_Start:
#if SIM
#else
												if (eDI.XYZ_MOTOR_PWR_MC.GetDI().Value == false)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.XYZ_MOTOR_PWR_MC.GetDI().Value == false)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if(eDI.JIG_POS_DETECTED_L.GetDI().Value==true)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.JIG_POS_DETECTED_R.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.JIG_POS_DETECTED_M.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.JIG_FEEDER_JIG_EXIST.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}					
#endif                         
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.IO_Check, false);
												NextMainStep();
												break;

										case eMODULE_SEQ_INIT_STATGE.IO_Check_Finish:
												//if(!FA.ACT.StageVauccmOff.Check()) 


												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.IO_Check, true);
												NextMainStep(eMODULE_SEQ_INIT_STATGE.CYL_Check_Start);
												break;

										case eMODULE_SEQ_INIT_STATGE.CYL_Check_Start:
#if SIM
#else
												//FA.ACT.CYL_JIG_ACC_DOWN.Run();
												FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
												FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
												FA.ACT.CYL_STOPPER_L_DOWN.Run();
												FA.ACT.CYL_STOPPER_R_DOWN.Run();
												FA.ACT.CYL_STOPPER_CENTER_DOWN.Run();
#endif
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.CYL_Check, false);
												NextMainStep();
												break;
										case eMODULE_SEQ_INIT_STATGE.CYL_Check_Finish:
#if SIM
#else
												//if (!FA.ACT.CYL_JIG_ACC_DOWN.Check()) break;
												if (!FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Check()) break;
												if (!FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Check()) break;
												if (!FA.ACT.CYL_STOPPER_L_DOWN.Check()) break;
												if (!FA.ACT.CYL_STOPPER_R_DOWN.Check()) break;
												if (!FA.ACT.CYL_STOPPER_CENTER_DOWN.Check()) break;
												
												//m_stRun.bHoldOnTimeout = true;
												//if (!pRunningScanner.IsDone_InitStauts(RunningScanner.eMODULE_STATUS_INIT_SCANNER.Motor_RZ_Y_Homing_Is_Completed))
												//		break;
											
#endif
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.CYL_Check, true);
												NextMainStep(eMODULE_SEQ_INIT_STATGE.Motor_AXIS_Home_PreChk);
												break;

										case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_Home_PreChk:

												if(!AXIS.RAIL_ADJUST.Status().IsServoOn)
												{
														AXIS.RAIL_ADJUST.ServoOn=true;
												}	

												m_stRun.stwatchForMain.SetDelay = 100;
												
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.Motor_AXIS_Homing_Is_Completed, false);
												NextMainStep();
												break;
										case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_Home_Start:
												if (!m_stRun.stwatchForMain.IsDone)
														break;


												if (!AXIS.RAIL_ADJUST.Status().IsServoOn)
														break;
												if (!AXIS.RAIL_ADJUST.HomeStart())
														break;

												m_stRun.stwatchForMain.SetDelay = 100;
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.Motor_AXIS_Homing_Is_Completed, false);
												NextMainStep();
												break;
										case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_Home_Finish:
												if (!m_stRun.stwatchForMain.IsDone)
														break;

												if (!AXIS.RAIL_ADJUST.Status().IsHomeComplete)
														break;
												if (!AXIS.RAIL_ADJUST.Status().IsMotionDone)
														break;

												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.Motor_AXIS_Homing_Is_Completed, true);
												NextMainStep(eMODULE_SEQ_INIT_STATGE.Motor_AXIS_RAIL_Adjst_MovePreChk);											
												break;
										case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_RAIL_Adjst_MovePreChk:
#if SIM
#else

												if (eDI.JIG_POS_DETECTED_L.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.JIG_POS_DETECTED_R.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}
												if (eDI.JIG_POS_DETECTED_M.GetDI().Value == true)
												{
														m_stRun.TimeoutNow();
														break;
												}
#endif
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.Motor_AXIS_RAIL_Adjust_Is_Completed, true);
												NextMainStep();												
												break;
											case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_RAIL_Adjst_MoveStart:


												ACT.MoveABS(FA.RCP.M100_RAIL_ADJUST_INIT_WIDTH);				
												UpdateInitStatus(eMODULES_STATUS_INIT_STAGE.Motor_AXIS_RAIL_Adjust_Is_Completed, true);
												NextMainStep();
												break;
										 case eMODULE_SEQ_INIT_STATGE.Motor_AXIS_RAIL_Adjst_MoveFinish:
												if (!FA.AXIS.RAIL_ADJUST.Status().IsMotionDone)
														break;								
												IsDone_Init = true;
												FinishMainStep();
												break;
											
								}

								
						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
						}

						MainSeqCheckTimeout(FA.DEF.Timeout_Init, FA.DEF.Error_Init_Stage + m_stRun.nMainStep);
				}
				public override void Ready()
				{
						throw new NotImplementedException();
				}

				public override bool Run()
				{
							m_stRun.bRunStop=true;
						return true;
						//throw new NotImplementedException();
				}

				public override void Stop()
				{
						throw new NotImplementedException();
				}

				public override bool FindAction()
				{
						throw new NotImplementedException();
				}

				public override void ConnectingModule()
				{
						pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
						pRunningProcess = (RunningProcess)FA.MGR.RunMgr.GetItem("PROCESS");
						pRunningLoad		= (RunningLoad)FA.MGR.RunMgr.GetItem("Load");
						pRunningUnload	= (RunningUnload)FA.MGR.RunMgr.GetItem("Unload");
				}
#endregion

#region [INTERFACE]
				private void UpdateInitStatus(eMODULES_STATUS_INIT_STAGE a_eItem, bool a_bCompleted)
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
				public bool IsDone_InitStauts(eMODULES_STATUS_INIT_STAGE a_eItem)
				{
						return Interface_Init[a_eItem].IsDone();
				}
				private void UpdateWorkStatus(eMODULES_STATUS_INIT_STAGE a_eItem, bool a_bStart, bool a_bFinish)
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
#endregion

		}
}
