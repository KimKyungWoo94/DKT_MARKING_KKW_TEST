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
	public partial class ucProcessProgressBar : System.Windows.Forms.UserControl
	{
		#region Constants
		private const int DEFAULT_INTERVAL = 50;
		private const int DEFAULT_CIRCLE_DIAMETER = 2;
		private readonly Color DEFAULT_CIRCLE_COLOR = Color.FromArgb(192, 192, 192);
		private readonly Size DEFAULT_CTRL_SIZE = new Size(32, 32);

		#endregion

		#region Enums
		public enum eRotation
		{
			CCW,//counter clockwise
			CW  //clockwise
		}
		#endregion

		#region Variables
		private int m_nInterval;
		PointF m_ptCenter = new PointF();
		int m_nInnerRadius = 0;
		int m_nStartAngle = 0;
		int m_nStartAlpha = 0;
		int m_nRotateCount = 0;
		int m_AngleIncrement = 0;
		int m_AlphaDecrement = 0;
		Timer m_Timer = null;
		#endregion


		#region Properties

		/// <summary>
		/// Time interval for each tick.
		/// </summary>
		public int Interval
		{
			get
			{
				return m_nInterval;
			}
			set
			{
				if (value > 0)
				{
					m_nInterval = value;
				}
				else
				{
					m_nInterval = DEFAULT_INTERVAL;
				}
			}
		}



		/// <summary>
		/// Color of the circle
		/// </summary>
		public Color CircleColor { get; set; }

		/// <summary>
		/// Size of the circle
		/// </summary>
		public int CircleDiameter { get; set; }

		/// <summary>
		/// Direction of rotation - CLOCKWISE/ANTICLOCKWISE
		/// </summary>
		public eRotation Rotation { get; set; }



		#endregion
		/// <summary>
		/// Handle the Tick event
		/// </summary>
		/// <param name="sender">Timer</param>
		/// <param name="e">EventArgs</param>
		void OnTimerTick(object sender, EventArgs e)
		{
			if (Rotation == eRotation.CW)
			{
				m_nStartAngle += m_AngleIncrement;

				if (m_nStartAngle >= 360)
					m_nStartAngle = 0;
			}
			else if (Rotation == eRotation.CCW)
			{
				m_nStartAngle -= m_AngleIncrement;

				if (m_nStartAngle <= -360)
					m_nStartAngle = 0;
			}

			Invalidate();
		}

		public ucProcessProgressBar()
		{
			this.DoubleBuffered = true;
			InitializeComponent();

			this.BackColor = Color.Transparent;
			this.CircleColor = DEFAULT_CIRCLE_COLOR;
			this.CircleDiameter = DEFAULT_CIRCLE_DIAMETER;
			this.MinimumSize = DEFAULT_CTRL_SIZE;
			this.Interval = DEFAULT_INTERVAL;
			this.Rotation = eRotation.CW;

			m_nInnerRadius = 0;
			m_nStartAngle = 0;
			m_nRotateCount = 12;
			m_nStartAlpha = 255;
			m_AngleIncrement = (int)(360 / m_nRotateCount);
			m_AlphaDecrement = (int)(m_nStartAlpha / m_nRotateCount);

			m_Timer = new Timer();
			m_Timer.Interval = this.Interval;
			m_Timer.Tick += new EventHandler(OnTimerTick);
		}


		/// <summary>
		/// Handles the Paint Event of the control
		/// </summary>
		/// <param name="e">PaintEventArgs</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// All the painting will be handled by us.
			//base.OnPaint(e);
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			int alpha = m_nStartAlpha;
			int angle = m_nStartAngle;

			int width = (this.Width < this.Height) ? this.Width : this.Height;
			m_ptCenter = new PointF(this.Width / 2, this.Height / 2);

			m_nInnerRadius = (width / 2 ) - (CircleDiameter / 2);

			// Render the spokes
			for (int i = 0; i < m_nRotateCount; i++)
			{
				PointF pt = new PointF(m_nInnerRadius * (float)Math.Cos(ConvertDegreesToRadians(angle)), m_nInnerRadius * (float)Math.Sin(ConvertDegreesToRadians(angle)));

				pt.X += m_ptCenter.X;
				pt.Y += m_ptCenter.Y;
				Brush brushBackColor = new SolidBrush(Color.FromArgb(alpha, this.CircleColor));
				e.Graphics.FillEllipse(brushBackColor, pt.X - CircleDiameter / 2, pt.Y - CircleDiameter / 2, CircleDiameter, CircleDiameter );

				if (Rotation == eRotation.CW)
				{
					angle -= m_AngleIncrement;
				}
				else if (Rotation == eRotation.CCW)
				{
					angle += m_AngleIncrement;
				}

				alpha -= m_AlphaDecrement;
			}
		}

		/// <summary>
		/// Converts Degrees to Radians
		/// </summary>
		/// <param name="degrees">Degrees</param>
		/// <returns></returns>
		private double ConvertDegreesToRadians(int degrees)
		{
			return ((Math.PI / (double)180) * degrees);
		}


		#region APIs

		/// <summary>
		/// Start the Tick Control rotation
		/// </summary>
		public void Start()
		{
			if (m_Timer != null)
			{
				m_Timer.Interval = this.Interval;
				m_Timer.Enabled = true;
			}
		}

		/// <summary>
		/// Stop the Tick Control rotation
		/// </summary>
		public void Stop()
		{
			if (m_Timer != null)
			{
				m_Timer.Enabled = false;
			}
		}

		#endregion



	}
}
