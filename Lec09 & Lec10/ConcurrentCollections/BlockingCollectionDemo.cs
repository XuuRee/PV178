using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class BlockingCollectionDemo
    {
        /// <summary>
        /// Blocking collection acts like a wrapper among collections implementing IProducerConsumer 
        /// such as ConcurrentQueue, ConcurrentStack or ConcurrentBag. It modifies the behaviour of
        /// TryTake / Take method - when called on empty collection, it waits (blocks) untill there 
        /// is at least a single element available.
        /// </summary>
        internal static void RunBlockingCollectionDemo()
        {
            RunBlockingCollectionBoundedLimitDemo();

            RunProducerConsumerBasicDemo();
            
            RunProducerConsumerCompleteAddingDemo();
        }

       
        /// <summary>
        /// We can specify the type of collection, wrapped by blocking collection and also 
        /// optionaly specify bounded capacity, which blocks the producer when the given 
        /// capacity is exceeded. By default ConcurrentQueue is used with no bounded capacity.
        /// </summary>
        private static void RunBlockingCollectionBoundedLimitDemo()
        {
            var blockingCollection = new BlockingCollection<int>(new ConcurrentBag<int>(), 3);
            blockingCollection.Add(1);
            blockingCollection.Add(1);
            blockingCollection.Add(2);

            // We can specify a timeout for waiting to add item to blocking collection with bounded limit of 3
            Console.WriteLine(blockingCollection.TryAdd(3, TimeSpan.FromSeconds(1))
                ? "Item added"
                : "Item was not added");
            Console.ReadKey();
        }

        /// <summary>
        /// Simple demonstration of one thread slowly producing content while other consuming it.
        /// </summary>
        private static void RunProducerConsumerBasicDemo()
        {
            var blockingCollection = new BlockingCollection<int>(new ConcurrentStack<int>(), 3) {1, 2, 3};
            Task.Run(() =>
            {
                for (var i = 0; i < 7; i++)
                {
                    // Add method will also be blocked unless some item will be taken from collection
                    blockingCollection.Add(i);
                    Thread.Sleep(250 * i);
                }
            });
            Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    // Will get blocked when items count in blockingCollection gets down to zero
                    var item = blockingCollection.Take();
                    Console.WriteLine($"Taken {item}");
                }
            });
            Console.ReadKey();
        }

        /// <summary>
        /// Example of CompleteAdding() usage to stop blocking in case of empty collection.
        /// </summary>
        private static void RunProducerConsumerCompleteAddingDemo()
        {
            var blockingCollection = new BlockingCollection<int>();
            Task.Run(() =>
            {
                for (var i = 0; i < 10; ++i)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    blockingCollection.Add(i);
                }

                // Signal that no more elements will be added to collection 
                // -> no blocking when collection is empty 
                blockingCollection.CompleteAdding();

                // this will set the IsAddingCompleted property of blockingCollection to true
            });

            // GetConsumingEnumerable() returns items as soon as they are available 
            // in the collection (and blocks when collection is empty)
            foreach (int item in blockingCollection.GetConsumingEnumerable())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Adding is completed and collection is empty: {blockingCollection.IsCompleted}");
            Console.ReadKey();
        }
    }
}