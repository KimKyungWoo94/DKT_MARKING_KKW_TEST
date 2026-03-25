using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;


namespace EzIna.Cam
{
    public class Transfomations
    {
        public enum emFunctionError_t
        {
            Success = 0,
            PathError,
            ParameterError,
            Fail,
        }

        public struct PointD
        {
            public double dX;
            public double dY;

            public void Clear()
            {
                dX = dY = 0.0;
            }
        }

        public struct AlignPos
        {
            public PointD[] pos;
            /// <summary>
            /// 가로 축 기준 라디안 기울기
            /// </summary>
            public double Angle;

            public void Init()
            {
                pos = new PointD[4];
                Angle = 0.0f;
            }
        }

        #region 변수선언
        private int m_nArrayX;
        private int m_nArrayY;
        private double m_fResolution;
        private bool m_bLoad { get; set; }
        private bool m_bOrgPosSet { get; set; }
        private bool m_bNewPosSet { get; set; }

        private PointD m_rpStart = new PointD();
        private AlignPos AlignOrg = new AlignPos();
        private AlignPos AlignNew = new AlignPos();
        private List<List<PointD>> m_rpCalPos = new List<List<PointD>>();

        double[,] m_HM = new double[3, 3];

				double m_fCalFileMinDX;
				double m_fCalFileMaxDX;
				double m_fCalFileMinDY;
				double m_fCalFileMaxDY;
				#endregion

				public Transfomations()
        {
            m_rpStart = new PointD();            
            m_rpCalPos = new List<List<PointD>>();
            AlignOrg.Init();
            AlignNew.Init();

            this.Clear();
        }

        ~Transfomations()
        {
            this.Clear();
        }

        public void Dispose()
        {
            this.Clear();
        }

        #region 외부 함수
        /// <summary>
        /// Initalize
        /// </summary>
        public void Clear()
        {
            m_bLoad = false;
            m_nArrayX = 0;
            m_nArrayY = 0;
            m_fResolution = 0;

            m_bOrgPosSet = false;
            m_bNewPosSet = false;

            m_rpStart.Clear();
            m_rpCalPos.Clear();

            for (int i = 0; i < 3; i++) 
                for (int j = 0; j < 3; j++)
                    m_HM[i, j] = 0.0;

        }

