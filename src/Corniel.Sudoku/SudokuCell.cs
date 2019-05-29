using System.Collections.Generic;

namespace Corniel.Sudoku
{
    public static class SudokuCell
	{
        /// <summary>Represents a cell with no valid options.</summary>
        public const uint Invalid = 0;

        /// <summary>Represents a cell with value 1.</summary>
        public const uint Value1 = 0x001;
        /// <summary>Represents a cell with value 2.</summary>
        public const uint Value2 = 0x002;
        /// <summary>Represents a cell with value 3.</summary>
        public const uint Value3 = 0x004;
        /// <summary>Represents a cell with value 4.</summary>
        public const uint Value4 = 0x008;
        /// <summary>Represents a cell with value 5.</summary>
        public const uint Value5 = 0x010;
        /// <summary>Represents a cell with value 6.</summary>
        public const uint Value6 = 0x020;
        /// <summary>Represents a cell with value 7.</summary>
        public const uint Value7 = 0x040;
        /// <summary>Represents a cell with value 8.</summary>
        public const uint Value8 = 0x080;
        /// <summary>Represents a cell with value 9.</summary>
        public const uint Value9 = 0x100;

        /// <summary>Gets the unknown value.</summary>
        public const uint Unknown = 0x1FF;

        /// <summary>Gets all single values a cell could (potentially) have.</summary>
        public static readonly IReadOnlyList<uint> Singles = new[] { Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9 };

        public static uint Single(int value) => _SingleLookUp[value];

        private static readonly uint[] _SingleLookUp = { Invalid, Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9 };

        /// <summary>Counts the number of options for this cell.</summary>
        public static int Count(uint cell) => CountLookup[cell];

		/// <summary>A lookup to get the number of options of a value.</summary>
		private static readonly byte[] CountLookup = GetCountLookup();

        

        private static byte[] GetCountLookup()
		{
			var count = new byte[SudokuPuzzle.Unknown + 1];

			for (ushort val = 1; val <= SudokuPuzzle.Unknown; val++)
			{
				count[val] = unchecked((byte)Bits.Count(val));
			}
			return count;
		}
	}
}
