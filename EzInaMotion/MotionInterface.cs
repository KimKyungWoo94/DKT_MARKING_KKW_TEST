using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Motion
{
    public interface MotionInterface 
    {      
        void Initialize();
        void Terminate();
        void ReadMotorStatus();
        void ReadPositionStatus();

      
        // A
  
        void SetZeroPosition();

        bool ServoOn { set; }

        void SetAlarmReset();

        bool SD_STOP();// Slow Down Stop

        bool E_STOP(); // emergency Stop;

        bool JOG_STOP();
        bool Move_Jog(bool a_bDir, double a_dVelocity, double a_dAcc, double a_dDecel);

        bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType);

        bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType, double a_fSpeedPercent);
        bool Move_Relative(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel);
        bool Move_Relative(double a_dPos, GDMotion.eSpeedType a_eSpeedType);

        bool Move_Relative(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed);

        bool Move_Absolute(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel);
        bool Move_Absolute(double a_dPos, GDMotion.eSpeedType a_eSpeedType);
        bool Move_Absolute(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed);

        bool HomeStart();
        void HomeReadStatus();
        Type DeviceType {get; }
        
    }
    public class MotionItem:IDisposable
    {
        protected string  m_strAxisName;
        protected int     m_iAxisNo;
        protected bool    m_bHomeComplete;
        protected string  m_strHomeSeachingResult;
        protected uint    m_iUPreviousHomeState;
        protected bool    m_bIsMotionDone;
        protected bool    m_bJogging;
        protected bool    m_bServoOn;
        private bool      m_IsDisposed = false;
        private bool      m_IsDisposing=false;
        protected double  m_dTargetCmd;
        public GDMotion.stMotionUserMoveParamSetting    m_stUserMoveParamSetting;

        //Status
        public GDMotion.stMotionPositionStatus          m_stPositionStatus;
        public GDMotion.stMotionHomeSearchStatus        m_stMotionHomeStatus;
        public GDMotion.stMotionInfoStatus              m_stMotionInfoStatus;
        //speed
        public GDMotion.stMotionSpeedSetting            m_Slow;
        public GDMotion.stMotionSpeedSetting            m_Fast;
        public GDMotion.stMotionSpeedSetting            m_Run;
        public GDMotion.stMotionSpeedSetting            m_User;
        public bool IsMotionDone{  get { return m_bIsMotionDone;  } }
        public bool IsServoOn { get { return m_bServoOn; }   }
        public bool IsHomeComplete { get { return m_bHomeComplete; } }
        public string strAxisName { get { return m_strAxisName; } set { m_strAxisName=value; } }
        public int iAxisNo { get { return m_iAxisNo; } }
        public bool IsJogging { get { return m_bJogging; } }
				public bool IsInposition {get {return m_stMotionInfoStatus.m_bIsInPos;} }
        //ks2 : Cmd 와 Act 을 비교하기 위한 TargetCmd 추가함.
        public double dTargetCmd { get { return m_dTargetCmd; } }

        public MotionItem(int a_iAxis, string a_strAxisName)
        {
            m_strAxisName   = a_strAxisName;
            m_iAxisNo       = a_iAxis;
            Initialize();          
        }
        ~MotionItem()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool a_Disposing)
        {
            if (this.m_IsDisposed)
                return;
            if (a_Disposing)
            {
                m_IsDisposing=true;
                // Free any other managed objects here.
            }


            m_IsDisposing= false;
            m_IsDisposed = true;
        }
        public virtual void Initialize()
        {
           
            m_stUserMoveParamSetting.Clear();

            m_stMotionInfoStatus.Clear();
            m_stPositionStatus.Clear();
            m_stMotionHomeStatus.Clear();

            m_Slow.Clear();
            m_Fast.Clear();
            m_Run.Clear();
            m_User.Clear();
        }
        public virtual void Terminate()
        {

        }
        public virtual bool OpenMotionSpeedProfile()
        {
            IniFile Ini = new IniFile(GDMotion.ConfigMotionSpeedIniFileFullName);
            //Slow
            m_Slow.m_dMaxSpeed = Ini.Read(m_strAxisName, "SLOW SPEED MAX SPEED", 1.0);
            m_Slow.m_dAcceleration = Ini.Read(m_strAxisName, "SLOW SPEED ACCELERATION", 10.0);
            m_Slow.m_dDeceleration = Ini.Read(m_strAxisName, "SLOW SPEED DECELERATION", 10.0);
            m_Slow.m_dCurveAcceleration = Ini.Read(m_strAxisName, "SLOW SPEED CURVE ACCELERATION", 10.0);
            m_Slow.m_dCurveDeceleration = Ini.Read(m_strAxisName, "SLOW SPEED CURVE DECELERATION", 10.0);

            //Fast
            m_Fast.m_dMaxSpeed = Ini.Read(m_strAxisName, "FAST SPEED MAX SPEED", 1.0);
            m_Fast.m_dAcceleration = Ini.Read(m_strAxisName, "FAST SPEED ACCELERATION", 10.0);
            m_Fast.m_dDeceleration = Ini.Read(m_strAxisName, "FAST SPEED DECELERATION", 10.0);
            m_Fast.m_dCurveAcceleration = Ini.Read(m_strAxisName, "FAST SPEED CURVE ACCELERATION", 10.0);
            m_Fast.m_dCurveDeceleration = Ini.Read(m_strAxisName, "FAST SPEED CURVE DECELERATION", 10.0);

            //Run
            m_Run.m_dMaxSpeed = Ini.Read(m_strAxisName, "RUN SPEED MAX SPEED", 1.0);
            m_Run.m_dAcceleration = Ini.Read(m_strAxisName, "RUN SPEED ACCELERATION", 10.0);
            m_Run.m_dDeceleration = Ini.Read(m_strAxisName, "RUN SPEED DECELERATION", 10.0);
            m_Run.m_dCurveAcceleration = Ini.Read(m_strAxisName, "RUN SPEED CURVE ACCELERATION", 10.0);
            m_Run.m_dCurveDeceleration = Ini.Read(m_strAxisName, "RUN SPEED CURVE DECELERATION", 10.0);

            //User
            m_User.m_dMaxSpeed = Ini.Read(m_strAxisName, "USER SPEED MAX SPEED", 1.0);
            m_User.m_dAcceleration = Ini.Read(m_strAxisName, "USER SPEED ACCELERATION", 10.0);
            m_User.m_dDeceleration = Ini.Read(m_strAxisName, "USER SPEED DECELERATION", 10.0);
            m_User.m_dCurveAcceleration = Ini.Read(m_strAxisName, "USER SPEED CURVE ACCELERATION", 10.0);
            m_User.m_dCurveDeceleration = Ini.Read(m_strAxisName, "USER SPEED CURVE DECELERATION", 10.0);
            return true;
        }

        public virtual bool SaveMotionSpeedProfile()
        {
            IniFile Ini = new IniFile(GDMotion.ConfigMotionSpeedIniFileFullName);
            //Slow
            Ini.Write(strAxisName, "SLOW SPEED MAX SPEED", m_Slow.m_dMaxSpeed);
            Ini.Write(strAxisName, "SLOW SPEED ACCELERATION", m_Slow.m_dAcceleration);
            Ini.Write(strAxisName, "SLOW SPEED DECELERATION", m_Slow.m_dDeceleration);
            Ini.Write(strAxisName, "SLOW SPEED CURVE ACCELERATION", m_Slow.m_dCurveAcceleration);
            Ini.Write(strAxisName, "SLOW SPEED CURVE DECELERATION", m_Slow.m_dCurveDeceleration);

            //Fast
            Ini.Write(strAxisName, "FAST SPEED MAX SPEED", m_Fast.m_dMaxSpeed);
            Ini.Write(strAxisName, "FAST SPEED ACCELERATION", m_Fast.m_dAcceleration);
            Ini.Write(strAxisName, "FAST SPEED DECELERATION", m_Fast.m_dDeceleration);
            Ini.Write(strAxisName, "FAST SPEED CURVE ACCELERATION", m_Fast.m_dCurveAcceleration);
            Ini.Write(strAxisName, "FAST SPEED CURVE DECELERATION", m_Fast.m_dCurveDeceleration);

            //Run
            Ini.Write(strAxisName, "RUN SPEED MAX SPEED", m_Run.m_dMaxSpeed);
            Ini.Write(strAxisName, "RUN SPEED ACCELERATION", m_Run.m_dAcceleration);
            Ini.Write(strAxisName, "RUN SPEED DECELERATION", m_Run.m_dDeceleration);
            Ini.Write(strAxisName, "RUN SPEED CURVE ACCELERATION", m_Run.m_dCurveAcceleration);
            Ini.Write(strAxisName, "RUN SPEED CURVE DECELERATION", m_Run.m_dCurveDeceleration);

            //User
            Ini.Write(strAxisName, "USER SPEED MAX SPEED", m_User.m_dMaxSpeed);
            Ini.Write(strAxisName, "USER SPEED ACCELERATION", m_User.m_dAcceleration);
            Ini.Write(strAxisName, "USER SPEED DECELERATION", m_User.m_dDeceleration);
            Ini.Write(strAxisName, "USER SPEED CURVE ACCELERATION", m_User.m_dCurveAcceleration);
            Ini.Write(strAxisName, "USER SPEED CURVE DECELERATION", m_User.m_dCurveDeceleration);
            return true;
        }
        public virtual bool OpenConfigureation(bool a_bOption)
        {
            return true;
        }
        public virtual bool SaveConfiguration(bool a_bOption)
        {
            return true;
        }
    }

}
