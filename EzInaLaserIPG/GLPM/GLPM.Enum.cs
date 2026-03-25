using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Laser.IPG
{
    public sealed partial class GLPM 
    {
        public enum STATUS
        {


            EMISSION,
            RESERVED_1,
            RESERVED_2,
            /// <summary>
            /// 0 = Internal trigger is selected. 1 = External trigger is selected.
            /// </summary>
            TRIGGER,
            /// <summary>
            /// 0 = Laser is not in warm up mode. 1 = Laser is in warm up mode.
            /// </summary>
            IS_WARM_UP_MODE,
            /// <summary>
            /// 0 = External frequency is OK.1 = External frequency is low, or disconnected.
            /// </summary>
            EXT_FREQ_STATUS,
            RESERVED_6,
            RESERVED_7,
            /// <summary>
            /// 0 = Module is in startup mode. 1 = Module startup is complete.
            /// </summary>
            IS_MODULE_START_UP_MODE,
            /// <summary>
            /// 0 = Booster power supply is disabled. 1 = Booster power supply is enabled.
            /// </summary>
            BOOSTER_POWER_SUPPLY_ENABLE,
            RESERVED_10,
            RESERVED_11,
            /// <summary>
            /// 0 = External trigger is selected. 1 = Internal trigger is selected.
            /// </summary>
            MODULATION,
            /// <summary>
            /// 0 = External trigger is selected. 1 = Internal trigger is selected.
            /// </summary>
            ANALOG_CONTROL,
            /// <summary>
            /// 0 = +24V PS is OK. 1 = +24V PS is out of range.
            /// </summary>
            VOLT_24_PS_STATUS,
            RESERVED_15,
            /// <summary>
            /// 0 = Laser temperature is OK. 1 = Laser is overheated.
            /// </summary>
            LASER_TEMP_STATUS,
            /// <summary>
            /// 0 = Backreflection level is OK. 1 = High backreflection level detected
            /// </summary>
            BACK_REFLECTION_LEV_STATUS,
            RESERVED_18,
            RESERVED_19,
            RESERVED_20,
            /// <summary>
            /// 0 = Head temperature is OK. 1 = Head overheated
            /// </summary>
            HEAD_TEMP_STATUS,           
        }
			

        
    }
}
