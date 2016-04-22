using System;
using Entith.AspNet.ModuledController;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
        public static void RegisterControllerModule<TModule>(this ISimpleRegistrationBuilder builder)
            where TModule : class, IControllerModule
        {
            builder.RegisterTypeAs<TModule, IControllerModule>();
        }

        public static void BootstrapModuledControllers(this ISimpleRegistrationBuilder builder)
        {
            builder.RegisterTypeAsSelf<ControllerModuleManager>();
        }
    }
}

