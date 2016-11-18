using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    public interface IDomainService<TEntity, TKey, TRepository> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TRepository : IRepository<TEntity, TKey>
    { }

    /// <summary>
    /// Service layer interface. Once instance per entity type.
    /// </summary>
    public interface IDomainService<TEntity, TKey> : IDomainService<TEntity>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets an entity by its Id.
        /// </summary>
        /// <param name="id">Entity Id.</param>
        TEntity Get(TKey id);

        /// <summary>
        /// Gets an entity by its Id.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <param name="id">Entity Id.</param>
        /// <param name="properties">The properties to eagerly load.</param>
        //TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties);
    }

    /// <summary>
    /// Service layer interface. Once instance per entity type.
    /// </summary>
    public interface IDomainService<TEntity> : IDomainService
        where TEntity : class, IEntity
    {
        #region fetch

        /// <summary>
        /// Returns all entities of this type.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Returns all entities of this type.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        /// <param name="properties">The properties to eagerly load.</param>
        //ICollection<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        /// <param name="properties">Properties.</param>
        //ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);

        #endregion

        #region paged fetch

        /// <summary>
        /// Returns all entities of this type wrapped in an IPaged container.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        IPaged<TEntity> GetAllPaged();

        /// <summary>
        /// Returns all entities of this type wrapped in an IPaged container.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        /// <param name="properties">The properties to eagerly load.</param>
        //IPaged<TEntity> GetAllPaged(params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate wrapped in an IPaged container.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate wrapped in an IPaged container.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        /// <param name="properties">Properties.</param>
        //IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);

        #endregion

        #region add/remove

        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a collection of entities to the repository.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove the specified entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes a collection of entities from the repository.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        #endregion
    }

    /// <summary>
    /// Service layer interface. Once instance per entity type.
    /// </summary>
    public interface IDomainService
    {
        void Init(IUnitOfWork uow);

        /// <summary>
        /// Triggers the Unit Of Work to save all pending changes.
        /// This can be called from any IDomainService instance bound
        /// to the same IUnitOfWork instance.
        /// </summary>
        /// <returns>Results of the attempt to save the changes.</returns>
        void SaveChanges();

        /// <summary>
        /// A callback the IUnitOfWork instance calls before saving changes to the database.
        /// Used for domain/business logic.
        /// </summary>
        void OnSaveChanges();

        /// <summary>
        /// A callback the IUnitOfWork instance calls after saving changes to the database.
        /// Used for domain/business logic.
        /// </summary>
        void PostSaveChanges();
    }
}

