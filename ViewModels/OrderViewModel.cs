using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantDesk.Models;
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
        private Menu selectedMenuItem;
        [ObservableProperty]
        private string totalAmount;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
            this.selectedMenus = new ObservableCollection<Menu>();
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            var menu = new List<Menu>();
            menu.Add(new Menu { MenuId = 1, Name = "Chicken Curry", Price = 200, Type = "F", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 11, Name = "Chicken Dopyaza", Price = 280, Type = "F", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 111, Name = "Chicken Curry", Price = 110, Type = "H", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 21, Name = "Chicken Dopyaza", Price = 150, Type = "H", Category = " ", Quantity = 1 });
            menu.Add(new Menu { MenuId = 31, Name = "Butter Checken", Price = 200, Type = "F", Category = " ", Quantity = 1 });

            menu.ForEach(x => x.CombineName = x.Name + " ( " + x.Type + " )");
            MenuItems = menu;
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

        }

        [RelayCommand]
        private void OnSelectedMenu()
        {
            if (!SelectedMenus.Contains(SelectedMenuItem))
            {
                SelectedMenus.Add(selectedMenuItem);
                var val = SelectedMenus.Sum(x => x.Price * x.Quantity);
                TotalAmount = string.Format("{0:0,0.00}", val);
            }
            OnPropertyChanged(nameof(SelectedMenus));
        }
    }
}
