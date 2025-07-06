using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

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
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            INavigationService navService = AppServices.Services!.GetRequiredService<INavigationService>();
            navService.Register(new NavItem { Name = "Home", Tag = "Home", Icon = Symbol.Home, PageType = typeof(HomePage) });
            navService.Register(new NavItem { Name = "User", Tag = "User", Icon = Symbol.OtherUser, PageType = typeof(UserPage) });
            navService.SettingsPageType = typeof(SettingsPage);
            navService.Frame = navigationFrame;

            ViewModel = AppServices.Services!.GetRequiredService<MainViewModel>();
        }

        /// <summary>
        /// Gets the view model for this class.
        /// </summary>
        public MainViewModel ViewModel { get; private set; }

        /// <summary>
        /// Changes the application theme based on the toggle switch state.
        /// </summary>
        /// <param name="sender">ToggleButton.</param>
        /// <param name="args">RoutedEventArgs.</param>
        public void ChangeTheme(object sender, RoutedEventArgs args)
        {
            if (sender is ToggleButton toggleButton)
            {
                bool isDark = toggleButton.IsChecked ?? false;

                if (Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = isDark ? ElementTheme.Dark : ElementTheme.Light;
                }

                nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                AppWindowTitleBar titleBar = AppWindow.GetFromWindowId(windowId).TitleBar;

                titleBar.ButtonForegroundColor = isDark ? Colors.White : Colors.Black;
                titleBar.ButtonHoverBackgroundColor = isDark ? Colors.DimGray : Colors.LightGray;
                titleBar.ButtonHoverForegroundColor = isDark ? Colors.White : Colors.Black;
            }
        }
    }
}
