using System;
using System.Collections.Generic;

using Microsoft.UI.Xaml.Controls;

using WinuiAppDemo.Models;

namespace WinuiAppDemo.Services.Interfaces;

/// <summary>
/// Provides functionality for navigating between pages in the application.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Gets or sets the page frame in UI.
    /// </summary>
    Frame Frame { get; set; }

    /// <summary>
    /// Gets or sets the type of a NavigationView-specific settings page.
    /// </summary>
    Type? SettingsPageType { get; set; }

    /// <summary>
    /// Navigate the page.
    /// </summary>
    /// <param name="tag">The page tag.</param>
    void Navigate(string tag);

    /// <summary>
    /// Register information for a NavigationViewItem.
    /// </summary>
    /// <param name="item">Information for a NavigationViewItem.</param>
    void Register(NavItem item);

    /// <summary>
    /// Check if there is a back page.
    /// </summary>
    /// <returns>Flag to indicate whether there is a back page.</returns>
    bool CanGoBack();

    /// <summary>
    /// Return to previous page.
    /// </summary>
    void GoBack();

    /// <summary>
    /// Gets a list of information for registered NavigationViewItems.
    /// </summary>
    /// <returns>A list of information for registered NavigationViewItems.</returns>
    IReadOnlyList<NavItem> GetNavItems();

    /// <summary>
    /// Gets the current frame type.
    /// </summary>
    /// <returns>The current frame type.</returns>
    NavItem GetCurrentFrameType();
}
