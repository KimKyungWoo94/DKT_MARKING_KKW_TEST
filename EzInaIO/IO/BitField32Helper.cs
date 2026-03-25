using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO
{
    public enum bit:int
    {
        B00,
        B01,
        B02,
        B03,
        B04,
        B05,
        B06,
        B07,
        B08,
        B09,
        B10,
        B11,
        B12,
        B13,
        B14,
        B15,
        B16,
        B17,
        B18,
        B19,
        B20,
        B21,
        B22,
        B23,
        B24,
        B25,
        B26,
        B27,
        B28,
        B29,
        B30,
        B31
    }
    public class BitField32Helper
    {
        private UInt32 m_32BitValue;
        public BitField32Helper(UInt32 a_value=0)
        {
            m_32BitValue=a_value;
        }
        public bool this[bit a_IDX]
        {
            get
            {
                return (((m_32BitValue >> (int)a_IDX) & 1)==1);
            }
            set
            {
                bitSet((int)a_IDX,value);
            }
        }
        public uint Data
        {
            get { return m_32BitValue;}
            set { m_32BitValue=value;}
        }
           
        private void bitSet(int a_bit,bool a_value)
        {
           if(a_value==true)
           {
                m_32BitValue|=(uint)(1<<a_bit);
           }
           else
           {
                m_32BitValue&=((uint)(~(1<<a_bit)));
           }             
        }
    }
}
