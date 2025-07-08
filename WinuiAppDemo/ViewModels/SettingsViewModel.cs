using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Options;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.ViewModels
{
    /// <summary>
    /// Provides functionality for the settings view model of the application.
    /// </summary>
    public partial class SettingsViewModel : ObservableObject
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IOptions<UserSettings> _options;
        private readonly ISettingsService _settingsService;

        private string _dPath = App.DPath;
        private string _selectedTimeFormat = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// /// <param name="options">The options.</param>
        /// <param name="settingsService">ISettingsService.</param>
        public SettingsViewModel(IOptions<UserSettings> options, ISettingsService settingsService)
        {
            _options = options;

            _settingsService = settingsService;
            SelectedTimeFormat = _settingsService.UserSettings.TimeFormat;
            DPath = _settingsService.DPath;
        }

        /// <summary>
        /// Gets or sets the selected time format.
        /// </summary>
        public string SelectedTimeFormat
        {
            get => _selectedTimeFormat;
            set
            {
                if (SetProperty(ref _selectedTimeFormat, value))
                {
                    _settingsService.UserSettings.TimeFormat = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the directory path.
        /// </summary>
        public string DPath
        {
            get => _dPath;
            set => SetProperty(ref _dPath, value);
        }

        [RelayCommand]
        private void SaveSettings()
        {
            _settingsService.SaveAsync();
        }
    }
}
