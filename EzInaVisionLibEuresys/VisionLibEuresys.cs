using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euresys.Open_eVision_2_14;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.InteropServices;
using EzInaVision;
using System.Diagnostics;

namespace EzInaVisionLibrary
{
    public sealed partial class VisionLibEuresys
    {
        public enum eLicensingModel
        {
            LegacyDongle = ELicensingModel.LegacyDongle,
            LegacySoftware = ELicensingModel.LegacySoftware,
            Legacy = ELicensingModel.Legacy,
            Neo = ELicensingModel.Neo,
            All = ELicensingModel.All
        }

        #region [ Static Functions ]
        public static List<VisionLibEuresys> m_vecLibraries = null;

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

        public static void SelectLicensingModel(eLicensingModel a_eModel)
        {
            Preconfiguration.SelectLicensingModels((ELicensingModel)a_eModel);
        }

        public static bool InitializeDriver()
        {

            if (m_bInitialized == false)
            {
                m_vecLibraries = new List<VisionLibEuresys>();
            }

            string strPath = Application.StartupPath;
            string[] Words = strPath.Split('\\');

            strPath = "";
            for (int i = 0; i < Words.Length - 1; i++)
                strPath += Words[i] + @"\";


            strPath += @"System\CFG\Vision";

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            IniFile VisionIni = new IniFile(strPath + @"\Vision.ini");
            IniFile LibIni = new IniFile(strPath + @"\LIB.ini");

            int nLibItems = 0;
            try
            {
                nLibItems = VisionIni.Read("LIB", "Count", 0);
                for (int i = 0; i < nLibItems; i++)
                {
                    EzInaVision.GDV.stLibInfo info = new EzInaVision.GDV.stLibInfo();
                    info.strName = LibIni.Read(string.Format("LIB_{0}", i), "strName", "");
                    info.iDriverIndex = LibIni.Read(string.Format("LIB_{0}", i), "iDriverIndex", -1);
                    info.bColor = LibIni.Read(string.Format("LIB_{0}", i), "bColor", false);
                    info.fCenterCoordinateOfX = LibIni.Read(string.Format("LIB_{0}", i), "fCenterCoordinateOfX", 0.0f);
                    info.fCenterCoordinateOfY = LibIni.Read(string.Format("LIB_{0}", i), "fCenterCoordinateOfY", 0.0f);
                    info.dLensScaleX = LibIni.Read(string.Format("LIB_{0}", i), "dLensScaleX", 0.0);
                    info.dLensScaleY = LibIni.Read(string.Format("LIB_{0}", i), "dLensScaleY", 0.0);
                    info.fImageW = LibIni.Read(string.Format("LIB_{0}", i), "fImageW", 0.0f);
                    info.fImageH = LibIni.Read(string.Format("LIB_{0}", i), "fImageH", 0.0f);
                    m_vecLibraries.Add(new VisionLibEuresys(info));
                }


            }
            catch (Exception exc)
            {
                m_bInitialized = false;
                return false;
            }

            //             foreach (VisionLibEuresys item in m_vecLibraries)
            //             {
            //             }

            m_bInitialized = true;
            return true;


        }

        public static void TerminateDriver()
        {
            try
            {
                if (m_vecLibraries != null)
                {
                    foreach (VisionLibEuresys item in m_vecLibraries)
                    {
                        item.Terminate();
                    }

                }

                m_vecLibraries.Clear();
                m_vecLibraries = null;
                m_bInitialized = false;

            }
            catch (Exception exc)
            {

            }

        }
    }
    #endregion[ Static Functions ]

    public sealed partial class VisionLibEuresys : VisionLibBaseClass
    {
        #region [ delegate that image for display ]
        public delegate void OnDisplayHandler(object obj, Bitmap bmp);
        private OnDisplayHandler _OnDisplayEvent;
        public event OnDisplayHandler OnDisplayEvent
        {
            add
            {
                _OnDisplayEvent += value;
            }
            remove
            {
                _OnDisplayEvent -= value;
            }
        }

        #endregion [ delegate that image for displaying ]
        Font m_drawFont;
        SolidBrush m_drawBrush;
        SolidBrush m_OKBrush;
        SolidBrush m_NGBrush;

        ERGBColor m_ColorYellow;
        ERGBColor m_ColorRed;
        ERGBColor m_ColorGreen;
        ERGBColor m_ColorBlue;

        //Gray
        EImageBW8 m_eProcessImgBW8;
        ConcurrentDictionary<int, EROIBW8> m_dicRoisForInspectionBW8;

        //Color
        EImageC24 m_eProcessEC24;
        ConcurrentDictionary<int, EROIC24> m_dicRoisForSearchingEC24;



        public VisionLibEuresys(EzInaVision.GDV.stLibInfo a_stLibInfo) : base(a_stLibInfo)
        {
            base.m_pbaseType = typeof(VisionLibEuresys);
            base.m_bInitialized = Initialize();
        }
        ~VisionLibEuresys()
        {
            //Terminate();
        }

        public override bool Initialize()
        {
            LibInit();
            MatchInit();
            BlobInit();
            FiltersInit();
            GaugeInit();
            MatrixCodeInit();
            MatrixCode1Init();
            AttachAllROIsToTheSrcImg(EzInaVision.GDV.eImageType.Regular);

            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH_RESULT, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB_RESULT, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CROSS, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.ROI, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.INSPECT_INSIDE, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.INSPECT_OUTSIDE, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.INSPECT_LOOP, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MEASURE, false, (k, v) => false);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CALIBRATION, false, (k, v) => false);

