using System;
using System.Collections.Generic;
using System.Globalization;
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
using RestaurantDesk.UserControls;

namespace RestaurantDesk.UserControls
{
    /// <summary>
    /// Interaction logic for MonthView.xaml
    /// </summary>
    public partial class MonthView : UserControl
    {

        internal DateTime _DisplayStartDate ;
        private int _DisplayMonth;
        private int _DisplayYear;
        private System.Globalization.CultureInfo _cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.LCID);
        System.Globalization.Calendar sysCal;
        private List<Appointment> _monthAppointments;

        public event DisplayMonthChangedEventHandler DisplayMonthChanged;

        public delegate void DisplayMonthChangedEventHandler(MonthChangedEventArgs e);
        public event DayBoxDoubleClickedEventHandler DayBoxDoubleClicked;

        public delegate void DayBoxDoubleClickedEventHandler(NewAppointmentEventArgs e);
        public event AppointmentDblClickedEventHandler AppointmentDblClicked;

        public delegate void AppointmentDblClickedEventHandler(int Appointment_Id);

        public DateTime DisplayStartDate
        {
            get
            {
                return _DisplayStartDate;
            }
            set
            {
                _DisplayStartDate = value;
                _DisplayMonth = _DisplayStartDate.Month;
                
                _DisplayYear = _DisplayStartDate.Year;
            }
        }

        internal List<global::Appointment> MonthAppointments
        {
            get
            {
                return _monthAppointments;
            }
            set
            {
                _monthAppointments = value;
                BuildCalendarUI();
            }
        }

