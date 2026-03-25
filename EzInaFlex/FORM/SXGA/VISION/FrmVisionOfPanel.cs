using Cyotek.Windows.Forms;
using EzIna.FA;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmVisionOfPanel : Form
    {
        #region [ Delegate ]
        public delegate void UpdateGoldenImageEventhandler(string a_strName, EzInaVision.GDV.eGoldenImages GoldenImage);
        private UpdateGoldenImageEventhandler _OnUpdateGoldenImageEvent;
        public event UpdateGoldenImageEventhandler OnUpdateGoldenImageEvent
        {
            add
            {
                lock (this)
                    _OnUpdateGoldenImageEvent += value;
            }
            remove
            {
                lock (this)
                    _OnUpdateGoldenImageEvent -= value;
            }

        }
        #endregion [ Delegate ]
        #region [Vision]
        EzInaVision.GDV.eRoiItems m_eRoiItem = EzInaVision.GDV.eRoiItems.None;
        EzInaVision.GDV.eGoldenImages m_eGoldenImage = EzInaVision.GDV.eGoldenImages.None;
        public EzInaVision.GDV.eRoiItems eSelectedRoiItem
        {
            get { return m_eRoiItem; }
        }
        public EzInaVision.GDV.eGoldenImages eSelectedGoldenImage
        {
            get { return m_eGoldenImage; }
        }
        enum eTagOfButton
        {
            None = 0
            , Live
            , Idle
            , Grab
            , Match
            , Blob
                        , Datamatrix
            , Center
            , Apply
            , Save
            , Max
        }

        string m_strVisionName;
        #endregion
        #region [ Point ]
        private int m_nGrayVal = 0;
        private List<Point> m_listPtBegin = new List<Point>();
        private List<Point> m_listPtEnd = new List<Point>();
        private List<string> m_listStrDist = new List<string>();

        bool m_IsDrawing = false;
        private Point m_ptNewBegin = Point.Empty;
        private Point m_ptNewEnd = Point.Empty;

        int m_object_radius = 1;
        #endregion

        private double m_dPreviousMX = 0.0;
        private double m_dPreviousMY = 0.0;

        public bool bIsOperation { get; set; } //메뉴 화면 축소 확대.
        TreeNode m_SelectedTreeNode = new TreeNode();
        System.Windows.Forms.Timer tmr = null;

        public FrmVisionOfPanel(string a_strVisionName)
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            m_strVisionName = a_strVisionName;
            bIsOperation = true;

            imageBoxEx_VisionOfPanel.MouseDown += new MouseEventHandler(imageBox_MouseDown);
            imageBoxEx_VisionOfPanel.Paint += new PaintEventHandler(imageBox_Paint);
            imageBoxEx_VisionOfPanel.MouseClick += new MouseEventHandler(OnMouseClick);



        }



        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                return;

            switch ((sender as Control).Name)
            {
                case "imageBoxEx_VisionOfPanel":
                    if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location) && chkbox_ClickMove.Checked)
                    {
                        Point ptTargetPos = new Point();
                        double dMX = 0.0, dMY = 0.0;

                        ptTargetPos = imageBoxEx_VisionOfPanel.PointToImage(e.X, e.Y);
                        if (m_strVisionName.ToUpper().Contains("FINE"))
                        {
                            dMX = FA.VISION.FINE_LIB.GetRelXPosToMoving(ptTargetPos);
                            dMY = FA.VISION.FINE_LIB.GetRelYPosToMoving(ptTargetPos);

                        }
                        else if (m_strVisionName.ToUpper().Contains("COARSE"))
                        {
                            dMX = FA.VISION.COARSE_LIB.GetRelXPosToMoving(ptTargetPos);
                            dMY = FA.VISION.COARSE_LIB.GetRelYPosToMoving(ptTargetPos);
                        }

                        FA.ACT.MoveRel(FA.DEF.eAxesName.RX, dMX, Motion.GDMotion.eSpeedType.FAST);
                        FA.ACT.MoveRel(FA.DEF.eAxesName.Y, dMY, Motion.GDMotion.eSpeedType.FAST);
                    }

                    break;
            }
        }
        #region [ MatrixCode1 Display ]
        public void OnMatrixCode1Display(object a_Obj, List<EzInaVision.GDV.MatrixCodeResult> a_ResultList)
        {
            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
            {
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();
            });
        }
        #endregion [ MatrixCode1 Display ]
        #region [ Match Display ]
        public void OnMatchDisplay(object a_Obj, List<List<ConcurrentDictionary<int, EzInaVision.GDV.MatchResult>>> a_vecMatchReuslt)
        {
            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
            {
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();
            });
        }
        #endregion[ Match Display ]

        #region [ Blob Display ] 
        public void OnBlobDisplay(object a_Obj, ConcurrentDictionary<int, EzInaVision.GDV.BlobResult> a_dicBlob)
        {

            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
            {
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();

            }
            );

        }
        #endregion [ Blob Display ] 
        #region [ Measure Points ]
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


                    EzInaVision.VisionLibBaseClass item = FA.MGR.VisionMgr.GetLib(m_strVisionName);
                    if (item != null)
                    {
                        if (item.GetType() == typeof(EzInaVisionLibrary.VisionLibEuresys))
                        {
                            m_listStrDist.Add
                            (
                              string.Format
                              (
                                "{0:F2}um, {1:F2}um"
                                , Math.Abs((ptRealEnd.X - ptRealBegin.X) * ((EzInaVisionLibrary.VisionLibEuresys)item).m_LibInfo.m_stLibInfo.dLensScaleX)
                                , Math.Abs((ptRealEnd.Y - ptRealBegin.Y) * ((EzInaVisionLibrary.VisionLibEuresys)item).m_LibInfo.m_stLibInfo.dLensScaleY)
                              )
                            );

                        }
                    }
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
        #endregion [ Measure Points ]
        #region [ delegate ]
        public void OnDisplay(object a_Sender, Bitmap a_Image)
        {
            try
            {
                imageBoxEx_VisionOfPanel.InvokeIfNeeded
                (
                    () =>
                    {
                        //if(imageBoxEx_VisionOfPanel.Image != null)
                        //{
                        Image Img = imageBoxEx_VisionOfPanel.Image;
                        imageBoxEx_VisionOfPanel.Image = a_Image;
                        if (Img != null)
                            Img.Dispose();
                        //}
                    }
                );

            }
            catch (Exception ex)
            {
                // MsgBox.Error(ex.ToString());
            }
        }
        #endregion [ delegate ]

        private void FrmVisionOfPanel_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel_VisionName.Text = string.Format("[ {0} CAMERA ]", m_strVisionName.ToUpper());

            //label_VisionInfor.BackColor = Color.Transparent;
            //label_VisionInfor.Parent = imageBoxEx_VisionOfPanel;

            // apply a minimum selection size for resize operations
            imageBoxEx_VisionOfPanel.MinimumSelectionSize = new Size(8, 8);
            imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.None;

            #region menu of Golden Images.
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
            #region menu of Rois

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


            toolStripBtn_IMG_SAV.Click += new EventHandler(OnToolStripMenuIMG_SAV);
            #endregion

            #region Button event
            gbtn_Live.Click += new EventHandler(btn_Func_Click);
            gbtn_Idle.Click += new EventHandler(btn_Func_Click);
            gbtn_Grab.Click += new EventHandler(btn_Func_Click);
            gbtn_RoiMoveToCenter.Click += new EventHandler(btn_Func_Click);
            //gbtn_Apply.Click += new EventHandler(btn_Func_Click);
            //btnSave.Click += new EventHandler(btn_Func_Click);

            gbtn_Live.Tag = eTagOfButton.Live;
            gbtn_Idle.Tag = eTagOfButton.Idle;
            gbtn_Grab.Tag = eTagOfButton.Grab;
            gbtn_Match.Tag = eTagOfButton.Match;
            gbtn_Blob.Tag = eTagOfButton.Blob;
            gbtn_RoiMoveToCenter.Tag = eTagOfButton.Center;
            //gbtn_Apply.Tag = eTagOfButton.Apply;
            //btnSave.Tag = eTagOfButton.Save;
            #endregion

            #region Timer for display
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 50;
            tmr.Tick += new EventHandler(Display);
            tmr.Start();
            #endregion

            #region checkbox option
            chkbox_CrossLine.CheckedChanged += new EventHandler(OnCheckedChanged);
            chkBox_BoxOfRoi.CheckedChanged += new EventHandler(OnCheckedChanged);
            chkbox_display_Filters.CheckedChanged += new EventHandler(OnCheckedChanged);

            chkbox_DispMatchResult.CheckedChanged += new EventHandler(OnCheckedChanged);
            chkbox_DispBlobResult.CheckedChanged += new EventHandler(OnCheckedChanged);
            chkbox_DispRois.CheckedChanged += new EventHandler(OnCheckedChanged);
            #endregion

            #region Vision live
            if (IsExistsItemOfVision())
            {
                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive() == true)
                    gbtn_Grab.Enabled = false;
                else
                    gbtn_Grab.Enabled = true;
            }
            #endregion


        }

        private void OnMenuOfGoldenImages(object sender, EventArgs e)
        {
            try
            {
                if (FA.MGR.VisionMgr == null)
                    throw new Exception("VisionMgr class is null");
#if !SIM
               if (FA.MGR.VisionMgr.bIntialized == false)
                return;
#else
                if (imageBoxEx_VisionOfPanel.Image == null)
                    return;
#endif

              


                for (int i = 0; i < (int)EzInaVision.GDV.eGoldenImages.Max; i++)
                    ((ToolStripMenuItem)toolStripSplitButtonGoldenImages.DropDownItems[i]).Checked = false;

                ((ToolStripMenuItem)sender).Checked = true;
                m_eGoldenImage = (EzInaVision.GDV.eGoldenImages)(Convert.ToInt32((sender as ToolStripMenuItem).Tag));

                toolStripStatusLabel_SelectedGoldenImage.Text = (sender as ToolStripMenuItem).Text;

            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
        /// <summary>
        /// 골든 이미지 저장 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region 골든 이미지 저장 함수
        private void OnMenuOfGoldenImagesSave(object sender, EventArgs e)
        {
            try
            {
                if (FA.MGR.VisionMgr == null)
                    throw new Exception("VisionMgr class is null");
                //                 if (FA.MGR.ProjectMgr == null)
                //                     throw new Exception("RecipeMgr class is null");

                if (FA.MGR.VisionMgr.bIntialized == false)
                    throw new Exception("Vision is not initialized");

                if (m_eGoldenImage == EzInaVision.GDV.eGoldenImages.None || m_eGoldenImage == EzInaVision.GDV.eGoldenImages.Max)
                    throw new Exception("It is not selected Golden image");

                if (!chkBox_BoxOfRoi.Checked)
                    throw new Exception("It is not checked \"Box of Roi \"");

                if (imageBoxEx_VisionOfPanel.SelectionRegion == RectangleF.Empty)
                    throw new Exception("The region size is empty");


                if (MsgBox.Confirm("Would you like to save this \"{0}\" ??", m_eGoldenImage.ToString("G")))
                {

                    FA.MGR.VisionMgr?.GetLib(m_strVisionName.ToUpper()).SavePattern(

                                                FA.MGR.ProjectMgr.SelectedModelPath
                        //FA.MGR.RecipeMgr.SelectedModelPath
                        , m_strVisionName
                        , m_eGoldenImage.ToString("G")
                        , Rectangle.Round(imageBoxEx_VisionOfPanel.SelectionRegion));

                    SelectNode(m_eGoldenImage);
                }


                // 				for (int i = 0; i < (int)GD.eGoldenImages.Max; i++)
                // 				    ((ToolStripMenuItem)toolStripSplitButtonGoldenImages.DropDownItems[i]).Checked = false;
                // 
                // 				((ToolStripMenuItem)sender).Checked = true;
                // 				m_eGoldenImage = (GD.eGoldenImages)(Convert.ToInt32((sender as ToolStripMenuItem).Tag));

            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
        #endregion [ 
        private void OnToolStripMenuItems(object sender, EventArgs e)
        {
            if (FA.MGR.VisionMgr == null) return;
#if !SIM
            if (FA.MGR.VisionMgr.bIntialized == false)
                return;
#else
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;
#endif

            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                ((ToolStripMenuItem)toolStripSplitButtonSelectRoi.DropDownItems[i]).Checked = false;

            ((ToolStripMenuItem)sender).Checked = true;
            m_eRoiItem = (EzInaVision.GDV.eRoiItems)(Convert.ToInt32((sender as ToolStripMenuItem).Tag));

            toolStripStatusLabel_SelectedRoi.Text = (sender as ToolStripMenuItem).Text;
        }

        private void CheckBox_Items_Init()
        {
            if (!IsExistsItemOfVision())
                return;

            //[To do list] 옵션 추가할것. 
            chkbox_CrossLine.Checked = OPT.CrossLineVisible;
            imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.CrossLineVisible, chkbox_CrossLine.Checked);

            //              chkbox_display_Filters  .Checked = OPT.FilterDisp;
            //              chkbox_DispMatchResult  .Checked = OPT.MatchResultDisp;
            //              chkbox_DispBlobResult   .Checked = OPT.BlobResultDisp;
            //              chkbox_DispRois         .Checked = OPT.ROIsVisible;
            // 
            //             chkbox_sellectedExpos   .ImageIndex = chkbox_sellectedExpos.Checked     ? 1 : 0;
            //             chkbox_CrossLine        .ImageIndex = chkbox_CrossLine.Checked          ? 1 : 0;
            //             chkbox_display_Filters  .ImageIndex = chkbox_display_Filters.Checked    ? 1 : 0;
            //             chkbox_DispMatchResult  .ImageIndex = chkbox_DispMatchResult.Checked    ? 1 : 0;
            //             chkbox_DispBlobResult   .ImageIndex = chkbox_DispBlobResult.Checked     ? 1 : 0;
            //             chkbox_DispRois         .ImageIndex = chkbox_DispRois.Checked           ? 1 : 0;
        }
        private void ToolStripMenuItems_Init()
        {
            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                ((ToolStripMenuItem)toolStripSplitButtonSelectRoi.DropDownItems[i]).Checked = false;

            m_eRoiItem = EzInaVision.GDV.eRoiItems.None;

            for (int i = 0; i < (int)EzInaVision.GDV.eGoldenImages.Max; i++)
                ((ToolStripMenuItem)toolStripSplitButtonGoldenImages.DropDownItems[i]).Checked = false;

            m_eGoldenImage = EzInaVision.GDV.eGoldenImages.None;

            toolStripStatusLabel_SelectedGoldenImage.Text = "________";
            toolStripStatusLabel_SelectedRoi.Text = "________";

            imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.None;
            imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.RoiBoxVisible, false);
            chkBox_BoxOfRoi.ImageIndex = 0;
        }

        private void OnToolStripMenuItemsSave(object sender, EventArgs e)
        {
            try
            {
                if (FA.MGR.VisionMgr == null)
                    throw new Exception("VisionMgr class is null");
#if !SIM
								if ((FA.MGR.VisionMgr).bIntialized == false)
                    throw new Exception("Vision is not initialized");
#endif
                if (m_eRoiItem == EzInaVision.GDV.eRoiItems.None || m_eRoiItem == EzInaVision.GDV.eRoiItems.Max)
                    throw new Exception("Is not selected ROI");

                if (!chkBox_BoxOfRoi.Checked)
                    throw new Exception("Is not checked \"Box of Roi \"");

                if (imageBoxEx_VisionOfPanel.SelectionRegion == RectangleF.Empty)
                    throw new Exception("Section size is empty");



                if (MsgBox.Confirm(string.Format("{0} {1}??", "Would you like to save ", m_eRoiItem.ToString())))
                {
                    //[To do list]
                    imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.Rectangle;
                    //Vision Module
                    FA.MGR.VisionMgr?.GetLib(m_strVisionName.ToUpper()).SetRoiForInspection((int)m_eRoiItem,
                                      Rectangle.Round(imageBoxEx_VisionOfPanel.SelectionRegion));
                    //Recipe Module
                    FA.MGR.ProjectMgr.ReadDatas_From_Vision(m_strVisionName.ToEnum<FA.DEF.eVision>(), FA.DEF.eVisionItem.ROIs);
                    FA.MGR.ProjectMgr.ProjectSave();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
        private void OnToolStripMenuIMG_SAV(object sender, EventArgs e)
        {
            try
            {
                if (FA.MGR.VisionMgr == null)
                    throw new Exception("VisionMgr class is null");
#if !SIM
								if ((FA.MGR.VisionMgr).bIntialized == false)
                    throw new Exception("Vision is not initialized");
																				
								if(FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive())
										 throw new Exception("Camera is Live , Stop First");
								if(FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab()==false)
										throw new Exception("Execute Camera Grab before Save ");
#endif

                SaveFileDialog SaveFileDlg = new SaveFileDialog();
                SaveFileDlg.DefaultExt = "*.bmp";
                SaveFileDlg.Filter = "Image Files(*.bmp)|*.bmp";
                //SaveFileDlg.CheckFileExists=true;
                SaveFileDlg.CheckPathExists = true;

                if (SaveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo pInfo = new DirectoryInfo(SaveFileDlg.FileName);
                    if (FA.MGR.VisionMgr.GetCam(m_strVisionName).SaveImage(pInfo.FullName) == false)
                    {
                        throw new Exception("Image Save Fail");
                    }

                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
        private void btn_Func_Click(object sender, EventArgs e)
        {
            if (FA.MGR.VisionMgr == null)
                return;

            if (FA.MGR.VisionMgr.bIntialized == false)
                return;

            //[To do list]
            //             if (FA.MGR.RunMgr.eRunMode != RD.eRunMode.Stop)
            //                 return;

            eTagOfButton eTag = (eTagOfButton)(sender as Button).Tag;

            switch (eTag)
            {
                case eTagOfButton.Live:
                    gbtn_Grab.Enabled = false;
                    FA.MGR.VisionMgr.GetCam(m_strVisionName).Live();
                    break;
                case eTagOfButton.Idle:
                    gbtn_Grab.Enabled = true;
                    FA.MGR.VisionMgr.GetCam(m_strVisionName).Idle();
                    break;
                case eTagOfButton.Grab:

                    FA.MGR.VisionMgr.GetCam(m_strVisionName).Grab();

                    break;
                case eTagOfButton.Center:
                    imageBoxEx_VisionOfPanel.RoiMoveToCenterOfImageEx();
                    break;
                case eTagOfButton.Apply:
                    {

                    }
                    break;
                case eTagOfButton.Save:

                    break;

            }
        }

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
                tmr.Start();

            }
        }

        private void UpdateStatusBar()
        {
            if (FA.MGR.VisionMgr == null)
                return;
            if (!FA.MGR.VisionMgr.bIntialized)
                return;
            //[To do list]
            //             if (M.JV502LightMgr == null)
            //                 return;
            //             if (!M.VisionMgr.IsInitialzed())
            // 				return;
            //             if (!M.JV502LightMgr.IsConnected())
            //                 return;

            //scale factor
            toolStripStatusLableZoom.Text = string.Format("{0:D3}%", this.imageBoxEx_VisionOfPanel.Zoom);
            //toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.GetImageViewPort());
            //선택된 ROI 영역 표시
            toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);

        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (sender as CheckBox);
            chkbox.InvokeIfNeeded(() =>
            {
                if (FA.MGR.VisionMgr != null)
                {
                    int iTag = Convert.ToInt32((sender as CheckBox).Tag.ToString());

                    bool bUse = (sender as CheckBox).Checked;

                    if ((sender as CheckBox).Checked) (sender as CheckBox).ImageIndex = 1;
                    else (sender as CheckBox).ImageIndex = 0;

                    switch (iTag)
                    {
                        case (int)EzInaVision.GDV.eOption.RoiBoxVisible:
                            {
                                if (bUse)
                                {
                                    for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                                        ((ToolStripMenuItem)toolStripSplitButtonSelectRoi.DropDownItems[i]).Checked = false;

                                    imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.Rectangle;
                                }
                                else
                                {
                                    imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.None;
                                }

                                imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.RoiBoxVisible, bUse);



                            }
                            break;
                        case (int)EzInaVision.GDV.eOption.CrossLineVisible:
                            {
                                imageBoxEx_VisionOfPanel.SetOptionEx((int)EzInaVision.GDV.eOption.CrossLineVisible, bUse);
                                FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.CROSS, bUse);
                            }
                            break;
                        case (int)EzInaVision.GDV.eOption.FiltersVisible:
                            {
                                if (!IsExistsItemOfVision())
                                    return;
                                FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, bUse);
                            }
                            break;
                        case (int)EzInaVision.GDV.eOption.DisplayMatchResult:
                            {
                                if (!IsExistsItemOfVision())
                                    return;
                                FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.MATCH_RESULT, bUse);

                            }
                            break;
                        case (int)EzInaVision.GDV.eOption.DisplayBlobResult:
                            {
                                if (!IsExistsItemOfVision())
                                    return;
                                FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.BLOB_RESULT, bUse);

                            }
                            break;
                        case (int)EzInaVision.GDV.eOption.DisplayROIs:
                            {
                                if (!IsExistsItemOfVision())
                                    return;

                                FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.ROI, bUse);

                            }
                            break;

                        case 6: //Expose or illuminate
                            {

                                SetTrackbar(bUse);
                            }
                            break;
                    }//end of switch
                }//end of if 
            });

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

        protected string FormatPoint(Point point)
        {
            if (FA.MGR.VisionMgr == null)
                return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);

            if (FA.MGR.VisionMgr.bIntialized == true)
            {
                m_nGrayVal = FA.MGR.VisionMgr.GetLib(m_strVisionName).GetPixel(point);
            }

            return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);
        }

        private void imageBoxEx_VisionOfPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (FA.MGR.VisionMgr == null)
                return;

            if (FA.MGR.VisionMgr.bIntialized == false)
                return;
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
                UpdateCursorPosition(e.Location);
        }

        private void imageBoxEx_VisionOfPanel_Paint(object sender, PaintEventArgs e)
        {
            if (FA.MGR.VisionMgr == null)
                return;
#if !SIM
						if (FA.MGR.VisionMgr.bIntialized == false)
                return;
#endif
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            PointF pFanXY = imageBoxEx_VisionOfPanel.GetOffsetPoint(0, 0);//.Image.Width;
            SizeF sScale = imageBoxEx_VisionOfPanel.GetScaledSize(imageBoxEx_VisionOfPanel.Image.Width, imageBoxEx_VisionOfPanel.Image.Height);
            sScale.Width /= imageBoxEx_VisionOfPanel.Image.Width;
            sScale.Height /= imageBoxEx_VisionOfPanel.Image.Height;
            pFanXY.X /= sScale.Width;
            pFanXY.Y /= sScale.Height;


            #region [Camera Information]
            EzInaVision.GDV.stCamInfo stCamInfo = new EzInaVision.GDV.stCamInfo();
            stCamInfo.fGetFrameRate = FA.MGR.VisionMgr.GetCam(m_strVisionName).GetFrameRate();
            stCamInfo.bLive = FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive();
            stCamInfo.bIdle = FA.MGR.VisionMgr.GetCam(m_strVisionName).IsIdle();
            stCamInfo.bGrabbed = FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab();
            stCamInfo.bGrabbing = FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab();
            stCamInfo.fGetExposeTime = FA.MGR.VisionMgr.GetCam(m_strVisionName).GetExpose();
            stCamInfo.nGrayVal = m_nGrayVal;
            //stCamInfo.nLightSourceVal	= 0;
            #endregion [Camera Information]

            FA.MGR.VisionMgr.GetLib(m_strVisionName).Display(e.Graphics, sScale.Width, sScale.Height, pFanXY, stCamInfo);

        }

        private void imageBoxEx_VisionOfPanel_SelectionRegionChanged(object sender, EventArgs e)
        {
            if (FA.MGR.VisionMgr == null)
                return;

            if (FA.MGR.VisionMgr.bIntialized == false)
                return;
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);
        }
        private void gbtn_DataMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsExistsItemOfVision())
                    throw new Exception("Is not exists VisionMgr class");

                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive() == true)
                    throw new Exception("It's live, must turn off the live");

                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab() == false)
                    throw new Exception("It's not grabbed, must be grabbing first");
                List<EzInaVision.GDV.MatrixCodeResult> pMatrixCodeResultList;
                Control pControl = sender as Control;
                float fSpanRatio=(pControl==null||pControl.Tag==null)? 4.0f :
                    pControl.Tag.ToString()=="JIG"?2.0f:4.0f;
                FA.MGR.VisionMgr.GetLib(m_strVisionName).ClearMatrixCode1Results();
                FA.MGR.VisionMgr.GetLib(m_strVisionName).MatrixCode1MultiRun
                                        (
                                        (int)m_eRoiItem,
                                        new Rectangle[]
                                        {
                                        FA.MGR.VisionMgr.GetLib(m_strVisionName).GetRoiForInspection((int)m_eRoiItem)
                                        }
                                        , fSpanRatio);
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();

                FA.MGR.VisionMgr.GetLib(m_strVisionName).GetMatrixCode1ResultList(out pMatrixCodeResultList);
                if (pMatrixCodeResultList != null)
                {
                    string strDP = string.Empty;
                    foreach (EzInaVision.GDV.MatrixCodeResult pItem in pMatrixCodeResultList)
                    {
                        if (string.IsNullOrEmpty(strDP))
                        {
                            strDP = pItem.m_strDecodedString;
                        }
                        else
                        {
                            strDP = string.Format("{0}\n{1}\n", strDP, pItem.m_strDecodedString);
                        }
                    }
                    rtb_ResultTextDP.Text = strDP;
                }
                else
                {
                    rtb_ResultTextDP.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            try
            {
#if !SIM 
                if (!IsExistsItemOfVision())
                    throw new Exception("Is not exists VisionMgr class");
#endif 
                if (m_eGoldenImage == EzInaVision.GDV.eGoldenImages.None || m_eGoldenImage == EzInaVision.GDV.eGoldenImages.Max)
                    throw new Exception("Is not selected Golden image");
#if !SIM
                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive() == true)
                    throw new Exception("It's live, must turn off the live");

                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab() == false)
                    throw new Exception("It's not grabbed, must be grabbing first");
