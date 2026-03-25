using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace EzCAM_Ver2
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public class Segment : ICloneable<Segment>//,IDisposable
    {
        public Segment_Type m_Type;               ///< Factor Type
        public Segment_Pattern m_Pattern;            ///< Pattern

        public PointF fStart;                   //< 시작지점 X, Y
        public PointF fEnd;                     //< 종료지점 X, Y
        public PointF fCenter;                  //< Circle 중심점 X, Y
        

        public PointF fBackStart;
        public PointF fBackEnd;
        public PointF fBackCenter;

        public float fRadius;                   //< Circle 반지름
        public float fStartAngle;               //< Circle 시작 Angle
        public float fEndAngle;                 //< Circle End Angle
        public float fSweepAngle;               //< Circle 스왑 Angle
        public bool bCCWDir;                    //< Circle CCW 방향
        public bool bSelected;                  //< Circle 선택
        public bool bDisable;                   //< Disable 선택. 도면에서 없는 데이타. 저장 할때 제거 됨
        public bool bMarking;                   //< 가공 여부 선택
        public int nShapeIndex;                 //< Shape Index
        public int nLayerIndex;                 //< Layer Index
        public bool bConnected;                 //< 다른 Shape 과 연결 유무 
        public int nGroupIndex;                 // < 연결 기준으로 GroupIndex;
        public string LayserName;
        //private bool IsDisposed = false;
        public RectangleF rectBoundary;

        [CategoryAttribute("Geometry")]
        [Browsable(false)]
        [DescriptionAttribute("Diameter of Arc (mm)")]
        public float fDiameter
        {
            get
            {
                return (float)(fRadius * 2.0);
            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("Start Position (mm)")]
        public PointF Start
        {
            get
            {
                PointF temp = new PointF();
                temp.X = (float)Math.Round((double)this.fStart.X, 3);
                temp.Y = (float)Math.Round((double)this.fStart.Y, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("End Position (mm)")]
        public PointF End
        {
            get
            {
                PointF temp = new PointF();
                temp.X = (float)Math.Round((double)this.fEnd.X, 3);
                temp.Y = (float)Math.Round((double)this.fEnd.Y, 3);
                return temp;
            }
            set
            {
           
            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("Center Position (mm)")]
        public PointF Center
        {
            get
            {
                PointF temp = new PointF();
                temp.X = (float)Math.Round((double)this.fCenter.X, 3);
                temp.Y = (float)Math.Round((double)this.fCenter.Y, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("Radius of Arc (mm)")]
        public float Radius
        {
            get
            {
                float temp;
                temp = (float)Math.Round((double)this.fRadius, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("Start Angle of Arc (degree)")]
        public float StartAngle
        {
            get
            {
                float temp;
                temp = (float)Math.Round((double)this.fStartAngle, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("End Angle of Arc (degree)")]
        public float EndAngle
        {
            get
            {
                float temp;
                temp = (float)Math.Round((double)this.fEndAngle, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("Sweep Angle of Arc (degree)")]
        public float SweepAngle
        {
            get
            {
                float temp;
                temp = (float)Math.Round((double)this.fSweepAngle, 3);
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Geometry")]
        [Browsable(true)]
        [DescriptionAttribute("CCW Direction of Arc")]
        public bool CCW
        {
            get
            {
                return this.bCCWDir;
            }
            set
            {
                
            }
        }

        

        [CategoryAttribute("Boundary")]
        [Browsable(true)]
        [DescriptionAttribute("Right, Bottom Position of Segment")]
        public PointF RightBottom
        {
            get
            {
                PointF temp = new PointF();
                temp.X = this.rectBoundary.Right;
                temp.Y = this.rectBoundary.Bottom;
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Boundary")]
        [Browsable(true)]
        [DescriptionAttribute("Left, Top Position of Segment")]
        public PointF LeftTop
        {
            get
            {
                PointF temp = new PointF();
                temp.X = this.rectBoundary.Left;
                temp.Y = this.rectBoundary.Top;
                return temp;
            }
            set
            {

            }
        }

        ~Segment()
        {
            //Dispose(false);
        }
        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);

        }
        //public virtual void Dispose(bool a_Disposeing)
        //{
        //    if (IsDisposed)
        //        return;

        //    if (a_Disposeing)
        //    {

        //    }
        //    IsDisposed = true;
        //}

        public void Clear()
        {
            m_Type = Segment_Type.CAM_TYPE_NON_PROCESS;
            m_Pattern = Segment_Pattern.CAM_JUMP;
            fStart.X = 0.0f; fStart.Y = 0.0f;
            fEnd.X = 0.0f; fEnd.Y = 0.0f;
            fCenter.X = 0.0f; fCenter.Y = 0.0f;
            fRadius = 0.0f;
            fStartAngle = 0.0f;
            fEndAngle = 0.0f;
            fSweepAngle = 0.0f;
            bCCWDir = false;
            bSelected = false;
            bDisable = false;
            bMarking = false;
            nShapeIndex = -1;
            nLayerIndex = -1;
            bConnected = false;
            nGroupIndex = -1;

            this.rectBoundary.X = .0f;
            this.rectBoundary.Y = .0f;
            this.rectBoundary.Width = .0f;
            this.rectBoundary.Height = .0f;
        }

        public void SetStartPos(float fx, float fy)
        {
            fStart.X = fx;
            fStart.Y = fy;

            // 추가
            //this.rect.X = fStart.X;
            //this.rect.Y = fStart.Y;
        }
        public void SetEndPos(float fx, float fy)
        {
            fEnd.X = fx;
            fEnd.Y = fy;

            // 추가
            //this.rect.Width = Math.Abs(fx - this.rect.X);
            //this.rect.Height = Math.Abs(fy - this.rect.Y
        }

        public void SetCenterPos(float fx, float fy)
        {
            fCenter.X = fx;
            fCenter.Y = fy;
        }

        //


        /// <summary>
        /// Set Boundary, Rectangle, 
        /// KTY
        /// </summary>
        /// <returns></returns>
        public bool SetBoundary()
        {
            if (this.fDiameter < 0.0001)
                return false;

            this.rectBoundary.X = (float)Math.Round((double)(this.fCenter.X - this.fRadius), 3);
            this.rectBoundary.Y = (float)Math.Round((double)(this.fCenter.Y - this.fRadius), 3);
            this.rectBoundary.Width = (float)Math.Round((double)(this.rectBoundary.Height = this.fDiameter), 3);

            return true;
        }


        /// 

        public void SetTypeAndPattern(Segment_Type nShape_Type, Segment_Pattern nShape_Kind)
        {
            m_Type = nShape_Type;
            m_Pattern = nShape_Kind;
        }

        public void Revese_Coordinate_Y()
        {
            // calculate Y - Reverse

            fStart.Y = -fStart.Y;
            fEnd.Y = -fEnd.Y;
            fCenter.Y = -fCenter.Y;
        }


        public Segment Clone()
        {
            return new Segment
            {
                m_Type = this.m_Type,
                m_Pattern = this.m_Pattern,

                fStart = this.fStart,
                fEnd = this.fEnd,
                fCenter = this.fCenter,

                fRadius = this.fRadius,
                fStartAngle = this.fStartAngle,
                fEndAngle = this.fEndAngle,
                fSweepAngle = this.fSweepAngle,

                bCCWDir = this.bCCWDir,
                bSelected = this.bSelected,
                bDisable = this.bDisable,
                bMarking = this.bMarking,
                nShapeIndex = this.nShapeIndex,
                nLayerIndex = this.nLayerIndex,
                bConnected = this.bConnected,
                nGroupIndex = this.nGroupIndex,
            };
        }
        public void swap(ref Segment data)
        {
            
        }

        public void TransForm_CW()
        {

            PointF Data = new PointF(this.fStart.X, this.fStart.Y);

            //this.fStart.Y = -this.fEnd.X;
            //this.fStart.X = -this.fEnd.Y;
            //this.fEnd.X = -Data.Y;
            //this.fEnd.Y = -Data.X;
            ////this.fCenter =

            ////this.fStart.Y *= -1.0f;
            ////this.fEnd.Y *= -1.0f;
            ////this.fCenter.Y *= -1.0f;

            //this.fStart.X = this.fStart.X * cos()
            float angle = 90.0f;
            float fTempX = this.fStart.X;
            float fTempY = this.fStart.Y;


            this.fStart.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempY * Math.Sin((angle) * Math.PI / 180.0f));
            this.fStart.Y = (float)(fTempX * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));


            fTempX = this.fEnd.X;
            fTempY = this.fEnd.Y;


            this.fEnd.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempY * Math.Sin((angle) * Math.PI / 180.0f));
            this.fEnd.Y = (float)(fTempX * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));

            if ( this.m_Pattern == Segment_Pattern.CAM_PATTERN_ARC || this.m_Pattern == Segment_Pattern.CAM_PATTERN_CIRCLE)
            {

                fTempX = this.fCenter.X;
                fTempY = this.fCenter.Y;

                this.fCenter.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempX * Math.Sin((angle) * Math.PI / 180.0f));
                this.fCenter.Y = (float)(fTempY * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));
            }

        }

        public void TransForm_CCW()
        {
            float angle = -90.0f;
            float fTempX = this.fStart.X;
            float fTempY = this.fStart.Y;


            this.fStart.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempY * Math.Sin((angle) * Math.PI / 180.0f));
            this.fStart.Y = (float)(fTempX * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));


            fTempX = this.fEnd.X;
            fTempY = this.fEnd.Y;


            this.fEnd.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempY * Math.Sin((angle) * Math.PI / 180.0f));
            this.fEnd.Y = (float)(fTempX * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));

            if (this.m_Pattern == Segment_Pattern.CAM_PATTERN_ARC || this.m_Pattern == Segment_Pattern.CAM_PATTERN_CIRCLE)
            {

                fTempX = this.fCenter.X;
                fTempY = this.fCenter.Y;

                this.fCenter.X = (float)(fTempX * Math.Cos((angle) * Math.PI / 180.0f) - fTempX * Math.Sin((angle) * Math.PI / 180.0f));
                this.fCenter.Y = (float)(fTempY * Math.Sin((angle) * Math.PI / 180.0f) + fTempY * Math.Cos((angle) * Math.PI / 180.0f));
            }
        }
        public void TransForm_Mirror()
        {
            this.fStart.X *= -1.0f;
            this.fEnd.X *= -1.0f;
            this.fCenter.X *= -1.0f;

            // Angle 변환 추가
            this.fStartAngle = -(this.fStartAngle - 180.0f);
            this.fEndAngle = -(this.fEndAngle - 180.0f);
            this.bCCWDir = !this.bCCWDir;
            this.fSweepAngle = -this.fSweepAngle;
        }

        public void TransForm_Flip()
        {
            this.fStart.Y *= -1.0f;
            this.fEnd.Y *= -1.0f;
            this.fCenter.Y *= -1.0f;

            // Angle 변환 추가
            this.fStartAngle = -this.fStartAngle;
            this.fEndAngle = -this.fEndAngle;
            this.bCCWDir = !this.bCCWDir;
            this.fSweepAngle = -this.fSweepAngle;
        }

        public void TransForm_Manual_Move(float fx, float fy)
        {
            this.fStart.X = this.fBackStart.X + fx;
            this.fStart.Y = this.fBackStart.Y + fy;

            this.fEnd.X = this.fBackEnd.X + fx;
            this.fEnd.Y = this.fBackEnd.Y + fy;

            this.fCenter.X = this.fBackCenter.X + fx;
            this.fCenter.Y = this.fBackCenter.Y + fy;


        }

        public void TransForm_Manual_Value(float fx, float fy)
        {
            this.fStart.X += fx;
            this.fEnd.X += fx;
            this.fCenter.X += fx;

            this.fStart.Y += fy;
            this.fEnd.Y += fy;
            this.fCenter.Y += fy;

        }

        public void TransForm_ReverseDirection()
        {
         
            PointF ptTemp;
            ptTemp = this.fEnd;
            this.fEnd = this.fStart;
            this.fStart = ptTemp;

            float fTempAngle;
            fTempAngle = this.fEndAngle;
            this.fEndAngle = this.fStartAngle;
            this.fStartAngle = fTempAngle;

            this.fSweepAngle = this.fSweepAngle * -1;

            this.bCCWDir = !this.bCCWDir;   
          
                      

        }


        public void Position_Backup()
        {
            this.fBackCenter = this.fCenter;
            this.fBackStart = this.fStart;
            this.fBackEnd = this.fEnd;
        }

        
    }
}