        /// <summary>
        /// Load Calibration File
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool LoadCalibrationFile(string strPath)
        {
            if (this.LoadCalFile(strPath) == Convert.ToInt32(emFunctionError_t.Success))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Confirm cal-file loading
        /// </summary>
        /// <returns></returns>
        public bool IsLoad()
        {
            return m_bLoad;
        }

        /// <summary>
        /// Set Original Align Position
        /// </summary>
        public bool SetAlignOrgPos(PointF[] point)
        {
            try
            {
                this.SortAlign(ref point);

                for (int i = 0; i < point.Length; i++)
                {
                    this.AlignOrg.pos[i].dX = (double)point[i].X;
                    this.AlignOrg.pos[i].dY = (double)point[i].Y;
                }

                this.AlignOrg.Angle = Math.Atan2((this.AlignOrg.pos[1].dY - this.AlignOrg.pos[0].dY), (this.AlignOrg.pos[1].dX - this.AlignOrg.pos[0].dX));

                this.m_bOrgPosSet = true;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Set New Align Position
        /// </summary>
        public bool SetAlignNewPos(PointF[] point)
        {
            try
            {
                this.SortAlign(ref point);

                for (int i = 0; i < point.Length; i++)
                {
                    this.AlignNew.pos[i].dX = point[i].X;
                    this.AlignNew.pos[i].dY = point[i].Y;
                }        

                this.AlignNew.Angle = Math.Atan2((this.AlignNew.pos[1].dY - this.AlignNew.pos[0].dY), (this.AlignNew.pos[1].dX - this.AlignNew.pos[0].dX));

                this.m_bNewPosSet = true;

                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 4p Align - 기울기, 쉬프트, 수축 팽창 적용
        /// </summary>
        /// <param name="dOrgX"></param>
        /// <param name="dOrgY"></param>
        /// <param name="dNewX"></param>
        /// <param name="dNewY"></param>
        /// <returns></returns>
        public int Get4PointAlignPos(double dOrgX, double dOrgY, out double dNewX, out double dNewY)
        {
            try
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                if (!this.m_bOrgPosSet || !this.m_bNewPosSet)
                    return (int)emFunctionError_t.ParameterError;

                // cal h matrix
                Homography_from_4corresp(this.AlignOrg.pos[0], this.AlignOrg.pos[1], this.AlignOrg.pos[2], this.AlignOrg.pos[3], 
                    this.AlignNew.pos[0], this.AlignNew.pos[1], this.AlignNew.pos[2], this.AlignNew.pos[3]);

                // homography transform
                dNewX = m_HM[0, 0] * dOrgX + m_HM[0, 1] * dOrgY + m_HM[0, 2];
                dNewY = m_HM[1, 0] * dOrgX + m_HM[1, 1] * dOrgY + m_HM[1, 2];

                return (int)emFunctionError_t.Success;

            }
            catch (Exception ex)
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                //m_cLog.WriteDebug("CorrectionPos() : " + ex.Message);
                return (int)emFunctionError_t.Fail;
            }
        }

        /// <summary>
        /// 2P Align - 기울기, 쉬프트 보정
        /// </summary>
        /// <param name="dOrgX"></param>
        /// <param name="dOrgY"></param>
        /// <param name="dNewX"></param>
        /// <param name="dNewY"></param>
        /// <returns></returns>
        public int Get2PointAlignPos(double dOrgX, double dOrgY, out double dNewX, out double dNewY)
        {
            try
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                if (!this.m_bOrgPosSet || !this.m_bNewPosSet)
                    return (int)emFunctionError_t.ParameterError;

                // calc shift
                double shiftX = this.AlignNew.pos[0].dX - this.AlignOrg.pos[0].dX;
                double shiftY = this.AlignNew.pos[0].dY - this.AlignOrg.pos[0].dY;

                // calc rotate angle
                double radian = this.AlignNew.Angle - this.AlignOrg.Angle;

                // Rigid Transformation
                dNewX = (Math.Cos(radian) * dOrgX) - (Math.Sin(radian) * dOrgY) + shiftX;
                dNewY = (Math.Sin(radian) * dOrgX) + (Math.Cos(radian) * dOrgY) + shiftY;

                return (int)emFunctionError_t.Success;

            }
            catch (Exception ex)
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                return (int)emFunctionError_t.Fail;
            }
        }

        /// <summary>
        /// 3P Align - 기울기, 쉬프트 보정
        /// </summary>
        /// <param name="dOrgX"></param>
        /// <param name="dOrgY"></param>
        /// <param name="dNewX"></param>
        /// <param name="dNewY"></param>
        /// <returns></returns>
        public int Get3PointAlignPos(double dOrgX, double dOrgY, out double dNewX, out double dNewY)
        {
            try
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                if (!this.m_bOrgPosSet || !this.m_bNewPosSet)
                    return (int)emFunctionError_t.ParameterError;

                PointF CenterOrg = new PointF();
                PointF CenterNew = new PointF();

                if (this.Get3PointAlignCenterPos(false, ref CenterOrg) != (int)emFunctionError_t.Success) return (int)emFunctionError_t.Fail;
                if (this.Get3PointAlignCenterPos(false, ref CenterNew) != (int)emFunctionError_t.Success) return (int)emFunctionError_t.Fail;

                // calc shift
                double shiftX = CenterNew.X - CenterOrg.X;
                double shiftY = CenterNew.Y - CenterOrg.Y;

                // calc rotate angle
                double radian = this.AlignNew.Angle - this.AlignOrg.Angle;

                // Rigid Transformation
                dNewX = (Math.Cos(radian) * dOrgX) - (Math.Sin(radian) * dOrgY) + shiftX;
                dNewY = (Math.Sin(radian) * dOrgX) + (Math.Cos(radian) * dOrgY) + shiftY;

                return (int)emFunctionError_t.Success;

            }
            catch (Exception ex)
            {
                dNewX = dOrgX;
                dNewY = dOrgY;

                return (int)emFunctionError_t.Fail;
            }
        }

