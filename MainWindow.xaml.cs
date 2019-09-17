using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;

namespace WpfHostingSkeleton
{
    public partial class MainWindow
    {
        private readonly ILogger<MainWindow> _logger;
        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        public MainWindow(string s, MainWindowViewModel viewModel, ILogger<MainWindow> logger)
        {
            _logger = logger;
            InitializeComponent();
            Text.Text = s;
            DataContext = viewModel;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            _logger.LogInformation("Someone clicked!");
            ViewModel.DoSomething();
        }

        private async void DoSomethingAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                await ViewModel.DoSomethingAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Meeeh...");
                MessageBox.Show("Meeh...");
            }
            finally
            {
                if (!ViewModel.IsBusy)
                {
                    Mouse.OverrideCursor = null;
                }
            }
        }
    }

    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ILogger<MainWindowViewModel> _logger;
        private string _data;

        public string Data
        {
            get => _data;
            protected set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
        {
            _logger = logger;
        }

        public void DoSomething()
        {
            Data = DateTime.Now.ToString("O");
        }

        public async Task DoSomethingAsync()
        {
            BusyCount++;
            await Task.Delay(TimeSpan.FromSeconds(2));
            DoSomething();
            _logger.LogInformation("Logging is easy for me !");
            BusyCount--;
        }
    }

    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        public DesignMainWindowViewModel()
            : base(null)
        {
            Data = DateTime.UtcNow.ToString("O");
        }
    }
}
