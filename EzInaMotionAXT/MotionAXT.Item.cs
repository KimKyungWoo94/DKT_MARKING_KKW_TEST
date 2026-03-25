using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Motion
{
    public class MotionAXTItem : MotionItem
    {
        public MotionAXTItem(int a_iAxis, string a_strAxisName) : base(a_iAxis, a_strAxisName)
        {

        }

        public stMotionAXT_ParamSetting            m_stParamSetting;
        public stMotionAXT_SignalSetting           m_stSignalSetting;
        public stMotionAXT_SWLimitSetting          m_stSWLimitSetting;
        public stMotionAXT_HomeSearchSetting       m_stHomeSearchSetting;
        private bool        m_IsDisposed=false;
        private bool        m_IsDisposing=false;


        public override void Dispose(bool a_Disposing)
        {
            if(this.m_IsDisposed==true)
                return;
            if(a_Disposing)
            {

            }

            this.m_IsDisposed=true;

            base.Dispose(a_Disposing);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Terminate()
        {
            base.Terminate();
        }

        public override bool OpenConfigureation(bool a_bOption)
        {
            return base.OpenConfigureation(a_bOption);
        }
   }
}
