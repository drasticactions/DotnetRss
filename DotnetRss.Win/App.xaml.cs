// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DotnetRss.Win
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? m_window;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
                .AddSingleton<IDatabaseContext, LiteDBDatabaseContext>()
                .AddSingleton<IErrorHandlerService, LoggerErrorHandlerService>()
                .AddSingleton<ITemplateService, HandlebarsTemplateService>()
                .AddSingleton<IRssService, FeedReaderService>()
                .AddSingleton<IPlatformService, WindowsPlatformService>()
                .AddTransient<RssFeedArticleViewModel>()
                .AddTransient<RssFeedItemListViewModel>()
                .AddTransient<RssFeedListViewModel>()
                .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.m_window = new MainWindow();
            this.m_window.Activate();
        }
    }
}
