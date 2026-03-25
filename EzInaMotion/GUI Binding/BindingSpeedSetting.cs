using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion
{
    public class BindingSpeedSetting : NotifyProperyChangedBase
    {
        private GDMotion.stMotionSpeedSetting m_Speed;
        private List<Binding> m_BindingList;
        #region     Binding Text
        public Binding BindingMaxSpeed
        {
            get
            {
                return m_BindingList[0];
            }
        }
        public Binding BindingAccel
        {
            get
            {
                return m_BindingList[1];
            }
        }
        public Binding BindingDecel
        {
            get
            {
                return m_BindingList[2];
            }
        }
        public Binding BindingCurveAccel
        {
            get
            {
                return m_BindingList[3];
            }
        }
        public Binding BindingCurveDecel
        {
            get
            {
                return m_BindingList[4];
            }
        }
        #endregion   Binding Text
        #region Property
        public GDMotion.stMotionSpeedSetting Speed
        {
            get{ return m_Speed; }            
            set
            {
                MaxSpeed=value.m_dMaxSpeed;
                Accel=value.m_dAcceleration;
                Decel=value.m_dDeceleration;
                CurveAccel=value.m_dCurveAcceleration;
                CurveDecel=value.m_dCurveDeceleration;
            }
        }


        public double MaxSpeed
        {
            get
            {             
                return m_Speed.m_dMaxSpeed;             
            }
            set
            {                
                CheckPropertyChanged<double>("MaxSpeed", ref m_Speed.m_dMaxSpeed, ref value);
            }
        }
        public double Accel
        {
            get
            {
              
                    return m_Speed.m_dAcceleration;
             
            }
            set
            {
               
                CheckPropertyChanged<double>("Accel", ref m_Speed.m_dAcceleration, ref value);
            }
        }

        public double Decel
        {
            get
            {
                    return m_Speed.m_dDeceleration;
              
            }
            set
            {               
                    
                    CheckPropertyChanged<double>("Decel", ref m_Speed.m_dDeceleration, ref value);
             
            }
        }

        public double CurveAccel
        {
            get
            {               
                    return m_Speed.m_dCurveAcceleration;
           
            }
            set
            {                               
                   CheckPropertyChanged<double>("CurveAccel", ref m_Speed.m_dCurveAcceleration, ref value);               
            }
        }

        public double CurveDecel
        {
            get
            {
              
                    return m_Speed.m_dCurveDeceleration;
               
            }
            set
            {                                 
                   CheckPropertyChanged<double>("CurveDecel", ref m_Speed.m_dCurveDeceleration, ref value);               
            }
        }
        #endregion Property
        public BindingSpeedSetting()
        {
            
            m_BindingList = new List<Binding>();
            m_BindingList.Add(new Binding("Text", this, "MaxSpeed", false, DataSourceUpdateMode.OnValidation));
            m_BindingList.Add(new Binding("Text", this, "Accel", false, DataSourceUpdateMode.OnValidation));
            m_BindingList.Add(new Binding("Text", this, "Decel", false, DataSourceUpdateMode.OnValidation));
            m_BindingList.Add(new Binding("Text", this, "CurveAccel", false, DataSourceUpdateMode.OnValidation));
            m_BindingList.Add(new Binding("Text", this, "CurveDecel", false, DataSourceUpdateMode.OnValidation));
            foreach (Binding bd in m_BindingList)
            {
                bd.FormattingEnabled = true;
                bd.FormatString = "F4";
                bd.BindingComplete += new BindingCompleteEventHandler(this.BingComplete);
            }
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
    }
}
