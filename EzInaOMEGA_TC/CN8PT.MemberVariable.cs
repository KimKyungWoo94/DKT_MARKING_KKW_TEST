using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.TempMeasure.OMEGA
{
		public sealed partial class CN8PT
		{
				bool IsDisposed = false;
				bool IsDisposing = false;
				bool m_bLoopEnable = false;
				bool m_bHandShakeSuccess = false;
				Thread m_pLoopThread;
				Thread m_pMeasureThread;
				int m_iMeasureInterval;
				bool m_bMeasureEnable;
				double m_fMeasureValue;
				double m_fMeasureAvgValue;
				double m_fMeasureMinValue;
				double m_fMeasureMaxValue;
				bool m_bFirstMeasureAvgSet;
				bool m_bDeviceContinousPacketMode;
				double m_fDeviceContinousPacketPeriod;
				List<double> m_fMeasureAvgValueList;
				uint m_iMeasureAvgCountDefault;
				uint m_iMeasureCount;
				StringBuilder m_MakePacketStringBuilder = new StringBuilder();
				StringBuilder m_MakeValueStringBuilder = new StringBuilder();
				StringBuilder m_RecvDataStringBuilder = new StringBuilder();
				string m_strConfigSection = "";
				string m_strFirmwareVersion = "";
		}
}
