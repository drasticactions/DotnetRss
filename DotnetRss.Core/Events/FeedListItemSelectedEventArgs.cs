using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    public class FeedListItemSelectedEventArgs : EventArgs
    {
        private readonly FeedListItem feedItem;

        public FeedListItemSelectedEventArgs(FeedListItem item)
        {
            this.feedItem = item;
        }

        public FeedListItem FeedListItem => this.feedItem;
    }
}
