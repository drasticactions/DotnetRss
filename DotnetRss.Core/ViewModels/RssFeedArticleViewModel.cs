using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core.ViewModels
{
    public class RssFeedArticleViewModel : BaseViewModel
    {
        private IRssWebview webview;
        private string html;
        private FeedItem? feedItem;

        public RssFeedArticleViewModel(IRssWebview webview, IServiceProvider services) : base(services)
        {
            this.webview = webview;
            this.html = string.Empty;
            this.FeedItemSelectedCommand = new AsyncCommand<FeedItem>(
            async (item) => await this.UpdateFeedItem(item),
            null,
            this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the Feed Html.
        /// </summary>
        public string Html
        {
            get => this.html;
            set => this.SetProperty(ref this.html, value);
        }

        /// <summary>
        /// Gets or sets the Feed Item.
        /// </summary>
        public FeedItem? FeedItem
        {
            get => this.feedItem;
            set => this.SetProperty(ref this.feedItem, value);
        }

        /// <summary>
        /// Gets the UpdateFeedListItem.
        /// </summary>
        public AsyncCommand<FeedItem> FeedItemSelectedCommand { get; private set; }

        private async Task UpdateFeedItem(FeedItem? item)
        {
            if (item is null)
            {
                return;
            }

            this.FeedItem = item;
            if (this.FeedItem.Link is not null)
            {
                SmartReader.Article article = await SmartReader.Reader.ParseArticleAsync(this.FeedItem.Link);
                this.FeedItem.Html = article.Content;
            }

            this.Context.AddOrUpdateFeedItem(this.FeedItem);
            this.RenderHtml();
        }

        private void RenderHtml()
        {
            if (this.feedItem is null)
            {
                return;
            }

            this.Html = this.Templates.RenderFeedItem(this.feedItem);
            this.webview.SetSource(this.Html);
        }
    }
}
