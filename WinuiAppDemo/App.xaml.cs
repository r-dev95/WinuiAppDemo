using Microsoft.UI.Xaml;

using NLog;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for the main application class.
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the current application window instance.
        /// </summary>
        public Window Window { get; private set; } = default!;

        /// <summary>
        /// Launches the application and initializes services.
        /// </summary>
        /// <param name="args">LaunchActivatedEventArgs.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            AppLogging.Configure();
            AppServices.ConfigureServices();

            Window = new Views.MainWindow();
            Window.Activate();
        }
    }
}
