using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NLog;

using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.ViewModels
{
    /// <summary>
    /// Provides functionality for the settings view model of the application.
    /// </summary>
    public partial class SettingsViewModel : ObservableObject
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly ISettingsService _settingsService;

        private string _dirPath = "None";
        private string _selectedTimeFormat = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settingsService">ISettingsService.</param>
        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _settingsService.SettingsLoaded += OnSettingsLoaded;
            SelectedTimeFormat = _settingsService.SettingsApp.TimeFormat;
            DirPath = _settingsService.DirPath;
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
                    _settingsService.SettingsApp.TimeFormat = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the directory path.
        /// </summary>
        public string DirPath
        {
            get => _dirPath;
            set => SetProperty(ref _dirPath, value);
        }

        private void OnSettingsLoaded(object? sender, EventArgs e)
        {
            DirPath = _settingsService.DirPath;
        }

        [RelayCommand]
        private void SaveSettings()
        {
            _settingsService.SaveSettingsAsync();
        }
    }
}
