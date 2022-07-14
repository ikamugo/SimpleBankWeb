using SimpleBankWeb.Business.EnumTypes;

namespace SimpleBankWeb.Business.Models;

public class CurrentAccount : Account
{
    private const double _minimumBalance = 20000;
    private const double _monthlyCharge = 3000;
    private const double _dailyTransactionLimit = 80000;

    public CurrentAccount(string number, string name, double initialDeposit) 
        : base(number, name, initialDeposit)
    {
        if(initialDeposit < _minimumBalance)
            throw new ArgumentOutOfRangeException(nameof(initialDeposit));
    }

    public override void MonthlyAccountProcess()
    {
        var monthlyCharge = Transaction.Debit(this.Number, _monthlyCharge, "Monthly account fees");
        Transactions.Add(monthlyCharge);
    }

    public override void Withdraw(double amount, string description)
    {
        //check minimum balance
        if (GetBalance() - amount < _minimumBalance)
            throw new Exception("Insufficient funds to complete the transaction");

        
        // get the total days transactions
        double todaysTransactions = 0;
        foreach(var transaction in Transactions)
        {
            //check if transaction is withdraw for today
            if (transaction.Type == TransactionType.Withdraw && transaction.Timestamp.Date == DateTime.Now.Date)
                todaysTransactions = todaysTransactions + transaction.Amount;
        }

        //check daily withdraw limit
        if (todaysTransactions > _dailyTransactionLimit)
            throw new Exception("Daily Transaction Limit exceeded");


        var withdraw = Transaction.Withdraw(this.Number, amount, description);
        Transactions.Add(withdraw);
    }
}
