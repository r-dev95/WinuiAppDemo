using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.ViewModels;

namespace WinuiAppDemo.Views;

/// <summary>
/// Provides functionality for the user page of the application.
/// </summary>
public sealed partial class UserPage : Page
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPage"/> class.
    /// </summary>
    public UserPage()
    {
        InitializeComponent();

        ViewModel = App.GetService<UserViewModel>();
        ViewModel.Startup();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public UserViewModel ViewModel { get; private set; }
}
