// <copyright file="ArticleViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Mac.Shared
{
    public class ArticleViewController : UIViewController
    {
        private ArticleSearchBar searchBar;
        private UIBarButtonItem nextArticleBarButtonItem;
        private UIBarButtonItem prevArticleBarButtonItem;
        private UIBarButtonItem appearanceBarButtonItem;

        public ArticleViewController()
        {
            this.searchBar = new ArticleSearchBar();
            this.nextArticleBarButtonItem = new UIBarButtonItem();
            this.prevArticleBarButtonItem = new UIBarButtonItem();
            this.appearanceBarButtonItem = new UIBarButtonItem();

            this.appearanceBarButtonItem.Image = UIImage.GetSystemImage("ellipsis.circle");

            this.prevArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.Up");
            this.prevArticleBarButtonItem.Title = "Previous Article";

            this.nextArticleBarButtonItem.Image = UIImage.GetSystemImage("chevron.down");
            this.nextArticleBarButtonItem.Title = "Next Article";

            this.SetToolbarItems(new UIBarButtonItem[] { this.appearanceBarButtonItem, this.nextArticleBarButtonItem, this.prevArticleBarButtonItem }, false);

            if (this.View is not null)
            {
                this.View.AddSubview(this.searchBar);
                this.View.BackgroundColor = UIColor.SystemBackground;
            }

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
        }
    }
}