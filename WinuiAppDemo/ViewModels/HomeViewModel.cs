using Microsoft.Extensions.Options;

using NLog;

using WinuiAppDemo.Models;

namespace WinuiAppDemo.ViewModels;

/// <summary>
/// Provides functionality for the home view model of the application.
/// </summary>
public partial class HomeViewModel
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly AppSettings _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
    /// </summary>
    /// /// <param name="options">The options.</param>
    public HomeViewModel(IOptions<AppSettings> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Starts up the view model.
    /// </summary>
    public void Startup()
    {
    }
}
