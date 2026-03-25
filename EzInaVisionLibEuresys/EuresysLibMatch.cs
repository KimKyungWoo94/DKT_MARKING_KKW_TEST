using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Euresys.Open_eVision_2_14;
using System.IO;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Drawing;
using EzInaVision;

namespace EzInaVisionLibrary
{
    public sealed partial class VisionLibEuresys : VisionLibBaseClass
    {
        #region [ delegate that matching image ]
        public delegate void OnMatchEventHandler(object obj, List<List<ConcurrentDictionary<int, EzInaVision.GDV.MatchResult>>> vecMatchResult);
        private OnMatchEventHandler _OnMatchDisPlayEvent;
        public event OnMatchEventHandler OnMatchDisplayEvent
        {
            add
            {
                lock (this)
                    _OnMatchDisPlayEvent += value;
            }

            remove
            {
                lock (this)
                    _OnMatchDisPlayEvent -= value;
            }

        }
        #endregion[ delegate that matching image ]
        //Match Process
        EImageBW8 m_ePatternBW;
        EMatcher m_eMatcher;
        List<List<EMatcher>> m_vecEMatchersForDisplying;


        //ConcurrentDictionary<int, EImageBW8> m_dicPatternImageBW8;
        //ConcurrentDictionary<int, EImageC24> m_dicPatternImageC24;

