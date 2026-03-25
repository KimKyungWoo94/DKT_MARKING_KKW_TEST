using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzIna.FA.DEF;

namespace EzIna.FA
{

    public static partial class ACT
    {
        public static List<MF.ActItem> m_ActItems = new List<MF.ActItem>();

        public static void Init()
        {

        }
        /*
        #region [Pneumatic]
        public static MF.ActItem StageVauccmOn = new MF.ActItem(
        "[Stage vacuum]\nstage vacuum on",
        () =>
        {
            eDO.WAFER_VAC_ON.GetDO().Value = true;
            eDO.WAFER_BLOW_ON.GetDO().Value = false;
            eDO.SW_STAGE_VACCUM_LAMP.GetDO().Value = true;
            return true;
        },
        () =>
        {
            if (!eDO.WAFER_VAC_ON.GetDO().Value) return false;
            if (eDO.WAFER_BLOW_ON.GetDO().Value) return false;
            return true;

        },
        3000,
        10,
        false);

        public static MF.ActItem StageBlowOn = new MF.ActItem(
        "[Stage vacuum]\nstage vacuum on",
        () =>
        {
            eDO.WAFER_VAC_ON.GetDO().Value = false;
            eDO.WAFER_BLOW_ON.GetDO().Value = true;
            eDO.SW_STAGE_VACCUM_LAMP.GetDO().Value = false;
            return true;
        },
        () =>
        {
            if (eDO.WAFER_VAC_ON.GetDO().Value) return false;
            if (!eDO.WAFER_BLOW_ON.GetDO().Value) return false;

            return true;

        },
        3000,
        10,
        false);

        public static MF.ActItem StageVauccmOff = new MF.ActItem(
        "[Stage vacuum]\nstage vacuum off",
        () =>
        {
            eDO.WAFER_VAC_ON.GetDO().Value = false;
            eDO.WAFER_BLOW_ON.GetDO().Value = false;
            eDO.SW_STAGE_VACCUM_LAMP.GetDO().Value = false;
            return true;
        },
        () =>
        {
            if (eDO.WAFER_VAC_ON.GetDO().Value) return false;
            if (eDO.WAFER_BLOW_ON.GetDO().Value) return false;

            return true;
        },
        3000,
        10,
        false);

        public static MF.ActItem SlideChuckClampingON = new MF.ActItem(
        "[Stage vacuum]\nSlide chuck clamping on",
        () =>
        {
            eDO.SLIDE_CHUCK_CLAMPING_ON.GetDO().Value = true;
            return true;
        },
        () =>
        {
            if (!eDO.SLIDE_CHUCK_CLAMPING_ON.GetDO().Value) return false;
            return true;
        },
        3000,
        10,
        true);

        public static MF.ActItem SlideChuckClampingOFF = new MF.ActItem(
        "[Stage vacuum]\nSlide chuck clamping off",
        () =>
        {
            eDO.SLIDE_CHUCK_CLAMPING_ON.GetDO().Value = false;
            return true;
        },
        () =>
        {
            if (eDO.SLIDE_CHUCK_CLAMPING_ON.GetDO().Value) return false;
            return true;
        },
        3000,
        10,
        true);


        #endregion
        */
        #region [Cylinders]
        public static MF.ActItem CYL_LaserShutter_Open = new MF.ActItem(
          "[LASER]\nLaser Shutter Open",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.LaserShutterOpen].Action(true);
          },
          () =>
          {
              return false; //FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.LaserShutterOpen].CurrentState == IO.CylinderState.FORWARD;
          },
          3000,
          10,
          true);

        public static MF.ActItem CYL_LaserShutter_Close = new MF.ActItem(
          "[LASER]\nLaser Shutter Close",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.LaserShutterOpen].Action(false);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.LaserShutterOpen].CurrentState == IO.CylinderState.BACKWARD;
          },
          3000,
          11,
          true);

        public static MF.ActItem CYL_PowerMeter_Open = new MF.ActItem(
          "[LASER]\nPowerMeter Open",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.PowerMeterShutterOpen].Action(true);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.PowerMeterShutterOpen].CurrentState == IO.CylinderState.FORWARD;
          },
          3000,
          12,
          true);

        public static MF.ActItem CYL_PowerMeter_Close = new MF.ActItem(
          "[LASER]\nPowerMeter Close",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.PowerMeterShutterOpen].Action(false);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.PowerMeterShutterOpen].CurrentState == IO.CylinderState.BACKWARD;
          },
          3000,
          13,
          true);


        public static MF.ActItem CYL_AutoDoorLeft_Open = new MF.ActItem(
          "[LASER]\nAutoDoorLeft Open",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorLeftOpen].Action(true);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorLeftOpen].CurrentState == IO.CylinderState.FORWARD;
          },
          3000,
          14,
          true);

        public static MF.ActItem CYL_AutoDoorLeft_Close = new MF.ActItem(
          "[LASER]\nAutoDoorLeft Close",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorLeftOpen].Action(false);
          },
          () =>
          {
              return false; //FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorLeftOpen].CurrentState == IO.CylinderState.BACKWARD;
          },
          3000,
          15,
          true);


        public static MF.ActItem CYL_AutoDoorRight_Open = new MF.ActItem(
          "[LASER]\nAutoDoorRight Open",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorRightOpen].Action(true);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorRightOpen].CurrentState == IO.CylinderState.FORWARD;
          },
          3000,
          16,
          true);

          public static MF.ActItem CYL_AutoDoorRight_Close = new MF.ActItem(
          "[LASER]\nAutoDoorRight Close",
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorRightOpen].Action(false);
          },
          () =>
          {
              return false;//FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.AutoDoorRightOpen].CurrentState == IO.CylinderState.BACKWARD;
          },
          3000,
          17,
          true);			
				#endregion [ Cylinders ]

				#region [ Motion ] 

				public static bool MoveABS(MF.RcpItem item)
        {
            try
            {
                if (FA.MGR.MotionMgr.GetItem(item.m_iAxis).Move_Absolute(item.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return true;
                return false;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }
        public static bool MoveABSWithOffset(MF.RcpItem item, double Offset)
        {
            try
            {
                if (FA.MGR.MotionMgr.GetItem(item.m_iAxis).Move_Absolute(item.AsDouble + Offset, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return true;
                return false;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }
        public static bool MoveABS(MF.RcpItem item1, MF.RcpItem item2)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem(item1.m_iAxis).Move_Absolute(item1.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return false;

                if (!FA.MGR.MotionMgr.GetItem(item2.m_iAxis).Move_Absolute(item2.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return false;

                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool MoveABS(MF.RcpItem item1, MF.RcpItem item2, MF.RcpItem item3)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem(item1.m_iAxis).Move_Absolute(item1.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return false;

                if (!FA.MGR.MotionMgr.GetItem(item2.m_iAxis).Move_Absolute(item2.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return false;

                if (!FA.MGR.MotionMgr.GetItem(item3.m_iAxis).Move_Absolute(item3.AsDouble, EzIna.Motion.GDMotion.eSpeedType.RUN))
                    return false;

                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool MoveABS(DEF.eAxesName a_eAxis, double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem((int)a_eAxis).Move_Absolute(a_dPos, a_dVelocity, a_dAcc, a_dDecel))
                    return false;


                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool MoveABS(DEF.eAxesName a_eAxis, double a_dPos, EzIna.Motion.GDMotion.eSpeedType a_eSpeedType)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem((int)a_eAxis).Move_Absolute(a_dPos, a_eSpeedType))
                    return false;

                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool MoveRel(DEF.eAxesName a_eAxis, double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem((int)a_eAxis).Move_Relative(a_dPos, a_dVelocity, a_dAcc, a_dDecel))
                    return false;


                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool MoveRel(DEF.eAxesName a_eAxis, double a_dPos, EzIna.Motion.GDMotion.eSpeedType a_eSpeedType)
        {
            try
            {
                if (!FA.MGR.MotionMgr.GetItem((int)a_eAxis).Move_Relative(a_dPos, a_eSpeedType))
                    return false;


                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        public static bool InPostion(MF.RcpItem item)
        {
            try
            {
                EzIna.Motion.MotionItem Axis = (FA.MGR.MotionMgr.GetItem(item.m_iAxis) as EzIna.Motion.MotionItem);

                if (!Axis.IsMotionDone)
                    return false;

                if (!Axis.m_stPositionStatus.fActPos.IsSame(item.AsDouble, FA.DEF.IN_POS))
                    return false;

                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }
        public static bool InPostionWithOffset(MF.RcpItem item, double Offset)
        {
            try
            {
                EzIna.Motion.MotionItem Axis = FA.MGR.MotionMgr.GetItem(item.m_iAxis) as EzIna.Motion.MotionItem;
                if (!Axis.IsMotionDone)
                    return false;
                if (!Axis.m_stPositionStatus.fActPos.IsSame(item.AsDouble + Offset, FA.DEF.IN_POS))
                    return false;
                return true;
            }
            catch (Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                return false;
            }
        }

        #endregion [ Motion ]

    }
}
