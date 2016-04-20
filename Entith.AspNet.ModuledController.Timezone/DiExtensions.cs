using System;
using Entith.AspNet.ModuledController;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
		public static void RegisterTimezoneControllerModule(this ISimpleRegistrationBuilder builder)
		{
			builder.RegisterControllerModule<TimezoneControllerModule>();
		}
    }
}

