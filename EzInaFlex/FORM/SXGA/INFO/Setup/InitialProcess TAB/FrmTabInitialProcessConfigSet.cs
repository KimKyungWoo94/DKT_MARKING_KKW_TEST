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
	public partial class FrmTabInitialProcessConfigSet : Form
	{
		FA.DEF.eRcpCategory		m_eSelectedCategory = FA.DEF.eRcpCategory.InitialProcess;
		FA.DEF.eRcpSubCategory	m_eSelectedSubCategory;
		public FrmTabInitialProcessConfigSet()
		{
			InitializeComponent();

			dataGridView_Datas		.DoubleBuffered(true);
			dataGridView_Options	.DoubleBuffered(true);
		}

		private void FrmTabInitialProcessConfigSet_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible)
			{
				FA.MGR.ProjectMgr.TreeView_Init(treeView_Menu, imageList_For_TreeMenu, m_eSelectedCategory);
			}
		}

		private void treeView_Menu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			string strPath = e.Node.FullPath;
			string[] strPaths = strPath.Split('\\');

			//For example, Category - Vision - Mather
			if (strPaths.Length > 2)
			{
				//dataGridView_Datas.Columns.Clear();
				dataGridView_Datas.Rows.Clear();

				//dataGridView_Options.Columns.Clear();
				dataGridView_Options.Rows.Clear();
				m_eSelectedCategory = strPaths[1].ToEnum<FA.DEF.eRcpCategory>();
				m_eSelectedSubCategory = strPaths[2].ToEnum<FA.DEF.eRcpSubCategory>();

				FA.MGR.ProjectMgr.DataGridView_ChangeToModules(dataGridView_Datas, dataGridView_Options, m_eSelectedCategory, m_eSelectedSubCategory, imageList_For_TreeMenu);
				Trace.WriteLine("Category - " + strPaths[1] + "SubCategory -" + strPaths[2]);
			}
		}

		public void Read_From()
		{
			MF.RCP.Read_From(m_eSelectedCategory	, m_eSelectedSubCategory, dataGridView_Datas);
			MF.OPT.Read_Form(m_eSelectedSubCategory	, dataGridView_Options					);
		}

		private void FrmTabInitialProcessConfigSet_Load(object sender, EventArgs e)
		{
			FA.MGR.ProjectMgr.DataGridView_Init_For_Recipe(dataGridView_Datas, true);
			FA.MGR.ProjectMgr.DataGridView_Init_For_Option(dataGridView_Options);
		}

		private void btn_Open_Click(object sender, EventArgs e)
		{

			if (MsgBox.Confirm("Would you like to open the file??"))
			{
				FA.MGR.ProjectMgr.RecipeOpen_InitProc();
				FA.MGR.ProjectMgr.OptionOpen_InitProc();
				FA.MGR.ProjectMgr.WriteDatas_To_Vision();
			}
		}

		private void btn_Save_Click(object sender, EventArgs e)
		{
			if (MsgBox.Confirm("Would you like to save the file??"))
			{
				Read_From();
				FA.MGR.ProjectMgr.RecipeSave_InitProc();
				FA.MGR.ProjectMgr.OptionSave_InitProc();
				FA.MGR.ProjectMgr.WriteDatas_To_Vision();
			}
		}
	}
}
