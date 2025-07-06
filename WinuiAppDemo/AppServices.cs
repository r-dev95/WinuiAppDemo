using System;

using Microsoft.Extensions.DependencyInjection;

using NLog;

using WinuiAppDemo.Services;
using WinuiAppDemo.Services.Interfaces;
using WinuiAppDemo.ViewModels;
using WinuiAppDemo.Views;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for configuring and accessing the application's services.
    /// </summary>
    public static class AppServices
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets the application's service provider.
        /// </summary>
        public static IServiceProvider Services { get; private set; } = default!;

        /// <summary>
        /// Cinfigures the application's services and sets up dependency injection.
        /// </summary>
        public static void Configure()
        {
            ServiceCollection services = new ();

            // Service
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IClockService, ClockService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModel and View
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<UserPage>();

            Services = services.BuildServiceProvider();
        }
    }
}
