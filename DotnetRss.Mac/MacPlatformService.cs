// <copyright file="MacPlatformService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;

namespace DotnetRss.Mac
{
    /// <summary>
    /// Mac Platform Service.
    /// </summary>
    public class MacPlatformService : IPlatformService
    {
        /// <inheritdoc/>
        public Task OpenBrowserAsync(string url)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ShareUrlAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}