using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using ClipperLib;

namespace EzCAM_Ver2
{
    public enum HATCH_TYPE
    {
        HATCH_TYPE_NONE = 0,
        HATCH_TYPE_LINE,
        HATCH_TYPE_ZIGZAG,
        HATCH_TYPE_CROSS,
        HATCH_TYPE_CONTOUR,
    }

    /// <summary>
    /// Hatch Option Struct
    /// </summary>
    public struct Hatch_Option
    {
        private HATCH_TYPE _Type { get; set; }
        private float _fAngle { get; set; }
        private float _fPitch { get; set; }
        private float _fOffset { get; set; }
        private float _fFlatness { get; set; }
        private bool _bOutline { get; set; }

        public HATCH_TYPE Type { get { return _Type; } set { _Type = value; } }
        public float fAngle { get { return _fAngle; } set { _fAngle = value; } }
        public float fPitch { get { return _fPitch; } set { _fPitch = value; } }
        public float fOffset { get { return _fOffset; } set { _fOffset = value; } }
        public float fFlatness { get { return _fFlatness; } set { _fFlatness = value; } }
        public bool bOutline { get { return _bOutline; } set { _bOutline = value; } }

        public void Clear()
        {
            this.fAngle = 0.0f;
            this.fPitch = 0.02f;
            this.fOffset = 0.0f;
            this.fFlatness = 0.25f;
            this.Type = HATCH_TYPE.HATCH_TYPE_LINE;
            this.bOutline = false;
        }
				public Hatch_Option Clone()
				{
						Hatch_Option pRet=new Hatch_Option();
						pRet.Clear();
						pRet.fAngle = this.fAngle;
						pRet.fPitch = this.fPitch;
						pRet.fOffset = this.fOffset;
						pRet.fFlatness = this.fFlatness;
						pRet.Type = this.Type;
						pRet.bOutline = this.bOutline;
						return pRet;
				}

    }

    
    /// <summary>
    /// Hatch Create Class
    /// </summary>
    public class Hatch:IDisposable
    {

        //private List<Segment> m_Segment = new List<Segment>();

        private bool bPathDone;

        public RectangleF Size = new RectangleF();
        public float Size_CenterX;
        public float Size_CenterY;

        public Hatch_Option Option;

        GraphicsFunction Funtion = new GraphicsFunction();

        const float UPSCALE = 10.0f;
        const float EPSILON = 0.001f;

        //[CategoryAttribute("Hatch")]
        //[Browsable(true)]
        //[DisplayName("Type")]
        //[DescriptionAttribute("Hatch Type")]
        public HATCH_TYPE Type { get { return this.Option.Type; } set { this.Option.Type = value; } }

        //[CategoryAttribute("Hatch")]
        //[Browsable(true)]
        //[DisplayName("Line Angle")]
        //[DescriptionAttribute("Hatch Line Angle, degree")]
        public float Angle { get { return this.Option.fAngle; } set { this.Option.fAngle = value; } }

        //[CategoryAttribute("Hatch")]
        //[Browsable(true)]
        //[DisplayName("Line Pitch")]
        //[DescriptionAttribute("Hatch Line Pitch, mm")]
        public float Pitch { get { return this.Option.fPitch; } set { this.Option.fPitch = value; } }

        //[CategoryAttribute("Hatch")]
        //[Browsable(true)]
        //[DisplayName("Offset")]
        //[DescriptionAttribute("Offset between outline and hatch, mm")]
        public float Offset { get { return this.Option.fOffset; } set { this.Option.fOffset = value; } }

        //[CategoryAttribute("Hatch")]
        //[Browsable(false)]
        public float Flatness { get { return this.Option.fFlatness; } set { this.Option.fFlatness = value; } }

        public Hatch()
        {
            //this.ClearSegment();
            this.Option.Clear();
        }

        public void Dispose()
        {

        }

        //public void ClearSegment()
        //{
        //    this.m_Segment.Clear();
        //}


        #region Shapes 기존 함수
        /// <summary>
        /// Select All Shape
        /// </summary>
        /// <param name="bSelect"></param>
        public void SelectAllShape(bool bSelect)
        {
            //foreach (Segment data in m_Segment)
            //{
            //    data.bSelected = true;
            //}
        }

