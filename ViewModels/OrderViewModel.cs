using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantDesk.Models;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Wpf.Ui.Common.Interfaces;

namespace RestaurantDesk.ViewModels
{
    public partial class OrderViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        

        [ObservableProperty]
        private IEnumerable<Menu>? menuItems;

        [ObservableProperty]
        private ObservableCollection<Menu>? selectedMenus;


        [ObservableProperty]
        private ObservableCollection<Booking> bookingList;

        [ObservableProperty]
        private Menu selectedMenuItem;
        [ObservableProperty]
        private string totalAmount;
        [ObservableProperty]
        private bool isItemSelectEnabled;
        [ObservableProperty]
        private string showBookingContent;
        [ObservableProperty]
        private bool isBookingViewVisible;
        [ObservableProperty]
        private string todayDate;

        [ObservableProperty]
        private Models.Table selectedTable;

        public List<Models.Table> TotalTable { get; set; }

        public OrderViewModel()
        {
            
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
            this.selectedMenus = new ObservableCollection<Menu>();
            this.ShowBookingContent = "";
            this.TodayDate = DateTime.Now.ToString("dd - MMM - yyyy");
        }

        public void OnNavigatedFrom()
        {
            
        }

        private void InitializeViewModel()
        {


            var tables=new List<Models.Table>();
            tables.Add(new Models.Table { TableId = 1, TableNumber = "T 1", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 2, TableNumber = "T 2", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 3, TableNumber = "T 3", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 4, TableNumber = "T 4", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 5, TableNumber = "T 5", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 6, TableNumber = "T 6", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 7, TableNumber = "T 7", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 8, TableNumber = "T 8", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 9, TableNumber = "T 9", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 10, TableNumber = "T 10", Tag = "4seater" });
            tables.Add(new Models.Table { TableId = 11, TableNumber = "T 11", Tag = "6seater" });
            tables.Add(new Models.Table { TableId = 12, TableNumber = "T 12", Tag = "6seater" });
            tables.Add(new Models.Table { TableId = 13, TableNumber = "T 13", Tag = "6seater" });
            tables.Add(new Models.Table { TableId = 14, TableNumber = "T 14", Tag = "6seater" });
            TotalTable = tables;


            var menu = new List<Menu>();
            menu.Add(new Menu { MenuId = 1, Name = "Chicken Curry", Price = 200, Type = "F", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 11, Name = "Chicken Dopyaza", Price = 280, Type = "F", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 111, Name = "Chicken Curry", Price = 110, Type = "H", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 21, Name = "Chicken Dopyaza", Price = 150, Type = "H", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 31, Name = "Butter Checken", Price = 200, Type = "F", Category = " ", Quantity = 1 });

            menu.ForEach(x => x.CombineName = x.Name + " ( " + x.Type + " )");
            MenuItems = menu;


            var booking = new List<Booking>();
            var dt = DateTime.Now;
            booking.Add(new Booking { ArrivalTime = dt.AddMinutes(10).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId =1,CustomerName="Subhash" ,Date=dt.Date,Pax=4,TableNum="T1",IsArrived=false});
            booking.Add(new Booking { ArrivalTime = dt.AddHours(7).AddMinutes(30).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 2, CustomerName = "Adityaraj", Date = dt.Date, Pax = 4, TableNum = "T1", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(8).AddMinutes(0).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 3, CustomerName = "Chaitali", Date = dt.Date, Pax = 6, TableNum = "T12", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(5).AddMinutes(30).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 4, CustomerName = "Keo Na", Date = dt.Date, Pax = 2, TableNum = "T5", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(6).AddMinutes(0).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 5, CustomerName = "Ashish", Date = dt.Date, Pax = 3, TableNum = "T4", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddMinutes(17).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 6, CustomerName = "Young", Date = dt.Date, Pax = 4, TableNum = "T3", IsArrived = false });

            BookingList = new ObservableCollection<Booking>(booking);
            OnPropertyChanged();

        }

        [RelayCommand]
        private void OnRecalculate()
        {
            var val = SelectedMenus.Sum(x => x.Price * x.Quantity);
            TotalAmount = string.Format("{0:0,0.00}", val);
            OnPropertyChanged();
        }
        [RelayCommand]
        private void OnDeleteItem(object prm)
        {
            var menu = prm as Menu;
            SelectedMenus.Remove(menu);

            var val = SelectedMenus.Sum(x => x.Price * x.Quantity);
            TotalAmount = string.Format("{0:0,0.00}", val);
            OnPropertyChanged();
        }
        [RelayCommand]
        private void OnPrint(object prm)
        {
            if(this.SelectedTable!=null)
            {
                this.SelectedTable.IsBusy = true;
               // this.IsBusy = true;
               OnPropertyChanged(nameof(this.SelectedTable.IsBusy));
            }
        }
        [RelayCommand]
        private void OnBill(object prm)
        {

        }

        [RelayCommand]
        private void OnSelectedMenu()
        {
            if (!SelectedMenus.Contains(SelectedMenuItem) && SelectedMenuItem != null)
            {
                SelectedMenus.Add(selectedMenuItem);
                var val = SelectedMenus.Sum(x => x.Price * x.Quantity);
                TotalAmount = string.Format("{0:0,0.00}", val);
                SelectedMenuItem = null;

                notifier.ShowSuccess("Item added");
            }
            OnPropertyChanged(nameof(SelectedMenus));
        }

        [RelayCommand]
        private void OnShowBooking()
        {
            this.IsBookingViewVisible = false;
        }
        [RelayCommand]
        private void OnTableSelecton(object prm)
        {
            this.ShowBookingContent = "Manage Booking";
            this.IsBookingViewVisible = true;
            
            //this.SelectedTable = new Models.Table { TableId =int.Parse( (((System.Windows.Data.Binding)prm).Path.Path).Split("T")[1]) };
           // OnPropertyChanged(nameof(SelectedTable));
        }

        [RelayCommand]
        private void OnSaveBooking()
        {

        }
        [RelayCommand]
        private void OnRejectBooking()
        {

        }

        [RelayCommand]
        private void OnSetIsBusy(string prm)
        {
            if (this.SelectedTable != null)
            {
                var val = bool.Parse(prm);

                //var assignedTableData = this..Where(x => x.TableNum.Length > 0);

                this.SelectedTable.IsBusy = val;
                OnPropertyChanged(nameof(this.SelectedTable.IsBusy)); 
            }
        }

        [RelayCommand]
        private void OnSetIsReserved(string prm)
        {
            if (this.SelectedTable != null)
            {
                var val = bool.Parse(prm);
                this.SelectedTable.IsBooked = val;
                OnPropertyChanged(nameof(this.SelectedTable.IsBooked));
            }
        }
    }
}
