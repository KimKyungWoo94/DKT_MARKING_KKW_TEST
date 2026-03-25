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
	public partial class FrmTabInitialProcessStageCal : Form
	{
		Timer tmr = new Timer();

		Point m_ptMapSelectedIndex	= new Point(0,0);
		Point m_ptMapCurrentIndex	= new Point(0,0);

		bool m_bFirstChecked		= false;
		bool m_bSecondChecked		= false;

		double m_d1stMX				= 0.0;
		double m_d2ndMX				= 0.0;
		double m_d1stMY				= 0.0;
		double m_d2ndMY				= 0.0;

		public FrmTabInitialProcessStageCal()
		{
			InitializeComponent();
		}


		private void FrmTabInitialProcessStageCal_Load(object sender, EventArgs e)
		{
			FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
			FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);

			tmr.Tick += new EventHandler(Display);
			tmr.Interval  = 50;

			btn_Set1stPos	.Click += new EventHandler(Stage2DCal_ManualCalibration);
			btn_Set2ndPos	.Click += new EventHandler(Stage2DCal_ManualCalibration);
			btn_ApplyOffset	.Click += new EventHandler(Stage2DCal_ManualCalibration);
			
		}
		private void Stage2DCal_ManualCalibration(object sender, EventArgs e)
		{
			switch((sender as Control).Name)
			{
				case "btn_Set1stPos":
					m_bFirstChecked		= true;
					m_bSecondChecked	= false;
					m_d1stMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d1stMY = AXIS.Y .Status().m_stPositionStatus.fActPos;
					break;
				case "btn_Set2ndPos":
					m_bSecondChecked	= true;
					m_d2ndMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d2ndMY = AXIS.Y .Status().m_stPositionStatus.fActPos;
					break;
				case "btn_ApplyOffset":

					if (MsgBox.Confirm("Would you like to apply the new offset??"))
					{
						double dXOffset = (m_d2ndMX - m_d1stMX) * -1;
						double dYOffset = (m_d2ndMY - m_d1stMY) * -1;

						lbl_NewXOffset.Text = string.Format("{0:F4}", dXOffset);
						lbl_NewYOffset.Text = string.Format("{0:F4}", dYOffset);

						//ucCellBox_Stage2DCal.SetValue_NewOffset( m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y, dXOffset, dYOffset, 0.0);
						if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
						{
							((RunningStage)FA.MGR.RunMgr?.GetItem("STAGE")).dStage2DCal_ManualTeachingPosX = dXOffset;
							((RunningStage)FA.MGR.RunMgr?.GetItem("STAGE")).dStage2DCal_ManualTeachingPosY = dYOffset;
							((RunningStage)FA.MGR.RunMgr?.GetItem("STAGE")).bStage2DCal_continue = true;
						}
						else
						{
							//ucCellBox_Stage2DCal.SetStatus(colidx, rowidx, pRunningScanner.GetFindFocusMap(colidx, rowidx).m_eStatus, true);

						}

						m_bFirstChecked = false;
						m_bSecondChecked= false;
					}
					break;
			}
		}

		private void FrmTabInitialProcessStageCal_VisibleChanged(object sender, EventArgs e)
		{
			tmr.Enabled = this.Visible;
			if(Visible)
			{
				dataGridView_Datas	.Rows	.Clear();
				dataGridView_Options.Rows	.Clear();

				FA.MGR.ProjectMgr.DataGridView_ChangeToModules	(dataGridView_Datas, dataGridView_Options, FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_STAGE_CAL,imageList_For_TreeMenu);
			}
		}

		#region [ucCellBox]
		public void ucCellBox_Stage2DCal_Create(int a_nColCount, int a_nRowCount)
		{
			ucCellBox_Stage2DCal.RowCount					= a_nRowCount;
			ucCellBox_Stage2DCal.ColCount					= a_nColCount;
		}

		public void ucCellBox_Stage2DCal_SetMeasuredValue(int colidx, int rowidx)
		{
			RunningStage pRunningStage = (RunningStage)FA.MGR.RunMgr.GetItem("STAGE");
			if(pRunningStage.GetStage2DCalMap(colidx, rowidx) == null)
				return;

			ucCellBox_Stage2DCal.SetValue(colidx, rowidx,
				
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dMappingOffsetX	,
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dMappingOffsetY	,
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dMappingOffsetZ	,
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dEncX			,
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dEncY			,
				pRunningStage.GetStage2DCalMap(colidx, rowidx).m_dEncZ			);
			
			ucCellBox_Stage2DCal.SetStatus(colidx, rowidx, pRunningStage.GetStage2DCalMap(colidx, rowidx).m_eStatus, true);

		}

		private void ucCellBox_Stage2DCal_CellActived(object sender, GUI.UserControls.CellEventArgs e)
		{
			lblActive.Text = "X="+e.Col.ToString("D4")+",Y="+e.Row.ToString("D4");
			m_ptMapCurrentIndex.X = e.Col;
			m_ptMapCurrentIndex.Y = e.Row;
		}

		private void ucCellBox_Stage2DCal_CellSelected(object sender, GUI.UserControls.CellEventArgs e)
		{
			try
			{

				lblMapIndex.Text = "X=" + e.Col.ToString("D4") + ",Y=" + e.Row.ToString("D4");
				m_ptMapSelectedIndex.X = e.Col;
				m_ptMapSelectedIndex.Y = e.Row;

				RunningStage pRunningStage = (RunningStage)FA.MGR.RunMgr.GetItem("STAGE");
				if (pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y) == null)
					throw new Exception("RunningStage GetStage2DCalMap is null");

				lbl_XOffset.Text = string.Format("{0:F4}", pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dMappingOffsetX);
				lbl_YOffset.Text = string.Format("{0:F4}", pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dMappingOffsetY);
			}
			catch (Exception exc)
			{
				FA.LOG.Fatal("EXCEPTION", exc.StackTrace, exc.Message);
			}
		}
		#endregion


		#region [Display]
		private void Display(object sender, EventArgs args)
		{
			try
			{
				tmr.Stop();

				btn_Set1stPos.ImageIndex = m_bFirstChecked == true ? 1 : 0;
				btn_Set2ndPos.ImageIndex = m_bSecondChecked == true ? 1 : 0;
				//No | Category | Name | Key | Position | Unit | Set button | Images | Move Button
				//0    1          2      3     4          5      6            7        8
				double dPos = 0.0;
				for (int i = 0; i < dataGridView_Datas.Rows.Count; i++)
				{
					MF.RcpItem item = MF.RCP.GetRecipeItem(Int32.Parse(dataGridView_Datas[3,i].Value.ToString()));

					if(double.TryParse(item.m_strValue, out dPos))
					{
						if(item.m_iAxis == -1) continue;
						dataGridView_Datas[7, i].Value = dPos.IsSame(FA.MGR.MotionMgr.GetItem(item.m_iAxis).Status().m_stPositionStatus.fActPos, FA.DEF.IN_POS) ? imageList_For_TreeMenu.Images[1] : imageList_For_TreeMenu.Images[0];

					}
				}

				tmr.Enabled=this.Visible;
			}
			catch (System.Exception exc)
			{
				tmr.Enabled=this.Visible;
				FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
			}

				
		}
		#endregion

		private void dataGridView_Datas_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
