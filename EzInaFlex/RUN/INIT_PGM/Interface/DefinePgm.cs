using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public class AxisParam
    {
        // Name
        public string AxisX_Name { get; set; }
        public string AxisY_Name { get; set; }
        public string AxisZ_Name { get; set; }
        public string GalvoX_Name { get; set; }
        public string GalvoY_Name { get; set; }

        // Move Profile
        public double AxisX_Velocity { get; set; }
        public double AxisX_RampRate { get; set; }
        public double AxisY_Velocity { get; set; }
        public double AxisY_RampRate { get; set; }
        public double AxisZ_Velocity { get; set; }
        public double AxisZ_RampRate { get; set; }


        // Laser Mode

    }

    public class ProcessParam
    {
        public double Marking_GalvoVelocity { get; set; }
        public double Marking_GalvoRampRate { get; set; }
				// For RTC5
				public double Marking_GalveDelay {get;set; }

				public double Jump_GalvoDelay {get;set; }
        public double Jump_GalvoXVelocity { get; set; }
        public double Jump_GalvoXRampRate { get; set; }

        public double Jump_GalvoYVelocity { get; set; }
        public double Jump_GalvoYRampRate { get; set; }

    }

    public class ProcessLaserMode
    {
        public double LaserFrequency { get; set; }
        public double LaserDutyCycle_Percent { get; set; }
        public int PsoMode { get; set; }
        public int LaserMode { get; set; }
    }
    public class PGM_Path
    {
        public string PgmPath { get; set; }
        public string GalvoCalibrationPath { get; set; }
        public string Path_Register { get; set;}
        
    }
}
