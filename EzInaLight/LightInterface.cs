using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EzIna.Light
{
    public interface LightInterface
    {
       string strID {get; }
       Type   DeviceType {get; } 

       int  ChannelCount {get;}
       bool GetOnOff(int a_Ch);
       void SetOnOff(int a_Ch,bool a_Value);
       int  GetIntensity(int a_Ch);
       void SetIntensity(int a_Ch,int a_Value);      
       void DisposeLight();             
    }
}
