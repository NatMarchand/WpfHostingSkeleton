using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WpfHostingSkeleton
{
    public class NavigationService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public NavigationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void ShowWindow<TWindow>(params object[] parameters) where TWindow : Window
        {
            var scope = _scopeFactory.CreateScope();
            var window = ActivatorUtilities.CreateInstance<TWindow>(scope.ServiceProvider, parameters);
            void DisposeScope(object o, EventArgs e)
            {
                scope.Dispose();
                window.Closed -= DisposeScope;
            }
            window.Closed += DisposeScope;
            window.Show();
        }
    }
}