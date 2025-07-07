using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.ViewModels;

namespace WinuiAppDemo.Views
{
    /// <summary>
    /// Provides functionality for the settings page of the application.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        /// <param name="viewModel">Settings view model.</param>
        public SettingsPage()
        {
            InitializeComponent();

            ViewModel = App.GetService<SettingsViewModel>();
        }

        /// <summary>
        /// Gets the view model for this class.
        /// </summary>
        public SettingsViewModel ViewModel { get; private set; }
    }
}
