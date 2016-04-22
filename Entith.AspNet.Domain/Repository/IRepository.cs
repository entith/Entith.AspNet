using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// The repository layer that talks directly to the ORM/storage layer.
    /// One instance per entity type.
    /// </summary>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
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
        TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties);
    }

    /// <summary>
    /// The repository layer that talks directly to the ORM/storage layer.
    /// One instance per entity type.
    /// </summary>
    public interface IRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Returns all entities of this type.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns all entities of this type.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <returns>All entities of this type.</returns>
        /// <param name="properties">The properties to eagerly load.</param>
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds all entities matching the criteria set in the predicate.
        /// Eagerly loads the specified properties.
        /// </summary>
        /// <param name="predicate">The search criteria.</param>
        /// <param name="properties">Properties.</param>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);

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
    }

    /// <summary>
    /// The repository layer that talks directly to the ORM/storage layer.
    /// One instance per entity type.
    /// </summary>
    public interface IRepository
    {
    }
}

