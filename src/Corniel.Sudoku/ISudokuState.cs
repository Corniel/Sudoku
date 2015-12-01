using System.Collections.Generic;

namespace Corniel.Sudoku
{
	/// <summary>Represents a state of a Sudoku puzzle (e.a. all squares and their optional values).</summary>
	public interface ISudokuState
	{
		/// <summary>Creates a copy of the current state.</summary>
		ISudokuState Copy();

		/// <summary>Return true if both indexes represent the same options, otherwise false.</summary>
		bool Equals(int index0, int index1);

		/// <summary>Return true of the square of the index has only one optional value, otherwise false.</summary>
		bool IsKnown(int index);

		/// <summary>Returns true if the Sudoku is solved, otherwise false.</summary>
		bool IsSolved();

		/// <summary>Gets the number of optional values for a given index of the Sudoku state.</summary>
		/// <param name="index">
		/// The index of the square.
		/// </param>
		int Count(int index);

		/// <summary>Reduced a square by excluding the options of the other.</summary>
		/// <param name="square">
		/// The index of the square to reduce.
		/// </param>
		/// <param name="index">
		/// The index of the square to exclude the value(s) from.
		/// </param>
		ReduceResult Reduce(int square, int index);

		/// <summary>Reduces the state by applying hidden singles.</summary>
		ReduceResult ReduceHiddenSingles(SudokuRegion region);

		/// <summary>Reduces a state checking on the requirements of the intersection.</summary>
		ReduceResult ReduceIntersection(SudokuRegion region, SudokuRegion other);
	}
}
