using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    // ----------------------------------------------------------------------------------------------------------
    // 
    // ----------------------------------------------------------------------------------------------------------
    public static partial class RCP
    {
        #region Initial Process Setting 

        private static void InitForIntialProess()
        {
            InitForInitialProces_Vision();
            InitForInitialProces_StageCal();
            InitForInitialProces_CrossHair();
            InitForInitialProces_FindFocus();
            InitForInitialProces_GalvoCal();
            InitForInitialProces_PATH();
            InitForInitialProces_POWERMETER();
            InitForInitialProces_ATT();
            InitForInitialProces_CPU();
            InitForInitialProces_TEACHING();
        }
        #endregion

        #region [ Initial Process - Vision 1000 to 1099]																																																		                                   //Set button, Status Image, Move button
        private static void InitForInitialProces_Vision()
        {
            M100_VisionCalParmDist = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1050, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Distance", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalParamStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1051, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalParamStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1052, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalParamVisionScore = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1053, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Score", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.percent, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalParamMoveDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1054, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Move Delay", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalFineScaleX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1055, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Fine Scale X", "0.0", typeof(double), "{0:F6}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalFineScaleY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1056, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Fine Scale Y", "0.0", typeof(double), "{0:F6}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalCoarseScaleX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1057, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Coarse Scale X", "0.0", typeof(double), "{0:F6}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_VisionCalCoarseScaleY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1058, FA.DEF.eRcpSubCategory.M100_VISION_CAL.ToString(), "Coarse Scale Y", "0.0", typeof(double), "{0:F6}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_VisionCalParmDist;
        public static MF.RcpItem M100_VisionCalParamStageVelocity;
        public static MF.RcpItem M100_VisionCalParamStageAccel;
        public static MF.RcpItem M100_VisionCalParamVisionScore;
        public static MF.RcpItem M100_VisionCalParamMoveDelay;
        public static MF.RcpItem M100_VisionCalFineScaleX;
        public static MF.RcpItem M100_VisionCalFineScaleY;
        public static MF.RcpItem M100_VisionCalCoarseScaleX;
        public static MF.RcpItem M100_VisionCalCoarseScaleY;
        #endregion

        #region [ Initial Process - Stage Cal 1100 to 1199]
        private static void InitForInitialProces_StageCal()
        {
            M100_StageCalParamAlignStartX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1100, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Align Start X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_StageCalParamAlignStartY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1101, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Align Start Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_StageCalParamAlignEndX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1102, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Align End X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_StageCalParamAlignEndY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1103, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Align End Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_StageCalParamStartPosX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1104, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Start Position X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_StageCalParamStartPosY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1105, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Start Position Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_StageCalParamMaxDistX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1106, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Max Distance X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamMaxDistY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1107, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Max Distance Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamPitchX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1108, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Pitch X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamPitchY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1109, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Pitch Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1110, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Stage Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1111, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Stage Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamMoveDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1112, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Move Delay", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.msec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_StageCalParamAccuracy = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1113, FA.DEF.eRcpSubCategory.M100_STAGE_CAL.ToString(), "Accuracy", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_StageCalParamAlignStartX;
        public static MF.RcpItem M100_StageCalParamAlignStartY;
        public static MF.RcpItem M100_StageCalParamAlignEndX;
        public static MF.RcpItem M100_StageCalParamAlignEndY;
        public static MF.RcpItem M100_StageCalParamStartPosX;
        public static MF.RcpItem M100_StageCalParamStartPosY;
        public static MF.RcpItem M100_StageCalParamMaxDistX;
        public static MF.RcpItem M100_StageCalParamMaxDistY;
        public static MF.RcpItem M100_StageCalParamPitchX;
        public static MF.RcpItem M100_StageCalParamPitchY;
        public static MF.RcpItem M100_StageCalParamStageVelocity;
        public static MF.RcpItem M100_StageCalParamStageAccel;
        public static MF.RcpItem M100_StageCalParamMoveDelay;
        public static MF.RcpItem M100_StageCalParamAccuracy;
        //	public static RcpItem I_StageCalParamVerficationMode = new RcpItem(FA.DEF.eRcpCategory.Motion, 1029, FA.DEF.eRcpSubCategory.M_STAGE.ToString(), "Left Table X Pos", "0.0", FA.DEF.eUnit.mm, false, -1);
        #endregion

        #region [ Initial Process - Cross Hair 1200 to 1299]
        private static void InitForInitialProces_CrossHair()
        {
            M100_CrossHairLaserFrq = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1200, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Laser FRQ.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairLaserPwr = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1201, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Power", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.W, true, (int)FA.DEF.eAxesName.None, false, false, false);

            M100_CrossHairMarkingVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1202, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairMarkingAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1203, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Marking Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairJumpVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1204, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Jump Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairJumpAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1205, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Jump Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairMarkingDist = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1206, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Marking Distance", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1208, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Stage Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1209, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Stage Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairMoveDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1210, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Move Delay", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.sec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairThickness = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1211, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Thickness", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);

            M100_CrossHairScannerRefPosX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1220, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Scanner Center. Pos X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_CrossHairScannerRefPosY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1221, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Scanner Center. Pos Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_CrossHairFineVisionRefPosX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1222, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine Vision Center Pos X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_CrossHairFineVisionRefPosY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1223, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine Vision Center Pos Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_CrossHairCoarseVisionRefPosX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1224, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Coarse Vision Center Pos X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true, false);
            M100_CrossHairCoarseVisionRefPosY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1225, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Coarse Vision Center Pos Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true, false);
            M100_CrossHairDistX_From_F_To_C = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1226, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine to Coarse Distance X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true, false);
            M100_CrossHairDistY_From_F_To_C = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1227, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine to Coarse Distance Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true, false);
            M100_CrossHairFine_ScannerAndVisionXOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1228, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine Offset X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairFine_ScannerAndVisionYOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1229, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Fine Offset Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairCoarse_ScannerAndVisionXOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1230, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Coarse Offset X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false, false);
            M100_CrossHairCoarse_ScannerAndVisionYOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1231, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Coarse Offset Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false, false);

            M100_CrossHairJumpDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1233, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Jump Delay.", "100.0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairMarkingDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1234, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "Marking Delay.", "100.0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairLaserOnDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1235, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "LaserOn Delay.", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CrossHairLaserOffDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1236, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR.ToString(), "LaserOff Delay.", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_CrossHairLaserFrq;
        public static MF.RcpItem M100_CrossHairLaserPwr;

        public static MF.RcpItem M100_CrossHairMarkingVelocity;
        public static MF.RcpItem M100_CrossHairMarkingAccel;
        public static MF.RcpItem M100_CrossHairJumpVelocity;
        public static MF.RcpItem M100_CrossHairJumpAccel;
        public static MF.RcpItem M100_CrossHairMarkingDist;
        public static MF.RcpItem M100_CrossHairStageVelocity;
        public static MF.RcpItem M100_CrossHairStageAccel;
        public static MF.RcpItem M100_CrossHairMoveDelay;
        public static MF.RcpItem M100_CrossHairThickness;

        public static MF.RcpItem M100_CrossHairScannerRefPosX;
        public static MF.RcpItem M100_CrossHairScannerRefPosY;
        public static MF.RcpItem M100_CrossHairFineVisionRefPosX;
        public static MF.RcpItem M100_CrossHairFineVisionRefPosY;
        public static MF.RcpItem M100_CrossHairCoarseVisionRefPosX;
        public static MF.RcpItem M100_CrossHairCoarseVisionRefPosY;
        public static MF.RcpItem M100_CrossHairDistX_From_F_To_C;
        public static MF.RcpItem M100_CrossHairDistY_From_F_To_C;
        public static MF.RcpItem M100_CrossHairFine_ScannerAndVisionXOffset;
        public static MF.RcpItem M100_CrossHairFine_ScannerAndVisionYOffset;
        public static MF.RcpItem M100_CrossHairCoarse_ScannerAndVisionXOffset;
        public static MF.RcpItem M100_CrossHairCoarse_ScannerAndVisionYOffset;

        /// <summary>
        /// For DKT
        /// </summary>    
        public static MF.RcpItem M100_CrossHairJumpDelay;
        public static MF.RcpItem M100_CrossHairMarkingDelay;
        public static MF.RcpItem M100_CrossHairLaserOnDelay;
        public static MF.RcpItem M100_CrossHairLaserOffDelay;
        #endregion

        #region [ Initial Process - Find Focus 1300 to 1399]
        private static void InitForInitialProces_FindFocus()
        {
            M100_FindFocusLaserFrq = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1300, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Laser FRQ.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusLaserPwr = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1301, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Power", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.W, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusZRange = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1302, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Z Range", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusZPitch = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1303, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Z Pitch", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusNumOfShot = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1304, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "#.of Shot", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.ea, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusMarkingVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1305, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusMarkingAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1306, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Marking Accel", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1307, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Stage Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1308, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Stage Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusBeamPitchX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1309, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Beam Pitch X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusBeamPitchY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1310, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Beam Pitch Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_FindFocusThickness = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1311, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS.ToString(), "Thickness", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }

        public static MF.RcpItem M100_FindFocusLaserFrq;
        public static MF.RcpItem M100_FindFocusLaserPwr;
        public static MF.RcpItem M100_FindFocusZRange;
        public static MF.RcpItem M100_FindFocusZPitch;
        public static MF.RcpItem M100_FindFocusNumOfShot;
        public static MF.RcpItem M100_FindFocusMarkingVelocity;
        public static MF.RcpItem M100_FindFocusMarkingAccel;
        public static MF.RcpItem M100_FindFocusStageVelocity;
        public static MF.RcpItem M100_FindFocusStageAccel;
        public static MF.RcpItem M100_FindFocusBeamPitchX;
        public static MF.RcpItem M100_FindFocusBeamPitchY;
        public static MF.RcpItem M100_FindFocusThickness;



        #endregion

        #region [ Initial Process - CPU 1400 to 1499]
        private static void InitForInitialProces_CPU()
        {
            M100_CPULaserFrq = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1400, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Laser FRQ.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPULaserPwr = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1401, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Power", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.W, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUMarkingVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1402, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUMarkingAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1403, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Marking Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1404, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Stage Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1405, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Stage Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUStageFOVSize = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1406, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "FOV Size", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUThickness = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1407, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Thickness", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CPUGalvoRotate = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1408, FA.DEF.eRcpSubCategory.M100_FIND_CPU.ToString(), "Galvo Rotate", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.deg, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_CPULaserFrq;
        public static MF.RcpItem M100_CPULaserPwr;
        public static MF.RcpItem M100_CPUMarkingVelocity;
        public static MF.RcpItem M100_CPUMarkingAccel;
        public static MF.RcpItem M100_CPUStageVelocity;
        public static MF.RcpItem M100_CPUStageAccel;
        public static MF.RcpItem M100_CPUStageFOVSize;
        public static MF.RcpItem M100_CPUThickness;
        public static MF.RcpItem M100_CPUGalvoRotate;
        #endregion

        #region [ Inital Process - Galvo Cal 1500 to 1599]
        private static void InitForInitialProces_GalvoCal()
        {
            M100_GalvoCalLaserFrq = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1500, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Laser FRQ.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalLaserPwr = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1501, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Power", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.W, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalMarkingVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1502, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalMarkingAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1503, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Marking Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalJumpVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1504, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Jump Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalJumpAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1505, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Jump Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalStageVelocity = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1506, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Stage Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalStageAccel = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1507, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Stage Accel.", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSecSquared, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalAccuracy = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1508, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Accuracy", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalFOVSize = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1509, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "FOV Size", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalNumOfGridX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1510, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "#. of Grid X", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.ea, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalNumOfGridY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1511, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "#. of Grid Y", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.ea, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalThickness = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1512, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Thickness", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalMarkingDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1514, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_GalvoCalJumpDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 1515, FA.DEF.eRcpSubCategory.M100_GALVO_CAL.ToString(), "Marking Velocity", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.usec, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_GalvoCalLaserFrq;
        public static MF.RcpItem M100_GalvoCalLaserPwr;
        public static MF.RcpItem M100_GalvoCalMarkingVelocity;
        public static MF.RcpItem M100_GalvoCalMarkingAccel;
        public static MF.RcpItem M100_GalvoCalJumpVelocity;
        public static MF.RcpItem M100_GalvoCalJumpAccel;
        public static MF.RcpItem M100_GalvoCalStageVelocity;
        public static MF.RcpItem M100_GalvoCalStageAccel;
        public static MF.RcpItem M100_GalvoCalAccuracy;
        public static MF.RcpItem M100_GalvoCalFOVSize;
        public static MF.RcpItem M100_GalvoCalNumOfGridX;
        public static MF.RcpItem M100_GalvoCalNumOfGridY;
        public static MF.RcpItem M100_GalvoCalThickness;
        public static MF.RcpItem M100_GalvoCalMarkingDelay;
        public static MF.RcpItem M100_GalvoCalJumpDelay;
        //public static RcpItem I_GalvoCalVerficationMode = new RcpItem(FA.DEF.eRcpCategory.Motion, 1000, FA.DEF.eRcpSubCategory.M_STAGE.ToString(), "Left Table X Pos", "0.0", FA.DEF.eUnit.mm, false, -1);
        #endregion

        #region [Initial Process - POWER TABLE 1600 to 1699]
        #endregion

        #region [Initial Process - POWERMETER 2000 to 2099]
        private static void InitForInitialProces_POWERMETER()
        {
            M100_PowerMeter_MeasPosStartX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2000, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Power Measurement Start X", "0.000", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_PowerMeter_MeasPosStartY = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2001, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Power Measurement Start Y", "0.000", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_PowerMeter_MeasPosStartZ = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2002, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Power Measurement Start Z", "0.000", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RZ, true, true, true);

            M100_Powermeter_QFrqMin = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2010, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Q-Frequency Min. Value", "0.00", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_Powermeter_QFrqMax = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2011, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Q-Frequency Max. Value", "0.00", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_Powermeter_QFrqStep = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2012, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Q-Frequency Step", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.Khz, true, (int)FA.DEF.eAxesName.None, false, false, false);

            M100_Powermeter_Scale = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2020, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Power Scale", "1.0", typeof(double), "{0:F4}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_Powermeter_Offset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2021, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Power Offset", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_Powermeter_WaveLength = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2022, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Wave Length", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.idx, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_Powermeter_HeadRange = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2023, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Head Range", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.idx, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_POwermeter_MeasureDelay = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2024, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Measurement delay", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.sec, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_PowerMeter_MeasPosStartX;
        public static MF.RcpItem M100_PowerMeter_MeasPosStartY;
        public static MF.RcpItem M100_PowerMeter_MeasPosStartZ;

        public static MF.RcpItem M100_Powermeter_QFrqMin;
        public static MF.RcpItem M100_Powermeter_QFrqMax;
        public static MF.RcpItem M100_Powermeter_QFrqStep;

        public static MF.RcpItem M100_Powermeter_Scale;
        public static MF.RcpItem M100_Powermeter_Offset;
        public static MF.RcpItem M100_Powermeter_WaveLength;
        public static MF.RcpItem M100_Powermeter_HeadRange;
        public static MF.RcpItem M100_POwermeter_MeasureDelay;
        #endregion
        #region [Initial Process - ATTENUATOR 2100 to 2199]
        private static void InitForInitialProces_ATT()
        {
            M100_ATTENUATOR_AangleMIN = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2100, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Angle MIN", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.deg, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_ATTENUATOR_AangleMAX = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2101, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Angle MAX", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.deg, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_ATTENUATOR_AngleStep = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2102, FA.DEF.eRcpSubCategory.M100_POWERTABLE.ToString(), "Angle Step", "0", typeof(double), "{0:F4}", FA.DEF.eUnit.deg, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_ATTENUATOR_AangleMIN;
        public static MF.RcpItem M100_ATTENUATOR_AangleMAX;
        public static MF.RcpItem M100_ATTENUATOR_AngleStep;
        #endregion

        #region [Initial Process - PATH 2200 to 2299]
        private static void InitForInitialProces_PATH()
        {
            M100_GALVO_CAL_PATH = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2200, FA.DEF.eRcpSubCategory.M100_PATH.ToString(), "Galvo calibration file path", "0", typeof(string), "{0}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_STAGE_CAL_PATH = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2201, FA.DEF.eRcpSubCategory.M100_PATH.ToString(), "Stage calibration file path", "0", typeof(string), "{0}", FA.DEF.eUnit.none, true, (int)FA.DEF.eAxesName.None, false, false, false);
        }
        public static MF.RcpItem M100_GALVO_CAL_PATH;
        public static MF.RcpItem M100_STAGE_CAL_PATH;
        #endregion
        #region [ Initial Process - TEACHING 2300 ~ 2399]
        private static void InitForInitialProces_TEACHING()
        {
            M100_LaserFocusZPos = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2300, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Laser Z Focus Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RZ, true, true, true);
            M100_JigHeight = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2301, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Jig Height Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_AutoFocusOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2302, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Auto Focus Offset", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_CoarseVisionFocusZPos = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2303, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Coarse Vision Z Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RZ, true, true, true, false);
            M100_FineVisionFocusZPos = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2304, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Fine Vision Z Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RZ, true, true, true);
            M100_VisionFocusOffset = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2305, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Vision Focus Offset", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.None, false, false, false);
            M100_RAIL_LOADING_X_POS = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2306, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Rail Loading X Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_RAIL_UNLOADING_X_POS = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2307, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Rail UnLoading X Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_RAIL_LOADING_PROC_AREA_X_POS = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2308, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Rail PROC Area X Pos", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_RAIL_ADJUST_INIT_WIDTH = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2309, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Rail Adjust Init Width", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RAIL_ADJUST, true, true, true);
            M100_X_STROKE_LIMIT = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2310, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "X Stroke Limit", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.RX, true, true, true);
            M100_Y_STROKE_LIMIT = new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2311, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "Y Stroke Limit", "0.0", typeof(double), "{0:F4}", FA.DEF.eUnit.mm, true, (int)FA.DEF.eAxesName.Y, true, true, true);
            M100_X_LOADING_RETURN_SPEED=new MF.RcpItem(FA.DEF.eRcpCategory.InitialProcess, 2312, FA.DEF.eRcpSubCategory.M100_TEACHING.ToString(), "X Loading Return Speed", "350", typeof(double), "{0:F4}", FA.DEF.eUnit.mmPerSec, true, (int)FA.DEF.eAxesName.RX, false, false, false);
        }
        public static MF.RcpItem M100_LaserFocusZPos;
        public static MF.RcpItem M100_JigHeight;
        public static MF.RcpItem M100_AutoFocusOffset;
        public static MF.RcpItem M100_CoarseVisionFocusZPos;
        public static MF.RcpItem M100_FineVisionFocusZPos;
        public static MF.RcpItem M100_VisionFocusOffset;
        public static MF.RcpItem M100_RAIL_LOADING_X_POS;
        public static MF.RcpItem M100_RAIL_UNLOADING_X_POS;
        public static MF.RcpItem M100_RAIL_LOADING_PROC_AREA_X_POS;
        public static MF.RcpItem M100_RAIL_ADJUST_INIT_WIDTH;
        public static MF.RcpItem M100_X_STROKE_LIMIT;
        public static MF.RcpItem M100_Y_STROKE_LIMIT;
        public static MF.RcpItem M100_X_LOADING_RETURN_SPEED;
        #endregion[ Initial Process - TEACHING ]
    }
}
