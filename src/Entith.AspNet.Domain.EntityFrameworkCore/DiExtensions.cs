using Entith.AspNet.Domain.DependencyInjection;
using Entith.AspNet.Domain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public static class DiExtensions
    {
        private static Type _contextType;

        public static void SetEfContextType<TDbContext>(this IDomainBuilder builder)
        {
            _contextType = typeof(TDbContext);
        }

        public static IEntityBuilder<TEntity, TKey> WithDefaultEfRepository<TEntity, TKey>(this IEntityBuilder<TEntity, TKey> builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            MethodInfo method = typeof(DiExtensions).GetTypeInfo().GetMethod("InteralWithDefaultEfRepository", BindingFlags.NonPublic | BindingFlags.Static)
                                    .MakeGenericMethod(new Type[] { _contextType, typeof(TEntity), typeof(TKey) });
            var result = method.Invoke(null, new object[] { builder });

            return (IEntityBuilder<TEntity, TKey>)result;
        }

        private static IEntityBuilder<TEntity, TKey> InteralWithDefaultEfRepository<TDbContext, TEntity, TKey>(this IEntityBuilder<TEntity, TKey> builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
        {
            return builder.WithDefaultEfRepository<TEntity, TKey, TDbContext>();
        }

        public static IEntityBuilder<TEntity, TKey> WithDefaultEfRepository<TEntity, TKey, TDbContext>(this IEntityBuilder<TEntity, TKey> builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
        {
            return builder.WithRepository<EfRepository<TEntity, TKey, TDbContext>, IEfRepository<TEntity, TKey>>();
        }
    }
}
