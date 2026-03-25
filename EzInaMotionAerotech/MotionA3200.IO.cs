using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.IO;

namespace EzIna.IO.AEROTECH
{
    
    public sealed partial class MotionA3200_IO : EzIna.IO.DeviceModule
    {

        public Type ClassType
        {
            get
            {
                return this.GetType();
            }
        }
        public bool IsDeviceInitialized
        {
            get
            {
                return EzIna.Motion.CMotionA3200.bDriverConnect;
            }
        }
        public void InitializeDevice()
        {
            if (IsDeviceInitialized)
            {
                EzIna.Motion.CMotionA3200.InitializeConnectDevice();
            }
        }
        public void TerminateDevice()
        {
            if (EzIna.Motion.CMotionA3200.bInitialized == false && IsDeviceInitialized)
            {
                EzIna.Motion.CMotionA3200.TerminateConnectedDriver();
            }
        }

        public IOAddrInfo CreateAddressInfo(params string [] a_Params)
        {
            return new MotionA3200_AIOAddrInfo(a_Params);
        }
        public double GetAI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            if (IsDeviceInitialized)
            {
                MotionA3200_AIOAddrInfo Addr = a_AddresInfo as MotionA3200_AIOAddrInfo;
                return EzIna.Motion.CMotionA3200.GetAI(Addr.iAxisIndex, Addr.iChannelNum);
            }
            return 0.0;
        }

        public double GetAO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            if (IsDeviceInitialized)
            {
                MotionA3200_AIOAddrInfo Addr = a_AddresInfo as MotionA3200_AIOAddrInfo;
                return EzIna.Motion.CMotionA3200.GetAO(Addr.iAxisIndex, Addr.iChannelNum);
            }
            return 0.0;
        }

        public bool GetDI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            if (IsDeviceInitialized)
            {
                MotionA3200_DIOAddrInfo Addr = a_AddresInfo as MotionA3200_DIOAddrInfo;
                return EzIna.Motion.CMotionA3200.GetDI(Addr.iAxisIndex, Addr.iPosition);
            }
            return false;
        }

        public bool GetDO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            if (IsDeviceInitialized)
            {
                MotionA3200_DIOAddrInfo Addr = a_AddresInfo as MotionA3200_DIOAddrInfo;
                return EzIna.Motion.CMotionA3200.GetDO(Addr.iAxisIndex, Addr.iPosition);
            }
            return false;
        }
        public void SetAO(IOAddrInfo a_AddresInfo, double a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            if (IsDeviceInitialized)
            {
                MotionA3200_AIOAddrInfo Addr = a_AddresInfo as MotionA3200_AIOAddrInfo;
                EzIna.Motion.CMotionA3200.SetAO(Addr.iAxisIndex, Addr.iChannelNum,a_value);
            }

        }

        public void SetDO(IOAddrInfo a_AddresInfo, bool a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
             if (IsDeviceInitialized)
            {
                MotionA3200_DIOAddrInfo Addr = a_AddresInfo as MotionA3200_DIOAddrInfo;
                EzIna.Motion.CMotionA3200.SetDO(Addr.iAxisIndex,Addr.iAxisIndex,a_value==true ? 1:0);
            }
        }
        #region     Not Support
        public void ReadAllAI()
        {

        }

        public void ReadAllAO()
        {

        }

        public void ReadAllDI()
        {

        }

        public void ReadAllDO()
        {

        }
        public void WriteAllDO()
        {

        }

        public void SetAIRange(IOAddrInfo a_AddresInfo)
        {
            throw new NotImplementedException();
        }

        public void SetAORange(IOAddrInfo a_AddresInfo)
        {
            throw new NotImplementedException();
        }
        #endregion  Not Support
    }
}
