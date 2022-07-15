namespace SimpleBankWeb.Business.Models
{
    public class SavingAccount : Account
    {
        private const double _interestRate = 0.02;
        private const int _maxFreeWithdraws = 2;
        private const double _minimumBalance = 100000;
        private const double _extraWithdrawCharge = 5000;

       
        private SavingAccount() { }
        public SavingAccount(string number, Customer customer, double initialDeposit) 
            : base(number, customer, initialDeposit)
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
            // get the number of withdraws in the current month
            int numOfWithdraws = 0;
            foreach (var transaction in this.Transactions)
            {
                if (transaction.Timestamp.Month == DateTime.Now.Month && transaction.Timestamp.Year == DateTime.Now.Year)
                    numOfWithdraws++;
            }

            // determine if withdraw charge is required
            var withdrawCharge = numOfWithdraws > _maxFreeWithdraws ? _extraWithdrawCharge : 0.0;

            //var withdrawCharge = 0.0;
            //if (numOfWithdraws > _maxFreeWithdraws)
            //{
            //    withdrawCharge = _extraWithdrawCharge;
            //}


            //check minimum balance
            if (GetBalance() - amount - withdrawCharge < _minimumBalance)
                throw new Exception("Insufficient funds to complete the transaction");

            
            // add the withdraw transaction
            Transactions.Add(Transaction.Withdraw(Number, amount, description));

            // add extra with fees if free transaction limit is exceeded
            if (withdrawCharge > 0)
                Transactions.Add(Transaction.Debit(Number, withdrawCharge, description));

        }
    }
}
