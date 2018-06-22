using System;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingSynchronization
{
    /// <summary>
    /// Full memory barrier (AKA full fence) prevents undesired instruction reordering 
    /// (due to optimisations) or read/write caching around its usage. 
    /// Following MSDN documentation for memmory barrier is misleading (perhaps even 
    /// untrue): "MemoryBarrier is required only on multiprocessor systems with weak 
    /// memory ordering (for example, a system employing multiple Intel Itanium processors)."
    /// </summary>
    internal class MemoryBarrierDemo
    {
        private int result1, result2;
        private bool complete1, complete2;


        /// <summary>
        /// There is no guarantee, this code will print 10, since
        /// optimizations may have taken place. Instruction reordering
        /// or variable caching (assigned values are not visible to other
        /// threads right away) can lead to scenarion where the output will
        /// be equal to integer default value.
        /// </summary>
        internal void PerformTest()
        {
            // Optimizations do not break single threaded code, 
            // which does not apply to following code...
            Task.Run(() => ComputeResult());
            Task.Run(() => WriteResult());
            Console.ReadKey();
        }

        private void ComputeResult()
        {
            // some computation here...
            result1 = 10;
            complete1 = true;
        }

        private void WriteResult()
        {
            if (complete1)
            {
                Console.WriteLine(result1);
            }
        }

        /// <summary>
        /// Full memory barriers are internaly used by lock statement,
        /// signaling or waiting on Tasks for instance...
        /// So the lock statement: lock (lockable) { ... } can be 
        /// translated into (ignoring the mutual exclusion guarantee):
        /// Thread.MemoryBarrier(); { ... } Thread.MemoryBarrier();
        /// </summary>
        internal void PerformTestSafe()
        {
            Task.Run(() => ComputeResultSafe());
            Task.Run(() => WriteResultSafe());
            Console.ReadKey();
        }

        /// <summary>
        /// Safe approach:
        /// 1. place MemoryBarriers before (and after) every read and write
        /// 2. remove those you dont need (can be a little tricky)
        /// </summary>
        private void ComputeResultSafe()
        {
            // some computation here...
            result2 = 10;
            Thread.MemoryBarrier();  // Ensures result2 will always be assigned before complete2 (prevents instruction reordering)
            complete2 = true;
            Thread.MemoryBarrier();  // Ensures new value of complete2 will be available to other threads (freshness)
        }

        /// <summary>
        /// General reccomendation:
        /// Do not use MemoryBarriers unless performance is 
        /// absolutely critical and every milisecond matters. 
        /// Simple locking is more comprehensive and much easier to use
        /// </summary>
        private void WriteResultSafe()
        {
            Thread.MemoryBarrier();         // Ensures reading complete2 will return true if ComputeResultSafe has finished
            if (complete2)
            {
                Thread.MemoryBarrier();     // Ensures reading complete2 will return correct result (10)
                Console.WriteLine(result2);
            }
        }
    }
}
