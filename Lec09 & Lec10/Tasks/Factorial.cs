using System.Linq;
using System.Numerics;

namespace Tasks
{
    public static class Factorial
    {
        public static int ComputeSmallFactorial(int n)
        {
            return n <= 1 ? 1 : Enumerable.Range(1, n).Aggregate((i, r) => r * i);
        }

        public static BigInteger ComputeBigFactorial(int n)
        {
            BigInteger result = 1;
            for (var i = 1; i <= n; i++)
            {
                result = result * i;
            }
            return result;
        }
    }
}
