using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EzIna.FA;
using EzIna.GUI.UserControls;
using static EzIna.RunningScanner;

namespace EzIna
{
    public partial class FrmInforRecipePanel_SXGA : Form
    {
        public delegate void RecipeEventHandler();
        private RecipeEventHandler _OnRecipeChangeEvent;

        public event RecipeEventHandler OnRecipeChangeEvent
        {
            add
            {
                _OnRecipeChangeEvent += value;
            }
            remove
            {
                _OnRecipeChangeEvent -= value;
            }

        }
        public FrmInforRecipePanel_SXGA()
        {
            InitializeComponent();

            btn_RecipeOpen.Click += btn_RecipeButton_Click;
            btn_RecipeRename.Click += btn_RecipeButton_Click;
            btn_RecipeAdd.Click += btn_RecipeButton_Click;
            btn_RecipeDelete.Click += btn_RecipeButton_Click;
            btn_RecipeSave.Click += btn_RecipeButton_Click;


            listBox_Recipe.DoubleBuffered(true);
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                FA.MGR.RecipeMgr.listBox_Recipe_Init(listBox_Recipe);
            }
        }


        private void btn_RecipeButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Tag == null)
                return;

            string strSelectedItemName = "";

            if (FA.MGR.RunMgr.eRunMode != FA.DEF.eRunMode.Stop)
            {
                MsgBox.Error("Run Mode Stop First");
                return;
            }
            if (FA.MGR.RecipeRunningData.bCurrentInProess == true)
            {
                MsgBox.Error("Processing JIG Exist ,Run Complete or Remove first");
                return;
            }
            if (listBox_Recipe.SelectedIndex == -1 && (sender as Button).Tag.ToString() != "ADD")
            {
                MsgBox.Error("Please select a recipe first");
                return;
            }

            if ((sender as Button).Tag.ToString() != "ADD")
            {
                strSelectedItemName = listBox_Recipe.Items[listBox_Recipe.SelectedIndex].ToString();
            }


            string strValue = "";
            switch ((sender as Button).Tag.ToString().ToUpper())
            {
                case "OPEN":
                    {
                        if (MsgBox.Confirm(string.Format("{0}\"{1}\"??", "Would you like to open the ", strSelectedItemName)))
                        {
                            //if (FA.MGR.RecipeMgr.SelectedModel.ToUpper().Equals(strSelectedItemName.ToUpper()))

                            FA.MGR.ProjectMgr.ProjectChange(strSelectedItemName);
                            FA.MGR.RecipeMgr.RecipeOpen(strSelectedItemName);
                            FA.MGR.RecipeMgr.listBox_Recipe_Init(listBox_Recipe);
                            if (_OnRecipeChangeEvent != null)
                                _OnRecipeChangeEvent();
                            FA.LOG.Recipe("RECIPE", string.Format("{0} Recipe Open", strSelectedItemName));
                        }
                    }
                    break;

                case "RENAME":
                    {
                        if (string.IsNullOrEmpty(strSelectedItemName.ToUpper()) == false)
                        {
                            if (strSelectedItemName.ToUpper() == "DEFAULT")
                            {
                                MsgBox.Error("Isn't Allow DEFAULT Recipe Rename");
                                break;
                            }
                            AlphaKeypad AlphaKeyPad = new AlphaKeypad();
                            AlphaKeyPad.OldValue = "";
                            AlphaKeyPad.NewValue = "";
                            AlphaKeyPad.MaxLength = 10;
                            if (AlphaKeyPad.ShowDialog() == DialogResult.Cancel)
                                break;
                            else
                                strValue = AlphaKeyPad.NewValue;

                            if (FA.MGR.RecipeMgr.CheckRecipeDuplicate(strValue.ToUpper()))
                            {
                                if (string.IsNullOrEmpty(strValue.ToUpper()))
                                {
                                    MsgBox.Error(string.Format("Already Exist Recipe Name {0}", strValue.ToUpper()));
                                    break;
                                }
                                else
                                {
                                    if (FA.MGR.RecipeMgr.SelectedModel.ToUpper().Equals(strValue.ToUpper()))
                                    {
                                        MsgBox.Error("Same Recipe Name can't be rename");
                                        break;
                                    }
                                }
                            }
                            if (MsgBox.Confirm(string.Format("Would you like to Rename?\n {0} -> {1}", strSelectedItemName.ToUpper(), strValue.ToUpper())))
                            {
                                string Selectedstr = strSelectedItemName;

                                FA.MGR.ProjectMgr.ProjectRename(strSelectedItemName.ToUpper(), strValue.ToUpper());
                                FA.MGR.RecipeMgr.RecipeRename(strSelectedItemName.ToUpper(), strValue.ToUpper());
                                FA.MGR.RecipeMgr.listBox_Recipe_Init(listBox_Recipe);
                                if (FA.MGR.RecipeMgr.SelectedModel.ToUpper().Equals(Selectedstr.ToUpper()))
                                {

                                }
                                FA.MGR.ProjectMgr.ProjectChange(strValue);
                                FA.MGR.RecipeMgr.RecipeOpen(strValue);
                                FA.LOG.Recipe("RECIPE", string.Format("{0} -> {1} Recipe Name Change", Selectedstr.ToUpper(), strValue.ToUpper()));
                            }
                        }
                        else
                        {
                            MsgBox.Error("Select Recipe First");
                            break;
                        }

                    }
                    break;

                case "ADD":
                    {
                        AlphaKeypad AlphaKeyPad = new AlphaKeypad();
                        AlphaKeyPad.OldValue = "";
                        AlphaKeyPad.NewValue = "";
                        AlphaKeyPad.MaxLength = 10;

                        if (AlphaKeyPad.ShowDialog(this) == DialogResult.Cancel)
                            break;
                        else
                            strValue = AlphaKeyPad.NewValue;
                        if (string.IsNullOrEmpty(strValue.ToUpper()) == false)
                        {
                            if (FA.MGR.RecipeMgr.CheckRecipeDuplicate(strValue.ToUpper()) == false)
                            {
                                if (MsgBox.Confirm(string.Format("{0}\"{1}\"??", "Would you like to Add the ", strValue.ToUpper())))
                                {
                                    FA.MGR.ProjectMgr.ProjectAdd(strValue.ToUpper());
                                    FA.MGR.RecipeMgr.RecipeAdd(strValue.ToUpper());
                                    FA.MGR.RecipeMgr.listBox_Recipe_Init(listBox_Recipe);
                                    FA.LOG.Recipe("RECIPE", string.Format("{0} Recipe ADD", strValue.ToUpper()));
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strValue.ToUpper()))
                                {
                                    MsgBox.Error(string.Format("Already Exist Recipe Name {0}", strValue.ToUpper()));
                                    break;
                                }
                                else
                                {
                                    MsgBox.Error("Empty Recipe Name Isn't Allow");
                                    break;
                                }
                            }
                        }
                    }
                    break;

                case "DELETE":
                    {
                        if (strSelectedItemName.ToUpper() == "DEFAULT")
                        {
                            MsgBox.Error("Isn't allow DEFAULT Recipe Delete");
                            break;
                        }
                        if (FA.MGR.RecipeMgr.SelectedModel.ToUpper().Equals(strSelectedItemName.ToUpper()))
                        {
                            MsgBox.Error("Active Recipe can't delete");
                            break;
                        }

                        if (MsgBox.Confirm(string.Format("{0}\"{1}\"??", "Would you like to delete the ", strSelectedItemName)))
                        {
                            FA.MGR.RecipeMgr.RecipeDelete(strSelectedItemName);
                            FA.MGR.ProjectMgr.ProjectDelete(strSelectedItemName);
                            FA.MGR.RecipeMgr.listBox_Recipe_Init(listBox_Recipe);
                            FA.LOG.Recipe("RECIPE", string.Format("{0} Recipe Removed", strSelectedItemName.ToUpper()));
                        }

                    }
                    break;

                case "SAVE":
                    {
                        if (string.IsNullOrEmpty(FA.MGR.RecipeMgr.SelectedModel.ToUpper()) == false)
                        {
                            //GUI.UserControls.FlexiableMsgBox.MAX_WIDTH_FACTOR=0.5;
                            Tuple<bool, string> pRet;
                            pRet = FA.MGR.RecipeRunningData.CheckMultiArrayVaildation(true);
                            if (pRet.Item1)
                            {
                                string strChangedRecipe = FA.MGR.RecipeMgr.GetPreSaveConfirmMSG();
                                if (GUI.UserControls.FlexiableMsgBox.Show(string.Format("Would you like to save??\n\n{0}", strChangedRecipe), "Confirm", MessageBoxButtons.YesNo)
                                     == DialogResult.Yes)
                                {
                                    FA.MGR.ProjectMgr.ProjectSave();
                                    FA.MGR.ProjectMgr.ProjectOpen();
                                    FA.MGR.RecipeMgr.RecipeSave(FA.MGR.RecipeMgr.SelectedModel);
                                    FA.MGR.RecipeMgr.RecipeOpen(FA.MGR.RecipeMgr.SelectedModel);

                                    if (_OnRecipeChangeEvent != null)
                                        _OnRecipeChangeEvent();
                                    FA.LOG.Recipe("RECIPE", string.Format("{0} Recipe Save\n{1}", strSelectedItemName.ToUpper(), strChangedRecipe));
                                }
                            }
                            else
                            {
                                MsgBox.Error(string.Format("Multi Array parameter Error, Check Parameter{0}{1}",Environment.NewLine, pRet.Item2));
                            }

                        }
                        else
                        {
                            MsgBox.Error("Active Recipe isn't Exist Open First");
                            break;
                        }
                    }
                    break;


            }

        }

        private void Form_Load(object sender, EventArgs e)
        {
            FA.FRM.FrmTabNewRecipeCommon.Dock = DockStyle.Fill;
            ucTabControlX1.AddTab("Common", FA.FRM.FrmTabNewRecipeCommon);
            FA.FRM.FrmTabNewRecipeProcess.Dock = DockStyle.Fill;
            ucTabControlX1.AddTab("Process", FA.FRM.FrmTabNewRecipeProcess);
            FA.FRM.FrmTabNewRecipeVision.Dock = DockStyle.Fill;
            ucTabControlX1.AddTab("Vision", FA.FRM.FrmTabNewRecipeVision);
            ucTabControlX1.SelectedTabIndex = 0;
            lb_SelectedModel.DataBindings.Add(new Binding("Text", FA.MGR.RecipeMgr, "SelectedModel", false, DataSourceUpdateMode.OnPropertyChanged, 0));

            _OnRecipeChangeEvent += FA.FRM.FrmTabNewRecipeCommon.OnRecipeChangeEvent;
            _OnRecipeChangeEvent += FA.FRM.FrmTabNewRecipeProcess.OnRecipeChangeEvent;
            _OnRecipeChangeEvent += FA.FRM.FrmTabNewRecipeVision.OnRecipeChangeEvent;
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            StreamWriter SW = null;
            
         
            try
            {
                SW = new StreamWriter(@"d:\TestMultiArray.csv", false, Encoding.UTF8);
                #region [ simul Multi Array coordinate ]
                #region [ Init Recipe Param ]


                int iSubSeqRowMovingMax = FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                iSubSeqRowMovingMax = FA.RCP_Modify.Inspection_RowCount.GetValue<int>() <= 0 ?
                    0 :
                   (int)(Math.Ceiling((double)iSubSeqRowMovingMax / FA.RCP_Modify.Inspection_RowCount.GetValue<int>()) - 1);
                int iSubSeqColMovingMax = FA.RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                iSubSeqColMovingMax = FA.RCP_Modify.Inspection_ColCount.GetValue<int>() <= 0 ?
                    0 :
                    (int)(Math.Ceiling((double)iSubSeqColMovingMax / FA.RCP_Modify.Inspection_ColCount.GetValue<int>()) - 1);

                int iSubSeqInspRowLastCount = FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() % FA.RCP_Modify.Inspection_RowCount.GetValue<int>();

                int iSubSeqMultiArrayMaxCount = FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>();
                int iSubSeqProductDoneMaxCount = FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                iSubSeqProductDoneMaxCount *= FA.RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();

                int iSubSeqRowMovingExeCount = 0;
                int iSubSeqColMovingExeCount = 0;
                int iSubSeqMultiArrayExeCount = 0;
                int iSubSeqProductDoneCount = 0;
                bool bSusbSeqRowReverseDirEnable = false;
                int iSubSeqProductDoneRowProgressCount = 0;
                int iSubSeqProductDoneColProgressCount = 0;
                int iSubSeqInspRowCount = FA.RCP_Modify.Inspection_RowCount.GetValue<int>(); ;
                int iSubSeqInspColCount = FA.RCP_Modify.Inspection_ColCount.GetValue<int>(); ;
                
                //m_bSusbSeqColReverseDirEnable=false;
                if (iSubSeqRowMovingMax < 0)
                    iSubSeqRowMovingMax = 0;
                if (iSubSeqColMovingMax < 0)
                    iSubSeqColMovingMax = 0;

                #endregion [ Init Recipe Param ]
                double fSubSeqTargetPosX;
                double fSubSeqTargetPosY;
                double fSubSeqTargetPosZ;
                double fSubSeqRowMovingPitch;
                double fSubSeqColMovingPitch;

                double fSubSeqRowMovingProductPitch;
                double fSubSeqColMovingProductPitch;

                double fSubSeqRowMovingPitchPerOnce;
                double fSubSeqColMovingPitchPerOnce;

                int iForLoopCount;
                bool bSubSeqMovingLastPerOneLine=false;
                int iInspRowCount;
                int iInspColCount;
                int RowCountMax;
                int ColCountMax;
                do
                {
                    fSubSeqTargetPosX = RCP_Modify.COMMON_INIT_PROC_AREA_X_POS.GetValue<double>();
                    fSubSeqTargetPosY = RCP_Modify.COMMON_INIT_PROC_AREA_Y_POS.GetValue<double>();
                    fSubSeqTargetPosZ = RCP_Modify.COMMON_INIT_PROC_AREA_Z_POS.GetValue<double>();
                    
                    
                    
                    fSubSeqRowMovingProductPitch = FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                    fSubSeqRowMovingProductPitch = fSubSeqRowMovingProductPitch <= 0 ? 0 : fSubSeqRowMovingProductPitch - 1;
                    fSubSeqRowMovingProductPitch *= fSubSeqRowMovingProductPitch<=0?0:RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>();

                    fSubSeqColMovingProductPitch = FA.RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                    fSubSeqColMovingProductPitch = fSubSeqColMovingProductPitch <= 0 ? 0 : fSubSeqColMovingProductPitch - 1;
                    fSubSeqColMovingProductPitch *= RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>();

                    fSubSeqRowMovingPitchPerOnce = RCP_Modify.Inspection_RowCount.GetValue<int>();
                    fSubSeqRowMovingPitchPerOnce *= RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>();
                    fSubSeqRowMovingPitch = fSubSeqRowMovingPitchPerOnce * iSubSeqRowMovingExeCount;

                    fSubSeqColMovingPitchPerOnce = RCP_Modify.Inspection_ColCount.GetValue<int>();
                    fSubSeqColMovingPitchPerOnce *= RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>();                  
                    fSubSeqColMovingPitch= fSubSeqColMovingPitchPerOnce* iSubSeqColMovingExeCount;
                    switch (RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<FA.eRecipeRowProgressDir>())
                    {
                        case eRecipeRowProgressDir.UP:
                            {
                                fSubSeqTargetPosY += fSubSeqRowMovingPitch;                               
                            }

                            break;
                        case eRecipeRowProgressDir.DOWN:
                            {
                                fSubSeqTargetPosY -= fSubSeqRowMovingPitch;                             
                            }
                   
                           
                            break;
                    }
                   
                    switch (RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<FA.eRecipeColProgressDir>())
                    {
                        case eRecipeColProgressDir.LEFT:
                            {
                                fSubSeqTargetPosX += fSubSeqColMovingPitch;                               
                            }                            
                            break;
                        case eRecipeColProgressDir.RIGHT:
                            {
                                fSubSeqTargetPosX -= fSubSeqColMovingPitch;                              
                            }
                           
                            break;
                    }
                    if (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>() == eRecieMultiArrayDir.COLUMN)
                    {
                        if (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>() >= 0)
                        {
                            fSubSeqTargetPosY += iSubSeqMultiArrayExeCount * fSubSeqRowMovingProductPitch;
                        }
                        else
                        {
                            fSubSeqTargetPosY -= iSubSeqMultiArrayExeCount * fSubSeqRowMovingProductPitch;
                        }
                        fSubSeqTargetPosY += iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                    }
                    else
                    {
                        if (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<FA.eRecieMultiArrayDir>() == eRecieMultiArrayDir.ROW)
                        {
                            if (RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>() >= 0)
                            {
                                fSubSeqTargetPosX += iSubSeqMultiArrayExeCount * fSubSeqColMovingProductPitch;
                            }
                            else
                            {
                                fSubSeqTargetPosX -= iSubSeqMultiArrayExeCount * fSubSeqColMovingProductPitch;
                            }                            
                            fSubSeqTargetPosX += iSubSeqMultiArrayExeCount * RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>();
                        }
                    }
                    iForLoopCount = bSubSeqMovingLastPerOneLine == false ?
                              iSubSeqInspRowCount :
                              iSubSeqInspRowLastCount <= 0 ? iSubSeqInspRowCount : iSubSeqInspRowLastCount;
                    SW.WriteLine($"{fSubSeqTargetPosX},{fSubSeqTargetPosY},{iSubSeqRowMovingExeCount},{iSubSeqColMovingExeCount},{iSubSeqProductDoneCount},{iSubSeqMultiArrayExeCount},{bSubSeqMovingLastPerOneLine},{iForLoopCount}");
                    #region [ End Check ] 
                    iInspRowCount = RCP_Modify.Inspection_RowCount.GetValue<int>();
                    iInspColCount = RCP_Modify.Inspection_ColCount.GetValue<int>();
                    RowCountMax = RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>();
                    ColCountMax = RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>();
                    iSubSeqProductDoneRowProgressCount += iInspRowCount;
                    iSubSeqProductDoneColProgressCount += iInspColCount;
                    iSubSeqProductDoneCount += (iInspRowCount * iInspColCount);
                    if (bSusbSeqRowReverseDirEnable == false)
                    {
                        if (iSubSeqRowMovingExeCount + 1 <= iSubSeqRowMovingMax)
                            iSubSeqRowMovingExeCount++;                     
                    }
                    else
                    {
                        if (iSubSeqRowMovingExeCount - 1 >= 0)
                            iSubSeqRowMovingExeCount--;
                    }
                    if (iSubSeqRowMovingExeCount == iSubSeqRowMovingMax)
                    {
                        bSubSeqMovingLastPerOneLine = true;
                    }
                    else
                    {
                        bSubSeqMovingLastPerOneLine = false;
                    }
                    if (iSubSeqProductDoneRowProgressCount >= RowCountMax)
                    {
                        iSubSeqProductDoneRowProgressCount = 0;
                        iSubSeqColMovingExeCount++;
                        bSusbSeqRowReverseDirEnable = !bSusbSeqRowReverseDirEnable;
                    }
                    #endregion [ End Check ]  
                    if (iSubSeqProductDoneCount < iSubSeqProductDoneMaxCount)
                    {

                    }
                    else
                    {
                        if ((iSubSeqMultiArrayExeCount + 1) < iSubSeqMultiArrayMaxCount)
                        {
                            iSubSeqMultiArrayExeCount++;
                            iSubSeqRowMovingExeCount = 0;
                            iSubSeqColMovingExeCount = 0;
                            iSubSeqProductDoneCount = 0;
                            iSubSeqProductDoneRowProgressCount = 0;
                            iSubSeqProductDoneColProgressCount = 0;
                            bSubSeqMovingLastPerOneLine = false;
                            bSusbSeqRowReverseDirEnable = false;
                        }
                        else
                        {
                            break;
                        }
                    }


                } while (iSubSeqProductDoneCount < iSubSeqProductDoneMaxCount);


                if(SW!=null)
                {
                    SW.Close();
                    SW.Dispose();
                 }
                #endregion [ simul Multi Array coordinate ]
            }
            catch
            {

                if (SW != null)
                {
                    SW.Close();
                    SW.Dispose();
                }
            }
        }

        private void listBox_Recipe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }//End of Class
}//End of Name Space
