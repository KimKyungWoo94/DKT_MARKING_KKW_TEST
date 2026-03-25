using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public partial class DGVToggleButtonCell:DataGridViewCheckBoxCell
    {
       
        public DGVToggleButtonCell():base(false)
        {          
            OnFont = new System.Drawing.Font("Century Gothic", 10F,FontStyle.Bold);
            OffFont = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold);
            ButtonStyle=ToggleSwitchStyle.Metro;
            Enabled=true;
            SetRenderer(new DGVToggleButtonMetroRender());
            ToggleOnSideClick=true;
            ToggleOnButtonClick=true;
            this.Value = false;           
            
        }
        public override object Clone()
        {
            var Cloned=base.Clone() as DGVToggleButtonCell;
            Cloned.ButtonStyle=this.ButtonStyle;
            Cloned.OnText=this.OnText;
            Cloned.OffText=this.OffText;
            Cloned.OffForeColor=this.OffForeColor;
            Cloned.OnForeColor=this.OnForeColor;                        
            Cloned.SetRenderer(CopyNode(_renderer));
            return Cloned;
        }
        protected override bool SetValue(int rowIndex, object value)
        {
            if (base.SetValue(rowIndex, value))
            {
                if (IsMouseMoving == false)
                {
                    if (this.RowIndex > -1)
                    {
                        if ((bool)value == true)
                        {
                            ButtonValue = this._renderer.ControlRect.Width - _renderer.GetButtonWidth();
                        }
                        else
                        {
                            ButtonValue = 0;
                        }
                    }
                    else
                    {
                        ButtonValue = 0;
                    }

                }
                //this.RefreshCell();
                return true;
            }
          
            return false;
            
        }
        protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
           //if (_renderer != null)
           //{
           //    _renderer.RenderBorder(graphics,bounds);
           //}
               base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
        }
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
           // base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.ResetClip();
            if(_renderer!=null)
            {                               
               DetectBindingValue();      
               _renderer.SetControlRect(new Rectangle(cellBounds.X,cellBounds.Y,cellBounds.Width,cellBounds.Height-this.OwningRow.DividerHeight));
               _renderer.RenderBackground( graphics,  clipBounds,  cellBounds,  rowIndex,  elementState,  value,  formattedValue,  errorText,  cellStyle,  advancedBorderStyle,  paintParts);                                            
               _renderer.RenderControl( graphics,  clipBounds,_renderer.ControlRect,  rowIndex,  elementState,  value,  formattedValue,  errorText,  cellStyle,  advancedBorderStyle,  paintParts);
            }
        }
     
        private void DetectBindingValue()
        {
            if (IsMouseMoving==false)
            {
                if (this.RowIndex > -1)
                {
                    if ((bool)this.Value)
                    {
                        ButtonValue = this._renderer.ControlRect.Width - _renderer.GetButtonWidth();
                    }
                    else
                    {
                        ButtonValue = 0;
                    }
                }
            }
        }
     
        public void SetRenderer(DGVToggleButtonRenderBase renderer)
        {
            renderer.SetToggleSwitch(this);
            _renderer = renderer;
            switch(_renderer.GetType().Name)
            {
                case "DGVToggleButtonMetroRender":
                    {
                        _style=ToggleSwitchStyle.Metro;
                    }
                    break;
                case "DGVToggleButtonAndroidRender":
                    {
                        _style=ToggleSwitchStyle.Android;
                    }
                    break;
                case "DGVToggleButtonIOS5Render":
                    {
                        _style=ToggleSwitchStyle.IOS5;
                    }
                    break;
                case "DGVToggleButtonIphoneRender":
                    {
                        _style=ToggleSwitchStyle.Iphone;
                    }
                    break;
                case "DGVToggleButtonModernRender":
                    {
                        _style=ToggleSwitchStyle.Modern;
                    }
                    break;
            }          
            if (_renderer != null)
                this.RefreshCell();
        }
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            
            Point pMousePoint = e.Location;
            pMousePoint.X += _renderer.ControlRect.X;
            pMousePoint.Y += _renderer.ControlRect.Y;
            int buttonWidth = _renderer.GetButtonWidth();
            Rectangle buttonRectangle = _renderer.GetButtonRectangle(buttonWidth);
           
           if (_moving)
           {
               int val = _xValue + (e.Location.X - _xOffset);
           
               if (val < 0)
                   val = 0;
           
               if (val > _renderer.ControlRect.Width - buttonWidth)
                   val = _renderer.ControlRect.Width - buttonWidth;
           
               ButtonValue = val;
               Trace.WriteLine(ButtonValue);
               this.RefreshCell();
               return;
           }

            if (buttonRectangle.Contains(e.Location))
            {
                _isButtonHovered = true;
                _isLeftFieldHovered = false;
                _isRightFieldHovered = false;
            }
            else
            {
                if (e.Location.X > buttonRectangle.X + buttonRectangle.Width)
                {
                    _isButtonHovered = false;
                    _isLeftFieldHovered = false;
                    _isRightFieldHovered = true;
                }
                else if (e.Location.X < buttonRectangle.X)
                {
                    _isButtonHovered = false;
                    _isLeftFieldHovered = true;
                    _isRightFieldHovered = false;
                }
            }

            this.RefreshCell();
        }
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseDown(e); 
            if (!AllowUserChange)
                return;
            if ((e.Button == MouseButtons.Left)==false)
                return;

            Point pMousePoint= e.Location;
            pMousePoint.X += _renderer.ControlRect.X;
            pMousePoint.Y+= _renderer.ControlRect.Y;
            int buttonWidth = _renderer.GetButtonWidth();
            Rectangle buttonRectangle = _renderer.GetButtonRectangle(buttonWidth);

           
           
            if (buttonRectangle.Contains(e.Location))
            {
                _isButtonPressed = true;
                _isLeftFieldPressed = false;
                _isRightFieldPressed = false;
                _moving = true;
                _xOffset = e.Location.X;
               _buttonValue = buttonRectangle.X;
               _savedButtonValue = ButtonValue;
               _xValue = ButtonValue;
               Trace.WriteLine("Contain True");
            }
            else
            {
                if (e.Location.X > buttonRectangle.X + buttonRectangle.Width)
                {
                    _isButtonPressed = false;
                    _isLeftFieldPressed = false;
                    _isRightFieldPressed = true;
                }
                else if (e.Location.X < buttonRectangle.X)
                {
                    _isButtonPressed = false;
                    _isLeftFieldPressed = true;
                    _isRightFieldPressed = false;
                }
            }

            this.RefreshCell();
        }
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (AllowUserChange==false)
                return;
            if ((e.Button == MouseButtons.Left) == false)
                return;
            //if (IsButtonHovered == true && IsButtonPressed==true)
            //    return;
            int buttonWidth = _renderer.GetButtonWidth();

            bool wasLeftSidePressed = IsLeftSidePressed;
            bool wasRightSidePressed = IsRightSidePressed;
            bool wasBunttonPressed = IsButtonPressed;
            _isButtonPressed = false;
            _isLeftFieldPressed = false;
            _isRightFieldPressed = false;
         
            if (_moving)
            {       
                double fnumerator=ButtonValue;
                double fdenorminator=_renderer.ControlRect.Width - buttonWidth;
                int percentage =(int)(fnumerator/fdenorminator *100);
                if ((bool)this.Value)
                {
                    if (percentage <= (100 - _thresholdPercentage))
                    {
                        //ButtonValue = 0;
                       this.Value=false;
                        ResetMouseMoveStatus();
                        
                    }
                    else if (ToggleOnButtonClick && _savedButtonValue == ButtonValue)
                    {
                       // ButtonValue = 0;
                        this.Value=false;
                        ResetMouseMoveStatus();
                       
                    }
                    else
                    {
                        //ButtonValue = this._renderer.ControlRect.Width - buttonWidth;
                        this.Value=true;
                        ResetMouseMoveStatus();
                       
                    }
                }
                else
                {
                    if (percentage >= _thresholdPercentage)
                    {
                        //ButtonValue = this._renderer.ControlRect.Width - buttonWidth;
                        this.Value=true;
                        ResetMouseMoveStatus();
                    }
                    else if (ToggleOnButtonClick && _savedButtonValue == ButtonValue)
                    {
                        //ButtonValue = _renderer.ControlRect.X+this._renderer.ControlRect.Width - buttonWidth;
                        this.Value=true;
                        ResetMouseMoveStatus();
                    }
                    else
                    {
                        //ButtonValue = 0;
                        this.Value = false;
                        ResetMouseMoveStatus();
                    }
                }
                _moving = false;
                return;
            }
            if (IsButtonOnRightSide(e.Location.X))
            {
                this.Value = true;
                //_buttonValue = this._renderer.ControlRect.Width - buttonWidth;
            }
            else
            {
                this.Value = false;
            }
            if (wasLeftSidePressed && ToggleOnSideClick)
            {
                this.Value = false;
                ResetMouseMoveStatus();
               
            }
            else if (wasRightSidePressed && ToggleOnSideClick)
            {
                this.Value = true;
                ResetMouseMoveStatus();
              
            }
            this.RefreshCell();
        }
        private void ResetMouseMoveStatus()
        {
            _isButtonHovered = false;
            _isLeftFieldHovered = false;
            _isRightFieldHovered = false;
            _isButtonPressed = false;
            _isLeftFieldPressed = false;
            _isRightFieldPressed = false;
            _moving = false;
        }
        
        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);
            if(this.RowIndex>-1)
            {
                if ((bool)this.Value)
                {
                    int buttonWidth = _renderer.GetButtonWidth();
                    ButtonValue = this._renderer.ControlRect.Width - buttonWidth;
                }
                else
                {
                    ButtonValue = 0;
                }
            }
            else
            {
                ButtonValue = 0;
            }
           
            ResetMouseMoveStatus();
            this.RefreshCell();
        }
      
        private void RefreshCell()
        {
            if(this.DataGridView!=null)
            {
                this.DataGridView.InvalidateCell(this);
            }
        }
        /// <summary>
        /// https://nowonbun.tistory.com/525
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        static T CopyNode<T>(T node)
        {
            Type type = node.GetType();
            T clone = (T)Activator.CreateInstance(type);
            // 클래스 내부에 있는 모든 변수를 가져온다.
            foreach (var field in type.GetFields(BindingFlags.Public |  BindingFlags.Instance))//BindingFlags.NonPublic |
            {
                // 변수가 Class 타입이면 그 재귀를 통해 다시 복제한다. 단, String은 class이지만 구조체처럼 사용되기 때문에 예외
                if (field.FieldType.IsClass && field.FieldType != typeof(String))
                {
                    // 새로운 클래스에 데이터를 넣는다.
                    field.SetValue(clone, CopyNode(field.GetValue(node)));
                    continue;
                }
                // 새로운 클래스에 데이터를 넣는다.
                field.SetValue(clone, field.GetValue(node));
            }
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //Step : 4 check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                    {
                        property.SetValue(clone, property.GetValue(node, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(node, null);
                        if (objPropertyValue == null)
                        {
                            property.SetValue(clone, null, null);
                        }
                        else
                        {
                            property.SetValue(clone, CopyNode(objPropertyValue), null);
                        }
                    }
                }
            }
            return clone;
        }



    }
}
