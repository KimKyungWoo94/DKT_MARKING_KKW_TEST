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
    public partial class FrmInforSetupScanner_SXGA : Form
    {
		Timer m_Timer = null;
        Scanner.ScanlabRTC5 pItem=null;
        Scanner.GUI.FrmScanlabRTC FrmScanlabRTC5;
        //Form AcvtiveViewForm=null;

		public FrmInforSetupScanner_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();
            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(this.Display);
            this.Tag=GUI.D.eTagFormID.FORM_ID_INFOR_SETUP_SCANNER;
            FrmScanlabRTC5=( Scanner.GUI.FrmScanlabRTC)InitializeForm(typeof(Scanner.GUI.FrmScanlabRTC));
        }

		private void FrmInforSetupScanner_Load(object sender, EventArgs e)
		{                     
           
            FA.MGR.RTC5Mgr.TreeView_Init(this.TV_DeviceList);
            //FrmScanlabRTC5.Show();
		}
        private Form InitializeForm(Type pFormType)
        {
            Form a_Form=(Form)Activator.CreateInstance(pFormType) ;
            a_Form.Dock=DockStyle.Fill;
            this.panelMain.Controls.Add(a_Form);               
            return a_Form; 
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
          /*if(AcvtiveViewForm!=null)
            {
                switch(AcvtiveViewForm.GetType().Name)
                {
                  
                }
            }
            */
            if(FrmScanlabRTC5.Visible==true)
            {
                FrmScanlabRTC5.UpdateDisplay();
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

            SelectDevice((Scanner.ScanlabRTC5)e.Node.Tag);
        }
        private void SelectDevice(Scanner.ScanlabRTC5 a_Item)
        {
            if(a_Item== null)
                return ;
            if(pItem!=null)
            {
                if(pItem.Equals(a_Item))
                    return;
            }
            pItem =a_Item; 
            FrmScanlabRTC5.InitForm(pItem);        
            if(FrmScanlabRTC5.Visible==false)
            {
                FrmScanlabRTC5.Show();
            }
        }

        private void TV_DeviceList_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
