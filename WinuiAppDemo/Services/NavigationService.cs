using System;
using System.Collections.Generic;

using Microsoft.UI.Xaml.Controls;

using NLog;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.Services;

/// <inheritdoc />
public class NavigationService : INavigationService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly Dictionary<string, NavItem> _navItems = [];

    /// <inheritdoc />
    public Frame Frame { get; set; } = default!;

    /// <inheritdoc />
    public Type? SettingsPageType { get; set; }

    /// <inheritdoc />
    public void Register(NavItem item)
    {
        if (!_navItems.ContainsKey(item.Tag))
        {
            _navItems[item.Tag] = item;
        }
        else
        {
            _logger.Warn($"item.Tag:{item.Tag} key already exists in _navItems.");
        }
    }

    /// <inheritdoc />
    public void Navigate(string tag)
    {
        if (_navItems.TryGetValue(tag, out var item))
        {
            Frame.Navigate(item.PageType);
        }
        else
        {
            _logger.Error($"tag:{tag} key does not exist in _navItems.");
        }
    }

    /// <inheritdoc />
    public bool CanGoBack()
    {
        return Frame != null && Frame.CanGoBack;
    }

    /// <inheritdoc />
    public void GoBack()
    {
        if (CanGoBack())
        {
            Frame!.GoBack();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<NavItem> GetNavItems()
    {
        return [.. _navItems.Values];
    }

    /// <inheritdoc />
    public NavItem GetCurrentFrameType()
    {
        Type pageType = Frame.CurrentSourcePageType;
        NavItem navItem = GetNavItems()[0];

        foreach (NavItem item in _navItems.Values)
        {
            if (item.PageType == pageType)
            {
                navItem = item;
                break;
            }
        }

        return navItem;
    }
}
