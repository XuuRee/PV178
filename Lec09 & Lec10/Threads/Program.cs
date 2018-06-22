using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    class Program
    {
        private static bool done;
        private static readonly object locker = new object();

        static void Main(string[] args)
        {
            BasicThreadTest1();
            BasicThreadTest2();
            ThreadSafetyProblemTest1();
            ThreadSafetyProblemTest2();
            ForegroundVsBackgroundThreadTest();
            ThreadExceptionHandlingProblemTest();
            CountDownTest();
        }

        private static void BasicThreadTest1()
        {
            Thread t1 = new Thread(WriteA);
            t1.Start();

            new Thread(() => Console.Write("O")).Start();

            for (int i = 0; i < 100; i++)
            {
                Console.Write("X");
            }

            t1.Join();
            Console.WriteLine();
        }

        private static void WriteA()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("A");
            }
            Thread.Sleep(1000);
        }
        
        private static void BasicThreadTest2()
        {
            var queue = new Queue<int>();
            var t = new Thread(AddPeople);
            t.Start(queue);
            t.Join();
            Console.WriteLine(queue.Dequeue());
        }

        private static void AddPeople(object item)
        {
            var queue = (Queue<int>) item;
            for (var i = 1; i < 10; i++)
            {
                queue.Enqueue(i);
            }
        }

        private static void ThreadSafetyProblemTest1()
        {
            new Thread(Go).Start();
            Go();
            //fixed problem
            done = false;
            new Thread(FixedGo).Start();
            FixedGo();
        }

        private static void Go()
        {
            if (!done)
            {
                done = true;
                Console.WriteLine("Done");
            }
        }

        private static void FixedGo()
        {
            lock (locker)
            {
                if (!done)
                {
                    done = true;
                    Console.WriteLine("I am done");
                }
            }
        }

        private static void ThreadSafetyProblemTest2()
        {
            for (int i = 0; i < 10; i++)
            {
                new Thread(() => Console.Write(i)).Start();
            }
            Console.WriteLine();
            //fixed problem
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                new Thread(() => Console.Write(temp)).Start();
            }
            Console.WriteLine();
        }

        private static void ForegroundVsBackgroundThreadTest()
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("Write something");
                Console.ReadLine();
            })
            { IsBackground = true };
            t.Start();

            Thread.Sleep(500);
            Console.WriteLine("Too late, ha ha ha!");
        }

        private static void ThreadExceptionHandlingProblemTest()
        {
            // wrong
            /*
            try
            {
                new Thread(BadMethod).Start();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Exception!");
            }*/
            new Thread(BetterMethod).Start();
        }

        private static void BadMethod()
        {
            throw new NullReferenceException();
        }

        private static void BetterMethod()
        {
            try
            {
                Thread.Sleep(200);
                throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Exception!");
            }
        }

        /// <summary>
        /// Samostatna prace:
        /// 
        /// Úkolem bude napsat program countdown, tedy nějaký odpočet.
        /// Countdown bude na začátku mít počet tiků, a rozmezí mezi tiky v sekundách.
        /// Za každý tik se zavolá funkce, specifikovaná v konstruktoru.
        /// Na konci se zavolá jiná funkce, také specifikovaná v konstruktoru.
        /// Countdown poběží na samostatném vláknu.
        /// </summary>
        private static void CountDownTest()
        {
            var c = new Solution(3, 5, () => { Console.WriteLine("End"); }, () => { Console.WriteLine("Tick"); });
            c.Start();
        }
    }



    /* Credits:
     * 
     * Following materials are partly based 
     * on Joseph Albahari Parallel Programming:
     * see: http://www.albahari.com/threading/ 
     * and articles from http://dotnetpattern.com
     */
}
