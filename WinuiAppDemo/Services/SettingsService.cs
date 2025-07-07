using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IOptions<AppSettings> _options;

        private readonly string _fName = "userSettings.json";

        private UserSettings _userSettings = default!;

#if UNPACKAGED
        private string _dirPath = AppContext.BaseDirectory;
#else
        private string _dirPath = ApplicationData.Current.LocalFolder.Path
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="options">AppSettings options.</param>
        public SettingsService(IOptions<AppSettings> options)
        {
            _options = options;
            _fName = _options.Value.UserSettingsFileNmae;
        }

        /// <inheritdoc />
        public UserSettings UserSettings
        {
            get => _userSettings;
            set
            {
                if (_userSettings != value)
                {
                    _userSettings = value;
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
        public void Load()
        {
            try
            {
                string fpath = Path.Combine(DirPath, _fName);
                string json = File.ReadAllText(fpath);
                UserSettings? data = JsonSerializer.Deserialize(json, AppJsonContext.Default.UserSettings);

                if (data != null)
                {
                    UserSettings = data;
                }
            }
            catch (FileNotFoundException)
            {
                _logger.Warn("Settings file not found. Using default settings.");
                UserSettings = new UserSettings();
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Failed to load settings.");
                UserSettings = new UserSettings();
            }
        }

        /// <inheritdoc />
        public async Task SaveAsync()
        {
            try
            {
                string fpath = Path.Combine(DirPath, _fName);
                string json = JsonSerializer.Serialize(_userSettings, AppJsonContext.Default.UserSettings);
                await File.WriteAllTextAsync(fpath, json);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Failed to save settings.");
            }
        }
    }
}
