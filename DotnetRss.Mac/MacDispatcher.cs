// <copyright file="MacDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;

namespace DotnetRss.Mac
{
    /// <summary>
    /// Mac Dispatcher.
    /// </summary>
    public class MacDispatcher : NSObject, IAppDispatcher
    {
        /// <inheritdoc/>
        public bool Dispatch(Action action)
        {
            this.InvokeOnMainThread(action);
            return true;
        }
    }
}