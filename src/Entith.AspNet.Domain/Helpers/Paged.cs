using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Entith.AspNet.Domain
{
    public class Paged<T> : PagedBase<IQueryable<T>, T>
    {
        public Paged(IQueryable<T> source)
            : base(source)
        {
        }
    }
}

