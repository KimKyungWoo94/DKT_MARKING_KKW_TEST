using EzInaVision;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzInaVisionLibrary
{

    public sealed partial class VisionLibEuresys : EzInaVision.VisionLibBaseClass
    {
        Euresys.Open_eVision_2_14.EMatrixCodeReader m_MatrixCode1Reader;
        Euresys.Open_eVision_2_14.EMatrixCode[] m_pMatrixCode1Result;
        Euresys.Open_eVision_2_14.EMatrixCode m_pMatrixCode1LearnResult;
        private uint m_iMatrixCode1ReadTimeout = 1000000;
        List<EzInaVision.GDV.MatrixCodeResult> m_pMatrixCode1ResultList;
        public delegate void OnMatrixCode1EventHandler(object obj, List<EzInaVision.GDV.MatrixCodeResult> a_ResultList);
        private OnMatrixCode1EventHandler _OnMatrixCode1DisPlayEvent;
        private object m_pResultArrayLock;
        public event OnMatrixCode1EventHandler OnMatrixCode1DisplayEvent
        {
            add
            {
                lock (this)
                    _OnMatrixCode1DisPlayEvent += value;
            }

            remove
            {
                lock (this)
                    _OnMatrixCode1DisPlayEvent -= value;
            }

        }
        bool m_bMatrixCode1Leared;
        public override void MatrixCode1Init()
        {
            m_MatrixCode1Reader = new Euresys.Open_eVision_2_14.EMatrixCodeReader();
            m_pMatrixCode1ResultList = new List<EzInaVision.GDV.MatrixCodeResult>();
            // m_MatrixCode1Reader.Reset();
            //m_MatrixCode1Reader.ComputeGrading=true;
            m_bMatrixCode1Leared = false;
            m_pResultArrayLock = new object();
        }
        public override void MatrixCode1Terminate()
        {
            if (m_MatrixCode1Reader != null)
            {
                m_MatrixCode1Reader.Dispose();
                m_MatrixCode1Reader = null;
            }
        }
        public override int MatrixCode1MultiRun(int a_eRoiItem, Rectangle[] a_Areaes, float a_IMGSpanRatio, bool a_bSave = false, string a_strFilePath = "", string[] a_strFileNames = null)
        {
            Euresys.Open_eVision_2_14.EImageBW8 pSpanIMG = null;

            try
            {

                lock (lockMatrixCode)
                {
                    if (a_Areaes.Length <= 0)
                        return -3;
                    Stopwatch sw = new Stopwatch();
                    Stopwatch SpanTime = new Stopwatch();
                    sw.Start();
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, false, (k, v) => false);
                    if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == true)
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Filter, a_eRoiItem);
                    else
                        AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Regular, a_eRoiItem);


                    Euresys.Open_eVision_2_14.EROIBW8 eRoiForMatrixCode = null;
                    SetDefaultMatrixCode1Method();

                    lock (m_pResultArrayLock)
                    {
                        if (m_pMatrixCode1Result != null)
                        {
                            for (int i = 0; i < m_pMatrixCode1Result.Length; i++)
                            {
                                if (m_pMatrixCode1Result[i] != null)
                                    m_pMatrixCode1Result[i].Dispose();
                            }
                            m_pMatrixCode1Result = null;
                        }
                        m_pMatrixCode1Result = new Euresys.Open_eVision_2_14.EMatrixCode[a_Areaes.Length];
                    }
                    pSpanIMG = new Euresys.Open_eVision_2_14.EImageBW8();

                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, true, (k, v) => false);
                    if (_OnMatrixCode1DisPlayEvent != null)
                        _OnMatrixCode1DisPlayEvent(this, m_pMatrixCode1ResultList);
                    for (int i = 0; i < a_Areaes.Length; i++)
                    {
                        SetRoiForInspection((int)a_eRoiItem, a_Areaes[i]);
                        m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out eRoiForMatrixCode);
                        if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == false)
                        {
                            if (m_eProcessImgBW8.IsVoid)
                            {
                                return -4;
                            }
                        }
                        else
                        {
                            if (m_eFilteredImgBW8.IsVoid)
                            {
                                return -5;
                            }
                        }
                        m_pMatrixCode1ResultList.Add(new EzInaVision.GDV.MatrixCodeResult());
                        try
                        {
                            if (a_IMGSpanRatio > 1)
                            {
                                SpanTime.Start();
                                pSpanIMG.SetSize((int)(eRoiForMatrixCode.Width * a_IMGSpanRatio), (int)(eRoiForMatrixCode.Height * a_IMGSpanRatio));
                                float OrginX = (float)((eRoiForMatrixCode.Width / 2.0) - 0.5f);
                                float OrginY = (float)((eRoiForMatrixCode.Height / 2.0) - 0.5f);
                                Euresys.Open_eVision_2_14.EasyImage.ScaleRotate(
                                        eRoiForMatrixCode,
                                        (float)((eRoiForMatrixCode.Width / 2.0) - 0.5f),
                                        (float)((eRoiForMatrixCode.Height / 2.0) - 0.5f),
                                        (float)pSpanIMG.Width / 2.0f - 0.5f,
                                        (float)pSpanIMG.Height / 2.0f - 0.5f,
                                        (float)a_IMGSpanRatio,
                                        (float)a_IMGSpanRatio,
                                        0.0f,
                                        pSpanIMG,
                                        0);

                                SpanTime.Stop();
                                //Trace.WriteLine(string.Format("{0}",SpanTime.ElapsedMilliseconds));                                
#if SIM

                                //pSpanIMG.Save(string.Format("D:\\SpanIMG{0}.bmp", DateTime.Now.ToString("yyyyhhmmss.fff")));
#endif

                                m_pMatrixCode1Result[i] = m_MatrixCode1Reader.Read(pSpanIMG);


                            }
                            else
                            {
                                m_pMatrixCode1Result[i] = m_MatrixCode1Reader.Read(eRoiForMatrixCode);
                            }

                            if (m_pMatrixCode1Result[i] != null)
                            {
                                m_pMatrixCode1ResultList[i].m_bFound = m_pMatrixCode1Result[i].Found;
                                m_pMatrixCode1ResultList[i].m_pCornerPoint.Clear();
                                for (int Coner = 0; Coner < 4; Coner++)
                                {
                                    m_pMatrixCode1ResultList[i].m_pCornerPoint.Add(new PointF(eRoiForMatrixCode.OrgX + (m_pMatrixCode1Result[i].GetCorner(Coner).X / a_IMGSpanRatio)
                                                                                                            , eRoiForMatrixCode.OrgY + (m_pMatrixCode1Result[i].GetCorner(Coner).Y / a_IMGSpanRatio)));
                                }
                                m_pMatrixCode1ResultList[i].m_strDecodedString = m_pMatrixCode1Result[i].DecodedString;
                                m_pMatrixCode1ResultList[i].m_bFlipping = m_pMatrixCode1Result[i].Flipping == Euresys.Open_eVision_2_14.EFlipping.Yes;
                                m_pMatrixCode1ResultList[i].m_iLogicalSizeRow = m_pMatrixCode1Result[i].LogicalSizeHeight;
                                m_pMatrixCode1ResultList[i].m_iLogicalSizeCol = m_pMatrixCode1Result[i].LogicalSizeWidth;
                                //GetMatrixSize(m_pMatrixCode1Result[i].LogicalSize,out m_pMatrixCode1ResultList[i].m_iLogicalSizeRow,out m_pMatrixCode1ResultList[i].m_iLogicalSizeCol);
                                m_pMatrixCode1ResultList[i].m_strFamily = m_pMatrixCode1Result[i].Family.ToString();
                                m_pMatrixCode1ResultList[i].m_iErrorsCorrected = m_pMatrixCode1Result[i].NumErrors;
                                m_pMatrixCode1ResultList[i].m_bGS1Encoded = m_pMatrixCode1Result[i].IsGS1();
                                
                            }
                        }
                        catch (Exception exc)
                        {
                            if (a_bSave)
                            {

                                if (System.IO.Directory.Exists(a_strFilePath) == false)
                                {
                                    System.IO.Directory.CreateDirectory(a_strFilePath);
                                }
                                if (a_strFileNames != null && a_strFileNames.Length > i)
                                {
                                    pSpanIMG.Save(string.Format("{0}\\{1}.bmp",
                                    a_strFilePath,
                                    a_strFileNames[i]));
                                }
                                else
                                {
                                    pSpanIMG.Save(string.Format("{0}\\DM_SpanIMG{1}.bmp",
                                    a_strFilePath,
                                    DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")));
                                }
                            }

                            continue;
                        }
                    }

                    sw.Stop();
                    if (pSpanIMG != null)
                    {
                        pSpanIMG.Dispose();
                        pSpanIMG = null;
                    }
                    base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, true, (k, v) => true);
                    Trace.WriteLine(string.Format("MatrixCode Tact Time = {0}ms {1}ea {2}ms", sw.ElapsedMilliseconds, a_Areaes.Length, a_Areaes.Length > 0 ? sw.ElapsedMilliseconds / a_Areaes.Length : 0));
                    if (_OnMatrixCode1DisPlayEvent != null)
                        _OnMatrixCode1DisPlayEvent(this, m_pMatrixCode1ResultList);
                    return 0;
                }
            }
            catch (Exception exc)
            {
                MsgBox.Error(exc.Message);
                if (pSpanIMG != null)
                {
                    pSpanIMG.Dispose();
                    pSpanIMG = null;
                }
                return -1;
            }
        }

        public override int GetMatrixCode1TotalResultCount()
        {
            if (m_pMatrixCode1ResultList != null)
                return m_pMatrixCode1ResultList.Count;

            return 0;
        }
        public override void GetMatrixCode1Result(int Index, out EzInaVision.GDV.MatrixCodeResult a_bufResult)
        {
            a_bufResult = null;
            if (m_pMatrixCode1ResultList != null)
            {
                if (m_pMatrixCode1ResultList.Count > Index && Index > -1)
                {
                    lock (m_pResultArrayLock)
                    {
                        a_bufResult = m_pMatrixCode1ResultList[Index].Clone();
                    }
                }
            }
            else
            {
                a_bufResult = new EzInaVision.GDV.MatrixCodeResult();
            }

        }
        public override void GetMatrixCode1ResultList(out List<EzInaVision.GDV.MatrixCodeResult> a_bufList)
        {
            if (m_pMatrixCode1ResultList != null && m_pMatrixCode1ResultList.Count > 0)
            {
                lock (m_pResultArrayLock)
                {
                    a_bufList = m_pMatrixCode1ResultList.ConvertAll(o => o.Clone());
                }
            }
            else
            {
                a_bufList = null;
            }
        }
        public override void ClearMatrixCode1Results()
        {
            lock (m_pResultArrayLock)
            {
                m_pMatrixCode1ResultList.Clear();
            }
        }
        public override void SetDefaultMatrixCode1Method()
        {
            // Todo List Initial Process Parameter
            m_MatrixCode1Reader.TimeOut = m_iMatrixCode1ReadTimeout;
        }
        public override void SetMatrixCode1ReadTimeout(uint a_ms)
        {
            m_MatrixCode1Reader.TimeOut = a_ms * 1000;
        }
        public override void SetMatrixCode1Method()
        {

        }

    }
}
