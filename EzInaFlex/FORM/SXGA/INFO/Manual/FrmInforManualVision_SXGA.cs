using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{

		public partial class FrmInforManualVision_SXGA : Form
		{
				#region [ Measure distance ]
				private int m_nGrayVal = 0;
				private List<Point> m_listPtBegin = new List<Point>();
				private List<Point> m_listPtEnd = new List<Point>();
				private List<string> m_listStrDist = new List<string>();

				bool m_IsDrawing = false;
				private Point m_ptNewBegin = Point.Empty;
				private Point m_ptNewEnd = Point.Empty;
				int m_object_radius = 1;
				#endregion
				#region [Recipe Items]
				List<RcpItem> m_listRcpItems = new List<RcpItem>();
				RcpItem F_Matcher_Minimum_Scale;
				RcpItem F_Matcher_Maximum_Scale;
				RcpItem F_Matcher_Match_Score;
				RcpItem F_Matcher_Match_Angle;
				RcpItem F_Matcher_Match_MaxCount;
				RcpItem F_Matcher_Match_CorrelationMode;
				RcpItem F_Matcher_Match_ContrastMode;

				//Filters
				//Fine Vision
				RcpItem F_Filter_ThresHoldLevel;
				RcpItem F_Filter_OpenDisk;
				RcpItem F_Filter_CloseDisk;
				RcpItem F_Filter_ErodeDisk;
				RcpItem F_Filter_Dilate;

				//Blobs
				//Fine Vision
				RcpItem F_Blob_Method;
				RcpItem F_Blob_GrayMinValue;
				RcpItem F_Blob_GrayMaxValue;
				RcpItem F_Blob_MinWidth;
				RcpItem F_Blob_MaxWidth;
				RcpItem F_Blob_MinHeight;
				RcpItem F_Blob_MaxHeight;
				RcpItem F_Blob_AeraMin;
				RcpItem F_Blob_AeraMax;

				//ROIs of Soldering Vision
				RcpItem F_ROI_NO_FiducialMarker;
				RcpItem F_ROI_NO1_X;
				RcpItem F_ROI_NO1_Y;
				RcpItem F_ROI_NO1_WIDTH;
				RcpItem F_ROI_NO1_HEIGHT;
				RcpItem F_ROI_NO2_X;
				RcpItem F_ROI_NO2_Y;
				RcpItem F_ROI_NO2_WIDTH;
				RcpItem F_ROI_NO2_HEIGHT;
				RcpItem F_ROI_NO3_X;
				RcpItem F_ROI_NO3_Y;
				RcpItem F_ROI_NO3_WIDTH;
				RcpItem F_ROI_NO3_HEIGHT;
				RcpItem F_ROI_NO4_X;
				RcpItem F_ROI_NO4_Y;
				RcpItem F_ROI_NO4_WIDTH;
				RcpItem F_ROI_NO4_HEIGHT;
				RcpItem F_ROI_NO5_X;
				RcpItem F_ROI_NO5_Y;
				RcpItem F_ROI_NO5_WIDTH;
				RcpItem F_ROI_NO5_HEIGHT;
				RcpItem F_ROI_NO6_X;
				RcpItem F_ROI_NO6_Y;
				RcpItem F_ROI_NO6_WIDTH;
				RcpItem F_ROI_NO6_HEIGHT;
				RcpItem F_ROI_NO7_X;
				RcpItem F_ROI_NO7_Y;
				RcpItem F_ROI_NO7_WIDTH;
				RcpItem F_ROI_NO7_HEIGHT;
				RcpItem F_ROI_NO8_X;
				RcpItem F_ROI_NO8_Y;
				RcpItem F_ROI_NO8_WIDTH;
				RcpItem F_ROI_NO8_HEIGHT;
				RcpItem F_ROI_NO9_X;
				RcpItem F_ROI_NO9_Y;
				RcpItem F_ROI_NO9_WIDTH;
				RcpItem F_ROI_NO9_HEIGHT;
				RcpItem F_ROI_NO10_X;
				RcpItem F_ROI_NO10_Y;
				RcpItem F_ROI_NO10_WIDTH;
				RcpItem F_ROI_NO10_HEIGHT;

				RcpItem F_VisionScaleX;
				RcpItem F_VisionScaleY;
				#endregion

				EzInaVision.GDV.eRoiItems m_eRoiItem = EzInaVision.GDV.eRoiItems.None;
				EzInaVision.GDV.eGoldenImages m_eGoldenImage = EzInaVision.GDV.eGoldenImages.None;

				EzInaVision.GDV.stLibInfo m_stLibInfo;
				EzInaVisionLibrary.VisionLibEuresys m_VisionLib = null;

				System.Drawing.Imaging.PixelFormat m_ePixelFormat;

				Bitmap m_bmp = null;
				TreeNode m_SelectedTreeNode = new TreeNode();
				bool m_bImgIsOpened = false;
				System.Windows.Forms.Timer tmr = null;
				public FrmInforManualVision_SXGA()
				{
						InitializeComponent();
						imageBoxEx_VisionOfPanel.MouseDown += new MouseEventHandler(imageBox_MouseDown);
						imageBoxEx_VisionOfPanel.Paint += new PaintEventHandler(imageBox_Paint);
						m_stLibInfo = new EzInaVision.GDV.stLibInfo();
						m_stLibInfo.Clear();
						m_stLibInfo.strName = "MANUAL";
						m_VisionLib = new EzInaVisionLibrary.VisionLibEuresys(m_stLibInfo);
						foreach (EzIna.FA.DEF.eROI_CUSTOM item in Enum.GetValues(typeof(EzIna.FA.DEF.eROI_CUSTOM)))
						{
								m_VisionLib.AddRoiForInspection((int)item);
						}
						
						AddRcpItems();
				}
				private void FrmInforManualVision_SXGA_Load(object sender, EventArgs e)
				{
						//label_VisionInfor.BackColor = Color.Transparent;
						//label_VisionInfor.Parent = imageBoxEx_VisionOfPanel;

						// apply a minimum selection size for resize operations
						imageBoxEx_VisionOfPanel.MinimumSelectionSize = new Size(8, 8);
						imageBoxEx_VisionOfPanel.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.None;

						treeView_GoldenImageList.DoubleBuffered(true);
						treeView_Menu.DoubleBuffered(true);

						#region [Add menu of Golden Images.]
						toolStripSplitButtonGoldenImages.DropDownItems.Clear();
						ToolStripMenuItem[] arrToolStripMenuItemsGoldenImage = null;
						arrToolStripMenuItemsGoldenImage = new ToolStripMenuItem[(int)EzInaVision.GDV.eGoldenImages.Max];

						string strName = "";
						string strText = "";
						int iGoldenImages = 0;
						for (int i = 0; i < (int)EzInaVision.GDV.eGoldenImages.Max; i++)
						{
								strName = string.Format("toolStripMenuItemGoldenImages{0}", i);

								EzInaVision.GDV.eGoldenImages eGoldenImage = (EzInaVision.GDV.eGoldenImages)i;
								strText = eGoldenImage.ToString("G");
								iGoldenImages = i;

								arrToolStripMenuItemsGoldenImage[i] = new ToolStripMenuItem();
								arrToolStripMenuItemsGoldenImage[i].Name = strName;
								arrToolStripMenuItemsGoldenImage[i].Size = new Size(100, 22);
								arrToolStripMenuItemsGoldenImage[i].Text = strText;
								arrToolStripMenuItemsGoldenImage[i].Tag = Convert.ToString(iGoldenImages);
								arrToolStripMenuItemsGoldenImage[i].Click += new EventHandler(OnMenuOfGoldenImages);
								toolStripSplitButtonGoldenImages.DropDownItems.Add(arrToolStripMenuItemsGoldenImage[i]);
						}

						//seperator
						toolStripSplitButtonGoldenImages.DropDownItems.Add(new ToolStripSeparator());

						ToolStripMenuItem MenuofGoldenImages = null;
						MenuofGoldenImages = new ToolStripMenuItem();
						strName = "MenuOfGoldenImages";
						strText = "Save";

						MenuofGoldenImages = new ToolStripMenuItem();
						MenuofGoldenImages.Name = strName;
						MenuofGoldenImages.Size = new Size(100, 22);
						MenuofGoldenImages.Text = strText;
						MenuofGoldenImages.Tag = 0;
						MenuofGoldenImages.Click += new EventHandler(OnMenuOfGoldenImagesSave);
						toolStripSplitButtonGoldenImages.DropDownItems.Add(MenuofGoldenImages);
						#endregion
						#region [Add menu of Rois]

						toolStripSplitButtonSelectRoi.DropDownItems.Clear();
						ToolStripMenuItem[] arrToolStripMenuItems = null;
						arrToolStripMenuItems = new ToolStripMenuItem[(int)EzInaVision.GDV.eRoiItems.Max];

						strName = "";
						strText = "";

						int iRoi = 0;
						for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
						{
								strName = string.Format("toolStripMenuItemRoi{0}", i);
								strText = string.Format("[ROI]:{0:D2}", i);
								iRoi = i;

								arrToolStripMenuItems[i] = new ToolStripMenuItem();
								arrToolStripMenuItems[i].Name = strName;
								arrToolStripMenuItems[i].Size = new Size(100, 22);
								arrToolStripMenuItems[i].Text = strText;
								arrToolStripMenuItems[i].Tag = Convert.ToString(iRoi);
								arrToolStripMenuItems[i].Click += new EventHandler(OnToolStripMenuItems);
								toolStripSplitButtonSelectRoi.DropDownItems.Add(arrToolStripMenuItems[i]);
						}
						//---------------------------------------------------
						toolStripSplitButtonSelectRoi.DropDownItems.Add(new ToolStripSeparator());

						ToolStripMenuItem arrToolStripMenuItemForSaving = null;
						arrToolStripMenuItemForSaving = new ToolStripMenuItem();
						strName = "toolStripMenuItemSaveSelected";
						strText = "Save selected ROI";

						arrToolStripMenuItemForSaving = new ToolStripMenuItem();
						arrToolStripMenuItemForSaving.Name = strName;
						arrToolStripMenuItemForSaving.Size = new Size(100, 22);
						arrToolStripMenuItemForSaving.Text = strText;
						arrToolStripMenuItemForSaving.Tag = 0;
						arrToolStripMenuItemForSaving.Click += new EventHandler(OnToolStripMenuItemsSave);
						toolStripSplitButtonSelectRoi.DropDownItems.Add(arrToolStripMenuItemForSaving);
						#endregion
						#region [Timer for display]
						tmr = new System.Windows.Forms.Timer();
						tmr.Interval = 50;
						tmr.Tick += new EventHandler(Display);
						tmr.Start();
						#endregion
						#region [checkbox option]
						chkbox_DispRois.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkBox_BoxOfRoi.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_CrossLine.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_MatchResult.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_BlobResult.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_CalibrationResult.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_FiiltersDisplay.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_Threshold.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_Morphology_Open.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_Morphology_Close.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_Morphology_Erode.CheckedChanged += new EventHandler(OnCheckedChanged);
						chkbox_Morphology_Dilate.CheckedChanged += new EventHandler(OnCheckedChanged);
						#endregion
						#region [TrackBar]
						SetTrackbar();
						trackBar_Threshold.ValueChanged += new EventHandler(OnTrackBar_ValueChanged);
						trackBar_Open.ValueChanged += new EventHandler(OnTrackBar_ValueChanged);
						trackBar_Close.ValueChanged += new EventHandler(OnTrackBar_ValueChanged);
						trackBar_Dilate.ValueChanged += new EventHandler(OnTrackBar_ValueChanged);
						trackBar_Erode.ValueChanged += new EventHandler(OnTrackBar_ValueChanged);
						#endregion
						#region [Set Grid]
						TreeView_Init(treeView_Menu, imageList_For_TreeMenu);
						SelectNode(FA.DEF.eRcpSubCategory.M10_Fine_Matcher);

						#endregion

				}
				private void FrmInforManualVision_SXGA_VisibleChanged(object sender, EventArgs e)
				{
						if (Visible)
						{
								tmr.Start();

								#region [ Tree Initialize ]
								if (m_VisionLib != null)
								{
										FA.MGR.VisionMgr.TreeView_Init(treeView_GoldenImageList);
										SelectNode(FA.DEF.eRcpSubCategory.M10_Fine_Matcher);
										SelectNode(EzInaVision.GDV.eGoldenImages.Fiducial_No1);

										DoLoad();
										WriteDatas_To_Vision();
										Write_To(FA.DEF.eRcpSubCategory.M10_Fine_Matcher, dataGridView_parameters);

								}
								#endregion [ Tree Initialize ]
						}
						else
						{
								tmr.Stop();
						}
				}
				private void AddRcpItems()
				{
						if (m_listRcpItems == null)
								return;
						F_Matcher_Minimum_Scale = new RcpItem(FA.DEF.eRcpCategory.Vision, 200, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Minimum Scale", "0.8", FA.DEF.eUnit.percent); m_listRcpItems.Add(F_Matcher_Minimum_Scale);
						F_Matcher_Maximum_Scale = new RcpItem(FA.DEF.eRcpCategory.Vision, 201, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Maximum Scale", "1.2", FA.DEF.eUnit.percent); m_listRcpItems.Add(F_Matcher_Maximum_Scale);
						F_Matcher_Match_Score = new RcpItem(FA.DEF.eRcpCategory.Vision, 202, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Score", "85", FA.DEF.eUnit.percent); m_listRcpItems.Add(F_Matcher_Match_Score);
						F_Matcher_Match_Angle = new RcpItem(FA.DEF.eRcpCategory.Vision, 203, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Angle", "10", FA.DEF.eUnit.deg); m_listRcpItems.Add(F_Matcher_Match_Angle);
						F_Matcher_Match_MaxCount = new RcpItem(FA.DEF.eRcpCategory.Vision, 204, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Max count", "10", FA.DEF.eUnit.ea); m_listRcpItems.Add(F_Matcher_Match_MaxCount);
						F_Matcher_Match_CorrelationMode = new RcpItem(FA.DEF.eRcpCategory.Vision, 205, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match Correlation Mode", "1", FA.DEF.eUnit.idx); m_listRcpItems.Add(F_Matcher_Match_CorrelationMode);
						F_Matcher_Match_ContrastMode = new RcpItem(FA.DEF.eRcpCategory.Vision, 206, FA.DEF.eRcpSubCategory.M10_Fine_Matcher.ToString(), "Matcher Match ContrastMode", "1", FA.DEF.eUnit.idx); m_listRcpItems.Add(F_Matcher_Match_ContrastMode);
						//Filters
						//Fine Vision
						F_Filter_ThresHoldLevel = new RcpItem(FA.DEF.eRcpCategory.Vision, 220, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters ThresHold Lvl", "100", FA.DEF.eUnit.lvl); m_listRcpItems.Add(F_Filter_ThresHoldLevel);
						F_Filter_OpenDisk = new RcpItem(FA.DEF.eRcpCategory.Vision, 221, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Open Disk", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Filter_OpenDisk);
						F_Filter_CloseDisk = new RcpItem(FA.DEF.eRcpCategory.Vision, 222, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Close Disk", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Filter_CloseDisk);
						F_Filter_ErodeDisk = new RcpItem(FA.DEF.eRcpCategory.Vision, 223, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Erode Disk", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Filter_ErodeDisk);
						F_Filter_Dilate = new RcpItem(FA.DEF.eRcpCategory.Vision, 224, FA.DEF.eRcpSubCategory.M10_Fine_Filter.ToString(), "Filters Dilate Disk", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Filter_Dilate);
						//Blobs
						//Fine Vision
						F_Blob_Method = new RcpItem(FA.DEF.eRcpCategory.Vision, 260, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Method", "100", FA.DEF.eUnit.lvl); m_listRcpItems.Add(F_Blob_Method);
						F_Blob_GrayMinValue = new RcpItem(FA.DEF.eRcpCategory.Vision, 261, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Gray Min Value", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_GrayMinValue);
						F_Blob_GrayMaxValue = new RcpItem(FA.DEF.eRcpCategory.Vision, 262, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs Gray Max Value", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_GrayMaxValue);
						F_Blob_MinWidth = new RcpItem(FA.DEF.eRcpCategory.Vision, 263, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MinWidth", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_MinWidth);
						F_Blob_MaxWidth = new RcpItem(FA.DEF.eRcpCategory.Vision, 264, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MaxWidth", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_MaxWidth);
						F_Blob_MinHeight = new RcpItem(FA.DEF.eRcpCategory.Vision, 265, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MinHeight", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_MinHeight);
						F_Blob_MaxHeight = new RcpItem(FA.DEF.eRcpCategory.Vision, 266, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs MaxHeight", "5", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_Blob_MaxHeight);
						F_Blob_AeraMin = new RcpItem(FA.DEF.eRcpCategory.Vision, 267, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs AreaMin", "5", FA.DEF.eUnit.percent); m_listRcpItems.Add(F_Blob_AeraMin);
						F_Blob_AeraMax = new RcpItem(FA.DEF.eRcpCategory.Vision, 268, FA.DEF.eRcpSubCategory.M10_Fine_Blob.ToString(), "Blobs AreaMax", "5", FA.DEF.eUnit.percent); m_listRcpItems.Add(F_Blob_AeraMax);
						//ROIs of Soldering Vision
						#region [ ROIs of Fine Vision ]
						F_ROI_NO_FiducialMarker = new RcpItem(FA.DEF.eRcpCategory.Vision, 400, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "NO_FiducialMarker", "0", FA.DEF.eUnit.idx); m_listRcpItems.Add(F_ROI_NO_FiducialMarker);
						F_ROI_NO1_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 401, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_01", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO1_X);
						F_ROI_NO1_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 402, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_01", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO1_Y);
						F_ROI_NO1_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 403, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_01", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO1_WIDTH);
						F_ROI_NO1_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 404, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_01", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO1_HEIGHT);
						F_ROI_NO2_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 405, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_02", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO2_X);
						F_ROI_NO2_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 406, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_02", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO2_Y);
						F_ROI_NO2_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 407, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_02", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO2_WIDTH);
						F_ROI_NO2_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 408, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_02", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO2_HEIGHT);
						F_ROI_NO3_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 409, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_03", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO3_X);
						F_ROI_NO3_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 410, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_03", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO3_Y);
						F_ROI_NO3_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 411, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_03", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO3_WIDTH);
						F_ROI_NO3_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 412, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_03", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO3_HEIGHT);
						F_ROI_NO4_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 413, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_04", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO4_X);
						F_ROI_NO4_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 414, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_04", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO4_Y);
						F_ROI_NO4_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 415, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_04", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO4_WIDTH);
						F_ROI_NO4_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 416, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_04", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO4_HEIGHT);
						F_ROI_NO5_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 417, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_05", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO5_X);
						F_ROI_NO5_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 418, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_05", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO5_Y);
						F_ROI_NO5_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 419, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_05", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO5_WIDTH);
						F_ROI_NO5_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 420, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_05", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO5_HEIGHT);
						F_ROI_NO6_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 421, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_06", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO6_X);
						F_ROI_NO6_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 422, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_06", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO6_Y);
						F_ROI_NO6_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 423, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_06", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO6_WIDTH);
						F_ROI_NO6_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 424, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_06", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO6_HEIGHT);
						F_ROI_NO7_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 425, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_07", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO7_X);
						F_ROI_NO7_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 426, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_07", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO7_Y);
						F_ROI_NO7_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 427, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_07", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO7_WIDTH);
						F_ROI_NO7_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 428, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_07", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO7_HEIGHT);
						F_ROI_NO8_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 429, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_08", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO8_X);
						F_ROI_NO8_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 430, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_08", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO8_Y);
						F_ROI_NO8_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 431, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_08", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO8_WIDTH);
						F_ROI_NO8_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 432, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_08", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO8_HEIGHT);
						F_ROI_NO9_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 433, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_09", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO9_X);
						F_ROI_NO9_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 434, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_09", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO9_Y);
						F_ROI_NO9_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 435, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_09", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO9_WIDTH);
						F_ROI_NO9_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 436, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_09", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO9_HEIGHT);
						F_ROI_NO10_X = new RcpItem(FA.DEF.eRcpCategory.Vision, 437, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "X_10", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO10_X);
						F_ROI_NO10_Y = new RcpItem(FA.DEF.eRcpCategory.Vision, 438, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "Y_10", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO10_Y);
						F_ROI_NO10_WIDTH = new RcpItem(FA.DEF.eRcpCategory.Vision, 439, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "W_10", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO10_WIDTH);
						F_ROI_NO10_HEIGHT = new RcpItem(FA.DEF.eRcpCategory.Vision, 440, FA.DEF.eRcpSubCategory.M10_Fine_Roi.ToString(), "H_10", "0", FA.DEF.eUnit.pxl); m_listRcpItems.Add(F_ROI_NO10_HEIGHT);
						#endregion [ ROIs of Fine Vision ]
						F_VisionScaleX = new RcpItem(FA.DEF.eRcpCategory.Vision, 351, FA.DEF.eRcpSubCategory.M10_Fine_VisionScale.ToString(), "X", "0", FA.DEF.eUnit.pxlPerMicro); m_listRcpItems.Add(F_VisionScaleX);
						F_VisionScaleY = new RcpItem(FA.DEF.eRcpCategory.Vision, 352, FA.DEF.eRcpSubCategory.M10_Fine_VisionScale.ToString(), "Y", "0", FA.DEF.eUnit.pxlPerMicro); m_listRcpItems.Add(F_VisionScaleY);

				}
				private bool DuplicateCheck()
				{
						for (int i = 0; i < m_listRcpItems.Count; i++)
						{
								var list = m_listRcpItems.FindAll(item => item.iKey == m_listRcpItems[i].iKey);
								if (list.Count > 1)
										return false;
						}

						return true;
				}
				#region [Event - ToolStrip]
				/// <summary>
				/// ROI 저장.
				/// </summary>
				/// <param name="sender"></param>
				/// <param name="e"></param>
				private void OnToolStripMenuItemsSave(object sender, EventArgs e)
				{
						try
						{
								if (!m_bImgIsOpened)
										throw new Exception("The image is not opened");
								if (m_eRoiItem == EzInaVision.GDV.eRoiItems.None || m_eRoiItem == EzInaVision.GDV.eRoiItems.Max)
										throw new Exception("Is not selected ROI");

								if (!chkBox_BoxOfRoi.Checked)
										throw new Exception("Is not checked \"Box of Roi \"");

								if (imageBoxEx_VisionOfPanel.SelectionRegion == RectangleF.Empty)
										throw new Exception("Section size is empty");



								if (EzInaVision.MsgBox.Confirm(string.Format("{0} {1}??", "Would you like to save ", m_eRoiItem.ToString("G"))))
								{
										imageBoxEx_VisionOfPanel.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
										//Vision Module
										m_VisionLib.SetRoiForInspection((int)m_eRoiItem,
												Rectangle.Round(imageBoxEx_VisionOfPanel.SelectionRegion));

										ReadDatas_From_Vision();
										DoSave();
								}
						}
						catch (Exception ex)
						{
								EzInaVision.MsgBox.Error(ex.Message);
						}
				}
				private void OnMenuOfGoldenImagesSave(object sender, EventArgs e)
				{
						try
						{
								if (!m_bImgIsOpened)
										throw new Exception("The image is not opened");

								if (m_eGoldenImage == EzInaVision.GDV.eGoldenImages.None || m_eGoldenImage == EzInaVision.GDV.eGoldenImages.Max)
										throw new Exception("It is not selected Golden image");

								if (!chkBox_BoxOfRoi.Checked)
										throw new Exception("It is not checked \"Box of Roi \"");

								if (imageBoxEx_VisionOfPanel.SelectionRegion == RectangleF.Empty)
										throw new Exception("The region size is empty");


								if (EzInaVision.MsgBox.Confirm("Would you like to save this \"{0}\" ??", m_eGoldenImage.ToString("G")))
								{
										m_VisionLib.SavePattern(FA.DIR.MANUAL_VISION
																	 , "MANUAL"
																	 , m_eGoldenImage.ToString()
																	 , Rectangle.Round(imageBoxEx_VisionOfPanel.SelectionRegion));

										//ex
										//Manual_Fiducial_No1.bmp
										//Manual_Ojbect_No1.bmp

										string strLoadImage = string.Format(@"{0}\{1}_{2}.bmp"
											, FA.DIR.MANUAL_VISION
											, "MANUAL"
											, m_eGoldenImage.ToString());


										btn_Unwrapping.SizeMode = PictureBoxSizeMode.Zoom;

										System.IO.FileInfo f = new System.IO.FileInfo(strLoadImage);
										if (f.Exists)
										{
												using (var fs = new System.IO.FileStream(strLoadImage, System.IO.FileMode.Open))
												{
														using (Bitmap bmp = new Bitmap(fs))
														{
																if (bmp != null)
																		btn_Unwrapping.Image = (Image)bmp.Clone();
														}
												}
										}

										SelectNode(m_eGoldenImage);
								}
						}
						catch (Exception ex)
						{
								EzInaVision.MsgBox.Error(ex.Message);
						}
				}
				private void OnMenuOfGoldenImages(object sender, EventArgs e)
				{
						try
						{
								if (!m_bImgIsOpened)
										throw new Exception("The image is not opened");


								for (int i = 0; i < (int)EzInaVision.GDV.eGoldenImages.Max; i++)
										((ToolStripMenuItem)toolStripSplitButtonGoldenImages.DropDownItems[i]).Checked = false;

								((ToolStripMenuItem)sender).Checked = true;
								m_eGoldenImage = (EzInaVision.GDV.eGoldenImages)(Convert.ToInt32((sender as ToolStripMenuItem).Tag));

								toolStripStatusLabel_SelectedGoldenImage.Text = (sender as ToolStripMenuItem).Text;

						}
						catch (Exception ex)
						{
								EzInaVision.MsgBox.Error(ex.Message);
						}
				}
				private void OnToolStripMenuItems(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
								return;


						for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
								((ToolStripMenuItem)toolStripSplitButtonSelectRoi.DropDownItems[i]).Checked = false;

						((ToolStripMenuItem)sender).Checked = true;
						m_eRoiItem = (EzInaVision.GDV.eRoiItems)(Convert.ToInt32((sender as ToolStripMenuItem).Tag));

						toolStripStatusLabel_SelectedRoi.Text = (sender as ToolStripMenuItem).Text;
				}
				private void toolStripStatusLabel_ImageOpen_Click(object sender, EventArgs e)
				{
						OpenFileDialog dlg = new OpenFileDialog();
						dlg.DefaultExt = "bmp";
						dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";//"bmp files(*.bmp)|*.bmp";

						if (dlg.ShowDialog() == DialogResult.OK)
						{
								if (m_bmp != null)
										m_bmp.Dispose();
								m_bImgIsOpened = false;

								m_bmp = new Bitmap(dlg.FileName, true);
								m_bImgIsOpened = true;
								SetBitmapToEImage();
						}
				}
				#endregion
				#region [Event - imageBoxEx]
				private void imageBoxEx_VisionOfPanel_MouseMove(object sender, MouseEventArgs e)
				{
						if (!m_bImgIsOpened)
								return;
						if (imageBoxEx_VisionOfPanel.Image == null)
								return;

						if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
								UpdateCursorPosition(e.Location);
				}
				private void imageBoxEx_VisionOfPanel_Paint(object sender, PaintEventArgs e)
				{
						if (!m_bImgIsOpened)
								return;
						if (imageBoxEx_VisionOfPanel.Image == null)
								return;


						PointF pFanXY = imageBoxEx_VisionOfPanel.GetOffsetPoint(0, 0);//.Image.Width;
						SizeF sScale = imageBoxEx_VisionOfPanel.GetScaledSize(imageBoxEx_VisionOfPanel.Image.Width, imageBoxEx_VisionOfPanel.Image.Height);
						sScale.Width /= imageBoxEx_VisionOfPanel.Image.Width;
						sScale.Height /= imageBoxEx_VisionOfPanel.Image.Height;
						pFanXY.X /= sScale.Width;
						pFanXY.Y /= sScale.Height;

						EzInaVision.GDV.stCamInfo stCamInfo = new EzInaVision.GDV.stCamInfo();
						stCamInfo.Clear();
						stCamInfo.bIdle			= true;
						stCamInfo.bGrabbed	= true;
						stCamInfo.bLive			= false;
						stCamInfo.nGrayVal	= m_nGrayVal;
						//m_VisionLib.Display(e.Graphics, imageBoxEx_VisionOfPanel.ZoomFactor, pFanXY, stCamInfo);
						m_VisionLib.Display(e.Graphics, sScale.Width, sScale.Height, pFanXY, stCamInfo);

				}
				private void imageBoxEx_VisionOfPanel_MouseWheel(object sender, MouseEventArgs e)
				{
						m_listPtBegin.Clear();
						m_listPtEnd.Clear();
						m_listStrDist.Clear();
				}
				private void imageBoxEx_VisionOfPanel_SelectionRegionChanged(object sender, EventArgs e)
				{
						try
						{
								if (!m_bImgIsOpened)
										throw new Exception("The image is not opened");
								if (imageBoxEx_VisionOfPanel.Image == null)
										throw new Exception("The image box is empty");


								toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);
						}
						catch (Exception exc)
						{
								MsgBox.Error(exc.ToString());
						}
				}
				private void imageBox_MouseDown(object sender, MouseEventArgs e)
				{
						if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
						{
								imageBoxEx_VisionOfPanel.MouseMove += imageBox_Move_Drawing;
								imageBoxEx_VisionOfPanel.MouseUp += imageBox_MouseUp_Drawing;

								m_IsDrawing = true;

								m_ptNewBegin = new Point(e.X, e.Y);
								m_ptNewEnd = new Point(e.X, e.Y);

								imageBoxEx_VisionOfPanel.Invalidate();
						}
				}
				private void imageBox_MouseUp_Drawing(object sender, MouseEventArgs e)
				{
						if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
						{
								if (e.Button == MouseButtons.Right)
								{
										imageBoxEx_VisionOfPanel.MouseMove -= imageBox_Move_Drawing;
										imageBoxEx_VisionOfPanel.MouseUp -= imageBox_MouseUp_Drawing;
										m_IsDrawing = false;
										m_listPtBegin.Add(m_ptNewBegin);
										m_listPtEnd.Add(m_ptNewEnd);

										Point ptRealBegin = new Point();
										Point ptRealEnd = new Point();

										ptRealBegin = imageBoxEx_VisionOfPanel.PointToImage(m_ptNewBegin.X, m_ptNewBegin.Y);
										ptRealEnd = imageBoxEx_VisionOfPanel.PointToImage(m_ptNewEnd.X, m_ptNewEnd.Y);

										m_listStrDist.Add
										(
											string.Format
											(
												"{0:F2}um, {1:F2}um"
												, Math.Abs(ptRealEnd.X - ptRealBegin.X) * m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleX
												, Math.Abs(ptRealEnd.Y - ptRealBegin.Y) * m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleY
											)
										);

										imageBoxEx_VisionOfPanel.Invalidate();
								}
						}
				}
				private void imageBox_Move_Drawing(object sender, MouseEventArgs e)
				{
						if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
						{
								if (e.Button == MouseButtons.Right)
								{
										m_ptNewEnd = new Point(e.X, e.Y);

										imageBoxEx_VisionOfPanel.Invalidate();
								}
						}
				}
				private void imageBox_Paint(object sender, PaintEventArgs e)
				{
						if (imageBoxEx_VisionOfPanel.Image != null)
						{
								Font f = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point);
								for (int i = 0; i < m_listPtBegin.Count; i++)
								{
										//draw the segment
										e.Graphics.DrawLine(Pens.Blue, m_listPtBegin[i], m_listPtEnd[i]);

										e.Graphics.DrawString(m_listStrDist[i], f, Brushes.Red, m_listPtEnd[i].X + m_object_radius * 2, m_listPtEnd[i].Y);
								}
								f.Dispose();
						}

						//Draw begin points
						foreach (Point ptItem in m_listPtBegin)
						{
								e.Graphics.FillEllipse
								(
									Brushes.White
									, ptItem.X - m_object_radius, ptItem.Y - m_object_radius
									, m_object_radius * 2, m_object_radius * 2
								);
								e.Graphics.DrawEllipse
								(
									Pens.Black
									, ptItem.X - m_object_radius, ptItem.Y - m_object_radius
									, m_object_radius * 2, m_object_radius * 2
								);

						}

						//Draw end points
						foreach (Point ptItem in m_listPtEnd)
						{
								e.Graphics.FillEllipse
								(
									Brushes.White
									, ptItem.X - m_object_radius, ptItem.Y - m_object_radius
									, m_object_radius * 2, m_object_radius * 2
								);
								e.Graphics.DrawEllipse
								(
									Pens.Black
									, ptItem.X - m_object_radius, ptItem.Y - m_object_radius
									, m_object_radius * 2, m_object_radius * 2
								);
						}

						if (m_IsDrawing)
						{
								e.Graphics.DrawLine(Pens.Red, m_ptNewBegin, m_ptNewEnd);
						}
				}

				protected string FormatRectangle(RectangleF rect)
				{
						return string.Format("X:{0:D4}, Y:{1:D4}, W:{2:D4}, H:{3:D4}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
				}
				private void UpdateCursorPosition(Point a_ptLocation)
				{
						Point point = new Point(0, 0);
						if (imageBoxEx_VisionOfPanel.IsPointInImage(a_ptLocation))
						{
								point = imageBoxEx_VisionOfPanel.PointToImage(a_ptLocation);
								toolStripStatusLabelCursorPosition.Text = this.FormatPoint(point);
						}
						else
						{
								toolStripStatusLabelCursorPosition.Text = this.FormatPoint(point);
						}
				}
				private void UpdateStatusBar()
				{
						//scale factor
						toolStripStatusLableZoom.Text = string.Format("{0:D3}%", this.imageBoxEx_VisionOfPanel.Zoom);
						//toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.GetImageViewPort());
						//선택된 ROI 영역 표시
						toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);

				}

				protected string FormatPoint(Point point)
				{
						if (m_VisionLib == null)
								return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);

						if (m_bImgIsOpened)
						{
								m_nGrayVal = m_VisionLib.GetPixel(point);
						}

						return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);
				}
				#endregion
				#region [Event - etc]
				private void Display(object sender, EventArgs e)
				{

						try
						{
								tmr.Stop();
								this.UpdateStatusBar();
						}
						catch (System.Exception ex)
						{
								MessageBox.Show(ex.ToString());
						}
						finally
						{
								if (Visible)
										tmr.Start();

						}
				}
				private void SetTrackbar()
				{
						trackBar_Threshold.Minimum = 0;
						trackBar_Threshold.Maximum = 255;
						trackBar_Threshold.TickFrequency = 10;
						trackBar_Threshold.LargeChange = 1;
						trackBar_Threshold.SmallChange = 1;
						trackBar_Threshold.Value = 0;

						trackBar_Open.Minimum = 0;
						trackBar_Open.Maximum = 100;
						trackBar_Open.TickFrequency = 10;
						trackBar_Open.LargeChange = 1;
						trackBar_Open.SmallChange = 1;
						trackBar_Open.Value = 0;

						trackBar_Close.Minimum = 0;
						trackBar_Close.Maximum = 100;
						trackBar_Close.TickFrequency = 10;
						trackBar_Close.LargeChange = 1;
						trackBar_Close.SmallChange = 1;
						trackBar_Close.Value = 0;

						trackBar_Dilate.Minimum = 0;
						trackBar_Dilate.Maximum = 100;
						trackBar_Dilate.TickFrequency = 10;
						trackBar_Dilate.LargeChange = 1;
						trackBar_Dilate.SmallChange = 1;
						trackBar_Dilate.Value = 0;


						trackBar_Erode.Minimum = 0;
						trackBar_Erode.Maximum = 100;
						trackBar_Erode.TickFrequency = 10;
						trackBar_Erode.LargeChange = 1;
						trackBar_Erode.SmallChange = 1;
						trackBar_Erode.Value = 0;

						lbl_TrackbarValue_Threshold.Text = "0";
						lbl_TrackbarValue_Open.Text = "0";
						lbl_TrackbarValue_Close.Text = "0";
						lbl_TrackbarValue_Dilate.Text = "0";
						lbl_TrackbarValue_Erode.Text = "0";
				}
				/// <summary>
				/// 트랙바 조정시 UpdateImageOfBW8함수 호출하여 Filter 적용.
				/// </summary>
				/// <param name="obj"></param>
				/// <param name="e"></param>
				private void OnTrackBar_ValueChanged(object obj, EventArgs e)
				{
						if (!chkbox_FiiltersDisplay.Checked)
								return;


						switch ((obj as TrackBar).Tag.ToString().ToUpper())
						{
								case "THRESHOLD":
										{
												lbl_TrackbarValue_Threshold.Text = (obj as TrackBar).Value.ToString();
												m_VisionLib.m_LibInfo.m_Filterconfig.m_nThresHoldValue = (uint)(obj as TrackBar).Value;
												break;
										}
								case "OPEN":
										{
												lbl_TrackbarValue_Open.Text = (obj as TrackBar).Value.ToString();
												m_VisionLib.m_LibInfo.m_Filterconfig.m_nOpenDiskValue = (uint)(obj as TrackBar).Value;
												break;
										}
								case "CLOSE":
										{
												lbl_TrackbarValue_Close.Text = (obj as TrackBar).Value.ToString();
												m_VisionLib.m_LibInfo.m_Filterconfig.m_nCloseDiskValue = (uint)(obj as TrackBar).Value;
												break;
										}
								case "DILATE":
										{
												lbl_TrackbarValue_Dilate.Text = (obj as TrackBar).Value.ToString();
												m_VisionLib.m_LibInfo.m_Filterconfig.m_nDilateDiskValue = (uint)(obj as TrackBar).Value;
												break;

										}
								case "ERODE":
										{
												lbl_TrackbarValue_Erode.Text = (obj as TrackBar).Value.ToString();
												m_VisionLib.m_LibInfo.m_Filterconfig.m_nErodeDiskValue = (uint)(obj as TrackBar).Value;
												break;
										}

						}
						UpdateImageOfBW8();
				}
				private void OnCheckedChanged(object sender, EventArgs e)
				{
						try
						{
								CheckBox chkbox = (sender as CheckBox);
								chkbox.InvokeIfNeeded(() =>
								{
										if (m_bImgIsOpened)
										{

												if (chkBox_BoxOfRoi.Checked)
												{
														for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
																((ToolStripMenuItem)toolStripSplitButtonSelectRoi.DropDownItems[i]).Checked = false;

														imageBoxEx_VisionOfPanel.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
														imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.RoiBoxVisible, true);
												}
												else
												{
														imageBoxEx_VisionOfPanel.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.None;
														imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.RoiBoxVisible, false);
												}

												imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.CrossLineVisible, chkbox_CrossLine.Checked);

												m_VisionLib.SetOption(EzInaVision.GDV.eLibOption.ROI, chkbox_DispRois.Checked);
												m_VisionLib.SetOption(EzInaVision.GDV.eLibOption.MATCH_RESULT, chkbox_MatchResult.Checked);
												m_VisionLib.SetOption(EzInaVision.GDV.eLibOption.BLOB_RESULT, chkbox_BlobResult.Checked);
												m_VisionLib.SetOption(EzInaVision.GDV.eLibOption.ROI, chkbox_DispRois.Checked);
												m_VisionLib.SetOption(EzInaVision.GDV.eLibOption.CALIBRATION, chkbox_CalibrationResult.Checked);


												UpdateImageOfBW8();
								// 						imageBoxEx_VisionOfPanel.Invalidate();
								// 						imageBoxEx_VisionOfPanel.Update();
								//						imageBoxEx_VisionOfPanel.Refresh(); //Invalidate() + Update()
								//SetTrackbar(chkbox_Expose);
						}//end of if 
				});
						}
						catch (Exception exc)
						{
								MsgBox.Error(exc.Message);
						}
				}
				#endregion[Event - etc]

				/// <summary>
				/// 옵션 적용.
				/// </summary>
				private void ApplyOptions()
				{
						if (!m_bImgIsOpened)
								return;

						if (chkbox_Threshold.Checked)
								m_VisionLib.ThresholdOfFiltersWithoutCam();

						if (chkbox_Morphology_Open.Checked)
								m_VisionLib.OpenDiskOfFilters();

						if (chkbox_Morphology_Close.Checked)
								m_VisionLib.CloseDiskOfFilters();

						if (chkbox_Morphology_Dilate.Checked)
								m_VisionLib.DilateDiskOfFilters();

						if (chkbox_Morphology_Erode.Checked)
								m_VisionLib.ErodeDiskOfFilters();
				}
				/// <summary>
				/// 선택된 파일을 비젼라이브러리에 적용한다.
				/// </summary>
				private void SetBitmapToEImage()
				{
						if (m_bImgIsOpened)
						{
								m_ePixelFormat = m_bmp.PixelFormat;

								switch (m_bmp.PixelFormat)
								{
								
										case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
												{
														m_VisionLib.m_LibInfo.m_stLibInfo.fImageW = m_bmp.Width;
														m_VisionLib.m_LibInfo.m_stLibInfo.fImageH = m_bmp.Height;
														m_VisionLib.SetBitmapToEImageBW8(m_bmp);
														UpdateImageOfBW8();
														break;
												}
										default:
												m_VisionLib.m_LibInfo.m_stLibInfo.fImageW = m_bmp.Width;
												m_VisionLib.m_LibInfo.m_stLibInfo.fImageH = m_bmp.Height;
											  m_VisionLib.SetColorBitmapToEImageBW8(m_bmp);
												UpdateImageOfBW8();
												break;
								}


						}
				}
				/// <summary>
				/// 변경된 필터값을 적용하여 업데이트한다.
				/// </summary>
				private void UpdateImageOfBW8()
				{
						ApplyOptions();

						Bitmap bmp = null;
						//if filters enable
						if (chkbox_FiiltersDisplay.Checked)
								bmp = m_VisionLib.FilteredImageConvertToBitmap();
						else
								bmp = m_VisionLib.ProcessImageConvertToBitmap();

						imageBoxEx_VisionOfPanel.Image = (Image)bmp.Clone();
						bmp.Dispose();
				}

				#region [TreeView]
				public void TreeView_Init(TreeView a_pTreeView_Modules, ImageList a_ImageList)
				{
						a_pTreeView_Modules.BeginUpdate();
						a_pTreeView_Modules.Nodes.Clear();

						a_pTreeView_Modules.ImageList = a_ImageList;
						a_pTreeView_Modules.Font = new Font("Century Gothic", 9F, FontStyle.Regular);

						TreeNode pCategory = new TreeNode("Category");
						pCategory.ImageIndex = a_ImageList.Images.IndexOfKey("Category.png");
						pCategory.SelectedImageIndex = a_ImageList.Images.IndexOfKey("Category.png");

						TreeNode[] arrTreeNodeMain = new TreeNode[(int)FA.DEF.eRcpSubCategory.Max];

						FA.MGR.ProjectMgr.FillValuesToTreeNode(FA.DEF.eRcpCategory.Vision, ref pCategory, a_ImageList);

						a_pTreeView_Modules.Nodes.Add(pCategory);
						a_pTreeView_Modules.EndUpdate();
				}
				private void treeView_Menu_AfterSelect(object sender, TreeViewEventArgs e)
				{
						if (treeView_Menu.SelectedNode != null && treeView_Menu.SelectedNode.Text.ToUpper().Equals("CATEGORY") == false)
						{
								SelectNode(treeView_Menu.SelectedNode.Text.ToEnum<FA.DEF.eRcpSubCategory>());
								Write_To(treeView_Menu.SelectedNode.Text.ToEnum<FA.DEF.eRcpSubCategory>(), dataGridView_parameters);
						}
				}
				private void treeView_GoldenImageList_AfterSelect(object sender, TreeViewEventArgs e)
				{
						if (e.Node.Tag == null)
								return;

						SelectNode((EzInaVision.GDV.eGoldenImages)(int)e.Node.Tag);
				}
				private void treeView_GoldenImageList_AfterExpand(object sender, TreeViewEventArgs e)
				{
						if (m_SelectedTreeNode.Tag != null)
						{
								SelectNode((EzInaVision.GDV.eGoldenImages)(int)m_SelectedTreeNode.Tag);
						}
				}
				private void FillValuesToTreeNode(FA.DEF.eRcpCategory a_eRcpcategory, ref TreeNode a_node, ImageList a_ImageList)
				{
						List<string> vecList = new List<string>();
						foreach (FA.DEF.eRcpSubCategory sub in Enum.GetValues(typeof(FA.DEF.eRcpSubCategory)))
						{
								vecList.Add(sub.ToString());
						}

						a_node.Tag = a_eRcpcategory.ToString();
						a_node.ImageIndex = a_ImageList.Images.IndexOfKey(a_eRcpcategory.ToString() + ".png");
						a_node.SelectedImageIndex = a_ImageList.Images.IndexOfKey(a_eRcpcategory.ToString() + ".png");


						List<string> Foundlist = new List<string>();

						switch (a_eRcpcategory)
						{
								case FA.DEF.eRcpCategory.Motion:
										{
												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M01") == true);
										}
										break;
								case FA.DEF.eRcpCategory.Vision:
										{
												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M02") == true);
										}
										break;
								case FA.DEF.eRcpCategory.CAM:
										{
												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M07") == true);
										}
										break;
								case FA.DEF.eRcpCategory.Interlock:
										{
												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M08") == true);
										}
										break;
								case FA.DEF.eRcpCategory.Path:
										{

												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M09") == true);
										}
										break;
								case FA.DEF.eRcpCategory.InitialProcess:
										{
												Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M10") == true);
										}
										break;
								case FA.DEF.eRcpCategory.Max:
										{
										}
										break;
								default:
										break;


						}

						for (int i = 0; i < Foundlist.Count; i++)
						{
								switch (Foundlist[i].ToEnum<FA.DEF.eRcpSubCategory>())
								{
										case FA.DEF.eRcpSubCategory.M10_Fine_Matcher:
										case FA.DEF.eRcpSubCategory.M10_Fine_Filter:
										case FA.DEF.eRcpSubCategory.M10_Fine_Blob:
										case FA.DEF.eRcpSubCategory.M10_Fine_Roi:
										case FA.DEF.eRcpSubCategory.M10_Fine_VisionScale:
												a_node.Nodes.Add(Foundlist[i].ToString(), Foundlist[i].ToString(), a_ImageList.Images.IndexOfKey("unchecked.png"), a_ImageList.Images.IndexOfKey("checked.png"));
												break;
								}
						}


				}
				private void SelectNode(EzInaVision.GDV.eGoldenImages a_SelectedGoldenImage)
				{
						//아이콘 초기화.
						foreach (TreeNode pParent in treeView_GoldenImageList.Nodes)
						{
								pParent.SelectedImageIndex = 2;
								foreach (TreeNode pChild in pParent.Nodes)
								{
										pChild.ImageIndex = 0;
										pChild.SelectedImageIndex = 0;
								}
						}

						//Select golden Image
						foreach (TreeNode pParent in treeView_GoldenImageList.Nodes)
						{
								foreach (TreeNode pChild in pParent.Nodes)
								{
										pChild.SelectedImageIndex = 0;

										if (a_SelectedGoldenImage == (EzInaVision.GDV.eGoldenImages)(int)pChild.Tag)
										{
												pChild.SelectedImageIndex = 1;
												treeView_GoldenImageList.SelectedNode = pChild;
												m_SelectedTreeNode = pChild;
												treeView_GoldenImageList.SelectedNode.Expand();


										}
								}
						}
						//             if (a_SelectedGoldenImage != (GDV.eGoldenImages)(int)treeView_Modules.SelectedNode.Tag)
						//                 treeView_Modules.SelectedNode.Expand();

						#region [Golden Image Display]
						btn_Unwrapping.SizeMode = PictureBoxSizeMode.Zoom;
						string strLoadImage = string.Format(@"{0}\{1}_{2}.bmp"
						, FA.DIR.MANUAL_VISION
						, "MANUAL"
						, ((EzInaVision.GDV.eGoldenImages)(int)m_SelectedTreeNode.Tag).ToString());


						System.IO.FileInfo f = new System.IO.FileInfo(strLoadImage);
						if (f.Exists)
						{
								using (var fs = new System.IO.FileStream(strLoadImage, System.IO.FileMode.Open))
								{
										using (Bitmap bmp = new Bitmap(fs))
										{
												if (bmp != null)
														btn_Unwrapping.Image = (Image)bmp.Clone();
										}
								}
						}
						else
						{
								btn_Unwrapping.SizeMode = PictureBoxSizeMode.CenterImage;
								btn_Unwrapping.Image = btn_Unwrapping.ErrorImage;
						}
						#endregion [Golden Image Display]

				}
				private void SelectNode(FA.DEF.eRcpSubCategory a_eRcpSubCategory)
				{
						foreach (TreeNode root in treeView_Menu.Nodes)
						{
								foreach (TreeNode child in root.Nodes)
								{
										child.SelectedImageIndex = 0;
										if (child.Text.ToString().Equals(a_eRcpSubCategory.ToString()))
										{
												child.SelectedImageIndex = 1;
												treeView_Menu.SelectedNode = child;
										}
								}
						}

						treeView_Menu.SelectedNode?.Expand();

				}
				#endregion[TreeView]
				#region [DataGridView]
				public void Read_From(FA.DEF.eRcpSubCategory a_eSubCategory, DataGridView a_grid)
				{
						if (a_eSubCategory < FA.DEF.eRcpSubCategory.M10_Fine_Matcher || a_eSubCategory > FA.DEF.eRcpSubCategory.M10_Fine_VisionScale)
								return;

						//No | Category | Name | Key | Value | Unit | 
						//0    1          2      3     4          5   	
						var list = m_listRcpItems.FindAll(item => item.m_strCategory.ToUpper().Equals(a_eSubCategory.ToString().ToUpper()) == true);
						for (int i = 0; i < list.Count; i++)
						{
								if (!string.IsNullOrEmpty(a_grid[4, i].Value?.ToString()))
								{
										(list[i] as RcpItem).m_strValue = a_grid[4, i].Value.ToString();
								}
						}


				}
				public void Write_To(FA.DEF.eRcpSubCategory a_eRcpSubCategory, DataGridView a_grid)
				{
						#region [DataGridView initialize]
						if (a_grid.RowCount == 0)
						{
								Color ForeColor = Color.Black;
								Color BackColor = SystemColors.Window;
								Color SelectionBackColor = Color.SteelBlue;
								Color SelectionForeColor = Color.White;
								// 				Color SelectionBackColor	= Color.SteelBlue;
								// 				Color SelectionForeColor	= Color.White;

								#region [Explain]
								//선택된 모든 셀의 선택해제
								//a_grid.ClearSelection();
								//첫번째 행 선택
								//a_grid.Rows[0].Selected = true;
								//마지막 행 선택
								//a_grid.Rows[a_grid.Rows.Count - 1].Selected = true;
								//선택행에 삽입.
								//a_grid.Rows.Insert(1, "test");
								//마지막행에 삽입.
								//a_grid.Rows.Add("last");
								//첫번째 선택된 행의 인덱스값.
								//a_grid.SelectedRows[0].Index;
								//특정 행 삭제
								//a_grid.Rows.RemoveAt(0);  //삭제
								//선택 행의 색 바꾸기
								//a_grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
								//a_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
								//특정 행의 색 바꾸기
								//a_grid.Rows[2].DefaultCellStyle.BackColor = Color.Red;
								//특정 행열의 색 바꾸기
								//a_grid.Rows[i].Cells[col].Style.BackColor = ColorTranslator.FromHtml(123, 123, 123, 123);
								//셀 내용 읽기 0행, 0열
								//a_grid.Rows[0].Cells[0].Value.ToString();
								//행의 총수
								//int lines = a_grid.Rows.Count
								//우측 스크롤바 현재 셀위치를 보여주게 자동 이동
								//a_grid.CurrentCell = a_grid.Rows[행].Cells[열];
								//활성화된 셀의 행 인덱스값
								//int select = a_grid.CurrentCell.RowIndex;
								#endregion Explain
								#region [Common]
								a_grid.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8F, FontStyle.Regular, GraphicsUnit.Point);
								a_grid.ReadOnly = false;
								a_grid.AllowUserToAddRows = false;
								a_grid.AllowUserToDeleteRows = false;
								a_grid.AllowUserToOrderColumns = false;
								a_grid.AllowUserToResizeColumns = false;
								a_grid.AllowUserToResizeRows = false;
								a_grid.ColumnHeadersVisible = true;
								a_grid.RowHeadersVisible = false;
								a_grid.MultiSelect = false;
								a_grid.EditMode = DataGridViewEditMode.EditOnEnter;
								a_grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
								a_grid.BackgroundColor = Color.White;
								a_grid.GridColor = Color.SteelBlue;

								a_grid.Columns.Clear();
								a_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
								a_grid.ColumnHeadersHeight = 30;
								a_grid.RowTemplate.Height = 30;
								a_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
								#endregion [Common]

								//No | Category | Name | Key | Value | Unit | 
								//0    1          2      3     4          5   				
								#region [No] [0]
								DataGridViewTextBoxColumn No = new DataGridViewTextBoxColumn();
								No.HeaderText = "No";
								No.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								No.Resizable = DataGridViewTriState.False;
								No.ReadOnly = true;
								No.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
								No.DefaultCellStyle.ForeColor = ForeColor;
								No.DefaultCellStyle.BackColor = BackColor;
								No.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								No.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [No] [0]
								#region [Category] [1]
								DataGridViewTextBoxColumn Category = new DataGridViewTextBoxColumn();
								Category.HeaderText = "Category";
								Category.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Category.Resizable = DataGridViewTriState.False;
								Category.ReadOnly = true;
								Category.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								Category.DefaultCellStyle.ForeColor = ForeColor;
								Category.DefaultCellStyle.BackColor = BackColor;
								Category.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Category.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Category] [1]
								#region [Name] [2]
								DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
								Name.HeaderText = "Name";
								Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Name.Resizable = DataGridViewTriState.False;
								Name.ReadOnly = true;
								Name.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								Name.DefaultCellStyle.ForeColor = ForeColor;
								Name.DefaultCellStyle.BackColor = BackColor;
								Name.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Name.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Name] 
								#region [Key] [3]
								DataGridViewTextBoxColumn Key = new DataGridViewTextBoxColumn();
								Key.HeaderText = "Key";
								Key.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Key.Resizable = DataGridViewTriState.False;
								Key.ReadOnly = true;
								Key.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
								Key.DefaultCellStyle.ForeColor = ForeColor;
								Key.DefaultCellStyle.BackColor = BackColor;
								Key.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Key.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Key] 
								#region [Value] [4]
								DataGridViewTextBoxColumn Value = new DataGridViewTextBoxColumn();
								Value.HeaderText = "Value";
								Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Value.Resizable = DataGridViewTriState.False;
								Value.ReadOnly = true;
								Value.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
								Value.DefaultCellStyle.ForeColor = ForeColor;
								Value.DefaultCellStyle.BackColor = BackColor;
								Value.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Value.DefaultCellStyle.SelectionForeColor = SelectionForeColor;

								#endregion [Value]
								#region [Unit] [5]
								DataGridViewTextBoxColumn Unit = new DataGridViewTextBoxColumn();
								Unit.HeaderText = "Unit";
								Unit.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Unit.Resizable = DataGridViewTriState.False;
								Unit.ReadOnly = true;
								Unit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
								Unit.DefaultCellStyle.ForeColor = ForeColor;
								Unit.DefaultCellStyle.BackColor = BackColor;
								Unit.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Unit.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Key]
								#region [Add]
								a_grid.Columns.AddRange(new DataGridViewTextBoxColumn[] {
				No, Category, Name, Key, Value, Unit});

								#endregion [Add]
								#region [ Head Setting ]
								//Headers setting
								foreach (DataGridViewColumn col in a_grid.Columns)
								{
										col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
										col.HeaderCell.Style.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
								}

								//a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
								a_grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
								a_grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
								a_grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
								a_grid.EnableHeadersVisualStyles = false;
								a_grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
								a_grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
								//a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
								//a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;

								#endregion [ head Setting ]
						}
						#endregion
						RefreshGridOfRcpItem(a_eRcpSubCategory, a_grid); // 데이터그리드뷰 업데이트
				}
				private void dataGridView_parameters_CellClick(object sender, DataGridViewCellEventArgs e)
				{

						//No | Category | Name | Key | Value | Unit | 
						//0    1          2      3     4          5   

						if (e.RowIndex >= 0 && e.ColumnIndex == 4)
						{
								Color OldBackColor = dataGridView_parameters[e.ColumnIndex, e.RowIndex].Style.BackColor;
								Color OldForceColor = dataGridView_parameters[e.ColumnIndex, e.RowIndex].Style.ForeColor;

								dataGridView_parameters[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;

								EzIna.GUI.UserControls.NumberKeypad Num = new EzIna.GUI.UserControls.NumberKeypad();
								if (Num.ShowDialog(0, 10000) == DialogResult.OK)
								{
										dataGridView_parameters[e.ColumnIndex, e.RowIndex].Value = Num.Result.ToString();
								}
								dataGridView_parameters[e.ColumnIndex, e.RowIndex].Style.BackColor = OldBackColor;
						}
						else
								return;
				}
				private void RefreshGridOfRcpItem(FA.DEF.eRcpSubCategory a_eRcpSubCategory, DataGridView a_grid)
				{
						a_grid.Rows.Clear();

						int index = 0;
						switch (a_eRcpSubCategory)
						{
								case FA.DEF.eRcpSubCategory.M10_Fine_Matcher:
										{
												index = 1;
												#region [ Matcher ]
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Minimum_Scale.m_strCategory, F_Matcher_Minimum_Scale.m_strCaption, F_Matcher_Minimum_Scale.iKey, string.Format("{0:F02}", F_Matcher_Minimum_Scale.AsDouble), FA.DEF.GetUnitString(F_Matcher_Minimum_Scale.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Maximum_Scale.m_strCategory, F_Matcher_Maximum_Scale.m_strCaption, F_Matcher_Maximum_Scale.iKey, string.Format("{0:F02}", F_Matcher_Maximum_Scale.AsDouble), FA.DEF.GetUnitString(F_Matcher_Maximum_Scale.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Match_Score.m_strCategory, F_Matcher_Match_Score.m_strCaption, F_Matcher_Match_Score.iKey, string.Format("{0:F02}", F_Matcher_Match_Score.AsDouble), FA.DEF.GetUnitString(F_Matcher_Match_Score.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Match_Angle.m_strCategory, F_Matcher_Match_Angle.m_strCaption, F_Matcher_Match_Angle.iKey, string.Format("{0:F02}", F_Matcher_Match_Angle.AsDouble), FA.DEF.GetUnitString(F_Matcher_Match_Angle.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Match_MaxCount.m_strCategory, F_Matcher_Match_MaxCount.m_strCaption, F_Matcher_Match_MaxCount.iKey, string.Format("{0:#,0}", F_Matcher_Match_MaxCount.AsInt), FA.DEF.GetUnitString(F_Matcher_Match_MaxCount.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Match_CorrelationMode.m_strCategory, F_Matcher_Match_CorrelationMode.m_strCaption, F_Matcher_Match_CorrelationMode.iKey, string.Format("{0:#,0}", F_Matcher_Match_CorrelationMode.AsInt), FA.DEF.GetUnitString(F_Matcher_Match_CorrelationMode.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Matcher_Match_ContrastMode.m_strCategory, F_Matcher_Match_ContrastMode.m_strCaption, F_Matcher_Match_ContrastMode.iKey, string.Format("{0:#,0}", F_Matcher_Match_ContrastMode.AsInt), FA.DEF.GetUnitString(F_Matcher_Match_ContrastMode.m_eUnit));
												#endregion
										}
										break;

								case FA.DEF.eRcpSubCategory.M10_Fine_Filter:
										{
												index = 1;
												#region [ Filters ]
												a_grid.Rows.Add(index++.ToString("D02"), F_Filter_ThresHoldLevel.m_strCategory, F_Filter_ThresHoldLevel.m_strCaption, F_Filter_ThresHoldLevel.iKey, string.Format("{0:#,0}", F_Filter_ThresHoldLevel.AsInt), FA.DEF.GetUnitString(F_Filter_ThresHoldLevel.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Filter_OpenDisk.m_strCategory, F_Filter_OpenDisk.m_strCaption, F_Filter_OpenDisk.iKey, string.Format("{0:#,0}", F_Filter_OpenDisk.AsInt), FA.DEF.GetUnitString(F_Filter_OpenDisk.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Filter_CloseDisk.m_strCategory, F_Filter_CloseDisk.m_strCaption, F_Filter_CloseDisk.iKey, string.Format("{0:#,0}", F_Filter_CloseDisk.AsInt), FA.DEF.GetUnitString(F_Filter_CloseDisk.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Filter_ErodeDisk.m_strCategory, F_Filter_ErodeDisk.m_strCaption, F_Filter_ErodeDisk.iKey, string.Format("{0:#,0}", F_Filter_ErodeDisk.AsInt), FA.DEF.GetUnitString(F_Filter_ErodeDisk.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Filter_Dilate.m_strCategory, F_Filter_Dilate.m_strCaption, F_Filter_Dilate.iKey, string.Format("{0:#,0}", F_Filter_Dilate.AsInt), FA.DEF.GetUnitString(F_Filter_Dilate.m_eUnit));
												#endregion
										}
										break;
								case FA.DEF.eRcpSubCategory.M10_Fine_Blob:
										{
												index = 1;
												#region [ Blobs ]
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_Method.m_strCategory, F_Blob_Method.m_strCaption, F_Blob_Method.iKey, string.Format("{0:#,0}", F_Blob_Method.AsInt), FA.DEF.GetUnitString(F_Blob_Method.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_GrayMinValue.m_strCategory, F_Blob_GrayMinValue.m_strCaption, F_Blob_GrayMinValue.iKey, string.Format("{0:#,0}", F_Blob_GrayMinValue.AsInt), FA.DEF.GetUnitString(F_Blob_GrayMinValue.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_GrayMaxValue.m_strCategory, F_Blob_GrayMaxValue.m_strCaption, F_Blob_GrayMaxValue.iKey, string.Format("{0:#,0}", F_Blob_GrayMaxValue.AsInt), FA.DEF.GetUnitString(F_Blob_GrayMaxValue.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_MinWidth.m_strCategory, F_Blob_MinWidth.m_strCaption, F_Blob_MinWidth.iKey, string.Format("{0:#,0}", F_Blob_MinWidth.AsInt), FA.DEF.GetUnitString(F_Blob_MinWidth.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_MaxWidth.m_strCategory, F_Blob_MaxWidth.m_strCaption, F_Blob_MaxWidth.iKey, string.Format("{0:#,0}", F_Blob_MaxWidth.AsInt), FA.DEF.GetUnitString(F_Blob_MaxWidth.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_MinHeight.m_strCategory, F_Blob_MinHeight.m_strCaption, F_Blob_MinHeight.iKey, string.Format("{0:#,0}", F_Blob_MinHeight.AsInt), FA.DEF.GetUnitString(F_Blob_MinHeight.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_MaxHeight.m_strCategory, F_Blob_MaxHeight.m_strCaption, F_Blob_MaxHeight.iKey, string.Format("{0:#,0}", F_Blob_MaxHeight.AsInt), FA.DEF.GetUnitString(F_Blob_MaxHeight.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_AeraMin.m_strCategory, F_Blob_AeraMin.m_strCaption, F_Blob_AeraMin.iKey, string.Format("{0:#,0}", F_Blob_AeraMin.AsInt), FA.DEF.GetUnitString(F_Blob_AeraMin.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_Blob_AeraMax.m_strCategory, F_Blob_AeraMax.m_strCaption, F_Blob_AeraMax.iKey, string.Format("{0:#,0}", F_Blob_AeraMax.AsInt), FA.DEF.GetUnitString(F_Blob_AeraMax.m_eUnit));
												#endregion
										}
										break;

								case FA.DEF.eRcpSubCategory.M10_Fine_Roi:
										{
												index = 1;
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO_FiducialMarker.m_strCategory, F_ROI_NO_FiducialMarker.m_strCaption, F_ROI_NO_FiducialMarker.iKey, string.Format("{0:#,0}", F_ROI_NO_FiducialMarker.AsDouble), FA.DEF.GetUnitString(F_ROI_NO_FiducialMarker.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO1_X.m_strCategory, F_ROI_NO1_X.m_strCaption, F_ROI_NO1_X.iKey, string.Format("{0:#,0}", F_ROI_NO1_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO1_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO1_Y.m_strCategory, F_ROI_NO1_Y.m_strCaption, F_ROI_NO1_Y.iKey, string.Format("{0:#,0}", F_ROI_NO1_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO1_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO1_WIDTH.m_strCategory, F_ROI_NO1_WIDTH.m_strCaption, F_ROI_NO1_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO1_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO1_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO1_HEIGHT.m_strCategory, F_ROI_NO1_HEIGHT.m_strCaption, F_ROI_NO1_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO1_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO1_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO2_X.m_strCategory, F_ROI_NO2_X.m_strCaption, F_ROI_NO2_X.iKey, string.Format("{0:#,0}", F_ROI_NO2_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO2_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO2_Y.m_strCategory, F_ROI_NO2_Y.m_strCaption, F_ROI_NO2_Y.iKey, string.Format("{0:#,0}", F_ROI_NO2_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO2_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO2_WIDTH.m_strCategory, F_ROI_NO2_WIDTH.m_strCaption, F_ROI_NO2_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO2_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO2_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO2_HEIGHT.m_strCategory, F_ROI_NO2_HEIGHT.m_strCaption, F_ROI_NO2_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO2_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO2_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO3_X.m_strCategory, F_ROI_NO3_X.m_strCaption, F_ROI_NO3_X.iKey, string.Format("{0:#,0}", F_ROI_NO3_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO3_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO3_Y.m_strCategory, F_ROI_NO3_Y.m_strCaption, F_ROI_NO3_Y.iKey, string.Format("{0:#,0}", F_ROI_NO3_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO3_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO3_WIDTH.m_strCategory, F_ROI_NO3_WIDTH.m_strCaption, F_ROI_NO3_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO3_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO3_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO3_HEIGHT.m_strCategory, F_ROI_NO3_HEIGHT.m_strCaption, F_ROI_NO3_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO3_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO3_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO4_X.m_strCategory, F_ROI_NO4_X.m_strCaption, F_ROI_NO4_X.iKey, string.Format("{0:#,0}", F_ROI_NO4_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO4_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO4_Y.m_strCategory, F_ROI_NO4_Y.m_strCaption, F_ROI_NO4_Y.iKey, string.Format("{0:#,0}", F_ROI_NO4_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO4_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO4_WIDTH.m_strCategory, F_ROI_NO4_WIDTH.m_strCaption, F_ROI_NO4_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO4_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO4_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO4_HEIGHT.m_strCategory, F_ROI_NO4_HEIGHT.m_strCaption, F_ROI_NO4_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO4_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO4_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO5_X.m_strCategory, F_ROI_NO5_X.m_strCaption, F_ROI_NO5_X.iKey, string.Format("{0:#,0}", F_ROI_NO5_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO5_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO5_Y.m_strCategory, F_ROI_NO5_Y.m_strCaption, F_ROI_NO5_Y.iKey, string.Format("{0:#,0}", F_ROI_NO5_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO5_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO5_WIDTH.m_strCategory, F_ROI_NO5_WIDTH.m_strCaption, F_ROI_NO5_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO5_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO5_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO5_HEIGHT.m_strCategory, F_ROI_NO5_HEIGHT.m_strCaption, F_ROI_NO5_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO5_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO5_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO6_X.m_strCategory, F_ROI_NO6_X.m_strCaption, F_ROI_NO6_X.iKey, string.Format("{0:#,0}", F_ROI_NO6_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO6_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO6_Y.m_strCategory, F_ROI_NO6_Y.m_strCaption, F_ROI_NO6_Y.iKey, string.Format("{0:#,0}", F_ROI_NO6_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO6_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO6_WIDTH.m_strCategory, F_ROI_NO6_WIDTH.m_strCaption, F_ROI_NO6_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO6_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO6_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO6_HEIGHT.m_strCategory, F_ROI_NO6_HEIGHT.m_strCaption, F_ROI_NO6_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO6_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO6_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO7_X.m_strCategory, F_ROI_NO7_X.m_strCaption, F_ROI_NO7_X.iKey, string.Format("{0:#,0}", F_ROI_NO7_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO7_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO7_Y.m_strCategory, F_ROI_NO7_Y.m_strCaption, F_ROI_NO7_Y.iKey, string.Format("{0:#,0}", F_ROI_NO7_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO7_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO7_WIDTH.m_strCategory, F_ROI_NO7_WIDTH.m_strCaption, F_ROI_NO7_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO7_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO7_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO7_HEIGHT.m_strCategory, F_ROI_NO7_HEIGHT.m_strCaption, F_ROI_NO7_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO7_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO7_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO8_X.m_strCategory, F_ROI_NO8_X.m_strCaption, F_ROI_NO8_X.iKey, string.Format("{0:#,0}", F_ROI_NO8_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO8_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO8_Y.m_strCategory, F_ROI_NO8_Y.m_strCaption, F_ROI_NO8_Y.iKey, string.Format("{0:#,0}", F_ROI_NO8_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO8_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO8_WIDTH.m_strCategory, F_ROI_NO8_WIDTH.m_strCaption, F_ROI_NO8_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO8_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO8_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO8_HEIGHT.m_strCategory, F_ROI_NO8_HEIGHT.m_strCaption, F_ROI_NO8_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO8_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO8_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO9_X.m_strCategory, F_ROI_NO9_X.m_strCaption, F_ROI_NO9_X.iKey, string.Format("{0:#,0}", F_ROI_NO9_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO9_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO9_Y.m_strCategory, F_ROI_NO9_Y.m_strCaption, F_ROI_NO9_Y.iKey, string.Format("{0:#,0}", F_ROI_NO9_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO9_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO9_WIDTH.m_strCategory, F_ROI_NO9_WIDTH.m_strCaption, F_ROI_NO9_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO9_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO9_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO9_HEIGHT.m_strCategory, F_ROI_NO9_HEIGHT.m_strCaption, F_ROI_NO9_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO9_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO9_HEIGHT.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO10_X.m_strCategory, F_ROI_NO10_X.m_strCaption, F_ROI_NO10_X.iKey, string.Format("{0:#,0}", F_ROI_NO10_X.AsInt), FA.DEF.GetUnitString(F_ROI_NO10_X.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO10_Y.m_strCategory, F_ROI_NO10_Y.m_strCaption, F_ROI_NO10_Y.iKey, string.Format("{0:#,0}", F_ROI_NO10_Y.AsInt), FA.DEF.GetUnitString(F_ROI_NO10_Y.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO10_WIDTH.m_strCategory, F_ROI_NO10_WIDTH.m_strCaption, F_ROI_NO10_WIDTH.iKey, string.Format("{0:#,0}", F_ROI_NO10_WIDTH.AsInt), FA.DEF.GetUnitString(F_ROI_NO10_WIDTH.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_ROI_NO10_HEIGHT.m_strCategory, F_ROI_NO10_HEIGHT.m_strCaption, F_ROI_NO10_HEIGHT.iKey, string.Format("{0:#,0}", F_ROI_NO10_HEIGHT.AsInt), FA.DEF.GetUnitString(F_ROI_NO10_HEIGHT.m_eUnit));

										}
										break;
								case FA.DEF.eRcpSubCategory.M10_Fine_VisionScale:
										{
												index = 1;
												a_grid.Rows.Add(index++.ToString("D02"), F_VisionScaleX.m_strCategory, F_VisionScaleX.m_strCaption, F_VisionScaleX.iKey, string.Format("{0:F02}", F_VisionScaleX.AsDouble), FA.DEF.GetUnitString(F_VisionScaleX.m_eUnit));
												a_grid.Rows.Add(index++.ToString("D02"), F_VisionScaleY.m_strCategory, F_VisionScaleY.m_strCaption, F_VisionScaleY.iKey, string.Format("{0:F02}", F_VisionScaleY.AsDouble), FA.DEF.GetUnitString(F_VisionScaleY.m_eUnit));
										}
										break;
								default:
										break;
						}

						int idx = 0;
						foreach (DataGridViewRow row in a_grid.Rows)
						{
								Color bg = row.DefaultCellStyle.BackColor;
								row.DefaultCellStyle.BackColor = idx++ % 2 == 0 ? bg : Color.AliceBlue;
						}

						a_grid.AutoResizeColumns();
				}
				#endregion[DataGridView]
				#region [Data Open/Save]
				/// <summary>
				/// 레시피 항목 로드.
				/// </summary>
				private void DoLoad()
				{
						if (!File.Exists(FA.FILE.ManualVision))
								return;

						using (StreamReader sr = new StreamReader(FA.FILE.ManualVision, Encoding.Unicode))
						{
								string line = "";
								while ((line = sr.ReadLine()) != null)
								{
										if (line.Length < 1) continue;
										string[] words = line.Split('|');
										if (words.Length < 2) continue;
										if (words[0].Trim().Equals("#")) continue;
										int iKey = 0;
										// Category | KEY | VALUE | Unit
										if (!int.TryParse(words[1], out iKey)) continue;
										RcpItem item = m_listRcpItems.Find(FindItem => FindItem.iKey == iKey);

										if (item == null) continue;
										if (words.Length > 2) item.m_strValue = words[2];
										if (words.Length > 3) item.m_eUnit = words[3].ToEnum<FA.DEF.eUnit>();

								}
						}

				}
				/// <summary>
				/// 레시피 항목 저장.
				/// </summary>
				private void DoSave()
				{

						using (StreamWriter sw = new StreamWriter(FA.FILE.ManualVision, false, Encoding.Unicode))
						{
								// Category | KEY | VALUE | Unit
								const string fmt = " | {0:D3} | {1:F5} | {2}";
								sw.WriteLine("-------------------------------------------------------------------------------------------");
								sw.WriteLine("#" + fmt, "KEY", "VALUE", "TEXT");
								sw.WriteLine("-------------------------------------------------------------------------------------------");

								foreach (var data in m_listRcpItems)
								{
										sw.WriteLine(data.m_strCategory + fmt, data.iKey, data.AsDouble, data.m_eUnit.ToString());

								}
						}

				}

				private void ReadDatas_From_Vision()
				{
						#region [ Scale ] 
						F_VisionScaleX.m_strValue = m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleX.ToString();
						F_VisionScaleY.m_strValue = m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleY.ToString();
						#endregion [ Scale ]
						#region [ Matcher ]
						EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
						MatchConfig = m_VisionLib.m_LibInfo.m_MatchConfig;

						F_Matcher_Minimum_Scale.m_strValue = MatchConfig.m_fMinScale.ToString();
						F_Matcher_Maximum_Scale.m_strValue = MatchConfig.m_fMaxScale.ToString();
						F_Matcher_Match_Score.m_strValue = MatchConfig.m_fScore.ToString();
						F_Matcher_Match_Angle.m_strValue = MatchConfig.m_fAngle.ToString();
						F_Matcher_Match_MaxCount.m_strValue = MatchConfig.m_fMaxPosition.ToString();
						F_Matcher_Match_CorrelationMode.m_strValue = MatchConfig.m_iCorrelationMode.ToString();
						F_Matcher_Match_ContrastMode.m_strValue = MatchConfig.m_iMatchContrastMode.ToString();

						#endregion [ Matcher ]
						#region [ Blob ]
						EzInaVision.GDV.BlobConfig BlobConfig = new EzInaVision.GDV.BlobConfig();
						BlobConfig = m_VisionLib.m_LibInfo.m_BlobParam;
						F_Blob_Method.m_strValue = BlobConfig.m_nPadBlobMethod.ToString();
						F_Blob_GrayMinValue.m_strValue = BlobConfig.m_nPadGrayMinVal.ToString();
						F_Blob_GrayMaxValue.m_strValue = BlobConfig.m_nPadGrayMaxVal.ToString();
						F_Blob_MinWidth.m_strValue = BlobConfig.m_fPadWidthMin.ToString();
						F_Blob_MaxWidth.m_strValue = BlobConfig.m_fPadWidthMax.ToString();
						F_Blob_MinHeight.m_strValue = BlobConfig.m_fPadHeightMin.ToString();
						F_Blob_MaxHeight.m_strValue = BlobConfig.m_fPadHeightMax.ToString();
						F_Blob_AeraMin.m_strValue = BlobConfig.m_fPadAreaMin.ToString();
						F_Blob_AeraMax.m_strValue = BlobConfig.m_fPadAreaMax.ToString();
						#endregion [ Blob ]
						#region [ Filters ] 
						EzInaVision.GDV.FilterConfig FilterConfig = new EzInaVision.GDV.FilterConfig();
						FilterConfig = m_VisionLib.m_LibInfo.m_Filterconfig;
						F_Filter_ThresHoldLevel.m_strValue = FilterConfig.m_nThresHoldValue.ToString();
						F_Filter_OpenDisk.m_strValue = FilterConfig.m_nOpenDiskValue.ToString();
						F_Filter_CloseDisk.m_strValue = FilterConfig.m_nCloseDiskValue.ToString();
						F_Filter_ErodeDisk.m_strValue = FilterConfig.m_nErodeDiskValue.ToString();
						F_Filter_Dilate.m_strValue = FilterConfig.m_nDilateDiskValue.ToString();
						#endregion [ Filters ] 
						#region [ ROIs ]
						Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];

						for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
						{
								m_VisionLib.m_LibInfo.m_dicRoiSize.TryGetValue(i, out rtItems[i]);
						}

						F_ROI_NO1_X.m_strValue			= rtItems[0].X.ToString();
						F_ROI_NO1_Y.m_strValue			= rtItems[0].Y.ToString();
						F_ROI_NO1_WIDTH.m_strValue	= rtItems[0].Width.ToString();
						F_ROI_NO1_HEIGHT.m_strValue = rtItems[0].Height.ToString();

						F_ROI_NO2_X.m_strValue			= rtItems[1].X.ToString();
						F_ROI_NO2_Y.m_strValue			= rtItems[1].Y.ToString();
						F_ROI_NO2_WIDTH.m_strValue	= rtItems[1].Width.ToString();
						F_ROI_NO2_HEIGHT.m_strValue = rtItems[1].Height.ToString();

						F_ROI_NO3_X.m_strValue			= rtItems[2].X.ToString();
						F_ROI_NO3_Y.m_strValue			= rtItems[2].Y.ToString();
						F_ROI_NO3_WIDTH.m_strValue	= rtItems[2].Width.ToString();
						F_ROI_NO3_HEIGHT.m_strValue = rtItems[2].Height.ToString();

						F_ROI_NO4_X.m_strValue			= rtItems[3].X.ToString();
						F_ROI_NO4_Y.m_strValue			= rtItems[3].Y.ToString();
						F_ROI_NO4_WIDTH.m_strValue	= rtItems[3].Width.ToString();
						F_ROI_NO4_HEIGHT.m_strValue = rtItems[3].Height.ToString();

						F_ROI_NO5_X.m_strValue			= rtItems[4].X.ToString();
						F_ROI_NO5_Y.m_strValue			= rtItems[4].Y.ToString();
						F_ROI_NO5_WIDTH.m_strValue	= rtItems[4].Width.ToString();
						F_ROI_NO5_HEIGHT.m_strValue = rtItems[4].Height.ToString();

						F_ROI_NO6_X.m_strValue			= rtItems[5].X.ToString();
						F_ROI_NO6_Y.m_strValue			= rtItems[5].Y.ToString();
						F_ROI_NO6_WIDTH.m_strValue	= rtItems[5].Width.ToString();
						F_ROI_NO6_HEIGHT.m_strValue = rtItems[5].Height.ToString();

						F_ROI_NO7_X.m_strValue			= rtItems[6].X.ToString();
						F_ROI_NO7_Y.m_strValue			= rtItems[6].Y.ToString();
						F_ROI_NO7_WIDTH.m_strValue	= rtItems[6].Width.ToString();
						F_ROI_NO7_HEIGHT.m_strValue = rtItems[6].Height.ToString();

						F_ROI_NO8_X.m_strValue			= rtItems[7].X.ToString();
						F_ROI_NO8_Y.m_strValue			= rtItems[7].Y.ToString();
						F_ROI_NO8_WIDTH.m_strValue	= rtItems[7].Width.ToString();
						F_ROI_NO8_HEIGHT.m_strValue = rtItems[7].Height.ToString();

						F_ROI_NO9_X.m_strValue			= rtItems[8].X.ToString();
						F_ROI_NO9_Y.m_strValue			= rtItems[8].Y.ToString();
						F_ROI_NO9_WIDTH.m_strValue	= rtItems[8].Width.ToString();
						F_ROI_NO9_HEIGHT.m_strValue = rtItems[8].Height.ToString();

						F_ROI_NO10_X.m_strValue			 = rtItems[9].X.ToString();
						F_ROI_NO10_Y.m_strValue			 = rtItems[9].Y.ToString();
						F_ROI_NO10_WIDTH.m_strValue	 = rtItems[9].Width.ToString();
						F_ROI_NO10_HEIGHT.m_strValue = rtItems[9].Height.ToString();
						#endregion [ ROIs ]

				}
				private void WriteDatas_To_Vision()
				{
						#region [ Scale ] 
						m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleX = F_VisionScaleX;
						m_VisionLib.m_LibInfo.m_stLibInfo.dLensScaleY = F_VisionScaleY;
						#endregion [ Scale ]
						#region [ Matcher ]
						EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
						MatchConfig.m_fMinScale = F_Matcher_Minimum_Scale;
						MatchConfig.m_fMaxScale = F_Matcher_Maximum_Scale;
						MatchConfig.m_fScore = F_Matcher_Match_Score;
						MatchConfig.m_fAngle = F_Matcher_Match_Angle;
						MatchConfig.m_fMaxPosition = F_Matcher_Match_MaxCount;
						MatchConfig.m_iCorrelationMode = F_Matcher_Match_CorrelationMode;
						MatchConfig.m_iMatchContrastMode = F_Matcher_Match_ContrastMode;
						m_VisionLib.m_LibInfo.m_MatchConfig = MatchConfig;
						#endregion [ Matcher ]
						#region [ Blob ]
						EzInaVision.GDV.BlobConfig BlobConfig = new EzInaVision.GDV.BlobConfig();
						BlobConfig.m_nPadBlobMethod = F_Blob_Method;
						BlobConfig.m_nPadGrayMinVal = F_Blob_GrayMinValue.AsUint;
						BlobConfig.m_nPadGrayMaxVal = F_Blob_GrayMaxValue.AsUint;
						BlobConfig.m_fPadWidthMin = F_Blob_MinWidth;
						BlobConfig.m_fPadWidthMax = F_Blob_MaxWidth;
						BlobConfig.m_fPadHeightMin = F_Blob_MinHeight;
						BlobConfig.m_fPadHeightMax = F_Blob_MaxHeight;
						BlobConfig.m_fPadAreaMin = F_Blob_AeraMin;
						BlobConfig.m_fPadAreaMax = F_Blob_AeraMax;
						m_VisionLib.m_LibInfo.m_BlobParam = BlobConfig;
						#endregion [ Blob ]
						#region [ Filters ] 
						EzInaVision.GDV.FilterConfig FilterConfig = new EzInaVision.GDV.FilterConfig();
						FilterConfig.m_nThresHoldValue = F_Filter_ThresHoldLevel.AsUint;
						FilterConfig.m_nOpenDiskValue = F_Filter_OpenDisk.AsUint;
						FilterConfig.m_nCloseDiskValue = F_Filter_CloseDisk.AsUint;
						FilterConfig.m_nErodeDiskValue = F_Filter_ErodeDisk.AsUint;
						FilterConfig.m_nDilateDiskValue = F_Filter_Dilate.AsUint;
						m_VisionLib.m_LibInfo.m_Filterconfig = FilterConfig;
						#endregion [ Filters ] 
						#region [ ROIs ]
						Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].X = F_ROI_NO1_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Y = F_ROI_NO1_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Width = F_ROI_NO1_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Height = F_ROI_NO1_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].X = F_ROI_NO2_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Y = F_ROI_NO2_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Width = F_ROI_NO2_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Height = F_ROI_NO2_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].X = F_ROI_NO3_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Y = F_ROI_NO3_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Width = F_ROI_NO3_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Height = F_ROI_NO3_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].X = F_ROI_NO4_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Y = F_ROI_NO4_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Width = F_ROI_NO4_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Height = F_ROI_NO4_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].X = F_ROI_NO5_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Y = F_ROI_NO5_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Width = F_ROI_NO5_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Height = F_ROI_NO5_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].X = F_ROI_NO6_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Y = F_ROI_NO6_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Width = F_ROI_NO6_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Height = F_ROI_NO6_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].X = F_ROI_NO7_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Y = F_ROI_NO7_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Width = F_ROI_NO7_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Height = F_ROI_NO7_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].X = F_ROI_NO8_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Y = F_ROI_NO8_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Width = F_ROI_NO8_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Height = F_ROI_NO8_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].X = F_ROI_NO9_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Y = F_ROI_NO9_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Width = F_ROI_NO9_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Height = F_ROI_NO9_HEIGHT;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].X = F_ROI_NO10_X;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Y = F_ROI_NO10_Y;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Width = F_ROI_NO10_WIDTH;
						rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Height = F_ROI_NO10_HEIGHT;


						for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
								m_VisionLib.m_LibInfo.m_dicRoiSize.AddOrUpdate(i, rtItems[i], (k, v) => rtItems[i]);
						#endregion [ ROIs ]

				}
				private void btn_Save_Click(object sender, EventArgs e)
				{
						if (treeView_Menu.SelectedNode != null && treeView_Menu.SelectedNode.Text.ToUpper().Equals("CATEGORY") == false)
						{
								if (MsgBox.Confirm("Would you like to save this??"))
								{
										Read_From(treeView_Menu.SelectedNode.Text.ToEnum<FA.DEF.eRcpSubCategory>(), dataGridView_parameters);
										WriteDatas_To_Vision();
										DoSave();
								}

						}
				}
				private void btn_Open_Click(object sender, EventArgs e)
				{
						if (treeView_Menu.SelectedNode != null && treeView_Menu.SelectedNode.Text.ToUpper().Equals("CATEGORY") == false)
						{
								if (MsgBox.Confirm("Would you like to open the recipe file??"))
								{
										DoLoad();
										WriteDatas_To_Vision();
										Write_To(treeView_Menu.SelectedNode.Text.ToEnum<FA.DEF.eRcpSubCategory>(), dataGridView_parameters);
								}
						}
				}

				#endregion [Data Open/Save]

				private void btn_Match_Click(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
						{
								MsgBox.Error("The image is not opened");
								return;
						}

						if (m_eGoldenImage == EzInaVision.GDV.eGoldenImages.None || m_eGoldenImage == EzInaVision.GDV.eGoldenImages.Max)
						{
								MsgBox.Error("The golden image is not selected");
								return;
						}
						m_VisionLib.CopyUnwrappedImageToProcessImage();
						m_VisionLib.ClearAllMatchResults();
						m_VisionLib.MatchRun(FA.DIR.MANUAL_VISION, m_eGoldenImage, m_eRoiItem);

						//CorrMappingItem item = new CorrMappingItem();
						//item.ChipPitch(m_VisionLib.m_LibInfo.m_vecMatchResultsForCalculation[(int)m_eRoiItem][(int)m_eGoldenImage]);
						//item.MatchCompile(m_VisionLib.m_LibInfo.m_vecMatchResultsForCalculation[(int)m_eRoiItem][(int)m_eGoldenImage], 150,150);
						//item.SaveMappingDataOfXY(150,150, @"D:\");
						imageBoxEx_VisionOfPanel.Invalidate();

				}

				private void btn_Blob_Click(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
						{
								MsgBox.Error("The image is not opened");
								return;
						}

						if (m_eRoiItem == EzInaVision.GDV.eRoiItems.None || m_eRoiItem >= EzInaVision.GDV.eRoiItems.Max)
						{
								MsgBox.Error("The ROI is not selected");
								return;
						}

						if(chkbox_FiiltersDisplay.Checked)
								m_VisionLib.CopyUnwrappedImageToFilterImage();
						else
								m_VisionLib.CopyUnwrappedImageToProcessImage();

						m_VisionLib.ClearAllMatchResults();
						m_VisionLib.BlobRun(m_eRoiItem);
						imageBoxEx_VisionOfPanel.Invalidate();
				}

				private void btn_Calibration_Click(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
						{
								MsgBox.Error("The image is not opened");
								return;
						}
						m_VisionLib.GaugeCalibration(m_eRoiItem, 5, 5);
						imageBoxEx_VisionOfPanel.Invalidate();
				}

				private void btn_Click(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
						{
								MsgBox.Error("The image is not opened");
								return;
						}

						if(m_VisionLib.GaugeUnwarping(m_bmp) == 0)
						{
							Bitmap bmp = null;
							bmp = m_VisionLib.UnwrappedImageConvertToBitamp();
							imageBoxEx_VisionOfPanel.Image = (Image)bmp.Clone();
							m_VisionLib.m_LibInfo.m_stLibInfo.fImageW = bmp.Width;
							m_VisionLib.m_LibInfo.m_stLibInfo.fImageH = bmp.Height;
						}
				}

				private void btn_DataMatrixCode_Click(object sender, EventArgs e)
				{
						if (!m_bImgIsOpened)
						{
								MsgBox.Error("The image is not opened");
								return;
						}
						Rectangle[] TestRectList = new Rectangle[]
								{
									 m_VisionLib.GetRoiForInspection((int)EzInaVision.GDV.eRoiItems.ROI_No0),
									 m_VisionLib.GetRoiForInspection((int)EzInaVision.GDV.eRoiItems.ROI_No1),
									 m_VisionLib.GetRoiForInspection((int)EzInaVision.GDV.eRoiItems.ROI_No2),
									 m_VisionLib.GetRoiForInspection((int)EzInaVision.GDV.eRoiItems.ROI_No3),
									 m_VisionLib.GetRoiForInspection((int)EzInaVision.GDV.eRoiItems.ROI_No4),
								};
						if (m_VisionLib.MatrixCode1MultiRun((int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
								TestRectList,5.0f
								) > 0)
						{
								//Bitmap bmp = null;
								//bmp = m_VisionLib.UnwrappedImageConvertToBitamp();
								//imageBoxEx_VisionOfPanel.Image = (Image)bmp.Clone();
								imageBoxEx_VisionOfPanel.Invalidate();
						}
				}

				private void button1_Click(object sender, EventArgs e)
				{					
						try
						{
								string strFilePath = string.Format(@"{0}.csv","D:\\TestCoordinate");//, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
								using (FileStream f = new FileStream(strFilePath, FileMode.Create, FileAccess.Write))
								{
										using (StreamWriter sw = new StreamWriter(f))
										{
												EzInaVision.GDV.MatchResult Temp;
												List<KeyValuePair<int,EzInaVision.GDV.MatchResult>> TempList=m_VisionLib.m_LibInfo.m_vecMatchResults[(int)m_eRoiItem][(int)m_eGoldenImage].ToList();
												List<KeyValuePair<int,EzInaVision.GDV.MatchResult>> SortTempList;
												List<KeyValuePair<int,EzInaVision.GDV.MatchResult>> SortRowList;
												List<KeyValuePair<int,EzInaVision.GDV.MatchResult>> SortTemp;
												SortTempList=TempList.OrderBy(p=> p.Value.m_fSensorYPos).ToList();
											 
												

												//// Header
												sw.WriteLine("Array X Count, Array Y Count, Resolution");
												sw.WriteLine(string.Format("{0},{1},{2:F3},{3:F3},{4:F3}", 11, 11,-49,-49, 9.8));

												for (int i = 0; i < 11; i++)
												{
														SortRowList=SortTempList.GetRange(i*11,11).ToList();
														SortTemp=SortRowList.OrderBy(x=>x.Value.m_fSensorXPos).ToList();
														for(int j=0;j<11;j++)
														{
																sw.Write(string.Format("{0:0000.000},{1:0000.000}", SortTemp[j].Value.m_fSensorXPos, SortTemp[j].Value.m_fSensorYPos));
																sw.Write("\t");
														}
														sw.WriteLine();		
												}																																						
												sw.Close();
										}
										f.Close();
								}								
						}
						catch (Exception ex)
						{
								
						}					
				}
		}

		/// <summary>
		/// 레시피 아이템 클래스
		/// </summary>
		public class RcpItem
		{
				public FA.DEF.eRcpCategory m_eRcpCategory;
				public bool m_IsCommon = false;
				public string m_strCategory = "NONE";
				public string m_strCaption = "NONE";
				public string m_strValue = "";
				public FA.DEF.eUnit m_eUnit = FA.DEF.eUnit.none;
				public int m_iAxis = -1;

				private int m_iKey;
				public int iKey { get { return m_iKey; } }

				public double AsDouble
				{
						get
						{
								double ret = 0.0;
								double.TryParse(m_strValue, out ret);
								return ret;
						}
				}
				public int AsInt
				{
						get
						{
								double dRet = 0.0;
								double.TryParse(m_strValue, out dRet);
								return (int)dRet;
						}
				}

				public uint AsUint
				{
						get
						{
								double dRet = 0.0;
								double.TryParse(m_strValue, out dRet);
								return (uint)dRet;
						}

				}

				public static implicit operator string(RcpItem a_item)
				{
						return a_item.m_strValue;
				}
				public static implicit operator int(RcpItem a_item)
				{
						int nRtn = 0;
						try
						{
								double dRet = 0.0;
								double.TryParse(a_item.m_strValue, out dRet);
								nRtn = (int)dRet;
						}
						catch (Exception exc)
						{
								FA.LOG.Debug("Exception", exc.Message);
						}

						return nRtn;

				}
				public static implicit operator double(RcpItem a_item)
				{
						double dRtn = 0.0;
						try
						{
								double.TryParse(a_item.m_strValue, out dRtn);
						}
						catch (Exception exc)
						{
								FA.LOG.Debug("Exception", exc.Message);
						}

						return dRtn;
				}
				public static implicit operator float(RcpItem a_item)
				{
						float fRtn = 0.0f;
						try
						{
								float.TryParse(a_item.m_strValue, out fRtn);
						}
						catch (Exception exc)
						{
								FA.LOG.Debug("Exception", exc.Message);
						}

						return fRtn;
				}
				public RcpItem(FA.DEF.eRcpCategory a_eRcpItem, int a_iKey)
				{
						m_eRcpCategory = a_eRcpItem;
						m_iKey = a_iKey;
				}
				public RcpItem(FA.DEF.eRcpCategory a_eRcpItem, int a_iKey, string a_strCategory, string a_strName, string a_strValue, FA.DEF.eUnit a_eUnit, bool a_IsCommon = false, int a_iAxis = -1)
				{
						m_eRcpCategory = a_eRcpItem;
						m_iKey = a_iKey;
						m_strCategory = a_strCategory;
						m_strCaption = a_strName;
						m_strValue = a_strValue;
						m_eUnit = a_eUnit;
						m_IsCommon = a_IsCommon;
						m_iAxis = a_iAxis;
				}
		}
}
