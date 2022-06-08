// <copyright file="MacFeedListItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core;

namespace DotnetRss.Mac.Shared
{
    public class MacFeedListItem : NSObject
    {
        public MacFeedListItem(FeedListItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.FeedDescription = item.Description;
            this.Language = item.Language;
            this.LastUpdatedDate = item.LastUpdatedDate;
            this.LastUpdatedDateString = item.LastUpdatedDateString;
            this.ImageUri = item.ImageUri;
            this.Link = this.Link;
            this.IsFavorite = item.IsFavorite;

            if (item.ImageCache is not null)
            {
                var imageStream = new MemoryStream(item.ImageCache);
                var imageData = Foundation.NSData.FromStream(imageStream);
                if (imageData is not null)
                {
                    this.Image = UIKit.UIImage.LoadFromData(imageData);
                }

                imageStream.Seek(0, SeekOrigin.Begin);
            }
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
        public string? FeedDescription { get; set; }

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
        /// Gets the image.
        /// </summary>
        public UIKit.UIImage? Image { get; private set; }

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