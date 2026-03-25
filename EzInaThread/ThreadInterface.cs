using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.UserThread
{
	interface ItemsInterface
	{
		void Start();
		void Stop();
		void Pause();
		void abort();
		void Joining();


		void Execute(Object obj);
	}

	interface ManagerInterface
	{
		bool Initialize();
		void Terminate();
		bool AddItem(String a_strKey, ThreadItem a_Item);
		ThreadItem GetItem(String a_strKey);

		int Size();
		void Execute();

		void StartItems();
		void StopItems();
		void JoiningItems();

	}
}
