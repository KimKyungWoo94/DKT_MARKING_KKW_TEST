using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;



//http://stackoverflow.com/questions/1142802/how-to-use-localization-in-c-sharp
namespace EzIna
{
		// http://stackoverflow.com/questions/16105097/why-isnt-messagebox-topmost

		public class Win32
		{
				#region Values & structs

				public const int WH_CBT = 5;
				public const int HCBT_ACTIVATE = 5;


				[StructLayout(LayoutKind.Sequential)]
				public struct RECT
				{
						public int left;
						public int top;
						public int right;
						public int bottom;
				}

				#endregion Values & structs


				#region Stock P/Invokes

				// Arg for SetWindowsHookEx()
				public delegate int WindowsHookProc(int nCode, IntPtr wParam, IntPtr lParam);

				[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
				public static extern int SetWindowsHookEx(int idHook, WindowsHookProc lpfn, IntPtr hInstance, int threadId);

				[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
				public static extern bool UnhookWindowsHookEx(int idHook);

				[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
				public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

				[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
				public static extern int GetWindowTextLength(IntPtr hWnd);

				[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
				public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

				[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
				public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

				[DllImport("user32.dll")]
				public static extern uint GetDlgItemText(IntPtr hDlg, int nIDDlgItem, [Out] StringBuilder lpString, int nMaxCount);

				[DllImport("user32.dll")]
				public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

				[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
				public static extern IntPtr GetParent(IntPtr hWnd);

				[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
				public static extern bool GetWindowRect(IntPtr handle, ref RECT r);

				[DllImport("user32.dll", SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

				#endregion Stock P/Invokes


				#region Simplified interfaces

				public static string GetClassName(IntPtr hWnd)
				{
						StringBuilder ClassName = new StringBuilder(100);
						//Get the window class name
						int nRet = GetClassName(hWnd, ClassName, ClassName.Capacity);
						return ClassName.ToString();
				}

				public static string GetWindowText(IntPtr hWnd)
				{
						// Allocate correct string length first
						int length = GetWindowTextLength(hWnd);
						StringBuilder sb = new StringBuilder(length + 1);
						GetWindowText(hWnd, sb, sb.Capacity);
						return sb.ToString();
				}

				public static string GetDlgItemText(IntPtr hDlg, int nIDDlgItem)
				{
						IntPtr hItem = GetDlgItem(hDlg, nIDDlgItem);
						if (hItem == IntPtr.Zero)
								return null;
						int length = GetWindowTextLength(hItem);
						StringBuilder sb = new StringBuilder(length + 1);
						GetWindowText(hItem, sb, sb.Capacity);
						return sb.ToString();
				}

				#endregion Simplified interfaces


		}
		public static class MsgBox
    {
				private static Win32.WindowsHookProc _hookProcDelegate = null;
				private static int _hHook = 0;
				private static string _title = null;
				private static string _msg = null;

				public static DialogResult Show(string msg, string title, MessageBoxButtons btns, MessageBoxIcon icon,MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
				{
						// Create a callback delegate
						_hookProcDelegate = new Win32.WindowsHookProc(HookCallback);

						// Remember the title & message that we'll look for.
						// The hook sees *all* windows, so we need to make sure we operate on the right one.
						_msg = msg;
						_title = title;

						// Set the hook.
						// Suppress "GetCurrentThreadId() is deprecated" warning. 
						// It's documented that Thread.ManagedThreadId doesn't work with SetWindowsHookEx()
#pragma warning disable 0618
						_hHook = Win32.SetWindowsHookEx(Win32.WH_CBT, _hookProcDelegate, IntPtr.Zero, AppDomain.GetCurrentThreadId());
#pragma warning restore 0618

						// Pop a standard MessageBox. The hook will center it.
						DialogResult rslt = MessageBox.Show(msg, title, btns, icon,defaultButton);

						// Release hook, clean up (may have already occurred)
						Unhook();
						
						return rslt;
				}

				private static void Unhook()
				{
						Win32.UnhookWindowsHookEx(_hHook);
						_hHook = 0;
						_hookProcDelegate = null;
						_msg = null;
						_title = null;
				}

				private static int HookCallback(int code, IntPtr wParam, IntPtr lParam)
				{
						int hHook = _hHook; // Local copy for CallNextHookEx() JIC we release _hHook

						// Look for HCBT_ACTIVATE, *not* HCBT_CREATEWND:
						//   child controls haven't yet been created upon HCBT_CREATEWND.
						if (code == Win32.HCBT_ACTIVATE)
						{
								string cls = Win32.GetClassName(wParam);
								if (cls == "#32770")  // MessageBoxes are Dialog boxes
								{
										string title = Win32.GetWindowText(wParam);
										string msg = Win32.GetDlgItemText(wParam, 0xFFFF);  // -1 aka IDC_STATIC
										if ((title == _title) && (msg == _msg))
										{
												CenterWindowOnParent(wParam);
												Unhook(); // Release hook - we've done what we needed
										}
								}
						}
						return Win32.CallNextHookEx(hHook, code, wParam, lParam);
				}

				// Boilerplate window-centering code.
				// Split out of HookCallback() for clarity.
				private static void CenterWindowOnParent(IntPtr hChildWnd)
				{
						// Get child (MessageBox) size
						Win32.RECT rcChild = new Win32.RECT();
						Win32.GetWindowRect(hChildWnd, ref rcChild);
						int cxChild = rcChild.right - rcChild.left;
						int cyChild = rcChild.bottom - rcChild.top;

						// Get parent (Form) size & location
						IntPtr hParent = Win32.GetParent(hChildWnd);
						Win32.RECT rcParent = new Win32.RECT();
						Win32.GetWindowRect(hParent, ref rcParent);
						int cxParent = rcParent.right - rcParent.left;
						int cyParent = rcParent.bottom - rcParent.top;

						// Center the MessageBox on the Form
						int x = rcParent.left + (cxParent - cxChild) / 2;
						int y = rcParent.top + (cyParent - cyChild) / 2;
						uint uFlags = 0x15; // SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE;
						Win32.SetWindowPos(hChildWnd, IntPtr.Zero, x, y, 0, 0, uFlags);
				}


		
		   /* public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
											
						return MessageBox.Show(text, Application.ProductName, buttons, icon, defaultButton);
        }
				public static DialogResult Show(string text,Form a_pOwner ,MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
				{
						DialogResult Result=DialogResult.Cancel;
						using (Form pRet = new Form())
						{
							  
								pRet.StartPosition = FormStartPosition.Manual;
								pRet.ShowInTaskbar = false;
								pRet.FormBorderStyle=FormBorderStyle.None;
								pRet.Size = new System.Drawing.Size(1, 1);
								pRet.Location = new Point(a_pOwner.Location.X + (a_pOwner.Width - a_pOwner.Width) / 2, a_pOwner.Location.Y + (a_pOwner.Height - a_pOwner.Height) / 2);
								pRet.Show();
								pRet.Focus();
								pRet.BringToFront();
								pRet.TopMost = true;
								Result=MessageBox.Show(pRet,text, Application.ProductName, buttons, icon, defaultButton);
								
						}		
										
						return Result;
				}*/

				public static void Show(string text)
        {
            Show(text,Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
				
				public static void Error(string text)
        {
            Show(text,Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
				
				public static void Error(string fmt, params object[] args)
        {
            Error(string.Format(fmt, args));
        }
				public static void Error(string fmt,Control a_pParent, params object[] args)
				{
						Error(string.Format(fmt, args),a_pParent);
				}
				public static void Warning(string text)
        {
            Show(text,Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
			
				public static bool Confirm(string text)
        {
            return Show(text,Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
				
				public static bool Confirm(string fmt, params object[] args)
        {
            return Confirm(string.Format(fmt, args));
        }
			
		}

    public static class Utils
    {
        # region Strings

        /// Unicode string to ANSI String
        /// http://msdn.microsoft.com/ko-kr/library/kdcak6ye(v=vs.110).aspx
        public static string UnicodeToAnsi(string sUnicode)
        {
            // CreateItem two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(sUnicode);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[Encoding.ASCII.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);

            return new string(asciiChars);
        }

        /// WIN API Pointer to string
        /// <param name="P">IntPtr</param>
        /// <returns></returns>
        public static string PtrToStr(IntPtr P)
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringAuto(P);
        }

        /// Bitmap Data to Base64
        public static string BitmapToStr(Bitmap bmp)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) // BMP -> Base64
            {
                bmp.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// Load Bitmap from Base64
        public static Bitmap StrToBitmap(string s)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(s)))
            {
                ms.Position = 0;
                return new Bitmap(ms);
            }
        }

        public static bool ExtractWords(string s, char sep, ref string s1, ref string s2)
        {
            string[] words = s.Split(sep);

            if (words.Length < 2) return false;

            s1 = words[0];
            s2 = words[1];

            return true;
        }
        public static bool ExtractWords(string s, char sep, ref string s1, ref string s2, ref string s3)
        {
            string[] words = s.Split(sep);

            if (words.Length < 3) return false;

            s1 = words[0];
            s2 = words[1];
            s3 = words[2];

            return true;
        }
        public static bool ExtractWords(string s, char sep, ref double d1, ref double d2)
        {
            string s1 = "", s2 = "";
            double tmp1, tmp2;
            
            if (!ExtractWords(s, sep, ref s1, ref s2)) return false;

            if (double.TryParse(s1, out tmp1) && double.TryParse(s2, out tmp2))
            {
                d1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                d2 = tmp2;
                return true;
            }
            else
                return false;
        }
        public static bool ExtractWords(string s, char sep, ref double d1, ref double d2, ref double d3)
        {
            string s1 = "", s2 = "", s3 = "";
            double tmp1, tmp2, tmp3;
            
            if (!ExtractWords(s, sep, ref s1, ref s2, ref s3)) return false;

            if (double.TryParse(s1, out tmp1) && double.TryParse(s2, out tmp2) && double.TryParse(s3, out tmp3))
            {
                d1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                d2 = tmp2;
                d3 = tmp3;
                return true;
            }
            else
                return false;
        }
        public static bool ExtractWords(string s, char sep, ref int i1, ref int i2)
        {
            string s1 = "", s2 = "";
            int tmp1, tmp2;
            
            if (!ExtractWords(s, sep, ref s1, ref s2)) return false;

            if (int.TryParse(s1, out tmp1) && int.TryParse(s2, out tmp2))
            {
                i1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                i2 = tmp2;
                return true;
            }
            else
                return false;
        }
        public static bool ExtractWords(string s, char sep, ref int i1, ref int i2, ref int i3)
        {
            string s1 = "", s2 = "", s3 = "";
            int tmp1, tmp2, tmp3;
            
            if (!ExtractWords(s, sep, ref s1, ref s2, ref s3)) return false;

            if (int.TryParse(s1, out tmp1) && int.TryParse(s2, out tmp2) && int.TryParse(s3, out tmp3))
            {
                i1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                i2 = tmp2;
                i3 = tmp3;
                return true;
            }
            else
                return false;
        }

        # endregion

        #region Struct & array

        /// <summary>
        /// Struct to Byte Array
        /// http://bytes.com/topic/c-sharp/answers/236808-how-convert-structure-byte-array
        /// </summary>
        public static byte[] StructToBytes(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);

            Marshal.Copy(ptr, arr, 0, len);

            Marshal.FreeHGlobal(ptr);

            return arr;
        }
        public static byte[] StructToBytes(int len, object obj)
        {
            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);

            Marshal.Copy(ptr, arr, 0, len);

            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        /// <summary>
        /// Byte Array to Struct
        /// http://bytes.com/topic/c-sharp/answers/236808-how-convert-structure-byte-array
        /// </summary>
        public static void BytesToStruct<T>(byte[] bytearray, ref T obj, int index = 0)
        {
            try
            {
                int len = Marshal.SizeOf(obj);

                IntPtr i = Marshal.AllocHGlobal(len);

                Marshal.Copy(bytearray, index, i, len);

                obj = Marshal.PtrToStructure<T>(i);

                Marshal.FreeHGlobal(i);
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// http://stackoverflow.com/questions/415291/best-way-to-combine-two-or-more-byte-arrays-in-c-sharp
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static byte[] CombineArray(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        #endregion Struct & array

        # region Directories

        /// <summary>
        /// sBaseDir 에서 nUpCount 만큼 상위 이동한 Directory 이름을 반환
        /// </summary>
        /// <param name="sBaseDir"></param>
        /// <param name="nUpCount"></param>
        /// <returns></returns>
        public static string GetDir(string sBaseDir, int nUpCount)
        {
            int nStartIndex = sBaseDir.Length - 1;
            if (sBaseDir.ElementAt(nStartIndex) == '\\') nStartIndex--;

            for (int i = 0; i < nUpCount; i++)
                nStartIndex = sBaseDir.LastIndexOf('\\', nStartIndex) - 1;

            return sBaseDir.Remove(nStartIndex + 2);
        }

        /// <summary>
        /// 상대 경로로 표현된 디렉토리를 BaseDir를 기반으로하는 풀 디렉토리명으로 만들어 반환
        /// </summary>
        /// <param name="baseDir"></param>
        /// <param name="relativeDir"></param>
        /// <returns></returns>
        public static string GetDir(string baseDir, string relativeDir)
        {
            if (relativeDir.IndexOf("..") == 0)
            {
                // http://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-within-a-string
                int nCnt = Regex.Matches(relativeDir, "..").Count;

                return Utils.GetDir(baseDir, nCnt);
            }
            else
            {
                if (relativeDir[0] == '.' || relativeDir[0] == '\\')
                    return baseDir + relativeDir.Substring(1);
                else
                    return baseDir + relativeDir;
            }
        }

        /// <summary>
        /// 상대 경로로 표현된 파일 이름의 풀 패쓰명을 반환
        /// </summary>
        /// <param name="sRelativeFileName"></param>
        /// <returns></returns>
        public static string GetFile(string sRelativeFileName)
        {
            if (sRelativeFileName.IndexOf("..\\") == 0)
            {
                // http://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-within-a-string
                int nCnt = Regex.Matches(sRelativeFileName, Regex.Escape("..\\")).Count;

                return Utils.GetDir(Utils.Path, nCnt) + sRelativeFileName.Substring(nCnt * 3);
            }
            else
            {
                if (sRelativeFileName[0] == '.' || sRelativeFileName[0] == '\\')
                    return Utils.Path + sRelativeFileName.Substring(1);
                else
                    return Utils.Path + sRelativeFileName;
            }
        }
        /// <summary>
        /// 상대 경로로 표현된 파일 이름의 FileInfo를 반환
        /// </summary>
        /// <param name="sRelativeFileName"></param>
        /// <returns></returns>
        public static System.IO.FileInfo GetFileInfo(string sRelativeFileName) { return new System.IO.FileInfo(GetFile(sRelativeFileName)); }
        /// <summary>
        /// 같은 파일인지 체크
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public static bool SameFileName(string file1, string file2) { return file1.Equals(file2, StringComparison.OrdinalIgnoreCase); }

        public static string CombineDir(params string[] paths) { return System.IO.Path.Combine(paths).Replace("\\.\\", "\\"); }
        public static string CombineDir(string path1, string path2) { return System.IO.Path.Combine(path1, path2).Replace("\\.\\", "\\"); }
        public static string CombineDir(string path1, string path2, string path3) { return System.IO.Path.Combine(path1, path2, path3).Replace("\\.\\", "\\"); }
        public static string CombineDir(string path1, string path2, string path3, string path4) { return System.IO.Path.Combine(path1, path2, path3, path4).Replace("\\.\\", "\\"); }



#endregion

        #region Math

        public static int Round(double value)
        {
            return (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }

        public static double ToRadian(double angleDeg)
        {
            return Math.PI / 180.0 * angleDeg;
        }

        public static double ToDegree(double angleDeg)
        {
            return 180.0 / Math.PI * angleDeg;
        }

        public static double PointToPointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
        public static bool GetBit(uint value, int bitIndex)
        {
            return (value & (1 << bitIndex)) != 0;
        }

        public static void SetBit(ref uint value, int bitIndex, bool state)
        {
            if (state)
                value |= ((uint)1 << bitIndex);
            else
                value &= ((uint)1 << bitIndex) ^ 0xFFFFFFFF;
        }
        public static void SetBit(ref byte value, int bitIndex, bool state)
        {
            if (state)
                value |= (byte)((byte)1 << bitIndex);
            else
                value &= (byte)(((byte)1 << bitIndex) ^ 0xFF);
        }

        public static void ToggleBit32(ref Int32 value, int bitIndex)
        {
            ToggleBit32(ref value, bitIndex);
        }

        #endregion

        #region Graphic

        /// Alpha Blending Drawing
        /// http://www.codeproject.com/Articles/5034/How-to-implement-Alpha-blending
        /// https://msdn.microsoft.com/ko-kr/library/system.drawing.imaging.colormatrix(v=vs.110).aspx
        /// <param name="aGraphics">Dest Graphics</param>
        /// <param name="aImage">Source Image</param>
        /// <param name="alpha">Alpha Blending Value (0..1)</param>
        public static void DrawAlpha(Graphics aDstGraphics, Image aSrcImage, int aDstX, int aDstY, float alpha)
        {
            ColorMatrix cm = new ColorMatrix(new float[][] {
                new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            });
            cm.Matrix33 = alpha;

            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            aDstGraphics.DrawImage(aSrcImage, new Rectangle(aDstX, aDstY, aSrcImage.Width, aSrcImage.Height), 0, 0, aSrcImage.Width, aSrcImage.Height, GraphicsUnit.Pixel, ia);
        }

        public static void DrawAlpha(Image aDstImage, Image aSrcImage, int aDstX, int aDstY, float alpha)
        {
            using (Graphics g = Graphics.FromImage(aDstImage))
            {
                Utils.DrawAlpha(g, aSrcImage, aDstX, aDstY, alpha);
            }
        }

        #endregion

        #region Geometry

        /// 두점을 대각으로 가지는 Rectangle 생성
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static Rectangle CreateRect_From2Point(int x1, int y1, int x2, int y2)
        {
            int x = Math.Min(x1, x2);
            int y = Math.Min(y1, y2);

            return new Rectangle(x, y, Math.Abs(x1 - x2 + 1), Math.Abs(y1 - y2 + 1));
        }

        public static Rectangle CreateRect_From2Point(Point p1, Point p2)
        {
            return CreateRect_From2Point(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static double Hypot(double dx, double dy) { return Math.Sqrt(dx * dx + dy * dy); }

        public static double Distance(double x1, double y1, double x2, double y2)
        {
            return Hypot(x2 - x1, y2 - y1);
        }

        #endregion

        /// This method accepts two strings the represent two files to
        /// compare. A return value of 0 indicates that the contents of the files
        /// are the same. A return value of any other value indicates that the
        /// files are not the same.
        /// https://support.microsoft.com/ko-kr/kb/320348
        public static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // ReadFrom and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // ReadFrom one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is
            // equal to "file2byte" at this point only if the files are
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        public static int MinEnum(Type t)
        {
            return Enum.GetValues(t).Cast<int>().Min();
        }
        public static int MaxEnum(Type t)
        {
            return Enum.GetValues(t).Cast<int>().Max();
        }
        public static void MakeComboItemsByEnum(ComboBox.ObjectCollection cbItems, Type enumType)
        {
            cbItems.Clear();
            foreach (string s in Enum.GetNames(enumType))
                cbItems.Add(s);
        }

        private static List<string> _RecognisedImageExtensions = null;

        public static bool IsRecognisedImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            if (String.IsNullOrEmpty(targetExtension))
                return false;
            else
                targetExtension = "*" + targetExtension.ToLowerInvariant();

            if (_RecognisedImageExtensions == null)
            {
                _RecognisedImageExtensions = new List<string>();

                foreach (System.Drawing.Imaging.ImageCodecInfo imageCodec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                    _RecognisedImageExtensions.AddRange(imageCodec.FilenameExtension.ToLowerInvariant().Split(";".ToCharArray()));
            }

            foreach (string extension in _RecognisedImageExtensions)
            {
                if (extension.Equals(targetExtension))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetParentDir(string strPath)
        {
            string path			= System.IO.Path.GetDirectoryName(strPath);
            string parentDir	= Directory.GetParent(path).FullName + "\\";
             
            return parentDir;
        }

        public static bool Exists(string ID, out Mutex mutex)
        {
            bool createNew = false;

            mutex = new Mutex(true, ID, out createNew);

            if (!createNew)
            {
                MsgBox.Error("Application already started!");

                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        WinAPIs.SetForegroundWindow(process.MainWindowHandle);
                        break;
                    }
                }
            }

            return !createNew;
        }

        // get exe application names
        public static string Name
        {
            // http://stackoverflow.com/questions/14829689/how-to-get-exe-application-name-and-version-in-c-sharp-compact-framework
            get { return System.IO.Path.GetFileNameWithoutExtension(ExeName); }
        }

        public static string ExeName
        {
            get { return Assembly.GetExecutingAssembly().CodeBase.ToString(); }
        }

//         public static string Version
//         {
//             get { return Application.ProductVersion; }
//         }

        public static string Version
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
#if DEBUG
                return string.Format("{0} VER : {1} Debug Mode", fvi.ProductName, fvi.FileVersion);

#else
                return string.Format("{0} VER : {1}", fvi.ProductName, fvi.FileVersion);
#endif
            }
        }

        public static string Path
        {
            // http://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
            get
            {
                string strRtn = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\";
                strRtn = GetParentDir(strRtn);
                return strRtn;
            }
        }

        public static string IniFileName
        {
            get { return Path + Name + ".ini"; }
        }

        public static void DeleteOldLogFiles(string path, string pattern, TimeSpan duration, SearchOption option = SearchOption.AllDirectories)
        {
            Task.Run(() =>
            {
                DateTime now = DateTime.Now;
                string[] files = Directory.GetFiles(path, pattern, option);

                foreach (var f in files)
                    if (now.Subtract(File.GetLastWriteTime(f)) > duration)
                        File.Delete(f);
            });
        }

        // x64 not supported , use AnyCPU
        public static void RunExec(string exe, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Utils.Path + exe, args);
            Process.Start(startInfo);
        }


    }


	// https://stackoverflow.com/questions/1189144/c-sharp-non-boxing-conversion-of-generic-enum-to-int
	/// <summary>
	/// Class to cast to type <see cref="T"/>
	/// </summary>
	/// <typeparam name="T">Target type</typeparam>
	public static class CastTo<T>
	{
		/// <summary>
		/// Casts <see cref="S"/> to <see cref="T"/>.
		/// This does not cause boxing for value types.
		/// Useful in generic methods.
		/// </summary>
		/// <typeparam name="S">Source type to cast from. Usually a generic type.</typeparam>
		public static T From<S>(S s)
		{
			return Cache<S>.caster(s);
		}

		private static class Cache<S>
		{
			public static readonly Func<S, T> caster = Get();

			private static Func<S, T> Get()
			{
				var p = System.Linq.Expressions.Expression.Parameter(typeof(S), "s");
				var c = System.Linq.Expressions.Expression.ConvertChecked(p, typeof(T));
				return System.Linq.Expressions.Expression.Lambda<Func<S, T>>(c, p).Compile();
			}
		}
	}
}