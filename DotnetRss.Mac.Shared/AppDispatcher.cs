// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DotnetRss.Core;

namespace DotnetRss.Mac.Shared
{
    /// <summary>
    /// App Dispatcher.
    /// </summary>
    public class AppDispatcher : NSObject, IAppDispatcher
    {
        /// <inheritdoc/>
        public bool Dispatch(Action action)
        {
            this.InvokeOnMainThread(action);
            return true;
        }
    }
}