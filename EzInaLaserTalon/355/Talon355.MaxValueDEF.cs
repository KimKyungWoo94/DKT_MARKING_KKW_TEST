using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Laser.Talon
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Talon355
    {
        public static readonly double MIN_REPETITION_REATE=0;
        public static readonly double MAX_REPETITION_REATE=2000000;

        public static readonly double MIN_DIODE_CURRENT=0.0;
        
        public static readonly double MIN_EPRF= 50000;
        public static readonly double MAX_EPRF= 500000;
         
        public static readonly  int   MIN_SHG_TEMP_REG_POINT=20000;
        public static readonly  int   MAX_SHG_TEMP_REG_POINT=65535;

        public static readonly  int   MIN_THG_TEMP_REG_POINT=10000;
        public static readonly  int   MAX_THG_TEMP_REG_POINT=65535;

        public static readonly int    MIN_THG_CRYITAL_POS=1;
        public static readonly int    MAX_THG_CRYITAL_POS=15;

        static Dictionary<int,string> m_DicSystemHistorystr=new Dictionary<int, string>();
    }
}

