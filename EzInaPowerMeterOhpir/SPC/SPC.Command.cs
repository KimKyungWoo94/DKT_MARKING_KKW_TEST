using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Reflection;

namespace EzIna.PowerMeter.Ohpir
{
    
        enum SPC_CMD
        {
            /// <summary>
            /// Ping /           
            /// <returns>*[CR] </returns>
            /// </summary>
            [PacketAttribute("$HP", "", "", false, false, false, true, enumPacketMode.GettingOnly)]
            PING_TEST,
            /// <summary>
            /// Send Version  /    
            /// <returns>*UO1.23[CR] </returns>
            /// </summary>            
            [PacketAttribute("$VE", "", "", false, false, false, true, enumPacketMode.GettingOnly)]
            VERSION,
            /// <summary>
            /// Resets the MCU software            
            /// <returns>*UO1.23[CR] </returns>
            /// </summary>      
            [PacketAttribute("$RE", "", "", false, false, true, true, enumPacketMode.SetOnly)]
            RESET_DEVICE,
            /// <summary>
            /// Returns or sets Mains Setting for head. Mains setting adjusts measurement period used for A-to-D, 
            /// <para>20ms for 50Hz or 16.666ms for 60Hz, to allow optimum filtering of noise at the mains frequency of the environment. </para>
            /// <para>Default setting is 50Hz. Setting this value is only important if power signal on head contains elements </para>
            /// <para>of 50/60Hz or 100/120Hz, or if working in high electrical noise environment or with head cover removed.            </para>
            /// <para>Set</para>
            /// <para>  $MA 1[CR] or $MA 2[CR] →*[CR]  </para>        
            /// </summary>     
            [PacketAttribute("$MA", "", "", false, false, true, true, enumPacketMode.Both)]
            MAINS_SETTING_HEAD,
            /// <summary>
            /// {Initiates a zero of head. After sending this command, zeroing will be in progress for approximately 22s 
            /// <para>during which most other commands, EXCEPT $ZQ or $HP, should not be used, as the head is not functioning in normal mode} </para>
            /// <para>Get </para>
            /// <para>$ZR[CR] –> *[CR]</para>
            /// </summary>
            [PacketAttribute("$ZR", "", "", false, false, true, true, enumPacketMode.SetOnly)]
            ZERO_HEAD,
            /// <summary>
            /// $ZQ[CR] -> *status of last zero performed using $ZR>[CR] {This should be used after $ZR to find the status 
            /// of the last zero command. Returns:
            /// <para>"*ZEROING NOT STARTED[CR]" if zero has not yet been performed since power-up, or since last $RE reset </para>
            /// <para>"*ZEROING IN PROGRESS[CR]" if zeroing is still in progress </para>
            /// <para>"*ZEROING FAILED[CR]" if zeroing failed (either a bad zero value was measured or failed to write values to EEROM) </para>
            /// <para>"*ZEROING COMPLETED[CR]" zeroing completed successfully </para>
            /// </summary>
            [PacketAttribute("$ZQ", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            ZERO_HEAD_STATUS,
            /// <summary>
            /// $HI[CR] -> *(2 letter head code) (S/N of head) (name of head) (capability code)
            ///<para>Returns information on the head, including its name and S/N</para>
            /// </summary>
            [PacketAttribute("$HI", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            HEAD_INFO,
            /// <summary>
            /// $SP[CR] ->*<Latest power measurement with defined format>[CR]
            /// <para>If power exceeds 110% of the Full-Scale Power, $SP returns "*OVER[CR]". </para>
            /// <para>If A-to-D is saturated (within 1% of full-scale A-to-D output), $SP returns "SAT[CR]" } </para>
            /// </summary>
            [PacketAttribute("$SP", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            MEASURE_VALUE,
            /// <summary>
            /// $WN 0[CR] –> *[CR] {changes range: 0,1,2,3,… parameter, 0=highest, or least sensitive, range. 
            /// </summary>
            [PacketAttribute("$WN", "", "", false, false, true, true, enumPacketMode.SetOnly)]
            HEAD_RANGE,

			/// <summary>
			//$WN 0[CR] –> *[CR] {changes range: 0,1,2,3,… parameter, 0=highest, or least sensitive, range. 
			//Power-up settings will be defined using $HC command, see below. NOTE that after using the $WN command,
			// the software should wait ~3s before resuming power measurements with the $SP command} 
            /// </summary>
            [PacketAttribute("$WI", "", "", false, false, true, true, enumPacketMode.SetOnly)]
            SET_WAVE_LENGTH,
            /// <summary>
            /// $RN 0[CR] –> *[CR] {changes range: 0,1,2,3,… parameter, 0=highest, or least sensitive, range. 
            /// </summary>
            [PacketAttribute("$RN", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            READ_HEAD_RANGE,
            /// <summary>
            ///  $RI[CR] –> *1[CR] {Read Wavelength Index of head being used at present, as defined in $WI command or at startup
            /// </summary>
            [PacketAttribute("$RI", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            READ_WAVE_LENGTH,

            /// <summary>
            ///  $AW[CR] -> (list of all defined wavelengths)[CR] {Returns the wavelengths defined for this head in the following format: 
            /// <para>DISCRETE MODE: "DISCRETE 1 VIS YAG ERB [CR]": Returns the mode; Wavelength index currently being used (1); </para>
            /// <para>and each of the defined laser ranges as they appear in the head EEROM, up to a maximum of 5</para>
            /// </summary>
            [PacketAttribute("$AW", "", "", false, false, true, true, enumPacketMode.GettingOnly)]
            READ_ALL_WAVE_LENGTHS,
            /// <summary>
            /// $HC[CR] -> *[CR] {Saves the CURRENT SETTINGS of power scale and wavelength as the power-up defaults} 
            /// <para> a. Set desired power scale using $WN command </para>
            /// <para>b. Set desired wavelength index using $WI command}</para>
            /// <para>c. Save chosen settings using $HC command </para>
            /// <para>d. (Optional): Run $RE to startup software, or power off and power on, and then run $RN and $RW </para>
            /// <para> to check power-up settings are correct</para>
            /// </summary>
            [PacketAttribute("$HC", "", "", false, false, true, true, enumPacketMode.SetOnly)]
            SAVE_CURRENT_SET,
        }
    
	 static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Value"></param>
        /// <returns></returns>
        public static PacketAttribute GetPacketAttrFrom(this SPC_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>();
        }
        public static string GetPacketAttrstrCMD(this SPC_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>().strCMD;

        }
        public static string GetPacketAttrstrSetMark(this SPC_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>().strSetMark;

        }
        public static string GetPacketAttrstrGettingMark(this SPC_CMD a_Value)
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
