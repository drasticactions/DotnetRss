// <copyright file="RootSplitViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Mac.Shared
{
    public class RootSplitViewController : UISplitViewController
    {
        private ArticleViewController articleViewController;
        private MasterTimelineViewController timelineViewController;
        private MasterFeedViewController feedViewController;

        public RootSplitViewController()
            : base(UISplitViewControllerStyle.TripleColumn)
        {
            this.PreferredDisplayMode = UISplitViewControllerDisplayMode.TwoBesideSecondary;
            this.articleViewController = new ArticleViewController();
            this.timelineViewController = new MasterTimelineViewController();
            this.feedViewController = new MasterFeedViewController();
            this.SetViewController(this.articleViewController, UISplitViewControllerColumn.Secondary);
            this.SetViewController(this.timelineViewController, UISplitViewControllerColumn.Supplementary);
            this.SetViewController(this.feedViewController, UISplitViewControllerColumn.Primary);
            // this.ViewControllers = new UIViewController[] { new UINavigationController(feedViewController), new UINavigationController(timelineViewController), new UINavigationController(articleViewController) };
        }
    }
}