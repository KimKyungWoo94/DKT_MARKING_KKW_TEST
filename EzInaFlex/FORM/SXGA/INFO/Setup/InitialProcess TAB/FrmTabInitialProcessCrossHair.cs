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
	public partial class FrmTabInitialProcessCrossHair : Form
	{
		System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
		public FrmTabInitialProcessCrossHair()
		{
			InitializeComponent();
		}

		private void FrmTabInitialProcessCrossHair_Load(object sender, EventArgs e)
		{
			FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
			FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);

			tmr.Tick += new EventHandler(Display);
			tmr.Interval = 50;

			btn_SetScannerCenterPos		.Click += new EventHandler(BtnClick_Scanner);
			btn_MoveToScannerCenterPos  .Click += new EventHandler(BtnClick_Scanner);

			btn_CoarseVision_Set		.Click += new EventHandler(BtnClick_Coarse);
			btn_CoarseVision_Move		.Click += new EventHandler(BtnClick_Coarse);
			btn_CoarseVision_SetOffset	.Click += new EventHandler(BtnClick_Coarse);

			btn_FineVision_Set			.Click += new EventHandler(BtnClick_Fine);
			btn_FineVision_Move			.Click += new EventHandler(BtnClick_Fine);
			btn_FineVision_SetOffset	.Click += new EventHandler(BtnClick_Fine);

			btn_CoarseToFine_Set.Click += new EventHandler(BtnClick_CoarseToVision);

			btn_Manual_CrossHairl_Start	.Click += new EventHandler(BtnClick_SemiAuto);
			btn_Manual_CrossHair_Stop	.Click += new EventHandler(BtnClick_SemiAuto);
			btn_Manual_CrossHair_Reset	.Click += new EventHandler(BtnClick_SemiAuto);

			this.DoubleBuffered = true;
		}

		#region [Button Event]

		private void BtnClick_SemiAuto(object sender, EventArgs e)
		{
			switch ((sender as Control).Name)
			{
				case "btn_Manual_CrossHairl_Start":
					if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
						return;
					
					if(MsgBox.Confirm("Would you like to execute for the cross hair"))
					FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.CROSS_HAIR);
					break;
						
				case "btn_Manual_CrossHair_Stop":
					FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToStopManual);
					break;

				case "btn_Manual_CrossHair_Reset":
					FA.MGR.RunMgr.ButtonSwitch(DEF.eButtonSW.RESET);
					break;

			}
		}
		private void BtnClick_Scanner(object sender, EventArgs e)
		{
			if(FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;

			switch((sender as Control).Name)
			{
				case "btn_SetScannerCenterPos":
					FA.RCP.M100_CrossHairScannerRefPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString("F4");
					FA.RCP.M100_CrossHairScannerRefPosY.m_strValue = AXIS.Y .Status().m_stPositionStatus.fActPos.ToString("F4");
					UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairScannerRefPosX);
					UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairScannerRefPosY);
					break;
				case "btn_MoveToScannerCenterPos":
					if(MsgBox.Confirm("Would you like to move the scanner center position??"))
					{
						ACT.MoveABS(FA.RCP.M100_CrossHairScannerRefPosX, FA.RCP.M100_CrossHairScannerRefPosX);
						ACT.MoveABS(FA.RCP.M100_CrossHairScannerRefPosY, FA.RCP.M100_CrossHairScannerRefPosY);
					}
					break;

			}
		}

		private void UpdateDataGridView(DataGridView a_grd, MF.RcpItem item)
		{
			int iKey = 0;
			for(int i = 0; i < a_grd.RowCount; i++)
			{
				if(int.TryParse(a_grd[3,i].Value.ToString(), out iKey))
				{
					if(iKey == item.iKey)
					{
						a_grd[4, i].Value = item.m_strValue;
						break;
					}
				}
			}
		}
		private void BtnClick_Coarse(object sender, EventArgs e)
		{
			if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;

			switch ((sender as Control).Name)
			{
				case "btn_CoarseVision_Set":
					//FA.RCP.M100_CrossHairCoarseVisionRefPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString("F4");
					//FA.RCP.M100_CrossHairCoarseVisionRefPosY.m_strValue = AXIS.Y .Status().m_stPositionStatus.fActPos.ToString("F4");

					//UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairCoarseVisionRefPosX);
					//UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairCoarseVisionRefPosY);
					break;
				case "btn_CoarseVision_Move":
					if (MsgBox.Confirm("Would you like to move the Coarse Vision center position??"))
					{
						//ACT.MoveABS(FA.RCP.M100_CrossHairCoarseVisionRefPosX, FA.RCP.M100_CrossHairCoarseVisionRefPosY);
					}
					break;
				case "btn_CoarseVision_SetOffset":
					if(MsgBox.Confirm(("Would you like to set the offset??")))
					{
						FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.m_strValue = string.Format("{0:F4}", FA.RCP.M100_CrossHairScannerRefPosX.AsDouble - FA.RCP.M100_CrossHairCoarseVisionRefPosX.AsDouble	);
						FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.m_strValue = string.Format("{0:F4}", FA.RCP.M100_CrossHairScannerRefPosY.AsDouble - FA.RCP.M100_CrossHairCoarseVisionRefPosY.AsDouble );
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset);
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset);
					}
					break;
			}
		}
		private void BtnClick_Fine(object sender, EventArgs e)
		{
			if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;

			switch ((sender as Control).Name)
			{
				case "btn_FineVision_Set":
					FA.RCP.M100_CrossHairFineVisionRefPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString("F4");
					FA.RCP.M100_CrossHairFineVisionRefPosY.m_strValue = AXIS.Y .Status().m_stPositionStatus.fActPos.ToString("F4");
					UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairFineVisionRefPosX);
					UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairFineVisionRefPosY);

					break;
				case "btn_FineVision_Move":
					if (MsgBox.Confirm("Would you like to move the fine Vision center position??"))
					{
						ACT.MoveABS(FA.RCP.M100_CrossHairFineVisionRefPosX, FA.RCP.M100_CrossHairFineVisionRefPosY);
					}
					break;
				case "btn_FineVision_SetOffset":
					if (MsgBox.Confirm(("Would you like to set the offset??")))
					{
						FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.m_strValue = string.Format("{0:F4}",FA.RCP.M100_CrossHairScannerRefPosX.AsDouble - FA.RCP.M100_CrossHairFineVisionRefPosX.AsDouble);
						FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.m_strValue = string.Format("{0:F4}",FA.RCP.M100_CrossHairScannerRefPosY.AsDouble - FA.RCP.M100_CrossHairFineVisionRefPosY.AsDouble);
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset);
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset);
					}
					break;
			}
		}
		private void BtnClick_CoarseToVision(object sender, EventArgs e)
		{
			if (FA.MGR.RunMgr.eRunMode != DEF.eRunMode.Stop)
				return;


			switch ((sender as Control).Name)
			{
				case "btn_CoarseToFine_Set":
					if(MsgBox.Confirm("Would you like to set the offset??"))
					{
						FA.RCP.M100_CrossHairDistX_From_F_To_C.m_strValue = string.Format("{0:F4}", FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset.AsDouble - FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsDouble);
						FA.RCP.M100_CrossHairDistY_From_F_To_C.m_strValue = string.Format("{0:F4}", FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset.AsDouble - FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsDouble);
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairDistX_From_F_To_C);
						UpdateDataGridView(dataGridView_Datas, FA.RCP.M100_CrossHairDistY_From_F_To_C);
					}
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

				lbl_CrossHairScannerRefPosX					.Text = FA.RCP.M100_CrossHairScannerRefPosX					.AsDouble.ToString("F4");
				lbl_CrossHairScannerRefPosY					.Text = FA.RCP.M100_CrossHairScannerRefPosY					.AsDouble.ToString("F4");
				lbl_CrossHairFineVisionRefPosX				.Text = FA.RCP.M100_CrossHairFineVisionRefPosX				.AsDouble.ToString("F4");
				lbl_CrossHairFineVisionRefPosY				.Text = FA.RCP.M100_CrossHairFineVisionRefPosY				.AsDouble.ToString("F4");
				lbl_CrossHairCoarseVisionRefPosX			.Text = FA.RCP.M100_CrossHairCoarseVisionRefPosX			.AsDouble.ToString("F4");
				lbl_CrossHairCoarseVisionRefPosY			.Text = FA.RCP.M100_CrossHairCoarseVisionRefPosY			.AsDouble.ToString("F4");
				lbl_CrossHairDistX_From_F_To_C				.Text = FA.RCP.M100_CrossHairDistX_From_F_To_C				.AsDouble.ToString("F4");
				lbl_CrossHairDistY_From_F_To_C				.Text = FA.RCP.M100_CrossHairDistY_From_F_To_C				.AsDouble.ToString("F4");
				lbl_CrossHairFine_ScannerAndVisionXOffset	.Text = FA.RCP.M100_CrossHairFine_ScannerAndVisionXOffset	.AsDouble.ToString("F4");
				lbl_CrossHairFine_ScannerAndVisionYOffset	.Text = FA.RCP.M100_CrossHairFine_ScannerAndVisionYOffset	.AsDouble.ToString("F4");
				lbl_CrossHairCoarse_ScannerAndVisionXOffset	.Text = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionXOffset	.AsDouble.ToString("F4");
				lbl_CrossHairCoarse_ScannerAndVisionYOffset	.Text = FA.RCP.M100_CrossHairCoarse_ScannerAndVisionYOffset	.AsDouble.ToString("F4");

				btn_MoveToScannerCenterPos	.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairScannerRefPosX		) & ACT.InPostion(FA.RCP.M100_CrossHairScannerRefPosY		) ? 1 : 0;
				btn_CoarseVision_Move		.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairCoarseVisionRefPosX	) & ACT.InPostion(FA.RCP.M100_CrossHairCoarseVisionRefPosY	) ? 1 : 0;
				btn_FineVision_Move			.ImageIndex = ACT.InPostion(FA.RCP.M100_CrossHairFineVisionRefPosX	) & ACT.InPostion(FA.RCP.M100_CrossHairFineVisionRefPosY		) ? 1 : 0;

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
			MF.RCP.Read_From(DEF.eRcpCategory.InitialProcess, DEF.eRcpSubCategory.M100_CROSS_HAIR, dataGridView_Datas);
			MF.OPT.Read_Form(DEF.eRcpSubCategory.M100_CROSS_HAIR, dataGridView_Options);
		}



		#endregion

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

		private void FrmTabInitialProcessCrossHair_VisibleChanged(object sender, EventArgs e)
		{
			tmr.Enabled = Visible;

			if (Visible)
			{
				dataGridView_Datas.Rows.Clear();
				dataGridView_Options.Rows.Clear();

				FA.MGR.ProjectMgr.DataGridView_ChangeToModules(dataGridView_Datas, dataGridView_Options, FA.DEF.eRcpCategory.InitialProcess, FA.DEF.eRcpSubCategory.M100_CROSS_HAIR, imageList_chkbox);
			}
		}
	}
}
