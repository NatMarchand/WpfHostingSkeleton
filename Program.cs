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
                .ConfigureWpfApplication<App>()
                .ConfigureServices(s =>
                {
                    s.AddScoped<MainWindow>();
                    s.AddScoped<MainWindowViewModel>();
                    s.AddSingleton<NavigationService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}
