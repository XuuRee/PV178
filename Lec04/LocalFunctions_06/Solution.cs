using System;

namespace LocalFunctions_06
{
    public class Solution
    {
        public static void TestSolution()
        {
            Console.WriteLine($"Sum of factorials for numbers 3 and 4 is: {ComputeSumOfFactorials(3, 4)}");
        }

        private static long ComputeSumOfFactorials(params int[] numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentException(nameof(numbers));
            }

            long sum = 0;
            foreach (var number in numbers)
            {
                sum += ComputeFactorial(number);
            }
            return sum;

            long ComputeFactorial(int number)
            {
                long factorial = 1;
                for (var i = 2; i <= number; i++)
                {
                    factorial *= i;
                }

                return factorial;
            }
        }
    }
}