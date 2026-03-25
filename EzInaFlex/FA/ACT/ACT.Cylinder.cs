using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
		public static partial class ACT
		{
				public static MF.ActItem CYL_STOPPER_L_DOWN = new MF.ActItem(
				"[LASER]\nStopperLeft Down",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_L_UP].Action(false);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_L_UP].CurrentState == IO.CylinderState.BACKWARD;
				},
				3000,
				18,
				true);
				public static MF.ActItem CYL_STOPPER_L_UP = new MF.ActItem(
				"[LASER]\nStopperLeft Up",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_L_UP].Action(true);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_L_UP].CurrentState == IO.CylinderState.FORWARD;
				},
				3000,
				19,
				true);
				public static MF.ActItem CYL_STOPPER_CENTER_DOWN = new MF.ActItem(
				"[LASER]\nStopperCenter Down",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_M_UP].Action(false);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_M_UP].CurrentState == IO.CylinderState.BACKWARD;
				},
				3000,
				18,
				true);
				public static MF.ActItem CYL_STOPPER_CENTER_UP = new MF.ActItem(
				"[LASER]\nStopperCenter Up",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_M_UP].Action(true);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_M_UP].CurrentState == IO.CylinderState.FORWARD;
				},
				3000,
				19,
				true);
				public static MF.ActItem CYL_STOPPER_R_DOWN = new MF.ActItem(
				"[LASER]\nStopperCenter Down",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_R_UP].Action(false);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_R_UP].CurrentState == IO.CylinderState.BACKWARD;
				},
				3000,
				20,
				true);
				public static MF.ActItem CYL_STOPPER_R_UP = new MF.ActItem(
				"[LASER]\nStopperCenter Up",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_R_UP].Action(true);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_STOPPER_R_UP].CurrentState == IO.CylinderState.FORWARD;
				},
				3000,
				21,
				true);

				public static MF.ActItem CYL_JIG_FEEDER_L_UNCLAMP = new MF.ActItem(
				"[LASER]\nJIG Feeder Left Unclamp",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_L_CLAMPER].Action(false);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_L_CLAMPER].CurrentState == IO.CylinderState.BACKWARD;
				},
				3000,
				22,
				true);
				public static MF.ActItem CYL_JIG_FEEDER_L_CLAMP = new MF.ActItem(
				"[LASER]\nJIG Feeder Left Clamp",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_L_CLAMPER].Action(true);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_L_CLAMPER].CurrentState == IO.CylinderState.FORWARD;
				},
				3000,
				23,
				true);
				public static MF.ActItem CYL_JIG_FEEDER_R_UNCLAMP = new MF.ActItem(
				"[LASER]\nJIG Feeder Right Unclamp",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_R_CLAMPER].Action(false);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_R_CLAMPER].CurrentState == IO.CylinderState.BACKWARD;
				},
				3000,
				24,
				true);
				public static MF.ActItem CYL_JIG_FEEDER_R_CLAMP = new MF.ActItem(
				"[LASER]\nJIG Feeder Right Clamp",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_R_CLAMPER].Action(true);
				},
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_FEEDER_R_CLAMPER].CurrentState == IO.CylinderState.FORWARD;
				},
				3000,
				25,
				true);


			
				public static MF.ActItem CYL_JIG_ACC_UP = new MF.ActItem(
				"[LASER]\nJIG JIG Acc Up",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_ACC_UP].Action(true);
				},
				() =>
				{
						return (FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_ACC_UP].CurrentState == IO.CylinderState.FORWARD)&&
										DEF.eDI.JIG_ACC_LOWER_DOWN.GetDI().Value==false;
				},
				3000,
				26,
				true);
				public static MF.ActItem CYL_JIG_ACC_DOWN = new MF.ActItem(
				"[LASER]\nJIG JIG Acc Up",
				() =>
				{
						return FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_ACC_UP].Action(false);
				},
				() =>
				{
						return (FA.MGR.IOMgr.CylinderList[(int)DEF.eCylinder.JIG_ACC_UP].CurrentState == IO.CylinderState.BACKWARD) &&
										DEF.eDI.JIG_ACC_LOWER_DOWN.GetDI().Value == true;
				},
				3000,
				27,
				true);

		}
}
