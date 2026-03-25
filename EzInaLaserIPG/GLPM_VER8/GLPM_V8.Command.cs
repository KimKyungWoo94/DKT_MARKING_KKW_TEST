using EzIna.Commucation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Laser.IPG
{
    public enum GLPM_V8_CMD
    {
        /// <summary>
        /// Sent:STA
        /// <para>Response:STA: 6081 </para>
        /// </summary>
        [PacketAttribute("STA", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        STATUS,

        /// <summary>
        /// Sent:RET
        /// <para>Response:RET: 6081 </para>
        /// </summary>
        [PacketAttribute("RET", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_EMISSION_TIME,

        /// <summary>
        /// Sent: EMON
        /// <para>Response:EMON </para> 
        /// </summary>
        [PacketAttribute("EMON", "", "", false, true, true, true, enumPacketMode.SetOnly)]
        EMISSION_ON,
        /// <summary>
        /// Sent: EMOFF
        /// <para>Response:EMOFF </para> 
        /// </summary>
        [PacketAttribute("EMOFF", "", "", false, true, true, true, enumPacketMode.SetOnly)]
        EMISSION_OFF,


        /// <summary>
        /// Sent: RAWCT
        /// <para>Response:RAWCT: 28.9 </para> 
        /// </summary>
        [PacketAttribute("RAWCT", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        CASE_TEMP,
        /// <summary>
        /// Sent: RHPT
        /// <para>Response:RHPT: 28.9 </para> 
        /// </summary>
        [PacketAttribute("RHPT", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        HEAD_TEMP,
        /// <summary>
        /// Sent: RLDT
        /// <para>Response:RLDT: 22 
        /// </summary>
        [PacketAttribute("RLDT", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        LOWER_DECK_TEMP,
        /// <summary>
        /// Sent: RHT
        /// <para>Response:RHT: 22 
        /// </summary>
        [PacketAttribute("RHT", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        ACTUAL_CRYSTAL_TEMP,
        /// <summary>
        /// Sent: READHTT
        /// <para>Response:READHTT: 22 
        /// </summary>
        [PacketAttribute("READHTT", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        SET_POINT_CRYSTAL_TEMP,

        /// <summary>
        /// Sent: SCS 25.2
        /// <para>Response:RBR: 22 (This indicates that the backreflection level is 22%)</para> 
        /// </summary>
        [PacketAttribute("SCS", "", "", false, true, true, true, enumPacketMode.SetOnly)]
        SET_CURRENT_SET_POINT,
        /// <summary>
        /// Sent: RCS 
        /// <para>Response:RCS: 22.5</para> 
        /// </summary>
        [PacketAttribute("RCS", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_CURRENT_SET_POINT,
        /// <summary>
        /// 0: External ,1 :Internal
        /// <para>Sent: STRM 0</para> 
        /// <para>Response:STRM: 0</para> 
        /// </summary>
        [PacketAttribute("STRM", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        SET_TRIGGER_MODE,
        /// <summary>
        /// 0: External ,1 :Internal
        /// <para>Sent: STRM 0</para> 
        /// <para>Response:STRM: 0</para> 
        /// </summary>
        [PacketAttribute("RTRM", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_TRIGGER_MODE,
        /// <summary>    
        /// <para>Sent: STF 11000</para> 
        /// <para>Response:STF: 11000</para> 
        /// </summary>
        [PacketAttribute("STF", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        SET_TRIGGER_FREQ,

        /// <summary>    
        /// <para>Sent: RITF</para> 
        /// <para>Response:RITF: 11000</para> 
        /// </summary>
        [PacketAttribute("RITF", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_INT_TRIGGER_FREQ,

        /// <summary>    
        /// <para>Sent:RETF</para> 
        /// <para>Response:RETF: 11000</para> 
        /// </summary>
        [PacketAttribute("RATF", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_EXT_TRIGGER_FREQ,

        /// <summary>    
        /// Sent:RNTF
        /// <para>Response: RNTF: 0;10016 </para> 
        /// <para>(This indicates that the minimum trigger frequency for currently selected pulse</para>  
        /// <para>mode PW0 is 10016 Hz, which is closest to 10000 Hz).</para> 
        /// </summary>
        [PacketAttribute("RNTF", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_MIN_TRIGGER_FREQ,
        /// <summary>    
        /// Sent:RNTF
        /// <para>Response: RNTF: 0;312500 </para> 
        /// <para>(This indicates that the minimum trigger frequency for currently selected pulse</para>  
        /// <para>mode PW0 is 312500 Hz, which is closest to 300000 Hz).</para> 
        /// </summary>        
        [PacketAttribute("RXTF", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_MAX_TRIGGER_FREQ,

        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: SMODM 0 (or SMODM 1)</para>
        /// <para>Response:SMODM: 0 (or SMODM: 1)</para>
        /// </summary>
        [PacketAttribute("SMODM", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        SET_SOURCE_MODULATION,

        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: RMODM (or SMODM 1)</para>
        /// <para>Response:RMODM: 0 (or SMODM: 1)</para>
        /// </summary>
        [PacketAttribute("RMODM", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_SOURCE_MODULATION,

        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: SINTXA 1 or SINTXA 0 (or SMODM 1)</para>
        /// <para>Response:SINTXA:1 or 0 (or SMODM: 1)</para>
        /// </summary>
        [PacketAttribute("SINTXA", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        SET_POWER_SOURCE,
        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: RINTXA </para>
        /// <para>Response:RINTXA:1 or 0 </para>
        /// </summary>
        [PacketAttribute("RINTXA", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_POWER_SOURCE,

        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: SXCCTL 0 or 1 </para>
        /// <para>Response:SXCCTL:0 or 1 </para>
        /// </summary>
        [PacketAttribute("SXCCTL", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        SET_EMIISION_CONTROL,

        /// <summary>
        /// 0: External  , 1: Internal 
        /// <para>Sent: RXCCTL </para>
        /// <para>Response:RXCCTL:0 or 1 </para>
        /// </summary>
        [PacketAttribute("RXCCTL", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_EMIISION_CONTROL,
        /// <summary>        
        /// <para>Sent: RERR </para>
        /// <para>Response:RERR </para>
        /// </summary>
        [PacketAttribute("RERR", "", "", true, true, true, true, enumPacketMode.SetOnly)]
        RESET_ERR,

        /// <summary>        
        /// <para>Sent: RFV </para>
        /// <para>Response:RFV:4.0.2.1 </para>
        /// </summary>
        [PacketAttribute("RFV", "", "", true, true, true, true, enumPacketMode.GettingOnly)]
        READ_FIRMWARE_VERSION,

    }
    static partial class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Value"></param>
        /// <returns></returns>
        public static PacketAttribute GetPacketAttrFrom(this GLPM_V8_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                                         .First()
                                         .GetCustomAttribute<PacketAttribute>();
        }
        public static string GetPacketAttrstrCMD(this GLPM_V8_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                                         .First()
                                         .GetCustomAttribute<PacketAttribute>().strCMD;

        }
        public static string GetPacketAttrstrSetMark(this GLPM_V8_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                                         .First()
                                         .GetCustomAttribute<PacketAttribute>().strSetMark;

        }
        public static string GetPacketAttrstrGettingMark(this GLPM_V8_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                                         .First()
                                         .GetCustomAttribute<PacketAttribute>().strGettingMark;
        }
    }
}
