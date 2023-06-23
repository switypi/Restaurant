using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using RestaurantDesk.Models;
using RestaurantDesk.UserControls;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;
using Wpf.Ui.Styles.Controls;

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
        private bool isAutomatedTableStatusProcessing;
        private BackgroundWorker backgroundWorker;
        private List<Models.Table> bookedTables;


        public OrderPage(ViewModels.OrderViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            numberOfTable4Seater = int.Parse($"{ConfigurationManager.AppSettings["NumberOfTables4Seater"]}");
            numberOfTable6Seater = int.Parse($"{ConfigurationManager.AppSettings["NumberOfTables6Seater"]}");
            isAutomatedTableStatusProcessing = bool.Parse($"{ConfigurationManager.AppSettings["IsAutomatedTableStatusProcessing"]}");

            //this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;

        }

        //private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "SelectedMenuItem")
        //    {
        //       ax.Text = "";
        //    }
        //}

        private void LoadCOntrols()
        {
            int numOfRows = 0;
            int numOfCols = 4;
            int tableCnt = 0;
            int tableCnt2 = 0;
            List<int> rowCollection = new List<int>();

            numberOfTable4Seater = ViewModel.TotalTable.Where(x => x.Tag.ToString() == "4seater").Count();

            numOfRows = (numberOfTable4Seater / 4);
            if (numberOfTable4Seater % 4 > 0)
            {
                numOfRows = numOfRows + 1;
            }

            for (int i = 1; i <= numOfCols; i++)
            {
                Eventgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            //4 seater
            #region 4 seater
            for (int ii = 1; ii <= numOfRows; ii++)
            {
                Eventgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });

                for (int i = 1; i <= numOfCols; i++)
                {
                    if (Eventgrid.Children.OfType<TableUserControl>().Count() < numberOfTable4Seater)
                    {
                        tableCnt = tableCnt + 1;
                        var dbTable = ViewModel.TotalTable.Where(x => x.Tag == "4seater").ToList()[tableCnt - 1];

                        TableUserControl cd = new TableUserControl();
                        cd.Tag = dbTable.Tag;
                        cd.TableId = dbTable.TableId;
                        cd.TableNum = dbTable.TableNumber;
                        cd.TableType = dbTable.TableType;
                        cd.HorizontalAlignment = HorizontalAlignment.Left;
                        cd.VerticalAlignment = VerticalAlignment.Top;
                        //cd.Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush;

                        //cd.Height = 100;
                        //cd.Width = 130;
                        MaterialDesignThemes.Wpf.ElevationAssist.SetElevation(cd, Elevation.Dp1);
                        cd.Style = Application.Current.Resources["cardStyle4seater"] as Style;
                        cd.GroupName = "abcd";
                        cd.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        cd.Unchecked += Cd_Unchecked;
                        cd.Checked += Cd_Checked;

                        ButtonUserControl lb = new ButtonUserControl();
                        lb.HorizontalAlignment = HorizontalAlignment.Stretch;
                        lb.VerticalAlignment = VerticalAlignment.Top;
                        lb.Content = cd.TableNum;
                        lb.Height = 40;
                        lb.Width = 40;
                        lb.Background = Application.Current.Resources["FreeTableBackground"] as SolidColorBrush;
                        lb.Style = Application.Current.Resources["roundButtonTemplate"] as Style;

                        if (cd.TableType == TableTypeEnum.Angular)
                        {
                            cd.HorizontalAlignment = HorizontalAlignment.Left;
                            cd.Margin = new Thickness(10, -20, 0, 0);
                            cd.VerticalAlignment = VerticalAlignment.Top;
                        }

                        StackPanel st = new StackPanel
                        {
                            Margin = new Thickness(2, 2, 2, 2),
                            //Width = 175,
                            Orientation = Orientation.Horizontal,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Right
                        };

                        st.Children.Add(lb);
                        cd.Content = st;
                        cd.Name = cd.TableNum.ToString().Replace(" ", "");

                        //ViewModel.SelectedTable = new Models.Table { IsBooked = cd.IsBooked, IsBusy = cd.IsBusy, TableNumber = cd.TableNum };
                        Binding bn = new Binding();
                        bn.Source = ViewModel;
                        bn.Path = new PropertyPath("SelectedTable");
                        bn.Mode = BindingMode.TwoWay;
                        cd.SetBinding(TableUserControl.SelectedTableProperty, bn);

                        System.Windows.Controls.ContextMenu contextMenu;
                        Separator separator;
                        contextMenu = new System.Windows.Controls.ContextMenu();
                        contextMenu.Items.Add(new MenuItem { Header = "Set Busy", CommandParameter = "True", Command = ViewModel.SetIsBusyCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Set Reserved", CommandParameter = "True", Command = ViewModel.SetIsReservedCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Set Free", CommandParameter = "false", Command = ViewModel.SetIsBusyCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Free Reserved", CommandParameter = "false", Command = ViewModel.SetIsReservedCommand });
                        cd.ContextMenu = contextMenu;

                        cd.SetBinding(Button.CommandProperty, new Binding { Source = ViewModel, Path = new PropertyPath("TableSelectonCommand") });
                        cd.CommandParameter = cd.Name;
                        cd.MouseEnter += Cd_MouseEnter;
                        Grid.SetColumn(cd, i - 1);
                        Grid.SetRow(cd, ii - 1);
                        Eventgrid.Children.Add(cd);
                        ViewModel.CustomTableControlList.Add(cd);
                    }

                }
                //}
            }

            #endregion

            #region 6 seater
            numberOfTable6Seater = ViewModel.TotalTable.Where(x => x.Tag.ToString() == "6seater").Count();
            numOfRows = (numberOfTable6Seater / 4);
            if (numberOfTable6Seater % 4 > 0)
            {
                numOfRows = numOfRows + 1;
            }


            for (int i = 1; i <= numOfCols; i++)
            {
                Eventgrid6seater.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            //Eventgrid6seater.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });

            for (int iii = 1; iii <= numOfRows; iii++)
            {
                Eventgrid6seater.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });

                for (int i = 1; i <= numOfCols; i++)
                {
                    if (Eventgrid6seater.Children.OfType<TableUserControl>().Count() < numberOfTable6Seater)
                    {
                        tableCnt = tableCnt + 1;
                        tableCnt2 = tableCnt2 + 1;
                        var dbTable = ViewModel.TotalTable.Where(x => x.Tag == "6seater").ToList()[tableCnt2 - 1];

                        TableUserControl cd = new TableUserControl();
                        cd.Tag = dbTable.Tag;
                        cd.TableId = dbTable.TableId;
                        cd.TableNum = dbTable.TableNumber;
                        cd.Name = cd.TableNum.ToString().Replace(" ", "");
                        cd.TableType = dbTable.TableType;
                        cd.BorderBrush = new SolidColorBrush(Colors.Transparent);

                        cd.HorizontalAlignment = HorizontalAlignment.Left;
                        cd.VerticalAlignment = VerticalAlignment.Stretch;
                        //cd.Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush;
                        //cd.Height = 80;
                        //cd.Width = 150;
                        cd.Padding = new Thickness(0);
                        MaterialDesignThemes.Wpf.ElevationAssist.SetElevation(cd, Elevation.Dp1);
                        cd.Style = Application.Current.Resources["cardStyle6seater"] as Style;
                        cd.HorizontalContentAlignment = HorizontalAlignment.Left;
                        cd.GroupName = "abcd";
                        cd.Unchecked += Cd_Unchecked;
                        cd.Checked += Cd_Checked;

                        ButtonUserControl lb = new ButtonUserControl();
                        lb.HorizontalAlignment = HorizontalAlignment.Right;
                        lb.VerticalAlignment = VerticalAlignment.Top;
                        lb.Content = cd.TableNum;
                        lb.Height = 40;
                        lb.Width = 40;
                        lb.Background = Application.Current.Resources["FreeTableBackground"] as SolidColorBrush;
                        lb.Style = Application.Current.Resources["roundButtonTemplate"] as Style;


                        StackPanel st = new StackPanel
                        {
                            Margin = new Thickness(2, 2, 2, 2),
                            //Width = 175,
                            Orientation = Orientation.Horizontal,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Stretch
                        };

                        Binding bn = new Binding();
                        bn.Source = ViewModel;
                        bn.Path = new PropertyPath("SelectedTable");
                        bn.Mode = BindingMode.TwoWay;
                        cd.SetBinding(TableUserControl.SelectedTableProperty, bn);

                        System.Windows.Controls.ContextMenu contextMenu;
                        Separator separator;
                        contextMenu = new System.Windows.Controls.ContextMenu();
                        contextMenu.Items.Add(new MenuItem { Header = "Set Busy", CommandParameter = "True", Command = ViewModel.SetIsBusyCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Set Reserved", CommandParameter = "True", Command = ViewModel.SetIsReservedCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Set Free", CommandParameter = "false", Command = ViewModel.SetIsBusyCommand });

                        separator = new Separator();
                        contextMenu.Items.Add(separator);
                        contextMenu.Items.Add(new MenuItem { Header = "Free Reserved", CommandParameter = "false", Command = ViewModel.SetIsReservedCommand });

                        cd.ContextMenu = contextMenu;

                        //st.Children.Add(img);

                        st.Children.Add(lb);
                        cd.Content = st;


                        cd.SetBinding(Button.CommandProperty, new Binding { Source = ViewModel, Path = new PropertyPath("TableSelectonCommand") });
                        cd.CommandParameter = new Binding(cd.Name);

                        Grid.SetColumn(cd, i - 1);
                        Grid.SetRow(cd, iii - 1);
                        Eventgrid6seater.Children.Add(cd);
                        ViewModel.CustomTableControlList.Add(cd);
                    }
                }

            }

            #endregion

        }

        private void Cd_MouseEnter(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Cd_Checked(object sender, RoutedEventArgs e)
        {

            Binding bn;

            var tbl = (e.Source as TableUserControl);
            tbl.IsChecked = true;

            //if (tbl.IsBooked == false && tbl.IsBusy == false)
            //{
            //    tbl.IsChecked = true;
            //    tbl.Background = Application.Current.Resources["actionBtnMouseOverColor"] as SolidColorBrush;

            //}

            ViewModel.SelectedMenus.Clear();
            var btn = (tbl.Content as StackPanel).Children.OfType<ButtonUserControl>().FirstOrDefault();

            var expIsBusy = btn.GetBindingExpression(ButtonUserControl.IsBusyProperty);
            var expIsbooked = btn.GetBindingExpression(ButtonUserControl.IsBookedProperty);



            ViewModel.SelectedTable = new Models.Table { IsBusy = btn.IsBusy, IsBooked = btn.IsBooked, TableNumber = (tbl.Content as StackPanel).Children.OfType<ButtonUserControl>().First().Content.ToString() };

            if (expIsBusy == null)
            {
                bn = new Binding();
                bn.Source = ViewModel.SelectedTable;
                bn.Path = new PropertyPath("IsBusy");
                bn.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(btn, ButtonUserControl.IsBusyProperty, bn);
            }
            if (expIsbooked == null)
            {
                bn = new Binding();
                bn.Source = ViewModel.SelectedTable;
                bn.Path = new PropertyPath("IsBooked");
                bn.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(btn, ButtonUserControl.IsBookedProperty, bn);
            }

            //if (tbl.IsBusy == false)
            //{
            //    contextMenu.Items.Add(new MenuItem { Header = "Set Busy", CommandParameter = "True", Command = ViewModel.SetIsBusyCommand });
            //}
            //else
            //{
            //    contextMenu.Items.Add(new MenuItem { Header = "Set Free", CommandParameter = "false", Command = ViewModel.SetIsBusyCommand, IsEnabled = true });
            //}

            //if (!isAutomatedTableStatusProcessing && tbl.IsBooked == false)
            //{
            //    Separator separator = new Separator();
            //    contextMenu.Items.Add(separator);
            //    contextMenu.Items.Add(new MenuItem { Header = "Set Reserved", CommandParameter = "True", Command = ViewModel.SetIsReservedCommand });
            //    tbl.ContextMenu = contextMenu;
            //}
            //else
            //{
            //    Separator separator = new Separator();
            //    contextMenu.Items.Add(separator);
            //    contextMenu.Items.Add(new MenuItem { Header = "Free Reserved", CommandParameter = "false", Command = ViewModel.SetIsReservedCommand });
            //    tbl.ContextMenu = contextMenu;
            //}

        }

        private void Cd_Unchecked(object sender, RoutedEventArgs e)
        {
            var tbl = (e.Source as TableUserControl);
            if (tbl.IsBooked == false && tbl.IsBusy == false)
            {
                tbl.IsChecked = false;
                tbl.Background = Application.Current.Resources["EmptyTableBackground"] as SolidColorBrush; ;

            }

        }


        private void UiPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Eventgrid.ColumnDefinitions.Count == 0)
                LoadCOntrols();
            if (IsInitialized && isAutomatedTableStatusProcessing)
            {
                backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
            }


        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Update UI
            foreach (var item in bookedTables)
            {
                TableUserControl tb = Eventgrid.Children.OfType<TableUserControl>().ToList().Where(x => x.Name == item.TableNumber).FirstOrDefault();
                tb.IsBooked = true;
            }
            // Run again
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            var bookingData = ViewModel.BookingList;
            var dt = DateTime.Now;
            bookedTables = new List<Models.Table>();

            var assignedTableData = bookingData.Where(x => x.TableNum.Length > 0);
            if (assignedTableData.Any())
            {
                foreach (var item in assignedTableData)
                {
                    var diff = (DateTime.Parse(item.ArrivalTime) - dt).TotalMinutes;
                    if (diff <= 15)
                    {
                        bookedTables.Add(new Models.Table { TableNumber = item.TableNum });
                    }
                }
            }

        }


    }
}
