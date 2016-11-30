using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    internal class RepositoryInterceptor<TEntity> : IInterceptor
        where TEntity : class, IEntity
    {
        private IDomainManager _domain;

        public RepositoryInterceptor(IDomainManager domain)
        {
            _domain = domain;
        }

        public void Intercept(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;

            bool hasAddAttribute = invocation.MethodInvocationTarget.CustomAttributes.Any(a => a.AttributeType == typeof(AddMethodAttribute));
            bool hasRemoveAttribute = invocation.MethodInvocationTarget.CustomAttributes.Any(a => a.AttributeType == typeof(RemoveMethodAttribute));

            bool isAdd = (methodName == "Add" || hasAddAttribute);
            bool isRemove = (methodName == "Remove" || hasRemoveAttribute);

            TEntity entity = invocation.Arguments.FirstOrDefault() as TEntity;

            if(isAdd)
                _domain.OnAdd(entity);
            if(isRemove)
                _domain.OnRemove(entity);

            invocation.Proceed();

            if (isAdd)
                _domain.PostAdd(entity);
            if (isRemove)
                _domain.PostRemove(entity);
        }
    }
}
