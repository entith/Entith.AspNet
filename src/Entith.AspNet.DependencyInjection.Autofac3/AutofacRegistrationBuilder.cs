using Autofac;
using Entith.AspNet.Domain.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.DependencyInjection.Autofac3
{
    public class AutofacRegistrationBuilder : Entith.AspNet.Domain.DependencyInjection.IRegistrationBuilder
    {
        private ContainerBuilder _builder;

        public AutofacRegistrationBuilder(ContainerBuilder builder)
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

    public static class Extensions
    {
        public static IDomainBuilder GetDomainBuilder(this ContainerBuilder builder)
        {
            return new DomainBuilder(new AutofacRegistrationBuilder(builder));
        }
    }
}
