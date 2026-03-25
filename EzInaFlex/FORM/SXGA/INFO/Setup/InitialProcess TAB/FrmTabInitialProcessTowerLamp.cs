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
	public partial class FrmTabInitialProcessTowerLamp : Form
	{
		public FrmTabInitialProcessTowerLamp()
		{
			InitializeComponent();
		}

		private void FrmTabInitialProcessTowerLamp_VisibleChanged(object sender, EventArgs e)
		{
			if(Visible)
				EzIna.MF.TOWERLAMP.WriteTo(dataGridView_LampNBuzzer    ); // 램프      그리드 셋팅
		}

		private void btn_TowerLampOpen_Click(object sender, EventArgs e)
		{
			if (MsgBox.Confirm("Would you like to open the tower lame & buzzer settings from the file?"))
			{
				EzIna.MF.TOWERLAMP.LoadFromFile(FA.FILE.InitProcTowerNBuzzer);
				EzIna.MF.TOWERLAMP.WriteTo(dataGridView_LampNBuzzer    ); // 램프      그리드 셋팅
			}
		}

		private void btn_TowerLampSave_Click(object sender, EventArgs e)
		{
			if (MsgBox.Confirm("Would you like to save the tower lamp & buzzer settings?"))
			{
				MF.TOWERLAMP.ReadFrom(dataGridView_LampNBuzzer);
				MF.TOWERLAMP.SaveToFile(FA.FILE.InitProcTowerNBuzzer);
			}
		}
	}
}
