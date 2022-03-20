using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    public interface IAppDispatcher
    {
        bool Dispatch(Action action);

        bool DispatchDelayed(TimeSpan delay, Action action);

        IAppDispatcherTimer CreateTimer();

        bool IsDispatchRequired { get; }
    }

    public interface IAppDispatcherTimer
    {
        TimeSpan Interval { get; set; }

        bool IsRepeating { get; set; }

        bool IsRunning { get; }

        event EventHandler Tick;

        void Start();

        void Stop();
    }
}
