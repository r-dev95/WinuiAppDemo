using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Options;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.ViewModels
{
    /// <summary>
    /// Provides functionality for the main view model of the application.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly AppSettings _options;
        private readonly IClockService _clockService;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;

        private readonly DispatcherTimer _timer = new ();
        private string _clockText = string.Empty;
        private ObservableCollection<NavItem> _navItems = [];
        private NavItem _selectedItem = default!;
        private bool _selectedTheme;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="clockService">The Clock service.</param>
        /// <param name="navigationService">The Navigation service.</param>
        /// <param name="settingsService">The Settings service.</param>
        public MainViewModel(IOptions<AppSettings> options, IClockService clockService, INavigationService navigationService, ISettingsService settingsService)
        {
            _options = options.Value;

            // ------------------------------------------------------
            // Setup Settings Service.
            // ------------------------------------------------------
            _settingsService = settingsService;
            SelectedTheme = _settingsService.UserSettings.Theme == ElementTheme.Dark; // Do not set SelectedTheme here.

            // ------------------------------------------------------
            // Setup Clock Service.
            // ------------------------------------------------------
            _clockService = clockService;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateClock;
            _timer.Start();

            // ------------------------------------------------------
            // Setup Navigation Service.
            // ------------------------------------------------------
            _navigationService = navigationService;
            foreach (NavItem item in _navigationService.GetNavItems())
            {
                NavItems.Add(item);
            }

            _navigationService.Navigate(NavItems[0].Tag);
            SelectedItem = NavItems[0];
        }

        /// <summary>
        /// Gets or sets a value indicating whether the selected theme is dark.
        /// </summary>
        public bool SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    _settingsService.UserSettings.Theme = value ? ElementTheme.Dark : ElementTheme.Light;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current clock text.
        /// </summary>
        public string ClockText
        {
            get => _clockText;
            set => SetProperty(ref _clockText, value);
        }

        /// <summary>
        /// Gets or sets the collection of navigation items.
        /// </summary>
        public ObservableCollection<NavItem> NavItems
        {
            get => _navItems;
            set => SetProperty(ref _navItems, value);
        }

        /// <summary>
        /// Gets or sets the currently selected navigation item.
        /// </summary>
        public NavItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        /// <summary>
        /// Navigates to the invoked item in the NavigationView.
        /// </summary>
        /// <param name="_">NavigationView.</param>
        /// <param name="args">NavigationViewItemInvokedEventArgs.</param>
        public void ItemInvoked(NavigationView _, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.Frame!.Navigate(_navigationService.SettingsPageType);
                return;
            }

            if (args.InvokedItem.ToString() is string tag)
            {
                _navigationService.Navigate(tag);
            }
        }

        /// <summary>
        /// Back navigation handler for the NavigationView control.
        /// </summary>
        /// <param name="sender">NavigationView.</param>
        /// <param name="_">NavigationViewBackRequestedEventArgs.</param>
        public void BackHandler(NavigationView sender, NavigationViewBackRequestedEventArgs _)
        {
            _navigationService.GoBack();

            if (_navigationService.Frame.CurrentSourcePageType == _navigationService.SettingsPageType)
            {
                sender.SelectedItem = sender.SettingsItem;
                return;
            }

            SelectedItem = _navigationService.GetCurrentFrameType();
        }

        /// <summary>
        /// Changes the application theme.
        /// </summary>
        /// <param name="_">Sender.</param>
        /// <param name="__"> RoutedEventArgs.</param>
        public void ChangeThemeLoaded(object? _, RoutedEventArgs? __)
        {
            bool isDark = SelectedTheme;

            if ((App.Current as App) !.Window.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = isDark ? ElementTheme.Dark : ElementTheme.Light;
            }

            AppWindowTitleBar titleBar = (App.Current as App) !.Window.AppWindow.TitleBar;
            titleBar.ButtonForegroundColor = isDark ? Colors.White : Colors.Black;
            titleBar.ButtonHoverBackgroundColor = isDark ? Colors.DimGray : Colors.LightGray;
            titleBar.ButtonHoverForegroundColor = isDark ? Colors.White : Colors.Black;
        }

        /// <summary>
        /// Changes the application theme.
        /// </summary>
        [RelayCommand]
        public void ChangeTheme()
        {
            ChangeThemeLoaded(null, null);
        }

        private void UpdateClock(object? sender, object e)
        {
            ClockText = _clockService.GetCurrentTime().ToString(_settingsService.UserSettings.TimeFormat);
        }
    }
}
