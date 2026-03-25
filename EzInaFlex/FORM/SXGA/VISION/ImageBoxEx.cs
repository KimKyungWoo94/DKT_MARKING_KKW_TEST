using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace EzIna
{
    // Cyotek ImageBox
    // Copyright (c) 2010-2014 Cyotek.
    // http://cyotek.com
    // http://cyotek.com/blog/tag/imagebox

    // Licensed under the MIT License. See imagebox-license.txt for the full text.

    // If you use this control in your applications, attribution, donations or contributions are welcome.

    internal class ImageBoxEx : ImageBox
    {

        #region Kai [Option]
        public enum eDef_Option
        {
            CrossLineVisible,
            RoiVisible,
            LineRegionVisble
        }
        private Dictionary<eDef_Option, bool> _OptionItems;

        //Set Option
        protected void SetOption(eDef_Option a_eOption, bool a_bValue)
        {

            switch (a_eOption)
            {

                case eDef_Option.RoiVisible:
                    {
                        if (a_bValue == true)
                        {
                            if (_OptionItems.ContainsKey(eDef_Option.LineRegionVisble))
                                _OptionItems[eDef_Option.LineRegionVisble] = false;
                            else
                            {
                                _OptionItems.Add(eDef_Option.LineRegionVisble, false);
                            }
                            SelectionMode = ImageBoxSelectionMode.Rectangle;

                        }
                        else
                        {
                            SelectionMode = ImageBoxSelectionMode.None;
                            foreach (DragHandle handle in this.DragHandles)
                            {
                                handle.Visible = false;
                            }
                        }
                        if (_OptionItems.ContainsKey(a_eOption))
                        {
                            _OptionItems[a_eOption] = a_bValue;

                        }
                        else
                        {
                            _OptionItems.Add(a_eOption, a_bValue);
                        }
                        OnSelectionRegionChanged(EventArgs.Empty);
                    }
                    break;
                case eDef_Option.LineRegionVisble:
                    {
                        if (a_bValue == true)
                        {
                            if (_OptionItems.ContainsKey(eDef_Option.RoiVisible))
                                _OptionItems[eDef_Option.RoiVisible] = false;
                            else
                            {
                                _OptionItems.Add(eDef_Option.RoiVisible, false);
                            }
                            SelectionMode = ImageBoxSelectionMode.Line;
                        }
                        else
                        {
                            SelectionMode = ImageBoxSelectionMode.None;
                            foreach (DragHandle handle in this.DragHandles)
                            {
                                handle.Visible = false;
                            }
                        }
                        if (_OptionItems.ContainsKey(a_eOption))
                        {
                            _OptionItems[a_eOption] = a_bValue;
                        }
                        else
                        {
                            _OptionItems.Add(a_eOption, a_bValue);
                        }
                        OnSelectionRegionChanged(EventArgs.Empty);
                    }
                    break;
                default:
                    if (_OptionItems.ContainsKey(a_eOption))
                        _OptionItems[a_eOption] = a_bValue;
                    else
                        _OptionItems.Add(a_eOption, a_bValue);
                    foreach (DragHandle handle in this.DragHandles)
                    {
                        handle.Visible = false;
                    }
                    SelectionMode = ImageBoxSelectionMode.None;
                    OnSelectionRegionChanged(EventArgs.Empty);
                    break;
            }

        }

        protected bool GetOption(eDef_Option a_eOption)
        {
            bool bRet = false;
            if (_OptionItems.Count < 1) return bRet;

            if (_OptionItems.TryGetValue(a_eOption, out bRet) == true)
                return bRet;

            return bRet;
        }
        #endregion
        #region Instance Fields

        private readonly DragHandleCollection _dragHandles;

        private int _dragHandleSize;

        private Size _minimumSelectionSize;

        #endregion

        #region Public Constructors

        public ImageBoxEx()
        {
            _dragHandles = new DragHandleCollection();
            this.DragHandleSize = 8;
            this.MinimumSelectionSize = Size.Empty;
            this.PositionDragHandles();
            #region kai  [ Option ]
            _OptionItems = new Dictionary<eDef_Option, bool>();
            foreach (eDef_Option item in Enum.GetValues(typeof(eDef_Option)))
            {
                _OptionItems.Add(item, false);

            }
            //_OptionItems.Add(eDef_Option.CrossLineVisible, false);
            //_OptionItems.Add(eDef_Option.RoiVisible, false);
            #endregion
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the DragHandleSize property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler DragHandleSizeChanged;

        /// <summary>
        /// Occurs when the MinimumSelectionSize property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler MinimumSelectionSizeChanged;

        [Category("Action")]
        public event EventHandler SelectionMoved;

        [Category("Action")]
        public event CancelEventHandler SelectionMoving;

        [Category("Action")]
        public event EventHandler SelectionResized;

        [Category("Action")]
        public event CancelEventHandler SelectionResizing;

        #endregion

        #region Overridden Methods

        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point imagePoint;

            imagePoint = this.PointToImage(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:
                        {
                            if ((this.SelectionRegion.Contains(imagePoint) || this.HitTest(e.Location) != DragHandleAnchor.None))
                            {
                                this.DragOrigin = e.Location;
                                this.DragOriginOffset = new Point(imagePoint.X - (int)this.SelectionRegion.X, imagePoint.Y - (int)this.SelectionRegion.Y);
                            }
                        }
                        break;
                    case ImageBoxSelectionMode.Line:
                        {
                            if (this.LineSelectionRegionHitTest(imagePoint) || this.HitTest(e.Location) != DragHandleAnchor.None)
                            {
                                this.DragOrigin = e.Location;
                                this.DragOrginLineRegionST_Offset = new Point(imagePoint.X - (int)this.LineSelectionRegionStart.X,
                                                                                imagePoint.Y - (int)this.LineSelectionRegionStart.Y);
                                this.DragOrginLineRegionED_Offset = new Point(imagePoint.X - (int)this.LineSelectionRegionEnd.X,
                                                                                imagePoint.Y - (int)this.LineSelectionRegionEnd.Y);
                            }

                        }
                        break;

                }
            }
            else
            {
                this.DragOriginOffset = Point.Empty;
                this.DragOrginLineRegionST_Offset = Point.Empty;
                this.DragOrginLineRegionED_Offset = Point.Empty;
                this.DragOrigin = Point.Empty;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // start either a move or a resize operation
            if (!this.IsSelecting && !this.IsMoving && !this.IsResizing && e.Button == MouseButtons.Left && !this.DragOrigin.IsEmpty && this.IsOutsideDragZone(e.Location))
            {
                DragHandleAnchor anchor;

                anchor = this.HitTest(this.DragOrigin);

                if (anchor == DragHandleAnchor.None)
                {
                    // move
                    this.StartMove();
                }
                else if (this.DragHandles[anchor].Enabled && this.DragHandles[anchor].Visible)
                {
                    // resize
                    this.StartResize(anchor);
                }
            }

            // set the cursor
            this.SetCursor(e.Location);

            // perform operations
            this.ProcessSelectionMove(e.Location);
            this.ProcessSelectionResize(e.Location);

            base.OnMouseMove(e);
        }
        protected override void ProcessSelection(MouseEventArgs e)
        {
            if (this.SelectionMode != ImageBoxSelectionMode.None && e.Button == MouseButtons.Left && !this.WasDragCancelled)
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
                    float CurrX;
                    float CurrY;
                    float StartX;
                    float StartY;
                    Point imageOffset;
                    Rectangle rectViewport;
                    RectangleF selection;
                    rectViewport = this.GetImageViewPort();
                    imageOffset = this.GetImageViewPort().Location;

                    if (e.X < startMousePosition.X)
                    {
                        x = e.X;
                        w = startMousePosition.X - e.X;
                    }
                    else
                    {
                        x = startMousePosition.X;
                        w = e.X - startMousePosition.X;
                    }

                    if (e.Y < startMousePosition.Y)
                    {
                        y = e.Y;
                        h = startMousePosition.Y - e.Y;
                    }
                    else
                    {
                        y = startMousePosition.Y;
                        h = e.Y - startMousePosition.Y;
                    }

                    x = x - imageOffset.X - this.AutoScrollPosition.X;
                    y = y - imageOffset.Y - this.AutoScrollPosition.Y;
                    CurrX = e.X - imageOffset.X - this.AutoScrollPosition.X;
                    CurrY = e.Y - imageOffset.Y - this.AutoScrollPosition.Y;
                    StartX = startMousePosition.X - imageOffset.X - this.AutoScrollPosition.X;
                    StartY = startMousePosition.Y - imageOffset.Y - this.AutoScrollPosition.Y;
                    x = x / (float)this.ZoomFactor;
                    y = y / (float)this.ZoomFactor;
                    w = w / (float)this.ZoomFactor;
                    h = h / (float)this.ZoomFactor;
                    CurrX = CurrX / (float)this.ZoomFactor;
                    CurrY = CurrY / (float)this.ZoomFactor;
                    StartX = StartX / (float)this.ZoomFactor;
                    StartY = StartY / (float)this.ZoomFactor;
                    selection = new RectangleF(x, y, w, h);
                    switch (SelectionMode)
                    {

                        case ImageBoxSelectionMode.Rectangle:

                            if (this.LimitSelectionToImage)
                            {
                                selection = this.FitRectangle(selection);
                            }
                            this.SelectionRegion = selection;
                            break;
                        case ImageBoxSelectionMode.Line:
                            {
                                if (StartX <= 0)
                                {
                                    StartX = 0;
                                }
                                else if (StartX >= this.ViewSize.Width)
                                {
                                    StartX = this.ViewSize.Width;
                                }
                                if (StartY <= 0)
                                {
                                    StartY = 0;
                                }
                                else if (StartY >= this.ViewSize.Height)
                                {
                                    StartY = this.ViewSize.Height;
                                }
                                if (CurrX <= 0)
                                {
                                    CurrX = 0;
                                }
                                else if (CurrX >= this.ViewSize.Width)
                                {
                                    CurrX = this.ViewSize.Width;
                                }
                                if (CurrY <= 0)
                                {
                                    CurrY = 0;
                                }
                                else if (CurrY >= this.ViewSize.Height)
                                {
                                    CurrY = this.ViewSize.Height;
                                }
                                LineSelectionRegionStart = new PointF(StartX, StartY);
                                LineSelectionRegionEnd = new PointF(CurrX, CurrY);
                            }

                            break;

                    }

                }
            }
        }
        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.IsMoving)
            {
                this.CompleteMove();
            }
            else if (this.IsResizing)
            {
                this.CompleteResize();
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.AllowPainting)
            {
                foreach (DragHandle handle in this.DragHandles)
                {
                    if (handle.Visible)
                    {
                        this.DrawDragHandle(e.Graphics, handle);
                    }
                }
            }

        }
        protected override void DrawCrossLine(Graphics g)
        {
            bool bDisplay = false;
            //Draw cross line.
            if (_OptionItems.TryGetValue(eDef_Option.CrossLineVisible, out bDisplay))
            {
                if (bDisplay)
                {
                    base.DrawCrossLine(g);
                }
            }
        }
        protected override void DrawSelection(PaintEventArgs e)
        {
            bool bDisplay = false;
            switch (SelectionMode)
            {
                case ImageBoxSelectionMode.Rectangle:
                    {
                        if (_OptionItems.TryGetValue(eDef_Option.RoiVisible, out bDisplay))
                        {
                            if (bDisplay && this.SelectionRegion.IsEmpty == false)
                                DrawRectRegionSelection(e.Graphics);
                        }
                    }
                    break;
                case ImageBoxSelectionMode.Line:
                    {
                        if (_OptionItems.TryGetValue(eDef_Option.LineRegionVisble, out bDisplay))
                        {
                            if (bDisplay)
                                DrawLineRegionSelection(e.Graphics);
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        protected void DrawRectRegionSelection(Graphics a_Graphics)
        {
            if (a_Graphics == null)
                return;
            RectangleF rect;
            a_Graphics.SetClip(this.GetInsideViewPort(true));
            rect = this.GetOffsetRectangle(SelectionRegion);
            using (Brush brush = new SolidBrush(Color.FromArgb(25, this.SelectionColor)))
            {
                a_Graphics.FillRectangle(brush, rect);
            }
            using (Pen pen = new Pen(this.SelectionColor))
            {
                a_Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            }
            a_Graphics.ResetClip();
        }
        protected void DrawLineRegionSelection(Graphics a_Graphics)
        {
            if (a_Graphics == null)
                return;
            PointF PT_ST;
            PointF PT_ED;
            a_Graphics.SetClip(this.GetInsideViewPort(true));
            PT_ST = this.GetOffsetPoint(LineSelectionRegionStart);
            PT_ED = this.GetOffsetPoint(LineSelectionRegionEnd);
            using (Pen pen = new Pen(this.SelectionColor, _LineWidth))
            {
                a_Graphics.DrawLine(pen, PT_ST, PT_ED);
            }
            a_Graphics.ResetClip();
        }
        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">
        ///   An <see cref="T:System.EventArgs" /> that contains the event data.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.PositionDragHandles();
        }

        /// <summary>
        ///   Raises the <see cref="System.Windows.Forms.ScrollableControl.Scroll" /> event.
        /// </summary>
        /// <param name="se">
        ///   A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);

            this.PositionDragHandles();
        }

        /// <summary>
        ///   Raises the <see cref="ImageBox.Selecting" /> event.
        /// </summary>
        /// <param name="e">
        ///   The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        /// 

        //kai
        protected override void OnSelecting(ImageBoxCancelEventArgs e)
        {
            e.Cancel = this.IsMoving || this.IsResizing || this.HitTest(e.Location) != DragHandleAnchor.None;
            switch (SelectionMode)
            {

                case ImageBoxSelectionMode.Rectangle:
                    e.Cancel = e.Cancel || this.SelectionRegion.Contains(this.PointToImage(e.Location));
                    break;
                case ImageBoxSelectionMode.Line:
                    e.Cancel = e.Cancel || this.LineSelectionRegionHitTest(this.PointToImage(e.Location));
                    break;

                default:
                    break;
            }
            base.OnSelecting(e);
        }
        public bool LineSelectionRegionHitTest(Point Pt)
        {
            // test if we fall outside of the bounding box:
            /* if ((Pt.X < LineSelectionRegionStart.X && Pt.X < LineSelectionRegionEnd.X) || (Pt.X > LineSelectionRegionStart.X && Pt.X > LineSelectionRegionEnd.X) ||
                 (Pt.Y < LineSelectionRegionStart.Y && Pt.Y < LineSelectionRegionEnd.Y) || (Pt.Y > LineSelectionRegionStart.Y && Pt.Y > LineSelectionRegionEnd.Y))
                 return false;
             // now we calculate the distance:
             float dy = LineSelectionRegionEnd.Y - LineSelectionRegionStart.Y;
             float dx = LineSelectionRegionEnd.X - LineSelectionRegionStart.X;
             float Z = dy * Pt.X - dx * Pt.Y + LineSelectionRegionStart.Y * LineSelectionRegionEnd.X - LineSelectionRegionStart.X * LineSelectionRegionEnd.Y;
             float N = dy * dy + dx * dx;
             float dist = (float)(Math.Abs(Z) / Math.Sqrt(N));
             return dist < _LineWidth / 2f;
             */
            bool bRet = false;
            using (GraphicsPath gp = new GraphicsPath())
            using (Pen pen = new Pen(this.SelectionColor, _LineWidth + 3))
            {
                gp.AddLine(LineSelectionRegionStart, LineSelectionRegionEnd);
                bRet = gp.IsOutlineVisible(Pt, pen);
            }
            // done:
            return bRet;
        }
        /// <summary>
        ///   Raises the <see cref="ImageBox.SelectionRegionChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///   The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        protected override void OnSelectionRegionChanged(EventArgs e)
        {
            base.OnSelectionRegionChanged(e);

            this.PositionDragHandles();
        }

        /// <summary>
        ///   Raises the <see cref="ImageBox.ZoomChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///   The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        protected override void OnZoomChanged(EventArgs e)
        {
            base.OnZoomChanged(e);

            this.PositionDragHandles();
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <returns>
        /// true if the key was processed by the control; otherwise, false.
        /// </returns>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process. </param>

        //kai
        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool result;

            //			if (keyData == Keys.Escape && (this.IsResizing || this.IsMoving))
            if (keyData == Keys.Escape)
            {
                if (this.IsResizing)
                {
                    this.CancelResize();
                }
                else
                {
                    this.CancelMove();
                }
                result = true;
            }


            else
            {
                result = base.ProcessDialogKey(keyData);
            }

            return result;
        }

        #endregion

        #region Public Properties

        [Category("Appearance")]
        [DefaultValue(8)]
        public virtual int DragHandleSize
        {
            get { return _dragHandleSize; }
            set
            {
                if (this.DragHandleSize != value)
                {
                    _dragHandleSize = value;

                    this.OnDragHandleSizeChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public DragHandleCollection DragHandles
        {
            get { return _dragHandles; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMoving { get; protected set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsResizing { get; protected set; }

        [Category("Behavior")]
        [DefaultValue(typeof(Size), "0, 0")]
        public virtual Size MinimumSelectionSize
        {
            get { return _minimumSelectionSize; }
            set
            {
                if (this.MinimumSelectionSize != value)
                {
                    _minimumSelectionSize = value;

                    this.OnMinimumSelectionSizeChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public RectangleF PreviousSelectionRegion { get; protected set; }
        public PointF LineSelectionRegionStart
        {
            get { return _LineSelectionRegionStart; }
            set
            {
                if (_LineSelectionRegionStart != value)
                {
                    _LineSelectionRegionStart = value;
                    OnSelectionRegionChanged(EventArgs.Empty);
                }
            }
        }

        public PointF LineSelectionRegionEnd
        {
            get { return _LineSelectionRegionEnd; }
            set
            {
                if (_LineSelectionRegionEnd != value)
                {
                    _LineSelectionRegionEnd = value;
                    OnSelectionRegionChanged(EventArgs.Empty);
                }
            }
        }

        public PointF PreviousLineSelectionRegionStart
        {
            get { return _PreviousLineSelectionRegionStart; }
            protected set
            {
                if (_PreviousLineSelectionRegionStart != value)
                {
                    _PreviousLineSelectionRegionStart = value;
                }
            }
        }
        public PointF PreviousLineSelectionRegionEnd
        {
            get { return _PreviousLineSelectionRegionEnd; }
            protected set
            {
                if (_PreviousLineSelectionRegionEnd != value)
                {
                    _PreviousLineSelectionRegionEnd = value;
                }
            }
        }
        #endregion
        private PointF _PreviousLineSelectionRegionStart;
        private PointF _PreviousLineSelectionRegionEnd;
        private PointF _LineSelectionRegionStart;
        private PointF _LineSelectionRegionEnd;
        private float _LineWidth = 1;
        #region Protected Properties

        protected Point DragOrigin { get; set; }

        protected Point DragOriginOffset { get; set; }

        protected Point DragOrginLineRegionST_Offset { get; set; }
        protected Point DragOrginLineRegionED_Offset { get; set; }

        protected DragHandleAnchor ResizeAnchor { get; set; }

        #endregion

        #region Public Members

        //kai
        public void CancelResize()
        {
            switch (SelectionMode)
            {

                case ImageBoxSelectionMode.Rectangle:
                    this.SelectionRegion = this.PreviousSelectionRegion;
                    break;
                case ImageBoxSelectionMode.Line:
                    this.LineSelectionRegionStart = this.PreviousLineSelectionRegionStart;
                    this.LineSelectionRegionEnd = this.PreviousLineSelectionRegionEnd;
                    break;

            }

            this.CompleteResize();
        }

        public void StartMove()
        {
            CancelEventArgs e;

            if (this.IsMoving || this.IsResizing)
            {
                throw new InvalidOperationException("A move or resize action is currently being performed.");
            }

            e = new CancelEventArgs();

            this.OnSelectionMoving(e);

            if (!e.Cancel)
            {
                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:
                        this.PreviousSelectionRegion = this.SelectionRegion;
                        break;
                    case ImageBoxSelectionMode.Line:
                        this.PreviousLineSelectionRegionStart = this.LineSelectionRegionStart;
                        this.PreviousLineSelectionRegionEnd = this.LineSelectionRegionEnd;
                        break;

                }
                this.IsMoving = true;
            }
        }

        #endregion

        #region Protected Members

        protected virtual void DrawDragHandle(Graphics graphics, DragHandle handle)
        {
            int left;
            int top;
            int width;
            int height;
            Pen outerPen;
            Brush innerBrush;

            left = handle.Bounds.Left;
            top = handle.Bounds.Top;
            width = handle.Bounds.Width;
            height = handle.Bounds.Height;

            if (handle.Enabled)
            {
                //kai
                outerPen = new Pen(Color.Aqua);
                innerBrush = new SolidBrush(Color.Aqua);
                //                 outerPen = SystemPens.WindowFrame;
                //                 innerBrush = SystemBrushes.Window;
            }
            else
            {
                outerPen = SystemPens.ControlDark;
                innerBrush = SystemBrushes.Control;
            }

            graphics.FillRectangle(innerBrush, left + 1, top + 1, width - 2, height - 2);
            graphics.DrawLine(outerPen, left + 1, top, left + width - 2, top);
            graphics.DrawLine(outerPen, left, top + 1, left, top + height - 2);
            graphics.DrawLine(outerPen, left + 1, top + height - 1, left + width - 2, top + height - 1);
            graphics.DrawLine(outerPen, left + width - 1, top + 1, left + width - 1, top + height - 2);
        }

        /// <summary>
        /// Raises the <see cref="DragHandleSizeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnDragHandleSizeChanged(EventArgs e)
        {
            EventHandler handler;

            this.PositionDragHandles();
            this.Invalidate();

            handler = this.DragHandleSizeChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="MinimumSelectionSizeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnMinimumSelectionSizeChanged(EventArgs e)
        {
            EventHandler handler;

            handler = this.MinimumSelectionSizeChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionMoved" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectionMoved(EventArgs e)
        {
            EventHandler handler;

            handler = this.SelectionMoved;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionMoving" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectionMoving(CancelEventArgs e)
        {
            CancelEventHandler handler;

            handler = this.SelectionMoving;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionResized" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectionResized(EventArgs e)
        {
            EventHandler handler;

            handler = this.SelectionResized;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionResizing" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectionResizing(CancelEventArgs e)
        {
            CancelEventHandler handler;

            handler = this.SelectionResizing;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Private Members

        private void CancelMove()
        {
            switch (SelectionMode)
            {
                case ImageBoxSelectionMode.Rectangle:
                    this.SelectionRegion = this.PreviousSelectionRegion;
                    break;
                case ImageBoxSelectionMode.Line:
                    this.LineSelectionRegionStart = this.PreviousLineSelectionRegionStart;
                    this.LineSelectionRegionEnd = this.PreviousLineSelectionRegionEnd;
                    break;
            }


            this.CompleteMove();
        }

        private void CompleteMove()
        {
            this.ResetDrag();
            this.OnSelectionMoved(EventArgs.Empty);
        }

        private void CompleteResize()
        {
            this.ResetDrag();
            this.OnSelectionResized(EventArgs.Empty);
        }

        private DragHandleAnchor HitTest(Point cursorPosition)
        {
            return this.DragHandles.HitTest(cursorPosition);
        }

        private bool IsOutsideDragZone(Point location)
        {
            Rectangle dragZone;
            int dragWidth;
            int dragHeight;

            dragWidth = SystemInformation.DragSize.Width;
            dragHeight = SystemInformation.DragSize.Height;
            dragZone = new Rectangle(this.DragOrigin.X - (dragWidth / 2), this.DragOrigin.Y - (dragHeight / 2), dragWidth, dragHeight);

            return !dragZone.Contains(location);
        }
        //kai

        public void GetPositionDragHandles()
        {
            PositionDragHandles();
        }
        private void PositionDragHandles()
        {
            if (this.DragHandles != null && this.DragHandleSize > 0)
            {
                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:
                        PositionDragHandlesRectRegion();
                        break;
                    case ImageBoxSelectionMode.Line:
                        PositionDragHandlesLineRegion();
                        break;
                    default:

                        break;
                }
            }
        }
        private void PositionDragHandlesRectRegion()
        {
            if (this.SelectionRegion.IsEmpty)
            {
                this.DragHandles[DragHandleAnchor.TopLeft].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.TopLeft].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.TopCenter].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.TopCenter].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.TopRight].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.MiddleLeft].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.MiddleRight].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.BottomLeft].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.BottomCenter].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.BottomRight].Bounds = Rectangle.Empty;
                this.DragHandles[DragHandleAnchor.TopLeft].Visible = false;
                this.DragHandles[DragHandleAnchor.TopLeft].Visible = false;
                this.DragHandles[DragHandleAnchor.TopCenter].Visible = false;
                this.DragHandles[DragHandleAnchor.TopCenter].Visible = false;
                this.DragHandles[DragHandleAnchor.TopRight].Visible = false;
                this.DragHandles[DragHandleAnchor.MiddleLeft].Visible = false;
                this.DragHandles[DragHandleAnchor.MiddleRight].Visible = false;
                this.DragHandles[DragHandleAnchor.BottomLeft].Visible = false;
                this.DragHandles[DragHandleAnchor.BottomCenter].Visible = false;
                this.DragHandles[DragHandleAnchor.BottomRight].Visible = false;
            }
            else
            {
                int left;
                int top;
                int right;
                int bottom;
                int halfWidth;
                int halfHeight;
                int halfDragHandleSize;
                Rectangle viewport;
                int offsetX;
                int offsetY;

                viewport = this.GetImageViewPort();
                offsetX = viewport.Left + this.Padding.Left + this.AutoScrollPosition.X;
                offsetY = viewport.Top + this.Padding.Top + this.AutoScrollPosition.Y;
                halfDragHandleSize = this.DragHandleSize / 2;
                left = Convert.ToInt32((this.SelectionRegion.Left * this.ZoomFactor) + offsetX);
                top = Convert.ToInt32((this.SelectionRegion.Top * this.ZoomFactor) + offsetY);
                right = left + Convert.ToInt32(this.SelectionRegion.Width * this.ZoomFactor);
                bottom = top + Convert.ToInt32(this.SelectionRegion.Height * this.ZoomFactor);
                halfWidth = Convert.ToInt32(this.SelectionRegion.Width * this.ZoomFactor) / 2;
                halfHeight = Convert.ToInt32(this.SelectionRegion.Height * this.ZoomFactor) / 2;

                this.DragHandles[DragHandleAnchor.TopLeft].Bounds = new Rectangle(left - this.DragHandleSize, top - this.DragHandleSize, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.TopCenter].Bounds = new Rectangle(left + halfWidth - halfDragHandleSize, top - this.DragHandleSize, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.TopRight].Bounds = new Rectangle(right, top - this.DragHandleSize, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.MiddleLeft].Bounds = new Rectangle(left - this.DragHandleSize, top + halfHeight - halfDragHandleSize, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.MiddleRight].Bounds = new Rectangle(right, top + halfHeight - halfDragHandleSize, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.BottomLeft].Bounds = new Rectangle(left - this.DragHandleSize, bottom, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.BottomCenter].Bounds = new Rectangle(left + halfWidth - halfDragHandleSize, bottom, this.DragHandleSize, this.DragHandleSize);
                this.DragHandles[DragHandleAnchor.BottomRight].Bounds = new Rectangle(right, bottom, this.DragHandleSize, this.DragHandleSize);

                this.DragHandles[DragHandleAnchor.TopLeft].Visible = true;
                this.DragHandles[DragHandleAnchor.TopLeft].Visible = true;
                this.DragHandles[DragHandleAnchor.TopCenter].Visible = true;
                this.DragHandles[DragHandleAnchor.TopCenter].Visible = true;
                this.DragHandles[DragHandleAnchor.TopRight].Visible = true;
                this.DragHandles[DragHandleAnchor.MiddleLeft].Visible = true;
                this.DragHandles[DragHandleAnchor.MiddleRight].Visible = true;
                this.DragHandles[DragHandleAnchor.BottomLeft].Visible = true;
                this.DragHandles[DragHandleAnchor.BottomCenter].Visible = true;
                this.DragHandles[DragHandleAnchor.BottomRight].Visible = true;
                this.DragHandles[DragHandleAnchor.LineStart].Visible = false;
                this.DragHandles[DragHandleAnchor.LineEnd].Visible = false;
            }
        }
        private void PositionDragHandlesLineRegion()
        {
            if (LineSelectionRegionStart.IsEmpty && LineSelectionRegionEnd.IsEmpty)
            {
                LineSelectionRegionStart = new PointF(DragOrigin.X, DragOrigin.Y);
                LineSelectionRegionStart = new PointF(DragOrigin.X + 10, DragOrigin.Y);


            }

            int halfDragHandleSize;
            Rectangle viewport;
            int offsetX;
            int offsetY;
            int ST_X;
            int ST_Y;
            int ED_X;
            int ED_Y;
            viewport = this.GetImageViewPort();
            offsetX = viewport.Left + this.Padding.Left + this.AutoScrollPosition.X;
            offsetY = viewport.Top + this.Padding.Top + this.AutoScrollPosition.Y;
            halfDragHandleSize = this.DragHandleSize / 2;
            ST_X = Convert.ToInt32((this.LineSelectionRegionStart.X * this.ZoomFactor) + offsetX);
            ST_Y = Convert.ToInt32((this.LineSelectionRegionStart.Y * this.ZoomFactor) + offsetY);
            ED_X = Convert.ToInt32((this.LineSelectionRegionEnd.X * this.ZoomFactor) + offsetX);
            ED_Y = Convert.ToInt32((this.LineSelectionRegionEnd.Y * this.ZoomFactor) + offsetY);

            this.DragHandles[DragHandleAnchor.LineStart].Bounds = new Rectangle(ST_X - halfDragHandleSize, ST_Y - halfDragHandleSize, this.DragHandleSize, this.DragHandleSize);
            this.DragHandles[DragHandleAnchor.LineEnd].Bounds = new Rectangle(ED_X - halfDragHandleSize, ED_Y - halfDragHandleSize, this.DragHandleSize, this.DragHandleSize);
            this.DragHandles[DragHandleAnchor.LineStart].Visible = true;
            this.DragHandles[DragHandleAnchor.LineEnd].Visible = true;
            this.DragHandles[DragHandleAnchor.TopLeft].Visible = false;
            this.DragHandles[DragHandleAnchor.TopLeft].Visible = false;
            this.DragHandles[DragHandleAnchor.TopCenter].Visible = false;
            this.DragHandles[DragHandleAnchor.TopCenter].Visible = false;
            this.DragHandles[DragHandleAnchor.TopRight].Visible = false;
            this.DragHandles[DragHandleAnchor.MiddleLeft].Visible = false;
            this.DragHandles[DragHandleAnchor.MiddleRight].Visible = false;
            this.DragHandles[DragHandleAnchor.BottomLeft].Visible = false;
            this.DragHandles[DragHandleAnchor.BottomCenter].Visible = false;
            this.DragHandles[DragHandleAnchor.BottomRight].Visible = false;
            // }

        }

        //kai
        private void ProcessSelectionMove(Point cursorPosition)
        {
            if (this.IsMoving)
            {

                Point imagePoint;

                imagePoint = this.PointToImage(cursorPosition, true);


                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:

                        ProcessSelectionMoveRectRegion(imagePoint.X, imagePoint.Y);
                        break;
                    case ImageBoxSelectionMode.Line:

                        ProcessSelectionMoveLineRegion(imagePoint.X, imagePoint.Y);
                        break;
                    default:
                        break;
                }
            }
        }
        private void ProcessSelectionMoveRectRegion(int a_X, int a_Y)
        {
            int x;
            int y;
            x = Math.Max(0, a_X - this.DragOriginOffset.X);
            y = Math.Max(0, a_Y - this.DragOriginOffset.Y);
            if (x + this.SelectionRegion.Width >= this.ViewSize.Width)
            {
                x = this.ViewSize.Width - (int)this.SelectionRegion.Width;
            }
            if (y + this.SelectionRegion.Height >= this.ViewSize.Height)
            {
                y = this.ViewSize.Height - (int)this.SelectionRegion.Height;
            }
            this.SelectionRegion = new RectangleF(x, y, this.SelectionRegion.Width, this.SelectionRegion.Height);
        }
        private void ProcessSelectionMoveLineRegion(int a_X, int a_Y)
        {
            int ST_X = Math.Max(0, a_X - this.DragOrginLineRegionST_Offset.X);
            int ST_Y = Math.Max(0, a_Y - this.DragOrginLineRegionST_Offset.Y);
            int ED_X = Math.Max(0, a_X - this.DragOrginLineRegionED_Offset.X);
            int ED_Y = Math.Max(0, a_Y - this.DragOrginLineRegionED_Offset.Y);
            ///Limit Left , Right ,Top ,Bottom
            // to do list 

            if (ST_X >= this.ViewSize.Width)
            {
                ST_X = this.ViewSize.Width - 1;
            }
            if (ST_Y >= this.ViewSize.Height)
            {
                ST_Y = this.ViewSize.Height - 1;
            }
            if (ED_X >= this.ViewSize.Width)
            {
                ED_X = this.ViewSize.Width - 1;
            }
            if (ED_Y >= this.ViewSize.Height)
            {
                ED_Y = this.ViewSize.Height - 1;
            }
            LineSelectionRegionStart = new PointF(ST_X, ST_Y);
            LineSelectionRegionEnd = new PointF(ED_X, ED_Y);

        }
        private void ProcessSelectionResize(Point cursorPosition)
        {
            if (this.IsResizing)
            {
                Point imagePosition;
                imagePosition = this.PointToImage(cursorPosition, true);

                // get the current selection
                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:
                        ProcessSelectionResizeRectRegion(imagePosition);
                        break;
                    case ImageBoxSelectionMode.Line:
                        ProcessSelectionResizeLineRegion(imagePosition);
                        break;
                    default:
                        break;
                }
            }
        }
        private void ProcessSelectionResizeRectRegion(Point a_ImagePosition)
        {
            Point imagePosition = a_ImagePosition;
            float left;
            float top;
            float right;
            float bottom;
            bool resizingTopEdge;
            bool resizingBottomEdge;
            bool resizingLeftEdge;
            bool resizingRightEdge;

            left = this.SelectionRegion.Left;
            top = this.SelectionRegion.Top;
            right = this.SelectionRegion.Right;
            bottom = this.SelectionRegion.Bottom;

            // decide which edges we're resizing
            resizingTopEdge = this.ResizeAnchor >= DragHandleAnchor.TopLeft && this.ResizeAnchor <= DragHandleAnchor.TopRight;
            resizingBottomEdge = this.ResizeAnchor >= DragHandleAnchor.BottomLeft && this.ResizeAnchor <= DragHandleAnchor.BottomRight;
            resizingLeftEdge = this.ResizeAnchor == DragHandleAnchor.TopLeft || this.ResizeAnchor == DragHandleAnchor.MiddleLeft || this.ResizeAnchor == DragHandleAnchor.BottomLeft;
            resizingRightEdge = this.ResizeAnchor == DragHandleAnchor.TopRight || this.ResizeAnchor == DragHandleAnchor.MiddleRight || this.ResizeAnchor == DragHandleAnchor.BottomRight;

            // and resize!
            if (resizingTopEdge)
            {
                top = imagePosition.Y;
                if (bottom - top < this.MinimumSelectionSize.Height)
                {
                    top = bottom - this.MinimumSelectionSize.Height;
                }
            }
            else if (resizingBottomEdge)
            {
                bottom = imagePosition.Y;
                if (bottom - top < this.MinimumSelectionSize.Height)
                {
                    bottom = top + this.MinimumSelectionSize.Height;
                }
            }

            if (resizingLeftEdge)
            {
                left = imagePosition.X;
                if (right - left < this.MinimumSelectionSize.Width)
                {
                    left = right - this.MinimumSelectionSize.Width;
                }
            }
            else if (resizingRightEdge)
            {
                right = imagePosition.X;
                if (right - left < this.MinimumSelectionSize.Width)
                {
                    right = left + this.MinimumSelectionSize.Width;
                }
            }
            this.SelectionRegion = new RectangleF(left, top, right - left, bottom - top);
        }
        private void ProcessSelectionResizeLineRegion(Point a_ImagePosition)
        {
            Point imagePosition = a_ImagePosition;
            if (imagePosition.X >= this.ViewSize.Width)
            {
                imagePosition.X = this.ViewSize.Width - 1;
            }
            if (imagePosition.Y >= this.ViewSize.Height)
            {
                imagePosition.Y = this.ViewSize.Height - 1;
            }
            switch (this.ResizeAnchor)
            {
                case DragHandleAnchor.LineStart:
                    LineSelectionRegionStart = new PointF(imagePosition.X, imagePosition.Y);
                    break;
                case DragHandleAnchor.LineEnd:
                    LineSelectionRegionEnd = new PointF(imagePosition.X, imagePosition.Y);
                    break;
            }
        }
        private void ResetDrag()
        {
            this.IsResizing = false;
            this.IsMoving = false;
            this.DragOrigin = Point.Empty;
            this.DragOriginOffset = Point.Empty;
        }

        private void SetCursor(Point point)
        {
            Cursor cursor;

            if (this.IsSelecting)
            {
                cursor = Cursors.Default;
            }
            else
            {
                DragHandleAnchor handleAnchor;

                handleAnchor = this.IsResizing ? this.ResizeAnchor : this.HitTest(point);
                if (handleAnchor != DragHandleAnchor.None && this.DragHandles[handleAnchor].Enabled)
                {
                    switch (handleAnchor)
                    {
                        case DragHandleAnchor.TopLeft:
                        case DragHandleAnchor.BottomRight:
                            cursor = Cursors.SizeNWSE;
                            break;
                        case DragHandleAnchor.TopCenter:
                        case DragHandleAnchor.BottomCenter:
                            cursor = Cursors.SizeNS;
                            break;
                        case DragHandleAnchor.TopRight:
                        case DragHandleAnchor.BottomLeft:
                            cursor = Cursors.SizeNESW;
                            break;
                        case DragHandleAnchor.MiddleLeft:
                        case DragHandleAnchor.MiddleRight:
                            cursor = Cursors.SizeWE;
                            break;
                        case DragHandleAnchor.LineStart:
                        case DragHandleAnchor.LineEnd:
                            cursor = Cursors.SizeWE;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (this.IsMoving ||
                    (SelectionMode == ImageBoxSelectionMode.Rectangle && this.SelectionRegion.Contains(this.PointToImage(point))) ||
                    (SelectionMode == ImageBoxSelectionMode.Line && this.LineSelectionRegionHitTest(this.PointToImage(point))))
                {
                    cursor = Cursors.SizeAll;
                }
                else
                {
                    cursor = Cursors.Default;
                }
            }
            this.Cursor = cursor;
        }

        private void StartResize(DragHandleAnchor anchor)
        {
            CancelEventArgs e;

            if (this.IsMoving || this.IsResizing)
            {
                throw new InvalidOperationException("A move or resize action is currently being performed.");
            }

            e = new CancelEventArgs();

            this.OnSelectionResizing(e);

            if (!e.Cancel)
            {
                this.ResizeAnchor = anchor;
                switch (SelectionMode)
                {

                    case ImageBoxSelectionMode.Rectangle:
                        this.PreviousSelectionRegion = this.SelectionRegion;
                        break;
                    case ImageBoxSelectionMode.Line:
                        this.PreviousLineSelectionRegionStart = this.LineSelectionRegionStart;
                        this.PreviousLineSelectionRegionEnd = this.LineSelectionRegionEnd;
                        break;
                    default:
                        break;
                }
                this.IsResizing = true;
            }
        }

        #endregion

        #region kai
        public void SetOptionEx(int a_nOption, bool a_bUse)
        {
            this.SetOption((eDef_Option)a_nOption, a_bUse);
        }
        public bool GetOptionEx(int a_nOption)
        {
            return this.GetOption((eDef_Option)a_nOption);
        }
        public bool IsVisibleCrossLine()
        {
            return GetOption(eDef_Option.CrossLineVisible);
        }

        public bool IsVisibleBoxOfROI()
        {
            return GetOption(eDef_Option.RoiVisible);
        }
        public bool IsVisibleLineRegion()
        {
            return GetOption(eDef_Option.LineRegionVisble);
        }


        public void RoiMoveToCenterOfImageEx()
        {
            this.RoiMoveToCenterOfImage();
        }
        #endregion
    }
}
