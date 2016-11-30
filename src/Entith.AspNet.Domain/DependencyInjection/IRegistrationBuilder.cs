using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.DependencyInjection
{
    public interface IRegistrationBuilder
    {
        void RegisterTypeAs<TType, TAs>();
        void RegisterTypeAsSelf<TType>();
    }
}
