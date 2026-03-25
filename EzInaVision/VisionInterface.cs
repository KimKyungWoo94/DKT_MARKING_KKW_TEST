using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzInaVision.GDV;

namespace EzInaVision
{
    #region [VisionInterface_CAM]
    public interface VisionInterface_CAM
    {
        bool Initialize();
        void Terminate();

        bool OpenDevice();
        bool CloseDevice();

        bool OpenConfig(string a_strPath);
        bool SaveConfig(string a_strPath);

        bool IsConnected();
        void Reconnect();

        bool Live();
        bool Idle();
        bool Grab();
        bool IsGrab();
        bool IsLive();
        bool IsStop();

        int GetImageWidth();
        int GetImageHeight();

        void SetGain(int a_nValue);
        float GetGain();
        void SetExposeTime(int a_nTime);
        float GetExpose();

        bool LoadImage(string a_strPath);
        bool SaveImage(string a_strPath);

        string GetSerialNum();

        float GetFrameRate();
    }
    #endregion [VisionInterface_CAM]

    public abstract class VisionCamBaseClass : IDisposable, VisionInterface_CAM
    {
        protected Type m_pbaseType = null;
        public Type BaseType { get { return m_pbaseType; } }

        public GDV.stCamInfo m_stCamInfo;
        protected bool m_bConnected;

        public VisionCamBaseClass(EzInaVision.GDV.stCamInfo a_stCamInfo)
        {
            m_stCamInfo = new EzInaVision.GDV.stCamInfo();
            m_stCamInfo = a_stCamInfo;
        }

        public virtual void Dispose()
        {


        }

        public abstract bool Initialize();
        public abstract void Terminate();

        public abstract bool OpenDevice();
        public abstract bool CloseDevice();

        public abstract bool OpenConfig(string a_strPath);
        public abstract bool SaveConfig(string a_strPath);

        public abstract bool IsConnected();
        public abstract void Reconnect();

        public abstract bool Live();
        public abstract bool Idle();
        public abstract bool Grab();
        public abstract bool IsGrab();
        public abstract bool IsLive();
        public abstract bool IsIdle();
        public abstract bool IsStop();
        public abstract int GetImageWidth();
        public abstract int GetImageHeight();

        public abstract void SetGain(int a_nValue);
        public abstract void SetExposeTime(int a_nTime);
        public abstract float GetGain();
        public abstract float GetExpose();

        public abstract bool LoadImage(string a_strPath);
        public abstract bool SaveImage(string a_strPath);

        public abstract string GetSerialNum();

        public abstract float GetFrameRate();


    }


    #region [VisionInterface_LIB]
    public interface VisionInterface_LIB
    {
        #region [ Lib ]
        bool Initialize();
        void Terminate();
        void LibInit();
        void LibTerminate();

        bool OpenConfig(string a_strPath);
        bool SaveConfig(string a_strPath);

        bool GetOption(GDV.eLibOption a_eOption);
        void SetOption(GDV.eLibOption a_eOption, bool a_bUse);
        void SetOptions(ConcurrentDictionary<GDV.eLibOption, bool> a_dicOptions);

        Rectangle GetRoiForInspection(int a_iKey);
        void SetRoiForInspection(int a_iKey, Rectangle a_rRect);
        int GetPixel(Point a_pPoint);
        void AttachOneROIToTheSrcImg(GDV.eImageType a_eType, GDV.eRoiItems a_eROI);
        bool AttachOneROIToTheSrcImg(GDV.eImageType a_eType, int a_eROI);
        void AttachAllROIsToTheSrcImg(GDV.eImageType a_eType);