        /// <summary>
        /// Swap_Shape
        /// </summary>
        /// <param name="data"></param>
        //public void Swap_Shape(ref Shapes data)
        //{
        //    // 백업 
        //    List < Segment > copy = data.m_Segment.ToList();
        //    RectangleF copySize = new RectangleF();
        //    float Cx = data.Size_CenterX;
        //    float Cy = data.Size_CenterY;
        //    bool bClose = data.bClose;
        //    bool bPathDone = data.bPathDone;
        //    bool bPathOpt = data.bPathOpt;

        //    copySize = data.Size;
        //    data.m_Segment.Clear();

        //    // swap 
        //    data.m_Segment = this.m_Segment.ToList(); 
        //    data.Size = this.Size;
        //    data.Size_CenterX = this.Size_CenterX;
        //    data.Size_CenterY = this.Size_CenterY;

        //    this.m_Segment = copy.ToList();
        //    this.Size = copySize;
        //    this.Size_CenterX = Cx;
        //    this.Size_CenterY = Cy;

        //    this.bPathDone = bPathDone;


        //    // 사용 데이타 Clear
        //    copy.Clear();
        //}
        #endregion


        public double DegreeToRadian(float fAngle) { return Math.PI * fAngle / 180.0f; }
        public double RadianToDegree(float fAngle) { return fAngle * (180.0f / Math.PI); }

