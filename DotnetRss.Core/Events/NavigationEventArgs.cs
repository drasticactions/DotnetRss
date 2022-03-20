// <copyright file="NavigationEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    public class NavigationEventArgs : EventArgs
    {
        public Type Type { get; }

        public object? Arguments { get; }

        public NavigationEventArgs(Type type!!, object? arguments)
        {
            this.Type = type;
            this.Arguments = arguments;
        }
    }
}
