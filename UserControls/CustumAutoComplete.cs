using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RestaurantDesk.Models;

namespace RestaurantDesk.UserControls
{
    public class CustumAutoComplete : HandyControl.Controls.AutoCompleteTextBox
    {

        public CustumAutoComplete()
        {

        }

        public static readonly DependencyProperty SearchWithProperty =
        DependencyProperty.Register("SearchWith", typeof(SearchWithEnum), typeof(CustumAutoComplete),
       new PropertyMetadata(SearchWithEnum.Default, SearchWithPropertyChanged));

        public SearchWithEnum SearchWith
        {
            get { return (SearchWithEnum)GetValue(SearchWithProperty); }
            set { SetValue(SearchWithProperty, value); }
        }

        private void SearchWithPropertyChanged(SearchWithEnum val)
        {
            //...
        }


        private static void SearchWithPropertyChanged(
          DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustumAutoComplete)d).SearchWithPropertyChanged((SearchWithEnum)e.NewValue);
        }


        public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.Register("SearchText", typeof(string), typeof(CustumAutoComplete),
       new PropertyMetadata("", SearchTextPropertyChanged));

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        private void SearchTextPropertyChanged(string val)
        {
            //...
        }


        private static void SearchTextPropertyChanged(
          DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustumAutoComplete)d).SearchTextPropertyChanged((string)e.NewValue);
        }

    }
}
