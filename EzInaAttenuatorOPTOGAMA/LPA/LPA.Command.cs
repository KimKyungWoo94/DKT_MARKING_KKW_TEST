using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
namespace EzIna.Attenuator.OPTOGAMA
{
   
    public enum LPA_CMD
    {
        /// <summary>  
        /// <para>Get : LPA>PWR? → LPA>PWR_X.XXX</para>  
        /// <para>Set : LPA>PWR! → LPA>PWR_X.XXX</para> 
        /// </summary>
        [PacketAttribute("LPA>PWR", "!", "?", true, true, true, true, enumPacketMode.Both)]
        POWER_PERCENT,
        /// <summary>  
        /// <para>Get : LPA>ANG? → LPA>ANG_X.XXX</para>  
        /// <para>Set : LPA>ANG! → LPA>ANG_X.XXX</para>          
        /// </summary>
        [PacketAttribute("LPA>ANG", "!", "?", true, true, true, true, enumPacketMode.Both)]
        ANGLE,
        /// <summary>  
        /// <para>Get : LPA>TGT? → LPA>TGT_X.XXX</para>  
        /// <para>Set : LPA>TGT! → LPA>TGT_X.XXX</para>          
        /// </summary>
        [PacketAttribute("LPA>TGT", "!", "?", true, true, true, true, enumPacketMode.Both)]
        POSITION,
       
        /// <summary>  
        /// <para>Get : LPA>DEF? → LPA>DEF_X</para>  
        /// <para>Set : LPA>DEF! → LPA>DEF_X</para>          
        /// </summary>
        [PacketAttribute("LPA>DEF", "!", "?", true, true, true, true, enumPacketMode.Both)]
        ZERO_SET,

        /// <summary>          
        /// <para>Set : LPA>STP! → LPA>STP</para>          
        /// </summary>
        [PacketAttribute("LPA>STP", "!", "?", true, false, true, true, enumPacketMode.SetOnly)]
        MOTOR_STOP,
       
        /// <summary>  
        /// Command used to set procedure after each homing: 
        /// <para>0 - device remains in home position (TGT_0)</para> 
        /// <para>1 - device goes to MIN power position (calibrated position for minimum power)</para> 
        /// <para>2 - device goes to LAST position it was before homing. </para>  
        /// <para>Get : LPA>AUTOGO? → LPA>AHOME_X</para>  
        /// <para>Set : LPA>AUTOGO!_X → LPA>AHOME_X</para>          
        /// </summary>
        [PacketAttribute("LPA>AUTOGO", "!", "?", false, false, true, true, enumPacketMode.Both)]
        HOME_SEARCH_OPTION,
        /// <summary>          
        /// <para>Set : LPA>HOME! → LPA>HOME</para>          
        /// </summary>
        [PacketAttribute("LPA>HOME", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
        HOME_COMMAND,

        /// <summary>      
        /// <para>Get : LPA>AHOME? → LPA>AHOME_1 OR 0</para>  
        /// <para>Set : LPA>AHOME! → LPA>AHOME_1</para>          
        /// </summary>
        [PacketAttribute("LPA>AHOME", "!", "?", true, true, true, true, enumPacketMode.Both)]
        AUTO_HOME_START,
        
        /// <summary>             
        /// <para>Set : LPA>NOAHOME! → LPA>AHOME_0</para>          
        /// </summary>
        [PacketAttribute("LPA>NOAHOME", "!", "?", false, false, true, true, enumPacketMode.SetOnly)]
        AUTO_HOME_STOP,

        /// <summary>             
        /// <para>Get : LPA>STATUS? → LPA>X_Y</para>          
        /// </summary>
        [PacketAttribute("LPA>STATUS", "!", "?", false, false, true, true, enumPacketMode.GettingOnly)]
        STATAUS,

        /// <summary>             
        /// <para>Get : LPA>WL? → LPA>WL_XXX</para>          
        /// </summary>
        [PacketAttribute("LPA>WL", "!", "?", false, false, true, true, enumPacketMode.GettingOnly)]
        WAVE_LENGTH,
       
        /// <summary>             
        /// <para>Get : LPA>FW? → LPA>_1.0.0.1</para>          
        /// </summary>
        [PacketAttribute("LPA>FW", "!", "?", false, false, true, true, enumPacketMode.GettingOnly)]
        FIRMWARE_VERSION,

        /// <summary>             
        /// <para>Get : LPA>ID? → LPA>_LPA1901001</para>          
        /// </summary>
        [PacketAttribute("LPA>ID", "!", "?", false, false, true, true, enumPacketMode.GettingOnly)]
        SERIAL_NUMBER,

        /// <summary>             
        /// <para>Get : LPA>RESET! → LPA>RESET</para>          
        /// </summary>
        [PacketAttribute("LPA>RESET", "!", "?", true, true, true, true, enumPacketMode.SetOnly)]
        DEVICE_RESET,

        /// <summary>             
        /// <para>Get : LPA>ON! → LPA>ON</para>          
        /// </summary>
        [PacketAttribute("LPA>ON", "!", "?", true, true, true, true, enumPacketMode.SetOnly)]
        MOTOR_ENABLE_ON,
        /// <summary>             
        /// <para>Get : LPA>OFF! → LPA>OFF</para>          
        /// </summary>
        [PacketAttribute("LPA>OFF", "!", "?", true, true, true, true, enumPacketMode.SetOnly)]
        MOTOR_ENABLE_OFF,
    }

    static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Value"></param>
        /// <returns></returns>
        public static PacketAttribute GetPacketAttrFrom(this LPA_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<Commucation.PacketAttribute>();
        }
        public static string GetPacketAttrstrCMD(this LPA_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<Commucation.PacketAttribute>().strCMD;

        }
        public static string GetPacketAttrstrSetMark(this LPA_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<Commucation.PacketAttribute>().strSetMark;

        }
        public static string GetPacketAttrstrGettingMark(this LPA_CMD a_Value)
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<Commucation.PacketAttribute>().strGettingMark;
        }
        /// <summary>
        /// Attribute Generic Function
        /// </summary>
        /// <typeparam name="T">Attribute Class</typeparam>
        /// <param name="a_Value">Enum Value</param>
        /// <returns>Attribute Class</returns>
        /*public static T GetAttributesFrom<T>(this Enum a_Value) where T : Attribute
        {
            return a_Value.GetType().GetMember(a_Value.ToString())
                           .First()
                           .GetCustomAttribute<T>();
        }*/
    }
}
