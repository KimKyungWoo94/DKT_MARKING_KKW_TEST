using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
	public partial class ucTabControlX : UserControl
	{
		public ucTabControlX()
		{
			InitializeComponent();
		}

		int selected_index = -1;
		public List<ButtonX> buttonlist = new List<ButtonX> { };
		public List<Form> tabPanelCtrlList = new List<Form> { };

		private Size	tab_size					= new Size(110, 25);
		private Color	sel_tab_forecolor			= Color.White;
		private Color	unsel_tab_forecolor			= Color.White;
		private Color	sel_tab_backcolor			= Color.FromArgb(20, 120, 240);
		private Color	un_sel_tab_backcolor		= Color.FromArgb(40, 40, 40);
		private Color	tab_mouseHvrColor			= Color.FromArgb(20, 120, 240);
		private Color	tab_mouseClkColor			= Color.FromArgb(20, 80, 200);
		private int		txt_x_loc					= 10;
		private int		txt_y_loc					= 5;
		private Color	ribbon_Color				= Color.FromArgb(20, 120, 240);
		private Color	tabCtrlPanel_backcolor		= Color.FromArgb(40, 40, 40);
		private Color	tabCtrlButPanel_backcolor	= Color.FromArgb(30, 30, 30);

		[Browsable(false)]
		public List<ButtonX> TabsList
		{
			get { return buttonlist; }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("탭의 사이즈 설정.")]
		public Size TabSize
		{
			get { return tab_size; }
			set { tab_size = value; setHeight(); Invalidate(); }
		}
        [Category("UserControl")]
        [Browsable(true)]
        [Description("TabPage Size")]
        public Size TabPageSize
        {
            get { return TabPanel.Size; }            
        }
        [Category("UserControl")]
 		[Browsable(true)]
		[Description("선택된 탭의 글씨 색상 설정.")]
		public Color SelTabForeColor
		{
			get { return sel_tab_forecolor; }
			set { sel_tab_forecolor = value; Invalidate(); }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("선택 되지 않은 탭의 글씨 색상 설정.")]
		public Color UnSelTabForeColor
		{
			get { return unsel_tab_forecolor; }
			set { unsel_tab_forecolor = value; Invalidate(); }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("선택된 탭의 바탕 색상 설정.")]
		public Color SelTabBackColor
		{
			get { return sel_tab_backcolor; }
			set { sel_tab_backcolor = value; Invalidate(); }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("선택되지 않은 탭의 바탕 색상 설정.")]
		public Color UnSelTabBackColor
		{
			get { return un_sel_tab_backcolor; }
			set { un_sel_tab_backcolor = value; Invalidate(); }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("마우스 호버 색상 설정.")]
		public Color MouseHrTabColor
		{
			get { return tab_mouseHvrColor; }
			set { tab_mouseHvrColor = value; Invalidate(); }
		}

		[Category("UserControl")]
		[Browsable(true)]
		[Description("마우스 클릭 색상 설정.")]
		public Color MouseClkTabColor
		{
			get { return tab_mouseClkColor; }
			set { tab_mouseClkColor = value; Invalidate(); }
		}
		[Category("UserControl")]
		[Browsable(true)]
		[Description("선택된 탭의 바탕 색상 설정.")]
		public int X_TextLoc
		{
			get { return txt_x_loc; }
			set { txt_x_loc = value; Invalidate(); }
		}
		[Category("UserControl")]
		[Browsable(true)]
		[Description("선택된 탭의 바탕 색상 설정.")]
		public int Y_TextLoc
		{
			get { return txt_y_loc; }
			set { txt_y_loc = value; Invalidate(); }
		}
		[Category("UserControl")]
		[Browsable(true)]
		[Description("리본 바탕 색상 설정.")]
		public Color RibbonColor
		{
			get { return ribbon_Color; }
			set { RibbonPanel.BackColor = value; ribbon_Color = value; Invalidate(); }
		}
		[Category("UserControl")]
		[Browsable(true)]
		[Description("폼 패널 바탕 색상 설정.")]
		public Color CtrlPanelColor
		{
			get { return tabCtrlPanel_backcolor; }
			set { TabPanel.BackColor = value; tabCtrlPanel_backcolor = value; Invalidate(); }
		}
		[Category("UserControl")]
		[Browsable(true)]
		[Description("버튼 패널 바탕 색상 설정.")]
		public Color TabPanelColor
		{
			get { return tabCtrlButPanel_backcolor; }
			set
			{
				BackTopPanel.BackColor = value; TabButtonPanel.BackColor = value;
				tabCtrlButPanel_backcolor = value; Invalidate();
			}
		}

		void setHeight()
		{
			if (!buttonlist.Any())
			{
				BackTopPanel	.Height	= tab_size.Height;
				RibbonPanel		.Height	= 2;
				TabButtonPanel	.Height	= tab_size.Height - RibbonPanel.Height;
				
			}
			else
			{
				BackTopPanel	.Height = buttonlist[0].Height;
				RibbonPanel		.Height = 2;
				TabButtonPanel	.Height = buttonlist[0].Height - RibbonPanel.Height;
				
			}
		}

		public int SelectedTabIndex
		{
			get {  return selected_index; }
            set
            {
                if(value > -1 && value <buttonlist.Count)
                {
                    buttonlist[value].PerformClick();
                }
            }
		}

		public int TabCount()
		{
			return buttonlist.Count;
		}

		public void ChangeTabText(string newtext, int index)
		{
			ButtonX but = buttonlist[index];
			but.Text = newtext;
		}
		void UpdateButtons()
		{
			if (buttonlist.Count > 0)
			{
				for (int i = 0; i < buttonlist.Count; i++)
				{
					if (i == selected_index)
					{
						buttonlist[i].ChangeColorMouseHC	= false;
						buttonlist[i].BXBackColor			= sel_tab_backcolor;
						buttonlist[i].ForeColor				= sel_tab_forecolor;
						buttonlist[i].MouseHoverColor		= sel_tab_backcolor;
						buttonlist[i].MouseClickColor1		= sel_tab_backcolor;
					}
					else
					{
						buttonlist[i].ChangeColorMouseHC	= true;
						buttonlist[i].ForeColor				= unsel_tab_forecolor;
						buttonlist[i].MouseHoverColor		= tab_mouseHvrColor;
						buttonlist[i].BXBackColor			= un_sel_tab_backcolor;
						buttonlist[i].MouseClickColor1		= tab_mouseClkColor;
					}
				}

				// 				for (int i = 0; i < buttonlist.Count; i++)
				// 				{
				// 					if (i == selected_index)
				// 					{
				// 						buttonlist[i].ChangeColorMouseHC = false;
				// 						buttonlist[i].BXBackColor		= Color.FromArgb(20, 120, 240);
				// 						buttonlist[i].ForeColor			= Color.White;
				// 						buttonlist[i].MouseHoverColor	= Color.FromArgb(20, 120, 240);
				// 						buttonlist[i].MouseClickColor1	 = Color.FromArgb(20, 120, 240);
				// 					}
				// 					else
				// 					{
				// 						buttonlist[i].ChangeColorMouseHC = true;
				// 						buttonlist[i].ForeColor = Color.White;
				// 						buttonlist[i].MouseHoverColor = Color.FromArgb(20, 120, 240);
				// 						buttonlist[i].BXBackColor = Color.FromArgb(40, 40, 40);
				// 						buttonlist[i].MouseClickColor1 = Color.FromArgb(20, 80, 200);
				// 					}
				// 				}
			}
		}

		void createAndAddButton(string tabtext, Form frm, Point loc)
		{
			ButtonX bx		= new ButtonX();
			bx.DisplayText	= tabtext;
			bx.Text			= tabtext;
			bx.Size			= tab_size;
			bx.Location		= loc;
			bx.ForeColor			= sel_tab_forecolor;
			bx.BXBackColor			= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
			bx.MouseHoverColor		= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
			bx.MouseClickColor1		= sel_tab_backcolor;//Color.FromArgb(20, 80, 200);
			bx.ChangeColorMouseHC	= false;
			bx.TextLocation_X		= txt_x_loc;
			bx.TextLocation_Y		= txt_y_loc;
		
			//bx.Font = this.Font;
			bx.Click += button_Click;
		
			TabButtonPanel.Controls.Add(bx);
			buttonlist.Add(bx);
			selected_index++;

			tabPanelCtrlList.Add(frm);
			TabPanel.Controls.Clear();
			TabPanel.Controls.Add(frm);

			UpdateButtons();
		}

		void button_Click(object sender, EventArgs e)
		{
			string btext = ((ButtonX)sender).Text;
			int index = 0, i;
			for (i = 0; i < buttonlist.Count; i++)
			{
				if(tabPanelCtrlList[i].Visible)
					tabPanelCtrlList[i].Hide();

				if (buttonlist[i].Text == btext)
				{
					index = i;
				}
			}
			TabPanel.Controls.Clear();
			TabPanel.Controls.Add(tabPanelCtrlList[index]);
			tabPanelCtrlList[index].Show();
			selected_index = ((ButtonX)sender).TabIndex;

			UpdateButtons();
		}

		public void AddTab(string tabtext, Form frm)
		{
			if (!buttonlist.Any())
			{
				createAndAddButton(tabtext, frm, new Point(0, 0));
			}
			else
			{
				createAndAddButton(tabtext, frm,
				new Point(buttonlist[buttonlist.Count - 1].Size.Width +
				buttonlist[buttonlist.Count - 1].Location.X, 0));
			}
		}

// 		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
// 		{
// 			toolStripDropDownButton1.DropDownItems.Clear();
// 			int mergeindex = 0;
// 			for (int i = 0; i < buttonlist.Count; i++)
// 			{
// 				ToolStripMenuItem tbr = new ToolStripMenuItem();
// 				tbr.Text = buttonlist[i].Text;
// 				tbr.MergeIndex = mergeindex;
// 				if (selected_index == i)
// 				{
// 					tbr.Checked = true;
// 				}
// 				tbr.Click += tbr_Click;
// 				toolStripDropDownButton1.DropDownItems.Add(tbr);
// 				mergeindex++;
// 			}
// 		}

		List<string> btstrlist = new List<string> { };
		void BackToFront_SelButton()
		{
			int i = 0;

			TabButtonPanel.Controls.Clear();
			btstrlist.Clear();
			for (i = 0; i < buttonlist.Count; i++)
			{
				btstrlist.Add(buttonlist[i].Text);
			}

			buttonlist.Clear();

			for (int j = 0; j < btstrlist.Count; j++)
			{
				if (j == 0)
				{
					ButtonX bx				= new ButtonX();
					bx.DisplayText			= btstrlist[j];
					bx.Text					= btstrlist[j];
					bx.Size					= tab_size;//new Size(130, 30);
					bx.Location				= new Point(0, 0);
					bx.ForeColor			= sel_tab_forecolor;//Color.White;
					bx.BXBackColor			= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
					bx.MouseHoverColor		= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
					bx.MouseClickColor1		= sel_tab_backcolor;//Color.FromArgb(20, 80, 200);
					bx.ChangeColorMouseHC	= false;
					bx.TextLocation_X		= txt_x_loc;
					bx.TextLocation_Y		= txt_y_loc;
					bx.Font					= this.Font;
					bx.Click				+= button_Click;
					TabButtonPanel.Controls.Add(bx);
					buttonlist.Add(bx);
					selected_index++;
				}
				else if (j > 0)
				{
					ButtonX bx				= new ButtonX();
					bx.DisplayText			= btstrlist[j];
					bx.Text					= btstrlist[j];
					bx.Size					= tab_size;//new Size(130, 30);
					bx.Location				= new Point(0, 0);
					bx.ForeColor			= sel_tab_forecolor;//Color.White;
					bx.BXBackColor			= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
					bx.MouseHoverColor		= sel_tab_backcolor;//Color.FromArgb(20, 120, 240);
					bx.MouseClickColor1		= sel_tab_backcolor;//Color.FromArgb(20, 80, 200);
					bx.ChangeColorMouseHC	= false;
					bx.TextLocation_X		= txt_x_loc;
					bx.TextLocation_Y		= txt_y_loc;
					bx.Font					= this.Font;
					bx.Click				+= button_Click;
					bx.Location				= new Point(buttonlist[j - 1].Size.Width + buttonlist[j - 1].Location.X, 0);
					TabButtonPanel.Controls.Add(bx);
					buttonlist.Add(bx);
					selected_index++;
				}
			}
			TabPanel.Controls.Clear();
		}

		void tbr_Click(object sender, EventArgs e)
		{
			int i;
			for (int k = 0; k < ((ToolStripMenuItem)sender).MergeIndex; k++)
			{
				int j = 0;
				for (i = ((ToolStripMenuItem)sender).MergeIndex; i >= 0; i--)
				{
					ButtonX but = buttonlist[i];
					ButtonX temp = buttonlist[j];
					buttonlist[i] = temp;
					buttonlist[j] = but;

					Form uct1 = tabPanelCtrlList[i];
					Form tempusr = tabPanelCtrlList[j];
					tabPanelCtrlList[i] = tempusr;
					tabPanelCtrlList[j] = uct1;
				}
			}

			string btext = ((ToolStripMenuItem)sender).Text;
			BackToFront_SelButton();
			selected_index = 0;
			TabPanel.Controls.Add(tabPanelCtrlList[buttonlist[0].TabIndex]);
			UpdateButtons();
		}


		public void RemoveTab(int index)
		{
			if (index >= 0 && buttonlist.Count > 0 && index < buttonlist.Count)
			{
				buttonlist.RemoveAt(index);
				tabPanelCtrlList.RemoveAt(index);
				BackToFront_SelButton();
				if (buttonlist.Count > 1)
				{
					if (index - 1 >= 0)
					{
						TabPanel.Controls.Add(tabPanelCtrlList[index - 1]);
					}
					else
					{
						TabPanel.Controls.Add(tabPanelCtrlList[(index - 1) + 1]);
						selected_index = (index - 1) + 1;
					}
				}
				selected_index = index - 1;

				if (buttonlist.Count == 1)
				{
					TabPanel.Controls.Add(tabPanelCtrlList[0]);
					selected_index = 0;
				}
			}
			UpdateButtons();
		}

	}
}
