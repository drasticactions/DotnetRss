// <copyright file="MainWindowController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotnetRss.Mac
{
    /// <summary>
    /// Main Window Controller.
    /// </summary>
    public class MainWindowController : NSWindowController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowController"/> class.
        /// </summary>
        public MainWindowController()
            : base()
        {
            CGRect contentRect = new CGRect(0, 0, 1000, 500);
            this.Window = new MainWindow(contentRect, NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable, NSBackingStore.Buffered, false);
        }
    }
}
