using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corniel.Sudoku
{
    /// <summary>Sudoku state factory.</summary>
    public class SudokuState : IEquatable<SudokuState>
    {
        /// <summary>Constructor.</summary>
        private SudokuState(int size, int unknown, ulong[] values)
        {
            Size = size;
            m_Unknown = unknown;
            m_Values = values;
        }

        public int Size { get; }

        /// <summary>The underlying byte array.</summary>
        private readonly ulong[] m_Values;

        /// <summary>The number of cells that is unknown.</summary>
        private int m_Unknown;

        public ulong this[int index] => m_Values[index];

        /// <summary>Gets the number of unknowns.</summary>
        public int Unknowns => m_Unknown;

        /// <summary>Creates a copy of the current state.</summary>
        public SudokuState Copy()
        {
            var copy = new ulong[m_Values.Length];
            Array.Copy(m_Values, copy, m_Values.Length);
            return new SudokuState(Size, m_Unknown, copy);
        }

        internal void GetValuesFrom(SudokuState test)
        {
            Array.Copy(test.m_Values, m_Values, test.m_Values.Length);
        }

        #region Count & count related

        /// <summary>Gets the number of optional values for a given index of the Sudoku state.</summary>
        public int Count(int index) => SudokuCell.Count(m_Values[index]);

        /// <summary>Return true of cell square of the index has only one optional value, otherwise false.</summary>
        public bool IsKnown(int index) => Count(index) == 1;

        /// <summary>Return true of cell square of the index has multiple optional values, otherwise false.</summary>
        public bool IsUnknown(int index) => Count(index) > 1;

        /// <summary>Returns true if the Sudoku is solved, otherwise false.</summary>
        public bool IsSolved => m_Unknown == 0;

        #endregion

        #region Updating

        /// <summary>Reduced a cell by excluding the options of the other.</summary>
        /// <param name="index0">
        /// The index of the cell to update.
        /// </param>
        /// <param name="index1">
        /// The index of the cell to exclude the value(s) from.
        /// </param>
        public ReduceResult Exclude(int index0, int index1)
        {
            var mask = ~m_Values[index1];
            return AndMask(index0, mask);
        }

        /// <summary>Reduced a cell by excluding the options of the other.</summary>
        public ReduceResult AndMask(int index, ulong mask)
        {
            unchecked
            {
                var val = m_Values[index];
                var nw = val & mask;
                m_Values[index] = nw;
                if (nw == SudokuPuzzle.Invalid)
                {
                    return ReduceResult.Inconsistent;
                }

                if (SudokuCell.Count(nw) == 1 && SudokuCell.Count(val) != 1)
                {
                    m_Unknown--;
                    if (IsSolved)
                    {
                        return ReduceResult.Solved;
                    }
                }

                return val != nw ? ReduceResult.Reduced : ReduceResult.None;
            }
        }

        #endregion

        #region Formatting

        /// <summary>Represents the Sudoku state as string.</summary>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var index = 0; index < m_Values.Length; index++)
            {
                var val = m_Values[index];

                if (index % (Size * Size) == 0) { sb.AppendLine(); }
                else if (index % Size == 0) { sb.Append('|'); }

                if (index > 0 && index % (Size * Size * Size) == 0) { sb.AppendLine(Sections[Size]); }

                var str = String.Empty;
                if (Mappings[Size].TryGetValue(val, out str))
                {
                    sb.Append(str);
                }
                else
                {
                    sb.Append('.');
                }
            }
            return sb.ToString();
        }
        private static readonly string[] Sections =
        {
            "",
            "-",
            "--+--",
            "---+---+---",
            "----+----+----+----",
        };
        private static readonly Dictionary<int, Dictionary<ulong, string>> Mappings = new Dictionary<int, Dictionary<ulong, string>>()
        {
            { 2, SudokuPuzzle2x2.Mapping },
            { 3, SudokuPuzzle3x3.Mapping },
        };
        #endregion

        #region IEquatable

        public override bool Equals(object obj)
        {
            if (obj is SudokuState)
            {
                return Equals((SudokuState)obj);
            }
            return false;
        }
        public bool Equals(SudokuState other)
        {
            for (var i = 0; i < m_Values.Length; i++)
            {
                if (m_Values[i] != other.m_Values[i]) { return false; }
            }
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = m_Values[0].GetHashCode();
                for (var i = 1; i < m_Values.Length; i++)
                {
                    var shift = i % 25;
                    var code = m_Values[i].GetHashCode();
                    hash ^= (code << shift) | (code >> (32 - shift));
                }
                return hash;
            }
        }

        #endregion

        #region Parsing

        /// <summary>Parses a Sudoku puzzle state.</summary>
        /// <param name="puzzle">
        /// The puzzle to parse.</param>
        public static SudokuState Parse(string puzzle)
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
                return Create3x3(rows);
            }
            if (rows.Count == 4 && rows.All(row => row.Length == 4))
            {
                return Create2x2(rows);
            }
            throw new ArgumentException("Invalid Sudoku puzzle.", "puzzle");
        }

        /// <summary>Creates a Sudoku state.</summary>
        internal static SudokuState Create2x2(List<SudokuParseToken[]> rows)
        {
            var values = new ulong[4 * 4];
            var unknown = 0;

            var index = 0;
            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    switch (rows[x][y])
                    {
                        case SudokuParseToken.Num1: values[index] = SudokuPuzzle2x2.Value1; break;
                        case SudokuParseToken.Num2: values[index] = SudokuPuzzle2x2.Value2; break;
                        case SudokuParseToken.Num3: values[index] = SudokuPuzzle2x2.Value3; break;
                        case SudokuParseToken.Num4: values[index] = SudokuPuzzle2x2.Value4; break;

                        case SudokuParseToken.Uknown:
                        default:
                            values[index] = SudokuPuzzle.Puzzle2x2.Unknown;
                            unknown++;
                            break;
                    }
                    index++;
                }
            }
            return new SudokuState(2, unknown, values);
        }

        /// <summary>Creates a Sudoku state.</summary>
        internal static SudokuState Create3x3(List<SudokuParseToken[]> rows)
        {
            var values = new ulong[9 * 9];
            var unknown = 0;

            var index = 0;
            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    switch (rows[x][y])
                    {
                        case SudokuParseToken.Num1: values[index] = SudokuPuzzle3x3.Value1; break;
                        case SudokuParseToken.Num2: values[index] = SudokuPuzzle3x3.Value2; break;
                        case SudokuParseToken.Num3: values[index] = SudokuPuzzle3x3.Value3; break;
                        case SudokuParseToken.Num4: values[index] = SudokuPuzzle3x3.Value4; break;
                        case SudokuParseToken.Num5: values[index] = SudokuPuzzle3x3.Value5; break;
                        case SudokuParseToken.Num6: values[index] = SudokuPuzzle3x3.Value6; break;
                        case SudokuParseToken.Num7: values[index] = SudokuPuzzle3x3.Value7; break;
                        case SudokuParseToken.Num8: values[index] = SudokuPuzzle3x3.Value8; break;
                        case SudokuParseToken.Num9: values[index] = SudokuPuzzle3x3.Value9; break;

                        case SudokuParseToken.Uknown:
                        default:
                            values[index] = SudokuPuzzle.Puzzle3x3.Unknown;
                            unknown++;
                            break;
                    }
                    index++;
                }
            }
            return new SudokuState(3, unknown, values);
        }

        #endregion
    }
}
