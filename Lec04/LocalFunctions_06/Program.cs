using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalFunctions_06
{
    class Program
    {
        /// <summary>
        /// Lokalni funkce se pouziva v pripadech, kdy pouze v ramci metody
        /// potrebujeme kratkou pomocnou funkci, ktera neni relevantni pro ostatni
        /// cleny dane tridy. Jejim pouzitim tak vylepsime dekompozici dane metody,
        /// v ktere je prislusna lokalni funkce zadefinovana, snadno uplatnime 
        /// princip DRY a ziskame lepsi zapouzdreni nez v pripade pouziti privatni metody.
        /// </summary>
        static void Main(string[] args)
        {
            // Priklad deklarace trivialni lokalni funkce
            int Sum(int x, int y) => x + y;
            // a jejiho volani
            Console.WriteLine(Sum(1, 2));

            // Pro srovnani zapis stejne funkce pomoci lambda vyrazu:
            Func<int, int,int> sum = (x, y) => x + y;
            Console.WriteLine(sum(1,2));
            // Rozdily mezi lambda vyrazy a lokalnimi funkcemi jsou shrnuty nize

            Console.WriteLine(FibonacciLocalFunctionsDemo(7));
            
            LocalFunctionValidationDemo();


            // Zadani:
            // Implementujte metodu, ktera na vstupu bere libovolny pocet cisel, 
            // a na vystupu vrati soucet faktorialu techto cisel. 
            // Pri implementaci vhodne pouzijte lokalni funkci.
            Solution.TestSolution();
        }

        /// <summary>
        /// Lokalni funkce je vzdy privatni a 
        /// muze byt zadefinovana a volana z:
        /// metody (i anonymni), konstruktoru, lambda vyrazu,
        /// pristupovych get, set metod vlastnosti, atd.
        /// K lokalni funkci ma pristup pouze clen ve kterem
        /// je zadefinovana, zadny dalsi clen dane tridy k ni
        /// nema pristup.
        /// </summary>
        private static int FibonacciLocalFunctionsDemo(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be >= 0", nameof(n));
            }    
            var cache = new Dictionary<int, int> { { 0, 0 }, { 1, 1 } };
            return GetFibonacciNumber(n);

            // Lokalni funkce pro vypocet fibonacciho cisla s vyuzitim memoizace (mezivysledky jsou cachovany).
            // Diky pouziti lokalni funkce je metoda FibonacciLocalFunctionsDemo01(...) lepe dekomponovana a jeji             
            // logika optimalne zapouzdrena (GetFibonacciNumber(...) je viditelna pouze z metody, ve ktere je definovana).
            int GetFibonacciNumber(int number)
            {
                if (cache.ContainsKey(number))
                {
                    return cache[number];   // Lokalni funkce ma pristup ke vsem promenym (a parametrum) metody, ktera ji definovala
                }   
                var value = GetFibonacciNumber(number - 1) + GetFibonacciNumber(number - 2);
                cache[number] = value;
                return value;
            }
        }

        /// <summary>
        /// Ukazka chovani lokalni funkce pri implementaci iteratoru
        /// </summary>
        private static void LocalFunctionValidationDemo()
        {
            var enumerableWithoutLocal = AsEnumerable<object>(null);
            try
            {
                enumerableWithoutLocal.ToList();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Validation was triggered on demand without local function.");
            }

            try
            {
                AsEnumerableWithLocalFunction<object>(null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Validation was triggered instantly with local function.");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// V pripade implementace iteratoru se kod vykona "on demand", validace tedy neprobehne okamzite
        /// </summary>
        public static IEnumerable<T> AsEnumerable<T>(params T[] items)
        {
            if (items == null)
            {
                throw new ArgumentException(nameof(items));
            }
            foreach (var item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// V pripade pouziti lokalni funkce se kod s validaci pred jejim zavolanim vykona okamzite
        /// </summary>
        public static IEnumerable<T> AsEnumerableWithLocalFunction<T>(params T[] items)
        {
            if (items == null)
            {
                throw new ArgumentException(nameof(items));
            }
            return Enumerate(items);

            IEnumerable<T> Enumerate(T[] array)
            {
                foreach (var item in array)
                {
                    yield return item;
                }
            }
        }

        // Porovnani lokalni funkce s lambda vyrazy:
        // -> Lokalni funkce maji lepsi vykon, nebot nevyzaduji alokaci delegatu, jako je tomu v pripade lambda vyrazu
        // -> Lokalni funkce podporuji ref, out a params, narozdil od lambda vyrazu, kde jejich pouziti neni mozne, viz: https://stackoverflow.com/questions/1365689/cannot-use-ref-or-out-parameter-in-lambda-expressions/1365865
        // -> Lambda vyrazy nemohou byt genericke, lokalni funkce ano
        // -> Lokalni funkce mohou byt definovany kdekoliv v dane metode (i za prikazem return)
        // -> Lokalni funkce mohou byt implementovany jako iterator (pomoci prikazu: yield return), lambda vyrazy nikoliv
    }
}
