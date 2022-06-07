// <copyright file="ArticleViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Mac.Shared
{
    public class ArticleViewController : UIViewController
    {
        private UIToolbar toolbar;
        private ArticleSearchBar searchBar;
        private UIBarButtonItem nextArticleBarButtonItem;
        private UIBarButtonItem prevArticleBarButtonItem;
        // private UIBarButtonItem appearanceBarButtonItem;
        private WebKit.WKWebView webview;

        public ArticleViewController()
        {
            this.webview = new WebKit.WKWebView(this.View?.Frame ?? CGRect.Empty, new WebKit.WKWebViewConfiguration());
            this.webview.AutoresizingMask = UIViewAutoresizing.All;
            this.toolbar = new UIToolbar(new CGRect(0, 0, this.View?.Frame.Width ?? 0, 30));
            toolbar.TranslatesAutoresizingMaskIntoConstraints = false;

            if (this.View != null)
            {
                var tbConstraints = new[]
                {
                  toolbar.LeadingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.LeadingAnchor),
                  toolbar.TrailingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TrailingAnchor),
                  toolbar.TopAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TopAnchor, 0.0f),
                  toolbar.HeightAnchor.ConstraintEqualTo(toolbar.IntrinsicContentSize.Height)
                };
                this.View?.AddSubview(this.toolbar);
                // this.toolbar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
                NSLayoutConstraint.ActivateConstraints(tbConstraints);
            }

            this.View?.AddSubview(this.webview);

            this.searchBar = new ArticleSearchBar();
            this.nextArticleBarButtonItem = new UIBarButtonItem();
            this.prevArticleBarButtonItem = new UIBarButtonItem();
            // this.appearanceBarButtonItem = new UIBarButtonItem();

            //this.appearanceBarButtonItem.Image = UIImage.GetSystemImage("ellipsis.circle");

            this.prevArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.up");
            this.prevArticleBarButtonItem.Title = "Previous Article";

            this.nextArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.down");
            this.nextArticleBarButtonItem.Title = "Next Article";

            this.toolbar.SetItems(new UIBarButtonItem[] { this.nextArticleBarButtonItem, this.prevArticleBarButtonItem }, false);

            if (this.View is not null)
            {
                this.View.AddSubview(this.searchBar);
                this.View.BackgroundColor = UIColor.SystemBackground;
            }

            this.NavigationItem.Title = "Article";

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

            this.webview.LoadHtmlString(new NSString("<h2>test</h2>"), null);
        }
    }
}