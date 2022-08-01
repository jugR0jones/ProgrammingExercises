using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConversions
{
    public class Configuration
    {
        public string OutputFormat
        {
            get; set;
        }
    }

    public class PressureConfiguration : Configuration
    {

        public enum Units
        {
            Pa,
            Kpa,
            Bar
        };

        public Units originalUnits = Units.Pa;
        public Units outputUnits = Units.Pa;

    }

    public class MassConfiguration : Configuration
    {

    }

    public class SpeedConfiguration : Configuration
    {

    }

    public static class Convert
    {
        /// <summary>
        /// Convert pressure to Pa.
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public static float PressureToPa(float pressure, PressureConfiguration.Units fromUnits)
        {
            return fromUnits switch
            {
                PressureConfiguration.Units.Pa => pressure,
                PressureConfiguration.Units.Kpa => pressure / 1000.0f,
                PressureConfiguration.Units.Bar => pressure / 100000.0f,
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Convert pressure from Pa.
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static float PressureFromPa(float pressure, PressureConfiguration.Units toUnits)
        {
            return toUnits switch
            {
                PressureConfiguration.Units.Pa => pressure,
                PressureConfiguration.Units.Kpa => pressure * 1000.0f,
                PressureConfiguration.Units.Bar => pressure * 100000.0f,
                _ => throw new NotImplementedException()
            };
        }
    }

    public static class Format
    {
        private static PressureConfiguration pressureConfiguration = new PressureConfiguration()
        {
            OutputFormat = "F2"
        };

        public static string Pressure(float pressure)
        {
            return pressure.ToString(pressureConfiguration.OutputFormat);
        }

        public static string Pressure(float pressure, string format)
        {
            return pressure.ToString(format);
        }

        /// <summary>
        /// Assume pressure is in KPa
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public static string Pressure(float pressure, PressureConfiguration.Units units)
        {
            float pressureInPa = Convert.PressureToPa(pressure, PressureConfiguration.Units.Kpa);
            float convertedPressure = Convert.PressureFromPa(pressureInPa, units);

            return convertedPressure.ToString(pressureConfiguration.OutputFormat);
        }
    }
}
