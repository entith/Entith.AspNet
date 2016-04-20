using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Entith.AspNet.ModuledController
{
    public static class TimezoneHtmlHelperExtensions
    {
        public static TimeSpan GetTimeZoneOffset(this HtmlHelper html)
        {
            return (TimeSpan) html.ViewContext.HttpContext.Items["TimeZoneOffset"];
        }

        public static HtmlString TimeZoneCookieBlock(this HtmlHelper html, bool forceReload = false)
        {
            return new HtmlString(string.Format(
@"<script>
    function cookieExists(name) {
        var nameToFind = name + "" = "";
        var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++)
                {
                    if (cookies[i].trim().indexOf(nameToFind) === 0) return true;
                }
                return false;
            }
    if (!cookieExists(""_timeZoneOffset"")) {
        var now = new Date();
            var timeZoneOffset = -now.getTimezoneOffset();  // in minutes
            now.setTime(now.getTime() + 10 * 24 * 60 * 60 * 1000); // keep it for 10 days
        document.cookie = ""_timeZoneOffset="" + timeZoneOffset.toString()
                      + "";expires="" + now.toGMTString() + "";path=/;"" + document.cookie;

        {0}
    }
</script>",
                    forceReload ? "window.location.reload();" : "")
                );
        }
    }
}
