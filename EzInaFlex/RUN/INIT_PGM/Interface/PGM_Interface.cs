using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public abstract class PGM_Interface
    {
        // File 
        public abstract bool Make_Pgm();

        public abstract string Get_FilePath();

        public abstract string Get_RegisterToAerotechPath();


        // motor condition
        public abstract void SetGalvoCondition(string NameX, string NameY, double Velcoity, double Accel);


        public abstract void SetStageXYCondition(string NameX, string NameY, double Velcoity, double Accel);

        public abstract void SetStageZCondition(string NameZ, double Velocity, double Accel);

        
        // Laser Condition
        public abstract void SetLaserMode(int nMode);

        public abstract void SetPSOMode(bool bPsoMode);

        public abstract void SetLaserFreq(double fFreq, double DutyPercent);


        public abstract void SetGalvoCalPath(string Path);
        public abstract void SetGalvoRotate(double rtoate);
    }
}
