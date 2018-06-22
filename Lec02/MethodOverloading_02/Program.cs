using System;
using System.Collections.Generic;
using OperatorOverloading;

namespace MethodOverloading_02
{
    /// <summary>
    /// Based on samples from Filip Opaleny and Milan Mikus
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Method overloading
            TestSimpleOverloadedMethods();

            // Operator overloading
            TestComplex();
            TestComplexComparer();

            // Samostatna prace: implementace pretizeni
            // nasledujicich operatoru ve tride BusinessMan
            // I.  implementujte operátory <, >
            // II. implementujte operátory <=, >= 
            //     (bez použitia CompareTo)
            TestSolution();
        }

        private static void TestSimpleOverloadedMethods()
        {
            Console.WriteLine("Output of simple overloaded methods \n");

            var logger = new Logger(DateTime.Now);

            // volani pretizene metody
            logger.WriteLoggedTime();

            // volani pretizene metody s volitelnym parametrem
            logger.WriteLoggedTime("Log with date");

            Console.ReadKey();
        }

        private static void TestComplex()
        {
            var complex1 = new Complex(10, 10);
            var complex2 = new Complex(1, 2);
            var complex3 = complex1 + complex2;
            int compareResult = complex1.CompareTo(complex2);
            Console.WriteLine($"Real: {complex3.Real}, Imag: {complex3.Imag}");
            Console.ReadKey();
        }

        private static void TestComplexComparer()
        {
            var list = new List<Complex> { new Complex(10, 10), new Complex(20, 30), new Complex(1, 2) };
            list.Sort(new ComplexComparer());
            foreach (var complex in list)
            {
                Console.WriteLine(complex.Real);
            }
            Console.ReadKey();
        }

        private static void TestSolution()
        {
            var businessMen = new List<BusinessMan>
            {
                new BusinessMan { Name = "Arnold", ValueOfBuisness = 8000, ValueOfCar = 200, ValueOfHouse = 3000 },
                new BusinessMan { Name = "Brandor", ValueOfBuisness = 3000, ValueOfCar = 0, ValueOfHouse = 5000 },
                new BusinessMan { Name = "Cyrill", ValueOfBuisness = 6000, ValueOfCar = 40, ValueOfHouse = 50 },
                new BusinessMan { Name = "Dean", ValueOfBuisness = 13, ValueOfCar = 444, ValueOfHouse = 10000 },
                new BusinessMan { Name = "Ester", ValueOfBuisness = 0, ValueOfCar = 0, ValueOfHouse = 10 },
                new BusinessMan { Name = "Franky", ValueOfBuisness = 5000, ValueOfCar = 5000, ValueOfHouse = 5000 }
            };


            if (businessMen[0] < businessMen[1]
                || businessMen[2] > businessMen[3]
                || businessMen[3] <= businessMen[4]
                || businessMen[4] >= businessMen[5])
            {
                Console.WriteLine("Neporovnáva niečo naopak?");
            }
            else if (businessMen[0] > businessMen[0] || businessMen[0] < businessMen[0])
            {
                Console.WriteLine("Funguje správne porovnávanie dvoch rovnakých objektov?");
            }
            else
            {
                Console.WriteLine("Správne!");
            }

            Console.ReadLine();
        }

    }
}
