using System;
using System.Linq;

namespace Entith.AspNet.Domain
{
	public static class QueryableExtentions
	{
		/// <summary>
		/// Wraps the IQueryable in an IPaged instance.
		/// </summary>
		/// <returns>The IPaged instance.</returns>
		/// <param name="queryable">The IQueryable to wrap.</param>
		public static IPaged<T> ToPaged<T>(this IQueryable<T> queryable)
		{
			return new Paged<T>(queryable);
		}
	}
}

