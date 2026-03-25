using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static EzIna.FA.DEF;
namespace EzIna
{
    public partial class FrmTabInitialProcessPowerTable : Form
    {
        DataGridView m_DGV_PARAM;
        GUI.UserControls.ExpandDataGridView m_DGV_GROUP;
        private Laser.LaserPwrTableData m_pPwrTableData;
        public FrmTabInitialProcessPowerTable()
        {
            InitializeComponent();

            m_DGV_PARAM = new DataGridView();
            m_DGV_GROUP = new GUI.UserControls.ExpandDataGridView();
            m_DGV_PARAM.Dock = DockStyle.Fill;
            m_DGV_GROUP.Dock = DockStyle.Fill;
            m_DGV_PARAM.DoubleBuffered(true);
            m_DGV_GROUP.DoubleBuffered(true);
            InitializeChart();
            Timer_Display.Tick += this.Display;
            //Panel_DataGridview.Controls.Add(m_DGV_PARAM);
            // Panel_DataGridview.Controls.Add(m_DGV_GROUP);

        }
        private void InitializeChart()
        {
            PowerTableChart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            PowerTableChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            PowerTableChart.Series[0].XValueType = ChartValueType.Auto;
            PowerTableChart.ChartAreas[0].AxisX.LabelStyle.Format = "0.00%";
            PowerTableChart.ChartAreas[0].AxisY.LabelStyle.Format = "0.00W";
            //PowerTableChart.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Milliseconds;
            //PowerTableChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //PowerTableChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Milliseconds;
            PowerTableChart.ChartAreas[0].AxisX.Interval = 0;
            PowerTableChart.ChartAreas[0].Area3DStyle.Enable3D = false;
        }
        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Timer_Display.Enabled = this.Visible;
            }
        }
        private void Display(object sender, EventArgs e)
        {
            Timer_Display.Enabled = false;
            LED_SYSTEM_READY.Value = FA.LASER.Instance.IsLaserReady;
            LED_EMISSION.Value = FA.LASER.Instance.IsEmissionOn;
            LED_LaserON.Value = FA.LASER.Instance.IsEmissionOn & (FA.LASER.Instance.RepetitionRate > 2000);
            GAUGE_SetPowerPercent.Value = (float)FA.LASER.Instance.SetDiodeCurrent;

            lb_OutTrigger_FREQ.Text = string.Format("{0:F3} KHz", FA.LASER.Instance.RepetitionRate / 1000.0);
            lb_SettingTrigger_FREQ.Text = string.Format("{0:F3} KHz", m_pPwrTableData != null ? m_pPwrTableData.iDefaultFrequency / 1000.0 : 0);
            Timer_Display.Enabled = this.Visible;
        }
        private void treeView_Menu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                lb_SelSubCategory.Text = e.Node.Text;

                m_pPwrTableData = FA.MGR.LaserMgr.GetPwrTableData(lb_SelSubCategory.Text).Clone() as Laser.LaserPwrTableData;
                if (m_pPwrTableData != null)
                {
                    PowerTableChart.ResetAutoValues();
                    PowerTableChart.Series[0].Points.Clear();
                    PowerTableChart.ChartAreas[0].AxisX.Minimum = m_pPwrTableData.MinimumPercent;
                    PowerTableChart.ChartAreas[0].AxisX.Maximum = m_pPwrTableData.MaximumPercent;
                    PowerTableChart.ChartAreas[0].AxisY.Minimum = m_pPwrTableData.MinimumPower;
                    PowerTableChart.ChartAreas[0].AxisY.Maximum = m_pPwrTableData.MaximumPower;
                    foreach (Laser.LaserPowerTableItem Item in m_pPwrTableData.TableItemList)
                    {
                        PowerTableChart.Series[0].Points.Add(new DataPoint(Item.PercentValue, Item.PowerValue));
                    }
                    PowerTableChart.ChartAreas[0].AxisX.Interval = 0.05;
                    PowerTableChart.ChartAreas[0].AxisY.Interval = (m_pPwrTableData.MaximumPower - m_pPwrTableData.MinimumPower) / 8;
                    DGV_PowerTableData.DataSource = new BindingList<Laser.LaserPowerTableItem>(m_pPwrTableData.TableItemList);
                }
                /*if (MF.RCP_Modify.IsExistGroupitem(FA.DEF.eRecipeCategory.PROCESS, e.Node.Text))
                {
                    if (m_DGV_PARAM.Visible == true)
                        m_DGV_PARAM.Visible = false;

                    m_DGV_GROUP.Visible = true;
                }
                else
                {
                    if (m_DGV_GROUP.Visible == true)
                        m_DGV_GROUP.Visible = false;

                    FA.MGR.RecipeMgr.InitDGV_DefaultParam(m_DGV_PARAM, FA.DEF.eRecipeCategory.PROCESS, e.Node.Text);
                    m_DGV_PARAM.Visible = true;
                }*/

            }
        }
        public void Read_From()
        {
            //MF.RCP.Read_From(m_eSelectedCategory, m_eSelectedSubCategory, dataGridView_Datas);
            //MF.OPT.Read_Form(m_eSelectedSubCategory, dataGridView_Options					);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas);
            //FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);
            FA.MGR.LaserMgr.TreeView_InitPowerTable(treeView_Menu);
            FA.MGR.LaserMgr.DGV_InitPowerTable(DGV_PowerTableData);
        }

        private void btn_TableSave_Click(object sender, EventArgs e)
        {
            Laser.LaserPwrTableData pPwrTable = FA.MGR.LaserMgr.GetPwrTableData(lb_SelSubCategory.Text);
            if (pPwrTable != null)
            {
                if (FA.MGR.LaserMgr.IsDataSerializePercent_PwrTableData(pPwrTable.iDefaultFrequency.ToString()) == false)
                {
                    MsgBox.Error("Percent Data isn't Serial");
                    return;
                }
                if (FA.MGR.LaserMgr.IsDataSerializePower_PwrTableData(pPwrTable.iDefaultFrequency.ToString()) == false)
                {
                    MsgBox.Error("Power Data isn't Serial");
                    return;
                }
                if (MsgBox.Confirm("Would like Save Laser Power Table ?"))
                {
                    FA.MGR.LaserMgr.AddPwrTable(m_pPwrTableData);
                    FA.MGR.LaserMgr.SavePowerTableData();
                }
            }
        }
        private void btn_EmssionOn_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Would like Save Laser Emission Enable ?"))
            {
                eDO.LASER_EM_ENABLE.GetDO().Value = true;
            }
        }
        private void btn_EmssionOff_Click(object sender, EventArgs e)
        {
            eDO.LASER_EM_ENABLE.GetDO().Value = false;
        }

        private void Btn_ScannerLaserOn_Click(object sender, EventArgs e)
        {
            Laser.LaserPwrTableData pPwrTable = FA.MGR.LaserMgr.GetPwrTableData(lb_SelSubCategory.Text);
            if (pPwrTable != null)
            {
                if (FA.LASER.Instance.IsEmissionOn)
                {

                    if (MsgBox.Confirm("Would like to Laser ON ?"))
                    {
                        FA.RTC5.Instance.ConfigData.FreQuency = pPwrTable.iDefaultFrequency;
                        FA.RTC5.Instance.ConfigData.FreQPulseLength = (FA.RTC5.Instance.ConfigData.FreQHalfPeriod * 2) * 0.5;
                        FA.RTC5.Instance.LaserOn();
                    }
                }
                else
                {
                    MsgBox.Show("Emission Enable First");
                }
            }
            else
            {
                MsgBox.Show("Select Power Table First");
            }
        }

        private void Btn_ScannerLaserOff_Click(object sender, EventArgs e)
        {
            FA.RTC5.Instance.ListStop();
            FA.RTC5.Instance.LaserOff();
        }

        private void lb_SettingTrigger_FREQ_Click(object sender, EventArgs e)
        {
            Laser.LaserPwrTableData pPwrTable = FA.MGR.LaserMgr.GetPwrTableData(lb_SelSubCategory.Text);
            if (m_pPwrTableData != null)
            {
                GUI.UserControls.NumberKeypad pNumberKeyPad = new GUI.UserControls.NumberKeypad();
                if (pNumberKeyPad.ShowDialog(Scanner.ScanlabRTC5.MinFreQuency / 1000.0, Scanner.ScanlabRTC5.MaxFreQuency / 1000.0, pPwrTable.iDefaultFrequency / 1000) == DialogResult.OK)
                {
                    TreeNode pOldNode = treeView_Menu.Nodes[0].Nodes[lb_SelSubCategory.Text];
                    FA.MGR.LaserMgr.SetPwrTableDataDefaultFrequency(pPwrTable.iDefaultFrequency.ToString(), (int)pNumberKeyPad.Result * 1000);
                    pOldNode.Text = pPwrTable.iDefaultFrequency.ToString();
                    pOldNode.Name = pPwrTable.iDefaultFrequency.ToString();
                    treeView_Menu_NodeMouseClick(this.treeView_Menu, new TreeNodeMouseClickEventArgs(pOldNode, new MouseButtons(), 0, 0, 0));
                }
            }
        }

        private void DGV_PowerTableData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (string.IsNullOrEmpty(lb_SelSubCategory.Text) == false)
            {
                //m_pPwrTableData = FA.MGR.LaserMgr.GetPwrTableData(lb_SelSubCategory.Text).Clone() as Laser.LaserPwrTableData;
                if (m_pPwrTableData != null)
                {
                    PowerTableChart.ResetAutoValues();
                    PowerTableChart.Series[0].Points.Clear();
                    PowerTableChart.ChartAreas[0].AxisX.Minimum = m_pPwrTableData.MinimumPercent;
                    PowerTableChart.ChartAreas[0].AxisX.Maximum = m_pPwrTableData.MaximumPercent;
                    PowerTableChart.ChartAreas[0].AxisY.Minimum = m_pPwrTableData.MinimumPower;
                    PowerTableChart.ChartAreas[0].AxisY.Maximum = m_pPwrTableData.MaximumPower;
                    foreach (Laser.LaserPowerTableItem Item in m_pPwrTableData.TableItemList)
                    {
                        PowerTableChart.Series[0].Points.Add(new DataPoint(Item.PercentValue, Item.PowerValue));
                    }
                    PowerTableChart.ChartAreas[0].AxisX.Interval = 0.05;
                    PowerTableChart.ChartAreas[0].AxisY.Interval = (m_pPwrTableData.MaximumPower - m_pPwrTableData.MinimumPower) / 8;
                    //DGV_PowerTableData.DataSource = new BindingList<Laser.LaserPowerTableItem>(m_pPwrTableData.TableItemList);
                }
            }
        }

        private void GAUGE_SetPowerPercent_MouseClick(object sender, MouseEventArgs e)
        {

            GUI.UserControls.NumberKeypad FrmNumberKeyPad = new GUI.UserControls.NumberKeypad();
            if (FrmNumberKeyPad.ShowDialog(0, 100, FA.LASER.Instance.SetDiodeCurrent) == DialogResult.OK)
            {
                FA.LASER.Instance.SetDiodeCurrent = (float)FrmNumberKeyPad.Result;
            }
        }
    }
}
