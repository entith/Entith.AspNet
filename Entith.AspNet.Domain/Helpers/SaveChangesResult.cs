using System;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// Base class for all SaveChanges results.
	/// </summary>
    public abstract class SaveChangesResult
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.SaveChangesResult"/> class.
		/// </summary>
		/// <param name="type">Result type.</param>
		/// <param name="isOnPostSaveChanges">If set to <c>true</c> if error occured
		/// during OnPostSaveChanges.</param>
		public SaveChangesResult(SaveChangesResultType type, bool isOnPostSaveChanges = false)
		{
			ResultType = type;
		}

		/// <summary>
		/// The result type.
		/// </summary>
		public SaveChangesResultType ResultType { get; protected set; }

		/// <summary>
		/// The result message.
		/// </summary>
		public abstract string Message { get; }
    }

	/// <summary>
	/// An implementation of SaveChangesResult that uses a string for its message.
	/// </summary>
	public class StringSaveChangesResult : SaveChangesResult
	{
		private string _message;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.StringSaveChangesResult"/> class.
		/// </summary>
		/// <param name="message">The result message.</param>
		public StringSaveChangesResult(string message, SaveChangesResultType type, bool isOnPostSaveChanges = false)
			: base(type, isOnPostSaveChanges)
		{
			_message = message;
		}

		public override string Message { get { return _message; } }
	}

	/// <summary>
	/// An implementation of SaveChangesResult that uses an exception to generate its message.
	/// </summary>
	public class ExceptionSaveChangesResult : SaveChangesResult
	{
		/// <summary>
		/// The exception this result is for.
		/// </summary>
		private Exception e;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.ExceptionSaveChangesResult"/> class.
		/// </summary>
		/// <param name="e">The exception this result is for.</param>
		public ExceptionSaveChangesResult(Exception e, SaveChangesResultType type, bool isOnPostSaveChanges = false)
			: base(type, isOnPostSaveChanges)
		{
			this.e = e;
		}

		/// <summary>
		/// Gets a string representation of the exception.
		/// </summary>
		/// <returns>A string representation of the exception.</returns>
		/// <param name="e">The exception.</param>
		private string GetString(Exception e)
		{
			if(e.InnerException == null)
			{
				return e.Message;
			}
			else
			{
				return e.Message + "\n" + GetString(e.InnerException);
			}
		}

		public override string Message
		{
			get { return GetString(e); }
		}
	}

	public enum SaveChangesResultType
	{
		Error,
		Warning,
		Info
	}
}

