namespace Corniel.Sudoku
{
    public static class SudokuCell
	{
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
