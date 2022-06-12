// <copyright file="IMasterTimelineCellLayout.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Runtime.InteropServices;

namespace DotnetRss.Mac.Shared
{
    public interface IMasterTimelineCellLayout
    {
        CGRect UnreadIndicatorRect { get; }

        NFloat Height { get; }

        CGRect StarRect { get; }

        CGRect IconImageRect { get; }

        CGRect TitleRect { get; }

        CGRect SummaryRect { get; }

        CGRect FeedNameRect { get; }

        CGRect DateRect { get; }
    }
}