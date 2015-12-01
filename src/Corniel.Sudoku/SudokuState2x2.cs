using System;
using System.Collections.Generic;
using System.Text;

namespace Corniel.Sudoku
{
	public struct SudokuState2x2 : ISudokuState, IEquatable<SudokuState2x2>
	{
		/// <summary>Represents a square with no valid options.</summary>
		public const byte Invalid = 0;
		/// <summary>Represents a square where all options are still open.</summary>
		public const byte Unkown = 0xF;
		/// <summary>Represents a square with value 1.</summary>
		public const byte Value1 = 0x1;
		/// <summary>Represents a square with value 2.</summary>
		public const byte Value2 = 0x2;
		/// <summary>Represents a square with value 3.</summary>
		public const byte Value3 = 0x4;
		/// <summary>Represents a square with value 4.</summary>
		public const byte Value4 = 0x8;

		/// <summary>All possible (single option) values.</summary>
		public static readonly byte[] Values = { Value1, Value2, Value3, Value4 };

		/// <summary>Constructor.</summary>
		private SudokuState2x2(byte[] vals)
		{
			values = vals;
		}

		/// <summary>The underlying byte array.</summary>
		private byte[] values;

		/// <summary>Creates a copy of the current state.</summary>
		public ISudokuState Copy()
		{
			var copy = new byte[4 * 4];
			Array.Copy(values, copy, 4 * 4);
			return new SudokuState2x2(copy);
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
		public int Count(int index)
		{
			return Bits.Count(values[index]);
		}

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

				if (nw == SudokuState2x2.Invalid) { return ReduceResult.Inconsistend; }

				values[square] = (byte)nw;
				return val != nw ? ReduceResult.Reduced : ReduceResult.None;
			}
		}

		/// <summary>Reduces the state by applying hidden singles.</summary>
		public ReduceResult ReduceHiddenSingles(SudokuRegion region)
		{
			var result = ReduceResult.None;

			foreach (var mask in SudokuState2x2.Values)
			{
				var cnt = 0;
				var found = -1;
				foreach (var index in region)
				{
					var val = values[index];
					if ((val & mask) != SudokuState2x2.Invalid)
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

							if (nw == SudokuState2x2.Invalid) { return ReduceResult.Inconsistend; }

							values[index] = (byte)nw;

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
				if (index % 4 == 2) { sb.Append('|'); }

				if (values.Length >> 1 == index) { sb.AppendLine("--+--"); }

				var val = values[index];
				switch (val)
				{
					case SudokuState2x2.Value1: sb.Append('1'); break;
					case SudokuState2x2.Value2: sb.Append('2'); break;
					case SudokuState2x2.Value3: sb.Append('3'); break;
					case SudokuState2x2.Value4: sb.Append('4'); break;
					default: sb.Append('.'); break;
				}
				if (index % 4 == 3) { sb.AppendLine(); }
			}
			return sb.ToString();
		}

		#region IEquatable

		public override bool Equals(object obj)
		{
			if (obj is SudokuState2x2)
			{
				return Equals((SudokuState2x2)obj);
			}
			return false;
		}
		public bool Equals(SudokuState2x2 other)
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
				for (var i = 1; i < 4 * 4; i++)
				{
					hash ^= values[i] << (i << 1);
				}
				return hash;
			}
		}

		#endregion

		/// <summary>Creates a Sudoku state.</summary>
		internal static SudokuState2x2 Create(List<SudokuParseToken[]> rows)
		{

			var vals = new byte[4 * 4];

			var index = 0;
			for (var x = 0; x < 4; x++)
			{
				for (var y = 0; y < 4; y++)
				{
					switch (rows[x][y])
					{
						case SudokuParseToken.Num1: vals[index] = SudokuState2x2.Value1; break;
						case SudokuParseToken.Num2: vals[index] = SudokuState2x2.Value2; break;
						case SudokuParseToken.Num3: vals[index] = SudokuState2x2.Value3; break;
						case SudokuParseToken.Num4: vals[index] = SudokuState2x2.Value4; break;

						case SudokuParseToken.Uknown:
						default: vals[index] = SudokuState2x2.Unkown; break;
					}
					index++;
				}
			}
			var collection = new SudokuState2x2(vals);
			return collection;
		}
	}
}
