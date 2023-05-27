using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;

namespace RestaurantDesk.Views.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>

    public partial class OrderPage : INavigableView<ViewModels.OrderViewModel>
    {
        public ViewModels.OrderViewModel ViewModel
        {
            get;
        }

        public OrderPage(ViewModels.OrderViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}
