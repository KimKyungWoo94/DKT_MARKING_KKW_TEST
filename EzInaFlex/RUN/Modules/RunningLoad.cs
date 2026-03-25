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
		public class RunningLoad : RunningBase
		{

				public enum eMODULE_SEQ_INIT_LOAD
				{
						Finish = 0
						, IO_Check_Start = FA.DEF.StartActionOfMain
						, IO_Check_Finish
						, Cylinder_chk_start
						, Cylinder_chk_Finish
						,
				}
				public enum eMODULE_STATUS_INIT_LOAD
				{
						IO_Check
						, Cylinder_Check
				}

				public enum eMODULE_SEQ_LOADING
				{
						Finish = 0

						, IO_Check_Start = FA.DEF.StartActionOfSub
						, IO_Check_Finish
						, Cylinder_chk_start = 20
						, Cylinder_chk_Finish
						, LOADER_IF_Start = 30
						, LOADER_IF_Finish


						, PINICH_ROLLER_ACT_PreActStart = 40
						, PINICH_ROLLER_ACT_PreAct_IO_CY_Check
						, PINICH_ROLLER_ACT_PreAct_Motor_Check

						, PINiCH_ROLLER_ACT_PreActCheck
						, PINiCH_ROLLER_ACT_PreActFinish
						, PINICH_ROLLER_ACT_Start = 50
						, PINICH_ROLLER_ACT_JIG_LOCK_CHK
						, PINICH_ROLLER_ACT_Finish



				}
				public enum eMODULE_SEQ_STATUS_LOADING
				{
						IO_CHECK,
						SEQ_CHECK,
						Cylinder_Check,
						LOADER_IF,
						PINICH_ROLLER_PRE_ACT,
						PINICH_ROLLER_ACT,
						LOADING_FINISH
				}

				public enum AUTO_AUTOMATIC_SEQ
				{
						AUTO_SEQ_FINISH_ACTION = 0,
						AUTO_SEQ_FIND_ACTION = 100,
						AUTO_SEQ_PROCESS = 200,

				}

				public Dictionary<eMODULE_STATUS_INIT_LOAD, FA.DEF.stRunModuleStatus> Interface_Init { get; set; }
				public Dictionary<eMODULE_SEQ_STATUS_LOADING, FA.DEF.stRunModuleStatus> Interface_SEQ { get; set; }

				Stopwatch m_TimeStamp = null;
				RunningScanner m_pRunScanner = null;
				RunningUnload m_pRunUnload = null;

				public RunningLoad(string a_strModuleName, int a_iModuleIndex) : base(a_strModuleName, a_iModuleIndex)
				{
						Interface_Init = new Dictionary<eMODULE_STATUS_INIT_LOAD, FA.DEF.stRunModuleStatus>();
						Interface_SEQ = new Dictionary<eMODULE_SEQ_STATUS_LOADING, FA.DEF.stRunModuleStatus>();
						foreach (eMODULE_STATUS_INIT_LOAD item in Enum.GetValues(typeof(eMODULE_STATUS_INIT_LOAD)))
						{
								Interface_Init.Add(item, new FA.DEF.stRunModuleStatus());
						}
						foreach (eMODULE_SEQ_STATUS_LOADING item in Enum.GetValues(typeof(eMODULE_SEQ_STATUS_LOADING)))
						{
								Interface_SEQ.Add(item, new FA.DEF.stRunModuleStatus());
						}
						m_TimeStamp = new Stopwatch();
				}

				public override void ConnectingModule()
				{
						m_pRunScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
						m_pRunUnload = (RunningUnload)FA.MGR.RunMgr.GetItem("Unload");
				}



				public override void Init()
				{
						try
						{
								switch (CastTo<eMODULE_SEQ_INIT_LOAD>.From(m_stRun.nMainStep))
								{
										case eMODULE_SEQ_INIT_LOAD.Finish:
												return;
										case eMODULE_SEQ_INIT_LOAD.IO_Check_Start:
												{
														UpdateInitStatus(eMODULE_STATUS_INIT_LOAD.IO_Check, false);
#if SIM
#else
														if (eDI.RAIL_MOUTH_L.GetDI().Value == true)
														{
																m_stRun.TimeoutNow();
																break;
														}
														if (eDI.JIG_POS_DETECTED_L.GetDI().Value == true)
														{
																m_stRun.TimeoutNow();
																break;
														}
														eDO.PINCH_ROLLER_L_U.GetDO().Value = false;
														eDO.PINCH_ROLLER_L_B.GetDO().Value = false;

#endif

														NextMainStep();
												}
												break;
										case eMODULE_SEQ_INIT_LOAD.IO_Check_Finish:
												{
														AXIS.PR_L_U.ServoOn = true;
														AXIS.PR_L_B.ServoOn = true;
														UpdateInitStatus(eMODULE_STATUS_INIT_LOAD.IO_Check, true);
														NextMainStep(eMODULE_SEQ_INIT_LOAD.Cylinder_chk_start);
												}
												break;
										case eMODULE_SEQ_INIT_LOAD.Cylinder_chk_start:
												{
														UpdateInitStatus(eMODULE_STATUS_INIT_LOAD.Cylinder_Check, false);
#if SIM
#else
														FA.ACT.CYL_STOPPER_L_DOWN.Run();
														FA.ACT.CYL_STOPPER_CENTER_DOWN.Run();
#endif
														m_stRun.stwatchForMain.SetDelay = 1000;
														NextMainStep();
												}
												break;
										case eMODULE_SEQ_INIT_LOAD.Cylinder_chk_Finish:
												{
														if (!m_stRun.stwatchForMain.IsDone)
																break;
#if SIM
#else
														if (!FA.ACT.CYL_STOPPER_L_DOWN.Check()) break;
														if (!FA.ACT.CYL_STOPPER_CENTER_DOWN.Check()) break;
#endif
														UpdateInitStatus(eMODULE_STATUS_INIT_LOAD.Cylinder_Check, true);
														//	InterfaceRunClear();													
														FinishMainStep();
														IsDone_Init = true;
												}
												break;

								}

						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("Exception", "RunningLoad_Init_{0}:{1}:{2}", m_stRun.nSubStep, exc.StackTrace, exc.Message);
						}
						MainSeqCheckTimeout(FA.DEF.Timeout_Init, FA.DEF.Error_Init_Loader + m_stRun.nMainStep);
				}

				public int SubSeq_LoadingStep
				{
						get { return DEF.Error_Run_LOADING + m_stRun.nSubStep; }
				}
				public bool SubSeq_Loading(bool a_bAuto)
				{
						try
						{
								if (!base.SetRecoveryRunInfo())
										return false;
								switch (CastTo<eMODULE_SEQ_LOADING>.From(m_stRun.nSubStep))
								{
										case eMODULE_SEQ_LOADING.Finish:
												return true;

										case eMODULE_SEQ_LOADING.IO_Check_Start:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.IO_CHECK, false);

#if SIM
												m_stRun.stwatchForSub.SetDelay = 100;
#else
												if (eDI.RAIL_MOUTH_L.GetDI().Value == true ||
														eDI.JIG_POS_DETECTED_L.GetDI().Value == true ||
														eDI.JIG_STOP_POS_CHK.GetDI().Value == true
														)
												{
														m_stRun.TimeoutNow();
														break;
												}


#endif
												FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Check Interference Sensor Start");
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.IO_Check_Finish:
#if SIM
												if (!m_stRun.stwatchForSub.IsDone)
														break;


#else
												eDO.LOADER_IF_START_CMD.GetDO().Value = false;
												eDO.PINCH_ROLLER_L_U.GetDO().Value = false;
												eDO.PINCH_ROLLER_L_B.GetDO().Value = false;


#endif
												FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Check Interference Sensor End");
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.IO_CHECK, true);
												NextSubStep(eMODULE_SEQ_LOADING.Cylinder_chk_start);
												break;
										case eMODULE_SEQ_LOADING.Cylinder_chk_start:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.Cylinder_Check, false);
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH, false);
#if SIM

												m_stRun.stwatchForSub.SetDelay = 100;
												NextSubStep();


