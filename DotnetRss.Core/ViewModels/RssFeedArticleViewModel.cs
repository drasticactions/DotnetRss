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
        private FeedListItem? feedListItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedArticleViewModel"/> class.
        /// </summary>
        /// <param name="webview">Rss Webview.</param>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        /// <param name="item">Feed Item.</param>
        public RssFeedArticleViewModel(IRssWebview webview, IServiceProvider services, FeedListItem? feedListItem = null, FeedItem? item = null)
            : base(services)
        {
            this.feedListItem = feedListItem;
            this.feedItem = item;
            this.webview = webview;
            this.html = string.Empty;
            this.SetIsFavoriteFeedItem = new AsyncCommand<FeedItem>(
            async (item) => await this.SetIsFavoriteFeedItemAsync(item),
            (FeedItem item) => item is not null,
            this.ErrorHandler);
            this.SetIsReadFeedItem = new AsyncCommand<FeedItem>(
            async (item) => await this.SetIsReadFeedItemAsync(item),
            (FeedItem item) => item is not null,
            this.ErrorHandler);
            this.ShareLinkCommand = new AsyncCommand<FeedItem>(
            async (item) => await this.ShareLinkAsync(item),
            (FeedItem item) => item is not null && !this.IsBusy,
            this.ErrorHandler);
            this.OpenBrowserCommand = new AsyncCommand<FeedItem>(
            async (item) => await this.OpenBrowserAsync(item),
            (FeedItem item) => item is not null && !this.IsBusy,
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
        /// Gets the SetIsFavoriteFeedItem.
        /// </summary>
        public AsyncCommand<FeedItem> SetIsFavoriteFeedItem { get; private set; }

        /// <summary>
        /// Gets the SetIsReadFeedItem.
        /// </summary>
        public AsyncCommand<FeedItem> SetIsReadFeedItem { get; private set; }

        /// <summary>
        /// Gets the ShareLinkCommand.
        /// </summary>
        public AsyncCommand<FeedItem> ShareLinkCommand { get; private set; }

        /// <summary>
        /// Gets the ShareLinkCommand.
        /// </summary>
        public AsyncCommand<FeedItem> OpenBrowserCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Feed Item.
        /// </summary>
        public FeedItem? FeedItem
        {
            get => this.feedItem;
            set => this.SetProperty(ref this.feedItem, value);
        }

        public override async Task OnLoad()
        {
            await base.OnLoad();
            await this.UpdateFeedItem(this.feedListItem, this.feedItem);
        }

        public async Task UpdateFeedItem(FeedListItem? feedListItem, FeedItem? item)
        {
            if (item is null || feedListItem is null)
            {
                return;
            }

            this.feedListItem = feedListItem;
            this.FeedItem = item;
            this.Title = this.FeedItem.Title ?? string.Empty;
            await this.RenderHtmlAsync();
            this.FeedItem.IsRead = true;
            this.Context.AddOrUpdateFeedItem(this.FeedItem);
        }

        /// <inheritdoc/>
        public override void RaiseCanExecuteChanged()
        {
            base.RaiseCanExecuteChanged();
            this.SetIsFavoriteFeedItem.RaiseCanExecuteChanged();
            this.SetIsReadFeedItem.RaiseCanExecuteChanged();
            this.OpenBrowserCommand.RaiseCanExecuteChanged();
            this.ShareLinkCommand.RaiseCanExecuteChanged();
        }

        private async Task RenderHtmlAsync()
        {
            if (this.feedItem is null || this.feedListItem is null)
            {
                return;
            }

            this.Html = await this.Templates.RenderFeedItemAsync(this.feedListItem, this.feedItem);
            this.webview.SetSource(this.Html);
        }
    }
}
