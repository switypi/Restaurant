﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RestaurantDesk.Models
{
    
    public  class Customer
    {
        public long CustomerId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public DateTime LastVisit { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }

    }

}
