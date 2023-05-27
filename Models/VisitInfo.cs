using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantDesk.Models
{
    class VisitInfo
    {

        public int VisitId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsPastVisit { get; set; }
        public bool IsPresentVisit { get; set; }
        public bool IsFutureVisit { get; set;  }


        public VisitInfo()
        {

        }

        public VisitInfo(string name, string phone, string email, string note, DateTime dateTime, DateTime start, DateTime end)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Note = note;
            VisitDate = dateTime;
            Start = start;
            End = end;
        }

        
    }
}
