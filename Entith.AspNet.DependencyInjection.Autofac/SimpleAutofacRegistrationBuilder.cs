using System;
using Autofac;

namespace Entith.AspNet.DependencyInjection
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

