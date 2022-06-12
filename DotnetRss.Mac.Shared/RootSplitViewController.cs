// <copyright file="RootSplitViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core.Tools;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class RootSplitViewController : UISplitViewController
    {
        private ArticleViewController articleViewController;
        private MasterTimelineViewController timelineViewController;
        private MasterFeedViewController feedViewController;
        private IServiceProvider services;

        public RootSplitViewController(IServiceProvider services)
            : base(UISplitViewControllerStyle.TripleColumn)
        {
            this.services = services;
            this.FeedListVM = services.ResolveWith<RssFeedListViewModel>();
            this.FeedItemListVM = services.ResolveWith<RssFeedItemListViewModel>();
            this.PreferredDisplayMode = UISplitViewControllerDisplayMode.TwoBesideSecondary;
            this.articleViewController = new ArticleViewController();
            this.timelineViewController = new MasterTimelineViewController(this.FeedItemListVM);
            this.feedViewController = new MasterFeedViewController(this.FeedListVM);
            this.articleViewController.FeedArticleVM = this.FeedArticleVM = services.ResolveWith<RssFeedArticleViewModel>(this.articleViewController.RssWebview);

            this.SetViewController(this.articleViewController, UISplitViewControllerColumn.Secondary);
            this.SetViewController(this.timelineViewController, UISplitViewControllerColumn.Supplementary);
            this.SetViewController(this.feedViewController, UISplitViewControllerColumn.Primary);

            this.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
            this.FeedListVM.OnFeedListItemSelected += this.FeedListVM_OnFeedListItemSelected;
            this.FeedItemListVM.OnFeedItemSelected += FeedItemListVM_OnFeedItemSelected1;
        }

        private async void FeedItemListVM_OnFeedItemSelected1(object? sender, Core.FeedItemSelectedEventArgs e)
        {
            if (e.FeedListItem is not null)
            {
                await this.FeedArticleVM.UpdateFeedItem(e.FeedListItem, e.FeedItem);
            }
        }

        private async void FeedListVM_OnFeedListItemSelected(object? sender, Core.FeedListItemSelectedEventArgs e)
        {
           await this.FeedItemListVM.GetCachedFeedItemsCommand.ExecuteAsync(e.FeedListItem);
        }

        private void FeedItemListVM_OnFeedItemSelected(object? sender, Core.FeedItemSelectedEventArgs e)
        {
        }

        /// <summary>
        /// Gets the Feed List VM.
        /// </summary>
        public RssFeedListViewModel FeedListVM { get; private set; }

        /// <summary>
        /// Gets the Feed Item List VM.
        /// </summary>
        public RssFeedItemListViewModel FeedItemListVM { get; private set; }

        /// <summary>
        /// Gets the Feed Article FM.
        /// </summary>
        public RssFeedArticleViewModel FeedArticleVM { get; private set; }
    }
}