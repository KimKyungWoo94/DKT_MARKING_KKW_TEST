using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzIna.FA.DEF;
namespace EzIna
{
	   public enum eSeqCalibMove
		{
		    Finish
			  ,Basic_Settings_start
		}
		public partial class RunningStage
		{
				public bool SubSeq_CalibMove()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eSeqCalibMove>.From(m_stRun.nSubStep))
						{
								case eSeqCalibMove.Finish:
										FA.LOG.Debug("SEQ", "Calib Move finish");
										return true;

								case eSeqCalibMove.Basic_Settings_start:
										if (base.IsRunModeStopped()) break;
										FA.LOG.Debug("SEQ", "Calib Move start");
										base.NextSubStep();
										break;
						}
						SubSeqCheckTimeout(FA.DEF.Timeout_Run,FA.DEF.Error_Run_AutoFocus + m_stRun.nSubStep);
						return false;
				}
		}
}
