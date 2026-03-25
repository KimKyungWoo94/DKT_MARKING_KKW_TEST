using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public partial class ucTabControl : System.Windows.Forms.TabControl
    {
        private Color m_SelectedTabColor=Color.RoyalBlue;
        [Category("Tab Color"), Description("Selected Tab Color")]
        public Color SelectedTabColor
        {
            get
            {
                return m_SelectedTabColor;
            }
            set
            {
                m_SelectedTabColor = value;
                this.Invalidate();
            }
        }
        public Color m_SelectedTabForeColor=Color.White;
        [Category("Tab Color"), Description("Selected Tab Fore Color")]
        public Color SelectedTabForeColor
        {
            get
            {
                return m_SelectedTabForeColor;
            }
            set
            {
                m_SelectedTabForeColor = value;
                this.Invalidate();
            }
        }
        private Color m_UnSelectTabColor=Color.LightSkyBlue;
        [Category("Tab Color"), Description("UnSelectTab Color")]
        public Color UnSelectTabColor
        {
            get
            {
                return m_UnSelectTabColor;
            }
            set
            {
                m_UnSelectTabColor = value;
                this.Invalidate();
            }
        }
        public Color m_UnSelectTabForeColor=Color.White;
        [Category("Tab Color"), Description("UnSelectTab Fore Color")]
        public Color UnSelectTabForeColor
        {
            get
            {
                return m_UnSelectTabForeColor;
            }
            set
            {
                m_UnSelectTabForeColor = value;
                this.Invalidate();
            }
        }
        public Color m_TabSpaceBackColor=Color.White;
        [Category("Tab Color"), Description("TabSpace Back Color")]
        public Color TabSpaceBackColor
        {
            get
            {
                return m_TabSpaceBackColor;
            }
            set
            {
                m_TabSpaceBackColor = value;
                this.Invalidate();
            }
        }
       
        public ucTabControl()
        {
            this.ItemSize = new Size(125, 25);
            this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DrawItem += new DrawItemEventHandler(this.RepaintControls);
            this.SizeMode=TabSizeMode.Fixed;
            
            this.Invalidate();
        }      
        private void RepaintControls(object sender, DrawItemEventArgs e)
        {
            try
            {
                Font font;
                Brush back_brush;
                Brush fore_brush;
                Rectangle bounds = e.Bounds;

                this.TabPages[e.Index].BackColor = Color.Silver;

                if (e.Index == this.SelectedIndex)
                {
                    font = new Font(e.Font, e.Font.Style);
                    back_brush = new SolidBrush(m_SelectedTabColor);
                    fore_brush = new SolidBrush(m_SelectedTabForeColor);
                    bounds = new Rectangle(bounds.X + (this.Padding.X / 2), bounds.Y + this.Padding.Y, bounds.Width - this.Padding.X, bounds.Height - (this.Padding.Y * 2));
                 
                }
                else
                {
                        
                    font = new Font(e.Font, e.Font.Style & ~FontStyle.Bold);
                    back_brush = new SolidBrush(m_UnSelectTabColor);
                    fore_brush = new SolidBrush(m_UnSelectTabForeColor);                 
                }               
                string tab_name = this.TabPages[e.Index].Text;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.FillRectangle(back_brush, bounds);
                e.Graphics.DrawString(tab_name, font, fore_brush, bounds, sf);
               
                Brush background_brush = new SolidBrush(m_TabSpaceBackColor);
                Rectangle LastTabRect = this.GetTabRect(this.TabPages.Count - 1);
                Rectangle rect = new Rectangle();
                rect.Location = new Point(LastTabRect.Right , this.Top );
                rect.Size = new Size(this.Right - rect.Left, LastTabRect.Height);
                e.Graphics.FillRectangle(background_brush, rect);
                background_brush.Dispose();
                
                sf.Dispose();
                back_brush.Dispose();
                fore_brush.Dispose();
                font.Dispose();
            }
            catch
            { }
        }
    }
}

