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
namespace EzIna.Attenuator.OPTOGAMA.GUI
{
    public partial class FrmAttenuatorLPA : Form
    {
        Color m_DGV_ForeColor = Color.Black;
        Color m_DGV_SetAbleForeColor=Color.DodgerBlue;
        Color m_DGV_BackColor = SystemColors.Window;
        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
        double m_fSetPosition=0.0;
        double m_fSetAngle=0.0;
        private LPA pItem =null;
        NumberKeypad m_FrmNumberKeyPad;
        ComboBox HomeOptionCombo;

       
        enum DGV_ROW_INDEX
        {
            MODEL_NAME,
            WAVE_LENGTH,            
            POS,
            ANGLE,
            MIN_POWER,
            MAX_POWER,
            CURRENT_POWER,
            HOME_SEARCH_OPTION
        }
        public FrmAttenuatorLPA()
        {
            InitializeComponent();
            m_FrmNumberKeyPad       = new NumberKeypad();
            InitializeCombo(out HomeOptionCombo);
            foreach (LPA.HomeOption Item in Enum.GetValues(typeof(LPA.HomeOption)))
            {
                HomeOptionCombo.Items.Add(Item.ToString());
            }
            this.TopMost            = false;
            this.TopLevel           = false;
            this.FormBorderStyle	= FormBorderStyle.None;
			this.AutoScaleMode		= AutoScaleMode.None;
			this.Font				= new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);   
        }        
        private void FrmAttenuatorLPA_Load(object sender, EventArgs e)
        {
            InitializeDataGridView(DGV_LPAParam);
            AddParameter(false ,""      ,"Monitor"     ,"ModelName"       ,"");
            AddParameter(false ,""      ,"Monitor"     ,"WaveLength"      ,"");                     
            AddParameter(false ,"0.000" ,"Monitor"     ,"Position"        ,"");
            AddParameter(false ,"0.000" ,"Monitor"     ,"Angle"           ,"");
            AddParameter(true  ,"0.000" ,"Operation"   ,"MinPower"        ,"");
            AddParameter(true  ,"0.000" ,"Operation"   ,"MaxPower"        ,"");
            AddParameter(true  ,"0.000%","Operation"   ,"CurrentPower"    ,"");
            AddParameter(true  ,""      ,"Home"        ,"HomeSearchOption" ,"");
        }
      
