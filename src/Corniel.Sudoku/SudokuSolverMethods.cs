using System;

namespace Corniel.Sudoku
{
	[Flags]
	public enum SudokuSolverMethods
	{
		None = 0,
		Singles = 0x1,
		HiddenSingles = 0x2,
		LockedCandidates = 0x4,
		NakedPairs = 0x8,

		All = Singles | HiddenSingles | LockedCandidates | NakedPairs,
	}
}
