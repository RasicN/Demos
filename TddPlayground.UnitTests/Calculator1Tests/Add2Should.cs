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
            // Arrange
            var number1 = RandomValue.Int(999);
            var number2 = RandomValue.Int(999);
            var numbers = new List<int> {number1, number2};

            // Act
            var sum = Calculator1.Add2(numbers);

            // Assert
            sum.Should().Be(number1 + number2);
        }

        // Requirement is that the Add2 method should add ALL numbers right now its adding just two
    }
}
