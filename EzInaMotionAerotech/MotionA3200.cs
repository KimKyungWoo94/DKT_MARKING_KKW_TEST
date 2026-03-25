using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using Aerotech.A3200;
using Aerotech.A3200.Exceptions;
using Aerotech.A3200.Information;
using Aerotech.A3200.Status;
using Aerotech.A3200.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
namespace EzIna.Motion
{
    #region Non Static Func
    public sealed partial class CMotionA3200 : MotionA3200Item, MotionInterface
    {
        private bool IsDisposed = false;
        public CMotionA3200(int a_iAxis, string a_strAxisName) : base(a_iAxis, a_strAxisName)
        {

            Initialize();
        }

        ~CMotionA3200()
        {
            Dispose(false);
        }
        public override void Dispose(bool a_Disposing)
        {
            if (IsDisposed)
                return;
            if (a_Disposing)
            {

                // Free any other managed objects here.
            }
            IsDisposed = true;
        }
        public override void Initialize()
        {
            base.Initialize();

            OpenMotionSpeedProfile();
        }
        public override void Terminate()
        {
            base.Terminate();
            SaveMotionSpeedProfile();
        }
        public bool ServoOn
        {
            set
            {
                if (value)
                {
                    myController.Commands.Axes[m_iAxisNo].Motion.Enable();
                }
                else
                {
                    myController.Commands.Axes[m_iAxisNo].Motion.Disable();
                }
            }
        }

        public Type DeviceType
        {
            get
            {
                return this.GetType();
            }
        }
        /// <summary>
        /// deletegate One Func Getting ,Don't Use Func
        /// 
        /// </summary>
        public void HomeReadStatus()
        {
            // deletegate One Func Getting 
            // Don't Use Func

        }

