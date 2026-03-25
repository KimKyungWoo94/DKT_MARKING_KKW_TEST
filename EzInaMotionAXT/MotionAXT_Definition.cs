using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion
{

    public class CAXT_AxisModuleInfo
    {
        public int m_iBoradNo;
        public int m_iModulePOS;
        private uint m_iMoudleID;
        private string m_strModuleID;

        public CAXT_AxisModuleInfo()
        {
            Clear();
        }
        public CAXT_AxisModuleInfo(int a_iBoardNo,int a_iModulePos,uint a_iModuleID)
        {
            m_iBoradNo=a_iBoardNo;
            m_iModulePOS=a_iModulePos;
            iModuleID=a_iModuleID;
        }
        public void Clear()
        {
            m_iBoradNo=-1;
            m_iModulePOS=-1;
            m_iMoudleID=0;
            m_strModuleID="[Unknown]";
        }
        public string strModuleID
        {
            get
            {
                return m_strModuleID;
            }
        }
        public uint iModuleID
        {
           get
            {
                return m_iMoudleID;
            }
            set
            {
                m_iMoudleID=value;
                switch (m_iMoudleID)
                {
                    //++ 지정한 축의 정보를 반환합니다.
                    case (uint)AXT_MODULE.AXT_SMC_4V04:             m_strModuleID = "[PCIB-QI4A]"; break;
                    case (uint)AXT_MODULE.AXT_SMC_2V04:             m_strModuleID = "[PCIB-QI2A]"; break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04:            m_strModuleID = "(RTEX-PM)";   break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04PM2Q:        m_strModuleID = "(RTEX-PM2Q)"; break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04PM2QE:       m_strModuleID = "(RTEX-PM2QE)";break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04A4:          m_strModuleID = "[RTEX-A4N]";  break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04A5:          m_strModuleID = "[RTEX-A5N]";  break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIISV:      m_strModuleID = "[MLII-SGDV]"; break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIPM:      m_strModuleID = "(MLII-PM)";   break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICL:      m_strModuleID = "[MLII-CSDL]"; break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICR:      m_strModuleID = "[MLII-CSDH]"; break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV:    m_strModuleID = "[SIIIH-MRJ4]";break;
                    default: m_strModuleID = "[Unknown]"; break;
                }

            }
        }
    }

    public struct stMotionAXT_ParamSetting
		{
			//< Pulse/Encoder Method && Move Parameter Setting >
			public uint UiPulseOutput_Mode;
			public uint UiEncoderInput_Mode;
			public double m_dMinVel;
			public double m_dMaxVel;

			public uint UiAbsRelMode;
			public uint UiProfileMode;
			public double m_dUnit;
			public int m_iPulse;
        
            public void Clear()
			{
				UiPulseOutput_Mode = 0;
				UiEncoderInput_Mode = 0;
				m_dMinVel = 0.0;
				m_dMaxVel = 0.0;

				UiAbsRelMode = 0;
				UiProfileMode = 0;
				m_dUnit = 0.0;
				m_iPulse = 0;
			}
		}
		public struct stMotionAXT_SignalSetting
		{
			//< Input/Output Signal Setting >
			public uint UiZPhase_ActiveLevel;
			public uint UiServoAlarm_ActiveLevel;
			public uint UiInPostion_ActiveLevel;

			public uint UiEMGStop_Mode;
			public uint UiEMGStop_ActiveLevel;
			public uint UiPositive_ActiveLevel;
			public uint UiNegative_ActiveLevel;

			public uint UiServoOn_ActiveLevel;
			public uint UiServoAlarmReset_ActiveLevel;

			public uint UiEncoderType;
            /*
            public stMotionSignalSetting()
            {
                Clear();
            }
            */
            public void Clear()
			{
				UiZPhase_ActiveLevel = 0;
				UiServoAlarm_ActiveLevel = 0;
				UiInPostion_ActiveLevel = 0;

				UiEMGStop_Mode = 0;
				UiEMGStop_ActiveLevel = 0;
				UiPositive_ActiveLevel = 0;
				UiNegative_ActiveLevel = 0;

				UiServoOn_ActiveLevel = 0;
				UiServoAlarmReset_ActiveLevel = 0;

				UiEncoderType = 0;
			}
		}
    	public struct stMotionAXT_SWLimitSetting
		{
			//< Software Limit Setting >
			public uint		UUse		; //사용유무
			public uint		UStopMode ; //소프트 리미트 On시 모션 정지 모드.
			public uint		USelection; //현재 위치 입력 소스 선택 Cmd Pos, Act Pos 
			public double	m_dSWLimitP	; //(+) 방향 소프트 리미트 위치값.
			public double	m_dSWLimitN	; //(-) 방향 소프트 리미트 위치값.
            /*
			public stMotionSWLimitSetting()
            {
                Clear();
            }
            */
            public void Clear()
			{
				UUse		= 0		;
				UStopMode = 0		;
				USelection= 0		;
				m_dSWLimitP = 0.0	;
				m_dSWLimitN = 0.0	;
			}
		}
        public struct stMotionAXT_HomeSearchSetting
		{
			//< Home Search Setting >
			public uint UiHomeSignal_ActiveLevel;
			public uint UiHomeSignal;
			public int m_iHomeDir;
			public uint UiHomeZPhase;
			public double m_dHomeClrTime;
			public double m_dHomeOffset;
			//Home Speed
			public double m_dVelFirst;
			public double m_dVelSecond;
			public double m_dVelThird;
			public double m_dVelLast;

			public double m_dAccFirst;
			public double m_dAccSecond;
            /*
            public stMotionHomeSearchSetting()
            {
                Clear();
            }
            */
            public void Clear()
			{
				UiHomeSignal_ActiveLevel = 0;
				UiHomeSignal = 0;
				m_iHomeDir = 0;
				UiHomeZPhase = 0;
				m_dHomeClrTime = 0.0;
				m_dHomeOffset = 0.0;
				//Home Speed
				m_dVelFirst = 0.0;
				m_dVelSecond = 0.0;
				m_dVelThird = 0.0;
				m_dVelLast = 0.0;

				m_dAccFirst = 0.0;
				m_dAccSecond = 0.0;
			}
		}
	
    
   
}
