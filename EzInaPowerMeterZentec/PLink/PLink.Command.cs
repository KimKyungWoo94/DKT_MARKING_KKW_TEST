using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
namespace EzIna.PowerMeter.Zentec
{
    enum PLINK_CMD
    {
        /// <summary> 
        /// Turns the attenuator correction ON when available for the detector
        /// <returns>"ACK\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("ATT", "","", false, false, true, true, enumPacketMode.SetOnly)]      
        ATTENUATOR_ON,
        /// <summary>
        /// Turns the attenuator correction OFF. OFF by default
        /// <returns>“ACK\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("ATF", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        ATTENUATOR_OFF,
        /// <summary>
        /// Sends the data points through the serial port at a frequency of 10Hz
        /// <returns>“9.793354e-01\r\r\n  9.792939e-01\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("CAU", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        CAU_MODE_START,
        /// <summary>
        /// Ends the *CAU mode
        /// <returns>“ACK\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("CSU", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        CAU_MODE_STOP,
        /// <summary>
        /// Returns a single measurement (the current measurement) through the serial port.
        /// <returns>“ACK\r\r\n9.793354e01\r\r\n” </returns>
        /// </summary>
        [PacketAttribute("CVU", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        GET_MEASURE_VALUE,
        /// <summary>
        /// Turns the anticipation ON. ON by default & return characters « ACK »
        /// <returns>“ACK\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("ANT", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        ANTICIPATION_ON,
        /// <summary>
        /// Turns the anticipation OFF /
        /// <returns> “ACK\r\r\n” </returns>
        /// </summary>
        [PacketAttribute("ANF", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        ANTICIPATION_OFF,
        /// <summary>
        /// Modifies the analog output voltage value (*AOB + 8 characters)
        /// <para>Example: *AOB1.00E+01 The maximum value of the analog</para>
        /// <para>display (2.05 volts) is 10 watts</para>
        /// </summary>
        [PacketAttribute("AOB", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        ANALOG_OUT_VOLT_VALUE,
        /// <summary>
        /// Sets an analog output delay from 1s to 7s for a return to 0V after 
        /// <para>an energy measurement. Default 0 (no return to zero after an energy measurement).</para>
        /// <para>Example: *AOD2.00E+00 The delay is 2 seconds.</para>
        /// <returns>“ACK\r\r\n”</returns>
        /// </summary>
        [PacketAttribute("AOD", "", "", false, false, true, true, enumPacketMode.SetOnly)]
        ANALOG_OUT_DISPLAY_DELAY,
        /// <summary>
        /// Reset Device /
        /// <returns>No Return </returns>
        /// </summary>
        [PacketAttribute("RST", "", "", false, false, false, false, enumPacketMode.SetOnly)]
        RESET_DEVICE,

        /// <summary>
        /// Reset Device /
        /// <returns>“ACK\r\r\n” </returns>
        /// </summary>
        [PacketAttribute("SOU", "", "", false, false, false, false, enumPacketMode.SetOnly)]
        SET_ZERO_OFFSET,

        /// <summary>
        /// Wavelength correction value (+ 5 characters)
        /// <para>Example: *PWC01064 selects the wavelength 1064 nm</para>
        /// <returns>“ACK\r\r\n” </returns>
        /// </summary>
        [PacketAttribute("PWC", "", "", false, false, false, false, enumPacketMode.SetOnly)]
        WAVE_LENGTH_SET,

        /// <summary>
        /// Modifies the trigger level in energy mode (+ 8 characters)
        /// <para>Example: *TLC2.00E-02 selects a Trig Level of 0.002 Joules</para>         
        /// <returns>“ACK\r\r\n” </returns>
        /// </summary>
        [PacketAttribute("TLC", "", "", false, false, false, false, enumPacketMode.SetOnly)]
        TRIGGER_LEVEL_IN_ENERGY_MODE,
       

    }
}
