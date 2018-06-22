using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Threads;

namespace Tasks
{
    /// <summary>
    /// In modern days, tasks are the way to go for parallel processing.
    /// When compared to threads, they are easier to use, provides support 
    /// for continuation, exception handling, hierarchy (usefull for more 
    /// complex parallel computations) and so on... 
    /// Internally, task are run via Threadpool, which means they are far more 
    /// effective than creating (and GC) own threads as seen in the Threads_01. 
    /// Therefore using threads directly is considered as a bad practice and 
    /// should be avoided.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            LaunchTask();
            //AttachChildToParentTask();
            //HandleExceptionsWithinTask();
            //CancelTask();
            //ContinueWithTask();

            // Samostatna prace:
            // 
            // I.    Vytvorte Task s nazvem parent, v ramci nehoz dojde k vypoctu 
            //       se bude zpracovavat field numbers a konstanta LoremIpsum (viz)
            //       body II. a III.)
            // 
            // II.   Uvnitr tasku parent vytvorte vnoreny Task evenNumbersProductTask,
            //       ktery pro vsechna suda cisla z numbers spocita jejich soucin.
            // 
            // III.  Uvnitr tasku parent vytvorte vnoreny Task occurancesInLoremIpsumTask,
            //       ktery spocita vyskyt vsech whitespace znaku v retezci LoremIpsum, 
            //       v pripade, ze jejich pocet bude ostre mensi nez jedna, nebo vetsi 
            //       nez 150, vyhodi vhodnou vyjimku.
            //       
            //       a) Pripadnou vyhozenou vyjimku podminene zpracujte v navazujicim Tasku
            //          (staci pouze vypsat chybovou zpravu a vratit hodnotu 1).
            // 
            //       b) V pripade, ze occurancesInLoremIpsumTask je uspesne dokoncen,
            //          vypoctete z jeho vysledku faktorial v navazujicim tasku
            // 
            // IV.   Jako vysledek parent Tasku vratte podil vysledku evenNumbersProductTask
            //       a prislusneho vysledku tasku navazujiciho na occurancesInLoremIpsumTask,
            //       (tedy pouzijete hodnotu bud z varianty a), nebo b) v zavislosti na tom,
            //       zda doslo k vyhozeni vyjimky ci nikoliv)
            // 
            // V.    Na vystup vypiste pocet cislic z vysledku paren tasku 
            // 
            // VI.   Zkuste si zmenit horni hranici vyskytu whitespace znaku v retezci 
            //       LoremIpsum ze 150 na 100 a upraveny kod otestujte     
            //
            // new Solution().Compute();
        }

        /// <summary>
        /// Common ways of creating & launching tasks
        /// </summary>
        private static void LaunchTask()
        {
            // Create & start a new task
            const string message = "Executing task with ID: ";
            var task = new Task(() => Console.WriteLine(message + Task.CurrentId));
            // Starting a so called "cold task" is not very common
            task.Start();

            // Easy (and prefered) way to start a new task
            const int factorial = 3;
            var startedTask = Task.Run(() => Factorial.ComputeSmallFactorial(factorial));
            // Can be ommited because we are accesing result on the next line
            startedTask.Wait();
            var factorialOfThree = startedTask.Result;

            // This usage can potentially lead to undesired side effects
            var startedTaskViaFactoryBad = Task.Factory.StartNew(() => Factorial.ComputeSmallFactorial(3));

            // Correct way to start new task using a Task.Factory
            var startedTaskViaFactoryGood = Task.Factory.StartNew(() => Factorial.ComputeSmallFactorial(3), CancellationToken.None,
                TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);

            Task.WaitAll(startedTaskViaFactoryBad, startedTaskViaFactoryGood);

            Task.Run(() => Console.WriteLine($"Are tasks run in background? {Thread.CurrentThread.IsBackground}"))
                .Wait();

            Console.WriteLine("Press any key to start countdown." + Environment.NewLine);
            Console.ReadKey();
            new Threads.Solution(5, 1, () => Console.WriteLine("End"), () => Console.WriteLine("Tick"))
                .StartWithTask();
            
            Console.ReadKey();
        }    

        /// <summary>
        /// Task can be attached to parent
        /// </summary>
        private static void AttachChildToParentTask()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() => // Detached task
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Detached task ended");
                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

                Task.Factory.StartNew(() => // Attached task
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("AttachedToParent task ended");
                }, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);

            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            Console.WriteLine("Waiting for parent task:" + Environment.NewLine);
            parent.Wait();
            Console.WriteLine("Parent task ended, press any key to continue");
            Console.ReadKey();

            // What will be the output if parent task had TaskCreationOptions.DenyChildAttach specified
        }

        /// <summary>
        /// Task exception handling specifics...
        /// </summary>
        private static void HandleExceptionsWithinTask()
        {
            // Results in UnobservedTaskException, program will continue
            Task.Run(() => { throw new NotImplementedException(); });

            // Unhandled exception when waiting for the task will terminate the program
            //Task.Run(() => { throw new NotImplementedException(); }).Wait();
           
            try
            {
                Task.Run(() => { throw new NotImplementedException(); } ).Wait();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("This catch is not working :(");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("But this one is :)");
                Console.WriteLine($"Caught a {ex.InnerExceptions.First()?.GetType()}");
            }
        }

        /// <summary>
        /// Task can be cancelled (in co-ordinated manner) by using cancellation token
        /// </summary>
        private static void CancelTask()
        {
            var cancelSource = new CancellationTokenSource();
            var token = cancelSource.Token;

            Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Canceling task...");
                        break;
                    }
                    Console.WriteLine($"Factorial of {i} is: {Factorial.ComputeBigFactorial(i)}");
                }
            }, token, TaskCreationOptions.None, TaskScheduler.Default);
            Thread.Sleep(20);
            cancelSource.Cancel();

            Console.WriteLine("Task finished, press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Task can have continuations when finished
        /// </summary>
        private static void ContinueWithTask()
        {
            // Simple use of task continuation
            Task.Run(() => Factorial.ComputeSmallFactorial(3))
            .ContinueWith(task =>
            {
                Console.WriteLine($"Value computed in previous task: {task.Result}");
            }).Wait();

            // Continuations can be alternative to standard exception handling
            Task.Run(() =>
            {
                throw new NotSupportedException();
                return Factorial.ComputeSmallFactorial(3);
            })            
            .ContinueWith(task =>
            {
                Console.WriteLine($"Task has thrown exception: {task?.Exception?.GetType()}");
            }, TaskContinuationOptions.OnlyOnFaulted).Wait();

            // In case there should be additional processing after possible exception, 
            // we simply use the returned Task from ContinueWith(...)
            var taskWithException = Task.Run(() =>
            {
                throw new NotSupportedException();
                return Factorial.ComputeSmallFactorial(3);
            });
            var handledTask = taskWithException.ContinueWith(task =>
            {
                Console.WriteLine($"Again, task has thrown exception: {task?.Exception?.GetType()}");
            }, TaskContinuationOptions.OnlyOnFaulted);
            handledTask.ContinueWith(ant => 
                Console.WriteLine("Proceeding..."), TaskContinuationOptions.NotOnCanceled);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Continuations can also be nested within parent task
            var parent = Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() => // Attached task
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("AttachedToParent task ended");
                }, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default)
                .ContinueWith(task =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Continuation for attachedToParent task ended");
                }, TaskContinuationOptions.AttachedToParent);

            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            Console.WriteLine("Waiting for parent task:" + Environment.NewLine);
            parent.Wait();
            Console.WriteLine("Parent task ended, press any key to continue");
            Console.ReadKey();

            // There can be even more (conditional) continuations
            Task.Run(() => 2)
              .ContinueWith(task => task.Result * 2, TaskContinuationOptions.NotOnCanceled)
              .ContinueWith(task => Math.Sqrt(task.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
              .ContinueWith(task => Console.WriteLine(task.Result))
              .Wait();
            
            // Multiple continuations are also possible
            var firstTask = Task.Run(() => Factorial.ComputeBigFactorial(3));
            var secondTask = Task.Run(() => Factorial.ComputeBigFactorial(42));
            var result = Task<BigInteger>.Factory
                .ContinueWhenAll(
                    new[] { firstTask, secondTask }, 
                    tasks => tasks.First().Result + tasks.Last().Result)
                    .Result;
            Console.WriteLine($"The result is: {result}");

            // In this case, both continuations will be executed simultaneously, 
            // so the output is nondeterministic.
            var antecedentTask = Task.Factory.StartNew(() => Thread.Sleep(1000));
            antecedentTask.ContinueWith(ant => Console.Write("X"));
            antecedentTask.ContinueWith(ant => Console.Write("Y"));
            Console.ReadKey();
        }
    }
}
