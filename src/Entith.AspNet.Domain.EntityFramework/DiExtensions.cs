using Entith.AspNet.Domain.DependencyInjection;
using Entith.AspNet.Domain.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public static class DiExtensions
    {
        public static IEntityBuilder<TEntity, TKey> WithDefaultEfRepository<TEntity, TKey>(this IEntityBuilder<TEntity, TKey> builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return builder.WithRepository<IEfRepository<TEntity, TKey>>();
        }
    }
}
