using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlinqTest();
            ParallelTest();
            BeatRecordTest();
        }

        private static void PlinqTest()
        {
            var source = Enumerable.Range(0, 3000000).ToList();

            var normalQuery1 = source.Where(n => Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0));
            var plinqQuery1 = source.AsParallel().Where(n => Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0));

            var sw = Stopwatch.StartNew();
            foreach (var n in normalQuery1) { }
            sw.Stop();

            Console.WriteLine($"Total LINQ query time: {sw.ElapsedMilliseconds} ms");

            sw = Stopwatch.StartNew();
            foreach (var n in plinqQuery1) { }
            sw.Stop();

            Console.WriteLine($"Total PLINQ query time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("Great! :)");

            var normalQuery2 = source.Where(n => n % 7 == 0);
            var plinqQuery2 = source.AsParallel().Where(n => n % 7 == 0);

            sw = Stopwatch.StartNew();
            foreach (var n in normalQuery2) { }
            sw.Stop();

            Console.WriteLine($"Total LINQ query time: {sw.ElapsedMilliseconds} ms");

            sw = Stopwatch.StartNew();
            foreach (var n in plinqQuery2) { }
            sw.Stop();

            Console.WriteLine($"Total PLINQ query time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("Not so great :(");

            // PLINQ also supports cancellation via token (as seen in tasks demo)
            var cancelSource = new CancellationTokenSource();
            var plinqQuery3 = source.AsParallel()
                .WithCancellation(cancelSource.Token)
                .Where(n => Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0));

            Task.Run(() =>
            {
                Thread.Sleep(250);
                cancelSource.Cancel();
                // Parallel query now shortly waits untill each worker thread finishes processing current element.
            });
            try
            {
                plinqQuery3.ToList(); // Lets fire up the query
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Query canceled");
                Console.ReadKey();
            }
        }

        private static void ParallelTest()
        {
            Parallel.Invoke(
                () => new WebClient().DownloadFile("http://www.google.com", "google.html"),
                () => new WebClient().DownloadFile("http://www.is.muni.cz", "muni.html"));

            // Passing cancelation token or limiting the number of threads - both can be done by ParallelOptions
            var cancelSource = new CancellationTokenSource();
            Parallel.Invoke(new ParallelOptions {CancellationToken = cancelSource.Token, MaxDegreeOfParallelism = 2},
                () => new WebClient().DownloadFile("http://www.google.com", "google.html"),
                () => new WebClient().DownloadFile("http://www.is.muni.cz", "muni.html"),
                () => new WebClient().DownloadFile("https://www.microsoft.com/cs-cz/", "microsoft.html"),
                () => new WebClient().DownloadFile("https://www.csfd.cz/", "csfd.html"));

           
            Parallel.ForEach("PV178 is great", (c, loopstate) =>
            {
                if (c.Equals('8'))
                {
                    loopstate.Break();
                }
                else
                {
                    Console.Write(c);
                }
            });
            Console.WriteLine();



            // Why the output wount be equal to 500K?
            var even = 0;
            Parallel.For(0, 1000_000, i =>
            {
                if (i % 2 == 0)
                {
                    even++;
                }
            });
            Console.WriteLine($"Even numbers (from 1M): {even}");
            Console.ReadKey();

            // Why the even will be very probably equal to 50?
            even = 0;
            Parallel.For(0, 100, i =>
            {
                if (i % 2 == 0)
                {
                    even++;
                }
            });
            Console.WriteLine($"Even numbers (from 100): {even}");
            Console.ReadKey();

            var locker = new object();
            var overallTotal = new BigInteger(1);
            Parallel.For(1, 10_000,
                // Initialize local value (for current thread)
                () => new BigInteger(1),
                // Modify and return localValue after processing single element from collection    
                (i, state, localTotal) => localTotal * (BigInteger)Math.Pow(i,2),    
                // Collating function (adds local values to overall)  
                localTotal =>                                                         
                {
                    lock (locker)
                    {
                        overallTotal *= localTotal;
                    }
                });
            Console.WriteLine($"Product of all squared numbers from 1 to 10K is: {overallTotal}");
            Console.ReadKey();
        }

        /// <summary>
        /// Napíšte PLINQ dotaz, ktorý bude trvať kratšie ako tento LINQ dotaz na
        /// kolekcií table a vráti ten istý výsledok.
        /// 
        /// Pre cviciacich: Uloha na to, aby si studenti uvedomili, ze PLINQ 
        /// nezachovava poradie pri LINQ metodach, ktore ho normalne zachovavaju.
        /// Nech skusia prist na to samy, ze tam treba AsOrdered
        /// </summary>
        private static void BeatRecordTest()
        {
            //crazy init
            MD5 md5Hasher = MD5.Create();
            var table = new List<Tuple<int, int>>();
            for (int i = 0; i < 10000; i++)
            {
                table.Add(
                    new Tuple<int, int>(
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(i.ToString())), 0) % 100),
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes((i + 2798).ToString())), 0) % 100)));
            }

            var sw = Stopwatch.StartNew();

            var linqQuery = table
                .Where(n => Enumerable.Range(2, (int) Math.Sqrt(n.Item1)).All(i => n.Item1 % i > 0))
                .Take(1000)
                .ToList();
            
            sw.Stop();
            var linqTime = sw.ElapsedMilliseconds;

            sw = Stopwatch.StartNew();
            var plinqQuery = table
                .AsParallel()
                .AsOrdered()
                .Where(n => Enumerable.Range(2, (int) Math.Sqrt(n.Item1)).All(i => n.Item1 % i > 0))
                .Take(1000)
                .ToList();
            sw.Stop();
            var plinqTime = sw.ElapsedMilliseconds;

            if (!plinqQuery.SequenceEqual(linqQuery))
            {
                Console.WriteLine("They are not the same results!");
                return;
            }
            if (linqTime > plinqTime)
            {
                Console.WriteLine("Linq is faster!");
                return;
            }
            Console.WriteLine("Congratulations :)");
        }
    }
}
