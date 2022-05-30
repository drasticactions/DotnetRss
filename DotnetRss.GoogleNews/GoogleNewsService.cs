﻿// <copyright file="GoogleNewsService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using DotnetRss.Core;

namespace DotnetRss.GoogleNews
{
    /// <summary>
    /// Google News Service.
    /// </summary>
    public class GoogleNewsService
    {
        private IRssService rssService;
        private string mainFeedUri = "https://news.google.com/rss?gl={1}&hl={0}&ceid={1}:{0}";
        private string sectiondUri = "https://news.google.com/news/rss/headlines/section/topic/{0}?ned={2}&hl={1}";

        public GoogleNewsService(IRssService service)
        {
            ArgumentNullException.ThrowIfNull(service);
            this.rssService = service;
        }

        public async Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadMainPageAsync(CultureInfo? culture = default, CancellationToken? token = default)
        {
            var (cultureName, cultureLocale) = this.GetCultureNameAndLocal(culture);

            var mainFeedFormat = string.Format(CultureInfo.InvariantCulture, this.mainFeedUri, cultureName, cultureLocale);

            return await this.rssService.ReadFeedAsync(mainFeedFormat, token);
        }

        public async Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadSectionAsync(NewsSections section, CultureInfo? culture = default, CancellationToken? token = default)
        {
            if (section is NewsSections.Unknown)
            {
                throw new ArgumentException(nameof(section));
            }

            var (cultureName, cultureLocale) = this.GetCultureNameAndLocal(culture);

            var sectionFeedFormat = string.Format(CultureInfo.InvariantCulture, this.sectiondUri, section.ToString().ToUpperInvariant(), cultureName, cultureLocale);

            return await this.rssService.ReadFeedAsync(sectionFeedFormat, token);
        }

        private (string CultureName, string CultureLocal) GetCultureNameAndLocal(CultureInfo? culture = default)
        {
            culture = culture ?? CultureInfo.CurrentCulture;

            var cultureNameAndLocale = culture.ToString().Split('-');

            var cultureLocale = "US";
            var cultureName = "en";

            if (cultureNameAndLocale.Length == 2)
            {
                cultureName = cultureNameAndLocale[0];
                cultureLocale = cultureNameAndLocale[1];
            }

            return (cultureName, cultureLocale);
        }
    }
}
