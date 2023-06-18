using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic.ApplicationServices;
using RestaurantDesk.Models;
using RestaurantDesk.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using Application = System.Windows.Application;
using Button = Wpf.Ui.Controls.Button;
using MenuItem = System.Windows.Controls.MenuItem;

namespace RestaurantDesk.ViewModels
{
    public partial class CustomerViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        ContextMenu contextMenu;
        private string baseAddress;

        [ObservableProperty]
        private IEnumerable<Customer>? customersList;
        [ObservableProperty]
        private Customer selectedCustomer;
        [ObservableProperty]
        private List<CustomerTypeEnum> customerTypes;
        [ObservableProperty]
        private CustomerTypeEnum selectedCustomerType;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private string selectedGender;
        [ObservableProperty]
        private List<string> genderlist;

        public ICommand RunDialogCommand;

        public void OnNavigatedTo()
        {
            this.PropertyChanged += CustomerViewModel_PropertyChanged;
            if (!_isInitialized)
            {
                baseAddress = $"{ConfigurationManager.AppSettings["ApiBaseAddress"]}";
                InitializeViewModel();
            }
        }

        private void CustomerViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedCustomer")
                this.IsEditMode = false;
        }

        public void OnNavigatedFrom()
        {
            //  RunDialogCommand = new RelayCommand(ExecuteDeleteContextMenuCommand);
        }

        private void InitializeViewModel()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("api/Customer").Result;
                if (response.IsSuccessStatusCode)
                {
                    var customers = response.Content.ReadAsStringAsync().Result;
                    var listOfCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(customers).ToList();
                    listOfCustomer.ForEach(x => x.FullName = x.Fname + " " + x.Lname);
                    this.CustomersList = listOfCustomer;
                    this.SelectedCustomer = this.CustomersList.First() as Customer;
                }
                else
                {

                }
            }

            var list = Enum.GetValues(typeof(CustomerTypeEnum))
                            .Cast<CustomerTypeEnum>()
                            .ToList();
            this.CustomerTypes = list;
            this.SelectedCustomerType = list.Where(x => x == SelectedCustomer.CustomerType).FirstOrDefault();

            _isInitialized = true;

        }
        private void ExecuteEditContextMenuCommand()
        {
            this.IsEditMode = true;
            var genderlist = new List<string>();
            genderlist.Add("Male");
            genderlist.Add("Female");
            this.Genderlist = genderlist;
            this.SelectedGender = this.SelectedCustomer.Gender == "M" ? "Male" : "Female";
            if (contextMenu != null)
                contextMenu.IsOpen = false;
        }
        private async void ExecuteDeleteContextMenuCommandAsync()
        {
            if (contextMenu != null)
                contextMenu.IsOpen = false;
            var view = new DeleteDialogView
            {
                DataContext = this
            };
            var result = await DialogHost.Show(view, "RootDialog", null, ClosingEventHandler, ClosedEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closing event, and cancel here.");

        private void ClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closed event here (1).");

        [RelayCommand]
        private void OnCustomerAction(object sender)
        {
            contextMenu = Application.Current.FindResource("contextMenu") as ContextMenu;
            ((contextMenu.Items[0] as MenuItem).Header as Button).Command = new RelayCommand(ExecuteEditContextMenuCommand);

            ((contextMenu.Items[1] as MenuItem).Header as Button).Command = new RelayCommand(ExecuteDeleteContextMenuCommandAsync);

            contextMenu.PlacementTarget = sender as Hyperlink;
            contextMenu.IsOpen = true;
        }

        [RelayCommand]
        private void OnSearch()
        {

        }
    }
}
