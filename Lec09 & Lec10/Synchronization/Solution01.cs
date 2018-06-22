using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    // Samostatná práce:
    //
    // Implementace obsluhy pacientů v nemocnici.
    // V nemocnici máme 3 všeobecné doktory, u kterých 
    // je nedeterministické pořadí pacientů.
    // Každý se zdrží u doktora náhodný čas mezi 1000 až 2000 ms.
    //
    // Doporučený postup:
    // Nejdříve si zkuste implementaci provést pomocí monitoru. 
    // Následně se pak zkuste zamyslet, zda by bylo možné použít
    // nějaký vhodnější synchronizační konstrukt.
    // </summary>
    public class Solution01
    {
        private readonly SemaphoreSlim sem = new SemaphoreSlim(3);
        private readonly Random rnd = new Random();

        public void Process10Patients()
        {
            var patients = new List<Task>();

            for (var patientId = 1; patientId <= 10; patientId++)
            {
                var patientIdCopy = patientId;
                var patientTask = Task.Run(() => Enter(patientIdCopy));
                patients.Add(patientTask);
            }
            Task.WhenAll(patients).Wait();
        }

        private void Enter(int id)
        {
            Console.WriteLine(id + ". patient wants to enter");
            sem.Wait();
            Console.WriteLine(id + ". patient is in!");
            Thread.Sleep(rnd.Next(1000, 2001));
            Console.WriteLine(id + ". patient is fixed");
            sem.Release();
        }



        // Implementace pomocí monitoru

        //private readonly object lockObject = new object();
        //private const int MaxPatientsAllowed = 3;
        //private int actualPacientsCount;
        //private readonly Random rnd = new Random();
        //public void Process10Patients()
        //{
        //    for (var patientId = 1; patientId <= 10; patientId++)
        //    {
        //        var patientIdCopy = patientId;
        //        Task.Run(()=> Enter(patientIdCopy));
        //    }
        //}
        //private void Enter(int id)
        //{
        //    Console.WriteLine(id + ". wants to enter");
        //    while (true)
        //    {
        //        lock (lockObject)
        //        {
        //            if (actualPacientsCount < MaxPatientsAllowed)
        //            {
        //                actualPacientsCount++;
        //                break;
        //            }
        //        }
        //        Thread.Sleep(100);
        //    }
        //    Console.WriteLine(id + ". is in!");
        //    Thread.Sleep(rnd.Next(1000, 2001));
        //    Console.WriteLine(id + ". is fixed");
        //    lock (lockObject)
        //    {
        //        actualPacientsCount--;
        //    }
        //}
    }
}