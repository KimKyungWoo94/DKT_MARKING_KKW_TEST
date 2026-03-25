using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.PowerMeter.Ohpir
{
    public sealed partial class SPC
    {
        public enum enumWaveLength
        {
            /// <summary>
            /// 515nm
            /// </summary>
            VIS=1,
            /// <summary>
            /// 1064nm
            /// </summary>
            YAG=2,
            /// <summary>
            /// 10600nm
            /// </summary>
            CO2=3,
        }
        public enum enumPowerScale
        {
            _150W=0,
            _50W=1,
            _5W=2,
        }
        public enum enumMainHeadSetting
        {
            _50Hz=1,
            _60Hz=2,
        }       
    }
}
