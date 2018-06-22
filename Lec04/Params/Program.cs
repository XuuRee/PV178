using System;

namespace Params
{
    class Program
    {
        static void Main(string[] args)
        {
            // Otestovani samostatne prace na cviceni (zadani viz Lab04_Tasks)
             Solution.TestSolution();

            // Invoking method which has no params keyword
            Console.WriteLine(Sum(new int[] {}));
            Console.WriteLine(Sum(new[] { 1 }));
            Console.WriteLine(Sum(new[] { 1, 2, 3 }));
            Console.WriteLine();

            // Invoking method with params keyword
            Console.WriteLine(SumParams());
            Console.WriteLine(SumParams(1));
            Console.WriteLine(SumParams(1, 2, 3));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static int Sum(int[] values)
        {
            var temp = 0;
            for (var i = 0; i < values.Length; i++)
            {
                temp += values[i];
            }
            return temp;
        }

        public static int SumParams(params int[] values)
        {
            var temp = 0;
            for (var i = 0; i < values.Length; i++)
            {
                temp += values[i];
            }
            return temp;
        }
    }
}
