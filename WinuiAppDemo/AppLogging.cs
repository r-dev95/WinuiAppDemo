using System.Text;

using Microsoft.Extensions.Options;

using NLog;
using NLog.Config;
using NLog.Targets;

using WinuiAppDemo.Models;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for configuring application logging using NLog.
    /// </summary>
    public class AppLogging
    {
        private readonly AppSettings _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLogging"/> class.
        /// </summary>
        /// <param name="options">AppSettings options.</param>
        public AppLogging(IOptions<AppSettings> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Configures the NLog logging settings for the application.
        /// </summary>
        public void Configure()
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
                ArchiveEvery = _options.LogArchiveEvery,
                MaxArchiveFiles = _options.LogMaxArchiveFiles,
                Layout = layout,
            };

            config.AddTarget(fileTarget);
            config.AddRule(_options.LogMinLevel, _options.LogMaxLevel, fileTarget);
            LogManager.Configuration = config;
        }
    }
}
