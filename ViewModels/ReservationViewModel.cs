using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace RestaurantDesk.ViewModels
{
    public partial class ReservationViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private IEnumerable<Customer>? customersList;
        [ObservableProperty]
        private bool isDialogOpen;

        public void OnNavigatedTo()
        {
            IsDialogOpen = false;
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }
        [RelayCommand]
        private void OnCancel()
        {
            IsDialogOpen = false;
        }
        [RelayCommand]
        private void OnSave()
        {
            IsDialogOpen = false;
        }
        [RelayCommand]
        private void OnDelete()
        {
            IsDialogOpen = false;
        }

        private void InitializeViewModel()
        {
            var customers = new List<Customer>();
            customers.Add(new Customer { Contact = "909090", EmailId = "ghbnn@email.com", Fname = "subhash", Lname = "frrds" });
            customers.Add(new Customer { Contact = "909090", EmailId = "2wwww@email.com", Fname = "rrrdss", Lname = "fcvvv" });
            customers.Add(new Customer { Contact = "123ee", EmailId = "sssssdx@email.com", Fname = "wersf", Lname = "fff" });
            customers.Add(new Customer { Contact = "332322", EmailId = "fgfff@email.com", Fname = "arcgdd", Lname = "njiko" });
            customers.Add(new Customer { Contact = "3es333", EmailId = "gvvvv@email.com", Fname = "fgf", Lname = "njiko" });

            CustomersList = customers;
            // Colors = colorCollection;

            _isInitialized = true;
        }
    }
}
