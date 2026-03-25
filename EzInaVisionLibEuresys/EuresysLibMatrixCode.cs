using EzInaVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euresys.Open_eVision_2_14;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace EzInaVisionLibrary
{
		public sealed partial class VisionLibEuresys : VisionLibBaseClass
		{
				Euresys.Open_eVision_2_14.EasyMatrixCode2.EMatrixCode[] m_eMatrixCodeReaderResult;
				Euresys.Open_eVision_2_14.EasyMatrixCode2.EMatrixCodeReader m_eMatrixCodeReader;
			
				public override void MatrixCodeInit()
				{
						m_eMatrixCodeReader = new Euresys.Open_eVision_2_14.EasyMatrixCode2.EMatrixCodeReader();
				}
				public override void MatrixCodeTerminate()
				{
						if (m_eMatrixCodeReader != null)
						{
								m_eMatrixCodeReader.Dispose();
								m_eMatrixCodeReader = null;
						}
				}
				object lockMatrixCode = new object();
				public override int MatrixCodeRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "")
				{
						try
						{
								lock (lockMatrixCode)
								{
										Stopwatch sw = new Stopwatch();
										sw.Start();
										int nQuantityOfElementsOfMatrixCode = 0;

										base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, false, (k, v) => false);

									  ClearMatrixCodeResults();

										if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == true)
												AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Filter, a_eRoiItem);
										else
												AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Regular, a_eRoiItem);

										SetRoiForInspection((int)a_eRoiItem, base.m_LibInfo.m_dicRoiSize[(int)a_eRoiItem]);

										EROIBW8 eRoiForMatrixCode = null;
										m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out eRoiForMatrixCode);

										if (a_bSave)
										{
												eRoiForMatrixCode.Save(a_strFileName, EImageFileType.Bmp);
										}


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

										//SetMatrixCodeMethod();
										SetDefaultMatrixCodeMethod();

										m_eMatrixCodeReader.Read(eRoiForMatrixCode);

										if (m_eMatrixCodeReaderResult != null)
										{
												for (int i = 0; i < m_eMatrixCodeReaderResult.Length; i++)
												{
														m_eMatrixCodeReaderResult[i].Dispose();
												}
										}
										m_eMatrixCodeReaderResult = new Euresys.Open_eVision_2_14.EasyMatrixCode2.EMatrixCode[m_eMatrixCodeReader.ReadResults.Length];
										for (int i = 0; i < m_eMatrixCodeReader.ReadResults.Length; i++)
										{
												m_eMatrixCodeReaderResult[i] = m_eMatrixCodeReader.ReadResults[i];
										}										
										nQuantityOfElementsOfMatrixCode = m_eMatrixCodeReader.ReadResults.Length;
										base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.MATRIX_CODE, true, (k, v) => true);
										sw.Stop();
										Trace.WriteLine(string.Format("MatrixCode Tact Time = {0}ms {1}ea {2}ms", sw.ElapsedMilliseconds, nQuantityOfElementsOfMatrixCode, sw.ElapsedMilliseconds / nQuantityOfElementsOfMatrixCode));
										return nQuantityOfElementsOfMatrixCode;
								}
						}
						catch (Exception exc)
						{
								MsgBox.Error(exc.Message);
								return -1;
						}
				}
				public override void ClearMatrixCodeResults()
				{
						for (int i = 0; i <base.m_LibInfo.m_vecMatrixCodeResults.Count; i++)
						{
								base.m_LibInfo.m_vecMatrixCodeResults[i].Clear();
								base.m_LibInfo.m_vecMatrixCodeResultsForCalculation[i].Clear();
						}
				}
				public override void SetDefaultMatrixCodeMethod()
				{
						m_eMatrixCodeReader.ComputeGrading = true;
						m_eMatrixCodeReader.MaxNumCodes = 100;
						m_eMatrixCodeReader.TimeOut = 1 * 60 * 1000 * 1000; //usec
						m_eMatrixCodeReader.ReadMode = Euresys.Open_eVision_2_14.EasyMatrixCode2.EReadMode.Quality;
				}
				public override void SetMatrixCodeMethod()
				{
						m_eMatrixCodeReader.ComputeGrading = base.m_LibInfo.m_MatrixCodeConfig.m_bcomputeGrading;
						m_eMatrixCodeReader.MaxNumCodes = (uint)base.m_LibInfo.m_MatrixCodeConfig.m_nMaxNumCodes;
						m_eMatrixCodeReader.TimeOut = (uint)base.m_LibInfo.m_MatrixCodeConfig.m_nTimeout;
						m_eMatrixCodeReader.ReadMode = (Euresys.Open_eVision_2_14.EasyMatrixCode2.EReadMode)base.m_LibInfo.m_MatrixCodeConfig.m_nReadMode;
						
				}

				#region [ delegate that MatrixCode Result ]
				public delegate void OnMatrixCodeReadEventHandler(object obj, List<List<ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>>> vecMatrixCodeResult);
				private OnMatrixCodeReadEventHandler _OnMatrixCodeReadDisPlayEvent;
				public event OnMatrixCodeReadEventHandler OnMatrixCodeDisplayEvent
				{
						add
						{
								lock (this)
										_OnMatrixCodeReadDisPlayEvent += value;
						}

						remove
						{
								lock (this)
										_OnMatrixCodeReadDisPlayEvent -= value;
						}

				}
				#endregion[ delegate that MatrixCode Result ]

		}
}
