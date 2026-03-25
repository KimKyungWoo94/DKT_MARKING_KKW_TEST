using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
		public class CCalcOfChipPitch
		{
				private List<double> m_vecValue;

				private double m_fMin;
				private double m_fMax;
				private List<double> m_vecDiffValue;
				private List<double> m_vecPeakValue;

				public CCalcOfChipPitch()
				{
						Initialize();
				}

				public void Initialize()
				{
						m_vecValue = new List<double>();

						m_fMin = m_fMax = 0;
						m_vecDiffValue = new List<double>();

						m_vecPeakValue = new List<double>();
				}
				public void InputValue(double a_fValue)
				{
						m_vecValue.Add(a_fValue);
						m_vecValue.Sort();
				}

				public void CalcDistanceBetweenTwoPoints()
				{
						double fDistanceValue = 0;
						for (int i = 0; i < m_vecValue.Count - 1; i++)
						{
								fDistanceValue = m_vecValue[i + 1] - m_vecValue[i];
								if (m_vecDiffValue.Count == 0)
								{
										m_fMin = m_fMax = fDistanceValue;
								}

								if (m_fMin > fDistanceValue) m_fMin = fDistanceValue;
								if (m_fMax < fDistanceValue) m_fMax = fDistanceValue;

								m_vecDiffValue.Add(fDistanceValue);
						}
				}

				public void CalcForPeakingPoints()
				{
						bool bNextLine = true;
						for (int i = 0; i < m_vecDiffValue.Count; i++)
						{
								if ((m_fMax - m_fMin) < 30) //pixel 
								{
										if (m_vecDiffValue[i] > 0.0)
										{
												m_vecPeakValue.Add(m_vecDiffValue[i]);
										}

								}
								else if (m_vecDiffValue[i] > (m_fMax - m_fMin) / 2.0) //( if more two lines )
								{
										if (bNextLine == true)
										{
												m_vecPeakValue.Add(m_vecDiffValue[i]);
												bNextLine = false;
										}
								}
								else
								{
										bNextLine = true;
								}
						}
				}

				public double CalcAverageForPeakingPoints()
				{
						double fAvg = 0;
						double fSum = 0;
						for (int i = 0; i < m_vecPeakValue.Count; i++)
						{
								fSum += m_vecPeakValue[i];
						}

						if (m_vecPeakValue.Count > 0)
								fAvg = fSum / m_vecPeakValue.Count;

						return fAvg;
				}

		}
		public class CVisionOfMapping : ICloneable
		{
				public int			iRow		;
				public int			iCol		;
				public double		dEncX		; //(m_x)
				public double		dEncY		;//(m_y) 
				public double		dAngleT	;//(v_t)
				public float		fPxlX		; //(pixel_x)
				public float		fPxlY		; //(pixel_y) 

				public double dMappingOffsetX;
				public double dMappingOffsetY;
				public double dMappingOffsetZ;

				public CVisionOfMapping()
				{
						Init();
				}

				public void Init()
				{
						iRow = -9999999;
						iCol = -9999999;
						dEncX = 0.0; //(m_x)
						dEncY = 0.0;//(m_y) 
						dAngleT = 0.0;//(v_t)
						dMappingOffsetX = 0.0;
						dMappingOffsetY = 0.0;
						dMappingOffsetZ = 0.0;
						fPxlX = 0.0f;
						fPxlY = 0.0f;
				}

				public void DataClear()
				{
						iRow = -9999;
						iCol = -9999;
						dEncX = -9999999.0; //(m_x)
						dEncY = -9999999.0;//(m_y) 
						dAngleT = 0.0;//(v_t)
						dMappingOffsetX = 0.0;
						dMappingOffsetY = 0.0;
						dMappingOffsetZ = 0.0;

						fPxlX = 0.0f;
						fPxlY	= 0.0f;
				}

				public object Clone()
				{
						CVisionOfMapping Mapping = new CVisionOfMapping();
						Mapping.iRow		= this.iRow		;
						Mapping.iCol		= this.iCol		;
						Mapping.dEncX		= this.dEncX		; //(m_x)
						Mapping.dEncY		= this.dEncY		;//(m_y) 
						Mapping.dAngleT	= this.dAngleT	;//(v_t)
						Mapping.fPxlX		= this.fPxlX		; //(pixel_x)
						Mapping.fPxlY		= this.fPxlY		; //(pixel_y) 

						Mapping.dMappingOffsetX = this.dMappingOffsetX;
						Mapping.dMappingOffsetY = this.dMappingOffsetY;
						Mapping.dMappingOffsetZ = this.dMappingOffsetZ;

						return Mapping;
				}

		}
		public class CorrMappingItem
		{
				List<List<CVisionOfMapping>> m_vecMapping;
				public Point m_ptMaxIndexOfMapping;
				public Point m_ptMinIndexOfMapping;

				private Point m_ptProcessPitch = new Point();
				private Point m_ptMaxIndexOfCal = new Point();
				private Point m_ptMinIndexOfCal = new Point();
				private PointF m_ptStartPos	= new PointF();
				List<List<CVisionOfMapping>> m_vecVisionCal;






				#region MappingStage XY
				public bool OpenMappingDataOfXY(string a_strFilePath)
				{
						if (m_vecMapping == null)
								m_vecMapping = new List<List<CVisionOfMapping>>();
						else
								m_vecMapping.Clear();

						string strPath = a_strFilePath;

						StreamReader Stream = new StreamReader(strPath, Encoding.UTF8);
						if (Stream.EndOfStream)
								return false;

						m_ptMinIndexOfMapping.X = 99999;
						m_ptMinIndexOfMapping.X = 99999;
						m_ptMaxIndexOfMapping.Y = -99999;
						m_ptMaxIndexOfMapping.Y = -99999;

						try
						{


								int nRows = 0, nCols = 0;
								//Date
								string line = Stream.ReadLine();
								//Rows
								line = Stream.ReadLine();
								string[] data = line.Split(',');
								nRows = Convert.ToInt32(data[1]);
								//Columns
								line = Stream.ReadLine();
								data = line.Split(',');
								nCols = Convert.ToInt32(data[1]);
								//Pitch X
								line = Stream.ReadLine();
								//Pitch Y
								line = Stream.ReadLine();
								//Item
								line = Stream.ReadLine();
								data = line.Split(',');

								for (int iRow = 0; iRow < nRows; iRow++)
								{
										m_vecMapping.Add(new List<CVisionOfMapping>());
										for (int iCol = 0; iCol < nCols; iCol++)
										{
												m_vecMapping[iRow].Add(new CVisionOfMapping());
										}
								}

								do
								{
										line = Stream.ReadLine();
										data = line.Split(',');
										CVisionOfMapping ReadInfo = new CVisionOfMapping();
										ReadInfo.Init();
										if (data.Length == 8)
										{
												ReadInfo.iCol = Convert.ToInt32(data[0]);
												ReadInfo.iRow = Convert.ToInt32(data[1]);
												ReadInfo.dEncX = Convert.ToDouble(data[2]);
												ReadInfo.dEncY = Convert.ToDouble(data[3]);
												//ActX [4]
												//ACtY [5]
												//ReadInfo.dAngleT = Convert.ToDouble(data[]);
												ReadInfo.dMappingOffsetX = Convert.ToDouble(data[6]);
												ReadInfo.dMappingOffsetY = Convert.ToDouble(data[7]);
												m_vecMapping[ReadInfo.iRow][ReadInfo.iCol] = ReadInfo;

												if (ReadInfo.iCol < m_ptMinIndexOfMapping.X) m_ptMinIndexOfMapping.X = ReadInfo.iCol;
												if (ReadInfo.iRow < m_ptMinIndexOfMapping.Y) m_ptMinIndexOfMapping.Y = ReadInfo.iRow;

												if (ReadInfo.iCol > m_ptMaxIndexOfMapping.X) m_ptMaxIndexOfMapping.X = ReadInfo.iCol;
												if (ReadInfo.iRow > m_ptMaxIndexOfMapping.Y) m_ptMaxIndexOfMapping.Y = ReadInfo.iRow;

										}
										else
										{
												continue;
										}



								} while (!Stream.EndOfStream);

								Stream.Close();
								Stream = null;
						}
						catch (Exception ex)
						{
								return false;
						}
						finally
						{
								if (Stream != null)
								{
										Stream.Close();
										Stream = null;
								}
						}

						return true;
				}
				public bool SaveMappingDataOfXY(int a_nPitchX = 150, int a_nPitchY = 150, string a_strPath = "")
        {
            try
            {
                string strFilePath = string.Format(@"{0}\{1}.csv", a_strPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                using (FileStream f = new FileStream(strFilePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(f))
                    {
                        //
                        //Date,20191101
                        //Rows,10
                        //Columns,15
                        //Pitch X,30000
                        //Pitch Y,30000
                        //Row,Col,CMDX,CMDY,ACTX,ACTY,offsetX,offsetY
                        sw.WriteLine(string.Format("{0},{1}", "Date"		, DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                        sw.WriteLine(string.Format("{0},{1}", "Rows"		, m_ptMaxIndexOfCal.Y));
                        sw.WriteLine(string.Format("{0},{1}", "Columns"	, m_ptMaxIndexOfCal.X));
                        sw.WriteLine(string.Format("{0},{1}", "Pitch X"	, a_nPitchX));
                        sw.WriteLine(string.Format("{0},{1}", "Pitch Y"	, a_nPitchY));
                        sw.WriteLine("Col,Row,PXL_X,PXL_Y,OffsetX,OffsetY");

                        int nRows = m_ptMaxIndexOfCal.Y;
                        int nCols = m_ptMaxIndexOfCal.X;

                        string  strTmp = "";
                        for(int iRow = 0; iRow < nRows; iRow++)
                        {
                            for(int iCol = 0; iCol < nCols; iCol++)
                            {
                                strTmp = "";
                                strTmp = string.Format("{0},{1},{2:F4},{3:F4},{4:F4},{5:F4}"
                                    , m_vecVisionCal[iRow][iCol].iCol
                                    , m_vecVisionCal[iRow][iCol].iRow
                                    , m_vecVisionCal[iRow][iCol].fPxlX
                                    , m_vecVisionCal[iRow][iCol].fPxlY
                                    , m_vecVisionCal[iRow][iCol].dMappingOffsetX
                                    , m_vecVisionCal[iRow][iCol].dMappingOffsetY);

                                sw.WriteLine(strTmp);
                            }
                        }
                        sw.Close();
                    }
                    f.Close();
                }

								return true;
            }
            catch (Exception ex)
            {
								return false;
            }
				}
				public double GetStageOffsetOfXY(bool a_bX, double a_dCmdX, double a_dCmdY)
				{
						try
						{
								if (m_vecMapping == null)
										return 0.0;

								if (m_vecMapping.Count <= 0)
										return 0.0;

								Point ptFindIndexLT = new Point(-9999999, -9999999);
								Point ptFindIndexLB = new Point(-9999999, -9999999);
								Point ptFindIndexRT = new Point(-9999999, -9999999);
								Point ptFindIndexRB = new Point(-9999999, -9999999);

								for (int iRow = m_ptMinIndexOfMapping.Y; iRow < m_ptMaxIndexOfMapping.Y; iRow++)
								{
										for (int iCol = m_ptMinIndexOfMapping.X; iCol < m_ptMaxIndexOfMapping.X; iCol++)
										{
												//ptSearchForIndex.X + 1, ptSearchForIndex.Y

												if (m_vecMapping[iRow][iCol].dEncX > a_dCmdX && a_dCmdX >= m_vecMapping[iRow][iCol + 1].dEncX)
												{
														ptFindIndexLT.X = iCol;
														ptFindIndexLT.Y = iRow;
														ptFindIndexRT.X = iCol + 1;
														ptFindIndexRT.Y = iRow;
														goto CompletedX;
												}
										}
								}

								return 0.0;

								CompletedX:

								for (int iRow = m_ptMinIndexOfMapping.Y; iRow < m_ptMaxIndexOfMapping.Y; iRow++)
								{
										//ptSearchForIndex.X + 1, ptSearchForIndex.Y
										if (m_vecMapping[iRow][ptFindIndexLT.X].dEncY <= a_dCmdY && a_dCmdY < m_vecMapping[iRow + 1][ptFindIndexLT.X].dEncY
												&& m_vecMapping[iRow][ptFindIndexRT.X].dEncY <= a_dCmdY && a_dCmdY < m_vecMapping[iRow + 1][ptFindIndexLT.X].dEncY)
										{
												ptFindIndexLB.X = ptFindIndexLT.X;
												ptFindIndexLB.Y = iRow + 1;
												ptFindIndexLT.Y = iRow;

												ptFindIndexRB.X = ptFindIndexRT.X;
												ptFindIndexRB.Y = iRow + 1;
												ptFindIndexRT.Y = iRow;
												goto CompteledY;
										}
								}

								return 0.0;

								CompteledY:

								//Ref) 보정테이블작성(수정).pdf 
								double A_CorrectionValue = 0.0;
								double B_CorrectionValue = 0.0;
								double C_CorrectionValue = 0.0;

								double f1stEnc = 0.0;
								double f2ndEnc = 0.0;
								double f1stOffset = 0.0;
								double f2ndOffset = 0.0;


								//calculate position of A
								f1stEnc = m_vecMapping[ptFindIndexLT.Y][ptFindIndexLT.X].dEncY;
								if (a_bX)
										f1stOffset = m_vecMapping[ptFindIndexLT.Y][ptFindIndexLT.X].dMappingOffsetX; // offset X
								else
										f1stOffset = m_vecMapping[ptFindIndexLT.Y][ptFindIndexLT.X].dMappingOffsetY; // offset Y



								f2ndEnc = m_vecMapping[ptFindIndexLB.Y][ptFindIndexLB.X].dEncY;
								if (a_bX)
										f2ndOffset = m_vecMapping[ptFindIndexLB.Y][ptFindIndexLB.X].dMappingOffsetX; // offset X
								else
										f2ndOffset = m_vecMapping[ptFindIndexLB.Y][ptFindIndexLT.X].dMappingOffsetY; // offset Y

								A_CorrectionValue = (f2ndOffset - f1stOffset) / (f2ndEnc - f1stEnc) * (a_dCmdY - f1stEnc);
								A_CorrectionValue += f1stOffset;

								//calculate position of B
								f1stEnc = m_vecMapping[ptFindIndexRT.Y][ptFindIndexRT.X].dEncY;
								if (a_bX)
										f1stOffset = m_vecMapping[ptFindIndexRT.Y][ptFindIndexRT.X].dMappingOffsetX; // offset X
								else
										f1stOffset = m_vecMapping[ptFindIndexRT.Y][ptFindIndexRT.X].dMappingOffsetY; // offset Y



								f2ndEnc = m_vecMapping[ptFindIndexRB.Y][ptFindIndexRB.X].dEncY;
								if (a_bX)
										f2ndOffset = m_vecMapping[ptFindIndexRB.Y][ptFindIndexRB.X].dMappingOffsetX; // offset X
								else
										f2ndOffset = m_vecMapping[ptFindIndexRB.Y][ptFindIndexRB.X].dMappingOffsetY; // offset Y

								B_CorrectionValue = ((f2ndOffset - f1stOffset) / (f2ndEnc - f1stEnc)) * (a_dCmdY - f1stEnc);
								B_CorrectionValue += f1stOffset;


								//calculate position of C    ( + ) <--- x ---> ( - )
								f1stEnc = m_vecMapping[ptFindIndexLB.Y][ptFindIndexLB.X].dEncX;   // X
								f2ndEnc = m_vecMapping[ptFindIndexRB.Y][ptFindIndexRB.X].dEncX;   // X'

								C_CorrectionValue = ((A_CorrectionValue - B_CorrectionValue) / (f1stEnc - f2ndEnc)) * (a_dCmdX - f1stEnc); //a_fTargetX
								C_CorrectionValue += A_CorrectionValue;

								return C_CorrectionValue;

						}
						catch (Exception ex)
						{
								return 0.0;
						}
				}
				
				public Point ChipPitch(ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> a_dicMatchResult)
				{
						m_ptProcessPitch.X = 0;
						m_ptProcessPitch.Y = 0;

						if (a_dicMatchResult == null) return m_ptProcessPitch;
						if (a_dicMatchResult.Count < 1) return m_ptProcessPitch;

						CCalcOfChipPitch[] CalcOfChipPitch = new CCalcOfChipPitch[2];
						CalcOfChipPitch[0] = new CCalcOfChipPitch();
						CalcOfChipPitch[1] = new CCalcOfChipPitch();


						EzInaVision.GDV.MatchResult MatchResult = new EzInaVision.GDV.MatchResult();
						for (int i = 0; i < a_dicMatchResult.Count; i++)
						{
								a_dicMatchResult.TryGetValue(i+1, out MatchResult);
								CalcOfChipPitch[0].InputValue(MatchResult.m_fDispPosX);
								CalcOfChipPitch[1].InputValue(MatchResult.m_fDispPosY);
						}
						double[] fValue = new double[2];

						for (int i = 0; i < 2; i++)
						{
								CalcOfChipPitch[i].CalcDistanceBetweenTwoPoints();
								CalcOfChipPitch[i].CalcForPeakingPoints();
								fValue[i] = CalcOfChipPitch[i].CalcAverageForPeakingPoints();
						}

						m_ptProcessPitch.X = (int)fValue[0];
						m_ptProcessPitch.Y = (int)fValue[1];

						if (m_ptProcessPitch.X == 0 && m_ptProcessPitch.Y > 0)
								m_ptProcessPitch.X = m_ptProcessPitch.Y;

						if (m_ptProcessPitch.X > 0 && m_ptProcessPitch.Y == 0)
								m_ptProcessPitch.Y = m_ptProcessPitch.X;

						return m_ptProcessPitch;

				}
				public bool MatchCompile(ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> a_dicMatchResult, double a_dPitchX, double a_dPitchY)
				{
						if (m_ptProcessPitch.X == 0 || m_ptProcessPitch.Y == 0) return false;

						try
						{		
								EzInaVision.GDV.MatchResult MatchResult = new EzInaVision.GDV.MatchResult();
								m_ptMinIndexOfCal.X = 99999;
								m_ptMinIndexOfCal.Y = 99999;
								m_ptMaxIndexOfCal.X = -99999;
								m_ptMaxIndexOfCal.Y = -99999;

								m_ptStartPos.X = 0.0f;
								m_ptStartPos.Y = 0.0f;

								for (int i = 0; i < a_dicMatchResult.Count; i++)
								{
										double fDX, fDY;
										double fIX, fIY;
										fDX = fDY = fIX = fIY = 0.0;
										Point ptPos = new Point();



										if (a_dicMatchResult.TryGetValue(i+1, out MatchResult))
										{
												fDX = MatchResult.m_fDispPosX;
												fDY = MatchResult.m_fDispPosY;

												fIX = fDX / (double)(m_ptProcessPitch.X);
												fIY = fDY / (double)(m_ptProcessPitch.Y);


												if (fIX < 0) ptPos.X = (int)(fIX - 0.5);
												else ptPos.X = (int)(fIX + 0.5);
												
												if (fIY < 0) ptPos.Y = (int)(fIY - 0.5);
												else ptPos.Y = (int)(fIY + 0.5);

												if (m_ptMinIndexOfCal.X > ptPos.X) m_ptMinIndexOfCal.X = ptPos.X;
												if (m_ptMinIndexOfCal.Y > ptPos.Y) m_ptMinIndexOfCal.Y = ptPos.Y;
												if (m_ptMaxIndexOfCal.X < ptPos.X) m_ptMaxIndexOfCal.X = ptPos.X;
												if (m_ptMaxIndexOfCal.Y < ptPos.Y) m_ptMaxIndexOfCal.Y = ptPos.Y;

												MatchResult.m_iX = ptPos.X;
												MatchResult.m_iY = ptPos.Y;

												a_dicMatchResult.AddOrUpdate(i, MatchResult, (k ,v) => MatchResult);

										}
								}

								for (int i = 0; i < a_dicMatchResult.Count; i++)
								{
										if (a_dicMatchResult.TryGetValue(i + 1, out MatchResult))
										{
												if(MatchResult.m_iX == m_ptMinIndexOfCal.X && MatchResult.m_iY == m_ptMinIndexOfCal.Y)
												{
														m_ptStartPos.X = MatchResult.m_fDispPosX;
														m_ptStartPos.Y = MatchResult.m_fDispPosY;
														break;
												}

										}
								}

								#region [Make Array for vision calibration]
								if (m_vecVisionCal != null)
								{
										m_vecVisionCal.Clear();
										m_vecVisionCal = null;
								}

								m_vecVisionCal = new List<List<CVisionOfMapping>>();
								for(int r = 0; r < m_ptMaxIndexOfCal.Y; r++ )
								{
										m_vecVisionCal.Add(new List<CVisionOfMapping>());
										for(int c = 0; c < m_ptMaxIndexOfCal.X; c++)
										{
												m_vecVisionCal[r].Add(new CVisionOfMapping());
										}
								}


								for(int i = 0; i < a_dicMatchResult.Count; i++)
								{
										if(a_dicMatchResult.TryGetValue(i, out MatchResult))
										{
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].iCol									= MatchResult.m_iX							- m_ptMinIndexOfCal.X;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].iRow									= MatchResult.m_iY							- m_ptMinIndexOfCal.Y;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dEncX								= MatchResult.m_fPosX						;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dEncY								= MatchResult.m_fPosY						;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dAngleT							= 0.0														;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].fPxlX								= MatchResult.m_fDispPosY				;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].fPxlY								= MatchResult.m_fDispPosY				;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dMappingOffsetX			=	(MatchResult.m_iX * a_dPitchX) - MatchResult.m_fDispPosX - m_ptStartPos.X;						
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dMappingOffsetY			= (MatchResult.m_iY * a_dPitchY) - MatchResult.m_fDispPosY - m_ptStartPos.Y;
												m_vecVisionCal[MatchResult.m_iY-m_ptMinIndexOfCal.Y][MatchResult.m_iX-m_ptMinIndexOfCal.X].dMappingOffsetZ			= 0.0														;
										}
								}

								#endregion

								return true;
						}
						catch (Exception ex)
						{
								MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
						}
				}
				#endregion



		}
}
