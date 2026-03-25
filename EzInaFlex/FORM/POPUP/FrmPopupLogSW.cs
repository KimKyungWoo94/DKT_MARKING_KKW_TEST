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
	public partial class FrmPopupLogSW : Form
	{
		public FrmPopupLogSW()
		{
			InitializeComponent();
			this.Width = FA.DEF.VGA_WIDTH;
			this.Height = FA.DEF.VGA_HEIGHT;
			this.StartPosition = FormStartPosition.CenterParent;

			listBox_SW.HorizontalScrollbar = true;
			
		}

		private void FrmPopupLogSW_MouseDown(object sender, MouseEventArgs e)
		{

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

		private void FrmPopupLogSW_Load(object sender, EventArgs e)
		{
			listBox_SW.HorizontalScrollbar = true;
		}
	}
}
