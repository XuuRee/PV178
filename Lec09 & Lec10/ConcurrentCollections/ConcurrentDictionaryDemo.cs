using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentDictionaryDemo
    {
        internal static void RunConcurrentDictionaryDemo()
        {
            // Usage of concurrent dictionary basic operations
            RunConcurrentDictionaryBasicUsageDemo();

            // Example of accessing concurrent dictionary from multiple tasks
            RunMultipleAccessDemo();

            // Performance test of concurrent dictionary vs. ordinary dictionary
            RunConcurrentCollectionsPerformanceDemo();
        }

        /// <summary>
        /// Concurrent collections provides (among others) 
        /// exceptionless Try* methods to perform modifications
        /// </summary>
        private static void RunConcurrentDictionaryBasicUsageDemo()
        {
            var dictionary = new ConcurrentDictionary<string, string>();

            // insert
            dictionary.TryAdd("1", "First"); 
            dictionary.TryAdd("2", "Second");
            dictionary.TryAdd("1", "Third");  //returns false (with no exception);

            //retrieve
            dictionary.TryGetValue("1", out string itemValue1);
            dictionary.TryGetValue("3", out string itemValue2); //returns false (with no exception);
            // enumeration is also fully thread safe

            // update
            var returnTrue = dictionary.TryUpdate("1", "New Value", "First");
            var returnsFalse = dictionary.TryUpdate("3", "New Value 2", "No Value"); //Returns false (with no exception);

            // delete
            var result = dictionary.TryRemove("2", out string removedItem);
        }

        /// <summary>
        /// Concurrent collections can are fully thread safe, 
        /// for instance unlike ordinary collections, they can 
        /// be updated during the enumeration as seen on the 
        /// demo below, where enumeration results in printing 
        /// (approximately) first 10 key-value pairs added.
        /// </summary>
        private static void RunMultipleAccessDemo()
        {
            var dictionary = new ConcurrentDictionary<string, string>();

            var t1 = Task.Run(() =>
            {
                for (var i = 0; i < 100; ++i)
                {
                    dictionary.TryAdd(i.ToString(), i.ToString());
                    Thread.Sleep(30);
                }
            });

            var t2 = Task.Run(() =>
            {
                Thread.Sleep(300);
                foreach (var item in dictionary)
                {
                    // Dictionary does not preserve the order in which the items were added
                    Console.WriteLine(item.Key + "-" + item.Value);
                }
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException)
            {
                // No exception will be caught
            }
        }

        /// <summary>
        /// Concurrent collections are tuned for parallel programming. 
        /// The conventional collections outperform them in all but 
        /// highly concurrent scenarios. Much more detailed study 
        /// is available at:
        /// http://download.microsoft.com/download/B/C/F/BCFD4868-1354-45E3-B71B-B851CD78733D/PerformanceCharacteristicsOfThreadSafeCollection.pdf
        /// </summary>
        private static void RunConcurrentCollectionsPerformanceDemo()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var concurrentDictionary = new ConcurrentDictionary<int, int>();
            for (var i = 0; i < 1_000_000; i++)
            {
                concurrentDictionary[i] = 42;
            }
            stopwatch.Stop();
            var concurrentDictionaryTime = stopwatch.Elapsed.Milliseconds;
           
            stopwatch.Restart();

            var ordinaryDictionary = new Dictionary<int, int>();
            for (var i = 0; i < 1_000_000; i++)
            {
                lock (ordinaryDictionary)
                {
                    ordinaryDictionary[i] = 42;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Ordinary collection (with locking) was more than " +
                              $"{concurrentDictionaryTime / stopwatch.Elapsed.Milliseconds} times faster.");
            Console.ReadLine();
        }
    }
}