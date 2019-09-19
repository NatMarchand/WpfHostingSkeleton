using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfHostingSkeleton
{
    public static class WpfHostingExtensions
    {
        public static IHostBuilder ConfigureWpfApplication<TApplication>(this IHostBuilder hostBuilder) where TApplication: Application
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<TApplication>();
                services.AddSingleton<IHostedService, WpfApplicationHostService<TApplication>>();
            });
        }
    }
}