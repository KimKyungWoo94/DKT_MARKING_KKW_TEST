using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
		public enum eProductResult { Good, Reject }
		public class RunningPM
		{
				private string m_strWaferID;
				private string m_strLotID;

				private long m_lGoodCount;
				private long m_lRejectCount;

				private long m_lOldGoodCount;
				private long m_lOldRejectCount;

				private long m_lErrorCount;
				private long m_lUPHCount;

				private Stopwatch m_CycleTimer = new Stopwatch();
				private Stopwatch m_RunningTimer = new Stopwatch();
				private Stopwatch m_StopTimer = new Stopwatch();
				private Stopwatch m_ErrorTimer = new Stopwatch();
				private Stopwatch m_UPHTimer = new Stopwatch();


				private TimeSpan m_OldRunningTime = TimeSpan.Zero;
				private TimeSpan m_OldStopTime = TimeSpan.Zero;
				private TimeSpan m_OldErrorTime = TimeSpan.Zero;
				private TimeSpan m_OldCycleCyleTime = TimeSpan.Zero;
				private DateTime m_OldStartCyleTime = DateTime.MinValue;
				public TimeSpan CycleTime { get { return m_OldCycleCyleTime; } }
				public TimeSpan RunningTime { get { return m_RunningTimer.Elapsed + m_OldRunningTime; } }
				public TimeSpan StopTime { get { return m_StopTimer.Elapsed + m_OldStopTime; } }
				public TimeSpan ErrorTime { get { return m_ErrorTimer.Elapsed + m_OldErrorTime; } }
				public TimeSpan TotalTime { get { return RunningTime + StopTime + ErrorTime; } }

				public long GoodCount { get { return m_lGoodCount; } }
				public long RejectCount { get { return m_lRejectCount; } }
				public long ProductCount { get { return m_lGoodCount + m_lRejectCount; } }
				public long TotalGoodCount { get { return m_lOldGoodCount + m_lGoodCount; } }
				public long TotalRejectCount { get { return m_lOldRejectCount + m_lRejectCount; } }
				public long TotalProductCount { get { return TotalGoodCount + TotalRejectCount; } }

				public RunningPM()
				{
						m_strWaferID = "";
						m_strLotID = "";

						m_CycleTimer = new Stopwatch();
						m_RunningTimer = new Stopwatch();
						m_StopTimer = new Stopwatch();
						m_ErrorTimer = new Stopwatch();
						m_UPHTimer = new Stopwatch();

						m_OldRunningTime = new TimeSpan();
						m_OldStopTime = new TimeSpan();
						m_OldErrorTime = new TimeSpan();
						m_OldCycleCyleTime = new TimeSpan();

						m_OldRunningTime = TimeSpan.Zero;
						m_OldStopTime = TimeSpan.Zero;
						m_OldErrorTime = TimeSpan.Zero;
						m_OldCycleCyleTime = TimeSpan.Zero;


						m_lGoodCount = 0;
						m_lRejectCount = 0;

						m_lOldGoodCount = 0;
						m_lOldRejectCount = 0;

						m_lErrorCount = 0;
						m_lUPHCount = 0;

				}
				/// <summary>
				/// MTBF(Mean Time Between Failure) : 평균 고장 시간 간격.
				/// </summary>
				/// <returns>Time</returns>
				public TimeSpan GetMTBF()
				{
						long den = m_lErrorCount + 1;

						return TimeSpan.FromTicks(RunningTime.Ticks / den.EnsureRange(1, long.MaxValue));
				}
				/// <summary>
				/// UPH(Unit Per Hour)
				/// </summary>
				/// <returns>Time</returns>
				public double GetUPH()
				{
						if (m_UPHTimer.Elapsed.Equals(TimeSpan.Zero))
								return 0;
						else
								return m_lUPHCount / m_UPHTimer.Elapsed.TotalHours;
				}
				public void ClearTotalGoodCount()
				{
						m_lGoodCount = 0;
						m_lOldGoodCount = 0;
				}
				public void ClearTotalRejectCount()
				{
						m_lRejectCount = 0;
						m_lOldRejectCount = 0;
				}
				public void ClearAllProductCount()
				{
						ClearTotalGoodCount();
						ClearTotalRejectCount();
				}
				public void ClearTimes()
				{
						m_OldRunningTime = TimeSpan.Zero;
						m_OldStopTime = TimeSpan.Zero;
						m_OldErrorTime = TimeSpan.Zero;

						m_CycleTimer.Reset();
						m_RunningTimer.Reset();
						m_StopTimer.Reset();
						m_ErrorTimer.Reset();
						m_UPHTimer.Reset();
				}
				public void ClearErrorCount()
				{
						m_lErrorCount = 0;
				}
        public void SetLotCode(string a_strValue)
        {
            m_strLotID = a_strValue;
        }

				public void AddProduct(eProductResult a_eType)
				{
						if (FA.MGR.RunMgr.eRunMode == FA.DEF.eRunMode.Run && !m_UPHTimer.IsRunning)
								m_UPHTimer.Start(); // _UPHTimer 자동 실행

						m_lUPHCount++;
						switch (a_eType)
						{
								case eProductResult.Good:
										m_lGoodCount++;
										break;
								case eProductResult.Reject:
										m_lRejectCount++;
										break;
						}
				}

				public void StartUPH()
				{
						if (!m_UPHTimer.IsRunning)
								m_UPHTimer.Start();
				}
				public void StopUPH()
				{
						m_UPHTimer.Stop();
				}
				public void ResetUPH()
				{
						m_UPHTimer.Reset();
						m_lUPHCount = 0;
				}
				public void StartCycleTime()
				{
						m_CycleTimer.Start();
						m_OldStartCyleTime=DateTime.Now;
				}
				public void StopCycleTime()
				{
						m_OldCycleCyleTime = m_CycleTimer.Elapsed;
						m_CycleTimer.Stop();
				}
				public void ResetCycleTime()
				{
						m_CycleTimer.Reset();
						m_OldCycleCyleTime = TimeSpan.Zero;
				}
				public bool IsCycleTimerRunning()
				{
						return m_CycleTimer.IsRunning;
				}
				public TimeSpan CurrentCycleTime()
				{
						return m_CycleTimer.Elapsed; 
				}
				public void UpdateTime(FA.DEF.eRunMode eMode)
				{
						switch (eMode)
						{
								case FA.DEF.eRunMode.Init:
										{
												m_RunningTimer.Stop();
												m_StopTimer.Stop();
												m_ErrorTimer.Stop();
										}

										break;
								case FA.DEF.eRunMode.Run:
										{
												m_RunningTimer.Start();
												m_StopTimer.Stop();
												m_ErrorTimer.Stop();
										}
										break;
								case FA.DEF.eRunMode.Stop:
										{
												m_RunningTimer.Stop();
												m_StopTimer.Start();
												m_ErrorTimer.Stop();
										}
										break;
								case FA.DEF.eRunMode.Jam:
										{
												m_RunningTimer.Stop();
												m_StopTimer.Stop();
												m_ErrorTimer.Start();
												m_lErrorCount++;
										}
										break;
						}
				}


				public void JobStart(string a_strIniPath)
				{
						IniFile ini = new IniFile(a_strIniPath);

						m_OldRunningTime = ini.Read("WORKS_INFO", "RunningTime", m_OldRunningTime);
						m_OldStopTime = ini.Read("WORKS_INFO", "StopTime", m_OldStopTime);
						m_OldErrorTime = ini.Read("WORKS_INFO", "ErrorTime", m_OldErrorTime);

						m_lOldGoodCount = ini.Read("WORKS_INFO", "GoodCount", m_lGoodCount);
						m_lOldRejectCount = ini.Read("WORKS_INFO", "RejectCount", m_lOldRejectCount);
						m_lErrorCount = ini.Read("WORKS_INFO", "ErrorCount", m_lErrorCount);

						m_lGoodCount = 0;
						m_lRejectCount = 0;

				}
				public void JIGFinish(string a_strLotCode, string a_strJigCode,
															string a_strStartNumber,string a_strEndNumber
															)
				{
						DateTime pEndTime=m_OldStartCyleTime.Add(CycleTime);
						FA.LOG.InfoLot
						(
							// LOT CODE | JIG Code | StartNumber | EndNumber | Start time | finish time | Run time
							//	  0            1           2					3						4              5           6
							"{0},{1},{2},{3},{4},{5},{6}",
							a_strLotCode,
							a_strJigCode,
							a_strStartNumber,
							a_strEndNumber,
							string.Format("{0:D2}:{1:D2}:{2:D2}", m_OldStartCyleTime.Hour, m_OldStartCyleTime.Minute, m_OldStartCyleTime.Second),							
							string.Format("{0:D2}:{1:D2}:{2:D2}", pEndTime.Hour, pEndTime.Minute, pEndTime.Second),
							string.Format("{0:D2}:{1:D2}:{2:D3}", CycleTime.Hours, CycleTime.Minutes, CycleTime.Seconds)
						);
				}
				public void JobFinish(string a_strIniPath)
				{
						FA.LOG.InfoLot
						(
							"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
							m_strLotID,
							string.Format("{0:D2}:{1:D2}:{2:D2}", RunningTime.Hours, RunningTime.Minutes, RunningTime.Seconds),
							string.Format("{0:D2}:{1:D2}:{2:D3}", StopTime.Hours, StopTime.Minutes, StopTime.Seconds),
							string.Format("{0:D2}:{1:D2}:{2:D3}", ErrorTime.Hours, ErrorTime.Minutes, ErrorTime.Seconds),
							string.Format("{0:D2}:{1:D2}:{2:D3}", TotalTime.Hours, TotalTime.Minutes, TotalTime.Seconds),
							GetUPH().ToString("F2"),
							string.Format("{0:D2}:{1:D2}:{2:D3}", GetMTBF().Hours, GetMTBF().Minutes, GetMTBF().Seconds),
							GoodCount.ToString(),
							RejectCount.ToString(),
							ProductCount.ToString(),
							TotalGoodCount.ToString(),
							TotalRejectCount.ToString(),
							TotalProductCount.ToString()
						);


						m_OldRunningTime = RunningTime;
						m_OldStopTime = StopTime;
						m_OldErrorTime = ErrorTime;


						if (m_RunningTimer.IsRunning)
								m_RunningTimer.Restart();
						else
								m_RunningTimer.Reset();

						if (m_StopTimer.IsRunning)
								m_StopTimer.Restart();
						else
								m_StopTimer.Reset();

						if (m_ErrorTimer.IsRunning)
								m_ErrorTimer.Restart();
						else
								m_ErrorTimer.Reset();

						m_lOldGoodCount = GoodCount;
						m_lOldRejectCount = RejectCount;

						m_lGoodCount = 0;
						m_lRejectCount = 0;

						m_strWaferID = "";
						m_strLotID = "";

						Save(a_strIniPath);


				}

				public void Save(string a_strIniPath)
				{
						IniFile ini = new IniFile(a_strIniPath);

						ini.Write("WORKS_INFO", "RunningTime", m_OldRunningTime.Ticks);
						ini.Write("WORKS_INFO", "StopTime", m_OldStopTime.Ticks);
						ini.Write("WORKS_INFO", "ErrorTime", m_OldErrorTime.Ticks);

						ini.Write("WORKS_INFO", "GoodCount", m_lGoodCount);
						ini.Write("WORKS_INFO", "RejectCount", m_lRejectCount);
						ini.Write("WORKS_INFO", "ErrorCount", m_lErrorCount);
				}

				public void DataGridViewWorkTime_Init(DataGridView a_Grid)
				{
						a_Grid.Rows.Clear();
						if (a_Grid.RowCount == 0)
						{


								Color ForeColor = Color.Black;
								Color BackColor = Color.White;
								Color SelectionBackColor = Color.SteelBlue;
								Color SelectionForeColor = Color.White;
								a_Grid.BorderStyle = BorderStyle.None;
								a_Grid.CellPainting += new DataGridViewCellPaintingEventHandler(delegate (object obj, DataGridViewCellPaintingEventArgs e)
								{
										if (e.RowIndex > -1 && e.ColumnIndex > -1)
										{
												e.Handled = true;
												using (Brush b = new SolidBrush(BackColor))
												{
														e.Graphics.FillRectangle(b, e.CellBounds);
												}
												using (Pen p = new Pen(Brushes.SteelBlue))
												{
														p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
														e.Graphics.DrawLine(p, new Point(0, e.CellBounds.Bottom - 1), new Point(e.CellBounds.Right, e.CellBounds.Bottom - 1));
												}
												e.PaintContent(e.ClipBounds);
										}
								});
								#region [Explain]
								//선택된 모든 셀의 선택해제
								//a_grid.ClearSelection();
								//첫번째 행 선택
								//a_grid.Rows[0].Selected = true;
								//마지막 행 선택
								//a_grid.Rows[a_grid.Rows.Count - 1].Selected = true;
								//선택행에 삽입.
								//a_grid.Rows.Insert(1, "test");
								//마지막행에 삽입.
								//a_grid.Rows.Add("last");
								//첫번째 선택된 행의 인덱스값.
								//a_grid.SelectedRows[0].Index;
								//특정 행 삭제
								//a_grid.Rows.RemoveAt(0);  //삭제
								//선택 행의 색 바꾸기
								//a_grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
								//a_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
								//특정 행의 색 바꾸기
								//a_grid.Rows[2].DefaultCellStyle.BackColor = Color.Red;
								//특정 행열의 색 바꾸기
								//a_grid.Rows[i].Cells[col].Style.BackColor = ColorTranslator.FromHtml(123, 123, 123, 123);
								//셀 내용 읽기 0행, 0열
								//a_grid.Rows[0].Cells[0].Value.ToString();
								//행의 총수
								//int lines = a_grid.Rows.Count
								//우측 스크롤바 현재 셀위치를 보여주게 자동 이동
								//a_grid.CurrentCell = a_grid.Rows[행].Cells[열];
								//활성화된 셀의 행 인덱스값
								//int select = a_grid.CurrentCell.RowIndex;
								#endregion Explain
								#region [Common]
								a_Grid.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point);
								a_Grid.ReadOnly = true;
								a_Grid.AllowUserToAddRows = false;
								a_Grid.AllowUserToDeleteRows = false;
								a_Grid.AllowUserToOrderColumns = false;
								a_Grid.AllowUserToResizeColumns = false;
								a_Grid.AllowUserToResizeRows = false;
								a_Grid.ColumnHeadersVisible = false;
								a_Grid.RowHeadersVisible = false;
								a_Grid.MultiSelect = false;
								a_Grid.EditMode = DataGridViewEditMode.EditOnEnter;
								a_Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
								a_Grid.BackgroundColor = Color.White;

								a_Grid.Columns.Clear();
								a_Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
								a_Grid.ColumnHeadersHeight = 30;
								a_Grid.RowTemplate.Height = 30;
								a_Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
								#endregion [Common]

								//Items | Time 
								//0    1       
								#region [Items] [0]
								DataGridViewTextBoxColumn Items = new DataGridViewTextBoxColumn();
								Items.HeaderText = "Items";
								Items.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Items.Resizable = DataGridViewTriState.False;
								Items.ReadOnly = true;
								Items.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								Items.DefaultCellStyle.ForeColor = ForeColor;
								Items.DefaultCellStyle.BackColor = BackColor;
								Items.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Items.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Items] [0]
								#region [Time] [1]
								DataGridViewTextBoxColumn Time = new DataGridViewTextBoxColumn();
								Time.HeaderText = "Time";
								Time.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
								Time.Resizable = DataGridViewTriState.False;
								Time.ReadOnly = true;
								Time.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								Time.DefaultCellStyle.ForeColor = ForeColor;
								Time.DefaultCellStyle.BackColor = BackColor;
								Time.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
								Time.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
								#endregion [Times] [1]
								#region [Add]
								a_Grid.Columns.AddRange(new DataGridViewTextBoxColumn[] { Items, Time });
								#endregion [Add]
								#region [ Head Setting ]
								//Headers setting
								foreach (DataGridViewColumn col in a_Grid.Columns)
								{
										col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
										col.HeaderCell.Style.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
								}

								//a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
								a_Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
								a_Grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
								a_Grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
								a_Grid.EnableHeadersVisualStyles = false;
								a_Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
								a_Grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
								//a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
								//a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;
								#endregion [ head Setting ]

								//a_Grid.AutoResizeColumns();
								a_Grid.AutoResizeRows();
								//a_Grid.FitCells();
								a_Grid.Columns[0].Width = 120;
								a_Grid.Columns[1].Width = a_Grid.Width - a_Grid.Columns[0].Width;
								a_Grid.Rows.Add("LOT CODE", "Wafer ID");
								a_Grid.Rows.Add("Cycle Time", string.Format("{0:D2}:{1:D2}:{2:D3}", CycleTime.Minutes, CycleTime.Seconds, CycleTime.Milliseconds));
								a_Grid.Rows.Add("Running Time", string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", RunningTime.Days, RunningTime.Hours, RunningTime.Minutes, RunningTime.Seconds));
								a_Grid.Rows.Add("Stop Time", string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", StopTime.Days, StopTime.Hours, StopTime.Minutes, StopTime.Seconds));
								a_Grid.Rows.Add("Error Time", string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", ErrorTime.Days, ErrorTime.Hours, ErrorTime.Minutes, ErrorTime.Seconds));
								a_Grid.Rows.Add("UPH", string.Format("{0:F2} Hours", GetUPH()));
								//a_Grid.Rows.Add("MTBF Time", string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", GetMTBF().Days, GetMTBF().Hours, GetMTBF().Minutes, GetMTBF().Seconds));
								a_Grid.ClearSelection();
								a_Grid.SelectionChanged+=DataGridVeiwNoSelection_SelectionChanged;
						}
				}
				private void DataGridVeiwNoSelection_SelectionChanged(object sender, EventArgs e)
				{
						DataGridView pcontrol = sender as DataGridView;
						pcontrol.ClearSelection();
				}
		}
}
