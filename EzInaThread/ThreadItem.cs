using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.UserThread
{
	public class ThreadItem : ItemsInterface
	{
		#region variables
		Thread m_Thread = null;
		String m_strName = "";
		int m_nInterval = 1;
		bool m_bActive = false;
		bool m_bStop = false;
		ThreadPriority m_ThreadPriority = ThreadPriority.Normal;
		Object m_DelegateLock = new object();
		Object m_ThreadLock = new object();
        Stopwatch m_sw = new Stopwatch();
		#endregion

		#region deletegate
        
		public delegate void ExecuteFuncHandler(Object obj);
		private ExecuteFuncHandler m_ExcuteFunc;
		public event ExecuteFuncHandler ExcuteFuncEvent
		{
			add
			{
				lock (m_DelegateLock)
				{
					m_ExcuteFunc += value;
				}
			}

			remove
			{
				lock (m_DelegateLock)
				{
					if (m_ExcuteFunc != null)
					{
						m_ExcuteFunc -= value;
					}
				}
			}


		}

		#endregion

		public ThreadItem(string a_StrName, int a_nInterval, ThreadPriority a_Priority)
		{
			m_strName = a_StrName;
			m_nInterval = a_nInterval;
			m_ThreadPriority = a_Priority;
		}
		~ThreadItem()
		{

		}

		public void Start()
		{
			if(!m_bActive)
			{
				m_bStop = false;
				m_Thread = new System.Threading.Thread(Execute);
				m_Thread.IsBackground = true;
				m_Thread.Priority = m_ThreadPriority;
				m_Thread.Start();
				m_bActive = true;
			}

		}
		public void Stop()
		{
			m_bStop = true;
		}
        public bool IsStopped()
        {   
            return m_bStop;
        }
		public void Pause()
		{

		}
		public void abort()
		{
			if(m_bActive)
			{
				m_Thread.Abort();
				m_bStop = true;
			}
		}
		public void Joining()
		{
			if(m_Thread != null)
				m_Thread.Join();
		}

		public void Execute(Object obj)
		{
			lock (m_ThreadLock)
			{
				while (m_bActive && !m_bStop)
				{

                    //m_sw.Restart();
                    Thread.Sleep(m_nInterval);
                    //Thread.Sleep(0);
                    //Trace.WriteLine("Thread Execute [ms] : " + m_sw.ElapsedMilliseconds.ToString());

                    m_ExcuteFunc(this);
				}
				m_bStop = true;
			}
		}

	}

}
