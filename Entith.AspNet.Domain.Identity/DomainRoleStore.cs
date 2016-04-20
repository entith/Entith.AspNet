using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Identity
{
    public class DomainRoleStore<TRole, TUser, TRoleKey, TUserKey> :
         IRoleStore<TRole, TRoleKey>,
         IQueryableRoleStore<TRole, TRoleKey>
         where TRoleKey : IEquatable<TRoleKey>
         where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
         where TUserKey : IEquatable<TUserKey>
         where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
    {

        IDomainService<TRole, TRoleKey> _service;

        public DomainRoleStore(IDomainService<TRole, TRoleKey> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets or sets a flag indicating if changes should be persisted after CreateAsync, UpdateAsync and DeleteAsync are called.
        /// </summary>
        /// <value>
        /// True if changes should be automatically persisted, otherwise false.
        /// </value>
        public bool AutoSaveChanges { get; set; } = true;

        private SaveChangesResults SaveChanges()
        {
            if (AutoSaveChanges)
                return _service.SaveChanges();
            else
                return null;
        }

        public IQueryable<TRole> Roles
        {
            get
            {
                return _service.GetAllPaged().AsQueryable();
            }
        }

        public virtual Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _service.Add(role);
            return Task.FromResult(SaveChanges());
        }

        public virtual Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _service.Remove(role);
            return Task.FromResult(SaveChanges());
        }

        public void Dispose()
        {

        }

        public virtual Task<TRole> FindByIdAsync(TRoleKey roleId)
        {
            return Task.FromResult(_service.Find(r => r.Id.Equals(roleId)).FirstOrDefault());
        }

        public virtual Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult(_service.Find(r => r.Name.Equals(roleName)).FirstOrDefault());
        }

        public virtual Task UpdateAsync(TRole role)
        {
            return Task.FromResult(SaveChanges());
        }
    }
}
