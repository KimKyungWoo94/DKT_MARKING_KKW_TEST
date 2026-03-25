using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzIna.GUI.UserControls;
using System.Diagnostics;

namespace EzIna.Scanner.GUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmScanlabRTC : Form
    {
        Color m_DGV_ForeColor = Color.Black;
        Color m_DGV_SetAbleForeColor = Color.DodgerBlue;
        Color m_DGV_BackColor = SystemColors.Window;
        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
        ScanlabRTC5 pItem;
        NumberKeypad m_FrmNumberKeyPad;
        EzIna.DataMatrix.DM m_DataMatrixItem;
        Task m_pDataMatrixCreating;
        //ComboBox m_CB_Param_LaserMode;
        //ComboBox m_CB_Param_LaserOnSignalLev;
        //ComboBox m_CB_Param_Laser1_2PortSignalLev;
        //ComboBox m_CB_Param_LaserEXTSignalLev;
        double m_CMD_X;
        double m_CMD_Y;
        int m_iCMD_ORG_X;
        int m_iCMD_ORG_Y;
        uint m_LaserOnTime;
        Dictionary<DGV_PARAM_IDX, object> m_SetParamList;


        enum DGV_HEAD_STATUS_ROW_IDX : int
        {
            ENABLE,
            POWER,
            TEMP,
            POS_ACK_X,
            POS_ACK_Y,
            SEL_TABLE_NUM,
            CUR_X,
            CUR_Y,
            CMD_X,
            CMD_Y,
            ACT_X,
            ACT_Y,
            ERR_X,
            ERR_Y,
            ENC_X,
            ENC_Y,
            GALVO_X_TEMP,
            GALVO_Y_TEMP,
            SERVO_X_TEMP,
            SERVO_Y_TEMP,
        }
        enum DGV_LIST_STATUS_IDX : int
        {
            LIST1_LOAD,
            LIST1_READY,
            LIST1_BUSY,
            LIST2_LOAD,
            LIST2_READY,
            LIST2_BUSY,
            LASER_ACTIVE,
        }
        enum DGV_EXELIST_STATUS_IDX : int
        {
            BUSY,
            INT_BUSY,
            PAUSED,
            READY,
            CURRENT_POS,
            Wait_CMD_POS,
        }
        enum DGV_CMD_IDX : int
        {
            CMD_X,
            CMD_Y,
            CMD_ORG_X,
            CMD_ORG_Y,
            MOVE_XY,
            MOVE_ORG_XY,
            SET_ZERO_BOTH,
            DIGIT_1,
            DIGIT_2,
        }
        enum DGV_OPER_IDX : int
        {
            LASER_ON,
            LASER_OFF,
            LASER_TIME_SET,
            LASER_TIME_ON,
            JUMP_SPEED_CTRL,
            MARK_SPEED_CTRL,
            LASER_ON_DELAY,
            LASER_OFF_DELAY,
        }
        enum DGV_PARAM_IDX
        {
            LIST1_SIZE,
            LIST2_SIZE,
            LASER_MODE,
            LASER_1ST_PULSE_KILLER,
            QSWITCH_DELAY,
            STANDBY_HALF_PERIOD,
            STANDBY_FREQ,
            STANDBY_PULSE_LEN,
            FREQ_HALF_PERIOD,
            FREQ_FREQ,
            FREQ_PULSE_WIDTH,
            POLYGON_DELAY,
            MINJUMP_DELAY,
            JUMP_LEN_LIMIT,
            JUMP_DELAY,
            MARK_DELAY,
            JUMP_SPEED,
            MARK_SPEED,

            PULSE_SWITCH,
            PHASE_SHIFT,
            ENABLE_LASER_ACTIVE,
            LASER_ON_ACTIVE_LEV,
            LASER_PORT_SIGNAL_LEV,
            LASER_EXT_SIGNAL_LEV,
            OUTPUT_SYNC_SWITCH,
            CONSTANT_PULSE_LEN_MODE,
        }
        enum DGV_CELL_TYPE
        {
            NONE,
            COMBOBOX,
            TOGGLE,
            BUTTON,
        }

        /// <summary>
        /// 
        /// </summary>
        public FrmScanlabRTC()
        {
            InitializeComponent();
            SetControlDoubleBuffered(tbLayoutPanel_Status, true);
            foreach (Control pControl in tbLayoutPanel_Status.Controls)
            {
                if (pControl.GetType() == typeof(DataGridView))
                {

                    InitializeDGVStyle(pControl as DataGridView);
                    AddDGV_2ParamColums(pControl as DataGridView);
                    SetControlDoubleBuffered(pControl, true);
                    (pControl as DataGridView).CurrentCell = null;
                    (pControl as DataGridView).SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
                    (pControl as DataGridView).Scroll += DGV_Scroll;
                }
            }
            InitDGV(ref DGV_HEAD_CMD);
            InitDGV(ref DGV_OPERATION);
            InitDGV(ref DGV_PARAM);
            m_SetParamList = new Dictionary<DGV_PARAM_IDX, object>();
            AddDGV_2ParamColums(DGV_HEAD_CMD);
            AddDGV_2ParamColums(DGV_OPERATION);
            AddDGVApplyAbleColums(DGV_PARAM);
            this.TopMost = false;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            #region Head Status                    
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.TOGGLE, false, "", "Enable");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.TOGGLE, false, "", "Power");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.TOGGLE, false, "", "Temperature");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.TOGGLE, false, "", "X Done");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.TOGGLE, false, "", "Y Done");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "Corr Table No");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.TOGGLE, false, "", "Enable");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.TOGGLE, false, "", "Power");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.TOGGLE, false, "", "Temperature");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.TOGGLE, false, "", "X Done");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.TOGGLE, false, "", "Y Done");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "Corr Table No");
            #endregion Head Status
            #region POS TEMP
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "CUR X");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "CUR Y");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "CMD X");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "CMD Y");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ACT X");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ACT Y");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ERR X");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ERR Y");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ENC X");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "", "ENC Y");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "0 ℃", "Galvo X Temp");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "0 ℃", "Galvo Y Temp");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "0 ℃", "Servo X Temp");
            Add2Param(ref DGV_Status_Head_A, DGV_CELL_TYPE.NONE, false, "0 ℃", "Servo Y Temp");


            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "CUR X");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "CUR Y");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "CMD X");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "CMD Y");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ACT X");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ACT Y");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ERR X");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ERR Y");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ENC X");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "", "ENC Y");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "0 ℃", "Galvo X TEMP");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "0 ℃", "Galvo Y TEMP");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "0 ℃", "Servo X TEMP");
            Add2Param(ref DGV_Status_Head_B, DGV_CELL_TYPE.NONE, false, "0 ℃", "Servo Y TEMP");
            #endregion POS TEMP
            #region List
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List1 Load");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List1 Ready");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List1 Busy");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List2 Load");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List2 Ready");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "List2 Busy");
            Add2Param(ref DGV_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "Laser Active");
            #endregion List
            #region EXEList
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "Busy");
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "INT-Busy");
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "Paused");
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.TOGGLE, false, "", "Ready");
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.NONE, false, "", "CUR Pos");
            Add2Param(ref DGV_EXE_LIST_STATUS, DGV_CELL_TYPE.NONE, false, "", "Wait CMD Pos");
            #endregion EXEList         
            #region AXIS Control
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.NONE, true, "0 um", "X");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.NONE, true, "0 um", "Y");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.NONE, true, "0", "ORG X");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.NONE, true, "0", "ORG Y");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.BUTTON, false, "", "Move XY");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.BUTTON, false, "", "Move ORG XY");
            Add2Param(ref DGV_HEAD_CMD, DGV_CELL_TYPE.BUTTON, false, "0 um", "Set Zero Both");
            #endregion AXIS Control
            #region     Operation
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.BUTTON, true, "", "Laser ON");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.BUTTON, true, "", "Laser OFF");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.NONE, true, "", "Laser On Time");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.BUTTON, true, "", "Laser Time ON");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.NONE, true, "", "Jump Speed");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.NONE, true, "", "Mark Speed");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.NONE, true, "0.00 us", "LaserOnDelay");
            Add2Param(ref DGV_OPERATION, DGV_CELL_TYPE.NONE, true, "0.00 us", "LaserOffDelay");

            #endregion  Operation
            #region     Param
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "List1 MemorySize", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "List2 MemorySize", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX, "Laser Mode", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "Laser FitstPulseKiller", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "QSwitchDelay", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "StandByHalfPeriod", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "StandByFrequency", "0.00 Hz", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "StandByPulseLength", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "FreQHalfPeriod", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "FreQ_Frequency", "0.00 Hz", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "FreQPulseLength", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "PonlgonDelay", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "MinJumpDelay", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "JumpLengthLimit", "0.00 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "JumpDelay", "0.000 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "MarkDelay", "0.000 us", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "JumpSpeed", "0.000 mm/sec", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "MarkSpeed", "0.000 mm/sec", true);

            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.TOGGLE, DGV_CELL_TYPE.TOGGLE, "PulseSwitch", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.TOGGLE, DGV_CELL_TYPE.TOGGLE, "PhaseShiftEnable", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.TOGGLE, DGV_CELL_TYPE.TOGGLE, "EnableLaserActive", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX, "LaserOnActiveLevel", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX, "LaserPortSignalLevel", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX, "LaserEXT_SignalLevel", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.TOGGLE, DGV_CELL_TYPE.TOGGLE, "OutputSyncSwitch", "", true);
            AddApplyAbleParam(ref DGV_PARAM, DGV_CELL_TYPE.TOGGLE, DGV_CELL_TYPE.TOGGLE, "ConstantPulseLengthMode", "", true);
            #endregion Param
            m_FrmNumberKeyPad = new NumberKeypad();
            DataGridViewComboBoxCell LaserModeCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_MODE] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserOnSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserPortSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserEXTSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV] as DataGridViewComboBoxCell);
            //LaserModeCombo.Items.AddRange(Enum.GetNames(typeof(ScanlabRTC5.LASER_MODE)));            
            LaserModeCombo.DataSource = Enum.GetValues(typeof(ScanlabRTC5.LASER_MODE));
            LaserModeCombo.ValueType = typeof(ScanlabRTC5.LASER_MODE);
            LaserOnSignalLevCombo.Items.Add("Active-High");
            LaserOnSignalLevCombo.Items.Add("Active-Low");
            LaserPortSignalLevCombo.Items.Add("Active-High");
            LaserPortSignalLevCombo.Items.Add("Active-Low");
            LaserEXTSignalLevCombo.Items.Add("Falling Edge");
            LaserEXTSignalLevCombo.Items.Add("Rising Edge");
            SetControlDoubleBuffered(panel_DataMatrixDraw, true);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Item"></param>
        public void InitForm(ScanlabRTC5 a_Item)
        {
            if (a_Item == null)
                return;

            pItem = a_Item;
            DataGridViewComboBoxCell LaserModeCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_MODE] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserOnSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserPortSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell LaserEXTSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV] as DataGridViewComboBoxCell);
            //LaserModeCombo.Value=pItem.ConfigData.LaserMode.ToString();
            LaserModeCombo.Value = pItem.ConfigData.LaserMode;
            LaserOnSignalLevCombo.Value = LaserOnSignalLevCombo.Items[pItem.ConfigData.LaserOnActiveLevel == true ? 1 : 0].ToString();
            LaserPortSignalLevCombo.Value = LaserPortSignalLevCombo.Items[pItem.ConfigData.LaserOutputPortSignalLevel == true ? 1 : 0].ToString();
            LaserEXTSignalLevCombo.Value = LaserEXTSignalLevCombo.Items[pItem.ConfigData.LaserOnEXT_SignalPulse == true ? 1 : 0].ToString();
        }
        #region GUI Control Initialize

        private void InitDGV(ref DataGridView a_DatagirdView)
        {
            InitializeDGVStyle(a_DatagirdView);
            SetControlDoubleBuffered(a_DatagirdView, true);
            a_DatagirdView.CurrentCell = null;
            a_DatagirdView.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
            a_DatagirdView.Scroll += DGV_Scroll;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_DatagirdView"></param>
        private void InitializeDGVStyle(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView.RowCount > 0 || a_DatagirdView.ColumnCount > 0)
            {
                a_DatagirdView.Columns.Clear();
                a_DatagirdView.Rows.Clear();
            }
            a_DatagirdView.Font = new Font("Century Gothic", 13F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ReadOnly = false;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = false;
            a_DatagirdView.ColumnHeadersVisible = false;
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            a_DatagirdView.ColumnHeadersHeight = 25;
            a_DatagirdView.RowTemplate.Height = 25;

            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.EnableHeadersVisualStyles = false;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ScrollBars = ScrollBars.None;
            a_DatagirdView.ClearSelection();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strHeaderTxt"></param>
        /// <param name="a_strBindingPropTxt"></param>
        /// <param name="a_Width"></param>
        /// <returns></returns>
        private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt, int a_Width)
        {
            DataGridViewTextBoxColumn pRet = new DataGridViewTextBoxColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;
            pRet.DefaultCellStyle.SelectionBackColor = m_DGV_SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = m_DGV_SelectionForeColor;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private DataGridViewButtonColumn CreateDataGridViewButtonColum(string a_strHeaderTxt, string a_strBindingPropTxt, string a_ButtonText, int a_Width = 100)
        {
            DataGridViewButtonColumn pRet = new DataGridViewButtonColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.Text = a_ButtonText;
            pRet.UseColumnTextForButtonValue = true;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private void AddDGV_2ParamColums(DataGridView a_DatagirdView)
        {
            a_DatagirdView.Columns.AddRange(CreateDataGridViewTextColumn("Title", "", (int)(a_DatagirdView.Width * 0.6)),
                                                                            CreateDataGridViewTextColumn("Value", "", (int)(a_DatagirdView.Width * 0.4))
                                                                            );
        }
        private void AddDGVApplyAbleColums(DataGridView a_DatagirdView)
        {
            a_DatagirdView.Columns.AddRange(CreateDataGridViewTextColumn("Title", "", (int)(a_DatagirdView.Width * 0.4)),
                                                                            CreateDataGridViewTextColumn("Display", "", (int)(a_DatagirdView.Width * 0.23)),
                                                                            CreateDataGridViewTextColumn("SetValue", "", (int)(a_DatagirdView.Width * 0.23)),
                                                                            CreateDataGridViewButtonColum("Apply", "", "Apply", (int)(a_DatagirdView.Width * 0.14))
                                                                            );
        }
        private void Add2Param(ref DataGridView pDGV,
                                                     DGV_CELL_TYPE a_Type,
                                                     bool a_bSetable,
                                                     string a_strFormat,
                                                     string a_Title
                                                     )
        {
            pDGV.Rows.Add();
            pDGV["Title", pDGV.Rows.Count - 1].Style.ForeColor = a_bSetable == true ? m_DGV_SetAbleForeColor : m_DGV_ForeColor;
            pDGV["Title", pDGV.Rows.Count - 1].Value = a_Title;
            pDGV["Value", pDGV.Rows.Count - 1].Style.Format = a_strFormat;

            switch (a_Type)
            {
                case DGV_CELL_TYPE.COMBOBOX:
                    {
                        DataGridViewComboBoxCell pTemp = new DataGridViewComboBoxCell();
                        pDGV["Value", pDGV.Rows.Count - 1] = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.TOGGLE:
                    {
                        DGVToggleButtonCell pTemp = new DGVToggleButtonCell();
                        pTemp.ButtonStyle = DGVToggleButtonCell.ToggleSwitchStyle.IOS5;
                        pDGV["Value", pDGV.Rows.Count - 1] = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.BUTTON:
                    {
                        DataGridViewButtonCell pTemp = new DataGridViewButtonCell();
                        pDGV["Value", pDGV.Rows.Count - 1] = pTemp;
                    }
                    break;
                default:
                    break;
            }
        }
        private void AddApplyAbleParam(ref DataGridView pDGV,
                                                                     DGV_CELL_TYPE a_DisplayCelType,
                                                                     DGV_CELL_TYPE a_SetValueCelType,
                                                                     string a_TitleName,
                                                                     string a_strFormat,
                                                                     bool a_bSetable
                                                                     )
        {
            DataGridViewCell pDisplayCell = null;
            DataGridViewCell pSetValueCell = null;
            pDGV.Rows.Add();
            pDGV["Title", pDGV.Rows.Count - 1].Style.ForeColor = a_bSetable == true ? m_DGV_SetAbleForeColor : m_DGV_ForeColor;
            pDGV["Title", pDGV.Rows.Count - 1].Value = a_TitleName;
            pDGV["Display", pDGV.Rows.Count - 1].Style.Format = a_strFormat;
            pDGV["Display", pDGV.Rows.Count - 1].ReadOnly = true;
            pDGV["SetValue", pDGV.Rows.Count - 1].Style.Format = a_strFormat;
            pDisplayCell = CreateDataGridCell(a_DisplayCelType);
            pSetValueCell = CreateDataGridCell(a_SetValueCelType);
            if (pDisplayCell != null)
            {
                pDGV["Display", pDGV.Rows.Count - 1] = pDisplayCell;

            }
            if (pSetValueCell != null)
            {
                pDGV["SetValue", pDGV.Rows.Count - 1] = pSetValueCell;
                pDGV["SetValue", pDGV.Rows.Count - 1].ReadOnly = false;
            }
            else
            {
                pDGV["SetValue", pDGV.Rows.Count - 1].ReadOnly = true;
            }
        }
        private DataGridViewCell CreateDataGridCell(DGV_CELL_TYPE a_CellType)
        {
            DataGridViewCell pRet = null;
            switch (a_CellType)
            {
                case DGV_CELL_TYPE.COMBOBOX:
                    {
                        DataGridViewComboBoxCell pTemp = new DataGridViewComboBoxCell();
                        pTemp.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        pRet = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.TOGGLE:
                    {
                        DGVToggleButtonCell pTemp = new DGVToggleButtonCell();
                        pTemp.ButtonStyle = DGVToggleButtonCell.ToggleSwitchStyle.IOS5;
                        pRet = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.BUTTON:
                    {
                        DataGridViewButtonCell pTemp = new DataGridViewButtonCell();
                        pRet = pTemp;
                    }
                    break;
            }
            return pRet;
        }
        private void InitializeCombo(out ComboBox a_Control, Control a_Parent, string a_strName)
        {
            a_Control = new ComboBox();
            a_Control.Name = a_strName;
            a_Control.DropDownStyle = ComboBoxStyle.DropDownList;
            a_Control.BringToFront();
            a_Control.Parent = a_Parent;
            a_Control.Hide();
            a_Control.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
            a_Control.ItemHeight = 25;
            a_Control.MouseLeave += Combo_MouseLeave;
            a_Control.DropDownClosed += Combo_DropDownClosed;
        }
        private void Combo_MouseLeave(object sender, EventArgs e)
        {

            ComboBox pControl = sender as ComboBox;
            if (pControl != null)
                pControl.Hide();
        }
        private void Combo_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox pControl = sender as ComboBox;
            if (pControl != null)
                pControl.Hide();

        }
        #endregion GUI Control Initialize
        private void FrmScanlabRTC_Load(object sender, EventArgs e)
        {

            DGV_PARAM.ScrollBars = ScrollBars.Both;

            TabScanner.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateDisplay()
        {
            if (pItem != null)
            {
                DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENABLE].Value = pItem.ConfigData.EnableHeadA;
                DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENABLE].Value = pItem.ConfigData.EnableHeadB;
                if (pItem.ConfigData.EnableHeadA)
                {
                    #region     HEAD_A_STATUS
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SEL_TABLE_NUM].Value = pItem.ConfigData.CorrTableNum_Head_A;
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POWER].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.A, ScanlabRTC5.HEAD_STATUS.POWER_STATUS);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.TEMP].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.A, ScanlabRTC5.HEAD_STATUS.TEMP_STATUS);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POS_ACK_X].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.A, ScanlabRTC5.HEAD_STATUS.POS_ACK_X);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POS_ACK_Y].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.A, ScanlabRTC5.HEAD_STATUS.POS_ACK_Y);
                    #endregion HEAD_A_STATUS  
                    #region     HEAD_A_POS_TEMP
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CUR_X].Value = pItem.GetCurrentPositionX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CUR_Y].Value = pItem.GetCurrentPositionY(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CMD_X].Value = pItem.GetCommandPositionX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CMD_Y].Value = pItem.GetCommandPositionY(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ACT_X].Value = pItem.GetActualPositionX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ACT_Y].Value = pItem.GetActualPositionY(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ERR_X].Value = pItem.GetErrorPositionX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ERR_Y].Value = pItem.GetErrorPositionY(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENC_X].Value = pItem.GetEncoderX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENC_Y].Value = pItem.GetEncoderY(ScanlabRTC5.RTC_HEAD.A);

                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.GALVO_X_TEMP].Value = pItem.GetGalvanoMirrorTempX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.GALVO_Y_TEMP].Value = pItem.GetGalvanoMirrorTempY(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SERVO_X_TEMP].Value = pItem.GetServoBoardTempX(ScanlabRTC5.RTC_HEAD.A);
                    DGV_Status_Head_A["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SERVO_Y_TEMP].Value = pItem.GetServoBoardTempY(ScanlabRTC5.RTC_HEAD.A);
                    #endregion  HEAD_A_POS_TEMP
                }
                if (pItem.ConfigData.EnableHeadB)
                {
                    #region    HEAD_B_STATUS
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SEL_TABLE_NUM].Value = pItem.ConfigData.CorrTableNum_Head_B;
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POWER].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.B, ScanlabRTC5.HEAD_STATUS.POWER_STATUS);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.TEMP].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.B, ScanlabRTC5.HEAD_STATUS.TEMP_STATUS);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POS_ACK_X].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.B, ScanlabRTC5.HEAD_STATUS.POS_ACK_X);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.POS_ACK_Y].Value = pItem.GetHeadStatus(ScanlabRTC5.RTC_HEAD.B, ScanlabRTC5.HEAD_STATUS.POS_ACK_Y);
                    #endregion HEAD_B_STATUS
                    #region     HEAD_B_POS_TEMP
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CUR_X].Value = pItem.GetCurrentPositionX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CUR_Y].Value = pItem.GetCurrentPositionY(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CMD_X].Value = pItem.GetCommandPositionX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.CMD_Y].Value = pItem.GetCommandPositionY(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ACT_X].Value = pItem.GetActualPositionX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ACT_Y].Value = pItem.GetActualPositionY(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ERR_X].Value = pItem.GetErrorPositionX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ERR_Y].Value = pItem.GetErrorPositionY(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENC_X].Value = pItem.GetEncoderX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.ENC_Y].Value = pItem.GetEncoderY(ScanlabRTC5.RTC_HEAD.B);

                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.GALVO_X_TEMP].Value = pItem.GetGalvanoMirrorTempX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.GALVO_Y_TEMP].Value = pItem.GetGalvanoMirrorTempY(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SERVO_X_TEMP].Value = pItem.GetServoBoardTempX(ScanlabRTC5.RTC_HEAD.B);
                    DGV_Status_Head_B["Value", (int)DGV_HEAD_STATUS_ROW_IDX.SERVO_Y_TEMP].Value = pItem.GetServoBoardTempY(ScanlabRTC5.RTC_HEAD.B);
                    #endregion  HEAD_B_POS_TEMP
                }
                #region  List Status
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST1_LOAD].Value = pItem.GetListStatus_Load(ScanlabRTC5.RTC_LIST._1st);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST1_READY].Value = pItem.GetListStatus_READY(ScanlabRTC5.RTC_LIST._1st);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST1_BUSY].Value = pItem.GetListStatus_BUSY(ScanlabRTC5.RTC_LIST._1st);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST2_LOAD].Value = pItem.GetListStatus_Load(ScanlabRTC5.RTC_LIST._2nd);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST2_READY].Value = pItem.GetListStatus_READY(ScanlabRTC5.RTC_LIST._2nd);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LIST2_BUSY].Value = pItem.GetListStatus_BUSY(ScanlabRTC5.RTC_LIST._2nd);
                DGV_LIST_STATUS["Value", (int)DGV_LIST_STATUS_IDX.LASER_ACTIVE].Value = pItem.GetStartStopInfo(ScanlabRTC5.GET_START_STOP_INFO.LASER_ACTIVE_ENABLE);
                #endregion List Status
                #region EXE List Status
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.BUSY].Value = pItem.IsExecuteList_BUSY;
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.INT_BUSY].Value = pItem.IsExecuteList_InternalBUSY;
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.PAUSED].Value = pItem.IsExecuteList_Paused;
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.READY].Value = pItem.IsExecuteList_Ready;
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.CURRENT_POS].Value = pItem.iCurrentExecuteListPos;
                DGV_EXE_LIST_STATUS["Value", (int)DGV_EXELIST_STATUS_IDX.Wait_CMD_POS].Value = pItem.iWaitCMDPosExecuteList;
                #endregion Exe List Status
                #region CMD
                DGV_HEAD_CMD["Value", (int)DGV_CMD_IDX.CMD_X].Value = m_CMD_X;
                DGV_HEAD_CMD["Value", (int)DGV_CMD_IDX.CMD_Y].Value = m_CMD_Y;
                DGV_HEAD_CMD["Value", (int)DGV_CMD_IDX.CMD_ORG_X].Value = m_iCMD_ORG_X;
                DGV_HEAD_CMD["Value", (int)DGV_CMD_IDX.CMD_ORG_Y].Value = m_iCMD_ORG_Y;
                #endregion CMD
                #region Operation
                DGV_OPERATION["Value", (int)DGV_OPER_IDX.LASER_TIME_SET].Value = m_LaserOnTime;
                DGV_OPERATION["Value", (int)DGV_OPER_IDX.LASER_ON_DELAY].Value = pItem.ConfigData.LaserOnDelay;
                DGV_OPERATION["Value", (int)DGV_OPER_IDX.LASER_OFF_DELAY].Value = pItem.ConfigData.LaserOffDelay;

                #endregion Operation
                #region     Param
                DataGridViewComboBoxCell LaserModeCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_MODE] as DataGridViewComboBoxCell);
                DataGridViewComboBoxCell LaserOnSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV] as DataGridViewComboBoxCell);
                DataGridViewComboBoxCell LaserPortSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV] as DataGridViewComboBoxCell);
                DataGridViewComboBoxCell LaserEXTSignalLevCombo = (DGV_PARAM["SetValue", (int)DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV] as DataGridViewComboBoxCell);

                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LIST1_SIZE].Value = pItem.ConfigData.List_1stMemorySize;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LIST2_SIZE].Value = pItem.ConfigData.List_2ndMemorySize;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LASER_MODE].Value = pItem.ConfigData.LaserMode.ToString();
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LASER_1ST_PULSE_KILLER].Value = pItem.ConfigData.FirstPulseKillerLength;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.QSWITCH_DELAY].Value = pItem.ConfigData.QSwitchDelay;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.STANDBY_HALF_PERIOD].Value = pItem.ConfigData.StandbyHalfPeriod;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.STANDBY_FREQ].Value = pItem.ConfigData.StandByFrequency;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.STANDBY_PULSE_LEN].Value = pItem.ConfigData.StandbyPulseLength;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.FREQ_HALF_PERIOD].Value = pItem.ConfigData.FreQHalfPeriod;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.FREQ_FREQ].Value = pItem.ConfigData.FreQuency;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.FREQ_PULSE_WIDTH].Value = pItem.ConfigData.FreQPulseLength;

                DGV_PARAM["Display", (int)DGV_PARAM_IDX.POLYGON_DELAY].Value = pItem.ConfigData.PolygonDelay;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.MINJUMP_DELAY].Value = pItem.ConfigData.MinimumJumpDelay;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.JUMP_LEN_LIMIT].Value = pItem.ConfigData.JumpLengthLimit;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.JUMP_DELAY].Value = pItem.ConfigData.JumpDelay;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.MARK_DELAY].Value = pItem.ConfigData.MarkDelay;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.JUMP_SPEED].Value = pItem.ConfigData.JumpSpeed;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.MARK_SPEED].Value = pItem.ConfigData.MarkSpeed;

                DGV_PARAM["Display", (int)DGV_PARAM_IDX.PULSE_SWITCH].Value = pItem.ConfigData.PulseSwitchSettingEnable;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.PHASE_SHIFT].Value = pItem.ConfigData.PhaseShiftEnable;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.ENABLE_LASER_ACTIVE].Value = pItem.ConfigData.LaserActiveEnable;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV].Value = GetDGV_ComboCellValueString(DGV_PARAM, "SetValue", (int)DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV, pItem.ConfigData.LaserOnActiveLevel == true ? 1 : 0).ToString();
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV].Value = GetDGV_ComboCellValueString(DGV_PARAM, "SetValue", (int)DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV, pItem.ConfigData.LaserOutputPortSignalLevel == true ? 1 : 0).ToString();
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV].Value = GetDGV_ComboCellValueString(DGV_PARAM, "SetValue", (int)DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV, pItem.ConfigData.LaserOnEXT_SignalPulse == true ? 1 : 0).ToString();
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.OUTPUT_SYNC_SWITCH].Value = pItem.ConfigData.OutputSyncchronization;
                DGV_PARAM["Display", (int)DGV_PARAM_IDX.CONSTANT_PULSE_LEN_MODE].Value = pItem.ConfigData.ConstantPulseLengthMode;
                #endregion  Param   
                LED_LaserConnectorOut_1.Value = pItem.GetLaserConnector_Output(ScanlabRTC5.LaserConnector_Out.OUT_1);
                LED_LaserConnectorOut_2.Value = pItem.GetLaserConnector_Output(ScanlabRTC5.LaserConnector_Out.OUT_2);
            }
        }
        private string GetDGV_ComboCellValueString(DataGridView pControl, string a_strColName, int a_RowIdx, int a_ItemIDX)
        {
            string pRetstr = "";
            try
            {
                DataGridViewComboBoxCell pCombo = (pControl[a_strColName, (int)a_RowIdx] as DataGridViewComboBoxCell);
                if (pCombo != null)
                {
                    if (pCombo.Items.Count > a_ItemIDX)
                    {
                        pRetstr = pCombo.Items[a_ItemIDX].ToString();
                    }
                }
            }
            catch
            {

            }

            return pRetstr;
        }
        private int GetDGV_ComboSelectedIDX(DataGridView pControl, string a_strColName, int a_RowIdx)
        {
            DataGridViewComboBoxCell pCombo = pControl[a_strColName, a_RowIdx] as DataGridViewComboBoxCell;
            if (pCombo != null)
                return pCombo.Items.IndexOf(pCombo.Value);
            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateFilker()
        {

        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Control pControl = sender as Control;
            if (pItem == null)
                return;


        }

        private void Frm_SizeChanged(object sender, EventArgs e)
        {
            DataGridView pDataGrid;
            foreach (Control pControl in tbLayoutPanel_Status.Controls)
            {
                if (pControl.GetType() == typeof(DataGridView))
                {
                    pDataGrid = pControl as DataGridView;
                    pDataGrid.Columns["Title"].Width = (int)(pDataGrid.Width * 0.6);
                    pDataGrid.Columns["Value"].Width = (int)(pDataGrid.Width * 0.4);
                }

            }
        }

        #region Like Cell Merge
        bool IsTheSameCellValue(int column, int row, DataGridView a_DGV)
        {
            DataGridViewCell cell1 = a_DGV[column, row];
            DataGridViewCell cell2 = a_DGV[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
        private void DGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView pControl = sender as DataGridView;
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {

                    if (e.RowIndex < pControl.Rows.Count - 1)
                    {
                        if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex + 1, pControl))
                        {
                            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                        }
                    }
                    if (e.RowIndex < 1 || e.ColumnIndex < 0)
                        return;

                    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, pControl))
                    {
                        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

                    }
                    else
                    {
                        e.AdvancedBorderStyle.Top = pControl.AdvancedCellBorderStyle.Top;
                        //e.AdvancedBorderStyle.Bottom = DGV_LaserParam.AdvancedCellBorderStyle.Bottom;                      
                    }
                }

            }
        }

        private void DGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView pControl = sender as DataGridView;
            if (e.ColumnIndex == 0)// <==조건문으로 열선택
            {
                if (e.RowIndex == 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, pControl))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }
        private void DataGridVeiwNoSelection_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView pcontrol = sender as DataGridView;
            pcontrol.ClearSelection();
        }
        #endregion
        private void SetControlDoubleBuffered(Control ctrl, bool setting)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }
        private void DGV_CMD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (pItem == null)
                return;
            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                return;
            if (DGV_HEAD_CMD.Columns["Value"].Index == e.ColumnIndex)
            {
                DGV_CMD_IDX IDX = (DGV_CMD_IDX)e.RowIndex;
                switch (IDX)
                {
                    case DGV_CMD_IDX.CMD_X:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(pItem.Move_X_NegativeLimit, pItem.Move_X_PositiveLimit, m_CMD_X) == DialogResult.OK)
                            {
                                m_CMD_X = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_CMD_IDX.CMD_Y:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(pItem.Move_Y_NegativeLimit, pItem.Move_Y_PositiveLimit, m_CMD_Y) == DialogResult.OK)
                            {
                                m_CMD_Y = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_CMD_IDX.CMD_ORG_X:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(ScanlabRTC5.MOVE_Negative_LIMIT, ScanlabRTC5.MOVE_Positive_LIMIT, m_iCMD_ORG_X) == DialogResult.OK)
                            {
                                m_iCMD_ORG_X = (int)m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_CMD_IDX.CMD_ORG_Y:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(ScanlabRTC5.MOVE_Negative_LIMIT, ScanlabRTC5.MOVE_Positive_LIMIT, m_iCMD_ORG_Y) == DialogResult.OK)
                            {
                                m_iCMD_ORG_Y = (int)m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_CMD_IDX.MOVE_XY:
                        {
                            pItem.Move(m_CMD_X, m_CMD_Y);
                        }
                        break;
                    case DGV_CMD_IDX.MOVE_ORG_XY:
                        {
                            pItem.MoveORG(m_iCMD_ORG_X, m_iCMD_ORG_Y);
                        }
                        break;
                    case DGV_CMD_IDX.SET_ZERO_BOTH:
                        {
                            pItem.Move(0, 0);
                        }
                        break;
                    default:
                        break;
                }
                m_FrmNumberKeyPad.Result = 0.0;
            }
        }
        private void DGV_OPERATION_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (pItem == null)
                return;

            DGV_OPER_IDX IDX = (DGV_OPER_IDX)e.RowIndex;
            if (DGV_OPERATION.Columns["Value"].Index == e.ColumnIndex)
            {
                switch (IDX)
                {
                    case DGV_OPER_IDX.LASER_ON:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (MessageBox.Show("Would like Laser On?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                pItem.LaserOn();
                            }
                        }
                        break;
                    case DGV_OPER_IDX.LASER_OFF:
                        {
                            if (MessageBox.Show("Would like Laser Off?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                pItem.LaserOff();
                            }
                        }
                        break;
                    case DGV_OPER_IDX.LASER_TIME_SET:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(0, uint.MaxValue / 10 / 10000, 0) == DialogResult.OK)
                            {
                                m_LaserOnTime = (uint)m_FrmNumberKeyPad.Result;
                                m_FrmNumberKeyPad.Result = 0.0;
                            }
                        }
                        break;
                    case DGV_OPER_IDX.LASER_TIME_ON:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (MessageBox.Show("Would like Laser Time On?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                pItem.GateOnTime(m_LaserOnTime);
                            }
                        }
                        break;
                    case DGV_OPER_IDX.JUMP_SPEED_CTRL:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (m_FrmNumberKeyPad.ShowDialog(pItem.MinJump_MarkSpeed, pItem.MaxJump_MarkSpeed, 0) == DialogResult.OK)
                            {
                                pItem.SetJumpSpeed(m_FrmNumberKeyPad.Result);
                            }
                        }
                        break;
                    case DGV_OPER_IDX.MARK_SPEED_CTRL:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (m_FrmNumberKeyPad.ShowDialog(pItem.MinJump_MarkSpeed, pItem.MaxJump_MarkSpeed, 0) == DialogResult.OK)
                            {
                                pItem.SetMarkSpeed(m_FrmNumberKeyPad.Result);
                            }
                        }
                        break;
                    case DGV_OPER_IDX.LASER_ON_DELAY:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (m_FrmNumberKeyPad.ShowDialog(ScanlabRTC5.MinLaserOnDelay, ScanlabRTC5.MaxLaserOnDelay, pItem.ConfigData.LaserOnDelay) == DialogResult.OK)
                            {
                                pItem.ConfigData.LaserOnDelay = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_OPER_IDX.LASER_OFF_DELAY:
                        {
                            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                                return;
                            if (m_FrmNumberKeyPad.ShowDialog(ScanlabRTC5.MinLaserOffDelay, ScanlabRTC5.MaxLaserOffDelay, pItem.ConfigData.LaserOnDelay) == DialogResult.OK)
                            {
                                pItem.ConfigData.LaserOffDelay = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void DGV_Param_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (pItem == null)
                return;
            if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                return;
            DGV_PARAM_IDX IDX = (DGV_PARAM_IDX)e.RowIndex;
            #region SetValue Cell
            if (DGV_PARAM.Columns["SetValue"].Index == e.ColumnIndex)
            {
                Type CellType = DGV_PARAM["SetValue", e.RowIndex].GetType();
                double fMin = 0;
                double fMax = 0;
                double fCurrentValue = 0;
                bool bEnable = false;
                if (CellType == typeof(DataGridViewTextBoxCell))
                {

                    switch (IDX)
                    {
                        case DGV_PARAM_IDX.LIST1_SIZE:
                            {
                                fMin = uint.MinValue;
                                fMax = uint.MaxValue;
                                fCurrentValue = pItem.ConfigData.List_1stMemorySize;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.LIST2_SIZE:
                            {
                                fMin = uint.MinValue;
                                fMax = uint.MaxValue;
                                fCurrentValue = pItem.ConfigData.List_1stMemorySize;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_1ST_PULSE_KILLER:
                            {
                                fMin = ScanlabRTC5.MinFirstPulseKillerLength;
                                fMax = ScanlabRTC5.MaxFirstPulseKillerLength;
                                fCurrentValue = pItem.ConfigData.FirstPulseKillerLength;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.QSWITCH_DELAY:
                            {
                                fMin = ScanlabRTC5.MinQSwitchDelay;
                                fMax = ScanlabRTC5.MaxQSwitchDelay;
                                fCurrentValue = pItem.ConfigData.QSwitchDelay;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_HALF_PERIOD:
                            {
                                fMin = ScanlabRTC5.MinStandbyHalfPeriod;
                                fMax = ScanlabRTC5.MaxStandbyHalfPeriod;
                                fCurrentValue = pItem.ConfigData.StandbyHalfPeriod;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_FREQ:
                            {
                                fMin = ScanlabRTC5.MinStandByFrequency;
                                fMax = ScanlabRTC5.MaxStandByFrequency;
                                fCurrentValue = pItem.ConfigData.StandByFrequency;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_PULSE_LEN:
                            {
                                fMin = ScanlabRTC5.MinStandbyPulseLength;
                                fMax = ScanlabRTC5.MaxStandbyPulseLength;
                                fCurrentValue = pItem.ConfigData.StandbyPulseLength;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_HALF_PERIOD:
                            {
                                fMin = ScanlabRTC5.MinFreQHalfPeriod;
                                fMax = ScanlabRTC5.MaxFreQHalfPeriod;
                                fCurrentValue = pItem.ConfigData.FreQHalfPeriod;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_FREQ:
                            {
                                fMin = ScanlabRTC5.MinFreQuency;
                                fMax = ScanlabRTC5.MaxFreQuency;
                                fCurrentValue = pItem.ConfigData.FreQuency;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_PULSE_WIDTH:
                            {
                                fMin = ScanlabRTC5.MinFreQPulseLength;
                                fMax = ScanlabRTC5.MaxFreQPulseLength;
                                fCurrentValue = pItem.ConfigData.FreQPulseLength;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.POLYGON_DELAY:
                            {
                                fMin = ScanlabRTC5.MinScannerDelays;
                                fMax = ScanlabRTC5.MaxScannerDelays;
                                fCurrentValue = pItem.ConfigData.PolygonDelay;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.MINJUMP_DELAY:
                            {
                                fMin = ScanlabRTC5LinkData.Min_MinimumJumpDelay;
                                fMax = ScanlabRTC5LinkData.Max_MinimumJumpDelay;
                                fCurrentValue = pItem.ConfigData.MinimumJumpDelay;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_LEN_LIMIT:
                            {
                                fMin = uint.MinValue;
                                fMax = uint.MaxValue;
                                fCurrentValue = pItem.ConfigData.JumpLengthLimit;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_DELAY:
                            {
                                fMin = pItem.ConfigData.MinJumpDelay;
                                fMax = pItem.ConfigData.MaxJumpDelay;
                                fCurrentValue = pItem.ConfigData.JumpDelay;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.MARK_DELAY:
                            {
                                fMin = pItem.ConfigData.MinMarkDelay;
                                fMax = pItem.ConfigData.MaxMarkDelay;
                                fCurrentValue = pItem.ConfigData.MarkDelay;
                                bEnable = true;
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_SPEED:
                        case DGV_PARAM_IDX.MARK_SPEED:
                            {
                                fMin = pItem.MinJump_MarkSpeed;
                                fMax = pItem.MaxJump_MarkSpeed;
                                fCurrentValue = pItem.ConfigData.MarkSpeed;
                                bEnable = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (bEnable)
                    {
                        if (m_FrmNumberKeyPad.ShowDialog(fMin, fMax, fCurrentValue) == DialogResult.OK)
                        {
                            /*if (m_SetParamList.ContainsKey(IDX))
{
    m_SetParamList[IDX] = m_FrmNumberKeyPad.Result;
}
else
{
    m_SetParamList.Add(IDX, m_FrmNumberKeyPad.Result);
}*/
                            DGV_PARAM["SetValue", (int)IDX].Value = m_FrmNumberKeyPad.Result;
                            m_FrmNumberKeyPad.Result = 0.0;
                        }
                    }

                }
            }
            #endregion SetValue Cell
            #region Apply Cell
            else if (DGV_PARAM.Columns["Apply"].Index == e.ColumnIndex)
            {
                if (DGV_PARAM["SetValue", e.RowIndex].GetType() == typeof(DataGridViewTextBoxCell))
                {
                    if (DGV_PARAM["SetValue", (int)IDX].Value == null)
                    {
                        MessageBox.Show("Input Value First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                string strMsg = string.Format("{0} -> {1} Change Value?", DGV_PARAM["Display", (int)IDX].Value, DGV_PARAM["SetValue", (int)IDX].Value);
                if (MessageBox.Show(strMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    switch (IDX)
                    {
                        case DGV_PARAM_IDX.LIST1_SIZE:
                            {
                                pItem.ConfigData.List_1stMemorySize = Convert.ToUInt32(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.LIST2_SIZE:
                            {
                                pItem.ConfigData.List_2ndMemorySize = Convert.ToUInt32(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_MODE:
                            {
                                pItem.ConfigData.LaserMode = (ScanlabRTC5.LASER_MODE)DGV_PARAM["SetValue", e.RowIndex].Value;//(ScanlabRTC5.LASER_MODE)GetDGV_ComboSelectedIDX(DGV_PARAM, "SetValue", e.RowIndex);
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_1ST_PULSE_KILLER:
                            {
                                pItem.ConfigData.FirstPulseKillerLength = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.QSWITCH_DELAY:
                            {
                                pItem.ConfigData.QSwitchDelay = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_HALF_PERIOD:
                            {
                                pItem.ConfigData.StandbyHalfPeriod = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_FREQ:
                            {
                                pItem.ConfigData.StandByFrequency = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.STANDBY_PULSE_LEN:
                            {
                                pItem.ConfigData.StandbyPulseLength = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_HALF_PERIOD:
                            {
                                pItem.ConfigData.FreQHalfPeriod = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_FREQ:
                            {
                                pItem.ConfigData.FreQuency = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.FREQ_PULSE_WIDTH:
                            {
                                pItem.ConfigData.FreQPulseLength = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.POLYGON_DELAY:
                            {
                                pItem.ConfigData.PolygonDelay = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.MINJUMP_DELAY:
                            {
                                pItem.ConfigData.MinimumJumpDelay = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_LEN_LIMIT:
                            {
                                pItem.ConfigData.JumpLengthLimit = Convert.ToUInt32(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_DELAY:
                            {
                                pItem.ConfigData.JumpDelay = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.MARK_DELAY:
                            {
                                pItem.ConfigData.MarkDelay = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.JUMP_SPEED:
                            {
                                pItem.ConfigData.JumpSpeed = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.MARK_SPEED:
                            {
                                pItem.ConfigData.MarkSpeed = Convert.ToDouble(DGV_PARAM["SetValue", e.RowIndex].Value);
                            }
                            break;
                        case DGV_PARAM_IDX.PULSE_SWITCH:
                            {
                                pItem.ConfigData.PulseSwitchSettingEnable = (bool)DGV_PARAM["SetValue", e.RowIndex].Value;
                            }
                            break;
                        case DGV_PARAM_IDX.PHASE_SHIFT:
                            {
                                pItem.ConfigData.PhaseShiftEnable = (bool)DGV_PARAM["SetValue", e.RowIndex].Value;
                            }
                            break;
                        case DGV_PARAM_IDX.ENABLE_LASER_ACTIVE:
                            {
                                pItem.ConfigData.LaserActiveEnable = (bool)DGV_PARAM["SetValue", e.RowIndex].Value;
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_ON_ACTIVE_LEV:
                            {
                                pItem.ConfigData.LaserOnActiveLevel = GetDGV_ComboSelectedIDX(DGV_PARAM, "SetValue", e.RowIndex) > 0;
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_PORT_SIGNAL_LEV:
                            {
                                pItem.ConfigData.LaserOutputPortSignalLevel = GetDGV_ComboSelectedIDX(DGV_PARAM, "SetValue", e.RowIndex) > 0;
                            }
                            break;
                        case DGV_PARAM_IDX.LASER_EXT_SIGNAL_LEV:
                            {
                                pItem.ConfigData.LaserOnEXT_SignalPulse = GetDGV_ComboSelectedIDX(DGV_PARAM, "SetValue", e.RowIndex) > 0;
                            }

                            break;
                        case DGV_PARAM_IDX.OUTPUT_SYNC_SWITCH:
                            {
                                pItem.ConfigData.OutputSyncchronization = (bool)DGV_PARAM["SetValue", e.RowIndex].Value;
                            }
                            break;
                        case DGV_PARAM_IDX.CONSTANT_PULSE_LEN_MODE:
                            {
                                pItem.ConfigData.ConstantPulseLengthMode = (bool)DGV_PARAM["SetValue", e.RowIndex].Value;
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion Apply Cell
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ConfigData.CorrTableNum_Head_A = 0;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ConfigData.CorrTableNum_Head_A = 1;
            }
        }

        private void DGV_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView pControl = sender as DataGridView;
            pControl.Invalidate();
        }
        private void CrossParam_X(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bORG = pControl.Tag?.ToString() == "ORG";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if (bORG)
                    {
                        dMIN = ScanlabRTC5.MOVE_Negative_LIMIT;
                        dMax = ScanlabRTC5.MOVE_Positive_LIMIT;

                    }
                    else
                    {
                        dMIN = pItem.Move_X_NegativeLimit;
                        dMax = pItem.Move_X_PositiveLimit;
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void CrossParam_Y(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bORG = pControl.Tag?.ToString() == "ORG";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if(bORG)
                    {
                        dMIN = ScanlabRTC5.MOVE_Negative_LIMIT;
                        dMax = ScanlabRTC5.MOVE_Positive_LIMIT;

                    }
                    else
                    {
                        dMIN = pItem.Move_Y_NegativeLimit;
                        dMax = pItem.Move_Y_PositiveLimit;
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void CrossParam_SIZE_X(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bORG = pControl.Tag?.ToString() == "ORG";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if (bORG)
                    {
                        dMIN = 0;
                        dMax = Math.Abs(ScanlabRTC5.MOVE_Negative_LIMIT)+ScanlabRTC5.MOVE_Positive_LIMIT;

                    }
                    else
                    {
                        dMIN = 0;
                        dMax = Math.Abs(pItem.Move_X_NegativeLimit) + pItem.Move_X_PositiveLimit; 
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void CrossParam_SIZE_Y(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bORG = pControl.Tag?.ToString() == "ORG";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if (bORG)
                    {
                        dMIN = 0;
                        dMax = Math.Abs(ScanlabRTC5.MOVE_Negative_LIMIT) + ScanlabRTC5.MOVE_Positive_LIMIT;

                    }
                    else
                    {
                        dMIN = 0;
                        dMax = Math.Abs(pItem.Move_Y_NegativeLimit) + pItem.Move_Y_PositiveLimit;
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void DM_CENTER_PARAM_CLICK(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bDIR = pControl.Tag?.ToString() == "X";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if (bDIR)
                    {
                        dMIN = pItem.Move_X_NegativeLimit;
                        dMax = pItem.Move_X_PositiveLimit;

                    }
                    else
                    {
                        dMIN = pItem.Move_Y_NegativeLimit;
                        dMax = pItem.Move_Y_PositiveLimit;
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void DM_SIZE_PARAM_CLICK(object sender, MouseEventArgs e)
        {
            TextBox pControl = sender as TextBox;
            if (pControl != null)
            {
                if (pItem != null)
                {
                    bool bDIR = pControl.Tag?.ToString() == "X";
                    double dMIN = 0.0;
                    double dMax = 0.0;
                    if (bDIR)
                    {
                        dMIN = pItem.Move_X_NegativeLimit;
                        dMax = pItem.Move_X_PositiveLimit;

                    }
                    else
                    {
                        dMIN = pItem.Move_Y_NegativeLimit;
                        dMax = pItem.Move_Y_PositiveLimit;
                    }
                    m_FrmNumberKeyPad.Result = 0.0;
                    if (m_FrmNumberKeyPad.ShowDialog(dMIN,
                                                                                     dMax,
                                                                                     0) == DialogResult.OK)
                    {
                        pControl.Text = (m_FrmNumberKeyPad.Result).ToString();
                    }
                }
            }
        }
        private void Btn_Event_MakeCrossLine(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                Control pCTRL = sender as Control;
                if (pCTRL == null)
                    return;
                if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                    return;
             
                bool bORG = pCTRL.Tag?.ToString() == "ORG";
                double W= 0.0, H = 0.0;
                double CX= 0.0, CY=0.0;
                int iW=0, iH =0;
                int iCX=0, iCY=0;
                if (bORG)
                {
                    int.TryParse(tb_ORG_CrossWidth.Text, out iW);
                    int.TryParse(tb_ORG_CrossHeight.Text, out iH);
                    int.TryParse(tb_ORG_CenterX.Text, out iCX);
                    int.TryParse(tb_ORG_CenterY.Text, out iCY);
                }
                else
                {
               
                    double.TryParse(tb_CrossWidth.Text, out W);
                    double.TryParse(tb_CrossHeight.Text, out H);
                    double.TryParse(tb_CenterX.Text, out CX);
                    double.TryParse(tb_CenterY.Text, out CY);
                }

            
                pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
                //pItem.ListMarkSpeed(1000);
                if(bORG)
                {
                    pItem.ListOrginJumpAbs((int)(iCX - iW / 2.0), iCY);
                    pItem.ListOrginMarkAbs((int)(iCX + iW / 2.0), iCY);
                    pItem.ListOrginJumpAbs(iCX, (int)(iCY - iH / 2.0));
                    pItem.ListOrginMarkAbs(iCX, (int)(iCY + iH / 2.0));
                }
                else
                {
                    pItem.ListJumpAbs(CX - W / 2.0, CY);
                    pItem.ListMarkAbs(CX + W / 2.0, CY);
                    pItem.ListJumpAbs(CX, CY - H / 2.0);
                    pItem.ListMarkAbs(CX, CY + H / 2.0);
                }
                pItem.ListEnd();
            }
        }

        private void Btn_Evt_MakeXLine(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                Control pCTRL = sender as Control;
                if (pCTRL == null)
                    return;
                if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                    return;

             
                bool bORG = pCTRL.Tag?.ToString() == "ORG";
                double W = 0.0, H = 0.0;
                double CX = 0.0, CY = 0.0;
                int iW = 0, iH = 0;
                int iCX = 0, iCY = 0;
                if (bORG)
                {
                    int.TryParse(tb_ORG_CrossWidth.Text, out iW);
                    int.TryParse(tb_ORG_CrossHeight.Text, out iH);
                    int.TryParse(tb_ORG_CenterX.Text, out iCX);
                    int.TryParse(tb_ORG_CenterY.Text, out iCY);
                }
                else
                {

                    double.TryParse(tb_CrossWidth.Text, out W);
                    double.TryParse(tb_CrossHeight.Text, out H);
                    double.TryParse(tb_CenterX.Text, out CX);
                    double.TryParse(tb_CenterY.Text, out CY);
                }
                pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
                //pItem.ListMarkSpeed(1000);
                if(bORG)
                {
                    pItem.ListOrginJumpAbs((int)(iCX - iW / 2.0), iCY);
                    pItem.ListOrginMarkAbs((int)(iCX + iW / 2.0), iCY);
                }
                else
                {
                    pItem.ListJumpAbs(CX - W / 2.0, CY);
                    pItem.ListMarkAbs(CX + W / 2.0, CY);
                }
              
                pItem.ListEnd();
            }
        }

        private void Btn_Evt_MakeYLine(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                Control pCTRL = sender as Control;
                if (pCTRL == null)
                    return;
                if (pItem.IsExecuteList_BUSY && pItem.IsExecuteList_BUSY)
                    return;


                bool bORG = pCTRL.Tag?.ToString() == "ORG";
                double W = 0.0, H = 0.0;
                double CX = 0.0, CY = 0.0;
                int iW = 0, iH = 0;
                int iCX = 0, iCY = 0;
                if (bORG)
                {
                    int.TryParse(tb_ORG_CrossWidth.Text, out iW);
                    int.TryParse(tb_ORG_CrossHeight.Text, out iH);
                    int.TryParse(tb_ORG_CenterX.Text, out iCX);
                    int.TryParse(tb_ORG_CenterY.Text, out iCY);
                }
                else
                {

                    double.TryParse(tb_CrossWidth.Text, out W);
                    double.TryParse(tb_CrossHeight.Text, out H);
                    double.TryParse(tb_CenterX.Text, out CX);
                    double.TryParse(tb_CenterY.Text, out CY);
                }
                pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
                //pItem.ListMarkSpeed(1000);
                if (bORG)
                {

                    pItem.ListOrginJumpAbs(iCX, (int)(CY - iH / 2.0));
                    pItem.ListOrginMarkAbs(iCX, (int)(CY + iH / 2.0));
                }
                else
                {
                    pItem.ListJumpAbs(CX, CY - H / 2.0);
                    pItem.ListMarkAbs(CX, CY + H / 2.0);
                }


                pItem.ListEnd();
            }
        }
        private void btn_List2Start_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ListExecute(ScanlabRTC5.RTC_LIST._2nd);
            }
        }

        private void btn_ListStop_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ListStop();
            }
        }

        private void btn_List1Start_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ListExecute(ScanlabRTC5.RTC_LIST._1st);
            }
        }

        private void tb_DataMatrix_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_MakeDataMatrix_Click(object sender, EventArgs e)
        {
            if (m_pDataMatrixCreating == null)
            {
                m_pDataMatrixCreating = new Task(MakeDataMatrix);
            }
            if (m_pDataMatrixCreating.Status != TaskStatus.Running)
            {
                m_pDataMatrixCreating = new Task(MakeDataMatrix);
                m_pDataMatrixCreating.Start();
            }
            else
            {
                Trace.WriteLine("Executing Generate DataMatrix");
            }

            /*
if (pItem != null)
{
    string strDataMatrix = tb_DataMatrix.Text;
    double W, H = 0;
    double.TryParse(tb_CrossWidth.Text, out W);
    double.TryParse(tb_CrossHeight.Text, out H);
    if (string.IsNullOrEmpty(strDataMatrix) == false &&
       string.IsNullOrWhiteSpace(strDataMatrix) == false &&
       W > 0 && H > 0
       )
    {
        if(m_DataMatrixItem!=null)
        panel_DataMatrixDraw.Paint-=m_DataMatrixItem.ImagePanelPaint;
        pItem.ListStop();
        pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
        m_DataMatrixItem = EzIna.DataMatrix.DMGenerater.Instance.CreateDataMatrix(strDataMatrix);
        m_DataMatrixItem.CreateCodrdinates(new PointF(0, 0), new SizeF((float)W, (float)H));
        pItem.MakeListFromGraphicsPath(m_DataMatrixItem.CodeGraphicsPath, ScanlabRTC5.RTC_LIST._1st);
        pItem.ListEnd();

        //m_DataMatrixItem.ImagePanelPaint(panel_DataMatrixDraw);

        panel_DataMatrixDraw.Paint+=m_DataMatrixItem.ImagePanelPaint;
        panel_DataMatrixDraw.MouseWheel -= PanelMouseWheel;
        panel_DataMatrixDraw.MouseWheel += PanelMouseWheel;
        tb_DataMatrixArray.Text = m_DataMatrixItem.ToString();
        panel_DataMatrixDraw.Invalidate();
    }
}*/
        }
        private void MakeDataMatrix()
        {
            if (pItem != null)
            {
                string strDataMatrix = "";
                this.Invoke(new MethodInvoker(delegate ()
                                        {
                                            strDataMatrix = tb_DataMatrix.Text;
                                        }));

                double W, H = 0;
                float CX, CY = 0;
                if (string.IsNullOrEmpty(strDataMatrix) == false &&
                     string.IsNullOrWhiteSpace(strDataMatrix) == false
                     )
                {

                  
                    float.TryParse(tb_DM_CenterX.Text, out CX);
                    float.TryParse(tb_DM_CenterY.Text, out CY);
                    double.TryParse(tb_DM_W.Text, out W);
                    double.TryParse(tb_DM_H.Text, out H);

                    if (W <= 0 || H <= 0)
                        return;
                    this.Invoke(new MethodInvoker(delegate ()
                                {
                                    if (m_DataMatrixItem != null)
                                        panel_DataMatrixDraw.Paint -= m_DataMatrixItem.ImagePanelPaint;
                                }));
#if !SIM
                    if (pItem.GetListStatus_Load(ScanlabRTC5.RTC_LIST._1st) == false)
                        return;
#endif
                    pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
                    m_DataMatrixItem = EzIna.DataMatrix.DMGenerater.Instance.CreateDataMatrix(strDataMatrix);
                    m_DataMatrixItem.CreateCodrdinates(new PointF(CX, CY), new SizeF((float)W, (float)H));
                    pItem.MakeListFromGraphicsPath(m_DataMatrixItem.CodeGraphicsPath, ScanlabRTC5.RTC_LIST._1st, false, false);
                    pItem.ListEnd();

                    //m_DataMatrixItem.ImagePanelPaint(panel_DataMatrixDraw);

                    this.Invoke(new MethodInvoker(delegate ()
                                    {
                                        panel_DataMatrixDraw.Paint += m_DataMatrixItem.ImagePanelPaint;
                                        panel_DataMatrixDraw.MouseWheel -= PanelMouseWheel;
                                        panel_DataMatrixDraw.MouseWheel += PanelMouseWheel;
                                        tb_DataMatrixArray.Text = m_DataMatrixItem.ToString();
                                        panel_DataMatrixDraw.Invalidate();
                                    }));
                }
            }
        }
        private void PanelMouseWheel(object sender, MouseEventArgs e)
        {
            if (m_pDataMatrixCreating == null || m_pDataMatrixCreating.Status == TaskStatus.Running)
                return;

            if (m_DataMatrixItem != null)
            {

                if (e.Delta > 0)
                {
                    m_DataMatrixItem.fDrawZoomMultiplier = (float)(m_DataMatrixItem.fDrawZoomMultiplier + 0.2);
                }
                else
                {
                    m_DataMatrixItem.fDrawZoomMultiplier = (float)(m_DataMatrixItem.fDrawZoomMultiplier - 0.2);

                }
                panel_DataMatrixDraw.Invalidate();
                //m_DataMatrixItem.ImagePanelPaint(panel_DataMatrixDraw);
            }
        }

        private void btnList1_Reset_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ListReset(ScanlabRTC5.RTC_LIST._1st);
            }
        }

        private void btnList2_Reset_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                pItem.ListReset(ScanlabRTC5.RTC_LIST._2nd);
            }
        }



        private void TabScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (m_DataMatrixItem != null)
            //{
            //    m_DataMatrixItem.ImagePanelPaint(panel_DataMatrixDraw);
            //}
        }

        private void btn_ResetDevice_Click(object sender, EventArgs e)
        {
            ScanlabRTC5.ResetAllDevice();
        }

        private void Btn_GridMarking_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                int iRowCnt, iColCnt;
                double fRowPitch, fColPitch;
                double fDrawPitch;
                int.TryParse(tb_Grid_Row_Count.Text, out iRowCnt);
                int.TryParse(tb_Grid_Col_Count.Text, out iColCnt);

                double.TryParse(tb_Grid_Row_Pitch.Text, out fRowPitch);
                double.TryParse(tb_Grid_Col_Pitch.Text, out fColPitch);
                double.TryParse(tbGrid_DrawPitch.Text, out fDrawPitch);
                pItem.ListBegin(ScanlabRTC5.RTC_LIST._1st);
                for (int Col = 0; Col < iColCnt; Col++)
                {
                    for (int Row = 0; Row < iRowCnt; Row++)
                    {
                        pItem.ListCrossLine((iColCnt - 1) * (fColPitch / 2.0) - Col * fColPitch, (iRowCnt - 1) * (fRowPitch / 2.0) - Row * fRowPitch, fDrawPitch, fDrawPitch);
                        Trace.WriteLine(string.Format("{0:F3} ,{1:F3}", (iColCnt - 1) * (fColPitch / 2.0) - Col * fColPitch, (iRowCnt - 1) * (fRowPitch / 2.0) - Row * fRowPitch));
                    }
                }
                pItem.ListEnd();
            }

        }
    }
}
