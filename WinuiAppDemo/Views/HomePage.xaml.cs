using Microsoft.UI.Xaml.Controls;

using NLog;

namespace WinuiAppDemo.Views
{
    /// <summary>
    /// Provides functionality for the home page of the application.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
