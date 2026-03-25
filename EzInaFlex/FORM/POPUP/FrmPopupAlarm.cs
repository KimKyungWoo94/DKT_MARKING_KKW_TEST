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
		public partial class FrmPopupAlarm : Form
		{
				delegate void AlarmHandler();
				
				public string Msg { get; set; }
				public int AlarmNo { get; set; }

				public FrmPopupAlarm()
				{
						InitializeComponent();
				}

				private void AlarmMsg()
				{
						if (IsDisposed || Disposing)
								return;

						if (InvokeRequired)
						{
								AlarmHandler OnErrorMsg = new AlarmHandler(AlarmMsg);
								Invoke(OnErrorMsg, new object());
						}
						else
						{
								//IniFile Ini = new IniFile(FA.FILE.Alarm);
								//string strSection = "Alarm List";

								//Msg = Ini.Read(strSection, AlarmNo.ToString(), "None");

								// 				int euckrCodepage = 51949;
								// 				System.Text.Encoding utf8	= System.Text.Encoding.UTF8;
								// 				System.Text.Encoding euckr	= System.Text.Encoding.GetEncoding(euckrCodepage);
								// 				byte[] utf8Bytes = utf8.GetBytes(Msg);
								// 				byte[] euckrBytes = euckr.GetBytes(Msg);
								// 
								// 				string decodedStringByEUCKR = euckr.GetString(utf8Bytes);
								// 				string decodedStringByUTF8 = utf8.GetString(utf8Bytes);
								lbl_AlarmMsg.Text = Msg;
						}
				}

				private void FrmPopupNotification_VisibleChanged(object sender, EventArgs e)
				{
						if (Visible)
						{
								AlarmMsg();
								ucProcessProgressBar1.Start();
								if (FA.MGR.IOMgr != null)
								{
										//if (FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_BUZZER).Value)
										//{
										//	btnBuzzerOn.ImageIndex = 1;
										//}
										//else
										//{
										//	btnBuzzerOn.ImageIndex = 0;
										//}
								}
						}
						else
						{
								ucProcessProgressBar1.Stop();
						}
				}

				private void btnclose_Click(object sender, EventArgs e)
				{
						this.Hide();
				}

				private void btnBuzzerOn_Click(object sender, EventArgs e)
				{
						if (MF.TOWERLAMP.GetBuzzerStatus(MF.eBuzzerItem.b1))
						{
								btnBuzzerOn.ImageIndex = 0;
								MF.TOWERLAMP.StartBuzzer = false;
						}
						else
						{
								btnBuzzerOn.ImageIndex = 1;
								MF.TOWERLAMP.StartBuzzer = true;
						}
				}

		}
}
