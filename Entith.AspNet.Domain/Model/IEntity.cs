using System;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// A basic interface that all entities need to implement.
	/// Specifies an Id property to be used as a primary key.
	/// </summary>
	public interface IEntity<TKey> : IEntity
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		/// The entity's primary key.
		/// </summary>
		TKey Id { get; set; }
	}

	/// <summary>
	/// An empty interface that all entities need to implement in order
	/// to be identified as entities.
	/// </summary>
	public interface IEntity
	{
	}
}

