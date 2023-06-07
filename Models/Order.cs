using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesk.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long? BillId { get; set; }
        public int? TableId { get; set; }
        
    }
}
