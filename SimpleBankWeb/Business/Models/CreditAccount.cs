using SimpleBankWeb.Business.EnumTypes;

namespace SimpleBankWeb.Business.Models
{
    public class CreditAccount : Account
    {
        private const double _interestRate = 0.05;
        private const double _overdraftFees = 10000;

        public CreditType Type { get; private set; }

        public double CreditLimit
        {
            get
            {
                if (Type == CreditType.Platinum)
                    return 10000000;
                else if (Type == CreditType.Gold)
                    return 5000000;
                else
                    return 1000000;
            }
        }

        private CreditAccount(CreditType type, string number, string name, double initialDeposit)
            : base(number, name, initialDeposit)
        {
            Type = type;

            if(initialDeposit < CreditLimit)
                throw new ArgumentOutOfRangeException(nameof(initialDeposit));
        }



        public override void PerformMonthEndProcessing()
        {
            var balance = GetBalance();
            if (balance < 0)
            {
                var interestFee = Math.Abs(balance) * _interestRate;
                Transactions.Add(Transaction.Debit(Number, interestFee, "Interest fees on credit"));
            }
        }


        public override void Withdraw(double amount, string description)
        {
            //check if transaction will result in negative balance, apply overdraft fees
            var withdrawFees = GetBalance() - amount < 0 ? _overdraftFees : 0;

            if (GetBalance() + CreditLimit < amount + withdrawFees)
                throw new InvalidOperationException("Insufficient funds to complete the transaction");

            //add the withdraw
            Transactions.Add(Transaction.Withdraw(Number, amount, description));

            //add withdraw fees if applicable
            if(withdrawFees > 0)
            {
                Transactions.Add(Transaction.Debit(Number, _overdraftFees, "Account overdraft fees"));
            }

        }

        public static CreditAccount Silver(string number, string name, double minimumBalance)
        {
            return new CreditAccount(CreditType.Silver, number, name, minimumBalance);
        }

        public static CreditAccount Gold(string number, string name, double minimumBalance)
        {
            return new CreditAccount(CreditType.Gold, number, name, minimumBalance);
        }

        public static CreditAccount Platinum(string number, string name, double minimumBalance)
        {
            return new CreditAccount(CreditType.Platinum, number, name, minimumBalance);
        }
    }
}
