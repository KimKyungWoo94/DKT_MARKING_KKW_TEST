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
namespace EzIna.Laser.IPG.GUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmLaserGLPM : Form
    {
        Color m_DGV_ForeColor = Color.Black;
        Color m_DGV_SetAbleForeColor=Color.DodgerBlue;
        Color m_DGV_BackColor = SystemColors.Window;
        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
        Laser.IPG.GLPM pItem;
        NumberKeypad m_FrmNumberKeyPad;
        ComboBox m_ComboboxModeSel;
        enum DGV_ROW_IDX:int
        {
            FIRMWARE_VER,
            CASE_TEMP,
            HEAD_TEMP,
            BACK_REFLECTION,
            MIN_TRIGGER_FREQ,
            MAX_TRIGGER_FREQ,
            INT_TRIGGER_FREQ,
            EXT_TRIGGER_FREQ,
            TRIGGER_MODE,
            MODULRATION_MODE,
            POWER_CONTROL_MODE,
            EMISSION_CONTROL_MODE,
            
        }
        /// <summary>
        /// 
        /// </summary>
        public FrmLaserGLPM()
        {
            InitializeComponent();    
            InitializeDataGridView(DGV_LaserParam);        
            InitializeCombo(out m_ComboboxModeSel);
            m_ComboboxModeSel.Name="m_ComboboxModeSel";
            m_ComboboxModeSel.Items.Add("External");
            m_ComboboxModeSel.Items.Add("Internal");
            this.TopMost            = false;
            this.TopLevel           = false;
            this.FormBorderStyle	= FormBorderStyle.None;
			this.AutoScaleMode		= AutoScaleMode.None;
			this.Font				= new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);   
           
            BtnEmssionOn    .Click+=Btn_Click;        
            BtnEmssionOff   .Click+=Btn_Click;         
            
            
            AddLaserParameter(false ,""             ,"Monitor"          ,"FirmWareVersion"      ,"");
            AddLaserParameter(false ,"0.00 ℃"      ,"Monitor"          ,"Case Temp"            ,"");
            AddLaserParameter(false ,"0.00 ℃"      ,"Monitor"          ,"Head Temp"            ,"");
            AddLaserParameter(false ,"0.00 %"       ,"Monitor"          ,"Back Reflection"      ,"");
            AddLaserParameter(false ,"0.00 Hz"      ,"Monitor"          ,"Min TriggerFREQ"      , "");
            AddLaserParameter(false ,"0.00 Hz"      ,"Monitor"          ,"Max TriggerFREQ"      , "");
            AddLaserParameter(true  ,"0.00 Hz"      ,"Operating"        ,"INT_TriggerFREQ"      ,"");
            AddLaserParameter(false ,"0.00 Hz"      ,"Operating"        ,"EXT_TriggerFREQ"      ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"TriggerMode"          ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"Modulation"           ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"PowerControl"         ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"EmissionControl"      ,"");
     
            m_FrmNumberKeyPad= new NumberKeypad();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Item"></param>
        public void InitForm(Laser.LaserInterface a_Item)
        {
            if (a_Item == null)
                return;
            if (a_Item.DeviceType == typeof(Laser.IPG.GLPM))
            {
                pItem = a_Item  as Laser.IPG.GLPM;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_DatagirdView"></param>
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
            a_DatagirdView.Columns.AddRange(CreateDataGridViewTextColumn("Group","",(int)(a_DatagirdView.Width*0.3)),
                                            CreateDataGridViewTextColumn("Item","",(int)(a_DatagirdView.Width*0.25)),
                                            CreateDataGridViewTextColumn("Value","",(int)(a_DatagirdView.Width*0.4))
                                            );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strHeaderTxt"></param>
        /// <param name="a_strBindingPropTxt"></param>
        /// <param name="a_Width"></param>
        /// <returns></returns>
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
        private void AddLaserParameter(bool a_bSetable,string a_strFormat,params string [] a_strItem)
        {
            DGV_LaserParam.Rows.Add(a_strItem);
            DGV_LaserParam["Item",DGV_LaserParam.Rows.Count-1].Style.ForeColor=a_bSetable==true?m_DGV_SetAbleForeColor:m_DGV_ForeColor;            
            DGV_LaserParam["Value",DGV_LaserParam.Rows.Count-1].Style.Format=a_strFormat;
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateDisplay()
        {
            if(pItem!=null)
            {
                LED_CONNECT.Value=pItem.IsConnected;
                
                if(pItem.IsConnected)
                {
                    LED_SYSTEM_READY.Value=pItem.IsLaserReady;
                    LED_EMISSION.Value=pItem.IsEmissionOn;  
                    LED_WarmupMode.Value=pItem.IsWarmupMode;                                                                    
                    GAUGE_SetPowerPercent.Value=pItem.SetPoint;
                    
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.FIRMWARE_VER].Value=pItem.strModelName;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.CASE_TEMP].Value=pItem.CaseTemp;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.HEAD_TEMP].Value=pItem.HeadTemp;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.BACK_REFLECTION].Value=pItem.BackReflection;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.MIN_TRIGGER_FREQ].Value=pItem.MinTriggerFREQ;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.MAX_TRIGGER_FREQ].Value=pItem.MinTriggerFREQ;
                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.INT_TRIGGER_FREQ].Value = pItem.INT_TriggerFREQ;
                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.EXT_TRIGGER_FREQ].Value = pItem.EXT_TriggerFREQ;

                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.TRIGGER_MODE].Value = pItem.TriggerMode==TRIG_MODE.INT?"Internal":"External";
                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.MODULRATION_MODE].Value =pItem.Modulation==true?"Internal":"External";
                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.POWER_CONTROL_MODE].Value =pItem.PowerSourceControl==true?"Internal":"External";
                    DGV_LaserParam["Value", (int)DGV_ROW_IDX.EMISSION_CONTROL_MODE].Value =pItem.EmissionControl==true?"Internal":"External";
                }
                else
                {
                    LED_SYSTEM_READY.Value=false;
                    LED_EMISSION.Value=false;
                    LED_WarmupMode.Value=false;
                    
                    GAUGE_SetPowerPercent.Value=0;   
                      
                    //PanelWarmupTime.Caption="";       
                }
            }
            else
            {
                LED_SYSTEM_READY.Value=false;
                LED_CONNECT.Value=false;
                LED_EMISSION.Value=false;
                LED_WarmupMode.Value=false;
                GAUGE_SetPowerPercent.Value=0;
                //PanelWarmupTime.Caption="";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateFilker()
        {
            
        }
        
        private void InitializeCombo(out ComboBox a_Control)
        {
            a_Control              = new ComboBox();
            a_Control.DropDownStyle=ComboBoxStyle.DropDownList;         
            a_Control.BringToFront();
            a_Control.Parent=DGV_LaserParam;
            a_Control.Hide();            
            a_Control.MouseLeave+=Combo_MouseLeave;
            a_Control.DropDownClosed+=Combo_DropDownClosed;            
        }        
        private void Combo_MouseLeave(object sender, EventArgs e)
        {
            ComboBox pControl =  sender as ComboBox;
            if( pControl!=null)
                pControl.Hide();
        }
        private void Combo_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox pControl =  sender as ComboBox;
            if( pControl!=null)
                pControl.Hide();
            if (pItem != null)
            {
                if (pControl.Name == "m_ComboboxModeSel")
                {
                    DGV_ROW_IDX idx = (DGV_ROW_IDX)pControl.Tag;
                    switch (idx)
                    {

                        case DGV_ROW_IDX.TRIGGER_MODE:
                            {
                                pItem.TriggerMode = pControl.SelectedIndex > 0?TRIG_MODE.INT:TRIG_MODE.EXT;
                            }
                            break;
                        case DGV_ROW_IDX.MODULRATION_MODE:
                            {
                                pItem.Modulation = pControl.SelectedIndex > 0;
                            }
                            break;
                        case DGV_ROW_IDX.POWER_CONTROL_MODE:
                            {
                                pItem.PowerSourceControl = pControl.SelectedIndex > 0;
                            }
                            break;
                        case DGV_ROW_IDX.EMISSION_CONTROL_MODE:
                            {
                                pItem.EmissionControl = pControl.SelectedIndex > 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }     
        private void Btn_Click(object sender, EventArgs e)
        {
            Control pControl = sender as Control;
            if(pItem==null)
                return;
            switch (pControl.Name)
            {
                case "BtnEmssionOn":
                    {
                        pItem.EmissionOnOff=true;
                    }
                    break;
                case "BtnEmssionOff":
                    {
                        pItem.EmissionOnOff=false;
                    }
                    break;           
            }
        }

        private void FrmLaserTalon_SizeChanged(object sender, EventArgs e)
        {
            DGV_LaserParam.Columns["Group"].Width=(int)(DGV_LaserParam.Width*0.35);
            DGV_LaserParam.Columns["Item"].Width=(int)(DGV_LaserParam.Width*0.25);
            DGV_LaserParam.Columns["Value"].Width=(int)(DGV_LaserParam.Width*0.4);            
        }
        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = DGV_LaserParam[column, row];
            DataGridViewCell cell2 = DGV_LaserParam[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
        private void DGV_LaserParam_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {

                    if(e.RowIndex<DGV_LaserParam.Rows.Count-1)
                    {
                        if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex+1))
                        {
                            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                        }
                    }                   
                    if (e.RowIndex < 1 || e.ColumnIndex < 0)
                        return;
                    
                    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                    {
                        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                        
                    }
                    else
                    {
                        e.AdvancedBorderStyle.Top = DGV_LaserParam.AdvancedCellBorderStyle.Top;  
                        //e.AdvancedBorderStyle.Bottom = DGV_LaserParam.AdvancedCellBorderStyle.Bottom;                      
                    }
                }

            }
        }
        private void DGV_LaserParam_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 0)// <==조건문으로 열선택
            {
                if (e.RowIndex == 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }

        private void DGV_LaserParam_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex <= -1)
                return ;
            if(pItem ==null)
                return;