        /// <summary>
        /// 3P Align Point 의 중심점을 계산
        /// </summary>
        /// <param name="bNewAlign">true = 측정한 Align Pos 사용, false 도면상 Align Pos 사용</param>
        /// <param name="ptCenter">중심점</param>
        /// <returns></returns>
        public int Get3PointAlignCenterPos(bool bNewAlign, ref PointF ptCenter)
        {
            try
            {
                PointD[] point = new PointD[3];

                if (bNewAlign) // 측정한 Align Point
                {
                    point[0] = this.AlignNew.pos[0];
                    point[1] = this.AlignNew.pos[1];
                    point[2] = this.AlignNew.pos[2];
                }
                else // 도면상 Align Point
                {
                    point[0] = this.AlignOrg.pos[0];
                    point[1] = this.AlignOrg.pos[1];
                    point[2] = this.AlignOrg.pos[2];
                }

                // 두 수직 이등분선의 기울기
                double d1 = (point[1].dX - point[0].dX) / (point[1].dY - point[0].dY);
                double d2 = (point[2].dX - point[1].dX) / (point[2].dY - point[1].dY);

                // 세 점이 지나는 원의 중심
                double cx = ((point[2].dY - point[0].dY) + (point[1].dX + point[2].dX) * d2 - (point[0].dX + point[1].dX) * d1) / (2 * (d2 - d1));
                double cy = -d1 * (cx - (point[0].dX + point[1].dX) / 2) + (point[0].dY + point[1].dY) / 2;

                // 원의 반지름
                double r = Math.Sqrt(Math.Pow(point[0].dX - cx, 2) + Math.Pow(point[0].dY - cy, 2));

                ptCenter.X = (float)cx;
                ptCenter.Y = (float)cy;

                //// 중심점과 첫번째 포인트를 기준으로 angle 계산함
                //if (bNewAlign) // 측정한 Align Point
                //    this.AlignOrg.Angle = Math.Atan2((this.AlignOrg.pos[0].dY - ptCenter.Y), (this.AlignOrg.pos[0].dX - ptCenter.X));
                //else
                //    this.AlignNew.Angle = Math.Atan2((this.AlignNew.pos[0].dY - ptCenter.Y), (this.AlignNew.pos[0].dX - ptCenter.X));

                return (int)emFunctionError_t.Success;

            }
            catch (Exception ex)
            {
                ptCenter.X = 0;
                ptCenter.Y = 0;

                return (int)emFunctionError_t.Fail;
            }
        }

        private void SortAlign(ref PointF[] ptArray)
        {
            // (0,0) 에서 4점의 각도를 구한다.
            Dictionary<PointF, double> dic = new Dictionary<PointF, double>();
            double[] Angle = new double[ptArray.Length];

            for (int i =0; i < ptArray.Length; i++)
            {
                Angle[i] = Math.Atan2(ptArray[i].Y * -1, ptArray[i].X * -1) / 180 * Math.PI;
                Angle[i] -= 180;

                if (Angle[i] < 180) Angle[i] += 180;

                dic.Add(ptArray[i], Angle[i]);
            }

            var sortDic = dic.OrderBy(x => x.Value);

            int n = 0;
            foreach (var Dictionary in sortDic)
            {
                ptArray[n] = Dictionary.Key;
                n++;
            }

        }

        public double dist(PointF p1, PointF p2)
        {
            return (p1.X - p2.X)*(p1.X - p2.X) + (p1.Y - p2.Y)*(p1.Y - p2.Y);
        }

        public int ccw(PointF p1, PointF p2, PointF p3)
        {

            double cross_product = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);

            if (cross_product > 0.000001)
            {
                return 1;
            }
            else if (cross_product < 0.000001)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        // right가 left의 반시계 방향에 있으면 true이다.
        // true if right is counterclockwise to left.
        //public int comparatorCCW(PointF left, PointF right)
        //{
        //    int ret;
        //    int direction = ccw(&p, left, right);

        //    if(direction == 0)
        //    {        
        //        ret = (dist(&p, left) < dist(&p, right));
        //    }
        //    else if(direction == 1){
        //        ret = 1;
        //    }
        //    else{
        //        ret = 0;
        //    }
        //    return ret;
        //}

    #endregion


