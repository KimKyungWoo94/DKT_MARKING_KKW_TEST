using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using ClipperLib;
using System.Windows.Forms;

namespace EzCAM_Ver2
{
    /// <summary>
    /// Shape Data 변형 및 계산에 관한 클래스
    /// </summary>
    class GraphicsFunction
    {
        private object lockObject = new object();
        const float UPSCALE = 10.0f;
        const float EPSILON = 0.001f;
        const float Flatness = 0.05f;

        /// <summary>
        /// Shape 을 GraphicsPath 로 변환
        /// </summary>
        /// <param name="Shapedata"></param>
        /// <param name="graphicsPath"></param>
        /// <returns></returns>
        //public bool ConvetShapeToGrapicsPath(Shapes Shapedata, ref GraphicsPath graphicsPath)
        //{
        //    try
        //    {
        //        graphicsPath.StartFigure();

        //        foreach (Segment segment in Shapedata.m_Segment)
        //        {
        //            switch (segment.m_Pattern)
        //            {
        //                case Segment_Pattern.CAM_PATTERN_LINE:
        //                    graphicsPath.AddLine(segment.fStart, segment.fEnd);
        //                    break;

        //                case Segment_Pattern.CAM_PATTERN_ARC:
        //                    {
        //                        segment.SetBoundary();

        //                        segment.TransForm_Flip();

        //                        graphicsPath.AddArc(segment.rectBoundary, segment.fStartAngle, segment.fSweepAngle);

        //                        segment.TransForm_Flip();

        //                        //graphicsPath.AddArc(segment.rectBoundary, segment.fStartAngle, segment.fSweepAngle * -1);
        //                        break;
        //                    }
        //                case Segment_Pattern.CAM_PATTERN_CIRCLE:
        //                    segment.SetBoundary();
        //                    graphicsPath.AddEllipse(segment.rectBoundary);
        //                    break;

        //                default:
        //                    break;
        //            }
        //        }

        //        if (Shapedata.bClose)
        //        {
        //            PointF last = graphicsPath.GetLastPoint();
        //            PointF[] points = graphicsPath.PathPoints;
        //            if (!points[0].Equals(last) && Math.Abs(last.X - points[0].X) < 0.001 && Math.Abs(last.Y - points[0].Y) < 0.001)
        //            {
        //                graphicsPath.AddLine(last, points[0]);
        //            }
        //        }

        //        graphicsPath.CloseFigure();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ConvetShapeToGrapicsPath() : " + ex.Message.ToString());
        //        return false;
        //    }
        //}



        /// <summary>
        /// GraphicsPath 를 Shape 으로 변환
        /// </summary>
        /// <param name="graphicsPath"></param>
        /// <param name="Shapedata"></param>
        /// <returns></returns>
        //public bool ConvetGrapicsPathToShape(GraphicsPath graphicsPath, ref Shapes Shapedata)
        //{
        //    try
        //    {
        //        PointF ptStart = new PointF();
        //        PointF ptBefore = new PointF();
        //        PointF ptPoint = new PointF();

        //        int iIndex = 0;
        //        int iPathCount = graphicsPath.PointCount;

        //        PointF[] Points = graphicsPath.PathPoints;
        //        byte[] Types = graphicsPath.PathTypes;

        //        while (iIndex < iPathCount)
        //        {
        //            for (bool flag = false; iIndex < iPathCount && !flag; ++iIndex)
        //            {
        //                ptPoint = Points[iIndex];
        //                switch (Types[iIndex])
        //                {
        //                    case 0:
        //                        ptStart = ptPoint;
        //                        ptBefore = ptStart;
        //                        break;

        //                    case 1:
        //                    case 3:
        //                        {
        //                            Segment segment = new Segment();
        //                            segment.m_Pattern = Segment_Pattern.CAM_PATTERN_LINE;
        //                            segment.fStart.X = ptBefore.X;
        //                            segment.fStart.Y = ptBefore.Y;
        //                            segment.fEnd.X = ptPoint.X;
        //                            segment.fEnd.Y = ptPoint.Y;

