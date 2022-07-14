using SimpleBankWeb.Business.EnumTypes;
using SimpleBankWeb.Business.Models;

namespace SimpleBankWeb.Business.Services
{
    public class TestService
    {

        public void Test()
        {

            

            var withdraw = new Transaction(TransactionType.Credit, "2002001" ,10000, "some withdraw");
            



            Console.WriteLine(withdraw.Amount);
        }
    }
}
