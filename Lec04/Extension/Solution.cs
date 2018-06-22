using System;
using System.Collections.Generic;

namespace Extension
{
    public static class Solution
    {
        public static void TestSolution()
        {
            var sampleLine = "Hello world!";
            var wordCount = sampleLine.CountWordsWithinLine();
            Console.WriteLine($"Line: {sampleLine} has {wordCount} words.");
            Console.WriteLine();
            Console.ReadKey();

            var sampleWords = new[] { "This", "is", "something", "that", "should", "be", "together" };
            Console.WriteLine($"Whole string is: \"{sampleWords.StringJoin("; ")}\"");
            Console.WriteLine();
            Console.ReadKey();

            var sampleWord = "Coolermaster";
            var first4Chars = sampleWord.GetFirstFourChars();
            Console.WriteLine($"First 4 chars of: {sampleWord} are {first4Chars}");
            Console.WriteLine();
            Console.ReadKey();

            const int element = 3;
            var numbers = new[] {1, 3, 7, 3, 5, 2, 3, 4, 8, 3, 9};
            var occurencesOfElement = numbers.CountOccurancesOfElement(element);
            Console.WriteLine($"Element: {element} has occured {occurencesOfElement}x times");
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// vrati prvni 4 znaky daneho retezce (pokud ma delku > 4)
        /// </summary>
        /// <param name="word">retezec v ramci ktereho vrati prvni 4 znaky</param>
        /// <returns></returns>
        public static string GetFirstFourChars(this string word)
        {
            return word.Length < 4 ? word : word.Substring(0, 4);
        }

        /// <summary>
        /// Pocita slova v ramci daneho radku (retezce)
        /// </summary>
        /// <param name="line">retezec v ramci ktereho se pocita vyskyt slov</param>
        /// <returns></returns>
        public static int CountWordsWithinLine(this string line)
        {
            return string.IsNullOrEmpty(line) ? 0 : line.Split(' ').Length;
        }

        /// <summary>
        /// pocita vyskyt daneho prvku v kolekci
        /// </summary>
        /// <typeparam name="T">typ prvku v kolekci</typeparam>
        /// <param name="collection">kolekce ve ktere prvek hledame</param>
        /// <param name="itemToMatch">prvek, jehoz vyskyt hledame</param>
        /// <returns>vrati pocet vyskytu daneho prvku v kolekci</returns>
        public static int CountOccurancesOfElement<T>(this IEnumerable<T> collection, T itemToMatch)
        {
            var count = 0;
            if (collection == null)
            {
                return count;
            }
            foreach (var item in collection)
            {
                if (item.Equals(itemToMatch))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Vraci stringovou reprezentaci prvku v kolekci
        /// </summary>
        /// <typeparam name="T">Typ prvku kolekce.</typeparam>
        /// <param name="items">Polozky, ktere chceme spojit do vysledneho retazce.</param>
        /// <param name="separator">Oddelovac jednotlivych polozek</param>
        public static string StringJoin<T>(this IEnumerable<T> items, string separator) {
            return items == null ? "" : string.Join(separator, items);
        }
    }
}
