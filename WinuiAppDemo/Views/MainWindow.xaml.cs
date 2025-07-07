using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;
using WinuiAppDemo.ViewModels;

namespace WinuiAppDemo.Views
{
    /// <summary>
    /// Provides functionality for the main window of the application.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">Main view model.</param>
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            INavigationService navService = App.GetService<INavigationService>();
            navService.Register(new NavItem { Name = "Home", Tag = "Home", Icon = Symbol.Home, PageType = typeof(HomePage) });
            navService.Register(new NavItem { Name = "User", Tag = "User", Icon = Symbol.OtherUser, PageType = typeof(UserPage) });
            navService.SettingsPageType = typeof(SettingsPage);
            navService.Frame = navigationFrame;

            ViewModel = App.GetService<MainViewModel>();
        }

        /// <summary>
        /// Gets the view model for this class.
        /// </summary>
        public MainViewModel ViewModel { get; private set; }
    }
}
