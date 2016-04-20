using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Identity
{
    public class DomainUserStore<TUser, TRole, TUserKey, TRoleKey> :
        IUserStore<TUser, TUserKey>,
        IUserEmailStore<TUser, TUserKey>,
        IUserPasswordStore<TUser, TUserKey>,
        IUserLockoutStore<TUser, TUserKey>,
        IUserSecurityStampStore<TUser, TUserKey>,
        IUserRoleStore<TUser, TUserKey>,
        IUserTwoFactorStore<TUser, TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : DomainRole<TRole, TRoleKey, TUser, TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TUser : DomainUser<TUser, TUserKey, TRole, TRoleKey>
    {
        IDomainService<TUser, TUserKey> _userService;
        IDomainService<TRole, TRoleKey> _roleService;

        public DomainUserStore(IDomainService<TUser, TUserKey> userService, IDomainService<TRole, TRoleKey> roleService)
        {
            _userService = userService;
            _roleService = roleService;
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
                return _userService.SaveChanges();
            else
                return null;
        }

        public virtual Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userService.Add(user);
            return Task.FromResult(SaveChanges());
        }

        public virtual Task DeleteAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userService.Remove(user);
            return Task.FromResult(SaveChanges());
        }

        public void Dispose()
        {
        }

        public virtual Task<TUser> FindByIdAsync(TUserKey userId)
        {
            return Task.FromResult(_userService.Find(u => u.Id.Equals(userId)).FirstOrDefault());
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(_userService.Find(u => u.UserName.Equals(userName)).FirstOrDefault());
        }

        public virtual Task UpdateAsync(TUser user)
        {
            return Task.FromResult(SaveChanges());
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.IsEmailConfirmed);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.IsEmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(_userService.Find(u => u.Email.Equals(email)).FirstOrDefault());
        }

        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult(user.LockoutEndDate);
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDate = lockoutEnd;
            return Task.FromResult(0);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.IsLockoutEnabled);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.IsLockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task AddToRoleAsync(TUser user, string roleName)
        {
            TRole role = _roleService.Find(r => r.Name == roleName).FirstOrDefault();
            if (role != null)
                user.Roles.Add(role);
            return Task.FromResult(SaveChanges());
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            TRole role = _roleService.Find(r => r.Name == roleName).FirstOrDefault();
            if (role != null)
                user.Roles.Remove(role);
            return Task.FromResult(SaveChanges());
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            IList<string> roleNames = new List<string>();
            foreach (TRole r in user.Roles)
                roleNames.Add(r.Name);
            return Task.FromResult(roleNames);
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(r => r.Name == roleName));
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }
    }
}