    #region 내부 함수
    /// <summary>
    /// Load Calibration File
    /// </summary>
    /// <param name="strPath"></param>
    /// <returns></returns>
				private int LoadCalFile(string strPath)
        {
            int iResult = -1;
						m_fCalFileMinDX=0;
						m_fCalFileMaxDX=0;
						m_fCalFileMinDY=0;
						m_fCalFileMaxDY=0;

						try
            {
                string strRead = string.Empty;

                FileInfo fileInfo = new FileInfo(strPath);
                if (!fileInfo.Exists)
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }
                else
                {
                    using (var stream = new StreamReader(strPath))
                    {
                        strRead = stream.ReadLine();
                        strRead = stream.ReadLine();

                        string[] strHeader = strRead.Split(',');

                        m_nArrayX = Convert.ToInt32(strHeader[0]);
                        m_nArrayY = Convert.ToInt32(strHeader[1]);
												m_rpStart.dX = Convert.ToDouble(strHeader[2]);
												m_rpStart.dY = Convert.ToDouble(strHeader[3]);
												m_fResolution = Convert.ToDouble(strHeader[4]);

												//strRead = stream.ReadLine();
											
												m_rpCalPos.Clear();
												string [] strCoordinate;
                        for (int i = 0; i < m_nArrayY; i++)
                        {
                            m_rpCalPos.Add(new List<PointD>());

                            strRead = stream.ReadLine();
														string[] strDataRow=strRead.Split(new char[] { '\t' },StringSplitOptions.RemoveEmptyEntries);
                          

                            for (int d = 0; d < strDataRow.Count(); d++)
                            {
                                strDataRow[d] = strDataRow[d].Trim();
                            }
                            
                            PointD stData = new PointD();
                            stData.Clear();

                            for (int j = 0; j < m_nArrayX; j++)
                            {
																
																strCoordinate =strDataRow[j].Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
																stData.Clear();
																stData.dX = Convert.ToDouble(strCoordinate[0]);
																stData.dY = Convert.ToDouble(strCoordinate[1]);
														
																m_rpCalPos[i].Add(stData);
																if (i == 0 && j == 0)
																{
																		m_fCalFileMinDX = stData.dX;
																		m_fCalFileMaxDX = stData.dX;
																		m_fCalFileMinDY = stData.dY;
																		m_fCalFileMaxDY = stData.dY;
																}
																else
																{
																		if(m_fCalFileMinDX>stData.dX)
																		{
																				m_fCalFileMinDX=stData.dX;
																		}
																		if(m_fCalFileMaxDX<stData.dX)
																		{
																				m_fCalFileMaxDX=stData.dX;
																		}
																		if (m_fCalFileMinDY > stData.dY)
																		{
																				m_fCalFileMinDY = stData.dY;
																		}
																		if (m_fCalFileMaxDY < stData.dY)
																		{
																				m_fCalFileMaxDY = stData.dY;
																		}
																}

														}
                        }
                    }

                    m_bLoad = true;
                    iResult = (int)emFunctionError_t.Success;
                }
            }
            catch (Exception ex)
            {
                
            }

            return iResult;
        }


