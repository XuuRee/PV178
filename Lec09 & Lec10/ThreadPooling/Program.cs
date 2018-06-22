using System;
using System.Threading;

namespace ThreadPooling
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPoolingTest();
        }

        private static void ThreadPoolingTest()
        {
            ThreadPool.QueueUserWorkItem(Write, 123);
            ThreadPool.QueueUserWorkItem(Write);

            Thread.Sleep(1000);
        }

        private static void Write(object data)
        {
            Console.WriteLine($"I write this {data}");
        }
    }
}