#if !SIM
            if(pItem.IsConnected==false)
                return;
#endif
                
            if(DGV_LaserParam.Columns["Value"].Index ==e.ColumnIndex)
            {
                DGV_ROW_IDX idx=(DGV_ROW_IDX)e.RowIndex;
                switch (idx)
                {
                    case DGV_ROW_IDX.FIRMWARE_VER:
                        break;
                    case DGV_ROW_IDX.CASE_TEMP:
                        break;
                    case DGV_ROW_IDX.HEAD_TEMP:
                        break;
                    case DGV_ROW_IDX.BACK_REFLECTION:
                        break;
                    case DGV_ROW_IDX.MIN_TRIGGER_FREQ:
                        break;
                    case DGV_ROW_IDX.MAX_TRIGGER_FREQ:
                        break;
                    case DGV_ROW_IDX.INT_TRIGGER_FREQ:
                        {
                                              
                        }
                        break;
                    case DGV_ROW_IDX.EXT_TRIGGER_FREQ:
                        break;
                    case DGV_ROW_IDX.TRIGGER_MODE:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_ComboboxModeSel.Location = new Point(temp.X, temp.Y);
                            m_ComboboxModeSel.Width = temp.Width;
                            m_ComboboxModeSel.Height = temp.Height;
                            m_ComboboxModeSel.SelectedIndex = pItem.TriggerMode==TRIG_MODE.INT?1:0;
                            m_ComboboxModeSel.DroppedDown = true;
                            m_ComboboxModeSel.Tag=DGV_ROW_IDX.TRIGGER_MODE;
                            m_ComboboxModeSel.Show();
                        }
                        break;
                    case DGV_ROW_IDX.MODULRATION_MODE:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_ComboboxModeSel.Location = new Point(temp.X, temp.Y);
                            m_ComboboxModeSel.Width = temp.Width;
                            m_ComboboxModeSel.Height = temp.Height;
                            m_ComboboxModeSel.SelectedIndex = pItem.Modulation == true ? 1 : 0;
                            m_ComboboxModeSel.DroppedDown = true;
                            m_ComboboxModeSel.Tag=DGV_ROW_IDX.MODULRATION_MODE;
                            m_ComboboxModeSel.Show();
                        }
                        break;
                    case DGV_ROW_IDX.POWER_CONTROL_MODE:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_ComboboxModeSel.Location = new Point(temp.X, temp.Y);
                            m_ComboboxModeSel.Width = temp.Width;
                            m_ComboboxModeSel.Height = temp.Height;
                            m_ComboboxModeSel.SelectedIndex = pItem.PowerSourceControl == true ? 1 : 0;
                            m_ComboboxModeSel.DroppedDown = true;
                            m_ComboboxModeSel.Tag=DGV_ROW_IDX.POWER_CONTROL_MODE;
                            m_ComboboxModeSel.Show();
                        }
                        break;
                    case DGV_ROW_IDX.EMISSION_CONTROL_MODE:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_ComboboxModeSel.Location = new Point(temp.X, temp.Y);
                            m_ComboboxModeSel.Width = temp.Width;
                            m_ComboboxModeSel.Height = temp.Height;
                            m_ComboboxModeSel.SelectedIndex = pItem.EmissionControl == true ? 1 : 0;
                            m_ComboboxModeSel.DroppedDown = true;
                            m_ComboboxModeSel.Tag=DGV_ROW_IDX.EMISSION_CONTROL_MODE;
                            m_ComboboxModeSel.Show();
                        }
                        break;
                    default:
                        break;
                }
                m_FrmNumberKeyPad.Result=0.0;
            }
            
        }

        private void GAUGE_SetPowerPercent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_FrmNumberKeyPad.ShowDialog(0, 100, pItem.SetPoint) == DialogResult.OK)
            {
                pItem.SetPoint = (float)m_FrmNumberKeyPad.Result;
            }
        }
    }
}