        public bool HomeStart()
        {
            try
            {
                myController.Commands.Axes[m_iAxisNo].Motion.Advanced.HomeAsync();
                return true;
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public bool Move_Absolute(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed)
        {
            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                myController.Commands.Motion.Setup.RampMode((int)m_iAxisNo, Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel((int)m_iAxisNo, a_stSpeed.m_dAcceleration);
                myController.Commands.Motion.Setup.RampRateDecel((int)m_iAxisNo, a_stSpeed.m_dDeceleration);
                myController.Commands.Motion.MoveAbs((int)m_iAxisNo, a_dPos, a_stSpeed.m_dMaxSpeed);
                base.m_dTargetCmd = a_dPos;
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Move_Absolute(double a_dPos, GDMotion.eSpeedType a_eSpeedType)
        {
            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                GDMotion.stMotionSpeedSetting speed = new GDMotion.stMotionSpeedSetting();
                if (a_eSpeedType == GDMotion.eSpeedType.SLOW)
                    speed = m_listAxises[(int)m_iAxisNo].m_Slow;
                else if (a_eSpeedType == GDMotion.eSpeedType.FAST)
                    speed = m_listAxises[(int)m_iAxisNo].m_Fast;
                else if (a_eSpeedType == GDMotion.eSpeedType.RUN)
                    speed = m_listAxises[(int)m_iAxisNo].m_Run;
                else
                    speed = m_listAxises[(int)m_iAxisNo].m_Slow;

                myController.Commands.Motion.Setup.RampMode((int)m_iAxisNo, Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel((int)m_iAxisNo, speed.m_dAcceleration);
                myController.Commands.Motion.Setup.RampRateDecel((int)m_iAxisNo, speed.m_dDeceleration);
                myController.Commands.Motion.MoveAbs((int)m_iAxisNo, a_dPos, speed.m_dMaxSpeed);
                base.m_dTargetCmd = a_dPos;
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Move_Absolute(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
        {

            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                myController.Commands.Motion.Setup.RampMode((int)m_iAxisNo, Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel((int)m_iAxisNo, a_dAcc);
                myController.Commands.Motion.Setup.RampRateDecel((int)m_iAxisNo, a_dDecel);
                myController.Commands.Motion.MoveAbs((int)m_iAxisNo, a_dPos, a_dVelocity);
                base.m_dTargetCmd = a_dPos;
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType)
        {
            try
            {

                GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();
                switch (a_eSpeedType)
                {
                    case GDMotion.eSpeedType.FAST:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Fast;
                        break;
                    case GDMotion.eSpeedType.RUN:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Run;
                        break;
                    case GDMotion.eSpeedType.SLOW:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Slow;
                        break;
                    case GDMotion.eSpeedType.USER:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_User;
                        break;
                }
                //duRetCode = CAXM.AxmOverrideSetMaxVel(m_lAxisNo, m_Main.m_dMaxSpeed * a_fSpeedPercent);
                if (a_bDir)
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, m_stSpeed.m_dMaxSpeed);
                }
                else
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, m_stSpeed.m_dMaxSpeed * -1.0f);
                }

            }
            catch (A3200Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType, double a_fSpeedPercent)
        {
            try
            {
                if (a_fSpeedPercent > 1.0 || a_fSpeedPercent < 0.0)
                    return false;

                GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();

                switch (a_eSpeedType)
                {
                    case GDMotion.eSpeedType.FAST:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Fast;
                        break;
                    case GDMotion.eSpeedType.RUN:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Run;
                        break;
                    case GDMotion.eSpeedType.SLOW:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_Slow;
                        break;
                    case GDMotion.eSpeedType.USER:
                        m_stSpeed = m_listAxises[(int)m_iAxisNo].m_User;
                        break;
                }

                m_stSpeed.m_dMaxSpeed *= a_fSpeedPercent;
                m_stSpeed.m_dAcceleration *= a_fSpeedPercent;
                m_stSpeed.m_dDeceleration *= a_fSpeedPercent;

                //duRetCode = CAXM.AxmOverrideSetMaxVel(m_lAxisNo, m_Main.m_dMaxSpeed * a_fSpeedPercent);

                if (a_bDir)
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, m_stSpeed.m_dMaxSpeed);
                }
                else
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, m_stSpeed.m_dMaxSpeed * -1.0f);
                }

            }
            catch (A3200Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Move_Jog(bool a_bDir, double a_dVelocity, double a_dAcc, double a_dDecel)
        {
            try
            {
                //duRetCode = CAXM.AxmOverrideSetMaxVel(m_lAxisNo, m_Main.m_dMaxSpeed * a_fSpeedPercent);
                if (a_bDir)
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, a_dVelocity);
                }
                else
                {
                    myController.Commands.Motion.FreeRun((int)m_iAxisNo, a_dVelocity * -1.0f);
                }
            }
            catch (A3200Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Move_Relative(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed)
        {
            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                myController.Commands.Motion.Setup.RampMode(Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel(a_stSpeed.m_dAcceleration);
                myController.Commands.Motion.Setup.RampRateDecel(a_stSpeed.m_dDeceleration);
                myController.Commands.Motion.MoveInc((int)m_iAxisNo, a_dPos, a_stSpeed.m_dMaxSpeed);
                base.m_dTargetCmd = a_dPos;

            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Move_Relative(double a_dPos, GDMotion.eSpeedType a_eSpeedType)
        {
            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                GDMotion.stMotionSpeedSetting speed = new GDMotion.stMotionSpeedSetting();
                if (a_eSpeedType == GDMotion.eSpeedType.SLOW)
                    speed = m_listAxises[(int)m_iAxisNo].m_Slow;
                else if (a_eSpeedType == GDMotion.eSpeedType.FAST)
                    speed = m_listAxises[(int)m_iAxisNo].m_Fast;
                else if (a_eSpeedType == GDMotion.eSpeedType.RUN)
                    speed = m_listAxises[(int)m_iAxisNo].m_Run;
                else
                    speed = m_listAxises[(int)m_iAxisNo].m_User;

                myController.Commands.Motion.Setup.RampMode(Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel(speed.m_dAcceleration);
                myController.Commands.Motion.Setup.RampRateDecel(speed.m_dDeceleration);
                myController.Commands.Motion.MoveInc((int)m_iAxisNo, a_dPos, speed.m_dMaxSpeed);
                base.m_dTargetCmd = a_dPos;

            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Move_Relative(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
        {
            try
            {
                if (this.IsHomeComplete == false)
                    return false;
                if (this.IsMotionDone == false)
                    return false;
                myController.Commands.Motion.Setup.RampMode(Aerotech.A3200.Commands.RampMode.Rate);
                myController.Commands.Motion.Setup.RampRateAccel(a_dAcc);
                myController.Commands.Motion.Setup.RampRateDecel(a_dDecel);
                myController.Commands.Motion.MoveInc((int)m_iAxisNo, a_dPos, a_dVelocity);
                base.m_dTargetCmd = a_dPos;
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public override bool OpenConfigureation(bool a_bOption)
        {
            return base.OpenConfigureation(a_bOption);
        }
        public override bool SaveConfiguration(bool a_bOption)
        {
            return base.SaveConfiguration(a_bOption);
        }

        // If you need , you will redefine this Func
        /*
        public override bool OpenMotionSpeedProfile()
        {
        }
        public override bool SaveMotionSpeedProfile()
        {         
            return true;
        }
        */
        public void ReadMotorStatus()
        {
            // deletegate One Func Getting 
            // Don't Use Func
        }
        public void ReadPositionStatus()
        {
            // deletegate One Func Getting 
            // Don't Use Func
        }


        public bool SD_STOP()
        {
            try
            {
                myController.Commands.Axes[(int)m_iAxisNo].Motion.Abort();
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool E_STOP()
        {
            try
            {
                myController.Commands.Axes[(int)m_iAxisNo].Motion.Abort();
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool JOG_STOP()
        {
            try
            {
                myController.Commands.Motion.FreeRunStop((int)m_iAxisNo);
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void SetAlarmReset()
        {
            AcknowlegeAll();
        }

        public void SetZeroPosition()
        {

        }

    }
    #endregion  Non Static Func
}
