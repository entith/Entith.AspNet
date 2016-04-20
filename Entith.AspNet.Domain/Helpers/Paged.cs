using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// IQueryable based implementation of the IPaged interface.
	/// </summary>
	public class Paged<T> : PagedBase<IQueryable<T>, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.Paged`1"/> class.
		/// </summary>
		/// <param name="source">The IQueryable instance to wrap.</param>
		public Paged(IQueryable<T> source)
			: base(source)
		{
		}
	}
}

