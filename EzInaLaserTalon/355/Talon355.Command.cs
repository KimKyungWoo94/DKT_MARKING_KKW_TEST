using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Reflection;

namespace EzIna.Laser.Talon
{
    /// <summary>
    /// TALON355 Laser Command , Getting 시 Query Mark 맨앞 or 맨뒤에 하던 상관없음
    /// </summary>
    public enum TALON355_CMD
    {
        /// <summary>
        /// Laser Diode drive Current(amps)        
        /// <para>C1:6.30 [CR]	Set the diode current to 6.30 Amps</para>
        /// <para>?C1 [CR]	    What is the laser diode measured current for diode #1?</para>
        /// <para>6.32 [CR][LF]	The laser diode current for diode #1 is 6.32A.</para>        
        /// </summary>
        [PacketAttribute("C1", ":", "?", false, false,false,true,enumPacketMode.Both)]
        DIODE_CURRENT,

			

	      /// <summary>
				/// The present state of laser diode emission
				/// <para>?D[CR] What is the On/Off Status of diode emission? </para>
				/// <para>1[CR][LF] </para>
				/// </summary>
				[PacketAttribute("D", ":", "?", false, false,false,true, enumPacketMode.GettingOnly)]
        DIODE_EMISSION_STATE,
        /// <summary>
        /// Set or Read EPRF
        /// <para> EPRF:100000[CR] Set the external EPRF to 100KHz </para>
        /// <para> ?EPRF[CR] What is the EPRFEXT setting </para>
        /// <para> 100000 [CR][LF] The external EPRF is set to 100 KHz.</para>
        /// </summary>
        [PacketAttribute("EPRF", ":", "?", false, false,false,true,enumPacketMode.Both)]
        EPRF_VALUE,
        /// <summary>
        /// The Current System status as an ASCII string 
        /// <para>?F[CR] </para>
        /// <para>SYSTEM OK[CR][LF]</para>
        /// <para>KEY ILK[CR][LF]</para>
        /// <para>SYS ILK[CR][LF]</para>
        /// </summary>
        [PacketAttribute("F", ":", "?", false, false,false,true,enumPacketMode.GettingOnly)]
        SYSTEM_STATUS_ASCII,
        /// <summary>
        /// The 16 Event System Status History , Data Delimited ; (Semicolon)
        /// <para>?FH[CR] </para>
        /// <para>011;013;000;002;000;000;000;000;000,000;000;000;000;000,000;000;000;000;000[CR][LF]</para>
        /// </summary>
        [PacketAttribute("FH", ":", "?", false, false,false,true,enumPacketMode.GettingOnly)]
        SYSTEM_STATUS_HISTORY,
        /// <summary>
        /// Internal Q-Switch to Stop/Start Laser Pulsing
        /// <para>G:1[CR]</para>
        /// <para>?G[CR]</para>
        /// <para>0[CR][LF]</para>
        /// </summary>
        [PacketAttribute("G", ":", "?", false, false,false,true,enumPacketMode.Both)]
        GATE_SWITCH,
        /// <summary>
        /// Enable external Gate Control 
        /// <para>GEXT:1[CR]</para>
        /// <para>?GEXT[CR]</para>
        /// <para>1[CR][LF]</para>
        /// </summary>
        [PacketAttribute("GEXT", ":", "?", false, false,false,true,enumPacketMode.Both)]
        ENABLE_EXT_GATE,
        /// <summary>
        /// accumulated operating hours for a current spot On the THG harmonic crystal
        /// <para>?MTR:THR[CR]</para>
        /// <para>-128.5[CR][LF]</para>        
        /// </summary>
        [PacketAttribute("MTR:THR", ":", "?", false, false,false,true,enumPacketMode.GettingOnly)]
        THG_CRYSTAL_SPOT_HOURS,
        /// <summary>
        /// moves the THG crystal to new spot position , Set Range 1~15  -99 → Moving now
        /// <para>MTR:TSPOT:2[CR]</para>
        /// <para>-99[CR][LF]</para>        
        /// </summary>
        [PacketAttribute("MTR:TSPOT", ":", "?", false, false,false,true,enumPacketMode.Both)]
        MOVE_THG_CRYSTAL_SPOT_POS,
        /// <summary>
        /// Laser Emission OFF
        /// <para>OFF[CR]</para>
        /// </summary>
        [PacketAttribute("OFF", ":", "?", false, false,false,false,enumPacketMode.SetOnly)]
        EMISSION_OFF,
        /// <summary>
        /// Laser Emission ON
        /// <para>ON[CR]</para>
        /// </summary>
        [PacketAttribute("ON", ":", "?", false, false,false,false,enumPacketMode.SetOnly)]
        EMISSION_ON,
        /// <summary>
        /// reads the internal laser power emitted by the laser (in watts)  Range: 0.0 ≤ f ≤ 20.00
        /// <para>?P[CR]</para>
        /// <para>10.00[CR][LF]</para>
        /// </summary>
        [PacketAttribute("P", ":", "?", false, false,true,true,enumPacketMode.GettingOnly)]
        POWER_EMITTED,
        /// <summary>
        /// the internal Q-switch pulse-repetition frequency for the laser (in Hertz). 
        /// <para>Range: 0 ≤ f ≤ 2000000 and Q = 0</para>
        /// <para>Q:50000[CR]</para>
        /// <para>?:Q[CR]</para>
        /// <para>50000[CR][LF]</para>
        /// </summary>
        [PacketAttribute("Q", ":", "?", false, false,false,true,enumPacketMode.Both)]
        REPETITION_RATE,
        /// <summary>
        /// starts (1) or stops (0) the SHG temperature auto-tune function
        /// <para>SAUTO:1[CR]</para>
        /// <para>?SAUTO[CR]</para>
        /// <para>1[CR][LF]</para>
        /// </summary>
        [PacketAttribute("SAUTO", ":", "?", false, false, false, true,enumPacketMode.Both)]
        SHG_AUTO_TUNE,
        /// <summary>
        /// the SHG crystal temperature regulation point ,Range: 20000 ≤ n ≤ 65535
        /// <para>SHG:29100[CR]</para>
        /// <para>?SHG[CR]</para>
        /// <para>29100[CR][LF]</para>
        /// </summary>
        [PacketAttribute("SHG", ":", "?", false, false, false, true,enumPacketMode.Both)]
        SHG_CRYSTAL_TEMP,
        /// <summary>
        /// SHG crystal temperature regulation point Range: 20000 ≤ n ≤ 65535
        /// <para>?SHGS[CR]</para>
        /// <para>35000[CR][LF]</para>
        /// </summary>
        [PacketAttribute("SHGS", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        SHG_CRYSTAL_TEMP_LAST_SET,
        /// <summary>
        /// opens ＆ closes the shutter on the laser head
        /// <para>SHT:0[CR]</para>
        /// <para>?SHT[CR]</para>
        /// <para>1[CR][LF]</para>
        /// </summary>
        [PacketAttribute("SHT", ":", "?", false, false, false, true,enumPacketMode.Both)]
        SHUTTER_ENABLE,
        /// <summary>
        /// opens ＆ closes the shutter on the laser head     
        /// <para>?SHT[CR]</para>
        /// <para>1[CR][LF]</para>
        /// </summary>
        [PacketAttribute("SHTS", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        SHUTTER_ENABLE_LAST_SET,
        /// <summary>
        /// System status byte reply needs to be converted to hex value 1~32(0~-31)bit
        /// <para>Bit # Description                                         </para>
        /// <para>0 Emission ,1 Shutter ,2 Gate ,3 SHG warming up           </para>
        /// <para>4 EXT gate ,5 System Fault,6 SHG autotune,7 THG autotune  </para>
        /// <para>8 Reserved,9 Motor moving ,10 Reserved,11 Reserved        </para>
        /// <para>12 Reserved,13 Reserved,14 Shutter time out,15 Reserved   </para>
        /// <para>16 Last Spot reached,17 Reserved,18 Reserved              </para>                                          
        /// <para>19 Reserved                                               </para>
        /// <para>19 value[CR][LF]                                          </para>
        /// </summary>
        [PacketAttribute("*STB", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        SYSTEM_STSTUS_BYTE,
        /// <summary>
        /// actual laser diode temperature in degrees Celsius
        /// <para>?T[CR]</para>
        /// <para>23.25[CR][LF]</para>
        /// </summary>
        [PacketAttribute("T", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        DIODE_TEMP,
        
        /// <summary>
        /// starts (1) or stops (0) the THG temperature auto-tune function
        /// <para>TAUTO:1[CR]</para>
        /// <para>?TAUTO[CR]</para>
        /// <para>1[CR][LF]</para>
        /// </summary>
        [PacketAttribute("TAUTO", ":", "?", false, false, false, true,enumPacketMode.Both)]
        THG_AUTO_TUNE,
        /// <summary>
        /// the THG (355 nm) crystal oven temperature (in counts).  Range: n = 10000 ... 65535
        /// <para>THG:16000[CR]</para>
        /// <para>?THG[CR]</para>
        /// <para>16000[CR][LF]</para>
        /// </summary>
        [PacketAttribute("THG", ":", "?", false, false, false, true,enumPacketMode.Both)]
        THG_OVEN_TEMP,
        /// <summary>
        /// the THGS (355 nm) crystal oven temperature (in counts).  Range: n = 10000 ... 65535
        /// <para>?THGS[CR]</para>
        /// <para>16000[CR][LF]</para>        
        /// </summary>
        [PacketAttribute("THGS", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        THG_OVEN_TEMP_LAST_SET,
        /// <summary>
        /// the crystal tower temperature in degrees Celsius. Range: n = 1 - 50
        /// <para>?TT[CR]</para>
        /// <para>23.50[CR][LF]</para>
        /// </summary>
        [PacketAttribute("TT", ":", "?", false, false, false, true,enumPacketMode.GettingOnly)]
        TOWER_TEMP,
        /// <summary>
        /// This query returns the remaining warm-up time of the harmonics crystals
        /// <para>?WARMUPTIME[CR]</para>
        /// <para>23:50[CR][LF]</para>
        /// </summary>
        [PacketAttribute("WARMUPTIME", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        REMAIN_WARM_UP_TIME,
        /// <summary>
        /// Comm Baudrate
        /// <para>?BAUDRATE[CR]</para>
        /// <para>value[CR][LF]</para>
        /// </summary>
        [PacketAttribute("BAUDRATE", ":", "?", false, false, false, true,enumPacketMode.Both)]
        COMM_BAUDRATE,
        /// <summary>
        /// inverts the control of the external GATE signal
        /// <para>BEI:1[CR]</para>
        /// <para>?BEI[CR] </para>
        /// <para>1[CR][LF] </para>
        /// </summary>
        [PacketAttribute("BEI", ":", "?", false, false, false, true,enumPacketMode.Both)]
        GATE_ENABLE_SIGNAL_LEVEL,
        /// <summary>
        /// inverts the trigger edge of the external TRIGGER signal
        /// <para>BTI:0 or the TRIGGER signal has an active rising edge. </para>
        /// <para>BTI:1 or the TRIGGER signal has an active falling edge. </para>
        /// <para>BTI:1[CR]</para>
        /// <para>?BTI[CR] </para>
        /// <para>1[CR][LF] </para>
        /// </summary>
        [PacketAttribute("BTI", ":", "?", false, false, false, true,enumPacketMode.Both)]
        TRIGGER_EDGE_SIGNAL_LEVEL,
        /// <summary>
        /// the initial current set point (in Amps) 
        /// <para>?CALC[CR] </para>
        /// <para>6.30[CR][LF] </para>
        /// </summary>
        [PacketAttribute("CALC", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        DIODE_CALB_CURRENT,
        /// <summary>
        /// recalibrate the internal power monitor , Range: 0.00 ≤ f ≤ 40.00
        /// <para>CAL:POW:PCAL:10.00[CR]</para>
        /// <para>?CAL:POW:PCAL[CR] </para>
        /// <para>10.00[CR][LF] </para>
        /// </summary>
        [PacketAttribute("CAL:POW:PCAL", ":", "?", false, false, true, true,enumPacketMode.Both)]
        RECALB_POWER_MONITOR,
        /// <summary>
        /// the actual laser chassis temperature in degrees Celsius
        /// <para>?CT[CR] </para>
        /// <para>6.30[CR][LF] </para>
        /// </summary>
        [PacketAttribute("CT", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        CHASSIS_TEMP,
        /// <summary>
        /// the amount of current headroom for the current limit (CALC+DCD = DCL). 
        /// <para>?DCD[CR] </para>
        /// <para>6.30[CR][LF] </para>
        /// </summary>
        [PacketAttribute("DCD", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        DIODE_CURRENT_HEADROOM,
        /// <summary>
        /// the laser diode current limit (in amps) from memory
        /// <para>?DCL1[CR] </para>
        /// <para>6.30[CR][LF] </para>
        /// </summary>
        [PacketAttribute("DCL1", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        DIODE1_CURRENT_LIMIT,
				/// <summary>
				/// the laser diode current Set Value
				/// /// <para>?CL1[CR] </para>
				/// <para>6.30[CR][LF] </para>
				/// </summary>
				[PacketAttribute("CS1", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        DIODE1_CURRENT_LAST_SET,
        /// <summary>
        /// the number of hours the laser diode emission has been on
        /// <para>?DH1[CR] </para>
        /// <para>6.30[CR][LF] </para>
        /// </summary>
        [PacketAttribute("DH1", ":", "?", false, false, true, true,enumPacketMode.GettingOnly)]
        DIODE1_HOURS,
       
        /// <summary>
        /// enables the use of the USB port as a flash drive connection and allows access to the data log files. The USB connection automatically reverts back to a virtual port.
        /// </summary>
        [PacketAttribute("DIAG:USB:MASS", ":", "?", false, false, false, false,enumPacketMode.SetOnly)]
        ENABLE_USB_PORT_FLASH_DRIVE,
        /// <summary>
        /// enables the use of the USB port as a serial control port (a virtual COM port). This is the default setting for the system
        /// </summary>
        [PacketAttribute("DIAG:USB:VIRT", ":", "?", false, false, false, false,enumPacketMode.SetOnly)]
        ENABLE_USB_PORT_AS_SERIAL_PORT,
        /// <summary>
        /// Internal time clock (real time clock ) 
        /// <para>?DIAG:TIME:READ[CR] </para>
        /// <para>2011-2-3 09:44:51[CR][LF] </para>
        /// </summary>
        [PacketAttribute("DIAG:TIME:READ", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        SYSTEM_CURRENT_TIME,
        /// <summary>
        /// the laser diode serial number
        /// <para>?DSN[CR] </para>
        /// <para>1345873[CR][LF] </para>
        /// </summary>
        [PacketAttribute("DSN", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        DIODE_SERIAL_NUMBER,
        /// <summary>
        /// Termination char  Range =0 , 16, 128   , 0=[LF] , 16=[CR][LF], 128=[CR]
        /// <para>ECHO:16[CR] </para>
        /// <para>?ECHO[CR] </para>
        /// <para>16[CR][LF] </para>
        /// </summary>
        [PacketAttribute("ECHO", ":", "?", false, false, false, true, enumPacketMode.Both)]
        COMM_TERMINATION_CHAR,
        /// <summary>
        /// the number of hours the laser head has been on. ( Hours)
        /// <para>?HEADERS[CR] </para>
        /// <para>1175.20[CR][LF] </para>
        /// </summary>
        [PacketAttribute("HEADHRS", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        LASER_HEAD_EMITTING_HOUR,

        /// <summary>
        /// the laser’s identification string
        /// <para>IDN?[CR] </para>
        /// <para>Spectra_Physics,Morpheus,21400.000.032/CD00000009”[CR][LF]</para>
        /// <para>mfg,model,S/N,Version</para>
        /// </summary>
        [PacketAttribute("*IDN", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        IDENT_PRODECT,
        /// <summary>
        /// the laser’s identification string
        /// <para>SHTEXIST?[CR] </para>      
        /// </summary>
        [PacketAttribute("SHTEXIST", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        SHUTTER_EXIST,
        /// <summary>
        /// the laser’s identification string
        /// <para>MTR:TALLHRS?[CR] </para>        
        /// </summary>
        [PacketAttribute("MTR:TALLHRS", ":", "?", false, false, false, true, enumPacketMode.GettingOnly)]
        THG_ALL_SPOT_HOURS,
    }
    /// <summary>
    /// <para>EnumHelper Class Enum 확장 메서드 </para>
    /// <see href="https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/extension-methods"/> 
    /// <para>같은 네임스페이스에 선언이 되어있어야 확장 메서드를 쓸수 있다.</para>
    /// </summary>
    static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Value"></param>
        /// <returns></returns>
        public static PacketAttribute GetPacketAttrFrom(this TALON355_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>();
        }
        public static string GetPacketAttrstrCMD(this TALON355_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>().strCMD;

        }
        public static string GetPacketAttrstrSetMark(this TALON355_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<PacketAttribute>().strSetMark;

        }
        public static string GetPacketAttrstrGettingMark(this TALON355_CMD a_Value)
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
        public static T GetAttributesFrom<T>(this Enum a_Value) where T : Attribute
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<T>();
        }
    }
}
