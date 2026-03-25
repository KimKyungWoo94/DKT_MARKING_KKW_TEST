using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public abstract class DGVToggleButtonRenderBase
    {

        #region Private Members
        private DGVToggleButtonCell _toggleSwitchCell;
        private Rectangle           _ControlRect=new Rectangle();
        #endregion Private Members

        #region Constructor

        protected DGVToggleButtonRenderBase()
        {
        }

        internal void SetToggleSwitch(DGVToggleButtonCell a_value)
        {
            _toggleSwitchCell = a_value;
        }

        internal DGVToggleButtonCell ToggleSwitch
        {
            get { return _toggleSwitchCell; }
        }

        #endregion Constructor

        #region Render Methods

        public void RenderBackground(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (_toggleSwitchCell == null)
                return;
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            {
                Color backColor = (ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? ToggleSwitch.OwningColumn.DefaultCellStyle.BackColor.ToGrayScale() : ToggleSwitch.OwningColumn.DefaultCellStyle.BackColor;
               
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;            
                FillBackground(graphics, cellBounds,SystemColors.AppWorkspace);                               
                FillBackground(graphics, this.ControlRect, backColor);   
                //cellBounds.Inflate(-1,-1);                                       
                RenderBorder(graphics, cellBounds);
            }
        }

        public void RenderControl(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (_toggleSwitchCell == null)
                return;

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle buttonRectangle = GetButtonRectangle();
            Rectangle DrawButtonRectangle  =new Rectangle(cellBounds.X+buttonRectangle.X,cellBounds.Y+buttonRectangle.Y,buttonRectangle.Width,buttonRectangle.Height);
            int totalToggleFieldWidth = cellBounds.Width - buttonRectangle.Width;

            if (buttonRectangle.X > 0)
            {
                Rectangle leftRectangle = new Rectangle(cellBounds.X, cellBounds.Y, buttonRectangle.X, cellBounds.Height);

                if (leftRectangle.Width > 0)
                    RenderLeftToggleField(graphics, leftRectangle, totalToggleFieldWidth);               
            }
          
            if (buttonRectangle.X + buttonRectangle.Width <cellBounds.Width)
            {
                Rectangle rightRectangle = new Rectangle(cellBounds.X+buttonRectangle.X + buttonRectangle.Width, cellBounds.Y+buttonRectangle.Y, cellBounds.Width-buttonRectangle.X- buttonRectangle.Width, cellBounds.Height);
                if (rightRectangle.Width > 0)
                   RenderRightToggleField(graphics, rightRectangle, totalToggleFieldWidth);               
            }
            RenderButton(graphics, DrawButtonRectangle);
        }  
        public void SetControlRect(Rectangle a_value)
        {
            _ControlRect=a_value;
        }
        public Rectangle ControlRect
        {
            get { return _ControlRect;}
        }

        protected void FillBackground(Graphics g, Rectangle controlRectangle,Color BackColor)
        {
            using (Brush backBrush = new SolidBrush(BackColor))
            {
                g.FillRectangle(backBrush, controlRectangle);
            }
        }
        public abstract void RenderBorder(Graphics g, Rectangle borderRectangle);
        public abstract void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth);
        public abstract void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth);
        public abstract void RenderButton(Graphics g, Rectangle buttonRectangle);

        #endregion Render Methods

        #region Helper Methods

        public abstract int GetButtonWidth();
        public abstract Rectangle GetButtonRectangle();
        public abstract Rectangle GetButtonRectangle(int buttonWidth);
       // public abstract object Clone();


        #endregion Helper Methods
    }
    public static class GraphicsExtensionMethods
    {
        public static Color ToGrayScale(this Color originalColor)
        {
            if (originalColor.Equals(Color.Transparent))
                return originalColor;

            int grayScale = (int)((originalColor.R * .299) + (originalColor.G * .587) + (originalColor.B * .114));
            return Color.FromArgb(grayScale, grayScale, grayScale);
        }
    }
}
