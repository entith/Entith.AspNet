using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public class EntityBuilder<TEntity, TKey> : IEntityBuilder<TEntity, TKey>
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
    {
        private IRegistrationBuilder _builder;

        public EntityBuilder(IRegistrationBuilder builder)
        {
            _builder = builder;
        }

        public IEntityBuilder<TEntity, TKey> WithDefaultService()
        {
            _builder.RegisterTypeAs<DomainService<TEntity, TKey>, IDomainService<TEntity, TKey>>();
            _builder.RegisterTypeAs<DomainService<TEntity, TKey>, IDomainService>();
            _builder.RegisterTypeAsSelf<DomainService<TEntity, TKey>>();

            return this;
        }

        public IEntityBuilder<TEntity, TKey> WithRepository<TRepository, TIRepository>()
            where TRepository : class, IRepository<TEntity, TKey>, TIRepository
            where TIRepository : IRepository<TEntity, TKey>
        {
            _builder.RegisterTypeAs<TRepository, TIRepository>();
            _builder.RegisterTypeAs<TRepository, IRepository<TEntity, TKey>>();
            _builder.RegisterTypeAs<TRepository, IRepository>();
            _builder.RegisterTypeAsSelf<TRepository>();

            return this;
        }

        public IEntityBuilder<TEntity, TKey> WithService<TService, TIService>()
            where TService : class, IDomainService<TEntity, TKey>, TIService
            where TIService : IDomainService<TEntity, TKey>
        {
            _builder.RegisterTypeAs<TService, TIService>();
            _builder.RegisterTypeAs<TService, IDomainService<TEntity, TKey>>();
            _builder.RegisterTypeAs<TService, IDomainService>();
            _builder.RegisterTypeAsSelf<TService>();

            return this;
        }
    }
}
