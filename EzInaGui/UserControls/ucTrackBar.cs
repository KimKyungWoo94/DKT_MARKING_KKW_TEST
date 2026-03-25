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
	public partial class ucTrackBar : System.Windows.Forms.UserControl
	{
		#region Constants
		#endregion

		#region Enums
		#endregion

		#region Variables
		int m_nMaxValue;
		int m_nValue;

		bool m_bIsMouseDown;
		#endregion

		#region Event
		//public event EventHandler ValueChangeEvent;
		#endregion
		#region Properties
		/// <summary>
		/// 트랙바의 최대값 설정.
		/// </summary>
		[Description("최대값"), Category("사용자컨트롤설정")]
		/// <summary>
		/// 최대위치값
		/// </summary>
		public int MaxValue
		{
			get
			{
				return m_nMaxValue;
			}
			set
			{
				if (value == 0)
					m_nMaxValue = 1;
				m_nMaxValue = value;
			}
		}
		[Description("값"), Category("사용자컨트롤설정")]
		/// <summary>
		/// 현재위치값
		/// </summary>
		public int Value
		{
			get
			{
				return m_nValue;
			}

			set
			{
				if (value < 1)
					value = 1;
				m_nValue = value;
			}
		}
		[Description("트랙바의색상"), Category("사용자컨트롤설정")]
		/// <summary>
		/// 트랙바 컬러
		/// </summary>
		public Color TrackbarColor { get; set; }
		[Description("유닛설정"), Category("사용자컨트롤설정")]
		/// <summary>
		/// 유닛설정.
		/// </summary>
		public string Unit { get; set; }

		#endregion

		public ucTrackBar()
		{
			InitializeComponent();

			//ValueChangeEvent += new EventHandler(trackBarValue.ValueChanged);

			m_nMaxValue = 100;
			m_nValue = 1;
			TrackbarColor = Color.GreenYellow;
			this.DoubleBuffered = true;

			label_TrackbarValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			label_TrackbarValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			label_TrackbarValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			label_TrackbarValue.MouseLeave += new EventHandler(this.OnMouseLeave);
		}

		/// <summary>
		/// Draw position in control 
		/// </summary>
		private void DrawControl(int a_nTrackbarPos)
		{
			try
			{
				label_TrackbarValue.Invalidate(); //컨트롤러 초기화.
				label_TrackbarValue.Update();

				using (var g = label_TrackbarValue.CreateGraphics())
				{
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					using (SolidBrush brushDefault = new SolidBrush(SystemColors.Control))
					{
						g.FillRectangle(brushDefault, 0, 0, label_TrackbarValue.Width, label_TrackbarValue.Height);
					}
					using (SolidBrush brushValue = new SolidBrush(Color.FromArgb(100, TrackbarColor)))
					{
						g.FillRectangle(brushValue, 0, 0, a_nTrackbarPos, label_TrackbarValue.Height);
					}

					label_CurrValue.Text = 
						Convert.ToString(ConvertToValueOfClass(a_nTrackbarPos, label_TrackbarValue.Width, m_nMaxValue));
					label_Unit.Text = Unit;

					m_nValue = Convert.ToInt32(label_CurrValue.Text.ToString());

					//ValueChangeEvent(nCurrValue, new EventArgs());

				}
				label_TrackbarValue.Update();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}


		#region APIs
		/// <summary>
		/// 위치값으로 컨트롤러의 상태를 변경한다.
		/// </summary>
		private void SetTrackbarPosition(int a_Vpos)
		{
			try
			{
				DrawControl(ConvertToPositionOfControl(a_Vpos, m_nMaxValue, label_TrackbarValue.Width));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		/// <summary>
		/// 컨트롤러의 상태값을 위치값으로 변환한다.
		/// </summary>
		private int ConvertToValueOfClass(int a_Cpos, int a_Cmax, int a_Vmax)
		{
			//a_Cmax : a_Cpos = a_Vmax : a_Vpos
			//a_Cpos * a_Vmax = a_Cmax * a_Vpos
			if (a_Cmax == 0)
				return 1;
			int nRet = (a_Cpos * a_Vmax) / a_Cmax;
			if (nRet <= 0)
				nRet = 1;
			return nRet; //a_Vpos
		}

		/// <summary>
		/// 위치값을 컨트롤러의 상태값으로 변환한다.
		/// </summary>
		private int ConvertToPositionOfControl(int a_Vpos, int a_Vmax, int a_Cmax)
		{
			//a_Cmax : a_Cpos = a_Vmax : a_Vpos
			//a_Cpos * a_Vmax = a_Cmax * a_Vpos
			if (a_Vmax == 0)
				return  1;

			return (a_Cmax * a_Vpos) / a_Vmax; //a_Cpos

		}
		#endregion

		#region mouse event
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			m_bIsMouseDown = true;
		}
		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			m_bIsMouseDown = false;
		}
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.X > 0 && e.X <= label_TrackbarValue.Width)
			{
				if (m_bIsMouseDown)
					DrawControl(e.X);
			}
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			m_bIsMouseDown = false;
		}
		#endregion

		#region Form event
		private void ucDadaTrackBar_VisibleChanged(object sender, EventArgs e)
		{
			if(this.Visible)
			{
				label_Unit.Text = Unit;
				m_nValue = 1;
				label_CurrValue.Text = Convert.ToString(m_nValue);
				SetTrackbarPosition(ConvertToPositionOfControl(m_nValue, m_nMaxValue, label_TrackbarValue.Width));
			}
		}
        #endregion

        private void ucDadaTrackBar_Paint(object sender, PaintEventArgs e)
        {
            label_Unit.Text = Unit;
            m_nValue = 1;
            label_CurrValue.Text = Convert.ToString(m_nValue);
            SetTrackbarPosition(ConvertToPositionOfControl(m_nValue, m_nMaxValue, label_TrackbarValue.Width));
        }
    }

}
