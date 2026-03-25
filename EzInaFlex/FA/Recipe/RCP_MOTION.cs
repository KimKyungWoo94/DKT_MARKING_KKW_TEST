using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
	// ----------------------------------------------------------------------------------------------------------
	// MOTION 2000 ~  2999
	// ----------------------------------------------------------------------------------------------------------
	
	public static partial class RCP
	{
				#region [ Motion - Stage 2000 ~ 2099]
				private static void InitForMotion()
				{
						M00_CrossHairStageXPos				= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2300, FA.DEF.eRcpSubCategory.M00_CROSSHAIR.ToString()		, "Cross Hair Stage X Pos"	, "0.0",typeof(double),"{0:F4}", FA.DEF.eUnit.mm			, false, (int)FA.DEF.eAxesName.RX	,	true	, true	, true	);
						M00_CrossHairStageYPos				= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2301, FA.DEF.eRcpSubCategory.M00_CROSSHAIR.ToString()		, "Cross Hair Stage Y Pos"	, "0.0",typeof(double),"{0:F4}", FA.DEF.eUnit.mm			, false, (int)FA.DEF.eAxesName.Y	,	true	, true	, true	);
				}


				#endregion[ Motion - Stage ]

				// 		#region [ Motion - Laser 2100 ~ 2199]
				// 		public static MF.RcpItem M00_LaserFocusZPos					= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2100, FA.DEF.eRcpSubCategory.M00_LASER.ToString()			, "Laser Z Focus Pos"	, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.RZ	,	true	, true	, true	);
				// 		public static MF.RcpItem M00_JigHeight						  = new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2101, FA.DEF.eRcpSubCategory.M00_LASER.ToString()			, "Jig Height Pos"		, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.None	,	false	, false	, false	);
				// 		public static MF.RcpItem M00_AutoFocusOffset				= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2102, FA.DEF.eRcpSubCategory.M00_LASER.ToString()			, "Auto Focus Offset"	, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.None	,	false	, false	, false	);
				// 		#endregion[ Motion - Laser ]
				// 
				// 		#region [ Motion - Laser 2200 ~ 2299]
				// 		public static MF.RcpItem M00_CoarseVisionFocusZPos		= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2200, FA.DEF.eRcpSubCategory.M00_VISION.ToString()		, "Coarse Vision Z Pos"	, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.RZ	,	true	, true	, true	);
				// 		public static MF.RcpItem M00_FineVisionFocusZPos			= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2201, FA.DEF.eRcpSubCategory.M00_VISION.ToString()		, "Fine Vision Z Pos"	, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.RZ	,	true	, true	, true	);
				// 		public static MF.RcpItem M00_VisionFocusOffset				= new MF.RcpItem(FA.DEF.eRcpCategory.Motion		, 2202, FA.DEF.eRcpSubCategory.M00_VISION.ToString()		, "Vision Focus Offset"	, "0.0", FA.DEF.eUnit.mm				, false, (int)FA.DEF.eAxesName.None	,	false	, false	, false	);
				// 		#endregion[ Motion - Laser ]

			#region [ Motion - Cross Hair 2300 ~ 2399]
			public static MF.RcpItem M00_CrossHairStageXPos;
		  public static MF.RcpItem M00_CrossHairStageYPos;
		  #endregion[ Motion - Cross Hair ]
	}
}
