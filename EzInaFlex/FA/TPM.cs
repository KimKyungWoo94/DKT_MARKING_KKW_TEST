using System;
using System.Diagnostics;

namespace EzIna.FA
{
    public enum TPMStatus { Stop, Run, Error, Initial };

    public enum ProductResult { Good, Reject }

    public enum RunningMode { Auto, Manual, Setup, Cycle, Initial }

    /// <summary>
    /// static class of Total Product Management
    /// </summary>
    public static class TPM
    {
        public static RunningMode Mode = RunningMode.Auto;

        private static TPMStatus _Status = TPMStatus.Stop;

        private static long _ErrorCount = 0;

        private static Stopwatch _CycleTimer   = new Stopwatch();
        private static Stopwatch _RunningTimer = new Stopwatch();
        private static Stopwatch _StopTimer    = new Stopwatch();
        private static Stopwatch _ErrorTimer   = new Stopwatch();
        private static Stopwatch _UPHTimer     = new Stopwatch();


        private static TimeSpan _OldRunningTime  = TimeSpan.Zero;
        private static TimeSpan _OldStopTime     = TimeSpan.Zero;
        private static TimeSpan _OldErrorTime    = TimeSpan.Zero;
        private static TimeSpan _OldCycleCyleTime= TimeSpan.Zero;


        private static long _GoodCount      = 0;
        private static long _RejectCount    = 0;
        private static long _OldGoodCount   = 0;
        private static long _OldRejectCount = 0;

        private static long _UPHCount = 0;

        public static long ErrorCount { get { return _ErrorCount; } }

        public static int  ErrorCode = 0;

        public static TPMStatus Status
        {
            get { return _Status; }
            set
            {
                if (value != _Status) LOG.Info("STATUS", value.ToString());

                if (_Status == TPMStatus.Run && value == TPMStatus.Error)
                    _ErrorCount++;
                _Status = value;
                UpdateTimer();
            }
        }

        public static TimeSpan CycleTime     { get { return _OldCycleCyleTime; } }
        public static TimeSpan RunningTime   { get { return _RunningTimer.Elapsed + _OldRunningTime; } }
        public static TimeSpan StopTime      { get { return _StopTimer.Elapsed + _OldStopTime; } }
        public static TimeSpan ErrorTime     { get { return _ErrorTimer.Elapsed + _OldErrorTime; } }
        public static TimeSpan TotalTime     { get { return RunningTime + StopTime + ErrorTime; } }

        public static long GoodCount         { get { return _GoodCount    ; } }
        public static long RejectCount       { get { return _RejectCount  ; } }
        public static long ProductCount      { get { return _GoodCount + _RejectCount; } }

        public static long TotalGoodCount    { get { return _OldGoodCount   + _GoodCount; } }
        public static long TotalRejectCount  { get { return _OldRejectCount + _RejectCount; } }
        public static long TotalProductCount { get { return TotalGoodCount + TotalRejectCount; } }

        public static bool AlarmExists       { get { return (ErrorCode != 0) || (Status == TPMStatus.Error); } }

        public static void UpdateTimer()
        {
            switch (_Status)
            {
                case TPMStatus.Error:
                    _RunningTimer.Stop();
                    _StopTimer.Stop();
                    _ErrorTimer.Start();
                    break;

                case TPMStatus.Stop:
                    _RunningTimer.Stop();
                    _StopTimer.Start();
                    _ErrorTimer.Stop();
                    break;

                case TPMStatus.Run:
                    _RunningTimer.Start();
                    _StopTimer.Stop();
                    _ErrorTimer.Stop();
                    break;

                case TPMStatus.Initial:
                    _RunningTimer.Stop();
                    _StopTimer.Stop();
                    _ErrorTimer.Stop();
                    break;
            }
        }

