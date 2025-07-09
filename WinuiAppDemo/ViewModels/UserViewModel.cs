using Microsoft.Extensions.Options;

using NLog;

using WinuiAppDemo.Models;

namespace WinuiAppDemo.ViewModels;

/// <summary>
/// Provides functionality for the settings view model of the application.
/// </summary>
public partial class UserViewModel
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly AppSettings _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserViewModel"/> class.
    /// </summary>
    /// /// <param name="options">The options.</param>
    public UserViewModel(IOptions<AppSettings> options)
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
