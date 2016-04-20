using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Identity
{
    public abstract class DomainRole<TRole, TRoleKey, TUser, TUserKey> : IEntity<TRoleKey>, IRole<TRoleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
    {
        public DomainRole()
        {
            Users = new List<TUser>();
        }

        public TRoleKey Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TUser> Users { get; set; }
    }
}