        /// <summary>
        /// Calibration using cal file 
        /// </summary>
        /// <param name="dOrgX"></param>
        /// <param name="dOrgY"></param>
        /// <param name="dNewX"></param>
        /// <param name="dNewY"></param>
        /// <returns></returns>
        public int CorrectPosUsingCalFile(double dOrgX, double dOrgY, out double dNewX, out double dNewY)
        {
            int iResult = -1;
            dNewX = dNewY = -999.999;

            try
            {
                PointD[] rpVsOrg = new PointD[4];
                PointD[] rpSC = new PointD[4];

                int nIndexX, nIndexY;
                bool bFind = false;

                nIndexX = nIndexY = -1;
                // 해당 영역을 찾는다.
                for (int j = 0; j < m_nArrayY - 1; j++)
                {
                    if (bFind) break;

                    for (int i = 0; i < m_nArrayX - 1; i++)
                    {
                        //polygon poly;
                        List<PointD> poly = new List<PointD>();

												//rpSC[0].dX = m_rpStart.dX + (i * m_fResolution);
												//rpSC[0].dY = m_rpStart.dY + (j * m_fResolution);
												//rpSC[1].dX = m_rpStart.dX + ((i + 1) * m_fResolution);
												//rpSC[1].dY = m_rpStart.dY + (j * m_fResolution);
												//rpSC[2].dX = m_rpStart.dX + ((i + 1) * m_fResolution);
												//rpSC[2].dY = m_rpStart.dY + ((j + 1) * m_fResolution);
												//rpSC[3].dX = m_rpStart.dX + (i * m_fResolution);
												//rpSC[3].dY = m_rpStart.dY + ((j + 1) * m_fResolution);

												rpSC[0] = m_rpCalPos[j][i];
												rpSC[1] = m_rpCalPos[j+1][i];
												rpSC[2] = m_rpCalPos[j+1][i+1];
												rpSC[3] = m_rpCalPos[j][i+1];

												poly.Add(rpSC[0]);
												poly.Add(rpSC[1]);
												poly.Add(rpSC[2]);
												poly.Add(rpSC[3]);

												PointD rpCur;
                        rpCur.dX = dOrgX; rpCur.dY = dOrgY;
                        if (isInside(rpCur, poly))
                        {
                            nIndexX = i;
                            nIndexY = j;
                            bFind = true;
                            break;
                        }
                    }
                }

                if (bFind)
                {
                    // cal 파일에서 가져온 값
                    rpVsOrg[0].dX = m_rpCalPos[nIndexY][nIndexX].dX;
                    rpVsOrg[0].dY = m_rpCalPos[nIndexY][nIndexX].dY;
                    rpVsOrg[1].dX = m_rpCalPos[nIndexY][nIndexX + 1].dX;
                    rpVsOrg[1].dY = m_rpCalPos[nIndexY][nIndexX + 1].dY;
                    rpVsOrg[2].dX = m_rpCalPos[nIndexY + 1][nIndexX + 1].dX;
                    rpVsOrg[2].dY = m_rpCalPos[nIndexY + 1][nIndexX + 1].dY;
                    rpVsOrg[3].dX = m_rpCalPos[nIndexY + 1][nIndexX].dX;
                    rpVsOrg[3].dY = m_rpCalPos[nIndexY + 1][nIndexX].dY;

                    //rpSC[0].dX = 0;
                    //rpSC[0].dY = 0;
                    //rpSC[1].dX = m_fResolution;
                    //rpSC[1].dY = 0;
                    //rpSC[2].dX = m_fResolution;
                    //rpSC[2].dY = m_fResolution;
                    //rpSC[3].dX = 0;
                    //rpSC[3].dY = m_fResolution;

										rpSC[0].dX = m_rpStart.dX + (nIndexX * m_fResolution);
										rpSC[0].dY = m_rpStart.dY + (nIndexY * m_fResolution);
										rpSC[1].dX = m_rpStart.dX + ((nIndexX + 1) * m_fResolution);
										rpSC[1].dY = m_rpStart.dY + (nIndexY * m_fResolution);
										rpSC[2].dX = m_rpStart.dX + ((nIndexX + 1) * m_fResolution);
										rpSC[2].dY = m_rpStart.dY + ((nIndexY + 1) * m_fResolution);
										rpSC[3].dX = m_rpStart.dX + (nIndexX * m_fResolution);
										rpSC[3].dY = m_rpStart.dY + ((nIndexY + 1) * m_fResolution);

										// cal h matrix
										Homography_from_4corresp(rpVsOrg[0], rpVsOrg[1], rpVsOrg[2], rpVsOrg[3], rpSC[0], rpSC[1], rpSC[2], rpSC[3]);

                    //dOrgX = dOrgX - (m_fResolution * nIndexX);
                    //dOrgY = dOrgY - (m_fResolution * nIndexY);
										
                    // homography transform
                    dNewX = m_HM[0, 0] * dOrgX + m_HM[0, 1] * dOrgY + m_HM[0, 2];
                    dNewY = m_HM[1, 0] * dOrgX + m_HM[1, 1] * dOrgY + m_HM[1, 2];

                    //dNewX += (m_fResolution * nIndexX);
                    //dNewY += (m_fResolution * nIndexY);
                }
                else
                {
                    dNewX = dOrgX;
                    dNewY = dOrgY;
                }

                if (bFind)
                    iResult = (int)emFunctionError_t.Success;
                else
                    iResult = (int)emFunctionError_t.Fail;
            }
            catch (Exception ex)
            {
                //m_cLog.WriteDebug("CorrectionPos() : " + ex.Message);
            }

            return iResult;
        }
			

