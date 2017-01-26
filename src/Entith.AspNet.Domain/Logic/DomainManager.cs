using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public class DomainManager : IDomainManager
    {
        private static ProxyGenerator ProxyGenerator { get; set; } = new ProxyGenerator();

        private IEnumerable<ILogicUnit> _logicUnits;
        private IEnumerable<IRepository> _repositories;
        private IUnitOfWork _uow;

        public DomainManager(IUnitOfWork uow, IEnumerable<ILogicUnit> logicUnits, IEnumerable<IRepository> repositories)
        {
            _uow = uow;
            _logicUnits = logicUnits;
            _repositories = repositories;

            uow.RegisterDomainManager(this);

            foreach(var unit in logicUnits)
            {
                unit.Init(this, uow);
            }
        }

        public void OnSaveChanges()
        {
            foreach (var unit in _logicUnits)
            {
                unit.OnSaveChanges();
            }
        }

        public void PostSaveChanges()
        {
            foreach (var unit in _logicUnits)
            {
                unit.PostSaveChanges();
            }
        }

        public TRepository GetRepository<TEntity, TRepository>()
            where TEntity : class, IEntity
            where TRepository : class, IRepository<TEntity>
        {
            var repo = _repositories.OfType<TRepository>().FirstOrDefault();
            var interceptor = new RepositoryInterceptor<TEntity>(this);
            //var dp = new ProxyGenerator();
            return ProxyGenerator.CreateInterfaceProxyWithTarget(repo, interceptor);
        }

        protected ICollection<ILogicUnit> GetUnitsForEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            return _logicUnits.Where(u => u.GetEntityType().IsAssignableFrom(entity.GetType())).ToList();
        }

        public void OnAdd<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            foreach (var unit in GetUnitsForEntity(entity))
            {
                unit.DoAdd(entity);
            }
        }

        public void OnRemove<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            foreach (var unit in GetUnitsForEntity(entity))
            {
                unit.DoRemove(entity);
            }
        }

        public void PostAdd<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            foreach (var unit in GetUnitsForEntity(entity))
            {
                unit.DoPostAdd(entity);
            }
        }

        public void PostRemove<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            foreach (var unit in GetUnitsForEntity(entity))
            {
                unit.DoPostRemove(entity);
            }
        }
    }
}
