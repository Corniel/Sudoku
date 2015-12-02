using System.Collections.Generic;

namespace Corniel.Sudoku
{
	/// <summary>Represents a default 2x2 puzzle.</summary>
	public class SudokuPuzzle2x2 : SudokuPuzzle
	{
		/// <summary>Represents a cell with value 1.</summary>
		public const ulong Value1 = 0x1;
		/// <summary>Represents a cell with value 2.</summary>
		public const ulong Value2 = 0x2;
		/// <summary>Represents a cell with value 3.</summary>
		public const ulong Value3 = 0x4;
		/// <summary>Represents a cell with value 4.</summary>
		public const ulong Value4 = 0x8;

		/// <summary>Gets the mapping from value to String.</summary>
		public static readonly Dictionary<ulong, string> Mapping = new Dictionary<ulong, string>()
		{
			{ Value1, "1" },
			{ Value2, "2" },
			{ Value3, "3" },
			{ Value4, "4" },
		};

		/// <summary>Constructor.</summary>
		internal SudokuPuzzle2x2() : base(2) { }

		/// <summary>Gets the unknown value.</summary>
		public override ulong Unknown { get { return 0xF; } }

		/// <summary>Gets the possible (single) values.</summary>
		public override ICollection<ulong> SingleValues { get { return Mapping.Keys; } }
	}
}
