// <copyright file="ArticleViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class ArticleViewController : UIViewController
    {
        private ArticleSearchBar searchBar;
        private UIBarButtonItem nextArticleBarButtonItem;
        private UIBarButtonItem prevArticleBarButtonItem;
        // private UIBarButtonItem appearanceBarButtonItem;
        private RssWebview webview;

        public ArticleViewController()
        {
            this.webview = new RssWebview(this.View?.Frame ?? CGRect.Empty, new WebKit.WKWebViewConfiguration());
            this.webview.AutoresizingMask = UIViewAutoresizing.All;
           // this.View?.AddSubview(this.webview);

            this.searchBar = new ArticleSearchBar();
            this.nextArticleBarButtonItem = new UIBarButtonItem();
            this.prevArticleBarButtonItem = new UIBarButtonItem();
            // this.appearanceBarButtonItem = new UIBarButtonItem();

            //this.appearanceBarButtonItem.Image = UIImage.GetSystemImage("ellipsis.circle");

            this.prevArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.up");
            this.prevArticleBarButtonItem.Title = "Previous Article";

            this.nextArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.down");
            this.nextArticleBarButtonItem.Title = "Next Article";

            this.NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { this.prevArticleBarButtonItem, this.nextArticleBarButtonItem }, false);

            if (this.View is not null)
            {
                this.View.AddSubview(this.searchBar);
                this.View.BackgroundColor = UIColor.SystemBackground;
            }

            this.NavigationItem.Title = "Article";
            this.NavigationItem.HidesSearchBarWhenScrolling = false;
            this.searchBar.BackgroundColor = UIColor.White;
            this.searchBar.Hidden = true;
            this.searchBar.TranslatesAutoresizingMaskIntoConstraints = true;

            if (this.View is not null)
            {
                this.View.SafeAreaLayoutGuide.TrailingAnchor.ConstraintEqualTo(this.searchBar.TrailingAnchor).Active = true;
                this.searchBar.LeadingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.LeadingAnchor).Active = true;

                try
                {
                    var searchBarBottomConstraint = this.View.SafeAreaLayoutGuide.BottomAnchor.ConstraintEqualTo(this.searchBar.BottomAnchor);
                    searchBarBottomConstraint.Active = true;
                }
                catch
                {
                }
            }

            // this.webview.LoadHtmlString(new NSString("<h2>test</h2>"), null);
        }

        /// <summary>
        /// Gets or sets the Feed Article FM.
        /// </summary>
        public RssFeedArticleViewModel? FeedArticleVM { get; set; }

        internal RssWebview RssWebview => this.webview;
    }
}