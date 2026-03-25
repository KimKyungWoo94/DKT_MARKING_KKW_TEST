using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;

namespace EzIna.GUI.UserControls
{
	#region Enumerator
	[Flags]
	public enum ImageBoxZoomActions
	{
		None		= 0, // No action.
		ZoomIn		= 1, // The control is increasing the zoom.
		ZoomOut		= 2, // The control is decreasing the zoom.
		ActualSize	= 4 // The control zoom was reset.
	}

	[Flags]
	public enum ImageBoxActionSources
	{
		Unknown = 0, // Unknown source.
		User	= 1  // A user initialized the action.
	}
	public enum eMouseMode
	{
		None,
		Pan,
		ZoomClick,
		ZoomRectangle
	}

	public enum eSelectionMode
	{
		None		,	//   No selection.
		Rectangle	,	//   Rectangle selection.
		Zoom			//   Zoom selection.
	}
	public enum eCellStatus : byte
	{
		Empty			= 0,
		Ready			= 1,
		Work			= 2,
		Finish			= 3,
		OK				= 4,
		NG				= 5,
		NG_INSPECTION	= 6,
		NG_SPEC_OUT		= 7,
		Max			
	};
	#endregion

	public partial class ucCellBox : UserControl
	{
		#region 상수 정의
		private const int MaxZoom = 100000;
		private const int MinZoom = 1;
		private const int SelectionDeadZone = 5;
		#endregion

		#region 변수정의
		private eCellStatus	[] _stripStatus;// 셀 상태
		private string		[] _stripText;
		private Color		[] _colorStatus; 
		private double		[] _stripEncXValue;
		private double		[] _stripEncYValue;
		private double		[] _stripEncZValue;
		private double		[] _stripXValue;
		private double		[] _stripYValue;
		private double		[] _stripZValue;

		private Color _mouseActiveColor;
		private Color _mouseSelectedColor;
		private Color _imageBorderColor;
		private Color _displayNumColor;
		private Color _textBackColor;
		private Color _pixelGridColor;
		private Color _pixelGridBlockColor;
		private Color _selectionColor;

		private bool _activeEnable;
		private bool _selectEnable;
		private bool _allowZoom;
		private bool _isPanning;
		private bool _limitSelectionToImage;
		private bool _shortcutsEnabled;
		private bool _showPixelGrid;
		private bool _display2GroupNumIndex;
		private bool _display2CellNumIndex;
		private bool _doubleSelectEnable;
		private bool _groupNumVisible;
		private bool _cellNumVisible;
		private bool _cellTxtVisible;

		private int _pixelGridThreshold;
		private int _pixelNumberThreshold;
		private int _updateCount;
		private int _zoom;

		private int _colCount;
		private int _rowCount;
		private int _blockColCount;
		private int _blockRowCount;

		private Point _startMousePosition;
		private Point _startScrollPosition;
		private Point _activePixelPosition;
		private Point _selectPixelPosition;

		private bool _mouseMoveChangeEnable;
		private bool _isMouseDown;

		private RectangleF _selectionRegion;
		private ZoomLevelCollection _zoomLevels;
		private Image _image;
		private int[] _rankDatas;

		private eSelectionMode _selectionMode;
		private eMouseMode _mouseMode;
		private InterpolationMode _interpolationMode;
		private ContentAlignment _textAlign;
		#endregion

		#region 생성자
		public ucCellBox()
		{
			InitializeComponent();
			this.AllowZoom             = true;
            this.LimitSelectionToImage = true;
            this.BackColor             = Color.White;
            this.AutoSize              = false;
            this.AutoScroll            = true;
            this.InterpolationMode     = InterpolationMode.NearestNeighbor;
            this.SelectionColor        = SystemColors.Highlight;
            this.ShortcutsEnabled      = true;
            this.ZoomLevels            = ZoomLevelCollection.Default;
            this.ImageBorderColor      = SystemColors.ControlDark;
            this.PixelGridColor        = Color.DimGray;
            this.PixelGridThreshold    =  5;
            this.PixelNumberThreshold  = 25;
            this.TextAlign             = ContentAlignment.MiddleCenter;
            this.TextBackColor         = Color.Transparent;
            this.AutoScaleMode         = AutoScaleMode.None;
            this.DoubleBuffered        = true;
            this.PerformActualSize(ImageBoxActionSources.Unknown);
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.StandardDoubleClick,true);
            this.UpdateStyles();

			_display2GroupNumIndex	= true;
            _display2CellNumIndex	= true;
            _activeEnable			= true;
            _selectEnable			= true;
            _doubleSelectEnable		= true;
            _groupNumVisible		= true;
            _cellNumVisible			= true;
            _cellTxtVisible			= true;
            _mouseMoveChangeEnable	= false; 
            _isMouseDown			= false;

            _colCount				= 100;
            _rowCount				= 100;
            _blockColCount			= 5;
            _blockRowCount			= 5;

            _image					= new Bitmap		(ColCount,RowCount);
            _rankDatas				= new int			[ColCount*RowCount];
            _stripStatus			= new eCellStatus	[ColCount*RowCount];
			_stripEncXValue			= new double		[ColCount*RowCount];
			_stripEncYValue			= new double	    [ColCount*RowCount];
			_stripEncZValue			= new double		[ColCount*RowCount];
			_stripXValue			= new double		[ColCount*RowCount];
			_stripYValue			= new double	    [ColCount*RowCount];
			_stripZValue			= new double		[ColCount*RowCount];
            _stripText				= new string		[ColCount*RowCount];
            _colorStatus			= new Color[256];

            using(Graphics  gra = Graphics.FromImage(_image))
            {
                gra.FillRectangle(new SolidBrush(Color.Blue),0,0,ColCount,RowCount);
            }

            _mouseActiveColor  = Color.DarkGreen;
            _mouseSelectedColor= Color.DarkRed;
            _displayNumColor   = Color.DarkBlue;



			for(int i = 0; i < _colorStatus.Length; i++)
			{
				_colorStatus[i] = Color.DimGray;
			}

			ColorEmpty			= Color.DimGray;
			ColorReady			= Color.Lime;
			ColorWork			= Color.AliceBlue;
            ColorFinish			= Color.Blue;
			ColorOkay			= Color.Blue;
            ColorNG				= Color.Red;   // 255
			ColorNG_INSPECTION	= Color.White;
			ColorNG_SPEC_OUT	= Color.Red;

            for (int i = 0; i < _colCount; i++)
                for (int j = 0; j < _rowCount; j++)
                    SetStatus(i,j,eCellStatus.Ready,false);

            ApplyStripInfo();
		}
		#endregion

