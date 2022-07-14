namespace SimpleBankWeb.Business.Models
{
    public class SavingAccount : Account
    {
        private const double _interestRate = 0.02;
        private const int _maxFreeWithdraws = 3;
        private const double _minimumBalance = 100000;
        private const double _extraWithdrawCharge = 5000;

        public SavingAccount(string number, string name, double initialDeposit) 
            : base(number, name, initialDeposit)
        {
            if (initialDeposit < _minimumBalance)
                throw new ArgumentOutOfRangeException(nameof(initialDeposit));
        }

        public override void PerformMonthEndProcessing()
        {
            var interest = GetBalance() * _interestRate;
            var credit = Transaction.Credit(this.Number, interest, "Interest on account balance");
            this.Transactions.Add(credit);
        }

        public override void Withdraw(double amount, string description)
        {
            //check minimum balance
            if (GetBalance() - amount < _minimumBalance)
                throw new Exception("Insufficient funds to complete the transaction");


            // get the number of withdraws in the current month
            int numOfWithdraws = 0;
            foreach(var transaction in this.Transactions)
            {
                if(transaction.Timestamp.Month == DateTime.Now.Month && transaction.Timestamp.Year == DateTime.Now.Year)
                    numOfWithdraws++;
            }

            // add extra with fees if free transaction limit is exceeded
            if (numOfWithdraws > _maxFreeWithdraws)
                Transactions.Add(Transaction.Debit(Number, _extraWithdrawCharge, description));

            // add the withdraw transaction
            Transactions.Add(Transaction.Withdraw(Number, amount, description));
        }
    }
}
