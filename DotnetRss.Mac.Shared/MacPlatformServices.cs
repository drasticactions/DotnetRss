// <copyright file="MacPlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core;

namespace DotnetRss.Mac.Shared
{
    public class MacPlatformServices : IPlatformService
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