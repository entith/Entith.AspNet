using System;
using System.Linq;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    public interface IOrderedPaged<T> : IPaged<T>, IOrderedEnumerable<T>
    {
        IOrderedPaged<T> ThenBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        IOrderedPaged<T> ThenByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);
    }
}

