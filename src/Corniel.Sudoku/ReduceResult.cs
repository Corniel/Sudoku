using System;

namespace Corniel.Sudoku
{
	/// <summary>The possible results of a reduce.</summary>
	[Flags]
	public enum ReduceResult
	{
		/// <summary>Nothing changed, no reduction of the options.</summary>
		None = 0,
		/// <summary>Something changed, at least one reduction of the options.</summary>
		Reduced = 1,
		/// <summary>An inconstancy occurred while reducing.</summary>
		Inconsistend = 2,
	}
}
