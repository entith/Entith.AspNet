using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.DependencyInjection.Autofac4
{
    public class SimpleAutofacRegistrationBuilder : ISimpleRegistrationBuilder
    {
        private ContainerBuilder _builder;

        public SimpleAutofacRegistrationBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public void RegisterTypeAs<TType, TAs>()
        {
            _builder.RegisterType<TType>().As<TAs>().InstancePerLifetimeScope();
        }

        public void RegisterTypeAsSelf<TType>()
        {
            _builder.RegisterType<TType>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
