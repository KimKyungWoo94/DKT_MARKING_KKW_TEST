using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion.AXT.GUI
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
            e.Handled = FilterNumber(e.KeyChar.ToString());

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
