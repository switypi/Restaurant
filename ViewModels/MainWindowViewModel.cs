using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace RestaurantDesk.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;
        [ObservableProperty]
        private string currentTime;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();
        [ObservableProperty]
        private string userFistName;

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
                LoadSettings();
            }

        }
        private void LoadSettings()
        {

        }
        private void InitializeData()
        {

        }
        private void InitializeViewModel()
        {
            ApplicationTitle = "RestO Desk";
            this.CurrentTime =DateTime.Now.ToString("hh : mm tt");
            this.UserFistName = "Subhash";

            BitmapImage logo1 = new BitmapImage();
            logo1.BeginInit();
            logo1.UriSource = new Uri("pack://application:,,,/Images/cutlery.png");
            
            logo1.EndInit();

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/Images/dashboard.png");
            logo.EndInit();

            BitmapImage logo2 = new BitmapImage();
            logo2.BeginInit();
            logo2.UriSource = new Uri("pack://application:,,,/Images/people.png");
            logo2.EndInit();

            BitmapImage logo3 = new BitmapImage();
            logo3.BeginInit();
            logo3.UriSource = new Uri("pack://application:,,,/Images/waiter.png");
            logo3.EndInit();

            BitmapImage logo4 = new BitmapImage();
            logo4.BeginInit();
            logo4.UriSource = new Uri("pack://application:,,,/Images/booking.png");
            logo4.EndInit();

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Dashboard",Width=100,Height=75,
                    PageTag = "Dashboard",
                    Image= logo,
                    PageType = typeof(Views.Pages.DashboardPage),
                     
                },
                
                new NavigationItem()
                {
                    Content = "Dine-In",Width=100,Height=75,
                    PageTag = "Order",
                    Image= logo1,
                    PageType = typeof(Views.Pages.OrderPage)
                },

                 new NavigationItem()
                {
                    Content = "Reservation",Width=100,Height=75,
                    PageTag = "Reservation",
                    Image=logo4,Padding=new Thickness(0,0,0,0),  
                    PageType = typeof(Views.Pages.Reservation)
                },

                  new NavigationItem()
                {
                    Content = "Waiter",Width=100,Height=75,
                    PageTag = "Waiter",
                     Image=logo3,
                    PageType = typeof(Views.Pages.WaiterPage)
                },
                   new NavigationItem()
                {
                    Content = "Customer",Width=100,Height=75,
                    PageTag = "Customer",
                    Image=logo2,
                    PageType = typeof(Views.Pages.CustomerPage)
                }
            };


          //  (NavigationItems[0] as NavigationItem).FontSize = 20;
            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings", Width=100,
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings48,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }
    }
}
