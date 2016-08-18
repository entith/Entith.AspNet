using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    internal class RepositoryInterceptor<TEntity, TKey> : IInterceptor
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private DomainService<TEntity, TKey> _domain;

        public RepositoryInterceptor(DomainService<TEntity, TKey> domain)
        {
            _domain = domain;
        }

        public void Intercept(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;
            TEntity entity = invocation.Arguments.FirstOrDefault() as TEntity;
            switch (methodName)
            {
                case "Add":
                    _domain.OnAdd(entity);
                    break;
                case "Remove":
                    _domain.OnRemove(entity);
                    break;
            }

            invocation.Proceed();

            switch (methodName)
            {
                case "Add":
                    _domain.PostAdd(entity);
                    break;
                case "Remove":
                    _domain.PostRemove(entity);
                    break;
            }
        }
    }
}
