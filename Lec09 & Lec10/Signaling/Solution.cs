using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Signaling
{
    /// <summary>
    /// Samostatna prace - signalizace pomoci CountdownEvent:
    /// 
    /// V predchystanem zadani probiha v separatnim vlakne vypocet 
    /// faktorialu pro tri dane intervaly. Z kazdeho z techto tri intervalu
    /// nas zajima pouze hodnota faktorialu pro jedno cislo, konkretne
    /// chceme vedet vypoctenou hodnotu pro cislo 7 z prvniho intervalu,
    /// cislo 18 ze druheho a cislo 21 ze tretiho.
    /// 
    /// Z techto 3 vypoctenych hodnot pak chceme co nejdrive vypsat uzivateli 
    /// na vystup jejich soucet (nechceme tedy cekat az dobehne vypocet faktorialu 
    /// pro vsechna cisla z daneho rozsahu). Bude tedy potreba signalizovat, ze
    /// mame k dispozici vypoctenou hodnotu pro kazde z dilcich cisel.
    /// Vasim ukolem je tedy pouzit CountdownEvent, tak aby se uzivateli mohl 
    /// zobrazit pozadovany soucet ve chvili, kdy jsou vsechny vysledky k dispozici.
    /// 
    /// Tipy: 
    /// 
    /// -> Vhodne pouzijte metodu Wait() tridy CountDownEvent, ktera ceka, dokud
    ///    nebude zavolan pozadovany pocet .Signal() metod teto tridy (tento pocet
    ///    bere konstruktor tridy CountDownEvent jako parametr)
    /// 
    /// -> vyuzijte parametru action metody ComputeFactorialFromRange k pripadne 
    ///    signalizaci (pokud jiz v danem intervalu doslo k vypoctu pozadovane hodnoty)
    /// </summary>
    internal class Solution
    {
        private const int FirstFactorial = 7;
        private const int SecondFactorial = 18;
        private const int ThirdFactorial = 21;

        private readonly CountdownEvent countdown = new CountdownEvent(3);

        internal void TestSolution()
        {
            Action<int> Signal(int signalNumber)
            {
                return number =>
                {
                    if (number == signalNumber) countdown.Signal();
                };
            }

            IList<BigInteger> part1 = new List<BigInteger>();
            IList<BigInteger> part2 = new List<BigInteger>();
            IList<BigInteger> part3 = new List<BigInteger>();
            Task.Run(() => ComputeFactorialFromRange(part1, 0, 9, Signal(7)));
            Task.Run(() => ComputeFactorialFromRange(part2, 10, 19, Signal(18)));
            Task.Run(() => ComputeFactorialFromRange(part3, 20, 100, Signal(21)));

            countdown.Wait();

            var sum = part1.ElementAt(7)
                      + part2.ElementAt(18-10)
                      + part3.ElementAt(21-20);
            Console.WriteLine($"The sum of factorials of {FirstFactorial}^2, " +
                              $"{SecondFactorial}^2 and {ThirdFactorial}^2 is: {sum}");
            Console.ReadKey();
        }

        /// <summary>
        /// Pro kazde cislo n z intervalu ostre ohraniceneho
        /// parametry min a max provede vypocet faktorialu 
        /// pro hodnotu n^2.
        /// </summary>
        /// <param name="part">Kolekci vypoctenych hodnot pro dany rozsah</param>
        /// <param name="min">Spodni hranice intervalu</param>
        /// <param name="max">Horni hranice intervalu</param>
        /// <param name="action">Delegat, ktery se vyvola po kazdem 
        /// vypoctu pro dane cislo n</param>
        private static void ComputeFactorialFromRange(IList<BigInteger> part, int min, int max, Action<int> action)
        {
            for (var n = min; n <= max; n++)
            {
                part.Add(Tasks.Factorial.ComputeBigFactorial(n^2));
                action?.Invoke(n);
            }
        }
    }
}
