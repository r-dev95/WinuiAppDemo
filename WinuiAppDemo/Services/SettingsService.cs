using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using NLog;

#if UNPACKAGED
#else
using Windows.Storage;
#endif

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.Services
{
    /// <inheritdoc />
    public class SettingsService : ISettingsService
    {
        private const string FileName = "settingsApp.json";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private SettingsApp _settingsApp = default!;
        private string _dirPath = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        public SettingsService()
        {
            _ = LoadSettingsAsync();
        }

        /// <inheritdoc />
        public event EventHandler? SettingsLoaded;

        /// <inheritdoc />
        public SettingsApp SettingsApp
        {
            get => _settingsApp;
            set
            {
                if (_settingsApp != value)
                {
                    _settingsApp = value;
                }
            }
        }

        /// <inheritdoc />
        public string DirPath
        {
            get => _dirPath;
            set
            {
                if (_dirPath != value)
                {
                    _dirPath = value;
                }
            }
        }

        /// <inheritdoc />
        public async Task LoadSettingsAsync()
        {
            try
            {
#if UNPACKAGED
                DirPath = AppContext.BaseDirectory;
                string fpath = System.IO.Path.Combine(DirPath, FileName);
                string json = await File.ReadAllTextAsync(fpath);
#else
                StorageFolder dpath = ApplicationData.Current.LocalFolder;
                DirPath = dpath.Path;
                StorageFile fpath = await dpath.GetFileAsync(FileName);
                string json = await FileIO.ReadTextAsync(fpath);
#endif
                SettingsApp? loaded = JsonSerializer.Deserialize(json, AppJsonContext.Default.SettingsApp);

                if (loaded != null)
                {
                    SettingsApp = loaded;
                }

                SettingsLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (FileNotFoundException)
            {
                _logger.Warn("Settings file not found. Using default settings.");
                SettingsApp = new SettingsApp();
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Failed to load settings.");
                SettingsApp = new SettingsApp();
            }
        }

        /// <inheritdoc />
        public async Task SaveSettingsAsync()
        {
            try
            {
#if UNPACKAGED
                DirPath = AppContext.BaseDirectory;
                string fpath = System.IO.Path.Combine(DirPath, FileName);
                string json = JsonSerializer.Serialize(_settingsApp, AppJsonContext.Default.SettingsApp);
                await File.WriteAllTextAsync(fpath, json);
#else
                StorageFolder dpath = ApplicationData.Current.LocalFolder;
                DirPath = dpath.Path;
                StorageFile fpath = await dpath.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
                string json = JsonSerializer.Serialize(_settingsApp, AppJsonContext.Default.SettingsApp);
                await FileIO.WriteTextAsync(fpath, json);
#endif
                SettingsLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Failed to save settings.");
            }
        }
    }
}
