// <copyright file="ITemplateService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Core
{
    /// <summary>
    /// Template Service.
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Render Feed Item.
        /// </summary>
        /// <param name="item">FeedItem.</param>
        /// <returns>Html String.</returns>
        public Task<string> RenderFeedItemAsync(FeedListItem feedListItem, FeedItem item);
    }
}
