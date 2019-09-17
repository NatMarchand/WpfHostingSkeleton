using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfHostingSkeleton
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(s =>
                {
                    s.AddScoped<MainWindow>();
                    s.AddScoped<MainWindowViewModel>();

                    s.AddSingleton<App>();
                    s.AddSingleton<NavigationService>();
                    s.AddSingleton<IHostedService, AppHostService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}
