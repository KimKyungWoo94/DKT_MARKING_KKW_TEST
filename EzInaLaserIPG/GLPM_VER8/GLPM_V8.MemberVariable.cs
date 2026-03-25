using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Laser.IPG
{
    public sealed partial class GLPM_V8 
    {
        bool                                IsDisposed = false;
        bool                                IsDisposing = false;
        bool                                m_bLoopEnable = false;
        bool                                m_bHandShakeSuccess=false;
        Thread                              m_pLoopThread;
        StringBuilder                       m_MakePacketStringBuilder=new StringBuilder();
        StringBuilder                       m_RecvDataStringBuilder=new StringBuilder();
        string                              m_strConfigSection;
        string                              m_strFirmwareVersion;
        EzIna.Commucation.BitField32Helper  m_Status=new Commucation.BitField32Helper();
        TimeSpan                            m_WarmUpRemain=new TimeSpan(0,0,0);
        double                              m_fINT_Trigger_FREQ;
        double                              m_fEXT_Trigger_FREQ;
        int                                 m_iMinTriggerFREQ;
        int                                 m_iMaxTriggerFREQ;
        float                               m_fCaseTemp;
        float                               m_fHeadTemp;
        float                               m_fLowerDeckTemp;
        float                               m_fActualCrystalTemp;         
        float                               m_fSetPointCrystalTemp;
        float                               m_fSetPoint;
        long                                m_iEmissionTime; // minute
        /// <summary>
        /// false : EXT ,true =INT
        /// </summary>
        bool                                m_bEmissionCtrl;
        /// <summary>
        /// false : EXT ,true =INT
        /// </summary>
        bool                                m_bPowerSourceCtrl;
    }
}
