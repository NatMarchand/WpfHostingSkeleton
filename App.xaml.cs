using System;
using System.Windows;

namespace WpfHostingSkeleton
{
    public partial class App
    {
        private readonly NavigationService _navigationService;

        private App()
        {

        }

        public App(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationService.ShowWindow<MainWindow>(DateTime.UtcNow.ToString());
            base.OnStartup(e);
        }
    }
}
