using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RestaurantDesk.Models;

namespace RestaurantDesk.UserControls
{
    public class TableUserControl : System.Windows.Controls.RadioButton
    {
        public int TableId { get; set; }
        public string TableNum { get; set; }
        public string Status { get; set; }
       


        public TableUserControl() { }

        public static readonly DependencyProperty IsBookedProperty =
        DependencyProperty.Register("IsBooked", typeof(bool), typeof(TableUserControl),
       new PropertyMetadata(false, IsBookedPropertyChanged));

        public static readonly DependencyProperty TableTypeProperty =
        DependencyProperty.Register("TableType", typeof(TableTypeEnum), typeof(TableUserControl),
       new PropertyMetadata(TableTypeEnum.Rectangle, TableTypePropertyChanged));


        public static readonly DependencyProperty IsBusyProperty =
        DependencyProperty.Register("IsBusy", typeof(bool), typeof(TableUserControl),
        new PropertyMetadata(false, IsBusyPropertyChanged));

        public static readonly DependencyProperty SelectedTableProperty =
        DependencyProperty.Register("SelectedTable", typeof(Table), typeof(TableUserControl),
        new PropertyMetadata(null, SelectedTablePropertyChanged));

        public TableTypeEnum TableType
        {
            get { return (TableTypeEnum)GetValue(TableTypeProperty); }
            set { SetValue(TableTypeProperty, value); }
        }

        public Table? SelectedTable
        {
            get { return (Table)GetValue(SelectedTableProperty); }
            set { SetValue(SelectedTableProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public bool IsBooked
        {
            get { return (bool)GetValue(IsBookedProperty); }
            set { SetValue(IsBookedProperty, value); }
        }

        private void IsBusyPropertyChanged(bool val)
        {
            //...
        }

        private void TableTypePropertyChanged(TableTypeEnum val)
        {
            //...
        }


        private void IsBookedPropertyChanged(bool val)
        {
            //...
        }

        private void SelectedTablePropertyChanged(Table val)
        {
            //...
        }

        private static void TableTypePropertyChanged(
          DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TableUserControl)d).TableTypePropertyChanged((TableTypeEnum)e.NewValue);
        }

        private static void IsBookedPropertyChanged(
           DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TableUserControl)d).IsBookedPropertyChanged((bool)e.NewValue);
        }

        private static void IsBusyPropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TableUserControl)d).IsBusyPropertyChanged((bool)e.NewValue);
        }

        private static void SelectedTablePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TableUserControl)d).SelectedTablePropertyChanged((Table)e.NewValue);
        }
    }
}
