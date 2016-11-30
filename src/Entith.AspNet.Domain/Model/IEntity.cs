using System;

namespace Entith.AspNet.Domain
{
    public interface IEntity<TKey> : IEntity
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntity
    {
    }
}

