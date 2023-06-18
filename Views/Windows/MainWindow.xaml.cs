using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace RestaurantDesk.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public ViewModels.MainWindowViewModel ViewModel
        {
            get;
        }

        public MainWindow(ViewModels.MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            this.DataContext = this;

            InitializeComponent();
            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);

            

            Application.Current.LoadCompleted += Current_LoadCompleted;
        
        }

        private void Current_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.Background = Application.Current.Resources["WindowMenuBackground"] as SolidColorBrush;
           // this.RootNavigation.FontSize = 15;
           // (this.RootNavigation.Items[1] as NavigationItem).fi = 15;
        }

        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

      
    }
}