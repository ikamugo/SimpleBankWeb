namespace SimpleBankWeb.Business.Models
{
    public abstract class Account
    {
        public Account(string number, string name, double initialDeposit)
        {
            // initialize members
            Number=number;
            Name=name;
            Transactions=new List<Transaction>();

            //make initial deposit
            Deposit(initialDeposit, "Initial deposit");
            
        }

        public string Number { get; private set; }
        public string Name { get; private set; }
        public List<Transaction> Transactions { get; private set; }

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

        public abstract void MonthlyAccountProcess();

                
    }
}
