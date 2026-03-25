using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EzIna
{
    public static class WinAPIs
    {
		///<see ref="https://www.codeproject.com/questions/495980/howplustopluschangeplusthepluscolorplusofplusthepl"/>
		///
		public static IntPtr MakeLParam(int LoWord, int HiWord)
		{
			return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
		}
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string wndName);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string wndName);

        [DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern void ReleaseCapture();

		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wp, int lp);

		[DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr SendMessageA(IntPtr hwnd, int wMsg, int wParam, int lParam);

		[DllImport("User32.Dll", EntryPoint = "PostMessageA")]
		public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
		
	
		public static void _PostMessage(IntPtr handle, int Msg, IntPtr wParam, IntPtr lParam)
		{
			PostMessage(handle, Msg, wParam, lParam);
		}
// 		public static void _PostMessageM(int Msg, int nWParam = 0, int nLParam = 0)
// 		{
//             //IntPtr hWnd = FindWindow("EzInaFlex");
//             //IntPtr hWnd2 = (IntPtr)FindWindow(null, "EzInaFlex");
//             foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
//             {
//                 object obj = Activator.CreateInstance(t);
//                 if(t.Name == "FrmMain")
//                 {
//                     Form f = obj as Form;
//                     IntPtr WParam = MakeLParam(nWParam, 0);
//                     IntPtr LParam = MakeLParam(nLParam, 0);
//                     PostMessage(f.Handle, Msg, WParam, LParam);
//                     break;;
// 
//                 }
//             };
// 		}

        public static void _PostMessageM(int Msg, int nWParam = 0, int nLParam = 0)
        {
								IntPtr hWnd  = (IntPtr)FindWindow(null, "EzInaFlex");
								IntPtr WParam = MakeLParam(nWParam, 0);
								IntPtr LParam = MakeLParam(nLParam, 0);
								PostMessage(hWnd, Msg, WParam, LParam);
        }
				public static void _PostMessageM_IntPtr(int Msg, IntPtr nWParam  , IntPtr nLParam )
				{
						IntPtr hWnd = (IntPtr)FindWindow(null, "EzInaFlex");
						//IntPtr WParam = MakeLParam(nWParam, 0);
						//IntPtr LParam = MakeLParam(nLParam, 0);
						PostMessage(hWnd, Msg, nWParam, nLParam);
				}
				public static IntPtr StringToIntPtr(string a_strValue)
				{
						IntPtr pRet=IntPtr.Zero;
						GCHandle GCH = GCHandle.Alloc(a_strValue, GCHandleType.Pinned);
						pRet = GCH.AddrOfPinnedObject();
						GCH.Free();
						return pRet;
				}
		public const int WM_SETREDRAW = 0xB;
		public const int SW_FORCEMINIMIZE = 11; // Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.</para>
		public const int SW_HIDE = 0; // Hides the window and activates another window.</para>
		public const int SW_MAXIMIZE = 3; // Maximizes the specified window.</para>
		public const int SW_MINIMIZE = 6; // Minimizes the specified window and activates the next top-level window in the Z order.</para>
		public const int SW_RESTORE = 9; // Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.</para>
		public const int SW_SHOW = 5; // Activates the window and displays it in its current size and position.</para>
		public const int SW_SHOWDEFAULT = 10; // Sets the show state based run the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.</para>
		public const int SW_SHOWMAXIMIZED = 3; // Activates the window and displays it as a maximized window.</para>
		public const int SW_SHOWMINIMIZED = 2; // Activates the window and displays it as a minimized window.</para>
		public const int SW_SHOWMINNOACTIVE = 7; // Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.</para>
		public const int SW_SHOWNA = 8; // Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.</para>
		public const int SW_SHOWNOACTIVATE = 4; // Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.</para>
		public const int SW_SHOWNORMAL = 1; // Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.</para>

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int ShowWindow(IntPtr nHandle, int nCmd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetForegroundWindow(IntPtr nHandle);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetActiveWindow(IntPtr nHandle);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowPos(IntPtr nHandle, IntPtr nHandlwInsertAfter, int x, int y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr WindowFromPoint(System.Drawing.Point pt);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);



	}
}
