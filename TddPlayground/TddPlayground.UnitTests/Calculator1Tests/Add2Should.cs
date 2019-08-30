using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace TddPlayground.UnitTests.Calculator1Tests
{
    [TestClass]
    public class Add2Should
    {
        /// <summary>
        ///  Example test to demonstrate a bug in our Add2 method
        /// </summary>
        [TestMethod]
        public void AddNumbers()
        {
            var number1 = RandomValue.Int(999);
            var number2 = RandomValue.Int(999);

            var numbers = new List<int> { number1, number2 };

            var sum = Calculator1.Add2(numbers);

            sum.Should().Be(number1 + number2);
        }

        // Requirement is that is should add "ALL" numbers in the list, currently its adding just the first two
    }
}
