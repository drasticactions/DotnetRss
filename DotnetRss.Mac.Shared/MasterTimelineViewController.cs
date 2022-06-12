// <copyright file="MasterTimelineViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Mac.Shared
{
    public class MasterTimelineViewController : UITableViewController
    {
        public MasterTimelineViewController(RssFeedItemListViewModel vm)
        {
            this.FeedItemListVM = vm;
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
            this.FeedItemListVM.OnFeedListItemUpdated += FeedItemListVM_OnFeedListItemUpdated;
            this.FeedItemListVM.PropertyChanged += FeedItemListVM_PropertyChanged;
        }

        private void FeedItemListVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(this.FeedItemListVM.FeedItems))
            {
                this.TableView.Source = new TimelineViewSource(this, this.FeedItemListVM.FeedItems);
                this.TableView.ReloadData();
            }
        }

        private void FeedItemListVM_OnFeedListItemUpdated(object? sender, FeedListItemUpdatedEventArgs e)
        {
        }

        /// <summary>
        /// Gets or sets the Feed Item List VM.
        /// </summary>
        public RssFeedItemListViewModel FeedItemListVM { get; set; }

        public class TimelineViewSource : UITableViewSource
        {
            private MasterTimelineViewController controller;
            private List<FeedItem> feedItems;

            private static string cellId = "cellid";

            public TimelineViewSource(MasterTimelineViewController controller, IEnumerable<FeedItem> items)
            {
                this.controller = controller;
                this.feedItems = items.ToList();
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(cellId);
                if (cell is null)
                {
                    cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellId);
                    cell.TextLabel.Lines = 0;
                    cell.TextLabel.LineBreakMode = UILineBreakMode.TailTruncation;
                    cell.TextLabel.AllowsDefaultTighteningForTruncation = false;
                    cell.TextLabel.AdjustsFontForContentSizeCategory = true;
                }

                var item = this.feedItems[indexPath.Row];
                cell.TextLabel.Text = item.Title;
                return cell;
            }

            public override async void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var item = this.feedItems[indexPath.Row];
                if (item is not null)
                {
                    await this.controller.FeedItemListVM.FeedItemSelectedCommand.ExecuteAsync(item);
                }
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                nint count = this.feedItems.Count;
                return count;
            }
        }
    }
}