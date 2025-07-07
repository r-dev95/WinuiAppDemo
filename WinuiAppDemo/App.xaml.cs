using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services;
using WinuiAppDemo.Services.Interfaces;
using WinuiAppDemo.ViewModels;
using WinuiAppDemo.Views;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for the main application class.
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices((context, services) =>
                {
                    // Configuration
                    services.Configure<AppSettings>(context.Configuration.GetSection(nameof(AppSettings)));

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
                })
                .Build();
        }

        /// <summary>
        /// Gets the current application host instance.
        /// </summary>
        public IHost Host { get; private set; }

        /// <summary>
        /// Gets the current application window instance.
        /// </summary>
        public Window Window { get; private set; } = default!;

        /// <summary>
        /// Gets a service of type T from the application's service provider.
        /// </summary>
        /// <typeparam name="T">A service.</typeparam>
        /// <returns>A application's service.</returns>
        /// <exception cref="ArgumentException">Occurs when a service is not registered with ConfigureServices.</exception>
        public static T GetService<T>()
        where T : class
        {
            if ((Current as App) !.Host.Services.GetService(typeof(T)) is not T service)
            {
                string msg = $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.";
                _logger.Error(msg);
                throw new ArgumentException(msg);
            }

            return service;
        }

        /// <summary>
        /// Launches the application and initializes the main window.
        /// </summary>
        /// <param name="args">LaunchActivatedEventArgs.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            AppLogging.Configure();

            GetService<ISettingsService>().Load();

            Window = GetService<MainWindow>();
            Window.Activate();
        }
    }
}
