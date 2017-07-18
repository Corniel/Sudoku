﻿using System;

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
		/// <summary>Action results in a solved puzzle.</summary>
		Solved = 2,
		/// <summary>An inconstancy occurred while reducing.</summary>
		Inconsistent = 4,

        /// <summary>When solved or inconsistent, the outcome is final.</summary>
        Final = Solved| Inconsistent,
	}
}
