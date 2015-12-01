using System;
using System.Collections.Generic;
using System.Text;

namespace Corniel.Sudoku
{
	public struct SudokuState3x3 : ISudokuState, IEquatable<SudokuState3x3>
	{
		/// <summary>Represents a square with no valid options.</summary>
		public const ushort Invalid = 0;
		/// <summary>Represents a square where all options are still open.</summary>
		public const ushort Unkown = 0x1FF;
		/// <summary>Represents a square with value 1.</summary>
		public const ushort Value1 = 0x001;
		/// <summary>Represents a square with value 2.</summary>
		public const ushort Value2 = 0x002;
		/// <summary>Represents a square with value 3.</summary>
		public const ushort Value3 = 0x004;
		/// <summary>Represents a square with value 4.</summary>
		public const ushort Value4 = 0x008;
		/// <summary>Represents a square with value 5.</summary>
		public const ushort Value5 = 0x010;
		/// <summary>Represents a square with value 6.</summary>
		public const ushort Value6 = 0x020;
		/// <summary>Represents a square with value 7.</summary>
		public const ushort Value7 = 0x040;
		/// <summary>Represents a square with value 8.</summary>
		public const ushort Value8 = 0x080;
		/// <summary>Represents a square with value 9.</summary>
		public const ushort Value9 = 0x100;

		/// <summary>All possible (single option) values.</summary>
		public static readonly ushort[] Values = { Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9 };

		/// <summary>A lookup to get the number of options of a value.</summary>
		private static readonly byte[] CountLookup = GetCountLookup();
		private static byte[] GetCountLookup()
		{
			var count = new byte[SudokuState3x3.Unkown + 1];

			for (ushort val = 1; val <= SudokuState3x3.Unkown; val++)
			{
				count[val] = unchecked((byte)Bits.Count(val));
			}
			return count;
		}

		/// <summary>Constructor.</summary>
		private SudokuState3x3(ushort[] vals)
		{
			values = vals;
		}

		/// <summary>The underlying byte array.</summary>
		private ushort[] values;

		/// <summary>Creates a copy of the current state.</summary>
		public ISudokuState Copy()
		{
			var copy = new ushort[9 * 9];
			Array.Copy(values, copy, 9 * 9);
			return new SudokuState3x3(copy);
		}

		/// <summary>Return true if both indexes represent the same options, otherwise false.</summary>
		public bool Equals(int index0, int index1) { return values[index0] == values[index1]; }

		/// <summary>Return true of the square of the index has only one optional value, otherwise false.</summary>
		public bool IsKnown(int index) { return Count(index) == 1; }

		/// <summary>Returns true if the Sudoku is solved, otherwise false.</summary>
		public bool IsSolved()
		{
			for (var index = 0; index < values.Length; index++)
			{
				if (!IsKnown(index)) { return false; }
			}
			return true;
		}

		/// <summary>Gets the number of optional values for a given index of the Sudoku state.</summary>
		public int Count(int index) { return CountLookup[values[index]]; }

		/// <summary>Reduced a square by excluding the options of the other.</summary>
		/// <param name="square">
		/// The index of the square to reduce.
		/// </param>
		/// <param name="index">
		/// The index of the square to exclude the value(s) from.
		/// </param>
		public ReduceResult Reduce(int square, int index)
		{
			unchecked
			{
				var val = values[square];
				var and = ~values[index];
				var nw = val & and;

				if (nw == SudokuState3x3.Invalid) { return ReduceResult.Inconsistend; }

				values[square] = (ushort)nw;
				return val != nw ? ReduceResult.Reduced : ReduceResult.None;
			}
		}