#else

												if (FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.CurrentStatus() == true &&
														FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.CurrentStatus() == true &&
														FA.ACT.CYL_STOPPER_CENTER_UP.CurrentStatus() == true &&
														FA.ACT.CYL_STOPPER_L_DOWN.CurrentStatus() == true
														)
												{
														UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.Cylinder_Check, true);
														FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Check Interference Cylinder OK");
														NextSubStep(eMODULE_SEQ_LOADING.LOADER_IF_Start);
												}
												else
												{
														if (AXIS.RX.Status().m_stPositionStatus.fActPos > RCP.M100_RAIL_LOADING_PROC_AREA_X_POS.AsDouble)
														{
																FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
																FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
														}
														FA.ACT.CYL_STOPPER_CENTER_UP.Run();
														FA.ACT.CYL_STOPPER_L_DOWN.Run();
														m_stRun.stwatchForSub.SetDelay = 200;
														FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Check Interference Cylinder Start");
														NextSubStep();
												}


#endif

												break;
										case eMODULE_SEQ_LOADING.Cylinder_chk_Finish:
												if (!m_stRun.stwatchForSub.IsDone) break;
#if SIM
#else
												if (AXIS.RX.Status().m_stPositionStatus.fActPos > RCP.M100_RAIL_LOADING_PROC_AREA_X_POS.AsDouble)
												{
														if (!FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Check()) break;
														if (!FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Check()) break;
												}
												if (!FA.ACT.CYL_STOPPER_CENTER_UP.Check()) break;
												if (!FA.ACT.CYL_STOPPER_L_DOWN.Check()) break;
