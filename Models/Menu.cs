using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace RestaurantDesk.Models
{
    public class Menu
    {
        public long? MenuId { get; set; }
        public string Name { get; set; }
        public string MenuCode { get; set; }
        public decimal Price { get; set; }
        public string ServingType { get; set; }
        public string? Category { get; set; }
        [BooleanFalseValues("N")]
        [BooleanTrueValues("Y")]
        public bool IsVeg { get; set; }
        public int? Quantity { get; set; }
        public string? CombineName { get; set; }
    }
}
