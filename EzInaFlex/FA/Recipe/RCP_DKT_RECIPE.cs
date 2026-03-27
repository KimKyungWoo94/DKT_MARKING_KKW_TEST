using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    public enum eRecipeRowProgressDir
    {
        UP,
        DOWN,
    }
    public enum eRecipeColProgressDir
    {
        LEFT,
        RIGHT
    }
    public enum eRecieMultiArrayDir
    {
        ROW,
        COLUMN,
    }
    public static partial class RCP_Modify
    {
        public static void InitRecipeParam()
        {
            InitForCommon();
            InitForProcessParam();
            InitForVision();

        }

        #region    COMMON
        private static void InitForCommon()
        {
            InitForCommon_Teaching();
            InitForCommon_ProductInfor();
        }

        #region TEACHING_POS
        private static void InitForCommon_Teaching()
        {
            int iStartIDX = 101;
            COMMON_INIT_PROC_AREA_X_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Init Proc Area X Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },

                SetButtonFunc = () =>
                {
                    return AXIS.RX.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RX.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_INIT_PROC_AREA_Y_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Init Proc Area Y Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.Y.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.Y.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_INIT_PROC_AREA_Z_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Init Proc Area Z Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 500.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RZ.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RZ.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };

            COMMON_FIRST_PRODUCT_X_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Product 1st X Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },

                SetButtonFunc = () =>
                {
                    return AXIS.RX.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RX.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };


            COMMON_FIRST_PRODUCT_Y_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Product 1st Y Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.Y.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.Y.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_FIRST_PRODUCT_Z_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Product 1st Z Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 500.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RZ.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RZ.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };


            COMMON_JIG_CODE_X_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "JIG Code X Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RX.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RX.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };


            COMMON_JIG_CODE_Y_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "JIG Code Y Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.Y.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.Y.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_JIG_CODE_Z_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "JIG Code Z Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 500.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RZ.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RZ.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };



            /// <summary>
            /// double 
            /// </summary>		
            COMMON_GUIDE_BAR_X_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Guide Bar X Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RX.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RX.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };

            COMMON_GUIDE_BAR_Y_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Guide Bar Y Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 1000.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.Y.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.Y.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_GUIDE_BAR_Z_POS = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Guide Bar Z Pos", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 500.0;
                },
                SetButtonFunc = () =>
                {
                    return AXIS.RZ.Status().IsHomeComplete == true ?
                                    Math.Truncate(AXIS.RZ.Status().m_stPositionStatus.fActPos * 1000) / 1000.0 :
                                    0.0;
                },
                iFormatNumberOfZero = 3
            };
        }

        /// <summary>
        /// double 
        /// </summary>
        /*public static MF.RecipeItem COMMON_LOADING_RAIL_WIDTH = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_TEACHING.ToString(), "Loading Rail Width", 100, 0.0, FA.DEF.eUnit.mm)
        {

                fMinDelegate = () =>
                {
                        return 0.0;
                },
                fMaxDelegate = () =>
                {
                        return 14.0;
                },
                SetButtonFunc = () =>
                {
                      return AXIS.RAIL_ADJUST.Status().IsHomeComplete==true ?  
                        Math.Truncate(AXIS.RAIL_ADJUST.Status().m_stPositionStatus.fCmdPos  * 1000) / 1000.0		:
                      0.0;
                },
              iFormatNumberOfZero=3
        };*/
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem COMMON_INIT_PROC_AREA_X_POS;
        /// <summary>
        /// double 
        /// </summary>														
        public static MF.RecipeItem COMMON_INIT_PROC_AREA_Y_POS;
        /// <summary>
        /// double 
        /// </summary>																		
        public static MF.RecipeItem COMMON_INIT_PROC_AREA_Z_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_FIRST_PRODUCT_X_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_FIRST_PRODUCT_Y_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_FIRST_PRODUCT_Z_POS;


        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_JIG_CODE_X_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_JIG_CODE_Y_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_JIG_CODE_Z_POS;

        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_GUIDE_BAR_X_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_GUIDE_BAR_Y_POS;
        /// <summary>
        /// double 
        /// </summary>		
        public static MF.RecipeItem COMMON_GUIDE_BAR_Z_POS;

        #endregion TEACHING_POS

        #region ProductInfor
        private static void InitForCommon_ProductInfor()
        {
            COMMON_PRODUCT_ROW_COUNT = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Row Count", 150, 0, FA.DEF.eUnit.ea)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 100;
                },
            };
            COMMON_PRODUCT_COL_COUNT = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Column Count", 151, 0, FA.DEF.eUnit.ea)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 100;
                },
            };
            COMMON_PRODUCT_ROW_PITCH = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Row Pitch", 152, 0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 100;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_PRODUCT_COL_PITCH = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Col Pitch", 153, 0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0.0;
                },
                fMaxDelegate = () =>
                {
                    return 100;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_PRODUCT_ROW_PROGRESS_DIR = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Row Progress Dir", 154, eRecipeRowProgressDir.DOWN, FA.DEF.eUnit.none)
            {

            };
            COMMON_PRODUCT_COL_PROGRESS_DIR = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Product Col Progress Dir", 155, eRecipeColProgressDir.LEFT, FA.DEF.eUnit.none)
            {

            };
            COMMON_PRODUCT_MULTI_ARRAY_COUNT = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "MultiArray Count", 156, 1, FA.DEF.eUnit.none)
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
            };
            COMMON_PRODUCT_MULTI_ARRAY_GAP = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "Multi Array GAP", 157, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return -300.0;
                },
                fMaxDelegate = () =>
                {
                    return 300.0;
                },
                iFormatNumberOfZero = 3
            };
            COMMON_PRODUCT_MULTI_ARRAY_DIR = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "MultiArray Direction", 158, eRecieMultiArrayDir.ROW, FA.DEF.eUnit.none)
            {

            };
            COMMON_PRODUCT_JIG_WIDTH = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "JIG width", 159, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 1000;
                },
            };

            COMMON_PRODUCT_JIG_HEIGHT = new MF.RecipeItem(FA.DEF.eRecipeCategory.COMMON, FA.DEF.eRcpSubCategory.M06_PRODUCT_INFOR.ToString(), "JIG Height", 160, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 1000;
                },
            };
        }

        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_ROW_COUNT;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_COL_COUNT;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_ROW_PITCH;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_COL_PITCH;
        /// <summary>
        /// 	FA.eRecipeRowProgressDir
        /// </summary>							
        public static MF.RecipeItem COMMON_PRODUCT_ROW_PROGRESS_DIR;
        /// <summary>
        /// FA.eRecipeColProgressDir
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_COL_PROGRESS_DIR;

        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_MULTI_ARRAY_COUNT;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_MULTI_ARRAY_GAP;
        /// <summary>
        /// eRecieMultiArrayDir
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_MULTI_ARRAY_DIR;

        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_JIG_WIDTH;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem COMMON_PRODUCT_JIG_HEIGHT;



        #endregion ProductInfor
        #endregion COMMON

        #region    PROCESS PARAM   

        private static void InitForProcessParam()
        {
            InitForProcessParam_Scanner_Laser();
            InitForProcessParam_DataMatrix();
            InitForProcessParam_FontMarking();//KKW Font Marking Parameter
        }
        #region Scanner & Laser
        private static void InitForProcessParam_Scanner_Laser()
        {
            PROCESS_SCANNER_FREQ = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Frequency", 902, 1.0, FA.DEF.eUnit.Khz, "1/(Value*2.0/1000.0)", FA.DEF.eUnit.usec)
            {
                //1 / (FreQHalfPeriod / 1000000.0 * 2.0)
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinFreQuency / 1000.0;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxFreQuency / 1000.0;
                },
            };
            PROCESS_SCANNER_DUTY_RATIO = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Duty Cycle Ratio", 903, 0.5, FA.DEF.eUnit.percent, "")
            {
                fMinDelegate = () =>
                {
                    return 0.1;
                },
                fMaxDelegate = () =>
                {
                    return 1.0;
                },
            };
            PROCESS_LASER_POWER = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Laser Power", 904, 0.00, FA.DEF.eUnit.W, "", FA.DEF.eUnit.none)
            {
                //1 / (FreQHalfPeriod / 1000000.0 * 2.0)
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    if (FA.MGR.LaserMgr.IsExistFrequencyTable(PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                    {
                        return FA.MGR.LaserMgr.GetPwrTableData(PROCESS_SCANNER_FREQ.GetValue<int>() * 1000).MaximumPower;
                    }
                    else
                    {
                        return 0;
                    }
                },
                iFormatNumberOfZero = 3
            };
            PROCESS_SCANNER_1ST_PULSE_KILLDER = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner 1st Pulse Killer", 905, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinFirstPulseKillerLength;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxFirstPulseKillerLength;
                },
            };
            PROCESS_SCANNER_JUMP_DELAY = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Jump Delay", 906, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinScannerDelays;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxScannerDelays;
                },
            };
            PROCESS_SCANNER_MARK_DELAY = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Mark Delay", 907, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinScannerDelays;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxScannerDelays;
                },
            };
            PROCESS_SCANNER_POLYGON_DELAY = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Polygon Delay", 908, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinScannerDelays;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxScannerDelays;
                },
            };
            PROCESS_SCANNER_LASER_ON_DELAY = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Laser On Delay", 909, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinLaserOnDelay;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxLaserOnDelay;
                },

            };
            PROCESS_SCANNER_LASER_OFF_DELAY = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Laser Off Delay", 910, 1.0, FA.DEF.eUnit.usec, "")
            {
                fMinDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MinLaserOffDelay;
                },
                fMaxDelegate = () =>
                {
                    return Scanner.ScanlabRTC5.MaxLaserOffDelay;
                },
            };
            PROCESS_SCANNER_JUMP_SPEED = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Jump Speed", 911, 1.0, FA.DEF.eUnit.mmPerSec, "")
            {
                fMinDelegate = () =>
                {
                    return RTC5.Instance.MinJump_MarkSpeed;
                },
                fMaxDelegate = () =>
                {
                    return RTC5.Instance.MaxJump_MarkSpeed;
                },
                iFormatNumberOfZero = 1
            };
            PROCESS_SCANNER_MARK_SPEED = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_MARKING_PARAM.ToString(), "Scanner Mark Speed", 912, 1.0, FA.DEF.eUnit.mmPerSec, "")
            {
                fMinDelegate = () =>
                {
                    return RTC5.Instance.MinJump_MarkSpeed;
                },
                fMaxDelegate = () =>
                {
                    return RTC5.Instance.MaxJump_MarkSpeed;
                },
                iFormatNumberOfZero = 1
            };
        }

        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_FREQ;
        /// <summary>
        /// double , min =0 , Max=1
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_DUTY_RATIO;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_LASER_POWER;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_1ST_PULSE_KILLDER;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_JUMP_DELAY;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_MARK_DELAY;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_POLYGON_DELAY;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_LASER_ON_DELAY;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_LASER_OFF_DELAY;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_JUMP_SPEED;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_SCANNER_MARK_SPEED;
        #endregion Scanner & Laser
        #region DataMatrix
        private static void InitForProcessParam_DataMatrix()
        {
            PROCESS_DATA_MAT_SIZE = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Size", 950, EzIna.DataMatrix.eDataMatrixSize.DM20X20, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_SHAPE = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Shape", 951, EzIna.DataMatrix.CellShape.Rectangle, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_ZERO_POS = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Zero Default", 952, EzIna.DataMatrix.ZeroPosition.Center, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_ROTATE = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Rotate", 953, EzIna.DataMatrix.Rotate.R_0, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_WIDTH = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Width", 954, 2.5, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 50;
                },
                iFormatNumberOfZero = 1
            };
            PROCESS_DATA_MAT_HEIGHT = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Height", 955, 2.5, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 50;
                },
                iFormatNumberOfZero = 1
            };
            PROCESS_DATA_MAT_HATCH_TYPE = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Hatch Type", 956, EzIna.DataMatrix.DM_HATCH_TYPE.LINE, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_HATCH_Outline_Enable = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Hatch OutLine Enable", 957, false, FA.DEF.eUnit.none, "")
            {

            };
            PROCESS_DATA_MAT_HATCH_LineAngle = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Hatch Line Angle", 958, 0.0, FA.DEF.eUnit.deg, "")
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 360;
                },

            };
            PROCESS_DATA_MAT_HATCH_LinePitch = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Hatch Line Pitch", 959, 0.1, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
             {
                 return 0.001;
             },
                fMaxDelegate = () =>
                 {
                     return 10000;
                 },
                iFormatNumberOfZero = 3
            };
            PROCESS_DATA_MAT_HATCH_OffSet = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Hatch Offset", 960, 0.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 10000;
                },
                iFormatNumberOfZero = 3
            };

            PROCESS_DATA_MAT_MARKING_OFFSET_X = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Marking Offset X", 961, 0.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return -10;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
                iFormatNumberOfZero = 3
            };



            PROCESS_DATA_MAT_MARKING_OFFSET_Y = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Data Matrix Marking Offset Y", 962, 0.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return -10;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
                iFormatNumberOfZero = 3
            };
            PROCESS_DATA_MAT_MARKING_NUM_COUNT = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Marking Number digit", 963, (int)5, FA.DEF.eUnit.none, "")
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 30;
                },
            };
            int iItemNum = 964;
            System.Reflection.FieldInfo[] fieldInfos = typeof(FA.RCP_Modify).GetFields(BindingFlags.Public | BindingFlags.Static);
            MF.RecipeItem_DPValue pItem = null;
            string strTmp = "";
            string strItemName;
            string[] strValueList = null;
            int iOffsetNumber = 0;
            foreach (FieldInfo Item in fieldInfos)
            {
                if (Item.FieldType == typeof(MF.RecipeItem_DPValue))
                {

                    if (Item.Name.Contains("PROCESS_DATA_MAT_MARKING_OFFSET_E_X"))
                    {
                        pItem = (MF.RecipeItem_DPValue)Item.GetValue(null);
                        strTmp = Item.Name.Replace("PROCESS_DATA_MAT_MARKING_OFFSET_E_X_", "");
                        int.TryParse(strTmp, out iOffsetNumber);
                        strItemName = string.Format("Data Matrix Marking Offset X{0}", iOffsetNumber + 1);
                        pItem = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), strItemName, iItemNum++, 0.0, FA.DEF.eUnit.mm, "")
                        {
                            fMinDelegate = () =>
                            {
                                return -10;
                            },
                            fMaxDelegate = () =>
                            {
                                return 10;
                            },
                            iFormatNumberOfZero = 3
                        };
                        Item.SetValue(Item.FieldHandle, pItem);
                    }
                    else if (Item.Name.Contains("PROCESS_DATA_MAT_MARKING_OFFSET_E_Y"))
                    {
                        pItem = (MF.RecipeItem_DPValue)Item.GetValue(null);
                        strTmp = Item.Name.Replace("PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_", "");
                        int.TryParse(strTmp, out iOffsetNumber);
                        strItemName = string.Format("Data Matrix Marking Offset Y{0}", iOffsetNumber + 1);
                        pItem = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), strItemName, iItemNum++, 0.0, FA.DEF.eUnit.mm, "")
                        {
                            fMinDelegate = () =>
                            {
                                return -10;
                            },
                            fMaxDelegate = () =>
                            {
                                return 10;
                            },
                            iFormatNumberOfZero = 3
                        };
                        Item.SetValue(Item.FieldHandle, pItem);
                    }

                }
            }
            PROCESS_DATA_MAT_REPEAT_MARKING_COUNT = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Marking Repeat Count", 984, (int)1, FA.DEF.eUnit.none, "")
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 100;
                },
            };

            PROCESS_DATA_MAT_MARKING_OFFSET_GB_X = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Guide Bar DMC Marking Offset X", 985, 0.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return -10;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
                iFormatNumberOfZero = 3
            };
            PROCESS_DATA_MAT_MARKING_OFFSET_GB_Y = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_DATA_MATRIX_PARAM.ToString(), "Guide Bar DMC Marking Offset Y", 986, 0.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () =>
                {
                    return -10;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
                iFormatNumberOfZero = 3
            };
        }
        #region Font Marking
        private static void InitForProcessParam_FontMarking() //KKW Font Marking Parameter
        {
            // 폰트 마킹 사용 여부 (false이면 DM만 마킹)
            PROCESS_FONT_MARKING_ENABLE = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_FONT_MARKING_PARAM.ToString(), "Font Marking Enable", 987, false, FA.DEF.eUnit.none, "")
            {
            };
            // 텍스트 영역 가로 크기 (글자 한 개의 너비 = 텍스트 블록 가로 크기, 사용자 직접 입력)
            PROCESS_FONT_TEXT_WIDTH_MM = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_FONT_MARKING_PARAM.ToString(), "Font Text Width", 988, 2.0, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () => { return 0.1; },
                fMaxDelegate = () => { return 100.0; },
                iFormatNumberOfZero = 2
            };
            // 데이터 매트릭스 오른쪽 끝에서 텍스트 시작까지의 간격
            PROCESS_FONT_GAP_FROM_MATRIX_MM = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_FONT_MARKING_PARAM.ToString(), "Font Gap From Matrix", 989, 0.5, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () => { return 0.0; },
                fMaxDelegate = () => { return 20.0; },
                iFormatNumberOfZero = 2
            };
            // 세로 배치 시 글자 간 간격
            PROCESS_FONT_CHAR_SPACING_MM = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_FONT_MARKING_PARAM.ToString(), "Font Char Spacing", 990, 0.2, FA.DEF.eUnit.mm, "")
            {
                fMinDelegate = () => { return 0.0; },
                fMaxDelegate = () => { return 10.0; },
                iFormatNumberOfZero = 2
            };
            // 폰트 마킹 전용 스캐너 마크 속도 (DM 마킹보다 낮게 설정)
            PROCESS_FONT_MARK_SPEED = new MF.RecipeItem_DPValue(FA.DEF.eRecipeCategory.PROCESS, FA.DEF.eRcpSubCategory.M07_FONT_MARKING_PARAM.ToString(), "Font Mark Speed", 991, 10.0, FA.DEF.eUnit.mmPerSec, "")
            {
                fMinDelegate = () => { return 1.0; },
                fMaxDelegate = () => { return 10000.0; },
                iFormatNumberOfZero = 1
            };
        }
        /// <summary>bool: 폰트 마킹 사용 여부 (false = DM 마킹만 수행)</summary>
        public static MF.RecipeItem_DPValue PROCESS_FONT_MARKING_ENABLE;//KKW Font Marking Parameter
        /// <summary>double: 텍스트 영역 가로 크기 (mm) - 사용자 직접 입력</summary>
        public static MF.RecipeItem_DPValue PROCESS_FONT_TEXT_WIDTH_MM;//KKW Font Marking Parameter
        /// <summary>double: 데이터 매트릭스 오른쪽 끝 → 텍스트 시작 간격 (mm)</summary>
        public static MF.RecipeItem_DPValue PROCESS_FONT_GAP_FROM_MATRIX_MM;//KKW Font Marking Parameter
        /// <summary>double: 세로 배치 글자 간격 (mm)</summary>
        public static MF.RecipeItem_DPValue PROCESS_FONT_CHAR_SPACING_MM;//KKW Font Marking Parameter
        /// <summary>double: 폰트 마킹 전용 마크 속도 (mm/s) - DM보다 낮게 설정</summary>
        public static MF.RecipeItem_DPValue PROCESS_FONT_MARK_SPEED;//KKW Font Marking Parameter
        #endregion Font Marking
        /// <summary>
        /// EzIna.DataMatrix.eDataMatrixSize
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_SIZE;
        /// <summary>
        /// EzIna.DataMatrix.CellShape
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_SHAPE;
        /// <summary>
        /// EzIna.DataMatrix.ZeroPosition
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_ZERO_POS;
        /// <summary>
        /// EzIna.DataMatrix.Rotate
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_ROTATE;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_WIDTH;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HEIGHT;
        /// <summary>
        /// EzIna.DataMatrix.DM_HATCH_TYPE
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HATCH_TYPE;
        /// <summary>
        /// bool
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HATCH_Outline_Enable;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HATCH_LineAngle;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HATCH_LinePitch;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_HATCH_OffSet;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_X;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_Y;
        #region [ Offset X,Y 10EA]
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_0;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_0;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_1;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_1;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_2;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_2;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_3;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_3;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_4;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_4;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_5;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_5;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_6;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_6;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_7;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_7;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_8;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_8;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_X_9;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_E_Y_9;
        #endregion [ Offset X,Y 10EA]
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_NUM_COUNT;
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_REPEAT_MARKING_COUNT;


        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_GB_X;
        /// <summary>
        /// double
        /// </summary>
        public static MF.RecipeItem_DPValue PROCESS_DATA_MAT_MARKING_OFFSET_GB_Y;
        #endregion DataMatrix
        #endregion PROCESS PARAM

        #region VISION

        private static void InitForVision()
        {
            int iStartIDX = 700;
            Matcher_Minimum_Scale = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Minimum Scale", iStartIDX++, 0.8, FA.DEF.eUnit.percent)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
            };
            Matcher_Maximum_Scale = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Maximum Scale", iStartIDX++, 1.2, FA.DEF.eUnit.percent)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
            };
            Matcher_Match_Score = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Score", iStartIDX++, 0.85, FA.DEF.eUnit.percent)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 1;
                },
            };
            Matcher_Match_Angle = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Angle", iStartIDX++, 10.0, FA.DEF.eUnit.deg)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 360;
                },

            };
            Matcher_Match_MaxCount = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Max count", iStartIDX++, (uint)10, FA.DEF.eUnit.ea)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 10000000;
                },
            };
            Matcher_Match_CorrelationMode = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Correlation Mode", iStartIDX++, 1, FA.DEF.eUnit.none)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 3;
                },
                /* 
                public enum ECorrelationMode
                {
                Standard = 0,
                OffsetNormalized = 1,
                GainNormalized = 2,
                Normalized = 3,
                reserved0 = 4,
                reserved1 = int.MaxValue
                }*/

            };
            Matcher_Match_ContrastMode = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match ContrastMode", iStartIDX++, 1, FA.DEF.eUnit.idx)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 2;
                },
                /*
                public enum EMatchContrastMode
                {
                Normal = 0,
                Inverse = 1,
                Any = 2,
                reserved0 = int.MaxValue
                }*/
            };
            LIGHT_Source_Lvl_L = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Light Left", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 255;
                },
                SetButtonFunc = () =>
                {
                    return LIGHTSOURCE.BAR?.GetIntensity((int)DEF.LIGHT_CH.LEFT);
                },
            };
            LIGHT_Source_Lvl_R = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Light Right", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                    {
                        return 0;
                    },
                fMaxDelegate = () =>
                {
                    return 255;
                },
                SetButtonFunc = () =>
                {
                    return LIGHTSOURCE.BAR?.GetIntensity((int)DEF.LIGHT_CH.RIGHT);
                },
            };
            LIGHT_Source_Lvl_U = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Light Up", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                    {
                        return 0;
                    },
                fMaxDelegate = () =>
                {
                    return 255;
                },
                SetButtonFunc = () =>
              {
                  return LIGHTSOURCE.BAR?.GetIntensity((int)DEF.LIGHT_CH.UP);
              },
            };
            LIGHT_Source_Lvl_B = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Light Bottom", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                    {
                        return 0;
                    },
                fMaxDelegate = () =>
                {
                    return 255;
                },
                SetButtonFunc = () =>
                {
                    return LIGHTSOURCE.BAR?.GetIntensity((int)DEF.LIGHT_CH.BOTTOM);
                },
            };
            Inspection_RowCount = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Inspection Row Count", iStartIDX++, 1, FA.DEF.eUnit.ea)
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },

            };
            Inspection_ColCount = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Inspection Col Count", iStartIDX++, 1, FA.DEF.eUnit.ea)
            {
                fMinDelegate = () =>
                {
                    return 1;
                },
                fMaxDelegate = () =>
                {
                    return 1;
                },

            };
            MATCH_IMG_IDX = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Image IDX", iStartIDX++, EzInaVision.GDV.eGoldenImages.Fiducial_No1, FA.DEF.eUnit.none)
            {

            };
            MATCH_FIND_ROI_IDX = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Match Find ROI IDX", iStartIDX++, EzInaVision.GDV.eRoiItems.ROI_No0, FA.DEF.eUnit.none)
            {

            };
            INSP_1st_Pixel_Center_X = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Inspection 1st Pixel Center X", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 4080;
