using System;
using System.Diagnostics;
using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Options;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.ViewModels;

/// <summary>
/// Provides functionality for the settings view model of the application.
/// </summary>
public partial class SettingsViewModel : ObservableObject
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly AppSettings _options;

    private readonly ISettingsService _settingsService;

    private readonly string _dPath = App.DPath;

    private string _selectedTimeFormat = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// /// <param name="options">The options.</param>
    /// <param name="settingsService">ISettingsService.</param>
    public SettingsViewModel(IOptions<AppSettings> options, ISettingsService settingsService)
    {
        _options = options.Value;
        _settingsService = settingsService;
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
    /// Starts up the view model.
    /// </summary>
    public void Startup()
    {
        SelectedTimeFormat = _settingsService.UserSettings.TimeFormat;
    }

    [RelayCommand]
    private void OpenExplorer()
    {
        try
        {
            if (Directory.Exists(_dPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _dPath,
                    UseShellExecute = true,
                });
            }
            else
            {
                _logger.Warn($"Directory does not exist: {_dPath}");
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to open directory in Explorer.");
        }
    }

    [RelayCommand]
    private void SaveSettings()
    {
        _settingsService.SaveAsync();
    }
}
