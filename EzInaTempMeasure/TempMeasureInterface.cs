using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.TempMeasure
{
    public interface ITempMeasure
    {
				string strID { get; }
				/// <summary>
				///     
				/// </summary>        

				Type DeviceType { get; }
				bool IsDeviceConnected { get; }
				bool IsMeasuring {get; }
				bool MeasureStart(int a_Interval, uint a_InitAVGCount);
				void MeasureStop();
				double fMeasureMinValue { get; }
				double fMeasureMaxValue { get; }
				double fMeasureAvgValue { get; }
				void ResetDevice();
				void DisposeTM();
		}
}
