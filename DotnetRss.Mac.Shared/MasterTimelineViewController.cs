// <copyright file="MasterTimelineViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class MasterTimelineViewController : UITableViewController
    {
        public MasterTimelineViewController()
        {
            this.TableView.DataSource = this;
            this.TableView.Delegate = this;

            this.ClearsSelectionOnViewWillAppear = false;
            this.ExtendedLayoutIncludesOpaqueBars = true;

            this.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
            this.NavigationItem.Title = "Timeline";

            this.TableView.AlwaysBounceVertical = true;
            this.TableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            this.TableView.BackgroundColor = UIColor.SystemBackground;
            this.TableView.ClipsToBounds = true;
            this.TableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
            this.TableView.SectionFooterHeight = 28;
            this.TableView.SectionHeaderHeight = 28;
        }

        /// <summary>
        /// Gets or sets the Feed Item List VM.
        /// </summary>
        public RssFeedItemListViewModel? FeedItemListVM { get; set; }
    }
}