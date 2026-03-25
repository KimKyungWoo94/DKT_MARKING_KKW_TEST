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
	public partial class FrmPopupLogin : Form
	{
		public FrmPopupLogin()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private void btn_Operator_Click(object sender, EventArgs e)
		{
			btn_Operator	.ImageIndex = btn_Operator		== sender ? 1 : 0;
			btn_Engineer	.ImageIndex = btn_Engineer		== sender ? 1 : 0;
			btn_Supervisor	.ImageIndex = btn_Supervisor	== sender ? 1 : 0;

			//edtPassword.Enabled = btnOper != sender;
			edtPassword.Focus();
		}

		private void btn_Frm_Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
		{
			WinAPIs.ReleaseCapture();
			WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
		}

		private void btn_Login_Click(object sender, EventArgs e)
		{
			if (MF.USER.Items[0].Password == edtPassword.Text)
			{
				MF.USER.Authority = MF.Authority.Developer;
			}
			else if (btn_Operator.ImageIndex == 1)
			{
				MF.USER.Authority = MF.Authority.Operator;
			}
			else if (btn_Engineer.ImageIndex == 1)
			{
				if (MF.USER.Items[2].Password == edtPassword.Text)
					MF.USER.Authority = MF.Authority.Engineer;
				else
				{
					lbl_Msg.Text = "Engineer Password mismatch!";
					edtPassword.Text = "";
					edtPassword.Focus();
					return;
				}
			}
			else if (btn_Supervisor.ImageIndex == 1)
			{
				if (MF.USER.Items[3].Password == edtPassword.Text)
					MF.USER.Authority = MF.Authority.Supervisor;
				else
				{
					lbl_Msg.Text = "Supervisor Password mismatch!";
					edtPassword.Text = "";
					edtPassword.Focus();
					return;
				}
			}
			else
				return;

			edtPassword.Text = "";
			Close();
		}
	}
}
