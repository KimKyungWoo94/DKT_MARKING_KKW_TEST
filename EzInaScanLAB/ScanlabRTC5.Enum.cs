using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Scanner
{
    public sealed partial class ScanlabRTC5
    {
        public enum RTC_HEAD:int
        {
            A=1,
            B
        }
        public enum RTC_AXIS:int
        {
            X=1,
            Y=2
        }
        public enum RTC_LIST:int
        {
            _1st=1,
            _2nd=2,
        }
        public enum LaserConnector_in
        {
            IN_1,
            IN_2,
        }
        public enum LaserConnector_Out
        {
            OUT_1,
            OUT_2,
        }
        /// <summary>
        /// Error Code
        /// </summary>        
        public enum DRIVER_ERROR_CODE:int
        {
            NO_ERR=0,
            /// <summary>
            /// No board found
            /// </summary>
            NO_CARD=1,
            /// <summary>
            /// Access denied
            /// <para> (this error can occur by init_rtc5_dll, select_rtc, acquire_rtc or any multi-board command).</para>              
            /// </summary>
            ACCESS_DENIED=2,
            /// <summary>
            /// Command not forwarded
            /// <para>(this error implies an internal, PCI or driver error, e.g.caused by a hardware defect oran incorrect connection) </para>
            /// </summary>
            SEND_ERR=4,
            /// <summary>
            /// No response from board
            /// <para> (it is likely that no program has been loaded onto the RTC5; this error can especially occur in connection with control </para>
            /// <para> commands that expect a response, e.g. get_hex_version).</para>            
            /// </summary>
            TIME_OUT=8,
            /// <summary>
            /// Invalid parameter
            ///  <para>(this error can occur through all commands for which invalid parameters are not </para>
            ///  <para>automatically corrected to valid values, e.g. parameters with limited choices such as get_head_para; </para>
            ///  <para>if this error occurs for a list command, it is replaced with list_nop; </para>
            ///  <para>if this error occurs for a control command, it is not executed).</para>            
            /// </summary>
            PARAM_ERR=16,
            /// <summary>
            /// List processing is (not) active
            /// <para>(e.g. for execute_list, if a list is currently being processed  e.g. for stop_execution, </para>
            /// <para>if no list is currently being processed e.g. for restart_list, </para>
            /// <para>if pause_list was not previously called)</para>            
            /// </summary>
            BUSY=32,
            /// <summary>
            /// List command rejected, illegal input pointer
            /// <para>(e.g. for any list command directly after load_char + list_return: the list command is then not loaded).</para>
            /// </summary>
            REJECTED =64,
            /// <summary>
            /// List command was converted to a list_nop
            /// <para>(e.g. set_end_of_list in a protected subroutine)</para>
            /// </summary>
            IGNORED =128,
            /// <summary>
            /// Version error: DLL version (RTC5 DLL), RTC version (firmware file) and HEX version (DSP program file) not compatible
            /// <para>(see also load_program_file).</para>
            /// </summary>
            VERSION_MIS_MATCH =256,
            /// <summary>
            /// Verify error: The download verification (see page 99) has detected an incorrect download
            /// </summary>
            VERRIFY_ERR=512,

            /// <summary>
            /// DSP version error: DSP version too old (this error only occurs with older RTC5 Boards – 
            /// <para>see get_rtc_version bits #16-23 – and only through a few commands such as mark_ellipse_abs; </para>
            /// <para>the corresponding command description’s “Version info” section contains related comments;</para>
            /// <para>Commands that generate the error are neither executed nor replaced by list_nop).</para>
            /// </summary>
            TYPE_REJECTED =1024,
            /// <summary>
            /// A DLL-internal Windows memory request failed.
            /// </summary>
            EEPPOM_ERR =2048,
            /// <summary>
            /// Error reading PCI configuration register (can only occur during init_rtc5_dll).
            /// </summary>
            CONFIG_ERR =65536      
        }

        /// <summary>
        /// 
        /// </summary>
        public enum LOAD_PGM_FILE_ERR_CODE
        {
            SUCCESS,
            RESET_ERR,
            UNRESET_ERR,
            FILE_ERR,
            FORMAT_ERR,
            SYSTEM_ERR,
            ACCESS_ERR,
            VERSION_ERR,
            SYSTEM_DRIVER_NOT_FOUND,
            DRIVER_CALL_ERR,
            CONFIG_ERR,
            FIRMWARE_ERR,
            PCI_DOWNLOAD_ERR,
            BUSY_ERR,
            DSP_MEMOERY_ERR,
            VERIFY_ERR,
            PCI_ERR,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum CORR_TABLE_NUM:int
        {
            TABLE1,
            TABLE2,
            TABLE3,
            TABLE4,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum CORR_DIMEMSION_NUM:uint
        {
            _2D=2,
            _3D=3,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum LASER_CTRL_SIGNAL_IDX
        {
            /// <summary>
            /// 0 :  The signals are cut off at the end of the LASERON signal.
            /// <para>1: The final pulse fully executes despite completion of the LASERON signal.</para>
            /// </summary>
            PULSE_SWITCH_SET,
            /// <summary>
            /// 0: No phase shift
            ///<para> 1: CO2 mode: The LASER1 signal is exchanged with the LASER2 signal.</para>
            ///<para>YAG modes: The LASER1 is shifted back 180° (half a signal period)</para>
            /// </summary>
            PHASE_SHIFT_ENABLE,
            /// <summary>
            ///  0: The “Laser active” laser control signals are enabled.
            /// <para>1: The “Laser active” laser control signals are disabled</para>
            /// </summary>
            ENABLE_LASER_ACTIVE,
            /// <summary>
            /// 0: The signal at the LASERON port is set to active-high.
            /// <para> 1: The signal at the LASERON port is set to active-low.</para>
            /// </summary>
            LASER_ON_SIGNAL_LEV,
            /// <summary>
            ///  LASER1/LASER2 signal level.
            /// <para> 0: The signals at the LASER1 and LASER2 output ports are set to active-high.</para>
            /// <para> 1: The signals at the LASER1 and LASER2 output ports are set to active-low.</para>
            /// </summary>
            LASER_1_2_PORT_SIGNAL_LEV,
            /// <summary>
            /// Determines for laser_on_pulses_list whether external signal pulses
            /// <para>  0: At the falling edge.</para>
            /// <para>  1: At the rising edge.</para>            
            /// </summary>
            LASER_EXT_SIGNAL_PULSES,
            /// <summary> 
            ///  0: Output synchronization is switched off 
            /// <para> 1: Output synchronization is switched on (see page 169)</para>
            /// </summary>
            OUTPUT_SYNC_SWITCH,
            /// <summary>
            /// 0: The constant pulse length mode is switched off 
            /// <para>1: The constant pulse length mode is switched on (see page 157 and set_pulse_picking_length)</para>
            /// </summary>
            CONSTANT_PULSE_LEN_MODE,
            
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POWER_OK_HEAD_A_X=15,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            TEMP_OK_HEAD_A_X,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POS_ACK_HEAD_A_X,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POWER_OK_HEAD_A_Y,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            TEMP_OK_HEAD_A_Y,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POS_ACK_HEAD_A_Y,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POWER_OK_HEAD_B_X,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            TEMP_OK_HEAD_B_X,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POS_ACK_HEAD_B_X,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POWER_OK_HEAD_B_Y,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            TEMP_OK_HEAD_B_Y,
            /// <summary>
            /// for laser-signal auto-suppression
            /// </summary>
            POS_ACK_HEAD_B_Y,
            /// <summary>
            /// 1: In case of error, automatic monitoring (laser-signal auto-suppression)
            /// <para>automatically generates a /STOP signal (list stops, laser control signals get permanently switched off)</para>
            /// </summary>
            ERR_AUTO_MONITORING

        }

        /// <summary>
        /// 
        /// </summary>
        public enum LASER_MODE:uint
        {
            CO2,
            YAG_1,
            YAG_2,
            YAG_3,
            LASER_4,
            YAG_5,
            LASER_6,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum SKY_WRTING_MODE:uint
        {
            DISABLE=0,
            MODE1,
            MODE2,
            MODE3,
        }
        /// <summary>
        /// read_status
        /// </summary>
        public enum LIST_READ_STATUS:uint
        {
            LOAD_LIST1,
            LOAD_LIST2,
            READY_LIST1,
            READY_LIST2,
            BUSY_LIST1,
            BUSY_LIST2,
            USED_LIST1,
            USED_LIST2,
        }
        public enum LIST_GET_STATUS
        {
            BUSY=0,
            INTERNAL_BUSY=7,
            PAUSED=15,
        }


        public enum HEAD_STATUS
        {
            POS_ACK_X=3,
            POS_ACK_Y,
            TEMP_STATUS=6,
            POWER_STATUS,
        }

        /// <summary>
        /// Current Operational State  
        /// <para>m_OperationStateX, m_OperationStateY</para>           
        /// </summary>
        public enum AXIS_STATUS:uint
        {
            /// <summary>
            ///  1: The control is activated, as soon as all necessary flags are set
            /// </summary>
            CTRL_ACTIVATED,
            REVERSED_2,
            /// <summary>
            /// intelliWELD   , else Reversed
            /// <para>1 = The sensor board was recognized)</para>
            /// </summary>
            SensorBoard_RECDOGNIZED, 
            /// <summary>
            /// intelliWELD ,else Reversed
            /// <para> 1 = currently no INTERLOCK error)</para>
            /// </summary>
            CURRENTLY_NO_INTERLOCK_ERR,
            /// <summary>
            /// 1: All control parameters valid.
            /// </summary>
            ALL_CTRL_PARAM_VAILD,
            /// <summary>
            /// 0: The galvanometer scanner has reached a critical edge position
            /// </summary>
            REACHED_CRITICAL_EDGE_POS,
            /// <summary>
            /// 1: The AD converter was successfully initialized.
            /// </summary>
            AD_CONVERTOR_INITIALIZD_OK,

            /// <summary>
            /// 0: The temperature in the scan system exceeds the maximum allowed value.
            /// <para>The system was switched into a temporary error state</para>  
            /// </summary>
            SYSTEM_TEMP_EXCEED_MAX,
            /// <summary>
            /// 0: The external power supply voltages have – at least temporarily – 
            /// <para>dropped below the allowed value</para>  
            /// </summary>
            EXT_POWER_DROPPED_ALLOWED_VALUE,
            /// <summary>
            ///  0: A critical error occurred. The system was switched into a permanent  error state.
            /// </summary>
            CRITCAL_ERR_OCCURRED,
            /// <summary>
            ///  1: Booting process completed
            /// </summary>
            BOOTING_PROCESS_COMPLATED,
            /// <summary>
            /// 1: Galvanometer scanner and servo board temperature within normal range.
            /// </summary>
            SYSTEM_TEMP_NORMAL,
            /// <summary>
            /// 1: Position error within normal range
            /// </summary>
            POS_ERR_WITH_IN_NORMAL_RANGE,
            /// <summary>
            /// 1: Internal voltages normal.
            /// </summary>
            INT_VOLTAGE_NORMAL,
            /// <summary>
            /// = 1: Galvanometer scanner heater’s output stage is on
            /// <para>(always 0 for intellicube, dynAXIS® XS and scan systems with watercooled scanners).</para>
            /// </summary>
            GALVONOMETER_HEATER_OUTPUT_STAGE_ON,
            /// <summary>
            ///  1: Galvanometer scanner’s output stage is on.
            /// </summary>
            GALVANOMETER_OUTPUT_STAGE_ON,
            /// <summary>
            /// 1: Galvanometer scanner operation temperature within normal range.
            /// </summary>
            GALVANOMETER_OPERATION_TEMP_OK=25,
            /// <summary>
            /// 1: Servo board operation temperature within normal range
            /// </summary>
            SERVO_BOARD_OPERATION_TEMP_OK,
            /// <summary>
            ///  1: AGC voltage (PD supply voltage) OK
            /// </summary>
            AGC_VOLTAGE_OK,
            /// <summary>
            /// 1: DSP core supply voltage (1.8 V) OK.
            /// </summary>
            DSP_CORE_VOLTAGE_OK,
            /// <summary>
            ///  1: DSP IO voltage (3.3 V) OK
            /// </summary>
            DSP_IO_VOLOTAGE_OK,
            /// <summary>
            ///  1: Analog section voltage (9 V) OK.
            /// </summary>
            ANALOG_SECTION_VOLTAGE_OK,
        }
        public enum STOP_EVENT_CODE
        {
            UNKNOWN,
            AXIS_REACHED_CRITICAL_EDGE_POS=0x0010,
            AD_CONVERTOR_ERR=0x0020,
            SYSTEM_TEMP_ABOVE_MAX=0x0030,
            EXT_POWER_VOLTAGE_DROPPED_ALLOW_VALUE=0x0040,
            FLAGS_ARE_NOT_VAILD=0x0050,
            /// <summary>
            ///  Watchdog 10 µs time out (loop time exceeded)
            /// </summary>
            WATCHDOG_TIMEOUT=0x00D0,
            POS_ACK_TIME_OUT=0x00E0,
        }
        /// <summary>
        /// get_error result,
        /// </summary>
        public enum GET_ERROR:uint
        {
            NO_BOARD_FOUND,
            ACCESS_DENIED,
            SEND_ERROR,
            TIME_OUT,
            PARAM_ERROR,
            BUSY,
            REJECTED,
            IGNORED,
            VERSION_MISMATCH,
            VERIFY_ERROR,
            TYPE_REJECTED,
            OUT_OF_MEMORY,
            EEPROM_ERROR,
            CONFIG_ERROR=16,
        }
        public enum GET_START_STOP_INFO:uint
        {
            /// <summary>
            /// An internal list start has been executed (by execute_list or similar) 
            /// <para>since the last get_startstop_info command was called</para>
            /// </summary>
            INT_LIST_START,
            /// <summary>
            /// An external list start has been executed (by /START, /START2, /SlaveSTART, simulate_ext_start or simulate_ext_start_ctrl) since the last 
            /// <para>get_startstop_info command was called</para>
            /// </summary>
            EXT_LIST_START,
            /// <summary>
            /// An internal list stop has been executed (by stop_execution) since the 
            /// <para>last get_startstop_info command was called</para>
            /// </summary>
            INT_LIST_STOP,
            /// <summary>
            /// An external list stop has been executed (by /STOP, /STOP2, /Slave-STOP 
            /// <para>or simulate_ext_stop) since the last get_startstop_info command was called.</para>
            /// <para>= 1: No stop signals are currently present at the inputs or the inputs are not connected</para>
            /// <para>= 0: There is a stop signal at at least one input.</para>
            /// </summary>
            EXT_LIST_STOP,
            /// <summary>
            /// The laser control signals for “laser active” operation are enabled (see 
            /// <para>also set_laser_control, enable_laser and disable_laser)</para>
            /// </summary>
            LASER_ACTIVE_ENABLE=9,
            /// <summary>
            /// = 1: The TTL laser control signals at the LASER1 and LASER2 output ports 
            /// <para>are active-low (the signal level can be defined by set_laser_control).</para>
            /// </summary>
            LASER1_2_PORT_TTL_LEV,
            /// <summary>
            /// = 1: Since the last call of get_startstop_info, at least one external list 
            /// <para>start has failed (more external starts were triggered than could be </para>
            /// <para>simultaneously held in the 8-start wait loop)</para>
            /// </summary>
            EXT_LIST_START_FAIL,
            /// <summary>
            ///  Ext-Start level (logical AND operation of the signals /START, /START2 and /Slave-START):
            /// <para>= 1: No start signals are currently present at the inputs or the inputs are not connected.</para>
            /// <para>= 0: A start signal is present at at least one input</para>
            /// </summary>
            EXT_START_LEV,
            /// <summary>
            /// = 1: The TTL laser control signal at the LASERON output port is active-low 
            /// <para>(the signal level can be defined by set_laser_control).</para>
            /// </summary>
            LASER_ON_PORT_TTL_LEV

        }

    }
}
