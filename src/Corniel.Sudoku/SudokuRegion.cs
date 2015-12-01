using System.Collections.Generic;

namespace Corniel.Sudoku
{
	/// <summary>Represents a (distinct) Sudoku region.</summary>
	public class SudokuRegion : List<int> 
	{
		/// <summary>Returns true if the other has a intersection with this region, but is not the same region.</summary>
		public bool IntersectsWith(SudokuRegion other)
		{
			if (other == this) { return false; }
			foreach (var index in this)
			{
				if (other.Contains(index)) { return true; }
			}
			return false;
		}
	}
}
