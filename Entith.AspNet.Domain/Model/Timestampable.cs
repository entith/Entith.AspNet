using System;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// Abstract implementation of ICreateTimestampable. Used as a shortcut
	/// to avoid having to manually add a DateCreated propery.
	/// </summary>
	public abstract class CreateTimestampable : ICreateTimestampable
	{
		public DateTime DateCreated { get; set; }
	}

	/// <summary>
	/// Abstract implementation of IModifyTimestampable. Used as a shortcut
	/// to avoid having to manually add a DateModified propery.
	/// </summary>
	public abstract class ModifyTimestampable : IModifyTimestampable
	{
		public DateTime DateModified { get; set; }
	}

	/// <summary>
	/// Abstract implementation of ICreateModifyTimestampable. Used as a shortcut
	/// to avoid having to manually add the DateCreated and DateModified properies.
	/// </summary>
	public abstract class CreateModifyTimestampable : ICreateModifyTimestampable
	{
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}

