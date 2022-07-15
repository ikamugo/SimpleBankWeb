using SimpleBankWeb.Business.EnumTypes;

namespace SimpleBankWeb.Business.Models
{
    public class Transaction
    {
        
        public int Id { get; set; }
        public string AccountNumber { get; private set; }
        public DateTime Timestamp { get; private set; }
        public double Amount { get; private set; }
        public string Description { get; private set; }
        public TransactionType Type { get; private set; }

        private Transaction() { }
        private Transaction(TransactionType type, string accNumber, double amount, string description)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (type == TransactionType.Withdraw || type == TransactionType.Debit)
            {
                amount = -amount;
            }

            Timestamp=DateTime.UtcNow;
            AccountNumber=accNumber;
            Amount=amount;
            Description=description;
            Type = type;
        }

        public static Transaction Credit(string accountNumber, double amount, string description)
        {
            return new Transaction(TransactionType.Credit, accountNumber, amount, description);

        }

        public static Transaction Debit(string accountNumber, double amount, string description)
        {
            return new Transaction(TransactionType.Debit, accountNumber, amount, description);

        }

        public static Transaction Deposit(string accountNumber, double amount, string description)
        {
            return new Transaction(TransactionType.Deposit, accountNumber, amount, description);
        }

        public static Transaction Withdraw(string accountNumber, double amount, string description)
        {
            return new Transaction(TransactionType.Withdraw, accountNumber, amount, description);

        }

    }
}
