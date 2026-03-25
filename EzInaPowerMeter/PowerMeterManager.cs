using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.PowerMeter
{
    public sealed class PowerMeterManager : SingleTone<PowerMeterManager>, IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        private List<PowerMeterInterface> m_Itemlist;
        ~PowerMeterManager()
        {
            Dispose(false);
        }
        protected override void OnCreateInstance()
        {
            m_Itemlist=new List<PowerMeterInterface>();
            base.OnCreateInstance();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool a_Disposeing)
        {
            if (IsDisposed)
                return;

            if (a_Disposeing)
            {
                IsDisposing = true;
                if (m_Itemlist.Count > 0)
                {
                    foreach (PowerMeterInterface item in m_Itemlist)
                    {
                        item.DisposePM();
                    }
                }
                m_Itemlist.Clear();
            }
            IsDisposed = true;
            IsDisposing = false;
        }
        public PowerMeterInterface this[int a_IDX]
        {
            get { return GetItem(a_IDX); }
        }
        public PowerMeterInterface GetItem(int a_IDX)
        {
            return m_Itemlist[a_IDX];
        }
    }
}
