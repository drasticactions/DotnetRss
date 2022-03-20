// <copyright file="FeedReaderTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetRss.Tests
{
    /// <summary>
    /// FeedReader Tests.
    /// </summary>
    [TestClass]
    public class FeedReaderTests
    {
        IRssService rss;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedReaderTests"/> class.
        /// </summary>
        public FeedReaderTests()
        {
            this.rss = new FeedReaderService();
        }

        /// <summary>
        /// Get Feed.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GetFeed()
        {
            (var feedList, var feedItemsList) = await this.rss.ReadFeedAsync("https://devblogs.microsoft.com/dotnet/feed/");
            Assert.IsNotNull(feedList);
            Assert.IsNotNull(feedItemsList);
        }
    }
}