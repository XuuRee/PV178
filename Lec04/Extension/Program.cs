using System;
using System.Collections.Generic;

namespace Extension
{
    /// <summary>
    /// Based on demo sample made by Lukas Daubner
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Otestovani samostatne prace na cviceni (zadani viz Lab04_Tasks)
            //Solution.TestSolution();

            var s = "abrahadabra";
            Console.WriteLine(s);
            Console.WriteLine(s.FirstLetterUpper());
            Console.WriteLine();
            Console.ReadKey();

            var i = 5;
            Console.WriteLine(i.IsDividibleBy(5));
            Console.WriteLine(i.IsDividibleBy(2));
            Console.WriteLine();
            Console.ReadKey();

            var dMessage = new Dictionary<string, string>()
            {
                {"Place", "Garden" },
                {"Food", "Salad" }
            };
            Console.WriteLine(dMessage.GetValueOrDefault("Place"));
            Console.WriteLine(dMessage.GetValueOrDefault("Animal") ?? "null");

            Console.ReadKey();
        }
    }
}
