using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzInaVision;
using System.Drawing;
using System.Drawing.Imaging;
using Euresys.MultiCam;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace EzInaVisionMultiCam
{
    public sealed partial class GrabLinkCam
    {
        #region [ Static Functions ]
        public static List<GrabLinkCam> m_vecCameras = null;


        static bool m_bDriverConnect = false;
        static bool m_bInitialized = false;
        public static bool bDriverConnect
        {
            get { return m_bDriverConnect; }
        }
        public static bool bInitialized
        {
            get { return m_bInitialized; }
        }

        public static bool InitializeDriver()
        {
            if (m_bInitialized == false)
            {
                m_vecCameras = new List<GrabLinkCam>();
            }

            string strPath = Application.StartupPath;
            string[] words = strPath.Split('\\');
            strPath = "";
            for (int i = 0; i < words.Length - 1; i++)
            {
                strPath += words[i] + @"\";
            }

            if (!System.IO.Directory.Exists(strPath))
                System.IO.Directory.CreateDirectory(strPath);

            strPath += @"System\CFG\Vision";

            IniFile VisionIni = new IniFile(strPath + @"\Vision.ini");
            IniFile CamIni = new IniFile(strPath + @"\CAM.ini");
            int nCamItems = 0;
            int nLibItems = 0;
            try
            {
                nCamItems = VisionIni.Read("CAM", "Count", 0);
                for (int i = 0; i < nCamItems; i++)
                {
                    EzInaVision.GDV.stCamInfo info = new EzInaVision.GDV.stCamInfo();
                    info.strCameraName = CamIni.Read(string.Format("CAM_{0}", i), "strCameraName", "");
                    info.strCamFile = CamIni.Read(string.Format("CAM_{0}", i), "strCamFile", "");
                    info.strSerialNumber = CamIni.Read(string.Format("CAM_{0}", i), "strSerialNumber", "");
                    info.iDriverIndex = CamIni.Read(string.Format("CAM_{0}", i), "iDriverIndex", -1);
                    info.strBoardTopology = CamIni.Read(string.Format("CAM_{0}", i), "strBoardTopology", "");
                    info.strConnectorType = CamIni.Read(string.Format("CAM_{0}", i), "strConnectorType", "");
                    info.strColorFormat = CamIni.Read(string.Format("CAM_{0}", i), "strColorFormat", "");
                    info.strFlipX = CamIni.Read(string.Format("CAM_{0}", i), "strFlipX", "");
                    info.strFlipY = CamIni.Read(string.Format("CAM_{0}", i), "strFlipY", "");
                    info.strAcquisitionMode = CamIni.Read(string.Format("CAM_{0}", i), "strAcquisitionMode", "");
                    info.strTrigMode = CamIni.Read(string.Format("CAM_{0}", i), "strTrigMode", "");
                    info.strNextTrigMode = CamIni.Read(string.Format("CAM_{0}", i), "strNextTrigMode", "");
                    info.iSeqLength_Fr = CamIni.Read(string.Format("CAM_{0}", i), "uSeqLength_Fr", -1);
                    info.strTrigLine = CamIni.Read(string.Format("CAM_{0}", i), "strTrigLine", "");
                    info.strTrigEdge = CamIni.Read(string.Format("CAM_{0}", i), "strTrigEdge", "");
                    info.strTrigFilter = CamIni.Read(string.Format("CAM_{0}", i), "strTrigFilter", "");
                    info.strTrigCtl = CamIni.Read(string.Format("CAM_{0}", i), "strTrigCtl", "");
                    info.fSetExposeTime = CamIni.Read(string.Format("CAM_{0}", i), "fSetExposeTime", 0.0f);
                    info.fSetGain = CamIni.Read(string.Format("CAM_{0}", i), "fSetGain", 0.0f);
                    info.fSetFrameRate = CamIni.Read(string.Format("CAM_{0}", i), "fSetFrameRate", 0.0f);
                    info.strCameraName = info.strCameraName.ToUpper();
                    info.strCamFile = strPath + @"\" + info.strCamFile;
                    m_vecCameras.Add(new GrabLinkCam(info));
                }
            }
            catch (Exception exc)
            {
                m_bInitialized = false;
                return false;
            }

            m_bInitialized = true;
            return true;


        }

        public static void TerminateDriver()
        {
            try
            {
                if (m_vecCameras != null)
                {
                    foreach (GrabLinkCam item in m_vecCameras)
                    {
                        item.Terminate();
                    }

                    m_vecCameras.Clear();
                    m_vecCameras = null;

                }

            }
            catch (Exception exc)
            {

            }


        }
        #endregion[ Static Functions ]
        #region [ Static Variables ]
        #endregion [ Static Variables ]
    }

    public sealed partial class GrabLinkCam : VisionCamBaseClass
    {
        #region [ delegate that grabbed image]
        public delegate void OnGrabbedImageHandler(object sender, Bitmap image);
        private OnGrabbedImageHandler _OnGrabbedImageEvent;
        public event OnGrabbedImageHandler OnGrabbedImageEvent
        {
            add
            {
                lock (this)
                {
                    _OnGrabbedImageEvent += value;
                }
            }

            remove
            {
                lock (this)
                {
                    _OnGrabbedImageEvent -= value;
                }
            }
        }
        #endregion[ delegate that grabbed image ]

        // The object that will contain the acquired image
        private Bitmap m_image = null;

        // The object that will contain the palette information for the bitmap
        private ColorPalette m_imgpal = null;

        // The Mutex object that will protect image objects during processing
        private static Mutex m_imageMutex = new Mutex();

        // The MultiCam object that controls the acquisition
        UInt32 m_channel;

        // The MultiCam object that contains the acquired buffer
        private UInt32 m_currentSurface;

        MC.CALLBACK multiCamCallback;

        public GrabLinkCam(EzInaVision.GDV.stCamInfo a_stCamInfo) : base(a_stCamInfo)
        {
            try
            {
                base.m_pbaseType = typeof(GrabLinkCam);
                base.m_bConnected = Initialize();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #region [ Open / Close Device ]
        public override bool Initialize()
        {
            return OpenDevice();
        }
        public override void Terminate()
        {
            CloseDevice();
        }
        public override bool OpenDevice()
        {
            try
            {
                #region [ Configuration Setting ]

                // Open MultiCam driver
                MC.OpenDriver();
                // Enable error logging
                MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

                // In order to support a 10-tap camera on Grablink Full
                // BoardTopology must be set to MONO_DECA
                // In all other cases the default value will work properly 
                // and the parameter doesn't need to be set

                // Set the board topology to support 10 taps mode (only with a Grablink Full)
                // MC.SetParam(MC.BOARD + 0, "BoardTopology", "MONO_DECA");
                MC.SetParam(MC.BOARD + 0, "BoardTopology", base.m_stCamInfo.strBoardTopology.Trim());

                // Create a channel and associate it with the first connector on the first board
                MC.Create("CHANNEL", out m_channel);
                MC.SetParam(m_channel, "DriverIndex", base.m_stCamInfo.iDriverIndex);

                // In order to use single camera on connector A
                // MC_Connector must be set to A for Grablink DualBase
                // For all other Grablink boards the parameter has to be set to M  

                // For all GrabLink boards except Grablink DualBase
                //MC.SetParam(channel, "Connector", "M");
                // For Grablink DualBase
                //MC.SetParam(channel, "Connector", "A");
                MC.SetParam(m_channel, "Connector", base.m_stCamInfo.strConnectorType);

                // Choose the CAM file
                //MC.SetParam(m_channel, "CamFile", "1000m_P50RG");
                MC.SetParam(m_channel, "CamFile", base.m_stCamInfo.strCamFile);
                // Choose the camera expose duration
                //MC.SetParam(m_channel, "Expose_us", 2000);
                MC.SetParam(m_channel, "Expose_us", base.m_stCamInfo.fSetExposeTime);
                // Choose the pixel color format
                MC.SetParam(m_channel, "ColorFormat", base.m_stCamInfo.strColorFormat);

                //Set the acquisition mode to Snapshot
                MC.SetParam(m_channel, "AcquisitionMode", base.m_stCamInfo.strAcquisitionMode);//SNAPSHOT
                                                                                               // Choose the way the first acquisition is triggered
                MC.SetParam(m_channel, "TrigMode", base.m_stCamInfo.strTrigMode); //COMBINED
                                                                                  // Choose the triggering mode for subsequent acquisitions
                MC.SetParam(m_channel, "NextTrigMode", base.m_stCamInfo.strNextTrigMode); //COMBINED
                                                                                          // Choose the number of images to acquire
                MC.SetParam(m_channel, "SeqLength_Fr", base.m_stCamInfo.iSeqLength_Fr); // MC.INDETERMINATE

								// Add by Smpark  OFF , ON
								MC.SetParam(m_channel, "ImageFlipX", base.m_stCamInfo.strFlipX);
								MC.SetParam(m_channel, "ImageFlipY", base.m_stCamInfo.strFlipY);
								// Add by Smpark

								#region [ Configure triggering line ]
								// A rising edge on the triggering line generates a trigger.
								// See the TrigLine Parameter and the board documentation for more details.
								MC.SetParam(m_channel, "TrigLine", base.m_stCamInfo.strTrigLine); //IIN1
                MC.SetParam(m_channel, "TrigEdge", base.m_stCamInfo.strTrigEdge);//GOHIGH
                MC.SetParam(m_channel, "TrigFilter", base.m_stCamInfo.strTrigFilter);//ON

                // Parameter valid for all Grablink but Full, DualBase, Base
                // MC.SetParam(channel, "TrigCtl", "ITTL");
                // Parameter valid only for Grablink Full, DualBase, Base
                MC.SetParam(m_channel, "TrigCtl", base.m_stCamInfo.strTrigCtl);//ISO
                #endregion [ Configure triggering line ]
                // Register the callback function
                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(m_channel, multiCamCallback, m_channel);

                // Enable the signals corresponding to the callback functions
                MC.SetParam(m_channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(m_channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

                // Prepare the channel in order to minimize the acquisition sequence startup latency
                MC.SetParam(m_channel, "ChannelState", "READY");


                MC.GetParam(m_channel, "ImageSizeX", out base.m_stCamInfo.nSizeX);
                MC.GetParam(m_channel, "ImageSizeY", out base.m_stCamInfo.nSizeY);

                base.m_bConnected = true;
                return true;
                #endregion [ Configuration Setting ]
            }
            catch (Euresys.MultiCamException exc)
            {
                // An exception has occurred in the try {...} block. 
                // Retrieve its description and display it in a message box.
                MessageBox.Show(exc.Message, "MultiCam Exception");
                return false;
            }
        }
        public override bool CloseDevice()
        {
            if (!base.m_bConnected)
                return true;

            try
            {
                if (m_channel != 0)
                {
                    MC.SetParam(m_channel, "ChannelState", "FREE"); //FREE
                    Thread.Sleep(5);
                    MC.Delete(m_channel);
                    Thread.Sleep(5);
                    MC.CloseDriver();
                }
                return true;
            }
            catch (Euresys.MultiCamException exc)
            {
                MessageBox.Show(exc.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName);
                return false;

            }
        }
        #endregion[ Open Driver / Close Driver ]
        #region [ Open / Save Configuration ]
        public override bool OpenConfig(string a_strPath)
        {
            return true;
        }
        public override bool SaveConfig(string a_strPath)
        {
            return true;
        }
        #endregion [ Open / Save Configuration ]
        #region [ Camera Status ]
        public override float GetExpose()
        {
            return m_stCamInfo.fGetExposeTime;
        }
        public override float GetFrameRate()
        {
            return m_stCamInfo.fGetFrameRate;
        }
        public override float GetGain()
        {
            return m_stCamInfo.fGetGain;
        }
        public override int GetImageHeight()
        {
            return m_stCamInfo.nSizeY;
        }
        public override int GetImageWidth()
        {
            return m_stCamInfo.nSizeX;
        }
        public override string GetSerialNum()
        {
            return m_stCamInfo.strSerialNumber;
        }
        public override bool IsConnected()
        {
            return base.m_bConnected;
        }
        public override bool IsGrab()
        {
            return base.m_stCamInfo.bGrabbed;
        }
        public override bool IsLive()
        {
            return base.m_stCamInfo.bLive;
        }
        public override bool IsIdle()
        {
            return base.m_stCamInfo.bIdle;
        }
        public override bool IsStop()
        {
            return IsIdle() || IsGrab();
        }
        #endregion [ Camera Status ]
        #region [ Set the Camera Status ]
        public override bool Live()
        {
            base.m_stCamInfo.bGrabbing = false;
            base.m_stCamInfo.bGrabbed = false;
            base.m_stCamInfo.bIdle = false;
            base.m_stCamInfo.bLive = false;

            String channelState;
            MC.GetParam(m_channel, "ChannelState", out channelState);
            if (channelState != "ACTIVE")
            {
                MC.SetParam(m_channel, "ChannelState", "ACTIVE");
            }

            MC.SetParam(m_channel, "ForceTrig", "TRIG");
            base.m_stCamInfo.bLive = true;
            return true;
        }
        public override bool Grab()
        {
            if (!base.m_bConnected) return false;

            try
            {
                base.m_stCamInfo.bGrabbing = true;
                base.m_stCamInfo.bGrabbed = false;
                base.m_stCamInfo.bIdle = false;
                base.m_stCamInfo.bLive = false;

                // Start an acquisition sequence by activating the channel
                String channelState;
                MC.GetParam(m_channel, "ChannelState", out channelState);
                if (channelState != "ACTIVE")
                    MC.SetParam(m_channel, "ChannelState", "ACTIVE");

                // Generate a soft trigger event
                MC.SetParam(m_channel, "ForceTrig", "TRIG");

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
        public override bool Idle()
        {
            if (!base.m_bConnected) return false;

            try
            {
                base.m_stCamInfo.bGrabbing = false;
                base.m_stCamInfo.bGrabbed = false;
                base.m_stCamInfo.bIdle = false;
                base.m_stCamInfo.bLive = false;
                String channelState;
                MC.GetParam(m_channel, "ChannelState", out channelState);
                if (channelState == "ACTIVE")
                    MC.SetParam(m_channel, "ChannelState", "IDLE");

                base.m_stCamInfo.bIdle = true;
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }

        }
        public override bool LoadImage(string a_strPath)
        {
            return true;
        }
        public override void Reconnect()
        {
        }
        public override bool SaveImage(string a_strPath)
        {
						if(IsLive()==false && IsGrab())
						{
							  if(m_image!=null)
								{
										m_image.Save(a_strPath);
								}
								return true;
						}
            return false; 
        }
        public override void SetExposeTime(int a_nTime)
        {
            if (!IsConnected())
                return;
            string strExposeTime = "";
            MC.SetParam(m_channel, "Expose_us", a_nTime);
            base.m_stCamInfo.fSetExposeTime = Convert.ToSingle(a_nTime);
        }
        public override void SetGain(int a_nValue)
        {
            if (!IsConnected())
                return;
            MC.SetParam(m_channel, "Gain", a_nValue);
        }
        #endregion [ Set the Camera status ]
        #region [ Callback Functions ]
        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;
                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;
                default:
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }
        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;
            m_currentSurface = signalInfo.SignalInfo;

            // + GrablinkSnapshot Sample Program

            try
            {
                // Update the image with the acquired image buffer data 
                Int32 width, height, bufferPitch;
                IntPtr bufferAddress;
                MC.GetParam(currentChannel, "ImageSizeX", out width);
                MC.GetParam(currentChannel, "ImageSizeY", out height);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                MC.GetParam(m_currentSurface, "SurfaceAddr", out bufferAddress);

                string strColorFormat = "";
                MC.GetParam(currentChannel, "ColorFormat", out strColorFormat);

                // Retrieve the frame rate
                Double frameRate_Hz;
                MC.GetParam(m_channel, "PerSecond_Fr", out frameRate_Hz);
                base.m_stCamInfo.fGetFrameRate = Convert.ToSingle(frameRate_Hz);

                // Retrieve the channel state
                String channelState;
                MC.GetParam(m_channel, "ChannelState", out channelState);

                // Retrieve the channel expose time
                string strExpose_us = "";
                MC.GetParam(currentChannel, "Expose_us", out strExpose_us);
                base.m_stCamInfo.fGetExposeTime = Convert.ToSingle(strExpose_us);

                // Retrieve the channel gain
                //string strGain = "";
                //MC.GetParam(currentChannel, "Gain", out strGain);
                //base.m_stCamInfo.fGetGain = Convert.ToSingle(strGain);


                try
                {
                    m_imageMutex.WaitOne();

                    /*
                     * Y8, Y10, Y12, Y16
                       RGB24, RGB32, RGB24PL, RGB30PL, RGB36PL, RGB48PL
                       BAYER8, BAYER10, BAYER12, BAYER16
                    */

                    int nPadding = 0;
                    switch (strColorFormat) //base.m_stCamInfo.strColorFormat;
                    {
                        case "Y8":
                            //nPadding = ((bufferPitch & 31) != 0 ? 32 - (bufferPitch & 31) : 0); // >
                            //bufferPitch = ((bufferPitch & 31) != 0 ? bufferPitch - (bufferPitch & 31) : 0); // <
                            //bufferPitch = bufferPitch + nPadding;
                            m_image = new Bitmap(width, height, bufferPitch, PixelFormat.Format8bppIndexed, bufferAddress);
                            m_imgpal = m_image.Palette;
                            //Build bitmap palette Y8
                            for (int i = 0; i < 256; i++)
                            {
                                m_imgpal.Entries[i] = Color.FromArgb((byte)0xFF, (byte)i, (byte)i, (byte)i);
                            }
                            m_image.Palette = m_imgpal;
                            break;
                        case "RGB24":
                            m_image = new Bitmap(width, height, bufferPitch, PixelFormat.Format24bppRgb, bufferAddress);
                            break;

                    }
                    /* Insert image analysis and processing code here */
                    _OnGrabbedImageEvent(this, m_image);
										base.m_stCamInfo.bGrabbed = true;

								}
								finally
                {
                    m_imageMutex.ReleaseMutex();
                }

                switch (base.m_stCamInfo.strTrigMode)
                {
                    case "IMMEDIATE": // The acquisition sequence starts immediately without waiting for a trigger
                        break;
                    case "HARD": // The start of the acquisition is delayed until the hardware trigger line senses a valid transition
                        break;
                    case "SOFT": // The start of the acquisition sequence is delayed until the software sets ForceTirg to TRIG
                        if (!IsLive()) break;
                        if (IsIdle()) break;
                        MC.SetParam(m_channel, "SeqLength_Fr", MC.INDETERMINATE);
                        MC.SetParam(m_channel, "ForceTrig", "TRIG");
                        break;
                    case "COMBINED": // The start of the acquisition sequence is delayed until detection of hardware or software tirgger.
                        if (!IsLive()) break;
                        if (IsIdle()) break;
                        MC.SetParam(m_channel, "SeqLength_Fr", MC.INDETERMINATE);
                        MC.SetParam(m_channel, "ForceTrig", "TRIG");
                        break;

                }


                // Display frame rate and channel state
                //statusBar.Text = String.Format("Frame Rate: {0:f2}, Channel State: {1}", frameRate_Hz, channelState);

                // Display the new image
                //this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
            }
            catch (Euresys.MultiCamException exc)
            {
                MessageBox.Show(exc.Message, "MultiCam Exception");
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, "System Exception");
            }
            // - GrablinkSnapshot Sample Program
        }
        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            // + GrablinkSnapshot Sample Program

            try
            {
                // Display frame rate and channel state
                //statusBar.Text = String.Format("Acquisition Failure, Channel State: IDLE");
                //this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, "System Exception");
            }

            // - GrablinkSnapshot Sample Program
        }


        /*public static bool InitializeDriver(GDV.)*/
        #endregion[ Callback Functions ]
    }
}

