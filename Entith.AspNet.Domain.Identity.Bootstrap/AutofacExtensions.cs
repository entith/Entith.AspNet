using Autofac;
using Entith.AspNet.Domain;
using Entith.AspNet.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Autofac
{
    public static class AutofacExtensions
    {
        public static void BootstrapDomainIdentity<TSignInManager, TUserManager, TUser, TUserKey, TRoleManager, TRole, TRoleKey>
            (this ContainerBuilder builder, string appName)
            where TUserManager : UserManager<TUser, TUserKey>
            where TUserKey : IEquatable<TUserKey>, IConvertible
            where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
            where TRoleManager : RoleManager<TRole, TRoleKey>
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
            where TSignInManager : SignInManager<TUser, TUserKey>
        {
            builder.Register(c => new DomainUserStore<TUser, TRole, TUserKey, TRoleKey>(
                c.Resolve<IDomainService<TUser, TUserKey>>(),
                c.Resolve<IDomainService<TRole, TRoleKey>>()
                )).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => new DomainRoleStore<TRole, TUser, TRoleKey, TUserKey>(
                c.Resolve<IDomainService<TRole, TRoleKey>>()
                )).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<TUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<TRoleManager>().AsSelf().InstancePerRequest();

            builder.RegisterType<TSignInManager>().AsSelf().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.Register(c => new IdentityFactoryOptions<TUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider(appName)
            });
        }
    }
}
