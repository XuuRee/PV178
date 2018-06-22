using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Signaling
{
    /// <summary>
    /// AutoResetEvent lets exactly one thread 
    /// to wake from the waiting state. You can
    /// picture AutoresetEvent as a ticket turnstil.
    /// </summary>
    internal class AutoResetEventDemo
    {
        /// <summary>
        /// AutoResetEvent maintains a boolean variable in memory. 
        /// If the boolean variable is false then it blocks the thread
        /// and and when the boolean variable is set to true it unblocks 
        /// the thread. Passing true to constructor acts like calling Set()
        /// method immediately after autoResetEvent instance creation.
        /// </summary>
        private readonly  AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        private BigInteger computedResult;

        internal void RunAutoResetEventDemo()
        {
            RunWaitOneAndSetDemo();

            // Resetting is done automatically :)
            // autoResetEvent.Reset();

            RunWaitOneAndSetDemo();
        }

        private void RunWaitOneAndSetDemo()
        {
            Task.Run(() => PerformComputation());

            // Put the current thread into waiting state until it receives the signal
            autoResetEvent.WaitOne();

            // waiting can also be limited by time interval
            // autoResetEvent.WaitOne(TimeSpan.FromSeconds(3));

            // Thread got the signal
            Console.WriteLine($"Just computed: {computedResult}");
            Console.ReadKey();
        }

        private void PerformComputation()
        {
            computedResult = Tasks.Factorial.ComputeBigFactorial(100);

            // Signal factorial computation is now complete
            autoResetEvent.Set();

            // Do some other work...
            Thread.Sleep(5000);
        }
    }
}
