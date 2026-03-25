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
    public partial class FrmInforSetupPowerMeter_SXGA : Form
    {
		Timer m_Timer = null;
        PowerMeter.Ohpir.GUI.FrmPowermeterSPC FrmSPC;
        Form AcvtiveViewForm=null;

		public FrmInforSetupPowerMeter_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();

            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(this.Display);
            this.Tag=GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_POWERMETER;
        }

		private void FrmInforSetupCylinder_Load(object sender, EventArgs e)
		{                     
            FrmSPC=new PowerMeter.Ohpir.GUI.FrmPowermeterSPC();    
            FrmSPC.Dock=DockStyle.Fill;
            this.panelMain.Controls.Add(FrmSPC);                        
            FA.MGR.POMgr.TreeView_Init(TV_DeviceList);
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
                    case "FrmPowermeterSPC":
                        {
                            FrmSPC.UpdateDisplay();
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
            TreeNode pSelectedNode=TV_DeviceList.SelectedNode;
            //아이콘 초기화.
               
          
            if (SelectDevice((PowerMeter.PowerMeterInterface)e.Node.Tag) == false)
            {
                TV_DeviceList.SelectedNode = pSelectedNode;               
            }
            else
            {
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
            }

        }
        private bool SelectDevice(PowerMeter.PowerMeterInterface a_Item)
        {
            if(a_Item== null)
                return false ;         
            switch(a_Item.DeviceType.Name)
            {
                case "SPC":
                    {
                        if(FrmSPC.InitForm(a_Item))
                        {
                            if (AcvtiveViewForm != null)
                              AcvtiveViewForm.Hide();
                            AcvtiveViewForm = FrmSPC;
                            AcvtiveViewForm.Show();
                            return true;
                        }                        
                    }
                    break;
            }
            return false;
        }   
    }
}
