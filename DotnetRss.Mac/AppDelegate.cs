// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace DotnetRss.Mac;

/// <summary>
/// Main Mac App Delegate.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{
    private MainWindowController? mainWindowController;

    /// <inheritdoc/>
    public override void DidFinishLaunching(NSNotification notification)
    {
        Ioc.Default.ConfigureServices(
                     new ServiceCollection()
                     .AddSingleton<IAppDispatcher>(new MacDispatcher())
                     .AddSingleton<IDatabaseContext, LiteDBDatabaseContext>()
                     .AddSingleton<IErrorHandlerService, LoggerErrorHandlerService>()
                     .AddSingleton<ITemplateService, HandlebarsTemplateService>()
                     .AddSingleton<IRssService, FeedReaderService>()
                     .AddSingleton<IPlatformService, MacPlatformService>()
                     .AddTransient<RssFeedArticleViewModel>()
                     .AddTransient<RssFeedItemListViewModel>()
                     .AddTransient<RssFeedListViewModel>()
                     .BuildServiceProvider());

        this.mainWindowController = new MainWindowController();
        this.mainWindowController.Window.MakeKeyAndOrderFront(this);
    }

    /// <inheritdoc/>
    public override void WillTerminate(NSNotification notification)
    {
        // Insert code here to tear down your application
    }
}
