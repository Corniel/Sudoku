using System;

namespace Corniel.Sudoku
{
	/// <summary>A solver for Sudoku puzzles.</summary>
	public class SudokuSolver
	{
		/// <summary>Initializes a new solver for a given puzzle.</summary>
		public SudokuSolver(SudokuPuzzle puzzle) : this(puzzle, SudokuSolverMethods.All) { }

		/// <summary>Initializes a new solver for a given puzzle.</summary>
		public SudokuSolver(SudokuPuzzle puzzle, SudokuSolverMethods methods)
		{
			if (puzzle == null) { throw new ArgumentNullException(); }
			Puzzle = puzzle;
			Methods = methods;
		}

		/// <summary>Gets the puzzle (structure) used by this solver.</summary>
		public SudokuPuzzle Puzzle { get; private set; }
		/// <summary>Gets the methods that are used to try to solve the puzzle.</summary>
		public SudokuSolverMethods Methods { get; private set; }

		/// <summary>Solves a Sudoku puzzle given the Sudoku state.</summary>
		public SudokuState Solve(SudokuState state)
		{
			// As states are not immutable, create a copy.
			var worker = state.Copy();

			var result = ReduceResult.Reduced;

			while (result == ReduceResult.Reduced)
			{
				result = ReduceResult.None;
				result |= ReduceSingles(result, worker);
				result |= ReduceHiddenSingles(result, worker);
				result |= ReduceLockedCandidates(result, worker);
				result |= ReduceNakedPairs(result, worker);
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
		private ReduceResult ReduceSingles(ReduceResult result, SudokuState state)
		{
			if (SkipMethod(SudokuSolverMethods.Singles, result)) { return result; }

			result = ReduceResult.Reduced;
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
									result |= state.Exclude(square, index);
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>Returns false if the result is solved or the method is disabled.</summary>
		private bool SkipMethod(SudokuSolverMethods method, ReduceResult result)
		{
			return
				(Methods & method) == SudokuSolverMethods.None || 
				(result & ReduceResult.Solved) == ReduceResult.Solved;
		}

		/// <summary>Reduces hidden singles.</summary>
		/// <remarks>
		/// Very frequently, there is only one candidate for a given row, column or
		/// sub square, but it is hidden among other candidates.
		/// </remarks>
		private ReduceResult ReduceHiddenSingles(ReduceResult result, SudokuState state)
		{
			if (SkipMethod(SudokuSolverMethods.HiddenSingles, result)) { return result; }

			foreach (var region in Puzzle.Regions)
			{
				foreach (var singleValue in Puzzle.SingleValues)
				{
					var cnt = 0;
					var found = -1;
					foreach (var index in region)
					{
						var val = state[index];
						if ((val & singleValue) != SudokuPuzzle.Invalid)
						{
							unchecked { cnt++; }
							if (state.IsKnown(index))
							{
								found = -1;
								break;
							}
							else if (cnt == 1)
							{
								found = index;
							}
						}
					}
					if (cnt == 1 && found != -1)
					{
						result |= state.AndMask(found, singleValue);
					}
					else if (cnt == 0)
					{
						return ReduceResult.Inconsistend;
					}
				}
			}
			return result;
		}

		/// <summary>Reduces options that should be in the intersection.</summary>
		private ReduceResult ReduceLockedCandidates(ReduceResult result, SudokuState state)
		{
			if (SkipMethod(SudokuSolverMethods.LockedCandidates, result)) { return result; }

			foreach (var region in Puzzle.Regions)
			{
				foreach (var other in region.Intersected)
				{
					ulong combined = 0;
					foreach (var index in region)
					{
						if (!other.Contains(index))
						{
							combined |= state[index];
						}
					}
					// There are options that should be in the intersection.
					if (combined != Puzzle.Unknown)
					{
						foreach (var index in other)
						{
							if (!region.Contains(index))
							{
								var val = state[index];
								var nw = val & combined;
								result |= state.AndMask(index, nw);
							}
						}
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
		private ReduceResult ReduceNakedPairs(ReduceResult result, SudokuState state)
		{
			if (SkipMethod(SudokuSolverMethods.NakedPairs, result)) { return result; }

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
							result |= state.Exclude(index, pair0);
						}
					}
				}

			}
			return result;
		}
	}
}
