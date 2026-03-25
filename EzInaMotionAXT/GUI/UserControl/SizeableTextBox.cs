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
    public partial class SizeableTextBox : TextBox
    {

        [DefaultValue(false)]
        [Browsable(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }
        public SizeableTextBox()
        {
            InitializeComponent();
            this.AutoSize = false;
        }

        public SizeableTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.AutoSize = false;
        }
    }
}
