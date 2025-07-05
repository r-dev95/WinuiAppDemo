using System;

using Microsoft.UI.Xaml.Controls;

namespace WinuiAppDemo.Models
{
    /// <summary>
    /// Provides functionality for defining a navigation item in the application.
    /// </summary>
    public class NavItem
    {
        /// <summary>
        /// Gets or sets the page name for NavigationViewItem Content.
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag for NavigationViewItem Tag.
        /// </summary>
        required public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the icon for NavigationViewItem Symbol Icon.
        /// </summary>
        required public Symbol Icon { get; set; }

        /// <summary>
        /// Gets or sets the page type for Frame Navigate.
        /// </summary>
        required public Type PageType { get; set; }
    }
}
