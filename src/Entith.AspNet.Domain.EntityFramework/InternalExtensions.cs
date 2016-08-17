using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Entith.AspNet.Domain.EntityFramework
{
    internal static class InternalExtensions
    {
        internal static IQueryable<T> IncludeProperties<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] properties)
        {
            foreach (var property in properties)
                source = source.Include(property);

            return source;
        }
    }
}

