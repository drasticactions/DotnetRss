// <copyright file="IPlatformService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    /// <summary>
    /// Cross Platform Services.
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// Share a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task.</returns>
        Task ShareUrlAsync(string url);

        /// <summary>
        /// Open a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task.</returns>
        Task OpenBrowserAsync(string url);
    }
}
