using Entith.AspNet.Domain;
using Entith.AspNet.Domain.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Entith.AspNet.ModuledController
{public class IdentityTimestampControllerModule<TUser, TUserKey> : ControllerModule
        where TUserKey : IEquatable<TUserKey>, IConvertible
        where TUser : DomainUser<TUserKey>
    {
        private IDomainService<TUser, TUserKey> _userService;
        private int _actionCounter;
        
        public IdentityTimestampControllerModule(IDomainService<TUser, TUserKey> userService)
        {
            _userService = userService;
            _actionCounter = 0;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext, ControllerContext context)
        {
            if(_actionCounter++ == 0 && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                TUser user = _userService.Get(HttpContext.Current.User.Identity.GetUserId<TUserKey>());
                user.LastActive = DateTime.UtcNow;
                _userService.SaveChanges();
            }
        }
    }
}
