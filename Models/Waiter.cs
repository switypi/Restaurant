using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesk.Models
{
    public class Waiter
    {
        public long WaiterId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }

        public DateTime JoinDate { get; set; }
        public int? TableId { get; set; }
    }
}
