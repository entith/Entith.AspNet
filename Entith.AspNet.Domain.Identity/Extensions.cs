using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Identity
{
    public static class Extensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync<TUser, TUserKey, TRole, TRoleKey>(this DomainUser<TUser, TUserKey, TRole, TRoleKey> user, UserManager<TUser, TUserKey> manager)
            where TUserKey : IEquatable<TUserKey>
            where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync((TUser)user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
