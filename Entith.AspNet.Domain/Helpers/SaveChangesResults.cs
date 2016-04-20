using System;
using System.Collections.Generic;
using System.Linq;

namespace Entith.AspNet.Domain
{
	/// <summary>
	/// The results of the Unit of Work's SaveChanges() attempt.
	/// </summary>
    public class SaveChangesResults
    {
		/// <summary>
		/// A collection of results.
		/// </summary>
		private ICollection<SaveChangesResult> _results;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.SaveChangesResults"/> class.
		/// </summary>
		public SaveChangesResults()
		{
			_results = new List<SaveChangesResult>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.SaveChangesResults"/> class.
		/// </summary>
		/// <param name="results">Results to add.</param>
		public SaveChangesResults(params SaveChangesResult[] results)
		{
			_results = new List<SaveChangesResult>();

			Add(results);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entith.AspNet.Domain.SaveChangesResults"/> class.
		/// </summary>
		/// <param name="results">Results to add.</param>
		public SaveChangesResults(params SaveChangesResults[] results)
		{
			_results = new List<SaveChangesResult>();

			Add(results);
		}

		/// <summary>
		/// An enumerable of all the results.
		/// </summary>
		public IEnumerable<SaveChangesResult> Results
		{
			get { return _results; }
		}

		/// <summary>
		/// Add results.
		/// </summary>
		/// <param name="results">Results to add.</param>
		public void Add(params SaveChangesResult[] results)
		{
			foreach (var result in results)
				_results.Add(result);
		}

		/// <summary>
		/// Add results.
		/// </summary>
		/// <param name="results">Results to add.</param>
		public void Add(params SaveChangesResults[] results)
		{
			foreach(var set in results)
				foreach (var result in set.Results)
					_results.Add(result);
		}

		/// <summary>
		/// Checks if there are any errors.
		/// </summary>
		/// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
		public bool HasErrors
		{
			get { return _results.Any(r => r.ResultType == SaveChangesResultType.Error); }
		}

		/// <summary>
		/// Checks if there are any warnings.
		/// </summary>
		/// <value><c>true</c> if this instance has warnings; otherwise, <c>false</c>.</value>
		public bool HasWarnings
		{
			get { return _results.Any(r => r.ResultType == SaveChangesResultType.Warning); }
		}

		/// <summary>
		/// Checks if there are any infos.
		/// </summary>
		/// <value><c>true</c> if this instance has info; otherwise, <c>false</c>.</value>
		public bool HasInfo
		{
			get { return _results.Any(r => r.ResultType == SaveChangesResultType.Info); }
		}
	}
}