#else
                    return FA.VISION.FINE_CAM.GetImageWidth();
#endif
                },
            };
            INSP_1st_Pixel_Center_Y = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Inspection 1st Pixel Center Y", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 3000;

#else
                    return FA.VISION.FINE_CAM.GetImageHeight();
#endif
                },
            };
            INSP_Pixel_Error_Range = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Inspection Error Range", iStartIDX++, 0.0, FA.DEF.eUnit.mm)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
                    return 10;
                },
            };

            JIGCode_ROI_POS_X = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "JIG Code ROI X", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 4080;
#else
                    return FA.VISION.FINE_CAM.GetImageWidth();
#endif
                },
                SetButtonFunc = () =>
                {
                    if (MGR.dicFrmVisions["FINE"].IsRoiBoxVisble)
                    {
                        Rectangle rect = MGR.dicFrmVisions["FINE"].rectRoiBox;
                        JIGCode_ROI_POS_Y.SettingValue = rect.Y;
                        JIGCode_ROI_POS_WIDTH.SettingValue = rect.Width;
                        JIGCode_ROI_POS_HEIGHT.SettingValue = rect.Height;
                        return rect.X;
                    }
                    else
                    {
                        return JIGCode_ROI_POS_X.GetValue<int>();
                    }
                },
            };
            JIGCode_ROI_POS_Y = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "JIG Code ROI Y", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 3000;
