using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors.Recommended;
using MaterialDesignThemes.Wpf;
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

        private int numberOfTable4Seater = 0;
        private int numberOfTable6Seater = 0;
        public OrderPage(ViewModels.OrderViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
           
            numberOfTable4Seater = int.Parse($"{ConfigurationManager.AppSettings["NumberOfTables4Seater"]}");
            numberOfTable6Seater = int.Parse($"{ConfigurationManager.AppSettings["NumberOfTables6Seater"]}");
            LoadCOntrols();
        }

        private void LoadCOntrols()
        {
            int numOfRows = 0;
            int numOfCols = 4;
            int tableCnt = 0;

            numOfRows = (numberOfTable4Seater / 4);
            if (numberOfTable4Seater % 4 > 0)
            {
                numOfRows = numOfRows + 1;
            }

            for (int i = 1; i <= numOfCols; i++)
            {
                Eventgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                //Adding 6seater table 

            } 

            for (int ii = 1; ii <= numOfRows; ii++)
            {
                Eventgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });

                for (int i = 1; i <= numOfCols; i++)
                {
                    if (Eventgrid.Children.OfType<RadioButton>().Count() < numberOfTable4Seater)
                    {
                        tableCnt = tableCnt + 1;
                        RadioButton cd = new RadioButton();
                        cd.HorizontalAlignment = HorizontalAlignment.Left;
                        cd.VerticalAlignment = VerticalAlignment.Stretch;
                        cd.Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush;
                        //cd.Height = 80;
                        //cd.Width = 180;
                        MaterialDesignThemes.Wpf.ElevationAssist.SetElevation(cd, Elevation.Dp1);
                        cd.Style = Application.Current.Resources["cardStyle4seater"] as Style;
                        cd.HorizontalContentAlignment = HorizontalAlignment.Left;
                        cd.GroupName = "abcd";
                        cd.Unchecked += Cd_Unchecked;
                        cd.Checked += Cd_Checked;

                        Label lb = new Label();
                        lb.HorizontalAlignment = HorizontalAlignment.Stretch;
                        lb.VerticalAlignment = VerticalAlignment.Top;
                        //lb.HorizontalContentAlignment = HorizontalAlignment.Right; ;
                        lb.Content = "T " + tableCnt;

                        lb.Style = Application.Current.Resources["lblTableStyle"] as Style;


                        StackPanel st = new StackPanel { Margin = new Thickness(2), Width=175, Orientation = Orientation.Horizontal, 
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Right };

                        //BitmapImage logo1 = new BitmapImage();
                        //logo1.BeginInit();
                        //logo1.UriSource = new Uri("pack://application:,,,/Images/4seaterg.png");
                        //logo1.EndInit();

                        //Image img = new Image();
                        //img.Source = logo1;
                        ////img.Width = 80;
                        ////img.Height = 80;
                        //img.Stretch = Stretch.UniformToFill  ;
                        //img.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                        //img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                        //    ;

                        
                        //st.Children.Add(img);
                        st.Children.Add(lb);


                        //Grid grd = new Grid();
                        //grd.ShowGridLines = false;
                        //grd.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        //grd.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2) });
                        //grd.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });


                        //grd.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        //grd.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2) });
                        //grd.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                        //Label lbOrderStatus = new Label();
                        //lbOrderStatus.Content = "Order Status";
                        //lbOrderStatus.HorizontalAlignment = HorizontalAlignment.Left;
                        //lbOrderStatus.VerticalAlignment = VerticalAlignment.Top;
                        //lbOrderStatus.Padding = new Thickness(0);
                        //lbOrderStatus.Style = Application.Current.Resources["lblTableStyleItem"] as Style;
                        //Grid.SetRow(lbOrderStatus, 0);
                        //Grid.SetColumn(lbOrderStatus, 0);
                        //grd.Children.Add(lbOrderStatus);

                        //Label lbOrderCount = new Label();
                        //lbOrderCount.Content = "Order Count";
                        //lbOrderCount.Padding = new Thickness(0);
                        //lbOrderCount.HorizontalAlignment = HorizontalAlignment.Left;
                        //lbOrderCount.VerticalAlignment = VerticalAlignment.Top;
                        //lbOrderCount.Style = Application.Current.Resources["lblTableStyleItem"] as Style;
                        //Grid.SetRow(lbOrderCount, 2);
                        //Grid.SetColumn(lbOrderCount, 0);
                        //grd.Children.Add(lbOrderCount);

                        //Label lbOrderCountValue = new Label();
                        //lbOrderCountValue.Content = "50";
                        //lbOrderCountValue.Padding = new Thickness(0);
                        //lbOrderCountValue.HorizontalAlignment = HorizontalAlignment.Left;
                        //lbOrderCountValue.VerticalAlignment = VerticalAlignment.Top;
                        //lbOrderCountValue.Style = Application.Current.Resources["lblTableStyleItemValue"] as Style;
                        //Grid.SetRow(lbOrderCountValue, 0);
                        //Grid.SetColumn(lbOrderCountValue, 2);
                        //grd.Children.Add(lbOrderCountValue);

                        //Label lbOrderStatusValue = new Label();
                        //lbOrderStatusValue.Content = "D";
                        //lbOrderStatusValue.Padding = new Thickness(0);
                        //lbOrderStatusValue.HorizontalAlignment = HorizontalAlignment.Left;
                        //lbOrderStatusValue.VerticalAlignment = VerticalAlignment.Top;
                        //lbOrderStatusValue.Style = Application.Current.Resources["lblTableStyleItemValue"] as Style;
                        //Grid.SetRow(lbOrderStatusValue, 2);
                        //Grid.SetColumn(lbOrderStatusValue, 2);
                        //grd.Children.Add(lbOrderStatusValue);


                        //Border br = new Border { BorderThickness = new Thickness(0, 1, 0, 0), BorderBrush = new SolidColorBrush(Colors.Green) };
                        //br.HorizontalAlignment = HorizontalAlignment.Stretch;
                        //br.VerticalAlignment = VerticalAlignment.Stretch;

                        //br.Child = grd;
                        //st.Children.Add(br);

                        cd.Content = st;

                        Grid.SetColumn(cd, i - 1);
                        Grid.SetRow(cd, ii - 1);
                        Eventgrid.Children.Add(cd);


                    }


                }
            }



            Eventgrid6seater.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });

            for ( int iii=1;iii<=numOfCols;iii++)
            {
                Eventgrid6seater.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                tableCnt = tableCnt + 1;
                RadioButton cd = new RadioButton();
                cd.HorizontalAlignment = HorizontalAlignment.Left;
                cd.VerticalAlignment = VerticalAlignment.Stretch;
                cd.Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush;
                //cd.Height = 80;
                //cd.Width = 180;
                MaterialDesignThemes.Wpf.ElevationAssist.SetElevation(cd, Elevation.Dp1);
                cd.Style = Application.Current.Resources["cardStyle6seater"] as Style;
                cd.HorizontalContentAlignment = HorizontalAlignment.Left;
                cd.GroupName = "abcd";
                cd.Unchecked += Cd_Unchecked;
                cd.Checked += Cd_Checked;

                Label lb = new Label();
                lb.HorizontalAlignment = HorizontalAlignment.Right;
                lb.VerticalAlignment = VerticalAlignment.Top;
                lb.Content = "T " + tableCnt;
                lb.Style = Application.Current.Resources["lblTableStyle"] as Style;


                StackPanel st = new StackPanel
                {
                    Margin = new Thickness(2),
                    Width = 175,
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                //BitmapImage logo1 = new BitmapImage();
                //logo1.BeginInit();
                //logo1.UriSource = new Uri("pack://application:,,,/Images/6seaterbl.png");
                //logo1.EndInit();

                //Image img = new Image();
                //img.Source = logo1;
                ////img.Width = 80;
                ////img.Height = 80;
                //img.VerticalAlignment = VerticalAlignment.Top;
                //img.Stretch = Stretch.UniformToFill;
                //img.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                //img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                
                //st.Children.Add(img);
                st.Children.Add(lb);
                cd.Content = st;

                Grid.SetColumn(cd, iii-1);
                Grid.SetRow(cd, 0);
                Eventgrid6seater.Children.Add(cd);

            }

        }

        private void Cd_Checked(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
            (e.Source as RadioButton).IsChecked = true;
            (e.Source as RadioButton).Background = Application.Current.Resources["actionBtnMouseOverColor"] as SolidColorBrush;
            ViewModel.SelectedMenus.Clear();

        }

        private void Cd_Unchecked(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            (e.Source as RadioButton).IsChecked = false;
            (e.Source as RadioButton).Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush; ;
        }
    }
}
