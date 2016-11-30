using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    public abstract class PagedBase<Q, T> : IPaged<T> where Q : IQueryable<T>
    {
        protected readonly Q source;

        public PagedBase(Q source)
        {
            this.source = source;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

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

        public int GetPages(int pageSize)
        {
            int count = Count;
            return (count / pageSize) + ((count % pageSize) == 0 ? 0 : 1);
        }
    }
}