        private void FrmAttenuatorLPA_VisibleChanged(object sender, EventArgs e)
        {
        }
        private void FrmAttenuatorLPA_Resize(object sender, EventArgs e)
        {
            DGV_LPAParam.Columns["Group"].Width=(int)(DGV_LPAParam.Width*0.35);
            DGV_LPAParam.Columns["Item"].Width=(int)(DGV_LPAParam.Width*0.25);
            DGV_LPAParam.Columns["Value"].Width=(int)(DGV_LPAParam.Width*0.4);  
        }
        private void InitializeCombo(out ComboBox a_Control)
        {
            a_Control              = new ComboBox();
            a_Control.DropDownStyle=ComboBoxStyle.DropDownList;         
            a_Control.BringToFront();
            a_Control.Parent=DGV_LPAParam;
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
                switch (pControl.Name)
                {
                    case "HomeOptionCombo":
                        {
                            pItem.HomeSearchOption = HomeOptionCombo.SelectedIndex;
                        }
                        break;
                }
            }
        }
        public void UpdateDisplay()
        {
            if(pItem ==null)
                return;
            LED_Conect.Value=pItem.IsConnected;
            Panel_POS_SET.Caption=string.Format("{0:0.000}",m_fSetPosition);
            Panel_ANG_SET.Caption=string.Format("{0:0.000}",m_fSetAngle);
            if(pItem.IsConnected)
            {
                LED_ERR.Value           =pItem.IsFault;
                LED_MotorEnable.Value   =pItem.IsMotorEnabled;
                LED_HOMED.Value         =pItem.IsHomeDone;
                LED_InMotion.Value      =pItem.IsMotionDone;
                LED_INP.Value           =pItem.IsInPosition;
                LED_PLIMIT.Value        =pItem.IsPositiveLimit;
                LED_NLIMIT.Value        =pItem.IsNagativeLimit;
                /*  enum DGV_ROW_INDEX
                {
                     MODEL_NAME,
                     WAVE_LENGTH,                     
                     POS,
                     ANGLE,
                     MIN_POWER,
                     MAX_POWER,
                     CURRENT_POWER,
                     HOME_SEARCH_OPTION
                }*/
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.MODEL_NAME].Value=pItem.strModelName;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.WAVE_LENGTH].Value=pItem.WaveLength;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.POS].Value=pItem.fPosition;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.ANGLE].Value=pItem.fAngle;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.MIN_POWER].Value=pItem.fMinPower;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.MAX_POWER].Value=pItem.fMaxPower;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.CURRENT_POWER].Value=pItem.fCurrentPower/100.0;
                DGV_LPAParam["Value",(int)DGV_ROW_INDEX.HOME_SEARCH_OPTION].Value=(LPA.HomeOption)pItem.HomeSearchOption;
            }
            else
            {
                LED_ERR.Value           =false;
                LED_MotorEnable.Value   =false;
                LED_HOMED.Value         =false;
                LED_InMotion.Value      =false;
                LED_INP.Value           =false;
                LED_PLIMIT.Value        =false;
                LED_NLIMIT.Value        =false;
            }

        }
        public void UpdateFlickr()
        {
            if (pItem == null)
                return;
        }
        public void InitForm(AttuenuatorInterface a_Item)
        {
            if (a_Item == null)
                return;
            if (a_Item.DeviceType == typeof(LPA))
            {
                pItem = a_Item as LPA;
            }
        }

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
          bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = DGV_LPAParam[column, row];
            DataGridViewCell cell2 = DGV_LPAParam[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
        private void DGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {

                    if(e.RowIndex<DGV_LPAParam.Rows.Count-1)
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
                        e.AdvancedBorderStyle.Top = DGV_LPAParam.AdvancedCellBorderStyle.Top;  
                        //e.AdvancedBorderStyle.Bottom = DGV_LaserParam.AdvancedCellBorderStyle.Bottom;                      
                    }
                }

            }
        }

        private void DGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        private void AddParameter(bool a_bSetable,string a_strFormat,params string [] a_strItem)
        {
            DGV_LPAParam.Rows.Add(a_strItem);
            DGV_LPAParam["Item",DGV_LPAParam.Rows.Count-1].Style.ForeColor=a_bSetable==true?m_DGV_SetAbleForeColor:m_DGV_ForeColor;            
            DGV_LPAParam["Value",DGV_LPAParam.Rows.Count-1].Style.Format=a_strFormat;
            
        }

        private void Panel_POS_SET_Click(object sender, EventArgs e)
        {          
            if(m_FrmNumberKeyPad.ShowDialog(0,300,m_fSetPosition)==DialogResult.OK)
            {
                m_fSetPosition=m_FrmNumberKeyPad.Result;
                m_FrmNumberKeyPad.Result=0.0;
            }
        }
        private void Panel_ANG_SET_Click(object sender, EventArgs e)
        {
            if(m_FrmNumberKeyPad.ShowDialog(0,360,m_fSetAngle)==DialogResult.OK)
            {
                m_fSetAngle=m_FrmNumberKeyPad.Result;
                m_FrmNumberKeyPad.Result=0.0;
            }
        }
        
        private void BtnABS_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.MoveAbSolute(m_fSetPosition);
        }
        private void BtnDeviceSet_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.DeviceReset();
        }        
        private void BtnHome_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.StartHoming(false);
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.StopMotion();
        }
        private void BtnAngleCCW_Click(object sender, EventArgs e)
        {
             if(pItem==null)
                return;
            pItem.fAngle=-m_fSetAngle;
        }
        private void BtnAngleCW_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.fAngle=m_fSetAngle;
        }
        private void BtnMotorOn_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.MotorEnable(true);
        }

        private void BtnMotorOff_Click(object sender, EventArgs e)
        {
            if(pItem==null)
                return;
            pItem.MotorEnable(false);
        }
        private void BtnZeroSet_Click(object sender, EventArgs e)
        {
             if(pItem==null)
                return;
            pItem.Zeroset();
        }
        private void DGV_LPAParam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex<0 || e.RowIndex<0)
                return ;

            if(DGV_LPAParam.Columns["Value"].Index==e.ColumnIndex)
            {
                DGV_ROW_INDEX IDX=(DGV_ROW_INDEX)e.RowIndex;
                switch (IDX)
                {
                    case DGV_ROW_INDEX.MODEL_NAME:
                        break;
                    case DGV_ROW_INDEX.WAVE_LENGTH:
                        break;
                    case DGV_ROW_INDEX.POS:
                        break;
                    case DGV_ROW_INDEX.ANGLE:
                        
                        break;
                    case DGV_ROW_INDEX.MIN_POWER:
                        {
                            if(m_FrmNumberKeyPad.ShowDialog(0,pItem.fMaxPower,pItem.fMinPower)==DialogResult.OK)
                            {
                                pItem.fMinPower=m_FrmNumberKeyPad.Result;
                                m_FrmNumberKeyPad.Result=0.0;
                            }
                        }
                        break;
                    case DGV_ROW_INDEX.MAX_POWER:
                        {
                            if(m_FrmNumberKeyPad.ShowDialog(pItem.fMaxPower,9999999,pItem.fMaxPower)==DialogResult.OK)
                            {
                                pItem.fMaxPower=m_FrmNumberKeyPad.Result;
                                m_FrmNumberKeyPad.Result=0.0;
                            }
                        }
                        break;
                    case DGV_ROW_INDEX.CURRENT_POWER:
                        {
                            if(m_FrmNumberKeyPad.ShowDialog(0,100,pItem.fCurrentPower)==DialogResult.OK)
                            {
                                pItem.fCurrentPower=m_FrmNumberKeyPad.Result;
                                m_FrmNumberKeyPad.Result=0.0;
                            }
                        }
                        break;
                    case DGV_ROW_INDEX.HOME_SEARCH_OPTION:
                        {
                             Rectangle temp=DGV_LPAParam.GetCellDisplayRectangle(e.ColumnIndex,e.RowIndex,true);
                             HomeOptionCombo.Location=new Point(temp.X,temp.Y);
                             HomeOptionCombo.Width=temp.Width;
                             HomeOptionCombo.Height=temp.Height;
                             HomeOptionCombo.SelectedIndex=pItem.HomeSearchOption;    
                             HomeOptionCombo.DroppedDown=true;                
                             HomeOptionCombo.Show();
                        }
                        break;
                    default:
                        break;
                }              
            }
        }

   
    }
}
