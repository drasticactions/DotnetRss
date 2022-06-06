// <copyright file="MasterFeedViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DotnetRss.Mac.Shared
{
    public class MasterFeedViewController : UITableViewController
    {
        private UIBarButtonItem filterButton;

        public MasterFeedViewController()
        {
            this.filterButton = new UIBarButtonItem();

            this.TableView.DataSource = this;
            this.TableView.Delegate = this;

            this.ClearsSelectionOnViewWillAppear = false;
            this.ExtendedLayoutIncludesOpaqueBars = true;

            this.NavigationItem.RightBarButtonItem = this.filterButton;
            this.NavigationItem.Title = "Feeds";

            this.filterButton.Image = UIImage.GetSystemImage("line.3.horizontal.decrease.circle");

            this.TableView.AlwaysBounceVertical = true;
            this.TableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            this.TableView.BackgroundColor = UIColor.SystemBackground;
            this.TableView.ClipsToBounds = true;
            this.TableView.SectionFooterHeight = 18;
            this.TableView.SectionHeaderHeight = 18;
        }
    }
}

