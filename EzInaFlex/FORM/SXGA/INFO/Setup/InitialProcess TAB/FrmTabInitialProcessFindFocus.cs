using EzIna.FA;
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
		public partial class FrmTabInitialProcessFindFocus : Form
		{
				Timer tmr = new Timer();

				Point m_ptMapSelectedIndex = new Point(0, 0);
				Point m_ptMapCurrentIndex = new Point(0, 0);

				public FrmTabInitialProcessFindFocus()
				{
						InitializeComponent();
				}

				private void FrmTabInitialProcessFindFocus_Load(object sender, EventArgs e)
				{
						FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
						FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);

						btn_Manual_FindFocus_Start.Click += new EventHandler(BtnClick_SemiAuto);
						btn_Manual_FindFocus_Stop.Click += new EventHandler(BtnClick_SemiAuto);
						btn_Manual_FindFocus_Reset.Click += new EventHandler(BtnClick_SemiAuto);
				}

				#region [Button Event]

				private void BtnClick_SemiAuto(object sender, EventArgs e)
				{
						switch ((sender as Control).Name)
						{
								case "btn_Manual_FindFocus_Start":
										if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
												return;

										if (MsgBox.Confirm("Would you like to execute for the cross hair"))
												FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.FOCUS_FINDER);
										break;

								case "btn_Manual_FindFocus_Stop":
										FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToStopManual);
										break;

								case "btn_Manual_FindFocus_Reset":
										FA.MGR.RunMgr.ButtonSwitch(DEF.eButtonSW.RESET);
										break;

						}
				}
				#endregion

				private void FrmTabInitialProcessFindFocus_VisibleChanged(object sender, EventArgs e)
				{
						tmr.Enabled = this.Visible;
						if (Visible)
						{
								dataGridView_Datas.Rows.Clear();
								dataGridView_Options.Rows.Clear();

								FA.MGR.ProjectMgr.DataGridView_ChangeToModules(dataGridView_Datas, dataGridView_Options, FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_FIND_FOCUS, imageList_For_TreeMenu);
						}
				}

				#region [ucCellBox]
				public void ucCellBox_FindFocus_Create(int a_nColCount, int a_nRowCount)
				{
						ucCellBox_FocusFinder.RowCount = a_nRowCount;
						ucCellBox_FocusFinder.ColCount = a_nColCount;
				}

				public void ucCellBox_FindFocus_SetMeasuredValue(int colidx, int rowidx)
				{
						RunningScanner pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
						ucCellBox_FocusFinder.SetValue(colidx, rowidx,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncX,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncY,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncZ,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncX,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncY,
							pRunningScanner.GetFindFocusMap(colidx, rowidx).m_dEncZ);

						ucCellBox_FocusFinder.SetStatus(colidx, rowidx, pRunningScanner.GetFindFocusMap(colidx, rowidx).m_eStatus, true);

				}

				private void ucCellBox_FindFocus_CellActived(object sender, GUI.UserControls.CellEventArgs e)
				{
						lblActive.Text = "X=" + e.Col.ToString("D4") + ",Y=" + e.Row.ToString("D4");
						m_ptMapCurrentIndex.X = e.Col;
						m_ptMapCurrentIndex.Y = e.Row;
				}

				private void ucCellBox_FindFocus_CellSelected(object sender, GUI.UserControls.CellEventArgs e)
				{
						try
						{
								lblMapIndex.Text = "X=" + e.Col.ToString("D4") + ",Y=" + e.Row.ToString("D4");
								m_ptMapSelectedIndex.X = e.Col;
								m_ptMapSelectedIndex.Y = e.Row;

								RunningScanner pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
								if (pRunningScanner.GetFindFocusMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y) == null)
										throw new Exception("The focus map of runningStage is null");
						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("EXCEPTION", exc.StackTrace, exc.Message);
						}
				}
				#endregion

				private void ucCellBox_FocusFinder_DoubleClick(object sender, EventArgs e)
				{
						try
						{
								if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
										return;

								if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
										return;

								RunningScanner pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");

								if (pRunningScanner.GetFindFocusMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y) == null)
										throw new Exception("The focus map of runningStage is null");

								double dMX = pRunningScanner.GetFindFocusMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncX;
								double dMY = pRunningScanner.GetFindFocusMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncY;


								if (MsgBox.Confirm("Would you like to move to clicked position??"))
								{
										FA.ACT.MoveABS(FA.DEF.eAxesName.RX, dMX, Motion.GDMotion.eSpeedType.FAST);
										FA.ACT.MoveABS(FA.DEF.eAxesName.Y, dMY, Motion.GDMotion.eSpeedType.FAST);
								}

						}
						catch (Exception exc)
						{
								FA.LOG.Fatal("EXCEPTION", exc.StackTrace, exc.Message);
						}

				}

				private void btn_ProjectOpen_Click(object sender, EventArgs e)
				{
						if (MsgBox.Confirm("Would you like to open the file??"))
						{
								FA.MGR.ProjectMgr.RecipeOpen_InitProc();
								FA.MGR.ProjectMgr.OptionOpen_InitProc();
						}
				}

				private void btn_ProjectSave_Click(object sender, EventArgs e)
				{
						if (MsgBox.Confirm("Would you like to save the file??"))
						{
								Read_From();
								FA.MGR.ProjectMgr.RecipeSave_InitProc();
								FA.MGR.ProjectMgr.OptionSave_InitProc();
						}
				}

				public void Read_From()
				{
						MF.RCP.Read_From(DEF.eRcpCategory.InitialProcess, DEF.eRcpSubCategory.M100_FIND_FOCUS, dataGridView_Datas);
						MF.OPT.Read_Form(DEF.eRcpSubCategory.M100_FIND_FOCUS, dataGridView_Options);
				}
		}
}
