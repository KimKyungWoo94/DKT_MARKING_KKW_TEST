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
	public partial class FrmTabInitialProcessGalvoCal : Form
	{
		Timer tmr = new Timer();

		Point m_ptMapSelectedIndex = new Point(0, 0);
		Point m_ptMapCurrentIndex = new Point(0, 0);

		bool m_bFirstChecked = false;
		bool m_bSecondChecked = false;

		double m_d1stMX = 0.0;
		double m_d2ndMX = 0.0;
		double m_d1stMY = 0.0;
		double m_d2ndMY = 0.0;

		public FrmTabInitialProcessGalvoCal()
		{
			InitializeComponent();
		}

		private void FrmTabInitialProcessGalvoCal_Load(object sender, EventArgs e)
		{
			FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
			FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);

			tmr.Tick += new EventHandler(Display);
			tmr.Interval = 50;

			btn_Set1stPos	.Click += new EventHandler(GalvoCal_ManualCalibration);
			btn_Set2ndPos	.Click += new EventHandler(GalvoCal_ManualCalibration);
			btn_ApplyOffset	.Click += new EventHandler(GalvoCal_ManualCalibration);


		}

		private void GalvoCal_ManualCalibration(object sender, EventArgs e)
		{
			switch ((sender as Control).Name)
			{
				case "btn_Set1stPos":
					m_bFirstChecked = true;
					m_bSecondChecked = false;
					m_d1stMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d1stMY = AXIS.Y.Status().m_stPositionStatus.fActPos;
					break;
				case "btn_Set2ndPos":
					m_bSecondChecked = true;
					m_d2ndMX = AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d2ndMY = AXIS.Y.Status().m_stPositionStatus.fActPos;
					break;
				case "btn_ApplyOffset":

					if (MsgBox.Confirm("Would you like to apply the new offset??"))
					{
						double dXOffset = m_d2ndMX - m_d1stMY * -1;
						double dYOffset = m_d2ndMY - m_d1stMY * -1;

						lbl_NewXOffset.Text = string.Format("{0:F4}", dXOffset);
						lbl_NewYOffset.Text = string.Format("{0:F4}", dYOffset);

						ucCellBox_GalvoCal.SetValue_NewOffset(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y, dXOffset, dYOffset, 0.0);
						((RunningScanner)FA.MGR.RunMgr?.GetItem("SCANNER")).bGalvoCal_continue = true;

					}

					m_bFirstChecked = false;
					m_bSecondChecked = false;
					break;
			}
		}

		private void FrmTabInitialProcessGalvoCal_VisibleChanged(object sender, EventArgs e)
		{
			tmr.Enabled = this.Visible;
			if (Visible)
			{
				dataGridView_Datas.Rows.Clear();
				dataGridView_Options.Rows.Clear();

				FA.MGR.ProjectMgr.DataGridView_ChangeToModules(dataGridView_Datas, dataGridView_Options, FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_GALVO_CAL, imageList_For_TreeMenu);
			}
		}

		#region [ucCellBox]
		public void ucCellBox_GalvoCal_Create(int a_nColCount, int a_nRowCount)
		{
			ucCellBox_GalvoCal.RowCount = a_nRowCount;
			ucCellBox_GalvoCal.ColCount = a_nColCount;
		}

		public void ucCellBox_GalvoCal_SetMeasuredValue(int colidx, int rowidx)
		{
			RunningScanner pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
			ucCellBox_GalvoCal.SetValue(colidx, rowidx,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dMappingOffsetX,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dMappingOffsetY,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dMappingOffsetZ,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dEncX,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dEncY,
				pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_dEncZ);

			ucCellBox_GalvoCal.SetStatus(colidx, rowidx, pRunningScanner.GetGalvoCalMap(colidx, rowidx).m_eStatus, true);

		}

		private void ucCellBox_GalvoCal_CellActived(object sender, GUI.UserControls.CellEventArgs e)
		{
			lblActive.Text = "X=" + e.Col.ToString("D4") + ",Y=" + e.Row.ToString("D4");
			m_ptMapCurrentIndex.X = e.Col;
			m_ptMapCurrentIndex.Y = e.Row;
		}

		private void ucCellBox_GalvoCal_CellSelected(object sender, GUI.UserControls.CellEventArgs e)
		{
			try
			{

				lblMapIndex.Text = "X=" + e.Col.ToString("D4") + ",Y=" + e.Row.ToString("D4");
				m_ptMapSelectedIndex.X = e.Col;
				m_ptMapSelectedIndex.Y = e.Row;

				RunningScanner pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
				if (pRunningScanner.GetGalvoCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y) == null)
					throw new Exception("RunningStage GetGalvoCalMap is null");

				lbl_XOffset.Text = string.Format("{0:F4}", pRunningScanner.GetGalvoCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dMappingOffsetX);
				lbl_YOffset.Text = string.Format("{0:F4}", pRunningScanner.GetGalvoCalMap(m_ptMapSelectedIndex.X, m_ptMapSelectedIndex.Y).m_dMappingOffsetY);
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

				if (((RunningStage)FA.MGR.RunMgr?.GetItem("STAGE"))?.bStage2DCal_Pause == true)
				{
					btn_Set1stPos.Visible = true;
					btn_Set2ndPos.Visible = true;
					btn_ApplyOffset.Visible = true;
				}
				else
				{
					btn_Set1stPos.Visible = false;
					btn_Set2ndPos.Visible = false;
					btn_ApplyOffset.Visible = false;
				}

				//No | Category | Name | Key | Position | Unit | Set button | Images | Move Button
				//0    1          2      3     4          5      6            7        8
				double dPos = 0.0;
				for (int i = 0; i < dataGridView_Datas.Rows.Count; i++)
				{
					MF.RcpItem item = MF.RCP.GetRecipeItem(Int32.Parse(dataGridView_Datas[3, i].Value.ToString()));

					if (double.TryParse(item.m_strValue, out dPos))
					{
						if (item.m_iAxis == -1) continue;
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

		private void btn_Manual_GalvoCal_Start_Click(object sender, EventArgs e)
		{
			try
			{
				if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
					throw new Exception("It is not stop mode");
				if (MsgBox.Confirm("Would like to execute for the galvo calibration?"))
				{
					(FA.MGR.RunMgr.GetItem("SCANNER") as RunningScanner).m_eSelectedVision = DEF.eVision.FINE;
					FA.MGR.RunMgr.ModeChange(FA.DEF.eRunMode.ToManual, 0, FA.DEF.eManualMode.GALVO_CAL);
				}
			}
			catch (Exception exc)
			{
				FA.LOG.Fatal("Exception", exc.StackTrace, exc.Message);
			}
		}

		private void btn_Manual_GalvoCal_Stop_Click(object sender, EventArgs e)
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
			MF.RCP.Read_From(DEF.eRcpCategory.InitialProcess, DEF.eRcpSubCategory.M100_GALVO_CAL, dataGridView_Datas);
			MF.OPT.Read_Form(DEF.eRcpSubCategory.M100_GALVO_CAL, dataGridView_Options);
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

		private void btn_Manual_GalvoCal_Reset_Click(object sender, EventArgs e)
		{
			if (MsgBox.Confirm("Would you like to reset the sequence??"))
			{
				FA.MGR.RunMgr.ButtonSwitch(FA.DEF.eButtonSW.RESET);
				FA.LOG.BTN_Event("BUTTON EVENT", "Reset button click");
			}
		}
	}
}
