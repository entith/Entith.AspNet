using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public class DomainBuilder : IDomainBuilder, IRegistrationBuilder
    {
        private IRegistrationBuilder _builder;

        public DomainBuilder(IRegistrationBuilder builder)
        {
            _builder = builder;
        }

        public IEntityBuilder<TEntity, TKey> RegisterEntity<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return new EntityBuilder<TEntity, TKey>(_builder);
        }

        public void RegisterTypeAs<TType, TAs>()
        {
            _builder.RegisterTypeAs<TType, TAs>();
        }

        public void RegisterTypeAsSelf<TType>()
        {
            _builder.RegisterTypeAsSelf<TType>();
        }

        public void BootstrapDomain<TUnitOfWork>()
            where TUnitOfWork : class, IUnitOfWork
        {
            _builder.RegisterTypeAs<TUnitOfWork, IUnitOfWork>();
            _builder.RegisterTypeAs<DomainManager, IDomainManager>();
            _builder.RegisterTypeAs<DomainManager, IRepositoryManager>();
        }

        void IDomainBuilder.RegisterLogicUnit<TLogicUnit>()
        {
            _builder.RegisterTypeAs<TLogicUnit, ILogicUnit>();
        }
    }
}
