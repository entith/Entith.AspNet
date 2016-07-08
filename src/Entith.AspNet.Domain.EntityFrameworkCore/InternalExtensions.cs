using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Entith.AspNet.Domain.EntityFramework;
using System.Reflection;

namespace Entith.AspNet.Domain.EntityFramework
{
    internal static class InternalExtensions
    {
        //internal static IQueryable<T> IncludeProperties<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] properties)
        //    where T : class
        //{
        //    foreach (var property in properties)
        //    {
        //        source = source.Include(property);
        //    }

        //    return source;
        //}
    }
}

