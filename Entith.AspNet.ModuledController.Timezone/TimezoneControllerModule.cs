using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entith.AspNet.ModuledController
{
    public class TimezoneControllerModule : ControllerModule
    {
		public override void OnActionExecuting(ActionExecutingContext filterContext, ControllerContext context)
        {

            // http://afana.me/post/aspnet-mvc-internationalization-date-time.aspx
            // Parse TimeZoneOffset.
            filterContext.HttpContext.Items["TimeZoneOffset"] = TimeSpan.FromMinutes(0); // Default offset (Utc) if cookie is missing.
            var timeZoneCookie = filterContext.HttpContext.Request.Cookies["_timeZoneOffset"];
            if (timeZoneCookie != null)
            {
                double offsetMinutes = 0;
                if (double.TryParse(timeZoneCookie.Value, out offsetMinutes))
                {
                    // Store in ViewBag. You can use Session, TempData, or anything else.
                    filterContext.HttpContext.Items["TimeZoneOffset"] = TimeSpan.FromMinutes(offsetMinutes);
                }
            }
        }
    }
}
