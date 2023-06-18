using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RestaurantDesk.Models
{
    public partial class Table: ObservableObject
    {
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public long OrderId { get;set; }

        public string Tag { get; set; }
        public TableTypeEnum TableType { get; set; }


        [ObservableProperty]
        private bool isBusy;
        [ObservableProperty]
        private bool isBooked;
    }
}
