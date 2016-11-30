using System;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    public interface IUnitOfWork : IChangeTracker, IDisposable
    {
        void SaveChanges();

        void ClearChanges(IEntity entity);

        void ClearAllChanges();

        void RegisterDomainManager(IDomainManager manager);
    }
}

