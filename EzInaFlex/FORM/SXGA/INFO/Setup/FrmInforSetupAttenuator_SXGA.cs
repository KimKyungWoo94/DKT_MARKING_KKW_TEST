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
    public partial class FrmInforSetupAttenuator_SXGA : Form
    {
		Timer m_Timer = null;
        Attenuator.OPTOGAMA.GUI.FrmAttenuatorLPA  FrmLPA;
        Form AcvtiveViewForm=null;

		public FrmInforSetupAttenuator_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();

            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(this.Display);
        }

		private void FrmInforSetupCylinder_Load(object sender, EventArgs e)
		{                     
            FrmLPA=new Attenuator.OPTOGAMA.GUI.FrmAttenuatorLPA();    
            FrmLPA.Dock=DockStyle.Fill;
            this.panelMain.Controls.Add(FrmLPA);                        
            FA.MGR.AttenuatorMgr.TreeView_Init(TV_DeviceList);
		}
		private void FrmInforSetupLaser_SXGA_VisibleChanged(object sender, EventArgs e)
        {
            m_Timer.Enabled=this.Visible;
        }
		private void Display(object sender, EventArgs e)
		{
            m_Timer.Enabled=false;
								UpdateActiveForm();
            m_Timer.Enabled=this.Visible;
		}
        private void UpdateActiveForm()
        {
            if(AcvtiveViewForm!=null)
            {
                switch(AcvtiveViewForm.GetType().Name)
                {
                    case "FrmAttenuatorLPA":
                        {
                            FrmLPA.UpdateDisplay();
                        }
                        break;
                }
            }
        }

        public void FlickrDisplay()
        {

        }
        private void TV_LaserList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            //아이콘 초기화.
            foreach (TreeNode pParent in TV_DeviceList.Nodes)
            {
                pParent.ImageIndex = 2;
                pParent.SelectedImageIndex = 2;

                foreach (TreeNode pChild in pParent.Nodes)
                {
                    pChild.ImageIndex = 0;
                    pChild.SelectedImageIndex = 0;
                }

            }
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
            SelectDevice((Attenuator.AttuenuatorInterface)e.Node.Tag);
        }
        private void SelectDevice(Attenuator.AttuenuatorInterface a_Item)
        {
            if(a_Item== null)
                return ;
            if(AcvtiveViewForm!=null)
                AcvtiveViewForm.Hide();
            switch(a_Item.DeviceType.Name)
            {
                case "LPA":
                    {
                        FrmLPA.InitForm(a_Item);
                        AcvtiveViewForm=FrmLPA;
                        AcvtiveViewForm.Show();
                    }
                    break;
            }
        }
    }
}
