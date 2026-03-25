using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.UserThread
{
	public class ThreadManager : ManagerInterface
	{
		private Dictionary<string, ThreadItem> m_dicThreads;

		public ThreadManager()
		{
			Initialize();
		}

		~ThreadManager()
		{
			Terminate();
		}

		public bool Initialize()
		{
			m_dicThreads = new Dictionary<string, ThreadItem>();
			return true;
		}
		public void Terminate()
		{
			if (m_dicThreads == null)
				return;

			StopItems();
			JoiningItems();

			m_dicThreads.Clear();
			m_dicThreads = null;

		}
		public bool AddItem(String a_strKey, ThreadItem a_Item)
		{
			m_dicThreads.Add(a_strKey, a_Item);
			return true;
		}

		public ThreadItem GetItem(String a_strKey)
		{
			ThreadItem item = default(ThreadItem);
			m_dicThreads.TryGetValue(a_strKey, out item);
			return item;
		}

		public int Size()
		{
			return m_dicThreads.Count;
		}
		public void Execute()
		{

		}

		public void StartItems()
		{
			if (m_dicThreads == null)
				return;

			foreach(KeyValuePair<String,ThreadItem> item in m_dicThreads)
			{
				item.Value.Start();
			}
		}

		public void StopItems()
		{
			if (m_dicThreads == null)
				return;

			foreach (KeyValuePair<String, ThreadItem> item in m_dicThreads)
			{
				item.Value.Stop();
			}
		}

		public void JoiningItems()
		{
			if (m_dicThreads == null)
				return;

			foreach (KeyValuePair<String, ThreadItem> item in m_dicThreads)
			{
				item.Value.Joining();
			}
		}


	}
}
