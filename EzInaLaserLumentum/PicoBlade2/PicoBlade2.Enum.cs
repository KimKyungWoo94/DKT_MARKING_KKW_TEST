using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Laser.Lumentum
{

    public sealed partial class PicoBlade2
    {
        /// <summary>
        /// 
        /// </summary>
        public enum enumBurstType
        {
            /// <summary>
            /// 
            /// </summary>
            FIX,
            /// <summary>
            /// 
            /// </summary>
            FLEX
        }
        /// <summary>
        /// 
        /// </summary>
       public enum enumPoDAnalogRange
       {
            /// <summary>
            /// 
            /// </summary>
            _5V,
            /// <summary>
            /// 
            /// </summary>
            _10V
       }
       /// <summary>
       /// Module 단위 
       /// </summary>
       public enum enumFaultStatus
       {            
            SAFETY,
            VOLTAGE_MONITOR,
            FIRMWARE_MONITOR,
            LASER,
            AMP1=5,
            SEEDER,
            AMP2,
            PA,
            PULSE_PICKER=14,
       }
       /// <summary>
       /// 
       /// </summary>
       public enum enumSafetyModuleFaultStatus
       {
            REMOTE_INTERLOCK,
            COVER_INTERLOCK,
            SHUTTER_OPEN,
            SHUTTER_CLOSE,
            SHUTTER_SENSOR,
            PB2_CONTROLLER_OVER_TEMP=6,
            ACDC_BLOCK_OVER_TEMP,
            REMOTE_CONTROL=9,
            NV_MEMORY_WRITE
       }
       /// <summary>
       /// 
       /// </summary>
       public enum enumLaserDiodeFaultStatus
       {
            FIBER_MONITOR,
            UNDERCURRENT,
            OVERCURRENT,
            CURRENTSPIKE,
            DRIVER1=5,
            DRIVER2,
            DRIVER3,
            DRIVER4,
            DRIVER5,
            BAD_NV_MEMORY_PARAM,
            UNDERTEMP,
            OVERTEMP,
            TEMP_DEVIATION,            
       }
       public enum enumLaserModuleFaultStatus
       {
            HEATSINK_UNDERTEMP,
            HEATSINK_OVERTEMP,
            DATALINK_TO_LASER_BOX,
            PLL_UNLOCKED,
            QSWITCHING,
            LOW_LOASERDC=6
       }
    }
}
