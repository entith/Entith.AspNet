using System;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// Interface to facilitate the Unit Of Work design pattern.
    /// IDomainService instances will bind to it.
    /// It will need to have references to all the repositores.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persists all pending changes.
        /// </summary>
        /// <returns>The result of the save attempt.</returns>
        void SaveChanges();

        /// <summary>
        /// Returns an instance of a repository for the specified entity type.
        /// </summary>
        /// <returns>A repository instance for the specified type.</returns>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TRepository">The repository type.</typeparam>
        TRepository GetRepository<TEntity, TRepository>() 
            where TEntity : class, IEntity 
            where TRepository : IRepository<TEntity>;

        /// <summary>
        /// Binds the service to this UnitOfWork instance.
        /// </summary>
        /// <param name="service">The service to bind.</param>
        void RegisterService(IDomainService service);

        /// <summary>
        /// Gets all pending newly added entities.
        /// </summary>
        /// <returns>The added.</returns>
        IEnumerable<IEntity> GetAdded();

        /// <summary>
        /// Gets all pending modified entities.
        /// </summary>
        /// <returns>The modified.</returns>
        IEnumerable<IEntity> GetModified();

        /// <summary>
        /// Gets all pending removed entities.
        /// </summary>
        /// <returns>The removed entities.</returns>
        IEnumerable<IEntity> GetRemoved();

        /// <summary>
        /// Gets all pending newly added entities of the specified type.
        /// </summary>
        /// <returns>The added entities.</returns>
        /// <typeparam name="TEntity">The type of entities to return.</typeparam>
        IEnumerable<TEntity> GetAdded<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Gets all pending modified entities of the specified type.
        /// </summary>
        /// <returns>The modified entites.</returns>
        /// <typeparam name="TEntity">The type of entities to return.</typeparam>
        IEnumerable<TEntity> GetModified<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Gets all pending removed entities of the specified type.
        /// </summary>
        /// <returns>The removed entites.</returns>
        /// <typeparam name="TEntity">The type of entities to return.</typeparam>
        IEnumerable<TEntity> GetRemoved<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Clears all of the entities pending changes.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void ClearChanges(IEntity entity);

        /// <summary>
        /// Clears all pending changes.
        /// </summary>
        void ClearAllChanges();
    }
}