				/// <summary>
				/// Homography (Projective Transformation) 식 계산
				/// </summary>
				/// <param name="rp1"></param>
				/// <param name="rp2"></param>
				/// <param name="rp3"></param>
				/// <param name="rp4"></param>
				/// <param name="h"></param>
				private void Homography_from_4pt(PointD rp1, PointD rp2, PointD rp3, PointD rp4, ref double[,] h)
        {
            try
            {
                double t1 = rp1.dX; double t2 = rp3.dX; double t4 = rp2.dY; double t5 = t1 * t2 * t4;
                double t6 = rp4.dY; double t7 = t1 * t6; double t8 = t2 * t7; double t9 = rp3.dY;
                double t10 = t1 * t9; double t11 = rp2.dX; double t14 = rp1.dY; double t15 = rp4.dX;
                double t16 = t14 * t15; double t18 = t16 * t11; double t20 = t15 * t11 * t9;
                double t21 = t15 * t4; double t24 = t15 * t9; double t25 = t2 * t4;
                double t26 = t6 * t2; double t27 = t6 * t11; double t28 = t9 * t11;
                double t30 = 0.1e1 / (-t24 + t21 - t25 + t26 - t27 + t28);
                double t32 = t1 * t15; double t35 = t14 * t11; double t41 = t4 * t1;
                double t42 = t6 * t41; double t43 = t14 * t2; double t46 = t16 * t9;
                double t48 = t14 * t9 * t11; double t51 = t4 * t6 * t2; double t55 = t6 * t14;

                h[0, 0] = -(-t5 + t8 + t10 * t11 - t11 * t7 - t16 * t2 + t18 - t20 + t21 * t2) * t30;
                h[0, 1] = (t5 - t8 - t32 * t4 + t32 * t9 + t18 - t2 * t35 + t27 * t2 - t20) * t30;
                h[0, 2] = t1;
                h[1, 0] = (-t9 * t7 + t42 + t43 * t4 - t16 * t4 + t46 - t48 + t27 * t9 - t51) * t30;
                h[1, 1] = (-t42 + t41 * t9 - t55 * t2 + t46 - t48 + t55 * t11 + t51 - t21 * t9) * t30;
                h[1, 2] = t14;
                h[2, 0] = (-t10 + t41 + t43 - t35 + t24 - t21 - t26 + t27) * t30;
                h[2, 1] = (-t7 + t10 + t16 - t43 + t27 - t28 - t21 + t25) * t30;
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Homography (Projective Transformation)을 이용한 변환
        /// </summary>
        /// <param name="rpImg1"></param>
        /// <param name="rpImg2"></param>
        /// <param name="rpImg3"></param>
        /// <param name="rpImg4"></param>
        /// <param name="rpReal1"></param>
        /// <param name="rpReal2"></param>
        /// <param name="rpReal3"></param>
        /// <param name="rpReal4"></param>
        private void Homography_from_4corresp(PointD rpImg1, PointD rpImg2, PointD rpImg3, PointD rpImg4,
                                                PointD rpReal1, PointD rpReal2, PointD rpReal3, PointD rpReal4)
        {
            try
            {
                double[,] Hr = new double[3, 3];
                double[,] Hl = new double[3, 3];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Hr[i, j] = 0.0;
                        Hl[i, j] = 0.0;
                    }
                }

                Homography_from_4pt(rpImg1, rpImg2, rpImg3, rpImg4, ref Hr);
                Homography_from_4pt(rpReal1, rpReal2, rpReal3, rpReal4, ref Hl);

                // the following code computes R = Hl * inverse Hr
                double t2 = Hr[1, 1] - Hr[2, 1] * Hr[1, 2];
                double t4 = Hr[0, 0] * Hr[1, 1];
                double t5 = Hr[0, 0] * Hr[1, 2];
                double t7 = Hr[1, 0] * Hr[0, 1];
                double t8 = Hr[0, 2] * Hr[1, 0];
                double t10 = Hr[0, 1] * Hr[2, 0];
                double t12 = Hr[0, 2] * Hr[2, 0];
                double t15 = 1 / (t4 - t5 * Hr[2, 1] - t7 + t8 * Hr[2, 1] + t10 * Hr[1, 2] - t12 * Hr[1, 1]);
                double t18 = -Hr[1, 0] + Hr[1, 2] * Hr[2, 0];
                double t23 = -Hr[1, 0] * Hr[2, 1] + Hr[1, 1] * Hr[2, 0];
                double t28 = -Hr[0, 1] + Hr[0, 2] * Hr[2, 1];
                double t31 = Hr[0, 0] - t12;
                double t35 = Hr[0, 0] * Hr[2, 1] - t10;
                double t41 = -Hr[0, 1] * Hr[1, 2] + Hr[0, 2] * Hr[1, 1];
                double t44 = t5 - t8;
                double t47 = t4 - t7;
                double t48 = t2 * t15;
                double t49 = t28 * t15;
                double t50 = t41 * t15;

                m_HM[0, 0] = Hl[0, 0] * t48 + Hl[0, 1] * (t18 * t15) - Hl[0, 2] * (t23 * t15);
                m_HM[0, 1] = Hl[0, 0] * t49 + Hl[0, 1] * (t31 * t15) - Hl[0, 2] * (t35 * t15);
                m_HM[0, 2] = -Hl[0, 0] * t50 - Hl[0, 1] * (t44 * t15) + Hl[0, 2] * (t47 * t15);
                m_HM[1, 0] = Hl[1, 0] * t48 + Hl[1, 1] * (t18 * t15) - Hl[1, 2] * (t23 * t15);
                m_HM[1, 1] = Hl[1, 0] * t49 + Hl[1, 1] * (t31 * t15) - Hl[1, 2] * (t35 * t15);
                m_HM[1, 2] = -Hl[1, 0] * t50 - Hl[1, 1] * (t44 * t15) + Hl[1, 2] * (t47 * t15);
                m_HM[2, 0] = Hl[2, 0] * t48 + Hl[2, 1] * (t18 * t15) - t23 * t15;
                m_HM[2, 1] = Hl[2, 0] * t49 + Hl[2, 1] * (t31 * t15) - t35 * t15;
                m_HM[2, 2] = -Hl[2, 0] * t50 - Hl[2, 1] * (t44 * t15) + t47 * t15;
            }
            catch (Exception ex)
            {
                //m_cLog.WriteDebug("homography_from_4corresp() : " + ex.Message);
            }
        }
        /// <summary>
        /// Rigid Transformation matrix
        /// </summary>
        private void RigidTransformation(PointD pdOrg1, PointD pdOrg2, PointD pdReal1, PointD pdReal2)
        {
           

        }

