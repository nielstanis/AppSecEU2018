using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Data
{
    public class Order
    {
        public int ID { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }
    }
}
