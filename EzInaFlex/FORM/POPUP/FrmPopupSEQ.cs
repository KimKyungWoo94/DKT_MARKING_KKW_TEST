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
	public partial class FrmPopupSEQ : Form
	{
		public FrmPopupSEQ()
		{
			InitializeComponent();
		}

		private void FrmPopupSEQ_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void btn_Frm_Close_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
		{
			WinAPIs.ReleaseCapture();
			WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
		}
	}
}
