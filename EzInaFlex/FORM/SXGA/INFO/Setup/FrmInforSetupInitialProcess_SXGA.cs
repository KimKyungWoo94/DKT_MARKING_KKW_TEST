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
		public partial class FrmInforSetupInitialProcess_SXGA : Form
		{
				public FrmInforSetupInitialProcess_SXGA()
				{
						InitializeComponent();

				}

				private void FrmInforSetupInitialProcess_SXGA_Load(object sender, EventArgs e)
				{
						FA.FRM.FrmTabInitProcConfigSet.Dock = DockStyle.Fill;
						ucTabControlX1.AddTab(("Configuration"), FA.FRM.FrmTabInitProcConfigSet);

						FA.FRM.FrmTabInitProcVisionCal.Dock = DockStyle.Fill;
						ucTabControlX1.AddTab("Vision Cal", FA.FRM.FrmTabInitProcVisionCal);

						FA.FRM.FrmTabInitProcVisionCal.Dock = DockStyle.Fill;
						ucTabControlX1.AddTab("PowerTable", FA.FRM.FrmTabInitProcPowerTable);

						//	FA.FRM.FrmTabInitProcStageCal.Dock = DockStyle.Fill;
						//	ucTabControlX1.AddTab("Stage Cal", FA.FRM.FrmTabInitProcStageCal);

						//	FA.FRM.FrmTabInitProcFindCPU.Dock = DockStyle.Fill;
						//	ucTabControlX1.AddTab("Find CPU", FA.FRM.FrmTabInitProcFindCPU);

						//FA.FRM.FrmTabInitProcFindFocus.Dock = DockStyle.Fill;
						//ucTabControlX1.AddTab("Find Focus", FA.FRM.FrmTabInitProcFindFocus);

   					FA.FRM.FrmTabInitProcCrossHair.Dock = DockStyle.Fill;
						ucTabControlX1.AddTab("Cross Hair", FA.FRM.FrmTabInitProcCrossHair);

						//FA.FRM.FrmTabInitProcGalvoCal.Dock = DockStyle.Fill;
						//ucTabControlX1.AddTab("Galvo Cal", FA.FRM.FrmTabInitProcGalvoCal);

						FA.FRM.FrmTabInitProcTowerLamp.Dock = DockStyle.Fill;
						ucTabControlX1.AddTab("Tower & Buzzer ", FA.FRM.FrmTabInitProcTowerLamp);
						ucTabControlX1.SelectedTabIndex = 0;
				}
		}
}
