using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.PowerMeter.Ohpir
{
    public sealed partial class SPC
    {
        bool            IsDisposed = false;
        bool            IsDisposing = false;
        bool            m_bLoopEnable = false;
		bool			m_bHandShakeSuccess = false;
		string          m_strPacketEndSpliter = "\r";
        string          m_strConfigSection;
        Thread          m_pMeasureThread;
        int             m_iMeasureInterval;
        bool            m_bMeasureEnable;
        Thread          m_pLoopThread;

		StringBuilder m_MakePacketStringBuilder=new StringBuilder();
		StringBuilder m_RecvDataStringBuilder=new StringBuilder();

		string	m_strVersion				;
		string	m_strZeroHeadStatus			;
		string	m_strHeadInformation		;
		string	m_strErrorMSG				;
		double	m_fMeasureValue				;
        double  m_fMeasureAvgValue          ;
        double  m_fMeasureMinValue          ;
        double  m_fMeasureMaxValue          ;
        List<double> m_fMeasureAvgValueList ;   
        uint    m_iMeasureAvgCountDefault   ;
        uint    m_iMeasureCount             ;
		int		m_iWaveLengthIndex			;	
        bool    m_bFirstMeasureAvgSet       ;	
        enumPowerScale                   m_eHeadRange;
        enumMainHeadSetting              m_eMainHeadSetting;
        enumWaveLength                   m_eWaveLength;  
        EzIna.PowerMeter.eZeroSetStataus m_eZeroSetStatus;

        bool m_bZeroSetCMDPreset;
        bool m_bZeroSetExecute;
        
    }
}
