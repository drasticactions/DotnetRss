// <copyright file="MasterFeedViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class MasterFeedViewController : UITableViewController
    {
        private UIBarButtonItem filterButton;
        private UIBarButtonItem newItemButton;

        public MasterFeedViewController()
        {
            this.filterButton = new UIBarButtonItem();
            this.newItemButton = new UIBarButtonItem();

            this.filterButton.Title = "Filter";
            this.newItemButton.Title = "New Feed";

            this.TableView.DataSource = this;
            this.TableView.Delegate = this;

            this.ClearsSelectionOnViewWillAppear = false;
            this.ExtendedLayoutIncludesOpaqueBars = true;

            this.NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { this.newItemButton }, false);
            this.NavigationItem.Title = "Feeds";

            this.newItemButton.Image = UIImage.GetSystemImage("arrow.triangle.2.circlepath");
            this.filterButton.Image = UIImage.GetSystemImage("line.3.horizontal.decrease.circle");

            this.TableView.AlwaysBounceVertical = true;
            this.TableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            this.TableView.BackgroundColor = UIColor.SystemBackground;
            this.TableView.ClipsToBounds = true;
            this.TableView.SectionFooterHeight = 18;
            this.TableView.SectionHeaderHeight = 18;
        }

        /// <summary>
        /// Gets or sets the Feed List VM.
        /// </summary>
        public RssFeedListViewModel? FeedListVM { get; set; }
    }
}