// <copyright file="RssFeedArticleViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Core.ViewModels
{
    /// <summary>
    /// Rss Feed Article View Model.
    /// </summary>
    public class RssFeedArticleViewModel : BaseViewModel
    {
        private IRssWebview webview;
        private string html;
        private FeedItem? feedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedArticleViewModel"/> class.
        /// </summary>
        /// <param name="webview">Rss Webview.</param>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public RssFeedArticleViewModel(IRssWebview webview, IServiceProvider services)
            : base(services)
        {
            this.webview = webview;
            this.html = string.Empty;
            this.FeedItemSelectedCommand = new AsyncCommand<FeedItem>(
            async (item) => await this.UpdateFeedItem(item),
            null,
            this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the Feed Html.
        /// </summary>
        public string Html
        {
            get => this.html;
            set => this.SetProperty(ref this.html, value);
        }

        /// <summary>
        /// Gets or sets the Feed Item.
        /// </summary>
        public FeedItem? FeedItem
        {
            get => this.feedItem;
            set => this.SetProperty(ref this.feedItem, value);
        }

        /// <summary>
        /// Gets the UpdateFeedListItem.
        /// </summary>
        public AsyncCommand<FeedItem> FeedItemSelectedCommand { get; private set; }

        private async Task UpdateFeedItem(FeedItem? item)
        {
            if (item is null)
            {
                return;
            }

            this.FeedItem = item;
            await this.RenderHtmlAsync();
            this.Context.AddOrUpdateFeedItem(this.FeedItem);
        }

        private async Task RenderHtmlAsync()
        {
            if (this.feedItem is null)
            {
                return;
            }

            this.Html = await this.Templates.RenderFeedItemAsync(this.feedItem);
            this.webview.SetSource(this.Html);
        }
    }
}
