using EzInaVision;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.FA
{
    /// <summary>
    /// Module : MOD
    /// </summary>
    public static class MGR
    {
        public static ProjectManager ProjectMgr = null;
        public static RecipeManager  RecipeMgr=null;
        public static VisionManager VisionMgr = null;
        public static Motion.MotionManager MotionMgr = null;
        public static GUI.CGuiManager GuiMgr = null;
        public static UserThread.ThreadManager ThreadMgr = null;
        public static RunningManager RunMgr = null;
        public static IO.IOManager IOMgr = null;
        public static Laser.LaserManager LaserMgr = null;
        public static Light.LightManager LightMgr = null;
        public static Attenuator.AttenuatorManager AttenuatorMgr = null;
        public static Scanner.ScanlabRTCMgr RTC5Mgr = null;
        public static PowerMeter.PowerMeterManager POMgr = null;
        public static ConcurrentDictionary<string, EzIna.FrmVisionOfPanel> dicFrmVisions = new ConcurrentDictionary<string, EzIna.FrmVisionOfPanel>();
		public static ConcurrentDictionary<string, EzIna.FrmVisionLIBOfPanel> dicFrmVisionLIBs = new ConcurrentDictionary<string, EzIna.FrmVisionLIBOfPanel>();
		public static Cam.Transfomations VisionToScannerCalb =null;
		public static RunningDataManager RecipeRunningData=null;
		public static DKT_MES_Manager  MESMgr=null;
		public static EzIna.DataMatrix.DMGenerater  DMGenertorMgr=null;

    }
}
