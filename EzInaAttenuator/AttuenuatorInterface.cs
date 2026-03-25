using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Attenuator
{
    public interface AttuenuatorInterface
    {
        Type DeviceType {get; }

        bool IsConnected {get; }
        bool IsFault {get; }
        string strID {get; }
        string strModelName {get; }
        double fCurrentPower {get;  set; }
        double fMinPower {get;set; }
        double fMaxPower {get;set; }
        double fAngle     {get; set; }
        double fPosition  {get; }
        int    WaveLength {get; }

        int    HomeSearchOption { get; set; }
        bool   IsPositiveLimit {get; }
        bool   IsNagativeLimit {get; }
        bool   IsMotorEnabled {get; }
        bool   IsMotionDone {get; }
        bool   IsInPosition {get; }
        bool   IsHomeDone {get; }
        void   DisposeDevice();
        void   DeviceReset();
        void   Zeroset();
        void   MotorEnable(bool a_value);
        void   StopMotion ();
        void   StartHoming(bool a_bAuto);
        void   StopHoming(bool  a_bAuto);
     

        void   MoveAbSolute(double a_Pos);
        void   MoveRelative(double a_Pos);
        
    }
}