        /// <summary>
        /// GraphicsPath Data 로 Hatch Data 생성
        /// </summary>
        /// <param name="pathShape"></param>
        /// <param name="pathHatch"></param>
        /// <returns></returns>
        public bool CreateShapeHatch(GraphicsPath pathShape, ref GraphicsPath pathHatch)
        {
            try
            {
                if (pathShape.PointCount < 1) return false;

                if (this.Option.Type == HATCH_TYPE.HATCH_TYPE_NONE) return false;

                GraphicsPath pathClipper = new GraphicsPath();

                // Apply Offset
                if (!Funtion.GetClipper(pathShape, ref pathClipper, this.Option.fOffset)) return false;

                // Make Hatch
                if (!this.MakeHatch(pathClipper, ref pathHatch)) return false;

                // Outline
                if (this.Option.bOutline)
                    pathHatch.AddPath(pathClipper, false);

                pathClipper.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Create Hach Shape for Shapes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public bool CreateShapeHatch(Shapes shapeData, ref Shapes shapeHatch)
        //{
        //    try
        //    {
        //        if (shapeData.iSegmentCount < 1) return false;

        //        if (this.Option.Type == HATCH_TYPE.HATCH_TYPE_NONE) return false;

        //        //if (!shapeData.bClose) return false;

        //        GraphicsPath pathShape = new GraphicsPath();
        //        GraphicsPath pathClipper = new GraphicsPath();
        //        GraphicsPath pathHatch = new GraphicsPath();

        //        if (!Funtion.ConvetShapeToGrapicsPath(shapeData, ref pathShape)) return false;

        //        //this.Option = shapeData.HatchOption;

        //        // Apply Offset
        //        if (!Funtion.GetClipper(pathShape, ref pathClipper, this.Option.fOffset)) return false;

        //        // Make Hatch
        //        if (!this.MakeHatch(pathClipper, ref pathHatch)) return false;

        //        // Get Hatch Shape
        //        if (!Funtion.ConvetGrapicsPathToShape(pathHatch, ref shapeHatch)) return false;

        //        // test
        //        //this.ConvetGrapicsPathToShape(pathClipper, ref shapeHatch);

        //        shapeHatch.ShpaeType = Shape_Type.CAM_SHAPE_HATCH;
        //        shapeHatch.HatchOption = this.Option;

        //        pathShape.Dispose();
        //        pathClipper.Dispose();
        //        pathHatch.Dispose();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Create Hach Shape for Layer
        /// </summary>
        /// <param name="iLayerIndex"></param>
        /// <param name="shapeHatch"></param>
        /// <returns></returns>
        //public bool CreateLayerHatch(int iLayerIndex, ref Shapes shapeHatch)
        //{
        //    try
        //    {
        //        if (iLayerIndex < 0) return false;

        //        Layer layer = CamFile.Instance.m_LayerList[iLayerIndex];

        //        // shapes -> graphicspath 변환
        //        GraphicsPath pathShape = new GraphicsPath();
        //        GraphicsPath pathClipper = new GraphicsPath();
        //        GraphicsPath pathHatch = new GraphicsPath();

        //        foreach (Shapes shapes in layer.m_ShapesList)
        //        {
        //            if (!Funtion.ConvetShapeToGrapicsPath(shapes, ref pathShape))
        //            {
        //                return false;
        //            }
        //        }

        //        this.Option = layer.HatchOption;

        //        // Apply Offset
        //        if (!Funtion.GetClipper(pathShape, ref pathClipper, this.Option.fOffset)) return false;

        //        if (!this.MakeHatch(pathClipper, ref pathHatch)) return false;

        //        if (!Funtion.ConvetGrapicsPathToShape(pathHatch, ref shapeHatch)) return false;

        //        // test
        //        //Funtion.ConvetGrapicsPathToShape(pathClipper, ref shapeHatch);

        //        shapeHatch.ShpaeType = Shape_Type.CAM_SHAPE_HATCH;
        //        shapeHatch.HatchOption = this.Option;

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


#region 내부함수
        /// <summary>
        /// GraphicsPath
        /// </summary>
        /// <param name="gshpaesPath"></param>
        /// <param name="gHatchPath"></param>
        /// <returns></returns>
        private bool MakeHatch(GraphicsPath pathShpaes, ref GraphicsPath pathHatch)
        {
            try
            {
                if (pathShpaes.PointCount < 1) return false;

                if (this.Option.Type == HATCH_TYPE.HATCH_TYPE_NONE) return false;

                bool bSuccess = false;

                switch (this.Option.Type)
                {
                    case HATCH_TYPE.HATCH_TYPE_LINE:
                    case HATCH_TYPE.HATCH_TYPE_ZIGZAG:
                        bSuccess = this.MakeHatchShape_Line(pathShpaes, ref pathHatch);
                        break;

                    case HATCH_TYPE.HATCH_TYPE_CROSS:
                        bSuccess = this.MakeHatchShape_Cross(pathShpaes, ref pathHatch);
                        break;

                    case HATCH_TYPE.HATCH_TYPE_CONTOUR:
                        bSuccess = this.MakeHatchShape_Contour(pathShpaes, ref pathHatch);
                        break;

                    default:
                        break;
                }

                // test
                //this.ConvetGrapicsPathToShape(pathShpaes, ref shapeHatch);

                return bSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Hatch shape을 생성
        /// </summary>
        /// <param name="graphicsPath"></param>
        /// <returns></returns>
        private bool MakeHatchShape_Line(GraphicsPath graphicsPath, ref GraphicsPath gHatchPath)
        {
            try
            {
                GraphicsPath pPath = new GraphicsPath();
                pPath.AddPath(graphicsPath, false);

                // matrix 1
                Matrix matrix1 = new Matrix();
                // Hatch Angle 적용
                matrix1.Rotate(this.Option.fAngle);

                // scale up
                matrix1.Scale(UPSCALE, UPSCALE);

                // flatten
                pPath.Flatten(matrix1, this.Option.fFlatness);

                this.MakeHatchSegment(pPath, ref gHatchPath);

                // matrix 2
                Matrix matrix2 = new Matrix();
                matrix2.Rotate(this.Option.fAngle * -1);
                matrix2.Scale(1.0f / UPSCALE, 1.0f / UPSCALE);

                // scale down
                gHatchPath.Transform(matrix2);

                pPath.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool MakeHatchShape_Cross(GraphicsPath pathShape, ref GraphicsPath pathHatch)
        {
            try
            {
                bool bSuccess = this.MakeHatchShape_Line(pathShape, ref pathHatch);

                if (bSuccess)
                {
                    GraphicsPath pathCross = new GraphicsPath();
                    this.Option.fAngle += 90;
                    bSuccess = this.MakeHatchShape_Line(pathShape, ref pathCross);
                    this.Option.fAngle -= 90;

                    if (bSuccess)
                        pathHatch.AddPath(pathCross, false);

                    pathCross.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool MakeHatchShape_Contour(GraphicsPath pathShape, ref GraphicsPath pathHatch)
        {
            try
            {
                List<List<IntPoint>> intPointListList = new List<List<IntPoint>>();
                float scale = -100000.0f;

                // matrix 1
                Matrix matrix1 = new Matrix();
                matrix1.Scale(UPSCALE, UPSCALE);

                //pathOrg.Flatten();
                pathShape.Flatten(matrix1, this.Flatness);

                if (!Funtion.ConvertPathToPolygon(pathShape, scale, ref intPointListList))
                    return false;

                Clipper clipper = new Clipper(0);
                clipper.AddPaths(intPointListList, PolyType.ptSubject, true);

                bool bSuccess = clipper.Execute(ClipType.ctXor, intPointListList, PolyFillType.pftNonZero, PolyFillType.pftEvenOdd);

                if (!bSuccess)
                {
                    pathHatch = pathShape;
                    return false;
                }
                else
                {
                    List<List<IntPoint>> solution = new List<List<IntPoint>>();
                    ClipperOffset clipperOffset = new ClipperOffset(2.0, 0.25); // 1.0, 1.0
                    clipperOffset.AddPaths(intPointListList, JoinType.jtMiter, EndType.etClosedPolygon);

                    bool bRun = true;
                    int iLineCount = 0;

                    while (bRun)
                    {
                        iLineCount++;
                        double delta = ((double)this.Option.fOffset + (double)this.Option.fPitch * (double)iLineCount) * 10.0 * (double)scale;

                        clipperOffset.Execute(ref solution, delta);

                        if (solution.Count > 0)
                        {
                            pathHatch.StartFigure();

                            for (int index = 0; index < solution.Count; ++index)
                            {
                                PointF[] pointFarray = Funtion.ConvertPolygonToPointF(solution[index], scale);
                                pathHatch.AddPolygon(pointFarray);
                            }

                            pathHatch.CloseFigure();
                            solution.Clear();
                        }    
                        else
                        {
                            bRun = false;
                        }                
                    }

                    // matrix 2
                    Matrix matrix2 = new Matrix();
                    matrix2.Scale(1.0f / UPSCALE, 1.0f / UPSCALE);

                    pathHatch.Transform(matrix2);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Hatch shpae을 구성하는 Line segments를 만듭니다.
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        private bool MakeHatchSegment(GraphicsPath graphicsPath, ref GraphicsPath pHatch)
        {
            try
            {
                // Get Shape Boundary
                RectangleF rectBounds = graphicsPath.GetBounds();
                PointF ptStart = new PointF();
                PointF ptEnd = new PointF();
                float fPitch = this.Option.fPitch * UPSCALE;

                // Virtual Line
                double fStartH = Math.Round((double)(rectBounds.Bottom - rectBounds.Top), 4) - 0.0001f;
                int iSegmentCount = Convert.ToInt32(Math.Ceiling(fStartH / fPitch) );
                float fHOffset = (float)((fStartH - (iSegmentCount * fPitch)) / 2.0f);

                ptStart.X = rectBounds.Left - 10.0f;
								// smpark 
                //ptStart.Y = (rectBounds.Top - fHOffset)*-1;
								ptStart.Y = (rectBounds.Top - fHOffset);
                ptEnd.X = rectBounds.Right + 10.0f;
								//smpark
                //ptEnd.Y = rectBounds.Bottom*-1;
								ptEnd.Y = rectBounds.Bottom;
                List<List<PointF>> MainList = new List<List<PointF>>();

                for (int i = 0; i < iSegmentCount; ++i)
                {
                    List<PointF> pointFList = new List<PointF>();

                    MainList.Add(pointFList);
                }

                Parallel.For(0, iSegmentCount, (Action<int>)(index =>
                {
                    //PointF ptLineStart = new PointF(ptStart.X, ptStart.Y - (float)index * fPitch);
										PointF ptLineStart = new PointF(ptStart.X, ptStart.Y + (float)index * fPitch);
                    PointF PtLineEnd = new PointF(ptEnd.X, ptLineStart.Y);

                    MainList[index] = Funtion.GetCrossPoint(graphicsPath, ptLineStart, PtLineEnd);
                }));

                // graphicpath 로 변환
                for (int index = 0; index < iSegmentCount; ++index)
                {
                    List<PointF> pointFList = MainList[index];

                    // Zigzag 일 경우 짝수 경로 반전
                    if (this.Option.Type == HATCH_TYPE.HATCH_TYPE_ZIGZAG && index % 2 == 1)
                        pointFList.Reverse();

                    for (int j = 0; j < pointFList.Count; j++)
                    {
                        if (j % 2 == 0)
                        {
                            pHatch.StartFigure();

                            if (j < pointFList.Count - 1)
                                pHatch.AddLine(pointFList[j], pointFList[j+1]);
                        }
                    }
                    pHatch.CloseFigure();
                    pointFList.Clear();
                }
                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ConvertArcToPolyline()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsClosePath()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        #endregion

    }

}
