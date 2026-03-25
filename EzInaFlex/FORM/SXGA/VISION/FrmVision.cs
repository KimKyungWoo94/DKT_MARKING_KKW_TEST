using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzInaVision;
using System.Collections.Concurrent;

namespace EzIna
{

    public partial class FrmVision : Form
    {
        string m_strSelectedVision = "FINE";

        public FrmVision()
        {
            InitializeComponent();

            this.ControlBox = false;

            doubleClickTimer.Interval = 100;
            doubleClickTimer.Tick += new EventHandler(doubleClickTimer_Tick);

            btnSelectFineVision.Click += ChangeVision;
            btnSelectCoarseVision.Click += ChangeVision;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (FA.MGR.VisionMgr == null)
                {
                    EzInaVisionLibrary.VisionLibEuresys.InitializeDriver();
                    EzInaVisionMultiCam.GrabLinkCam.InitializeDriver();
                    FA.MGR.VisionMgr = new VisionManager();

                    foreach (EzInaVision.VisionCamBaseClass item in EzInaVisionMultiCam.GrabLinkCam.m_vecCameras)
                    {
                        FA.MGR.VisionMgr.AddItem(item);
                    }

                    foreach (EzInaVision.VisionLibBaseClass item in EzInaVisionLibrary.VisionLibEuresys.m_vecLibraries)
                    {
                        FA.MGR.VisionMgr.AddItem(item);
                    }


                    foreach (EzInaVision.VisionCamBaseClass cam in FA.MGR.VisionMgr.vecCameras)
                    {
                        foreach (EzInaVision.VisionLibBaseClass lib in FA.MGR.VisionMgr.vecLibraries)
                        {
                            if (cam.m_stCamInfo.strCameraName.Equals(lib.m_LibInfo.m_stLibInfo.strName))
                            {
                                if (cam.GetType() == typeof(EzInaVisionMultiCam.GrabLinkCam))
                                {
                                    FA.MGR.dicFrmVisions.AddOrUpdate(lib.m_LibInfo.m_stLibInfo.strName
                                    , new EzIna.FrmVisionOfPanel(lib.m_LibInfo.m_stLibInfo.strName)
                                    , (k, v) => new EzIna.FrmVisionOfPanel(lib.m_LibInfo.m_stLibInfo.strName));

                                    if (lib.GetType() == typeof(EzInaVisionLibrary.VisionLibEuresys))
                                    {
                                        ((EzInaVisionMultiCam.GrabLinkCam)cam).OnGrabbedImageEvent += ((EzInaVisionLibrary.VisionLibEuresys)lib).OnGrabbedImage;

                                        EzIna.FrmVisionOfPanel item = null;

                                        FA.MGR.dicFrmVisions.TryGetValue(lib.m_LibInfo.m_stLibInfo.strName, out item);

                                        if (item != null)
                                        {
                                            ((EzInaVisionLibrary.VisionLibEuresys)lib).OnDisplayEvent += item.OnDisplay;
                                            ((EzInaVisionLibrary.VisionLibEuresys)lib).OnMatchDisplayEvent += item.OnMatchDisplay;
                                            ((EzInaVisionLibrary.VisionLibEuresys)lib).OnBlobDisplayEvent += item.OnBlobDisplay;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MsgBox.Error(exc.ToString());
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //[To do list]
            // 			if(ANI.FA.MGR.VisionCamFine() != null)
            // 			{
            // 				if (FA.MGR.VisionCamFine().GetType() == typeof(EzInaVisionMultiCam.GrabLinkCam))
            // 				{
            // 					FA.MGR.VisionCamFine().Idle();
            // 				}
            // 			}

            FA.MGR.VisionMgr.Idle();

            //             while(FA.MGR.VisionMgr.IsLive())
            //             {
            //                 FA.MGR.VisionMgr.Idle();
            //             }





            //EzInaVisionLibrary.VisionLibEuresys.TerminateDriver();
            //EzInaVisionMultiCam.GrabLinkCam.TerminateDriver();

            FA.MGR.VisionMgr.Terminate();

        }

        private void btn_Frm_Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_Frm_Max_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btn_Frm_Normalize.BringToFront();
        }

        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        #region [화면(최소,최대) Resize Event]
        ///<see cref ="https://docs.microsoft.com/ko-kr/dotnet/framework/winforms/how-to-distinguish-between-clicks-and-double-clicks"/>

        private Rectangle hitTestRectangle = new Rectangle();
        private Rectangle doubleClickRectangle = new Rectangle();
        private TextBox textBox1 = new TextBox();
        private Timer doubleClickTimer = new Timer();

        private bool isFirstClick = true;
        private bool isDoubleClick = false;
        private int milliseconds = 0;



        private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            WinAPIs.ReleaseCapture();
            WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);

            //             if (!hitTestRectangle.Contains(e.Location))
            //             {
            //                 return;
            //             }

            // This is the first mouse click.
            if (isFirstClick)
            {
                isFirstClick = false;

                // Determine the location and size of the double click
                // rectangle area to draw around the cursor point.
                doubleClickRectangle = new Rectangle(
                  e.X - (SystemInformation.DoubleClickSize.Width / 2),
                  e.Y - (SystemInformation.DoubleClickSize.Height / 2),
                  SystemInformation.DoubleClickSize.Width,
                  SystemInformation.DoubleClickSize.Height);
                //Invalidate();

                // Start the double click timer.
                doubleClickTimer.Start();
            }

            // This is the second mouse click.
            else
            {
                // Verify that the mouse click is within the double click
                // rectangle and is within the system-defined double
                // click period.
                if (doubleClickRectangle.Contains(e.Location) &&
                  milliseconds < SystemInformation.DoubleClickTime)
                {
                    isDoubleClick = true;
                }
            }

            if (isDoubleClick)
            {
                WindowState = FormWindowState.Maximized;
                btn_Frm_Normalize.BringToFront();
            }
            else
            {
                WindowState = FormWindowState.Normal;
                btn_Frm_Max.BringToFront();
            }
        }

        void doubleClickTimer_Tick(object sender, EventArgs e)
        {
            milliseconds += 100;
            // The timer has reached the double click time limit.
            if (milliseconds >= SystemInformation.DoubleClickTime)
            {
                doubleClickTimer.Stop();
                // Allow the MouseDown event handler to process clicks again.
                isFirstClick = true;
                isDoubleClick = false;
                milliseconds = 0;
            }
        }
        #endregion

        private void btn_Frm_Normalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btn_Frm_Max.BringToFront();
        }

        private void AttachVision(string a_strName)
        {
            EzIna.FrmVisionOfPanel item = null;
            EzIna.FA.MGR.dicFrmVisions.TryGetValue(a_strName.ToUpper(), out item);
            if (item != null)
            {
                if (item.Visible == false)
                {
                    if (panel_Vision.Controls.Contains(item) == false)
                    {
                        panel_Vision.Controls.Add(item);
                        item.Show();

                    }
                }
            }
        }

        private void DetachVisions()
        {
            foreach (EzIna.FrmVisionOfPanel item in panel_Vision.Controls)
            {
                item.Hide();
            }

            if (panel_Vision.Controls.Count > 0)
            {
                panel_Vision.Controls.Clear();
            }
        }
        //private void Add
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                AttachVision(m_strSelectedVision.ToUpper());
                
            }
            else
            {
                DetachVisions();
            }
            // 			if(Visible)
            // 			{
            // 				#region Vision
            // 				
            // 
            // 				FA.MGR.dicFrmVisions
            // 				if (FA.MGR.pFrmVisionOfFine != null)
            // 				{
            // 					FA.MGR.pFrmVisionOfFine.bIsOperation = true;
            // 					if (!FA.MGR.pFrmVisionOfFine.Visible)
            // 					{
            // 						if (panel_Vision.Controls.Contains(FA.MGR.pFrmVisionOfFine) == false)
            // 						{
            // 							panel_Vision.Controls.Add(FA.MGR.pFrmVisionOfFine);
            // 							//[To do list]
            // 							//FA.MGR.pFrmVisionOfFine.OnUpdateGoldenImageEvent += UpdateGoldenImages;
            // 							FA.MGR.pFrmVisionOfFine.Show();
            // 						}
            // 					}
            // 				}
            // 
            // 				if (M.pFrmVisionOfCoarse != null)
            // 				{
            // 					M.pFrmVisionOfCoarse.bIsOperation = true;
            // 					if (!M.pFrmVisionOfCoarse.Visible)
            // 					{
            // 						if (panelVisionOfAlignment.Controls.Contains(M.pFrmVisionOfCoarse) == false)
            // 						{
            // 							panelVisionOfAlignment.Controls.Add(M.pFrmVisionOfCoarse);
            // 							M.pFrmVisionOfCoarse.CreateFineEvent(UpdateGoldenImages);
            // 							M.pFrmVisionOfCoarse.Show();
            // 						}
            // 					}
            // 				}
            // 				#endregion
            // 			}
            // 			else
            // 			{
            // 
            // 			}
        }

        private void ChangeVision(object a_obj, EventArgs a_e)
        {

            switch ((a_obj as Button).Tag.ToString().ToUpper())
            {
                case "FINE":
                    DetachVisions();
                    m_strSelectedVision = "FINE";
                    AttachVision(m_strSelectedVision.ToUpper());
                    break;
                case "COARSE":
                    DetachVisions();
                    m_strSelectedVision = "COARSE";
                    AttachVision(m_strSelectedVision.ToUpper());
                    break;
            }

        }

        private void btnSelectFineVision_Click(object sender, EventArgs e)
        {


        }
    }
}
