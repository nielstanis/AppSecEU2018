using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;

namespace MVCApp.Pages.RazorOrder
{
    public class IndexModel : PageModel
    {
        private readonly MVCApp.Data.OrderDataContext _context;

        public IndexModel(MVCApp.Data.OrderDataContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders.ToListAsync();
        }
    }
}
