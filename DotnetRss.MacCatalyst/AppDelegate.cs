// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;
using DotnetRss.Mac.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace DotnetRss.MacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override UIWindow? Window
    {
        get;
        set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IAppDispatcher>(new AppDispatcher())
                .AddSingleton<IDatabaseContext, LiteDBDatabaseContext>()
                .AddSingleton<IErrorHandlerService, LoggerErrorHandlerService>()
                .AddSingleton<ITemplateService, HandlebarsTemplateService>()
                .AddSingleton<IRssService, FeedReaderService>()
                .AddSingleton<IPlatformService, MacPlatformServices>()
                .AddTransient<RssFeedArticleViewModel>()
                .AddTransient<RssFeedItemListViewModel>()
                .AddTransient<RssFeedListViewModel>()
                .BuildServiceProvider());

        // create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new RootSplitViewController(Ioc.Default);
        Window.RootViewController = vc;

        // make the window visible
        Window.MakeKeyAndVisible();

        return true;
    }
}
