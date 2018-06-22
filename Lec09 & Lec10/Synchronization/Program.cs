using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            MutexTest();
            ReaderWriterLockTest();
        }

        #region Mutex

        private static void MutexTest()
        {
            using (var mutex = new Mutex(false, "PV178"))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another instance of this app is running.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("I am running");
                    Console.ReadLine();
                }
            }

            // Samostatná práce - použití vhodného synchronizačního konstruktu:
            //
            // Implementace obsluhy pacientů v nemocnici.
            // V nemocnici máme 3 všeobecné doktory, u kterých 
            // je nedeterministické pořadí pacientů.
            // Každý se zdrží u doktora náhodný čas mezi 1000 až 2000 ms.
            //
            // Doporučený postup:
            // Nejdříve si zkuste implementaci provést pomocí monitoru. 
            // Následně se pak zkuste zamyslet, zda by bylo možné použít
            // nějaký vhodnější synchronizační konstrukt.
            // </summary>
            var hospital = new Solution01();
            hospital.Process10Patients();
            Console.ReadLine();


            // Samostatná práce - prevence deadlocku:
            // 
            // Implementace nize uvedene metody PerformTransferTo
            // (viz dokumentace). Pri implementaci si dejte pozor
            // na mozny deadlock pri soubezne manipulaci s ucty.
            //
            Solution02.TestAccountTransactions();
            Console.ReadLine();
        }

        #endregion

        #region ReaderWriterLockSlim

        /// <summary>
        /// Generally ReaderWriterLockSlim has greater overhead 
        /// than simple lock, however in case of number of readers
        /// significantly outnumbers the writers and the lock is held 
        /// for longer period of time, it can outperform the lock.
        /// </summary>
        private static readonly ReaderWriterLockSlim RwLockSlim = new ReaderWriterLockSlim();
        private static readonly List<int> Items = new List<int>();
        private static readonly Random rand = new Random();

        /// <summary>
        /// You can see that ReaderWriterLockSlim prefers readers over writers,
        /// this can potentially lead to many queud-up writers in write-intensive
        /// scenarios. 
        /// </summary>
        private static void ReaderWriterLockTest()
        {
            Task.Run(() => Write("FirstW"));
            for (var i = 0; i < 200; i++)
            {
                Task.Run(() => Read());
            }
            Task.Run(() => Write("SecondW"));
            Console.Read();
        }

        /// <summary>
        /// There can be more readers sharing the same (read) lock
        /// </summary>
        private static void Read()
        {
            RwLockSlim.EnterReadLock();
            foreach (var unused in Items)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("R");
            RwLockSlim.ExitReadLock();
        }

        /// <summary>
        /// But only single writer at a time
        /// </summary>
        private static void Write(string writerName)
        {
            for (var i = 0; i < 20; i++)
            {
                var newNumber = GetRandNum(50);
                RwLockSlim.EnterWriteLock();
                Items.Add(newNumber);
                Console.WriteLine($"{writerName} wrote {newNumber} to: {string.Join(",", Items)}");
                RwLockSlim.ExitWriteLock();
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Random is not thread safe
        /// </summary>
        static int GetRandNum(int max)
        {
            lock (rand)
            {
                return rand.Next(max);
            }           
        }

        #endregion
    }
}
