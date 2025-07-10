using Microsoft.UI.Xaml.Controls;

using WinuiAppDemo.Models;
using WinuiAppDemo.Services.Interfaces;
using WinuiAppDemo.ViewModels;

namespace WinuiAppDemo.Views;

/// <summary>
/// Provides functionality for the main shell page of the application.
/// </summary>
public sealed partial class ShellPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShellPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);

        INavigationService navService = App.GetService<INavigationService>();
        navService.Register(new NavItem { Name = "Home", Tag = "Home", Icon = Symbol.Home, PageType = typeof(HomePage) });
        navService.Register(new NavItem { Name = "User", Tag = "User", Icon = Symbol.OtherUser, PageType = typeof(UserPage) });
        navService.SettingsPageType = typeof(SettingsPage);
        navService.Frame = navigationFrame;
        ViewModel.Startup();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public ShellViewModel ViewModel { get; }
}
