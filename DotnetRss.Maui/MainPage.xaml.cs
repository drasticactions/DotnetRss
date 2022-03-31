// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Maui;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : BasePage
{
    private RssFeedListViewModel vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="services"><see cref="IServiceProvider"/>.</param>
    public MainPage(IServiceProvider services)
        : base(services)
    {
        this.InitializeComponent();
        this.BindingContext = this.vm = this.ServiceProvider.GetService(typeof(RssFeedListViewModel)) as RssFeedListViewModel
            ?? throw new ArgumentNullException(nameof(this.vm));
        this.FeedListItemCollection.SelectionChanged += this.FeedListItemCollection_SelectionChanged;
    }

    private void FeedListItemCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FeedListItem item)
        {
            this.Navigation.PushAsync(new FeedListPage(item, this.ServiceProvider)).FireAndForgetSafeAsync();
        }
    }

    private async void NewFeedButton_Clicked(object sender, EventArgs e)
    {
        var result = await this.DisplayPromptAsync("Add New Feed", "Add a new RSS Feed.", keyboard: Keyboard.Url);
        if (result is not null)
        {
            await this.vm.AddOrUpdateNewFeedListItemAsync(result);
        }
    }

    private void NewWindowButton_Clicked(object sender, EventArgs e)
    {
        App.Current?.OpenWindow(new Window(new NavigationPage(new MainPage(this.ServiceProvider))));
    }
}