#endif
												FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Check Interference Cylinder End");
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.Cylinder_Check, true);
												NextSubStep(eMODULE_SEQ_LOADING.LOADER_IF_Start);
												break;

										case eMODULE_SEQ_LOADING.LOADER_IF_Start:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.LOADER_IF, false);
												m_stRun.bHoldOnTimeout = true;
#if SIM
#else
												if (OPT.RunningLoaderIFEnable.m_bState)
												{
														eDO.LOADER_IF_START_CMD.GetDO().Value = true;
														if (eDI.LOADER_IF_READY.GetDI().Value == false)
														{
																break;
														}
												}
#endif
												FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Loader IF Start");
												// to be continue
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.LOADER_IF_Finish:
												m_stRun.bHoldOnTimeout = true;
#if SIM

#else
												if (OPT.RunningLoaderIFEnable.m_bState)
												{
														if (eDI.LOADER_IF_READY.GetDI().Value == true)
																break;

												}
												eDO.LOADER_IF_START_CMD.GetDO().Value = false;
#endif
												m_stRun.bHoldOnTimeout = false;
												FA.LOG.SEQ(string.Format("Load_{0}", SubSeq_LoadingStep), "Loader IF End");
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.LOADER_IF, true);
												NextSubStep(eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_PreActStart);
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_PreActStart:
												if (base.IsRunModeStopped()) break;
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_PRE_ACT, false);
												if (!OPT.RunningLoaderIFEnable.m_bState)
												{
														m_stRun.stwatchForSub.SetDelay = 1000;
														m_stRun.bHoldOnTimeout = true;
												}


#if SIM
#else
												m_stRun.bHoldOnTimeout = true;
												if (eDI.RAIL_MOUTH_L.GetDI().Value == false && eDI.JIG_POS_DETECTED_L.GetDI().Value == false)
												{
														if (OPT.RunningLoaderIFEnable.m_bState)
														{
																m_stRun.TimeoutNow();
																break;
														}
														else
														{
																break;
														}

												}




#endif
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_PreAct_IO_CY_Check:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_PRE_ACT, false);

												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else

												if (eDI.RAIL_MOUTH_L.GetDI().Value == false && eDI.JIG_POS_DETECTED_L.GetDI().Value == false)
												{
														m_stRun.TimeoutNow();
														break;
												}


												if (AXIS.RX.Status().m_stPositionStatus.fActPos > RCP.M100_RAIL_LOADING_PROC_AREA_X_POS.AsDouble)
												{
														if (FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.CurrentStatus() == false ||
																	FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.CurrentStatus() == false)
														{
																FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
																FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
																m_stRun.stwatchForSub.SetDelay = 200;
																break;
														}
												}
												eDO.PINCH_ROLLER_L_U.GetDO().Value = true;
												eDO.PINCH_ROLLER_L_B.GetDO().Value = true;
												FA.AXIS.PR_L_U.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN);
												FA.AXIS.PR_L_B.Move_Jog(true, Motion.GDMotion.eSpeedType.RUN);
#endif
												if (a_bAuto)
												{
														FA.MGR.RunMgr.PM.ResetCycleTime();
														FA.MGR.RunMgr.PM.StartCycleTime();
												}
												m_stRun.bHoldOnTimeout = false;
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_PreAct_Motor_Check:

												if (a_bAuto)
												{
														if (m_pRunUnload.IsDone_SEQ_Stauts(RunningUnload.eMODULE_SEQ_STATUS_UNLOADING.Cylinder_Check) == false)
																break;
												}
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else
												if (FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.CurrentStatus() == false ||
														 FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.CurrentStatus() == false)
												{
														FA.ACT.CYL_JIG_FEEDER_L_UNCLAMP.Run();
														FA.ACT.CYL_JIG_FEEDER_R_UNCLAMP.Run();
														m_stRun.stwatchForSub.SetDelay = 200;
														break;
												}
#endif

												FA.AXIS.RX.Move_Absolute(FA.RCP.M100_RAIL_LOADING_X_POS.AsDouble,
													FA.RCP.M100_X_LOADING_RETURN_SPEED.AsDouble,
													FA.RCP.M100_X_LOADING_RETURN_SPEED.AsDouble*10,
													FA.RCP.M100_X_LOADING_RETURN_SPEED.AsDouble*10
													);
												NextSubStep();
												break;

										case eMODULE_SEQ_LOADING.PINiCH_ROLLER_ACT_PreActCheck:

												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_PRE_ACT, false);
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else

												if (eDI.JIG_STOP_POS_CHK.GetDI().Value == false)
														break;
