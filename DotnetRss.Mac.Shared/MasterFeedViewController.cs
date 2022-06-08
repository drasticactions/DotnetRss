// <copyright file="MasterFeedViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class MasterFeedViewController : UIViewController, IUICollectionViewDelegate
    {
        private UICollectionView collectionView;
        private UICollectionViewDiffableDataSource<NSString, MacFeedListItem>? dataSource;
        private UIBarButtonItem filterButton;
        private UIBarButtonItem newItemButton;
        private UIBarButtonItem refreshButton;
        private UIAlertController addNewFeedController;

        public MasterFeedViewController(RssFeedListViewModel vm)
        {
            this.FeedListVM = vm;

            if (this.View is null)
            {
                throw new NullReferenceException(nameof(this.View));
            }

            this.addNewFeedController = UIAlertController.Create("Add New Feed", string.Empty, UIAlertControllerStyle.Alert);
            this.addNewFeedController.AddTextField((field) =>
            {
                field.Placeholder = "https://...";
                field.Text = "https://ascii.jp/biz/rss.xml";
            });

            this.addNewFeedController.AddAction(UIAlertAction.Create("Add", UIAlertActionStyle.Default, async (action) =>
            {
                if (!this.addNewFeedController.TextFields.Any())
                {
                    return;
                }

                var textField = this.addNewFeedController.TextFields[0];
                if (!string.IsNullOrEmpty(textField?.Text) && this.FeedListVM is not null)
                {
                    var feedListItem = await this.FeedListVM.AddOrUpdateNewFeedListItemAsync(textField.Text);

                    if (feedListItem is not null)
                    {
                        var macFeedListItem = new MacFeedListItem(feedListItem);
                        this.SetupNavigationItems(this.GetNavigationSnapshot(new List<MacFeedListItem>() { macFeedListItem }));
                    }
                }
            }));

            this.filterButton = new UIBarButtonItem();
            this.newItemButton = new UIBarButtonItem();
            this.refreshButton = new UIBarButtonItem();
            this.newItemButton.Clicked += this.NewItemButton_Clicked;

            this.refreshButton.Clicked += this.RefreshButton_Clicked;

            this.filterButton.Title = "Filter";
            this.newItemButton.Title = "New Feed";

            this.ExtendedLayoutIncludesOpaqueBars = true;

            this.NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { this.refreshButton, this.newItemButton }, false);
            this.NavigationItem.Title = "Feeds";

            this.newItemButton.Image = UIImage.GetSystemImage("plus");
            this.refreshButton.Image = UIImage.GetSystemImage("arrow.triangle.2.circlepath");
            this.filterButton.Image = UIImage.GetSystemImage("line.3.horizontal.decrease.circle");

            this.collectionView = new UICollectionView(this.View.Bounds, this.CreateLayout());
            this.collectionView.Delegate = this;
            this.View.AddSubview(this.collectionView);

            // Anchor collectionView
            this.collectionView.TranslatesAutoresizingMaskIntoConstraints = false;

            var constraints = new List<NSLayoutConstraint>();
            constraints.Add(this.collectionView.BottomAnchor.ConstraintEqualTo(this.View.BottomAnchor));
            constraints.Add(this.collectionView.LeftAnchor.ConstraintEqualTo(this.View.LeftAnchor));
            constraints.Add(this.collectionView.RightAnchor.ConstraintEqualTo(this.View.RightAnchor));
            constraints.Add(this.collectionView.HeightAnchor.ConstraintEqualTo(this.View.HeightAnchor));

            NSLayoutConstraint.ActivateConstraints(constraints.ToArray());

            this.ConfigureDataSource();
        }

        private async void RefreshButton_Clicked(object? sender, EventArgs e)
        {
            await this.ReloadItemsAsync();
        }

        private void NewItemButton_Clicked(object? sender, EventArgs e)
        {
            this.PresentViewController(this.addNewFeedController, true, null);
        }

        private void SetupNavigationItems(NSDiffableDataSourceSectionSnapshot<MacFeedListItem> snapshot)
        {
            if (this.dataSource is null)
            {
                return;
            }

            // Add base sidebar items
            var sectionIdentifier = new NSString("base");
            this.dataSource.ApplySnapshot(snapshot, sectionIdentifier, false);
        }

        private UICollectionViewLayout CreateLayout()
        {
            var config = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.Sidebar);
            config.HeaderMode = UICollectionLayoutListHeaderMode.None;
            config.ShowsSeparators = false;

            return UICollectionViewCompositionalLayout.GetLayout(config);
        }

        private NSDiffableDataSourceSectionSnapshot<MacFeedListItem> GetNavigationSnapshot(IEnumerable<MacFeedListItem> items)
        {
            var snapshot = new NSDiffableDataSourceSectionSnapshot<MacFeedListItem>();

            snapshot.AppendItems(items.ToArray());

            return snapshot;
        }

        private void ConfigureDataSource()
        {
            var rowRegistration = UICollectionViewCellRegistration.GetRegistration(typeof(UICollectionViewListCell),
                new UICollectionViewCellRegistrationConfigurationHandler((cell, indexpath, item) =>
                {
                    var sidebarItem = item as MacFeedListItem;
                    if (sidebarItem is null)
                    {
                        return;
                    }

                    var cfg = UIListContentConfiguration.SidebarCellConfiguration;
                    cfg.Text = sidebarItem.Name;
                    cfg.Image = sidebarItem.Image;

                    cell.ContentConfiguration = cfg;
                })
             );

            if (this.collectionView is null)
            {
                throw new NullReferenceException(nameof(this.collectionView));
            }

            this.dataSource = new UICollectionViewDiffableDataSource<NSString, MacFeedListItem>(this.collectionView,
                new UICollectionViewDiffableDataSourceCellProvider((collectionView, indexPath, item) =>
                {
                    var sidebarItem = item as MacFeedListItem;
                    if (sidebarItem is null || this.collectionView is null)
                    {
                        throw new Exception();
                    }

                    return this.collectionView.DequeueConfiguredReusableCell(rowRegistration, indexPath, item);
                })
            );
        }

        /// <summary>
        /// Gets or sets the Feed List VM.
        /// </summary>
        public RssFeedListViewModel FeedListVM { get; set; }

        public async Task ReloadItemsAsync()
        {
            var macFeedListItem = this.FeedListVM.FeedListItems.Select(n => new MacFeedListItem(n));
            var snapshot = this.GetNavigationSnapshot(macFeedListItem);
            this.SetupNavigationItems(this.GetNavigationSnapshot(macFeedListItem));
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            await this.FeedListVM.OnLoad();
        }
    }
}