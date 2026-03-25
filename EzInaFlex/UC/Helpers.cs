using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Drawing.Text;
using System.Text;

namespace EzIna
{

	#region [ Menu Color Table ]
	///
	/// <summary>
	/// https://stackoverflow.com/questions/32307778/change-the-border-color-of-winforms-menu-dropdown-list
	/// </summary>
	public class MenuColorTable : ProfessionalColorTable
	{
		public override Color ToolStripDropDownBackground
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color ImageMarginGradientBegin
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color ImageMarginGradientMiddle
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color ImageMarginGradientEnd
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuBorder
		{
			get
			{
				return Color.White;
			}
		}

		public override Color MenuItemBorder
		{
			get
			{
				return Color.Silver;
			}
		}

		public override Color MenuItemSelected
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuStripGradientBegin
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuStripGradientEnd
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuItemSelectedGradientBegin
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuItemSelectedGradientEnd
		{
			get
			{
				return FA.DEF.MenuColor;;
			}
		}

		public override Color MenuItemPressedGradientBegin
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}

		public override Color MenuItemPressedGradientEnd
		{
			get
			{
				return FA.DEF.MenuColor;
			}
		}
	}
	public class MenubarRenderer : ToolStripProfessionalRenderer
	{
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
			Color c = Color.FromArgb(1, 90, 170);
			using (SolidBrush brush = new SolidBrush(c))
				e.Graphics.FillRectangle(brush, rc);
		}
	}

	#endregion

	#region [ DataGridView - Disable Button]
	public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
	{
		public DataGridViewDisableButtonColumn()
		{
			this.CellTemplate = new DataGridViewDisableButtonCell();
		}
	}

	public class DataGridViewDisableButtonCell : DataGridViewButtonCell
	{
		private bool enabledValue;
		public bool Enabled
		{
			get
			{
				return enabledValue;
			}
			set
			{
				enabledValue = value;
			}
		}

		// Override the Clone method so that the Enabled property is copied.
		public override object Clone()
		{
			DataGridViewDisableButtonCell cell =
				(DataGridViewDisableButtonCell)base.Clone();
			cell.Enabled = this.Enabled;
			return cell;
		}

		// By default, enable the button cell.
		public DataGridViewDisableButtonCell()
		{
			this.enabledValue = true;
		}

		protected override void Paint(Graphics graphics,
			Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
			DataGridViewElementStates elementState, object value,
			object formattedValue, string errorText,
			DataGridViewCellStyle cellStyle,
			DataGridViewAdvancedBorderStyle advancedBorderStyle,
			DataGridViewPaintParts paintParts)
		{
			// The button cell is disabled, so paint the border,
			// background, and disabled button for the cell.
			if (!this.enabledValue)
			{
				// Draw the cell background, if specified.
				if ((paintParts & DataGridViewPaintParts.Background) ==
					DataGridViewPaintParts.Background)
				{
					SolidBrush cellBackground =
						new SolidBrush(cellStyle.BackColor);
					graphics.FillRectangle(cellBackground, cellBounds);
					cellBackground.Dispose();
				}

				// Draw the cell borders, if specified.
				if ((paintParts & DataGridViewPaintParts.Border) ==
					DataGridViewPaintParts.Border)
				{
					PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
						advancedBorderStyle);
				}

				// Calculate the area in which to draw the button.
				Rectangle buttonArea = cellBounds;
				Rectangle buttonAdjustment =
					this.BorderWidths(advancedBorderStyle);
				buttonArea.X += buttonAdjustment.X;
				buttonArea.Y += buttonAdjustment.Y;
				buttonArea.Height -= buttonAdjustment.Height;
				buttonArea.Width -= buttonAdjustment.Width;

				// Draw the disabled button.
				ButtonRenderer.DrawButton(graphics, buttonArea,
					System.Windows.Forms.VisualStyles.PushButtonState.Disabled);

				// Draw the disabled button text.
				if (this.FormattedValue is String)
				{
					TextRenderer.DrawText(graphics,
						(string)this.FormattedValue,
						new Font("Century Gothic", 9.0F, FontStyle.Regular, GraphicsUnit.Point),
					buttonArea, SystemColors.GrayText);
				}
			}
			else
			{
				// The button cell is enabled, so let the base class
				// handle the painting.
				base.Paint(graphics, clipBounds, cellBounds, rowIndex,
					elementState, value, formattedValue, errorText,
					cellStyle, advancedBorderStyle, paintParts);
			}
		}
	}
	#endregion

	public static class ControlHelper
    {
        public static int ProgressMinimum   = 0;
        public static int ProgressMaximum   = 100;
        public static int ProgressValue     = 0;

		public static void ProgressBarUpdate(this Control ctrl, PaintEventArgs e)
        {
            // Clear the background.
            e.Graphics.Clear((ctrl as PictureBox).BackColor);

            // Draw the progress bar.
            float fraction = 0;

            if (ProgressValue == 0)
                fraction = 0;
            else
                fraction = (float)(ProgressValue - ProgressMinimum) /
                (ProgressMaximum - ProgressMinimum);
            int wid = (int)(fraction * (ctrl as PictureBox).ClientSize.Width);
            e.Graphics.FillRectangle(
                Brushes.LightGreen, 0, 0, wid,
                (ctrl as PictureBox).ClientSize.Height);

            // Draw the text.
            e.Graphics.TextRenderingHint =
                TextRenderingHint.AntiAliasGridFit;
            using (StringFormat sf = new StringFormat())
            {
                using (Font f = new Font("Century Gothic", 9F,FontStyle.Regular, GraphicsUnit.Point))
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    int percent = (int)(fraction * 100);
                    string strValue = string.Format("{0} / {1}  [ {2:D3}% ]", ProgressValue, ProgressMaximum, percent);
                    e.Graphics.DrawString(
                        strValue,
                        f, Brushes.Black,
                        (ctrl as PictureBox).ClientRectangle, sf);
                }
            
            }
        }
        public static void DoubleBuffered(this Control ctrl, bool setting)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }

        public static int    AsInt(this Control ctrl, int defaultValue = 0)
        {
            try
            {
                return int.Parse(ctrl.Text);
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }

        public static double AsDouble(this Control ctrl, double defaultValue = 0)
        {
            try
            {
                return double.Parse(ctrl.Text);
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }

        public static void   SetValue(this Control ctrl, object o)
        {
            ctrl.Text = o.ToString();
        }

        public static void   SetValue(this Control ctrl, double value, string fmt)
        {
            ctrl.Text = value.ToString(fmt);
        }

        /// <summary>
        /// 불필요한 UI Drawing 을 수행하지 않도록 한다
        /// http://stackoverflow.com/questions/778095/windows-forms-using-backgroundimage-slows-down-drawing-of-the-forms-controls
        /// </summary>
        public static void   SuspendDrawing(this Control ctrl)
        {
            if (ctrl.IsDisposed) return;

            WinAPIs.SendMessage(ctrl.Handle, WinAPIs.WM_SETREDRAW, 0, 0);
        }

        public static void   ResumeDrawing(this Control ctrl, bool redraw = true)
        {
            if (ctrl.IsDisposed) return;

            WinAPIs.SendMessage(ctrl.Handle, WinAPIs.WM_SETREDRAW, 1, 0);
            if (redraw) ctrl.Refresh();
        }

        /// <summary>
        /// generic invoke
        /// http://www.devpia.com/Maeul/Contents/Detail.aspx?BoardID=18&MAEULNO=8&indexdexdexdexdexdexdexdexdex=1723&page=8</summary>
        /// </summary>
        public static void InvokeIfNeeded(this Control ctrl, Action action)
        {
            try
            {
                if (ctrl.IsDisposed || ctrl.Disposing) return;

                if (ctrl.InvokeRequired)
                    ctrl.Invoke(action);
                else
                    action();
            }
            catch(Exception ex)
            {
                //APP.Logger.Fatal(ctrl.Name + ".InvokeIfNeeded()", ex);
            }
        }
				public static void BeginInvokeIfNeeded(this Control ctrl, Action action)
				{
						try
						{
								if (ctrl.IsDisposed || ctrl.Disposing) return;

								if (ctrl.InvokeRequired)
										ctrl.BeginInvoke(action);
								else
										action();
						}
						catch (Exception ex)
						{
								//APP.Logger.Fatal(ctrl.Name + ".InvokeIfNeeded()", ex);
						}
				}
				
				/// <summary>
				/// generic invoke
				/// http://www.devpia.com/Maeul/Contents/Detail.aspx?BoardID=18&MAEULNO=8&indexdexdexdexdexdexdexdexdex=1723&page=8</summary>
				/// </summary>
				public static void InvokeIfNeeded<T>(this Control ctrl, Action<T> action, T args)
        {
            if (ctrl.IsDisposed || ctrl.Disposing) return;

            if (ctrl.InvokeRequired)
                ctrl.Invoke(action, args);
            else
                action(args);
        }
		public static void InvokeIfNeeded<T>(this Control ctrl, Action<T,T> action, T WParam, T LParam )
		{
			if (ctrl.IsDisposed || ctrl.Disposing) return;

			if (ctrl.InvokeRequired)
				ctrl.Invoke(action, WParam, LParam);
			else
				action(WParam, LParam);
		}

		/// <summary>
		/// Hide child forms
		/// </summary>
		public static void HideChildForms(this Control ctrl)
        {
            if (ctrl != null)
                foreach (Control f in ctrl.Controls)
                {
                    if (f is Form)
                    {
                        (f as Form).Hide();
                    }
                }
        }

        /// <summary>
        /// Close child forms
        /// </summary>
        public static void CloseChildForms(this Control ctrl)
        {
            if (ctrl != null)
                foreach (Control f in ctrl.Controls)
                {
                    if (f is Form)
                    {
                        (f as Form).Close();
                    }
                }
        }

        /// <summary>
        /// Double Buffering All DataGridView in control 
        /// </summary>
        public static void DoubleBufferingAllDataGrid(this Control ctrl)
        {
            foreach (var c in ctrl.Controls)
                if (c is DataGridView)
                    ((DataGridView)c).DoubleBuffered(true);
                else if (c is Control)
                    DoubleBufferingAllDataGrid((Control)c);
        }

        /// <summary>
        /// Disable child controls
        /// </summary>
        public static void DisableControls(this Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.Enabled = false;
            }
        }

        /// <summary>
        /// Enable child controls
        /// </summary>
        public static void EanbleControls(this Control ctrl, int authority)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.ForeColor.Equals(Color.Blue) || c.ForeColor.Equals(Color.Navy))
                    c.Enabled = authority > 0;
                else if (c.ForeColor == Color.Red || c.ForeColor == Color.Maroon)
                    c.Enabled = authority > 1;
                else if (c.ForeColor == Color.Purple)
                    c.Enabled = authority >= 2;
                else
                    c.Enabled = true;
            }
        }

        // https://stackoverflow.com/questions/3419159/how-to-get-all-child-controls-of-a-windows-forms-form-of-a-specific-type-button
        public static IEnumerable<Control> GetAllControls(this Control ctrl)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in ctrl.Controls)
            {
                controlList.AddRange(GetAllControls(c));
                controlList.Add(c);
            }
            return controlList;
        }

        public static void SetScroll (this Control ctrl, int xPos, int yPos, bool bRedraw = true)
        {
            WinAPIs.SetScrollPos(ctrl.Handle, 0, xPos, bRedraw);
            WinAPIs.SetScrollPos(ctrl.Handle, 1, yPos, bRedraw);
            ctrl.Invalidate();
            //const int EM_LINESCROLL = 0x00B6;
            //WinApi.SendMessage(ctrl.Handle, EM_LINESCROLL, 0, xPos);
            //WinApi.SendMessage(ctrl.Handle, EM_LINESCROLL, 1, yPos);

        }
        public static void SetScrollX(this Control ctrl, int xPos, bool bRedraw = true)
        {
            WinAPIs.SetScrollPos(ctrl.Handle, 0, xPos, bRedraw);
        }
        public static void SetScrollY(this Control ctrl, int yPos, bool bRedraw = true)
        {
            WinAPIs.SetScrollPos(ctrl.Handle, 1, yPos, bRedraw);
        }
    }

    public static class DataGridViewRowHelper
    {
        public static void FitCells(this DataGridView self)
        {
            //self.FitRows();

            self.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in self.Columns)
            {
                col.FillWeight = 100;
            }
        }

        public static void FitRows(this DataGridView self)
        {
            self.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            int h = self.ClientSize.Height - 3;
            if (self.ScrollBars.In(ScrollBars.Both, ScrollBars.Horizontal)) h -= 8;

            if (self.ColumnHeadersVisible)
                h -= self.ColumnHeadersHeight;
            for (int i = 0; i < self.RowCount; i++)
            {
                self.Rows[i].Height = h / (self.RowCount - i);
                h -= self.Rows[i].Height;
            }
        }

        public static void DrawEmpty(this DataGridView self, Color color)
        {
            self.RowCount = 1;
            self.ColumnCount = 1;
            self[0, 0].Style.BackColor = color;
            self[0, 0].Value = "EMPTY";
            self.ClearSelection();
            self.ReadOnly = true;
            self.FitCells();
        }
        public static void DrawEmpty(this DataGridView self)
        {
            self.DrawEmpty(Color.DarkGray);
        }

        public static void SaveToCSV(this DataGridView self, bool a_bWriteHead)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "csv (*.csv) | *.csv";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string strDelimiter = ",";
                    if (self.Rows.Count == 0) return;

                    FileStream f = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
                    StreamWriter CsvFileExport = new StreamWriter(f, Encoding.UTF8);

                    if (a_bWriteHead)
                    {
                        for (int i = 0; i < self.Columns.Count; i++)
                        {
                            CsvFileExport.Write(self.Columns[i].HeaderText);
                            if (i != self.Columns.Count - 1)
                            {
                                CsvFileExport.Write(strDelimiter);
                            }

                        }
                    }

                    CsvFileExport.WriteLine("");
                    CsvFileExport.WriteLine("NO,ADDR X,ADDR Y,POS X [um],POS Y [um],WIDTH [um],HEIGHT [um],Meas Result");

                    foreach (DataGridViewRow row in self.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < self.ColumnCount; i++)
                            {
                                CsvFileExport.Write(row.Cells[i].Value);
                                if (i != self.Columns.Count - 1)
                                {
                                    CsvFileExport.Write(strDelimiter); //add with delimiter
                                }
                            }
                            CsvFileExport.Write(CsvFileExport.NewLine);
                        }
                    }

                    CsvFileExport.Flush();
                    CsvFileExport.Close();
                    f.Close();

                    CsvFileExport.Dispose();
                    f.Dispose();

                }


            }
        }
             
    }

    public static class FormHelper
    {
        /// <summary>
        /// Show Fom in parent control and Close/Hide previous insided forms
        /// </summary>
        public static void ShowInside(this Form form, Control parent)
        {
            form.TopLevel = false;
            parent.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Show();
        }

        public static void ShowAndFront(this Form self, Form main)
        {
            if (self.Visible) self.BringToFront();
            else self.Show(main);
        }
    }

    public static class ComparableHelper
    {
        public static bool In<T>(this T self, params T[] args) where T : IComparable
        {
            if (args == null) throw new Exception("Comparable.In : args is NULL");

            foreach (T a in args)
                if (self.Equals(a)) return true;

            return false;
        }

        public static bool InRange<T>(this T self, T min, T max) where T : IComparable
        {
            return (self.CompareTo(min) >= 0 && self.CompareTo(max) <= 0);
        }

        public static T EnsureRange<T>(this T self, T min, T max) where T : IComparable
        {
            if (self.CompareTo(min) < 0) return min;
            else if (self.CompareTo(max) > 0) return max;
            return self;
        }
    }

    public static class DoubleHelper
    {
        public static bool IsZero(this double self, double ES = 10E-7)
        {
            return (self >= -ES && self <= ES);
        }

        public static bool IsSame(this double self, double value, double ES = 10E-7)
        {
            return (self - value).IsZero(ES);
        }
    }

	public static class SingleHelper
	{
		public static bool IsZero(this float self, float EPSILON = 10E-7f)
		{
			return (self >= -EPSILON && self <= EPSILON);
		}

		public static bool IsSame(this float self, float value, float EPSILON = 10E-7f)
		{
			return (self - value).IsZero(EPSILON);
		}
	}

	public static class ObjectHelper
    {
        public static bool In(this object self, params object[] args)
        {
            foreach (var a in args)
                if (self == a) return true;

            return false;
        }
    }

    public static class StringHelper
    {
		public static string ByteToString(this byte[] self)
		{
			return Encoding.Default.GetString(self);
		}

		public static byte[] StringtoByte(this string self)
		{
			return Encoding.UTF8.GetBytes(self);
		}
		public static List<string> GetDataSourceTypes<T>()
		{
			Type t = typeof(T);
			return !t.IsEnum ? null : Enum.GetNames(t).ToList();
		}

		public static T ToEnum<T>(this string self)
        {
            return (T)Enum.Parse(typeof(T), self, true);
        }


        public static bool IsSame(this string self, string s)
        {
            return string.Equals(self, s, StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsSame(this string self, params string[] args)
        {
            foreach (string a in args)
                if (self.IsSame(a)) return true;

            return false;
        }

        public static bool ExtractWords(this string self, char sep, ref string s1, ref string s2)
        {
            string[] words = self.Split(sep);

            if (words.Length < 2) return false;

            s1 = words[0];
            s2 = words[1];

            return true;
        }
        public static bool ExtractWords(this string self, char sep, ref double d1, ref double d2)
        {
            string s1 = "", s2 = "";
            double tmp1, tmp2;
            
            if (!self.ExtractWords(sep, ref s1, ref s2)) return false;

            if (double.TryParse(s1, out tmp1) && double.TryParse(s2, out tmp2))
            {
                d1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                d2 = tmp2;
                return true;
            }
            else
                return false;
        }
        public static bool ExtractWords(this string self, char sep, ref int i1, ref int i2)
        {
            string s1 = "", s2 = "";
            int tmp1, tmp2;
            
            if (!self.ExtractWords(sep, ref s1, ref s2)) return false;

            if (int.TryParse(s1, out tmp1) && int.TryParse(s2, out tmp2))
            {
                i1 = tmp1; // 실패시 ref 값이 바뀌지 않게하기 위해
                i2 = tmp2;
                return true;
            }
            else
                return false;
        }
    }

    // https://stackoverflow.com/questions/15380730/foreach-every-subitem-in-a-menustrip
    public static class ToolStripItemCollectionHelper
    {
        /// <summary>
        /// Recusively retrieves all menu items from the input collection
        /// </summary>
        public static IEnumerable<ToolStripMenuItem> GetAllMenuItems(this ToolStripItemCollection items)
        {
            var allItems = new List<ToolStripMenuItem>();
            foreach (var item in items.OfType<ToolStripMenuItem>())
            {
                allItems.Add(item);
                allItems.AddRange(GetAllMenuItems(item.DropDownItems));
            }
            return allItems;
        }
    }

    public static class ColorHelper
    {
        public static uint ToColorREF(this Color self)
        {
            return (uint)(self.R | (self.G << 8) | (self.B << 16));
        }

        public static Color FromColorREF(this Color self, uint rgb)
        {
            return Color.FromArgb((int)rgb & 0xFF, (int)(rgb & 0xFF00) >> 8, (int)rgb & (0xFF0000) >> 16);
        }

        public static string ToHtmlString(this Color self)
        {
            return string.Format("#{0:X}{1:X}{2:X}", self.R, self.G, self.B);
        }

    }

    public static class CancellationTokenHelper
	{
		struct Unit { }

		public static Task AsTask(this CancellationToken @this)
		{
			var tcs = new TaskCompletionSource<Unit>();

			@this.Register(() => tcs.SetResult(default(Unit)));

			return tcs.Task;
		}
	}

}
