using System;

namespace Corniel.Sudoku
{
	/// <summary>A solver for Sudoku puzzles.</summary>
	public class SudokuSolver
	{
		/// <summary>Initializes a new solver for a given puzzle.</summary>
		public SudokuSolver(SudokuPuzzle puzzle)
		{
			if (puzzle == null) { throw new ArgumentNullException(); }
			Puzzle = puzzle;
		}

		/// <summary>Gets the puzzle (structure) used by this solver.</summary>
		public SudokuPuzzle Puzzle { get; private set; }

		/// <summary>Solves a Sudoku puzzle given the Sudoku state.</summary>
		public ISudokuState Solve(ISudokuState state)
		{
			// As states are not immutable, create a copy.
			var worker = state.Copy();

			var result = ReduceResult.Reduced;

			while (result == ReduceResult.Reduced)
			{
				result = ReduceSingles(worker);
				result |= ReduceHiddenSingles(worker);
				result |= ReduceNakedPairs(worker);
				result |= ReduceLockedCandidates(worker);
			}
			if (result.HasFlag(ReduceResult.Inconsistend))
			{
				throw new InvalidPuzzleException();
			}
			return worker;
		}

		/// <summary>Reduces singles.</summary>
		/// <remarks>
		/// Any cells which have only one candidate can safely be assigned that value.
		/// 
		/// It is very important whenever a value is assigned to a cell, that this
		/// value is also excluded as a candidate from all other blank cells sharing
		/// the same row, column and sub square.
		/// </remarks>
		private ReduceResult ReduceSingles(ISudokuState state)
		{
			var result = ReduceResult.Reduced;

			while (result == ReduceResult.Reduced)
			{
				result = ReduceResult.None;

				for (var index = 0; index <= Puzzle.MaximumIndex; index++)
				{
					if (state.IsKnown(index))
					{
						foreach (var group in Puzzle.Lookup[index])
						{
							foreach (var square in group)
							{
								if (square != index)
								{
									result |= state.Reduce(square, index);
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>Reduces hidden singles.</summary>
		/// <remarks>
		/// Very frequently, there is only one candidate for a given row, column or
		/// sub square, but it is hidden among other candidates.
		/// </remarks>
		private ReduceResult ReduceHiddenSingles(ISudokuState state)
		{
			var result = ReduceResult.None;

			foreach (var region in Puzzle.Regions)
			{
				result |= state.ReduceHiddenSingles(region);
			}
			return result;
		}

		/// <summary>Reduces options that should be in the intersection.</summary>
		private ReduceResult ReduceLockedCandidates(ISudokuState state)
		{
			var result = ReduceResult.None;

			foreach (var region in Puzzle.Regions)
			{
				foreach (var other in Puzzle.Regions)
				{
					if (region.HasIntersectionOf2OrMoreSquares(other))
					{
						result |= state.ReduceIntersection(region, other);
					}
				}
			}
			return result;
		}

		/// <summary>Reduces naked pairs.</summary>
		/// <remarks>
		/// If two cells in a group contain an identical pair of candidates and only
		/// those two candidates, then no other cells in that group could be those
		/// values.
		/// 
		/// These 2 candidates can be excluded from other cells in the group.
		/// </remarks>
		private	ReduceResult ReduceNakedPairs(ISudokuState state)
		{
			var result = ReduceResult.None;

			foreach (var region in Puzzle.Regions)
			{
				var pair0 = -1;
				var pair1 = -1;
				foreach (var index in region)
				{
					if(state.Count(index) == 2)
					{
						if (pair0 == -1)
						{
							pair0 = index;
						}
						else if (pair1 == -1)
						{
							if (state.Equals(pair0, index))
							{
								pair1 = index;
							}
						}
						// more than 2 pairs with the same restriction.
						else
						{
							return ReduceResult.Inconsistend;
						}
					}
				}
				// We found a naked pair.
				if (pair0 != -1 && pair1 != -1)
				{
					foreach (var index in region)
					{
						if (index != pair0 && index != pair1)
						{
							result |= state.Reduce(index, pair0);
						}
					}
				}

			}
			return result;
		}
	}
}
