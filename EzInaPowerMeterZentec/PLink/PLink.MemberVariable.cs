using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.PowerMeter.Zentec
{
    public sealed partial class PLink
    {
        bool            IsDisposed = false;
        bool            IsDisposing = false;
        bool            m_bLoopEnable=false;
        Thread          m_pLoopThread;
    }
}
