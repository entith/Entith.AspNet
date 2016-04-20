using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Identity
{
    public abstract class DomainUser<TUser, TUserKey, TRole, TRoleKey> : DomainUser<TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
    {
        public DomainUser()
        {
            Roles = new List<TRole>();
        }

        public virtual ICollection<TRole> Roles { get; set; }
    }

    public abstract class DomainUser<TUserKey> : IEntity<TUserKey>, IUser<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset LockoutEndDate { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsLockoutEnabled { get; set; }

        public string SecurityStamp { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
