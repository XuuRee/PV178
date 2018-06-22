using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLocalStorage
{
    /// <summary>
    /// ThreadLocal provides thread local storage for static as well as instance fields and local variables.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Thread local storage can be given Func parameter (valueFactory) for creating the default value
        /// </summary>
        private static readonly ThreadLocal<int> Temperature = new ThreadLocal<int>(() => 24);

        static void Main(string[] args)
        {
            RunStaticFieldDemo();
            RunCapturedLocalVariableDemo();
        }

        private static void RunStaticFieldDemo()
        {
            Task.Run(() =>
            {
                // valueFactory is lazily evaluated at first call (for given thread)
                Console.WriteLine($"Default value of temperature: {Temperature.Value}");
                Temperature.Value = Temperature.Value + 3;
                Console.WriteLine($"Temperature after adjustment: {Temperature.Value}");
                // do not forget to call .Dispose() in case of using ThreadLocal for type implementing IDisposable
            });
            Task.Run(() =>
            {
                Thread.Sleep(250);
                Console.WriteLine($"Default value of temperature: {Temperature.Value}");
                Temperature.Value = Temperature.Value - 3;
                Console.WriteLine($"Temperature after adjustment: {Temperature.Value}");
            });
            Console.ReadKey();
        }

        private static void RunCapturedLocalVariableDemo()
        {
            var localRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            Task.Run(() => Console.WriteLine($"Random number from first thread: {localRandom.Value.Next(100)}"));
            Task.Run(() => Console.WriteLine($"Random number from second thread: {localRandom.Value.Next(100)}"));
            Console.ReadKey();
        }
    }
}
