using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EzIna.Laser
{
    /// <summary>
    /// Laser    
    /// </summary>		
    public interface LaserInterface
    {
        /// <summary>
        /// 
        /// </summary>
        string strID {get; }
        /// <summary>
        ///     
        /// </summary>        
        string strModelName {get; }
        /// <summary>
        /// 
        /// </summary>
        Type DeviceType {get; }        
        /// <summary>
        /// 
        /// </summary>
        bool  IsSystemFault {get; }        
        /// <summary>
        ///     
        /// </summary>
        bool IsLaserReady {get; }
        /// <summary>
        ///     
        /// </summary>
        bool IsConnected {get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCommConnected {get; }
        /// <summary>
        ///  = Diode On 
        /// </summary>
        bool IsEmissionOn{get; }
        /// <summary>
        ///  = Diode On 
        /// </summary>
        bool EmissionOnOff {set; }
				/// <summary>
				/// 
				/// </summary>
				bool IsQSW_On {get; }
				/// <summary>
				/// 
				/// </summary>
				bool QSW_OnOff {set; }

        /// <summary>
        ///     
        /// </summary>
        bool IsGateOpen {get; }
			
        /// <summary>
        ///     
        /// </summary>
        bool GateOpen {set; }
        
				/// <summary>
				/// 
				/// </summary>
				TRIG_MODE TriggerMode {get; set; }
				/// <summary>
				/// 
				/// </summary>
				GATE_MODE GateMode {get;set; }
				/// <summary>
        /// 
        /// </summary>
        bool IsShutterOpen {get; }
        /// <summary>
        ///     
        /// </summary>
        bool ShutterOpen {set; }
        /// <summary>
        ///     
        /// </summary>
        double RepetitionRate {get ;set; }
		/// <summary>
		/// 
		/// </summary>
		double EPRF {get;set; }
		/// <summary>
		/// 
		/// </summary>
		double DiodeCurrent {get; }
		/// <summary>
		/// 
		/// </summary>
		double SetDiodeCurrent {get;set; }
        /// <summary>
        ///     
        /// </summary>
        TimeSpan RemainWarmUptime {get;}   
         /// <summary>
         /// 
         /// </summary>
        void Reset();
        /// <summary>
        /// 
        /// </summary>
        void DisposeLaser();                    
    }
}
