// <copyright file="MasterTimelineTableViewCell.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using ObjCRuntime;

namespace DotnetRss.Mac.Shared
{
    public class MasterTimelineTableViewCell : UITableViewCell
    {
        private UILabel titleView = GenerateMultiLineUILabel();
        private UILabel summaryView = GenerateMultiLineUILabel();
        private MasterUnreadIndicatorView unreadIndicatorView = new MasterUnreadIndicatorView(CGRect.Empty);
        private UILabel dateView = GenerateSingleLineUILabel();
        private UILabel feedNameView = GenerateSingleLineUILabel();

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
            //this.Frame = new CGRect(0, 44.5, 414, 208);
            //this.PreservesSuperviewLayoutMargins = true;
            //this.ContentView.ClipsToBounds = true;
            //this.ContentView.ContentMode = UIViewContentMode.Center;
            //this.ContentView.InsetsLayoutMarginsFromSafeArea = false;
            //this.ContentView.MultipleTouchEnabled = true;
            //this.ContentView.PreservesSuperviewLayoutMargins = true;

            this.AddSubviewAtInit(this.titleView, false);
            this.AddSubviewAtInit(this.summaryView, true);
            this.AddSubviewAtInit(this.unreadIndicatorView, true);
            this.AddSubviewAtInit(this.dateView, false);
            this.AddSubviewAtInit(this.feedNameView, true);

        }

        private void AddSubviewAtInit(UIView view, bool hidden)
        {
            this.AddSubview(view);
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            view.Hidden = hidden;
        }

        private static UILabel GenerateMultiLineUILabel()
        {
            var label = new NonIntrinsicLabel();
            label.Lines = 0;
            label.LineBreakMode = UILineBreakMode.TailTruncation;
            label.AllowsDefaultTighteningForTruncation = false;
            label.AdjustsFontForContentSizeCategory = true;
            return label;
        }

        private static UILabel GenerateSingleLineUILabel()
        {
            var label = new NonIntrinsicLabel();
            label.LineBreakMode = UILineBreakMode.TailTruncation;
            label.AllowsDefaultTighteningForTruncation = false;
            label.AdjustsFontForContentSizeCategory = true;
            return label;
        }
    }
}