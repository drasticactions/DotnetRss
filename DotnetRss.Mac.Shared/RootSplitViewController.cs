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
            this.PreferredDisplayMode = UISplitViewControllerDisplayMode.TwoBesideSecondary;
            this.articleViewController = new ArticleViewController();
            this.timelineViewController = new MasterTimelineViewController();
            this.feedViewController = new MasterFeedViewController();

            this.feedViewController.FeedListVM = this.FeedListVM = services.ResolveWith<RssFeedListViewModel>();
            this.timelineViewController.FeedItemListVM = this.FeedItemListVM = services.ResolveWith<RssFeedItemListViewModel>();
            this.articleViewController.FeedArticleVM = this.FeedArticleVM = services.ResolveWith<RssFeedArticleViewModel>(this.articleViewController.RssWebview);

            this.SetViewController(this.articleViewController, UISplitViewControllerColumn.Secondary);
            this.SetViewController(this.timelineViewController, UISplitViewControllerColumn.Supplementary);
            this.SetViewController(this.feedViewController, UISplitViewControllerColumn.Primary);
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