        public static void Init(string iniFile)
        {
            IniFile ini = new IniFile(iniFile);

            _OldRunningTime = ini.Read("TPM", "RunningTime", _OldRunningTime);
            _OldStopTime    = ini.Read("TPM", "StopTime", _OldStopTime);
            _OldErrorTime   = ini.Read("TPM", "ErrorTime", _OldErrorTime);
            _OldGoodCount   = ini.Read("TPM", "GoodCount", _OldGoodCount);
            _OldRejectCount = ini.Read("TPM", "RejectCount", _OldRejectCount);
            _ErrorCount     = ini.Read("TPM", "ErrorCount", _ErrorCount);
            _GoodCount   = 0;
            _RejectCount = 0;
        }
        public static void Final(string iniFile)
        {
            _OldRunningTime = RunningTime;
            _OldStopTime = StopTime;
            _OldErrorTime = ErrorTime;

            if (_RunningTimer.IsRunning) _RunningTimer.Restart(); else _RunningTimer.Reset();
            if (_StopTimer.IsRunning) _StopTimer.Restart(); else _StopTimer.Reset();
            if (_ErrorTimer.IsRunning) _ErrorTimer.Restart(); else _ErrorTimer.Reset();

            _OldGoodCount = GoodCount;
            _OldRejectCount = RejectCount;

            _GoodCount = 0;
            _RejectCount = 0;

            SaveSettings(iniFile);
        }

        public static void SaveSettings(string iniFile)
        {
            IniFile ini = new IniFile(iniFile);

            ini.Write("TPM", "RunningTime", _OldRunningTime.Ticks);
            ini.Write("TPM", "StopTime"   , _OldStopTime.Ticks);
            ini.Write("TPM", "ErrorTime"  , _OldErrorTime.Ticks);
            ini.Write("TPM", "GoodCount"  , _OldGoodCount);
            ini.Write("TPM", "RejectCount", _OldRejectCount);
            ini.Write("TPM", "ErrorCount" , _ErrorCount);
        }

        public static TimeSpan GetMTBF()
        {
            long den = _ErrorCount + 1;

            return TimeSpan.FromTicks(RunningTime.Ticks / den.EnsureRange(1, long.MaxValue));
        }
        public static double GetUPH()
        {
            if (_UPHTimer.Elapsed.Equals(TimeSpan.Zero))
                return 0;
            else
                return _UPHCount / _UPHTimer.Elapsed.TotalHours;
        }

        public static void ClearTotalGoodCount  () { _GoodCount   = 0; _OldGoodCount   = 0; }
        public static void ClearTotalRejectCount() { _RejectCount = 0; _OldRejectCount = 0; }
        public static void ClearAllProductCount () { ClearTotalGoodCount(); ClearTotalRejectCount(); }

        public static void ClearTimes()
        {
            _OldRunningTime = TimeSpan.Zero;
            _OldStopTime    = TimeSpan.Zero;
            _OldErrorTime   = TimeSpan.Zero;

            _RunningTimer.Reset();
            _StopTimer .Reset();
            _ErrorTimer.Reset();
            _CycleTimer.Reset();
            UpdateTimer();
        }


        public static void ClearErrorCount() { _ErrorCount = 0; }

        public static void AddProduct(ProductResult vr)
        {
            if (Status == TPMStatus.Run && !_UPHTimer.IsRunning) _UPHTimer.Start(); // _UPHTimer 자동 실행
             _UPHCount++;
            switch (vr)
            {
                case ProductResult.Good:
                    _GoodCount++;
                    break;
                case ProductResult.Reject:
                    _RejectCount++;
                    break;
            }
        }

        public static void StartUPH() { if (!_UPHTimer.IsRunning) _UPHTimer.Start(); }
        public static void StopUPH () { _UPHTimer.Stop(); }
        public static void ResetUPH() { _UPHTimer.Reset(); _UPHCount = 0; }

        public static void StartCycleTime()
        {
            _CycleTimer.Start();
        }
        public static void StopCycleTime()
        {
            _OldCycleCyleTime= _CycleTimer.Elapsed; 
            _CycleTimer.Stop();
        }
        public static void ResetCycleTime()
        {
            _CycleTimer.Reset();
        }
    }
}