using System;
using System.Threading;

namespace NonBlockingSynchronization
{
    /// <summary>    
    /// Interlocked methods are approximately twice as fast when 
    /// compared to standard lock statement, however in case of for
    /// loop with thousands of iterations could be significantly 
    /// slower than a single lock statement, wrapping the for loop.
    /// </summary>
    internal class InterlockedDemo
    {
        private static long number; // all operations on 64bit number are non-atomic in x86 environments

        internal void RunInterlockedDemo()
        {
            // Interlocked class provides a set of methods for atomic operations
            Interlocked.Increment(ref number);
            // Internally, all interlocked methods generates full fence, 
            // so there is no need to use additional MemoryBarrier() calls.
            Interlocked.Add(ref number, 41);
            Console.WriteLine(Interlocked.Read(ref number));

            // Interlocked.Exchange atomically reads old value and writes a new one, so 42 gets printed again     
            Console.WriteLine(Interlocked.Exchange(ref number, 1));

            // Interlocked.Interlocked.Exchange writes value only if the former value matches the expected one  
            Console.WriteLine(Interlocked.CompareExchange(ref number, 7, 1));
            Console.WriteLine(Interlocked.Read(ref number));
            Console.ReadKey();
        }
    }
}
