using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Extensions
    {
        public static IList<T> PipedAdd<T>(this IList<T> l, T item)
        {
            l.Add(item);
            return l;
        }
    }
}
