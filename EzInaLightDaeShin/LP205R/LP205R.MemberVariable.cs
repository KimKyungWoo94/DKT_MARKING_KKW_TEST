using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Light.DaeShin
{
    public sealed partial class LP205R
    {
   
        bool IsDisposed = false;
        bool IsDisposing = false;
        bool m_bLoopEnable = false;
        Thread m_pLoopThread;
        private string m_strConfigSection;
       
        int [] m_iIntensity;
    }
}
