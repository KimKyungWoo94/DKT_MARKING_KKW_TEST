using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Euresys.Open_eVision_2_14;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using EzInaVision;

namespace EzInaVisionLibrary
{
		public sealed partial class VisionLibEuresys : VisionLibBaseClass
		{
				#region [ delegate that blob image]
				public delegate void OnBlobEventHandler(Object obj, ConcurrentDictionary<int, EzInaVision.GDV.BlobResult> a_dicBlob);
				private OnBlobEventHandler _OnBlobDisplayEvent;
				public event OnBlobEventHandler OnBlobDisplayEvent
				{
						add
						{
								lock (this)
										_OnBlobDisplayEvent += value;

						}
						remove
						{
								lock (this)
										_OnBlobDisplayEvent -= value;
						}
				}
				#endregion[ delegate that blob image ]

				EImageEncoder m_eEncoderImg;
				ECodedImage2 m_eCodedImg;
				EObjectSelection m_eSelect;

				public override void BlobInit()
				{
						m_eEncoderImg = new EImageEncoder(); //blob 
						m_eCodedImg = new ECodedImage2();
						m_eSelect = new EObjectSelection();
				}

				public override void BlobTerminate()
				{
						if (m_eEncoderImg != null)
						{
								m_eEncoderImg.Dispose();
								m_eEncoderImg = null;
						}

						if (m_eCodedImg != null)
						{
								m_eCodedImg.Dispose();
								m_eCodedImg = null;
						}

						if (m_eCodedImg != null)
						{
								m_eCodedImg.Dispose();
								m_eCodedImg = null;
						}

						if (m_eSelect != null)
						{
								m_eSelect.Dispose();
								m_eSelect = null;
						}

				}

				object lockBlob = new object();
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_eRoiItem"></param>
				/// <param name="a_bSave"></param>
				/// <param name="a_strFileName"></param>
				/// <returns></returns>
				public override int BlobRun(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "")
				{
						lock (lockBlob)
						{
								int nQuantityOfElementsOfBlob = 0;
								try
								{
										base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB, false, (k, v) => false);
										//m_dictVisionOption.AddOrUpdate(eLibOption.BLOB, false, (k, v) => false);
										// m_dictVisionOption.AddOrUpdate(eLibOption.BLOB_RESULT, false, (k, v) => false);


										base.m_LibInfo.m_iCodedImage2Index = (int)a_eRoiItem;

										for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
										{
												base.m_LibInfo.m_vecBlobResults[i].Clear();
												base.m_LibInfo.m_vecBlobResultsForCalculation[i].Clear();
										}

										if (GetOption(EzInaVision.GDV.eLibOption.ENABLE_FILTERS) == true)
												AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Filter, a_eRoiItem);
										else
												AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Regular, a_eRoiItem);

										SetRoiForInspection((int)a_eRoiItem, base.m_LibInfo.m_dicRoiSize[(int)a_eRoiItem]);

										base.m_LibInfo.m_stClosePointToCenter.Clear();
										EROIBW8 eRoiForBlob = null;

										m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out eRoiForBlob);

