// <copyright file="FeedItemSelectedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Core
{
    public class FeedItemSelectedEventArgs : EventArgs
    {
        private readonly FeedItem feedItem;

        public FeedItemSelectedEventArgs(FeedItem item)
        {
            this.feedItem = item;
        }

        public FeedItem FeedItem => this.feedItem;
    }
}
