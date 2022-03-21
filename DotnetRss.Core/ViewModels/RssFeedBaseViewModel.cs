// <copyright file="RssFeedBaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Core.ViewModels
{
    /// <summary>
    /// RSS Feed Base View Model.
    /// </summary>
    public class RssFeedBaseViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedBaseViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>/</param>
        public RssFeedBaseViewModel(IServiceProvider services)
            : base(services)
        {
        }

        /// <summary>
        /// Adds New Feed List.
        /// </summary>
        /// <param name="feedUri">The Feed Uri.</param>
        /// <returns>Task.</returns>
        public async Task<FeedListItem?> AddOrUpdateNewFeedListItemAsync(string feedUri)
        {
            try
            {
                (var feed, var feedListItems) = await this.Rss.ReadFeedAsync(feedUri);
                var item = this.Context.GetFeedListItem(new Uri(feedUri));
                if (item is null)
                {
                    item = feed;
                }

                if (item is null || feedListItems is null)
                {
                    // TODO: Handle error. It shouldn't be null.
                    return null;
                }

                var result = this.Context.AddOrUpdateFeedListItem(item);

                foreach (var feedItem in feedListItems)
                {
                    feedItem.FeedListItemId = item.Id;
                    this.Context.AddOrUpdateFeedItem(feedItem);
                    this.SendFeedUpdateRequest(item, feedItem);
                }

                this.SendFeedListUpdateRequest(item);

                return item;
            }
            catch (Exception ex)
            {
                this.ErrorHandler.HandleError(ex);
            }

            return null;
        }
    }
}