										if (a_bSave)
										{
												eRoiForBlob.Save(a_strFileName, EImageFileType.Bmp);
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

										SetBlobMethod(base.m_LibInfo.m_BlobParam);

										m_eEncoderImg.Encode(eRoiForBlob, m_eCodedImg); //eRoiForBlob
										m_eSelect.Clear();
										m_eSelect.AddObjects(m_eCodedImg);
										m_eSelect.AttachedImage = eRoiForBlob; //eRoiForBlob

										//ECodedElement aElement2 = m_eSelect.GetElement(1);

										if (m_eSelect.ElementCount != 0)
										{
												m_eSelect.RemoveUsingFloatFeature(EFeature.BoundingBoxWidth, base.m_LibInfo.m_BlobParam.m_fPadWidthMin / (float)base.m_LibInfo.m_stLibInfo.dLensScaleX, base.m_LibInfo.m_BlobParam.m_fPadWidthMax/ (float)base.m_LibInfo.m_stLibInfo.dLensScaleX, EDoubleThresholdMode.Outside); //EDoubleThresholdMode.Outside
										}
										if (m_eSelect.ElementCount != 0)
										{
												m_eSelect.RemoveUsingFloatFeature(EFeature.BoundingBoxHeight, base.m_LibInfo.m_BlobParam.m_fPadHeightMin / (float)base.m_LibInfo.m_stLibInfo.dLensScaleY, base.m_LibInfo.m_BlobParam.m_fPadHeightMax / (float)base.m_LibInfo.m_stLibInfo.dLensScaleY, EDoubleThresholdMode.Outside);
										}

										base.m_LibInfo.m_nEObjectSelectionTotalCount = m_eSelect.ElementCount;
										m_eSelect.Sort(EFeature.BoundingBoxCenterX);
										//m_eSelect.Sort(EFeature.BoundingBoxCenterY);


										EROIBW8 Roi;
										Random RandomColor = new Random();
										float fRoiCenterX = 0.0f, fRoiCenterY = 0.0f;

										if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out Roi) == true)
										{
												fRoiCenterX = (float)Roi.Width / 2.0f;
												fRoiCenterY = (float)Roi.Height / 2.0f;
										}

										for (uint i = 0; i < m_eSelect.ElementCount; i++)
										{
												ECodedElement aElement = m_eSelect.GetElement(i);
												//aElement.LayerIndex;
												EzInaVision.GDV.BlobResult BlobResult = new EzInaVision.GDV.BlobResult();
												BlobResult.clear();

												BlobResult.fPosX				= aElement.BoundingBox.CenterX ;
												BlobResult.fPosY				= aElement.BoundingBox.CenterY ;
												BlobResult.fWidth				= aElement.BoundingBox.Width	 ;
												BlobResult.fHeight			= aElement.BoundingBox.Height	 ;
												BlobResult.nArea				= aElement.Area;
												BlobResult.nObjectIndex = aElement.ElementIndex;
												BlobResult.nLayerIndex	= aElement.LayerIndex;
												BlobResult.bResultOK		= false;
												BlobResult.BlobColor		= Color.FromArgb(RandomColor.Next(0, 256), RandomColor.Next(0, 256), RandomColor.Next(0, 256));

												double fAreaMin = 0.0, fAreaMax = 0.0;
												//RCP 에서 설정한 원의 넓이를 구한후 Min, Max 값을 설정한다. // (width/2)*(height/2)*pi
												//um to pixe
												float fRadiusX = base.m_LibInfo.m_BlobParam.m_fPadWidthMin  /(float)base.m_LibInfo.m_stLibInfo.dLensScaleX / 2.0f;
												float fRadiusY = base.m_LibInfo.m_BlobParam.m_fPadHeightMin /(float)base.m_LibInfo.m_stLibInfo.dLensScaleX / 2.0f;

												fAreaMin = fRadiusX * fRadiusY * Math.PI * (base.m_LibInfo.m_BlobParam.m_fPadAreaMin /100.0);

												fRadiusX = base.m_LibInfo.m_BlobParam.m_fPadWidthMax  / (float)base.m_LibInfo.m_stLibInfo.dLensScaleX / 2.0f;
												fRadiusY = base.m_LibInfo.m_BlobParam.m_fPadHeightMax / (float)base.m_LibInfo.m_stLibInfo.dLensScaleX / 2.0f;

												fAreaMax = fRadiusX * fRadiusY * Math.PI * (base.m_LibInfo.m_BlobParam.m_fPadAreaMax /100.0);

												double fX, fY;
												fX = fY = 0.0;
												//This is the calculation for the Descartes coordinate.
												fX = base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX - (Roi.OrgX + BlobResult.fPosX);
												fY = (Roi.OrgY + BlobResult.fPosY) - base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;



												BlobResult.fPosX				= (float)fX											* (float)base.m_LibInfo.m_stLibInfo.dLensScaleX;//float)(fX * base.m_LibInfo.m_stLibInfo.dLensScaleX); //pixel to um
												BlobResult.fPosY				= (float)fY											* (float)base.m_LibInfo.m_stLibInfo.dLensScaleY;//float)(fY * base.m_LibInfo.m_stLibInfo.dLensScaleY); //pixel to um
												BlobResult.fWidth				= aElement.BoundingBox.Width		* (float)base.m_LibInfo.m_stLibInfo.dLensScaleX;
												BlobResult.fHeight			= aElement.BoundingBox.Height		* (float)base.m_LibInfo.m_stLibInfo.dLensScaleY; 
												BlobResult.fDispPosX		= aElement.BoundingBox.CenterX	;																	 
												BlobResult.fDispPosY		= aElement.BoundingBox.CenterY	;
												BlobResult.fDispWidth		= aElement.BoundingBox.Width		;
												BlobResult.fDispHeight	= aElement.BoundingBox.Height		;
												BlobResult.fSensorXPos	= (Roi.OrgX + BlobResult.fPosX);
												BlobResult.fSensorYPos	= (Roi.OrgY + BlobResult.fPosY);
												BlobResult.fIMGCenterX	= base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfX;
												BlobResult.fIMGCenterY	= base.m_LibInfo.m_stLibInfo.fCenterCoordinateOfY;
												BlobResult.fLensScaleX	= base.m_LibInfo.m_stLibInfo.dLensScaleX;
												BlobResult.fLensScaleY	= base.m_LibInfo.m_stLibInfo.dLensScaleY;


												//tBlobResult.fPosX = (float)(fX * fLensScaleX);
												//tBlobResult.fPosY = (float)(fY * fLensScaleY);

												//pixel to um
												//BlobResult.fWidth = aElement.BoundingBox.Width * (float)base.m_LibInfo.m_stLibInfo.dLensScaleX;
												//BlobResult.fHeight = aElement.BoundingBox.Height * (float)base.m_LibInfo.m_stLibInfo.dLensScaleY;

												if (BlobResult.nArea > fAreaMin && BlobResult.nArea < fAreaMax)
												{
														BlobResult.bResultOK = true;
														base.m_LibInfo.m_vecBlobResults[(int)a_eRoiItem].AddOrUpdate(Convert.ToInt32(i), BlobResult, (k, v) => BlobResult);
														base.m_LibInfo.m_vecBlobResultsForCalculation[(int)a_eRoiItem].AddOrUpdate(Convert.ToInt32(i), BlobResult, (k, v) => BlobResult);
														nQuantityOfElementsOfBlob++;
												}
												else
												{
														Trace.WriteLine(string.Format("{0:F2},{1:F2}", BlobResult.fDispWidth, BlobResult.fDispHeight));
												}
												aElement.Dispose();
										}

										base.m_LibInfo.m_dicLibOption.AddOrUpdate(EzInaVision.GDV.eLibOption.BLOB, true, (k, v) => true);
										//m_dictVisionOption.AddOrUpdate(eLibOption.BLOB_RESULT, true, (k, v) => true);

										EzInaVision.GDV.BlobResult BlobResultDefault = new EzInaVision.GDV.BlobResult();
										if (base.m_LibInfo.m_vecBlobResultsForCalculation[(int)a_eRoiItem].TryGetValue(0, out BlobResultDefault) == false)
										{
												EzInaVision.GDV.BlobResult BlobResultNG = new EzInaVision.GDV.BlobResult();
												BlobResultNG.clear();
												base.m_LibInfo.m_vecBlobResultsForCalculation[(int)a_eRoiItem].AddOrUpdate(0, BlobResultNG, (k, v) => BlobResultNG);
										}
										if (_OnBlobDisplayEvent != null)
												_OnBlobDisplayEvent(this, base.m_LibInfo.m_vecBlobResultsForCalculation[(int)a_eRoiItem]);
										// m_eCodedImg.Draw(g, m_eSelect);// EDrawableFeature.BoundingBox, m_eSelect);
										Trace.WriteLine(string.Format("Blob Result : {0}", nQuantityOfElementsOfBlob));



										return nQuantityOfElementsOfBlob;
								}
								catch (Exception ex) // Exception ex
								{
										ex.ToString();
										return -1;
								}
						}

				}
				public override bool BlobResult(int a_nIndex, out EzInaVision.GDV.BlobResult a_BlobResult)
				{
						EzInaVision.GDV.BlobResult BlobResult = new EzInaVision.GDV.BlobResult();
						if (base.m_LibInfo.m_dicBlobResult.TryGetValue(a_nIndex, out BlobResult))
						{
								a_BlobResult = BlobResult;
								return true;
						}
						a_BlobResult = BlobResult;
						return false;
				}
				public override List<KeyValuePair<int,EzInaVision.GDV.BlobResult>> GetBlobResultList(EzInaVision.GDV.eRoiItems a_eRoiItem)
				{
						return base.m_LibInfo.m_vecBlobResults[(int)a_eRoiItem].ToList();
				}
				public override void ClearBlobResults()
				{
						for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
						{
								base.m_LibInfo.m_vecBlobResults[i].Clear();
								base.m_LibInfo.m_vecBlobResultsForCalculation[i].Clear();
						}
				}
				public override void SetDefaultBlobMethod()
				{
						base.m_LibInfo.m_BlobParam.m_nPadBlobMethod = (int)EzInaVision.GDV.eEuresysBlobThresHold.Single;
						base.m_LibInfo.m_BlobParam.m_fPadWidthMax = 50;
						base.m_LibInfo.m_BlobParam.m_fPadWidthMin = 10;
						base.m_LibInfo.m_BlobParam.m_fPadHeightMax = 50;
						base.m_LibInfo.m_BlobParam.m_fPadHeightMin = 10;
						try
						{
								if (base.m_LibInfo.m_BlobParam.m_nPadBlobMethod == (int)EzInaVision.GDV.eEuresysBlobThresHold.Single) //single, double
								{
										m_eEncoderImg.SegmentationMethod = ESegmentationMethod.GrayscaleSingleThreshold;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.Mode = EGrayscaleSingleThreshold.Absolute;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.AbsoluteThreshold = base.m_LibInfo.m_BlobParam.m_nPadGrayMinVal;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded = true;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.BlackLayerEncoded = false;
								}
								else
								{
										m_eEncoderImg.SegmentationMethod = ESegmentationMethod.GrayscaleDoubleThreshold; // color
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.WhiteLayerEncoded = true;// chBWLayer[0].Checked;
																																														 //m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.NeutralLayerEncoded = chBWLayer[1].Checked;
																																														 //m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.BlackLayerEncoded = chBWLayer[2].Checked;
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.LowThreshold = base.m_LibInfo.m_BlobParam.m_nPadGrayMinVal;
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.HighThreshold = base.m_LibInfo.m_BlobParam.m_nPadGrayMaxVal;
								}
						}
						catch (System.Exception ex)
						{
								MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name);
						}
				}
				public override void SetBlobMethod(EzInaVision.GDV.BlobConfig a_BlobParam)
				{
						//m_stBlobParam = a_stBlobParam;
						try
						{
								if (base.m_LibInfo.m_BlobParam.m_nPadBlobMethod == (int)EzInaVision.GDV.eEuresysBlobThresHold.Single) //single, double
								{
										m_eEncoderImg.SegmentationMethod = ESegmentationMethod.GrayscaleSingleThreshold;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.Mode = EGrayscaleSingleThreshold.Absolute;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.AbsoluteThreshold = a_BlobParam.m_nPadGrayMinVal;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded = true;
										m_eEncoderImg.GrayscaleSingleThresholdSegmenter.BlackLayerEncoded = false;// 임시 수정
								}
								else
								{
										m_eEncoderImg.SegmentationMethod = ESegmentationMethod.GrayscaleDoubleThreshold; // color
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.WhiteLayerEncoded = true;// chBWLayer[0].Checked;
																																														 //m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.NeutralLayerEncoded = chBWLayer[1].Checked;
																																														 //m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.BlackLayerEncoded = chBWLayer[2].Checked;
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.LowThreshold = a_BlobParam.m_nPadGrayMinVal;
										m_eEncoderImg.GrayscaleDoubleThresholdSegmenter.HighThreshold = a_BlobParam.m_nPadGrayMaxVal;
								}
						}
						catch (System.Exception ex)
						{
								MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name);
						}
				}
				public override uint GetBlobElementCount()
				{
						return base.m_LibInfo.m_nEObjectSelectionTotalCount;
				}

		}//end of class
}
