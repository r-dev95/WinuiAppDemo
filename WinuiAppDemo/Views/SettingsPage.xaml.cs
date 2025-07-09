using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.ViewModels;

namespace WinuiAppDemo.Views;

/// <summary>
/// Provides functionality for the settings page of the application.
/// </summary>
public sealed partial class SettingsPage : Page
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    public SettingsPage()
    {
        InitializeComponent();

        ViewModel = App.GetService<SettingsViewModel>();
        ViewModel.Startup();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public SettingsViewModel ViewModel { get; private set; }
}
