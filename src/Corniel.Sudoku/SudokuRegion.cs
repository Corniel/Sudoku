using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Corniel.Sudoku
{
	/// <summary>Represents a (distinct) Sudoku region.</summary>
	[DebuggerDisplay("{ToString()}")]
	public class SudokuRegion : HashSet<int> 
	{
		public SudokuRegion(SudokuRegionType type)
		{
			RegionType = type;
		}

		/// <summary>Gets the type of the region.</summary>
		public SudokuRegionType RegionType { get; protected set; }

		/// <summary>Represents the region as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("{1}: {{{0}}}", string.Join(", ", this.ToArray()), RegionType);
		}

		/// <summary>Returns true if the other has a intersection with this region of at least 2 squares, but is not the same region.</summary>
		public bool HasIntersectionOf2OrMoreSquares(SudokuRegion other)
		{
			if (other == this) { return false; }
			var count = 0;
			foreach (var index in this)
			{
				if (other.Contains(index)) { count++; }
				if (count > 1) { break; }
			}
			return count > 1;
		}
	}
}
