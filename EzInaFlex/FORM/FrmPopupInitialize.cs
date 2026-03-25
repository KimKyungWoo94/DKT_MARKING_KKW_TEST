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
using EzInaGui;
using EzIna.UserThread;

namespace EzIna
{
    public partial class FrmPopupInitialize : Form
    {
        private int m_nStep = 0;
        Timer m_Timer = null;
        public bool bInit { get; set; }

        public FrmPopupInitialize()
        {
            InitializeComponent();
            try
            {
                m_Timer = new Timer();
                m_Timer.Interval = 500;
                m_Timer.Tick += new EventHandler(timerInit_Tick);
                bInit = false;
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            try
            {

                int nProgressRate = 0;
                int nTotalSteps = (int)FA.DEF.eMoudleName.MAX;
                nProgressRate = (int)((double)m_nStep / (double)nTotalSteps * 100.0);

                lblLoading.Text = Convert.ToString(nProgressRate) + "%";

                switch ((FA.DEF.eMoudleName)m_nStep)
                {
                    #region MIN
                    case FA.DEF.eMoudleName.MIN:
                        {
                            if (bInit)
                            {
                                ucProcessProgressBar_Initialize.Start();
                                listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                                listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                                m_nStep++;
                            }
                            else
                            {
                                ucProcessProgressBar_Initialize.Stop();
                                listBox_Data.Items.Clear();
                                m_nStep = -1;
                                m_Timer.Enabled = false;

                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                        }
                        break;
                    #endregion MIN
                    #region THREAD
                    case FA.DEF.eMoudleName.THREAD:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.ThreadMgr = null;
                                FA.MGR.ThreadMgr = new UserThread.ThreadManager();
                                m_nStep++;
                            }
                            else
                            {

                                if (FA.MGR.ThreadMgr != null)
                                {
                                    FA.MGR.ThreadMgr.Terminate();
                                    FA.MGR.ThreadMgr = null;
                                }
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion THREAD
                    #region VISION
                    case FA.DEF.eMoudleName.VISION:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {

                                EzInaVisionLibrary.VisionLibEuresys.InitializeDriver();
                                EzInaVisionMultiCam.GrabLinkCam.InitializeDriver();
                                FA.MGR.VisionMgr = null;
                                FA.MGR.VisionMgr = new VisionManager();


                                //add camera
                                foreach (EzInaVision.VisionCamBaseClass item in EzInaVisionMultiCam.GrabLinkCam.m_vecCameras)
                                {
                                    FA.MGR.VisionMgr.AddItem(item);
                                }
                                //add library
                                foreach (EzInaVision.VisionLibBaseClass item in EzInaVisionLibrary.VisionLibEuresys.m_vecLibraries)
                                {
                                    FA.MGR.VisionMgr.AddItem(item);
                                }
                                //FA.MGR.VisionMgr.vecLibraries[0].GaugeCalibrationModelLoad(FA.FILE.VisionCalbIMG,5,5);																
                                foreach (EzIna.FA.DEF.eROI_CUSTOM item in Enum.GetValues(typeof(EzIna.FA.DEF.eROI_CUSTOM)))
                                {
                                    FA.MGR.VisionMgr.vecLibraries[0].AddRoiForInspection((int)item);
                                }
                                //FA.MGR.VisionMgr.vecLibraries[0].SetOption(EzInaVision.GDV.eLibOption.ENABLE_CALIBRATION,true);
                                //attach form
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
                                                        ((EzInaVisionLibrary.VisionLibEuresys)lib).OnMatrixCode1DisplayEvent += item.OnMatrixCode1Display;

                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }

                                FA.MGR.VisionToScannerCalb = new Cam.Transfomations();
                                FA.MGR.VisionToScannerCalb.LoadCalibrationFile(FA.DIR.CFG + "Vision\\VisionCalibrationData.csv");
                                m_nStep++;

                            }
                            else
                            {
                                if (FA.MGR.VisionMgr != null)
                                {
                                    FA.MGR.VisionMgr.Idle();
                                    FA.MGR.VisionMgr.Terminate();
                                    FA.MGR.VisionMgr = null;
                                }
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion VISION
                    #region IO
                    case FA.DEF.eMoudleName.IO:
                        {

                            m_Timer.Enabled = false;

                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;

                            if (bInit)
                            {

                                FA.MGR.IOMgr = EzIna.IO.IOManager.Instance;
                                FA.MGR.IOMgr.LoadIO();
                                MF.TOWERLAMP.Init(FA.FILE.InitProcTowerNBuzzer);
                                MF.TOWERLAMP.DoThreadProc = FA.SYS.DoTowerLamp;

                                // 								FA.MGR.DIOMgr = null;
                                // 								FA.MGR.DIOMgr = new DIOManager();
                                //                                 FA.MGR.DIOMgr.OpenDevice();
                                // 								FA.MGR.DIOMgr.LoadItem(GDIO.eDIO_Type.INPUT);
                                // 								FA.MGR.DIOMgr.LoadItem(GDIO.eDIO_Type.OUTPUT);
                                // 								FA.MGR.DIOMgr.GetItem(GDIO.eDIO_Type.INPUT).Initialize();
                                // 								FA.MGR.DIOMgr.GetItem(GDIO.eDIO_Type.OUTPUT).Initialize();

                                m_nStep++;
                                m_Timer.Enabled = true;
                            }
                            else
                            {
                                //FA.MGR.IOMgr.SaveIO();
                                FA.MGR.IOMgr.Dispose();
                                //FA.MGR.DIOMgr = null;
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion IO
                    #region LASER
                    case FA.DEF.eMoudleName.Laser:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.LaserMgr = EzIna.Laser.LaserManager.Instance;
                                FA.MGR.LaserMgr.OpenDecive();
                                FA.MGR.LaserMgr.OpenPowerTableDataes();
                                m_nStep++;
                            }
                            else
                            {
                                FA.MGR.LaserMgr.Dispose();
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion LASER
                    #region ATTENUATOR
                    case FA.DEF.eMoudleName.Attenuator:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.AttenuatorMgr = Attenuator.AttenuatorManager.Instance;
                                FA.MGR.AttenuatorMgr.OpenDecive();
                                m_nStep++;
                                m_Timer.Enabled = true;
                            }
                            else
                            {
                                FA.MGR.AttenuatorMgr.Dispose();
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion ATTENUATOR
                    #region PowerMeter
                    case FA.DEF.eMoudleName.PowerMeter:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.POMgr = PowerMeter.PowerMeterManager.Instance;
                                FA.MGR.POMgr.OpenDecive();
                                m_nStep++;

                            }
                            else
                            {
                                FA.MGR.POMgr.Dispose();
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion PowerMeter
                    #region LIGHT
                    case FA.DEF.eMoudleName.Light:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.LightMgr = EzIna.Light.LightManager.Instance;
                                FA.MGR.LightMgr.OpenDecive();



                                m_nStep++;

                            }
                            else
                            {
                                FA.MGR.LightMgr.Dispose();
                                m_nStep--;

                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion LIGHT
                    #region MOTION
                    case FA.DEF.eMoudleName.MOTION:
                        {
                            m_Timer.Enabled = false;

                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;

                            if (bInit)
                            {
                                FA.MGR.MotionMgr = null;
                                FA.MGR.MotionMgr = new Motion.MotionManager();

                                //FA.MGR.MotionMgr.TestFileSave();
                                FA.MGR.MotionMgr.OpenDevice();
                                //선언된 객체 호출.
                                //FA.MGR.MotionMgr.GetItem(0)
                                //static 선언되어 직접호출.
                                //EzIna.Motion.CMotionA3200.GetTaskState_Enum;
                                m_nStep++;
                            }
                            else
                            {
                                if (FA.MGR.MotionMgr != null)
                                {
                                    FA.MGR.MotionMgr.Terminate();
                                    FA.MGR.MotionMgr = null;
                                }
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion MOTION                   
                    #region Scanner
                    case FA.DEF.eMoudleName.SCANNER:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;

                            if (bInit)
                            {
                                FA.MGR.RTC5Mgr = EzIna.Scanner.ScanlabRTCMgr.Instance;
                                FA.MGR.RTC5Mgr.OpenDecive();
                                m_nStep++;
                            }
                            else
                            {
                                if (FA.MGR.RTC5Mgr != null)
                                {
                                    FA.MGR.RTC5Mgr.Dispose();
                                    EzIna.Scanner.ScanlabRTC5.TerminateDriver();
                                    FA.MGR.RTC5Mgr = null;
                                }
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion Scanner                   
                    #region RUN
                    case FA.DEF.eMoudleName.RUN:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                FA.MGR.RunMgr = null;
                                FA.MGR.RunMgr = new RunningManager();
                                FA.MGR.RunMgr.ConnectingModules();

                                // if(FA.SYS.MotionMgr != null)
                                // {
                                //     MotionAero Aero = (MotionAero)FA.SYS.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
                                //     if(Aero != null)
                                //         FA.MGR.RunMgr.CreateEvent(Aero.OnAeroStatusUtility);
                                // }
                                FA.MGR.ProjectMgr = null;
                                FA.MGR.ProjectMgr = new ProjectManager();
                                FA.MGR.ProjectMgr.ProjectOpen();

                                FA.MGR.RecipeRunningData = EzIna.RunningDataManager.Instance;
                                FA.MGR.DMGenertorMgr = EzIna.DataMatrix.DMGenerater.Instance;
                                m_nStep++;

                            }
                            else
                            {
                                // FA.MGR.RunMgr = null;
                                FA.MGR.ProjectMgr = null;
                                // 	FA.MGR.ChipMgr.DeleteItems();
                                // 	FA.MGR.ChipMgr = null;
                                // 	FA.SYS.DigitalMgr = null;
                                FA.MGR.RecipeRunningData = null;
                                FA.MGR.DMGenertorMgr = null;
                                m_nStep--;


                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion RUN
                    #region GUI
                    case FA.DEF.eMoudleName.GUI:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                // 
                                FA.MGR.GuiMgr = null;
                                FA.MGR.GuiMgr = new GUI.CGuiManager();
                                m_nStep++;


                            }
                            else
                            {
                                FA.MGR.GuiMgr = null;
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion GUI                
                    #region Recipe
                    case FA.DEF.eMoudleName.RECIPE:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                // 
                                FA.MGR.RecipeMgr = RecipeManager.Instance;
                                m_nStep++;


                            }
                            else
                            {
                                FA.MGR.RecipeMgr = null;
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion Recipe      
                    #region MES
                    case FA.DEF.eMoudleName.MES:
                        {
                            m_Timer.Enabled = false;
                            listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                            listBox_Data.SelectedIndex = listBox_Data.Items.Count - 1;
                            if (bInit)
                            {
                                // 
                                FA.MGR.MESMgr = DKT_MES_Manager.Instance;

                                //FA.MGR.MESMgr.DoConnect();

                                m_nStep++;
                            }
                            else
                            {
                                FA.MGR.MESMgr = null;
                                m_nStep--;
                            }
                            m_Timer.Enabled = true;
                        }
                        break;
                    #endregion MES    
                    #region MAX
                    case FA.DEF.eMoudleName.MAX:
                        {
                            if (bInit)
                            {
                                //ACT.Init();
                                //TC.Init();
                                //TC.allocSeries((int)RD.eSeries.Type_Max, 1, 10 * 1000); //5sec / Thread Sampling time

                                //                                 if(FA.MGR.MotionMgr != null)
                                //                                 {
                                //                                     MotionAero Aero = (MotionAero)FA.MGR.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH,(int)GDMotion.eMotorNameAero.M_U);
                                //                                     if(Aero != null)
                                //                                     {
                                //                                         Aero.CreateEvent_For_Tasks(FA.MGR.RunMgr.UpdateAeroTasksInofr);
                                //                                     }
                                //                                 }

                                ucProcessProgressBar_Initialize.Stop();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                                m_nStep = -1;
                                m_Timer.Enabled = false;
                            }
                            else
                            {
                                //TC.freeSeries();
                                m_Timer.Enabled = false;
                                ucProcessProgressBar_Initialize.Start();
                                listBox_Data.Items.Add(FA.DEF.GetInitializeString((FA.DEF.eMoudleName)m_nStep, bInit));
                                m_nStep--;
                                m_Timer.Enabled = true;
                            }
                        }
                        break;
                        #endregion MAX
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);

                ucProcessProgressBar_Initialize.Stop();
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
                m_nStep = -1;
                m_Timer.Enabled = false;
            }
        }

        private void FrmPopupInitialize_Load(object sender, EventArgs e)
        {
            //m_Timer.Enabled = true;
        }

        private void FrmPopupInitialize_Shown(object sender, EventArgs e)
        {

            if (bInit)
            {
                m_nStep = (int)FA.DEF.eMoudleName.MIN;
                listBox_Data.Items.Clear();
                listBox_Data.Items.Add("Loading...");
                ucProcessProgressBar_Initialize.Rotation = GUI.UserControls.ucProcessProgressBar.eRotation.CW;

            }
            else
            {
                m_nStep = (int)(int)FA.DEF.eMoudleName.MAX;
                listBox_Data.Items.Clear();
                listBox_Data.Items.Add("Unloading...");
                ucProcessProgressBar_Initialize.Rotation = GUI.UserControls.ucProcessProgressBar.eRotation.CCW;

            }
            lblLoading.Text = "";
            m_Timer.Enabled = true;

        }
    }
}
