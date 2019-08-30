using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace TddPlayground.UnitTests.Calculator1Tests
{
    [TestClass]
    public class Add1Should
    {
        [TestMethod]
        public void AddNumbers()
        {
            var number1 = RandomValue.Int(999);
            var number2 = RandomValue.Int(999);

            var sum = Calculator1.Add1(number1, number2);
            sum.Should().Be(number1 + number2);
        }
    }
}
