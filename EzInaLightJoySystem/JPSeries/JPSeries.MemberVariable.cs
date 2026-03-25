using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Light.JoySystem
{
    public sealed partial class JPSeries
    {

        bool            IsDisposed = false;
        bool            IsDisposing = false;
        bool            m_bHandShakeSuccess=false;
        bool            m_bLoopEnable = false;
        Thread          m_pLoopThread;
        string          m_strConfigSection;
        StringBuilder   m_RecvDataStringBuilder = new StringBuilder();
		    Dictionary<int,int> m_DicIntensity=new Dictionary<int,int>();
		
    }
}

