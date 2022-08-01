using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using UnitConversions;

namespace UnitsOfMeasureTests
{
    [TestFixture]
    class V1Tests
    {
        private const float pressure = 1234.5678f;

        [Test]
        public void GivenOriginalValueEqualsConstant()
        {
            Assert.AreEqual(pressure, 1234.5678f, 0.0f);
        }

        [Test]
        public void GivelOriginalPressureOutputShouldMatch()
        {
            string output = Format.Pressure(pressure);

            Assert.AreEqual("1234.5678", output);
        }

        //Console.WriteLine(" Original Pressure: " + pressure);
        //    Console.WriteLine("==============================");
        //    Console.WriteLine("Default output: " + Format.Pressure(pressure));
        //    Console.WriteLine("With format string: " + Format.Pressure(pressure, "N2"));
        //    Console.WriteLine("As Bar: " + Format.Pressure(pressure, PressureConfiguration.Units.Bar));

    }
}