// 				if(FA.MGR.RunMgr == null)
// 					return;
// 				if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
// 					return;
// 				if (!FA.MGR.RunMgr.IsInitialized)
// 					return;
// 
// 				//No | Category | Name | Key | Position | Unit | Set button | Images | Move Button
// 				//0    1          2      3     4          5      6            7        8
// 				if (e.ColumnIndex == 6) //set
// 				{
// 
// 					MF.RcpItem item = MF.RCP.GetRecipeItem(Int32.Parse(dataGridView_Datas[3, e.RowIndex].Value.ToString()));
// 					if (MsgBox.Confirm(string.Format("Would you like to set \"{0}\" position??", item.m_strCaption)) == false)
// 						return;
// 
// 					item.m_strValue = FA.MGR.MotionMgr?.GetItem(item.m_iAxis).Status().m_stPositionStatus.fActPos.ToString("F4");
// 					dataGridView_Datas[4, e.RowIndex].Value = item.m_strValue;
// 				}
// 
// 				if (e.ColumnIndex == 8) //move
// 				{
// 					MF.RcpItem item = MF.RCP.GetRecipeItem(Int32.Parse(dataGridView_Datas[3, e.RowIndex].Value.ToString()));
// 					if (MsgBox.Confirm(string.Format("Would you like to move to \"{0}\" ??", item.m_strCaption)) == false)
// 						return;
// 
// 					double dPos = 0.0;
// 					if(double.TryParse(item.m_strValue, out dPos))
// 						FA.MGR.MotionMgr?.GetItem(item.m_iAxis).Move_Absolute(dPos, Motion.GDMotion.eSpeedType.FAST);
// 				}
			}
			catch (Exception exc)
			{
				FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
			}
		}

		private void btn_Manual_StageCal_Start_Click(object sender, EventArgs e)
		{
			try
			{
				if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
					throw new Exception("It is not stop mode");
				if (MsgBox.Confirm("Would like to execute for the Array vision inspection?"))
					FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.ToManual, 0, FA.DEF.eManualMode.STAGE_CAL);
			}
			catch (Exception exc)
			{
				FA.LOG.Fatal("Exception", exc.StackTrace, exc.Message);
			}
		}

		private void btn_Manual_StageCal_Stop_Click(object sender, EventArgs e)
		{
			FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.STOP);
		}

		private void btn_ProjectOpen_Click(object sender, EventArgs e)
		{
			if (MsgBox.Confirm("Would you like to open the file??"))
			{
				FA.MGR.ProjectMgr.RecipeOpen_InitProc();
				FA.MGR.ProjectMgr.OptionOpen_InitProc();
			}
		}
		public void Read_From()
		{
			MF.RCP.Read_From(DEF.eRcpCategory.InitialProcess, DEF.eRcpSubCategory.M100_STAGE_CAL, dataGridView_Datas);
			MF.OPT.Read_Form(DEF.eRcpSubCategory.M100_STAGE_CAL, dataGridView_Options);
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

		private void btn_Manual_StageCal_Reset_Click(object sender, EventArgs e)
		{
			if(MsgBox.Confirm("Would you like to reset the sequence??"))
			{
				FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.RESET);
				FA.LOG.BTN_Event("BUTTON EVENT", "Reset button click");
			}
		}

		private void ucCellBox_Stage2DCal_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
					return;
				RunningStage pRunningStage = (RunningStage)FA.MGR.RunMgr.GetItem("STAGE");
				if (pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y) == null)
					throw new Exception("RunningStage GetStage2DCalMap is null");

				if (MsgBox.Confirm(string.Format("{0}\n X = {1:F4}, Y = {2:F4}", "Would you like to move the ",
					pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncX,
					pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncY)))
				{
					FA.ACT.MoveABS(FA.DEF.eAxesName.RX, pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncX, Motion.GDMotion.eSpeedType.FAST);
					FA.ACT.MoveABS(FA.DEF.eAxesName.Y, pRunningStage.GetStage2DCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dEncY, Motion.GDMotion.eSpeedType.FAST);
				}

			}
			catch (Exception exc)
			{
				FA.LOG.Fatal("EXCEPTION", exc.StackTrace, exc.Message);
			}
		}
	}
}