        public MonthView()
        {
            sysCal = _cultureInfo.Calendar;
            DisplayStartDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1));
            InitializeComponent();
            MonthYearLabel.Content = _DisplayStartDate.ToString("MMMM - yyyy");
        }

        private void MonthView_Loaded(object sender, RoutedEventArgs e)
        {
            // -- Want to have the calendar show up, even if no appoints are assigned 
            // Note - in my own app, appointments are loaded by a backgroundWorker thread to avoid a laggy UI
            if (_monthAppointments is null)
                BuildCalendarUI();
        }

        private void BuildCalendarUI()
        {
            int iDaysInMonth = 7;// sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month);
            int iOffsetDays = Convert.ToInt16(Enum.ToObject(typeof(DayOfWeek), (int)_DisplayStartDate.DayOfWeek));
            int iWeekCount = 0;
            var weekRowCtrl = new WeekOfDaysControls();

            MonthViewGrid.Children.Clear();
            AddRowsToMonthGrid(iDaysInMonth, iOffsetDays);
            MonthYearLabel.Content = Microsoft.VisualBasic.DateAndTime.MonthName(_DisplayMonth) + " " + _DisplayYear;

            for (int i = 1, loopTo = iDaysInMonth; i <= loopTo; i++)
            {
                if (i != 1 && Math.IEEERemainder(i + iOffsetDays - 1, 7d) == 0d)
                {
                    // -- add existing weekrowcontrol to the monthgrid
                    Grid.SetRow(weekRowCtrl, iWeekCount);
                    MonthViewGrid.Children.Add(weekRowCtrl);
                    // -- make a new weekrowcontrol
                    weekRowCtrl = new WeekOfDaysControls();
                    iWeekCount += 1;
                }

                // -- load each weekrow with a DayBoxControl whose label is set to day number
                var dayBox = new DayBoxControl();
                dayBox.DayNumberLabel.Content = i.ToString();
                dayBox.Tag = i;
                dayBox.MouseDoubleClick += DayBox_DoubleClick;

                // -- customize daybox for today:
                if (new DateTime(_DisplayYear, _DisplayMonth, i) == DateTime.Today)
                {
                    dayBox.DayLabelRowBorder.Background = (Brush)dayBox.TryFindResource("OrangeGradientBrush");
                    dayBox.DayAppointmentsStack.Background = Brushes.Wheat;
                }

                // -- for design mode, add appointments to random days for show...
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    if ((double)Microsoft.VisualBasic.VBMath.Rnd(1f) < 0.25d)
                    {
                        var apt = new DayBoxAppointmentControl();
                        apt.DisplayText.Text = "Apt on " + i + "th";
                        dayBox.DayAppointmentsStack.Children.Add(apt);
                    }
                }

                else if (_monthAppointments is not null)
                {
                    // -- Compiler warning about unpredictable results if using i (the iterator) in lambda, the 
                    // "hint" suggests declaring another var and set equal to iterator var
                    int iday = i;
                    var aptInDay = _monthAppointments.FindAll(new Predicate<global::Appointment>((apt) => apt.StartTime.Value.Day == iday));

                    foreach (global::Appointment a in aptInDay)
                    {
                        var apt = new DayBoxAppointmentControl();
                        apt.DisplayText.Text = a.Subject;
                        apt.Tag = a.AppointmentID;
                        apt.MouseDoubleClick += Appointment_DoubleClick;
                        dayBox.DayAppointmentsStack.Children.Add(apt);
                    }

                }

                Grid.SetColumn(dayBox, i - iWeekCount * 7 + iOffsetDays);
                weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
            }
            Grid.SetRow(weekRowCtrl, iWeekCount);
            MonthViewGrid.Children.Add(weekRowCtrl);
        }

        private void AddRowsToMonthGrid(int DaysInMonth, int OffSetDays)
        {
            MonthViewGrid.RowDefinitions.Clear();
            var rowHeight = new System.Windows.GridLength(60, System.Windows.GridUnitType.Star);

            int EndOffSetDays = 7 - (Convert.ToInt16(Enum.ToObject(typeof(DayOfWeek), (int)_DisplayStartDate.AddDays(DaysInMonth - 1).DayOfWeek)) + 1);

            for (int i = 1, loopTo = (int)Math.Abs((DaysInMonth + OffSetDays + EndOffSetDays) / 7d); i <= loopTo; i++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = rowHeight;
                MonthViewGrid.RowDefinitions.Add(rowDef);
            }
        }

        private void UpdateMonth(int MonthsToAdd)
        {
            var ev = new MonthChangedEventArgs();
            ev.OldDisplayStartDate = _DisplayStartDate;
            DisplayStartDate = _DisplayStartDate.AddMonths(MonthsToAdd);
            ev.NewDisplayStartDate = _DisplayStartDate;
            DisplayMonthChanged?.Invoke(ev);
        }

        #region  UI Event Handlers 

        public void MonthGoPrev_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(-1);
        }

        public void MonthGoNext_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(1);
        }

        public void Appointment_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (object.ReferenceEquals(e.Source.GetType, typeof(DayBoxAppointmentControl)))
            {
                if (((DayBoxAppointmentControl)e.Source).Tag is not null)
                {
                    // -- You could put your own call to your appointment-displaying code or whatever here..
                    AppointmentDblClicked?.Invoke((int)((DayBoxAppointmentControl)e.Source).Tag);
                }
                e.Handled = true;
            }
        }

        private void DayBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // -- call to FindVisualAncestor to make sure they didn't click on existing appointment (in which case,
            // that appointment window is already opened by handler Appointment_DoubleClick)
            if (object.ReferenceEquals(e.Source.GetType, typeof(DayBoxControl)) 
                && Utilities.FindVisualAncestor(typeof(DayBoxAppointmentControl), (Visual)e.OriginalSource) is null)
            {

                var ev = new NewAppointmentEventArgs();
                if (((DayBoxControl)e.Source).Tag is not null)
                {
                    ev.StartDate = new DateTime(_DisplayYear, _DisplayMonth, (int)((DayBoxControl)e.Source).Tag, 10, 0, 0);
                    ev.EndDate = ev.StartDate.Value.AddHours(2d);
                }
                DayBoxDoubleClicked?.Invoke(ev);
                e.Handled = true;
            }

        }
    }
    #endregion
}

public struct MonthChangedEventArgs
{
    public DateTime OldDisplayStartDate;
    public DateTime NewDisplayStartDate;
}

public struct NewAppointmentEventArgs
{
    public DateTime? StartDate;
    public DateTime? EndDate;
    public int? CandidateId;
    public int? RequirementId;
}

class Utilities
{
    // -- Many thanks to Bea Stollnitz, on whose blog I found the original C# version of below in a drag-drop helper class... 
    public static FrameworkElement FindVisualAncestor(Type ancestorType, System.Windows.Media.Visual visual)

    {

        while (visual is not null && !ancestorType.IsInstanceOfType(visual))
            visual = (System.Windows.Media.Visual)System.Windows.Media.VisualTreeHelper.GetParent(visual);
        return (FrameworkElement)visual;
    }

}
