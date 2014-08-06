using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFox2.Phone.Util
{
    public static class CollectionExtensions
    {
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }
    }
}
