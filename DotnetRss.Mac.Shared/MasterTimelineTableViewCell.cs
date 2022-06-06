// <copyright file="MasterTimelineTableViewCell.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using ObjCRuntime;

namespace DotnetRss.Mac.Shared
{
    public class MasterTimelineTableViewCell : UITableViewCell
    {
        public MasterTimelineTableViewCell()
        {
            this.SetupUI();
        }

        public MasterTimelineTableViewCell(NSCoder coder) : base(coder)
        {
            this.SetupUI();
        }

        public MasterTimelineTableViewCell(CGRect frame) : base(frame)
        {
            this.SetupUI();
        }

        public MasterTimelineTableViewCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            this.SetupUI();
        }

        public MasterTimelineTableViewCell(UITableViewCellStyle style, NSString? reuseIdentifier) : base(style, reuseIdentifier)
        {
            this.SetupUI();
        }

        protected MasterTimelineTableViewCell(NSObjectFlag t) : base(t)
        {
            this.SetupUI();
        }

        protected internal MasterTimelineTableViewCell(NativeHandle handle) : base(handle)
        {
            this.SetupUI();
        }

        private void SetupUI()
        {
            this.ClipsToBounds = true;
            this.Frame = new CGRect(0, 44.5, 414, 208);
            this.PreservesSuperviewLayoutMargins = true;
            this.ContentView.ClipsToBounds = true;
            this.ContentView.ContentMode = UIViewContentMode.Center;
            this.ContentView.InsetsLayoutMarginsFromSafeArea = false;
            this.ContentView.MultipleTouchEnabled = true;
            this.ContentView.PreservesSuperviewLayoutMargins = true;
        }
    }
}