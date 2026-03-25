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
		public partial class FrmPopupLaserWarmupAlarm : Form
		{
				Timer tmr = null;
				public FrmPopupLaserWarmupAlarm()
				{
						InitializeComponent();

						//this.Width = FA.DEF.VGA_WIDTH;
						//this.Height= FA.DEF.VGA_HEIGHT;

						tmr = new Timer();
						tmr.Tick += new EventHandler(Display);
						tmr.Interval = 50;

				}

				private void Display(object sender, EventArgs e)
				{
						try
						{
								tmr.Stop();
								GetInfo();
								CheckError();
								tmr.Enabled = this.Visible;

						}
						catch (System.Exception ex)
						{
								tmr.Enabled = this.Visible;
						}
				}

				private void GetInfo()
				{
						if (FA.LASER.Instance.GetType() == typeof(EzIna.Laser.Talon.Talon355))
						{
								EzIna.Laser.Talon.Talon355 pItem = FA.LASER.Instance as EzIna.Laser.Talon.Talon355;
								if (pItem != null)
								{
										lbl_SHGGetTemp.Text = pItem.SHG_Temperature_REG_Point.ToString();
										lbl_THGGetTemp.Text = string.Format("{0} ({1} hr)", pItem.THG_OvenTemprature, pItem.THG_CrystalSpotHours);
										lbl_SystemStatus.Text = pItem.strSystemStatus;
								}
						}
						else if (FA.LASER.Instance.GetType() == typeof(EzIna.Laser.IPG.GLPM))
						{
								EzIna.Laser.IPG.GLPM pItem = FA.LASER.Instance as EzIna.Laser.IPG.GLPM;
								if (pItem != null)
								{
										lbl_SHGGetTemp.Text = "Not Support";
										lbl_THGGetTemp.Text = "Not Support";
										lbl_SystemStatus.Text = FA.LASER.Instance.IsLaserReady == true ? "Ready" : " Not Ready";
								}
						}
						lbl_WarmupTimeRemaining.Text = FA.LASER.Instance.RemainWarmUptime.ToString();
				}

				private void CheckError()
				{
						lbl_SHGGetTemp.BackColor = (!FA.LASER.Instance.IsConnected || !FA.LASER.Instance.IsLaserReady) ? Color.Red : Color.Lime;
						lbl_THGGetTemp.BackColor = (!FA.LASER.Instance.IsConnected || !FA.LASER.Instance.IsLaserReady) ? Color.Red : Color.Lime;
						lbl_SystemStatus.BackColor = (!FA.LASER.Instance.IsConnected || !FA.LASER.Instance.IsLaserReady) ? Color.Red : Color.Lime;
						lbl_WarmupTimeRemaining.BackColor = (!FA.LASER.Instance.IsConnected || !FA.LASER.Instance.IsLaserReady) ? Color.Red : Color.Lime;
				}
				private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
				{
						WinAPIs.ReleaseCapture();
						WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
				}

				private void FrmPopupLaserWarmupAlarm_Load(object sender, EventArgs e)
				{
						this.StartPosition = FormStartPosition.CenterParent;
						this.lbl_Name.MouseDown += new MouseEventHandler(panelFrmTitleBar_MouseDown);
				}

				private void btn_Frm_Close_Click(object sender, EventArgs e)
				{
						this.Hide();
				}

				private void FrmPopupLaserWarmupAlarm_VisibleChanged(object sender, EventArgs e)
				{
						tmr.Enabled = Visible;
				}
		}
}
