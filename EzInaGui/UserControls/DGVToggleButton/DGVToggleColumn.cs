using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{   
    public class DGVToggleColumn:DataGridViewCheckBoxColumn
    {
        public DGVToggleColumn()
        {
            this.CellTemplate=new DGVToggleButtonCell();   
            (this.CellTemplate as DGVToggleButtonCell).ButtonStyle= DGVToggleButtonCell.ToggleSwitchStyle.IOS5;
            (this.CellTemplate as DGVToggleButtonCell).OnForeColor=System.Drawing.Color.White;
            (this.CellTemplate as DGVToggleButtonCell).OffForeColor=Color.FromArgb(141, 123, 141);           
        }
        public DGVToggleButtonCell.ToggleSwitchStyle ButtonStyle
        {
            get
            {
                return  (this.CellTemplate as DGVToggleButtonCell).ButtonStyle;
            }
            set
            {
                (this.CellTemplate as DGVToggleButtonCell).ButtonStyle=value;
            }
        }
        public string OnText
        {
            get
            {
                return (this.CellTemplate as DGVToggleButtonCell).OnText;
            }
            set
            {
                (this.CellTemplate as DGVToggleButtonCell).OnText = value;
            }
        }
        public string OffText
        {
            get
            {
                return (this.CellTemplate as DGVToggleButtonCell).OffText;
            }
            set
            {
                (this.CellTemplate as DGVToggleButtonCell).OffText = value;
            }
        }
        public Color OnForeColor
        {
            get
            {
                return (this.CellTemplate as DGVToggleButtonCell).OnForeColor;
            }
            set
            {
                (this.CellTemplate as DGVToggleButtonCell).OnForeColor = value;
            }
        }
        public Color OffForeColor
        {
            get
            {
                return (this.CellTemplate as DGVToggleButtonCell).OffForeColor;
            }
            set
            {
                (this.CellTemplate as DGVToggleButtonCell).OffForeColor = value;
            }
        }
        public void SetToggleButtonRender(DGVToggleButtonRenderBase pRender)
        {            
            (this.CellTemplate as DGVToggleButtonCell).SetRenderer(pRender);                        
        }       
    }
}
