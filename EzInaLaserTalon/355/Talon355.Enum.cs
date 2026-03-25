using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Laser.Talon
{
    public sealed partial class Talon355
    {
        /// <summary>
        /// 
        /// </summary>
        public enum enumSystemStatus
        {
            /// <summary>
            /// 
            /// </summary>
            EMSSIMON_ONOFF,
            /// <summary>
            /// 
            /// </summary>
            SHUTTER,
            /// <summary>
            /// 
            /// </summary>
            GATE,
            /// <summary>
            /// 
            /// </summary>
            SHG_WARMING_UP,
            /// <summary>
            /// 
            /// </summary>
            EXT_GATE,
            /// <summary>
            /// 
            /// </summary>
            SYSTEM_FAULT,
            /// <summary>
            /// 
            /// </summary>
            SHG_AUTO_TUNE,
            /// <summary>
            /// 
            /// </summary>
            THG_AUTO_TUNE,
            /// <summary>
            /// 
            /// </summary>
            MOTOR_MOVING=9,
            /// <summary>
            /// 
            /// </summary>
            SHUTTER_TIME_OUT=14,
            /// <summary>
            /// 
            /// </summary>
            LAST_SPOT_REACHED=16,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum enumGateEnableLevel:int
        {
            /// <summary>
            /// 
            /// </summary>
            ActiveLow,
            /// <summary>
            /// 
            /// </summary>
            ActiveHigh,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum enumTriggerEdgeLevel:int
        {
            /// <summary>
            /// 
            /// </summary>
            RisingEdge,
            /// <summary>
            /// 
            /// </summary>
            FallingEdge,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum enumHistoryEventCode:int
        {
            /// <summary>
            /// 
            /// </summary>
            SYSTEM_READY=0,
            /// <summary>
            /// 
            /// </summary>
            SYS_ILK=11,
            /// <summary>
            /// 
            /// </summary>
            TEST_ILK=12,
            /// <summary>
            /// 
            /// </summary>
            KEY_ILK=13,
            /// <summary>
            /// 
            /// </summary>
            DIODE1_TEMP_ERR=31,
            /// <summary>
            /// 
            /// </summary>
            TOWER_TEMP_ERR=36,
            /// <summary>
            /// 
            /// </summary>
            CHASSIS_TEMP_ERR=37,
            /// <summary>
            /// 
            /// </summary>
            WATCHDOG_EXPIRED=66,
            /// <summary>
            /// 
            /// </summary>
            TOWER_TEMP_WARNING=128,
            /// <summary>
            /// 
            /// </summary>
            SHUTTER_IN_UNEXPECTED_STATE=134,
            /// <summary>
            /// 
            /// </summary>
            THG_RECOVERY=137,
            /// <summary>
            /// 
            /// </summary>
            DIODE_TEMP_WARNING=181,
        }
    }
}
