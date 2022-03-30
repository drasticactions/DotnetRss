// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Maui;

/// <summary>
/// Main Maui Program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Create Maui App.
    /// </summary>
    /// <returns><see cref="MauiApp"/>.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Services.AddSingleton<IAppDispatcher>(new AppDispatcher())
                .AddSingleton<IDatabaseContext, LiteDBDatabaseContext>()
                .AddSingleton<IErrorHandlerService, LoggerErrorHandlerService>()
                .AddSingleton<ITemplateService, HandlebarsTemplateService>()
                .AddSingleton<IRssService, FeedReaderService>()
                .AddTransient<RssFeedArticleViewModel>()
                .AddTransient<RssFeedItemListViewModel>()
                .AddTransient<RssFeedListViewModel>();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
