namespace Synchronization
{
    public class Account
    {
        public readonly long AccountId;

        public decimal Balance { get; private set; }

        public string Owner { get; }

        public Account(long accountId, string owner, decimal initialBalance)
        {
            AccountId = accountId;
            Owner = owner;
            Balance = initialBalance;            
        }

        public void PerformPayment(decimal amount)
        {
            Balance -= amount;
        }

        public void ReceivePayment(decimal amount)
        {
            Balance += amount;
        }
    }
}
