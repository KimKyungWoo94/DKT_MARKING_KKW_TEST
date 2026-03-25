using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Laser.Talon
{
    public sealed partial class Talon355
    {
                
        bool                    IsDisposed = false;
        bool                    IsDisposing = false;
        bool                    m_bLoopEnable = false;
        bool                    m_bHandShakeSuccess=false;
        Thread                  m_pLoopThread;
        string                  m_strPacketEndSpliter="\n";
        string                  m_strConfigSection;
        StringBuilder           m_MakePacketStringBuilder=new StringBuilder();
        double                  m_fDiodeCurrent;   
				double									m_fSetDiodeCurrent;   
        double                  m_fEPRF;     
        bool                    m_bShutterEnableLastSet;      
        bool                    m_bEXTGateControlEnable;
        double                  m_fTHGCrstalSpotHours;
        int                     m_iTHGCrystalSpotPos;//1~15 , -99 : Moving
        
        int                     m_iRepetitionRate;
        double                  m_fLaserEmittedPower;//W
        int                     m_iSHGTempRegulationPoint; // 20000~65535
        int                     m_iSHGCrystalTempLastSet;
        
        Commucation.BitField32Helper m_SystemStatus=new Commucation.BitField32Helper();
        double                  m_fDiodeTemp;
        
        int                     m_iTHGOvenTemp;
        int                     m_iTHGOvenTempLastSet;
        double                  m_fTowerTemp;
        bool                    m_bShutterExist;
        TimeSpan                m_WarmupRemainTime;
        enumGateEnableLevel     m_GateEnableSignalLevel;
        enumTriggerEdgeLevel    m_TriggerEdgeSignalLevel;
        int[]m_iStatusHistory=new int[16];
        double m_fDiodeCalibrationCurrent;
        double m_fReCalibratePowerMonitor;
        double m_fChassisTemp;
        double m_fDiodeCurrentHeadRoom;
        double m_fDiodeCurrentLimit;
        double m_fDiodeCurrentLastSet;
        double m_fDiodeHours;        
        double m_fLaserHeadHours;
        string m_strDiodeSerialNum="";
        string m_strManufacturer="";
        string m_strModel="";
        string m_strSerialNum="";
        string m_strVersion="";
        string m_strSystemStatus="";
        List<double> m_THGSpotHoursList=new List<double>();
        StringBuilder m_RecvDataStringBuilder=new StringBuilder();
       
    }
}
