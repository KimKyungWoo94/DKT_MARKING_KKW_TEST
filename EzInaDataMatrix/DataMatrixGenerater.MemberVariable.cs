using EzCAM_Ver2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintNet;
namespace EzIna.DataMatrix
{
    public sealed partial class DMGenerater
    {
        private Symbology m_SymbolID = Symbology.DataMatrix;
        private ZintNetLib m_BarCodeLib = null;
        private DataMatrixSize m_DatamatrixSize=DataMatrixSize.DM20X20;
        public eDataMatrixSize DatamatrixSize
        {
            get { return (eDataMatrixSize)(int)m_DatamatrixSize;}
            set { m_DatamatrixSize=(DataMatrixSize)(int)value;}
        }
        private Hatch_Option m_HatchOption;
        public Hatch_Option HatchOption
        {
            get { return m_HatchOption;}
            set { m_HatchOption=value;}
        }
        private SizeF m_MarkingRealSize;
        public float RealWidth
        {
            get { return m_MarkingRealSize.Width;}
            set
            {
                m_MarkingRealSize.Width=value;
            }
        }
        public float RealHeight
        {
            get { return m_MarkingRealSize.Height; }
            set
            {
                m_MarkingRealSize.Height = value;
            }
        }


    }
}
