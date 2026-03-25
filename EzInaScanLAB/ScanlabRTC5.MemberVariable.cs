using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Scanner
{
    public sealed partial class ScanlabRTC5
    {
        readonly int m_MAX_HEAD_COUNT = 2;
        bool IsDisposed = false;
        bool IsDisposing = false;
        uint m_iLastErrCode;
        BitField32Helper m_ErrorStatus;
        BitField32Helper m_LastErrorStatus;
        BitField32Helper m_HeadStatusA;
        BitField32Helper m_HeadStatusB;
        BitField32Helper m_LaserConnectorOutput;
        BitField32Helper m_LaserConnectorInput;
        BitField32Helper m_ListStatus;
        BitField32Helper m_StartStopInfo;
        BitField32Helper m_ExecuteListStatus;
        BitField32Helper[] m_OperationStateX;
        BitField32Helper[] m_OperationStateY;
        uint m_iList1Point;
        uint m_iList2Point;


        uint m_iListTotalSpace;


        uint m_ListCurrentStatus;
        uint m_ListCurrentPos;
        uint m_ListCurrentWaitCount;



        long[,] m_lStopEventArry;
        uint m_nFlagsOnStopEvent;
        bool m_bAlarm;                    // 알람 상태를 가진다.
        int[] m_iGalvanoMirrorTempX; // 갈바노 미러 X축의 온도를 가진다. (1 = 0.1도)
        int[] m_iGalvanoMirrorTempY; // 갈바노 미러 Y축의 온도를 가진다. (1 = 0.1도)
        int[] m_iServoBoardTempX;    // 서보 보드 X축의 온도를 가진다. (1 = 0.1도)
        int[] m_iServoBoardTempY;    // 서보 보드 Y축의 온도를 가진다. (1 = 0.1도)
        long[] m_lCommandPositionX;
        long[] m_lCommandPositionY;
        long[] m_lActualPositionX;
        long[] m_lActualPositionY;
        long[] m_lPositionX;
        long[] m_lPositionY;
        long[] m_lErrorPositionX;
        long[] m_lErrorPositionY;
        int[] m_iEncoderX;
        int[] m_iEncoderY;

        ScanlabRTC5LinkData m_ConfigData;
        string m_strPGMFilePath="";
        double m_fMarkingSpeed;
        double m_fJumpSpeed;


        private int HEAD_A_IDX { get { return (int)RTC_HEAD.A - 1; } }
        private int HEAD_B_IDX { get { return (int)RTC_HEAD.B - 1; } }

        private int AXIS_X_IDX { get { return (int)RTC_AXIS.X - 1; } }
        private int AXIS_Y_IDX { get { return (int)RTC_AXIS.Y - 1; } }


        public double fJumpSpeed
        {
            get { return m_ConfigData.fScale > 0 ? m_fJumpSpeed / m_ConfigData.fScale : 0; }
            set
            {

            }
        }
        public double fMarkSpeed
        {
            get { return m_ConfigData.fScale > 0 ? m_fJumpSpeed / m_ConfigData.fScale : 0; }
            set
            {

            }
        }
        public double dScale
        {
            get
            {
                return m_ConfigData.fScale;
            }
            set
            {
                if (m_ConfigData.fScale != value)
                {
                    m_ConfigData.fScale = value;
                }
            }

        }
      
        public double dPosScaleX
        {
            get { return m_ConfigData.fXPosScale; }
            set
            {
                if (m_ConfigData.fXPosScale != value )
                {
                    m_ConfigData.fXPosScale = value;
                }
            }
        }
        public double dPosScaleY
        {
            get { return m_ConfigData.fYPosScale; }
            set
            {
                if (m_ConfigData.fYPosScale != value )
                {
                    m_ConfigData.fYPosScale = value;
                }
            }
        }
        public double Angle
        {
            get { return m_ConfigData.AngleCompenstation; }
        }
        public uint nFlagsOnStopEvent { get { return m_nFlagsOnStopEvent; } }
        public bool bAlarm { get { return m_bAlarm; } }                // 알람 상태를 가진다.

        public int GetGalvanoMirrorTempX(RTC_HEAD a_value)
        {
            return m_iGalvanoMirrorTempX[(int)a_value - 1];
        }
        public int GetGalvanoMirrorTempY(RTC_HEAD a_value)
        {
            return m_iGalvanoMirrorTempY[(int)a_value - 1];
        }
        public int GetServoBoardTempX(RTC_HEAD a_value)
        {
            return m_iServoBoardTempX[(int)a_value - 1];
        }
        public int GetServoBoardTempY(RTC_HEAD a_value)
        {
            return m_iServoBoardTempY[(int)a_value - 1];
        }
        public long GetCommandPositionX(RTC_HEAD a_value)
        {
            return m_lCommandPositionX[(int)a_value - 1];
        }
        public long GetCommandPositionY(RTC_HEAD a_value)
        {
            return m_lCommandPositionY[(int)a_value - 1];
        }
        public long GetActualPositionX(RTC_HEAD a_value)
        {
            return m_lActualPositionX[(int)a_value - 1];
        }
        public long GetActualPositionY(RTC_HEAD a_value)
        {
            return m_lActualPositionX[(int)a_value - 1];
        }
        public long GetCurrentPositionX(RTC_HEAD a_value)
        {
            return m_lPositionX[(int)a_value - 1];
        }
        public long GetCurrentPositionY(RTC_HEAD a_value)
        {
            return m_lPositionY[(int)a_value - 1];
        }
        public long GetErrorPositionX(RTC_HEAD a_value)
        {
            return m_lErrorPositionX[(int)a_value - 1];
        }
        public long GetErrorPositionY(RTC_HEAD a_value)
        {
            return m_lErrorPositionY[(int)a_value - 1];
        }
        public long GetEncoderX(RTC_HEAD a_value)
        {
            return m_iEncoderX[(int)a_value - 1];
        }
        public long GetEncoderY(RTC_HEAD a_value)
        {
            return m_iEncoderY[(int)a_value - 1];
        }

        public bool GetListStatus(LIST_READ_STATUS a_IDX)
        {
            if (m_bDllInit)
                m_ListStatus.Data = RTC5Import.RTC5Wrap.n_read_status(m_ConfigData.CardNo);
            return m_ListStatus[(bit)a_IDX];
        }
        public bool GetListStatus_Load(RTC_LIST a_value)
        {
            if (m_bDllInit)
                m_ListStatus.Data = RTC5Import.RTC5Wrap.n_read_status(m_ConfigData.CardNo);
            return a_value == RTC_LIST._1st ? m_ListStatus[(bit)LIST_READ_STATUS.LOAD_LIST1] : m_ListStatus[(bit)LIST_READ_STATUS.LOAD_LIST2];
        }
        public bool GetListStatus_READY(RTC_LIST a_value)
        {
            if (m_bDllInit)
                m_ListStatus.Data = RTC5Import.RTC5Wrap.n_read_status(m_ConfigData.CardNo);
            return a_value == RTC_LIST._1st ? m_ListStatus[(bit)LIST_READ_STATUS.READY_LIST1] : m_ListStatus[(bit)LIST_READ_STATUS.READY_LIST2];
        }
        public bool GetListStatus_BUSY(RTC_LIST a_value)
        {
            if (m_bDllInit)
                m_ListStatus.Data = RTC5Import.RTC5Wrap.n_read_status(m_ConfigData.CardNo);
            return a_value == RTC_LIST._1st ? m_ListStatus[(bit)LIST_READ_STATUS.BUSY_LIST1] : m_ListStatus[(bit)LIST_READ_STATUS.BUSY_LIST2];
        }
        public bool IsExecuteList_BUSY
        {
            get
            {
                if (m_bDllInit)
                    RTC5Import.RTC5Wrap.n_get_status(m_ConfigData.CardNo,
                                                                                 out m_ListCurrentStatus,
                                                                                 out m_ListCurrentPos);
                m_ExecuteListStatus.Data = m_ListCurrentStatus;
                return m_ExecuteListStatus[(bit)LIST_GET_STATUS.BUSY];
            }
        }
        public bool IsExecuteList_InternalBUSY
        {
            get
            {
                if (m_bDllInit)
                    RTC5Import.RTC5Wrap.n_get_status(m_ConfigData.CardNo,
                                                                                 out m_ListCurrentStatus,
                                                                                 out m_ListCurrentPos);
                m_ExecuteListStatus.Data = m_ListCurrentStatus;
                return m_ExecuteListStatus[(bit)LIST_GET_STATUS.INTERNAL_BUSY];
            }
        }
        public bool IsExecuteList_Paused
        {
            get
            {
                if (m_bDllInit)
                    RTC5Import.RTC5Wrap.n_get_status(m_ConfigData.CardNo,
                                                                                 out m_ListCurrentStatus,
                                                                                 out m_ListCurrentPos);
                m_ExecuteListStatus.Data = m_ListCurrentStatus;
                return m_ExecuteListStatus[(bit)LIST_GET_STATUS.PAUSED];
            }
        }
        public bool IsExecuteList_Ready
        {
            get
            {
                if (m_bDllInit)
                    RTC5Import.RTC5Wrap.n_get_status(m_ConfigData.CardNo,
                                                                                 out m_ListCurrentStatus,
                                                                                 out m_ListCurrentPos);
                m_ExecuteListStatus.Data = m_ListCurrentStatus;
                return m_ExecuteListStatus[(bit)LIST_GET_STATUS.BUSY] == true &&
   m_ExecuteListStatus[(bit)LIST_GET_STATUS.INTERNAL_BUSY] == true &&
   m_ExecuteListStatus[(bit)LIST_GET_STATUS.PAUSED] == false;
            }
        }
        public bool IsExistExecuteList_WaitCommnad
        {
            get
            {
                return m_ListCurrentWaitCount > 0;
            }
        }
        public uint iCurrentExecuteListPos
        {
            get { return m_ListCurrentPos; }
        }
        public uint iWaitCMDPosExecuteList
        {
            get { return m_ListCurrentWaitCount; }
        }
        public STOP_EVENT_CODE GetStopEvent(RTC_HEAD a_Head, RTC_AXIS a_Axis)
        {
            STOP_EVENT_CODE Ret = STOP_EVENT_CODE.UNKNOWN;

            if (Enum.IsDefined(typeof(STOP_EVENT_CODE), m_lStopEventArry[(int)a_Head - 1, (int)a_Axis - 1]))
            {
                Ret = (STOP_EVENT_CODE)m_lStopEventArry[(int)a_Head - 1, (int)a_Axis - 1];
            }
            return Ret;
        }
        public bool GetAxisStatus(RTC_HEAD a_Head, RTC_AXIS a_Axis, AXIS_STATUS a_IDX)
        {
            BitField32Helper Ret = a_Axis == RTC_AXIS.X ? m_OperationStateX[(int)a_Head - 1] : m_OperationStateY[(int)a_Head - 1];
            return Ret[(bit)a_IDX];
        }
        public bool GetHeadStatus(RTC_HEAD a_Head, HEAD_STATUS a_IDX)
        {
            BitField32Helper Ret = a_Head == RTC_HEAD.A ? m_HeadStatusA : m_HeadStatusB;
            return Ret[(bit)a_IDX];
        }
        public bool GetStartStopInfo(GET_START_STOP_INFO a_IDX)
        {
            return m_StartStopInfo[(bit)a_IDX];
        }
    }
}
