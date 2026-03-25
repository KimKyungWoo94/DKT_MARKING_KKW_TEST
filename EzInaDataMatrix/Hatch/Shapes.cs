using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EzCAM_Ver2
{
    #region enum 
    public enum Shape_Type
    {
        CAM_SHAPE_NONE = 0,
        CAM_SHAPE_HATCH,
    }

    public enum Segment_Type
    {
        CAM_TYPE_PROCESS = 0,
        CAM_TYPE_NON_PROCESS,
        CAM_TYPE_ALIGN,
        CAM_TYPE_DEMENSION
    };

    public enum Segment_Pattern
    {
        CAM_JUMP = 0,
        PCAM_POINT,
        CAM_PATTERN_LINE,
        CAM_PATTERN_ARC,
        CAM_PATTERN_CIRCLE,
        CAM_PATTERN_RECTANGLE,
        CAM_PATTERN_POLYLINE,
        CAM_PATTERN_COUNT
    };

    #endregion

    public class Shapes:IDisposable
    {
        public List<Segment> m_Segment = new List<Segment>();
       // public Rectangle m_LinkSize = new Rectangle();

        public bool bClose;                // Shape 구성 안에 Segment 가 폐곡선 
        public bool bPathDone;
        public bool bPathOpt;
        public string strName { get; set; }

        public RectangleF Size = new RectangleF();
        public float Size_CenterX;
        public float Size_CenterY;
        //private bool IsDisposed = false;

        public Hatch_Option HatchOption; 





        [CategoryAttribute("Shape")]
        [Browsable(true)]
        [DisplayName("Type")]
        [DescriptionAttribute("Shape Type")]
        public Shape_Type ShpaeType { get; set; }

        [CategoryAttribute("Shape")]
        [Browsable(true)]
        [DisplayName("Segment")]
        [DescriptionAttribute("Number of Segment in Shape")]
        public Int32 iSegmentCount
        {
            get
            {
                return this.m_Segment.Count;
            }
        }

        [CategoryAttribute("Shape")]
        [Browsable(true)]
        [DescriptionAttribute("Center Position of Shape Boundary")]
        public PointF Center
        {
            get
            {
                PointF temp = new PointF();
                temp.X = this.Size_CenterX;
                temp.Y = this.Size_CenterY;
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Shape")]
        [Browsable(true)]
        [DescriptionAttribute("Right, Bottom Position of Shape Boundary")]
        public PointF RightBottom
        {
            get
            {
                PointF temp = new PointF();
                temp.X = this.Size.Right;
                temp.Y = this.Size.Bottom;
                return temp;
            }
            set
            {

            }
        }

        [CategoryAttribute("Shape")]
        [Browsable(true)]
        [DescriptionAttribute("Left, Top Position of Shape Boundary")]
        public PointF LeftTop
        {
            get
            {
                PointF temp = new PointF();
                temp.X = this.Size.Left;
                temp.Y = this.Size.Top;
                return temp;
            }
            set
            {

            }
        }


        [CategoryAttribute("Hatch")]
        [Browsable(true)]
        [DisplayName("Type")]
        [DescriptionAttribute("Hatch Type")]
        public HATCH_TYPE Type { get { return this.HatchOption.Type; } set { this.HatchOption.Type = value; } }

        [CategoryAttribute("Hatch")]
        [Browsable(true)]
        [DisplayName("Line Angle")]
        [DescriptionAttribute("Hatch Line Angle, degree")]
        public float Angle { get { return this.HatchOption.fAngle; } set { this.HatchOption.fAngle = value; } }

        [CategoryAttribute("Hatch")]
        [Browsable(true)]
        [DisplayName("Line Pitch")]
        [DescriptionAttribute("Hatch Line Pitch, mm")]
        public float Pitch { get { return this.HatchOption.fPitch; } set { this.HatchOption.fPitch = value; } }

        [CategoryAttribute("Hatch")]
        [Browsable(true)]
        [DisplayName("Offset")]
        [DescriptionAttribute("Offset between outline and hatch, mm")]
        public float Offset { get { return this.HatchOption.fOffset; } set { this.HatchOption.fOffset = value; } }

        [CategoryAttribute("Hatch")]
        [Browsable(false)]
        public float Flatness { get { return this.HatchOption.fFlatness; } set { this.HatchOption.fFlatness = value; } }



        //public void Swap_array( ref Shapes source)
        //{
        //    List<Segment> copy = source.m_Segment.ToList();
        //    source.m_Segment.Clear();
        //    source.m_Segment = this.m_Segment.ToList();
        //    this.m_Segment = copy.ToList();
        //    copy.Clear();

        //}

        public Shapes()
        {
            this.Clear();
        }
        ~Shapes()
        {
            //Dispose(false);
        }
        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
        //public virtual void Dispose(bool a_Disposing)
        //{
        //    if (IsDisposed)
        //        return;
        //    if(a_Disposing)
        //    {
        //        foreach(Segment item in m_Segment)
        //        {
        //            item.Dispose();
        //        }
        //        m_Segment.Clear();
        //    }
        //    IsDisposed = true;
        //}

        void Clear()
        {
            this.strName = "";
            bClose = bPathDone = false;
            this.ShpaeType = Shape_Type.CAM_SHAPE_NONE;
            this.HatchOption.Clear();     
        }

        public void SelectAllShape(bool bSelect)
        {
            foreach (Segment data in m_Segment)
            {
                data.bSelected = bSelect;
            }
        }

        public void Swap_Shape(ref Shapes data)
        {
            // 백업 
            List<Segment> copy = data.m_Segment.ToList();
            RectangleF copySize = new RectangleF();
            float Cx = data.Size_CenterX;
            float Cy = data.Size_CenterY;
            bool bClose = data.bClose;
            bool bPathDone = data.bPathDone;
            bool bPathOpt = data.bPathOpt;

            copySize = data.Size;
            data.m_Segment.Clear();

            // swap 
            data.m_Segment = this.m_Segment.ToList();
            data.Size = this.Size;
            data.Size_CenterX = this.Size_CenterX;
            data.Size_CenterY = this.Size_CenterY;

            this.m_Segment = copy.ToList();
            this.Size = copySize;
            this.Size_CenterX = Cx;
            this.Size_CenterY = Cy;

            this.bClose = bClose;
            this.bPathDone = bPathDone;
            this.bPathOpt = bPathOpt;


            // 사용 데이타 Clear
            copy.Clear();
        }

        /// <summary>
        /// Shape 에 Segment 추가. Close Shape 여부 확인.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public bool AddSegment(Segment segment)
        {
            try
            {
                this.m_Segment.Add(segment);

                if (this.m_Segment.Count < 2)
                {
                    Segment segment1 = this.m_Segment[0];

                    if (Math.Abs(segment1.End.X - segment1.Start.X) < 0.0001 && Math.Abs(segment1.End.Y - segment1.Start.Y) < 0.0001)
                        this.bClose = true;
                    else
                        this.bClose = false;
                }
                else
                {
                    Segment segment1 = this.m_Segment[0];
                    Segment segment2 = this.m_Segment[this.m_Segment.Count - 1];

                    if (Math.Abs(segment2.End.X - segment1.Start.X) < 0.0001 && Math.Abs(segment2.End.Y - segment1.Start.Y) < 0.0001)
                        this.bClose = true;
                    else
                        this.bClose = false;
                }

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("AddSegment() : " + ex.Message.ToString());
                return false;
            }
            
        }
       
    }

}
