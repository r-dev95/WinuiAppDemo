using Microsoft.UI.Xaml.Controls;

using NLog;

namespace WinuiAppDemo.Views
{
    /// <summary>
    /// Provides functionality for the settings page of the application.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPage"/> class.
        /// </summary>
        public UserPage()
        {
            InitializeComponent();
        }
    }
}
