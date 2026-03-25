using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Commucation
{
	public class StopWatchTimer
	{
		Stopwatch m_sw = null;
		long m_lDelayMsec;
		bool m_bPause;

		public long SetDelay
		{
			set
			{
				m_lDelayMsec = value;

				if (m_sw.IsRunning)
				{
					m_sw.Reset();
					m_sw.Start();
				}
				else
				{
					m_sw.Start();
				}
			}
		}
		public long GetDelay
		{
			get
			{
				return m_sw.ElapsedMilliseconds;
			}
		}

		public void SetTime()
		{
			if (m_sw.IsRunning)
			{
				m_sw.Reset();
				m_sw.Start();
			}
			else
			{
				m_sw.Start();
			}

			m_bPause = false;

		}
		public string GetTime()
		{
			string strTime = string.Format("{0:D2} : {1:D2} : {2:D2}", 0, 0, 0);
			TimeSpan ts = m_sw.Elapsed;
			strTime = string.Format("{0:D2} : {1:D2} : {2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
			return strTime;
		}

		public bool IsDone
		{
			get
			{
				if (m_sw.IsRunning)
				{
					if (m_sw.ElapsedMilliseconds <= m_lDelayMsec)
					{
						return false;
					}
					else
					{
						m_sw.Stop();
						m_sw.Reset();
					}
				}

				return true;
			}
		}
		public bool IsRunning
		{
			get
			{
				return m_sw.IsRunning;
			}
		}

		public void DeleyStop()
		{
			if (m_sw.IsRunning)
			{
				m_sw.Stop();
				m_sw.Reset();
			}
		}
		public void TimeReStart()
		{
			if (!m_sw.IsRunning)
			{
				m_sw.Start();
				m_bPause = false;
			}

		}
		public void TimePause()
		{
			if (m_sw.IsRunning)
			{
				m_sw.Stop();
				m_bPause = true;
			}


		}

		public StopWatchTimer()
		{
			m_sw = new Stopwatch();
			m_bPause = false;
		}
		~StopWatchTimer()
		{
			if (m_sw.IsRunning)
				m_sw.Stop();

			m_sw = null;
		}
	}
}
