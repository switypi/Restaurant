﻿using Microsoft.VisualBasic;
using RestaurantDesk.Models;
using RestaurantDesk.ViewModels;
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

namespace RestaurantDesk.Views.Pages
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : INavigableView<ViewModels.ReservationViewModel>
    {
        public ViewModels.ReservationViewModel ViewModel
        {
            get;
        }

        private List<Appointment> _myAppointmentsList = new List<Appointment>();
        static DateTime _dt = DateTime.Now;
        string dayOfWeek = _dt.DayOfWeek.ToString();
        DateTime prevDT = new DateTime(_dt.Year, _dt.Month, _dt.Day);
        DateTime satCal = DateTime.Now;
        DateTime sunCal = DateTime.Now;
        TimeSpan startTime = TimeSpan.FromHours(9);
        TimeSpan endTime = TimeSpan.FromHours(23);

        public Reservation(ViewModels.ReservationViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
            InitializeControl();
            onLoaded();
            if (DateTime.Now.TimeOfDay.Hours >= 14)
                ScrollEventsViewer.ScrollToEnd();


        }

        private void onLoaded()
        {
            today.Content = _dt.ToString("MMMM - yyyy");
            SetDates(_dt);
            
        }

        private void InitializeControl()
        {
            //Columns
            int totalCol = 8;
            var totalRow = (endTime - startTime).TotalHours;
            Label lb;
            StackPanel cv;
            List<object> objs = new List<object>();
            for (int icnt = 1; icnt <= totalCol; icnt++)
            {
                if (icnt == 1) { EventsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80, GridUnitType.Pixel) }); }
                else
                {
                    EventsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    cv = new StackPanel { Name = "columnCanvus" + (icnt - 1), Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Stretch };

                    objs.Add(cv);
                }
            }
            for (int icnt = 0; icnt <= totalRow; icnt++)
            {
                EventsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
                lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Top; lb.HorizontalAlignment = HorizontalAlignment.Center;
                lb.Margin = new Thickness(0, -10, 0, 0);
                lb.Content = DateTime.Today.Add(TimeSpan.FromHours(startTime.Hours + icnt)).ToString("hh:mm tt");
                Grid.SetColumn(lb, 0);
                Grid.SetRow(lb, icnt);
                EventsGrid.Children.Add(lb);


            }

            foreach (var item in objs)
            {
                var index = objs.IndexOf(item);
                Grid.SetColumn((item as StackPanel), index + 1);
                Grid.SetRow((item as StackPanel), 0);
                Grid.SetRowSpan((item as StackPanel), (int)totalRow);
                (item as StackPanel).PreviewMouseLeftButtonUp += Reservation_PreviewMouseLeftButtonUp;
                EventsGrid.Children.Add((item as StackPanel));
            }
            //switch (dayOfWeek)
            //{
            //    case "Monday":
            //        Canvas? cv1 = EventsGrid.Children.OfType<Canvas>().ToList().Take(1) as Canvas;
            //        cv1.Background = new SolidColorBrush(Colors.AliceBlue);
            //        break;
            //    case "Tuesday":
            //        var lstCanvas = EventsGrid.Children.OfType<Canvas>().ToList().Take(2);
            //        foreach (var c in lstCanvas)
            //        {
            //            c.Background = new SolidColorBrush(Colors.AliceBlue);
            //        }


            //        break;
            //    case "Wednesday":
            //        var lstCanvas3 = EventsGrid.Children.OfType<Canvas>().ToList().Take(3);
            //        foreach (var c in lstCanvas3)
            //        {
            //            c.Background = new SolidColorBrush(Colors.AliceBlue);
            //        }

            //        break;
            //    case "Thursday":
            //        var lstCanvas4 = EventsGrid.Children.OfType<Canvas>().ToList().Take(4);
            //        foreach (var c in lstCanvas4)
            //        {
            //            c.Background = new SolidColorBrush(Colors.AliceBlue);
            //        }

            //        break;
            //    case "Friday":
            //        var lstCanvas5 = EventsGrid.Children.OfType<Canvas>().ToList();

            //        foreach (var c in lstCanvas5)
            //        {
            //            if (lstCanvas5.FindIndex(x => x == c) < 5)
            //            {
            //                c.Background = new SolidColorBrush(Colors.LightBlue);
            //                c.IsEnabled = false;
            //                c.Opacity = .5;
            //            }
            //            else
            //            {
            //                c.Background = new SolidColorBrush(Colors.Transparent);
            //                //c.IsEnabled = false;
            //                c.Opacity = .5;
            //            }

            //        }

            //        break;
            //    case "Saturday":
            //        var lstCanvas6 = EventsGrid.Children.OfType<Canvas>().ToList().Take(6);
            //        foreach (var c in lstCanvas6)
            //        {
            //            c.Background = new SolidColorBrush(Colors.AliceBlue);
            //        }

            //        break;
            //    case "Sunday":

            //        break;

            //}

        }

        private void Reservation_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void SetDates(DateTime dt)
        {
            string caseSwitch = dayOfWeek;
            prevDT = dt;
            var _dt = dt;
            today.Content = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).ToString("MMMM - yyyy");
            switch (caseSwitch)
            {
                case "Monday":
                    dayLabel1.Content = "Sun " + _dt.AddDays(-1).Day; //dzień 0
                    dayLabel1.Tag = _dt.AddDays(-1);
                    dayLabel2.Content = "Mon " + (_dt.AddDays(0).Day);
                    dayLabel2.Tag = _dt.AddDays(0);
                    dayLabel2.Style = (Style)Application.Current.Resources["lblStyle"];
                    dayLabel3.Content = "Tue " + (_dt.AddDays(1).Day);// + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel3.Tag = _dt.AddDays(1);
                    dayLabel4.Content = "Wed " + (_dt.AddDays(2).Day);// + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel4.Tag = _dt.AddDays(2);
                    dayLabel5.Content = "Thu " + (_dt.AddDays(3).Day);// + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    dayLabel5.Tag = _dt.AddDays(3);
                    dayLabel6.Content = "Fri " + (_dt.AddDays(4).Day);// + "/" + _dt.AddDays(5).Month + "/" + _dt.AddDays(5).Year;
                    dayLabel6.Tag = _dt.AddDays(4);
                    dayLabel7.Content = "Sa " + (_dt.AddDays(5).Day);// + "/" + _dt.AddDays(6).Month + "/" + _dt.AddDays(6).Year;
                    dayLabel7.Tag = _dt.AddDays(5);
                    satCal = _dt.AddDays(5);
                    sunCal = _dt.AddDays(-1);
                    break;
                case "Tuesday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(-2).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel1.Tag = _dt.AddDays(-2);
                    dayLabel2.Content = "Mon " + _dt.AddDays(-1).Day;// + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel2.Tag = _dt.AddDays(-1);
                    dayLabel3.Content = "Tue " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel3.Tag = _dt.AddDays(0);
                    dayLabel3.Style = (Style)Application.Current.Resources["lblStyle"];

                    dayLabel4.Content = "Wed " + (_dt.AddDays(1).Day);// + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel4.Tag = _dt.AddDays(1);
                    dayLabel5.Content = "Thu " + (_dt.AddDays(2).Day);// + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel5.Tag = _dt.AddDays(2);
                    dayLabel6.Content = "Fri " + (_dt.AddDays(3).Day);// + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    dayLabel6.Tag = _dt.AddDays(3);
                    dayLabel7.Content = "Sa " + (_dt.AddDays(4).Day);// + "/" + _dt.AddDays(5).Month + "/" + _dt.AddDays(5).Year;
                    dayLabel7.Tag = _dt.AddDays(4);
                    satCal = _dt.AddDays(4);
                    sunCal = _dt.AddDays(-2);
                    break;
                case "Wednesday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(-3).Day);// + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel1.Tag = _dt.AddDays(-3);
                    dayLabel2.Content = "Mon " + (_dt.AddDays(-2).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel2.Tag = _dt.AddDays(-2);
                    dayLabel3.Content = "Tue " + _dt.AddDays(-1).Day;// + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel3.Tag = _dt.AddDays(-1);
                    dayLabel4.Content = "Wed " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel4.Tag = _dt.AddDays(0);
                    dayLabel4.Style = (Style)Application.Current.Resources["lblStyle"];
                    dayLabel5.Content = "Thu " + (_dt.AddDays(1).Day);// + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel5.Tag = _dt.AddDays(1);
                    dayLabel6.Content = "Fri " + (_dt.AddDays(2).Day);// + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel6.Tag = _dt.AddDays(2);
                    dayLabel7.Content = "Sa " + (_dt.AddDays(3).Day);// + "/" + _dt.AddDays(4).Month + "/" + _dt.AddDays(4).Year;
                    dayLabel7.Tag = _dt.AddDays(-3);
                    satCal = _dt.AddDays(3);
                    sunCal = _dt.AddDays(-3);
                    break;
                case "Thursday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(-4).Day);// + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel1.Tag = _dt.AddDays(-4);
                    dayLabel2.Content = "Mon " + (_dt.AddDays(-3).Day);// + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel2.Tag = _dt.AddDays(-3);
                    dayLabel3.Content = "Tue " + (_dt.AddDays(-2).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel3.Tag = _dt.AddDays(-2);
                    dayLabel4.Content = "Wed " + _dt.AddDays(-1).Day;// + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel4.Tag = _dt.AddDays(-1);
                    dayLabel5.Content = "Thu " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel5.Tag = _dt.AddDays(0);
                    dayLabel5.Style = (Style)Application.Current.Resources["lblStyle"];
                    dayLabel6.Content = "Fri " + (_dt.AddDays(1).Day);// + "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel6.Tag = _dt.AddDays(1);
                    dayLabel7.Content = "Sa " + (_dt.AddDays(2).Day);// + "/" + _dt.AddDays(3).Month + "/" + _dt.AddDays(3).Year;
                    dayLabel7.Tag = _dt.AddDays(2);
                    satCal = _dt.AddDays(2);
                    sunCal = _dt.AddDays(-4);
                    break;
                case "Friday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(-5).Day);// + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel1.Tag = _dt.AddDays(-5);
                    dayLabel2.Content = "Mon " + (_dt.AddDays(-4).Day);// + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel2.Tag = _dt.AddDays(-4);
                    dayLabel3.Content = "Tue " + (_dt.AddDays(-3).Day);// + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel3.Tag = _dt.AddDays(-3);
                    dayLabel4.Content = "Wed " + (_dt.AddDays(-2).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel4.Tag = _dt.AddDays(-2);
                    dayLabel5.Content = "Thu " + _dt.AddDays(-1).Day;// + "/" + _dt.Month + "/" + _dt.Year;//dzień 0
                    dayLabel5.Tag = _dt.AddDays(-1);
                    dayLabel6.Content = "Fri " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel6.Tag = _dt.AddDays(0);
                    dayLabel6.Style = (Style)Application.Current.Resources["lblStyle"];
                    dayLabel7.Content = "Sa " + (_dt.AddDays(1).Day); //+ "/" + _dt.AddDays(2).Month + "/" + _dt.AddDays(2).Year;
                    dayLabel7.Tag = _dt.AddDays(1);
                    satCal = _dt.AddDays(1);
                    sunCal = _dt.AddDays(-5);
                    break;
                case "Saturday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(-6).Day);// + "/" + _dt.AddDays(-5).Month + "/" + _dt.AddDays(-5).Year;
                    dayLabel1.Tag = _dt.AddDays(-6);
                    dayLabel2.Content = "Mon " + (_dt.AddDays(-5).Day);// + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel2.Tag = _dt.AddDays(-5);
                    dayLabel3.Content = "Tue " + (_dt.AddDays(-4).Day);// + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel3.Tag = _dt.AddDays(-4);
                    dayLabel4.Content = "Wed " + (_dt.AddDays(-3).Day);// + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel4.Tag = _dt.AddDays(-3);
                    dayLabel5.Content = "Thu " + (_dt.AddDays(-2).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel5.Tag = _dt.AddDays(-2);
                    dayLabel6.Content = "Fri " + _dt.AddDays(-1).Day;// + "/" + _dt.Month + "/" + _dt.Year;//dzień 0
                    dayLabel6.Tag = _dt.AddDays(-1);
                    dayLabel7.Content = "Sa " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(1).Month + "/" + _dt.AddDays(1).Year;
                    dayLabel7.Tag = _dt.AddDays(0);
                    dayLabel7.Style = (Style)Application.Current.Resources["lblStyle"];
                    satCal = _dt;
                    sunCal = _dt.AddDays(-6);
                    break;
                case "Sunday":
                    dayLabel1.Content = "Sun " + (_dt.AddDays(0).Day);// + "/" + _dt.AddDays(-0).Month + "/" + _dt.AddDays(-6).Year;
                    dayLabel1.Tag = _dt.AddDays(0);
                    dayLabel1.Style = (Style)Application.Current.Resources["lblStyle"];
                    dayLabel2.Content = "Mon " + (_dt.AddDays(1).Day);// + "/" + _dt.AddDays(-5).Month + "/" + _dt.AddDays(-5).Year;
                    dayLabel2.Tag = _dt.AddDays(1);
                    dayLabel3.Content = "Tue " + (_dt.AddDays(2).Day);// + "/" + _dt.AddDays(-4).Month + "/" + _dt.AddDays(-4).Year;
                    dayLabel3.Tag = _dt.AddDays(2);
                    dayLabel4.Content = "Wed " + (_dt.AddDays(3).Day);// + "/" + _dt.AddDays(-3).Month + "/" + _dt.AddDays(-3).Year;
                    dayLabel4.Tag = _dt.AddDays(3);
                    dayLabel5.Content = "Thu " + (_dt.AddDays(4).Day);// + "/" + _dt.AddDays(-2).Month + "/" + _dt.AddDays(-2).Year;
                    dayLabel5.Tag = _dt.AddDays(4);
                    dayLabel6.Content = "Fri " + (_dt.AddDays(5).Day);// + "/" + _dt.AddDays(-1).Month + "/" + _dt.AddDays(-1).Year;
                    dayLabel6.Tag = _dt.AddDays(5);
                    dayLabel7.Content = "Sat " + _dt.AddDays(6).Day;// + "/" + _dt.Month + "/" + _dt.Year; //dzień 0
                    dayLabel7.Tag = _dt.AddDays(6);
                    satCal = _dt.AddDays(6);
                    sunCal = _dt;
                    break;

            }

        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            //wyświetl przeszły tydzień
            string caseSwitch = dayOfWeek;
            prevDT = prevDT.AddDays(-7);
            today.Content = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).ToString("MMMM - yyyy");
            SetDates(prevDT);
            clrConvas();
            GetBookingForThisWeek();

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // wyświetl następny tydzień
            string caseSwitch = dayOfWeek;
            prevDT = prevDT.AddDays(7);
            today.Content = new DateTime(prevDT.Year, prevDT.Month, prevDT.Day).ToString("MMMM - yyyy");
            SetDates(prevDT);
            clrConvas();
            GetBookingForThisWeek();
        }

        private void goToWaitingList_Click(object sender, RoutedEventArgs e)
        {
            //idż do listy 
            // WaitingList wlWindow = new WaitingList();
            // wlWindow.ShowDialog();

        }

        private void goToAddVisit_Click(object sender, RoutedEventArgs e)
        {
            //AddVisit vWidnow = new AddVisit();
            // vWidnow.ShowDialog();
        }


        void OnPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string day = "";
            int startHour = 0;
            int startMinute = 0;
            DateTime clickedDay;

            if (e.ClickCount == 2) // for double-click, remove this condition if only want single click
            {
                var point = Mouse.GetPosition(EventsGrid);

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in EventsGrid.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in EventsGrid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

                Console.WriteLine(row + ":" + col);

                switch (col)
                {
                    case 0:
                        day += dayLabel1.Tag;
                        break;
                    case 1:
                        day += dayLabel1.Tag;
                        break;
                    case 2:
                        day += dayLabel2.Tag;
                        break;
                    case 3:
                        day += dayLabel3.Tag;
                        break;
                    case 4:
                        day += dayLabel4.Tag;
                        break;
                    case 5:
                        day += dayLabel5.Tag;
                        break;
                    case 6:
                        day += dayLabel6.Tag;
                        break;
                    case 7:
                        day += dayLabel7.Tag;
                        break;
                }

                int d = DateTime.Parse(day.ToString()).Day;// int.Parse(subs[0]);
                int m = DateTime.Parse(day.ToString()).Month;// int.Parse(subs[1]);
                int y = DateTime.Parse(day.ToString()).Year;// int.Parse(subs[2]);

                switch (row)
                {
                    case 0:
                        startHour = 9;
                        break;
                    case 1:
                        startHour = 10;
                        break;
                    case 2:
                        startHour = 11;
                        break;
                    case 3:
                        startHour = 12;
                        break;
                    case 4:
                        startHour = 13;
                        break;
                    case 5:
                        startHour = 14;
                        break;
                    case 6:
                        startHour = 15;
                        break;
                    case 7:
                        startHour = 16;
                        break;
                    case 8:
                        startHour = 17;
                        break;
                    case 9:
                        startHour = 18;
                        break;
                    case 10:
                        startHour = 19;
                        break;
                    case 11:
                        startHour = 20;
                        break;
                    case 12:
                        startHour = 21;
                        break;
                    case 13:
                        startHour = 22;
                        break;
                    case 14:
                        startHour = 23;
                        break;


                }

                clickedDay = new DateTime(y, m, d, startHour, startMinute, 0);
                Console.WriteLine(clickedDay);
                if (clickedDay != null)
                {

                    ViewModel.IsDialogOpen = true;
                }
            }

        }

        private void clrConvas()
        {
            List<StackPanel> objs = EventsGrid.Children.OfType<StackPanel>().ToList();
            foreach (var item in objs)
            {
                item.Children.Clear();
            }
        }

        private void EventClick(object sender, RoutedEventArgs ev)
        {
            //Button _btn = sender as Button;
            //Console.WriteLine(_btn.DataContext);
            //int _btnVisitId = (int)_btn.DataContext;
            //Console.WriteLine(_btnVisitId);
            //AddVisit vWidnow = new AddVisit(_btnVisitId);
            //vWidnow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            clrConvas();
            GetBookingForThisWeek();
        }

        private void GetBookingForThisWeek()
        {
            List<VisitInfo> bookingList = new List<VisitInfo>();
            DateTime dt = DateTime.Now.AddHours(1);

            bookingList.Add(new VisitInfo { Email = "abcd@ema", VisitDate = DateTime.Now.Date, Name = "Subhash", Start = dt, End = dt.AddHours(1) });
            bookingList.Add(new VisitInfo { Email = "abcd@ema", VisitDate = DateTime.Now.Date, Name = "Subhash", Start = dt, End = dt.AddHours(2) });
            bookingList.Add(new VisitInfo { Email = "abcd@ema", VisitDate = DateTime.Now.Date, Name = "Subhash", Start = dt.AddHours(-2), End = dt.AddHours(1) });
            bookingList.Add(new VisitInfo { Email = "abcd@ema", VisitDate = DateTime.Now.AddDays(-1).Date, Name = "Subhash", Start = dt.AddDays(-1), End = dt.AddDays(-1).AddHours(2) });
            bookingList.Add(new VisitInfo { Email = "abcd@ema", VisitDate = DateTime.Now.Date, Name = "Subhash", Start = dt.AddHours(2), End = dt.AddHours(3) });

            List<VisitInfo> result = new List<VisitInfo>();

            var weekVisits = VisitsInThisWeek(bookingList);

            List<VisitInfo> VisitsInThisWeek(List<VisitInfo> visitList)
            {
                List<VisitInfo> result = new List<VisitInfo>();

                foreach (var v in visitList)
                {
                    if ((v.VisitDate).CompareTo(sunCal.Date) >= 0 && (v.VisitDate).CompareTo(satCal.Date) <= 0)
                    {
                        if ( v.End < DateTime.Now)
                            v.IsPastVisit = true;
                        else if (v.Start > DateTime.Now)
                            v.IsFutureVisit = true;
                        else
                            v.IsPresentVisit = true;

                        result.Add(v);
                    }
                }
                return result;
            }
            var totalApts = weekVisits.Count;
            foreach (var v in weekVisits)
            {
                CreateVisitControl(v, totalApts);
            }
        }

        private void CreateVisitControl(VisitInfo v, int aptsCounts)
        {
            int column = (int)v.VisitDate.DayOfWeek;//0 dla niedzieli, 1 dla pon...
            int row = 0;
            int h = v.Start.Hour;
            int m = v.Start.Minute;
            int eh = v.End.Hour;
            if (eh < h)
            {
                eh = 24;
            }
            int em = v.End.Minute;

            switch (h)
            {
                case 9:
                    row = 0;
                    break;
                case 10:
                    row = 1;
                    break;
                case 11:
                    row = 2;
                    break;
                case 12:
                    row = 3;
                    break;
                case 13:
                    row = 4;
                    break;
                case 14:
                    row = 5;
                    break;
                case 15:
                    row = 6;
                    break;
                case 16:
                    row = 7;
                    break;
                case 17:
                    row = 8;
                    break;
                case 18:
                    row = 9;
                    break;
                case 19:
                    row = 10;
                    break;
                case 20:
                    row = 11;
                    break;
                case 21:
                    row = 12;
                    break;
                case 22:
                    row = 10;
                    break;
                case 23:
                    row = 11;
                    break;
            }

            //switch (m)
            //{
            //    case 0:
            //        row += 0;
            //        break;
            //    case 15:
            //        row += 1;
            //        break;
            //    case 30:
            //        row += 2;
            //        break;
            //    case 45:
            //        row += 3;
            //        break;
            //}

            int length = ((eh * 60) - (h * 60)) / 60;

            //tworzenie kontrolki button - wizyty
            Wpf.Ui.Controls.Button b = new Wpf.Ui.Controls.Button();
            b.Background = Brushes.BlanchedAlmond;

            b.Height = 50 * (length);
            //b.Width = EventsGrid[1].ActualWidth/ aptsCounts; //GridLength.Auto.Value;  //EventsGrid.Children.OfType<ColumnDefinition>().ToList()[0].ActualWidth; //150;
            b.HorizontalAlignment = HorizontalAlignment.Stretch;
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Margin = new Thickness(0, row * 50, 0, 0);
            b.Focusable = true;
            b.MinWidth = 20;
            b.Foreground = new SolidColorBrush(Colors.White);
            b.Style = this.Resources["aptCustom"] as Style;
            if (v.IsFutureVisit)
                b.Background = Application.Current.Resources["FutureVisitBackgroun"] as SolidColorBrush;
            else if (v.IsPastVisit)
                b.Background = Application.Current.Resources["pastVisitBackground"] as SolidColorBrush;
            else
                b.Background = Application.Current.Resources["presentVisitBackground"] as SolidColorBrush;


            b.Content = v.Name;
            //if (v.Note != "")
            //{
            //    b.Content += "\n" + v.Note;
            //}
            b.DataContext = v.VisitId;
            b.Click += new RoutedEventHandler(EventClick);

            ToolTip tt = new ToolTip();
            tt.Content = v.Name + "\n" + v.Phone;
            if (v.Email != "")
            {
                tt.Content += "\n" + v.Email;
            }
            if (v.Note != "")
            {
                tt.Content += "\n" + v.Note;
            }

            b.ToolTip = tt;
            switch (column)
            {
                case 1:
                    StackPanel item = EventsGrid.Children.OfType<StackPanel>().ToList()[1];
                    //Canvas.SetLeft(b, 5);
                    item.Children.Add(b);
                    break;
                case 2:
                    StackPanel item1 = EventsGrid.Children.OfType<StackPanel>().ToList()[2];
                    item1.Children.Add(b);
                    break;
                case 3:
                    StackPanel item2 = EventsGrid.Children.OfType<StackPanel>().ToList()[3];
                    item2.Children.Add(b);
                    break;
                case 4:
                    StackPanel item3 = EventsGrid.Children.OfType<StackPanel>().ToList()[4];
                    item3.Children.Add(b);
                    break;
                case 5:
                    StackPanel item4 = EventsGrid.Children.OfType<StackPanel>().ToList()[5];
                    //item4.Height = (double)GridUnitType.Star;
                    item4.Children.Add(b);
                    break;
                case 6:
                    StackPanel item6 = EventsGrid.Children.OfType<StackPanel>().ToList()[6];
                    item6.Children.Add(b);
                    break;
                case 0:
                    StackPanel item0 = EventsGrid.Children.OfType<StackPanel>().ToList()[0];
                    item0.Children.Add(b);
                    break;
            }
        }

        private void EventsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            GetBookingForThisWeek();
        }
    }
}
