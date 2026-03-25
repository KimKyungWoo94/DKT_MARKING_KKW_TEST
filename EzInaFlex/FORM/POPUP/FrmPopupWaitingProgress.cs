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
	public partial class FrmPopupWaitingProgress : Form
	{
		public FrmPopupWaitingProgress()
		{
			InitializeComponent();
			this.Opacity = 0.5;
			this.StartPosition = FormStartPosition.CenterScreen;
		}
		public void CenterToForm(Form a_pParents)
		{
				this.StartPosition=FormStartPosition.Manual;
				this.Location=new Point((a_pParents.Location.X+a_pParents.Width)/2- this.Width/2,
												(a_pParents.Location.Y+a_pParents.Height)/2-this.Height/2);
											
		}
		private void FrmPopupWaitingProgress_VisibleChanged(object sender, EventArgs e)
		{
			if(this.Visible)
			{
				ucWaitingProgressBar1.Start();
				ucWaitingProgressBar1.Rotation = GUI.UserControls.ucWaitingProgressBar.Direction.CLOCKWISE;
			}
			else
				ucWaitingProgressBar1.Stop();
		}
	}
}