#endif

												m_stRun.stwatchForSub.SetDelay = 100;
												NextSubStep();
												break;

										case eMODULE_SEQ_LOADING.PINiCH_ROLLER_ACT_PreActFinish:
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else
												FA.AXIS.PR_L_U.JOG_STOP();
												FA.AXIS.PR_L_B.JOG_STOP();
												
#endif
												if (!FA.AXIS.RX.Status().IsMotionDone) break;
												if (!FA.AXIS.RX.Status().IsInposition) break;

												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_PRE_ACT, true);
												NextSubStep(eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_Start);
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_Start:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_ACT, false);
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else
												FA.ACT.CYL_JIG_FEEDER_L_CLAMP.Run();
												FA.ACT.CYL_JIG_FEEDER_R_CLAMP.Run();
												m_stRun.stwatchForSub.SetDelay = 50;
#endif
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_JIG_LOCK_CHK:
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_ACT, false);
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else
												if (!FA.ACT.CYL_JIG_FEEDER_L_CLAMP.Check()) break;
												if (!FA.ACT.CYL_JIG_FEEDER_R_CLAMP.Check()) break;

												eDO.PINCH_ROLLER_L_U.GetDO().Value = false;
												eDO.PINCH_ROLLER_L_B.GetDO().Value = false;
												FA.ACT.CYL_STOPPER_CENTER_DOWN.Run();
												m_stRun.stwatchForSub.SetDelay = 50;
#endif
												NextSubStep();
												break;
										case eMODULE_SEQ_LOADING.PINICH_ROLLER_ACT_Finish:
												if (!m_stRun.stwatchForSub.IsDone)
														break;
#if SIM
#else
												if (!FA.ACT.CYL_STOPPER_CENTER_DOWN.Check()) break;
#endif
												UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.PINICH_ROLLER_ACT, true);

												FinishSubStep();
												break;
								}							
								SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_LOADING + m_stRun.nSubStep);
								return false;
						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("Exception", "RunningLoad_{0}:{1}:{2}", m_stRun.nSubStep, exc.StackTrace, exc.Message);
								return false;
						}

				}
				public override bool InterfaceInitClear()
				{
						FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
						eMODULE_STATUS_INIT_LOAD item;
						for (int i = 0; i < Interface_Init.Count; i++)
						{
								item = (eMODULE_STATUS_INIT_LOAD)i;
								stWork = Interface_Init[item];
								stWork.Clear();
								Interface_Init[item] = stWork;
						}
						return true;
				}

				private void UpdateInitStatus(eMODULE_STATUS_INIT_LOAD a_eItem, bool a_bCompleted)
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
				public void UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING a_eItem, bool a_bCompleted)
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
				public eMODULE_SEQ_LOADING SubSeqStep
				{
						get { return CastTo<eMODULE_SEQ_LOADING>.From(m_stRun.nSubStep); }
				}
				public bool IsDone_SEQ_Stauts(eMODULE_SEQ_STATUS_LOADING a_eItem)
				{
						return Interface_SEQ[a_eItem].IsDone();
				}
				public bool IsDone_InitStauts(eMODULE_STATUS_INIT_LOAD a_eItem)
				{
						return Interface_Init[a_eItem].IsDone();
				}

				public override bool InterfaceRunClear()
				{
						FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
						eMODULE_SEQ_STATUS_LOADING item;
						for (int i = 0; i < Interface_SEQ.Count; i++)
						{
								item = (eMODULE_SEQ_STATUS_LOADING)i;
								stWork = Interface_SEQ[item];
								stWork.Clear();
								Interface_SEQ[item] = stWork;
						}
						UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH, false);
						return true;
				}
				public override void Ready()
				{

				}
				public override bool FindAction()
				{

						if (!base.SetRecoveryRunInfo())
								return false;

						if (
								this.IsDone_SEQ_Stauts(eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH) == false &&
								m_pRunScanner.IsDone_SEQStauts(RunningScanner.eMODULE_SEQ_STATUS_PROC.JOB_END)

								)
						{
								base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_PROCESS;
						}
						// do to list 
						// Flag , Status 
						// Loading Complate Flag ADD
						return true;

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
												if (this.SubSeq_Loading(true))
												{
														base.m_stRun.nMainStep = (int)AUTO_AUTOMATIC_SEQ.AUTO_SEQ_FIND_ACTION;
														base.m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
														base.SetRecoveryRunInfo();
														m_pRunScanner.UpdateSeqStatus(RunningScanner.eMODULE_SEQ_STATUS_PROC.JOB_END, false);
														UpdateSEQ_Status(eMODULE_SEQ_STATUS_LOADING.LOADING_FINISH, true);
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
