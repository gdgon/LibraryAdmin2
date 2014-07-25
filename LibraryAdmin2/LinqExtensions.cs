using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> sequence)
        {
            return sequence.Where(e => e != null);
        }
    }
}