using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// IOrderedQueryable implementation of the IOrderedPaged interface.
	/// </summary>
	public class OrderedPaged<T> : PagedBase<IOrderedQueryable<T>, T>, IOrderedPaged<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.OrderedPaged`1"/> class.
		/// </summary>
		/// <param name="source">The IQueryable instance to wrap.</param>
		public OrderedPaged(IOrderedQueryable<T> source)
			: base(source)
		{
		}

		/// <summary>
		/// WARNING: NOT IMPLEMENTED!
		/// This isn't implemented as there shouldn't be any need for it that I can see.
		/// </summary>
		public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			throw new NotImplementedException("CreateOrderedEnumerable() in OrderedPaged isn't implemented as there shouldn't be any need for it that I can see.");
		}

		public IOrderedPaged<T> ThenBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
		{
			return new OrderedPaged<T>(source.ThenBy(orderBy));
		}

		public IOrderedPaged<T> ThenByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
		{
			return new OrderedPaged<T>(source.ThenByDescending(orderBy));
		}
	}
}

