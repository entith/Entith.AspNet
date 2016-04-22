using System;
using Entith.AspNet.ModuledController;
using Entith.AspNet.Domain;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
        public static void RegisterDomainTimestampControllerModule(this ISimpleRegistrationBuilder builder)
        {
            builder.RegisterTypeAsSelf<TimestampableService>();
            builder.RegisterControllerModule<DomainTimestampControllerModule>();
        }
    }
}

