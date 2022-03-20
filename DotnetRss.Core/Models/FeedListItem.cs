// <copyright file="FeedListItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Core
{
    /// <summary>
    /// Feed List Item.
    /// </summary>
    public class FeedListItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedListItem"/> class.
        /// </summary>
        public FeedListItem()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the feed name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the feed description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the feed Language.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date string.
        /// </summary>
        public string? LastUpdatedDateString { get; set; }

        /// <summary>
        /// Gets or sets the image uri.
        /// </summary>
        public Uri? ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the Feed Uri.
        /// </summary>
        public Uri? Uri { get; set; }

        /// <summary>
        /// Gets or sets the image cache.
        /// </summary>
        public byte[]? ImageCache { get; set; }

        /// <summary>
        /// Gets or sets the Feed Link.
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the feed is favorited.
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}