            return true;

        }
        public override bool IsInitialized()
        {
            return base.m_bInitialized;
        }
        public override void LibInit()
        {
            //Process image   
            m_eProcessImgBW8 = new EImageBW8(100, 100);

            m_dicRoisForInspectionBW8 = new ConcurrentDictionary<int, EROIBW8>();

            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
            {
                m_dicRoisForInspectionBW8.TryAdd(i, new EROIBW8());
            }
            // 			m_eProcessEC24 = new EImageC24();
            // 			m_dicRoisForSearchingEC24 = new ConcurrentDictionary<int, EROIC24>();
            // 			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
            // 			{
            // 				m_dicRoisForSearchingEC24.TryAdd(i, new EROIBW8());
            // 			}


            m_drawFont = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            m_drawBrush = new SolidBrush(Color.Red);
            m_OKBrush = new SolidBrush(Color.Lime);
            m_NGBrush = new SolidBrush(Color.Red);

            m_ColorYellow = new ERGBColor(255, 255, 0);
            m_ColorRed = new ERGBColor(255, 0, 0);
            m_ColorGreen = new ERGBColor(0, 255, 0);
            m_ColorBlue = new ERGBColor(0, 0, 255);
        }
        public override void Terminate()
        {
            if (!base.m_bInitialized)
                return;

            LibTerminate();
            MatchTerminate();
            BlobTerminate();
            FiltersTerminate();
            GaugeTerminate();
            MatrixCodeTerminate();
            MatrixCode1Terminate();

        }

        public override void LibTerminate()
        {
            if (!m_eProcessImgBW8.IsVoid)
            {
                m_eProcessImgBW8.Dispose();
                m_eProcessImgBW8 = null;
            }

            if (m_dicRoisForInspectionBW8 != null)
            {
                m_dicRoisForInspectionBW8.Clear();
            }

            if (m_drawFont != null)
            {
                m_drawFont.Dispose();
                m_drawFont = null;
            }

            if (m_drawBrush != null)
            {
                m_drawBrush.Dispose();
                m_drawBrush = null;
            }

            if (m_OKBrush != null)
            {
                m_OKBrush.Dispose();
                m_OKBrush = null;
            }

            if (m_NGBrush != null)
            {
                m_NGBrush.Dispose();
                m_NGBrush = null;
            }

        }
        public override bool OpenConfig(string strPath)
        {
            IniFile Ini = new IniFile(strPath);
            string strSection = base.m_LibInfo.m_stLibInfo.strName;


            base.m_LibInfo.m_stLibInfo.dLensScaleX = Ini.Read(strSection, "LensScaleX", 6.95652);
            base.m_LibInfo.m_stLibInfo.dLensScaleY = Ini.Read(strSection, "LensScaleY", 6.95652);
            //Matchers
            base.m_LibInfo.m_MatchConfig.m_fMinScale = Ini.Read(strSection, "MinScale", 1.0);
            base.m_LibInfo.m_MatchConfig.m_fMaxScale = Ini.Read(strSection, "MaxScale", 1.0);
            base.m_LibInfo.m_MatchConfig.m_fScore = Ini.Read(strSection, "Score", 98.0);
            base.m_LibInfo.m_MatchConfig.m_fAngle = Ini.Read(strSection, "Angle", 0.0);
            base.m_LibInfo.m_MatchConfig.m_fMaxPosition = Ini.Read(strSection, "MaxPositions", 3000.0);
            base.m_LibInfo.m_MatchConfig.m_iCorrelationMode = Ini.Read(strSection, "CorrelationMode", (int)ECorrelationMode.Standard);
            base.m_LibInfo.m_MatchConfig.m_iMatchContrastMode = Ini.Read(strSection, "MatchContrastMode", (int)EMatchContrastMode.Normal);

            //Blobs
            base.m_LibInfo.m_BlobParam.m_nPadBlobMethod = Ini.Read(strSection, "PadBlobMethod", 0);
            base.m_LibInfo.m_BlobParam.m_nPadGrayMinVal = Ini.Read(strSection, "PadGrayMinVal", 120U);
            base.m_LibInfo.m_BlobParam.m_nPadGrayMaxVal = Ini.Read(strSection, "PadGrayMaxVal", 255U);
            base.m_LibInfo.m_BlobParam.m_fPadHeightMin = Ini.Read(strSection, "PadHeightMin", 0.01f);
            base.m_LibInfo.m_BlobParam.m_fPadHeightMax = Ini.Read(strSection, "PadHeightMax", 1.0f);
            base.m_LibInfo.m_BlobParam.m_fPadWidthMin = Ini.Read(strSection, "PadWidthMin", 0.01f);
            base.m_LibInfo.m_BlobParam.m_fPadWidthMax = Ini.Read(strSection, "PadWidthMax", 1.0f);
            base.m_LibInfo.m_BlobParam.m_fPadAreaMin = Ini.Read(strSection, "AreaMin", 1.0f);
            base.m_LibInfo.m_BlobParam.m_fPadAreaMax = Ini.Read(strSection, "AreaMin", 1.0f);

            //Filters
            base.m_LibInfo.m_Filterconfig.m_nThresHoldValue = Ini.Read(strSection, "ThresHoldValue", 200U);
            base.m_LibInfo.m_Filterconfig.m_nOpenDiskValue = Ini.Read(strSection, "OpenDiskValue", 5U);
            base.m_LibInfo.m_Filterconfig.m_nCloseDiskValue = Ini.Read(strSection, "CloseDiskValue", 5U);
            base.m_LibInfo.m_Filterconfig.m_nErodeDiskValue = Ini.Read(strSection, "ErodeDiskValue", 5U);
            base.m_LibInfo.m_Filterconfig.m_nDilateDiskValue = Ini.Read(strSection, "DilateDiskValue", 5U);

            //ROIs
            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
            {
                Rectangle rect = new Rectangle();
                rect.X = Ini.Read(strSection, string.Format("Roi{0}_X", i), 0);
                rect.Y = Ini.Read(strSection, string.Format("Roi{0}_Y", i), 0);
                rect.Width = Ini.Read(strSection, string.Format("Roi{0}_Width", i), 640);
                rect.Height = Ini.Read(strSection, string.Format("Roi{0}_Height", i), 480);
                base.m_LibInfo.m_dicRoiSize[i] = rect;
            }

            //Selected ROIs


            //Options
            ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool> dicVisionOption = new ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool>();
            bool bOption = false;
            bOption = Ini.Read(strSection, "DispROIs", false);
            dicVisionOption.TryAdd(EzInaVision.GDV.eLibOption.ROI, bOption);

            bOption = Ini.Read(strSection, "DispCrosslines", false);
            dicVisionOption.TryAdd(EzInaVision.GDV.eLibOption.CROSS, bOption);

            bOption = Ini.Read(strSection, "DispResultMatcher", false);
            dicVisionOption.TryAdd(EzInaVision.GDV.eLibOption.MATCH_RESULT, bOption);

            bOption = Ini.Read(strSection, "DispResultBlob", false);
            dicVisionOption.TryAdd(EzInaVision.GDV.eLibOption.BLOB_RESULT, bOption);

            bOption = Ini.Read(strSection, "DispFilters", false);
            dicVisionOption.TryAdd(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, bOption);

            SetOptions(dicVisionOption);

            Ini = null;
            return true;
        }
        public override bool SaveConfig(string a_strPath)
        {
            return true;
        }

        public override bool GetOption(EzInaVision.GDV.eLibOption a_eOption)
        {
            bool bEnabled = false;
            if (base.m_LibInfo.m_dicLibOption.TryGetValue(a_eOption, out bEnabled))
            {
                if (bEnabled)
                    return true;
                else
                    return false;
            }
            return bEnabled;
        }
        public override void SetOption(EzInaVision.GDV.eLibOption a_eOption, bool a_bUse)
        {
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(a_eOption, a_bUse, (k, v) => a_bUse);
        }

        public override void SetVisionScale(double a_scaleX, double a_scaleY)
        {
            base.m_LibInfo.m_stLibInfo.dLensScaleX = a_scaleX;
            base.m_LibInfo.m_stLibInfo.dLensScaleY = a_scaleY;
        }
        public override bool GetVisionScale(ref double a_scaleX, ref double a_scaleY)
        {
            a_scaleX = base.m_LibInfo.m_stLibInfo.dLensScaleX;
            a_scaleY = base.m_LibInfo.m_stLibInfo.dLensScaleY;
            return true;

        }
        public override void SetOptions(ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool> a_dicOptions)
        {
            bool bSet = false;

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.MATCH_RESULT, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH_RESULT, bSet, (k, v) => bSet);

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.BLOB_RESULT, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB_RESULT, bSet, (k, v) => bSet);

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.ROI, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.ROI, bSet, (k, v) => bSet);

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, bSet, (k, v) => bSet);

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.CROSS, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CROSS, bSet, (k, v) => bSet);

            a_dicOptions.TryGetValue(EzInaVision.GDV.eLibOption.CALIBRATION, out bSet);
            base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CALIBRATION, bSet, (k, v) => bSet);

        }
        public override Rectangle GetRoiForInspection(int a_iKey)
        {
            Rectangle rRect = Rectangle.Empty;

            if (base.m_LibInfo.m_dicRoiSize.TryGetValue(a_iKey, out rRect))
            {
                if (m_dicRoisForInspectionBW8.ContainsKey(a_iKey))
                {
                    EROIBW8 Roi;
                    if (m_dicRoisForInspectionBW8.TryGetValue(a_iKey, out Roi))
                    {
                        Roi.SetPlacement(rRect.X, rRect.Y, rRect.Width, rRect.Height);
                    }
                }
            }
            else
            {
                return rRect;
            }

            return rRect;
        }
        public override bool AddRoiForInspection(int iKey)
        {
            if (m_dicRoisForInspectionBW8.ContainsKey(iKey) == false)
            {
                m_dicRoisForInspectionBW8.TryAdd(iKey, new EROIBW8());
                return true;
            }
            return false;
        }
        public override void SetRoiForInspection(int a_iKey, Rectangle a_rRect)
        {
            base.m_LibInfo.m_dicRoiSize.AddOrUpdate(a_iKey, a_rRect, (k, v) => a_rRect);
            if (m_dicRoisForInspectionBW8.ContainsKey(a_iKey))
            {
                EROIBW8 Roi;
                if (m_dicRoisForInspectionBW8.TryGetValue(a_iKey, out Roi))
                {
                    Roi.SetPlacement(a_rRect.X, a_rRect.Y, a_rRect.Width, a_rRect.Height);
                }
            }
        }

        public override int GetPixel(Point a_pPoint)
        {
            if (m_eProcessImgBW8.IsVoid)
                return 0;
            return m_eProcessImgBW8.GetPixel(a_pPoint.X, a_pPoint.Y).Value;
        }
        public override void AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType a_eType, EzInaVision.GDV.eRoiItems a_eROI)
        {
            try
            {
                switch (a_eType)
                {
                    case EzInaVision.GDV.eImageType.Regular:
                        {
                            if (m_eProcessImgBW8.IsVoid)
                                return;

                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi) == true)
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eProcessImgBW8);
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Filter:
                        {
                            if (m_eFilteredImgBW8.IsVoid)
                                return;
                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi))
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eFilteredImgBW8);
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Calibrate:
                        {
                            if (m_eCalibratedImgBW8.IsVoid)
                                return;

                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi) == true)
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eCalibratedImgBW8);
                                }
                            }
                        }
                        break;

                }


                //color
                // 				for (int i = 0; i < m_dicRoisForSearchingEC24.Count; i++)
                // 				{
                // 
                // 					EROIC24 Roi;
                // 					if (i == m_dicRoisForSearchingEC24.Count - 1)
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 					else
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 				}
            }
            catch (EException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override bool AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType a_eType, int a_eROI)
        {
            try
            {
                if (m_dicRoisForInspectionBW8.ContainsKey(a_eROI) == false)
                    return false;
                switch (a_eType)
                {
                    case EzInaVision.GDV.eImageType.Regular:
                        {
                            if (m_eProcessImgBW8.IsVoid)
                                return false;

                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi) == true)
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eProcessImgBW8);
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Filter:
                        {
                            if (m_eFilteredImgBW8.IsVoid)
                                return false;
                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi))
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eFilteredImgBW8);
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Calibrate:
                        {
                            if (m_eCalibratedImgBW8.IsVoid)
                                return false;

                            EROIBW8 Roi = null;
                            if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi) == true)
                            {
                                if (Roi != null)
                                {
                                    if (Roi.IsVoid)
                                        Roi.Detach();
                                    Roi.Attach(m_eCalibratedImgBW8);
                                }
                            }
                        }
                        break;

                }
                return true;

                //color
                // 				for (int i = 0; i < m_dicRoisForSearchingEC24.Count; i++)
                // 				{
                // 
                // 					EROIC24 Roi;
                // 					if (i == m_dicRoisForSearchingEC24.Count - 1)
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 					else
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 				}

            }
            catch (EException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public override void AttachAllROIsToTheSrcImg(EzInaVision.GDV.eImageType a_eType)
        {
            try
            {
                switch (a_eType)
                {
                    case EzInaVision.GDV.eImageType.Regular:
                        {
                            if (m_eProcessImgBW8.IsVoid)
                                return;

                            for (int i = 0; i < m_dicRoisForInspectionBW8.Count; i++)
                            {
                                EROIBW8 Roi = null;
                                if (m_dicRoisForInspectionBW8.TryGetValue(i, out Roi) == true)
                                {
                                    if (Roi != null)
                                    {
                                        if (Roi.IsVoid)
                                            Roi.Detach();
                                        Roi.Attach(m_eProcessImgBW8);
                                    }
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Filter:
                        {
                            if (m_eFilteredImgBW8.IsVoid)
                                return;

                            for (int i = 0; i < m_dicRoisForInspectionBW8.Count; i++)
                            {
                                EROIBW8 Roi = null;
                                if (m_dicRoisForInspectionBW8.TryGetValue(i, out Roi) == true)
                                {
                                    if (Roi != null)
                                    {
                                        if (Roi.IsVoid)
                                            Roi.Detach();
                                        Roi.Attach(m_eFilteredImgBW8);
                                    }
                                }
                            }
                        }
                        break;

                    case EzInaVision.GDV.eImageType.Calibrate:
                        {
                            if (m_eCalibratedImgBW8 == null)
                                return;

                            for (int i = 0; i < m_dicRoisForInspectionBW8.Count; i++)
                            {
                                EROIBW8 Roi = null;
                                if (m_dicRoisForInspectionBW8.TryGetValue(i, out Roi) == true)
                                {
                                    if (Roi != null)
                                    {
                                        if (Roi.IsVoid)
                                            Roi.Detach();
                                        Roi.Attach(m_eCalibratedImgBW8);
                                    }
                                }
                            }
                        }
                        break;
                }
                //color
                // 				for (int i = 0; i < m_dicRoisForSearchingEC24.Count; i++)
                // 				{
                // 
                // 					EROIC24 Roi;
                // 					if (i == m_dicRoisForSearchingEC24.Count - 1)
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 					else
                // 					{
                // 						m_dicRoisForSearchingEC24.TryGetValue(GDV.GOLDEN_ROIS, out Roi);
                // 						Roi.Detach();
                // 						Roi.Attach(m_eProcessImgBW8);
                // 					}
                // 				}
            }
            catch (EException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetEROIBW8(EzInaVision.GDV.eRoiItems a_eRoiItem, out EROIBW8 a_ERoiBW8)
        {
            a_ERoiBW8 = null;

            if (m_dicRoisForInspectionBW8.Count < 1)
                return;

            m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out a_ERoiBW8);
        }


        #region [ Delegate function processing ]
        public void OnGrabbedImage(object a_obj, Bitmap a_Bitmap)
        {
            try
            {
                if (a_Bitmap != null)
                {
                    if (a_Bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
                        SetBitmapToEImageBW8(a_Bitmap);

                    Bitmap BmpForDisplay = null;
                    #region [ Filters ]
                    //SetOption(EzInaVision.GDV.eLibOption.ENABLE_THRESHOLD, true);

                    if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS))
                    {
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_THRESHOLD))
                        {
                            ThresholdOfFilters();
                        }
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_ERODE_DISK))
                        {
                            ErodeDiskOfFilters();
                        }
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_DILATE_DISK))
                        {
                            DilateDiskOfFilters();
                        }
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_OPEN_DISK))
                        {
                            OpenDiskOfFilters();
                        }
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_CLOSE_DISK))
                        {
                            CloseDiskOfFilters();
                        }

                        //CopyFilteredImageToProcessImage();
                        CopyFilteredImageToProcessImage();
                        BmpForDisplay = FilteredImageConvertToBitmap();
                        _OnDisplayEvent(this, (Bitmap)BmpForDisplay.Clone());
                        BmpForDisplay.Dispose();
                        #endregion[ Filters ]
                    }
                    else
                    {
                        BmpForDisplay = ProcessImageConvertToBitmap();
                        _OnDisplayEvent(this, (Bitmap)BmpForDisplay.Clone());
                        BmpForDisplay.Dispose();
                    }

                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.ToString());
            }
        }
        public void SetBitmapToEImageBW8(Bitmap a_bmp)
        {

            if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_CALIBRATION))
            {
                //GaugeCalibration(EzInaVision.GDV.eRoiItems.ROI_No0, 10, 10);
                if (GaugeUnwarping(a_bmp) == 0)
                {
                    m_eProcessImgBW8.SetSize(m_eUnwrappedImgBW8);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eUnwrappedImgBW8, m_eProcessImgBW8);
                    m_eFilteredImgBW8.SetSize(m_eUnwrappedImgBW8);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eUnwrappedImgBW8, m_eFilteredImgBW8);
                    m_eCalibratedImgBW8.SetSize(m_eUnwrappedImgBW8);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eUnwrappedImgBW8, m_eCalibratedImgBW8);
                    base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX = (float)m_eUnwrappedImgBW8.Width / 2.0f;
                    base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY = (float)m_eUnwrappedImgBW8.Height / 2.0f;
                }
            }
            else
            {
                using (EImageBW8 eProcessBW = new EImageBW8(GetEImageBW8(a_bmp)))
                {
                    m_eProcessImgBW8.SetSize(eProcessBW);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eProcessImgBW8);
                    m_eFilteredImgBW8.SetSize(eProcessBW);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eFilteredImgBW8);
                    m_eCalibratedImgBW8.SetSize(eProcessBW);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eCalibratedImgBW8);
                    //m_eUnwrappedImgBW8.SetSize(eProcessBW);
                    //EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eUnwrappedImgBW8);
                    base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX = (float)m_eProcessImgBW8.Width / 2.0f;
                    base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY = (float)m_eProcessImgBW8.Height / 2.0f;
                }
            }
        }
        public void SetColorBitmapToEImageBW8(Bitmap a_bmp)
        {
            EImageC24 pIMG = new EImageC24(GetEImageColor(a_bmp));
            using (EImageBW8 eProcessBW = new EImageBW8(pIMG.Width, pIMG.Height))
            {
                EasyImage.Convert(pIMG, eProcessBW);
                m_eProcessImgBW8.SetSize(eProcessBW);
                //the eProcessBW image copy to m_eProcessImgBW8
                EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eProcessImgBW8);
                m_eFilteredImgBW8.SetSize(eProcessBW);
                EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eFilteredImgBW8);
                m_eCalibratedImgBW8.SetSize(eProcessBW);
                EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eCalibratedImgBW8);
                //m_eUnwrappedImgBW8.SetSize(eProcessBW);
                //EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eUnwrappedImgBW8);

                base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX = (float)m_eProcessImgBW8.Width / 2.0f;
                base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY = (float)m_eProcessImgBW8.Height / 2.0f;
            }
        }
        private EImageC24 GetEImageColor(Bitmap a_bmp)
        {
            EImageC24 eImageC24 = new EImageC24();
            BitmapData bmpData = null;
            try
            {
                bmpData = a_bmp.LockBits(new Rectangle(0, 0, a_bmp.Width, a_bmp.Height), ImageLockMode.ReadWrite, a_bmp.PixelFormat);
                IntPtr addr = bmpData.Scan0;
                eImageC24.SetImagePtr(a_bmp.Width, a_bmp.Height, addr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                a_bmp.UnlockBits(bmpData);
            }

            return eImageC24;
        }

        public Bitmap ProcessImageConvertToBitmap()
        {
            Bitmap bmp = null;

            if (m_eProcessImgBW8.IsVoid)
                return bmp;

            ColorPalette imgpal = null;

            try
            {
                int bytePerPixel = Convert.ToInt32(m_eProcessImgBW8.BitsPerPixel) / 8; //bit to byte
                int padding = (m_eProcessImgBW8.Width & 31) != 0 ? 32 - (m_eProcessImgBW8.Width & 31) : 0;
                int nStribe = m_eProcessImgBW8.Width * bytePerPixel + padding;

                int nRowPitch = m_eProcessImgBW8.RowPitch;
                bmp = new Bitmap(m_eProcessImgBW8.Width, m_eProcessImgBW8.Height, nStribe, PixelFormat.Format8bppIndexed, m_eProcessImgBW8.GetImagePtr(0, 0));
                imgpal = bmp.Palette;

                for (int i = 0; i < 256; i++)
                {
                    imgpal.Entries[i] = Color.FromArgb((byte)0xFF, (byte)i, (byte)i, (byte)i);
                }

                bmp.Palette = imgpal;

            }
            catch (Exception exc)
            {
                MsgBox.Error("{0}{1}", exc.ToString(), MethodBase.GetCurrentMethod().Name);
            }

            return bmp;

        }
        public Bitmap FilteredImageConvertToBitmap()
        {
            Bitmap bmp = null;
            if (m_eFilteredImgBW8.IsVoid)
                return bmp;

            ColorPalette imgpal = null;
            try
            {
                int bytePerPixel = Convert.ToInt32(m_eFilteredImgBW8.BitsPerPixel) / 8;
                int padding = (m_eFilteredImgBW8.Width & 31) != 0 ? 32 - (m_eFilteredImgBW8.Width & 31) : 0;
                int nStribe = m_eFilteredImgBW8.Width * bytePerPixel + padding;

                //int nRowPitch = m_eFilteredImgBW8.RowPitch;
                int nRowPitch = m_eFilteredImgBW8.RowPitch;
                bmp = new Bitmap(m_eFilteredImgBW8.Width, m_eFilteredImgBW8.Height, nStribe, PixelFormat.Format8bppIndexed, m_eFilteredImgBW8.GetImagePtr(0, 0));
                imgpal = bmp.Palette;
                //Build bitmap palette Y8
                for (int i = 0; i < 256; i++)
                {
                    imgpal.Entries[i] = Color.FromArgb((byte)0xFF, (byte)i, (byte)i, (byte)i);
                }

                // set palette back
                bmp.Palette = imgpal;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }

            /*
                Bitmap bmp = new Bitmap(eProcThreshBW.Width, eProcThreshBW.Height);
                Graphics g = Graphics.FromImage(bmp);
                eProcThreshBW.Draw(g);
            */

            return bmp;


        }
        public Bitmap UnwrappedImageConvertToBitamp()
        {
            Bitmap bmp = null;

            if (m_eUnwrappedImgBW8.IsVoid)
                return bmp;

            ColorPalette imgPal = null;

            int nBytePerPixel = Convert.ToInt32(m_eUnwrappedImgBW8.BitsPerPixel / 8);
            int nPadding = (m_eUnwrappedImgBW8.Width & 31) != 0 ? 32 - (m_eUnwrappedImgBW8.Width & 31) : 0;
            int nStribe = m_eUnwrappedImgBW8.Width * nBytePerPixel + nPadding;

            int nRowPitch = m_eUnwrappedImgBW8.RowPitch;
            bmp = new Bitmap(m_eUnwrappedImgBW8.Width, m_eUnwrappedImgBW8.Height, nStribe, PixelFormat.Format8bppIndexed, m_eUnwrappedImgBW8.GetImagePtr(0, 0));
            imgPal = bmp.Palette;

            for (int i = 0; i < 256; i++)
                imgPal.Entries[i] = Color.FromArgb((byte)0xFF, (byte)i, (byte)i, (byte)i);
            bmp.Palette = imgPal;

            return bmp;

        }
        private EImageBW8 GetEImageBW8(Bitmap a_bmp)
        {
            EImageBW8 eImageBW8 = new EImageBW8();
            BitmapData bmpData = null;
            try
            {
                bmpData = a_bmp.LockBits(new Rectangle(0, 0, a_bmp.Width, a_bmp.Height), ImageLockMode.ReadWrite, a_bmp.PixelFormat);
                IntPtr addr = bmpData.Scan0;
                eImageBW8.SetImagePtr(a_bmp.Width, a_bmp.Height, addr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                a_bmp.UnlockBits(bmpData);
            }

            return eImageBW8;
        }
        public void CopyFilteredImageToProcessImage()
        {
            if (m_eProcessImgBW8.IsVoid) return;
            if (m_eFilteredImgBW8.IsVoid) return;

            EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eFilteredImgBW8, m_eProcessImgBW8);

        }
        public void CopyUnwrappedImageToProcessImage()
        {
            if (m_eProcessImgBW8.IsVoid) return;
            if (m_eUnwrappedImgBW8.IsVoid) return;

            EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eUnwrappedImgBW8, m_eProcessImgBW8);
        }

        public void CopyUnwrappedImageToFilterImage()
        {
            if (m_eProcessImgBW8.IsVoid) return;
            if (m_eUnwrappedImgBW8.IsVoid) return;

            EasyImage.Oper(EArithmeticLogicOperation.Copy, m_eUnwrappedImgBW8, m_eFilteredImgBW8);
        }
        #endregion[ Delegate function processing ]
        #region [ Display ]
        object DisplayLock = new object();
        public override void Display(Graphics g, double a_dXScale, double a_dYScale, PointF a_pFanXY, EzInaVision.GDV.stCamInfo a_stCamInfo)
        {
            try
            {
                lock (DisplayLock)
                {
                    float fCenterCoordX, fCenterCoordY;
                    fCenterCoordX = fCenterCoordY = 0.0f;

                    bool bDisplay = false;

                    #region Option ROI
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.ROI, out bDisplay))
                    {
                        if (bDisplay)
                        {
                            for (int i = 0; i < m_dicRoisForInspectionBW8.Count; i++)
                            {
                                Rectangle rRectMain = GetRoiForInspection(i);
                                if (rRectMain == Rectangle.Empty)
                                    continue;
                                rRectMain.X = (int)(((float)rRectMain.X + a_pFanXY.X) * a_dXScale);
                                rRectMain.Y = (int)(((float)rRectMain.Y + a_pFanXY.Y) * a_dYScale);

                                rRectMain.Width = (int)((float)rRectMain.Width * a_dXScale);
                                rRectMain.Height = (int)((float)rRectMain.Height * a_dYScale);

                                using (Pen pen = new Pen(Color.Blue, (float)a_dXScale)
                                {
                                    DashStyle = DashStyle.Solid,
                                    DashCap = DashCap.Round
                                })
                                {
                                    g.DrawRectangle(pen, rRectMain);
                                }

                                string drawString = ((EzInaVision.GDV.eRoiItems)i).ToString();
                                Font drawFont = new Font("Century Gothic", 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
                                SolidBrush drawBrush = new System.Drawing.SolidBrush(Color.Blue);
                                float x = rRectMain.X;
                                float y = rRectMain.Y - 20;
                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                                drawFont.Dispose();
                                drawBrush.Dispose();
                            }
                        }
                    }
                    #endregion
                    #region cross lines

                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.CROSS, out bDisplay))
                    {
                        if (bDisplay)
                        {
                            if (m_eProcessImgBW8.IsVoid == false)
                            {
                                fCenterCoordX = (float)m_eProcessImgBW8.Width / 2;
                                fCenterCoordY = (float)m_eProcessImgBW8.Height / 2;

                                float fTempStart1Y = 0.0f;//m_fCenterCoordY;
                                float fTempStart2X = 0.0f;//fCenterCoordX;
                                fTempStart1Y = ((fCenterCoordY + a_pFanXY.Y) * (float)a_dXScale);
                                fTempStart2X = ((fCenterCoordX + a_pFanXY.X) * (float)a_dYScale);

                                using (Pen pen = new Pen(Color.Blue, (float)a_dXScale)
                                {
                                    DashStyle = DashStyle.Dash,
                                    DashCap = DashCap.Round
                                })
                                {
                                    g.DrawLine(pen,
                                        ((a_pFanXY.X) * (float)a_dXScale),
                                        fTempStart1Y,
                                        ((fCenterCoordX * 2 + a_pFanXY.X) * (float)a_dXScale),
                                        fTempStart1Y);

                                    g.DrawLine(pen,
                                        fTempStart2X,
                                        ((a_pFanXY.Y) * (float)a_dYScale),
                                        fTempStart2X,
                                        (fCenterCoordY * 2 + a_pFanXY.Y) * (float)a_dYScale);
                                }
                            }
                        }
                    }

                    #endregion
                    #region MATCH
                    //if (bDisplay && !a_stCamInfo.bLive  && a_stCamInfo.bGrabbed)
                    if (a_stCamInfo.iDriverIndex > -1 ? !a_stCamInfo.bLive && a_stCamInfo.bGrabbed : true)
                    {
                        ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> dicMatchResult = null;
                        EzInaVision.GDV.MatchResult stMatchResult = null;
                        double fDispPosX, fDispPosY;
                        Rectangle rRectMain;
                        String drawString = "";
                        StringFormat drawFormat = null;
                        base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.MATCH_RESULT, out bDisplay);
                        for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
                        {
                            for (int iGoldenImg = 0; iGoldenImg < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImg++)
                            {
                                if (base.m_LibInfo.m_vecMatchResults[iRoi][iGoldenImg].Count < 1)
                                    continue;

                                #region MATCH RESULT
                                if (bDisplay)
                                {
                                    m_vecEMatchersForDisplying[iRoi][iGoldenImg].DrawPositions(g, m_ColorGreen, true, (float)a_dXScale, (float)a_dYScale, a_pFanXY.X, a_pFanXY.Y);
                                    dicMatchResult = base.m_LibInfo.m_vecMatchResults[iRoi][iGoldenImg];
                                    /*using (drawFormat = new StringFormat())
                                    {
                                        for (int i = 0; i < dicMatchResult.Count; i++)
                                        {
                                            //stMatchResult = new EzInaVision.GDV.MatchResult();
                                            stMatchResult = null;
                                            dicMatchResult.TryGetValue(i + 1, out stMatchResult);
                                            if (stMatchResult != null)
                                            {
                                                rRectMain = GetRoiForInspection(stMatchResult.m_nMatchRoiNumber);
                                                fDispPosX = ((stMatchResult.m_fDispPosX + rRectMain.X + a_pFanXY.X) * a_dXScale);
                                                fDispPosY = ((stMatchResult.m_fDispPosY + rRectMain.Y + a_pFanXY.Y) * a_dYScale);

                                                drawString = string.Format("NO.{0}\n{1:f0}%\n{2:f3}\n{3:f3}",
                                                                            stMatchResult.m_nMatchNumber,
                                                                            stMatchResult.m_fScore * 100.0,
                                                                            stMatchResult.m_fPosX,
                                                                            stMatchResult.m_fPosY);
                                                //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                                g.DrawString(drawString,
                                                m_drawFont,
                                                m_drawBrush,
                                                (float)fDispPosX,
                                                (float)fDispPosY - 9.0f,
                                                drawFormat);
                                            }

                                        }
                                    }*/
                                }
                                #endregion MATCH RESULT
                            }
                        }
                    }
                    #endregion
                    #region MATCH RESULT
                     if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.MATCH_RESULT, out bDisplay))
                     {
                         if (a_stCamInfo.iDriverIndex > -1 ? !a_stCamInfo.bLive && a_stCamInfo.bGrabbed : bDisplay)
                         {
                             for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
                             {
                                 for (int iGoldenImage = 0; iGoldenImage < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImage++)
                                 {
                                     if (base.m_LibInfo.m_vecMatchResults[iRoi][iGoldenImage].Count < 1)
                                         continue;
                                     ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> dicMatchResult = new ConcurrentDictionary<int, EzInaVision.GDV.MatchResult>();
                                     dicMatchResult = base.m_LibInfo.m_vecMatchResults[iRoi][iGoldenImage];

                                     for (int i = 0; i < dicMatchResult.Count; i++)
                                     {
                                         EzInaVision.GDV.MatchResult stMatchResult = new EzInaVision.GDV.MatchResult();
                                         dicMatchResult.TryGetValue(i + 1, out stMatchResult);
                                         Rectangle rRectMain = GetRoiForInspection(iRoi);

                                         double fDispPosX, fDispPosY;
                                         fDispPosX = ((stMatchResult.m_fDispPosX + rRectMain.X + a_pFanXY.X) * a_dXScale);
                                         fDispPosY = ((stMatchResult.m_fDispPosY + rRectMain.Y + a_pFanXY.Y) * a_dYScale);
                                         String drawString = null;
                                         drawString = string.Format("NO.{0}\n{1:f0}%\n{2:f3}\n{3:f3}",
                                                                     stMatchResult.m_nMatchNumber,
                                                                     stMatchResult.m_fScore * 100.0,
                                                                     stMatchResult.m_fSensorXPos,
                                                                     stMatchResult.m_fSensorYPos);

                                         StringFormat drawFormat = new StringFormat();
                                         //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                         g.DrawString(drawString,
                                         m_drawFont,
                                         m_drawBrush,
                                         (float)fDispPosX,
                                         (float)fDispPosY - 9.0f,
                                         drawFormat);

                                     }
                                 }

                             }
                         }
                     }
                    #endregion
                    #region BLOB
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.BLOB, out bDisplay))
                    {
                        if (bDisplay && !a_stCamInfo.bLive && a_stCamInfo.bGrabbed)
                        {
                            for (uint iRoiIndex = 0; iRoiIndex < (int)EzInaVision.GDV.eRoiItems.Max; iRoiIndex++)
                            {
                                if (base.m_LibInfo.m_vecBlobResults[Convert.ToInt32(iRoiIndex)].Count < 1)
                                    continue;
                                EzInaVision.GDV.BlobResult stBlobResult;
                                Rectangle rRectMain = GetRoiForInspection(Convert.ToInt32(iRoiIndex));
                                //Draw Object
                                #region [ DrawObject ]
                                for (int i = 0; i < GetBlobElementCount(); i++)
                                {
                                    if (base.m_LibInfo.m_vecBlobResults[Convert.ToInt32(iRoiIndex)].TryGetValue(i, out stBlobResult) == true)
                                    {
                                        m_eCodedImg.DrawObject(g, new ERGBColor(stBlobResult.BlobColor.R, stBlobResult.BlobColor.G, stBlobResult.BlobColor.B), (uint)stBlobResult.nLayerIndex, (uint)stBlobResult.nObjectIndex, (float)a_dXScale, (float)a_dYScale, a_pFanXY.X, a_pFanXY.Y);
                                    }
                                }
                                #endregion [ DrawObject ]
                            }
                        }
                        base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB, false, (k, v) => false);
                    }
                    #endregion
                    #region BLOB RESULT
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.BLOB_RESULT, out bDisplay))
                    {
                        if (bDisplay && !a_stCamInfo.bLive && a_stCamInfo.bGrabbed)
                        {
                            for (uint iRoiIndex = 0; iRoiIndex < (int)EzInaVision.GDV.eRoiItems.Max; iRoiIndex++)
                            {
                                if (base.m_LibInfo.m_vecBlobResults[Convert.ToInt32(iRoiIndex)].Count < 1)
                                    continue;
                                EzInaVision.GDV.BlobResult stBlobResult;
                                Rectangle rRectMain = GetRoiForInspection(Convert.ToInt32(iRoiIndex));
                                //Draw Inspection Result
                                #region [ Draw Result Data ]
                                for (int i = 0; i < base.m_LibInfo.m_vecBlobResults[Convert.ToInt32(iRoiIndex)].Count; i++)
                                {
                                    if (base.m_LibInfo.m_vecBlobResults[Convert.ToInt32(iRoiIndex)].TryGetValue(i, out stBlobResult) == true)
                                    {
                                        rRectMain = GetRoiForInspection(Convert.ToInt32(iRoiIndex));
                                        double fDispPosX, fDispPosY;
                                        fDispPosX = ((stBlobResult.fDispPosX + rRectMain.X + a_pFanXY.X) * a_dXScale);
                                        fDispPosY = ((stBlobResult.fDispPosY + rRectMain.Y + a_pFanXY.Y) * a_dYScale);

                                        double fDispPosW, fDispPosH;
                                        fDispPosW = ((stBlobResult.fDispWidth) * a_dXScale);
                                        fDispPosH = ((stBlobResult.fDispHeight) * a_dYScale);

                                        g.DrawEllipse(Pens.Lime,
                                            (float)(fDispPosX - (fDispPosW / 2.0)), (float)(fDispPosY - (fDispPosH / 2.0)),
                                            (float)fDispPosW, (float)fDispPosH);

                                        string drawString = string.Format("{0}", stBlobResult.bResultOK ? "OK" : "NG");


                                        StringFormat drawFormat = new StringFormat();
                                        using (Font f = new Font("Century Gothic", 9.0F * (float)a_dXScale, FontStyle.Regular, GraphicsUnit.Pixel))
                                        {
                                            using (SolidBrush brush = new SolidBrush(Color.LimeGreen))
                                            {
                                                //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                                g.DrawString(drawString,
                                                f,
                                                brush,
                                                (float)fDispPosX - (float)(9.0f * a_dXScale) / 2.0f,
                                                (float)fDispPosY - (float)(9.0f * a_dYScale) / 2.0f,
                                                drawFormat);
                                            }
                                        }
                                        using (Font f = new Font("Century Gothic", 9.0F * (float)a_dXScale, FontStyle.Regular, GraphicsUnit.Pixel))
                                        {
                                            using (SolidBrush brush = new SolidBrush(Color.LimeGreen))
                                            {
                                                drawString = string.Format("X Position {0:f2}\nY Position {1:f2}",
                                                stBlobResult.fPosX,
                                                stBlobResult.fPosY);
                                                //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                                g.DrawString(drawString,
                                                f,
                                                brush,
                                                (float)fDispPosX - (float)(9.0f * a_dXScale) / 2.0f,
                                                (float)fDispPosY + (float)(9.0f * a_dYScale),
                                                drawFormat);
                                            }
                                        }

                                    }
                                }
                                #endregion [ DrawString ] 
                            }

                        }
                        //base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB_RESULT, false, (k, v) => false);
                    }
                    #endregion
                    #region FIND
                    if (a_stCamInfo.iDriverIndex > -1 ? !a_stCamInfo.bLive && a_stCamInfo.bGrabbed : true)
                    {
                        ConcurrentDictionary<int, EzInaVision.GDV.FindResult> dicFindResult = null;
                        EzInaVision.GDV.FindResult stFindResult = null;
                        double fDispPosX, fDispPosY;
                        Rectangle rRectMain;
                        String drawString = "";
                        StringFormat drawFormat = null;
                        base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.FIND_RESULT, out bDisplay);
                        if (bDisplay)
                        {
                            for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
                            {
                                for (int iGoldenImg = 0; iGoldenImg < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImg++)
                                {
                                    if (base.m_LibInfo.m_vecFindResults[iRoi][iGoldenImg].Count < 1)
                                        continue;

                                    #region FIND RESULT
                                    dicFindResult = base.m_LibInfo.m_vecFindResults[iRoi][iGoldenImg];
                                    using (drawFormat = new StringFormat())
                                    {
                                        for (int i = 0; i < dicFindResult.Count; i++)
                                        {
                                            //stFindResult = new EzInaVision.GDV.FindResult();
                                            stFindResult = null;
                                            dicFindResult.TryGetValue(i + 1, out stFindResult);
                                            if (m_vecEFindsForDisplaying[iRoi][iGoldenImg][i] != null)
                                                m_vecEFindsForDisplaying[iRoi][iGoldenImg][i].Draw(g, m_ColorGreen, (float)a_dXScale, (float)a_dYScale, a_pFanXY.X, a_pFanXY.Y);
                                            if (stFindResult != null)
                                            {

                                                rRectMain = GetRoiForInspection(stFindResult.m_nFindRoiNumber);
                                                fDispPosX = ((stFindResult.m_fDispPosX + rRectMain.X + a_pFanXY.X) * a_dXScale);
                                                fDispPosY = ((stFindResult.m_fDispPosY + rRectMain.Y + a_pFanXY.Y) * a_dYScale);

                                                drawString = string.Format("NO.{0}\n{1:f0}%\n{2:f3}\n{3:f3}",
                                                                            stFindResult.m_nFindNumber,
                                                                            stFindResult.m_fScore * 100.0,
                                                                            stFindResult.m_fPosX,
                                                                            stFindResult.m_fPosY);
                                                //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                                g.DrawString(drawString,
                                                m_drawFont,
                                                m_drawBrush,
                                                (float)fDispPosX,
                                                (float)fDispPosY - 9.0f,
                                                drawFormat);
                                            }
                                        }
                                    }
                                }
                                #endregion FIND RESULT
                            }
                        }
                    }
                    #endregion FIND
                    #region[CALIBRATION]
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.CALIBRATION, out bDisplay))
                    {

                        if (bDisplay && !a_stCamInfo.bLive && a_stCamInfo.bGrabbed && m_bCalibrated)
                        {
                            if (!m_eSelectedROIBW8.IsVoid)
                            {
                                m_EWorldShape.SetZoom((float)a_dXScale, (float)a_dYScale);
                                m_EWorldShape.SetPan(a_pFanXY.X + m_eSelectedROIBW8.OrgX, a_pFanXY.Y + m_eSelectedROIBW8.OrgY);
                                m_EWorldShape.DrawGrid(g, new ERGBColor(255, 0, 0));
                            }


                        }
                        //base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CALIBRATION, false, (k, v) => false);
                    }
                    #endregion[CALIBRATION]
                    #region[MatrixCode]

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.MATRIX_CODE, out bDisplay))
                    {
                        if (bDisplay)
                        {
                            if (m_eMatrixCodeReaderResult != null)
                            {
                                for (int i = 0; i < m_eMatrixCodeReaderResult.Length; i++)
                                {
                                    m_eMatrixCodeReaderResult[i].DrawGrid(g, (float)a_dXScale, (float)a_dYScale, (float)a_pFanXY.X, (float)a_pFanXY.Y);
                                }
                            }
                            if (m_pMatrixCode1ResultList != null)
                            {

                                using (GraphicsPath path = new GraphicsPath())
                                using (Pen pDrawPolygon = new Pen(Color.Red, 1))
                                using (Matrix m2 = new Matrix())
                                {
                                    m2.Scale((float)a_dXScale, (float)a_dYScale);
                                    m2.Translate(a_pFanXY.X, a_pFanXY.Y);

                                    for (int i = 0; i < m_pMatrixCode1ResultList.Count; i++)
                                    {


                                        if (m_pMatrixCode1ResultList[i].m_pCornerPoint != null && m_pMatrixCode1ResultList[i].m_pCornerPoint.Count > 0)
                                        {
                                            path.StartFigure();
                                            path.AddLines(m_pMatrixCode1ResultList[i].m_pCornerPoint.ToArray());
                                            path.CloseFigure();
                                        }

                                    }
                                    //path.CloseFigure();
                                    path.Transform(m2);
                                    g.DrawPath(pDrawPolygon, path);
                                }
                            }
                        }
                        //Trace.WriteLine(string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.ElapsedMilliseconds));
                        //base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, false, (k, v) => false);
                    }
                    #endregion[MatrixCode]
                    #region [Camera Information]
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.CAMERA_INFO, true, (k, v) => true);
                    if (base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.CAMERA_INFO, out bDisplay))
                    {
                        if (bDisplay)
                        {
                            Font drawFont = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
                            SolidBrush drawBrush = new System.Drawing.SolidBrush(Color.Lime);
                            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                            PointF pt = new PointF();
                            pt.X = 5.0f;
                            pt.Y = 5.0f;
                            string drawString = string.Format("[FPS] {0:F2}", a_stCamInfo.fGetFrameRate);
                            g.DrawString(drawString, drawFont, drawBrush, pt.X, pt.Y, drawFormat);
                            pt.Y += 14.0f;
                            string strStatus = "";
                            if (a_stCamInfo.bLive) strStatus = "LIVE";
                            else if (a_stCamInfo.bIdle) strStatus = "IDLE";
                            //else if(a_stCamInfo.bGrabbing	) strStatus = "GRABBING";
                            else if (a_stCamInfo.bGrabbed) strStatus = "GRABBED";
                            drawString = string.Format("[STATUS] {0}", strStatus);
                            g.DrawString(drawString, drawFont, drawBrush, pt.X, pt.Y, drawFormat);
                            pt.Y += 14.0f;
                            drawString = string.Format("[Gray level]:{0:D3}", a_stCamInfo.nGrayVal);
                            g.DrawString(drawString, drawFont, drawBrush, pt.X, pt.Y, drawFormat);
                            pt.Y += 14.0f;
                            drawString = string.Format("[Expose]:{0:F4}", a_stCamInfo.fGetExposeTime);
                            g.DrawString(drawString, drawFont, drawBrush, pt.X, pt.Y, drawFormat);
                            pt.Y += 14.0f;
                            /*
                                                        drawString = string.Format("[Illuminate]:{0:D3}", a_stCamInfo.nLightSourceVal);
                                                        g.DrawString(drawString, drawFont, drawBrush, pt.X, pt.Y, drawFormat);
                            */
                            drawFont.Dispose();
                            drawBrush.Dispose();

                        }
                    }
                    #endregion [Camera Information]
                    //}}
                }

            }
            catch (EException exc)
            {
                if (exc.Error != EError.LicenseMissing)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion[ Display ]

    }//end of class
}//end of namespace
