using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Attenuator.OPTOGAMA
{
    public sealed partial class LPA
    {
        public enum HomeOption:int
        {
            TARGET_POS_0=0,
            MIN_POWER,
            LAST_POS,
         /* 0 - device remains in home position (TGT_0); 
            1 - device goes to MIN power position (calibrated position for minimum power); 
            2 - device goes to LAST position it was before homing.*/
        }
        public enum Status:int
        {
            DRIVER_ERR=0,
            DRIVER_HIGH_TEMP_WARNING,
            DRIVER_OVER_TEMP,
            DRIVER_LOAD_ERR,
            LOAD_WARNING,
            UNDER_VOLTAGE_ERR,
            EXTERNAL_MEMEORY_ERR,
            RESET_OCCUURRED,
            LEFT_LIMIT,
            RIGHT_LIMIT,
            STALL_GUARD_FLAG_ACTIVE,
            MOTOR_STAND_STILL,
            MOTOR_TARGET_VEL_REACHED,
            TARGET_POS_READCHED,
            HOMING_RAN,
            DEVICE_CALB_DONE,            
        }
    }
}
