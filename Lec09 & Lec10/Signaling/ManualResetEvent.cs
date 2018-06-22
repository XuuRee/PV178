using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Signaling
{
    /// <summary>
    /// Key differences between AutoResetEvent and 
    /// ManualResetEventSlim consist in behaviour,
    /// unlike AutoResetEvent, the ManualResetEventSlim
    /// will let any number of threads "through" between
    /// calling Set() and Reset() methods. 
    /// ManualResetEventSlim is also on average more 
    /// than 20x faster when compared to AutoResetEvent
    /// (considering the short wait scenarios).
    /// </summary>
    internal class ManualResetEventDemo
    {
        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        private BigInteger computedResult;

        internal void RunManualResetEventDemo()
        {
            Task.Run(() => PerformFactorialComputation(12));
            Task.Run(() => PerformFactorialComputation(18));

            // Show first set of computed factorials
            manualResetEvent.Set();
            // All waiting
            manualResetEvent.Reset();

            Console.ReadKey();
            // Show second set of computed factorials
            manualResetEvent.Set();

            Console.ReadKey();
        }

        private void PerformFactorialComputation(int n)
        {
            computedResult = Tasks.Factorial.ComputeBigFactorial(n);
            Console.WriteLine($"Just computed factorial of {n}: {computedResult}");

            manualResetEvent.WaitOne();

            computedResult = Tasks.Factorial.ComputeBigFactorial(n*2);
            Console.WriteLine($"Just computed factorial of {n*2}: {computedResult}");

            manualResetEvent.WaitOne();

            // Do some other work...
            Thread.Sleep(5000);
        }
    }
}
