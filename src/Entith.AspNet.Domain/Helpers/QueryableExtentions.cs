using System;
using System.Linq;

namespace Entith.AspNet.Domain
{
    public static class QueryableExtentions
    {
        public static IPaged<T> ToPaged<T>(this IQueryable<T> queryable)
        {
            return new Paged<T>(queryable);
        }
    }
}