        bool AddRoiForInspection(int iKey);
        #endregion[ Lib ]
        #region [ Filter ]
        void FiltersInit();
        void FiltersTerminate();
        void ThresholdOfFilters();
        void ThresholdOfFiltersWithoutCam();
        void OpenDiskOfFilters();
        void CloseDiskOfFilters();
        void ErodeDiskOfFilters();
        void DilateDiskOfFilters();
        #endregion[ Filter ]
        #region [ Matcher ]
        void MatchInit();
        void MatchTerminate();
        bool TeachPattern(string a_strPath, string a_strCamera, string a_strType);
        bool SavePattern(string a_strPath, string a_strCamera, string a_strtype, Rectangle a_rect);
        bool SetMatcherScale(double a_dMinScale, double a_dMaxScale);
        bool SetMatcherAngleNScore(double a_dAngle, double a_dScore);
        bool SetMaxInitialPosition(uint a_nMaxInitialPositions);
        bool SetMatchMaximumPositions(uint a_nMaxPositions);
        bool SetDefaultParamOfMatcher();
        bool SetParamOfMatcher();
        int MatchRun(string a_strGoldenImageDirPath, GDV.eGoldenImages a_eGoldenImage, GDV.eRoiItems a_eROI, bool a_bNGSave = false, string a_strSavePath = "", string a_strFileName = "");
        bool MatchResult(eGoldenImages a_eGoldenImage, eRoiItems a_eROI, out List<EzInaVision.GDV.MatchResult> a_MatchResultLists);
        int GetMatchResultCount(eGoldenImages a_eGoldenImage, eRoiItems a_eROI);
        void ClearMatchResult(eGoldenImages a_eGoldenImage, eRoiItems a_eROI);
        int GetMatchResultForCalculation();
        void ClearAllMatchResults();
        bool DrawRoiImage(int a_iKey, Graphics a_g);
        #endregion[ Matcher ]
        #region [ Blob ]
        void BlobInit();
        void BlobTerminate();
        int BlobRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "");
        bool BlobResult(int a_nIndex, out EzInaVision.GDV.BlobResult a_BlobResult);
        List<KeyValuePair<int, EzInaVision.GDV.BlobResult>> GetBlobResultList(EzInaVision.GDV.eRoiItems a_eRoiItem);
        void ClearBlobResults();
        void SetDefaultBlobMethod();
        void SetBlobMethod(GDV.BlobConfig a_BlobParam);
        uint GetBlobElementCount();
        #endregion[ Blob ]
        #region [Gauge]
        void GaugeInit();
        void GaugeTerminate();

        int GaugeCalibrationModelLoad(string a_strFilePath, float a_fPitchX, float a_fPitchY);
        int GaugeCalibration(EzInaVision.GDV.eRoiItems a_eROI, float a_nPitchX, float a_nPitchY);
        int GaugeUnwarping(Bitmap a_OrginIMG);

        int GetSensorToWorldCordinate(PointF a_Point, PointF a_Center, ref PointF a_worldPoint);
        int GetSensorToLocalCordinate(PointF a_Point, PointF a_Center, ref PointF a_worldPoint);

        #endregion
        #region  [ FIND ]
        void FIND_Init();
        void FIND_Terminate();
        bool FIND_TeachPattern(string a_strPath, string a_strCamera, string a_strType);
        bool FIND_TeachingPattern(string a_strImgPath);
        bool FIND_TeachingPattern(Image a_pImg);
        bool FIND_SavePattern(string a_strPath, string a_strCamera, string a_strtype, Rectangle a_rect);
        bool SetFinderScale(double a_dBias, double a_dTolerance);
        bool SetFinderAngle(double a_dBias, double a_dTolerance);
        bool SetFinderScore(double a_dScore);
        bool SetFinderMaximumInstanceCount(uint a_nMaxPositions);
        bool SetFinderDefaultParam();
        bool SetFinderParam();
        int FindRun(string a_strGoldenImageDirPath, GDV.eGoldenImages a_eGoldenImage, GDV.eRoiItems a_eROI);
        bool FindResult(int a_nIndex, out EzInaVision.GDV.FindResult a_tMatchResult);
        int GetFindResultCount();
        void ClearFindResult();
        int GetFindResultForCalculation();
        void ClearAllFindResults();
        bool FindDrawRoiImage(int a_iKey, Graphics a_g);