		/// <summary>Reduces the state by applying hidden singles.</summary>
		public ReduceResult ReduceHiddenSingles(SudokuRegion region)
		{
			unchecked
			{
				var result = ReduceResult.None;

				foreach (var mask in SudokuState3x3.Values)
				{
					var cnt = 0;
					var found = -1;
					foreach (var index in region)
					{
						var val = values[index];
						if ((val & mask) != SudokuState3x3.Invalid)
						{
							cnt++;
							if (IsKnown(index))
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
						values[found] = mask;
						result |= ReduceResult.Reduced;
					}
					else if (cnt == 0)
					{
						return ReduceResult.Inconsistend;
					}
				}
				return result;
			}
		}

		/// <summary>Reduces a state checking on the requirements of the intersection.</summary>
		public ReduceResult ReduceIntersection(SudokuRegion region, SudokuRegion other)
		{
			unchecked
			{
				var result = ReduceResult.None;

				var combined = 0;
				foreach (var index in region)
				{
					if (!other.Contains(index))
					{
						combined |= values[index];
					}
				}
				// There are options that should be in the intersection.
				if (combined != Unkown)
				{
					foreach (var index in other)
					{
						if (!region.Contains(index))
						{
							var val = values[index];
							var nw = val & combined;

							if (nw == SudokuState3x3.Invalid) { return ReduceResult.Inconsistend; }

							values[index] = (ushort)nw;

							if (val != nw)
							{
								result |= ReduceResult.Reduced;
							}
						}
					}
				}
				return result;
			}
		}

		/// <summary>Represents the Sudoku state as string.</summary>
		public override string ToString()
		{
			var sb = new StringBuilder();

			for (var index = 0; index < values.Length; index++)
			{
				var val = values[index];

				if (index % 9 == 0) { sb.AppendLine(); }
				else if (index % 3 == 0) { sb.Append('|'); }
				if (index > 0 && index % (3 * 9) == 0) { sb.AppendLine("---+---+---"); }

				switch (val)
				{
					case SudokuState3x3.Value1: sb.Append('1'); break;
					case SudokuState3x3.Value2: sb.Append('2'); break;
					case SudokuState3x3.Value3: sb.Append('3'); break;
					case SudokuState3x3.Value4: sb.Append('4'); break;
					case SudokuState3x3.Value5: sb.Append('5'); break;
					case SudokuState3x3.Value6: sb.Append('6'); break;
					case SudokuState3x3.Value7: sb.Append('7'); break;
					case SudokuState3x3.Value8: sb.Append('8'); break;
					case SudokuState3x3.Value9: sb.Append('9'); break;
					default: sb.Append('.'); break;
				}
				
			}
			return sb.ToString();
		}

		#region IEquatable

		public override bool Equals(object obj)
		{
			if (obj is SudokuState3x3)
			{
				return Equals((SudokuState3x3)obj);
			}
			return false;
		}
		public bool Equals(SudokuState3x3 other)
		{
			for (var i = 0; i < values.Length; i++)
			{
				if (values[i] != other.values[i]) { return false; }
			}
			return true;
		}
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = values[0];
				for (var i = 1; i < 9 * 9; i++)
				{
					hash ^= values[i] << ((i << 1) % 29);
				}
				return hash;
			}
		}

		#endregion

		/// <summary>Creates a Sudoku state.</summary>
		internal static SudokuState3x3 Create(List<SudokuParseToken[]> rows)
		{
			var vals = new ushort[9 * 9];

			var index = 0;
			for (var x = 0; x < 9; x++)
			{
				for (var y = 0; y < 9; y++)
				{
					switch (rows[x][y])
					{
						case SudokuParseToken.Num1: vals[index] = SudokuState3x3.Value1; break;
						case SudokuParseToken.Num2: vals[index] = SudokuState3x3.Value2; break;
						case SudokuParseToken.Num3: vals[index] = SudokuState3x3.Value3; break;
						case SudokuParseToken.Num4: vals[index] = SudokuState3x3.Value4; break;
						case SudokuParseToken.Num5: vals[index] = SudokuState3x3.Value5; break;
						case SudokuParseToken.Num6: vals[index] = SudokuState3x3.Value6; break;
						case SudokuParseToken.Num7: vals[index] = SudokuState3x3.Value7; break;
						case SudokuParseToken.Num8: vals[index] = SudokuState3x3.Value8; break;
						case SudokuParseToken.Num9: vals[index] = SudokuState3x3.Value9; break;
						
						case SudokuParseToken.Uknown:
						default: vals[index] = SudokuState3x3.Unkown; break;
					}
					index++;
				}
			}
			var collection = new SudokuState3x3(vals);
			return collection;
		}
	}
}
