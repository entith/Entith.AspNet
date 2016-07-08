using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// Abstract class containing all the shared functionality for Paged and OrderedPaged.
    /// </summary>
    public abstract class PagedBase<Q, T> : IPaged<T> where Q : IQueryable<T>
    {
        protected readonly Q source;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entith.AspNet.Domain.PagedBase`2"/> class.
        /// </summary>
        /// <param name="source">The IQueryable instance to wrap.</param>
        public PagedBase(Q source)
        {
            this.source = source;
        }

        /// <summary>
        /// Return this container as an IEnumerator
        /// </summary>
        /// <returns>IEnumerator instance of this container.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        /// <summary>
        /// Return this container as an IEnumerator
        /// </summary>
        /// <returns>IEnumerator instance of this container.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return source.Count(); }
        }

        public IEnumerable<T> GetRange(int index, int count)
        {
            return source.Skip(index).Take(count);
        }

        public IEnumerable<T> GetPage(int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IOrderedPaged<T> OrderBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return new OrderedPaged<T>(source.OrderBy(orderBy));
        }

        public IOrderedPaged<T> OrderByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return new OrderedPaged<T>(source.OrderByDescending(orderBy));
        }
    }
}

