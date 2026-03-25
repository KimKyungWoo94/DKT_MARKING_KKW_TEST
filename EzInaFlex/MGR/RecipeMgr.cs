using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EzIna
{
    public sealed class RecipeManager : SingleTone<RecipeManager> ,INotifyPropertyChanged
    {


        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
			  ComboBox m_enumDGVComboBox;
				MF.RecipeItemBase m_enumDGVComboboxCell;
        public enum DGV_CELL_TYPE
        {
            NONE,
            COMBOBOX,
            TOGGLE,
            BUTTON,
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
            {
                oldValue = newValue;

                FirePropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        private void FirePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string m_strSelectedModel;

        public string SelectedModel
        {
            get
            {
                return m_strSelectedModel;
            }
            set
            {
                CheckPropertyChanged<string>("SelectedModel", ref m_strSelectedModel, value);
            }
        }
        public string SelectedModelPath { get; set; }
        protected override void OnCreateInstance()
        {

            SelectedModel = "";
            SelectedModelPath = "";
            string strLastReicpe="";
            IniFile Ini = new IniFile(FA.FILE.RecipeIni);
            strLastReicpe=Ini.Read("SELECT", "NAME", "");
            RecipeOpen(strLastReicpe);					
						InitializeCombo(out m_enumDGVComboBox);
            base.OnCreateInstance();
        }
        ~RecipeManager()
        {
            IniFile Ini = new IniFile(FA.FILE.RecipeIni);
            Ini.Write("SELECT", "NAME", SelectedModel);
        }
        public bool CheckRecipeDuplicate(string a_Value)
        {
            if(string.IsNullOrEmpty(a_Value)==false)
            {
                string[] RecipeList = System.IO.Directory.GetDirectories(FA.DIR.RCP);
                string strSelRecipePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, a_Value.ToUpper()));
                if (Array.Exists(RecipeList, x => x == strSelRecipePath))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        public bool RecipeOpen(string a_strRecipeModel)
        {
            try
            {
                string [] RecipeList=System.IO.Directory.GetDirectories(FA.DIR.RCP);
                string strSelRecipePath=System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, a_strRecipeModel.ToUpper()));                
                if(Array.Exists(RecipeList,x=>x==strSelRecipePath))
                {                    
                    RecipeFileOpen(string.Format("{0}\\Recipe.rcp",strSelRecipePath)); 
                    IniFile Ini = new IniFile(FA.FILE.RecipeIni);
                    SelectedModel = a_strRecipeModel.ToUpper();
                    SelectedModelPath = strSelRecipePath;
                    Ini.Write("SELECT", "NAME", SelectedModel);
                    Ini = null;
                    return true;
                }                 
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString() + "\n" + ex.Message.ToString());
                return false;
            }
            return false;
        }
        
        public bool RecipeSave(string a_strRecipeModel)
        {
            try
            {
                string strSelRecipePath=System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, SelectedModel));
                RecipeFileSave(string.Format("{0}\\Recipe.rcp",strSelRecipePath));                
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
                return false;
            }
            return false;
        }
        public bool RecipeRename(string a_strSource, string a_strChnagePjtName)
        {
            try
            {
                string strSourcePath = null, strDestPath = null;
                strSourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, a_strSource));
                strDestPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, a_strChnagePjtName));
                if (System.IO.Directory.Exists(strSourcePath))
                {
                    System.IO.Directory.Move(strSourcePath, strDestPath);
                    //Directory.Delete(strSourcePath);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
        public bool RecipeAdd(string a_strNewProjectName)
        {
            try
            {
                string strSourcePath = null, strTargetPath = null;
                strTargetPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, a_strNewProjectName));
                System.IO.Directory.CreateDirectory(strTargetPath);
                if (string.IsNullOrEmpty(SelectedModel)==false)
                {
                    strSourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(FA.DIR.RCP, SelectedModel));
                    string strSourceFile = null, strDestFile;
                    if (System.IO.Directory.Exists(strSourcePath))
                    {
                        string[] files = System.IO.Directory.GetFiles(strSourcePath);
                        foreach (string s in files)
                        {
                            strSourceFile = System.IO.Path.GetFileName(s);
                            strDestFile = System.IO.Path.Combine(strTargetPath, strSourceFile);
                            System.IO.File.Copy(s, strDestFile, true);
                        }
                        return true;
                    }
                    return false;
                }
                else
                {
                    RecipeFileSave(string.Format("{0}\\Recipe.rcp",strTargetPath));
                    return true;
                }                                                                                                             
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
        public bool RecipeDelete(string a_strDeleteProjectName)
        {
            try
            {
                if (SelectedModel.ToUpper().Equals(a_strDeleteProjectName.ToUpper()))
                    return false;

                string strTarget = System.IO.Path.Combine(FA.DIR.RCP, a_strDeleteProjectName);
                if (System.IO.Directory.Exists(strTarget))
                {
                    string[] files = System.IO.Directory.GetFiles(strTarget);
                    foreach (string s in files)
                    {
                        string fileName = System.IO.Path.GetFileName(s);
                        string deletefile = System.IO.Path.Combine(strTarget, fileName);
                        System.IO.File.Delete(deletefile);

                    }
                    // 					DirectoryInfo dir = new DirectoryInfo(strTarget);
                    // 					FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    // 					foreach (FileInfo file in files)
                    // 					{
                    // 						file.Attributes = FileAttributes.Normal;
                    // 					}



                    System.IO.Directory.Delete(strTarget);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
    
        private void RecipeFileOpen(string a_strPath)
        {
            MF.RCP_Modify.LoadFromFile(a_strPath, false);
						SyncMatchConfig();
        }
        private void RecipeFileSave(string a_strPath)
        {
            MF.RCP_Modify.SaveToFile(a_strPath, false);
						SyncMatchConfig();
        }

				/// <summary>
				/// 임시 
				/// </summary>
				private void SyncMatchConfig()
				{
						EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
						MatchConfig.m_fMinScale					= FA.RCP_Modify.Matcher_Minimum_Scale.GetValue<double>()*100;
						MatchConfig.m_fMaxScale					= FA.RCP_Modify.Matcher_Maximum_Scale.GetValue<double>()*100;
						MatchConfig.m_fScore						= FA.RCP_Modify.Matcher_Match_Score.GetValue<double>()*100;
						MatchConfig.m_fAngle						= FA.RCP_Modify.Matcher_Match_Angle.GetValue<double>();
						MatchConfig.m_fMaxPosition			= FA.RCP_Modify.Matcher_Match_MaxCount.GetValue<int>();						
						MatchConfig.m_iCorrelationMode	= FA.RCP_Modify.Matcher_Match_CorrelationMode.GetValue<int>();
						MatchConfig.m_iMatchContrastMode = FA.RCP_Modify.Matcher_Match_ContrastMode.GetValue<int>();
						FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_MatchConfig = MatchConfig;
				}
				public string GetPreSaveConfirmMSG()
        {
            return MF.RCP_Modify.GetPreSaveConfirmMSG();
        }
        public void listBox_Recipe_Init(ListBox a_pList)
        {
            try
            {
                a_pList.BeginUpdate();
                a_pList.Items.Clear();
                string[] directories = System.IO.Directory.GetDirectories(FA.DIR.RCP);

                for (int i = 0; i < directories.Length; i++)
                {
                    a_pList.Items.Add(directories[i].Replace(FA.DIR.RCP, ""));
                }

                if (a_pList.Items.Count > 1)
                {
                    a_pList.SelectedIndex = 0;
                }

                for (int i = 0; i < a_pList.Items.Count; i++)
                {
                    if (FA.MGR.RecipeMgr.SelectedModel.ToUpper().Equals(a_pList.Items[i].ToString().ToUpper()))
                        a_pList.SelectedIndex = i;
                }
                a_pList.EndUpdate();
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }
        }

        #region TreeView Ref
        public void TreeView_Init(TreeView a_pTreeView_Modules,
                                  string a_strTitle,
                                  string [] a_strSubCategory
                                  )
        {
            a_pTreeView_Modules.BeginUpdate();
            a_pTreeView_Modules.Nodes.Clear();
           
            a_pTreeView_Modules.Font = new Font("Century Gothic", 15F, FontStyle.Regular,GraphicsUnit.Pixel);

            //Main Category - Sub Category
            //for example Vision - Matcher
            //			  Motion - Stage, Head, 
            TreeNode pCategory = new TreeNode(a_strTitle);
            pCategory.Name=a_strTitle;
            pCategory.NodeFont=new Font("Century Gothic", 15F, FontStyle.Regular,GraphicsUnit.Pixel);
           
            if(a_strSubCategory!=null &&a_strSubCategory.Length>0)
            {                                      
                foreach (string item in a_strSubCategory)
                {
                    pCategory.Nodes.Add(
                        
                        new TreeNode(item)
                        {
                            Name=item,
                            NodeFont=new Font("Century Gothic", 12F, FontStyle.Regular,GraphicsUnit.Pixel)
                        }                                                
                        );                                           
                }               
            }          
            a_pTreeView_Modules.Nodes.Add(pCategory);
            a_pTreeView_Modules.Nodes[pCategory.Name].Expand();                       
            a_pTreeView_Modules.EndUpdate();
        }
        #endregion TreeView Ref
        #region DGV REF

        public bool InitDGV_DefaultParam(DataGridView a_DatagirdView,FA.DEF.eRecipeCategory a_MainCategory,string a_strSubCategory)
        {
            InitializeDataGridVeiwStyle(a_DatagirdView);
            if(MF.RCP_Modify.IsExistCategory(a_MainCategory,a_strSubCategory)==true)
            {
                List<MF.RecipeItemBase> pItemList=MF.RCP_Modify.GetParameterList(a_MainCategory,a_strSubCategory);
                if(pItemList!=null && pItemList.Count>0)
                {
                   var pDPValueList=from item in pItemList
                                    where item.GetType()==typeof(MF.RecipeItem_DPValue)
                                    select item ;
                                    
                   if(pDPValueList!=null && pDPValueList.Count<MF.RecipeItemBase>()>0)
                   {
                        a_DatagirdView.Columns.AddRange(
                            CreateDataGridViewTextColumn("Key", "iKey", (int)(60)),
                            CreateDataGridViewTextColumn("Name", "strCaption", (int)((a_DatagirdView.Width - 60-20-60) * 0.4)),
                            CreateDataGridViewTextColumn("Value", "Value", (int)((a_DatagirdView.Width - 60-20-60) * 0.2)),
														CreateDataGridViewTextColumn("Convert", "DisplaySettingValue", (int)((a_DatagirdView.Width - 60-20-60) * 0.2)),
                            CreateDataGridViewTextColumn("Setting", "SettingValue", (int)((a_DatagirdView.Width - 60-20-60) * 0.2)),
                            CreateDataGridViewButtonColum("SetBtn","","Set",(int)60)
                            );
                      
                   }                                           
                   else
                   {
                       a_DatagirdView.Columns.AddRange(
                             CreateDataGridViewTextColumn("Key", "iKey", (int)(60)),
                             CreateDataGridViewTextColumn("Name", "strCaption", (int)((a_DatagirdView.Width - 60-20-60) * 0.4)),
                             CreateDataGridViewTextColumn("Value", "Value", (int)((a_DatagirdView.Width - 60-20-60) * 0.3)),
                             CreateDataGridViewTextColumn("Setting", "SettingValue", (int)((a_DatagirdView.Width - 60-20-60) * 0.3)),
														 CreateDataGridViewButtonColum("SetBtn","","Set",(int)60)
                             );
                       

                    }
                    //a_DatagirdView.CellClick
                    a_DatagirdView.DataBindingComplete -= DGV_PARAM_DataBindingComplate;
                    a_DatagirdView.DataBindingComplete += DGV_PARAM_DataBindingComplate;
                    a_DatagirdView.CellClick-=DGV_DefaultCellClick;
                    a_DatagirdView.CellClick+=DGV_DefaultCellClick;                    
                    a_DatagirdView.CurrentCellDirtyStateChanged-=CurrentCellDirtyStateChange;
                    a_DatagirdView.CurrentCellDirtyStateChanged+=CurrentCellDirtyStateChange;
										a_DatagirdView.DataError-=DGVDataErrorEvent;
										a_DatagirdView.DataError+=DGVDataErrorEvent;
                    a_DatagirdView.DataSource = new BindingList<MF.RecipeItemBase>(pItemList);
                    return true;
                }
                                                
            }
            return false;
        }
        private void DGV_PARAM_DataBindingComplate(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView pControl = sender as DataGridView;
						
            if (pControl != null)
            {
                if (pControl.DataSource.GetType() == typeof(BindingList<MF.RecipeItemBase>))
                {
                    BindingList<MF.RecipeItemBase> pDataBindingSource = pControl.DataSource as BindingList<MF.RecipeItemBase>;
                   
                    for (int i = 0; i < pControl.Rows.Count; i++)
                    {
                       /* if (pDataBindingSource[i].ValueType.BaseType == typeof(System.Enum))
                        {
                            
                            DataGridViewComboBoxCell pDGVItem=CreateDataGridCell(DGV_CELL_TYPE.COMBOBOX) as DataGridViewComboBoxCell;                   
														pDGVItem.DataSource=Enum.GetValues(pDataBindingSource[i].ValueType).Cast<object>().ToList();
														if (!string.IsNullOrEmpty(pControl["Setting", i].OwningColumn.DataPropertyName)
                               &&!string.IsNullOrWhiteSpace(pControl["Setting", i].OwningColumn.DataPropertyName))
                            {
																pDGVItem.ValueType = pDataBindingSource[i].ValueType;
																pDGVItem.Value = pDataBindingSource[i].SettingValue.ToString();
																
														}     
														pDGVItem.ReadOnly=false;                       
                            pControl["Setting", i] = pDGVItem;
                        }
                        else
                        {*/
                            switch (Type.GetTypeCode(pDataBindingSource[i].ValueType))
                            {
                                case TypeCode.Boolean:
                                    {
                                        pControl["Value", i] = CreateDataGridCell(DGV_CELL_TYPE.TOGGLE);
                                        pControl["Setting", i] = CreateDataGridCell(DGV_CELL_TYPE.TOGGLE);
                                    }
                                    break;                                    
                            }
                        //}
                        pControl["Value", i].Style.Format = pDataBindingSource[i].GetValueFormatString(pDataBindingSource[i].iFormatNumberOfZero, true);
                        pControl["Setting", i].Style.Format = pDataBindingSource[i].GetValueFormatString(pDataBindingSource[i].iFormatNumberOfZero, true);
                        if(pDataBindingSource[i].GetType()==typeof(MF.RecipeItem_DPValue))
                        {
                            pControl["Convert", i].Style.Format = (pDataBindingSource[i] as MF.RecipeItem_DPValue).GetDisplayValueFormatString(3,true); 
                        }
												(pControl["SetBtn", i] as EzIna.DataGridViewDisableButtonCell).Enabled=pDataBindingSource[i].SetButtonFunc!=null;
                        pControl.Rows[i].DefaultCellStyle.BackColor = i % 2 == 0 ? SystemColors.Control : Color.White;
                    }
                    //pControl.AutoResizeColumns();
                }
                else
                {

                }

            }
        }
        #region DGV Style
        private void InitializeDataGridVeiwStyle(DataGridView a_DatagirdView)
        {

            a_DatagirdView.DataSource=null; 
            if (a_DatagirdView.RowCount > 0 || a_DatagirdView.ColumnCount > 0)
            {
                a_DatagirdView.Columns.Clear();
                a_DatagirdView.Rows.Clear();
            }                    
            a_DatagirdView.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ForeColor = Color.Black;
            a_DatagirdView.ReadOnly = false;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            a_DatagirdView.ColumnHeadersHeight = 25;
            a_DatagirdView.RowTemplate.Height = 25;

            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.BackgroundColor=Color.White;
            a_DatagirdView.ForeColor=Color.Black;
            a_DatagirdView.EnableHeadersVisualStyles = false;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ScrollBars = ScrollBars.Vertical;
            a_DatagirdView.ClearSelection();
        }
        private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt, int a_Width)
        {
            DataGridViewTextBoxColumn pRet = new DataGridViewTextBoxColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pRet.DefaultCellStyle.ForeColor = Color.Black;
            pRet.DefaultCellStyle.BackColor = Color.White;
            pRet.DefaultCellStyle.SelectionBackColor = m_DGV_SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = m_DGV_SelectionForeColor;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private EzIna.DataGridViewDisableButtonColumn CreateDataGridViewButtonColum(string a_strHeaderTxt, string a_strBindingPropTxt, string a_ButtonText, int a_Width = 100)
        {
            DataGridViewDisableButtonColumn pRet = new DataGridViewDisableButtonColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.Text = a_ButtonText;
            pRet.UseColumnTextForButtonValue = true;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pRet.DefaultCellStyle.ForeColor = Color.Black;
            pRet.DefaultCellStyle.BackColor = Color.White;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        public DataGridViewCell CreateDataGridCell(DGV_CELL_TYPE a_CellType)
        {
            DataGridViewCell pRet = null;
            switch (a_CellType)
            {
                case DGV_CELL_TYPE.COMBOBOX:
                    {
                        DataGridViewComboBoxCell pTemp = new DataGridViewComboBoxCell();
                        pTemp.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        pRet = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.TOGGLE:
                    {
                        GUI.UserControls.DGVToggleButtonCell pTemp = new GUI.UserControls.DGVToggleButtonCell();
                        pTemp.ButtonStyle = GUI.UserControls.DGVToggleButtonCell.ToggleSwitchStyle.IOS5;
                        pRet = pTemp;
                    }
                    break;
                case DGV_CELL_TYPE.BUTTON:
                    {
                        DataGridViewButtonCell pTemp = new DataGridViewButtonCell();
                        pRet = pTemp;
                    }
                    break;
                default:
                    {
                        DataGridViewTextBoxCell pTemp = new DataGridViewTextBoxCell();
                        pRet = pTemp;
                    }
                    break;
            }
            return pRet;
        }
        #endregion DGV Style
        public void DGV_DefaultCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            DataGridView pControl = sender as DataGridView;
            if(pControl!=null)
            {
                BindingList<MF.RecipeItemBase> pDataBindingSource = pControl.DataSource as BindingList<MF.RecipeItemBase>;
                if(pDataBindingSource!=null && pDataBindingSource.Count>0)
                {
                    if (pControl.Columns["Setting"].Index == e.ColumnIndex)
                    {
                        MF.RecipeItemBase pItem=pDataBindingSource[e.RowIndex];
                        if(pItem!=null)
                        {
                            Type pValueType=pItem.ValueType;
                            TypeCode pTypeCode=Type.GetTypeCode(pValueType);                            
                            if(pValueType.BaseType==typeof(System.Enum))
                            {
																m_enumDGVComboBox.Items.Clear();
															 foreach(var item in Enum.GetValues(pItem.ValueType))
															 {
																		m_enumDGVComboBox.Items.Add(item);
															 }
															 m_enumDGVComboBox.Parent=pControl;
															 Rectangle temp = pControl.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
															 m_enumDGVComboBox.Location = new Point(temp.X, temp.Y);
															 m_enumDGVComboBox.Width = temp.Width;
															 m_enumDGVComboBox.Height = temp.Height;
															 m_enumDGVComboBox.SelectedItem = pItem.SettingValue;
															 m_enumDGVComboBox.DroppedDown = true;
														   m_enumDGVComboboxCell=pItem;
															 m_enumDGVComboBox.BringToFront();
															 m_enumDGVComboBox.Show();
														}
                            else
                            {
                                switch (pTypeCode)
                                {
                                    case TypeCode.Boolean:
                                        {

                                            if (MessageBox.Show(string.Format("Would you like Change Value?\r\nName : {0}\r\nValue : {1} -> {2}",
                                                                pItem.strCaption,
                                                                (bool)pItem.SettingValue,
                                                                (bool)pItem.SettingValue==true ? false : true
                                                                ), "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                            {
                                                pItem.SettingValue=(bool)pItem.SettingValue==true ? false : true;
                                            }
                                        }
                                        break;
                                    case TypeCode.Int16:
                                    case TypeCode.UInt16:
                                    case TypeCode.Int32:
                                    case TypeCode.UInt32:
                                    case TypeCode.Int64:
                                    case TypeCode.UInt64:
                                    case TypeCode.Single:
                                    case TypeCode.Double:
                                        {
                                            GUI.UserControls.NumberKeypad DlgNumberPad=new GUI.UserControls.NumberKeypad();
                                            double fCurrent=0.0;
																						double fMin=0.0;
																						double fMax=0.0;

                                            pItem.GetSettingValue<double>(out fCurrent);
											fMin=pItem.fMin;
											fMax=pItem.fMax;
											if(pItem.eValueUnit==FA.DEF.eUnit.percent)
											{
												fCurrent*=100.0;
												fMin *= 100.0;
												fMax *= 100.0;
											}
                                            if(DlgNumberPad.ShowDialog(fMin, fMax, fCurrent)==DialogResult.OK)
                                            {
												if(pItem.eValueUnit==FA.DEF.eUnit.percent)
												{
														pItem.SettingValue=DlgNumberPad.Result/100.0;
												}
												else
												{
														pItem.SettingValue=DlgNumberPad.Result;
												}
																								
                                            }
                                        }
                                        break;
                                    case TypeCode.String:
                                        {

                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }                            
                        }
                    }
										else if(pControl.Columns["SetBtn"].Index == e.ColumnIndex)
										{
												if((pControl["SetBtn",e.RowIndex] as DataGridViewDisableButtonCell).Enabled==true)
												{
														MF.RecipeItemBase pItem=pDataBindingSource[e.RowIndex];
														pItem.SettingValue=pItem.SetButtonFunc();
												}
										}
                }                
            }
        }
				private void InitializeCombo(out ComboBox a_Control)
				{
						a_Control = new ComboBox();
						a_Control.DropDownStyle = ComboBoxStyle.DropDownList;
						a_Control.BringToFront();
						
						a_Control.Hide();
						a_Control.MouseLeave += Combo_MouseLeave;
						a_Control.DropDownClosed += Combo_DropDownClosed;
				}
				private void Combo_MouseLeave(object sender, EventArgs e)
				{

						ComboBox pControl = sender as ComboBox;
						if (pControl != null)
								pControl.Hide();
				}
				private void Combo_DropDownClosed(object sender, EventArgs e)
				{
						ComboBox pControl = sender as ComboBox;
						if (pControl != null)
								pControl.Hide();
						
						if(m_enumDGVComboboxCell!=null)
						{
								if(pControl.SelectedItem!=null)
								m_enumDGVComboboxCell.SettingValue=pControl.SelectedItem;
								m_enumDGVComboboxCell=null;
						}
					
				}
				public void DGVDataErrorEvent(object sender, DataGridViewDataErrorEventArgs e)
			 {
						DataGridView pControl = sender as DataGridView;
						if (pControl[e.ColumnIndex, e.RowIndex].ValueType.BaseType == typeof(System.Enum))
						{
									e.ThrowException=false;								
						}
				}
       public void CurrentCellDirtyStateChange(object sender, EventArgs e)
       {
            DataGridView pControl = sender as DataGridView;
            if (pControl != null)
            {
                 //pControl.Columns["Setting"].ReadOnly=true;
            }
        }            
        #endregion DGV REF
    }
}
