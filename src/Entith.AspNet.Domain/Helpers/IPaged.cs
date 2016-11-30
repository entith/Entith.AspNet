using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    public interface IPaged<T> : IEnumerable<T>
    {
        int Count { get; }

        int GetPages(int pageSize);

        IEnumerable<T> GetRange(int index, int count);

        IEnumerable<T> GetPage(int page, int pageSize);

        IOrderedPaged<T> OrderBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        IOrderedPaged<T> OrderByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);
    }
}