        /// <summary>
        /// 한 점이 Poly 영역내에 있는지 판별한다
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="poly"></param>
        /// <returns></returns>
        bool isInside(PointD rp, List<PointD> poly)
        {            
            bool bResult = false;
            
            try
            {
                //crosses는 점과 오른쪽 반직선과 다각형과의 교점의 개수
                int crosses = 0;
                for (int i = 0; i < poly.Count; i++)
                {
                    int j = (i + 1) % poly.Count;
                    //점이 선분 (p[i], p[j])의 y좌표 사이에 있음
                    //if ((poly[i].y > rp.y) != (poly[j].y > rp.y))
                    if ((poly[i].dY < rp.dY && poly[j].dY >= rp.dY) || (poly[i].dY > rp.dY && poly[j].dY < rp.dY))
                    {
                        //atX는 점 B를 지나는 수평선과 선분 (p[i], p[j])의 교점
                        double atX = (poly[j].dX - poly[i].dX) * (rp.dY - poly[i].dY) / (poly[j].dY - poly[i].dY) + poly[i].dX;
                        //atX가 오른쪽 반직선과의 교점이 맞으면 교점의 개수를 증가시킨다.
                        //if (rp.x < atX)
                        if (rp.dX - atX < 0.000001)
                            crosses++;
                    }
                }

                bResult = Convert.ToBoolean(crosses % 2 > 0);
            }
            catch(Exception ex)
            {
                
            }

            return bResult;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="poly"></param>
        /// <returns></returns>
        bool isInside2(PointD rp, List<PointD> poly)
        {
            bool bResult = false;

            try
            {
                //crosses는 점과 오른쪽 반직선과 다각형과의 교점의 개수
                int crosses = 0;
                for (int i = 0; i < poly.Count; i++)
                {
                    int j = (i + 1) % poly.Count;
                    //점이 선분 (p[i], p[j])의 y좌표 사이에 있음
                    if ((rp.dX == poly[i].dX && rp.dX == poly[j].dX) &&
                        ((rp.dY >= poly[i].dY && rp.dY <= poly[j].dY)))// || (rp.y <= poly[i].y && rp.y >= poly[j].y)))
                    {
                        crosses = 1;
                        break;
                    }
                    else if ((rp.dY == poly[i].dY && rp.dY == poly[j].dY) &&
                        ((rp.dX >= poly[i].dX && rp.dX <= poly[j].dX)))// || (rp.x <= poly[i].x && rp.x >= poly[j].x)))
                    {
                        crosses = 1;
                    }
                }

                bResult = Convert.ToBoolean(crosses % 2 > 0);
            }
            catch(Exception ex)
            {
                
            }
            
            return bResult;
        }
        #endregion
    }
}
