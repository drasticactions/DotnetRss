using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace DotnetRss.WinUI
{
    public static class WinUIExtensions
    {
        public static IRandomAccessStream ToRandomAccessStream(this byte[] array)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
            {
                writer.WriteBytes(array);
                writer.StoreAsync().GetResults();
            }

            return ms;
        }
    }
}
