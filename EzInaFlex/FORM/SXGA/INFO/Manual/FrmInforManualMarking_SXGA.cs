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
    public partial class FrmInforManualMarking_SXGA : Form
    {
		Timer m_Timer = null;
        Attenuator.OPTOGAMA.GUI.FrmAttenuatorLPA  FrmLPA;
        Form AcvtiveViewForm=null;

		public FrmInforManualMarking_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();

            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(this.Display);
        }

		private void FrmInforSetupCylinder_Load(object sender, EventArgs e)
		{                     
          
		}
		private void FrmInforSetupLaser_SXGA_VisibleChanged(object sender, EventArgs e)
        {
            m_Timer.Enabled=this.Visible;
        }
		private void Display(object sender, EventArgs e)
		{
            m_Timer.Enabled=false;
		   
            m_Timer.Enabled=this.Visible;
		}
    

        public void FlickrDisplay()
        {

        }
      
    }
}
