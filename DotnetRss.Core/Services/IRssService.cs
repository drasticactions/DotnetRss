using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRss.Core
{
    public interface IRssService
    {
        Task<(FeedListItem?, IList<FeedItem>?)> ReadFeedAsync(string feedUri, CancellationToken? token = default);
    }
}
