using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Laser.Lumentum
{
    /// <summary>
    ///  LaserPicoBlade2 Partial Member Variable
    /// </summary>
    public sealed partial class PicoBlade2
    {
       
        readonly string CMDSplitterParttern = @"([a-zA-z0-9][a-zA-z0-90-9])([a-zA-z0-9][a-zA-z0-9])";
        readonly string RecvParttern = @"^[""*""](.*?)[""#""]$";
        readonly string PasingEchoDataParttern = @"([a-zA-z0-9][a-zA-z0-90-9])([a-zA-z0-9][a-zA-z0-9])([+-]?\d{5,6})";
        bool IsDisposed = false;
        bool IsDisposing = false;
        bool m_bLoopEnable = false;
        bool m_bHandShakeSuccess=false;
        Thread m_pLoopThread;    
        private string m_strConfigSection;
        private string m_strControllerFirmwareVER   ="";
        private string m_strControllerBootloaderVER ="";
        private string m_strControllerSN            ="";
        private string m_strLaerHeadSN              ="";

        private int m_iControllerTotalOnTime=0;
        private int m_iLaserDiode1TotalOnTime=0;
        private int m_iLaserDiode2otalOnTime=0;
        private int m_iLaserDiode3TotalOnTime=0;
        private int m_iLaserDiode4otalOnTime=0;
       
       
       
        private bool m_bEmissionEnable=false;

        private int m_iPRFDivider=0;  
        private double m_fPRF=0.0;
        private int m_iBurstSize=0;
        private enumBurstType m_enumBurstType=enumBurstType.FIX;
        private int m_iGainPulseFlexBurst;
        private bool m_bPoD_Enable;
        private bool m_bPoD_EXTGateEnable;
        private int  m_iPoD_Divider;
        private int  m_iPoD_OutputPowerSet;
        private bool m_bShutterOpen;
        private bool m_bPoD_LevelTriggerEnable;
        private bool m_bPoD_EdgeTriggerEnable;
        private bool m_bPoD_EXT_AnalogEnable;
        private enumPoDAnalogRange m_enumPoDAnalogRange;
        private bool m_bPRFoutPoDDividerEnable;
        private bool m_bPRFoutPoDEnable;
        private double m_fControllerTemp;
        private double m_fSeederPumpDiodeTemp;
        private double m_fSeederPumpDiodeTEC_Currnet;//uA
        private double m_fSeederPumpDiodeTEC_Voltage;//mV
        private double m_fPhotoDiodeMonitoringOSC_INTRA_POWERCurrent;//uA
        private double m_fPhotoDiodeMonitoringSeederPumpDiodeCurrent;//uA
        private double m_fAmp1DiodeTemp;
        private double m_fPhotoDiodeMonitoringAmp1FiberOutputCurrent;//uA
        private double m_fAmp2DiodeTemp;
        private double m_fPhotoDiodeMonitoringAmp2FiberOutputCurrent;//uA
        private double m_fAmplifierDiodeTemp;
        private double m_fPhotoDiodeMonitoringPAfiberOutputCurrent;//uA
        private double m_fPhotoDiodeMonitoringIRPowerAfterPoDCurrent;//uA
        private double m_fAD3BoardTemp;
        private double m_fOutputPower; //mW
        private BitVector32 m_FalutStatus=new BitVector32(0);
        private BitVector32 m_SafetyModuleFalutStatus=new BitVector32(0);
        private BitVector32 m_DiodeModuleFalutStatus=new BitVector32(0);   
        private BitVector32 m_LaserModuleFalutStatus=new BitVector32(0); 
        

    }
}
