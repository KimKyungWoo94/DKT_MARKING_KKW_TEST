using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
		public partial class FrmInforOperAuto_ProcessStatus : Form
		{

				private int m_iUpdateStartSetNum=0;
				private int m_iUpdateStartNum=0;
				bool m_bProcessInfoRowAddEnable = false;
				private int m_iLoopDGVIDX=0;
				Color m_DisplayOKColor = Color.LimeGreen;
				Color m_DisplayMisMatchColor = Color.Orange;
				Color m_DisplayFailColor = Color.Red;
				Color m_DisplayDefaultColor = Color.Black;
				public FrmInforOperAuto_ProcessStatus()
				{
						InitializeComponent();
						StatusUpdateTimer.Enabled=false;
						//StatusUpdateTimer.Interval
						StatusUpdateTimer.Tick+=UpdateStatus;
				}
				private void UpdateDisplayProcessInfo()
				{
						try
						{
								
							  lb_Status_StartNum.Text=(m_iUpdateStartSetNum+1).ToString();
								m_iUpdateStartNum=m_iUpdateStartSetNum;
								if (FA.MGR.RecipeRunningData.pCurrentProcessData != null)
								{
										DGV_ProcessInfo.SuspendDrawing();
										m_bProcessInfoRowAddEnable = false;
										if (((FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount-m_iUpdateStartNum) != DGV_ProcessInfo.RowCount))
										{
												DGV_ProcessInfo.Rows.Clear();
												m_bProcessInfoRowAddEnable = true;
										}
										for (int i = m_iUpdateStartNum; i < FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount; i++)
										{
												m_iLoopDGVIDX=i-m_iUpdateStartNum;
												if (m_bProcessInfoRowAddEnable)
												{
														DGV_ProcessInfo.Rows.Add();
														//DGV_ProcessInfo.Rows[m_iLoopDGVIDX].HeaderCell.Value=(i+1).ToString();
												}
												/*
												 		a_DatagirdView.Columns.AddRange(
															CreateDataGridViewTextColumn("MarkingNo","",		(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
															CreateDataGridViewTextColumn("PosINSP","",			(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
															CreateDataGridViewTextColumn("Marking","" ,			(int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25)),
															CreateDataGridViewTextColumn("MarkingINSP", "", (int)((a_DatagirdView.Width-a_DatagirdView.RowHeadersWidth-iHScrollbarSize)*0.25))
															);
												*/

											
												
												if (FA.MGR.RecipeRunningData.pCurrentProcessData != null && FA.MGR.RecipeRunningData.pCurrentProcessData[i].pDataMatrix != null)
												{
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingNo"].Value = string.Format("{0}", FA.MGR.RecipeRunningData.pCurrentProcessData != null ? string.Format("{0}", FA.MGR.RecipeRunningData.pCurrentProcessData[i].strMarkingIDX) : "");
													  //string.Format("{0}", m_pDisPlayJIGData != null ? string.Format("{0}({1})", m_pDisPlayJIGData[i].strMarkingIDX_TO_32, m_pDisPlayJIGData[i].iMarkingIDX.ToString()) : "");
												}
												else
												{
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingNo"].Value = "";
												}

												#region			POS INSP
												if (FA.MGR.RecipeRunningData.pCurrentProcessData != null && FA.MGR.RecipeRunningData.pCurrentProcessData[i].bPosInspExecuted)
												{
														if (FA.MGR.RecipeRunningData.pCurrentProcessData[i].pMatchResult != null)
														{
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Style.ForeColor = m_DisplayOKColor;
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Value = "OK";
														}
														else
														{
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Style.ForeColor = m_DisplayFailColor;
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Value = "FAIL";
														}
												}
												else
												{
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Style.ForeColor = m_DisplayDefaultColor;
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["PosINSP"].Value = "";
												}
												#endregion	POS INSP
												#region			Makring
												if (FA.MGR.RecipeRunningData.pCurrentProcessData != null && FA.MGR.RecipeRunningData.pCurrentProcessData[i].bMarkingDone)
												{
														if (FA.MGR.RecipeRunningData.pCurrentProcessData[i].pMatchResult != null)
														{
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Style.ForeColor = m_DisplayOKColor;
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Value = "DONE";
														}
														else
														{
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Style.ForeColor = m_DisplayDefaultColor;
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Value = "";
														}


												}
												else
												{
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Style.ForeColor = m_DisplayDefaultColor;
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["Marking"].Value = "";
												}
												#endregion	Marking
												#region			Marking INSP
												if (FA.MGR.RecipeRunningData.pCurrentProcessData != null && FA.MGR.RecipeRunningData.pCurrentProcessData[i].bMarkingInspExecuted)
												{
														if (FA.MGR.RecipeRunningData.pCurrentProcessData[i].bMarkingDone)
														{

																if (FA.MGR.RecipeRunningData.pCurrentProcessData[i].pMatrixCodeResult != null)

																{
																		if (FA.MGR.RecipeRunningData.pCurrentProcessData[i].pMatrixCodeResult.m_bFound == true)
																		{
																				if (string.Equals(FA.MGR.RecipeRunningData.pCurrentProcessData[i].pDataMatrix.DatamatrixText,
																						FA.MGR.RecipeRunningData.pCurrentProcessData[i].pMatrixCodeResult.m_strDecodedString))
																				{
																						DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayOKColor;
																						DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "OK";
																				}
																				else
																				{
																						DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayMisMatchColor;
																						DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "MIS Match";
																				}
																		}
																		else
																		{
																				DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayFailColor;
																				DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "Not Found";
																		}
																}
																else
																{
																		DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayFailColor;
																		DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "Not Found";
																}
														}
														else
														{
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayDefaultColor;
																DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "";
														}
												}
												else
												{
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Style.ForeColor = m_DisplayDefaultColor;
														DGV_ProcessInfo.Rows[m_iLoopDGVIDX].Cells["MarkingINSP"].Value = "";
												}
												#endregion	Marking INSP
										}
										DGV_ProcessInfo.ResumeDrawing();
								}
						}
						catch (Exception ex)
						{

						}
				}

				private void btn_Frm_Close_Click(object sender, EventArgs e)
				{
						this.Hide();
				}

				private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
				{
						WinAPIs.ReleaseCapture();
						WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
				}
				private void UpdateStatus(object sender, EventArgs e)
				{
						this.StatusUpdateTimer.Enabled=false;						
						UpdateDisplayProcessInfo();
						this.StatusUpdateTimer.Enabled=this.Visible;
				}
				private void FrmFrmInforOperAuto_ProcessStatus_Load(object sender, EventArgs e)
				{
					 FA.MGR.RecipeRunningData.InitDGV_DefaultParam(DGV_ProcessInfo);
					 DGV_ProcessInfo.RowPostPaint+=dgGrid_RowPostPaint;
				}
				private void FrmInforOperAuto_ProcessStatus_VisibleChanged(object sender, EventArgs e)
				{
						this.StatusUpdateTimer.Enabled=this.Visible;						
				}

				private void lb_Status_StartNum_Click(object sender, EventArgs e)
				{
						EzIna.GUI.UserControls.NumberKeypad pNumberKeyPad=new GUI.UserControls.NumberKeypad();
						if(pNumberKeyPad.ShowDialog(1,FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount,m_iUpdateStartNum+1)==DialogResult.OK)
						{
								if(FA.MGR.RecipeRunningData.pCurrentProcessData.iProcessDataListCount>(int)pNumberKeyPad.Result)
								m_iUpdateStartSetNum=(int)pNumberKeyPad.Result-1;
																
						}
				}

				private void dgGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
				{
						var grid = sender as DataGridView;
						var rowIdx = (m_iUpdateStartNum+e.RowIndex + 1).ToString();

						var centerFormat = new StringFormat()
						{
								// right alignment might actually make more sense for numbers
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center
						};
						var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
						e.Graphics.DrawString(rowIdx, grid.DefaultCellStyle.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
				}
			
		}
}
