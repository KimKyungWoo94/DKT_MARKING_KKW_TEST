using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
		public partial class FrmPopupError : Form
		{
				public string Msg { get; set; }

				private string m_Dscr;
				public string Dscr {get {return m_Dscr; } set{m_Dscr=value; } }
				public int ErrNo { get; set; }

				public FrmPopupError()
				{
						InitializeComponent();
						this.TopMost = true;
						m_Dscr="";
				}

				private void FrmPopupError_VisibleChanged(object sender, EventArgs e)
				{
						if (Visible)
						{
								ErrorMsg();
								MF.TOWERLAMP.StartBuzzer = true;
								if (MF.TOWERLAMP.StartBuzzer)
										btnBuzzerOn.ImageIndex = 0;
								else
										btnBuzzerOn.ImageIndex = 1;
								ucDadaFlicker.Start();
						}
						else
								ucDadaFlicker.Stop();
				}

				private void ErrorMsg()
				{
						this.InvokeIfNeeded(() =>
						{
								IniFile Ini = new IniFile(FA.FILE.Error);
								string strSection = "Error List";
								Msg = Ini.Read(strSection, ErrNo.ToString(), "None");
								lbl_ErrorMsg.Text =Msg;
								lbl_ErrNo.Text = ErrNo.ToString();
								lbl_Err_Descr.Text=Dscr;
								Dscr="";
								FA.LOG.ErrorOccurMsg(ErrNo, string.Format("{0}\n{1}",lbl_ErrorMsg.Text,lbl_Err_Descr.Text));
						});
				}
				private void btnclose_Click(object sender, EventArgs e)
				{
						this.Hide();
				}
				private void btnBuzzerOn_Click(object sender, EventArgs e)
				{
						if (MF.TOWERLAMP.StartBuzzer)
						{
								btnBuzzerOn.ImageIndex = 1;
								MF.TOWERLAMP.StartBuzzer = false;
						}
						else
						{
								btnBuzzerOn.ImageIndex = 0;
								MF.TOWERLAMP.StartBuzzer = true;
						}
				}

				private void label_Title_MouseDown(object sender, MouseEventArgs e)
				{
						WinAPIs.ReleaseCapture();
						WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
				}
		}
}
