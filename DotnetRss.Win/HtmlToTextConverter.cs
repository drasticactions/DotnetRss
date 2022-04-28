// <copyright file="HtmlToTextConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.RegularExpressions;

namespace DotnetRss.Win
{
    public class HtmlToTextConverter : Microsoft.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not string htmlString)
            {
                return string.Empty;
            }

            // We don't want to render the HTML, we just want to get the raw text out.
            return Regex.Replace(htmlString, "<.*?>", string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
