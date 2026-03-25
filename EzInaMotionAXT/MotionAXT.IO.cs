using EzIna.IO.AXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO.AXT
{
    
    public sealed class MotionAXT_DIO : DeviceModule
    {
        static bool m_bBoardInitialize = false;
        int m_nAxisTotalCount;                
        uint m_iLoopDIValueTemp;
        readonly int m_iMaxIOCount=5;
        readonly int m_iAllowIOChCount=2;

        Dictionary<int, BitField32Helper> m_DicInput = new Dictionary<int, BitField32Helper>();
        Dictionary<int, BitField32Helper> m_DicOutput = new Dictionary<int, BitField32Helper>();
        public bool IsDeviceInitialized
        {
            get
            {
               return m_bBoardInitialize;
            }
        }

        public Type ClassType
        {
            get
            {
                return this.GetType();
            }
        }

        public void InitializeDevice()
        {
#if SIM
#else
            if(m_bBoardInitialize==false)
            {                          
                if(EzIna.Motion.CMotionAXT.bDriverConnect==false)
                {
                    EzIna.Motion.CMotionAXT.InitializeConnectDriver();
                }                             
                CAXM.AxmInfoGetAxisCount(ref m_nAxisTotalCount);
                if(m_nAxisTotalCount>0)
                {
                    for(int i=0;i<m_nAxisTotalCount;i++)
                    {
                        m_DicInput.Add(i,new BitField32Helper());
                        m_DicInput.Add(i,new BitField32Helper());
                    }
                    m_bBoardInitialize=true;
                }
            }
#endif 
        }

        public void TerminateDevice()
        {
            if(m_bBoardInitialize)
            {             
                m_DicInput.Clear();
                m_DicOutput.Clear();
                m_bBoardInitialize=false;
                if(EzIna.Motion.CMotionAXT.bInitialized==true)
                {
                    EzIna.Motion.CMotionAXT.TerminateConnectedDriver();
                }
            }
        }
        public IOAddrInfo CreateAddressInfo(params string [] a_Params)
        {
            return new MotionDIOAddrInfoAXT(a_Params);
        }
     
        /// <summary>
        /// 0(Servo ON),1(Z Phase Signal) Bit Motion Use Signal 
        /// </summary>
        /// <param name="a_AddresInfo"></param>
        /// <param name="a_value"></param>
        /// <param name="a_ValueFrom"></param>
        public bool GetDI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.API)
        {
            MotionDIOAddrInfoAXT Addr=a_AddresInfo as MotionDIOAddrInfoAXT;
            bool bRet=false;
            uint iRet=0;
            if(m_DicOutput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition)&&                   
                   (int)Addr.iPosition < m_iMaxIOCount)
                {

                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                bRet=m_DicOutput[Addr.iModuleNumber][(bit)Addr.iPosition];
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                CAXM.AxmSignalReadInputBit(Addr.iModuleNumber,Addr.iPosition,ref iRet );
                                bRet=iRet==1;
                            }
                            break;
                    }                   
                }                
            }
            return bRet;
        }
        /// <summary>
        /// 0(Servo ON),1(Z Phase Signal) Bit Motion Use Signal [Block them]
        /// </summary>
        /// <param name="a_AddresInfo"></param>
        /// <param name="a_value"></param>
        /// <param name="a_ValueFrom"></param>
        public bool GetDO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.API)
        {
            MotionDIOAddrInfoAXT Addr=a_AddresInfo as MotionDIOAddrInfoAXT;
            bool bRet=false;
            uint iRet=0;
            if(m_DicOutput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition)&&                
                   (int)Addr.iPosition< m_iMaxIOCount)
                {
                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                bRet=m_DicOutput[Addr.iModuleNumber][(bit)Addr.iPosition];
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                CAXM.AxmSignalReadOutputBit(Addr.iModuleNumber,Addr.iPosition,ref iRet);
                                bRet=iRet==1;
                            }
                            break;
                    }                   
                }                
            }
            return bRet;
        }
        /// <summary>
        /// Buffer Mode Not Support 0(Servo ON),1(Z Phase Signal) Bit Motion Use Signal  [Block them]
        /// </summary>
        /// <param name="a_AddresInfo"></param>
        /// <param name="a_value"></param>
        /// <param name="a_ValueFrom"></param>
        public void SetDO(IOAddrInfo a_AddresInfo, bool a_value, enumValueFrom a_ValueFrom = enumValueFrom.API)
        {
            MotionDIOAddrInfoAXT Addr=a_AddresInfo as MotionDIOAddrInfoAXT;           
            if(m_DicOutput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition)&&
                   (int)Addr.iPosition >=m_iAllowIOChCount        &&
                   (int)Addr.iPosition< m_iMaxIOCount)
                {                  
                    CAXM.AxmSignalWriteOutputBit(Addr.iModuleNumber,Addr.iPosition,a_value==true?(uint)1:0);                                                                     
                }                
            }          
        }   
        /// <summary>
        /// Loop Execute 
        /// </summary>                    
        public void ReadAllDI()
        {
           foreach(KeyValuePair<int, BitField32Helper> Item in m_DicInput)
           {
                CAXM.AxmSignalReadInput(Item.Key,ref m_iLoopDIValueTemp);
                Item.Value.Data=m_iLoopDIValueTemp;
           }
        }
        /// <summary>
        /// Loop Execute [ must be Include ]
        /// </summary>
        public void ReadAllDO()
        {
           foreach(KeyValuePair<int, BitField32Helper> Item in m_DicOutput)
           {
                CAXM.AxmSignalReadOutput(Item.Key,ref m_iLoopDIValueTemp);
                Item.Value.Data=m_iLoopDIValueTemp;
           }
        }

#region     Not Available
        /// <summary>
        ///  Not Available
        /// </summary>
        public void ReadAllAI()
        {
            
        }
        /// <summary>
        ///  Not Available
        /// </summary>
        public void ReadAllAO()
        {
           
        }
        /// <summary>
        ///  Not Available
        /// </summary>
        public void WriteAllDO()
        {
            
        }
        /// <summary>
        ///  Not Available
        /// </summary>
        public void SetAO(IOAddrInfo a_AddresInfo, double a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            
        }
        /// <summary>
        ///  Not Available
        /// </summary>
        public double GetAI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.API)
        {
            return 0.0;
        }
        /// <summary>
        ///  Not Available
        /// </summary>
        public double GetAO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.API)
        {
            return 0.0;
        }

        public void SetAIRange(IOAddrInfo a_AddresInfo)
        {
            
        }

        public void SetAORange(IOAddrInfo a_AddresInfo)
        {
            
        }
#endregion  Not Available
    }

}
