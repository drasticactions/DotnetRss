// <copyright file="RssWebview.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using ObjCRuntime;
using WebKit;

namespace DotnetRss.Mac.Shared
{
    public class RssWebview : WKWebView, IRssWebview
    {
        public RssWebview(NSCoder coder)
            : base(coder)
        {
        }

        public RssWebview(CGRect frame, WKWebViewConfiguration configuration)
            : base(frame, configuration)
        {
        }

        protected RssWebview(NSObjectFlag t)
            : base(t)
        {
        }

        protected internal RssWebview(NativeHandle handle)
            : base(handle)
        {
        }

        /// <inheritdoc/>
        public void SetSource(string html)
        {
            this.LoadHtmlString(new NSString(html), null);
        }
    }
}