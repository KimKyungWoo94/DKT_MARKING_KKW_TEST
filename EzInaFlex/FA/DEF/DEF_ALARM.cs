using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    public static partial class DEF
    {
        #region [PostMessage]
        public const int WM_USER = 0x0400;


				public const int GUI_AUTO_LOT_CODE_TXT_READONLY = WM_USER + 40;

        public const int MSG_SHOW_ERROR = WM_USER + 50;
        public const int MSG_HIDE_ERROR = WM_USER + 51;
        public const int MSG_SHOW_ALARM = WM_USER + 52;
        public const int MSG_HIDE_ALARM = WM_USER + 53;
        public const int MSG_FROM_CLOSE_ALL = WM_USER + 54;
        public const int MSG_SHOW_VISION = WM_USER + 55;
        public const int MSG_SHOW_LASER = WM_USER + 56;

				

        public const int MSG_STAGE_2D_CAL_CREATE_MAP = WM_USER + 60;
        public const int MSG_STAGE_2D_CAL_SET_MEASURED_VALUE = WM_USER + 61;

        public const int MSG_FIND_FOCUS_CREATE_MAP = WM_USER + 62;
        public const int MSG_FIND_FOCUS_SET_MEASURED_VALUE = WM_USER + 63;

        public const int MSG_GALVO_CAL_CREATE_MAP = WM_USER + 64;
        public const int MSG_GALVO_CAL_SET_MEASURED_VALUE = WM_USER + 65;


        #endregion

        #region [ALARM LISTS]
        public const int ALM_INIT = 1;
        public const int ALM_JOB_FINISH = 2;
				public const int ALM_NOTIFY=3;
        public const int ALM_STAGE_2D_CAL_PAUSE = 10;
        #endregion



    }
}
