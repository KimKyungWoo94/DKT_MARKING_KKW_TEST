using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace EzIna
{
    public static class FLICKER
    {
        private static System.Threading.Timer _FlickerTimer = new System.Threading.Timer(DoFlickerTimer, null, 0, 500);
        private static bool _IsOn = false;
        private static void DoFlickerTimer(object sender) { _IsOn = !_IsOn; }

        public static bool IsOn { get { return _IsOn; } }
        
        public static void Restart(int period = 500) { _FlickerTimer.Change(0, period); }
        public static void Stop() { _FlickerTimer.Change(Timeout.Infinite, Timeout.Infinite); }
    }

    public static class FlickerHelper
    {
        /// <summary>
        /// Flickering Control.BackColor by state
        /// </summary>
        public static void SetFlicker(this Control self, bool state, Color onColor, Color offColor)
        {
            self.BackColor = state && FLICKER.IsOn ? onColor : offColor;
        }
        /// <summary>
        /// Flickering Control.BackColor by state
        /// </summary>
        public static void SetFlicker(this Control self, bool state, Color onFlickerColor1, Color onFlickerColor2, Color offColor)
        {
            if (state) self.BackColor = FLICKER.IsOn ? onFlickerColor1 : onFlickerColor2;
            else       self.BackColor = offColor;
        }
        public static void SetFlicker(this Control self, Color flickerColor1, Color flickerColor2)
        {
            self.BackColor = FLICKER.IsOn ? flickerColor1 : flickerColor2;
        }
        public static void SetFlicker(this Control self, bool state, Color onFlickerColor1, Color onFlickerColor2, string onText, Color offColor, string offText)
        {
            if (state)
            {
                self.BackColor = FLICKER.IsOn ? onFlickerColor1 : onFlickerColor2;
                self.Text = onText;
            }
            else
            {
                self.BackColor = offColor;
                self.Text = offText;
            }
        }
        /// <summary>
        /// Flickering Control.ImageIndex by state
        /// </summary>
        public static void SetFlicker(this Button self, bool state, int onIndex, int offIndex)
        {
            self.ImageIndex = state && FLICKER.IsOn ? onIndex : offIndex;
        }
        /// <summary>
        /// Flickering Control.ImageIndex by state
        /// </summary>
        public static void SetFlicker(this Button self, bool state, int onFlickerIndex1, int onFlickerIndex2, int offIndex)
        {
            if (state) self.ImageIndex = FLICKER.IsOn ? onFlickerIndex1 : onFlickerIndex2;
            else       self.ImageIndex = offIndex;
        }
    }

}
