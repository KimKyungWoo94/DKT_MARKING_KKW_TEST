using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Scanner
{
    public sealed partial class ScanlabRTC5
    {
        public ScanlabRTC5(uint a_Index, string a_strName, string a_strPGMFilePath = "")
        {
            m_ConfigData = new ScanlabRTC5LinkData();
            m_ConfigData.CardNo = a_Index;
            m_ConfigData.strID = a_strName;
            m_strPGMFilePath = a_strPGMFilePath;
            Initialize();
        }
        public ScanlabRTC5(ScanlabRTC5LinkData a_SettingData, string a_strPGMFilePath = "")
        {
            m_ConfigData = a_SettingData;
            m_strPGMFilePath = a_strPGMFilePath;
            Initialize();
        }
        ~ScanlabRTC5()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Disposeing"></param>
        private void Dispose(bool a_Disposeing)
        {
            if (this.IsDisposed)
                return;

            if (a_Disposeing)
            {
                this.IsDisposing = true;
                SetActive(false);

                // Free any other managed objects here.                
            }
            //Free Unmanage Objects here               
            this.IsDisposing = false;
            this.IsDisposed = true;
        }
        private void Initialize()
        {
            m_lStopEventArry = new long[m_MAX_HEAD_COUNT, 2];
            m_nFlagsOnStopEvent = 0;
            m_bAlarm = false;
            m_iGalvanoMirrorTempX = new int[m_MAX_HEAD_COUNT];
            m_iGalvanoMirrorTempY = new int[m_MAX_HEAD_COUNT];
            m_iServoBoardTempX = new int[m_MAX_HEAD_COUNT];
            m_iServoBoardTempY = new int[m_MAX_HEAD_COUNT];

            m_lCommandPositionX = new long[m_MAX_HEAD_COUNT];
            m_lCommandPositionY = new long[m_MAX_HEAD_COUNT];
            m_lActualPositionX = new long[m_MAX_HEAD_COUNT];
            m_lActualPositionY = new long[m_MAX_HEAD_COUNT];
            m_lErrorPositionX = new long[m_MAX_HEAD_COUNT];
            m_lErrorPositionY = new long[m_MAX_HEAD_COUNT];
            m_lPositionX = new long[m_MAX_HEAD_COUNT];
            m_lPositionY = new long[m_MAX_HEAD_COUNT];
            m_iEncoderX = new int[m_MAX_HEAD_COUNT];
            m_iEncoderY = new int[m_MAX_HEAD_COUNT];

            m_HeadStatusA = new BitField32Helper();
            m_HeadStatusB = new BitField32Helper();
            m_ListStatus = new BitField32Helper();
            m_ExecuteListStatus = new BitField32Helper();
            m_ErrorStatus = new BitField32Helper();
            m_LastErrorStatus = new BitField32Helper();
            m_StartStopInfo = new BitField32Helper();

            m_LaserConnectorOutput = new BitField32Helper();
            m_LaserConnectorInput = new BitField32Helper();
            m_OperationStateX = new BitField32Helper[m_MAX_HEAD_COUNT];
            m_OperationStateX[HEAD_A_IDX] = new BitField32Helper();
            m_OperationStateX[HEAD_B_IDX] = new BitField32Helper();

            m_OperationStateY = new BitField32Helper[m_MAX_HEAD_COUNT];
            m_OperationStateY[HEAD_A_IDX] = new BitField32Helper();
            m_OperationStateY[HEAD_B_IDX] = new BitField32Helper();
            SetActive(true);
        }

        public void SetActive(bool a_bValue)
        {
            if (ScanlabRTC5.bDllInit)
            {
                try
                {
                    if (a_bValue)
                    {
                        //Orgin load_program_file
                        //Orgin n_load_Corrction_file
                        m_ConfigData.InitializeFunction(m_strPGMFilePath);
                        m_ConfigData.DoChanged();
                    }
                    else
                    {
                        RTC5Import.RTC5Wrap.n_stop_list(m_ConfigData.CardNo);
                        RTC5Import.RTC5Wrap.n_disable_laser(m_ConfigData.CardNo);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void Execute()
        {
            if (ScanlabRTC5.bDllInit)
            {

                if ((m_iLastErrCode = RTC5Import.RTC5Wrap.n_get_error(m_ConfigData.CardNo)) != 0)
                {
                    // SetActive(false);
                    Trace.WriteLine(string.Format("Scanlab Err Code {0}\n{1}", m_iLastErrCode, ((GET_ERROR)m_iLastErrCode).ToString()));
                }
                if (m_ConfigData.EnableHeadA)
                    m_HeadStatusA.Data =
                            RTC5Import.RTC5Wrap.n_get_head_status(m_ConfigData.CardNo,
                                                                  (uint)RTC_HEAD.A);
                if (m_ConfigData.EnableHeadB)
                    m_HeadStatusB.Data =
                            RTC5Import.RTC5Wrap.n_get_head_status(m_ConfigData.CardNo,
                                                                  (uint)RTC_HEAD.B);
                m_ListStatus.Data = RTC5Import.RTC5Wrap.n_read_status(m_ConfigData.CardNo);
                RTC5Import.RTC5Wrap.n_get_status(m_ConfigData.CardNo,
                                                 out m_ListCurrentStatus,
                                                 out m_ListCurrentPos);
                m_ExecuteListStatus.Data = m_ListCurrentStatus;
                m_ListCurrentWaitCount = RTC5Import.RTC5Wrap.n_get_wait_status(m_ConfigData.CardNo);
                m_StartStopInfo.Data = RTC5Import.RTC5Wrap.n_get_startstop_info(m_ConfigData.CardNo);
                m_iListTotalSpace = RTC5Import.RTC5Wrap.n_get_list_space(m_ConfigData.CardNo);
                m_ErrorStatus.Data = RTC5Import.RTC5Wrap.n_get_error(m_ConfigData.CardNo);
                m_LastErrorStatus.Data = RTC5Import.RTC5Wrap.n_get_last_error(m_ConfigData.CardNo);
                m_LaserConnectorInput.Data = RTC5Import.RTC5Wrap.n_get_laser_pin_in(m_ConfigData.CardNo);
                RTC5Import.RTC5Wrap.n_set_laser_pin_out(m_ConfigData.CardNo, m_LaserConnectorOutput.Data);
                #region HEAD A Status
                if (m_ConfigData.EnableHeadA && m_ConfigData.CorrTableNum_Head_A > 0)
                {

                    m_lPositionX[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 7);
                    m_lPositionY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 8);


                    if (m_ConfigData.IntelliScanMode == true)
                    {

                        long Value, Status;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x528);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status = Value & 0xFFFF;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x529);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status |= ((Value & 0xFFFF) << 16);
                        m_OperationStateX[HEAD_A_IDX].Data = (uint)Status;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x528);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status = Value & 0xFFFF;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x529);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status |= ((Value & 0xFFFF) << 16);
                        m_OperationStateY[HEAD_A_IDX].Data = (uint)Status;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x52A);
                        m_lStopEventArry[HEAD_A_IDX, AXIS_X_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1) >> 4;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x52A);
                        m_lStopEventArry[HEAD_A_IDX, AXIS_Y_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2) >> 4;


                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x514);
                        m_iGalvanoMirrorTempX[HEAD_A_IDX] = (RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1) / 160 * 10);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, 2, 0x514);
                        m_iGalvanoMirrorTempY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2) / 160 * 10;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x0515);
                        m_iServoBoardTempX[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1) / 160 * 10;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, 2, 0x0515);
                        m_iServoBoardTempY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2) / 160 * 10;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x0502);
                        m_lCommandPositionX[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x0502);
                        m_lCommandPositionY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x0501);
                        m_lActualPositionX[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x0501);
                        m_lActualPositionY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.X, 0x0503);
                        m_lErrorPositionX[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.A, (uint)RTC_AXIS.Y, 0x0503);
                        m_lErrorPositionY[HEAD_A_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 2);

                    }
                }
                #endregion HEAD A Status
                #region HEAD B Status
                if (m_ConfigData.EnableHeadB && m_ConfigData.CorrTableNum_Head_B > 0)
                {
                    m_lPositionX[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 7);
                    m_lPositionY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 8);
                    if (m_ConfigData.IntelliScanMode == true)
                    {
                        long Value, Status;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x528);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status = Value & 0xFFFF;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x529);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status |= ((Value & 0xFFFF) << 16);
                        m_OperationStateX[HEAD_B_IDX].Data = (uint)Status;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x528);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status = Value & 0xFFFF;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x529);
                        Value = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) >> 4;
                        Status |= ((Value & 0xFFFF) << 16);
                        m_OperationStateY[HEAD_B_IDX].Data = (uint)Status;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x52A);
                        m_lStopEventArry[HEAD_B_IDX, AXIS_X_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 1) >> 4;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x52A);
                        m_lStopEventArry[HEAD_B_IDX, AXIS_Y_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2) >> 4;

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x514);
                        m_iGalvanoMirrorTempX[HEAD_B_IDX] = (RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) / 160 * 10);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x514);
                        m_iGalvanoMirrorTempY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo + 1, 2) / 160 * 10;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x0515);
                        m_iServoBoardTempX[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1) / 160 * 10;
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x0515);
                        m_iServoBoardTempY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 2) / 160 * 10;


                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x0502);
                        m_lCommandPositionX[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x0502);
                        m_lCommandPositionY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 2);

                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x0501);
                        m_lActualPositionX[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x0501);
                        m_lActualPositionY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 2);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.X, 0x0503);
                        m_lErrorPositionX[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 1);
                        RTC5Import.RTC5Wrap.n_control_command(m_ConfigData.CardNo, (uint)RTC_HEAD.B, (uint)RTC_AXIS.Y, 0x0503);
                        m_lErrorPositionY[HEAD_B_IDX] = RTC5Import.RTC5Wrap.n_get_value(m_ConfigData.CardNo, 2);


                    }
                }
                #endregion HEAD B Status
            }

        }
        public bool IsSystemOK_HeadA
        {
            get
            {
                return GetHeadStatus(RTC_HEAD.A, HEAD_STATUS.POWER_STATUS) &
                                     GetHeadStatus(RTC_HEAD.A, HEAD_STATUS.TEMP_STATUS);
            }
        }
        public bool IsSystemOK_HeadB
        {
            get
            {
                return GetHeadStatus(RTC_HEAD.B, HEAD_STATUS.POWER_STATUS) &
                             GetHeadStatus(RTC_HEAD.B, HEAD_STATUS.TEMP_STATUS);
            }
        }

        public void LaserOn(double Frequency, double PulseLength)
        {

            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;

            //	EnterCriticalSection(&FCriticalSection);
            uint Half = (uint)(1.0 / Frequency / 2.0 * 1000.0 * 64);
            uint Length = (uint)(PulseLength / 100.0 * Half * 2);
            RTC5Import.RTC5Wrap.n_set_laser_pulses_ctrl(m_ConfigData.CardNo, Half, Length);
            RTC5Import.RTC5Wrap.n_enable_laser(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_laser_signal_on(m_ConfigData.CardNo);

            //	LeaveCriticalSection(&FCriticalSection);
        }
        public void LaserOn()
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            if (GetListStatus_BUSY(RTC_LIST._1st) == true)
                return;
            // List Pause 
            if (IsExistExecuteList_WaitCommnad == true)
                return;

            RTC5Import.RTC5Wrap.n_set_laser_pulses_ctrl(m_ConfigData.CardNo, m_ConfigData.OrginFreQHalfPeriod, m_ConfigData.OrginFreQPulseLength);
            RTC5Import.RTC5Wrap.n_enable_laser(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_laser_signal_on(m_ConfigData.CardNo);
            uint uError = RTC5Import.RTC5Wrap.get_last_error();
        }
        public void GateOnTime(double Frequency, double PulseLength, uint Time)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            if (GetListStatus_BUSY(RTC_LIST._1st) == true)
                return;
            // List Pause 
            if (IsExistExecuteList_WaitCommnad == true)
                return;
            //	EnterCriticalSection(&FCriticalSection);
            uint Half = (uint)(1.0 / Frequency / 2.0 * 1000.0 * 64);
            uint Length = (uint)(PulseLength / 100.0 * Half * 2);
            RTC5Import.RTC5Wrap.n_set_start_list_pos(m_ConfigData.CardNo, 1, 0);
            RTC5Import.RTC5Wrap.n_set_start_list(m_ConfigData.CardNo, 1);
            RTC5Import.RTC5Wrap.n_set_laser_pulses(m_ConfigData.CardNo, Half, Length);
            RTC5Import.RTC5Wrap.n_laser_signal_on_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_laser_on_list(m_ConfigData.CardNo, Time * 1000 / 10);
            RTC5Import.RTC5Wrap.n_laser_signal_off_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_set_end_of_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_execute_list(m_ConfigData.CardNo, 1);
            //	LeaveCriticalSection(&FCriticalSection);
        }
        /// <summary>
        /// sec
        /// </summary>
        /// <param name="Time"></param>
        public void GateOnTime(double Time)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            if (GetListStatus_BUSY(RTC_LIST._1st) == true)
                return;
            // List Pause 
            if (IsExistExecuteList_WaitCommnad == true)
                return;

            if (GetListStatus_Load(RTC_LIST._1st) == false)
                RTC5Import.RTC5Wrap.n_set_start_list_pos(m_ConfigData.CardNo, 1, 0);

            RTC5Import.RTC5Wrap.n_set_start_list(m_ConfigData.CardNo, 1);
            RTC5Import.RTC5Wrap.n_set_laser_pulses(m_ConfigData.CardNo, m_ConfigData.OrginFreQHalfPeriod, m_ConfigData.OrginFreQPulseLength);
            RTC5Import.RTC5Wrap.n_laser_signal_on_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_laser_on_list(m_ConfigData.CardNo, (uint)(Time * 10000 * 10));
            RTC5Import.RTC5Wrap.n_laser_signal_off_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_set_end_of_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_execute_list(m_ConfigData.CardNo, 1);
        }
        public bool GateOnNumberOfShot(uint a_NumberOfshot)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return false;
            if (GetListStatus_BUSY(RTC_LIST._1st) == true)
                return false;
            // List Pause 
            if (IsExistExecuteList_WaitCommnad == true)
                return false;
            if (GetListStatus_Load(RTC_LIST._1st))
                RTC5Import.RTC5Wrap.n_set_start_list_pos(m_ConfigData.CardNo, 1, 0);
            RTC5Import.RTC5Wrap.n_set_start_list(m_ConfigData.CardNo, 1);
            RTC5Import.RTC5Wrap.n_set_laser_pulses(m_ConfigData.CardNo, m_ConfigData.OrginFreQHalfPeriod, m_ConfigData.OrginFreQPulseLength);
            RTC5Import.RTC5Wrap.n_laser_signal_on_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_laser_on_list(m_ConfigData.CardNo, (m_ConfigData.OrginFreQHalfPeriod * 2) * a_NumberOfshot);
            RTC5Import.RTC5Wrap.n_laser_signal_off_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_set_end_of_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_execute_list(m_ConfigData.CardNo, 1);
            return true;
        }
        public void ListGateOnNumberOfShot(uint a_Numberofshot)
        {
            RTC5Import.RTC5Wrap.n_laser_on_list(m_ConfigData.CardNo, (m_ConfigData.OrginFreQHalfPeriod * 2) * a_Numberofshot);
        }
        public void LaserOff()
        {
            //	EnterCriticalSection(&FCriticalSection);

            RTC5Import.RTC5Wrap.n_laser_signal_off(m_ConfigData.CardNo);
            //RTC5Import.RTC5Wrap.n_stop_execution(m_ConfigData.CardNo);
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public void Move(double X, double Y)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            //	EnterCriticalSection(&FCriticalSection);
            RTC5Import.RTC5Wrap.n_goto_xy(m_ConfigData.CardNo, (int)(X * m_ConfigData.fXPosScale), (int)(Y * m_ConfigData.fYPosScale));

            uint uError = RTC5Import.RTC5Wrap.get_last_error();
            //if (uError != 0U)
            //    throw new Exception(string.Format("goto_xy function has occurred error : {0:D}", uError));
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public void MoveORG(int X, int Y)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            //	EnterCriticalSection(&FCriticalSection);
            RTC5Import.RTC5Wrap.n_goto_xy(m_ConfigData.CardNo, X,Y);

            uint uError = RTC5Import.RTC5Wrap.get_last_error();
            //if (uError != 0U)
            //    throw new Exception(string.Format("goto_xy function has occurred error : {0:D}", uError));
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public void SetJumpSpeed(double a_value)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            RTC5Import.RTC5Wrap.n_set_jump_speed_ctrl(m_ConfigData.CardNo, a_value / 1000.0 * m_ConfigData.fScale);
        }
        public void SetMarkSpeed(double a_value)
        {
            if (IsExecuteList_BUSY || IsExecuteList_InternalBUSY)
                return;
            RTC5Import.RTC5Wrap.n_set_mark_speed_ctrl(m_ConfigData.CardNo, a_value / 1000.0 * m_ConfigData.fScale);
        }
        public void ListBegin(RTC_LIST a_ListNo, double Frequency, double PulseLength)
        {
            if (GetListStatus_BUSY(a_ListNo) == true)
                return;
            //	EnterCriticalSection(&FCriticalSection);
            double Half = 1.0 / Frequency / 2.0 * 1000.0 * 64;
            double Length = PulseLength / 100.0 * Half * 2;

            RTC5Import.RTC5Wrap.n_set_start_list(m_ConfigData.CardNo + 1, (uint)a_ListNo);
            RTC5Import.RTC5Wrap.n_laser_on_pulses_list(m_ConfigData.CardNo + 1,
                                                       (uint)Half,
                                                       (uint)Length);
            //	LeaveCriticalSection(&FCriticalSection);
        }

        public bool ListBegin(RTC_LIST a_ListNo)
        {
#if !SIM
            if (GetListStatus_BUSY(a_ListNo) == true)
                return false;
            if (GetListStatus_Load(a_ListNo) == false)
                return false;
#endif
            //RTC5Import.RTC5Wrap.n_set_start_list_pos(m_ConfigData.CardNo, (uint)a_ListNo,0);
            RTC5Import.RTC5Wrap.n_set_start_list(m_ConfigData.CardNo, (uint)a_ListNo);
            RTC5Import.RTC5Wrap.n_set_laser_pulses(m_ConfigData.CardNo,
                                                                                                                m_ConfigData.OrginFreQHalfPeriod,
                                                                                                                m_ConfigData.OrginFreQPulseLength);
            RTC5Import.RTC5Wrap.n_set_scanner_delays(m_ConfigData.CardNo,
                                                                                                                m_ConfigData.OrginJumpDelay,
                                                                                                                m_ConfigData.OrginMarkDelay,
                                                                                                                 m_ConfigData.OrginPolygonDelay);
            RTC5Import.RTC5Wrap.n_set_laser_delays(m_ConfigData.CardNo, m_ConfigData.OrginLaserOnDelay, m_ConfigData.OrginLaserOffDelay);
            RTC5Import.RTC5Wrap.n_set_jump_speed(m_ConfigData.CardNo, m_ConfigData.OrginJumpSpeed);
            RTC5Import.RTC5Wrap.n_set_mark_speed(m_ConfigData.CardNo, m_ConfigData.OrginMarkSpeed);
            //RTC5Import.RTC5Wrap.n_laser_signal_on_list(m_ConfigData.CardNo);
            return true;
        }
        public bool ListLaserDelay(int OnDelay, uint OffDelay)
        {
            //	EnterCriticalSection(&FCriticalSection);
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return false;
            uint RTCMode = RTC5Import.RTC5Wrap.get_rtc_mode();
            RTC5Import.RTC5Wrap.n_set_laser_delays(m_ConfigData.CardNo,
                                                   RTCMode == 5 ? OnDelay * 2 : OnDelay,
                                                   RTCMode == 5 ? OffDelay * 2 : OffDelay
                                                   ); // Unit: 0.5us
            return true;
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public bool ListScannerDelay(uint JumpDelay, uint MarkDelay, uint PolygonDelay)
        {
#if !SIM
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return false;
#endif
            //	EnterCriticalSection(&FCriticalSection);
            RTC5Import.RTC5Wrap.n_set_scanner_delays(m_ConfigData.CardNo, JumpDelay / 10, MarkDelay / 10, PolygonDelay / 10); // Unit: 10us
            return true;                                                                                                                  //	LeaveCriticalSection(&FCriticalSection);
        }
        //         public void ListMarkSpeed(double Value)
        //         {
        // #if !SIM
        //             if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
        //                 return;
        // #endif
        //             //	EnterCriticalSection(&FCriticalSection);
        //             RTC5Import.RTC5Wrap.n_set_mark_speed(m_ConfigData.CardNo, Value / 1000.0 * m_ConfigData.fScale);
        //             //	LeaveCriticalSection(&FCriticalSection);
        //         }
        //         public void ListJumpSpeed(double Value)
        //         {
        // #if !SIM
        //             if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
        //                 return;
        // #endif
        //             //	EnterCriticalSection(&FCriticalSection);
        //             RTC5Import.RTC5Wrap.n_set_jump_speed(m_ConfigData.CardNo, Value / 1000.0 * m_ConfigData.fScale);
        //             //	LeaveCriticalSection(&FCriticalSection);
        //         }
        public void ListJumpAbs(double a_Xvalue, double a_YValue)
        {
#if !SIM
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return;
#endif
            RTC5Import.RTC5Wrap.n_jump_abs(m_ConfigData.CardNo, (int)(a_Xvalue * m_ConfigData.fXPosScale), (int)(a_YValue * m_ConfigData.fYPosScale));
        }
        public void ListOrginJumpAbs(int a_Xvalue, int a_YValue)
        {
#if !SIM
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return;
#endif
            RTC5Import.RTC5Wrap.n_jump_abs(m_ConfigData.CardNo, a_Xvalue, a_YValue);
        }


        public void ListMarkAbs(double a_Xvalue, double a_YValue)
        {
#if !SIM
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return;
#endif
            RTC5Import.RTC5Wrap.n_mark_abs(m_ConfigData.CardNo, (int)(a_Xvalue * m_ConfigData.fXPosScale), (int)(a_YValue * m_ConfigData.fYPosScale));
        }
        public void ListOrginMarkAbs(int a_Xvalue, int a_YValue)
        {
#if !SIM
            if (GetListStatus_Load(RTC_LIST._1st) == false && GetListStatus_Load(RTC_LIST._2nd) == false)
                return;
#endif
            RTC5Import.RTC5Wrap.n_mark_abs(m_ConfigData.CardNo, a_Xvalue, a_YValue);
        }
        public void ListOrginCrossLine(int a_CenterX, int a_CenterY, int a_XPitch, int a_YPitch)
        {
            ListOrginJumpAbs((-a_XPitch / 2) + a_CenterX, a_CenterY);
            ListOrginMarkAbs((a_XPitch / 2) + a_CenterX, a_CenterY);
            ListOrginJumpAbs(a_CenterX, (-a_YPitch / 2) + a_CenterY);
            ListOrginMarkAbs(a_CenterX, (a_YPitch / 2) + a_CenterY);
        }
        public void ListCrossLine(double a_CenterX, double a_CenterY, double a_XPitch, double a_YPitch)
        {
            ListJumpAbs((-a_XPitch / 2.0) + a_CenterX, a_CenterY);
            ListMarkAbs((a_XPitch / 2.0) + a_CenterX, a_CenterY);
            ListJumpAbs(a_CenterX, (-a_YPitch / 2.0) + a_CenterY);
            ListMarkAbs(a_CenterX, (a_YPitch / 2.0) + a_CenterY);
        }

        public void ListCrossGrid(
            double a_fCenterX, double a_fCenterY,
            double a_fXPitch, double a_fYPitch,
            int a_iRowGridCount,
            int a_iColGridCount,
            double a_fRowGridPitch,
            double a_fColGridPitch
            )
        {
            int iRowGridCOunt = a_iRowGridCount % 2 == 0 ? a_iRowGridCount + 1 : a_iRowGridCount;
            int iColGridCOunt = a_iColGridCount % 2 == 0 ? a_iColGridCount + 1 : a_iColGridCount;
            double dCalCenterX = 0.0;
            double dCalCenterY = 0.0;
            for (int Col = 0; Col < iColGridCOunt; Col++)
            {
                dCalCenterX = ((iColGridCOunt - 1) * (a_fXPitch) / 2.0) - Col * a_fXPitch + a_fCenterX;
                for (int Row = 0; Row < iRowGridCOunt; Row++)
                {
                    dCalCenterY = ((iRowGridCOunt - 1) * (a_fYPitch) / 2.0) - Row * a_fYPitch + a_fCenterY;
                    ListCrossLine(dCalCenterX, dCalCenterY, a_fColGridPitch, a_fRowGridPitch);
                }
            }
        }
        public void ListCrossGridOutLineRect(
           double a_fCenterX, double a_fCenterY,
           double a_fXPitch, double a_fYPitch,
           int a_iRowGridCount,
           int a_iColGridCount,
           double a_fRowGridPitch,
           double a_fColGridPitch
           )
        {
            int iRowGridCOunt = a_iRowGridCount % 2 == 0 ? a_iRowGridCount + 1 : a_iRowGridCount;
            int iColGridCOunt = a_iColGridCount % 2 == 0 ? a_iColGridCount + 1 : a_iColGridCount;
          

            double dOutlineWidth =  a_fXPitch * (iColGridCOunt -1) + a_fColGridPitch;
            double dOutlineHeight = a_fYPitch * (iRowGridCOunt-1) + a_fRowGridPitch;
            double dHalfOutlineWidth = dOutlineWidth / 2.0;
            double dHalfOutlineHeight = dOutlineHeight / 2.0;
            ListJumpAbs(-dHalfOutlineWidth+ a_fCenterX, dHalfOutlineHeight+ a_fCenterY);
            ListMarkAbs( dHalfOutlineWidth+ a_fCenterX, dHalfOutlineHeight+ a_fCenterY);
            ListMarkAbs( dHalfOutlineWidth+ a_fCenterX, -dHalfOutlineHeight + a_fCenterY);
            ListMarkAbs( -dHalfOutlineWidth+ a_fCenterX, -dHalfOutlineHeight + a_fCenterY);
            ListMarkAbs(-dHalfOutlineWidth + a_fCenterX, dHalfOutlineHeight + a_fCenterY);

        }
        public void ListEnd()
        {
            //	EnterCriticalSection(&FCriticalSection);
            RTC5Import.RTC5Wrap.n_laser_signal_off_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_set_end_of_list(m_ConfigData.CardNo);
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public bool ListExecute(RTC_LIST a_value)
        {
#if !SIM
            if (GetListStatus_BUSY(a_value) == true || GetListStatus_READY(a_value) == false)
                return false;
#endif
            //	EnterCriticalSection(&FCriticalSection);
            //RTC5Import.RTC5Wrap.stop_execution();
            RTC5Import.RTC5Wrap.n_execute_list(m_ConfigData.CardNo, (uint)a_value);
            return true;
            //	LeaveCriticalSection(&FCriticalSection);
        }

        public void ListStop()
        {
            //	EnterCriticalSection(&FCriticalSection);
            RTC5Import.RTC5Wrap.n_stop_list(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_stop_execution(m_ConfigData.CardNo);
            //	LeaveCriticalSection(&FCriticalSection);
        }
        public bool ListReset(RTC_LIST a_ListNo)
        {
#if !SIM
            if (GetListStatus_BUSY(a_ListNo) == true)
                return false;
#endif
            RTC5Import.RTC5Wrap.n_set_start_list_pos(m_ConfigData.CardNo, (uint)a_ListNo, 0);
            return true;
        }
        public bool MakeListFromGraphicsPath(GraphicsPath path, RTC_LIST a_ListNo, bool a_XDirReverse, bool a_YDirReverse, bool a_bXYReverse = false)
        {
#if !SIM
            if (GetListStatus_BUSY(a_ListNo) == true)
                return false;
            if (GetListStatus_Load(a_ListNo) == false)
                return false;
#endif
            PointF ptStart = new PointF();
            PointF ptBefore = new PointF();
            PointF ptTemp = new PointF();
            int index = 0;
            int iPathCount = path.PointCount;

            PointF[] Points = path.PathPoints;
            byte[] Types = path.PathTypes;
            System.Drawing.Drawing2D.PathPointType pType;
            foreach (PointF ptPoint in Points)
            {

                ptTemp.X = a_XDirReverse == true ? -ptPoint.X : ptPoint.X;
                ptTemp.Y = a_YDirReverse == true ? -ptPoint.Y : ptPoint.Y;
                pType = (System.Drawing.Drawing2D.PathPointType)Types[index];
                switch (pType)
                {
                    case PathPointType.Start:
                        ptStart = ptTemp;
                        ptBefore = ptTemp;
                        if (a_bXYReverse == false)
                        {
                            ListJumpAbs(ptTemp.X, ptTemp.Y);
                            //Trace.WriteLine(string.Format("Jump ({0} , {1}) Type: {2}", ptTemp.X, ptTemp.Y,(int)pType));
                        }

                        else
                        {
                            ListJumpAbs(ptTemp.Y, ptTemp.X);
                            //Trace.WriteLine(string.Format("Jump ({0} , {1}) Type: {2} Reverse", ptTemp.Y, ptTemp.X,(int)pType));
                        }


                        break;
                    case PathPointType.Line:
                        ptBefore.X = ptTemp.X;
                        ptBefore.Y = ptTemp.Y;
                        if (a_bXYReverse == false)
                        {
                            ListMarkAbs(ptTemp.X, ptTemp.Y);
                            //Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2}", ptTemp.X, ptTemp.Y,(int)pType));
                        }
                        else
                        {
                            ListMarkAbs(ptTemp.Y, ptTemp.X);
                            // Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2} Reverse", ptTemp.Y, ptTemp.X,(int)pType));
                        }
                        break;
                    case PathPointType.Bezier:
                        ///case PathPointType.Bezier3                            
                        ptBefore.X = ptTemp.X;
                        ptBefore.Y = ptTemp.Y;
                        if (a_bXYReverse == false)
                        {
                            ListMarkAbs(ptTemp.X, ptTemp.Y);
                            //Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2} ", ptTemp.X, ptTemp.Y,(int)pType));
                        }

                        else
                        {
                            ListMarkAbs(ptTemp.Y, ptTemp.X);
                            // Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2}Reverse", ptTemp.Y, ptTemp.X));
                        }
                        break;
                    case PathPointType.PathTypeMask:
                        break;
                    case PathPointType.DashMode:
                        break;
                    case PathPointType.PathMarker:
                        break;
                    case PathPointType.CloseSubpath:
                    case (PathPointType)129:
                    case (PathPointType)161:
                        if (a_bXYReverse == false)
                        {
                            ListMarkAbs(ptTemp.X, ptTemp.Y);
                            //Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2}", ptTemp.X, ptTemp.Y,(int)pType));                           
                            if (index + 1 >= Points.Length)
                            {
                                ListJumpAbs(ptBefore.X, ptBefore.Y);

                                //Trace.WriteLine("string ADD Last Line");
                            }

                        }
                        else
                        {
                            ListMarkAbs(ptTemp.Y, ptTemp.X);
                            //Trace.WriteLine(string.Format("Mark ({0} , {1}) Type: {2} Reverse", ptTemp.Y, ptTemp.X,(int)pType));
                            if (index + 1 >= Points.Length)
                            {
                                ListJumpAbs(ptBefore.Y, ptBefore.X);
                                //Trace.WriteLine("string ADD Last Line");
                            }
                        }
                        ptBefore.X = ptTemp.X;
                        ptBefore.Y = ptTemp.Y;
                        break;
                    default:
                        break;
                }
                index++;
            }
            return true;
        }


        public void ResetDevice()
        {
            uint ErrCode = RTC5Import.RTC5Wrap.n_get_error(m_ConfigData.CardNo);
            RTC5Import.RTC5Wrap.n_reset_error(m_ConfigData.CardNo, ErrCode);
        }
        public bool GetLaserConnector_Input(LaserConnector_in a_Ch)
        {
            return m_LaserConnectorInput[(bit)a_Ch];
        }
        public bool GetLaserConnector_Output(LaserConnector_Out a_Ch)
        {
            return m_LaserConnectorOutput[(bit)a_Ch];
        }
        public void SetLaserConnector_Output(LaserConnector_Out a_Ch, bool a_Value)
        {
            m_LaserConnectorOutput[(bit)a_Ch] = a_Value;
        }
        public bool IsFault
        {
            get { return m_ErrorStatus.Data > 0; }
        }
        public ScanlabRTC5LinkData ConfigData
        {
            get { return m_ConfigData; }
        }
        public override string ToString()
        {
            return m_ConfigData.strID;
        }

        public double Move_X_NegativeLimit
        {
            get
            {
                return m_ConfigData.fXPosScale > 0 ? ScanlabRTC5.MOVE_Negative_LIMIT / m_ConfigData.fXPosScale : ScanlabRTC5.MOVE_Negative_LIMIT;

            }
        }
        public double Move_X_PositiveLimit
        {
            get
            {
                return m_ConfigData.fXPosScale > 0 ? ScanlabRTC5.MOVE_Positive_LIMIT / m_ConfigData.fXPosScale : ScanlabRTC5.MOVE_Negative_LIMIT;

            }
        }
        public double Move_Y_NegativeLimit
        {
            get
            {
                return m_ConfigData.fYPosScale > 0 ? ScanlabRTC5.MOVE_Negative_LIMIT / m_ConfigData.fYPosScale : ScanlabRTC5.MOVE_Negative_LIMIT;

            }
        }
        public double Move_Y_PositiveLimit
        {
            get
            {
                return m_ConfigData.fXPosScale > 0 ? ScanlabRTC5.MOVE_Positive_LIMIT / m_ConfigData.fYPosScale : ScanlabRTC5.MOVE_Negative_LIMIT;

            }
        }
        /// <summary>
        /// mm / Sec
        /// </summary>
        public double MinJump_MarkSpeed
        {
            get
            {
                return m_ConfigData.fScale > 0 ? (ScanlabRTC5.MIN_JUMP_MARK_SPEED / m_ConfigData.fScale) * 1000 : ScanlabRTC5.MIN_JUMP_MARK_SPEED * 1000;
            }
        }
        /// <summary>
        /// mm / sec
        /// </summary>
        public double MaxJump_MarkSpeed
        {
            get
            {
                return m_ConfigData.fScale > 0 ? (ScanlabRTC5.MAX_JUMP_MARK_SPEED / m_ConfigData.fScale) * 1000 : ScanlabRTC5.MAX_JUMP_MARK_SPEED * 1000;
            }
        }
    }
}
