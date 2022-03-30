// <copyright file="UrlImageConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;

namespace DotnetRss.Maui.Converters
{
    /// <summary>
    /// Url Image Converter.
    /// </summary>
    public class UrlImageConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value is Uri urlValue)
            //{
            //    return new UriImageSource() { Uri = urlValue };
            //}

            return new FileImageSource() { File = "dotnet_bot.png" };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
