using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EzIna.GUI
{
		/// <summary>
		/// EzInaGui definition
		/// </summary>
		public class D
		{
				public const int MAX_NEVIGATION_FUN = 7;

				public const int GUI_TITLE_HEIGHT = 110;
				public const int GUI_NAVI_HEIGHT = 110;

				public const int GUI_WINDOWS_WIDTH = 1920; //24inch
				public const int GUI_WINDOWS_HEIGHT = 1080;


				public const int GUI_CMD_WIDTH = 110;
				public const int GUI_CMD_HEIGHT = GUI_WINDOWS_HEIGHT - GUI_TITLE_HEIGHT - GUI_NAVI_HEIGHT;

				public const int GUI_INFOR_WIDTH = GUI_WINDOWS_WIDTH - GUI_CMD_WIDTH;
				public const int GUI_INFOR_HEIGHT = GUI_WINDOWS_HEIGHT - GUI_NAVI_HEIGHT - GUI_TITLE_HEIGHT;



				public static readonly Color clMainTitle = Color.FromArgb(0, 51, 102);
				public static readonly Color clSubTitle = Color.FromArgb(20, 51, 102);
				public static readonly Color clNavi = Color.FromArgb(0, 51, 102);
				public static readonly Color clInfo = Color.FromArgb(61, 102, 171);
				public static readonly Color clCmd = Color.FromArgb(0, 51, 102);
				public static readonly Color clFormBackground = Color.FromArgb(61, 102, 171);

				public static readonly Color clCheckedButton = Color.FromArgb(255, 255, 242, 157);
				public static readonly Color clUncheckedButton = SystemColors.Control;


				public enum eTagFormType
				{
						NONE,
						FORM_TYPE_TITLE = 1,
						FORM_TYPE_CMD,
						FORM_TYPE_NAVI,
						FORM_TYPE_INFOR,
						FORM_TYPE_INFOR_WITHOUT_CMD,
						FORM_TYPE_INFOR_POPUP,
				}
				public enum eTagFormShowType
				{
						NONE,
						MODAL_LESS,
						MODAL,
				}
				public enum eTagFormID
				{
						FORM_ID_TITLE_PANEL = 1,
						FORM_ID_NAVI_PANEL = 2,

						FORM_ID_CMD_OPERATION = 100,
						FORM_ID_INFOR_OPERATION_AUTO = 101,
						FORM_ID_INFOR_OPERATION_SEMI = 102,
						FORM_ID_INFOR_OPERATION_VISION_PANEL = 103,

						FORM_ID_CMD_RECIPE = 200,
						FORM_ID_INFOR_RECIPE = 201,
						FORM_ID_INFOR_RECIPE_PROJECT = 202,
						FORM_ID_CMD_MANUAL = 300,
						FORM_ID_INFOR_MANUAL_MOTION = 301,
						FORM_ID_INFOR_MANUAL_VISION = 302,
						FORM_ID_INFOR_MANUAL_LASER = 303,
						FORM_ID_INFOR_MANUAL_SCANNER = 304,
						FORM_ID_INFOR_MANUAL_POWERMETER = 305,
						FORM_ID_INFOR_MANUAL_ATTENUATOR = 306,
						FORM_ID_INFOR_MANUAL_COM = 307,

						FORM_ID_CMD_SETUP = 400,
						FORM_ID_INFOR_SETUP_INIT_PROCESS = 401,
						FORM_ID_INFOR_SETUP_TEACHING = 402,
						FORM_ID_INFOR_SETUP_MOTION = 403,
						FORM_ID_INFOR_SETUP_VISION = 404,
						FORM_ID_INFOR_SETUP_LASER = 405,
						FORM_ID_INFOR_SETUP_SCANNER = 406,
						FORM_ID_INFOR_SETUP_POWERMETER = 407,
						FORM_ID_INFOR_SETUP_ATTENUATOR = 408,
						FORM_ID_INFOR_SETUP_COM = 409,
						FORM_ID_INFOR_SETUP_CYLINDER = 410,
						FORM_ID_INFOR_SETUP_IO = 411,


						FORM_ID_CMD_LOG = 500,
						FORM_ID_INFOR_LOG_EVENTLOG = 501,
						FORM_ID_INFOR_LOG_WORKLOG = 502,
						FORM_ID_INFOR_LOG_SEQUENCY = 503,
						FORM_ID_INFOR_LOG_TACTTIME = 504,
						FORM_ID_INFOR_LOG_COMMUNICATION = 505,




						FORM_ID_CMD_LOGIN = 600,
						FORM_ID_INFOR_LOGIN = 601,

						FORM_ID_EXIT = 700,

				};


				public enum eTagSendMsg
				{
						MSG_FORM_CLOSE,
				};
		}
		///<see cref="https://yaraba.tistory.com/227"/>
}