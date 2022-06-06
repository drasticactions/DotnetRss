// <copyright file="MasterFeedTableViewCell.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>


using ObjCRuntime;

namespace DotnetRss.Mac.Shared
{
    public class MasterFeedTableViewCell : UITableViewCell
    {
        public MasterFeedTableViewCell()
        {
            this.SetupUI();
        }

        public MasterFeedTableViewCell(NSCoder coder) : base(coder)
        {
            this.SetupUI();
        }

        public MasterFeedTableViewCell(CGRect frame) : base(frame)
        {
            this.SetupUI();
        }

        public MasterFeedTableViewCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            this.SetupUI();
        }

        public MasterFeedTableViewCell(UITableViewCellStyle style, NSString? reuseIdentifier) : base(style, reuseIdentifier)
        {
            this.SetupUI();
        }

        protected MasterFeedTableViewCell(NSObjectFlag t) : base(t)
        {
            this.SetupUI();
        }

        protected internal MasterFeedTableViewCell(NativeHandle handle) : base(handle)
        {
            this.SetupUI();
        }

        private void SetupUI()
        {
            this.ClipsToBounds = true;
            this.Frame = new CGRect(0, 49.5, 414, 43.5);
            this.PreservesSuperviewLayoutMargins = true;
            this.ContentView.ClipsToBounds = true;
            this.ContentView.ContentMode = UIViewContentMode.Center;
            this.ContentView.InsetsLayoutMarginsFromSafeArea = false;
            this.ContentView.MultipleTouchEnabled = true;
            this.ContentView.PreservesSuperviewLayoutMargins = true;
        }
    }
}