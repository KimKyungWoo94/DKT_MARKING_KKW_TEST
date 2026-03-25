using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
	public static partial class DEF
	{
        #region [ERROR LISTS]
        public const int ERR_MC_IS_NOT_INITIALIZED = 1;
        public const int ERR_INITIALIZE_FAILED = 2;
        public const int ERR_EMO_HAS_BEEN_DETECTED = 3;
        public const int ERR_MAIN_AIR = 4;
        public const int ERR_MAIN_VACUUM = 5;
        public const int ERR_STAGE_VACUUM = 6;
				public const int ERR_DOOR_OPEN = 7;
				
				public const int ERR_SERVO_POWER_OFF=9;
				public const int ERR_STEP_MOTOR_POWER_OFF=10;

				public const int ERR_LOT_CODE_EMPTY=11;


        public const int ERR_MOTOR_IS_NOT_SERVO_ON = 20;
        public const int ERR_MOTOR_IS_NOT_HOMED = 30;
        public const int ERR_AN_ALARM_OCCURRED_ON_THE_MOTOR = 40;

				

        #endregion
    }
}
