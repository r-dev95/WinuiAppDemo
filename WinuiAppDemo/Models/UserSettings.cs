using Microsoft.UI.Xaml;

namespace WinuiAppDemo.Models
{
    /// <summary>
    /// Defines user settings for the application.
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Gets or sets the app theme.
        /// <list type="bullet">
        ///     <item>
        ///         <term>ElementTheme.Light</term>
        ///         <description>Light mode.</description>
        ///     </item>
        ///     <item>
        ///         <term>ElementTheme.Dark</term>
        ///         <description>Dark mode.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public ElementTheme Theme { get; set; } = ElementTheme.Light;

        /// <summary>
        /// Gets or sets the clock format to display in the title bar.
        /// <list type="bullet">
        ///     <item>
        ///         <term>HH:mm:ss</term>
        ///         <description>24-hour format.</description>
        ///     </item>
        ///     <item>
        ///         <term>hh:mm:ss tt</term>
        ///         <description>12-hour format with AM/PM.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public string TimeFormat { get; set; } = "HH:mm:ss";
    }
}
