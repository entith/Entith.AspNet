using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    [AttributeUsage(validOn: AttributeTargets.Method)]
    public class AddMethodAttribute : Attribute
    {
    }

    [AttributeUsage(validOn: AttributeTargets.Method)]
    public class RemoveMethodAttribute : Attribute
    {
    }
}
