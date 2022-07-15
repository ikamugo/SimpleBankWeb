using System.ComponentModel.DataAnnotations;

namespace SimpleBankWeb.Business.Models
{
    public abstract class Account
    {
        [Key]
        public string Number { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        protected Account() { }
        public Account(string number, Customer customer, double initialDeposit)
        {
            // initialize members
            Number=number;
            Customer = customer;
            Transactions=new List<Transaction>();

            //make initial deposit
            Deposit(initialDeposit, "Initial deposit");

        }

        public double GetBalance()
        {
            var balance = 0.0;
            foreach(var transaction in Transactions)
            {
                balance = balance + transaction.Amount;
            }

            return balance;
        }

        public void Deposit(double amount, string description)
        {
            var deposit = Transaction.Deposit(this.Number, amount, description);
            Transactions.Add(deposit);
        }

        public abstract void Withdraw(double amount, string description);

        public abstract void PerformMonthEndProcessing();

                
    }
}
