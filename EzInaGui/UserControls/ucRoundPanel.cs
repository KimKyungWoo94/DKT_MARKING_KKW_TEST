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

namespace EzIna.GUI.UserControls
{
	[DefaultEvent("Click")]

	public partial class ucRoundedPanel : System.Windows.Forms.UserControl
	{
		int _wh = 20;
		int _BorderSize = 1;
		string _Caption = "EzIna";

		Color _clBegin = Color.FromArgb(26, 26, 26);
		Color _clEnd = Color.FromArgb(14, 14, 14);
		Color _clBorder = Color.FromArgb(0, 0, 0);

		StringAlignment _TextAlignVertical = StringAlignment.Center;
		StringAlignment _TextAlignHorizontal = StringAlignment.Center;

		public ucRoundedPanel()
		{
			this.DoubleBuffered = true;
		}

		[Category("사용자컨트롤[Border]"), Description("Radius")]
		public int BorderRadius
		{
			get { return _wh; }
			set { _wh = value; Invalidate(); }
		}
		[Category("사용자컨트롤[Border]"), Description("BorderSize")]
		public int BorderSize
		{
			get { return _BorderSize; }
			set { _BorderSize = value; Invalidate(); }
		}
		[Category("사용자컨트롤[Border]"), Description("Border Color")]
		public Color clBorder
		{
			get { return _clBorder; }
			set { _clBorder = value; Invalidate(); }
		}

		[Category("사용자컨트롤[Gradation]"), Description("Gradation Color0")]
		public Color clBegin
		{
			get { return _clBegin; }
			set { _clBegin = value; Invalidate(); }
		}

		[Category("사용자컨트롤[Gradation]"), Description("Gradation Color1")]
		public Color clEnd
		{
			get { return _clEnd; }
			set { _clEnd = value; Invalidate(); }
		}
		[Category("사용자컨트롤[Text]"), Description("Text")]
		public string Caption
		{
			get { return _Caption; }
			set { _Caption = value; Invalidate(); }

		}
		[Category("사용자컨트롤[Text]"), Description("TextAlign_Vertical")]
		public StringAlignment TextAlignVertical
		{
			get { return _TextAlignVertical; }
			set { _TextAlignVertical = value; Invalidate(); }
		}
		[Category("사용자컨트롤[Text]"), Description("TextAlign_Horizontal")]
		public StringAlignment TextAlignHorizontal
		{
			get { return _TextAlignHorizontal; }
			set { _TextAlignHorizontal = value; Invalidate(); }
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			GraphicsPath gp = new GraphicsPath();

			float fStartPoint = _BorderSize;

			gp.AddArc(new RectangleF(fStartPoint, fStartPoint, _wh, _wh), 180, 90);
			gp.AddArc(new RectangleF(Width - _wh - fStartPoint, fStartPoint, _wh, _wh), 270, 90);
			gp.AddArc(new RectangleF(Width - _wh - fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 0, 90);
			gp.AddArc(new RectangleF(fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 90, 90);
			e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, clBegin, clEnd, 90), gp);

			if (_BorderSize > 0)
			{
				GraphicsPath gp2 = new GraphicsPath();
				//fStartPoint = (int)Math.Ceiling(Convert.ToDouble(_BorderSize) / 2.0);
				fStartPoint = _BorderSize;
				gp2.AddArc(new RectangleF(0, 0, _wh, _wh), 180, 90);
				gp2.AddArc(new RectangleF(Width - _wh - fStartPoint, 0, _wh, _wh), 270, 90);
				gp2.AddArc(new RectangleF(Width - _wh - fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 0, 90);
				gp2.AddArc(new RectangleF(0, Height - _wh - fStartPoint, _wh, _wh), 90, 90);
				gp2.AddArc(new RectangleF(0, 0, _wh, _wh), 180, 90);
				e.Graphics.DrawPath(new Pen(_clBorder, _BorderSize), gp2);
			}

			e.Graphics.DrawString(_Caption, this.Font, new SolidBrush(this.ForeColor), ClientRectangle, new StringFormat() { LineAlignment = _TextAlignVertical, Alignment = _TextAlignHorizontal }); //


			base.OnPaint(e);
/*
			if (_wh == 0)
			{
				gp.AddRectangle(new Rectangle(0,0,Width,Height));

			}
			else
			{
				float fStartPoint = _BorderSize;
				gp.AddArc(new RectangleF(fStartPoint, fStartPoint, _wh, _wh), 180, 90);
				gp.AddArc(new RectangleF(Width - _wh - fStartPoint, fStartPoint, _wh, _wh), 270, 90);
				gp.AddArc(new RectangleF(Width - _wh - fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 0, 90);
				gp.AddArc(new RectangleF(fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 90, 90);
				e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, clBegin, clEnd, 90), gp);

				GraphicsPath gp2 = new GraphicsPath();
				//fStartPoint = (int)Math.Ceiling(Convert.ToDouble(_BorderSize) / 2.0);
				fStartPoint = Convert.ToSingle(_BorderSize / 1.0f);
				gp2.AddArc(new RectangleF(fStartPoint, fStartPoint, _wh, _wh), 180, 90);
				gp2.AddArc(new RectangleF(Width - _wh - fStartPoint, fStartPoint, _wh, _wh), 270, 90);
				gp2.AddArc(new RectangleF(Width - _wh - fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 0, 90);
				gp2.AddArc(new RectangleF(fStartPoint, Height - _wh - fStartPoint, _wh, _wh), 90, 90);
				gp2.AddArc(new RectangleF(fStartPoint, fStartPoint, _wh, _wh), 180, 90);
				e.Graphics.DrawPath(new Pen(_clBorder, _BorderSize), gp2);
			}
			e.Graphics.DrawString(_Caption, this.Font, new SolidBrush(this.ForeColor), ClientRectangle, new StringFormat() { LineAlignment = _TextAlignVertical, Alignment = _TextAlignHorizontal }); //
			base.OnPaint(e);
			*/
		}
	}
}

