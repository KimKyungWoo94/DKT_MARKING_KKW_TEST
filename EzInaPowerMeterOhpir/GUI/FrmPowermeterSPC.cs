using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzIna.GUI.UserControls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace EzIna.PowerMeter.Ohpir.GUI
{
	public partial class FrmPowermeterSPC : Form
	{
	    Color m_DGV_ForeColor			= Color			.Black;
        Color m_DGV_SetAbleForeColor	= Color			.DodgerBlue;
        Color m_DGV_BackColor			= SystemColors	.Window;
        Color m_DGV_SelectionBackColor	= Color			.SteelBlue;
        Color m_DGV_SelectionForeColor	= Color			.White;

		SPC pItem = null;
        Random m_ChartTest;
        NumberKeypad m_FrmNumberKeyPad;
        Thread m_pMeasureThread=null;
        bool m_bMeasureThreadEnable=false;
        delegate void ChartUpdate();
        ChartUpdate ChartUpdateInvoke;
        int m_iChartUpdateInterval=100;
		enum DGV_ROW_IDX:int
        {           
           HEAD_INFO,
           VERSION,
           MAIN_FREQ,
           HEAD_RANGE,
           WAVE_LEN,
        }
		enum DGV_CELL_TYPE
        {
            NONE,
            COMBOBOX,
            TOGGLE,
            BUTTON,
        }
        enum DGV_COLUMN_IDX
        {
            TITLE,
            DISPLAY,
            SET_VALUE,
            APPLY_BTN,
        }
		public FrmPowermeterSPC()
		{
			InitializeComponent();
            InitializeChart();
            ResistChartFunc();
			m_FrmNumberKeyPad=new NumberKeypad();
            m_ChartTest = new Random();
			this.TopMost            = false;
            this.TopLevel           = false;
            this.FormBorderStyle	= FormBorderStyle.None;
			this.AutoScaleMode		= AutoScaleMode.None;
			this.Font				= new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);   
		}
        #region DGV Cell Config
        private void InitializeDataGridView(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView.RowCount > 0 || a_DatagirdView.ColumnCount > 0)
            {
                a_DatagirdView.Columns.Clear();
                a_DatagirdView.Rows.Clear();
            }
         				            
            a_DatagirdView.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ReadOnly = false;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = false;
            
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            a_DatagirdView.ColumnHeadersHeight = 30;
            a_DatagirdView.RowTemplate.Height = 30;
                        
            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);	
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.EnableHeadersVisualStyles = false;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ClearSelection();

            a_DatagirdView.Columns.AddRange(CreateDataGridViewTextColumn("Title","",(int)(a_DatagirdView.Width*0.25)),
                                            CreateDataGridViewTextColumn("Display","",(int)(a_DatagirdView.Width*0.15)),
                                            CreateDataGridViewTextColumn("SetValue","",(int)(a_DatagirdView.Width*0.15)),
                                            CreateDataGridViewButtonColum("Apply","","Apply",(int)(a_DatagirdView.Width*0.1))
                                            );
        }

		private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt,int a_Width)
        {
            DataGridViewTextBoxColumn pRet = new DataGridViewTextBoxColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name=a_strHeaderTxt;
            pRet.DividerWidth=1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;            
            pRet.Width=a_Width;            
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;            
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;            
            pRet.DefaultCellStyle.SelectionBackColor = m_DGV_SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = m_DGV_SelectionForeColor;
            pRet.SortMode=DataGridViewColumnSortMode.NotSortable;
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
        #endregion DGV Cell Config
        private void InitializeChart()
        {
            PowerChart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            PowerChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            PowerChart.Series[0].XValueType = ChartValueType.Time;
            PowerChart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            PowerChart.ChartAreas[0].AxisY.LabelStyle.Format = "0.00W";
            PowerChart.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Milliseconds;
            PowerChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            PowerChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Milliseconds;
            PowerChart.ChartAreas[0].AxisX.Interval = 0;
            PowerChart.ChartAreas[0].Area3DStyle.Enable3D = false;
        }
        
        private void FrmPowermeterSPC_Load(object sender, EventArgs e)
		{
			InitializeDataGridView(DGV_Param);
            AddApplyAbleParam(ref DGV_Param, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "Head Info", "", false);
            AddApplyAbleParam(ref DGV_Param, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.NONE, "Version"  , "", false);
            AddApplyAbleParam(ref DGV_Param, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX,"MainFrequency","",true);
            AddApplyAbleParam(ref DGV_Param, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX,"HeadRange"  , "", true);
            AddApplyAbleParam(ref DGV_Param, DGV_CELL_TYPE.NONE, DGV_CELL_TYPE.COMBOBOX, "WaveLength"  , "", true);
            DataGridViewComboBoxCell ComboMAIN_FREQ = (DGV_Param["SetValue", (int)DGV_ROW_IDX.MAIN_FREQ] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell ComboHEAD_RANGE = (DGV_Param["SetValue", (int)DGV_ROW_IDX.HEAD_RANGE] as DataGridViewComboBoxCell);
            DataGridViewComboBoxCell ComboWAVE_LEN = (DGV_Param["SetValue", (int)DGV_ROW_IDX.WAVE_LEN] as DataGridViewComboBoxCell);

            foreach(SPC.enumMainHeadSetting Item in Enum.GetValues(typeof(SPC.enumMainHeadSetting)))
            {
                ComboMAIN_FREQ.Items.Add(Item.ToString().Trim('_'));
                                            
            }
            foreach (SPC.enumPowerScale Item in Enum.GetValues(typeof(SPC.enumPowerScale)))
            {
                ComboHEAD_RANGE.Items.Add(Item.ToString().Trim('_'));                
            }
            foreach(SPC.enumWaveLength Item in Enum.GetValues(typeof(SPC.enumWaveLength)))
            {
                ComboWAVE_LEN.Items.Add(Item.ToString());
            }
            
        }
        public bool InitForm(PowerMeterInterface a_Item)
        {
            if (a_Item == null)
                return false;
            if (m_pMeasureThread!=null&& m_pMeasureThread.IsAlive)
            {
                MessageBox.Show("Measure Stop First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (a_Item.DeviceType == typeof(SPC))
            {
                pItem = a_Item as SPC; 
                m_bMeasureThreadEnable=false;               
            }            
            return true;
        }
        public void UpdateDisplay()
        {
            if(pItem!=null)
            {
                LED_CONNECT.Value=pItem.IsDeviceConnected;

                if(pItem.IsDeviceConnected)
                {                    

                    LED_MEASURE.Value = pItem.IsMeasuring;                  
                    LED_ZERO_SET.Value = pItem.IsZeroSetExecuting;
                    GAUGE_OutPower.Value = (float)pItem.fMeasuredPower;
                    lb_OutPower.Text = pItem.fMeasuredPower.ToString("0.00W");
                    /*
                     enum DGV_ROW_IDX:int
                     {           
                        HEAD_INFO,
                        VERSION,
                        MAIN_FREQ,
                        HEAD_RANGE,
                        WAVE_LEN,
                     }
                     */
                     DGV_Param["Display",(int)DGV_ROW_IDX.HEAD_INFO].Value=pItem.GetHeadInfo();
                     DGV_Param["Display",(int)DGV_ROW_IDX.VERSION].Value=pItem.GetVersion();
                     DGV_Param["Display",(int)DGV_ROW_IDX.MAIN_FREQ].Value=pItem.GetMainSetting().ToString().TrimStart('_');
                     DGV_Param["Display",(int)DGV_ROW_IDX.HEAD_RANGE].Value=pItem.GetHeadRange().ToString().TrimStart('_');
                     DGV_Param["Display",(int)DGV_ROW_IDX.WAVE_LEN].Value=pItem.GetWaveLength().ToString();
                }
                else
                {
                    LED_CONNECT.Value=false;

#if SIM
                    LED_MEASURE.Value = m_pMeasureThread != null ? m_pMeasureThread.IsAlive | m_bMeasureThreadEnable : m_bMeasureThreadEnable;
#else
                    LED_MEASURE.Value=false;
#endif                    
                    LED_ZERO_SET.Value=false;                      
                    GAUGE_OutPower.Value=0;     
                    lb_OutPower.Text="0.00W";
                }
            }
            else
            {
                LED_CONNECT.Value = false;
#if SIM
                LED_MEASURE.Value = m_pMeasureThread != null ? m_pMeasureThread.IsAlive | m_bMeasureThreadEnable : m_bMeasureThreadEnable;
#else
                LED_MEASURE.Value=false;
#endif  
                LED_ZERO_SET.Value = false;
                GAUGE_OutPower.Value = 0;
                lb_OutPower.Text = "0.00W";
            }
        }

        private void DGV_Param_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(pItem==null)
               return ;
#if !SIM
            if(pItem.IsDeviceConnected==false)
                return;

#endif
            if ((int)DGV_COLUMN_IDX.SET_VALUE==e.ColumnIndex || (int)DGV_COLUMN_IDX.APPLY_BTN==e.ColumnIndex)
            {
                
                DGV_COLUMN_IDX iCol=(DGV_COLUMN_IDX)e.ColumnIndex;
                DGV_ROW_IDX iRow=(DGV_ROW_IDX)e.RowIndex;
                bool bSetable=DGV_Param["Title",e.RowIndex].Style.ForeColor==m_DGV_SetAbleForeColor;                
                if(bSetable)
                {
                    if ((int)DGV_COLUMN_IDX.APPLY_BTN == e.ColumnIndex)
                    {
                        if (DGV_Param["SetValue", e.RowIndex].Value == null)
                        {
                            MessageBox.Show("Input Value First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (m_pMeasureThread != null && m_pMeasureThread.IsAlive)
                        {
                            MessageBox.Show("Measure Stop First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (pItem.IsZeroSetExecuting)
                        {
                            MessageBox.Show("Zero Executing, Not Allow Any Setting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }


                    }
                }
                switch (iRow)
                {                    
                    case DGV_ROW_IDX.MAIN_FREQ:
                        {                           
                            if (iCol == DGV_COLUMN_IDX.APPLY_BTN)
                            {
                                SPC.enumMainHeadSetting pSet;
                                Enum.TryParse(string.Format("_{0}",DGV_Param["SetValue",e.RowIndex].Value),
                                              out pSet);
                                if(Enum.IsDefined(typeof(SPC.enumMainHeadSetting),pSet))
                                {
                                    pItem.SetMainSetting(pSet);
                                }
                            }
                        }
                        break;
                    case DGV_ROW_IDX.HEAD_RANGE:
                        {                            
                            if (iCol == DGV_COLUMN_IDX.APPLY_BTN)
                            {
                                SPC.enumPowerScale pSet;
                                Enum.TryParse(string.Format("_{0}", DGV_Param["SetValue", e.RowIndex].Value),
                                              out pSet);
                                if (Enum.IsDefined(typeof(SPC.enumPowerScale), pSet))
                                {
                                    pItem.SetHeadRange(pSet);
                                }
                            }
                        }                        
                        break;
                    case DGV_ROW_IDX.WAVE_LEN:
                        {                            
                            if (iCol == DGV_COLUMN_IDX.APPLY_BTN)
                            {
                                eWaveLength pSet;
                                Enum.TryParse(DGV_Param["SetValue", e.RowIndex].Value.ToString(),
                                              out pSet);
                                if (Enum.IsDefined(typeof(eWaveLength), pSet))
                                {
                                    pItem.SetWaveLength(pSet);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }            
        }

        private void BtnZeroSetStart_Click(object sender, EventArgs e)
        {
            if(pItem!=null)
            {
                pItem.SetZero();
            }
        }

        private void BtnMeasureStart_Click(object sender, EventArgs e)
        {
            if (pItem != null)
            {
                
#if !SIM
                if(pItem.IsDeviceConnected==false)
                {
                  MessageBox.Show("Device isn't Connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
                }

#endif
                if (m_pMeasureThread!=null && m_bMeasureThreadEnable)
                {
                    MessageBox.Show("Measure Stop First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

      
               pItem.MeasureStart();
               m_pMeasureThread=new Thread(MeasureExecute);
               m_bMeasureThreadEnable=true;   
               m_pMeasureThread.Start();            
            }
        }
        private void AddPowerData()
        {
            if (pItem != null && pItem.IsMeasuring)
            {
                DateTime now = DateTime.Now;
                //Insert a number into the list.

                PowerChart.ResetAutoValues();
                //Remove old datas from the chart.
                if (PowerChart.Series[0].Points.Count > 0)
                {
                    while (PowerChart.Series[0].Points[0].XValue < now.AddSeconds(-3).ToOADate())
                    {
                        PowerChart.Series[0].Points.RemoveAt(0);
                        if(PowerChart.Series[0].Points.Count > 0)
                        {
                            PowerChart.ChartAreas[0].AxisX.Minimum = PowerChart.Series[0].Points[0].XValue;
                            PowerChart.ChartAreas[0].AxisX.Maximum = now.AddSeconds(3).ToOADate();
                        }
                    }
                }
                //Insert a data into the chart.
#if SIM
                PowerChart.Series[0].Points.AddXY(now.ToOADate(),m_ChartTest.Next(0, 150));
#else
                PowerChart.Series[0].Points.AddXY(now.ToOADate(), pItem.fMeasureAvgPower);
#endif
                
                PowerChart.Invalidate();
            }
        }
        private void ResistChartFunc()
        {
            ChartUpdateInvoke += new ChartUpdate(AddPowerData);
            DateTime now = DateTime.Now;
            PowerChart.ChartAreas[0].AxisX.Minimum = now.ToOADate();
            PowerChart.ChartAreas[0].AxisX.Maximum = now.AddSeconds(3).ToOADate();        
        }
        private void MeasureExecute()
        {
            while (m_bMeasureThreadEnable)
            {
                try
                {
                    PowerChart.Invoke(ChartUpdateInvoke);
                    Thread.Sleep(m_iChartUpdateInterval);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Exception : " + e.ToString());
                }
            }
        }
        private void BtnMeasureStop_Click(object sender, EventArgs e)
        {
            if(pItem!=null)
            {                
                m_bMeasureThreadEnable=false;
                pItem.MeasureStop();
            }
        }

        private void FrmPowermeterSPC_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(m_pMeasureThread!=null&& m_pMeasureThread.IsAlive)
            {
                this.m_bMeasureThreadEnable = false;
                this.m_pMeasureThread.Join(100);
            }
        }
    }
}