        //                            Shapedata.AddSegment(segment);

        //                            ptBefore.X = ptPoint.X;
        //                            ptBefore.Y = ptPoint.Y;

        //                            break;
        //                        }
        //                    case 128:
        //                    case 129:
        //                        {
        //                            Segment segment = new Segment();
        //                            segment.m_Pattern = Segment_Pattern.CAM_PATTERN_LINE;
        //                            segment.fStart.X = ptBefore.X;
        //                            segment.fStart.Y = ptBefore.Y;
        //                            segment.fEnd.X = ptPoint.X;
        //                            segment.fEnd.Y = ptPoint.Y;

        //                            Shapedata.AddSegment(segment);

        //                            ptBefore.X = ptPoint.X;
        //                            ptBefore.Y = ptPoint.Y;

        //                            // for Closed Path
        //                            if (ptPoint.X != ptStart.X || ptBefore.Y != ptStart.Y)
        //                            {
        //                                Segment segment2 = new Segment();
        //                                segment2.m_Pattern = Segment_Pattern.CAM_PATTERN_LINE;
        //                                segment2.fStart.X = ptPoint.X;
        //                                segment2.fStart.Y = ptPoint.Y;
        //                                segment2.fEnd.X = ptStart.X;
        //                                segment2.fEnd.Y = ptStart.Y;

        //                                Shapedata.AddSegment(segment2);
        //                            }

        //                            break;
        //                        }
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ConvetGrapicsPathToShape() : " + ex.Message.ToString());
        //        return false;
        //    }
        //}

        /// <summary>
        /// 두개의 포인트를 라인 세그먼트로 변환
        /// </summary>
        /// <param name="ptList"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        //public bool ConvetPointListToLineSegment(List<PointF> ptList, ref Segment segment)
        //{
        //    try
        //    {
        //        // hatch segment로 등록
        //        int nSegmentCount = Convert.ToInt32(ptList.Count / 2.0f);

        //        for (int i = 0; i < nSegmentCount; i++)
        //        {
        //            segment.m_Pattern = Segment_Pattern.CAM_PATTERN_LINE;
        //            segment.fStart.X = ptList[0].X;
        //            segment.fStart.Y = ptList[0].Y;
        //            segment.fEnd.X = ptList[1].X;
        //            segment.fEnd.Y = ptList[1].Y;

        //            //segment.LayserName = ;
        //            //segment.nShapeIndex = ;
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ConvetPointListToLineSegment() : " + ex.Message.ToString());
        //        return false;
        //    }
        //}