        void DrawFindLearnedModelGeometry(Graphics g, double a_dXScale, double a_dYScale, PointF a_pFanXY);
        #endregion FIND
        #region [MatrixCode]
        void MatrixCodeInit();
        void MatrixCodeTerminate();
        int MatrixCodeRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "");
        void ClearMatrixCodeResults();
        void SetDefaultMatrixCodeMethod();
        void SetMatrixCodeMethod();
        #endregion
        #region [MatrixCode1]
        void MatrixCode1Init();
        void MatrixCode1Terminate();
        int MatrixCode1MultiRun(int a_eRoiItem, Rectangle[] a_Areaes, float a_IMGSpanRatio, bool a_bSave = false, string a_strFilePath = "", string[] a_strFileNames = null);
        void ClearMatrixCode1Results();
        void SetDefaultMatrixCode1Method();
        void SetMatrixCode1Method();
        void SetMatrixCode1ReadTimeout(uint a_ms);
        int GetMatrixCode1TotalResultCount();

        void GetMatrixCode1Result(int Index, out EzInaVision.GDV.MatrixCodeResult a_bufResult);
        void GetMatrixCode1ResultList(out List<EzInaVision.GDV.MatrixCodeResult> a_bufList);

        #endregion
    }
    public abstract class VisionLibBaseClass : IDisposable, VisionInterface_LIB
    {
        protected Type m_pbaseType = null;
        public Type BaseType
        {
            get
            {
                return m_pbaseType;
            }
        }

        public GDV.LibInfo m_LibInfo;

        protected bool m_bInitialized;

        public VisionLibBaseClass(EzInaVision.GDV.stLibInfo a_stLibInfo)
        {
            m_LibInfo = new EzInaVision.GDV.LibInfo(a_stLibInfo);
        }

        public double GetRelXPosToMoving(Point a_pt)
        {
            //This is the calculation for the Descartes coordinate.
            double dX = 0.0;
            dX = a_pt.X - m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
            dX *= m_LibInfo.m_stLibInfo.dLensScaleX;
            return dX;
        }
        public double GetRelYPosToMoving(Point a_pt)
        {
            //This is the calculation for the Descartes coordinate.
            double dY = 0.0;
            dY = a_pt.Y - m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;
            dY *= m_LibInfo.m_stLibInfo.dLensScaleY;
            return dY;
        }

        #region [ IDisposable Support ]
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~VisionLibBaseClass() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion [ IDisposable Support ]

        #region [ Lib ]
        public abstract bool Initialize();
        public abstract void Terminate();
        public abstract bool IsInitialized();
        public abstract void LibInit();
        public abstract void LibTerminate();
        public abstract bool OpenConfig(string a_strPath);
        public abstract bool SaveConfig(string a_strPath);
        public abstract bool GetOption(eLibOption a_eOption);
        public abstract void SetOption(eLibOption a_eOption, bool a_bUse);

        public abstract void SetVisionScale(double a_scaleX, double a_scaleY);
        public abstract bool GetVisionScale(ref double a_scaleX, ref double a_scaleY);

        public abstract void SetOptions(ConcurrentDictionary<eLibOption, bool> a_dicOptions);
        public abstract Rectangle GetRoiForInspection(int a_iKey);
        public abstract void SetRoiForInspection(int a_iKey, Rectangle a_rRect);

        // add by smpark
        public abstract bool AddRoiForInspection(int iKey);
        // add by smpark
        public abstract int GetPixel(Point a_pPoint);
        public abstract void AttachOneROIToTheSrcImg(GDV.eImageType a_eType, GDV.eRoiItems a_eROI);
        public abstract bool AttachOneROIToTheSrcImg(GDV.eImageType a_eType, int a_eROI);
        public abstract void AttachAllROIsToTheSrcImg(GDV.eImageType a_eType);
        public abstract void Display(Graphics g, double a_dXScale, double a_dYScale, PointF a_pFanXY, EzInaVision.GDV.stCamInfo a_stCamInfo);
        #endregion[ Lib ]
        #region [ Filters ]
        public abstract void FiltersInit();
        public abstract void FiltersTerminate();
        public abstract void ThresholdOfFilters();
        public abstract void ThresholdOfFiltersWithoutCam();
        public abstract void OpenDiskOfFilters();
        public abstract void CloseDiskOfFilters();
        public abstract void ErodeDiskOfFilters();
        public abstract void DilateDiskOfFilters();
        #endregion [ Filters ]
        #region [ Matcher ]
        public abstract void MatchInit();
        public abstract void MatchTerminate();
        public abstract bool TeachPattern(string a_strPath, string a_strCamera, string a_strType);
        public abstract bool SavePattern(string a_strPath, string a_strCamera, string a_strtype, Rectangle a_rect);
        public abstract bool SetMatcherScale(double a_dMinScale, double a_dMaxScale);
        public abstract bool SetMatcherAngleNScore(double a_dAngle, double a_dScore);
        public abstract bool SetMaxInitialPosition(uint a_nMaxInitialPositions);
        public abstract bool SetMatchMaximumPositions(uint a_nMaxPositions);
        public abstract bool SetDefaultParamOfMatcher();
        public abstract bool SetParamOfMatcher();
        public abstract int MatchRun(string a_strGoldenImageDirPath, eGoldenImages a_eGoldenImage, eRoiItems a_eROI, bool a_bNGSave = false, string a_strSavePath = "", string a_strFileName = "");
        public abstract bool MatchResult(eGoldenImages a_eGoldenImage, eRoiItems a_eROI, out List<EzInaVision.GDV.MatchResult> a_MatchResultLists);
        public abstract int GetMatchResultCount(eGoldenImages a_eGoldenImage, eRoiItems a_eROI);
        public abstract void ClearMatchResult(eGoldenImages a_eGoldenImage, eRoiItems a_eROI);
        public abstract int GetMatchResultForCalculation();
        public abstract void ClearAllMatchResults();
        public abstract bool DrawRoiImage(int a_iKey, Graphics a_g);
        #endregion [ Matcher ]

        #region [ FIND ]

        public abstract void FIND_Init();
        public abstract void FIND_Terminate();
        public abstract bool FIND_TeachPattern(string a_strPath, string a_strCamera, string a_strType);
        public abstract bool FIND_TeachingPattern(string a_strImgPath);
        public abstract bool FIND_TeachingPattern(Image a_pImg);
        public abstract bool FIND_SavePattern(string a_strPath, string a_strCamera, string a_strtype, Rectangle a_rect);
        public abstract bool SetFinderScale(double a_dBias, double a_dTolerance);
        public abstract bool SetFinderAngle(double a_dBias, double a_dTolerance);
        public abstract bool SetFinderScore(double a_dScore);
        public abstract bool SetFinderMaximumInstanceCount(uint a_nMaxPositions);
        public abstract bool SetFinderDefaultParam();
        public abstract bool SetFinderParam();
        public abstract int FindRun(string a_strGoldenImageDirPath, GDV.eGoldenImages a_eGoldenImage, GDV.eRoiItems a_eROI);
        public abstract bool FindResult(int a_nIndex, out EzInaVision.GDV.FindResult a_tMatchResult);
        public abstract int GetFindResultCount();
        public abstract void ClearFindResult();
        public abstract int GetFindResultForCalculation();
        public abstract void ClearAllFindResults();
        public abstract bool FindDrawRoiImage(int a_iKey, Graphics a_g);
        public abstract void DrawFindLearnedModelGeometry(Graphics g, double a_dXScale, double a_dYScale, PointF a_pFanXY);

        #endregion FIND


        #region [ Blob ]
        public abstract void BlobInit();
        public abstract void BlobTerminate();
        public abstract int BlobRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "");
        public abstract bool BlobResult(int a_nIndex, out EzInaVision.GDV.BlobResult a_stBlobResult);
        public abstract List<KeyValuePair<int, EzInaVision.GDV.BlobResult>> GetBlobResultList(EzInaVision.GDV.eRoiItems a_eRoiItem);
        public abstract void ClearBlobResults();
        public abstract void SetDefaultBlobMethod();
        public abstract void SetBlobMethod(BlobConfig a_stBlobParam);
        public abstract uint GetBlobElementCount();
        #endregion[ Blob ]
        #region [Gauge]
        public abstract void GaugeInit();
        public abstract void GaugeTerminate();
        public abstract int GaugeCalibrationModelLoad(string a_strFilePath, float a_fPitchX, float a_fPitchY);
        public abstract int GaugeCalibration(EzInaVision.GDV.eRoiItems a_eROI, float a_nPitchX, float a_nPitchY);
        public abstract int GaugeUnwarping(Bitmap a_OrginIMG);
        public abstract int GetSensorToWorldCordinate(PointF a_Point, PointF a_Center, ref PointF a_worldPoint);
        public abstract int GetSensorToLocalCordinate(PointF a_Point, PointF a_Center, ref PointF a_worldPoint);

        #endregion[Gauge]
        #region [MatrixCode]
        public abstract void MatrixCodeInit();
        public abstract void MatrixCodeTerminate();
        public abstract int MatrixCodeRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "");
        public abstract void ClearMatrixCodeResults();
        public abstract void SetDefaultMatrixCodeMethod();
        public abstract void SetMatrixCodeMethod();
        #endregion


        #region [MatrixCode1]
        public abstract void MatrixCode1Init();
        public abstract void MatrixCode1Terminate();
        public abstract int MatrixCode1MultiRun(int a_eRoiItem, Rectangle[] a_Areaes, float a_IMGSpanRatio, bool a_bSave = false, string a_strFilePath = "", string[] a_strFileNames = null);
        public abstract void ClearMatrixCode1Results();
        public abstract void SetDefaultMatrixCode1Method();
        public abstract void SetMatrixCode1Method();
        public abstract void SetMatrixCode1ReadTimeout(uint a_ms);
        public abstract int GetMatrixCode1TotalResultCount();
        public abstract void GetMatrixCode1Result(int Index, out EzInaVision.GDV.MatrixCodeResult a_bufResult);
        public abstract void GetMatrixCode1ResultList(out List<EzInaVision.GDV.MatrixCodeResult> a_bufList);


        #endregion
    }
    #endregion[VisionInterface_LIB]
}
