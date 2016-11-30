using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public interface IRepositoryManager
    {
        TRepository GetRepository<TEntity, TRepository>()
               where TEntity : class, IEntity
               where TRepository : class, IRepository<TEntity>;
    }
}
