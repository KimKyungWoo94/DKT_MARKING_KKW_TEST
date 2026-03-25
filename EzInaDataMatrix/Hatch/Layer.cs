using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EzCAM_Ver2
{
    public struct St_Decoder
    {
        public int nDecoder { get; set; }
        public int nIndex;
        public bool bType_Circle;
        public float fSize;
        public string SName;

        public St_Decoder(bool _bTypeCircle, int _nDecoder, float _fSize, int _nIndex, string name = "")
        {
            nDecoder = _nDecoder;
            nIndex = _nIndex;
            fSize = _fSize;
            bType_Circle = _bTypeCircle;
            SName = name;
        }
    }

    public class Layer:IDisposable
    {
        public string m_LayerName = "";
        public float m_ToolSize = 0.0f;
        public int m_NumbOfShape = 0;
        public int m_NumOfSegment = 0;
        public int m_LayserIndex;
        public List<Shapes> m_ShapesList = new List<Shapes>();
        private bool IsDisposed = false;

        public Hatch_Option HatchOption;


        [CategoryAttribute("Layer")]
        [Browsable(true)]
        [DisplayName("Name")]
        //[DescriptionAttribute("Hatch Type")]
        public string Name { get { return this.m_LayerName; } set { this.m_LayerName = value; } }

        [CategoryAttribute("Layer")]
        [Browsable(true)]
        [DisplayName("Index")]
        [DescriptionAttribute("Layer Index")]
        public int Index { get { return this.m_LayserIndex; } set { this.m_LayserIndex = value; } }

        [CategoryAttribute("Layer")]
        [Browsable(true)]
        [DisplayName("Tool Size")]
        [DescriptionAttribute("Layer Tool Size")]
        public float ToolSize { get { return this.m_ToolSize; } set { this.m_ToolSize = value; } }

        [CategoryAttribute("Layer")]
        [Browsable(true)]
        [DisplayName("Shapes")]
        [DescriptionAttribute("Number of Shapes")]
        public int NumOfShape { get { return this.m_NumbOfShape; } set { this.m_NumbOfShape = value; } }

        [CategoryAttribute("Layer")]
        [Browsable(true)]
        [DisplayName("Segments")]
        [DescriptionAttribute("Number of Segments")]
        public int NumOfSegments { get { return this.m_NumOfSegment; } set { this.m_NumOfSegment = value; } }


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


        public Layer()
        {
            Clear();
        }

        ~Layer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool a_Disposing)
        {
            if (IsDisposed)
                return;
            if(a_Disposing)
            {
                // Disposing Memory Work                 
                foreach(Shapes item in m_ShapesList)
                {
                    // Dispose
                    item.Dispose();
                }
                m_ShapesList.Clear();                
            }
            IsDisposed = true;
        }

        public void Clear()
        {
            m_LayerName = "";
            m_ToolSize = 0.0f;
            m_NumbOfShape = 0;
            m_NumOfSegment = 0;
            m_LayserIndex = 0;

            this.HatchOption.Clear();
        }

        public bool Add_Shape(int Shape_Index, Segment nSegment)
        {
            int nShapeCnt = m_ShapesList.Count;
            if (nShapeCnt < Shape_Index+1)
            {
                for (int i = nShapeCnt; i < nShapeCnt + 1; i++)
                {
                    Shapes NewShape = new Shapes();
                    NewShape.strName = "shape " + Convert.ToString(Shape_Index);
                    m_ShapesList.Add(NewShape);

                    m_NumbOfShape++;
                }
               
            }
            // KTY 수정
            //m_ShapesList[Shape_Index].m_Segment.Add(nSegment);
            m_ShapesList[Shape_Index].AddSegment(nSegment);

            return true;
        }

        public void SelectLayer()
        {
            foreach (Shapes data in m_ShapesList)
            {
                data.SelectAllShape(true);
            }

        }

      
    }

}
