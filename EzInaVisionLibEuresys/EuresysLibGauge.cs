using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euresys.Open_eVision_2_14;
using EzInaVision;
using System.Drawing;
using System.Diagnostics;

namespace EzInaVisionLibrary
{
		public sealed partial class VisionLibEuresys : VisionLibBaseClass
		{
				EWorldShape m_EWorldShape = null;
				EImageBW8 m_eCalibratedImgBW8 = null;
				EImageBW8 m_eUnwrappedImgBW8 = null;
				EROIBW8 m_eSelectedROIBW8 = null;
				bool m_bCalibrated = false;


				public bool bCalibrated { get { return m_bCalibrated; } }
				public override void GaugeInit()
				{
						m_eCalibratedImgBW8 = new EImageBW8();
						m_eUnwrappedImgBW8 = new EImageBW8();
						m_eSelectedROIBW8 = new EROIBW8();
						m_bCalibrated = false;
				}
				public override void GaugeTerminate()
				{
						if (m_EWorldShape != null)
						{
								m_EWorldShape.Dispose();
								m_EWorldShape = null;
						}

						if (m_eCalibratedImgBW8 != null)
						{
								m_eCalibratedImgBW8.Dispose();
								m_eCalibratedImgBW8 = null;
						}

						if (m_eUnwrappedImgBW8 != null)
						{
								m_eUnwrappedImgBW8.Dispose();
								m_eUnwrappedImgBW8 = null;
						}
				}

				public override int GaugeCalibration(EzInaVision.GDV.eRoiItems a_eROI, float a_fPitchX, float a_fPitchY)
				{
						try
						{
								m_bCalibrated = false;
								if (m_eCalibratedImgBW8.IsVoid)
								{
										return -1;
								}

								if (m_EWorldShape != null)
								{
										m_EWorldShape.Dispose();
										m_EWorldShape = null;
								}

								if (a_eROI == EzInaVision.GDV.eRoiItems.None || a_eROI == EzInaVision.GDV.eRoiItems.Max)
										return -2;

								AttachOneROIToTheSrcImg(EzInaVision.GDV.eImageType.Calibrate, a_eROI);

								EROIBW8 Roi = null;
								if (m_dicRoisForInspectionBW8.TryGetValue((int)a_eROI, out Roi) == false)
										return -3;

								Roi.CopyTo(m_eSelectedROIBW8);

								m_EWorldShape = new EWorldShape();
								m_EWorldShape.SetSensorSize(Roi.Width, Roi.Height);
								m_EWorldShape.Process(Roi, true);
								m_EWorldShape.AutoCalibrateDotGrid(Roi, a_fPitchX, a_fPitchY); //a_nPitchX, a_nPitchY);

								m_bCalibrated = true;
								return 0;
						}
						catch (Euresys.Open_eVision_2_14.EException exc)
						{
								MsgBox.Error(exc.Message);
								return -1;
						}
				}
				public override int GaugeCalibrationModelLoad(string a_strFilePath,float a_fPitchX,float a_fPitchY)
				{
						try
						{
								m_bCalibrated = false;
								if (m_EWorldShape != null)
								{
										m_EWorldShape.Dispose();
										m_EWorldShape = null;
								}
								using (EImageBW8 CalbDefaultIMG = new EImageBW8())
								{
										CalbDefaultIMG.Load(a_strFilePath);
										m_EWorldShape = new EWorldShape();
										//m_EWorldShape.Load(a_strFilePath, true);
										m_EWorldShape.SetSensorSize(CalbDefaultIMG.Width, CalbDefaultIMG.Height);
										m_EWorldShape.SetCenterXY(CalbDefaultIMG.Width / 2.0f, CalbDefaultIMG.Height / 2.0f);
										m_EWorldShape.Process(CalbDefaultIMG, true);
										m_EWorldShape.AutoCalibrateDotGrid(CalbDefaultIMG, a_fPitchX, a_fPitchY); //a_nPitchX, a_nPitchY);
										
										m_bCalibrated = true;
										return 0;
								}
										
						}
						catch (Euresys.Open_eVision_2_14.EException exc)
						{
								MsgBox.Error(exc.Message);
								return -1;
						}
				}
				public override int GaugeUnwarping(Bitmap a_OrginIMG)
				{
						try
						{
							  if (m_EWorldShape == null)
										return -1;
								if (!m_bCalibrated)
										return -1;

								using (EImageBW8 pOrginIMG = new EImageBW8(GetEImageBW8(a_OrginIMG)))
								{
										//m_EWorldShape.SetCenterXY(pOrginIMG.Width / 2.0f, pOrginIMG.Height / 2.0f);
										m_eUnwrappedImgBW8.SetSize(pOrginIMG);
										m_EWorldShape.Unwarp(pOrginIMG, m_eUnwrappedImgBW8, false);			
																	
								}
								return 0;
						}
						catch (Euresys.Open_eVision_2_14.EException exc)
						{
								MsgBox.Error(exc.Message);
								return -1;
						}
				}
				public override int GetSensorToWorldCordinate(PointF a_Point,PointF a_Center,ref PointF a_worldPoint)
				{
						try
						{			
								if(a_worldPoint==null)
										a_worldPoint=new PointF();
							  		
								if (m_EWorldShape == null)
								return -1;
								if (!m_bCalibrated)
								return -1;
								
								float CenterXGap=m_EWorldShape.CenterX-a_Center.X;
								float CenterYGap=m_EWorldShape.CenterY-a_Center.Y;

								EPoint Ret=m_EWorldShape.SensorToWorld(new EPoint(CenterXGap+a_Point.X,CenterYGap+a_Point.Y));								
								a_worldPoint.X=Ret.X;
								a_worldPoint.Y=Ret.Y;
								return 0;
						}
						catch (Euresys.Open_eVision_2_14.EException exc)
						{
								
								MsgBox.Error(exc.Message);
								return -1;
						}
				}
				public override int GetSensorToLocalCordinate(PointF a_Point, PointF a_Center, ref PointF a_LocalPoint)
				{
						try
						{
								if (a_LocalPoint == null)
										a_LocalPoint = new PointF();

								if (m_EWorldShape == null)
										return -1;
								if (!m_bCalibrated)
										return -1;

								float CenterXGap = 0;//m_EWorldShape.CenterX - a_Center.X;
								float CenterYGap = 0;//m_EWorldShape.CenterY - a_Center.Y;

								EPoint Ret = m_EWorldShape.SensorToLocal(new EPoint(CenterXGap + a_Point.X, CenterYGap + a_Point.Y));
								a_LocalPoint.X = Ret.X;
								a_LocalPoint.Y = Ret.Y;
								return 0;
						}
						catch (Euresys.Open_eVision_2_14.EException exc)
						{

								MsgBox.Error(exc.Message);
								return -1;
						}
				}


		}
}
