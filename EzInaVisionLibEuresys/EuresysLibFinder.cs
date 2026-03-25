using Euresys.Open_eVision_2_14;
using System.IO;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Drawing;
using EzInaVision;
using System.Collections.Generic;
using System;

namespace EzInaVisionLibrary
{
    public sealed partial class VisionLibEuresys : VisionLibBaseClass
    {

        #region [ delegate that matching image ]
        public delegate void OnFindEventHandler(object obj, List<List<ConcurrentDictionary<int, EzInaVision.GDV.FindResult>>> vecMatchResult);
        private OnFindEventHandler _OnFindDisPlayEvent;
        public event OnFindEventHandler OnFindDisplayEvent
        {
            add
            {
                lock (this)
                    _OnFindDisPlayEvent += value;
            }

            remove
            {
                lock (this)
                    _OnFindDisPlayEvent -= value;
            }

        }
        #endregion[ delegate that matching image ]
        private EImageBW8 m_eFindPatternBW;
        private EPatternFinder m_pFinder;
        private List<List<List<EFoundPattern>>> m_vecEFindsForDisplaying;
        public override void FIND_Init()
        {
            m_eFindPatternBW = new EImageBW8();
            m_pFinder = new EPatternFinder();
            m_vecEFindsForDisplaying = new List<List<List<EFoundPattern>>>();
            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
            {
                m_vecEFindsForDisplaying.Add(new List<List<EFoundPattern>>()); // List를 초기화 하고 EMatcher 를 추가 (Row)
                for (int j = 0; j < (int)EzInaVision.GDV.eGoldenImages.Max; j++)
                {
                    m_vecEFindsForDisplaying[i].Add(new List<EFoundPattern>()); // List에 EMatcher 추가 (Col).
                }
            }
        }
        public override void FIND_Terminate()
        {

            if (!base.m_bInitialized)
                return;

            if (!m_eFindPatternBW.IsVoid)
            {
                m_eFindPatternBW.Dispose();
                m_eFindPatternBW = null;
            }

            if (m_pFinder != null)
            {
                m_pFinder.Dispose();
                m_pFinder = null;
            }

            if (m_vecEFindsForDisplaying != null)
            {
                for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                {
                    
                    for (int j = 0; j < (int)EzInaVision.GDV.eGoldenImages.Max; j++)
                    {
                        if(m_vecEFindsForDisplaying[i][j]!=null)
                        {
                            for(int k=0;k < m_vecEFindsForDisplaying[i][j].Count;k++)
                            {
                                if (m_vecEFindsForDisplaying[i][j][k]!=null)
                                m_vecEFindsForDisplaying[i][j][k].Dispose();
                            }
                            m_vecEFindsForDisplaying[i][j].Clear();
                        }                          
                    }
                    m_vecEFindsForDisplaying[i].Clear();
                }
                m_vecEFindsForDisplaying.Clear();
                m_vecEFindsForDisplaying = null;
            }

        }
        public override bool FIND_TeachPattern(string a_strPath, string a_strCamera, string a_strType)
        {
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
                using (EImageBW8 eProcessBW = new EImageBW8())
                {
                    eProcessBW.Load(file);
                    m_eFindPatternBW.SetSize(eProcessBW);
                    //the eProcessBW image copy to m_eProcessImgBW8
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eFindPatternBW);
                }
                //m_eFindPatternBW.Load(file);
                m_pFinder.Learn(m_eFindPatternBW);
                SetFinderParam();
                return m_pFinder.LearningDone;
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
        }
        public override bool FIND_TeachingPattern(string a_strImgPath)
        {

            FileInfo pFileInfo = new FileInfo(a_strImgPath);
            
                if (!Directory.Exists(pFileInfo.DirectoryName))
                {
                    return false;
                }              

                if (!pFileInfo.Exists)
                {
                    return false;
                }

                try
                {
                // 				if (m_dicPatternImageBW8.TryGetValue(a_iKey, out EPatternBW8) == false)
                // 					return false;
                    using (EImageBW8 eProcessBW = new EImageBW8())
                    {
                        eProcessBW.Load(a_strImgPath);
                        EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eFindPatternBW);
                    }                  
                    m_pFinder.Learn(m_eFindPatternBW);
                    SetFinderParam();                
                    return m_pFinder.LearningDone;
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
        }
        public override bool FIND_TeachingPattern(Image a_pImg)
        { 
            if(a_pImg!=null)
            {
                switch(a_pImg.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        {
                            break;
                        }
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                        {
                            break;
                        }
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                        {                         
                            using (EImageBW8 eProcessBW = new EImageBW8(GetEImageBW8((Bitmap)a_pImg)))
                            {
                                m_eFindPatternBW.SetSize(eProcessBW);
                                //the eProcessBW image copy to m_eProcessImgBW8
                                EasyImage.Oper(EArithmeticLogicOperation.Copy, eProcessBW, m_eFindPatternBW);                             
                            }
                            break;
                        }
                }
                if(m_eFindPatternBW.IsVoid==false)
                {
                    m_pFinder.Learn(m_eFindPatternBW);
                    SetFinderParam();
                    return m_pFinder.LearningDone;
                }
            }
            return false;
        }
        public override bool FIND_SavePattern(string a_strPath, string a_strCamera, string a_strtype, Rectangle a_rect)
        {
            string file = null;

            if (!Directory.Exists(a_strPath))
            {
                return false;
            }

            file = a_strPath + "\\" + a_strCamera + "_" + a_strtype + ".bmp";

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

        public override bool SetFinderScale(double a_dBias, double a_dTolerance)
        {
            base.m_LibInfo.m_FindConig.m_fScaleBias = a_dBias;
            base.m_LibInfo.m_FindConig.m_fScaleTolerance = a_dTolerance;

            m_pFinder.ScaleBias = (float)a_dBias;
            m_pFinder.ScaleTolerance = (float)a_dTolerance;
            return true;
        }
        public override bool SetFinderAngle(double a_dBias, double a_dTolerance)
        {
            base.m_LibInfo.m_FindConig.m_fAngleBias = a_dBias;
            base.m_LibInfo.m_FindConig.m_fAngleTolerance = a_dTolerance;

            m_pFinder.AngleBias         = (float)a_dBias;
            m_pFinder.ScaleTolerance    = (float)a_dTolerance;
            return true;
        }
        public override bool SetFinderScore(double a_dScore)
        {            
            base.m_LibInfo.m_FindConig.m_fScore = a_dScore;
            m_pFinder.MinScore = (float)a_dScore;
            return true;
        }
        public override bool SetFinderMaximumInstanceCount(uint a_nMaxPositions)
        {
            base.m_LibInfo.m_FindConig.m_uiMaxPosition = a_nMaxPositions;
            m_pFinder.MaxInstances = a_nMaxPositions;
            return true;
        }
        public override bool SetFinderDefaultParam()
        {
            m_pFinder.ScaleBias = 1.0f;
            m_pFinder.ScaleTolerance = 0.0f;
            m_pFinder.AngleBias = 0.0f;
            m_pFinder.ScaleTolerance = 0.0f;

            m_pFinder.MinScore = -1.0f;
            m_pFinder.MaxInstances = 1;
            m_pFinder.ContrastMode = EFindContrastMode.Normal;
            return true;
        }
        public override bool SetFinderParam()
        {
            m_pFinder.ScaleBias = (float)base.m_LibInfo.m_FindConig.m_fScaleBias;
            m_pFinder.ScaleTolerance = (float)base.m_LibInfo.m_FindConig.m_fScaleTolerance;
            m_pFinder.AngleBias = (float)base.m_LibInfo.m_FindConig.m_fAngleBias;
            m_pFinder.AngleTolerance = (float)base.m_LibInfo.m_FindConig.m_fAngleTolerance;

            m_pFinder.MinScore = (float)base.m_LibInfo.m_FindConig.m_fScore;
            m_pFinder.MaxInstances = base.m_LibInfo.m_FindConig.m_uiMaxPosition;
            m_pFinder.ContrastMode = (EFindContrastMode)base.m_LibInfo.m_FindConig.m_iContrastMode;

            return false;
        }
        object lockFind = new object();
        public override int FindRun(string a_strGoldenImageDirPath, EzInaVision.GDV.eGoldenImages a_eGoldenImage, EzInaVision.GDV.eRoiItems a_eROI)
        {
            lock (lockFind)
            {
                int nMatchedQuantity = 0;
                try
                {
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.FIND, false, (k, v) => false);
                    //m_dictVisionOption.AddOrUpdate(eLibOption.MATCH_RESULT, false, (k, v) => false);

                    if (a_eROI == EzInaVision.GDV.eRoiItems.None || a_eROI == EzInaVision.GDV.eRoiItems.Max)
                        return -2;

                  
                    //매칭 결과데이터를 관리할 벡터를 클리어한다.
                    base.m_LibInfo.m_vecFindResults[(int)a_eROI][(int)a_eGoldenImage].Clear();
                    base.m_LibInfo.m_vecFindResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].Clear();


                    if (m_vecEFindsForDisplaying[(int)a_eROI][(int)a_eGoldenImage].Count > 0)
                    {
                        for (int i = 0; i < m_vecEFindsForDisplaying[(int)a_eROI][(int)a_eGoldenImage].Count; i++)
                        {
                            m_vecEFindsForDisplaying[(int)a_eROI][(int)a_eGoldenImage][i].Dispose();
                        }
                        m_vecEFindsForDisplaying[(int)a_eROI][(int)a_eGoldenImage].Clear();
                    }
                    //이미지 센터에서 가장 가까운 object 위치 저장.
                    //base.m_LibInfo.m_vecClosePointToCenter[(int)a_eROI][(int)a_eGoldenImage].Clear();

                    //Inspection 할 이미지에 ROI Attach
                    if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == true)
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Filter, a_eROI);
                    else
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Regular, a_eROI);

                    SetRoiForInspection((int)a_eROI, base.m_LibInfo.m_dicRoiSize[(int)a_eROI]);

                    if (!FIND_TeachPattern(a_strGoldenImageDirPath, base.m_LibInfo.m_stLibInfo.strName, a_eGoldenImage.ToString()))
                        return -3;

                    int numOccurrences = 0;
                    double fX, fY;
                    fX = fY = 0.0;

                    EROIBW8 eRoiToFind = null;
                    EFoundPattern[] pFoundPattern = null;
                    if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out eRoiToFind))
                    {
                        if (eRoiToFind == null)
                            return -4;
                    }


                    pFoundPattern=m_pFinder.Find(eRoiToFind);

                    if(pFoundPattern!=null )
                    {
                        numOccurrences = pFoundPattern.Length;

                        if (numOccurrences < 1 || numOccurrences > base.m_LibInfo.m_MatchConfig.m_fMaxPosition)
                            return -5;
                           
                        EFoundPattern pResultPattern = null;                   
                        for (uint i = 0; i < numOccurrences; i++)
                        {
                            m_vecEFindsForDisplaying[(int)a_eROI][(int)a_eGoldenImage].Add(new EFoundPattern(pFoundPattern[i]));
                            pResultPattern = pFoundPattern[i];                            
                            EzInaVision.GDV.FindResult pResult = new EzInaVision.GDV.FindResult();

                            nMatchedQuantity++;

                            if (base.m_LibInfo.m_stLibInfo.bColor)
                            {
                                pResult.m_fPosX = pResultPattern.Center.X;
                                pResult.m_fPosY = pResultPattern.Center.Y;
                                pResult.m_fDispPosX = pResultPattern.Center.X;
                                pResult.m_fDispPosY = pResultPattern.Center.Y;
                            }
                            else
                            {
                                pResult.m_fPosX = pResultPattern.Center.X;
                                pResult.m_fPosY = pResultPattern.Center.Y;
                                pResult.m_fDispPosX = pResultPattern.Center.X;
                                pResult.m_fDispPosY = pResultPattern.Center.Y;
                            }

                            pResult.m_fAngle = pResultPattern.Angle;
                            pResult.m_fScore = pResultPattern.Score;
                            pResult.m_nFindNumber = nMatchedQuantity;
                            pResult.m_nFindRoiNumber = (int)a_eROI;
                            //This is the calculation for the Descartes coordinate.
                            fX = (eRoiToFind.OrgX + pResult.m_fPosX) - base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
                            fY = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY - (eRoiToFind.OrgY + pResult.m_fPosY);
                            //fY = (eRoiToMatch.OrgY + MatchResult.m_fPosY) - base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;



                            // add by smpark
                            pResult.m_fSensorXPos = (float)(eRoiToFind.OrgX + pResult.m_fPosX);
                            pResult.m_fSensorYPos = (float)(eRoiToFind.OrgY + pResult.m_fPosY);
                            pResult.m_fIMGCenterX = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
                            pResult.m_fIMGCenterY = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;
                            pResult.m_fLensScaleX = base.m_LibInfo.m_stLibInfo.dLensScaleX;
                            pResult.m_fLensScaleY = base.m_LibInfo.m_stLibInfo.dLensScaleY;
                         
                            // add by smpark


                            pResult.m_fPosX = fX * base.m_LibInfo.m_stLibInfo.dLensScaleX;
                            pResult.m_fPosY = fY * base.m_LibInfo.m_stLibInfo.dLensScaleY;

                            //This is the code to find the close point of center in the circle.

                           

                            bool bInspection = false;
                            base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.INSPECT_INSIDE, out bInspection);
                            if (bInspection)
                            {
                            }

                            base.m_LibInfo.m_dicLibOption.TryGetValue(EzInaVision.GDV.eLibOption.INSPECT_OUTSIDE, out bInspection);
                            if (bInspection)
                            {
                            }
                           
                            base.m_LibInfo.m_vecFindResults[(int)a_eROI][(int)a_eGoldenImage].AddOrUpdate(nMatchedQuantity, pResult, (k, v) => pResult);
                            base.m_LibInfo.m_vecFindResultsForCalculation[(int)a_eROI][(int)a_eGoldenImage].AddOrUpdate(nMatchedQuantity, pResult, (k, v) => pResult);
                        }

                        //This is the average of the angle of matched chips                      
                       // base.m_LibInfo.m_vecClosePointToCenter[(int)a_eROI][(int)a_eGoldenImage] = stClosePointToCenter;
                        base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.FIND, true, (k, v) => true);
                        base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.FIND_RESULT, true, (k, v) => true);
                        base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH, false, (k, v) => false);
                        base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATCH_RESULT, false, (k, v) => false);
                        if (_OnFindDisPlayEvent != null)
                            _OnFindDisPlayEvent(this, base.m_LibInfo.m_vecFindResultsForCalculation);
                    }
                }
                catch (Euresys.Open_eVision_2_14.EException exc)
                {
                    MessageBox.Show(exc.Message, "Euresys Exception");
                    return -1;
                }

                return nMatchedQuantity;
            }
        }

        public override bool FindResult(int a_nIndex, out EzInaVision.GDV.FindResult a_tFindResult)
        {
            EzInaVision.GDV.FindResult pResult = null;
            if (base.m_LibInfo.m_dicFindResult.TryGetValue(a_nIndex, out pResult))
            {
                a_tFindResult = (EzInaVision.GDV.FindResult)pResult.Clone();
                return true;
            }

            a_tFindResult = pResult;

            return false;
        }
        public override int GetFindResultCount()
        {
            return base.m_LibInfo.m_dicFindResultForCalculation.Count;
        }
        public override void ClearFindResult()
        {
            for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
            {
                for (int iGoldenImg = 0; iGoldenImg < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImg++)
                {
                    
                    base.m_LibInfo.m_vecFindResults[iRoi][iGoldenImg].Clear();
                    base.m_LibInfo.m_vecFindResultsForCalculation[iRoi][iGoldenImg].Clear();
                    //m_vecEMatchersForDisplying[iRoi][iGoldenImg].Clear();
                }
            }
        }
        public override int GetFindResultForCalculation()
        {
            return base.m_LibInfo.m_dicFindResultForCalculation.Count;
        }
        public override void ClearAllFindResults()
        {
            for (int iRoi = 0; iRoi < (int)EzInaVision.GDV.eRoiItems.Max; iRoi++)
            {
                for (int iGoldenImg = 0; iGoldenImg < (int)EzInaVision.GDV.eGoldenImages.Max; iGoldenImg++)
                {                  
                    base.m_LibInfo.m_vecFindResults[iRoi][iGoldenImg].Clear();
                    base.m_LibInfo.m_vecFindResultsForCalculation[iRoi][iGoldenImg].Clear();
                    //m_vecEMatchersForDisplying[iRoi][iGoldenImg].Clear();
                }
            }
        }
        public override bool FindDrawRoiImage(int a_iKey, Graphics a_g)
        {
            return false;
        }
        object m_pFinderLearnedModelDP_Lock = new object();
        public override void DrawFindLearnedModelGeometry(Graphics g, double a_dXScale, double a_dYScale, PointF a_pFanXY)
        {
            try
            {
                lock(m_pFinderLearnedModelDP_Lock)
                {
                    if(m_pFinder !=null&&m_pFinder.LearningDone)
                    {
                        m_pFinder.DrawModel(g, m_ColorGreen, (float)a_dXScale, (float)a_dYScale, a_pFanXY.X, a_pFanXY.Y);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
