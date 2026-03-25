using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public class CountsPerUnit_Pgm : PGM_Interface
    {
        private AxisParam AxisParameter = new AxisParam();
        private ProcessParam ProcessParameter = new ProcessParam();
        private ProcessLaserMode ProcessLaserMode = new ProcessLaserMode();
        private PGM_Path m_Path = new PGM_Path();

        private int LaserMode;
        private double Distance;
        private bool PosMode;
        private double LaserFreq;
        private double DutyCyclePercent;
        private double GalvoRotate;
        public CountsPerUnit_Pgm()
        {
            m_Path.PgmPath = EzIna.FA.DIR.INIT_PGM + "CountsPerUnit.pgm";
            m_Path.Path_Register = EzIna.FA.DIR.INIT_PGM + "Register_CountsPerUnit.pgm";
        }
        public void SetCPU_distance(double _distance)
        {
            Distance = _distance / 2.0;
        }
        public void Register_CPU_To_Aerotech(double cpuX, double cpuY)
        {

        }
        // File 
        public override void SetGalvoCalPath(string path)
        {
            m_Path.GalvoCalibrationPath = path;
        }

        public override void SetLaserFreq(double _fFreq, double _DutyPercent)
        {
            LaserFreq = _fFreq;
            DutyCyclePercent = _DutyPercent;

        }
        public override void SetGalvoRotate(double _Rtoate)
        {
            GalvoRotate = _Rtoate;
        }



        public override string Get_FilePath()
        {
            return m_Path.PgmPath;
        }
        public override string Get_RegisterToAerotechPath()
        {
            return m_Path.Path_Register;
        }

        // motor condition
        public override void SetGalvoCondition(string _NameX, string _NameY, double _Velocity, double _Accel)
        {
            AxisParameter.GalvoX_Name = _NameX;
            AxisParameter.GalvoY_Name = _NameY;

            ProcessParameter.Marking_GalvoVelocity = _Velocity;
            ProcessParameter.Marking_GalvoRampRate = _Accel;

            ProcessParameter.Jump_GalvoXVelocity = _Velocity;
            ProcessParameter.Jump_GalvoYVelocity = _Velocity;

            ProcessParameter.Jump_GalvoXRampRate = _Accel;
            ProcessParameter.Jump_GalvoYRampRate = _Accel;
        }

        public override void SetStageXYCondition(string _NameX, string _NameY, double _Velocity, double _Accel)
        {
            AxisParameter.AxisX_Name = _NameX;
            AxisParameter.AxisY_Name = _NameY;
            AxisParameter.AxisX_Velocity = _Velocity;
            AxisParameter.AxisX_RampRate = _Accel;
            AxisParameter.AxisY_Velocity = _Velocity;
            AxisParameter.AxisY_RampRate = _Accel;
        }

        public override void SetStageZCondition(string _NameZ, double _Velocity, double _Accel)
        {
            AxisParameter.AxisZ_Name = _NameZ;
            AxisParameter.AxisZ_Velocity = _Velocity;
            AxisParameter.AxisZ_RampRate = _Accel;
        }

        // Laser Condition
        public override void SetLaserMode(int _nMode)
        {
            LaserMode = _nMode;
        }

        public override void SetPSOMode(bool _bPsoMode)
        {
            PosMode = _bPsoMode;
        }
        public bool Make_Pgm_Register(int GalvoX_Index, int GalvoY_Index, double CpuX, double CpuY)
        {

            System.IO.StreamWriter sw = new System.IO.StreamWriter(m_Path.Path_Register, false, Encoding.Default);
            if (sw != null)
            {

                //CountsPerUnit
                //EmulatedQuadratureDivider
                sw.WriteLine(string.Format("{0}{1} {2} {3}", "CountsPerUnit.", AxisParameter.GalvoX_Name, "=", CpuX));
                sw.WriteLine(string.Format("{0}{1} {2} {3}", "CountsPerUnit.", AxisParameter.GalvoY_Name, "=", CpuY));

                sw.WriteLine(string.Format("{0}{1} {2} {3}", "EmulatedQuadratureDivider.", AxisParameter.GalvoX_Name, "=", CpuX / 6250));
                sw.WriteLine(string.Format("{0}{1} {2} {3}", "EmulatedQuadratureDivider.", AxisParameter.GalvoY_Name, "=", CpuY / 6250));


                sw.WriteLine(string.Format("{0}", "# DEFINE ParameterFileName $strtask[0]"));
                sw.WriteLine(string.Format("{0}", "ParameterFileName =  READCONFIGSTRING PARAMETERFILE"));
                sw.WriteLine(string.Format("{0} {1} {2} {3}", "WRITEPARAM(ParameterFileName, ", GalvoX_Index,
                                            "PARAMETERID_CountsPerUnit, "), CpuX);
                sw.WriteLine(string.Format("{0} {1} {2} {3}", "WRITEPARAM(ParameterFileName, ", GalvoY_Index,
                                            "PARAMETERID_CountsPerUnit, "), CpuY);


                sw.WriteLine(string.Format("{0} {1} {2} {3}", "WRITEPARAM(ParameterFileName, ", GalvoX_Index,
                                            "PARAMETERID_EmulatedQuadratureDivider, "), CpuX / 6250);
                sw.WriteLine(string.Format("{0} {1} {2} {3}", "WRITEPARAM(ParameterFileName, ", GalvoY_Index,
                                            "PARAMETERID_EmulatedQuadratureDivider, "), CpuY / 6250);
                sw.WriteLine(string.Format("{0}", "END PROGRAM"));





                //WRITEPARAM(ParameterFileName, GalvoIndex1, PARAMETERID_TorqueAngleOffset, $MotorOffset1)

            }
            sw.Close();
            return true;
        }

        public override bool Make_Pgm()
        {

            double fDefaultDelay = 10.0;
            System.IO.StreamWriter sw = null;
            try
            {

                using (sw = new System.IO.StreamWriter(new System.IO.FileStream(m_Path.PgmPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite), Encoding.Default))
                    if (sw != null)
                    {

                        // file information
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", "; FILE INFORMATION"));
                        sw.WriteLine(string.Format("{0}", "; NAME : CPU PGM"));
                        sw.WriteLine(string.Format("{0}{1}", "; CREATE : ", DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss")));
                        sw.WriteLine(string.Format("{0}", "; MODIFY : ------"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));

                        // 최소 설정
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= Default Param =============="));
                        sw.WriteLine(string.Format("{0}", "ABSOLUTE"));
                        sw.WriteLine(string.Format("{0}", "SECOND"));
                        sw.WriteLine(string.Format("{0}", "PRIMARY"));
                        sw.WriteLine(string.Format("{0}", "LOOKAHEAD FAST"));
                        sw.WriteLine(string.Format("{0}", "ExecuteNumLines = 110"));
                        sw.WriteLine(string.Format("{0}", "MotionUpdateRate = 48"));
                        sw.WriteLine(string.Format("{0}", "MotionBufferSize = 4"));
                        sw.WriteLine(string.Format("{0}", "RAMP TYPE SINE"));
                        sw.WriteLine(string.Format("{0}", "G361"));
                        sw.WriteLine(string.Format("{0}", "VELOCITY OFF"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));


                        // 가공 속도 및 가감속 설정
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= MOVE PROFILE =============="));
                        sw.WriteLine(string.Format("{0}", "RAMP MODE RATE"));
                        sw.WriteLine(string.Format("{0} {1} {2} ", "RAMP RATE ", AxisParameter.AxisX_Name, AxisParameter.AxisX_RampRate));
                        sw.WriteLine(string.Format("{0} {1} {2} ", "RAMP RATE ", AxisParameter.AxisY_Name, AxisParameter.AxisY_RampRate));
                        sw.WriteLine(string.Format("{0} {1} {2} ", "RAMP RATE ", AxisParameter.AxisZ_Name, AxisParameter.AxisZ_RampRate));
                        sw.WriteLine(string.Format("{0} {1} {2} ", "RAMP RATE ", AxisParameter.GalvoX_Name, ProcessParameter.Jump_GalvoXRampRate));
                        sw.WriteLine(string.Format("{0} {1} {2} ", "RAMP RATE ", AxisParameter.GalvoY_Name, ProcessParameter.Jump_GalvoYRampRate));

                        //todo list
                        sw.WriteLine(string.Format("{0} {1} ", "RAMP RATE ", ProcessParameter.Jump_GalvoYRampRate));


                        sw.WriteLine(string.Format("{0}{1} {2} ", AxisParameter.AxisX_Name, "F", AxisParameter.AxisX_Velocity));
                        sw.WriteLine(string.Format("{0}{1} {2} ", AxisParameter.AxisY_Name, "F", AxisParameter.AxisY_Velocity));
                        sw.WriteLine(string.Format("{0}{1} {2} ", AxisParameter.AxisZ_Name, "F", AxisParameter.AxisZ_Velocity));
                        sw.WriteLine(string.Format("{0}{1} {2} ", AxisParameter.GalvoX_Name, "F", ProcessParameter.Jump_GalvoXVelocity));
                        sw.WriteLine(string.Format("{0}{1} {2} ", AxisParameter.GalvoY_Name, "F", ProcessParameter.Jump_GalvoYVelocity));



                        sw.WriteLine(string.Format("{0} {1:0}", "F ", ProcessParameter.Marking_GalvoVelocity));
                        sw.WriteLine(string.Format("{0}", ";************************************"));


                        // load cal & rotate 등....
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= INIT =============="));
                        sw.WriteLine(string.Format("{0}", "IFOV OFF"));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));
                        sw.WriteLine(string.Format("G90 G0 {0} 0  {1} 0", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO PROJECTION ", AxisParameter.GalvoX_Name, "OFF"));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO ROTATION", AxisParameter.GalvoX_Name, GalvoRotate));
                        sw.WriteLine(string.Format("{0} {1}{2}{3} {4} ", "LOADCALFILE", "\"", m_Path.GalvoCalibrationPath.Trim(), "\",", "GALVO_CAL_2D"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASERONDELAY", AxisParameter.GalvoX_Name, 0));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASERONDELAY", AxisParameter.GalvoY_Name, 0));
                        sw.WriteLine(string.Format("{0}", ";************************************"));


                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= PSO & GATE INITIALIZE =============="));
                        sw.WriteLine(string.Format("{0} {1} {2}", "PSOCONTROL", AxisParameter.GalvoX_Name, "RESET"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "PSOCONTROL", AxisParameter.GalvoX_Name, "OFF"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROUTPUTPERIOD", AxisParameter.GalvoX_Name, "0"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASER1PULSEWIDTH", AxisParameter.GalvoX_Name, "0"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASER2PULSEWIDTH", AxisParameter.GalvoX_Name, "0"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROVERRIDE", AxisParameter.GalvoX_Name, "OFF"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASERMODE", AxisParameter.GalvoX_Name, "1"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "$AO[0].", AxisParameter.GalvoX_Name, " = 0"));

                        // PSO 또는 GATE 설정
                        if (PosMode)
                        {
                            sw.WriteLine(string.Format("{0}", ";"));
                            sw.WriteLine(string.Format("{0}", ";************************************"));
                            sw.WriteLine(string.Format("{0}", ";============= CONFIGURATION PSO =============="));
                            sw.WriteLine(string.Format("{0} {1} {2}", "PSOCONTROL", AxisParameter.GalvoX_Name, "RESET"));
                            sw.WriteLine(string.Format("{0} {1} {2}", "PSOCONTROL", AxisParameter.GalvoX_Name, "OFF"));
                            sw.WriteLine(string.Format("{0} {1} {2}", "PSOOUTPUT", AxisParameter.GalvoX_Name, "CONTROL 0, 1"));
                            sw.WriteLine(string.Format("{0} {1} {2}", "PSOTRACK", AxisParameter.GalvoX_Name, "INPUT 0 1"));
                            double PsoDistance = ProcessParameter.Marking_GalvoVelocity / LaserFreq; // 
                            sw.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ",
                                          "PSODISTANCE",
                                          AxisParameter.GalvoX_Name,
                                          "FIXED",
                                          PsoDistance,
                                          "*",
                                          "CountsPerUnit.",
                                          AxisParameter.GalvoX_Name,
                                          "/",
                                          "EmulatedQuadratureDivider.",
                                          AxisParameter.GalvoX_Name));
                            double PsoTime = 1 / LaserFreq * 1000000.0;
                            //sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "PSOPULSE", AxisParameter.GalvoX_Name, "TIME", PsoTime, ", 1" ));
                            sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "PSOPULSE", AxisParameter.GalvoX_Name, "TIME", "1", ", 1"));

                            sw.WriteLine(string.Format("{0} {1} {2} ", "PSOOUTPUT", AxisParameter.GalvoX_Name, "PULSE LASER MASK"));
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROVERRIDE", AxisParameter.GalvoX_Name, "AUTO"));
                            sw.WriteLine(string.Format("{0} {1} {2} ", "PSOCONTROL", AxisParameter.GalvoX_Name, "ARM"));
                            sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));

                        }
                        else
                        {
                            sw.WriteLine(string.Format("{0}", ";"));
                            sw.WriteLine(string.Format("{0}", ";************************************"));
                            sw.WriteLine(string.Format("{0}", ";============= CONFIGURATION CFG=============="));
                            sw.WriteLine(string.Format("{0}", ";************************************"));
                            double PsoTime = 1 / LaserFreq * 1000000.0;
                            double Duty = (DutyCyclePercent / 100.0) * PsoTime;
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROUTPUTPERIOD", AxisParameter.GalvoX_Name, PsoTime));
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASER1PULSEWIDTH", AxisParameter.GalvoX_Name, Duty));
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASER2PULSEWIDTH", AxisParameter.GalvoX_Name, Duty));
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASERMODE", AxisParameter.GalvoX_Name, "1"));
                            sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROVERRIDE", AxisParameter.GalvoX_Name, "AUTO"));

                        }
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));

                        // Marking..
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= MARKING CPU =============="));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, -Distance, AxisParameter.GalvoY_Name, 1.0));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, -Distance, AxisParameter.GalvoY_Name, -1.0));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, -Distance, AxisParameter.GalvoY_Name, 0.0));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, Distance, AxisParameter.GalvoY_Name, 0.0));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, Distance, AxisParameter.GalvoY_Name, -1.0));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, Distance, AxisParameter.GalvoY_Name, 1.0));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, -1.0, AxisParameter.GalvoY_Name, -Distance));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, 1.0, AxisParameter.GalvoY_Name, -Distance));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, 0.0, AxisParameter.GalvoY_Name, -Distance));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, 0.0, AxisParameter.GalvoY_Name, Distance));

                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G0", AxisParameter.GalvoX_Name, -1.0, AxisParameter.GalvoY_Name, Distance));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4} ", "G90 G1", AxisParameter.GalvoX_Name, 1.0, AxisParameter.GalvoY_Name, Distance));


                        // End program
                        sw.WriteLine(string.Format("{0}", ";"));
                        sw.WriteLine(string.Format("{0}", ";************************************"));
                        sw.WriteLine(string.Format("{0}", ";============= END PROGRAM =============="));
                        sw.WriteLine(string.Format("{0} {1} {2} {3} {4}", "MOVEDELAY", AxisParameter.GalvoX_Name, AxisParameter.GalvoY_Name, ",", fDefaultDelay));
                        sw.WriteLine(string.Format("{0} {1} {2}", "PSOCONTROL", AxisParameter.GalvoX_Name, "OFF"));
                        sw.WriteLine(string.Format("{0} {1} {2}", "GALVO LASEROVERRIDE", AxisParameter.GalvoX_Name, "OFF"));

                        sw.WriteLine(string.Format("{0}", "END PROGRAM"));

                    }
            }
            catch (System.Exception ex)
            {
                if (sw != null)
                    sw.Close();

                return false;
            }

            return true;
        }
    }
}
