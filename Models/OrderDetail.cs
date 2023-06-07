using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesk.Models
{
    public class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public int MenuId { get; set; }
        public long? OrderId { get; set; }
        public int quantity { get; set; }
        public decimal Amount { get; set; } 
    }
}
