// <copyright file="RssWebview.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;

namespace DotnetRss.Maui
{
    public class RssWebview : WebView, IRssWebview
    {
        /// <inheritdoc/>
        public void SetSource(string html)
        {
            var source = new HtmlWebViewSource();
            source.Html = html;
            this.Dispatcher.Dispatch(() => this.Source = source);
        }
    }
}
