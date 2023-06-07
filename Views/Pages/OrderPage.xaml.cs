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

        }

        private void LoadCOntrols()
        {
            int numOfRows = 0;
            int numOfCols = 4;
            int tableCnt = 0;
            List<int> rowCollection = new List<int>();

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
                Eventgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });
                //rowCollection.Add(ii);
                //if (ii % 2 != 0)
                //{
                    for (int i = 1; i <= numOfCols; i++)
                    {
                        if (Eventgrid.Children.OfType<TableUserControl>().Count() < numberOfTable4Seater)
                        {
                            var dbTable = ViewModel.TotalTable.Where(x => x.Tag == "4seater").ToList()[i - 1];
                            tableCnt = tableCnt + 1;
                            TableUserControl cd = new TableUserControl();
                            cd.Tag = dbTable.Tag;
                            cd.TableId = dbTable.TableId;
                            cd.TableNum = dbTable.TableNumber;
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
                            lb.Content = cd.TableNum;
                            lb.Style = Application.Current.Resources["lblTableStyle"] as Style;

                            StackPanel st = new StackPanel
                            {
                                Margin = new Thickness(2),
                                Width = 175,
                                Orientation = Orientation.Horizontal,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                HorizontalAlignment = HorizontalAlignment.Right
                            };

                            st.Children.Add(lb);
                            cd.Content = st;
                            cd.Name = cd.TableNum.ToString().Replace(" ", "");


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

                            Grid.SetColumn(cd, i - 1);
                            Grid.SetRow(cd, ii - 1);
                            Eventgrid.Children.Add(cd);
                        }

                    }
                //}
            }

            #endregion

            #region 6 seater

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
                Eventgrid6seater.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });

                for (int i = 1; i <= numOfCols; i++)
                {
                    if (Eventgrid6seater.Children.OfType<TableUserControl>().Count() < numberOfTable6Seater)
                    {
                        var dbTable = ViewModel.TotalTable.Where(x => x.Tag == "6seater").ToList()[i - 1];
                        tableCnt = tableCnt + 1;
                        TableUserControl cd = new TableUserControl();
                        cd.Tag = dbTable.Tag;
                        cd.TableId = dbTable.TableId;
                        cd.TableNum = dbTable.TableNumber;
                        cd.Name = cd.TableNum.ToString().Replace(" ", "");

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
                        lb.Content = cd.TableNum;
                        lb.Style = Application.Current.Resources["lblTableStyle"] as Style;


                        StackPanel st = new StackPanel
                        {
                            Margin = new Thickness(2),
                            Width = 175,
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
                    }
                }

            }

            #endregion

        }

        private void Cd_Checked(object sender, RoutedEventArgs e)
        {

            Binding bn;
            var tbl = (e.Source as TableUserControl);

            if (tbl.IsBooked == false && tbl.IsBusy == false)
            {
                tbl.IsChecked = true;
                tbl.Background = Application.Current.Resources["actionBtnMouseOverColor"] as SolidColorBrush;

            }
            ViewModel.SelectedMenus.Clear();


            ViewModel.SelectedTable = new Models.Table { TableId = int.Parse((tbl.Content as StackPanel).Children.OfType<Label>().First().Content.ToString().Split("T")[1]) };

            var expIsBusy = tbl.GetBindingExpression(TableUserControl.IsBusyProperty);
            var expIsbooked = tbl.GetBindingExpression(TableUserControl.IsBookedProperty);
            if (expIsBusy == null)
            {
                bn = new Binding();
                bn.Source = ViewModel.SelectedTable;
                bn.Path = new PropertyPath("IsBusy");
                bn.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(tbl, TableUserControl.IsBusyProperty, bn);
            }
            if (expIsbooked == null)
            {
                bn = new Binding();
                bn.Source = ViewModel.SelectedTable;
                bn.Path = new PropertyPath("IsBooked");
                bn.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(tbl, TableUserControl.IsBookedProperty, bn);
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
