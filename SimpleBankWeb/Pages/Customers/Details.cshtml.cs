using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBankWeb.Business.Models;
using SimpleBankWeb.Data;

namespace SimpleBankWeb.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly SimpleBankWeb.Data.ApplicationDbContext _context;

        public DetailsModel(SimpleBankWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; } = default!;
        public List<Transaction> Transactions { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
                
                var accNumber = _context.Accounts
                    .Where(a => a.CustomerId == customer.Id)
                    .First()
                    .Number;

                Transactions = _context.Transactions
                    .Where(t => t.AccountNumber == accNumber)
                    .ToList();
            }
            return Page();
        }
    }
}
