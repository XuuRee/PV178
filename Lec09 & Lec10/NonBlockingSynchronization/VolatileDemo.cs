using System;
using System.Threading.Tasks;

namespace NonBlockingSynchronization
{
    /// <summary>
    /// "volatile" keyword (applied to field) tells the compiler 
    /// to generate an acquire-fence on every read (prevents other 
    /// reads/writes from being moved before the read), and a 
    /// release-fence on every write (prevents other reads/writes
    /// from being moved after the fence).
    /// In other words: except from write-read scenarios, it ensures
    /// the most up-to-date value is present in the targeted field 
    /// at all times.
    /// </summary>
    internal class VolatileDemo
    {
        private volatile int x, y;

        /// <summary>
        /// Volatile does not prevent from swapping reads and writes,
        /// so following demo can contain 0 for both temp1 and temp2
        /// variables due to the instruction reordering.
        /// Remark: writes swapping is prohibited by CLR.
        /// Reccomendation: avoid using volatile as its common source
        /// of errors and misunderstandings.
        /// </summary>
        internal void RunVolatileDemo()
        {
            Task.WaitAll(
                Task.Run(() =>
                {
                    x = 1; // Volatile write, basically a MemoryBarrier(); followed by assigment    
                    int temp1 = y; // Volatile read, basically a value read, MemoryBarrier(); and return of the value
                    Console.WriteLine($"{nameof(temp1)}= {temp1}");
                }),
                // Reads and writes can be swapped since no barrier is in the midle of volatile write and read
                Task.Run(() =>
                {
                    y = 1; 
                    int temp2 = x; 
                    Console.WriteLine($"{nameof(temp2)}= {temp2}");
                }));
            Console.ReadKey();
        }
    }
}
