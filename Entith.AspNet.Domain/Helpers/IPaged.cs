using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// Provides a wrapper around IQueryable to discourage the presentation
    /// layer from manipulating queries directly.
    /// </summary>
    public interface IPaged<T> : IEnumerable<T>
    {
        /// <summary>
        /// The total number of entities.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets a range of entities specified by the index and count
        /// </summary>
        /// <returns>Entities in range.</returns>
        /// <param name="index">The index of the first entity to get.</param>
        /// <param name="count">How many entities to get.</param>
        IEnumerable<T> GetRange(int index, int count);

        /// <summary>
        /// Gets a page of entities
        /// </summary>
        /// <returns>The entites in the specified page.</returns>
        /// <param name="page">The page number to get.</param>
        /// <param name="pageSize">How many entities per page.</param>
        IEnumerable<T> GetPage(int page, int pageSize);

        /// <summary>
        /// Orders the entities.
        /// </summary>
        /// <returns>Ordered container of entities.</returns>
        /// <param name="orderBy">The propery to order by.</param>
        /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
        IOrderedPaged<T> OrderBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        /// <summary>
        /// Orders the entities in reverse order.
        /// </summary>
        /// <returns>Ordered container of entities.</returns>
        /// <param name="orderBy">The propery to order by.</param>
        /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
        IOrderedPaged<T> OrderByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);
    }
}

