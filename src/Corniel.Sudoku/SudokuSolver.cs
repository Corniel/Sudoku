using System;
using System.Linq;

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
				result |= ReduceNakedTriples(result, worker);
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
				(result & (ReduceResult.Solved | ReduceResult.Inconsistend)) != ReduceResult.None;
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

			foreach (var singleValue in Puzzle.SingleValues)
			{
				foreach (var region in Puzzle.Regions)
				{
					var index0 = -1;
					var index1 = -1;

					var match = singleValue;

					foreach (var index in region)
					{
						var value = state[index];
						if (!state.IsKnown(index) && (value & match) != SudokuPuzzle.Invalid)
						{
							match |= value;

							/**/ if (index0 == -1) { index0 = index; }
							else if (index1 == -1) { index1 = index; }
							else { index1 = -1; break; }
						}
					}
					// We found 2 squares.
					if (index1 != -1 && SudokuState.CountLookup[match] == 2)
					{
						foreach (var index in region)
						{
							if (index != index0 && index != index1)
							{
								result |= state.AndMask(index, ~match);
							}
						}
					}
				}
			}
			return result;
		}

		private ReduceResult ReduceNakedTriples(ReduceResult result, SudokuState state)
		{
			if (SkipMethod(SudokuSolverMethods.NakedTriples, result)) { return result; }

			foreach (var singleValue in Puzzle.SingleValues)
			{
				foreach (var region in Puzzle.Regions)
				{
					var index0 = -1;
					var index1 = -1;
					var index2 = -1;

					var match = singleValue;

					foreach (var index in region)
					{
						var value = state[index];
						if (!state.IsKnown(index) && (value & match) != SudokuPuzzle.Invalid)
						{
							match |= value;

							/**/ if (index0 == -1) { index0 = index; }
							else if (index1 == -1) { index1 = index; }
							else if (index2 == -1) { index2 = index; }
							else { index2 = -1; break; }
						}
					}
					// We found 3 squares.
					if (index2 != -1 && SudokuState.CountLookup[match] == 3)
					{
						foreach (var index in region)
						{
							if (index != index0 && index != index1 && index != index2)
							{
								result |= state.AndMask(index, ~match);
							}
						}
					}
				}
			}
			return result;
		}
	}
}
