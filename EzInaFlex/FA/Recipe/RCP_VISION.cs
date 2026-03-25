using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    // ----------------------------------------------------------------------------------------------------------
    // VISION 0 ~ 999  
    // ----------------------------------------------------------------------------------------------------------
    public static partial class RCP
    {
        public static void Init()
        {
            InitForIntialProess();
						InitForVision();
						InitForMotion();
        }

				#region [ Vision]
				private static void InitForVision()
				{
						F_Matcher_Minimum_Scale						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 200, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Minimum Scale"						, "0.8"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Maximum_Scale						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 201, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Maximum Scale"						, "1.2"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Match_Score							= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 202, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Score"							, "85"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Match_Angle							= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 203, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Angle"							, "10"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.deg, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Match_MaxCount					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 204, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Max count"					, "10"	,typeof(int)		,"{0}"	 , FA.DEF.eUnit.ea, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Match_CorrelationMode		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 205, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Correlation Mode"	, "1"		,typeof(int)		,"{0}"	 , FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Matcher_Match_ContrastMode			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 206, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match ContrastMode"			, "1"		,typeof(int)		,"{0}"	 , FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);

						C_Matcher_Minimum_Scale						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 210, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Minimum Scale"					, "0.8"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Maximum_Scale						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 211, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Maximum Scale"					, "1.2"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Match_Score							= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 212, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Match Score"						, "85"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Match_Angle							= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 213, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Match Angle"						, "10"	,typeof(double)	,"{0:F4}", FA.DEF.eUnit.deg, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Match_MaxCount					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 214, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Match Max count"				, "10"	,typeof(int)		,"{0}"	 , FA.DEF.eUnit.ea, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Match_CorrelationMode		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 215, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Match Correlation Mode", "1"		,typeof(int)		,"{0}"	 , FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Matcher_Match_ContrastMode			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 216, FA.DEF.eRcpSubCategory.M10_Coarse_Matcher.ToString(), "Matcher Match ContrastMode"		, "1"		,typeof(int)		,"{0}"	 , FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);



						F_Filter_ThresHoldLevel		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 220, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters ThresHold Lvl", "100"	,typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Filter_OpenDisk					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 221, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Open Disk"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Filter_CloseDisk				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 222, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Close Disk"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Filter_ErodeDisk				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 223, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Erode Disk"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Filter_Dilate						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 224, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Dilate Disk"	, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						
						C_Filter_ThresHoldLevel		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 250, FA.DEF.eRcpSubCategory.M10_Coarse_Filter.ToString(), "Filters ThresHold Lvl"	, "100"	,typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Filter_OpenDisk					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 251, FA.DEF.eRcpSubCategory.M10_Coarse_Filter.ToString(), "Filters Open Disk"			, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Filter_CloseDisk				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 252, FA.DEF.eRcpSubCategory.M10_Coarse_Filter.ToString(), "Filters Close Disk"			, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Filter_ErodeDisk				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 253, FA.DEF.eRcpSubCategory.M10_Coarse_Filter.ToString(), "Filters Erode Disk"			, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Filter_Dilate						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 254, FA.DEF.eRcpSubCategory.M10_Coarse_Filter.ToString(), "Filters Dilate Disk"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);

						F_Blob_Method						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 260, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Method"						, "100"	,typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_GrayMinValue			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 261, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Gray Min Value"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_GrayMaxValue			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 262, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Gray Max Value"		, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_MinWidth					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 263, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MinWidth"					, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_MaxWidth					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 264, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MaxWidth"					, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_MinHeight				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 265, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MinHeight"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_MaxHeight				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 266, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MaxHeight"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_AeraMin					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 267, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs AreaMin"					, "5"		,typeof(int),"{0}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Blob_AeraMax					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 268, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs AreaMax"					, "5"		,typeof(int),"{0}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_Method						= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 270, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs Method"					, "100"	,typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_GrayMinValue			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 271, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs Gray Min Value"	, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_GrayMaxValue			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 272, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs Gray Max Value"	, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_MinWidth					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 273, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs MinWidth"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_MaxWidth					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 274, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs MaxWidth"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_MinHeight				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 275, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs MinHeight"			, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_MaxHeight				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 276, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs MaxHeight"			, "5"		,typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_AeraMin					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 277, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs AreaMin"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_Blob_AeraMax					= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 278, FA.DEF.eRcpSubCategory.M10_Coarse_Blob.ToString(), "Blobs AreaMax"				, "5"		,typeof(int),"{0}", FA.DEF.eUnit.percent, false, (int)FA.DEF.eAxesName.None, false, false, false);

						F_ROI_NO_FiducialMarker = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 280, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Fiducial Marker", "0",typeof(int),"{0}", FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);												
						C_ROI_NO_FiducialMarker = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 290, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Fiducial Marker", "0",typeof(int),"{0}", FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);
																		
						F_CAM_ExposeTime = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 300, FA.DEF.eRcpSubCategory.M10_Fine_ExposeTime.ToString(), "Expose Time", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);												
						C_CAM_ExposeTime = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 310, FA.DEF.eRcpSubCategory.M10_Coarse_ExposeTime.ToString(), "Expose Time", "0",typeof(int),"{0}", FA.DEF.eUnit.msec, false, (int)FA.DEF.eAxesName.None, false, false, false);


						F_LIGHT_Source_Ring_For_Run = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 320, FA.DEF.eRcpSubCategory.M10_Fine_LightSource.ToString(), "RUN", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_LIGHT_Source_Spot_For_Run = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 321, FA.DEF.eRcpSubCategory.M10_Fine_LightSource.ToString(), "RUN", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						
						C_LIGHT_Source_Ring_For_Run = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 330, FA.DEF.eRcpSubCategory.M10_Coarse_LightSource.ToString(), "RUN", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_LIGHT_Source_Spot_For_Run = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 331, FA.DEF.eRcpSubCategory.M10_Coarse_LightSource.ToString(), "RUN", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						
																		
						F_LIGHT_Source_Ring_For_AF = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 340, FA.DEF.eRcpSubCategory.M10_Fine_LightSource.ToString(), "AF", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_LIGHT_Source_Spot_For_AF = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 341, FA.DEF.eRcpSubCategory.M10_Fine_LightSource.ToString(), "AF", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						
						C_LIGHT_Source_Ring_For_AF = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 350, FA.DEF.eRcpSubCategory.M10_Coarse_LightSource.ToString(), "AF", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_LIGHT_Source_Spot_For_AF = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 351, FA.DEF.eRcpSubCategory.M10_Coarse_LightSource.ToString(), "AF", "0",typeof(int),"{0}", FA.DEF.eUnit.lvl, false, (int)FA.DEF.eAxesName.None, false, false, false);



						F_ROI_NO1_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 400, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_01", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO1_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 401, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_01", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO1_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 402, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_01", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO1_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 403, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_01", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO2_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 404, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_02", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO2_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 405, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_02", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO2_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 406, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_02", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO2_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 407, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_02", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO3_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 408, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_03", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO3_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 409, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_03", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO3_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 410, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_03", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO3_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 411, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_03", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO4_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 412, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_04", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO4_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 413, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_04", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO4_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 414, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_04", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO4_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 415, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_04", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO5_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 416, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_05", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO5_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 417, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_05", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO5_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 418, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_05", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO5_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 419, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_05", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO6_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 420, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_06", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO6_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 421, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_06", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO6_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 422, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_06", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO6_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 423, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_06", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO7_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 424, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_07", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO7_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 425, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_07", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO7_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 426, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_07", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO7_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 427, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_07", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO8_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 428, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_08", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO8_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 429, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_08", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO8_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 430, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_08", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO8_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 431, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_08", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO9_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 432, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_09", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO9_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 433, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_09", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO9_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 434, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_09", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO9_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 435, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_09", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO10_X			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 436, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_10", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO10_Y			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 437, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_10", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_ROI_NO10_WIDTH	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 438, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_10", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);

						F_ROI_NO10_HEIGHT = new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 439, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_10", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);

						C_ROI_NO1_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 500, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO1_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 501, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO1_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 502, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO1_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 503, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO2_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 504, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO2_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 505, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO2_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 506, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO2_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 507, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO3_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 508, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO3_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 509, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO3_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 510, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO3_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 511, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO4_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 512, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO4_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 513, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO4_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 514, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO4_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 515, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO5_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 516, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO5_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 517, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO5_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 518, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO5_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 519, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO6_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 520, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO6_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 521, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO6_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 522, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO6_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 523, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO7_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 524, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO7_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 525, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO7_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 526, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO7_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 527, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO8_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 528, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO8_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 529, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO8_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 530, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO8_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 531, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO9_X				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 532, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO9_Y				= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 533, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO9_WIDTH		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 534, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO9_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 535, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO10_X			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 536, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "X", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO10_Y			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 537, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "Y", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO10_WIDTH	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 538, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "W", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						C_ROI_NO10_HEIGHT	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 539, FA.DEF.eRcpSubCategory.M10_Coarse_Roi.ToString(), "H", "0",typeof(int),"{0}", FA.DEF.eUnit.pxl, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_AutoFocus_Roi_No		= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 550, FA.DEF.eRcpSubCategory.M10_AF.ToString(), "Searching ROI NO."	, "1"		,typeof(int),"{0}", FA.DEF.eUnit.idx, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_AutoFocus_Range			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 551, FA.DEF.eRcpSubCategory.M10_AF.ToString(), "Searching Range"		, "0.2"	,typeof(double),"{0}", FA.DEF.eUnit.mm, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Autofocus_Velocity	= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 552, FA.DEF.eRcpSubCategory.M10_AF.ToString(), "Searching Velocity", "1.0"	,typeof(double),"{0}", FA.DEF.eUnit.mmPerSec, false, (int)FA.DEF.eAxesName.None, false, false, false);
						F_Autofocus_Accel			= new MF.RcpItem(FA.DEF.eRcpCategory.Vision, 553, FA.DEF.eRcpSubCategory.M10_AF.ToString(), "Searching Accel"		, "10.0",typeof(double),"{0}", FA.DEF.eUnit.mmPerSecSquared, false, (int)FA.DEF.eAxesName.None, false, false, false);
				}

        //Fine Vision
        public static MF.RcpItem F_Matcher_Minimum_Scale				;
        public static MF.RcpItem F_Matcher_Maximum_Scale				;
        public static MF.RcpItem F_Matcher_Match_Score					;
        public static MF.RcpItem F_Matcher_Match_Angle					;
        public static MF.RcpItem F_Matcher_Match_MaxCount				;
        public static MF.RcpItem F_Matcher_Match_CorrelationMode;
        public static MF.RcpItem F_Matcher_Match_ContrastMode		;

        //Coarse Vision																													
        public static MF.RcpItem C_Matcher_Minimum_Scale					;
        public static MF.RcpItem C_Matcher_Maximum_Scale					;
        public static MF.RcpItem C_Matcher_Match_Score						;
        public static MF.RcpItem C_Matcher_Match_Angle						;
        public static MF.RcpItem C_Matcher_Match_MaxCount					;
        public static MF.RcpItem C_Matcher_Match_CorrelationMode	;
        public static MF.RcpItem C_Matcher_Match_ContrastMode			;
        //Filters
        //Fine Vision
        public static MF.RcpItem F_Filter_ThresHoldLevel		;
        public static MF.RcpItem F_Filter_OpenDisk					;
        public static MF.RcpItem F_Filter_CloseDisk					;
        public static MF.RcpItem F_Filter_ErodeDisk					;
        public static MF.RcpItem F_Filter_Dilate						;
																														
        public static MF.RcpItem C_Filter_ThresHoldLevel		;
        public static MF.RcpItem C_Filter_OpenDisk					;
        public static MF.RcpItem C_Filter_CloseDisk					;
        public static MF.RcpItem C_Filter_ErodeDisk					;
        public static MF.RcpItem C_Filter_Dilate						;

        //Blobs
        //Fine Vision
        public static MF.RcpItem F_Blob_Method			;
        public static MF.RcpItem F_Blob_GrayMinValue;
        public static MF.RcpItem F_Blob_GrayMaxValue;
        public static MF.RcpItem F_Blob_MinWidth		;
        public static MF.RcpItem F_Blob_MaxWidth		;
        public static MF.RcpItem F_Blob_MinHeight		;
        public static MF.RcpItem F_Blob_MaxHeight		;
        public static MF.RcpItem F_Blob_AeraMin			;
        public static MF.RcpItem F_Blob_AeraMax			;

        //Coarse Vision
        public static MF.RcpItem C_Blob_Method			;
        public static MF.RcpItem C_Blob_GrayMinValue;
        public static MF.RcpItem C_Blob_GrayMaxValue;
        public static MF.RcpItem C_Blob_MinWidth		;
        public static MF.RcpItem C_Blob_MaxWidth		;
        public static MF.RcpItem C_Blob_MinHeight		;
        public static MF.RcpItem C_Blob_MaxHeight		;
        public static MF.RcpItem C_Blob_AeraMin			;
        public static MF.RcpItem C_Blob_AeraMax			;
        //ROIs
        //Fine Vision
        public static MF.RcpItem F_ROI_NO_FiducialMarker; 

        //Coarse Vision
        public static MF.RcpItem C_ROI_NO_FiducialMarker;

        //Camera Expose Time
        //Fine Vision
        public static MF.RcpItem F_CAM_ExposeTime;

        //Coarse Vision
        public static MF.RcpItem C_CAM_ExposeTime;

        //Light Source Level _ While Running
        //Fine Vision
        public static MF.RcpItem F_LIGHT_Source_Ring_For_Run;
        public static MF.RcpItem F_LIGHT_Source_Spot_For_Run;
        //Coarse Vision																			
        public static MF.RcpItem C_LIGHT_Source_Ring_For_Run;
        public static MF.RcpItem C_LIGHT_Source_Spot_For_Run;


        //Light Source Level _ While AF
        //Fine Vision
        public static MF.RcpItem F_LIGHT_Source_Ring_For_AF;
        public static MF.RcpItem F_LIGHT_Source_Spot_For_AF;
        //Coarse Vision																		 
        public static MF.RcpItem C_LIGHT_Source_Ring_For_AF;
        public static MF.RcpItem C_LIGHT_Source_Spot_For_AF;

        //ROIs of Soldering Vision
        #region [ ROIs of Fine Vision ]					 
        public static MF.RcpItem F_ROI_NO1_X			;
        public static MF.RcpItem F_ROI_NO1_Y			;
        public static MF.RcpItem F_ROI_NO1_WIDTH  ;
        public static MF.RcpItem F_ROI_NO1_HEIGHT;	
        public static MF.RcpItem F_ROI_NO2_X			;
        public static MF.RcpItem F_ROI_NO2_Y			;
        public static MF.RcpItem F_ROI_NO2_WIDTH	;
        public static MF.RcpItem F_ROI_NO2_HEIGHT;	
        public static MF.RcpItem F_ROI_NO3_X			;
        public static MF.RcpItem F_ROI_NO3_Y			;
        public static MF.RcpItem F_ROI_NO3_WIDTH	;
        public static MF.RcpItem F_ROI_NO3_HEIGHT;	
        public static MF.RcpItem F_ROI_NO4_X			;
        public static MF.RcpItem F_ROI_NO4_Y			;
        public static MF.RcpItem F_ROI_NO4_WIDTH	;
        public static MF.RcpItem F_ROI_NO4_HEIGHT;	
        public static MF.RcpItem F_ROI_NO5_X			;
        public static MF.RcpItem F_ROI_NO5_Y			;
        public static MF.RcpItem F_ROI_NO5_WIDTH	;
        public static MF.RcpItem F_ROI_NO5_HEIGHT;	
        public static MF.RcpItem F_ROI_NO6_X			;
        public static MF.RcpItem F_ROI_NO6_Y			;
        public static MF.RcpItem F_ROI_NO6_WIDTH	;
        public static MF.RcpItem F_ROI_NO6_HEIGHT;	
        public static MF.RcpItem F_ROI_NO7_X			;
        public static MF.RcpItem F_ROI_NO7_Y			;
        public static MF.RcpItem F_ROI_NO7_WIDTH	;
        public static MF.RcpItem F_ROI_NO7_HEIGHT ;	
        public static MF.RcpItem F_ROI_NO8_X			;
        public static MF.RcpItem F_ROI_NO8_Y			;
        public static MF.RcpItem F_ROI_NO8_WIDTH	;
        public static MF.RcpItem F_ROI_NO8_HEIGHT;	
        public static MF.RcpItem F_ROI_NO9_X		 ;
        public static MF.RcpItem F_ROI_NO9_Y		 ;
        public static MF.RcpItem F_ROI_NO9_WIDTH ;
        public static MF.RcpItem F_ROI_NO9_HEIGHT;	
        public static MF.RcpItem F_ROI_NO10_X		 ;
        public static MF.RcpItem F_ROI_NO10_Y		 ;
        public static MF.RcpItem F_ROI_NO10_WIDTH;	
        public static MF.RcpItem F_ROI_NO10_HEIGHT; 
        #endregion [ ROIs of Fine Vision ]

        #region [ ROIs of Coarse Vision ]
        public static MF.RcpItem C_ROI_NO1_X			;
        public static MF.RcpItem C_ROI_NO1_Y			;
        public static MF.RcpItem C_ROI_NO1_WIDTH	;
        public static MF.RcpItem C_ROI_NO1_HEIGHT	;
        public static MF.RcpItem C_ROI_NO2_X			;
        public static MF.RcpItem C_ROI_NO2_Y			;
        public static MF.RcpItem C_ROI_NO2_WIDTH	;
        public static MF.RcpItem C_ROI_NO2_HEIGHT	;
        public static MF.RcpItem C_ROI_NO3_X			;
        public static MF.RcpItem C_ROI_NO3_Y			;
        public static MF.RcpItem C_ROI_NO3_WIDTH	;
        public static MF.RcpItem C_ROI_NO3_HEIGHT	;
        public static MF.RcpItem C_ROI_NO4_X			;
        public static MF.RcpItem C_ROI_NO4_Y			;
        public static MF.RcpItem C_ROI_NO4_WIDTH	;
        public static MF.RcpItem C_ROI_NO4_HEIGHT	;
        public static MF.RcpItem C_ROI_NO5_X			;
        public static MF.RcpItem C_ROI_NO5_Y			;
        public static MF.RcpItem C_ROI_NO5_WIDTH	;
        public static MF.RcpItem C_ROI_NO5_HEIGHT	;
        public static MF.RcpItem C_ROI_NO6_X			;
        public static MF.RcpItem C_ROI_NO6_Y			;
        public static MF.RcpItem C_ROI_NO6_WIDTH	;
        public static MF.RcpItem C_ROI_NO6_HEIGHT	;
        public static MF.RcpItem C_ROI_NO7_X			;
        public static MF.RcpItem C_ROI_NO7_Y			;
        public static MF.RcpItem C_ROI_NO7_WIDTH	;
        public static MF.RcpItem C_ROI_NO7_HEIGHT	;
        public static MF.RcpItem C_ROI_NO8_X			;
        public static MF.RcpItem C_ROI_NO8_Y			;
        public static MF.RcpItem C_ROI_NO8_WIDTH	;
        public static MF.RcpItem C_ROI_NO8_HEIGHT	;
        public static MF.RcpItem C_ROI_NO9_X			;
        public static MF.RcpItem C_ROI_NO9_Y			;
        public static MF.RcpItem C_ROI_NO9_WIDTH	;
        public static MF.RcpItem C_ROI_NO9_HEIGHT	;
        public static MF.RcpItem C_ROI_NO10_X			;
        public static MF.RcpItem C_ROI_NO10_Y			;
        public static MF.RcpItem C_ROI_NO10_WIDTH	;
        public static MF.RcpItem C_ROI_NO10_HEIGHT;


        #endregion [ ROIs of Coarse Vision ]

        #region [AUTO FOCUS]
        public static MF.RcpItem F_AutoFocus_Roi_No		;
        public static MF.RcpItem F_AutoFocus_Range		;
        public static MF.RcpItem F_Autofocus_Velocity	;
        public static MF.RcpItem F_Autofocus_Accel		;
        #endregion
        #endregion


    }
}
