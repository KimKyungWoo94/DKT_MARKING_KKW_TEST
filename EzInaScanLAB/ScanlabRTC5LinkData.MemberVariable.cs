using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Scanner
{
    public partial class ScanlabRTC5LinkData
    {               
        string m_strID;
        /// <summary>
        /// 스캐너번호
        /// </summary>
        uint m_iCardNo;
        bool m_bEnableHeadA;
        bool m_bEnableHeadB;


        /// <summary>
        /// LASER_CTRL_SIGNAL_IDX
        /// </summary>
        BitField32Helper m_LaserCtrlValue;
        /// <summary>
        /// 1~4
        /// </summary>
        uint m_iHeadACorrTableNum;
        /// <summary>
        /// 1~4
        /// </summary>
        uint m_iHeadBCorrTableNum;
        string []  m_strCorrectionTableFileNames;
        uint []    m_iCorrectionDimemsion;
        /// <summary>
        ///┬ 메모리를 할당한다. 
        ///│메모리 할당은 두개의 블럭으로 할당할 수 있다. 
        ///├	메모리크기 
        ///│	  : -1) 사용할수 있는 최대 메모리를 할당한다. 
        ///│	  : 0) 사용하지 않는다. 
        ///│	  : n) 사용할 메모리를 n크기 만큼 할당한다. 
        ///├ 입력범위 
        ///│	  : -1 ~ 2^20 
        ///├ 기본값 
        ///│	  : -1, 0 
        ///└ 관련함수 
        ///	  : config_list, n_config_list 
        /// </summary>
        uint m_iList1stMemorySize;
        uint m_iList2ndMemorySize;

        /// <summary>
        /// > 인텔리스켄의 사용유무를 결정한다.
        /// </summary>
        bool m_bIntelliscan;

        /// <summary>
        /// ┬ Polygon Delay 사용유무를 가진다.
        /// ├ 입력범위
        /// │	  : 0) Disable
        /// │	  : 0 over Enable
        /// │     0~ 2^32-1 
        /// ├ 기본값
        /// │	  : 0
        /// └ 관련함수
        ///     : set_delay_mode, n_set_delay_mode , Set_Scanner_delays
        ///     1= 1/10 us 
        /// </summary>
        uint m_iPolygonDelay;


        /// <summary>
        /// ┬ Jump Delay 사용유무를 가진다.
        /// ├ 입력범위
        /// │	  0~ 2^32-1         
        /// ├ 기본값
        /// │	  : 0
        /// └ 관련함수
        ///     :  Set_Scanner_delays
        ///     1= 1/10 us 
        /// </summary>
        uint m_iJumpDelay;

        /// <summary>
        /// ┬ Marking Delay 사용유무를 가진다.
        /// ├ 입력범위
        /// │	  0~ 2^32-1         
        /// ├ 기본값
        /// │	  : 0
        /// └ 관련함수
        ///     :  Set_Scanner_delays
        ///     1= 1/10 us 
        /// </summary>
        uint m_iMarkDelay;
        /// <summary>
        /// ┬ Marking Delay 사용유무를 가진다.
        /// ├ 입력범위
        /// │	  – 2^31 … +(2^21–1)        
        /// ├ 기본값
        /// │	  : 0
        /// └ 관련함수
        ///     :  set_laser_delays
        ///     1= 0.5 us
        /// </summary>
        int m_iLaserOnDelay;
        /// <summary>
        /// ┬ Marking Delay 사용유무를 가진다.
        /// ├ 입력범위
        /// │	    0 … +(2^21–1)               
        /// ├ 기본값
        /// │	  : 0
        /// └ 관련함수
        ///     :  set_laser_delays
        ///     1= 0.5 us 
        /// </summary>
        uint m_iLaserOffDelay;


        double m_fJumpSpeed;
        double m_fMarkSpeed;


        /// <summary>
        ///┬ 3D 사용유무를 가진다.
        ///├ 입력범위
        ///│	  : 0) Disable
        ///│	  : 1) Enable
        ///├ 기본값
        ///│	  : 0
        ///└ 관련함수
        ///    : set_delay_mode, n_set_delay_mode
        /// </summary>
        uint m_iDirectMove3D;

        ///<summary>
        ///┬ 최대 LASER ON 시간을 가진다
        ///├ 입력범위 
        ///│	  : 0 ~ 2^32 - 1 
        ///├ 기본값 
        ///│	  : 0 
        ///├ 단위 
        ///│   : 1 = 1/10us 
        ///└ 관련함수 
        ///    : set_delay_mode, n_set_delay_mode 
        ///</summary>                                    
        uint m_iEdgeLevel;

        ///<summary>
        ///┬ 최소 점프 시간을 가진다.
        ///├ 입력범위 
        ///│	  : 0 ~ 2^32 - 1 
        ///├ 기본값 
        ///│	  : 0 
        ///├ 단위 
        ///│   : 1 = 1/10us 
        ///└ 관련함수 
        ///    : set_delay_mode, n_set_delay_mode 
        ///</summary>                           
        uint m_iMinimumJumpDelay;

        ///<summary>
        ///┬ 최소 점프 거리를 가진다.
        ///├ 입력범위 
        ///│	  : 0 ~ 2^32 - 1 
        ///├ 기본값 
        ///│	  : 0 
        ///└ 관련함수 
        ///    : set_delay_mode, n_set_delay_mode 
        ///</summary>                       
        uint m_iJumpLengthLimit;

        ///<summary>
        ///┬ 사용하는 레이저 모드
        ///├ 입력범위 
        ///│   : 0) CO2 
        ///│   : 1) YAG #1 
        ///│   : 2) YAG #2 
        ///│   : 3) YAG #3 
        ///│   : 4) LASER #4 
        ///│   : 5) YAG #5 
        ///│   : 6) LASER #6 
        ///├ 기본값 
        ///│   : 0) CO2 
        ///└ 관련함수 </summary>                   
        ///    : set_laser_mode, n_set_laser_modr                   
        uint m_iLaserMode;

       
        /// <summary>
        /// ┬ 레이저가 꺼져 있다가 켜 질때 레이저 파워의 흔들림을 무시한다.
        /// ├ 입력범위
        /// │   : 0 ~ 2^26 -1
        /// ├ 기본값
        /// │   : 0
        /// ├ 단위
        /// │   : 1 = 1/64us
        /// └ 관련함수
        ///     : set_firstpulse_killer, n_set_firstpulse_killer
        /// </summary>
        uint m_iFirstPulseKillerLen;

        /// <summary>
        ///┬ FirstPluseKiller 유사한 기능을 한다. 
        ///├ 입력범위 
        ///│   : 0 ~ 2^26 -1 
        ///├ 기본값 
        ///│   : 0 
        ///├ 단위 
        ///│   : 1 = 1/64us 
        ///└ 관련함수 
        ///    : set_qswitch_delay, n_set_qswitch_delay 
        /// </summary>
        uint m_iQSwitchDelay;

        ///<summary>
        ///┬ 레이저의 주파수의 반
        ///├ 입력범위 
        ///│   : 0 ~ 2^32 -1 
        ///├ 기본값 
        ///│   : 0 
        ///├ 단위 
        ///│   : 1 = 1/64us 
        ///└ 관련함수 
        ///    : set_standby, n_set_standby 
        /// </summary>
        uint m_iStandbyHalfPeriod;

        /// <summary>
        /// ┬ 레이저 주파수의 켜진 시간
        /// ├ 입력범위
        /// │   : 0 ~ 2^32 -1
        /// ├ 기본값
        /// │   : 0
        /// ├ 단위
        /// │   : 1 = 1/64us
        /// └ 관련함수
        ///     : set_standby, n_set_standby
        /// </summary>
        uint m_iStandbyPulseLength;


        uint m_iHalfPeriod;
        uint m_iPulseLength;
        double m_iAngleCompensation;
      
        uint m_iSkyWritingMode;
        uint m_iSkyWritingTimelag;
        int  m_iSkyWritingLaserOnShift;
        uint m_iSkyWritingNPrev;
        uint m_iSkyWritingNPost;
        uint m_iSkyWritingLimit;
        uint m_iGalvaMirrorMinTemp;
        uint m_iGalvaMirrorMaxTemp;
        uint m_iServoBoardMinTemp;
        uint m_iServoBoardMaxTemp;
        double m_fXPosScale;
        double m_fYPosScale;
        double m_fScale;
        
    }
}
