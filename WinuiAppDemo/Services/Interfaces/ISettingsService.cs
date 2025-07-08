using System.Threading.Tasks;

using WinuiAppDemo.Models;

namespace WinuiAppDemo.Services.Interfaces
{
    /// <summary>
    /// Provides functionality for managing application settings.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets or sets the application settings configuration.
        /// </summary>
        UserSettings UserSettings { get; set; }

        /// <summary>
        /// Loads the application settings asynchronously from a file in the local storage directory.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves the application settings asynchronously to a file in the local storage directory.
        /// </summary>
        /// <returns>A asynchronous task.</returns>
        Task SaveAsync();
    }
}
