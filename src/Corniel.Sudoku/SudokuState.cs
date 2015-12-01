using System;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
	/// <summary>Sudoku state factory.</summary>
	public static class SudokuState
	{
		/// <summary>Parses a Sudoku puzzle state.</summary>
		/// <param name="puzzle">
		/// The puzzle to parse.</param>
		public static ISudokuState Parse(string puzzle)
		{
			if (String.IsNullOrEmpty(puzzle)) { throw new ArgumentNullException("puzzle"); }

			var rows = new List<SudokuParseToken[]>();

			var buffer = new List<SudokuParseToken>();
			foreach (var ch in puzzle)
			{
				switch (ch)
				{
					case '.':
					case '?': buffer.Add(SudokuParseToken.Uknown); break;
					case '1': buffer.Add(SudokuParseToken.Num1); break;
					case '2': buffer.Add(SudokuParseToken.Num2); break;
					case '3': buffer.Add(SudokuParseToken.Num3); break;
					case '4': buffer.Add(SudokuParseToken.Num4); break;
					case '5': buffer.Add(SudokuParseToken.Num5); break;
					case '6': buffer.Add(SudokuParseToken.Num6); break;
					case '7': buffer.Add(SudokuParseToken.Num7); break;
					case '8': buffer.Add(SudokuParseToken.Num8); break;
					case '9': buffer.Add(SudokuParseToken.Num9); break;
					case '\n':
						if (buffer.Any())
						{
							rows.Add(buffer.ToArray());
							buffer.Clear();
						}
						break;
					// Just ignore.
					default: break;
				}

			}
			if (buffer.Any()) { rows.Add(buffer.ToArray()); }

			if (rows.Count == 9 && rows.All(row => row.Length == 9))
			{
				return SudokuState3x3.Create(rows);
			}
			if (rows.Count == 4 && rows.All(row => row.Length == 4))
			{
				return SudokuState2x2.Create(rows);
			}
			return null;
		}
	}
}