#else
                if (imageBoxEx_VisionOfPanel.Image == null)
                    return;
#endif 
                FA.MGR.VisionMgr.GetLib(m_strVisionName).ClearAllMatchResults();
                FA.MGR.VisionMgr.GetLib(m_strVisionName).MatchRun(

                                        FA.MGR.ProjectMgr.SelectedModelPath,
                                        m_eGoldenImage,
                                        m_eRoiItem);

                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();

            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }
        public void UpdateImageOfBW8()
        {

            Bitmap bmp = null;
            //if filters enable					
            bmp = ((EzInaVisionLibrary.VisionLibEuresys)FA.MGR.VisionMgr.GetLib(m_strVisionName)).ProcessImageConvertToBitmap();
            imageBoxEx_VisionOfPanel.Image = (Image)bmp.Clone();
            bmp.Dispose();
        }
        private void btnBlob_Click(object sender, EventArgs e)
        {
            try
            {
                //[To do list]
                //                 if (FA.MGR.RunMgr == null)
                //                     throw new Exception("The running class does not exist");

                if (!IsExistsItemOfVision())
                    throw new Exception("The vision class does not exist");

                if (m_eRoiItem == EzInaVision.GDV.eRoiItems.None || m_eRoiItem == EzInaVision.GDV.eRoiItems.Max)
                    throw new Exception("It's not selected Roi");

                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsLive() == true)
                    throw new Exception("It's live, must turn off the live");

                if (FA.MGR.VisionMgr.GetCam(m_strVisionName).IsGrab() == false)
                    throw new Exception("It's live, must turn off the live");

                //Modules.VisionMgr.GetLib(m_eVision).SetDefaultMethodOfBlob();
                //M.RunMgr.UpdateTask(RD.eTasks.PanelVisionInspection, false);

                //[To do list]
                //StopWatchTimer stwTimer = new StopWatchTimer();

                FA.MGR.VisionMgr.GetLib(m_strVisionName).BlobRun(m_eRoiItem);
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();


                //M.RunMgr.UpdateTask(RD.eTasks.PanelVisionInspection, true);
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
        }

        private void FrmVisionOfPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                splitContainer_Center.Panel1MinSize = 0;
                splitContainer_Center.Panel2MinSize = 0;

                ToolStripMenuItems_Init();
                CheckBox_Items_Init();

                if (!bIsOperation)
                {
                    toolStripSplitButtonGoldenImages.Enabled = false;
                    toolStripSplitButtonSelectRoi.Enabled = false;
                    toolStripStatusLabel_ShowMenu.Visible = false;
                    toolStripStatusLabel_HIdeMenu.Visible = false;

                }
                else
                {
                    toolStripSplitButtonGoldenImages.Enabled = true;
                    toolStripSplitButtonSelectRoi.Enabled = true;
                    toolStripStatusLabel_ShowMenu.Visible = true;
                    toolStripStatusLabel_HIdeMenu.Visible = true;
                }
                //splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 60 + 1;
                splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 1;


                //m_dPreviousMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
                //m_dPreviousMY = AXIS.Y.Status().m_stPositionStatus.fActPos;
            }
            else
            {
                //                 if (m_eVision == GDV.eVision.Panel)
                //                     DeleteEvent();
                //                 else if (m_eVision == GDV.eVision.Block)
                //                     DeleteEventBlock();
            }

        }

        private void toolStripStatusLabel_ShowMenu_Click(object sender, EventArgs e)
        {
            //             if (Modules.RunMgr.eRunMode != RD.eRunMode.Stop)
            //                 return;
            splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 150 + 1;
            FA.MGR.VisionMgr.TreeView_Init(treeView_GoldenImageList);

        }

        private void toolStripStatusLabel_HIdeMenu_Click(object sender, EventArgs e)
        {
            //[To do list]
            //             if (FA.MGR.RunMgr.eRunMode != RD.eRunMode.Stop)
            //                 return;

            splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 1;

        }

        private void SetTrackbar(bool a_bExpose)
        {
            if (a_bExpose)
            {
                trackBar_Value.Minimum = 100;
                trackBar_Value.Maximum = Convert.ToInt32(5 * 1000);
                trackBar_Value.TickFrequency = 10000;
                trackBar_Value.LargeChange = 100;
                trackBar_Value.SmallChange = 10;
                trackBar_Value.Value = 100;
            }
            else
            {
                trackBar_Value.Minimum = 2;
                trackBar_Value.Maximum = 255;
                trackBar_Value.TickFrequency = 25;
                trackBar_Value.LargeChange = 2;
                trackBar_Value.SmallChange = 1;
                trackBar_Value.Value = 2;
            }

            lbl_TrackbarValue.Text = "0";

        }

        private void trackBar_Value_Scroll(object sender, EventArgs e)
        {
            lbl_TrackbarValue.Text = "" + trackBar_Value.Value;
            if (IsExistsItemOfVision())
                FA.MGR.VisionMgr.GetCam(m_strVisionName).SetExposeTime(trackBar_Value.Value);
        }

        private bool IsExistsItemOfVision()
        {
            if (FA.MGR.VisionMgr == null)
                return false;
            if (FA.MGR.VisionMgr.bIntialized == false)
                return false;
            if (FA.MGR.VisionMgr.GetCam(m_strVisionName) == null)
                return false;
            if (FA.MGR.VisionMgr.GetLib(m_strVisionName) == null)
                return false;

            return true;

        }

        public void BlobDisplay(bool a_bOk)
        {
            //             if(a_bOk)
            //             {
            //                 label_VisionInfor.ForeColor = Color.Gold;
            //                 label_VisionInfor.Text = "OK";
            //             }
            //             else
            //             {
            //                 label_VisionInfor.ForeColor = Color.Red;
            //                 label_VisionInfor.Text = "NG";
            //             }
        }

        private void imageBoxEx_VisionOfPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            m_listPtBegin.Clear();
            m_listPtEnd.Clear();
            m_listStrDist.Clear();
        }

        private void treeView_GoldenImageList_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (m_SelectedTreeNode.Tag != null)
            {
                SelectNode((EzInaVision.GDV.eGoldenImages)(int)m_SelectedTreeNode.Tag);
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
            PictureBox_GoldenImage.SizeMode = PictureBoxSizeMode.Zoom;
            string strLoadImage = string.Format(@"{0}\{1}_{2}.bmp"
            , FA.MGR.ProjectMgr.SelectedModelPath
            , m_strVisionName
            , ((EzInaVision.GDV.eGoldenImages)(int)m_SelectedTreeNode.Tag).ToString());


            System.IO.FileInfo f = new System.IO.FileInfo(strLoadImage);
            if (f.Exists)
            {
                using (var fs = new System.IO.FileStream(strLoadImage, System.IO.FileMode.Open))
                {
                    using (Bitmap bmp = new Bitmap(fs))
                    {
                        if (bmp != null)
                            PictureBox_GoldenImage.Image = (Image)bmp.Clone();
                    }
                }
            }
            else
            {
                PictureBox_GoldenImage.SizeMode = PictureBoxSizeMode.CenterImage;
                PictureBox_GoldenImage.Image = PictureBox_GoldenImage.ErrorImage;
            }
            #endregion [Golden Image Display]

        }

        private void treeView_GoldenImageList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            SelectNode((EzInaVision.GDV.eGoldenImages)(int)e.Node.Tag);
        }

        private void chkbox_ClickMove_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                return;

            switch ((sender as Control).Name)
            {
                case "chkbox_ClickMove":
                    chkbox_ClickMove.ImageIndex = chkbox_ClickMove.Checked ? 1 : 0;
                    if (chkbox_ClickMove.Checked)
                    {
                        m_dPreviousMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
                        m_dPreviousMY = AXIS.Y.Status().m_stPositionStatus.fActPos;
                    }


                    break;
            }
        }

        private void glassButton_PreviousMove_Click(object sender, EventArgs e)
        {
            if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
                return;
            if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
                return;

            switch ((sender as Control).Name)
            {
                case "glassButton_PreviousMove":
                    if (chkbox_ClickMove.Checked)
                    {
                        if (MsgBox.Confirm("{0}\n{1:F4},{2:F4}", "Would you like to move the previous position??", m_dPreviousMX, m_dPreviousMY))
                        {
                            FA.ACT.MoveABS(FA.DEF.eAxesName.RX, m_dPreviousMX, Motion.GDMotion.eSpeedType.FAST);
                            FA.ACT.MoveABS(FA.DEF.eAxesName.Y, m_dPreviousMY, Motion.GDMotion.eSpeedType.FAST);
                        }
                    }
                    break;
            }
        }

        public void UpdateImageBoxEx()
        {
            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
           {
               imageBoxEx_VisionOfPanel.Invalidate();
               imageBoxEx_VisionOfPanel.Update();
           });
        }

        public bool IsRoiBoxVisble
        {
            get { return imageBoxEx_VisionOfPanel.IsVisibleBoxOfROI(); }
        }
        public Rectangle rectRoiBox
        {
            get { return Rectangle.Round(imageBoxEx_VisionOfPanel.SelectionRegion); }
        }



    }//end of wafer
}//end of namespace