#else
                    return FA.VISION.FINE_CAM.GetImageHeight();
#endif
                },
                SetButtonFunc = () =>
                {
                    if (MGR.dicFrmVisions["FINE"].IsRoiBoxVisble)
                    {
                        Rectangle rect = MGR.dicFrmVisions["FINE"].rectRoiBox;
                        JIGCode_ROI_POS_X.SettingValue = rect.X;
                        JIGCode_ROI_POS_WIDTH.SettingValue = rect.Width;
                        JIGCode_ROI_POS_HEIGHT.SettingValue = rect.Height;
                        return rect.Y;
                    }
                    else
                    {
                        return JIGCode_ROI_POS_Y.GetValue<int>();
                    }
                },
            };
            JIGCode_ROI_POS_WIDTH = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "JIG Code ROI Width", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 4080 - JIGCode_ROI_POS_X.GetValue<int>();
#else
                    return FA.VISION.FINE_CAM.GetImageWidth() - JIGCode_ROI_POS_X.GetValue<int>();
#endif
                },
                SetButtonFunc = () =>
                {
                    if (MGR.dicFrmVisions["FINE"].IsRoiBoxVisble)
                    {
                        Rectangle rect = MGR.dicFrmVisions["FINE"].rectRoiBox;
                        JIGCode_ROI_POS_X.SettingValue = rect.X;
                        JIGCode_ROI_POS_Y.SettingValue = rect.Y;
                        JIGCode_ROI_POS_HEIGHT.SettingValue = rect.Height;
                        return rect.Width;
                    }
                    else
                    {
                        return JIGCode_ROI_POS_WIDTH.GetValue<int>();
                    }
                },
            };
            JIGCode_ROI_POS_HEIGHT = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "JIG Code ROI Height", iStartIDX++, 0, FA.DEF.eUnit.pxl)
            {
                fMinDelegate = () =>
                {
                    return 0;
                },
                fMaxDelegate = () =>
                {
#if SIM
                    return 3000 - JIGCode_ROI_POS_Y.GetValue<int>();
#else
                    return FA.VISION.FINE_CAM.GetImageHeight() - JIGCode_ROI_POS_Y.GetValue<int>();
#endif
                },
                SetButtonFunc = () =>
                {
                    if (MGR.dicFrmVisions["FINE"].IsRoiBoxVisble)
                    {
                        Rectangle rect = MGR.dicFrmVisions["FINE"].rectRoiBox;
                        JIGCode_ROI_POS_X.SettingValue = rect.X;
                        JIGCode_ROI_POS_Y.SettingValue = rect.Y;
                        JIGCode_ROI_POS_WIDTH.SettingValue = rect.Width;
                        return rect.Height;
                    }
                    else
                    {
                        return JIGCode_ROI_POS_HEIGHT.GetValue<int>();
                    }
                },
            };
            GUIDE_BAR_MATCH_IMG_IDX = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Guide Bar Match Image IDX", iStartIDX++, EzInaVision.GDV.eGoldenImages.Fiducial_No1, FA.DEF.eUnit.none)
            {

            };
            GUIDE_BAR_MATCH_FIND_ROI_IDX = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_PARAM.ToString(), "Guide Bar Match Find ROI IDX", iStartIDX++, EzInaVision.GDV.eRoiItems.ROI_No0, FA.DEF.eUnit.none)
            {

            };

        }
        /// <summary>
        /// double  0~10  ,1 = 100%
        /// </summary>
        public static MF.RecipeItem Matcher_Minimum_Scale;
        /// <summary> 
        /// double 0~10 , 1 = 100%
        /// </summary>
        public static MF.RecipeItem Matcher_Maximum_Scale;
        /// <summary>
        /// double ,1 = 100%
        /// </summary>
        public static MF.RecipeItem Matcher_Match_Score;
        /// <summary>
        /// double 
        /// </summary>
        public static MF.RecipeItem Matcher_Match_Angle;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem Matcher_Match_MaxCount;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem Matcher_Match_CorrelationMode;
        /// <summary>
        /// int 
        // </summary>
        public static MF.RecipeItem Matcher_Match_ContrastMode;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem LIGHT_Source_Lvl_L;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem LIGHT_Source_Lvl_R;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem LIGHT_Source_Lvl_U;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem LIGHT_Source_Lvl_B;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem Inspection_RowCount;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem Inspection_ColCount;
        /// <summary>
        /// EzInaVision.GDV.eGoldenImages
        /// </summary>
        public static MF.RecipeItem MATCH_IMG_IDX;
        /// <summary>
        /// EzInaVision.GDV.eRoiItems
        /// </summary>
        public static MF.RecipeItem MATCH_FIND_ROI_IDX;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem INSP_1st_Pixel_Center_X;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem INSP_1st_Pixel_Center_Y;
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem INSP_Pixel_Error_Range;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem JIGCode_ROI_POS_X;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem JIGCode_ROI_POS_Y;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem JIGCode_ROI_POS_WIDTH;
        /// <summary>
        /// int
        /// </summary>
        public static MF.RecipeItem JIGCode_ROI_POS_HEIGHT;

        public static MF.RecipeItem GUIDE_BAR_MATCH_IMG_IDX;
        /// <summary>
        /// EzInaVision.GDV.eRoiItems
        /// </summary>
        public static MF.RecipeItem GUIDE_BAR_MATCH_FIND_ROI_IDX;
        /*
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem FIND_ROI_NO1_X = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_ROI.ToString(), "FindROI_X", 800, 0, FA.DEF.eUnit.pxl)
        {
                fMinDelegate = () =>
                {
                        return 0;
                },
                fMaxDelegate = () =>
                {
                        return 10000000;
                },
                SetButtonFunc = () =>
                    {
                            return FA.MGR.dicFrmVisions["FINE"].IsRoiBoxVisble == true ?
                            FA.MGR.dicFrmVisions["FINE"].rectRoiBox.X : 0;
                    }

        };
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem FIND_ROI_NO1_Y = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_ROI.ToString(), "FindROI_Y", 801, 0, FA.DEF.eUnit.pxl)
        {
                fMinDelegate = () =>
                {
                        return 0;
                },
                fMaxDelegate = () =>
                {
                        return 10000000;
                },
                SetButtonFunc = () =>
                {
                        return FA.MGR.dicFrmVisions["FINE"].IsRoiBoxVisble == true ?
                        FA.MGR.dicFrmVisions["FINE"].rectRoiBox.Y : 0;
                }
        };
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem FIND_ROI_NO1_WIDTH = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_ROI.ToString(), "FindROI_W", 802, 0, FA.DEF.eUnit.pxl)
        {
                fMinDelegate = () =>
                {
                        return 0;
                },
                fMaxDelegate = () =>
                {
                        return 10000000;
                },
                SetButtonFunc = () =>
                {
                        return FA.MGR.dicFrmVisions["FINE"].IsRoiBoxVisble == true ?
                        FA.MGR.dicFrmVisions["FINE"].rectRoiBox.Width : 0;
                }
        };
        /// <summary>
        /// int 
        /// </summary>
        public static MF.RecipeItem FIND_ROI_NO1_HEIGHT = new MF.RecipeItem(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_ROI.ToString(), "FindROI_H", 803, 0, FA.DEF.eUnit.pxl)
        {
                fMinDelegate = () =>
                {
                        return 0;
                },
                fMaxDelegate = () =>
                {
                        return 10000000;
                },
                SetButtonFunc = () =>
                {
                        return FA.MGR.dicFrmVisions["FINE"].IsRoiBoxVisble == true ?
                        FA.MGR.dicFrmVisions["FINE"].rectRoiBox.Height : 0;
                }
        };
        */
        /// <summary>
        /// ROI Group 
        /// </summary>
        /*public static MF.RecipeItemGroup FIND_ROI = new MF.RecipeItemGroup(FA.DEF.eRecipeCategory.VISION, FA.DEF.eRcpSubCategory.M08_VISION_ROI.ToString(),
                                                                                                                                            FIND_ROI_NO1_X,
                                                                                                                                            FIND_ROI_NO1_Y,
                                                                                                                                            FIND_ROI_NO1_WIDTH,
                                                                                                                                            FIND_ROI_NO1_HEIGHT
                                                                                                                                            );*/

        #endregion VISION
    }
}
