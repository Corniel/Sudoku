using System.Collections.Generic;
namespace Corniel.Sudoku
{
	/// <summary>Represents a default 3x3 puzzle.</summary>
	public class SudokuPuzzle3x3 : SudokuPuzzle
	{
		/// <summary>Represents a square with value 1.</summary>
		public const ulong Value1 = 0x001;
		/// <summary>Represents a square with value 2.</summary>
		public const ulong Value2 = 0x002;
		/// <summary>Represents a square with value 3.</summary>
		public const ulong Value3 = 0x004;
		/// <summary>Represents a square with value 4.</summary>
		public const ulong Value4 = 0x008;
		/// <summary>Represents a square with value 5.</summary>
		public const ulong Value5 = 0x010;
		/// <summary>Represents a square with value 6.</summary>
		public const ulong Value6 = 0x020;
		/// <summary>Represents a square with value 7.</summary>
		public const ulong Value7 = 0x040;
		/// <summary>Represents a square with value 8.</summary>
		public const ulong Value8 = 0x080;
		/// <summary>Represents a square with value 9.</summary>
		public const ulong Value9 = 0x100;

		/// <summary>Gets the mapping from value to String.</summary>
		public static readonly Dictionary<ulong, string> Mapping = new Dictionary<ulong, string>()
		{
			{ Value1, "1" },
			{ Value2, "2" },
			{ Value3, "3" },
			{ Value4, "4" },
			{ Value5, "5" },
			{ Value6, "6" },
			{ Value7, "7" },
			{ Value8, "8" },
			{ Value9, "9" },
		};

		/// <summary>Constructor.</summary>
		internal SudokuPuzzle3x3() : base(3) { }

		/// <summary>Gets the unknown value.</summary>
		public override ulong Unknown { get { return  0x1FF; } }

		/// <summary>Gets the possible (single) values.</summary>
		public override ICollection<ulong> SingleValues { get { return Mapping.Keys; } }
	}
}
