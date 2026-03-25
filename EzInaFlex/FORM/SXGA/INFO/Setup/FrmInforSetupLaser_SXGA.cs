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
    public partial class FrmInforSetupLaser_SXGA : Form
    {
		Timer m_Timer = null;
        Laser.LaserInterface pItem=null;
        Laser.Talon.GUI.FrmLaserTalon FrmTalonLaser;
        Laser.IPG.GUI.FrmLaserGLPM FrmGLPMLaser;
        Laser.IPG.GUI.FrmLaserGLPM_V8 FrmGLPM_V8_Laser;
        Form AcvtiveViewForm=null;

		public FrmInforSetupLaser_SXGA()
        {
            InitializeComponent();
            m_Timer = new Timer();

            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(this.Display);
        }

		private void FrmInforSetupLaser_Load(object sender, EventArgs e)
		{                     
            FrmTalonLaser=(Laser.Talon.GUI.FrmLaserTalon)InitializeLaserForm(typeof(Laser.Talon.GUI.FrmLaserTalon));
            FrmGLPMLaser =(Laser.IPG.GUI.FrmLaserGLPM )InitializeLaserForm(typeof(Laser.IPG.GUI.FrmLaserGLPM));  
            FrmGLPM_V8_Laser=(Laser.IPG.GUI.FrmLaserGLPM_V8 )InitializeLaserForm(typeof(Laser.IPG.GUI.FrmLaserGLPM_V8));  
            FA.MGR.LaserMgr.TreeView_Init(TV_DeviceList);
		}
        private Form InitializeLaserForm(Type pFormType)
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
            if(AcvtiveViewForm!=null)
            {
                switch(AcvtiveViewForm.GetType().Name)
                {
                    case "FrmLaserTalon":
                        {
                            FrmTalonLaser.UpdateDisplay();
                        }
                        break;
                    case "FrmLaserGLPM":
                        {
                            FrmGLPMLaser.UpdateDisplay();
                        }
                        break;
                    case "FrmLaserGLPM_V8":
                        {
                            FrmGLPM_V8_Laser.UpdateDisplay();
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

            SelectLaser((Laser.LaserInterface)e.Node.Tag);
        }
        private void SelectLaser(Laser.LaserInterface a_Item)
        {
            if(a_Item== null)
                return ;
            if(pItem!=null)
            {
                if(pItem.Equals(a_Item))
                    return;
            }
            if(AcvtiveViewForm!=null)
            {
                AcvtiveViewForm.Hide();
            }
                
            switch(a_Item.DeviceType.Name)
            {
                case "Talon355":
                    {
                        pItem=a_Item;
                        FrmTalonLaser.InitForm(pItem);
                        AcvtiveViewForm=FrmTalonLaser;                        
                        AcvtiveViewForm.Show();
                    }
                    break;
                case "GLPM":
                    {
                        pItem=a_Item;
                        FrmGLPMLaser.InitForm(pItem);
                        AcvtiveViewForm=FrmGLPMLaser;
                        AcvtiveViewForm.Show();
                    }
                    break;
                case "GLPM_V8":
                    {
                        pItem = a_Item;
                        FrmGLPM_V8_Laser.InitForm(pItem);
                        AcvtiveViewForm = FrmGLPM_V8_Laser;
                        AcvtiveViewForm.Show();
                    }
                    break;
            }
        }
    }
}
