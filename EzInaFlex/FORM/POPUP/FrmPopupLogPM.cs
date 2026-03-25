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
	public partial class FrmPopupLogPM : Form
	{
		public FrmPopupLogPM()
		{
			InitializeComponent();
			this.Width = FA.DEF.VGA_WIDTH;
			this.Height = FA.DEF.VGA_HEIGHT;
			this.StartPosition = FormStartPosition.CenterParent;

			listBox_PM.HorizontalScrollbar = true;
		}	
	}
}
