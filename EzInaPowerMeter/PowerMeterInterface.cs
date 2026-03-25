using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EzIna.PowerMeter
{
    public enum eWaveLength
    {
        UNKNOWN,
        UV=355,
        VIS=515,
        YAG=1064,
        CO2=10600
    }
    public enum eZeroSetStataus
    {
        NOT_STARTED,
        IN_PROGRESS,
        FAILED,
        COMPLETED,
    }
    public interface PowerMeterInterface
    {
	    string strID {get; }
        /// <summary>
        ///     
        /// </summary>        
     
        Type DeviceType {get; }      
        bool IsDeviceConnected {get; }
        double fMeasuredPower {get;}
        bool IsMeasuring {get; }
        bool IsZeroSetExecuting {get; }  
        double fMeasureMinPower {get; }
        double fMeasureMaxPower {get; }
        double fMeasureAvgPower {get; }
        bool MeasureStart(int a_Interval,uint a_InitAVGCount);
        void MeasureStop(); 

        bool IsOverMeasureDefaultCount {get; }
        uint GetMeasureCount();     
				bool SetZero();
        eZeroSetStataus GetZeroSetStatus();
        void SetWaveLength(eWaveLength a_value);
        eWaveLength  GetWaveLength();
        void SetPowerOffset(double a_value);
        void ResetDevice();
        void DisposePM();
    }
}
