using Entith.AspNet.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    public static class OwinExtensions
    {
        public static void ConfigureDomainIdentity<TUserManager, TUser, TUserKey, TRole, TRoleKey>(this IAppBuilder app, string loginPath)
            where TUserManager : UserManager<TUser, TUserKey>
            where TUserKey : IEquatable<TUserKey>, IConvertible
            where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(loginPath),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<TUserManager, TUser, TUserKey>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (identity) => identity.GetUserId<TUserKey>())
                }
            });
        }
    }
}
