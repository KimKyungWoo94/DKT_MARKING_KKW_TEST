using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    public static class OPT
    {
        public static void Init()
        {
            Trace.WriteLine("OPT.Init Completed");
            Init_VisionOption();
            Init_InterlockOption();
            Init_InitialProcessOption();
        }
        #region [MOTION]

        //public static MF.OptItem InvertYAxisDirectionForJogging		= new MF.OptItem(FA.DEF.eRcpCategory.Motion, FA.DEF.eRcpSubCategory.M00_STAGE, "Jog Y Direction Reverse"			, false	,	false	 									);
        //public static MF.OptItem AxisZMoveDelayForDrilling			= new MF.OptItem(FA.DEF.eRcpCategory.Motion, FA.DEF.eRcpSubCategory, "Axis Z Additional Move Delay"		, false	,	true	, "1.0"	,FA.DEF.eUnit.sec			);
        //public static MF.OptItem AxisZVelocity						= new MF.OptItem(FA.DEF.eRcpCategory.Motion, FA.DEF.eRcpSubCategory, "Axis Z Velocity Using"			, false	,	true	, "3.0"	,FA.DEF.eUnit.mmPerSec		);
        #endregion [MOTION]

        #region [VISION]
        public static void Init_VisionOption()
        {
            BlobResultDisplay = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M100_VISION_CAL, "Blob Result Display", false);
            FilterDisplay = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M100_VISION_CAL, "Filter Display", false);
            MatchResultDisplay = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M100_VISION_CAL, "Matcher Result Display", false);
            CrossLineVisible = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M100_VISION_CAL, "Cross Line Visible", false);
            //ROIsVisible = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M100_VISION_CAL, "ROIs Visible", false);

            UseAutoFocus = new MF.OptItem(FA.DEF.eRcpCategory.Vision, FA.DEF.eRcpSubCategory.M00_VISION, "Use auto focus", false);
        }
        public static MF.OptItem BlobResultDisplay;
        public static MF.OptItem FilterDisplay;
        public static MF.OptItem MatchResultDisplay;
        public static MF.OptItem CrossLineVisible;
        //public static MF.OptItem ROIsVisible;

        public static MF.OptItem UseAutoFocus;
        #endregion[VISION]

        #region [SCANNER]
        #endregion[SCANNER]

        #region [LASER]
        #endregion[LASER]

        #region [PWRMETER]
        #endregion[PWRMETER]

        #region [ATTENUATOR]
        #endregion

        #region [CAM]
        #endregion[CAM]

        #region [INTERLOCK]
        public static void Init_InterlockOption()
        {

        }

        #endregion [INTERLOCK]

        #region [INITIAL PROCESS]
        public static void Init_InitialProcessOption()
        {
            StageCalParam_VerficationEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_STAGE_CAL, "Use the stage calibration verification", false);
            StageCalParam_Y_Dir_FirstEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_STAGE_CAL, "Enable the y-axis direction first mode", false);
            StageCalParam_X_Axis_ZigzagEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_STAGE_CAL, "Enable the x-axis zigzag movement mode", false);
            StageCalParam_Y_Axis_ZigzagEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_STAGE_CAL, "Enable the y-axis zigzag movement mode", false);

            GalvoCalParam_Y_Dir_FirstEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_GALVO_CAL, "Enable the y-axis direction first mode", false);
            GalvoCalParam_X_Axis_ZigzagEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_GALVO_CAL, "Enable the x-axis zigzag movement mode", false);
            GalvoCalParam_Y_Axis_ZigzagEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_GALVO_CAL, "Enable the y-axis zigzag movement mode", false);
            GalvoCalParam_VerficationEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_GALVO_CAL, "Use the galvo calibration verification", false);


            DoorOpenRunAllow = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable Door Open Run", false);
            DryRunningEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable Dry Running", false);
            RunningLoaderIFEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable Loader IF ", true);
            RunninguUnLoaderIFEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable UnLoader IF ", true);
            MES_TestModeEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable MES Test Mode ", false);
            RunningProductErrorAlarmEnable = new MF.OptItem(FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_SYSTEM, "Enable Product Error Alarm", false);
        }

        public static MF.OptItem StageCalParam_VerficationEnable;
        public static MF.OptItem StageCalParam_Y_Dir_FirstEnable;
        public static MF.OptItem StageCalParam_X_Axis_ZigzagEnable;
        public static MF.OptItem StageCalParam_Y_Axis_ZigzagEnable;

        public static MF.OptItem GalvoCalParam_Y_Dir_FirstEnable;
        public static MF.OptItem GalvoCalParam_X_Axis_ZigzagEnable;
        public static MF.OptItem GalvoCalParam_Y_Axis_ZigzagEnable;
        public static MF.OptItem GalvoCalParam_VerficationEnable;

        public static MF.OptItem DoorOpenRunAllow;
        public static MF.OptItem DryRunningEnable;
        public static MF.OptItem RunningLoaderIFEnable;
        public static MF.OptItem RunninguUnLoaderIFEnable;
        public static MF.OptItem MES_TestModeEnable;
        public static MF.OptItem RunningProductErrorAlarmEnable;


        #endregion


    }
}
