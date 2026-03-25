using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
		public static class LOG
		{

				public static void SEQ(string head, string msg)
				{
						APP.Logger_SW.DebugFormat("[SEQ]{0},{1}", head, msg);
				}
				public static void Debug(string head, string msg)
				{
						APP.Logger_SW.DebugFormat("[DEBUG]{0},{1}", head, msg);
				}
				public static void Debug(string head, string format, params object[] args)
				{
						APP.Logger_SW.DebugFormat("[DEBUG], {0},{1}", head, string.Format(format, args));
				}

				public static void Fatal(string head, string msg)
				{
						APP.Logger_SW.FatalFormat("[FATAL], {0},{1}", head, msg);
				}
				public static void Fatal(string head, string format, params object[] args)
				{
						APP.Logger_SW.FatalFormat("[FATAL], {0},{1}", head, string.Format(format, args));
				}

				#region [PM : Products Manager]

				public static void InfoSection(string format, params object[] args)
				{
						APP.Logger_PM.InfoFormat("[WAFER],{0}", string.Format(format, args));
				}
				
				public static void InfoLot(string format, params object[] args)
				{
						APP.Logger_PM.InfoFormat("[LOT],{0}", string.Format(format, args));
				}
				public static void InfoJIG(string format, params object[] args)
				{
						APP.Logger_PM.InfoFormat("[JIG],{0}", string.Format(format, args));
				}

				#endregion
				public static void ProcessEvent(string head, string msg)
				{
						APP.Logger_MC.InfoFormat("[Process], {0},{1}", head, msg);
				}


				public static void LogInOccurMsg(string head, string msg)
				{
						APP.Logger_MC.InfoFormat("[LOGIN], {0},{1}", head, msg);
				}

				public static void AlarmOccurMsg(int code, string msg)
				{
						APP.Logger_MC.InfoFormat("[ALARM],{0:D4},{1}", code, msg);
				}
				public static void ErrorOccurMsg(int code, string msg)
				{
						APP.Logger_MC.ErrorFormat("[ERROR],{0:D4},{1}", code, msg);
				}
				public static void Recipe(string head, string msg)
				{
						APP.Logger_MC.InfoFormat("[RECIPE], {0},{1}", head, msg);
				}

				public static void Recipe(string head, string format, params object[] args)
				{
						APP.Logger_MC.InfoFormat("[RECIPE], {0},{1}", head, string.Format(format, args));
				}

				public static void BTN_Event(string head, string msg)
				{
						APP.Logger_MC.InfoFormat("[BUTTON EVENT], {0},{1}", head, msg);
				}

		}
}
