using System;
using Entith.AspNet.ModuledController;
using Entith.AspNet.Domain;
using Entith.AspNet.Domain.Identity;

namespace Entith.AspNet.DependencyInjection
{
    public static class DiExtensions
    {
		public static void RegisterIdentityTimestampControllerModule<TUser, TUserKey>(this ISimpleRegistrationBuilder builder)
            where TUserKey : IEquatable<TUserKey>, IConvertible
            where TUser : DomainUser<TUserKey>
        {
            builder.RegisterControllerModule<IdentityTimestampControllerModule<TUser, TUserKey>>();
		}
    }
}

