using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace EzIna.FA
{
    public static partial class DEF
    {
        #region [ GUI ]
        public static int VGA_WIDTH = 640;
        public static int VGA_HEIGHT = 400;
        public static int SXGA_WIDTH = 1280;
        public static int SXGA_HEIGHT = 1024;
        #endregion
        #region System
        public enum eAxesName
        {
            None = -1,
            RX,
            Y,
            RZ,
            RAIL_ADJUST,
            PINCH_ROLLER_L_U,
            PINCH_ROLLER_L_B,
            PINCH_ROLLER_R_U,
            PINCH_ROLLER_R_B,
            //Not Use
            RA,
            //Not Use
            RB,
            Max
        }

        public enum eDI
        {
            EMERGENCY,
            X01,
            X02,
            X03,
            RAIL_MOUTH_L,
            RAIL_MOUTH_R,
            JIG_POS_DETECTED_L,
            JIG_POS_DETECTED_M,
            JIG_POS_DETECTED_R,
            X09,
            JIG_FEEDER_CLAMP_L_CY_FWD,
            JIG_FEEDER_CLAMP_L_CY_BWD,
            JIG_FEEDER_CLAMP_R_CY_FWD,
            JIG_FEEDER_CLAMP_R_CY_BWD,
            PUSHER_L_OVERLOAD_DETECTED,
            PUSHER_R_OVERLOAD_DETECTED,
            JIG_FEEDER_JIG_EXIST,
            X17,
            X18,
            X19,
            X20,
            X21,
            JIG_ACC_UPPER_DOWN,
            JIG_ACC_LOWER_DOWN,
            JIG_STOPPER_L_UP,
            JIG_STOPPER_L_DOWN,
            JIG_STOPPER_R_UP,
            JIG_STOPPER_R_DOWN,
            JIG_STOPPER_CT_UP,
            JIG_STOPPER_CT_DOWN,
            JIG_STOP_POS_CHK,
            X31,
            SW_START,
            SW_STOP,
            SW_RESET,
            SW_KEY,
            X36,
            X37,
            MAIN_PRESSURE_CHECK,
            X39,
            FRONT_DOOR_OPENED,
            REAR_DOOR_OPENED,
            STEP_DRIVER_PWR_MC,
            XYZ_MOTOR_PWR_MC,
            X44,
            X45,
            X46,
            X47,
            X48,
            X49,
            X50,
            X51,
            X52,
            X53,
            X54,
            X55,
            X56,
            X57,
            X58,
            X59,
            LOADER_IF_READY,
            LOADER_SMEMA_IF_S2,
            UNLOADER_IF_READY,
            UNLOADER_SMEMA_IF_S2,
        }

        public enum eDO
        {
            LIGHT_CH1_L_SW,
            LIGHT_CH2_R_SW,
            LIGHT_CH3_U_SW,
            LIGHT_CH3_B_SW,
            Y04,
            Y05,
            PINCH_ROLLER_L_U,
            PINCH_ROLLER_L_B,
            PINCH_ROLLER_R_U,
            PINCH_ROLLER_R_B,
            JIG_FEEDER_CLAMP_L_FWD,
            JIG_FEEDER_CLAMP_L_BWD,
            JIG_FEEDER_CLAMP_R_FWD,
            JIG_FEEDER_CLAMP_R_BWD,
            Y14,
            Y15,
            Y16,
            Y17,
            Y18,
            JIG_ACC_UPPER_UP,
            JIG_ACC_LOWER_UP,
            Y21,
            Y22,
            Y23,
            JIG_STOPPER_L_UP,
            JIG_STOPPER_L_DOWN,
            JIG_STOPPER_R_UP,
            JIG_STOPPER_R_DOWN,
            JIG_STOPPER_CT_UP,
            JIG_STOPPER_CT_DOWN,
            Y30,
            Y31,
            SW_START_LAMP,
            SW_STOP_LAMP,
            SW_RESET_LAMP,
            Y35,
            Y36,
            Y37,
            TOWER_LAMP_RED,
            TOWER_LAMP_YELLOW,
            TOWER_LAMP_GREEN,
            TOWER_LAMP_BUZZER,
            Y42,
            Y43,
            Y44,
            Y45,
            Y46,
            DEBRIS_SOL,
            DUST_COLLECTOR_REMOTE,
            SCANNER_POWER_OFF,
            Y50,
            Y51,
            Y52,
            Y53,
            Y54,
            Y55,
            Y56,
            Y57,
            Y58,
            LASER_EM_ENABLE,
            LOADER_IF_START_CMD,
            LOADER_SMEMA_IF_S2,
            UNLOADER_START_CMD,
            UNLOADER_SMEMA_IF_S2,
        }
        public enum RTC5_DO
        {
            AUX_OFF,
            EMISSION_ENABLE,
        }
        public static void SetLaserPinDO(this RTC5_DO a_value, bool a_SetValue)
        {
            RTC5.Instance.SetLaserConnector_Output((Scanner.ScanlabRTC5.LaserConnector_Out)a_value, a_SetValue);
        }
        public static void GetLaserPinDO(this RTC5_DO a_value)
        {
            RTC5.Instance.GetLaserConnector_Output((Scanner.ScanlabRTC5.LaserConnector_Out)a_value);
        }

        public static EzIna.IO.DI GetDI(this eDI a_value)
        {
            return EzIna.FA.MGR.IOMgr.DIList[(int)a_value];
        }
        public static EzIna.IO.DO GetDO(this eDO a_value)
        {
            return EzIna.FA.MGR.IOMgr.DOList[(int)a_value];
        }
        public enum eCylinder
        {
            JIG_FEEDER_L_CLAMPER,
            JIG_FEEDER_R_CLAMPER,
            JIG_STOPPER_L_UP,
            JIG_STOPPER_R_UP,
            JIG_STOPPER_M_UP,
            JIG_ACC_UP,

        }
        public enum RTC5Scanner
        {
            Main,
        }

        public enum lightSource
        {
            BAR,
        }
        public enum LIGHT_CH
        {
            NONE = 0,
            LEFT,
            RIGHT,
            BOTTOM,
            UP
        }

        public enum Laser
        {
            //DKT IPG
            MAIN_LASER,
        }
        public enum Powermeter
        {
            Ophir
        }

        public enum Attenuator
        {
            LPA,
        }
        public enum eUnit
        {
            [AddstringAttr("")]
            none,
            [AddstringAttr("m")]
            m,
            [AddstringAttr("mm")]
            mm,
            [AddstringAttr("um")]
            um,
            [AddstringAttr("m")]
            nm,
            [AddstringAttr("sec")]
            sec,
            [AddstringAttr("msec")]
            msec,
            [AddstringAttr("usec")]
            usec,
            [AddstringAttr("nsec")]
            nsec,
            [AddstringAttr("mm/s")]
            mmPerSec,
            [AddstringAttr("mm/s^2")]
            mmPerSecSquared,
            [AddstringAttr("count")]
            ea,
            [AddstringAttr("pxl")]
            pxl,
            [AddstringAttr("pxl/um")]
            pxlPerMicro,
            [AddstringAttr("%")]
            percent,
            [AddstringAttr("deg")]
            deg,
            [AddstringAttr("idx")]
            idx,
            [AddstringAttr("Lvl")]
            lvl,
            [AddstringAttr("Hz")]
            hz,
            [AddstringAttr("khz")]
            Khz,
            [AddstringAttr("W")]
            W,
            [AddstringAttr("mW")]
            mW,
            [AddstringAttr("Max")]
            Max
        }
        public static string GetUnitString(eUnit a_Value)
        {
            var Unitstr = a_Value.GetType().GetMember(a_Value.ToString())
                                   .First()
                                   .GetCustomAttribute<AddstringAttr>();
            if (Unitstr != null)
                return Unitstr.strValue;
            return "";
        }
        public class AddstringAttr : System.Attribute
        {

            private string m_strValue;
            public string strValue
            {
                get { return m_strValue; }
            }
            public AddstringAttr(string a_strvalue)
            {
                m_strValue = a_strvalue;
            }
        }


        public enum eRcpCategory
        {

            Motion,

            Vision,

            CAM,

            Interlock,

            Path,

            InitialProcess,
            Max
        }


        public enum eRcpSubCategory
        {
            M00_STAGE_RAIL,//Motion
            M00_STAGE_SCANNER,

            M00_LASER,
            M00_VISION,
            M00_CROSSHAIR,

            M10_Fine_Matcher,//Vision
            M10_Fine_Filter,
            M10_Fine_Blob,
            M10_Fine_Roi,
            M10_Fine_ExposeTime,
            M10_Fine_LightSource,
            M10_Fine_VisionScale,

            M10_Coarse_Matcher,
            M10_Coarse_Filter,
            M10_Coarse_Blob,
            M10_Coarse_Roi,
            M10_Coarse_ExposeTime,
            M10_Coarse_LightSource,
            M10_Coarse_VisionScale,

            M10_AF,
            M20_, //CAM
            M30_SYSTEM, //Interlock
            M40_, //Path


            //Recipe For DKT
            M06_TEACHING,
            M06_PRODUCT_INFOR,
            M07_MARKING_PARAM,
            M07_DATA_MATRIX_PARAM,
            M07_FONT_MARKING_PARAM, // KKW Font Marking Parameter
            M08_VISION_PARAM,
            M08_VISION_ROI,
            /*
          Laser_Power_Percent
          Half_Period, //Laser
          Pulse_length,
          1st_pulse_Killer,
          Jump_delay,
          Mark_delay,
          Polygon_delay,
          LaserON_delay,
          LaserOFF_delay,
          Jump_Speed,
          Mark_Speed,
          */


            M100_VISION_CAL,//Initial Process 
            M100_STAGE_CAL,
            M100_CROSS_HAIR,
            M100_FIND_FOCUS,
            M100_FIND_CPU,
            M100_GALVO_CAL,
            M100_POWERTABLE,
            M100_PATH,
            M100_TEACHING,
            M100_SYSTEM,
            Max
        }
        #endregion
        public enum eRecipeCategory
        {
            COMMON,
            PROCESS,
            VISION,
            TEACHING,
        }

        #region Vision
        public enum eVision
        {
            FINE,
            COARSE,
        }

        public enum eVisionItem
        {
            None = -1,
            Scale,
            Match,
            Blob,
            Filters,
            ROIs,
            Max
        }
        #endregion


        public static string GetInitializeString(eMoudleName a_Enum, bool a_bInit)
        {
            string strRet = "";
            switch (a_Enum)
            {
                case eMoudleName.MIN:
                    {
                        if (a_bInit == true)
                        {
                            strRet = "Starting Loading";
                        }
                        else
                        {
                            strRet = "Finish Unloading";
                        }
                    }
                    break;
                case eMoudleName.MAX:
                    {
                        if (a_bInit == true)
                        {
                            strRet = "Finish Loading";
                        }
                        else
                        {
                            strRet = "Start Unloading";
                        }
                    }
                    break;
                default:
                    {
                        if (a_bInit == true)
                        {
                            strRet = string.Format("Create the {0} Manager", a_Enum.ToString());
                        }
                        else
                        {
                            strRet = string.Format("Delete the {0} Manager", a_Enum.ToString());
                        }
                    }
                    break;
            }
            return strRet;
        }
        public enum eMoudleName
        {
            MIN = 0,
            VISION,
            IO,
            Laser,
            Attenuator,
            PowerMeter,
            Light,
            MOTION,
            SCANNER,
            RUN,
            GUI,
            RECIPE,
            MES,
            THREAD,
            MAX,
        }

    }

}
