// <copyright file="FeedContentPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.Tools;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Maui;

/// <summary>
/// Feed Content Page.
/// </summary>
public partial class FeedContentPage : BasePage
{
    private RssFeedArticleViewModel vm;
    private FeedItem item;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedContentPage"/> class.
    /// </summary>
    /// <param name="item"><see cref="FeedItem"/>.</param>
    /// <param name="services"><see cref="IServiceProvider"/>.</param>
    public FeedContentPage(FeedItem item, IServiceProvider services)
        : base(services)
    {
        this.item = item;
        this.InitializeComponent();
        this.BindingContext = this.vm = this.ServiceProvider.ResolveWith<RssFeedArticleViewModel>(this.FeedWebView, this.item)
            ?? throw new ArgumentNullException(nameof(this.vm));
    }
}