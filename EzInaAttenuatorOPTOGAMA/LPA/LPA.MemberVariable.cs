using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EzIna.Commucation;
namespace EzIna.Attenuator.OPTOGAMA
{
    public sealed partial class LPA
    {
        string                  m_strConfigSection;
        bool                    IsDisposed = false;
        bool                    IsDisposing = false;
        bool                    m_bLoopEnable = false;
        bool                    m_bHandShakeSuccess=false;
        Thread                  m_pLoopThread;
        StringBuilder           m_MakePacketStringBuilder=new StringBuilder();
        StringBuilder           m_RecvDataStringBuilder=new StringBuilder();
        BitField32Helper        m_Status=new BitField32Helper();
        double                  m_fPowerPercent;
        double                  m_fMinPower;
        double                  m_fMaxPower;
        double                  m_fAngle;
        double                  m_fPosition;
        bool                    m_bMotorEnable;
        bool                    m_bAutoHomeEnable;
        string                  m_strSerialNumber;
        string                  m_strFirmwareVersion;
        int                     m_iWaveLength;
        HomeOption              m_enumHomeOption;
                             
    }
}
