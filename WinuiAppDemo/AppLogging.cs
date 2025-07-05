using System.Text;

using NLog;
using NLog.Config;
using NLog.Targets;

#if UNPACKAGED
#else
using Windows.Storage;
#endif

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
#if UNPACKAGED
            string basePath = "${basedir}";
#else
            string basePath = ApplicationData.Current.LocalFolder.Path;
#endif
            LoggingConfiguration config = new ();

            ConsoleTarget consoleTarget = new ("console");

            FileTarget fileTarget = new ("File")
            {
                Layout = "[${longdate}][${threadid:padding=8}][${uppercase:${level:padding=-5}}][${callsite}] - ${message} ${exception:format=@}",
                FileName = basePath + "/logs/${date:format=yyyyMMdd}.log",
                Encoding = Encoding.UTF8,
                ArchiveFileName = basePath + "/logs/archive.{#}.log",
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 7,
            };

            config.AddTarget(consoleTarget);
            config.AddTarget(fileTarget);

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }
    }
}
