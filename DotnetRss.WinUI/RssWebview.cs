// <copyright file="RssWebview.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using Microsoft.UI.Xaml.Controls;

namespace DotnetRss.WinUI
{
    /// <summary>
    /// Rss Webview Reader.
    /// </summary>
    public class RssWebview : WebView2, IRssWebview
    {
        /// <inheritdoc/>
        public void SetSource(string html)
        {
            this.DispatcherQueue.TryEnqueue(async () =>
            {
                await this.EnsureCoreWebView2Async();
                this.NavigateToString(html);
            }
            );
        }
    }
}
