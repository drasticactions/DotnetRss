// <copyright file="RssFeedItemListViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;

namespace DotnetRss.Core.ViewModels
{
    /// <summary>
    /// Rss Feed Item List View model.
    /// </summary>
    public class RssFeedItemListViewModel : RssFeedBaseViewModel
    {
        private FeedListItem? feedListItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedItemListViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public RssFeedItemListViewModel(IServiceProvider services, FeedListItem? item = null)
            : base(services)
        {
            this.FeedListItem = item;
            this.FeedItems = new ObservableCollection<FeedItem>();
            this.GetCachedFeedItemsCommand = new AsyncCommand<FeedListItem>(
            async (item) => await this.GetCachedFeedItems(item),
            null,
            this.ErrorHandler);
            this.OnFeedItemUpdated += this.RssFeedItemListViewModel_OnFeedItemUpdated;
            this.FeedItemSelectedCommand = new AsyncCommand<FeedItem>(
            async (item) => this.OnFeedItemSelected?.Invoke(this, new FeedItemSelectedEventArgs(this.FeedListItem, item)),
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
        /// Gets or sets the Feed List Item.
        /// </summary>
        public FeedListItem? FeedListItem
        {
            get { return this.feedListItem; }
            set { this.SetProperty(ref this.feedListItem, value); }
        }

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

        private async Task GetCachedFeedItems(FeedListItem item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            this.FeedListItem = item;
            this.Title = item.Name ?? string.Empty;
            this.FeedItems.Clear();
            var feedItems = this.Context.GetFeedItems(this.FeedListItem).OrderByDescending(n => n.PublishingDate).ToList();
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

            this.OnPropertyChanged(nameof(this.FeedItems));
        }

        private void RssFeedItemListViewModel_OnFeedItemUpdated(object? sender, FeedItemUpdatedEventArgs e)
        {
            if (this.FeedListItem?.Id != e.FeedListItem.Id)
            {
                this.FeedListItem = e.FeedListItem;
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