		#region Overridden 함수들..
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.Invalidate();
		}

		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
			if (this.Dock != DockStyle.None)
				this.AutoSize = false;
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Invalidate();
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.Invalidate();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.ShortcutsEnabled && this.AllowZoom)
				this.ProcessScrollingShortcuts(e);
		}


		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			_isMouseDown = true;
		}


		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_isMouseDown = false;

			if (this.Focused)
				this.OnLostFocus(e);
		}




		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);


			if (e.Button == MouseButtons.Left)
			{
				if (SelectEnable)
				{
					float pixelSize;
					pixelSize = (float)this.ZoomFactor;
					if (pixelSize > 2)
					{
						if (!IsPointInImage(e.Location))
							return;
						_selectPixelPosition = PointToImage(e.Location);
						OnCellSelected(new CellEventArgs(this.ColCount, this.RowCount, this.BlockColCount, this.BlockRowCount, _selectPixelPosition.X, _selectPixelPosition.Y, "", StripStatus[_selectPixelPosition.Y * ColCount + _selectPixelPosition.X]));
						_selectPixelPosition = new Point(_selectPixelPosition.X + 1, _selectPixelPosition.Y + 1);
						this.Invalidate();
					}
				}
				else
					_selectPixelPosition = Point.Empty;
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);


			if (e.Button == MouseButtons.Left)
			{
				if (DoubleSelectEnable)
				{
					float pixelSize;
					pixelSize = (float)this.ZoomFactor;
					if (pixelSize > 2)
					{
						if (!IsPointInImage(e.Location))
							return;
						_selectPixelPosition = PointToImage(e.Location);
						OnCellDSelected(new CellEventArgs(this.ColCount, this.RowCount, this.BlockColCount, this.BlockRowCount, _selectPixelPosition.X, _selectPixelPosition.Y, "", StripStatus[_selectPixelPosition.Y * ColCount + _selectPixelPosition.X]));
						_selectPixelPosition = new Point(_selectPixelPosition.X + 1, _selectPixelPosition.Y + 1);
						this.Invalidate();
					}
				}
				else
					_selectPixelPosition = Point.Empty;
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (!this.Focused)
				this.Focus();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.Right)
			{
				this.ProcessPanning(e);
				this.ProcessSelection(e);
			}

			if (ActiveEnable || (e.Button == MouseButtons.Left && MouseMoveChangeEnable && SelectEnable && _isMouseDown))
			{
				float pixelSize;
				pixelSize = (float)this.ZoomFactor;
				if (pixelSize > 2)
				{
					if (!IsPointInImage(e.Location))
					{
						_activePixelPosition = Point.Empty;
						return;
					}
					Point actpt = PointToImage(e.Location);
					if (actpt.X != _activePixelPosition.X || actpt.Y != _activePixelPosition.Y)
					{
						if (ActiveEnable)
						{
							OnCellActived(new CellEventArgs(this.ColCount, this.RowCount, this.BlockColCount, this.BlockRowCount, actpt.X, actpt.Y, "", StripStatus[actpt.Y * ColCount + actpt.X]));
							_activePixelPosition = new Point(actpt.X + 1, actpt.Y + 1);
							this.Invalidate();
						}

						if (e.Button == MouseButtons.Left && MouseMoveChangeEnable && SelectEnable && _isMouseDown)
						{
							OnCellSelected(new CellEventArgs(this.ColCount, this.RowCount, this.BlockColCount, this.BlockRowCount, actpt.X, actpt.Y, "", StripStatus[actpt.Y * ColCount + actpt.X]));
							_selectPixelPosition = new Point(actpt.X + 1, actpt.Y + 1);
							this.Invalidate();
						}
					}
				}
			}
			else
				_activePixelPosition = Point.Empty;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			bool doNotProcessClick;
			base.OnMouseUp(e);
			_isMouseDown = false;

			doNotProcessClick = this.IsPanning || this.IsSelecting;
			if (this.IsPanning)
				this.IsPanning = false;
			if (this.IsSelecting)
				this.EndDrag();
			this.WasDragCancelled = false;


			if (MouseMode == eMouseMode.ZoomClick)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (ModifierKeys == Keys.None)
						this.ProcessMouseZoom(true, e.Location);
					else
						this.ProcessMouseZoom(false, e.Location);
				}
			}

			if (MouseMode == eMouseMode.Pan)
			{
				if (ModifierKeys == Keys.Control)
				{
					ZoomToFit();
					Invalidate();
				}
			}

			//if (!doNotProcessClick && this.AllowZoom && this.AllowClickZoom && !this.IsPanning && this.SizeMode == ImageBoxSizeMode.Normal)
			//{
			//    if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None)
			//        this.ProcessMouseZoom(true, e.Location);
			//    else if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && ModifierKeys != Keys.None))
			//        this.ProcessMouseZoom(false, e.Location);
			//}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.AllowZoom)
			{
				int spins;
				spins = Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta);
				for (int i = 0; i < spins; i++)
					this.ProcessMouseZoom(e.Delta > 0, e.Location);
			}
		}

		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.AdjustLayout();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.AllowPainting)
			{
				Rectangle innerRectangle;
				innerRectangle = this.GetInsideViewPort(false);
				using (SolidBrush brush = new SolidBrush(this.BackColor))
				{
					e.Graphics.FillRectangle(brush, innerRectangle);
				}

				if (_image != null)
					this.DrawImage(e.Graphics);

				if (this.ShowPixelGrid)
				{
					this.DrawPixelGrid(e.Graphics);
					this.DrawCellNumber(e.Graphics);
				}

				if (this.ActiveEnable)
					DrawActiveCell(e.Graphics, _activePixelPosition);

				if (this.SelectEnable || this.DoubleSelectEnable)
					DrawSelectCell(e.Graphics, _selectPixelPosition);

				if (this.SelectionRegion != Rectangle.Empty)
					this.DrawSelection(e);

				base.OnPaint(e);
			}
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			this.AdjustLayout();
		}

		protected override void OnResize(EventArgs e)
		{
			this.AdjustLayout();
			base.OnResize(e);
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			this.Invalidate();
			base.OnScroll(se);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.Invalidate();
		}

		protected override bool IsInputKey(Keys keyData)
		{
			bool result;
			if ((keyData & Keys.Right) == Keys.Right | (keyData & Keys.Left) == Keys.Left | (keyData & Keys.Up) == Keys.Up | (keyData & Keys.Down) == Keys.Down)
				result = true;
			else
				result = base.IsInputKey(keyData);
			return result;
		}

		protected override Point ScrollToControl(Control activeControl)
		{
			return this.DisplayRectangle.Location;
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size size;

			if (!this.ViewSize.IsEmpty)
			{
				int width;
				int height;
				width = this.ScaledImageWidth;
				height = this.ScaledImageHeight;
				width += this.Padding.Horizontal;
				height += this.Padding.Vertical;
				size = new Size(width, height);
			}
			else
				size = base.GetPreferredSize(proposedSize);
			return size;
		}
		#endregion

		#region Overridden 속성둘..

		[DefaultValue(true)]
		public override bool AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
			set
			{
				if (base.AutoSize != value)
				{
					base.AutoSize = value;
					this.AdjustLayout();
				}
			}
		}

		[DefaultValue(typeof(Color), "White")]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		#endregion

		#region Private  함수둘..
		private Size GetImageSize()
		{
			Size result;
			if (_image != null)
			{
				try
				{
					result = _image.Size;
				}
				catch
				{
					result = Size.Empty;
				}
			}
			else
			{
				result = Size.Empty;
			}

			return result;
		}

		private void OnFrameChangedHandler(object sender, EventArgs eventArgs)
		{
			this.Invalidate();
		}

		private void PerformActualSize(ImageBoxActionSources source)
		{
			this.SetZoom(100, ImageBoxZoomActions.ActualSize | (this.Zoom < 100 ? ImageBoxZoomActions.ZoomIn : ImageBoxZoomActions.ZoomOut), source);
		}

		private void PerformZoomIn(ImageBoxActionSources source)
		{
			this.SetZoom(this.ZoomLevels.NextZoom(this.Zoom), ImageBoxZoomActions.ZoomIn, source);
		}

		private void PerformZoomOut(ImageBoxActionSources source)
		{
			this.SetZoom(this.ZoomLevels.PreviousZoom(this.Zoom), ImageBoxZoomActions.ZoomOut, source);
		}

		private void SetZoom(int value, ImageBoxZoomActions actions, ImageBoxActionSources source)
		{
			int previousZoom;
			previousZoom = this.Zoom;
			if (value < MinZoom)
				value = MinZoom;
			else if (value > MaxZoom)
				value = MaxZoom;

			if (_zoom != value)
			{
				_zoom = value;
				this.OnZoomChanged(EventArgs.Empty);
			}
		}
		#endregion

		#region Protected 함수들..

		protected void AdjustLayout()
		{
			if (this.AllowPainting)
			{
				if (this.AutoSize)
				{
					this.AdjustSize();
				}
				//else if (this.SizeMode != ImageBoxSizeMode.Normal)
				//{
				//  this.ZoomToFit();
				//}
				else if (this.AutoScroll)
				{
					this.AdjustViewPort();
				}

				this.Invalidate();
			}
		}

		protected void AdjustScroll(int x, int y)
		{
			Point scrollPosition;

			scrollPosition = new Point(this.HorizontalScroll.Value + x, this.VerticalScroll.Value + y);

			this.UpdateScrollPosition(scrollPosition);
		}

		protected void AdjustSize()
		{
			if (this.AutoSize && this.Dock == DockStyle.None)
			{
				base.Size = base.PreferredSize;
			}
		}

		protected void AdjustViewPort()
		{
			if (this.AutoScroll && !this.ViewSize.IsEmpty)
			{
				this.AutoScrollMinSize = new Size(this.ScaledImageWidth + Padding.Horizontal, this.ScaledImageHeight + Padding.Vertical);
			}
		}

		protected void DrawImage(Graphics g)
		{
			InterpolationMode currentInterpolationMode;
			PixelOffsetMode currentPixelOffsetMode;

			currentInterpolationMode = g.InterpolationMode;
			currentPixelOffsetMode = g.PixelOffsetMode;

			g.InterpolationMode = this.InterpolationMode;

			// disable pixel offsets. Thanks to Rotem for the info.
			// http://stackoverflow.com/questions/14070311/why-is-graphics-drawimage-cropping-part-of-my-image/14070372#14070372
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			try
			{
				g.DrawImage(_image, this.GetImageViewPort(), this.GetSourceImageRegion(), GraphicsUnit.Pixel);
			}
			catch (ArgumentException)
			{
				// ignore errors that occur due to the image being disposed
			}
			catch (OutOfMemoryException)
			{
				// also ignore errors that occur due to running out of memory
			}

			g.PixelOffsetMode = currentPixelOffsetMode;
			g.InterpolationMode = currentInterpolationMode;
		}

		protected void DrawLabel(Graphics graphics, string text, Rectangle bounds)
		{
			this.DrawLabel(graphics, text, this.Font, this.ForeColor, this.TextBackColor, this.TextAlign, bounds);
		}

		protected void DrawLabel(Graphics graphics, string text, Font font, Rectangle bounds)
		{
			this.DrawLabel(graphics, text, font, this.ForeColor, this.TextBackColor, this.TextAlign, bounds);
		}

		protected void DrawLabel(Graphics graphics, string text, Font font, Color foreColor, Rectangle bounds)
		{
			this.DrawLabel(graphics, text, font, foreColor, this.TextBackColor, this.TextAlign, bounds);
		}

		protected void DrawLabel(Graphics graphics, string text, Font font, Color foreColor, Color backColor, Rectangle bounds)
		{
			this.DrawLabel(graphics, text, font, foreColor, backColor, this.TextAlign, bounds);
		}

		protected void DrawLabel(Graphics graphics, string text, Font font, Color foreColor, Color backColor, ContentAlignment textAlign, Rectangle bounds)
		{
			TextFormatFlags flags;
			flags = TextFormatFlags.NoPrefix | TextFormatFlags.WordEllipsis | TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;

			switch (textAlign)
			{
				case ContentAlignment.TopLeft:
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.BottomLeft:
					flags |= TextFormatFlags.Left;
					break;
				case ContentAlignment.TopRight:
				case ContentAlignment.MiddleRight:
				case ContentAlignment.BottomRight:
					flags |= TextFormatFlags.Right;
					break;
				default:
					flags |= TextFormatFlags.HorizontalCenter;
					break;
			}

			switch (textAlign)
			{
				case ContentAlignment.TopCenter:
				case ContentAlignment.TopLeft:
				case ContentAlignment.TopRight:
					flags |= TextFormatFlags.Top;
					break;
				case ContentAlignment.BottomCenter:
				case ContentAlignment.BottomLeft:
				case ContentAlignment.BottomRight:
					flags |= TextFormatFlags.Bottom;
					break;
				default:
					flags |= TextFormatFlags.VerticalCenter;
					break;
			}

			TextRenderer.DrawText(graphics, text, font, bounds, foreColor, backColor, flags);
		}

		protected void DrawPixelGrid(Graphics g)
		{
			float pixelSize;

			pixelSize = (float)this.ZoomFactor;

			if (pixelSize > this.PixelGridThreshold)
			{
				Rectangle viewport;
				float offsetX;
				float offsetY;
				Point asp = this.AutoScrollPosition;

				viewport = this.GetImageViewPort();
				offsetX = Math.Abs(asp.X) % pixelSize;
				offsetY = Math.Abs(asp.Y) % pixelSize;


				Color gridcolor = Color.FromArgb(128, this.PixelGridColor);
				Color gridcolorB = Color.FromArgb(128, this.PixelGridBlockColor);
				Color gridcolorC = Color.FromArgb(255, this.PixelGridBlockColor);

				using (Pen pen1 = new Pen(gridcolor) { DashStyle = DashStyle.Solid },
						   pen2 = new Pen(gridcolorB) { DashStyle = DashStyle.Solid },
						   pen3 = new Pen(gridcolorC, 2) { DashStyle = DashStyle.Solid })
				{
					float fx, fy;
					int ix, iy;
					for (float x = (float)viewport.Left - offsetX; x <= (float)viewport.Right + this.PixelGridThreshold / 2.0F; x += pixelSize)
					{
						if (asp != Point.Empty)
							fx = (float)(x - asp.X);
						else
							fx = (float)(x);
						ix = (int)Math.Round((fx - (float)viewport.X) / pixelSize);

						if (ix < 0 || ix > ColCount)
							continue;
						if (x < viewport.Left)
							continue;

						if ((ix % BlockColCount) == 0)
						{
							if (ix == 0 || ix == ColCount)
								g.DrawLine(pen3, x, (float)viewport.Top, x, (float)viewport.Bottom);
							else
								g.DrawLine(pen2, x, (float)viewport.Top, x, (float)viewport.Bottom);
						}
						else
							g.DrawLine(pen1, x, (float)viewport.Top, x, (float)viewport.Bottom);
					}

					for (float y = (float)viewport.Top - offsetY; y <= (float)viewport.Bottom + this.PixelGridThreshold / 2.0F; y += pixelSize)
					{
						if (asp != Point.Empty)
							fy = (float)(y - asp.Y);
						else
							fy = (float)y;
						iy = (int)Math.Round((fy - (float)viewport.Y) / pixelSize);

						if (iy < 0 || iy > RowCount)
							continue;

						if (y < viewport.Top)
							continue;

						if ((iy % BlockRowCount) == 0)
						{
							if (iy == 0 || iy == RowCount)
								g.DrawLine(pen3, (float)viewport.Left, y, (float)viewport.Right, y);
							else
								g.DrawLine(pen2, (float)viewport.Left, y, (float)viewport.Right, y);
						}
						else
							g.DrawLine(pen1, (float)viewport.Left, y, (float)viewport.Right, y);
					}
				}
			}
		}

		protected void DrawActiveCell(Graphics g, Point mpt)
		{
			float pixelSize;
			pixelSize = (float)this.ZoomFactor;
			if (pixelSize > 2 && mpt != Point.Empty)
			{
				Point pt = new Point(mpt.X - 1, mpt.Y - 1);
				Rectangle rect = GetPixelToRect(pt);


				if (!IsPointInImage(new Point(rect.Left, rect.Top)) ||
					!IsPointInImage(new Point(rect.Left, rect.Bottom - 1)) ||
					!IsPointInImage(new Point(rect.Right - 1, rect.Top)) ||
					!IsPointInImage(new Point(rect.Right - 1, rect.Bottom - 1)))
					return;
				Pen ActPen = new Pen(this.MouseActiveColor, 2.0F);
				ActPen.DashStyle = DashStyle.Solid;
				g.DrawRectangle(ActPen, rect);
			}
		}

		protected void DrawSelectCell(Graphics g, Point mpt)
		{
			float pixelSize;
			pixelSize = (float)this.ZoomFactor;
			if (pixelSize > 2 && mpt != Point.Empty)
			{
				Point pt = new Point(mpt.X - 1, mpt.Y - 1);
				Rectangle rect = GetPixelToRect(pt);


				if (!IsPointInImage(new Point(rect.Left, rect.Top)) ||
					!IsPointInImage(new Point(rect.Left, rect.Bottom - 1)) ||
					!IsPointInImage(new Point(rect.Right - 1, rect.Top)) ||
					!IsPointInImage(new Point(rect.Right - 1, rect.Bottom - 1)))
					return;
				Pen SelPen = new Pen(this.MouseSelectedColor, 2.0F);
				SelPen.DashStyle = DashStyle.Solid;
				g.DrawRectangle(SelPen, rect);
			}
		}

		protected Rectangle GetPixelToRect(Point PixelPt)
		{
			float pixelSize = (float)this.ZoomFactor;
			Rectangle viewport;
			viewport = this.GetImageViewPort();
			float x = PixelPt.X * pixelSize + viewport.X + this.AutoScrollPosition.X;
			float y = PixelPt.Y * pixelSize + viewport.Y + this.AutoScrollPosition.Y;
			return new Rectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(pixelSize), (int)Math.Round(pixelSize));
		}

		protected void DrawCellNumber(Graphics g)
		{
			Rectangle bounds;
			float pixelSize = (float)this.ZoomFactor;

			if (pixelSize > this.PixelNumberThreshold)
			{
				Rectangle viewport;
				float offsetX;
				float offsetY;
				Point asp = this.AutoScrollPosition;

				viewport = this.GetImageViewPort();
				offsetX = Math.Abs(asp.X) % pixelSize;
				offsetY = Math.Abs(asp.Y) % pixelSize;

				float fx, fy, x, y;
				int ix, iy;
				string txt;
				int idx, segidx, segcol, segrow;
				for (x = (float)viewport.Left - offsetX; x < (float)viewport.Right ; x += pixelSize)
				{
					if (asp != Point.Empty)
						fx = (float)(x - asp.X);
					else
						fx = (float)x;
					ix = (int)Math.Round((fx - (float)viewport.X) / pixelSize);
					if ((ix < 0 || ix > ColCount) || (x < viewport.Left))
						continue;

					for (y = (float)viewport.Top - offsetY; y < (float)viewport.Bottom ; y += pixelSize)
					{
						if (asp != Point.Empty)
							fy = (float)(y - asp.Y);
						else
							fy = (float)y;

						iy = (int)Math.Round((fy - (float)viewport.Y) / pixelSize);
						if ((iy < 0 || iy > RowCount) || (y < viewport.Top))
							continue;
						bounds = new Rectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(pixelSize), (int)Math.Round(pixelSize));


						txt = "";
						idx = ix + iy * ColCount;
						segcol = ((int)((idx % ColCount) / BlockColCount));
						segrow = ((int)((int)((idx / ColCount) / BlockRowCount)));
						segidx = segrow * ((int)(ColCount / BlockColCount)) + segcol;

						if (GroupNumVisible)
						{
							if (this.Display2GroupNumIndex)
								txt = (segcol + 1).ToString() + "-" + (segrow + 1).ToString() + Environment.NewLine;
							else
							{
								if (ColCount * RowCount > idx)
									txt = ((int)_stripStatus[idx]).ToString() + Environment.NewLine;  // (segidx+1).ToString()
							}
						}

						if (CellNumVisible)
						{
							if (this.Display2CellNumIndex)
								txt += (idx + 1).ToString();
							else
								txt += (ix + 1).ToString() + "-" + (iy + 1).ToString();
						}

						if (CellTxtVisible && ColCount * RowCount > idx && _stripText[idx] != null)
						{
							txt += _stripText[idx];
						}
						this.DrawLabel(g, txt, this.Font, this.DisplayNumColor, this.TextBackColor, this.TextAlign, bounds);
					}
				}
			}
		}

		protected void DrawSelection(PaintEventArgs e)
		{
			RectangleF rect;

			e.Graphics.SetClip(this.GetInsideViewPort(true));

			rect = this.GetOffsetRectangle(this.SelectionRegion);

			using (Brush brush = new SolidBrush(Color.FromArgb(128, this.SelectionColor)))
			{
				e.Graphics.FillRectangle(brush, rect);
			}

			using (Pen pen = new Pen(this.SelectionColor))
			{
				e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
			}

			e.Graphics.ResetClip();
		}

		protected void EndDrag()
		{
			this.IsSelecting = false;
			this.OnSelected(EventArgs.Empty);
		}

		protected void OnAllowZoomChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.AllowZoomChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnInterpolationModeChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.InterpolationModeChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnLimitSelectionToImageChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.LimitSelectionToImageChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnPanEnd(EventArgs e)
		{
			EventHandler handler;

			handler = this.PanEnd;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnPanStart(CancelEventArgs e)
		{
			EventHandler handler;

			handler = this.PanStart;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnPixelGridColorChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.PixelGridColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnPixelGridThresholdChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.PixelGridThresholdChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnPixelNumberThresholdChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.PixelNumberThresholdChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnSelected(EventArgs e)
		{
			EventHandler<EventArgs> handler;

			switch (this.MouseMode)
			{
				case eMouseMode.ZoomRectangle:
					if (this.SelectionRegion.Width > SelectionDeadZone && this.SelectionRegion.Height > SelectionDeadZone)
					{
						this.ZoomToRegion(this.SelectionRegion);
						this.SelectNone();
					}
					break;
			}

			handler = this.Selected;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnSelecting(ImageBoxCancelEventArgs e)
		{
			EventHandler<ImageBoxCancelEventArgs> handler;

			handler = this.Selecting;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnSelectionColorChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.SelectionColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnSelectionModeChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.SelectionModeChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnSelectionRegionChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.SelectionRegionChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnShortcutsEnabledChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.ShortcutsEnabledChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnShowPixelGridChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.ShowPixelGridChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnTextAlignChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.TextAlignChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnTextBackColorChanged(EventArgs e)
		{
			EventHandler handler;

			this.Invalidate();

			handler = this.TextBackColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnZoomChanged(EventArgs e)
		{
			EventHandler handler;

			this.AdjustLayout();

			handler = this.ZoomChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnZoomLevelsChanged(EventArgs e)
		{
			EventHandler handler;

			handler = this.ZoomLevelsChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void ProcessImageShortcuts(KeyEventArgs e)
		{
			Point currentPixel;
			int currentZoom;
			Point relativePoint;

			relativePoint = this.CenterPoint;
			currentPixel = this.PointToImage(relativePoint);
			currentZoom = this.Zoom;

			if (this.Zoom != currentZoom)
			{
				this.ScrollTo(currentPixel, relativePoint);
			}
		}

		protected void ProcessMouseZoom(bool isZoomIn, Point cursorPosition)
		{
			Point currentPixel;
			int currentZoom;

			currentPixel = this.PointToImage(cursorPosition);
			currentZoom = this.Zoom;

			this.SetZoom(isZoomIn ? this.ZoomLevels.NextZoom(this.Zoom) : this.ZoomLevels.PreviousZoom(this.Zoom), isZoomIn ? ImageBoxZoomActions.ZoomIn : ImageBoxZoomActions.ZoomOut, ImageBoxActionSources.User);

			if (this.Zoom != currentZoom)
			{
				this.ScrollTo(currentPixel, cursorPosition);
			}
		}

		protected void ProcessPanning(MouseEventArgs e)
		{
			if (this._mouseMode == eMouseMode.Pan && !this.ViewSize.IsEmpty)
			{
				if (!this.IsPanning && (this.HScroll | this.VScroll))
				{
					_startMousePosition = e.Location;
					this.IsPanning = true;
				}

				if (this.IsPanning)
				{
					int x;
					int y;
					Point position;
					x = -_startScrollPosition.X + (_startMousePosition.X - e.Location.X);
					y = -_startScrollPosition.Y + (_startMousePosition.Y - e.Location.Y);
					position = new Point(x, y);
					this.UpdateScrollPosition(position);
				}
			}
		}

		protected void ProcessScrollingShortcuts(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Left:
					this.AdjustScroll(-(e.Modifiers == Keys.None ? this.HorizontalScroll.SmallChange : this.HorizontalScroll.LargeChange), 0);
					break;

				case Keys.Right:
					this.AdjustScroll(e.Modifiers == Keys.None ? this.HorizontalScroll.SmallChange : this.HorizontalScroll.LargeChange, 0);
					break;

				case Keys.Up:
					this.AdjustScroll(0, -(e.Modifiers == Keys.None ? this.VerticalScroll.SmallChange : this.VerticalScroll.LargeChange));
					break;

				case Keys.Down:
					this.AdjustScroll(0, e.Modifiers == Keys.None ? this.VerticalScroll.SmallChange : this.VerticalScroll.LargeChange);
					break;
			}
		}

		protected void ProcessSelection(MouseEventArgs e)
		{
			if (this.MouseMode == eMouseMode.ZoomRectangle && e.Button == MouseButtons.Left && !this.WasDragCancelled)
			{
				if (!this.IsSelecting)
				{
					this.StartDrag(e);
				}

				if (this.IsSelecting)
				{
					float x;
					float y;
					float w;
					float h;
					Point imageOffset;
					RectangleF selection;

					imageOffset = this.GetImageViewPort().Location;

					if (e.X < _startMousePosition.X)
					{
						x = e.X;
						w = _startMousePosition.X - e.X;
					}
					else
					{
						x = _startMousePosition.X;
						w = e.X - _startMousePosition.X;
					}

					if (e.Y < _startMousePosition.Y)
					{
						y = e.Y;
						h = _startMousePosition.Y - e.Y;
					}
					else
					{
						y = _startMousePosition.Y;
						h = e.Y - _startMousePosition.Y;
					}

					x = x - imageOffset.X - this.AutoScrollPosition.X;
					y = y - imageOffset.Y - this.AutoScrollPosition.Y;

					x = x / (float)this.ZoomFactor;
					y = y / (float)this.ZoomFactor;
					w = w / (float)this.ZoomFactor;
					h = h / (float)this.ZoomFactor;

					selection = new RectangleF(x, y, w, h);
					if (this.LimitSelectionToImage)
					{
						selection = this.FitRectangle(selection);
					}

					this.SelectionRegion = selection;
				}
			}
		}

		protected void StartDrag(MouseEventArgs e)
		{
			ImageBoxCancelEventArgs args;
			args = new ImageBoxCancelEventArgs(e.Location);
			this.OnSelecting(args);
			this.WasDragCancelled = args.Cancel;
			this.IsSelecting = !args.Cancel;
			if (this.IsSelecting)
			{
				this.SelectNone();
				_startMousePosition = e.Location;
			}
		}

		protected void UpdateScrollPosition(Point position)
		{
			this.AutoScrollPosition = position;
			this.Invalidate();
			this.OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, 0));
		}

		//[Category("Property Changed")]
		//public event EventHandler<CellEventArgs> CellSelected;

		//[Category("Property Changed")]
		//public event EventHandler<CellEventArgs> CellDSelected;

		//[Category("Property Changed")]
		//public event EventHandler<CellEventArgs> CellActived;


		protected void OnCellSelected(CellEventArgs e)
		{
			EventHandler<CellEventArgs> handler;
			handler = this.CellSelected;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnCellDSelected(CellEventArgs e)
		{
			EventHandler<CellEventArgs> handler;
			handler = this.CellDSelected;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void OnCellActived(CellEventArgs e)
		{
			EventHandler<CellEventArgs> handler;
			handler = this.CellActived;

			if (handler != null)
			{
				handler(this, e);
			}
		}
		#endregion

		#region Protected 속성들..

		protected bool AllowPainting
		{
			get { return _updateCount == 0; }
		}

		protected bool WasDragCancelled { get; set; }

		protected int ScaledImageHeight
		{
			get { return Convert.ToInt32(this.ViewSize.Height * this.ZoomFactor); }
		}

		protected int ScaledImageWidth
		{
			get { return Convert.ToInt32(this.ViewSize.Width * this.ZoomFactor); }
		}

		protected Size ViewSize
		{
			get { return this.GetImageSize(); }
		}

		#endregion

		#region Public 함수들..

		public void SetStatusAll(eCellStatus status)
		{
			int i, j;
			for (i = 0; i < ColCount; i++)
			{
				for (j = 0; j < RowCount; j++)
				{
					_stripStatus[j * ColCount + i] = status;
				}
			}
			ApplyStripInfo();
		}

		public void SetValueClear()
		{
			int i, j;
			for (i = 0; i < ColCount; i++)
			{
				for (j = 0; j < RowCount; j++)
				{
					_stripXValue	[j * ColCount + i] = 0.0;
					_stripYValue	[j * ColCount + i] = 0.0;
					_stripZValue	[j * ColCount + i] = 0.0;
					_stripEncXValue	[j * ColCount + i] = 0.0;
					_stripEncYValue	[j * ColCount + i] = 0.0;
					_stripEncZValue	[j * ColCount + i] = 0.0;
				}
			}

		}


		public void SetStatus(int index, eCellStatus sts_value, bool paint)
		{
			_stripStatus[index] = sts_value;
			if (paint)
				((Bitmap)_image).SetPixel(index % ColCount, (int)(index / ColCount), ColorStatus[(int)sts_value]);
		}

		public void SetStatus(int colidx, int rowidx, eCellStatus sts_value, bool paint)
		{
			_stripStatus[rowidx * ColCount + colidx] = sts_value;
			if (paint)
				((Bitmap)_image).SetPixel(colidx, rowidx, _colorStatus[(int)sts_value]);
			this.Invalidate();
		}

		public void SetStatus(int segcolidx, int segrowidx, int cellcolidx, int cellrowidx, eCellStatus sts_value, bool paint)
		{
			_stripStatus[(segrowidx * BlockRowCount + cellrowidx) * ColCount + segcolidx * BlockColCount + cellcolidx] = sts_value;
			if (paint)
				((Bitmap)_image).SetPixel(segcolidx * BlockColCount + cellcolidx, segrowidx * BlockRowCount + cellrowidx, _colorStatus[(int)sts_value]);
		}

		public void SetText(int index, string text)
		{
			_stripText[index] = text;
		}

		public void SetText(int colidx, int rowidx, string text)
		{
			_stripText[rowidx * ColCount + colidx] = text;
		}
		public void SetValue(int colidx, int rowidx, double dVX, double dVY, double dVZ, double dMX, double dMY, double dMZ)
		{
			_stripXValue	[rowidx * ColCount + colidx] = dVX;
			_stripYValue	[rowidx * ColCount + colidx] = dVY;
			_stripZValue	[rowidx * ColCount + colidx] = dVZ;

			_stripEncXValue	[rowidx * ColCount + colidx] = dMX;
			_stripEncYValue	[rowidx * ColCount + colidx] = dMY;
			_stripEncZValue	[rowidx * ColCount + colidx] = dMZ;
		}

		public void SetValue_NewOffset(int colidx, int rowidx, double dVX, double dVY, double dVZ)
		{
			_stripXValue[rowidx * ColCount + colidx] = dVX;
			_stripYValue[rowidx * ColCount + colidx] = dVY;
			_stripZValue[rowidx * ColCount + colidx] = dVZ;
		}
		public double GetVXValue(int colidx, int rowidx)
		{
			if(colidx >= ColCount) return 0.0;
			if(rowidx >= RowCount) return 0.0;
			return _stripXValue[rowidx * ColCount + colidx];
		}
		public double GetVYValue(int colidx, int rowidx)
		{
			if (colidx >= ColCount) return 0.0;
			if (rowidx >= RowCount) return 0.0;
			return _stripYValue[rowidx * ColCount + colidx];
		}
		public double GetVZValue(int colidx, int rowidx)
		{
			if (colidx >= ColCount) return 0.0;
			if (rowidx >= RowCount) return 0.0;
			return _stripZValue[rowidx * ColCount + colidx];
		}

		public double GetMXValue(int colidx, int rowidx)
		{
			if (colidx >= ColCount) return 0.0;
			if (rowidx >= RowCount) return 0.0;
			return _stripEncXValue[rowidx * ColCount + colidx];
		}
		public double GetMYValue(int colidx, int rowidx)
		{
			if (colidx >= ColCount) return 0.0;
			if (rowidx >= RowCount) return 0.0;
			return _stripEncYValue[rowidx * ColCount + colidx];
		}
		public double GetMZValue(int colidx, int rowidx)
		{
			if (colidx >= ColCount) return 0.0;
			if (rowidx >= RowCount) return 0.0;
			return _stripEncZValue[rowidx * ColCount + colidx];
		}

		public eCellStatus GetStatus(int index)
		{
			return _stripStatus[index];
		}
		public eCellStatus GetStatus(int colidx, int rowidx)
		{
			return _stripStatus[rowidx * ColCount + colidx];
		}

		public void SetStatusColor(byte idx, Color color)
		{
			_colorStatus[idx] = color;
		}
		public void ReSizeCount()
		{
			if(_image != null)
				_image.Dispose();
			if(_stripStatus != null) _stripStatus = null;
			if(_rankDatas   != null) _rankDatas	  = null;
			if(_stripText   != null) _stripText	  = null;
			if(_stripXValue != null) _stripXValue = null;
			if(_stripYValue != null) _stripYValue = null;
			if(_stripZValue != null) _stripZValue = null;

			_image = new Bitmap(ColCount, RowCount);

			Graphics gra = Graphics.FromImage(_image);
			gra.FillRectangle(new SolidBrush(Color.Blue), 0, 0, _image.Width, _image.Height);
			_stripStatus	= new eCellStatus	[ColCount * RowCount];
			_rankDatas		= new int			[ColCount * RowCount];
			_stripText		= new string		[ColCount * RowCount];
			_stripXValue	= new double		[ColCount * RowCount];
			_stripYValue	= new double		[ColCount * RowCount];
			_stripZValue	= new double		[ColCount * RowCount];
			SetStatusAll(eCellStatus.Empty);
			SetValueClear();
		}
		public void ApplyStripInfo()
		{
			for (int i = 0; i < ColCount * RowCount; i++)
				_rankDatas[i] = _colorStatus[(int)_stripStatus[i]].ToArgb();

			BitmapData bd = ((Bitmap)_image).LockBits(new Rectangle(0, 0, ColCount, RowCount), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Marshal.Copy(_rankDatas, 0, bd.Scan0, ColCount * RowCount);
			((Bitmap)_image).UnlockBits(bd);


			for(int i=0; i <ColCount; i++)
			{
			    for(int j=0; j < RowCount; j++)
			    {
			        ((Bitmap)_image).SetPixel(i,j,ColorStatus[(int)_stripStatus[j * ColCount + i]]);
			    }
			}
			Refresh();
		}

		public void BeginUpdate()
		{
			_updateCount++;
		}

		public void CenterAt(Point imageLocation)
		{
			this.ScrollTo(imageLocation, new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2));
		}

		public void CenterAt(int x, int y)
		{
			this.CenterAt(new Point(x, y));
		}

		public void CenterAt(float x, float y)
		{
			this.CenterAt(new Point((int)x, (int)y));
		}

		public void CenterToImage()
		{
			this.AutoScrollPosition = new Point((this.AutoScrollMinSize.Width - this.ClientSize.Width) / 2, (this.AutoScrollMinSize.Height - this.ClientSize.Height) / 2);
		}

		public void EndUpdate()
		{
			if (_updateCount > 0)
			{
				_updateCount--;
			}

			if (this.AllowPainting)
			{
				this.Invalidate();
			}
		}

		public void ScrollTo(int x, int y, int relativeX, int relativeY)
		{
			this.ScrollTo(new Point(x, y), new Point(relativeX, relativeY));
		}

		public void ScrollTo(float x, float y, float relativeX, float relativeY)
		{
			this.ScrollTo(new Point((int)x, (int)y), new Point((int)relativeX, (int)relativeY));
		}

		public void ScrollTo(Point imageLocation, Point relativeDisplayPoint)
		{
			int x;
			int y;

			x = (int)(imageLocation.X * ZoomFactor) - relativeDisplayPoint.X;
			y = (int)(imageLocation.Y * ZoomFactor) - relativeDisplayPoint.Y;

			this.AutoScrollPosition = new Point(x + Padding.Left, y + Padding.Top);
		}

		public void SelectAll()
		{
			this.SelectionRegion = new RectangleF(PointF.Empty, this.ViewSize);
		}

		public void SelectNone()
		{
			this.SelectionRegion = RectangleF.Empty;
		}

		public void ZoomIn()
		{
			this.PerformZoomIn(ImageBoxActionSources.Unknown);
		}

		public void ZoomOut()
		{
			this.PerformZoomOut(ImageBoxActionSources.Unknown);
		}

		public void ZoomToFit()
		{
			if (!this.ViewSize.IsEmpty)
			{
				this.Zoom = 1;

				Rectangle innerRectangle;
				double zoom;
				double aspectRatio;


				this.AutoScrollMinSize = Size.Empty;

				innerRectangle = this.GetInsideViewPort(true);

				if (this.ViewSize.Width > this.ViewSize.Height)
				{
					aspectRatio = (double)((double)innerRectangle.Width / (double)this.ViewSize.Width);
					zoom = aspectRatio * 100.0;

					if ((double)innerRectangle.Height < ((double)(this.ViewSize.Height * zoom) / 100.0))
					{
						aspectRatio = (double)innerRectangle.Height / this.ViewSize.Height;
						zoom = aspectRatio * 100.0;
					}
				}
				else
				{
					aspectRatio = (double)((double)innerRectangle.Height / (double)this.ViewSize.Height);
					zoom = aspectRatio * 100.0;

					if (innerRectangle.Width < ((double)(this.ViewSize.Width * zoom) / 100.0))
					{
						aspectRatio = (double)((double)innerRectangle.Width / (double)this.ViewSize.Width);
						zoom = aspectRatio * 100.0;
					}
				}

				this.Zoom = (int)Math.Round(Math.Floor(zoom));
				Invalidate();
			}
		}

		public void ZoomToRegion(float x, float y, float width, float height)
		{
			this.ZoomToRegion(new RectangleF(x, y, width, height));
		}

		public void ZoomToRegion(int x, int y, int width, int height)
		{
			this.ZoomToRegion(new RectangleF(x, y, width, height));
		}

		public void ZoomToRegion(RectangleF rectangle)
		{
			double ratioX;
			double ratioY;
			double zoomFactor;
			int cx;
			int cy;

			ratioX = this.ClientSize.Width / rectangle.Width;
			ratioY = this.ClientSize.Height / rectangle.Height;
			zoomFactor = Math.Min(ratioX, ratioY);
			cx = (int)(rectangle.X + (rectangle.Width / 2));
			cy = (int)(rectangle.Y + (rectangle.Height / 2));

			this.Zoom = (int)(zoomFactor * 100);
			this.CenterAt(new Point(cx, cy));
		}

		public bool GetIdxToXY(int idx, out int col, out int row)
		{
			col = 0; row = 0;
			if (idx < 0 && idx >= (RowCount * ColCount))
				return false;
			col = idx % ColCount;
			row = (int)(idx / ColCount);
			return true;
		}

		public bool GetIdxToXY(int idx, out int scol, out int srow, out int ccol, out int crow)
		{
			scol = 0; srow = 0; ccol = 0; crow = 0;
			if (idx < 0 && idx >= (RowCount * ColCount))
				return false;
			scol = (int)((idx % ColCount) / BlockColCount);
			srow = (int)((int)(idx / ColCount) / BlockRowCount);
			ccol = (idx % ColCount) % BlockColCount;
			crow = (int)(idx / ColCount) % BlockRowCount;


			return true;
		}

		public bool GetXYToIdx(int col, int row, out int idx)
		{
			idx = 0;
			if ((col < 0 && col >= ColCount) || (row < 0 && row >= RowCount))
				return false;
			idx = col + row * ColCount;
			return true;
		}

		public bool GetXYToIdx(int scol, int srow, int ccol, int crow, out int idx)
		{
			idx = (scol * BlockColCount + ccol) + (srow * BlockRowCount + crow) * ColCount;
			if (idx < 0 && idx >= (RowCount * ColCount))
			{
				idx = 0;
				return false;
			}
			return true;
		}

		public bool IsPointInImage(Point point)
		{
			return this.GetImageViewPort().Contains(point);
		}

		public bool IsPointInImage(int x, int y)
		{
			return this.IsPointInImage(new Point(x, y));
		}

		public bool IsPointInImage(float x, float y)
		{
			return this.IsPointInImage(new Point((int)x, (int)y));
		}

		public Point PointToImage(Point point)
		{
			return this.PointToImage(point, false);
		}


		//
		public Point PointToImage(float x, float y)
		{
			return this.PointToImage(x, y, false);
		}

		public Point PointToImage(float x, float y, bool fitToBounds)
		{
			return this.PointToImage(new Point((int)x, (int)y), fitToBounds);
		}

		public Point PointToImage(int x, int y)
		{
			return this.PointToImage(x, y, false);
		}

		public Point PointToImage(int x, int y, bool fitToBounds)
		{
			return this.PointToImage(new Point(x, y), fitToBounds);
		}

		public Point PointToImage(Point point, bool fitToBounds)
		{
			Rectangle viewport;
			int x;
			int y;

			viewport = this.GetImageViewPort();

			if (viewport.Contains(point) || fitToBounds)
			{
				if (this.AutoScrollPosition != Point.Empty)
				{
					point = new Point(point.X - this.AutoScrollPosition.X, point.Y - this.AutoScrollPosition.Y);
				}

				x = (int)((point.X - viewport.X) / this.ZoomFactor);
				y = (int)((point.Y - viewport.Y) / this.ZoomFactor);

				if (x < 0)
				{
					x = 0;
				}
				else if (x > this.ViewSize.Width)
				{
					x = this.ViewSize.Width;
				}

				if (y < 0)
				{
					y = 0;
				}
				else if (y > this.ViewSize.Height)
				{
					y = this.ViewSize.Height;
				}
			}
			else
			{
				x = 0; // Return Point.Empty if we couldn't match
				y = 0;
			}

			return new Point(x, y);
		}

		public Point GetOffsetPoint(Point source)
		{
			PointF offset;

			offset = this.GetOffsetPoint(new PointF(source.X, source.Y));

			return new Point((int)offset.X, (int)offset.Y);
		}

		public Point GetOffsetPoint(int x, int y)
		{
			return this.GetOffsetPoint(new Point(x, y));
		}

		public Point GetScaledPoint(int x, int y)
		{
			return this.GetScaledPoint(new Point(x, y));
		}

		public Point GetScaledPoint(Point source)
		{
			return new Point((int)(source.X * this.ZoomFactor), (int)(source.Y * this.ZoomFactor));
		}


		//
		public PointF GetOffsetPoint(float x, float y)
		{
			return this.GetOffsetPoint(new PointF(x, y));
		}

		public PointF GetOffsetPoint(PointF source)
		{
			Rectangle viewport;
			PointF scaled;
			int offsetX;
			int offsetY;

			viewport = this.GetImageViewPort();
			scaled = this.GetScaledPoint(source);
			offsetX = viewport.Left + this.Padding.Left + this.AutoScrollPosition.X;
			offsetY = viewport.Top + this.Padding.Top + this.AutoScrollPosition.Y;

			return new PointF(scaled.X + offsetX, scaled.Y + offsetY);
		}

		public PointF GetScaledPoint(float x, float y)
		{
			return this.GetScaledPoint(new PointF(x, y));
		}

		public PointF GetScaledPoint(PointF source)
		{
			return new PointF((float)(source.X * this.ZoomFactor), (float)(source.Y * this.ZoomFactor));
		}


		//
		public Size GetScaledSize(int width, int height)
		{
			return this.GetScaledSize(new Size(width, height));
		}

		public Size GetScaledSize(Size source)
		{
			return new Size((int)(source.Width * this.ZoomFactor), (int)(source.Height * this.ZoomFactor));
		}


		//        
		public SizeF GetScaledSize(float width, float height)
		{
			return this.GetScaledSize(new SizeF(width, height));
		}

		public SizeF GetScaledSize(SizeF source)
		{
			return new SizeF((float)(source.Width * this.ZoomFactor), (float)(source.Height * this.ZoomFactor));
		}


		//
		public Rectangle FitRectangle(Rectangle rectangle)
		{
			int x;
			int y;
			int w;
			int h;

			x = rectangle.X;
			y = rectangle.Y;
			w = rectangle.Width;
			h = rectangle.Height;

			if (x < 0)
			{
				x = 0;
			}

			if (y < 0)
			{
				y = 0;
			}

			if (x + w > this.ViewSize.Width)
			{
				w = this.ViewSize.Width - x;
			}

			if (y + h > this.ViewSize.Height)
			{
				h = this.ViewSize.Height - y;
			}

			return new Rectangle(x, y, w, h);
		}

		public Rectangle GetImageViewPort()
		{
			Rectangle viewPort;

			if (!this.ViewSize.IsEmpty)
			{
				Rectangle innerRectangle;
				Point offset;
				int width;
				int height;

				innerRectangle = this.GetInsideViewPort(true);

				int x;
				int y;

				x = !this.HScroll ? (innerRectangle.Width - this.ScaledImageWidth) / 2 : 0;
				y = !this.VScroll ? (innerRectangle.Height - this.ScaledImageHeight) / 2 : 0;
				offset = new Point(x, y);
				width = Math.Min(this.ScaledImageWidth - Math.Abs(this.AutoScrollPosition.X), innerRectangle.Width);
				height = Math.Min(this.ScaledImageHeight - Math.Abs(this.AutoScrollPosition.Y), innerRectangle.Height);
				viewPort = new Rectangle(offset.X + innerRectangle.Left, offset.Y + innerRectangle.Top, width, height);
			}
			else
			{
				viewPort = Rectangle.Empty;
			}

			return viewPort;
		}

		public Rectangle GetInsideViewPort(bool includePadding)
		{
			int left;
			int top;
			int width;
			int height;

			left = 0;
			top = 0;
			width = this.ClientSize.Width;
			height = this.ClientSize.Height;
			if (includePadding)
			{
				left += this.Padding.Left;
				top += this.Padding.Top;
				width -= this.Padding.Horizontal;
				height -= this.Padding.Vertical;
			}
			return new Rectangle(left, top, width, height);
		}

		public Rectangle GetOffsetRectangle(Rectangle source)
		{
			Rectangle viewport;
			Rectangle scaled;
			int offsetX;
			int offsetY;

			viewport = this.GetImageViewPort();
			scaled = this.GetScaledRectangle(source);
			offsetX = viewport.Left + this.Padding.Left + this.AutoScrollPosition.X;
			offsetY = viewport.Top + this.Padding.Top + this.AutoScrollPosition.Y;

			return new Rectangle(new Point(scaled.Left + offsetX, scaled.Top + offsetY), scaled.Size);
		}

		public Rectangle GetOffsetRectangle(int x, int y, int width, int height)
		{
			return this.GetOffsetRectangle(new Rectangle(x, y, width, height));
		}

		public Rectangle GetScaledRectangle(Point location, Size size)
		{
			return this.GetScaledRectangle(new Rectangle(location, size));
		}

		public Rectangle GetScaledRectangle(int x, int y, int width, int height)
		{
			return this.GetScaledRectangle(new Rectangle(x, y, width, height));
		}

		public Rectangle GetScaledRectangle(Rectangle source)
		{
			return new Rectangle((int)(source.Left * this.ZoomFactor), (int)(source.Top * this.ZoomFactor), (int)(source.Width * this.ZoomFactor), (int)(source.Height * this.ZoomFactor));
		}


		//        
		public RectangleF GetOffsetRectangle(RectangleF source)
		{
			RectangleF viewport;
			RectangleF scaled;
			float offsetX;
			float offsetY;

			viewport = this.GetImageViewPort();
			scaled = this.GetScaledRectangle(source);
			offsetX = viewport.Left + this.Padding.Left + this.AutoScrollPosition.X;
			offsetY = viewport.Top + this.Padding.Top + this.AutoScrollPosition.Y;

			return new RectangleF(new PointF(scaled.Left + offsetX, scaled.Top + offsetY), scaled.Size);
		}

		public RectangleF GetOffsetRectangle(float x, float y, float width, float height)
		{
			return this.GetOffsetRectangle(new RectangleF(x, y, width, height));
		}

		public RectangleF FitRectangle(RectangleF rectangle)
		{
			float x;
			float y;
			float w;
			float h;

			x = rectangle.X;
			y = rectangle.Y;
			w = rectangle.Width;
			h = rectangle.Height;

			if (x < 0)
			{
				x = 0;
			}

			if (y < 0)
			{
				y = 0;
			}

			if (x + w > this.ViewSize.Width)
			{
				w = this.ViewSize.Width - x;
			}

			if (y + h > this.ViewSize.Height)
			{
				h = this.ViewSize.Height - y;
			}

			return new RectangleF(x, y, w, h);
		}

		public RectangleF GetScaledRectangle(float x, float y, float width, float height)
		{
			return this.GetScaledRectangle(new RectangleF(x, y, width, height));
		}

		public RectangleF GetScaledRectangle(PointF location, SizeF size)
		{
			return this.GetScaledRectangle(new RectangleF(location, size));
		}

		public RectangleF GetScaledRectangle(RectangleF source)
		{
			return new RectangleF((float)(source.Left * this.ZoomFactor), (float)(source.Top * this.ZoomFactor), (float)(source.Width * this.ZoomFactor), (float)(source.Height * this.ZoomFactor));
		}

		public RectangleF GetSourceImageRegion()
		{
			RectangleF region;

			if (!this.ViewSize.IsEmpty)
			{
				//if (this.SizeMode != ImageBoxSizeMode.Stretch)
				//{
				float sourceLeft;
				float sourceTop;
				float sourceWidth;
				float sourceHeight;
				Rectangle viewPort;

				viewPort = this.GetImageViewPort();
				sourceLeft = (float)(-this.AutoScrollPosition.X / this.ZoomFactor);
				sourceTop = (float)(-this.AutoScrollPosition.Y / this.ZoomFactor);
				sourceWidth = (float)(viewPort.Width / this.ZoomFactor);
				sourceHeight = (float)(viewPort.Height / this.ZoomFactor);

				region = new RectangleF(sourceLeft, sourceTop, sourceWidth, sourceHeight);
				//}
				//else
				//{
				//  region = new RectangleF(PointF.Empty, this.ViewSize);
				//}
			}
			else
			{
				region = RectangleF.Empty;
			}

			return region;
		}

		public Image GetSelectedImage()
		{
			Image result;

			result = null;

			if (!this.SelectionRegion.IsEmpty)
			{
				Rectangle rect;

				rect = this.FitRectangle(new Rectangle((int)this.SelectionRegion.X, (int)this.SelectionRegion.Y, (int)this.SelectionRegion.Width, (int)this.SelectionRegion.Height));

				if (rect.Width > 0 && rect.Height > 0)
				{
					result = new Bitmap(rect.Width, rect.Height);

					using (Graphics g = Graphics.FromImage(result))
					{
						g.DrawImage(_image, new Rectangle(Point.Empty, rect.Size), rect, GraphicsUnit.Pixel);
					}
				}
			}

			return result;
		}

		#endregion

		#region public 이벤트들..

		[Category("Property Changed")]
		public event EventHandler AllowZoomChanged;

		[Category("Property Changed")]
		public event EventHandler InterpolationModeChanged;

		[Category("Property Changed")]
		public event EventHandler LimitSelectionToImageChanged;

		[Category("Property Changed")]
		public event EventHandler PanEnd;

		[Category("Property Changed")]
		public event EventHandler PanStart;

		[Category("Property Changed")]
		public event EventHandler PixelGridColorChanged;

		[Category("Property Changed")]
		public event EventHandler PixelGridThresholdChanged;

		[Category("Property Changed")]
		public event EventHandler PixelNumberThresholdChanged;

		[Category("Action")]
		public event EventHandler<EventArgs> Selected;

		[Category("Action")]
		public event EventHandler<ImageBoxCancelEventArgs> Selecting;

		[Category("Property Changed")]
		public event EventHandler SelectionColorChanged;

		[Category("Property Changed")]
		public event EventHandler SelectionModeChanged;

		[Category("Property Changed")]
		public event EventHandler SelectionRegionChanged;

		[Category("Property Changed")]
		public event EventHandler ShortcutsEnabledChanged;

		[Category("Property Changed")]
		public event EventHandler ShowPixelGridChanged;

		[Category("Property Changed")]
		public event EventHandler TextAlignChanged;

		[Category("Property Changed")]
		public event EventHandler TextBackColorChanged;


		[Category("Property Changed")]
		public event EventHandler ZoomChanged;

		[Category("Property Changed")]
		public event EventHandler ZoomLevelsChanged;

		[Category("Property Changed")]
		public event EventHandler<CellEventArgs> CellSelected;

		[Category("Property Changed")]
		public event EventHandler<CellEventArgs> CellDSelected;

		[Category("Property Changed")]
		public event EventHandler<CellEventArgs> CellActived;

		#endregion

		#region Public 속성들

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AllowZoom
		{
			get { return _allowZoom; }
			set
			{
				if (this.AllowZoom != value)
				{
					_allowZoom = value;

					this.OnAllowZoomChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}

		[Browsable(false)]
		public Point CenterPoint
		{
			get
			{
				Rectangle viewport;

				viewport = this.GetImageViewPort();

				return new Point(viewport.Width / 2, viewport.Height / 2);
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(int), "100")]
		public int ColCount
		{
			get { return _colCount; }
			set
			{
				//if (_colCount != value && value > 0)
				//{
					_colCount = value;
					ReSizeCount();
					Invalidate();
				//}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(int), "100")]
		public int RowCount
		{
			get { return _rowCount; }
			set
			{
				//if (_rowCount != value && value > 0)
				//{
					_rowCount = value;
					ReSizeCount();
					Invalidate();
				//}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(int), "5")]
		public int BlockColCount
		{
			get { return _blockColCount; }
			set
			{
				if (_blockColCount != value && value > 0)
				{
					_blockColCount = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(int), "100")]
		public int BlockRowCount
		{
			get { return _blockRowCount; }
			set
			{
				if (_blockRowCount != value && value > 0)
				{
					_blockRowCount = value;
					Invalidate();
				}
			}
		}
		#region Work Status Colors
		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell Empty 상태 색상")]
		public Color ColorEmpty
		{
			get { return ColorStatus[(int)eCellStatus.Empty]; }
			set { ColorStatus[(int)eCellStatus.Empty] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell Ready 상태 색상")]
		public Color ColorReady
		{
			get { return ColorStatus[(int)eCellStatus.Ready]; }
			set { ColorStatus[(int)eCellStatus.Ready] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell Work 상태 색상")]
		public Color ColorWork
		{
			get { return ColorStatus[(int)eCellStatus.Work]; }
			set { ColorStatus[(int)eCellStatus.Work] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell Finish 상태 색상")]
		public Color ColorFinish
		{
			get { return ColorStatus[(int)eCellStatus.Finish]; }
			set { ColorStatus[(int)eCellStatus.Finish] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell OK 상태 색상")]
		public Color ColorOkay
		{
			get { return ColorStatus[(int)eCellStatus.OK]; }
			set { ColorStatus[(int)eCellStatus.OK] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell NG 상태 색상")]
		public Color ColorNG
		{
			get { return ColorStatus[(int)eCellStatus.NG]; }
			set { ColorStatus[(int)eCellStatus.NG] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell NG INSPECTION 상태 색상")]
		public Color ColorNG_INSPECTION
		{
			get { return ColorStatus[(int)eCellStatus.NG_INSPECTION]; }
			set { ColorStatus[(int)eCellStatus.NG_INSPECTION] = value; }
		}

		[Category("Control Color")]
		[Browsable(true)]
		[Description("Cell NG SPEC OUT 상태 색상")]
		public Color ColorNG_SPEC_OUT
		{
			get { return ColorStatus[(int)eCellStatus.NG_SPEC_OUT]; }
			set { ColorStatus[(int)eCellStatus.NG_SPEC_OUT] = value; }
		}

		#endregion



		[Category("Control Color")]
		[Browsable(true)]
		[Description("Ranking System 색상들")]
		public Color[] ColorStatus
		{
			get { return _colorStatus; }
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "ControlDark")]
		public Color ImageBorderColor
		{
			get { return _imageBorderColor; }
			set
			{
				if (this.ImageBorderColor != value)
					_imageBorderColor = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "DarkOrange")]
		public Color DisplayNumColor
		{
			get { return _displayNumColor; }
			set
			{
				if (this.DisplayNumColor != value)
					_displayNumColor = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "DarkGray")]
		public Color MouseActiveColor
		{
			get { return _mouseActiveColor; }
			set
			{
				if (this.MouseActiveColor != value)
					_mouseActiveColor = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "DarkRed")]
		public Color MouseSelectedColor
		{
			get { return _mouseSelectedColor; }
			set
			{
				if (this.MouseSelectedColor != value)
					_mouseSelectedColor = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool ActiveEnable
		{
			get { return _activeEnable; }
			set
			{
				if (this.ActiveEnable != value)
					_activeEnable = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool SelectEnable
		{
			get { return _selectEnable; }
			set
			{
				if (this.SelectEnable != value)
					_selectEnable = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool DoubleSelectEnable
		{
			get { return _doubleSelectEnable; }
			set
			{
				if (this.DoubleSelectEnable != value)
					_doubleSelectEnable = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool GroupNumVisible
		{
			get { return _groupNumVisible; }
			set
			{
				if (this.GroupNumVisible != value)
					_groupNumVisible = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool CellNumVisible
		{
			get { return _cellNumVisible; }
			set
			{
				if (this.CellNumVisible != value)
					_cellNumVisible = value;
			}
		}


		[Category("Appearance")]
		[DefaultValue(true)]
		public bool CellTxtVisible
		{
			get { return _cellTxtVisible; }
			set
			{
				if (this._cellTxtVisible != value)
					_cellTxtVisible = value;
			}
		}

		[Category("Appearance")]
		[Browsable(true)]
		[Description("Mouse Down && Move ==> Cell Change")]
		public bool MouseMoveChangeEnable
		{
			get { return _mouseMoveChangeEnable; }
			set
			{
				if (this.MouseMoveChangeEnable != value)
					_mouseMoveChangeEnable = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(InterpolationMode.NearestNeighbor)]
		public InterpolationMode InterpolationMode
		{
			get { return _interpolationMode; }
			set
			{
				if (value == InterpolationMode.Invalid)
				{
					value = InterpolationMode.Default;
				}

				if (_interpolationMode != value)
				{
					_interpolationMode = value;
					this.OnInterpolationModeChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		public bool IsActualSize
		{
			get { return this.Zoom == 100; }
		}

		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsPanning
		{
			get { return _isPanning; }
			protected set
			{
				if (_isPanning != value)
				{
					CancelEventArgs args;

					args = new CancelEventArgs();

					if (value)
					{
						this.OnPanStart(args);
					}
					else
					{
						this.OnPanEnd(EventArgs.Empty);
					}

					if (!args.Cancel)
					{
						_isPanning = value;

						if (value)
						{
							_startScrollPosition = this.AutoScrollPosition;
							this.Cursor = Cursors.SizeAll;
						}
						else
						{
							this.Cursor = Cursors.Default;
						}
					}
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSelecting { get; protected set; }

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool LimitSelectionToImage
		{
			get { return _limitSelectionToImage; }
			set
			{
				if (this.LimitSelectionToImage != value)
				{
					_limitSelectionToImage = value;

					this.OnLimitSelectionToImageChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "DimGray")]
		public Color PixelGridColor
		{
			get { return _pixelGridColor; }
			set
			{
				if (this.PixelGridColor != value)
				{
					_pixelGridColor = value;

					this.OnPixelGridColorChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "DarkBlue")]
		public Color PixelGridBlockColor
		{
			get { return _pixelGridBlockColor; }
			set
			{
				if (this.PixelGridBlockColor != value)
				{
					_pixelGridBlockColor = value;

					this.OnPixelGridColorChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(5)]
		public int PixelGridThreshold
		{
			get { return _pixelGridThreshold; }
			set
			{
				if (this.PixelGridThreshold != value)
				{
					_pixelGridThreshold = value;

					this.OnPixelGridThresholdChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(5)]
		public int PixelNumberThreshold
		{
			get { return _pixelNumberThreshold; }
			set
			{
				if (this.PixelNumberThreshold != value)
				{
					_pixelNumberThreshold = value;

					this.OnPixelNumberThresholdChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Highlight")]
		public Color SelectionColor
		{
			get { return _selectionColor; }
			set
			{
				if (this.SelectionColor != value)
				{
					_selectionColor = value;

					this.OnSelectionColorChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(eMouseMode), "None")]
		public eMouseMode MouseMode
		{
			get { return _mouseMode; }
			set
			{
				if (this.MouseMode != value)
					_mouseMode = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(eSelectionMode), "None")]
		public eSelectionMode SelectionMode
		{
			get { return _selectionMode; }
			set
			{
				if (this.SelectionMode != value)
				{
					_selectionMode = value;

					this.OnSelectionModeChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RectangleF SelectionRegion
		{
			get { return _selectionRegion; }
			set
			{
				if (this.SelectionRegion != value)
				{
					_selectionRegion = value;

					this.OnSelectionRegionChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShortcutsEnabled
		{
			get { return _shortcutsEnabled; }
			set
			{
				if (this.ShortcutsEnabled != value)
				{
					_shortcutsEnabled = value;

					this.OnShortcutsEnabledChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public bool ShowPixelGrid
		{
			get { return _showPixelGrid; }
			set
			{
				if (this.ShowPixelGrid != value)
				{
					_showPixelGrid = value;

					this.OnShowPixelGridChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
		public ContentAlignment TextAlign
		{
			get { return _textAlign; }
			set
			{
				if (this.TextAlign != value)
				{
					_textAlign = value;

					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Transparent")]
		public Color TextBackColor
		{
			get { return _textBackColor; }
			set
			{
				if (this.TextBackColor != value)
				{
					_textBackColor = value;

					this.OnTextBackColorChanged(EventArgs.Empty);
				}
			}
		}

		[DefaultValue(100)]
		[Category("Appearance")]
		public int Zoom
		{
			get { return _zoom; }
			set { this.SetZoom(value, value > this.Zoom ? ImageBoxZoomActions.ZoomIn : ImageBoxZoomActions.ZoomOut, ImageBoxActionSources.Unknown); Invalidate(); }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double ZoomFactor
		{
			get { return (double)this.Zoom / 100; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ZoomLevelCollection ZoomLevels
		{
			get { return _zoomLevels; }
			set
			{
				if (this.ZoomLevels != value)
				{
					_zoomLevels = value;
					this.OnZoomLevelsChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Geometric")]
		[Browsable(true)]
		[Description("Strip Cell Status")]
		public eCellStatus[] StripStatus
		{
			get { return _stripStatus; }
		}

		[Category("Geometric")]
		[Browsable(true)]
		[Description("Strip Cell Text")]
		public string[] StripText
		{
			get { return _stripText; }
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool Display2GroupNumIndex
		{
			get { return _display2GroupNumIndex; }
			set
			{
				if (this.Display2GroupNumIndex != value)
					_display2GroupNumIndex = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool Display2CellNumIndex
		{
			get { return _display2CellNumIndex; }
			set
			{
				if (this.Display2CellNumIndex != value)
					_display2CellNumIndex = value;
			}
		}


		#endregion

	}

	#region 클래스들.. 
	public class ImageBoxCancelEventArgs : CancelEventArgs
	{
		public ImageBoxCancelEventArgs(Point location) : this() { this.Location = location; }
		protected ImageBoxCancelEventArgs() { }
		public Point Location { get; protected set; }
	}

	public class ZoomLevelCollection : IList<int>
	{
		public ZoomLevelCollection() { this.List = new SortedList<int, int>(); }
		public ZoomLevelCollection(IEnumerable<int> collection) : this()
		{
			if (collection == null)
				throw new ArgumentNullException("collection");
			this.AddRange(collection);
		}

		public static ZoomLevelCollection Default
		{
			get
			{
				return new ZoomLevelCollection(new[] {5,10,20, 25, 30, 50, 70, 100, 150, 200, 300, 400, 500, 600, 700, 800, 1200
			, 1600, 1800,2000, 2500, 3000, 3500, 4000, 5000,6000, 7500, 9000, 12000, 15000, 20000});
			}
		}

		protected SortedList<int, int> List { get; set; }
		public int Count { get { return this.List.Count; } }
		public bool IsReadOnly { get { return false; } }
		public int this[int index]
		{
			get { return this.List.Values[index]; }
			set
			{
				this.List.RemoveAt(index);
				this.Add(value);
			}
		}


		#region Public Members
		public void Add(int item)
		{
			this.List.Add(item, item);
		}

		public void AddRange(IEnumerable<int> collection)
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			foreach (int value in collection)
				this.Add(value);
		}

		public void Clear()
		{
			this.List.Clear();
		}

		public bool Contains(int item)
		{
			return this.List.ContainsKey(item);
		}

		public void CopyTo(int[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
				array[arrayIndex + i] = this.List.Values[i];
		}

		public int FindNearest(int zoomLevel)
		{
			int nearestValue = this.List.Values[0];
			int nearestDifference = Math.Abs(nearestValue - zoomLevel);
			for (int i = 1; i < this.Count; i++)
			{
				int value = this.List.Values[i];
				int difference = Math.Abs(value - zoomLevel);
				if (difference < nearestDifference)
				{
					nearestValue = value;
					nearestDifference = difference;
				}
			}
			return nearestValue;
		}

		public IEnumerator<int> GetEnumerator()
		{
			return this.List.Values.GetEnumerator();
		}

		public int IndexOf(int item)
		{
			return this.List.IndexOfKey(item);
		}

		public void Insert(int index, int item)
		{
			throw new NotImplementedException();
		}

		public int NextZoom(int zoomLevel)
		{
			int index;
			index = this.IndexOf(this.FindNearest(zoomLevel));
			if (index < this.Count - 1)
				index++;
			return this[index];
		}

		public int PreviousZoom(int zoomLevel)
		{
			int index;
			index = this.IndexOf(this.FindNearest(zoomLevel));
			if (index > 0)
				index--;
			return this[index];
		}

		public bool Remove(int item)
		{
			return this.List.Remove(item);
		}

		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		public int[] ToArray()
		{
			int[] results;
			results = new int[this.Count];
			this.CopyTo(results, 0);
			return results;
		}

		#endregion

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}

	public class CellEventArgs : EventArgs
	{
		public CellEventArgs() { }
		public CellEventArgs(int colcount, int rowcount, int bcolcount, int browcount, int col, int row, string txt, eCellStatus status) : this()
		{
			this.ColCount		= colcount;
			this.RowCount		= rowcount;
			this.BlockColCount	= bcolcount;
			this.BlockRowCount	= browcount;
			this.Col = col;
			this.Row = row;
			this.Text = txt;
			this.Status = status;
		}

		private int ColCount { get; set; }
		private int RowCount { get; set; }
		private int BlockColCount { get; set; }
		private int BlockRowCount { get; set; }

		public int Col { get; set; }
		public int Row { get; set; }
		public string Text { get; set; }
		public eCellStatus Status { get; set; }
		public int Index { get { return Row * ColCount + Col; } }
		public int UnitCol { get { return Col % BlockColCount; } }
		public int UnitRow { get { return Row % BlockRowCount; } }
		public int BlockCol { get { return (int)Math.Floor((double)(Col / BlockColCount)); } }
		public int BlockRow { get { return (int)Math.Floor((double)(Row / BlockRowCount)); } }
	}
	#endregion



}
