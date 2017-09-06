using System;
using System.Data.Entity;
using System.Collections.Generic;
using Entith.AspNet.Domain;
using System.Linq;

namespace Entith.AspNet.Domain.EntityFramework
{
    public class EfUnitOfWork<TDbContext> : UnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;

        public EfUnitOfWork(TDbContext context)
        {
            _context = context;
        }

        protected override void PerformSaveChanges()
        {
            _context.SaveChanges();
        }

        public override void Dispose()
        {
            _context.Dispose();
        }

        public override IEnumerable<IEntity> GetTracked()
        {
            return GetTracked<IEntity>();
        }

        public override IEnumerable<IEntity> GetAdded()
        {
            return GetAdded<IEntity>();
        }

        public override IEnumerable<IEntity> GetModified()
        {
            return GetModified<IEntity>();
        }

        public override IEnumerable<IEntity> GetRemoved()
        {
            return GetRemoved<IEntity>();
        }

        public override IEnumerable<TEntity> GetTracked<TEntity>()
        {
            return _context.ChangeTracker.Entries<TEntity>()
                .Select(x => x.Entity).ToList();
        }

        public override IEnumerable<TEntity> GetAdded<TEntity>()
        {
            return _context.ChangeTracker.Entries<TEntity>().
                Where(x => x.State == EntityState.Added)
                    .Select(x => x.Entity).ToList();
        }

        public override IEnumerable<TEntity> GetModified<TEntity>()
        {
            return _context.ChangeTracker.Entries<TEntity>().
                Where(x => x.State == EntityState.Modified)
                    .Select(x => x.Entity).ToList();
        }

        public override IEnumerable<TEntity> GetRemoved<TEntity>()
        {
            return _context.ChangeTracker.Entries<TEntity>().
                Where(x => x.State == EntityState.Deleted)
                    .Select(x => x.Entity).ToList();
        }

        public override void ClearChanges(IEntity entity)
        {
            _context.Entry<IEntity>(entity).State = EntityState.Unchanged;
        }

        public override void ClearAllChanges()
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}

