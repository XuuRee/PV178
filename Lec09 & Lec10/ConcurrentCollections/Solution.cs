using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class Solution
    {
        /// <summary>
        /// Samostatna prace - konzumace vice blokujicich kolekci:
        ///
        /// I.   Vytvorte pole o dvou blokujicich kolekci, jedna z nich bude interne pouzivat 
        ///      ConcurrentQueue, druha pak ConcurrentStack. 
        /// 
        /// II.  Vytvorte dva Tasky, prvni z nich bude pridavat do prvni blokujici kolekce prvky
        ///      s casovym intervalem 250ms, druhy pak 500ms (kazdy z Tasku prida prave 10 prvku).
        /// 
        /// III. Zajistete aby po pridani vsech prvku v obou Tascich jiz nebyla ani jedna z 
        ///      kolekci nadale blokovana (tedy aby pri volani TryTake neprobihalo cekani).
        /// 
        /// IV.  Pomoci staticke metody BlockingCollection<int>.TryTakeFromAny(...) odebirejte 
        ///      prvky z obou kolekci zaroven (cekejte vsak pouze v pripade pokud jsou obe prazdne
        ///      a maji se do nich jeste pridavat prvky). Odebrane prvky prubezne vypisujte na 
        ///      konzoly (jako typ prvku muzete napriklad zvolit cela cisla).
        /// </summary>
        internal static void TestSolution()
        {
            var producers = new BlockingCollection<int>[2];
            producers[0] = new BlockingCollection<int>();
            producers[1] = new BlockingCollection<int>(new ConcurrentStack<int>());

            Task.Run(() =>
            {
                for (var i = 1; i <= 10; ++i)
                {
                    producers[0].Add(i);
                    Thread.Sleep(250);
                }
                producers[0].CompleteAdding();
            });

            Task.Run(() =>
            {
                for (var i = 11; i <= 20; ++i)
                {
                    producers[1].Add(i);
                    Thread.Sleep(500);
                }
                producers[1].CompleteAdding();
            });

            while (!producers[0].IsCompleted || !producers[1].IsCompleted)
            {
                BlockingCollection<int>.TryTakeFromAny(producers, out int item);
                if (item != default(int))
                {
                    Console.WriteLine(item);
                }
            }
            Console.ReadKey();
        }
    }
}
