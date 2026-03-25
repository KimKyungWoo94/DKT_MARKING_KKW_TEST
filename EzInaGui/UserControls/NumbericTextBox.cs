using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public partial class NumbericTextBox : SizeableTextBox
    {
        public NumbericTextBox()
        {
            InitializeComponent();
        }

        public NumbericTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
						if (Text == "")
						{
								if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\\b\\-]$"))
								{
										e.Handled= true;
								}
						}
						else
						{
								if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\\b\\.]$"))
								{
										e.Handled= true;
										if(e.KeyChar.ToString()=="-")
										{
												if (Text[0] == '-')
												{
														Text=Text.Remove(0,1);
												}
												else
												{
														Text=string.Format("-{0}",Text);
												}				
												Select(Text.Length,0);							
										}
								}
								
						}

						
						
            base.OnKeyPress(e);
        }
        protected bool FilterNumber(string a_str)
        {
            if (Text == "")
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(a_str, "^[0-9\\b\\-]$"))
                {
                    return true;
                }
            }
            else
            {								
                if (!System.Text.RegularExpressions.Regex.IsMatch(a_str, "^[0-9\\b\\.]$"))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
