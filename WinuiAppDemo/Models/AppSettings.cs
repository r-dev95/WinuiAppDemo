using NLog;
using NLog.Targets;

namespace WinuiAppDemo.Models
{
    /// <summary>
    /// Defines application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the user settings file name.
        /// </summary>
        public string UserSettingsFileNmae { get; set; } = "UserSettings.json";

        /// <summary>
        /// Gets or sets the logging min level.
        /// </summary>
        public LogLevel LogMinLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// Gets or sets the logging max level.
        /// </summary>
        public LogLevel LogMaxLevel { get; set; } = LogLevel.Fatal;

        /// <summary>
        /// Gets or sets the logging archive every.
        /// </summary>
        public FileArchivePeriod LogArchiveEvery { get; set; } = FileArchivePeriod.Day;

        /// <summary>
        /// Gets or sets the number of logging archive files.
        /// </summary>
        public int LogMaxArchiveFiles { get; set; } = 7;
    }
}
