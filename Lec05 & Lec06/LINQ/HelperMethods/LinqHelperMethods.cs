using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataLoader.Model;
using LINQ.Enums;


namespace LINQ.HelperMethods
{
    public static class LinqHelperMethods
    {
        public static AirCrashClassification ClassifyAircrash(int fatalities)
        {
            if (fatalities < 50)
            {
                return AirCrashClassification.Other;
            }
            if (fatalities < 100)
            {
                return AirCrashClassification.Severe;
            }
            return fatalities < 250 ? AirCrashClassification.Tragic : AirCrashClassification.Catastrophic;
        }


        #region WriteResult helper methods

        public static void WriteResult<T>(IEnumerable<T> collection, string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void WriteResult<T>(IList<IGrouping<bool, T>> groupingsList, string message)
        {
            foreach (var group in groupingsList)
            {
                if (group.Key)
                {
                    Console.WriteLine(message);
                    foreach (var item in group)
                    {
                        Console.WriteLine($"{item}");
                    }
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void WriteResult<T>(IList<IGrouping<AirCrashClassification, T>> groupingsList, string message)
        {
            foreach (var group in groupingsList)
            {
                Console.WriteLine();
                Console.WriteLine(group.Key + $" {message}");
                Console.WriteLine();
                foreach (var item in group)
                {
                    Console.WriteLine($"{item}");
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void WriteResult<T, U>(KeyValuePair<T, U> result, string message)
        {
            Console.WriteLine($"{message} {result.Key} {result.Value}");
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void WriteResult<T, U>(IEnumerable<KeyValuePair<T,U>> results, string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            foreach (var result in results)
            {
                var innerCollection = result.Value as IEnumerable;
                if (innerCollection != null)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{result.Key}");
                    Console.WriteLine();
                    foreach (var item in innerCollection)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine($"{result.Key} {result.Value}");
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void WriteResult(int result, string message)
        {
            WriteResult<int>(result, message);
        }

        public static void WriteResult(double result, string message)
        {
            WriteResult<double>(result, message);
        }

        public static void WriteResult(bool result, string message)
        {
            WriteResult<bool>(result, message);
        }

        public static void WriteResult(AirCrash result, string message)
        {
            WriteResult<AirCrash>(result, message);
        }

        public static void WriteResult(Aircraft result, string message)
        {
            WriteResult<Aircraft>(result, message);
        }

        private static void WriteResult<T>(T result, string message)
        {
            Console.WriteLine($"{message} {result}");
            Console.WriteLine();
            Console.ReadKey();
        }

        #endregion
    }
}
