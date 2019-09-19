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

        public TWindow ShowWindow<TWindow>(params object[] parameters) where TWindow : Window
        {
            return ShowWindow<TWindow>(null, parameters);
        }

        public TWindow ShowWindow<TWindow>(Action<TWindow> configureWindow, params object[] parameters) where TWindow : Window
        {
            var scope = _scopeFactory.CreateScope();
            var window = ActivatorUtilities.CreateInstance<TWindow>(scope.ServiceProvider, parameters);
            void DisposeScope(object o, EventArgs e)
            {
                scope.Dispose();
                window.Closed -= DisposeScope;
            }
            configureWindow?.Invoke(window);
            window.Closed += DisposeScope;
            window.Show();

            return window;
        }

        public bool? ShowDialog<TWindow>(params object[] parameters) where TWindow : Window
        {
            return ShowDialog<TWindow>(null, parameters);
        }

        public bool? ShowDialog<TWindow>(Action<TWindow> configureWindow = null, params object[] parameters) where TWindow : Window
        {
            var scope = _scopeFactory.CreateScope();
            var window = ActivatorUtilities.CreateInstance<TWindow>(scope.ServiceProvider, parameters);
            void DisposeScope(object o, EventArgs e)
            {
                scope.Dispose();
                window.Closed -= DisposeScope;
            }
            configureWindow?.Invoke(window);
            window.Closed += DisposeScope;
            return window.ShowDialog();
        }
    }
}