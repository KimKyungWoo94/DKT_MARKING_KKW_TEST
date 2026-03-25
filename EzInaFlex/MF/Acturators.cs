using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.MF
{
		public enum ActStatus { Stop, Running, Timeout, Finished }
		public class ActItem
		{
				public delegate bool ActionRun();
				public delegate bool ActionChk();

				private ActionRun OnRun = null;
				private ActionChk OnCheck = null;
				private bool m_bDone = false;
				private bool m_bWithOutTimeChk = false;

				private int m_iTimeOutErrCode = 1000; // msec
				private ActStatus m_eActStatus = ActStatus.Stop;
				private Stopwatch m_swTimer = new Stopwatch();

				public int m_nTimeoutLimit = 3000;
				public int m_nDelayTime = 0;
				public string m_strCaption = "";


				public bool bDone { get { return m_bDone; } }
				public ActStatus eActStatus { get { return m_eActStatus; } }
				public bool IsFinished { get { return m_eActStatus == ActStatus.Finished; } }
				public bool IsTimeout { get { return m_eActStatus == ActStatus.Timeout; } }
				public int iTimeoutErrCode { get { return m_iTimeOutErrCode; } }


				public ActItem(string a_strCaption, ActionRun a_Run, ActionChk a_Check, int a_nTimeout, int a_iTimeoutErrCode, bool a_bWithOutTimeChk = false)
				{
						m_strCaption = a_strCaption;
						OnRun += new ActionRun(a_Run);
						OnCheck += new ActionChk(a_Check);
						m_nTimeoutLimit = a_nTimeout;
						m_iTimeOutErrCode = a_iTimeoutErrCode;
						m_bWithOutTimeChk = a_bWithOutTimeChk;
						m_eActStatus = ActStatus.Finished;
						try
						{
								FA.ACT.m_ActItems.Add(this);
						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
						}
				}

				public bool Run(int a_nDelayTime = 0)
				{
						if (m_bWithOutTimeChk)
						{
								if (OnRun())
										return true;
						}
						else
						{
								OnRun();
								if (FA.MGR.RunMgr.eRunMode == FA.DEF.eRunMode.Jam) return false;
								//if (bDone) return true;

								m_eActStatus = ActStatus.Running;
								m_nDelayTime = a_nDelayTime;

								if (!m_swTimer.IsRunning)
										m_swTimer.Restart();
						}

						return false;
				}
				public bool CurrentStatus()
				{
						return OnCheck();
				}

				public bool Check()
				{
						if (m_bWithOutTimeChk)
						{
								if (OnCheck())
								{
										if (m_eActStatus != ActStatus.Finished)
										{
												m_eActStatus = ActStatus.Finished;
										}

										m_swTimer.Reset();
										m_nDelayTime = 0;
										m_bDone = true;
										return true;
								}
						}
						else
						{
								if (OnCheck())
								{
										if (m_nDelayTime > 0 && m_swTimer.ElapsedMilliseconds <= m_nDelayTime)
										{
												m_bDone = false;
												return false;
										}

										if (m_eActStatus != ActStatus.Finished)
										{
												m_eActStatus = ActStatus.Finished;
										}
										m_swTimer.Reset();
										m_nDelayTime = 0;
										m_bDone = true;
										return true;
								}
								else if (!OnCheck() && m_nTimeoutLimit > 0 && m_swTimer.ElapsedMilliseconds > m_nTimeoutLimit)
								{
										m_eActStatus = ActStatus.Timeout;
										m_swTimer.Reset();
										FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.Jam, m_iTimeOutErrCode);
										m_bDone = false;
										return false;
								}
								else
								{
										m_bDone = false;
										return false;
								}
						}
						m_bDone = false;
						return false;
				}
				public void Reset()
				{
						m_eActStatus = ActStatus.Stop;
						m_swTimer.Reset();
				}

		}
}
