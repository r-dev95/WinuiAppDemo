using System.Text;

using NLog;
using NLog.Config;
using NLog.Targets;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for configuring application logging using NLog.
    /// </summary>
    public static class AppLogging
    {
        /// <summary>
        /// Configures the NLog logging settings for the application.
        /// </summary>
        public static void Configure()
        {
            string fName = App.DPath + "/logs/${date:format=yyyyMMdd}.log";
            string archiveFName = App.DPath + "/logs/archive.${date:format=yyyyMMdd}.log"; // "/logs/archive.{#}.log"
            string layout = "[${longdate}]"
                + "[${threadid:padding=5}]"
                + "[${uppercase:${level:padding=-5}}]"
                + "[${callsite}]"
                + " - ${message}"
                + " - ${exception:format=@}";

            LoggingConfiguration config = new ();

            FileTarget fileTarget = new ("file")
            {
                Encoding = Encoding.UTF8,
                FileName = fName,
                ArchiveFileName = archiveFName,
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 7,
                Layout = layout,
            };

            config.AddTarget(fileTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
        }
    }
}
