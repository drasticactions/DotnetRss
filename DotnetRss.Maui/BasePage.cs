// <copyright file="BasePage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using DotnetRss.Core.ViewModels;

namespace DotnetRss.Maui
{
    /// <summary>
    /// Base Page.
    /// </summary>
    public class BasePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public BasePage(IServiceProvider services)
        {
            this.ServiceProvider = services;
        }

        /// <summary>
        /// Gets the internal service provider.
        /// </summary>
        internal IServiceProvider ServiceProvider { get; private set; }

        /// <inheritdoc/>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext is BaseViewModel vm)
            {
                vm.OnLoad().FireAndForgetSafeAsync();
            }
        }
    }
}
