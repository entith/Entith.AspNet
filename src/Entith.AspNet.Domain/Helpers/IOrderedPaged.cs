using System;
using System.Linq;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// Provides a wrapper around IOrderedQueryable to discourage the presentation
    /// layer from manipulating queries directly. The ordered counterpart to IPaged.
    /// </summary>
    public interface IOrderedPaged<T> : IPaged<T>, IOrderedEnumerable<T>
    {
        /// <summary>
        /// Specify secondary proprties to order by.
        /// </summary>
        /// <returns>Ordered container of entities.</returns>
        /// <param name="orderBy">The propery to order by.</param>
        /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
        IOrderedPaged<T> ThenBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        /// <summary>
        /// Specify secondary proprties to order by in reverse order.
        /// </summary>
        /// <returns>Ordered container of entities.</returns>
        /// <param name="orderBy">The propery to order by.</param>
        /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
        IOrderedPaged<T> ThenByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);
    }
}

