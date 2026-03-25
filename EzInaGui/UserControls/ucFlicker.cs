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
    public partial class ucFlicker : System.Windows.Forms.UserControl
    {
        public ucFlicker()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Interval = DEFAULT_INTERVAL;

            m_Timer = new Timer();
            m_Timer.Interval = Interval;
            m_Timer.Tick += new EventHandler(OnTimerTick);

        }

        #region Constants
        private const int DEFAULT_INTERVAL = 50;
        #endregion

        #region Variables
        private int m_nInterval;
        Timer m_Timer = null;
        bool bVisible = false;

        #endregion

        #region Enums
        public enum eImage
        {
            Error
            ,Warning
            ,Question
        }
        #endregion



        #region Properties
        /// <summary>
        ///selection flicker image
        /// </summary>
        public eImage flickerImg { get; set; }
               
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

        #endregion
        /// <summary>
        /// Handle the Tick event
        /// </summary>
        /// <param name="sender">Timer</param>
        /// <param name="e">EventArgs</param>
        void OnTimerTick(object sender, EventArgs e)
        {
            m_Timer.Enabled = false;
            bVisible = !bVisible;
            if (bVisible)
            {
                switch (flickerImg)
                {
                    case eImage.Error:
                        label_Image.ImageIndex = 0;
                        break;
                    case eImage.Question:
                        label_Image.ImageIndex = 1;
                        break;
                    case eImage.Warning:
                        label_Image.ImageIndex = 2;
                        break;
                }
            }
            else
            {
                label_Image.ImageIndex = -1;
            }
            m_Timer.Enabled = true;
        }

        #region APIs
        /// <summary>
        /// Start the Tick Control rotation
        /// </summary>
        public void Start()
        {
            if (m_Timer != null)
            {
                m_Timer.Interval = Interval;
                m_Timer.Enabled = true;
                flickerImg = eImage.Error;
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

    }//end of class
}//end of namespace
