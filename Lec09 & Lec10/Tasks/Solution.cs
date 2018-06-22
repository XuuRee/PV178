using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Tasks
{
    internal class Solution
    {
        private readonly IEnumerable<int> numbers = Enumerable.Range(1, 10_000);

        private const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In faucibus sapien elit, et maximus tortor finibus nec. Mauris a arcu accumsan, consequat felis eu, placerat neque. Etiam aliquam efficitur libero, sit amet tincidunt tortor. Vivamus posuere massa ut tortor consectetur, in accumsan odio fringilla. Proin lacinia felis eu odio molestie, ut ultrices leo finibus. Pellentesque pharetra sodales lectus vitae commodo. Aliquam laoreet tortor ut orci imperdiet, sit amet tempus tortor vehicula. Nunc vel nulla turpis. Sed a laoreet ante. Integer sit amet mauris in lectus varius rhoncus. Morbi consequat suscipit libero nec vehicula. Cras cursus ut odio in dictum. Maecenas non tristique neque, ac aliquam diam. In id libero eu sapien porttitor vehicula. Nunc neque ligula, laoreet a fringilla pharetra, feugiat a magna. Ut pulvinar suscipit diam sit amet faucibus.";

        /// <summary>
        /// Samostatna prace:
        /// 
        /// I.    Vytvorte Task s nazvem parent, v ramci nehoz dojde k vypoctu 
        ///       se bude zpracovavat field numbers a konstanta LoremIpsum (viz)
        ///       body II. a III.)
        /// 
        /// II.   Uvnitr tasku parent vytvorte vnoreny Task evenNumbersProductTask,
        ///       ktery pro vsechna suda cisla z numbers spocita jejich soucin.
        /// 
        /// III.  Uvnitr tasku parent vytvorte vnoreny Task occurancesInLoremIpsumTask,
        ///       ktery spocita vyskyt vsech whitespace znaku v retezci LoremIpsum, 
        ///       v pripade, ze jejich pocet bude ostre mensi nez jedna, nebo vetsi 
        ///       nez 150, vyhodi vhodnou vyjimku.
        ///       
        ///       a) Pripadnou vyhozenou vyjimku podminene zpracujte v navazujicim Tasku
        ///          (staci pouze vypsat chybovou zpravu a vratit hodnotu 1).
        /// 
        ///       b) V pripade, ze occurancesInLoremIpsumTask je uspesne dokoncen,
        ///          vypoctete z jeho vysledku faktorial v navazujicim tasku
        /// 
        /// IV.   Jako vysledek parent Tasku vratte podil vysledku evenNumbersProductTask
        ///       a prislusneho vysledku tasku navazujiciho na occurancesInLoremIpsumTask,
        ///       (tedy pouzijete hodnotu bud z varianty a), nebo b) v zavislosti na tom,
        ///       zda doslo k vyhozeni vyjimky ci nikoliv)
        /// 
        /// V.    Na vystup vypiste pocet cislic z vysledku paren tasku       
        /// 
        /// VI.   Zkuste si zmenit horni hranici vyskytu whitespace znaku v retezci 
        ///       LoremIpsum ze 150 na 100 a upraveny kod otestujte  
        /// </summary>
        public void Compute()
        {
            var parent = Task.Run(() =>
            {
                var evenNumbersProductTask = Task.Run(() =>
                {
                   return numbers
                    .Where(number => number % 2 == 0)
                    .Aggregate(new BigInteger(1),(accumulate, item) => accumulate * item);
                });

                var occurancesInLoremIpsumTask = Task.Run(() =>
                {
                    var numberOfOccurences = LoremIpsum.Count(char.IsWhiteSpace);
                    return numberOfOccurences > 0 && numberOfOccurences < 150
                        ? numberOfOccurences
                        : throw new ArgumentOutOfRangeException("numberOfOccurences");
                });

                var fault = occurancesInLoremIpsumTask.ContinueWith(
                    ant => { Console.WriteLine(ant.Exception?.InnerException?.Message); return 1; }, TaskContinuationOptions.OnlyOnFaulted);

                var success = occurancesInLoremIpsumTask.ContinueWith(faultTask =>
                    Factorial.ComputeBigFactorial(faultTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion);

                var res = occurancesInLoremIpsumTask.IsFaulted ? fault.Result : success.Result;
                
                return evenNumbersProductTask.Result / res;
            });

            Console.WriteLine($"Number of digits: {parent.Result.ToString().Length}");
            Console.ReadKey();
        }
    }
}
