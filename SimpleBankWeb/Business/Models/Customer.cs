using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBankWeb.Business.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
    }
}
