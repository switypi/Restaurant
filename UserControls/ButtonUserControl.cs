using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RestaurantDesk.UserControls
{
    public class ButtonUserControl : Button
    {
        public ButtonUserControl()
        {
            
        }


        public static readonly DependencyProperty IsBookedProperty =
        DependencyProperty.Register("IsBooked", typeof(bool), typeof(ButtonUserControl),
       new PropertyMetadata(false, IsBookedPropertyChanged));


        public static readonly DependencyProperty IsBusyProperty =
        DependencyProperty.Register("IsBusy", typeof(bool), typeof(ButtonUserControl),
        new PropertyMetadata(false, IsBusyPropertyChanged));

       
        

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

        private void IsBookedPropertyChanged(bool val)
        {
            //...
        }

     
        private static void IsBookedPropertyChanged(
           DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonUserControl)d).IsBookedPropertyChanged((bool)e.NewValue);
        }

        private static void IsBusyPropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonUserControl)d).IsBusyPropertyChanged((bool)e.NewValue);
        }

      
    }
}
