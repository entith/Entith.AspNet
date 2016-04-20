using System;
using Entith.AspNet.ModuledController;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
		public static void RegisterMessageControllerModule(this ISimpleRegistrationBuilder builder)
		{
			builder.RegisterControllerModule<MessageControllerModule>();
			builder.RegisterTypeAsSelf<MessageManager>();
		}
    }
}

