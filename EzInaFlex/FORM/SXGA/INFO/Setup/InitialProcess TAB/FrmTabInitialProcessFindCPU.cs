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
	public partial class FrmTabInitialProcessFindCPU : Form
	{
		System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();

		double m_d1stPosX = 0.0, m_d1stPosY = 0.0 ,m_d2ndPosX = 0.0, m_d2ndPosY = 0.0, m_dResult = 0.0;
		bool m_b1stPosClicked = false, m_b2ndPosClicked = false;

		bool m_bDirX = false;
		bool m_bDirY = false;

		public FrmTabInitialProcessFindCPU()
		{
			InitializeComponent();
		}

		private void FrmTabInitialProcessFindCPU_Load(object sender, EventArgs e)
		{
			FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
			FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);


			btn_Manual_FindCPU_Start	.Click += new EventHandler(BtnClick_SemiAuto);
			btn_Manual_FindCPU_Stop		.Click += new EventHandler(BtnClick_SemiAuto);
			btn_Manual_FindCPU_Reset	.Click += new EventHandler(BtnClick_SemiAuto);


			btn_CPU_Top		.Click += new EventHandler(BtnClick_Funcs);
			btn_CPU_Bottom	.Click += new EventHandler(BtnClick_Funcs);
			btn_CPU_Left	.Click += new EventHandler(BtnClick_Funcs);
			btn_CPU_Right	.Click += new EventHandler(BtnClick_Funcs);
			btn_CPU_Center  .Click += new EventHandler(BtnClick_Funcs);


			btn_CPU_Set1stX	.Click += new EventHandler(btn_CPU_Set1st_Click);
			btn_CPU_Set2ndX	.Click += new EventHandler(btn_CPU_Set1st_Click);
			btn_CPU_CalcX	.Click += new EventHandler(btn_CPU_Set1st_Click);

			btn_CPU_Set1stY	.Click += new EventHandler(btn_CPU_Set1st_Click);
			btn_CPU_Set2ndY	.Click += new EventHandler(btn_CPU_Set1st_Click);
			btn_CPU_CalcY	.Click += new EventHandler(btn_CPU_Set1st_Click);


		}

		#region [Button Event]
		private void BtnClick_Funcs(object sender, EventArgs e)
		{
			if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;
			if(!FA.AXIS.RX.Status().IsMotionDone || !FA.AXIS.Y.Status().IsMotionDone)
				return;

			double dMX = 0.0, dMY = 0.0;
			dMX = (FA.MGR.RunMgr.GetItem("SCANNER") as RunningScanner).GetCPUStartPosX;
			dMY = (FA.MGR.RunMgr.GetItem("SCANNER") as RunningScanner).GetCPUStartPosY;

			switch ((sender as Control).Name)
			{
				case "btn_CPU_Top":
					dMY = dMY - FA.RCP.M100_CPUStageFOVSize.AsDouble / 2.0;
					break;
				case "btn_CPU_Bottom":
					dMY = dMY + FA.RCP.M100_CPUStageFOVSize.AsDouble / 2.0;
					break;
				case "btn_CPU_Left":
					dMX = dMX - FA.RCP.M100_CPUStageFOVSize.AsDouble / 2.0;
					break;
				case "btn_CPU_Right":
					dMX = dMX + FA.RCP.M100_CPUStageFOVSize.AsDouble / 2.0;
					break;
				case "btn_CPU_Center":
					break;
			}

			FA.ACT.MoveABS(FA.DEF.eAxesName.RX, dMX, Motion.GDMotion.eSpeedType.FAST);
			FA.ACT.MoveABS(FA.DEF.eAxesName.Y, dMY, Motion.GDMotion.eSpeedType.FAST);

		}
		private void BtnClick_SemiAuto(object sender, EventArgs e)
		{
			switch ((sender as Control).Name)
			{
				case "btn_Manual_FindCPU_Start":
					if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
						return;

					if (MsgBox.Confirm("Would you like to execute for the cross hair"))
					{
						FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.FIND_CPU);
						m_b1stPosClicked = false;
						m_b2ndPosClicked = false;
					}
					break;

				case "btn_Manual_FindCPU_Stop":
					FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToStopManual);
					break;

				case "btn_Manual_FindCPU_Reset":
					FA.MGR.RunMgr.ButtonSwitch(DEF.eButtonSW.RESET);
					break;

			}
		}
		#endregion

		#region [Display]
		private void Display(object sender, EventArgs args)
		{
			try
			{
				tmr.Stop();

				//No | Category | Name | Key | Position | Unit | Set button | Images | Move Button
				//0    1          2      3     4          5      6            7        8
				double dPos = 0.0;
				for (int i = 0; i < dataGridView_Datas.Rows.Count; i++)
				{
					MF.RcpItem item = MF.RCP.GetRecipeItem(Int32.Parse(dataGridView_Datas[3, i].Value.ToString()));

					if (double.TryParse(item.m_strValue, out dPos))
					{
						if (item.m_iAxis == -1) continue;

						dataGridView_Datas[7, i].Value = dPos.IsSame(FA.MGR.MotionMgr.GetItem(item.m_iAxis).Status().m_stPositionStatus.fActPos, FA.DEF.IN_POS) ? imageList_chkbox.Images[1] : imageList_chkbox.Images[0];

					}
				}

				//lbl_CrossHairScannerRefPosX					.Text = FA.RCP.M100_CrossHairScannerRefPosX					.AsDouble.ToString("F4");
				//lbl_CrossHairScannerRefPosY					.Text = FA.RCP.M100_CrossHairScannerRefPosY					.AsDouble.ToString("F4");
				//lbl_CrossHairFineVisionRefPosX				.Text = FA.RCP.M100_CrossHairFineVisionRefPosX				.AsDouble.ToString("F4");
				//lbl_CrossHairFineVisionRefPosY				.Text = FA.RCP.M100_CrossHairFineVisionRefPosY				.AsDouble.ToString("F4");
				//lbl_CrossHairCoarseVisionRefPosX				.Text = FA.RCP.M100_CrossHairCoarseVisionRefPosX			.AsDouble.ToString("F4");
				//lbl_CrossHairCoarseVisionRefPosY				.Text = FA.RCP.M100_CrossHairCoarseVisionRefPosY			.AsDouble.ToString("F4");
				//lbl_CrossHairDistX_From_F_To_C				.Text = FA.RCP.M100_CrossHairDistX_From_F_To_C				.AsDouble.ToString("F4");
				//lbl_CrossHairDistY_From_F_To_C				.Text = FA.RCP.M100_CrossHairDistY_From_F_To_C				.AsDouble.ToString("F4");
				//lbl_CrossHairFine_ScannerAndVisionXOffset		.Text = FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset	.AsDouble.ToString("F4");
				//lbl_CrossHairFine_ScannerAndVisionYOffset		.Text = FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset	.AsDouble.ToString("F4");
				//lbl_CrossHairCoarse_ScannerAndVisionXOffset	.Text = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset	.AsDouble.ToString("F4");
				//lbl_CrossHairCoarse_ScannerAndVisionYOffset	.Text = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset	.AsDouble.ToString("F4");
				//
				//btn_MoveToScannerCenterPos					.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairScannerRefPosX		) & ACT.InPostion(FA.RCP.M100_CrossHairScannerRefPosY		) ? 1 : 0;
				//btn_CoarseVision_Move							.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairCoarseVisionRefPosX	) & ACT.InPostion(FA.RCP.M100_CrossHairCoarseVisionRefPosY	) ? 1 : 0;
				//btn_FineVision_Move							.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairFineVisionRefPosX	) & ACT.InPostion(FA.RCP.M100_CrossHairFineVisionRefPosY		) ? 1 : 0;

				tmr.Enabled=this.Visible;
			}
			catch (System.Exception exc)
			{
				tmr.Enabled=this.Visible;
				FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
			}
		}

		public void Read_From()
		{
			MF.RCP.Read_From(DEF.eRcpCategory.InitialProcess, DEF.eRcpSubCategory.M100_FIND_CPU, dataGridView_Datas);
			MF.OPT.Read_Form(DEF.eRcpSubCategory.M100_FIND_CPU, dataGridView_Options);
		}



		#endregion

		private void FrmTabInitialProcessFindCPU_VisibleChanged(object sender, EventArgs e)
		{
			tmr.Enabled = Visible;

			if (Visible)
			{
				dataGridView_Datas.Rows.Clear();
				dataGridView_Options.Rows.Clear();

				FA.MGR.ProjectMgr.DataGridView_ChangeToModules(dataGridView_Datas, dataGridView_Options, FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_FIND_CPU, imageList_chkbox);
			}
		}

		private void btn_CPU_Stop_Click(object sender, EventArgs e)
		{
			if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;

			FA.AXIS.AllSDStop();
		}

		private void btn_CPU_Set1st_Click(object sender, EventArgs e)
		{
			if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;
			if (!FA.AXIS.RX.Status().IsMotionDone || !FA.AXIS.Y.Status().IsMotionDone)
				return;

			switch((sender as Control).Name)
			{
				case "btn_CPU_Set1stX":
// 					if(m_b1stPosClicked || m_b2ndPosClicked)
// 					{
// 						MsgBox.Error("CPU Sequence execute first!!!");
// 						break;
// 					}
					m_d1stPosX = FA.AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d1stPosY = FA.AXIS.Y.Status().m_stPositionStatus.fActPos;

					m_bDirX = true;
					m_bDirY = false;
					m_b1stPosClicked = true;
					m_b2ndPosClicked = false;
					break;
				case "btn_CPU_Set2ndX":

					if(!m_b1stPosClicked || !m_bDirX) break;

					m_d2ndPosX = FA.AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d2ndPosY = FA.AXIS.Y.Status().m_stPositionStatus.fActPos;
					m_b1stPosClicked = true;
					m_b2ndPosClicked = true;
					break;
				case "btn_CPU_CalcX":
					{
						if (!m_b1stPosClicked || !m_b2ndPosClicked)
							break;
						object obj = EzIna.Motion.CMotionA3200.ExecuteCmd(1, "CountsPerUnit." + FA.DEF.eAxesName.RA.ToString());
						
						double dOldCPU =  0.0, dNewCPU = 0.0;
						if(!double.TryParse(obj.ToString(), out dOldCPU))
							break;
						
						double dRad = Math.Atan((m_d2ndPosY - m_d1stPosY) / (m_d2ndPosX - m_d1stPosX));
						double dDeg = dRad * (180.0 / Math.PI) * -1;
						FA.RCP.M100_CPUGalvoRotate.m_strValue = string.Format("{0:F4}", FA.RCP.M100_CPUGalvoRotate.AsDouble + dDeg);

						dNewCPU = FA.RCP.M100_CPUStageFOVSize.AsDouble * dOldCPU / Math.Abs( m_d2ndPosX - m_d1stPosX );

						lbl_CPU_X_NewValue	.Text = dNewCPU.ToString("F6");
						lbl_CPU_NewAngle	.Text = dDeg.ToString("F4");


						m_bDirX = false;
						m_b1stPosClicked = true;
						m_b2ndPosClicked = true;
					}
					break;

				case "btn_CPU_Set1stY":
// 					if (m_b1stPosClicked || m_b2ndPosClicked)
// 					{
// 						MsgBox.Error("CPU Sequence execute first!!!");
// 						break;
// 					}
					m_d1stPosX = FA.AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d1stPosY = FA.AXIS.Y.Status().m_stPositionStatus.fActPos;

					m_bDirX = false;
					m_bDirY = true;
					m_b1stPosClicked = true;
					m_b2ndPosClicked = false;
					break;
				case "btn_CPU_Set2ndY":

					if (!m_b1stPosClicked || !m_bDirY) break;

					m_d2ndPosX = FA.AXIS.RX.Status().m_stPositionStatus.fActPos;
					m_d2ndPosY = FA.AXIS.Y.Status().m_stPositionStatus.fActPos;
					m_b1stPosClicked = true;
					m_b2ndPosClicked = true;
					break;
				case "btn_CPU_CalcY":
					{
						if (!m_b1stPosClicked || !m_b2ndPosClicked)
							break;

						object obj = EzIna.Motion.CMotionA3200.ExecuteCmd(1, "CountsPerUnit." + FA.DEF.eAxesName.RB.ToString());

						double dOldCPU = 0.0, dNewCPU = 0.0;
						if (!double.TryParse(obj.ToString(), out dOldCPU))
							break;

						dNewCPU = FA.RCP.M100_CPUStageFOVSize.AsDouble * dOldCPU / Math.Abs(m_d2ndPosY - m_d1stPosY);
						//

						lbl_CPU_Y_NewValue.Text = dNewCPU.ToString("F6");
						m_bDirY = false;
						m_b1stPosClicked = true;
						m_b2ndPosClicked = true;
					}
					break;
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
	}
}
