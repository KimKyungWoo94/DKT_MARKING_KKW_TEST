using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO.ScanLAB
{
    public sealed class ScanlabRTC5_DIO : DeviceModule
    {
        static bool m_bBoardInitialize = false;
        Dictionary<int, BitField32Helper> m_DicInput = new Dictionary<int, BitField32Helper>();
        Dictionary<int, BitField32Helper> m_DicInputLaserPin = new Dictionary<int, BitField32Helper>();
        Dictionary<int, BitField32Helper> m_DicOutput = new Dictionary<int, BitField32Helper>();
        Dictionary<int, BitField32Helper> m_DicOutputLaserPin = new Dictionary<int, BitField32Helper>();
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
               return m_bBoardInitialize;
            }
        }
        public void InitializeDevice()
        {
            if (m_bBoardInitialize == false)
            {
                if (Scanner.ScanlabRTC5.bDllInit == false)
                {
                    Scanner.ScanlabRTC5.InitializeDriver();
                }
                uint MaxCard = RTC5Import.RTC5Wrap.rtc5_count_cards();
                for (uint i = 0; i < MaxCard; i++)
                {
                    m_DicInput.Add((int)i,new BitField32Helper());
                    m_DicInputLaserPin.Add((int)i,new BitField32Helper());                    
                    m_DicOutput.Add((int)i, new BitField32Helper());
                    m_DicOutputLaserPin.Add((int)i,new BitField32Helper());
                }
                m_bBoardInitialize = true;
            }
        }
        public void TerminateDevice()
        {            
            if(Scanner.ScanlabRTC5.bDllInit==true)
            {
               Scanner.ScanlabRTC5.TerminateDriver();
            }           
        }


        public IOAddrInfo CreateAddressInfo(params string[] a_Params)
        {
            return new ScanlabRTC5DIOAddrInfo(a_Params);
        }

      
        /// <summary>
        /// API Not Support
        /// </summary>
        /// <param name="a_AddresInfo"></param>
        /// <param name="a_ValueFrom"></param>
        /// <returns></returns>
        public bool GetDI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
             ScanlabRTC5DIOAddrInfo Addr=a_AddresInfo as ScanlabRTC5DIOAddrInfo;
            bool bRet=false;
            uint iRet=0;
            if(m_DicOutput.ContainsKey(Addr.iCardNo))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {
                    if(Addr.bLaserConnector==false)
                    {
                         bRet=m_DicInput[Addr.iCardNo][(bit)Addr.iPosition];                        
                    }
                    
                }                
            }
            return bRet;
        }

        /// <summary>
        /// API not Support
        /// </summary>
        /// <param name="a_AddresInfo"></param>
        /// <param name="a_ValueFrom"></param>
        /// <returns></returns>
        public bool GetDO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            ScanlabRTC5DIOAddrInfo Addr=a_AddresInfo as ScanlabRTC5DIOAddrInfo;
            bool bRet=false;
            uint iRet=0;
            if(m_DicOutput.ContainsKey(Addr.iCardNo))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {
                     bRet=m_DicOutput[Addr.iCardNo][(bit)Addr.iPosition];                        
                }                
            }
            return bRet;
        }    
        /// <summary>
        /// NotSupport
        /// </summary>      
        public void ReadAllDI()
        {
           foreach(KeyValuePair<int,BitField32Helper> Item in m_DicInput)
           {
                Item.Value.Data=RTC5Import.RTC5Wrap.n_read_io_port((uint)Item.Key+1);
           }
        }          
        public void ReadAllDO()
        {
            foreach(KeyValuePair<int,BitField32Helper> Item in m_DicOutput)
            {
                Item.Value.Data=RTC5Import.RTC5Wrap.n_get_io_status((uint)Item.Key+1);
            }
        }
        public void SetDO(IOAddrInfo a_AddresInfo, bool a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            ScanlabRTC5DIOAddrInfo Addr=a_AddresInfo as ScanlabRTC5DIOAddrInfo;             
            if(m_DicOutput.ContainsKey(Addr.iCardNo))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {
                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                m_DicOutput[Addr.iCardNo][(bit)Addr.iPosition]=a_value;
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                m_DicOutput[Addr.iCardNo][(bit)Addr.iPosition]=a_value;
                                RTC5Import.RTC5Wrap.n_write_io_port_mask((uint)Addr.iCardNo+1,m_DicOutput[Addr.iCardNo].Data,
                                     (uint)1<<Addr.iPosition);
                            }
                            break;
                    }
                  
                }                
            }             
        }     
        public void WriteAllDO()
        {
             foreach(KeyValuePair<int,BitField32Helper> Item in m_DicOutput)
            {
                RTC5Import.RTC5Wrap.n_write_io_port((uint)Item.Key+1,Item.Value.Data);
            }
        }
        #region Not Support

        public double GetAI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            return 0.0;
        }
        public double GetAO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
          return 0.0;
        }        
        public void SetAO(IOAddrInfo a_AddresInfo, double a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
           
        }
           /// <summary>
        /// NotSupport
        /// </summary>      
        public void ReadAllAI()
        {
            
        }
        /// <summary>
        /// NotSupport
        /// </summary>
        public void ReadAllAO()
        {
            
        }

        public void SetAIRange(IOAddrInfo a_AddresInfo)
        {
            
        }

        public void SetAORange(IOAddrInfo a_AddresInfo)
        {
            
        }
        #endregion Not Support
    }
}
