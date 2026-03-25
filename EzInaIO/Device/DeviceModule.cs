using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO
{
    public enum enumValueFrom
    {
        GETTING_BUFFER,
        API,
    }
    public interface DeviceModule
    {               
       bool IsDeviceInitialized {get;} 
       Type ClassType {get;}      
       void InitializeDevice();
       void TerminateDevice();
       void ReadAllDI();
       void ReadAllDO();        
       void WriteAllDO();
       void ReadAllAI();
       void ReadAllAO();

      
       IOAddrInfo CreateAddressInfo(params string [] a_Params);

       bool GetDI(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
       bool GetDO(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
       void SetDO(IOAddrInfo a_AddresInfo,bool a_value,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
       double GetAI(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
       double GetAO(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
       void SetAO(IOAddrInfo a_AddresInfo, double a_value,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);                    

       void SetAIRange(IOAddrInfo a_AddresInfo);
       void SetAORange(IOAddrInfo a_AddresInfo);
       
    }
}
