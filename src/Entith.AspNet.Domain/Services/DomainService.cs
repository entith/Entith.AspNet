using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Castle.DynamicProxy;

namespace Entith.AspNet.Domain
{
    public abstract class DomainService<TEntity, TKey, TRepository> : DomainService<TEntity, TKey>, IDomainService<TEntity, TKey, TRepository>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TRepository : IRepository<TEntity, TKey>
    {
        protected TRepository CustomRepository { get; private set; }

        public override void Init(IUnitOfWork uow)
        {
            base.Init(uow);
            CustomRepository = uow.GetRepository<TEntity, TRepository>();
        }
    }

    /// <summary>
    /// Basic implementation of the IDomainService interface. Can be used
    /// as is or extended if more advanced methods are needed or you need
    /// to add domain logic.
    /// </summary>
    public class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region ctor + fields

        /// <summary>
        /// The repository instance this service wraps around.
        /// </summary>
        protected IRepository<TEntity, TKey> Repository { get; private set; }

        /// <summary>
        /// The unit of work this service is bound to.
        /// </summary>
        protected IUnitOfWork Uow { get; private set; }

        protected ICollection<ILogicUnit<TEntity, TKey>> LogicUnits { get; private set; }

        private static IEnumerable<Type> _logicUnitTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entith.AspNet.Domain.DomainService`2"/> class.
        /// </summary>
        /// <param name="uow">The unit of work instance to bind to.</param>
        //public DomainService()
        //{
        //}
        public virtual void Init(IUnitOfWork uow)
        {
            //uow.RegisterService(this);
            Uow = uow;

            LogicUnits = new HashSet<ILogicUnit<TEntity, TKey>>();

            // Wrap the repository in a proxy to intercept add and remove calls for business logic.
            var repo = uow.GetRepository<TEntity, IRepository<TEntity, TKey>>();
            var interceptor = new RepositoryInterceptor<TEntity, TKey>(this);
            var dp = new ProxyGenerator();
            Repository = dp.CreateInterfaceProxyWithTarget(repo, interceptor);


            // Add all applicable logic units found in application.
            if(_logicUnitTypes == null)
                _logicUnitTypes = AssemblyHelper.GetAssemblies()
                    .Where(t => typeof(ILogicUnit<TEntity, TKey>).IsAssignableFrom(t));
            
            foreach(Type t in _logicUnitTypes)
            {
                try
                {
                    ILogicUnit<TEntity, TKey> logicUnit = (ILogicUnit<TEntity, TKey>) Activator.CreateInstance(t, uow, this, Repository);
                    LogicUnits.Add(logicUnit);
                }
                catch
                {
                    Console.WriteLine("Could not instantiate logic unit of type " + t.FullName);
                }
            }
        }

        #endregion

        #region fetch

        public virtual ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Find(predicate).ToList();
        }

        //public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToList();
        //}

        public virtual TEntity Get(TKey id)
        {
            return Repository.Get(id);
        }

        //public TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Get(id, properties);
        //}

        public virtual ICollection<TEntity> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        //public ICollection<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToList();
        //}

        #endregion

        #region paged fetch

        public virtual IPaged<TEntity> GetAllPaged()
        {
            return Repository.GetAll().ToPaged();
        }

        //public IPaged<TEntity> GetAllPaged(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToPaged();
        //}

        public virtual IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Find(predicate).ToPaged();
        }

        //public IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToPaged();
        //}

        #endregion

        #region add/remove

        public virtual void Add(TEntity entity)
        {
            Repository.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Repository.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                Remove(entity);
        }

        #endregion

        #region UOW + domain logic

        public void SaveChanges()
        {
            Uow.SaveChanges();
        }

        public void OnSaveChanges()
        {
            foreach (ILogicUnit unit in LogicUnits)
                unit.OnSaveChanges();
        }

        public void PostSaveChanges()
        {
            foreach (ILogicUnit unit in LogicUnits)
                unit.PostSaveChanges();
        }

        public void OnAdd(TEntity entity)
        {
            foreach (ILogicUnit<TEntity, TKey> unit in LogicUnits)
                unit.OnAdd(entity);
        }

        public void OnRemove(TEntity entity)
        {
            foreach (ILogicUnit<TEntity, TKey> unit in LogicUnits)
                unit.OnRemove(entity);
        }

        public void PostAdd(TEntity entity)
        {
            foreach (ILogicUnit<TEntity, TKey> unit in LogicUnits)
                unit.PostAdd(entity);
        }

        public void PostRemove(TEntity entity)
        {
            foreach (ILogicUnit<TEntity, TKey> unit in LogicUnits)
                unit.PostRemove(entity);
        }

        #endregion
    }
}

