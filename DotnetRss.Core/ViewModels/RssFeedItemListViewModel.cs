// <copyright file="RssFeedItemListViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;

namespace DotnetRss.Core.ViewModels
{
    /// <summary>
    /// Rss Feed Item List View model.
    /// </summary>
    public class RssFeedItemListViewModel : BaseViewModel
    {
        private FeedListItem? feedListItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedItemListViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public RssFeedItemListViewModel(IServiceProvider services, FeedListItem? feedListItem)
            : base(services)
        {
            this.feedListItem = feedListItem;
            this.FeedItems = new ObservableCollection<FeedItem>();
            this.GetCachedFeedItemsCommand = new AsyncCommand<FeedListItem>(
            async (item) => await this.GetCachedFeedItems(item),
            null,
            this.ErrorHandler);
            this.OnFeedItemUpdated += this.RssFeedItemListViewModel_OnFeedItemUpdated;
            this.FeedItemSelectedCommand = new AsyncCommand<FeedItem>(
            async (item) => this.OnFeedItemSelected?.Invoke(this, new FeedItemSelectedEventArgs(item)),
            null,
            this.ErrorHandler);
        }

        /// <summary>
        /// Fired when a feed item is selected.
        /// </summary>
        public event EventHandler<FeedItemSelectedEventArgs>? OnFeedItemSelected;

        /// <summary>
        /// Gets the list of feed items.
        /// </summary>
        public ObservableCollection<FeedItem> FeedItems { get; }

        /// <summary>
        /// Gets the UpdateFeedListItem.
        /// </summary>
        public AsyncCommand<FeedListItem> GetCachedFeedItemsCommand { get; private set; }

        /// <summary>
        /// Gets the UpdateFeedListItem.
        /// </summary>
        public AsyncCommand<FeedItem> FeedItemSelectedCommand { get; private set; }

        /// <inheritdoc/>
        public override async Task OnLoad()
        {
            await base.OnLoad();
            if (this.feedListItem is not null)
            {
                await this.GetCachedFeedItems(this.feedListItem);
            }
        }

        private async Task GetCachedFeedItems(FeedListItem item!!)
        {
            this.feedListItem = item;
            this.FeedItems.Clear();
            var feedItems = this.Context.GetFeedItems(this.feedListItem).OrderBy(n => n.PublishingDate);
            if (feedItems.Any())
            {
                foreach (var feedItem in feedItems)
                {
                    this.FeedItems.Add(feedItem);
                }
            }
            else
            {
                await this.AddOrUpdateNewFeedListItemAsync(item.Uri?.ToString() ?? throw new ArgumentNullException(nameof(item.Uri)));
            }
        }

        private void RssFeedItemListViewModel_OnFeedItemUpdated(object? sender, FeedItemUpdatedEventArgs e)
        {
            if (this.feedListItem != e.FeedListItem)
            {
                this.feedListItem = e.FeedListItem;
                this.FeedItems.Clear();
            }

            var item = this.FeedItems.FirstOrDefault(n => n.Id == e.FeedItem.Id);
            if (item is null)
            {
                this.FeedItems.Add(e.FeedItem);
            }
            else
            {
                this.FeedItems[this.FeedItems.IndexOf(item)] = item;
            }
        }
    }
}
