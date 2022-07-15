namespace SimpleBankWeb.ViewModels
{
    public enum AccountType
    {
        Current,
        Saving,
        SilverCredit,
        GoldCredit,
        PlatinumCredit
    }
    public class CreateCustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double InitialDeposit { get; set; }

        public AccountType AccountType { get; set; }

    }
}
