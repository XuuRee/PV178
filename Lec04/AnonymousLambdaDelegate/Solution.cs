using System;
using System.Collections.Generic;

namespace AnonymousLambdaDelegate
{
    public static class Solution
    {
        public static void TestAll()
        {
            // TODO prvky z dane kolekce vypiste na samostatne radky
            var collection = new List<string> { "these", "are", "some", "elements" };
            collection.ForEach(Console.WriteLine);
            Console.WriteLine();
            Console.ReadKey();

            // TODO vypsat prvky z teto kolekce, ktere ostre mensi nez 15 
            var numbersToFilter = new[] { 0, 1, 3, 12, 8, 7, 43, 18, 93, 26, 57 };
            numbersToFilter.Where(s=> s < 15).ForEach(Console.WriteLine);
            Console.WriteLine();
            Console.ReadKey();

            // TODO vypsat retezce z teto kolekce, ktere splnuji vlastnost palindromu
            var words = new[] { "civic", "disco", "level", "numb", "racecar"};
            words.PrintPalindromes(word => 
            {
                var charArray = word.ToCharArray();
                Array.Reverse(charArray);
                return word.Equals(new string(charArray));
            });
            Console.WriteLine(Environment.NewLine);
            Console.ReadKey();
        }

        /// <summary>
        /// Vykona akci na kazdem prvku dane kolekce
        /// </summary>
        /// <typeparam name="T">Typ prvku v kolekci</typeparam>
        /// <param name="collection">Kolekce prvku</param>
        /// <param name="action">Akce, ktera se vykona pro kazdy prvek dane kolekce</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action(element);
            }
        }

        /// <summary>
        /// Vyfiltruje z kolekce stringu retezce splnujici vlastnost palindromu
        /// </summary>
        /// <param name="words">Kolekce retezcu</param>
        /// <param name="isPalindromeFunc">predikat, ktery urcuje, zda je dany retezec palindrom</param>
        private static void PrintPalindromes(this IEnumerable<string> words, Func<string, bool> isPalindromeFunc)
        {            
            foreach (var word in words)
            {
                if (isPalindromeFunc(word))
                {
                    Console.WriteLine($"{word}");
                }
            }
        }

        /// <summary>
        /// Vyfiltruje prvky kolekce dle daneho predikatu
        /// </summary>
        /// <typeparam name="T">Typ prvku kolekce</typeparam>
        /// <param name="collection">Kolekce prvku</param>
        /// <param name="condition">Predikat, ktery maji vysledne prvky splnovat</param>
        /// <returns>Prvky splnujici dany predikat</returns>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> condition)
        {
            foreach (var element in collection)
            {
                if (condition(element))
                {
                    yield return element;
                }
            }
        }

    }
}
