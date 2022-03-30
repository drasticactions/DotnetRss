// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using Microsoft.Maui.Dispatching;

namespace DotnetRss.Maui
{
    /// <summary>
    /// App Dispatcher.
    /// </summary>
    public class AppDispatcher : IAppDispatcher
    {
        /// <inheritdoc/>
        public bool Dispatch(Action action)
        {
            return Dispatcher.GetForCurrentThread().Dispatch(action);
        }
    }
}
