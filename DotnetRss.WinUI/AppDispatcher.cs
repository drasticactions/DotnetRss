using System;
using DotnetRss.Core;
using Microsoft.UI.Dispatching;

namespace DotnetRss.WinUI
{
    public class AppDispatcher : IAppDispatcher
    {
        readonly DispatcherQueue _dispatcherQueue;

        public AppDispatcher(DispatcherQueue dispatcherQueue)
        {
            _dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));
        }

        public bool Dispatch(Action action)
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));
            return _dispatcherQueue.TryEnqueue(() => action());
        }
    }
}
