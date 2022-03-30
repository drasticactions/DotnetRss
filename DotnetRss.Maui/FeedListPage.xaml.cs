// <copyright file="FeedListPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.Tools;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Maui;

/// <summary>
/// Feed List Page.
/// </summary>
public partial class FeedListPage : BasePage
{
    private RssFeedItemListViewModel vm;
    private FeedListItem item;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedListPage"/> class.
    /// </summary>
    /// <param name="item">Selected Feed List Item.</param>
    /// <param name="services"><see cref="IServiceProvider"/>.</param>
    public FeedListPage(FeedListItem item, IServiceProvider services)
        : base(services)
    {
        this.item = item;
        this.InitializeComponent();
        this.BindingContext = this.vm = this.ServiceProvider.ResolveWith<RssFeedItemListViewModel>(this.item)
            ?? throw new ArgumentNullException(nameof(this.vm));
        this.FeedItemCollection.SelectionChanged += this.FeedItemCollection_SelectionChanged;
    }

    private void FeedItemCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FeedItem item)
        {
            this.Navigation.PushAsync(new FeedContentPage(item, this.ServiceProvider)).FireAndForgetSafeAsync();
        }
    }
}