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
        public void DefaultFormatShouldMatchOriginalPressureWith2DecimalPlaces()
        {
            string output = Format.Pressure(pressure);

            Assert.AreEqual("1234.57", output);
        }

        [Test]
        public void FormatStringAsArgumentShouldMatchOriginalPressureWith2DecimalPlaces()
        {
            string output = Format.Pressure(pressure, "N2");

            //'1 234.57' is not equal to 1234.5678.ToString("N2"); This is a bug
            Assert.AreEqual((1234.5678f).ToString("N2"), output);
        }

        [Test]
        public void PressureAsBar()
        {
            string output = Format.Pressure(pressure, PressureConfiguration.Units.Bar);

            Assert.AreEqual("123456.78", output);
        }

        //    Console.WriteLine("As Bar: " + Format.Pressure(pressure, PressureConfiguration.Units.Bar));

    }
}
