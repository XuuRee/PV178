using System;
using System.Threading.Tasks;

namespace Synchronization
{
    internal static class Solution02
    {
        /// <summary>
        /// Samostatná práce:
        /// 
        /// Implementace nize uvedene metody PerformTransferTo
        /// (viz dokumentace). Pri implementaci si dejte pozor
        /// na mozny deadlock pri soubezne manipulaci s ucty.
        /// </summary>
        internal static void TestAccountTransactions()
        {
            const int iterationsCount = 1000;
            var johnsAccount = new Account(1, "John", 100);
            var daisysAccount = new Account(2, "Daisy", 200);

            Task.WaitAll(
                Task.Run(() =>
                {
                    for (var i = 0; i < iterationsCount; i++)
                    {
                        johnsAccount.PerformTransferTo(daisysAccount, 5);
                    }
                }),
                Task.Run(() =>
                {
                    for (var i = 0; i < iterationsCount; i++)
                    {
                        johnsAccount.PerformTransferTo(daisysAccount, 25);
                    }
                }),
                Task.Run(() =>
                {
                    for (var i = 0; i < iterationsCount; i++)
                    {
                        daisysAccount.PerformTransferTo(johnsAccount, 10);
                    }
                }),
                Task.Run(() =>
                {
                    for (var i = 0; i < iterationsCount; i++)
                    {
                        daisysAccount.PerformTransferTo(johnsAccount, 20);
                    }
                }));

            Console.WriteLine($"John's balance: {johnsAccount.Balance} $");
            Console.WriteLine($"Daisy's balance: {daisysAccount.Balance} $");
            Console.ReadKey();
        }

        /// <summary>
        /// Metoda (vlaknove bezpecne) prevede castku z jednoho uctu na druhy
        /// </summary>
        /// <param name="fromAccount">Ucet, z ktereho ma byt uskutecnena platba</param>
        /// <param name="toAccount">Ucet, na ktery ma prijit platba</param>
        /// <param name="amount">Zaplacena castka</param>
        internal static void PerformTransferTo(this Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount == null || toAccount == null || fromAccount.AccountId == toAccount.AccountId)
            {
                throw new ArgumentException("Invalid argument");
            }

            var firstLock = fromAccount.AccountId < toAccount.AccountId ? fromAccount : toAccount;
            var secondLock = fromAccount.AccountId > toAccount.AccountId ? fromAccount : toAccount;

            lock (firstLock)
            {
                lock (secondLock)
                {
                    fromAccount.PerformPayment(amount);
                    toAccount.ReceivePayment(amount);
                }
            }
        }

    }
}
