using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.ModuledController
{
    public static class ControllerExtensions
    {
        public static TimeSpan GetTimeZoneOffset(this ModuledController controller)
        {
            return (TimeSpan) controller.HttpContext.Items["TimeZoneOffset"];
        }
    }
}
