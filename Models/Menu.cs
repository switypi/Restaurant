﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesk.Models
{
    public class Menu
    {
        public long MenuId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public int Quantity { get; set; }

        public string CombineName { get; set; }
    }
}