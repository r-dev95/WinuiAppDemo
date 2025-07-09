using Microsoft.UI.Xaml;

using NLog;

namespace WinuiAppDemo;

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
    }
}
