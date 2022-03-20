using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    public interface IRssService
    {
        Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadFeedAsync(string feedUri, CancellationToken? token = default);
    }
}
