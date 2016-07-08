using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Entith.AspNet.Domain
{
    public abstract class DomainService<TEntity, TKey, TRepository> : DomainService<TEntity, TKey>, IDomainService<TEntity, TKey, TRepository>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TRepository : IRepository<TEntity, TKey>
    {
        protected TRepository _customRepository;

        public DomainService(IUnitOfWork uow)
            : base(uow)
        {
            _customRepository = uow.GetRepository<TEntity, TRepository>();
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
        protected IRepository<TEntity, TKey> _repository;

        /// <summary>
        /// The unit of work this service is bound to.
        /// </summary>
        protected IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entith.AspNet.Domain.DomainService`2"/> class.
        /// </summary>
        /// <param name="uow">The unit of work instance to bind to.</param>
        public DomainService(IUnitOfWork uow)
        {
            _repository = uow.GetRepository<TEntity, IRepository<TEntity, TKey>>();
            uow.RegisterService(this);
            _uow = uow;
        }

        #endregion

        #region fetch

        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate).ToList();
        }

        //public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToList();
        //}

        public TEntity Get(TKey id)
        {
            return _repository.Get(id);
        }

        //public TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Get(id, properties);
        //}

        public ICollection<TEntity> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        //public ICollection<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToList();
        //}

        #endregion

        #region paged fetch

        public IPaged<TEntity> GetAllPaged()
        {
            return _repository.GetAll().ToPaged();
        }

        //public IPaged<TEntity> GetAllPaged(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToPaged();
        //}

        public IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate).ToPaged();
        }

        //public IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToPaged();
        //}

        #endregion

        #region add/remove

        public void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
        }

        #endregion

        #region UOW + domain logic

        public SaveChangesResults SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public virtual ICollection<SaveChangesResult> OnSaveChanges()
        {
            return null;
        }

        public virtual ICollection<SaveChangesResult> PostSaveChanges()
        {
            return null;
        }

        #endregion
    }
}

