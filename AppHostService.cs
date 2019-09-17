using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfHostingSkeleton
{
    public class AppHostService : IHostedService
    {
        private readonly Thread _thread;
        private event Action OnStopRequested;

        public AppHostService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _thread = new Thread(() =>
            {
                var app = serviceProvider.GetRequiredService<App>();

                void ShutdownApp()
                {
                    app.Dispatcher.Invoke(() =>app.Shutdown());
                    OnStopRequested -= ShutdownApp;
                }

                void StopApplication(object sender, EventArgs e)
                {
                    OnStopRequested -= ShutdownApp;
                    app.Exit -= StopApplication;
                    hostApplicationLifetime.StopApplication();
                }

                OnStopRequested += ShutdownApp;
                app.Exit += StopApplication;

                app.Run();
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