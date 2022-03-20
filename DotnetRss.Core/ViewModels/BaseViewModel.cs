// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using AngleSharp.Html.Parser;
using CodeHollow.FeedReader;

namespace DotnetRss.Core.ViewModels
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private string title = string.Empty;
        private HttpClient client;
        private HtmlParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public BaseViewModel(IServiceProvider services!!)
        {
            this.client = new HttpClient();
            this.parser = new HtmlParser();
            this.Services = services;
            this.Templates = services.GetService(typeof(ITemplateService)) as ITemplateService ?? throw new NullReferenceException(nameof(ITemplateService));
            this.Dispatcher = services.GetService(typeof(IAppDispatcher)) as IAppDispatcher ?? throw new NullReferenceException(nameof(IAppDispatcher));
            this.ErrorHandler = services.GetService(typeof(IErrorHandlerService)) as IErrorHandlerService ?? throw new NullReferenceException(nameof(IErrorHandlerService));
            this.Context = services.GetService(typeof(IDatabaseContext)) as IDatabaseContext ?? throw new NullReferenceException(nameof(IDatabaseContext));
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets a baseline navigation handler.
        /// Handle this to handle navigation events within the view model.
        /// </summary>
        public event EventHandler<NavigationEventArgs>? Navigation;

        /// <summary>
        /// Fired when a feed list item updates.
        /// </summary>
        public event EventHandler<FeedListItemUpdatedEventArgs>? OnFeedListItemUpdated;

        /// <summary>
        /// Fired when a feed item updates.
        /// </summary>
        public event EventHandler<FeedItemUpdatedEventArgs>? OnFeedItemUpdated;

        /// <summary>
        /// Gets or sets a value indicating whether the VM is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets the Error Handler.
        /// </summary>
        internal IErrorHandlerService ErrorHandler { get; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/>.
        /// </summary>
        internal IServiceProvider Services { get; }

        /// <summary>
        /// Gets the Dispatcher.
        /// </summary>
        internal IAppDispatcher Dispatcher { get; }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        internal IDatabaseContext Context { get; }

        /// <summary>
        /// Gets the templates context.
        /// </summary>
        internal ITemplateService Templates { get; }

        /// <summary>
        /// Called on VM Load.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task OnLoad()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends a navigation request to whatever handlers attach to it.
        /// </summary>
        /// <param name="viewModel">The view model type.</param>
        /// <param name="arguments">Arguments to send to the view model.</param>
        public void SendNavigationRequest(Type viewModel, object? arguments = default)
        {
            if (viewModel.IsSubclassOf(typeof(BaseViewModel)))
            {
                this.Navigation?.Invoke(this, new NavigationEventArgs(viewModel, arguments));
            }
        }

        /// <summary>
        /// Adds New Feed List.
        /// </summary>
        /// <param name="feedUri">The Feed Uri.</param>
        /// <returns>Task.</returns>
        public async Task<FeedListItem?> AddOrUpdateNewFeedListItemAsync(string feedUri)
        {
            try
            {
                var feed = await FeedReader.ReadAsync(feedUri);
                var item = this.Context.GetFeedListItem(new Uri(feedUri));
                if (item is null)
                {
                    item = new FeedListItem(feed, feedUri);
                }

                if (item.ImageCache is null && item.ImageUri is not null)
                {
                    item.ImageCache = await this.client.GetByteArrayAsync(item.ImageUri);
                }
                else if (item.ImageCache is null)
                {
                    item.ImageCache = GetPlaceholderIcon();
                }

                var result = this.Context.AddOrUpdateFeedListItem(item);

                foreach (var feedItem in feed.Items)
                {
                    using var document = await this.parser.ParseDocumentAsync(feedItem.Content);
                    var image = document.QuerySelector("img");
                    var imageUrl = string.Empty;
                    if (image is not null)
                    {
                        imageUrl = image.GetAttribute("src");
                    }

                    var fd = new FeedItem(item, feedItem, imageUrl);
                    this.Context.AddOrUpdateFeedItem(fd);
                    this.SendFeedUpdateRequest(item, fd);
                }

                this.SendFeedListUpdateRequest(item);

                return item;
            }
            catch (Exception ex)
            {
                this.ErrorHandler.HandleError(ex);
            }

            return null;
        }

        /// <summary>
        /// Called when wanting to raise a Command Can Execute.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
        }

        /// <summary>
        /// Call OnFeedListItemUpdated event handler.
        /// </summary>
        /// <param name="item">Feed List Item.</param>
        internal void SendFeedListUpdateRequest(FeedListItem item!!)
        {
            this.OnFeedListItemUpdated?.Invoke(this, new FeedListItemUpdatedEventArgs(item));
        }

        /// <summary>
        /// Call OnFeedListItemUpdated event handler..
        /// </summary>
        /// <param name="feedItem">Feed List Item.</param>
        /// <param name="item">Feed Item.</param>
        internal void SendFeedUpdateRequest(FeedListItem feedItem!!, FeedItem item!!)
        {
            this.OnFeedItemUpdated?.Invoke(this, new FeedItemUpdatedEventArgs(feedItem, item));
        }

#pragma warning disable SA1600 // Elements should be documented
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
#pragma warning restore SA1600 // Elements should be documented
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// On Property Changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.Dispatcher?.Dispatch(() =>
            {
                var changed = this.PropertyChanged;
                if (changed == null)
                {
                    return;
                }

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        private static byte[] GetPlaceholderIcon()
        {
            var resource = GetResourceFileContent("Icon.favicon.ico");
            if (resource is null)
            {
                throw new Exception("Failed to get placeholder icon.");
            }

            using MemoryStream ms = new MemoryStream();
            resource.CopyTo(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Get Resource File Content via FileName.
        /// </summary>
        /// <param name="fileName">Filename.</param>
        /// <returns>Stream.</returns>
        private static Stream? GetResourceFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DotnetRss.Core." + fileName;
            if (assembly is null)
            {
                return null;
            }

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
