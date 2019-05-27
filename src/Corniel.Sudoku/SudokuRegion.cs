#pragma warning disable S3925 // "ISerializable" should be implemented correctly
// No support for serialization.

using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>Represents a (distinct) Sudoku region.</summary>
    public class SudokuRegion : HashSet<int>
    {
		/// <summary>Creates a Sudoku region.</summary>
		public SudokuRegion(SudokuRegionType type)
		{
			RegionType = type;
			Intersected = new SudokuRegions();
		}

		/// <summary>Gets the type of the region.</summary>
		public SudokuRegionType RegionType { get; }

		public SudokuRegions Intersected { get; }

		/// <summary>Represents the region as <see cref="string"/>.</summary>
		public override string ToString()
		{
			return string.Format("{1}: {{{0}}}", string.Join(", ", this.ToArray()), RegionType);
		}

		/// <summary>Returns true if the other has a intersection with this region of at least 2 cells, but is not the same region.</summary>
		public bool HasIntersectionOf2OrMoreSquares(SudokuRegion other)
		{
			if (other == this) { return false; }
			var count = 0;
			foreach (var index in this)
			{
                if (other.Contains(index))
                {
                    count++;
                }
                if (count > 1)
                {
                    break;
                }
			}
			return count > 1;
		}
	}
}
