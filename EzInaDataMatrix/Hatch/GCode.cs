using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EzCAM_Ver2
{
    class GCode
    {
        GraphicsFunction Function;

        /// <summary>
        /// X 방향 반전 여부
        /// </summary>
        public bool ReverseX { get; set; }
        /// <summary>
        /// Y 방향 반전 여부
        /// </summary>
        public bool ReverseY { get; set; }
        /// <summary>
        /// First Axis Name, X Direction
        /// </summary>
        public string AxisXName { get; set; }
        /// <summary>
        /// Second Axis Name, Y Direction
        /// </summary>
        public string AxisYName { get; set; }

        public GCode()
        {
            this.Function = new GraphicsFunction();
            this.ReverseX = false;
            this.ReverseY = false;
            this.AxisXName = "RA";
            this.AxisYName = "RB";
        }

        /// <summary>
        /// Generate G-Code
        /// </summary>
        /// <param name="path"></param>
        /// <param name="GCodeList"></param>
        /// <returns></returns>
        public bool Generate(GraphicsPath path, ref List<string>GCodeList)
        {
            if (this.MakeCode(path, ref GCodeList))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Convert GraphicsPath To G-Code 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="GCodeList"></param>
        /// <returns></returns>
        private bool MakeCode(GraphicsPath path, ref List<string> GCodeList)
        {
            try
            {
                PointF ptStart = new PointF();
                PointF ptBefore = new PointF();
                //PointF ptPoint = new PointF();

                int index = 0;
                int iPathCount = path.PointCount;

                PointF[] Points = path.PathPoints;
                byte[] Types = path.PathTypes;

                System.Drawing.Drawing2D.PathPointType pType;
                foreach (PointF ptPoint in Points)
                {
                    pType=( System.Drawing.Drawing2D.PathPointType)Types[index];
                    switch (pType)
                    {
                        case PathPointType.Start:
                            ptStart = ptPoint;
                            ptBefore = ptStart;

                            GCodeList.Add(this.AddJump(ptPoint));
                            break;
                        case PathPointType.Line:
                            ptBefore.X = ptPoint.X;
                            ptBefore.Y = ptPoint.Y;

                            GCodeList.Add(this.AddLine(ptPoint));
                            break;
                        case PathPointType.Bezier:
                     ///case PathPointType.Bezier3                            
                            ptBefore.X = ptPoint.X;
                            ptBefore.Y = ptPoint.Y;
                            GCodeList.Add(this.AddLine(ptPoint));
                            break;
                        case PathPointType.PathTypeMask:
                            break;
                        case PathPointType.DashMode:
                            break;
                        case PathPointType.PathMarker:
                            break;
                        case PathPointType.CloseSubpath:
                        case (PathPointType)129:
                            ptBefore.X = ptPoint.X;
                            ptBefore.Y = ptPoint.Y;
                            GCodeList.Add(this.AddLine(ptPoint));
                            break;
                        default:
                            break;
                    }               
                    index++;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// return Jump G-Code
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string AddJump(PointF point)
        {
            string strPoint = string.Format("G0 {0}{1} {2}{3}", this.AxisXName, point.X, this.AxisYName, point.Y);

            return strPoint;
        }

        /// <summary>
        /// return Line G-Code
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string AddLine(PointF point)
        {
            string strPoint = string.Format("G1 {0}{1} {2}{3}", this.AxisXName, point.X, this.AxisYName, point.Y);

            return strPoint;
        }

        private string AddArc(PointF Start, PointF End)
        {
            string strPoint = "";

            return strPoint;
        }

        private string AddCircle(PointF Start, PointF End)
        {
            string strPoint = "";

            return strPoint;
        }
    }
}