        public override void MatchInit()
        {
            m_ePatternBW = new EImageBW8();
            // 			//Golden images.
            // 			m_dicPatternImageBW8 = new ConcurrentDictionary<int, EImageBW8>();
            // 			for (int i = 0; i < GDV.MAX_ROIS; i++)
            // 			{
            // 				m_dicPatternImageBW8.TryAdd(i, new EImageBW8());
            // 			}
            //For processing.
            m_eMatcher = new EMatcher();
            //For displaying.
            m_vecEMatchersForDisplying = new List<List<EMatcher>>(); //2차원 동적 배열 생성.
            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
            {
                m_vecEMatchersForDisplying.Add(new List<EMatcher>()); // List를 초기화 하고 EMatcher 를 추가 (Row)
                for (int j = 0; j < (int)EzInaVision.GDV.eGoldenImages.Max; j++)
                {
                    m_vecEMatchersForDisplying[i].Add(new EMatcher()); // List에 EMatcher 추가 (Col).
                }
            }
        }
        public override void MatchTerminate()
        {
            if (!base.m_bInitialized)
                return;
            if (!m_ePatternBW.IsVoid)
            {
                m_ePatternBW.Dispose();
                m_ePatternBW = null;
            }

            if (m_eMatcher != null)
            {
                m_eMatcher.Dispose();
                m_eMatcher = null;
            }

            if (m_vecEMatchersForDisplying != null)
            {
                m_vecEMatchersForDisplying.Clear();
                m_vecEMatchersForDisplying = null;
            }
        }
        public override bool TeachPattern(string a_strPath, string a_strCamera, string a_strType)
        {
            //a_strType = Fiducial or LED
            //a_strpatternID Fuiducial_1.bmp LED_1.bmp ...

            string file = null;

            if (!Directory.Exists(a_strPath))
            {
                return false;
            }

            file = a_strPath + "\\" + a_strCamera + "_" + a_strType + ".bmp";

            if (!File.Exists(file))
            {
                return false;
            }

            try
            {
                // 				if (m_dicPatternImageBW8.TryGetValue(a_iKey, out EPatternBW8) == false)
                // 					return false;
                m_ePatternBW.Load(file);
                m_eMatcher.LearnPattern(m_ePatternBW);

                SetParamOfMatcher();

            }
            catch (System.ArgumentException e)
            {
                MessageBox.Show(e.ParamName, e.Message.ToString());
                return false;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;

        }
        public override bool SavePattern(string a_strPath, string a_strCamera, string a_strType, Rectangle a_rect)
        {
            string file = null;

            if (!Directory.Exists(a_strPath))
            {
                return false;
            }

            file = a_strPath + "\\" + a_strCamera + "_" + a_strType + ".bmp";

            try
            {
                using (EROIBW8 RoiBW8 = new EROIBW8())
                {
                    if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == false)
                    {

                        if (!m_eProcessImgBW8.IsVoid)
                        {
                            RoiBW8.Detach();
                            RoiBW8.Attach(m_eProcessImgBW8);
                        }
                    }
                    else
                    {

                        if (!m_eFilteredImgBW8.IsVoid)
                        {
                            RoiBW8.Detach();
                            RoiBW8.Attach(m_eFilteredImgBW8);
                        }
                    }

                    if (RoiBW8.IsVoid)
                        return false;
                    RoiBW8.SetPlacement(a_rect.X, a_rect.Y, a_rect.Width, a_rect.Height);
                    RoiBW8.Save(file, EImageFileType.Bmp);
                }
            }
            catch (System.ArgumentException e)
            {
                MessageBox.Show(e.ParamName, e.Message.ToString());
                return false;
            }

            return true;

        }
        public override bool SetMatcherScale(double a_fMinScale, double a_fMaxScale)
        {
            base.m_LibInfo.m_MatchConfig.m_fMaxScale = a_fMaxScale;
            base.m_LibInfo.m_MatchConfig.m_fMinScale = a_fMinScale;

            m_eMatcher.MaxScale = (float)a_fMaxScale;
            m_eMatcher.MinScale = (float)a_fMinScale;
            return true;
        }
        public override bool SetMatcherAngleNScore(double a_fAngle, double a_fScore)
        {
            base.m_LibInfo.m_MatchConfig.m_fAngle = a_fAngle;
            base.m_LibInfo.m_MatchConfig.m_fScore = a_fScore;

            m_eMatcher.MinScore = (float)a_fScore / 100.0f;


            if (a_fAngle > 0)
            {
                m_eMatcher.MinAngle = (float)(a_fAngle * -1.0);
                m_eMatcher.MaxAngle = (float)(a_fAngle);

            }
            else
            {
                m_eMatcher.MinAngle = (float)(a_fAngle);
                m_eMatcher.MaxAngle = (float)(a_fAngle * -1.0);
            }

            return true;
        }
        public override bool SetMaxInitialPosition(uint a_nMaxInitialPositions)
        {
            m_eMatcher.MaxInitialPositions = a_nMaxInitialPositions;
            return true;
        }
        public override bool SetMatchMaximumPositions(uint a_nMaxPositions)
        {
            base.m_LibInfo.m_MatchConfig.m_fMaxPosition = a_nMaxPositions;
            m_eMatcher.MaxPositions = a_nMaxPositions;
            return true;
        }
        public override bool SetDefaultParamOfMatcher()
        {
            m_eMatcher.MinScale = (float)base.m_LibInfo.m_MatchConfig.m_fMinScale;
            m_eMatcher.MaxScale = (float)base.m_LibInfo.m_MatchConfig.m_fMaxScale;

            if (base.m_LibInfo.m_MatchConfig.m_fAngle >= 0)
            {
                m_eMatcher.MinAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle * -1.0);
                m_eMatcher.MaxAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle);

            }
            else
            {
                m_eMatcher.MinAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle);
                m_eMatcher.MaxAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle * -1.0);
            }

            m_eMatcher.MinScore = (float)base.m_LibInfo.m_MatchConfig.m_fScore / 100.0f;
            m_eMatcher.MaxPositions = (uint)base.m_LibInfo.m_MatchConfig.m_fMaxPosition;

            m_eMatcher.CorrelationMode = (ECorrelationMode)base.m_LibInfo.m_MatchConfig.m_iCorrelationMode;
            m_eMatcher.ContrastMode = (EMatchContrastMode)base.m_LibInfo.m_MatchConfig.m_iMatchContrastMode;
            return true;
        }
        public override bool SetParamOfMatcher()
        {
            m_eMatcher.MinScale = (float)base.m_LibInfo.m_MatchConfig.m_fMinScale / 100.0f;
            m_eMatcher.MaxScale = (float)base.m_LibInfo.m_MatchConfig.m_fMaxScale / 100.0f;

            if (base.m_LibInfo.m_MatchConfig.m_fAngle >= 0)
            {
                m_eMatcher.MinAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle * -1.0);
                m_eMatcher.MaxAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle);

            }
            else
            {
                m_eMatcher.MinAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle);
                m_eMatcher.MaxAngle = (float)(base.m_LibInfo.m_MatchConfig.m_fAngle * -1.0);
            }

            m_eMatcher.MinScore = (float)base.m_LibInfo.m_MatchConfig.m_fScore / 100.0f;
            m_eMatcher.MaxPositions = (uint)base.m_LibInfo.m_MatchConfig.m_fMaxPosition;

            m_eMatcher.CorrelationMode = (ECorrelationMode)base.m_LibInfo.m_MatchConfig.m_iCorrelationMode;
            m_eMatcher.ContrastMode = (EMatchContrastMode)base.m_LibInfo.m_MatchConfig.m_iMatchContrastMode;
            return true;
        }

        object lockMatch = new object();
        public override int MatchRun(string a_strGoldenImageDirPath, EzInaVision.GDV.eGoldenImages a_eGoldenImage, EzInaVision.GDV.eRoiItems a_eROI
                , bool a_bNGSave = false, string a_strSavePath = "", string a_strFileName = ""
                )
        {
            lock (lockMatch)
            {
                int nMatchedQuantity = 0;
                try
                {

                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH, false, (k, v) => false);
                    //m_dictVisionOption.AddOrUpdate(eLibOption.MATCH_RESULT, false, (k, v) => false);

                    if (a_eROI == EzInaVision.GDV.eRoiItems.None || a_eROI == EzInaVision.GDV.eRoiItems.Max)
                        return -2;

                    //매칭 결과데이터를 관리할 벡터를 클리어한다.
                    base.m_LibInfo.m_vecMatchResults[(int)a_eROI][(int)a_eGoldenImage].Clear();
                    base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].Clear();

                    //이미지 센터에서 가장 가까운 object 위치 저장.
                    base.m_LibInfo.m_vecClosePointToCenter[(int)a_eROI][(int)a_eGoldenImage].Clear();

                    //Inspection 할 이미지에 ROI Attach
                    if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == true)
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Filter, a_eROI);
                    else
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Regular, a_eROI);
                    SetRoiForInspection((int)a_eROI, base.m_LibInfo.m_dicRoiSize[(int)a_eROI]);

                    TeachPattern(a_strGoldenImageDirPath, base.m_LibInfo.m_stLibInfo.strName, a_eGoldenImage.ToString());

                    uint numOccurrences = 0;
                    double fX, fY;
                    fX = fY = 0.0;


                    EROIBW8 eRoiToMatch = null;

                    if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out eRoiToMatch))
                    {
                        if (eRoiToMatch == null)
                            return -4;
                    }

                    m_eMatcher.Match(eRoiToMatch);


                    numOccurrences = m_eMatcher.NumPositions;
                    if (numOccurrences < base.m_LibInfo.m_MatchConfig.m_fMaxPosition)
                    {
                        if (a_bNGSave)
                        {
                            if (System.IO.Directory.Exists(a_strSavePath) == false)
                            {
                                System.IO.Directory.CreateDirectory(a_strSavePath);
                            }
                            if (string.IsNullOrEmpty(a_strFileName) || string.IsNullOrWhiteSpace(a_strFileName))
                            {
                                eRoiToMatch.Save(string.Format("{0}\\MatchIMG{1}.bmp",
                                a_strSavePath,
                                DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")));
                            }
                            else
                            {
                                eRoiToMatch.Save(string.Format("{0}\\{1}.bmp",
                                                                                            a_strSavePath,
                                                                                            a_strFileName));
                            }

                        }
                    }
                    if (numOccurrences < 1 || numOccurrences > base.m_LibInfo.m_MatchConfig.m_fMaxPosition)
                        return -5;

                    m_eMatcher.CopyTo(m_vecEMatchersForDisplying[(int)a_eROI][(int)a_eGoldenImage]);


                    EzInaVision.GDV.stClosePointToCenter stClosePointToCenter = new EzInaVision.GDV.stClosePointToCenter();
                    stClosePointToCenter.Clear();
                    for (uint i = 0; i < numOccurrences; i++)
                    {
                        //m_eMatcher.GetPixelDimensions()
                        EMatchPosition eMatchPos = m_eMatcher.GetPosition(i);
                        EzInaVision.GDV.MatchResult MatchResult = new EzInaVision.GDV.MatchResult();

                        nMatchedQuantity++;

                        if (base.m_LibInfo.m_stLibInfo.bColor)
                        {
                            MatchResult.m_fPosX = eMatchPos.CenterX;
                            MatchResult.m_fPosY = eMatchPos.CenterY;
                            MatchResult.m_fDispPosX = eMatchPos.CenterX;
                            MatchResult.m_fDispPosY = eMatchPos.CenterY;
                        }
                        else
                        {
                            MatchResult.m_fPosX = eMatchPos.CenterX;
                            MatchResult.m_fPosY = eMatchPos.CenterY;
                            MatchResult.m_fDispPosX = eMatchPos.CenterX;
                            MatchResult.m_fDispPosY = eMatchPos.CenterY;

                        }

                        MatchResult.m_fAngle = eMatchPos.Angle;
                        MatchResult.m_fScore = eMatchPos.Score;
                        MatchResult.m_nMatchNumber = nMatchedQuantity;

                        //This is the calculation for the Descartes coordinate.
                        fX = (eRoiToMatch.OrgX + MatchResult.m_fPosX) - base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
                        fY = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY - (eRoiToMatch.OrgY + MatchResult.m_fPosY);

                        // add by smpark
                        MatchResult.m_fSensorXPos = (float)(eRoiToMatch.OrgX + MatchResult.m_fPosX);
                        MatchResult.m_fSensorYPos = (float)(eRoiToMatch.OrgY + MatchResult.m_fPosY);
                        MatchResult.m_fIMGCenterX = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
                        MatchResult.m_fIMGCenterY = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;
                        MatchResult.m_fLensScaleX = base.m_LibInfo.m_stLibInfo.dLensScaleX;
                        MatchResult.m_fLensScaleY = base.m_LibInfo.m_stLibInfo.dLensScaleY;
                        MatchResult.m_fMatchWidth = m_eMatcher.PatternWidth;
                        MatchResult.m_fMatchHeight = m_eMatcher.PatternHeight;
                        // add by smpark

                        MatchResult.m_fPosX = fX * base.m_LibInfo.m_stLibInfo.dLensScaleX;
                        MatchResult.m_fPosY = fY * base.m_LibInfo.m_stLibInfo.dLensScaleY;

                        //This is the code to find the close point of center in the circle.

                        stClosePointToCenter.fSetR = Math.Sqrt(Math.Pow(fX, 2) + Math.Pow(fY, 2));


                        if (stClosePointToCenter.fMaxR > stClosePointToCenter.fSetR)
                        {
                            stClosePointToCenter.fMaxR = stClosePointToCenter.fSetR;
                            stClosePointToCenter.fPosX = MatchResult.m_fPosX;
                            stClosePointToCenter.fPosY = MatchResult.m_fPosY;
                            stClosePointToCenter.fAngle = MatchResult.m_fAngle;
                            stClosePointToCenter.fScore = MatchResult.m_fScore;
                        }

                        MatchResult.m_eStatus = EzInaVision.GDV.eChipStatus.CHIP_EXISTS;

                        bool bInspection = false;
                        base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.INSPECT_INSIDE, out bInspection);
                        if (bInspection)
                        {
                        }

                        base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.INSPECT_OUTSIDE, out bInspection);
                        if (bInspection)
                        {
                        }

                        stClosePointToCenter.fAVGAngle += MatchResult.m_fAngle;

                        base.m_LibInfo.m_vecMatchResults[(int)a_eROI][(int)a_eGoldenImage].AddOrUpdate(nMatchedQuantity, MatchResult, (k, v) => MatchResult);
                        base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].AddOrUpdate(nMatchedQuantity, MatchResult, (k, v) => MatchResult);
                    }

                    //This is the average of the angle of matched chips
                    stClosePointToCenter.fAVGAngle /= nMatchedQuantity;
                    base.m_LibInfo.m_vecClosePointToCenter[(int)a_eROI][(int)a_eGoldenImage] = stClosePointToCenter;
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH, true, (k, v) => true);
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH_RESULT, true, (k, v) => true);

                    if (_OnMatchDisPlayEvent != null)
                        _OnMatchDisPlayEvent(this, base.m_LibInfo.m_vecMatchResultsForCalculation);

                }
                catch (Euresys.Open_eVision_2_14.EException exc)
                {
                    MessageBox.Show(exc.Message, "Euresys Exception");
                    return -1;
                }

                return nMatchedQuantity;
            }
        }
        public override bool MatchResult(EzInaVision.GDV.eGoldenImages a_eGoldenImage, EzInaVision.GDV.eRoiItems a_eROI, out List<EzInaVision.GDV.MatchResult> a_MatchResultLists)
        {
            try
            {

                a_MatchResultLists = null;
                if (m_LibInfo.m_vecMatchResultsForCalculation != null &&
                      base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage] != null &&
                      base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].Count > 0
                      )
                {
                    a_MatchResultLists = base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].Values.ToList().ConvertAll(o => (EzInaVision.GDV.MatchResult)o.Clone());
                    return true;
                }

            }
            catch (Exception exc)
            {
                a_MatchResultLists = null;
                return false;
            }
            return false;
        }
        public override int GetMatchResultCount(EzInaVision.GDV.eGoldenImages a_eGoldenImage, EzInaVision.GDV.eRoiItems a_eROI)
        {
            return base.m_LibInfo.m_vecMatchResults[(int)a_eROI][(int)a_eGoldenImage].Count;
        }
        public override void ClearMatchResult(EzInaVision.GDV.eGoldenImages a_eGoldenImage, EzInaVision.GDV.eRoiItems a_eROI)
        {

            base.m_LibInfo.m_vecMatchResults[(int)a_eROI][(int)a_eGoldenImage].Clear();
            base.m_LibInfo.m_vecMatchResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].Clear();
            //m_vecEMatchersForDisplying[iRoi][iGoldenImg].Clear();					
        }
        public override int GetMatchResultForCalculation()
        {
            return base.m_LibInfo.m_dicMatchResultForCalculation.Count;
        }
        public override void ClearAllMatchResults()
        {
            for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
            {
                for (int iGoldenImg = 0; iGoldenImg < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImg++)
                {
                    base.m_LibInfo.m_vecMatchResults[iRoi][iGoldenImg].Clear();
                    base.m_LibInfo.m_vecMatchResultsForCalculation[iRoi][iGoldenImg].Clear();
                }
            }

        }
        public override bool DrawRoiImage(int a_iKey, Graphics a_g)
        {
            if (m_dicRoisForInspectionBW8.IsEmpty)
                return false;

            EROIBW8 Roi;
            if (m_dicRoisForInspectionBW8.TryGetValue(a_iKey, out Roi) == true)
                Roi.Draw(a_g);
            else
                return false;

            return true;
        }
    }//end of class
}//end of namespace


