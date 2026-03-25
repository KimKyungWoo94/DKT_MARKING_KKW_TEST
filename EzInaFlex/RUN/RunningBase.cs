using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public abstract class RunningBase : Run_Interface
		{

				public FA.DEF.stRunInfo m_stRun;
				public FA.DEF.stRunInfo m_stRunBackup;

				public EzIna.UC.StopWatchTimer m_stwatchForTimeoutOfMain;
				public EzIna.UC.StopWatchTimer m_stwatchForTimeoutOfSub;

				string m_strModuleName;
				int m_iModuleIndex;

				public bool IsDone_ToInit { get; set; }
				public bool IsDone_Init { get; set; }
				public bool IsDone_ToReady { get; set; }
				public bool IsDone_Ready { get; set; }
				public bool IsDone_ToRun { get; set; }
				public bool IsDone_Run { get; set; }

				public RunningBase(string a_strModuleName, int a_iModuleIndex)
				{
						m_strModuleName = a_strModuleName;
						m_iModuleIndex = a_iModuleIndex;
						m_stRun.Init();
						m_stRunBackup.Init();
						m_stwatchForTimeoutOfMain = new EzIna.UC.StopWatchTimer();
						m_stwatchForTimeoutOfSub = new EzIna.UC.StopWatchTimer();
				}


				public bool StartManualMode()
				{
						// 			if (m_stRun.nSubStep != FA.DEF.FinishActionOfSub)
						// 				return false;

						m_stRun.Init();

						m_stRun.nSubStep = FA.DEF.StartActionOfSub;
						return true;
				}

			public bool GetRecoveryRunInfo(bool a_bPauseStop=true)
			{
				if(a_bPauseStop)
				{
					m_stRun = m_stRunBackup;
					m_stRun.nMainStep = FA.DEF.GetStepOfMain(m_stRun.nMainStep);
					m_stRun.nSubStep = FA.DEF.GetStepOfSub(m_stRun.nSubStep);
				}
				else
				{
					m_stRun.nMainStep			= FA.DEF.FindAction;
					m_stRun.nSubStep			= FA.DEF.StartActionOfSub;
					m_stRun.nParallelSeqStep	= FA.DEF.SEQ_FINISH;
				}


						if (m_stRun.nMainStep == FA.DEF.FinishActionOfMain)
								m_stRun.nMainStep = FA.DEF.FindAction;


						return true;
				}

				public bool SetRecoveryRunInfo()
				{
						if (FA.MGR.RunMgr == null) return false;
						if (!FA.MGR.RunMgr.IsInitialized) return false;

						if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.ToStop
							&& FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Run
											&& FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Manual
											&& FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.ToStopManual)
								return false;

						m_stRunBackup = m_stRun;

						return true;
				}
				public void ClearRecoveryRunInfo()
				{
						m_stRunBackup.Clear();
				}
				public void ClearRunInfo()
				{
				     m_stRun.Clear();
				}
				public bool IsRunModeStopped()
				{
						if (FA.MGR.RunMgr == null)
								return false;

						if (FA.MGR.RunMgr.eRunMode == FA.DEF.eRunMode.ToStopManual ||
								FA.MGR.RunMgr.eRunMode == FA.DEF.eRunMode.ToStop)
						{
								m_stRun.nMainStep = FA.DEF.FinishActionOfMain;
								m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
								m_stRun.nParallelSeqStep = FA.DEF.SEQ_FINISH;
								return true;
						}

						return false;
				}

				public bool IsStop()
				{
						if (FA.MGR.RunMgr == null) return false;
						return m_stRun.bRunStop;
				}




				#region interface 
				public bool ToInit()
				{
						IsDone_ToInit = false;

						//             if (m_stRunOfMain.nMainStep != FA.DEF.FinishActionOfMain)
						//                 return false;

						IsDone_Init = false;
						m_stRun.nMainStep = FA.DEF.StartActionOfMain;
						IsDone_ToInit = true;

						return true;
				}
				public bool ToReady()
				{
						return true;
				}
				public bool ToRun(bool a_bPauseStop = true)
				{
					bool bRtn = false;
					bRtn = GetRecoveryRunInfo(a_bPauseStop);
					m_stRun.bRunStop = false;
					m_stwatchForTimeoutOfMain.SetTime();
					m_stwatchForTimeoutOfSub.SetTime();

					return bRtn;
				}
				public bool ToStop()
				{
					   return true;
				}



				public abstract void Init();
				public abstract void Ready();
				public abstract bool Run();
				public abstract void Stop();
				public abstract bool FindAction();
				public abstract void ConnectingModule();

				#endregion


				//common function
				#region Function related to Error
				public void ResetTimeout()
				{
						m_stRun.bHoldOnTimeout = false;
						m_stRun.bTimeoutNow = false;
						m_stRun.stwatchForMain.SetDelay = 0;
						m_stRun.stwatchForSub.SetDelay = 0;
				}
				#endregion
				#region check timeout

				public void MainSeqCheckTimeout(int a_nTimeout, int a_nErrorNo)
				{
						if (m_stRun.nMainStep == FA.DEF.FinishActionOfMain)
								return;

						if (m_stRun.bHoldOnTimeout)
								return;

						if (m_stRun.bTimeoutNow)
						{
								FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Jam, a_nErrorNo);
								return;
						}



						if (m_stRun.nPrevMainStep != m_stRun.nMainStep)
						{
								m_stRun.nPrevMainStep = m_stRun.nMainStep;
								m_stwatchForTimeoutOfMain.SetDelay = a_nTimeout;
						}
						else if (m_stwatchForTimeoutOfMain.IsDone)
						{
								FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Jam, a_nErrorNo);
								m_stwatchForTimeoutOfMain.SetDelay = a_nTimeout;
								return;
						}

				}

				public void SubSeqCheckTimeout(int a_nTimeout, int a_nErrorNo)
				{
						if (m_stRun.nSubStep == FA.DEF.FinishActionOfSub)
								return;

						if (m_stRun.bHoldOnTimeout)
						{
								m_stRun.bHoldOnTimeout = false;
								return;
						}

						if (m_stRun.bTimeoutNow)
						{
								FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Jam, a_nErrorNo);
								m_stRun.bTimeoutNow = false;
								return;
						}

						if (m_stRun.nPrevSubStep != m_stRun.nSubStep)
						{
								m_stRun.nPrevSubStep = m_stRun.nSubStep;
								m_stwatchForTimeoutOfSub.SetDelay = a_nTimeout;
						}
						else if (m_stwatchForTimeoutOfSub.IsDone)
						{
								FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Jam, a_nErrorNo);
								m_stwatchForTimeoutOfSub.SetDelay = a_nTimeout;
						}
				}
				#endregion
				#region Sequence interface
				public virtual bool InterfaceInitClear()
				{
						return true;
				}
				public virtual bool InterfaceReadyClear()
				{
						return true;
				}
				public virtual bool InterfaceRunClear()
				{
						return true;
				}

				public virtual bool GetInterfaceInitValue<T>(T Value)
				{
						return true;
				}

				#endregion
				#region Step manager
				public void NextMainStep(int a_iSeq)
				{
						m_stRun.nMainStep = a_iSeq;
				}
				public void FinishMainStep()
				{
						m_stRun.nMainStep = FA.DEF.FinishActionOfMain;
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
				public void NextSubStep()
				{
						m_stRun.nSubStep++;
				}
				public void FinishSubStep()
				{
						m_stRun.nSubStep = FA.DEF.FinishActionOfSub;
				}

				public void Next_Parallel_SEQ_Step()
				{
						m_stRun.nParallelSeqStep++;
				}
				public void Next_Parallel_SEQ_Step<TEnum>(TEnum a_eSeq)
				{
						m_stRun.nParallelSeqStep = ConvertToIndex(a_eSeq);
				}

				public void Start_SEQ_Step()
				{
						m_stRun.nParallelSeqStep = FA.DEF.SEQ_START;
				}
				public void Finish_SEQ_Step()
				{
						m_stRun.nParallelSeqStep = FA.DEF.SEQ_FINISH;
				}

				int ConvertToIndex<TEnum>(TEnum key)
				{
						return CastTo<int>.From(key);
				}


				#endregion Step manager



		}//end of class
		 // 	static class EnumHelper {
		 // 	   public EzIna.IO.DI GetDI(this EzIna.FA.DEF.eDI a_value)
		 // 	   {
		 // 	      return EzIna.FA.MGR.IOMgr.DIList[(int)a_value];
		 // 	   }
		 // 	}
}//end of namespace
