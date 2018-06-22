namespace NonBlockingSynchronization
{
    /// <summary>
    /// Nonblocking synchronization is used to perform simple 
    /// operations (such as read, increment, ...) without ever 
    /// blocking, pausing, or waiting. Absence of locking brings
    /// greater efficiency since contended lock brings overhead 
    /// of a context switch and the latency of being descheduled.
    /// Notice this solution has enabled Optimize code option...
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var memmoryBarrierDemo = new MemoryBarrierDemo();
            memmoryBarrierDemo.PerformTest();

            new VolatileDemo().RunVolatileDemo();

            new InterlockedDemo().RunInterlockedDemo();
        }
    }
}
