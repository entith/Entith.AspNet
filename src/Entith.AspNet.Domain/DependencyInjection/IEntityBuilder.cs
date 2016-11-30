using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public interface IEntityBuilder<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IEntityBuilder<TEntity, TKey> WithRepository<TRepository, TIRepository>()
            where TRepository : class, IRepository<TEntity, TKey>, TIRepository
            where TIRepository : IRepository<TEntity, TKey>;
        IEntityBuilder<TEntity, TKey> WithService<TService, TIService>()
            where TService : class, IDomainService<TEntity, TKey>, TIService
            where TIService : IDomainService<TEntity, TKey>;
        IEntityBuilder<TEntity, TKey> WithDefaultService();
    }
}
