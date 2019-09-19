using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfHostingSkeleton
{
    public class WpfApplicationHostService<TApplication> : IHostedService where TApplication: Application
    {
        private readonly Thread _thread;
        private event Action OnStopRequested;

        public WpfApplicationHostService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _thread = new Thread(() =>
            {
                var app = serviceProvider.GetRequiredService<TApplication>();

                void ShutdownApp()
                {
                    app.Dispatcher.Invoke(() => app.Shutdown());
                }

                OnStopRequested += ShutdownApp;

                try
                {
                    app.Run();
                }
                finally
                {
                    OnStopRequested -= ShutdownApp;
                    hostApplicationLifetime.StopApplication();
                }
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            OnStopRequested?.Invoke();
            return Task.CompletedTask;
        }
    }
}