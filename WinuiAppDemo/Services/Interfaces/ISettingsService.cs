using System;
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
        /// Occurs when the settings have been successfully loaded.
        /// </summary>
        event EventHandler? SettingsLoaded;

        /// <summary>
        /// Gets or sets the application settings configuration.
        /// </summary>
        SettingsApp SettingsApp { get; set; }

        /// <summary>
        /// Gets or sets the directory path.
        /// </summary>
        string DirPath { get; set; }

        /// <summary>
        /// Loads the application settings asynchronously from a file in the local storage directory.
        /// </summary>
        /// <returns>A asynchronous task.</returns>
        Task LoadSettingsAsync();

        /// <summary>
        /// Saves the application settings asynchronously to a file in the local storage directory.
        /// </summary>
        /// <returns>A asynchronous task.</returns>
        Task SaveSettingsAsync();
    }
}
