// <copyright file="MainWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Mac
{
    /// <summary>
    /// Main Window.
    /// </summary>
    public class MainWindow : NSWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="contentRect">Content Rectangle.</param>
        /// <param name="aStyle"><see cref="NSWindowStyle"/>.</param>
        /// <param name="bufferingType"><see cref="NSBackingStore"/>.</param>
        /// <param name="deferCreation">Defer Creation.</param>
        public MainWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation)
            : base(contentRect, aStyle, bufferingType, deferCreation)
        {
            this.Title = "DotnetRss";

            // Create the content view for the window and make it fill the window
            this.ContentView = new NSView(this.Frame);
        }

        /// <inheritdoc/>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
