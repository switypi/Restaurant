using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace RestaurantDesk.ViewModels
{
    public partial class WaiterViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private IEnumerable<Waiter>? waiterList;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            var waiters = new List<Waiter>();
            waiters.Add(new Waiter { Contact = "909090",  Fname = "subhash", Lname = "frrds",JoinDate=DateTime.Parse("11/08/2022"),Sex="Male",Address="345/8hur" });
            waiters.Add(new Waiter { Contact = "909090", Fname = "rrrdss", Lname = "fcvvv", JoinDate = DateTime.Parse("02/12/2022"), Sex = "Male", Address = "788/segr" });
            waiters.Add(new Waiter { Contact = "123ee",  Fname = "wersf", Lname = "fff", JoinDate = DateTime.Parse("01/12/2022"), Sex = "Male", Address = "87/juski lane" });
            waiters.Add(new Waiter { Contact = "332322", Fname = "arcgdd", Lname = "njiko", JoinDate = DateTime.Parse("12/02/2022"), Sex = "Female", Address = "9/kieso l" });
            waiters.Add(new Waiter { Contact = "3es333", Fname = "fgf", Lname = "njiko", JoinDate = DateTime.Parse("12/01/2022"), Sex = "Male", Address = "780/jutebox lane" });

            WaiterList = waiters;
            // Colors = colorCollection;

            _isInitialized = true;
        }
    }
}
