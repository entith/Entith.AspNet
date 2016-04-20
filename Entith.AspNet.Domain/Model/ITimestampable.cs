using System;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// Adds a DateCreated property.
	/// </summary>
	public interface ICreateTimestampable : IEntity
	{
		/// <summary>
		/// A timestamp of when the entity was first created.
		/// </summary>
		DateTime DateCreated { get; set; }
	}

	/// <summary>
	/// Adds a DateModified property.
	/// </summary>
	public interface IModifyTimestampable : IEntity
	{
		/// <summary>
		/// A timestamp of when the entity was last modified.
		/// </summary>
		DateTime DateModified { get; set; }
	}

	/// <summary>
	/// Adds both a created timestamp and a modified timestamp.
	/// </summary>
	public interface ICreateModifyTimestampable : ICreateTimestampable, IModifyTimestampable, IEntity
	{
	}
}

