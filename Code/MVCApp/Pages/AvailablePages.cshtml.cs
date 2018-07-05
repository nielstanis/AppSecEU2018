using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Hosting;

namespace MVCApp.Pages
{
    public class AvailablePagesModel : PageModel
    {
        public ApplicationPartManager PartManager { get; private set; }
        public List<RazorCompiledItem> Items { get; set; }

        public AvailablePagesModel(ApplicationPartManager manager)
        {
            PartManager = manager;

        }
        public void OnGet()
        {
            var razorCompiled = PartManager.ApplicationParts.OfType<IRazorCompiledItemProvider>();
            Items = new List<RazorCompiledItem>();
            foreach (IRazorCompiledItemProvider item in razorCompiled)
            {
                Items.AddRange(item.CompiledItems);
            }
        }
    }
}