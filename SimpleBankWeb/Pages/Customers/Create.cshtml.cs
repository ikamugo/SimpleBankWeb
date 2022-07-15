using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBankWeb.Business.Models;
using SimpleBankWeb.Data;
using SimpleBankWeb.ViewModels;

namespace SimpleBankWeb.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly SimpleBankWeb.Data.ApplicationDbContext _context;

        public CreateModel(SimpleBankWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateCustomerModel CreateCustomerModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || CreateCustomerModel == null)
            {
                return Page();
            }

            var customer = new Customer
            {
                DateOfBirth = CreateCustomerModel.DateOfBirth,
                Name = CreateCustomerModel.Name,
                Gender = CreateCustomerModel.Gender,
                Phone = CreateCustomerModel.Phone,
                Location = CreateCustomerModel.Location
            };

            var accNumber = DateTime.Now.ToString("ddMMyyyyHHmmss");
            Account account;

            try
            {
                switch (CreateCustomerModel.AccountType)
                {
                    case AccountType.Current:
                        account = new CurrentAccount(accNumber, customer, CreateCustomerModel.InitialDeposit);
                        break;
                    case AccountType.Saving:
                        account = new SavingAccount(accNumber, customer, CreateCustomerModel.InitialDeposit);
                        break;
                    case AccountType.SilverCredit:
                        account = CreditAccount.Silver(accNumber, customer, CreateCustomerModel.InitialDeposit);
                        break;
                    case AccountType.GoldCredit:
                        account = CreditAccount.Gold(accNumber, customer, CreateCustomerModel.InitialDeposit);
                        break;
                    case AccountType.PlatinumCredit:
                        account = CreditAccount.Platinum(accNumber, customer, CreateCustomerModel.InitialDeposit);
                        break;
                    default:
                        throw new Exception("Invalid account selection");
                }

                _context.Accounts.Add(account);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }

            

            return RedirectToPage("./Index");
        }
    }
}
