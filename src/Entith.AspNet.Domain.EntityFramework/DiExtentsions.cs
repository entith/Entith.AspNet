using System;
using Entith.AspNet.Domain.EntityFramework;
using Entith.AspNet.Domain;
using System.Data.Entity;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {

        public static void RegisterEfRepository<TEntity, TKey, TDbContext>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
        {
            builder.RegisterRepository<TEntity, TKey, EfRepository<TEntity, TKey, TDbContext>>();
        }

        public static void RegisterServiceEfRepository<TEntity, TKey, TService, TDbContext>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TService : class, IDomainService<TEntity, TKey>
            where TDbContext : DbContext
        {
            builder.RegisterService<TEntity, TKey, TService>();
            builder.RegisterEfRepository<TEntity, TKey, TDbContext>();
        }

        public static void RegisterServiceEfRepository<TEntity, TKey, TDbContext>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
        {
            builder.RegisterService<TEntity, TKey>();
            builder.RegisterEfRepository<TEntity, TKey, TDbContext>();
        }

        public static void RegisterEfUnitOfWork<TDbContext>(this ISimpleRegistrationBuilder builder)
            where TDbContext : DbContext
        {
            builder.RegisterTypeAs<EfUnitOfWork<TDbContext>, IUnitOfWork>();
        }

        public static void BootstrapDomainEntityFramework<TDbContext>(this ISimpleRegistrationBuilder builder)
            where TDbContext : DbContext
        {
            builder.RegisterTypeAs<TDbContext, DbContext>();
            builder.RegisterTypeAsSelf<TDbContext>();
            builder.RegisterEfUnitOfWork<TDbContext>();
        }
    }
}

