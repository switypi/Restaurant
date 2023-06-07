using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesk.Models
{
    public class Booking
    {
        public long BookingId { get; set; }
        public string TableNum { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public long? CustId { get; set; }
        public int? TableId { get; set; }
        public string CustomerName { get; set; }
        public int Pax { get; set; }
        public  string ArrivalTime { get; set; }
        public bool IsArrived { get; set; }
        

    }

    public class BookingDisplay
    {
        public long BookingId { get; set; }
        public string TableNum { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public int Pax { get; set; }
        public string ArrivalTime { get; set; }
        public bool IsArrived { get; set; }


    }
}
