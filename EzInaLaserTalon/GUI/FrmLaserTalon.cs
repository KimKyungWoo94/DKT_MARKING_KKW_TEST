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
namespace EzIna.Laser.Talon.GUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrmLaserTalon : Form
    {
        Color m_DGV_ForeColor = Color.Black;
        Color m_DGV_SetAbleForeColor=Color.DodgerBlue;
        Color m_DGV_BackColor = SystemColors.Window;
        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
        Laser.Talon.Talon355 pItem;
        NumberKeypad m_FrmNumberKeyPad;
        ComboBox m_Combo_EXT_GATE_SIG;
        ComboBox m_Combo_TRIGGER_EDGE_SIG;
        enum DGV_ROW_IDX:int
        {
            SYSTEM_MSG,
            DIODE_CURRENT,
            QSW,
            EPRF,
            SHG,
            THG,
            EXT_GATE_SIGNAL,
            TRIGGER_EDGE_SIGNAL,
            CURRENT_THG_SPOT,
            CURRENT_THG_SPOT_HOUR,
            ALL_THG_SPOT_HOUR,
        }
        /// <summary>
        /// 
        /// </summary>
        public FrmLaserTalon()
        {
            InitializeComponent();    
            InitializeDataGridView(DGV_LaserParam);        
            this.TopMost            = false;
            this.TopLevel           = false;
            this.FormBorderStyle	= FormBorderStyle.None;
			this.AutoScaleMode		= AutoScaleMode.None;
			this.Font				= new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);   
           
            BtnEmssionOn    .Click+=Btn_Click;        
            BtnEmssionOff   .Click+=Btn_Click;         
            BtnShutterOpen  .Click+=Btn_Click;          
            BtnShutterClose .Click+=Btn_Click;           
            BtnGateOpen     .Click+=Btn_Click;
            BtnGateClose    .Click+=Btn_Click;        
            BtnExtGateEnable  .Click+=Btn_Click;         
            BtnExtGateDisable .Click+=Btn_Click;
            
            AddLaserParameter(false ,""             ,"Monitor"          ,"System MSG"           ,"");
            AddLaserParameter(true  ,"0.00 A"       ,"Operating"        ,"Diode Current(AMP)"   ,"");
            AddLaserParameter(true  ,"0 Hz"         ,"Operating"        ,"QSW"                  ,"");
            AddLaserParameter(true  ,"0 Hz"         ,"Operating"        ,"EPRF"                 ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"SHG"                  ,"");
            AddLaserParameter(true  ,""             ,"Operating"        ,"THG"                  ,"");
            AddLaserParameter(true  ,""             ,"Signal"           ,"EXT Gate Signal"      ,"");
            AddLaserParameter(true  ,""             ,"Signal"           ,"Trigger Edge"         ,"");  
            AddLaserParameter(true  ,""             ,"THG Translation"  ,"Current Spot"         ,"");
            AddLaserParameter(false ,"0.00 Hours"   ,"THG Translation"  ,"Current Spot Hour"    ,"");
            AddLaserParameter(false ,"0.00 Hours"   ,"THG Translation"  ,"All Spot Hour"    ,"");
            InitializeCombo(out m_Combo_EXT_GATE_SIG);
            InitializeCombo(out m_Combo_TRIGGER_EDGE_SIG);

            m_Combo_EXT_GATE_SIG.Name="Combo_EXT_GATE_SIG.Name";
            m_Combo_TRIGGER_EDGE_SIG.Name="Combo_TRIGGER_EDGE_SIG";
            foreach(Talon355.enumGateEnableLevel Item in Enum.GetValues(typeof(Talon355.enumGateEnableLevel)))
            {
                m_Combo_EXT_GATE_SIG.Items.Add(Item);
            }
            foreach (Talon355.enumTriggerEdgeLevel Item in Enum.GetValues(typeof(Talon355.enumTriggerEdgeLevel)))
            {
                m_Combo_TRIGGER_EDGE_SIG.Items.Add(Item);
            }
            m_FrmNumberKeyPad = new NumberKeypad();
        }

        private void InitializeCombo(out ComboBox a_Control)
        {
            a_Control = new ComboBox();
            a_Control.DropDownStyle = ComboBoxStyle.DropDownList;
            a_Control.BringToFront();
            a_Control.Parent = DGV_LaserParam;
            a_Control.Hide();
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

            if (pItem != null)
            {
                switch (pControl.Name)
                {
                    case "Combo_EXT_GATE_SIG":
                        {
                            if((int)pItem.GateEnableSignalLevel!=pControl.SelectedIndex)
                            {
                                pItem.GateEnableSignalLevel=(Talon355.enumGateEnableLevel)pControl.SelectedIndex;
                            }
                        }
                        break;
                    case "Combo_TRIGGER_EDGE_SIG":
                        {
                            if ((int)pItem.TriggerEdgeSignalLevel != pControl.SelectedIndex)
                            {
                                pItem.TriggerEdgeSignalLevel = (Talon355.enumTriggerEdgeLevel)pControl.SelectedIndex;
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Item"></param>
        public void InitForm(Laser.LaserInterface a_Item)
        {
            if (a_Item == null)
                return;
            if (a_Item.DeviceType == typeof(Laser.Talon.Talon355))
            {
                pItem = a_Item  as Laser.Talon.Talon355;
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
                    
                    LED_SHUTTER.Value=pItem.IsShutterOpen;
                    LED_GATE.Value=pItem.IsGateOpen;
                    LED_EXT_GATE.Value=pItem.GateMode==GATE_MODE.EXT;  
                    GAUGE_OutPower.Value=(float)pItem.LaserEmittedPower;  
                   // PanelWarmupTime.Caption=pItem.RemainWarmUptime.ToString("mm:ss"); 
                   // To be continue 
                    /* 
                   AddLaserParameter(false ,""         ,"Monitor"          ,"System MSG"           ,"");
                    AddLaserParameter(true  ,"0 A"      ,"Operating"        ,"Diode Current(AMP)"   ,"");
                    AddLaserParameter(true  ,"0 Hz"     ,"Operating"        ,"QSW"                  ,"");
                    AddLaserParameter(true  ,"0 Hz"     ,"Operating"        ,"EPRF"                 ,"");
                    AddLaserParameter(true  ,""         ,"Operating"        ,"SHG"                  ,"");
                    AddLaserParameter(true  ,""         ,"Operating"        ,"THG"                  ,"");
                    AddLaserParameter(true  ,""         ,"THG Translation"  ,"Current Spot"         ,"");
                    AddLaserParameter(false ,"0 Hours"  ,"THG Translation"  ,"Current Spot Hour"    ,"");
                    */
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.SYSTEM_MSG].Value           =pItem.GetSystemHistory(0);
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.DIODE_CURRENT].Value        =pItem.DiodeCurrent;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.QSW].Value                  =pItem.RepetitionRate;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.EPRF].Value                 =pItem.EPRF;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.SHG].Value                  =pItem.SHG_Temperature_REG_Point;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.THG].Value                  =pItem.THG_OvenTemprature;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.EXT_GATE_SIGNAL].Value      =pItem.GateEnableSignalLevel.ToString();
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.TRIGGER_EDGE_SIGNAL].Value  =pItem.TriggerEdgeSignalLevel.ToString();
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.CURRENT_THG_SPOT].Value     =pItem.THG_CrystalSpotPos;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.CURRENT_THG_SPOT_HOUR].Value=pItem.THG_CrystalSpotHours;
                    DGV_LaserParam["Value",(int)DGV_ROW_IDX.ALL_THG_SPOT_HOUR].Value=pItem.AllTHGSpotToalHours;
                    lb_WarmUpTime.Text=pItem.RemainWarmUptime.ToString();
                }
                else
                {
                    LED_SYSTEM_READY.Value=false;
                    LED_EMISSION.Value=false;
                    LED_SHUTTER.Value=false;
                    LED_GATE.Value=false;
                    LED_EXT_GATE.Value=false;       
                    GAUGE_OutPower.Value=0;     
                    lb_WarmUpTime.Text="";       
                }
            }
            else
            {
                LED_SYSTEM_READY.Value=false;
                LED_CONNECT.Value=false;
                LED_EMISSION.Value=false;
                LED_SHUTTER.Value=false;
                LED_GATE.Value=false;
                LED_EXT_GATE.Value=false;
                GAUGE_OutPower.Value=0;
                lb_WarmUpTime.Text="";
            }
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
                case "BtnShutterOpen":
                    {
                        pItem.ShutterOpen=true;
                    }
                    break;
                case "BtnShutterClose":
                    {
                        pItem.ShutterOpen=false;
                    }
                    break;
                case "BtnGateOpen":
                    {
                        pItem.GateOpen=true;
                    }
                    break;
                case "BtnGateClose":
                    {
                        pItem.GateOpen=false;
                    }
                    break;
                case "BtnExtGateEnable":
                    {
                        pItem.GateMode=GATE_MODE.EXT;
                    }
                    break;
                case "BtnExtGateDisable":
                    {
                        pItem.GateMode=GATE_MODE.INT;
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

            if(DGV_LaserParam.Columns["Value"].Index ==e.ColumnIndex)
            {
                /*
                 AddLaserParameter(false ,""         ,"Monitor"          ,"System MSG"           ,"");
                 AddLaserParameter(true  ,"0 A"      ,"Operating"        ,"Diode Current(AMP)"   ,"");
                 AddLaserParameter(true  ,"0 Hz"     ,"Operating"        ,"QSW"                  ,"");
                 AddLaserParameter(true  ,"0 Hz"     ,"Operating"        ,"EPRF"                 ,"");
                 AddLaserParameter(true  ,""         ,"Operating"        ,"SHG"                  ,"");
                 AddLaserParameter(true  ,""         ,"Operating"        ,"THG"                  ,"");
                 AddLaserParameter(true  ,""         ,"THG Translation"  ,"Current Spot"         ,"");
                 AddLaserParameter(false ,"0 Hours"  ,"THG Translation"  ,"Current Spot Hour"    ,"");
                 */

               
                DGV_ROW_IDX IDX=(DGV_ROW_IDX)e.RowIndex;
                switch (IDX)
                {
                 
                    case DGV_ROW_IDX.DIODE_CURRENT:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_DIODE_CURRENT, pItem.DiodeCurrentLimit, pItem.DiodeCurrent) == DialogResult.OK)
                            {
                                pItem.SetDiodeCurrent = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_ROW_IDX.QSW:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_REPETITION_REATE, Talon355.MAX_REPETITION_REATE, pItem.RepetitionRate) == DialogResult.OK)
                            {
                                pItem.RepetitionRate = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_ROW_IDX.EPRF:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_EPRF, Talon355.MAX_EPRF, pItem.EPRF) == DialogResult.OK)
                            {
                                pItem.EPRF = m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_ROW_IDX.SHG:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_SHG_TEMP_REG_POINT, Talon355.MAX_SHG_TEMP_REG_POINT, pItem.SHG_Temperature_REG_Point_LastSet) == DialogResult.OK)
                            {
                                pItem.SHG_Temperature_REG_Point = (int)m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_ROW_IDX.THG:
                        {
                            if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_THG_TEMP_REG_POINT, Talon355.MAX_THG_TEMP_REG_POINT, pItem.THG_OvenTempratureLastSet) == DialogResult.OK)
                            {
                                pItem.THG_OvenTemprature = (int)m_FrmNumberKeyPad.Result;
                            }
                        }
                        break;
                    case DGV_ROW_IDX.EXT_GATE_SIGNAL:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_Combo_EXT_GATE_SIG.Location = new Point(temp.X, temp.Y);
                            m_Combo_EXT_GATE_SIG.Width = temp.Width;
                            m_Combo_EXT_GATE_SIG.Height = temp.Height;
                            m_Combo_EXT_GATE_SIG.SelectedIndex = (int)pItem.GateEnableSignalLevel;
                            m_Combo_EXT_GATE_SIG.DroppedDown = true;
                            m_Combo_EXT_GATE_SIG.Show();
                        }
                        break;
                    case DGV_ROW_IDX.TRIGGER_EDGE_SIGNAL:
                        {
                            Rectangle temp = DGV_LaserParam.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            m_Combo_TRIGGER_EDGE_SIG.Location = new Point(temp.X, temp.Y);
                            m_Combo_TRIGGER_EDGE_SIG.Width = temp.Width;
                            m_Combo_TRIGGER_EDGE_SIG.Height = temp.Height;
                            m_Combo_TRIGGER_EDGE_SIG.SelectedIndex = (int)pItem.TriggerEdgeSignalLevel;
                            m_Combo_TRIGGER_EDGE_SIG.DroppedDown = true;
                            m_Combo_TRIGGER_EDGE_SIG.Show();
                        }
                        break;
                    case DGV_ROW_IDX.CURRENT_THG_SPOT:
                        {
                            if (MessageBox.Show("Would you like to Change Spot Position??", "Warning"
                            , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                if (m_FrmNumberKeyPad.ShowDialog(Talon355.MIN_THG_CRYITAL_POS, pItem.MaxTHGSpotCount, pItem.THG_CrystalSpotPos) == DialogResult.OK)
                                {
                                    pItem.THG_CrystalSpotPos = (int)m_FrmNumberKeyPad.Result;
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
                m_FrmNumberKeyPad.Result=0.0;
            }
            
        }
    }
}
