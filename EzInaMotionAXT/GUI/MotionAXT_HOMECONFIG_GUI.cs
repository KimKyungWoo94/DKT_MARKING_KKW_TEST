using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion.AXT.GUI
{
    public partial class CMotionAXT_HOMECONFIG_GUI : UserControl
    {

        private CMotionAXT_HOMESEARCH_SETTING_GUI m_HomeSearchSetting;
        private EzIna.Motion.MotionAXTItem  m_pMotion;

        public CMotionAXT_HOMECONFIG_GUI()
        {
            InitializeComponent();
            m_HomeSearchSetting=new CMotionAXT_HOMESEARCH_SETTING_GUI();
            m_pMotion=null;
        }
        protected void ClearAll()
        {
       
        }
        protected void InitBinding()
        {
            #region     HomeSearch
            cboHomeLevel.DataSource = new BindingSource(m_HomeSearchSetting.m_DicHomeSignalActiveLevel, null);
            cboHomeLevel.DataBindings.Add(m_HomeSearchSetting.BindingComboHomeSignalActiveLevel);
            cboZPhaseUse.DataSource = new BindingSource(m_HomeSearchSetting.m_DicHomeZPhaseUse, null);
            cboZPhaseUse.DataBindings.Add(m_HomeSearchSetting.BindingComboHomeZPhaseUse);
            cboHomeSignal.DataSource = new BindingSource(m_HomeSearchSetting.m_DicHomeSignal, null);
            cboHomeSignal.DataBindings.Add(m_HomeSearchSetting.BindingComboHomeSignal);
            cboHomeDir.DataSource = new BindingSource(m_HomeSearchSetting.m_DicHomeDir, null);
            cboHomeDir.DataBindings.Add(m_HomeSearchSetting.BindingComboHomeDir);
            edtHomeClrTime.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeClearTime);
            edtHomeOffset.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeOffset);
            edtVelFirst.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeVelFirst);
            edtVelSecond.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeVelSecond);
            edtVelThird.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeVelThird);
            edtVelLast.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeVelLast);
            edtAccFirst.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeAccFirst);
            edtAccSecond.DataBindings.Add(m_HomeSearchSetting.BindingTextHomeAccSecond);            
            #endregion  HomeSearch

        }
        public void InitControl(MotionInterface a_pItem)
        {
            if(a_pItem!=null)
            {

                if(a_pItem.DeviceType.Name!="CMotionAXT")
                {
                    return;
                }
                CAXT_AxisModuleInfo pAxisInfor=null;                    
                m_pMotion=a_pItem as MotionAXTItem;
                pAxisInfor=CMotionAXT.GetAxisInformation(m_pMotion.iAxisNo);
                InitializeControl(pAxisInfor.iModuleID);
            }            
        }              
        protected void InitializeControl(uint a_ModouleType)
        {
           
            switch (a_ModouleType)
            {                
                case (uint)AXT_MODULE.AXT_SMC_4V04:
                case (uint)AXT_MODULE.AXT_SMC_R1V04:
                case (uint)AXT_MODULE.AXT_SMC_2V04:
                case (uint)AXT_MODULE.AXT_SMC_R1V04PM2Q:
                case (uint)AXT_MODULE.AXT_SMC_R1V04PM2QE:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIPM:
                    {
                        ClearAll();                
                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0,"00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1,"01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4,"04:HomeSensor");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(5,"05:EncodZPhase");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0,"00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1,"01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0,"00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1,"01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2,"02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                    }
                    break;        
                case (uint)AXT_MODULE.AXT_SMC_R1V04A4:
                    {
                        ClearAll();
                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(5, "05:EncodZPhase");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0, "00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir

                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");                    
                        #endregion  ZPhaseUse
                    }
                    break;
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIPM:
                    {
                        ClearAll();                            
                        //Home
                        #region     HomeSignal                        
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");                        
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        #endregion  ZPhaseUse
                    }
                    break;

                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIISV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04A5:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICL:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICR:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIORI:
                case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV_R:
                    {
                        ClearAll();                  
                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");                        
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1, "01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2, "02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                    }
                    break;
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV_MD:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7S:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7W:
                    {
                        ClearAll();               
                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0, "00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1, "01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2, "02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                    }
                    break;
                default:
                    {
                        ClearAll();
                    }
                    break;
            }
            COMMON:
            {
               
            }
        }
        



    }
}
