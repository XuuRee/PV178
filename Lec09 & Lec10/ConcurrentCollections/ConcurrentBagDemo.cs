using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentBagDemo
    {
        /// <summary>
        /// Concurrent Bag stores an unordered collection of objects (duplicates are allowed).
        /// It is used for situation when we dont care which element is obtained from the collection,
        /// we can also refer to this usage as a typical producent - consument scenario.
        /// </summary>
        internal static void RunConcurrentBagDemo()
        {
            RunConcurrentBagBasicUsageDemo();

            RunProducerConsumerDemo();
        }
    
        private static void RunConcurrentBagBasicUsageDemo()
        {
            var concurrentBag = new ConcurrentBag<int>();

            // Calling Add(...) from multiple threads causes no contention 
            // as each thread has its own private linked list of items
            concurrentBag.Add(1);
            concurrentBag.Add(2);
            concurrentBag.Add(1);

            // gives you the element added most recently on current thread, in case of no elements, false is returned
            concurrentBag.TryTake(out int takenElement);
            // TryPeek(...) does not remove element from the bag
            concurrentBag.TryPeek(out int peakedElement);
        }

        private static void RunProducerConsumerDemo()
        {
            var bag = new ConcurrentBag<int>();
            var autoResetEvent = new AutoResetEvent(false);

            Task.Run(() =>
            {
                for (var i = 7; i < 10; i++)
                {
                    bag.Add(i);
                }
                autoResetEvent.Set();
            });

            // Concurrent bag are good for situation when thread is both producing and consuming items
            Task.Run(() =>
                {
                    for (var i = 1; i < 7; i++)
                    {
                        bag.Add(i);
                    }
                    //wait for second thread to add its items
                    autoResetEvent.WaitOne();

                    while (!bag.IsEmpty)
                    {
                        if (bag.TryTake(out int item))
                        {
                            Console.WriteLine(item);
                        }
                    }
                })
                .Wait();
            Console.ReadKey();

            // Ideal usage of concurrent bag is when number of Add and Take operatios are 
            // at least balanced (or Adds have majority) on given thread, otherwise there can 
            // be a significant contention.
        }

    }
}