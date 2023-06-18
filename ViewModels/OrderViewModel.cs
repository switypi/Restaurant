using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enterwell.Clients.Wpf.Notifications;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.Logging;
using RestaurantDesk.Models;
using RestaurantDesk.UserControls;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Interfaces;
using Menu = RestaurantDesk.Models.Menu;
using System.Configuration;

namespace RestaurantDesk.ViewModels
{
    public partial class OrderViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private BackgroundWorker backgroundWorker;
        private string baseAddress;
        internal List<TableUserControl> CustomTableControlList = new List<TableUserControl>();
        private Random rndum { get; set; }

        [ObservableProperty]
        private IEnumerable<Menu>? menuItems;

        [ObservableProperty]
        private ObservableCollection<Menu>? selectedMenus;
        [ObservableProperty]
        private ObservableCollection<Booking> bookingList;
        [ObservableProperty]
        private bool isBusy;

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
        private bool isUnbilledContentVisible;

        [ObservableProperty]
        private string todayDate;

        [ObservableProperty]
        private Models.Table selectedTable;

        private Order Currentorder { get; set; }
        public List<Models.Table> TotalTable { get; set; }
        private Dictionary<string, List<Menu>> OrderedItems { get; set; }

        public OrderViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                baseAddress = $"{ConfigurationManager.AppSettings["ApiBaseAddress"]}";
                InitializeViewModel();
                this.selectedMenus = new ObservableCollection<Menu>();
                this.ShowBookingContent = "";
                this.TodayDate = DateTime.Now.ToString("dd - MMM - yyyy");
                OrderedItems = new Dictionary<string, List<Menu>>();
                this.SelectedTable = null;
                this.PropertyChanged += OrderViewModel_PropertyChanged;
            }
            _isInitialized = true;
            rndum = new Random();
        }

        private void OrderViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBookingViewVisible" && this.IsBookingViewVisible == true)
            {
                if (this.MenuItems == null || this.MenuItems.Count() == 0)
                    backgroundWorker = new BackgroundWorker
                    {
                        WorkerReportsProgress = true,
                        WorkerSupportsCancellation = true
                    };
                backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                this.IsBusy = true;
                backgroundWorker.RunWorkerAsync();
            }

        }

        public void OnNavigatedTo()
        {


        }

        public void OnNavigatedFrom()
        {

        }

        private void InitializeViewModel()
        {


            var tables = new List<Models.Table>();
            tables.Add(new Models.Table { TableId = 1, TableNumber = "T 1", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 2, TableNumber = "T 2", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 3, TableNumber = "T 3", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 4, TableNumber = "T 4", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 5, TableNumber = "T 5", Tag = "4seater", TableType = TableTypeEnum.Angular });
            tables.Add(new Models.Table { TableId = 6, TableNumber = "T 6", Tag = "4seater", TableType = TableTypeEnum.Angular });
            tables.Add(new Models.Table { TableId = 7, TableNumber = "T 7", Tag = "4seater", TableType = TableTypeEnum.Angular });
            tables.Add(new Models.Table { TableId = 8, TableNumber = "T 8", Tag = "4seater", TableType = TableTypeEnum.Angular });
            tables.Add(new Models.Table { TableId = 9, TableNumber = "T 9", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 10, TableNumber = "T 10", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 11, TableNumber = "T 11", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 12, TableNumber = "T 12", Tag = "4seater", TableType = TableTypeEnum.Rectangle });


            tables.Add(new Models.Table { TableId = 15, TableNumber = "T 13", Tag = "6seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 16, TableNumber = "T 14", Tag = "6seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 17, TableNumber = "T 15", Tag = "6seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 18, TableNumber = "T 16", Tag = "6seater", TableType = TableTypeEnum.Circle });

            tables.Add(new Models.Table { TableId = 19, TableNumber = "T 17", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 20, TableNumber = "T 18", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 21, TableNumber = "T 19", Tag = "4seater", TableType = TableTypeEnum.Rectangle });
            tables.Add(new Models.Table { TableId = 22, TableNumber = "T 20", Tag = "4seater", TableType = TableTypeEnum.Rectangle });


            TotalTable = tables;


            var menu = new List<Menu>();
            //menu.Add(new Menu { MenuId = 1, Name = "Chicken Curry", IsVeg = false, Price = 200, ServingType = "F", Category = " ", Quantity = 1 });
            //menu.Add(new Menu { MenuId = 11, Name = "Chicken Dopyaza", IsVeg = false, Price = 280, ServingType = "F", Category = " ", Quantity = 1 });
            //menu.Add(new Menu { MenuId = 111, Name = "Chicken Curry", IsVeg = false, Price = 110, ServingType = "H", Category = " ", Quantity = 1 });
            //menu.Add(new Menu { MenuId = 21, Name = "Chicken Dopyaza", IsVeg = false, Price = 150, ServingType = "H", Category = " ", Quantity = 1 });
            //menu.Add(new Menu { MenuId = 31, Name = "Butter Checken", IsVeg = false, Price = 200, ServingType = "F", Category = " ", Quantity = 1 });

            //menu.Add(new Menu { MenuId = 21, Name = "Paneer Dopyaza", IsVeg = true, Price = 150, ServingType = "H", Category = " ", Quantity = 1 });
            //menu.Add(new Menu { MenuId = 31, Name = "Gobi Masala", IsVeg = true, Price = 200, ServingType = "F", Category = " ", Quantity = 1 });

            //menu.ForEach(x => x.CombineName = x.Name + " ( " + x.ServingType + " )");
            //MenuItems = menu;


            var booking = new List<Booking>();
            var dt = DateTime.Now;
            booking.Add(new Booking { ArrivalTime = dt.AddMinutes(10).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 1, CustomerName = "Subhash", Date = dt.Date, Pax = 4, TableNum = "T1", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(7).AddMinutes(30).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 2, CustomerName = "Adityaraj", Date = dt.Date, Pax = 4, TableNum = "T8", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(8).AddMinutes(0).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 3, CustomerName = "Chaitali", Date = dt.Date, Pax = 6, TableNum = "T12", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(5).AddMinutes(30).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 4, CustomerName = "Keo Na", Date = dt.Date, Pax = 2, TableNum = "T5", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddHours(6).AddMinutes(0).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 5, CustomerName = "Ashish", Date = dt.Date, Pax = 3, TableNum = "T4", IsArrived = false });
            booking.Add(new Booking { ArrivalTime = dt.AddMinutes(17).ToString("hh:mm tt", CultureInfo.InvariantCulture), BookingId = 6, CustomerName = "Young", Date = dt.Date, Pax = 4, TableNum = "T3", IsArrived = false });

            BookingList = new ObservableCollection<Booking>(booking);
            OnPropertyChanged();
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            this.IsBusy = false;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("api/Menu").Result;
                if (response.IsSuccessStatusCode)
                {
                    var menuItems = response.Content.ReadAsStringAsync().Result;
                    var listOfMenuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Menu>>(menuItems).ToList();

                    listOfMenuItems.ForEach(x => x.CombineName = x.Name + " ( " + x.ServingType + " )");
                    MenuItems = listOfMenuItems;
                }
                else
                {

                }
            }

        }

        [RelayCommand]
        private void OnCheckedIn(object prm)
        {
            Binding bn;
            var item = prm as Booking;


            var tblControl = this.CustomTableControlList.Where(x => x.TableNum.Replace(" ", "") == item.TableNum.Replace(" ", "")).FirstOrDefault();
            if (tblControl != null)
            {

                var btn = (tblControl.Content as StackPanel).Children.OfType<ButtonUserControl>().FirstOrDefault();
                var expIsBusy = btn.GetBindingExpression(ButtonUserControl.IsBusyProperty);
                var expIsbooked = btn.GetBindingExpression(ButtonUserControl.IsBookedProperty);

                tblControl.IsBusy = item.IsArrived;
                var cntbl = this.CustomTableControlList.FirstOrDefault(x => x.TableNum.Replace(" ", "") == item.TableNum.Replace(" ", ""));
                btn.IsBusy = item.IsArrived;

                this.SelectedTable = new Models.Table { IsBusy = tblControl.IsBusy, TableNumber = tblControl.TableNum };
                if (expIsBusy == null)
                {
                    bn = new Binding();
                    bn.Source = SelectedTable;
                    bn.Path = new PropertyPath("IsBusy");
                    bn.Mode = BindingMode.TwoWay;
                    BindingOperations.SetBinding(btn, ButtonUserControl.IsBusyProperty, bn);
                }


                // OnPropertyChanged(nameof(SelectedTable));
                OnSetIsBusy(this.SelectedTable.IsBusy.ToString());
            }

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
            if (this.SelectedTable != null)
            {
                this.SelectedTable.IsBusy = true;

                List<Menu> varMenu = new List<Menu>();
                foreach (var item in this.SelectedMenus)
                {
                    varMenu.Add(new Menu { Name = item.Name, IsVeg = item.IsVeg, Price = item.Price, Quantity = item.Quantity, ServingType = item.ServingType });
                }

                if (OrderedItems.ContainsKey(this.SelectedTable.TableNumber))
                {
                    List<Menu> menuList;
                    OrderedItems.TryGetValue(this.SelectedTable.TableNumber, out menuList);
                    menuList.AddRange(varMenu);
                    OrderedItems.Remove(this.SelectedTable.TableNumber);
                    OrderedItems.Add(this.SelectedTable.TableNumber, menuList);
                }
                else
                    OrderedItems.Add(this.SelectedTable.TableNumber, varMenu);



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


                //GrowlInfo f = new GrowlInfo();
                //f.ShowCloseButton = false;
                //f.ShowDateTime = false;
                //f.Message = "File saved successfully!";
                //f.Token = "SuccessMsg";
                //f.Type = InfoType.Success;
                //Growl.Success(f);


                // manager.CreateMessage()
                //.Accent("#1751C3")
                //.Background("#333")
                //.HasBadge("Info")
                //.HasMessage("Item has been added.")
                //.Dismiss().WithButton("Update now", button => { })
                //.Dismiss().WithButton("Release notes", button => { })
                //.Dismiss().WithButton("Later", button => { })
                //.Dismiss().WithDelay(2000)
                //.Queue();
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
            if (SelectedTable != null && SelectedTable.IsBusy == true)
            {
                this.IsUnbilledContentVisible = true;
            }
            else
                this.IsUnbilledContentVisible = false;

            //this.SelectedTable = new Models.Table { TableId =int.Parse( (((System.Windows.Data.Binding)prm).Path.Path).Split("T")[1]) };
            // OnPropertyChanged(nameof(SelectedTable));
        }

        [RelayCommand]
        private void OnSaveBooking()
        {
            //Binding bn;
            //foreach (var item in this.BookingList)
            //{

            //    var tbl = this.TotalTable.Where(x => x.TableNumber.Replace(" ", "") == item.TableNum).FirstOrDefault();
            //    var tblControl = this.CustomTableControlList.Where(x => x.TableNum.Replace(" ", "") == tbl.TableNumber.Replace(" ", "")).FirstOrDefault();
            //    if (tblControl != null)
            //    {

            //        var btn = (tblControl.Content as StackPanel).Children.OfType<ButtonUserControl>().FirstOrDefault();
            //        var expIsBusy = tblControl.GetBindingExpression(TableUserControl.IsBusyProperty);
            //        var expIsbooked = tblControl.GetBindingExpression(TableUserControl.IsBookedProperty);

            //        tblControl.IsBusy = item.IsArrived;
            //        var cntbl = this.CustomTableControlList.FirstOrDefault(x => x.TableNum.Replace(" ", "") == tbl.TableNumber.Replace(" ", ""));
            //        cntbl.IsBusy = item.IsArrived;

            //        this.SelectedTable = new Models.Table { IsBusy = tblControl.IsBusy, IsBooked = tblControl.IsBooked, TableNumber = tblControl.TableNum };
            //        if (expIsBusy == null)
            //        {
            //            bn = new Binding();
            //            bn.Source = SelectedTable;
            //            bn.Path = new PropertyPath("IsBusy");
            //            bn.Mode = BindingMode.TwoWay;
            //            BindingOperations.SetBinding(btn, ButtonUserControl.IsBusyProperty, bn);
            //        }


            //       // OnPropertyChanged(nameof(SelectedTable));
            //        OnSetIsBusy(this.SelectedTable.IsBusy.ToString());
            //    }
            //}
            // this.SelectedTable = null;
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
                this.SelectedTable.IsBusy = val;

                var item = this.CustomTableControlList.FirstOrDefault(x => x.TableNum.Replace(" ", "") == this.SelectedTable.TableNumber.Replace(" ", ""));
                var btn = (item.Content as StackPanel).Children.OfType<ButtonUserControl>().FirstOrDefault();

                item.IsBusy = val;
                btn.IsBusy = val;

                if (val == true)
                    this.IsUnbilledContentVisible = true;
                else
                    this.IsUnbilledContentVisible = false;
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
                var item = this.CustomTableControlList.FirstOrDefault(x => x.TableNum.Replace(" ", "") == this.SelectedTable.TableNumber.Replace(" ", ""));
                item.IsBooked = val;

                OnPropertyChanged(nameof(this.SelectedTable.IsBooked));
            }
        }

        [RelayCommand]
        private void OnShowUnbilledItem()
        {
            List<Menu> menuList;
            if (this.SelectedTable != null && OrderedItems.ContainsKey(this.SelectedTable.TableNumber))
            {
                OrderedItems.TryGetValue(this.SelectedTable.TableNumber, out menuList);
                var val = menuList.Sum(x => x.Price * x.Quantity);
                TotalAmount = string.Format("{0:0,0.00}", val);
                SelectedMenus = new ObservableCollection<Menu>(menuList);
                OnPropertyChanged(nameof(SelectedMenus));
            }
        }
    }
}