        /// <summary>
        /// Convert GraphicsPath to Polygon
        /// </summary>
        /// <param name="path"></param>
        /// <param name="scale"></param>
        /// <param name="polys"></param>
        /// <returns></returns>
        public bool ConvertPathToPolygon(GraphicsPath path, float scale, ref List<List<IntPoint>> polys)
        {
            try
            {
                GraphicsPathIterator graphicsPathIterator = new GraphicsPathIterator(path);
                graphicsPathIterator.Rewind();

                polys.Clear();
                PointF[] points = new PointF[graphicsPathIterator.Count];
                byte[] types = new byte[graphicsPathIterator.Count];
                graphicsPathIterator.Enumerate(ref points, ref types);

                int index = 0;
                while (index < graphicsPathIterator.Count)
                {
                    List<IntPoint> intPointList = new List<IntPoint>();
                    polys.Add(intPointList);

                label_2:
                    IntPoint intPoint = new IntPoint((long)(int)((double)points[index].X * (double)scale), (long)(int)((double)points[index].Y * (double)scale));
                    intPointList.Add(intPoint);
                    ++index;
                    if (index < graphicsPathIterator.Count && types[index] != (byte)0)
                        goto label_2;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ConvertPathToPolygon() : " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Convert Polygon To PointF Array
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="scale"></param>
        /// <returns>PointF[]</returns>
        public PointF[] ConvertPolygonToPointF(List<IntPoint> pointList, float scale)
        {
            PointF[] pointFArray = new PointF[pointList.Count];
            for (int index = 0; index < pointList.Count; ++index)
            {
                pointFArray[index].X = (float)pointList[index].X / scale;
                pointFArray[index].Y = (float)pointList[index].Y / scale;
            }
            return pointFArray;
        }

        /// <summary>
        /// GraphicsPath의 경로와 만나는 지점을 계산
        /// </summary>
        /// <param name="pPath"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<PointF> GetCrossPoint(GraphicsPath Path, PointF Start, PointF End)
        {
            try
            {

                List<PointF> pointFList = new List<PointF>();
                PointF ptFind = new PointF();
                PointF ptCurr = new PointF();
                PointF ptPoint = new PointF();

                //GraphicsPath pathTemp = new GraphicsPath();
                //pathTemp.AddPath(pPath);

                int iIndex = 0;
                int iPathCount = 0;

                PointF[] Points;
                byte[] Types;

                RectangleF recctBounds; ;

                GraphicsPath pPath = new GraphicsPath();
                pPath.AddPath(Path, false);

                //lock (lockObject)
                //{
                iPathCount = pPath.PointCount;

                Points = pPath.PathPoints;
                Types = pPath.PathTypes;

                recctBounds = pPath.GetBounds();
                //}

                // Org
                while (iIndex < iPathCount)
                {
                    if ((double)Start.Y >= (double)recctBounds.Top && (double)Start.Y <= (double)recctBounds.Bottom)
                    {
                        for (bool flag = false; iIndex < iPathCount && !flag; ++iIndex)
                        {
                            PointF ptPrev = ptPoint;
                            ptPoint = Points[iIndex];

                            switch (Types[iIndex])
                            {
                                case 0:
                                    ptCurr = ptPoint;
                                    break;

                                case 1:
                                    if (this.GetIntersectionPoint(Start, End, ptPrev, ptPoint, ref ptFind))
                                    {
                                        pointFList.Add(new PointF(ptFind.X, ptFind.Y));
                                    }
                                    break;

                                case 129:
                                case 161:
                                    if (this.GetIntersectionPoint(Start, End, ptPrev, ptPoint, ref ptFind))
                                        pointFList.Add(new PointF(ptFind.X, ptFind.Y));

                                    if (ptPoint != ptCurr && this.GetIntersectionPoint(Start, End, ptPoint, ptCurr, ref ptFind))
                                        pointFList.Add(new PointF(ptFind.X, ptFind.Y));

                                    flag = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // To Do
                        // 확인 필요함

                        ++iIndex;

                    }

                }

                // sort;
                pointFList.Sort((pointA, pointB) => pointA.X.CompareTo(pointB.X)); // 람다식으로 정렬 구현함.

                return pointFList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetCrossPoint() : " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 기존
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="Prev"></param>
        /// <param name="Curr"></param>
        /// <param name="Find"></param>
        /// <returns></returns>
        public bool GetIntersectionPoint(PointF Start, PointF End, PointF Prev, PointF Curr, ref PointF Find)
        {
            try
            {
                // 가로로 평행할 경우
                if ((double)Start.Y == (double)End.Y && (double)Curr.Y == (double)Start.Y)
                {
                    Find = Curr;
                    return true;
                }

                double num1 = ((double)Curr.Y - (double)Prev.Y) * ((double)End.X - (double)Start.X) - ((double)Curr.X - (double)Prev.X) * ((double)End.Y - (double)Start.Y);

                // 교점 없음
                if (num1 == 0.0)
                    return false;

                double num2 = ((double)Curr.X - (double)Prev.X) * ((double)Start.Y - (double)Prev.Y) - ((double)Curr.Y - (double)Prev.Y) * ((double)Start.X - (double)Prev.X);
                double num3 = ((double)End.X - (double)Start.X) * ((double)Start.Y - (double)Prev.Y) - ((double)End.Y - (double)Start.Y) * ((double)Start.X - (double)Prev.X);
                double num4 = num2 / num1;
                double num5 = num3 / num1;

                if (num4 < 0.0 || num4 > 1.0 || (num5 <= 0.0 || num5 > 1.0) || num2 == 0.0 && num3 == 0.0)
                    return false;

                Find.X = Start.X + (float)(num4 * ((double)End.X - (double)Start.X));
                Find.Y = Start.Y + (float)(num4 * ((double)End.Y - (double)Start.Y));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetIntersectionPoint() : " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// 교차점 수식 이해를 위해 새로 짜봄
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="Find"></param>
        /// <returns></returns>
        public bool GetIntersectionPointF(PointF p1, PointF p2, PointF p3, PointF p4, ref PointF Find)
        {
            try
            {
                double d = (p2.X - p1.X) * (p4.Y - p3.Y) - (p2.Y - p1.Y) * (p4.X - p3.X);

                if (d == 0)
                    return false;

                double t1 = (p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X);
                double s1 = (p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X);

                if (t1 == 0 && s1 == 0)
                    return false;

                double t2 = t1 / d;
                double s2 = s1 / d;

                if (t2 < 0.0f || t2 > 1.0f || s2 < 0.0f || s2 > 1.0f)
                    return false;

                Find.X = (float)(p1.X + t2 * (p2.X - p1.X));
                Find.Y = (float)(p1.Y + t2 * (p2.Y - p1.Y));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ConvetShapeToGrapicsPath() : " + ex.Message.ToString());
                return false;
            }
        }


        /// <summary>
        /// Offset 이 적용된 Shape 외곽라인을 생성한다
        /// </summary>
        /// <param name="pathOrg"></param>
        /// <param name="pathClipper"></param>
        /// <returns></returns>
        public bool GetClipper(GraphicsPath pathOrg, ref GraphicsPath pathClipper, float fOffset)
        {
            try
            {
                List<List<IntPoint>> intPointListList = new List<List<IntPoint>>();
                float scale = -100000.0f;

                // matrix 1
                Matrix matrix1 = new Matrix();
                matrix1.Scale(UPSCALE, UPSCALE);

                //pathOrg.Flatten();
                pathOrg.Flatten(matrix1, Flatness);

                // matrix 2
                //Matrix matrix2 = new Matrix();
                //matrix2.Scale(1.0f / fUPSCALE, 1.0f / fUPSCALE); 

                //pathOrg.Transform(matrix2);

                if (!this.ConvertPathToPolygon(pathOrg, scale, ref intPointListList))
                    return false;

                Clipper clipper = new Clipper(0);
                clipper.AddPaths(intPointListList, PolyType.ptSubject, true);

                bool bSuccess = clipper.Execute(ClipType.ctXor, intPointListList, PolyFillType.pftNonZero, PolyFillType.pftEvenOdd);

                if (!bSuccess)
                {
                    pathClipper = pathOrg;
                    return false;
                }
                else
                {
                    List<List<IntPoint>> solution = new List<List<IntPoint>>();
                    ClipperOffset clipperOffset = new ClipperOffset(1.0, 1.0);
                    clipperOffset.AddPaths(intPointListList, JoinType.jtMiter, EndType.etClosedPolygon);
                    clipperOffset.Execute(ref solution, (double)fOffset * (double)scale);

                    pathClipper.StartFigure();

                    if (solution.Count > 0)
                    {
                        for (int index = 0; index < solution.Count; ++index)
                        {
                            PointF[] pointFarray = this.ConvertPolygonToPointF(solution[index], scale);
                            pathClipper.AddPolygon(pointFarray);
                        }
                    }

                    pathClipper.CloseFigure();

                    //matrix 2
                    Matrix matrix2 = new Matrix();
                    matrix2.Scale(1.0f / UPSCALE, 1.0f / UPSCALE);

                    pathClipper.Transform(matrix2);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ConvetShapeToGrapicsPath() : " + ex.Message.ToString());
                return false;
            }
        }
    }
}
