using Cyotek.Windows.Forms;
using EzIna.FA;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmImgFindModoelOfPanel : Form
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

        string m_strLibName;
        #endregion
        #region [ Point ]

        private List<string> m_listStrDist = new List<string>();       
        #endregion

   
        public bool bIsOperation { get; set; } //메뉴 화면 축소 확대.
        TreeNode m_SelectedTreeNode = new TreeNode();
        System.Windows.Forms.Timer tmr = null;

        public FrmImgFindModoelOfPanel(string a_strLibName)
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            m_strLibName = a_strLibName;
            bIsOperation = true;           
            //imageBoxEx_VisionOfPanel.Paint += new PaintEventHandler(imageBox_Paint);

        }
        #region [ Measure Points ]             
        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            if (imageBoxEx_VisionOfPanel.Image != null)
            {               
                       
            }
            //Draw begin points                  
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

        private void Frm_Load(object sender, EventArgs e)
        {

            imageBoxEx_VisionOfPanel.MinimumSelectionSize = new Size(8, 8);
            imageBoxEx_VisionOfPanel.SelectionMode = ImageBoxSelectionMode.None;
            #region Button event
       
            #endregion

            #region Timer for display
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 50;
            tmr.Tick += new EventHandler(Display);
            tmr.Start();
            #endregion

            #region checkbox option
         
            #endregion

        }
        private void CheckBox_Items_Init()
        {
            if (!IsExistsItemOfVision())
                return;

            //[To do list] 옵션 추가할것.           
            imageBoxEx_VisionOfPanel.SetOptionEx((int)ImageBoxEx.eDef_Option.CrossLineVisible, false);        
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

                case eTagOfButton.Center:
                    imageBoxEx_VisionOfPanel.RoiMoveToCenterOfImageEx();
                    break;
                case eTagOfButton.Apply:
                    {

                    }
                    break;
                case eTagOfButton.Save:
                    {

                    }
                    break;
            }
        }

        private void Display(object sender, EventArgs e)
        {

            try
            {
                tmr.Stop();
                this.UpdateStatusBar();
                tmr.Enabled = this.Visible;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                tmr.Enabled = this.Visible;
            }
        }

        private void UpdateStatusBar()
        {
            if (FA.MGR.VisionMgr == null)
                return;
            if (!FA.MGR.VisionMgr.bLIBInitialized)
                return;

            //scale factor
            toolStripStatusLableZoom.Text = string.Format("{0:D3}%", this.imageBoxEx_VisionOfPanel.Zoom);
            //toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.GetImageViewPort());
            //선택된 ROI 영역 표시
            if(imageBoxEx_VisionOfPanel.GetOptionEx((int)ImageBoxEx.eDef_Option.RoiVisible))
            toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);
          
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (sender as CheckBox);
            chkbox.InvokeIfNeeded(() =>
            {

                int iTag = Convert.ToInt32((sender as CheckBox).Tag.ToString());
               
                bool bUse = (sender as CheckBox).Checked;

                if ((sender as CheckBox).Checked) (sender as CheckBox).ImageIndex = 1;
                else (sender as CheckBox).ImageIndex = 0;

                switch (chkbox.Name)
                {                                      
                    case "chkbox_CrossLine":
                        {
                            imageBoxEx_VisionOfPanel.SetOptionEx((int)ImageBoxEx.eDef_Option.CrossLineVisible, bUse);                            
                            //FA.MGR.VisionMgr.GetLib(m_strVisionName).SetOption(EzInaVision.GDV.eLibOption.CROSS, bUse);
                        }
                        break;
                  


                }//end of switch
            });

        }
        protected string FormatRectangle(RectangleF rect)
        {
            return string.Format("X:{0:D4}, Y:{1:D4}, W:{2:D4}, H:{3:D4}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
        protected string FormatLine(Point pSt,Point pED)
        {
            return string.Format("ST_X:{0:D4}, ST_Y:{1:D4}, ED_X:{2:D4}, ED_Y:{3:D4} , L:{4:F3}",
              pSt.X, pSt.Y, pED.X, pED.Y, Math.Abs(Utils.PointToPointDistance(pSt, pED)));
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


            if (imageBoxEx_VisionOfPanel.Image == null)
                return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);

            // m_nGrayVal = FA.MGR.VisionMgr.GetLib(m_strVisionName).GetPixel(point);


            return string.Format("X:{0:D4}, Y:{1:D4}", point.X, point.Y);
        }

        private void imageBoxEx_VisionOfPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            if (imageBoxEx_VisionOfPanel.IsPointInImage(e.Location))
                UpdateCursorPosition(e.Location);
        }
        private void imageBoxEx_VisionOfPanel_Paint(object sender, PaintEventArgs e)
        {
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            PointF pFanXY = imageBoxEx_VisionOfPanel.GetOffsetPoint(0, 0);//.Image.Width;
            PointF ptBegin=PointF.Empty;
            PointF ptEnd = PointF.Empty;
            SizeF sScale = imageBoxEx_VisionOfPanel.GetScaledSize(imageBoxEx_VisionOfPanel.Image.Width, imageBoxEx_VisionOfPanel.Image.Height);
            float fZoomScale = (float)imageBoxEx_VisionOfPanel.ZoomFactor;
            sScale.Width /= imageBoxEx_VisionOfPanel.Image.Width;
            sScale.Height /= imageBoxEx_VisionOfPanel.Image.Height;
            pFanXY.X /= sScale.Width;
            pFanXY.Y /= sScale.Height;
            EzInaVision.VisionLibBaseClass pLib = MGR.VisionMgr.GetLib(m_strLibName);
            if(pLib!=null)
            {
                pLib.DrawFindLearnedModelGeometry(e.Graphics, sScale.Width, sScale.Height, pFanXY);
            }                             
        }        
        private void imageBoxEx_VisionOfPanel_SelectionRegionChanged(object sender, EventArgs e)
        {
            if (imageBoxEx_VisionOfPanel.Image == null)
                return;

            if (imageBoxEx_VisionOfPanel.GetOptionEx((int)ImageBoxEx.eDef_Option.RoiVisible))
                toolStripStatusLabelSelection.Text = this.FormatRectangle(imageBoxEx_VisionOfPanel.SelectionRegion);
           
        }

        private void Frm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                splitContainer_Center.Panel1MinSize = 0;
                splitContainer_Center.Panel2MinSize = 0;


                CheckBox_Items_Init();

                if (!bIsOperation)
                {
                    toolStripStatusLabel_ShowMenu.Visible = false;
                    toolStripStatusLabel_HIdeMenu.Visible = false;

                }
                else
                {
                    toolStripStatusLabel_ShowMenu.Visible = true;
                    toolStripStatusLabel_HIdeMenu.Visible = true;
                }
           
                splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 1;
           
            }
            else
            {
           
            }

        }
        private void toolStripStatusLabel_ShowMenu_Click(object sender, EventArgs e)
        {
            //             if (Modules.RunMgr.eRunMode != RD.eRunMode.Stop)
            //                 return;
            splitContainer_Center.SplitterDistance = splitContainer_Center.Height -63 + 1;
        }

        private void toolStripStatusLabel_HIdeMenu_Click(object sender, EventArgs e)
        {
            //[To do list]
            //             if (FA.MGR.RunMgr.eRunMode != RD.eRunMode.Stop)
            //                 return;
            splitContainer_Center.SplitterDistance = splitContainer_Center.Height - 1;
        }
        private bool IsExistsItemOfVision()
        {
            if (this.imageBoxEx_VisionOfPanel.Image == null)
                return false;
            return true;
        }
        private void imageBoxEx_VisionOfPanel_MouseWheel(object sender, MouseEventArgs e)
        {        
            m_listStrDist.Clear();
        }
        public void UpdateImageBoxEx()
        {
            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
           {
               imageBoxEx_VisionOfPanel.Invalidate();
               imageBoxEx_VisionOfPanel.Update();
           });
        }
        public void SetImage(Image a_IMG)
        {
            if (a_IMG == null)
                return;
           
            imageBoxEx_VisionOfPanel.InvokeIfNeeded(() =>
            {
                Image Img = imageBoxEx_VisionOfPanel.Image;
                imageBoxEx_VisionOfPanel.Image = a_IMG;
                if (Img != null)
                    Img.Dispose();
               
                EzInaVision.VisionLibBaseClass pLib = MGR.VisionMgr.GetLib(m_strLibName);
                pLib.FIND_TeachingPattern((Image)a_IMG);
                imageBoxEx_VisionOfPanel.Invalidate();
                imageBoxEx_VisionOfPanel.Update();
            });
        }
        public void DisposeImage()
        {
            if(imageBoxEx_VisionOfPanel.Image!=null)
            {
                imageBoxEx_VisionOfPanel.Image.Dispose();
            }
        }                
        public Size ImageSize
        {
            get {
                if (this.imageBoxEx_VisionOfPanel.Image != null)
                    return this.imageBoxEx_VisionOfPanel.Image.Size;

                return new Size(0, 0);
                }
        }
        public Image ImageBuffer
        {
            get { return imageBoxEx_VisionOfPanel.Image; }
        }
      
        public void ZoomToFit()
        {
            imageBoxEx_VisionOfPanel.ZoomToFit();
        }       
    }//end of wafer
}//end of namespace
