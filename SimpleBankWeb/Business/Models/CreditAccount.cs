namespace SimpleBankWeb.Business.Models
{
    public class CreditAccount : Account
    {
        public CreditAccount(string number, string name, double minimumBalance) 
            : base(number, name, minimumBalance)
        {

        }

        public override void MonthlyAccountProcess()
        {
            throw new NotImplementedException();
        }

        public override void Withdraw(double amount, string description)
        {
            throw new NotImplementedException();
        }
    }
}
