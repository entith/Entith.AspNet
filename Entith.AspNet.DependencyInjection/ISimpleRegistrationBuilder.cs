using System;

namespace Entith.AspNet.DependencyInjection
{
    public interface ISimpleRegistrationBuilder
    {
        void RegisterTypeAs<TType, TAs>();
        void RegisterTypeAsSelf<TType>();
    }
}

