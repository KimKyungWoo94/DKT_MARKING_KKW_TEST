using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using log4net.Appender;
using log4net;
using log4net.Config;
using System.Reflection;

namespace EzIna
{
		///<see cref="https://gracefulprograming.tistory.com/122"/>
		internal class ListBoxLogAppender : AppenderSkeleton
		{
				private ListBox m_ListBox_MC;
				private ListBox m_ListBox_SW;
				private int m_nMaxLines;

				public ListBoxLogAppender(ListBox listBox_MC, ListBox listBox_SW, int maxLines = 1024)
				{
						m_ListBox_MC = listBox_MC;
						m_ListBox_MC.DoubleBuffered(true);
						m_ListBox_SW = listBox_SW;
						m_ListBox_SW.DoubleBuffered(true);
						m_nMaxLines = maxLines;

						// Repository를 연결
						((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.AddAppender(this);

						// find appender and extract patternlayout
						IAppender[] appenders = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).GetAppenders();
						foreach (IAppender a in appenders)
						{
								Type t = a.GetType();
								if (t.Equals(typeof(FileAppender)) || t.Equals(typeof(RollingFileAppender)))
								{
										this.Layout = ((FileAppender)a).Layout; // copy layout
										break;
								}
						}
				}

				protected override void Append(log4net.Core.LoggingEvent loggingEvent)
				{
						if (loggingEvent.LoggerName == "MC")
						{
								m_ListBox_MC.InvokeIfNeeded(() =>
								{
										m_ListBox_MC.Items.Add(RenderLoggingEvent(loggingEvent).Trim());
										m_ListBox_MC.SelectedIndex = m_ListBox_MC.Items.Count - 1;
										if (m_ListBox_MC.Items.Count > m_nMaxLines) m_ListBox_MC.Items.RemoveAt(0);
								});
						}


						if (loggingEvent.LoggerName == "SW")
						{
								m_ListBox_SW.InvokeIfNeeded(() =>
								{
										m_ListBox_SW.Items.Add(RenderLoggingEvent(loggingEvent).Trim());
										m_ListBox_SW.SelectedIndex = m_ListBox_SW.Items.Count - 1;
										if (m_ListBox_SW.Items.Count > m_nMaxLines) m_ListBox_SW.Items.RemoveAt(0);
								});
						}

						if (loggingEvent.LoggerName == "PM")
						{
								m_ListBox_SW.InvokeIfNeeded(() =>
								{
										m_ListBox_SW.Items.Add(RenderLoggingEvent(loggingEvent).Trim());
										m_ListBox_SW.SelectedIndex = m_ListBox_SW.Items.Count - 1;
										if (m_ListBox_SW.Items.Count > m_nMaxLines) m_ListBox_SW.Items.RemoveAt(0);
								});
						}
				}
		}

		public static class APP
		{
				//public static readonly ILog Logger	= LogManager.GetLogger(typeof(APP));
				public static readonly ILog Logger_MC = LogManager.GetLogger("MC");
				public static readonly ILog Logger_SW = LogManager.GetLogger("SW");
				public static readonly ILog Logger_PM = LogManager.GetLogger("PM");

				public static void InitLogger(ListBox listBox_MC, ListBox listBox_SW, ListBox listBox_PM, string xmlFile = "LogConfig.xml", int maxLines = 1024)
				{
						log4net.Util.LogLog.InternalDebugging = true;
						InitLogger(xmlFile);
						new ListBoxLogAppender(listBox_MC, listBox_SW, maxLines);
						FA.LOG.Debug("SYS", "InitLogger");
				}

				public static void CustomLog(log4net.Core.Level level, string msg)
				{
						Logger_MC.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, level, msg, null);
						Logger_SW.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, level, msg, null);
				}
				public static void InitLogger(string xmlFile = "LogConfig.xml")
				{
						// 파일이 존재하면 파일로 설정, 없으면 App.Config로 설정
						if (File.Exists(xmlFile))
								XmlConfigurator.Configure(new System.IO.FileInfo(xmlFile));
						else
								XmlConfigurator.Configure();

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

				public static string Version
				{
						get { return Application.ProductVersion; }
				}

				public static string Path
				{
						get
						{
								string path = System.Windows.Forms.Application.StartupPath;
								//string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
								string[] words = path.Split('\\');
								path = "";
								for (int i = 0; i < words.Length - 1; i++)
								{
										path += words[i] + @"\";
								}

								return path;

								// 				string strPath = Environment.CurrentDirectory;
								// 				strPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(strPath, @"..\..\..\"));
								// 				return strPath + @"bin";
						}
						// http://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
						//get { return System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\"; }
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
						ProcessStartInfo startInfo = new ProcessStartInfo(APP.Path + exe, args);
						Process.Start(startInfo);
				}
		}
}