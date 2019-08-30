using System;
using System.Collections.Generic;

namespace TddPlayground
{
    public static class Calculator1
    {
        public static int Add1(int number1, int number2)
        {
            return number1 + number2;
        }

        public static int Add2(List<int> numbers)
        {
            var number1 = numbers[0];
            var number2 = numbers[1];

            return number1 + number2;
        }
    }
}
