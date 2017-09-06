using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public abstract class LogicUnit<TEntity> : LogicUnit, ILogicUnit<TEntity>
        where TEntity : class, IEntity
    {



        public virtual void OnAdd(TEntity entity) { }

        public virtual void OnRemove(TEntity entity) { }

        public virtual void PostAdd(TEntity entity) { }

        public virtual void PostRemove(TEntity entity) { }

        public override sealed Type GetEntityType()
        {
            return typeof(TEntity);
        }

        protected TEntity ConvertEntity(IEntity entity)
        {
            if (!typeof(TEntity).IsAssignableFrom(entity.GetType()))
            {
                throw new ArgumentException("Logic Unit for type " + typeof(TEntity) + " cannot handle entity of type " + entity.GetType());
            }

            return (TEntity)entity;
        }

        public override sealed void DoAdd(IEntity entity)
        {
            OnAdd(ConvertEntity(entity));
        }

        public override sealed void DoRemove(IEntity entity)
        {
            OnRemove(ConvertEntity(entity));
        }

        public override sealed void DoPostAdd(IEntity entity)
        {
            PostAdd(ConvertEntity(entity));
        }

        public override sealed void DoPostRemove(IEntity entity)
        {
            PostRemove(ConvertEntity(entity));
        }

    }

    public abstract class LogicUnit : ILogicUnit
    {
        protected IChangeTracker ChangeTracker { get; set; }
        protected IRepositoryManager RepoManager { get; set; }

        public void Init(IRepositoryManager repoManager, IChangeTracker changeTracker)
        {
            RepoManager = repoManager;
            ChangeTracker = changeTracker;

            Init();
        }

        protected virtual void Init() { }

        public virtual void OnSaveChanges() { }

        public virtual bool PostSaveChanges() { return false; }

        public abstract Type GetEntityType();
        public abstract void DoAdd(IEntity entity);
        public abstract void DoRemove(IEntity entity);
        public abstract void DoPostAdd(IEntity entity);
        public abstract void DoPostRemove(IEntity entity);
    }
}
