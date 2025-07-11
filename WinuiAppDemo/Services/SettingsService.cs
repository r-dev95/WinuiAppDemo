using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.Services;

/// <inheritdoc />
public class SettingsService : ISettingsService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly AppSettings _options;

    private readonly string _fName;

    private readonly string _dPath = App.DPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsService"/> class.
    /// </summary>
    /// <param name="options">AppSettings options.</param>
    public SettingsService(IOptions<AppSettings> options)
    {
        _options = options.Value;
        _fName = _options.UserSettingsFileNmae;
    }

    /// <inheritdoc />
    public UserSettings UserSettings { get; set; } = new ();

    /// <inheritdoc />
    public void Load()
    {
        try
        {
            string fpath = Path.Combine(_dPath, _fName);
            string json = File.ReadAllText(fpath);
            UserSettings? data = JsonSerializer.Deserialize(json, AppJsonContext.Default.UserSettings);

            if (data != null)
            {
                UserSettings = data;
            }

            _logger.Debug($"Loading successful. _dPath: {_dPath}, _fName: {_fName}");
        }
        catch (FileNotFoundException)
        {
            UserSettings = new UserSettings();
            _logger.Warn("Settings file not found. Using default settings.");
        }
        catch (Exception ex)
        {
            UserSettings = new UserSettings();
            _logger.Error(ex, "Failed to load settings.");
        }
    }

    /// <inheritdoc />
    public async Task SaveAsync()
    {
        try
        {
            Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            string fpath = Path.Combine(_dPath, _fName);
            string json = JsonSerializer.Serialize(UserSettings, AppJsonContext.Indented.UserSettings);
            await File.WriteAllTextAsync(fpath, json, encoding);
            _logger.Debug($"Successful save. _dPath: {_dPath}, _fName: {_fName}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to save settings.");
        }
    }
}
