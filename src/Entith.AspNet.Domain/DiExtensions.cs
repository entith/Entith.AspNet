using System;
using Entith.AspNet.Domain;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
        public static void RegisterService<TEntity, TKey>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            builder.RegisterService<TEntity, TKey, DomainService<TEntity, TKey>>();
        }

        public static void RegisterService<TEntity, TKey, TService>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TService : class, IDomainService<TEntity, TKey>
        {
            builder.RegisterTypeAs<TService, IDomainService<TEntity, TKey>>();
            builder.RegisterTypeAsSelf<TService>();
        }

        public static void RegisterService<TEntity, TKey, TService, TRepository>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TService : class, IDomainService<TEntity, TKey, TRepository>
            where TRepository : IRepository<TEntity, TKey>
        {
            builder.RegisterTypeAs<TService, IDomainService<TEntity, TKey, TRepository>>();
            builder.RegisterService<TEntity, TKey, TService>();
        }

        public static void RegisterRepository<TEntity, TKey, TRepository>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TRepository : class, IRepository<TEntity, TKey>
        {
            builder.RegisterTypeAs<TRepository, IRepository<TEntity, TKey>>();
            builder.RegisterTypeAs<TRepository, IRepository>();
            builder.RegisterTypeAsSelf<TRepository>();
        }

        public static void RegisterServiceRepository<TEntity, TKey, TRepository>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TRepository : class, IRepository<TEntity, TKey>
        {
            builder.RegisterService<TEntity, TKey>();
            builder.RegisterRepository<TEntity, TKey, TRepository>();
        }

        public static void RegisterServiceRepository<TEntity, TKey, TService, TRepository>(this ISimpleRegistrationBuilder builder)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TService : class, IDomainService<TEntity, TKey>
            where TRepository : class, IRepository<TEntity, TKey>
        {
            builder.RegisterService<TEntity, TKey, TService>();
            builder.RegisterRepository<TEntity, TKey, TRepository>();
        }
    }
}

