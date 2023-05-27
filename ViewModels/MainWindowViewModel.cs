using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace RestaurantDesk.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "RestaurantDesk";
            //BitmapIcon ico = new BitmapIcon();
            //ico.UriSource = new Uri("");

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Dashboard", Width=100,
                    PageTag = "Dashboard",
                    Icon =SymbolRegular.DataBarVertical16,
                    PageType = typeof(Views.Pages.DashboardPage),IconSize=30,
                },
                new NavigationItem()
                {
                    Content = "Order", 
                    PageTag = "Order",
                    Icon = SymbolRegular.TableSimple20,
                    PageType = typeof(Views.Pages.OrderPage)
                },

                 new NavigationItem()
                {
                    Content = "Reservation",
                    PageTag = "Reservation",
                    Icon = SymbolRegular.BookInformation24,
                    PageType = typeof(Views.Pages.Reservation)
                },

                  new NavigationItem()
                {
                    Content = "Waiter",
                    PageTag = "Waiter",
                    Icon = SymbolRegular.PeopleList20,
                    PageType = typeof(Views.Pages.WaiterPage)
                },
                   new NavigationItem()
                {
                    Content = "Customer",
                    PageTag = "Customer",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.CustomerPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
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
