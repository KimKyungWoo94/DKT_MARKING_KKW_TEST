using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Reflection;

namespace EzIna.Laser
{
    namespace Lumentum
    {
        /// <summary>
        /// <para>PicoBlade2 Command</para>
        /// <code>
        /// 
        /// <para>Loop Example [Enum 확장메서드 참고]</para> 
        /// <para> EzInaCommucation.PacketAttribute tmp=null;</para> 
        /// <para>  foreach (PICO_BLADE2_CMD item in Enum.GetValues(typeof(PICO_BLADE2_CMD)))</para> 
        /// <para>  {</para> 
        ///
        ///  <para>     tmp = item.GetPacketAttributeFrom();</para> 
        ///  <para> }</para> 
        /// 
        /// 
        /// </code>
        /// </summary>
        public enum PICO_BLADE2_CMD
        {

            /// <summary>
            /// <para>Start Button ＆ Wheel</para>
            /// <para>*S0!FP00000n# </para>
            /// <para> n=0 Disable  , n=1 Enable </para>
            /// </summary>
            [PacketAttribute("S0FP", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            FRONT_PANEL_LOCKOUT,
            /// <summary>
            /// Turn Laser Emission 
            /// <para>*S1!ON00000n#</para>
            /// n=0 OFF, n=1 ON , n=2 TestMode
            /// </summary>
            [PacketAttribute("S0ON", "!", "?", false, false, true, true, enumPacketMode.Both)]
            TURN_LASER_EMISSION,
            /// <summary>
            /// <para>Set Clearable errors ＆ Warning Clear  *SO!FS000000#</para>        
            /// <para>Getting *S0?FSzzzzzz#</para>
            /// <para>*S0FSnnnnnn#  nnnnnn = 0 → No errors nnnnnn 〉 0 → At least one faulty</para>
            /// <para>Bit0: Safety module           </para>
            /// <para>Bit1: Voltage monitor module  </para>
            /// <para>Bit2: Firmware monitor module </para>
            /// <para>Bit3: Laser module            </para>
            /// <para>Bit5: Amp1 module             </para>
            /// <para>Bit6: Seeder module           </para>
            /// <para>Bit7: Amp2 module             </para>
            /// <para>Bit8: P.A. module             </para>
            /// <para>Bit14: Pulse picker module    </para>
            /// nnnnnn = 999999 → unknown module error
            /// </summary>
            [PacketAttribute("S0FS", "!", "?", false, false, true, true, enumPacketMode.Both)]
            FAULTS_STATUS,
            /// <summary>
            /// Enforce Full HW Reset Of Controller
            /// <para>*SO!RE000000#</para>
            /// </summary>
            [PacketAttribute("S0RE", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            FULL_CONTROLLER_RESET,
            /// <summary>
            /// <para>*S0!LSnnnnnn#</para>
            /// <para>nnnnnn = 0 ~ 180</para>
            /// nnnnnn = 0→ logging disabled
            /// </summary>
            [PacketAttribute("S0LS", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            COMM_LOGGING_SPEED,
            /// <summary>
            /// Enforces  RS-232 Record (Logging Line) or Full Controller Settings Printout
            /// n = 0 -> Single Record  , n = 1 -> Complete Settings
            /// n = 2 -> Records’ Title , n = 3 -> Records’ Subtitle           
            /// </summary>
            [PacketAttribute("S0LL", "!", "?", true, false, true, true, enumPacketMode.SetOnly)]
            COMM_RECORD,
            /// <summary>
            /// SYNC mode [optional] enabled
            /// <para>*S0!SY00000n# </para>
            /// n = 0 -> SYNC mode disabled ,n = 1 → SYNC mode enabled            
            /// </summary>
            [PacketAttribute("S0SY", "!", "?", true, false, true, true, enumPacketMode.SetOnly)]
            SYNC_MODE_ENABLE,
            /// <summary>
            /// Divider for Pulse Repetition Frequency            
            /// <para>*S1!PDnnnnnn#</para>
            /// Sets Divider for PRF  PRF = RR / nnnnnn 
            /// </summary>
            [PacketAttribute("S1PD", "!", "?", false, true, true, true, enumPacketMode.Both)]
            DIVIDER_PRF,
            /// <summary>
            /// Pulse Repetition Frequency
            /// <para>*S1!PFaabbbb#</para>
            /// Sets PRF  PRF = aa.bbbb MHz
            /// </summary>                            
            [PacketAttribute("S1PF", "!", "?", false, true, true, true, enumPacketMode.Both)]
            PRF,
            /// <summary>
            /// actual burst size
            /// <para>*S1!BT0000nn#</para>
            /// Sets (fix) Burst to nn = 01…40               
            /// </summary>   
            [PacketAttribute("S1BT", "!", "?", false, true, true, true, enumPacketMode.Both)]
            FIX_BURST_SIZE,
            /// <summary>
            /// <para>*S1!FB00000n#</para>
            /// <para>Loads ＆ sets FlexBurst n = ＃1…＃8    </para>
            /// <para>Get S1FB00000n#  → n = 0 Fix Burst is active , n 〉 0  FlexBurst is active </para>        
            /// </summary>   
            [PacketAttribute("S1FB", "!", "?", false, true, true, true, enumPacketMode.Both)]
            LOAD_FLEX_BURST,
            /// <summary>
            /// <para>*S0!FB00000n#</para>
            /// Saves FlexBurst as n = ＃1…＃8            
            /// </summary>
            [PacketAttribute("S0FB", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            SAVE_FLEX_BURST,
            /// <summary>
            /// <para>*S1!MS0000nn#</para>
            /// Loads Main Set nn = ＃01…＃10 → PRF ＆ (Fix/Flex)Burst ＆ PoD           
            /// </summary>
            [PacketAttribute("S1MS", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            LOAD_MAIN_SET,
            /// <summary>
            /// <para>*S0!MS0000nn#</para>
            /// Saves Main Set as nn = ＃01…＃10 → PRF ＆ (Fix/Flex)Burst ＆ PoD
            /// </summary>
            [PacketAttribute("S0MS", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
            SAVE_MAIN_SET,
            /// <summary>
            /// <para>*S1!PG0ppnnn# </para>
            /// gain = nnn (0..999) of pulse #pp
            /// </summary>
            [PacketAttribute("S1PG", "!", "?", false, true, true, true, enumPacketMode.Both)]
            GAIN_SPEC_PULSE_IN_FLEXBURST,
            /// <summary>
            /// <para>*S1!PBa0000n#</para>
            /// a=HwGating , n = Enable Pod  0: Disable 1:Enable
            /// </summary>
            [PacketAttribute("S1PB", "!", "?", false, true, true, true, enumPacketMode.Both)]
            ENABLE_POD_N_HW_GATING,
            /// <summary>
            ///  <para> *S1!DPnnnnnn#</para>
            /// PoD divider = nnnnnn
            /// </summary>
            [PacketAttribute("S1DP", "!", "?", false, true, true, true, enumPacketMode.Both)]
            DRIVIDER_FOR_POD,
            /// <summary>
            /// <para> *S1!AO00nnnn#</para>
            /// Relative IR Output Power = nnnn [%o]
            /// </summary>
            [PacketAttribute("S1AO", "!", "?", false, true, true, true, enumPacketMode.Both)]
            OUTPUT_POWER_FOR_POD,
            /// <summary>
            ///  Position of Shutter
            /// <para>*S1!SH00000n# </para>
            /// n = 2 → enforce shutter closing, n 〈 2 → release shutter
            /// <para>Getting</para>
            /// <para>*S1SH00000n# → n = 1 → Shutter is closed , n = 0 → Shutter is open </para>
            /// </summary>
            [PacketAttribute("S1SH", "!", "?", false, true, true, true, enumPacketMode.Both)]
            SHUTTER,
            /// <summary>
            /// <para>SET </para>
            /// <para>*S1!OP00100n# Enables Level triggering (sampled at rising edge of PRF out) of PoD</para>
            /// <para>*S1!OP00200n# Enables Edge triggering (any low to high transition) of PoD</para>
            /// <para>*S1!OP00500n# Enables external analog modulation of PoD(SMA connector PoD A in)</para>
            /// <para>*S1!OP00600n# Sets the analog modulation range of PoD (SMA connector PoD A in), n=1 → 5V , n=0 → 10V</para>        
            /// <para>*S1!OP01000n# Enables the modulation of the PRF out by the PoD divider</para>
            /// <para>*S1!OP01100n# Enables the modulation of the PRF out by the PoD on/off signal</para>        
            /// 
            /// </summary>
            [PacketAttribute("S1OP", "!", "?", false, true, true, true, enumPacketMode.Both)]
            POD_MODULATION_ENABLE,

            /// <summary>
            /// Get Firmware Version  *S0?VSzzzzzz#
            /// <para>Getting *S0VSnnnmmm# , Version = n.nn  , Build=mmm</para>        
            /// </summary>
            [PacketAttribute("S1VS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            FIRMWARE_VERSION,
            /// <summary>
            /// Boot loader Version  *S0?VFzzzzzz# 
            /// <para>Getting *S0VSnnnmmm# , Version = n.nn  , Build=mmm</para>  
            /// </summary>
            [PacketAttribute("S1VF", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            BOOTLOADER_VERSION,
            /// <summary>
            /// Serial Number of Controller  *S0?SNzzzzzz#
            /// <para>Getting *S0SNnnnnnn#  , S/N = nnnnnn </para> 
            /// </summary>
            [PacketAttribute("S0SN", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            COTROLLER_SERIAL_NUM,
            /// <summary> 
            /// Serial Number of LaserHead  *S1?SNzzzzzz#
            /// <para>Getting *S1SNnnnnnn#  , S/N = nnnnnn </para> 
            /// </summary>
            [PacketAttribute("S1SN", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            LASERHEAD_SERIAL_NUM,

            /// <summary>
            /// current of TEC seeder pump diode *D2?IT# 
            /// <para>Getting *D2ITnnnnnn# or *D2IT-nnnnnn#</para> 
            /// <para>Heating Current (positive value)= nnnnnn [mA] </para>
            /// <para>Cooling Current (negative value)= -nnnnnn [mA] </para>
            /// </summary>
            [PacketAttribute("D2IT", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            CURRENT_TEC_SEEDER_PUMP_DIODE,
            /// <summary>
            /// voltage of TEC seeder pump diode *D2?UT#
            /// <para>Getting *D2UTnnnnnn# or *D2UT-nnnnnn#</para> 
            /// <para>Heating Voltage (positive value)= nnnnnn [mA] </para>
            /// <para>Cooling Voltage (negative value)= -nnnnnn [mA] </para>
            /// </summary>
            [PacketAttribute("D2UT", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            VOLTAGE_TEC_SEEDER_PUMP_DIODE,
            /// <summary>
            ///  DC signal from  photo diode monitoring oscillator intracavity power
            ///  <para>Getting *S1LDnnnnnn# → Current = nnnnnn [µA] </para>
            /// </summary>
            [PacketAttribute("S1LD", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            PHOTO_DIODE_MONITORING_OSC_INTRA_POWER,
            /// <summary>
            /// controller temperature *S0?TC#
            ///  <para>Getting *S0TCaaabbb#  , → Temperature = aaa.bbb [°C] </para> 
            /// </summary>
            [PacketAttribute("S0TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            COLTROLLER_TEMP,
            /// <summary>
            /// measured Amp1 diode temperature *D1?TC#
            /// <para>Getting *D1TCaaabbb# → Temperature = aaa.bbb[°C]</para>
            /// </summary>
            [PacketAttribute("D1TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            AMP1_DIODE_TEMP,
            /// <summary>
            ///  measured seeder diode temperature *D2?TC#
            ///  <para>Getting *D2TCaaabbb#  , → Temperature = aaa.bbb [°C] </para> 
            /// </summary>
            [PacketAttribute("D2TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            SEEDER_DIODE_TEMP,
            /// <summary>
            /// measured Amp2 diode temperature *D3?TC#
            /// <para>Getting *D3TCaaabbb# → Temperature = aaa.bbb[°C]</para>
            /// </summary>
            [PacketAttribute("D3TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            AMP2_DIODE_TEMP,
            /// <summary> 
            /// measured power amplifier diode temperature  *D4?TC#
            /// <para>Getting *D3TCaaabbb# → Temperature = aaa.bbb[°C]</para>
            /// </summary>
            [PacketAttribute("D4TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            POWER_AMP_DIODE_TEMP,

            /// <summary>
            /// measure temperature
            /// <para>SET  *S1?TCnnnnnn#</para>
            /// <para>nnnnnn=000001 HeatSink</para>
            /// <para>nnnnnn=000002 AD-3 board</para>
            /// <para>Getting *S1TCaaabbb# → Temperature = aaa.bbb[°C]</para>
            /// </summary>
            [PacketAttribute("S1TC", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            SYSTEM_PARTS_TEMP,

            /// <summary>
            /// DC signal from photo diode monitoring  
            /// <para>SET *S0CUnnnnnn#</para>
            /// <para>nnnnnn=000010 ,DC signal from photoDiode monitoring seeder pump diode</para>
            /// <para>nnnnnn=000012 ,DC signal from photoDiode monitoring Amp1 fiber Output</para>
            /// <para>nnnnnn=000013 ,DC signal from photoDiode monitoring Amp2 fiber Output</para>
            /// <para>nnnnnn=000031 ,DC signal from photoDiode monitoring PA fiber Output</para>
            /// <para>nnnnnn=000032 ,DC signal from photoDiode monitoring IR Power After PoD</para>
            /// <para>Getting *S0CUnnnnnn#  → Current = nnnnnn[µA] </para>
            /// </summary>
            [PacketAttribute("S0CU", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            PHOTO_DIODE_MONITORING,
            /// <summary>
            /// measured output power *S1?MP#
            /// <para>Getting *S1MPnnnnnn# → Power = nnnnnn [mW] </para>
            /// </summary>
            [PacketAttribute("S1MP", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            OUTPUT_POWER,

            /// <summary>
            /// error status of safety module  *S2?FSzzzzzz#
            /// <para>nnnnnn = 0 → No errors , nnnnnn > 0 → At least one error ,present in this module. (Laser Emission will stop automatically.) </para>
            /// <para>Module errors are binary coded. Binary(nnnnnn): Bit0…Bit18.</para>
            /// <para>Bit0: Remote interlock               </para>
            /// <para>Bit1: Cover interlock                </para>
            /// <para>Bit2: Shutter does not open          </para>
            /// <para>Bit3: Shutter does not close         </para>
            /// <para>Bit4: Shutter sensor                 </para>
            /// <para>Bit6: PB2 controller                 </para>
            /// <para>fan/overtemperature                  </para>
            /// <para>Bit7: ACDC block fan/overtemperature </para>
            /// <para>Bit9: Remote control                 </para>
            /// <para>Bit10: NV memory write               </para>
            /// </summary>
            [PacketAttribute("S2FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            FAULT_STATUS_SAFETY_MODELE,

            /// <summary>
            /// error status of laser diode module
            /// <para>nnnnnn = 0 → No errors , nnnnnn > 0 → At least one error ,present in this module. (Laser Emission will stop automatically.) </para>
            /// <para>Module errors are binary coded. Binary(nnnnnn): Bit0…Bit18.</para>        
            /// <para>Bit0: Fiber monitor</para>
            /// <para>Bit1: Under current</para>
            /// <para>Bit2: Over current</para>
            /// <para>Bit3: Current spike</para>
            /// <para>Bit5: Driver #1</para>
            /// <para>Bit6: Driver #2</para>
            /// <para>Bit7: Driver #3</para>
            /// <para>Bit8: Driver #4</para>
            /// <para>Bit9: Driver #5</para>
            /// <para>Bit10: Bad NV memory parameter</para>
            /// <para>Bit11: Under temperature</para>
            /// <para>Bit12: Over temperature</para>
            /// <para>Bit13: Temperature deviation</para>
            /// </summary>
            [PacketAttribute("D2FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            SEEDER_FAULT_STATUS_LASER_DIODE_MODULE,
            /// <summary>
            /// <see href="SEEDER_FAULT_STATUS_LASER_DIODE_MODULE description"/>
            /// </summary>
            [PacketAttribute("D1FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            AMP1_FAULT_STATUS_LASER_DIODE_MODULE,
            /// <summary>
            /// <see href="See Item SEEDER_FAULT_STATUS_LASER_DIODE_MODULE description"/>
            /// </summary>
            [PacketAttribute("D3FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            AMP2_FAULT_STATUS_LASER_DIODE_MODULE,
            /// <summary>
            /// <see href="See Item SEEDER_FAULT_STATUS_LASER_DIODE_MODULE description"/>
            /// </summary>
            [PacketAttribute("D4FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            POWER_AMP_FAULT_STATUS_LASER_DIODE_MODULE,
            /// <summary>
            /// error status of laser module *S1?FSzzzzzz#
            /// <para>nnnnnn = 0 → No errors , nnnnnn > 0 → At least one error ,present in this module. (Laser Emission will stop automatically.) </para>
            /// <para>Module errors are binary coded. Binary(nnnnnn): Bit0…Bit18.</para>  
            /// <para>Bit0: Heat sink under temperature</para>
            /// <para>Bit1: Heat sink over temperature </para>
            /// <para>Bit2: data link to laser box     </para>
            /// <para>Bit3: PLL unlocked               </para>
            /// <para>Bit4: Qswitching                 </para>
            /// <para>Bit6: low LaserDC                </para>
            /// </summary>
            [PacketAttribute("S1FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            FAULT_STATUS_LASER_MODULE,
            /// <summary>
            /// error status of voltage monitor module
            /// <para>nnnnnn = 0 → No errors , nnnnnn > 0 → At least one error ,present in this module. (Laser Emission will stop automatically.) </para>
            /// <para>Module errors are binary coded. Binary(nnnnnn): Bit0…Bit18.</para>  
            /// <para>Bit0: Voltage #1   </para> 
            /// <para>Bit1: Voltage #2   </para> 
            /// <para>Bit2: Voltage #3   </para> 
            /// <para>...                </para> 
            /// <para>Bit17: Voltage #18 </para> 
            /// <para>Bit18: Voltage #19 </para>                  
            /// </summary>
            [PacketAttribute("S6FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            FAULT_STATUS_VOLTAGE_MONITOR_MODULE,
            /// <summary>
            ///  error status of firmware monitor module
            /// <para>nnnnnn = 0 → No errors , nnnnnn > 0 → At least one error ,present in this module. (Laser Emission will stop automatically.) </para>
            /// nnnnnn = Decimal code of firmware error
            /// </summary>
            [PacketAttribute("S7FS", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            FAULT_STATUS_FIRMWARE_MONITOR_MODULE,
            /// <summary>
            /// nnnnnn = 0 → No warnings
            /// nnnnnn = Decimal code of last warning
            /// </summary>
            [PacketAttribute("S0WA", "!", "?", false, true, true, true, enumPacketMode.GettingOnly)]
            LAST_WARNING,

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
            public static PacketAttribute GetPacketAttrFrom(this PICO_BLADE2_CMD a_Value)
            {
                return a_Value.GetType().GetMember(a_Value.ToString())
                               .First()
                               .GetCustomAttribute<PacketAttribute>();
            }
            public static string GetPacketAttrstrCMD(this PICO_BLADE2_CMD a_Value)
            {
                return a_Value.GetType().GetMember(a_Value.ToString())
                               .First()
                               .GetCustomAttribute<PacketAttribute>().strCMD;

            }
            public static string GetPacketAttrstrSetMark(this PICO_BLADE2_CMD a_Value)
            {
                return a_Value.GetType().GetMember(a_Value.ToString())
                               .First()
                               .GetCustomAttribute<PacketAttribute>().strSetMark;

            }
            public static string GetPacketAttrstrGettingMark(this PICO_BLADE2_CMD a_Value)
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
    
   
}
