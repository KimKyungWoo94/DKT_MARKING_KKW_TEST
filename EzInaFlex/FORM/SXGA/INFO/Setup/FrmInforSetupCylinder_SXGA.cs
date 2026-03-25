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

namespace EzIna
{
		public partial class FrmInforSetupCylinder_SXGA : Form
		{
				Timer m_Timer = null;
				BindingList<EzIna.IO.BaseIO> m_CylinderIOBindingList;
				NumberKeypad FrmNumberKeyPad;
				int m_iExpandIDX = -1;
				public FrmInforSetupCylinder_SXGA()
				{
						InitializeComponent();
						m_Timer = new Timer();
						FrmNumberKeyPad = new NumberKeypad();
						m_Timer.Interval = 100;
						m_Timer.Tick += new EventHandler(this.Display);
						m_CylinderIOBindingList = new BindingList<IO.BaseIO>();
				}

				private void FrmInforSetupCylinder_Load(object sender, EventArgs e)
				{
						DGV_CYLINDER.ExpandChange += DGVCylinder_ExpandChange;
						DGV_CYLINDER.ChildDataGridView.DataSource = m_CylinderIOBindingList;
						DGV_CYLINDER.ChildDataGridView.RowsAdded += DGV_CylinderIO_RowAdded;
						DGV_CYLINDER.ChildDataGridView.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
						DGV_CYLINDER.ChildDataGridView.DataBindingComplete += DGV_CYLINDER_IO_DataBindingComplete;
						DGV_CYLINDER.ChildDataGridView.CellClick += DGV_CYLINDER_IO_CellClick;

						EzIna.FA.MGR.IOMgr.InitializeCylinderDataGridView(DGV_CYLINDER);
						EzIna.FA.MGR.IOMgr.CreateColumnCylinderIODataGridView(DGV_CYLINDER.ChildDataGridView);
						EzIna.ControlHelper.DoubleBuffered(DGV_CYLINDER, true);
						EzIna.ControlHelper.DoubleBuffered(DGV_CYLINDER.ChildDataGridView, true);
				}
				private void FrmInforSetupCylinder_VisibleChanged(object sender, EventArgs e)
				{
						m_Timer.Enabled = this.Visible;
				}
				private void DGVCylinder_ExpandChange(object sender, GUI.UserControls.ExpandDGVEventArgs e)
				{
						if (e.IsExpand == true)
						{
								if (e.RowIndex > -1)
								{
										if (e.OldRowIndex.Equals(e.RowIndex) == false)
										{
												m_CylinderIOBindingList.Clear();
												if (FA.MGR.IOMgr.CylinderList[e.RowIndex].OperType == IO.CylinderType.SINGLE)
												{
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].ForwardSensor);
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].ForwardSol);
												}
												else
												{
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].ForwardSensor);
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].BackwardSensor);
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].ForwardSol);
														m_CylinderIOBindingList.Add(FA.MGR.IOMgr.CylinderList[e.RowIndex].BackwardSol);
												}
												m_CylinderIOBindingList.ResetBindings();
												
										}
								}
						}
				}
				private void DataGridVeiwNoSelection_SelectionChanged(object sender, EventArgs e)
				{
						DataGridView pcontrol = sender as DataGridView;
						pcontrol.ClearSelection();
				}
				private void DGV_CylinderIO_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
				{
						//DataGridView pCon=sender as DataGridView;
						//for(int i=0;i<pCon.Rows[e.RowIndex].Cells.Count;i++)
						//{
						//    //pCon.Rows[e.RowIndex].Cells[i].Style=pCon.Rows[e.RowIndex].Cells[i].OwningColumn.DefaultCellStyle;
						//}
				}
				private void Display(object sender, EventArgs e)
				{
						m_Timer.Enabled = false;
						UpdateDisplayDGV_Cylinder();
						UpdateDisplayDGV_CylinderIO();
						m_Timer.Enabled = this.Visible;
				}
				public void FlickrDisplay()
				{

				}

				private void UpdateDisplayDGV_Cylinder()
				{
						EzIna.FA.MGR.IOMgr.UpdateCylinderInfor(DGV_CYLINDER);
				}
				private void UpdateDisplayDGV_CylinderIO()
				{
						if (DGV_CYLINDER.ChildDataGridViewVisble && DGV_CYLINDER.ChildDataGridView.Rows.Count > 0)
						{

								foreach (DataGridViewRow Item in DGV_CYLINDER.ChildDataGridView.Rows)
								{
										if (m_CylinderIOBindingList[Item.Index].GetType() == typeof(EzIna.IO.DI))
										{
												Item.Cells["Status"].Value = (m_CylinderIOBindingList[Item.Index] as EzIna.IO.DI).Value;
										}
										else if (m_CylinderIOBindingList[Item.Index].GetType() == typeof(EzIna.IO.DO))
										{
												Item.Cells["Status"].Value = (m_CylinderIOBindingList[Item.Index] as EzIna.IO.DO).Value;
										}
								}
						}
				}

				private void DGV_CYLINDER_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
				{

				}
				private void DGV_CYLINDER_IO_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
				{
						if (DGV_CYLINDER.ChildDataGridView.Rows.Count == 2)
						{
								DGV_CYLINDER.ChildDataGridView.Rows[0].HeaderCell.Value = "F.Sensor";
								DGV_CYLINDER.ChildDataGridView.Rows[1].HeaderCell.Value = "F.Sol";

						}
						if (DGV_CYLINDER.ChildDataGridView.Rows.Count == 4)
						{
								DGV_CYLINDER.ChildDataGridView.Rows[0].HeaderCell.Value = "F.Sensor";
								DGV_CYLINDER.ChildDataGridView.Rows[1].HeaderCell.Value = "B.Sensor";
								DGV_CYLINDER.ChildDataGridView.Rows[2].HeaderCell.Value = "F.Sol";
								DGV_CYLINDER.ChildDataGridView.Rows[3].HeaderCell.Value = "B.Sol";
						}
						foreach (DataGridViewRow Item in DGV_CYLINDER.ChildDataGridView.Rows)
						{
								Item.Cells["Addr0"].Value = m_CylinderIOBindingList[Item.Index].AddressInfo.m_strParamList[0];
								Item.Cells["Addr1"].Value = m_CylinderIOBindingList[Item.Index].AddressInfo.m_strParamList[1];
								Item.Cells["Addr2"].Value = m_CylinderIOBindingList[Item.Index].AddressInfo.m_strParamList[2];
								Item.Cells["Addr3"].Value = m_CylinderIOBindingList[Item.Index].AddressInfo.m_strParamList[3];
								Item.Cells["Addr4"].Value = m_CylinderIOBindingList[Item.Index].AddressInfo.m_strParamList[4];
						}
						//DGV_CYLINDER.Invalidate();
				}
				private void DGV_CYLINDER_CellClick(object sender, DataGridViewCellEventArgs e)
				{
						if (e.RowIndex < 0 || e.ColumnIndex < 0)
								return;

						string ComfirmMsg;
						if (DGV_CYLINDER.Columns["Forward"].Index == e.ColumnIndex)
						{
								ComfirmMsg = string.Format("Would like Action Forward Cylinder?\nID:{0}\nStatus:{1} ",
										DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value,
										DGV_CYLINDER.Rows[e.RowIndex].Cells["Status"].Value);
								if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
								{
										EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].Action(true);
								}
						}
						else if (DGV_CYLINDER.Columns["Backward"].Index == e.ColumnIndex)
						{
								ComfirmMsg = string.Format("Would like Action Backward Cylinder?\nID:{0}\nStatus:{1} ",
									 DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value,
									 DGV_CYLINDER.Rows[e.RowIndex].Cells["Status"].Value);
								if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
								{
										EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].Action(false);
								}
						}
						else if (DGV_CYLINDER.Columns["F.SensorCheck"].Index == e.ColumnIndex)
						{
								ComfirmMsg = string.Format("Would like Change Forward Sensor check Setting?\nID:{0}\nStatus:{1} -> {2} ",
									DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value,
									(bool)DGV_CYLINDER.Rows[e.RowIndex].Cells["F.SensorCheck"].Value,
									!(bool)DGV_CYLINDER.Rows[e.RowIndex].Cells["F.SensorCheck"].Value);
								if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
								{
										EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].bForwardSersorCheck = !EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].bForwardSersorCheck;
								}
						}
						else if (DGV_CYLINDER.Columns["B.SensorCheck"].Index == e.ColumnIndex)
						{
								ComfirmMsg = string.Format("Would like Change Backward Sensor check Setting?\nID:{0}\nStatus:{1} -> {2} ",
									 DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value,
									 (bool)DGV_CYLINDER.Rows[e.RowIndex].Cells["B.SensorCheck"].Value,
									 !(bool)DGV_CYLINDER.Rows[e.RowIndex].Cells["B.SensorCheck"].Value);
								if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
								{
										EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].bBackwardSersorCheck = !EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].bBackwardSersorCheck;
								}
						}
						else if (DGV_CYLINDER.Columns["RepeatCount"].Index == e.ColumnIndex)
						{
								if (FrmNumberKeyPad.ShowDialog(0, 10000000, EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].RepeatCount) == DialogResult.OK)
								{
										EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].RepeatCount = (int)FrmNumberKeyPad.Result;
										FrmNumberKeyPad.Result = 0.0;
								}
						}
						else if (DGV_CYLINDER.Columns["RepeatStatus"].Index == e.ColumnIndex)
						{
								if (EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].IsRepeatActionExcute == false)
								{
										ComfirmMsg = string.Format("Would like Start Repeat Action?\nID:{0}",
										 DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value);
										if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
										{
												EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].StartRepeatAction();
										}
								}
								else
								{
										ComfirmMsg = string.Format("Would like Stop Repeat Action?\nID:{0}",
										DGV_CYLINDER.Rows[e.RowIndex].Cells["ID"].Value);
										if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
										{
												EzIna.FA.MGR.IOMgr.CylinderList[e.RowIndex].StopRepeatAction();
										}
								}
						}


				}
				private void DGV_CYLINDER_IO_CellClick(object sender, DataGridViewCellEventArgs e)
				{
						if (e.RowIndex < 0 || e.ColumnIndex < 0)
								return;
				}
		}
}
