// <copyright file="HtmlToTextConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using System.Text.RegularExpressions;

namespace DotnetRss.Win
{
    public class HtmlToTextConverter : Microsoft.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not FeedItem feedListItem)
            {
                return string.Empty;
            }

            var htmlString = !string.IsNullOrEmpty(feedListItem.Description) ? feedListItem.Description : feedListItem.Content;

            // We don't want to render the HTML, we just want to get the raw text out.
            return Regex.Replace(htmlString ?? string.Empty, "<.*?>", string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
