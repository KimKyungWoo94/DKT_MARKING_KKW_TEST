using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
		public partial class FrmTabRecipePanelCommon : Form
		{
				DataGridView m_DGV_PARAM;
				GUI.UserControls.ExpandDataGridView m_DGV_GROUP;

				public FrmTabRecipePanelCommon()
				{
						InitializeComponent();
						m_DGV_PARAM = new DataGridView();
						m_DGV_GROUP = new GUI.UserControls.ExpandDataGridView();
						m_DGV_PARAM.Dock = DockStyle.Fill;
						m_DGV_GROUP.Dock = DockStyle.Fill;
						m_DGV_PARAM.DoubleBuffered(true);
						m_DGV_GROUP.DoubleBuffered(true);
						Panel_DataGridview.Controls.Add(m_DGV_PARAM);
						Panel_DataGridview.Controls.Add(m_DGV_GROUP);
				}

				private void Form_VisibleChanged(object sender, EventArgs e)
				{
						if (Visible)
						{
								FA.MGR.RecipeMgr.TreeView_Init(treeView_Menu,
																							 FA.DEF.eRecipeCategory.COMMON.ToString(),
																							 MF.RCP_Modify.GetSubCatagoryStringList(FA.DEF.eRecipeCategory.COMMON));
						}
				}

				private void treeView_Menu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
				{
						if (e.Node.Nodes.Count == 0)
						{
								lb_SelSubCategory.Text = e.Node.Text;
								if (MF.RCP_Modify.IsExistGroupitem(FA.DEF.eRecipeCategory.COMMON, e.Node.Text))
								{
										if (m_DGV_PARAM.Visible == true)
												m_DGV_PARAM.Visible = false;

										m_DGV_GROUP.Visible = true;
								}
								else
								{
										if (m_DGV_GROUP.Visible == true)
												m_DGV_GROUP.Visible = false;

										FA.MGR.RecipeMgr.InitDGV_DefaultParam(m_DGV_PARAM, FA.DEF.eRecipeCategory.COMMON, e.Node.Text);
										m_DGV_PARAM.Visible = true;
								}

						}
				}



				private void Form_Load(object sender, EventArgs e)
				{
						//FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas);
						//FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);
				}
				public void OnRecipeChangeEvent()
				{
						if (this.Visible)
						{
								m_DGV_PARAM.Invalidate();
								m_DGV_GROUP.Invalidate();
						}
				}
		}
}
