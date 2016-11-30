using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public interface IDomainBuilder : IRegistrationBuilder
    {
        IEntityBuilder<TEntity, TKey> RegisterEntity<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;

        void BootstrapDomain<TUnitOfWork>()
            where TUnitOfWork : class, IUnitOfWork;

        void RegisterLogicUnit<TLogicUnit>()
            where TLogicUnit : class, ILogicUnit;
    }
}
