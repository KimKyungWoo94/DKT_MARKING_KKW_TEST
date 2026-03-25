using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace EzIna.GUI.UserControls
{
	public partial class ucDadaTextBox : System.Windows.Forms.UserControl
	{
		#region Properties
		/// <summary>
		/// value
		/// </summary>
		public new string Text
		{
			get
			{
				return textBox.Text;
			}
			set
			{
				textBox.Text = value;
			}
		}

		[Description("최대 정수부 길이"), Category("사용자컨트롤설정")]
		[DefaultValue(0)]
		/// <summary>
		/// 최대 정수부 길이
		/// </summary>
		public int MaximumIntegerPartLength
		{
			get
			{
				return this.maximumIntegerPartLength;
			}
			set
			{
				this.maximumIntegerPartLength = value;
			}
		}
		[Description("최대 소수부 길이"), Category("사용자컨트롤설정")]
		[DefaultValue(0)]
		/// <summary>
		/// 최대 소수부 길이
		/// </summary>
		public int MaximumFractionPartLength
		{
			get
			{
				return this.maximumFractionPartLength;
			}
			set
			{
				this.maximumFractionPartLength = value;
			}
		}

		[Description("Unit"), Category("사용자컨트롤설정")]
		[DefaultValue("msec")]
		/// <summary>
		/// 유닛설정.
		/// </summary>
		public string Unit
		{
			get
			{
				return label_unit.Text;
			}
			set
			{
				label_unit.Text = value;
			}
		}

		[Description("Title"), Category("사용자컨트롤설정")]
		[DefaultValue("Title")]
		/// <summary>
		/// 유닛설정.
		/// </summary>
		public string Title
		{
			get
			{
				return label_title.Text;
			}
			set
			{
				label_title.Text = value;
			}
		}


		#endregion

		/// <summary>
		/// 정수 값
		/// </summary>
		public int IntegerValue
		{
			get
			{
				return Int32.Parse(Text);
			}
		}

		/// <summary>
		/// 십진수 값
		/// </summary>
		public decimal DecimalValue
		{
			get
			{
                if (string.IsNullOrEmpty(Text))
                    return 0;
				return Decimal.Parse(Text);
			}
		}

        /// <summary>
        /// 소수점 값
        /// </summary>
        public double doubleValue
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return 0;
                return double.Parse(Text);
            }
        }

        public ucDadaTextBox()
		{
			InitializeComponent();
		}

		private void textBox_Resize(object sender, EventArgs e)
		{
// 			textBox.Left = 0;
// 
// 			textBox.Top = (this.Height - textBox.Height) / 2;
// 			textBox.Width = this.Width;
// 			textBox.TextAlign = HorizontalAlignment.Center;
		}

		private void ucDadaTextBox_Resize(object sender, EventArgs e)
		{
			
			textBox.Left = 0;
			textBox.Top = (panel_textbox.Height - textBox.Height) / 2;
			textBox.Width = panel_textbox.Width;
			textBox.TextAlign = HorizontalAlignment.Center;
		}

		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			#region 키 Press 처리하기
			string inputKey = e.KeyChar.ToString();
			string previousText = Text;
			NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;

			string numberDecimalSeparator = numberFormatInfo.NumberDecimalSeparator;



			string numberGroupSeparator = numberFormatInfo.NumberGroupSeparator;

			string negativeSign = numberFormatInfo.NegativeSign;



			int numberDecimalSeparatorIndex = previousText.IndexOf(numberDecimalSeparator);

			int currentIndex = textBox.SelectionStart;


			if (Char.IsDigit(e.KeyChar))
			{
				// 소수점이 있는 경우
				if (numberDecimalSeparatorIndex > -1)
				{
					// 현재 인덱스가 소수점 인덱스보다 큰 경우
					if (currentIndex > numberDecimalSeparatorIndex)
					{
						// 소수부를 구한다.
						string fractionPart = previousText.Substring(numberDecimalSeparatorIndex);
						// 소수부 길이가 최대 소수부 길이보다 큰 경우
						if (fractionPart.Length > this.maximumFractionPartLength)
						{
							// 입력을 취소한다.
							e.Handled = true;
						}
					}
					else
					{
						// 정수부를 구한다.
						string integerPart = previousText.Substring(0, numberDecimalSeparatorIndex);
						// 마이너스 기호 인덱스를 구한다.
						int negativeSignIndex = integerPart.IndexOf(negativeSign);
						// 마이너스 기호가 있는 경우
						if (negativeSignIndex > -1)
						{
							// 정수부 길이가 최대 정수부 길이보다 큰 경우
							if (integerPart.Length > this.maximumIntegerPartLength)
							{
								// 입력을 취소한다.
								e.Handled = true;
							}
						}
						else // 마이너스 기호가 없는 경우
						{
							// 정수부 길이가 (최대 정수부 길이 - 1)보다 큰 경우
							if (integerPart.Length > (this.maximumIntegerPartLength - 1))
							{
								// 입력을 취소한다.
								e.Handled = true;
							}
						}
					}
				}
				else // 소수점이 없는 경우
				{
					// 마이너스 기호 인덱스를 구한다.
					int negativeSignIndex = previousText.IndexOf(negativeSign);
					// 마이너스 기호가 있는 경우
					if (negativeSignIndex > -1)
					{
						// 이전 텍스트 길이가 최대 정수부 길이보다 큰 경우
						if (previousText.Length > this.maximumIntegerPartLength)
						{
							// 입력을 취소한다.
							e.Handled = true;
						}
					}
					else
					{
						// 이전 텍스트 길이가 (최대 정수부 길이 - 1)보다 큰 경우
						if (previousText.Length > (this.maximumIntegerPartLength - 1))
						{
							// 입력을 취소한다.
							e.Handled = true;
						}
					}
				}
			}
			else if (inputKey.Equals(numberDecimalSeparator)) // 소수점을 입력한 경우
			{
				// 소수점이 있는 경우
				if (previousText.IndexOf(numberDecimalSeparator) > -1)
				{
					// 입력을 취소한다.
					e.Handled = true;
				}
			}
			else if (inputKey.Equals(numberGroupSeparator)) // 콤마를 입력한 경우
			{
				// 입력을 취소한다.
				e.Handled = true;
			}
			else if (inputKey.Equals(negativeSign)) // 마이너스 기호를 입력한 경우
			{
				// 마이너스 기호 인덱스를 구한다.
				int negativeSignIndex = previousText.IndexOf(negativeSign);
				// 마이너스 기호가 있는 경우
				if (negativeSignIndex > -1)
				{
					// 입력을 취소한다.
					e.Handled = true;
				}
				// 현재 위치가 맨 앞이 아닌 경우
				if (currentIndex != 0)
				{
					// 입력을 취소한다.
					e.Handled = true;
				}
			}
			else if (e.KeyChar == Convert.ToChar(Keys.Back))
			{

            }
			else
			{
				// 입력을 취소한다.
				e.Handled = true;

			}
			#endregion
		}

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
//                 textBox.Text = string.Format("{0:#,##0}", doubleValue);
//                 textBox.SelectionStart = textBox.TextLength;
//                 textBox.SelectionLength = 0;
            }
            catch (Exception ex)
            {

            }
        }

		#region Field
		/// <summary>
		/// 최대 정수부 길이
		/// </summary>

		private int maximumIntegerPartLength = 10;
		/// <summary>
		/// 최대 소수부 길이
		/// </summary>
		private int maximumFractionPartLength = 2;
		#endregion

	}



	
	
}
