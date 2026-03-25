using EzIna.Commucation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.TempMeasure.OMEGA
{
		enum CN8PT_CMD_OMEGA
		{
				[PacketAttribute("100","W","R", true, true, true, true, enumPacketMode.Both)]
				SENSOR_INFO,
			  [PacketAttribute("110","","G", true, true, true, true, enumPacketMode.GettingOnly)]
				CURRENT_VALUE,

				[PacketAttribute("311", "W", "R", true, true, true, true, enumPacketMode.Both)]
				VALUE_CONTINOUS_SEND_MODE,

		}
		static class EnumHelper
		{


				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Value"></param>
				/// <returns></returns>
				public static PacketAttribute GetPacketAttrFrom(this CN8PT_CMD_OMEGA a_Value)
				{
						return a_Value.GetType().GetMember(a_Value.ToString())
													 .First()
													 .GetCustomAttribute<PacketAttribute>();
				}
				public static string GetPacketAttrstrCMD(this CN8PT_CMD_OMEGA a_Value)
				{
						return a_Value.GetType().GetMember(a_Value.ToString())
													 .First()
													 .GetCustomAttribute<PacketAttribute>().strCMD;

				}
				public static string GetPacketAttrstrSetMark(this CN8PT_CMD_OMEGA a_Value)
				{
						return a_Value.GetType().GetMember(a_Value.ToString())
													 .First()
													 .GetCustomAttribute<PacketAttribute>().strSetMark;

				}
				public static string GetPacketAttrstrGettingMark(this CN8PT_CMD_OMEGA a_Value)
				{
						return a_Value.GetType().GetMember(a_Value.ToString())
													 .First()
													 .GetCustomAttribute<PacketAttribute>().strGettingMark;
				}
				/// <summary>
				/// Attribute Generic Function
				/// </summary>
				/// <typeparam name="T">Attribute Class</typeparam>
				/// <param name="a_Value">Enum Value</param>
				/// <returns>Attribute Class</returns>

		}
}
