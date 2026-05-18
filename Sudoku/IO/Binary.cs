namespace Sudoku.IO;

/// <summary>Binary helper methods to convert from and to binary structures.</summary>
public static class Binary
{
    /// <summary>Converts a number into <see cref="Cells"/>.</summary>
    /// <remarks>
    /// Only the first 100 bits are considered.
    /// </remarks>
    [Pure]
    public static Cells TolCells(UInt128 num)
    {
        var indexes = new byte[_9x9];

        for (var pos = MaxPos; pos >= 0; pos--)
        {
            var factor = Factor[pos];
            indexes[pos] = (byte)(num % factor);
            num /= factor;
        }

        var cells = Cells.Empty;
        Digits[] rows = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] cols = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] boxs = [.. range(_9).Select(_ => _1_to_9)];

        for (Pos pos = Pos.O; pos < _9x9; pos++)
        {
            var (row, col) = pos;
            var box = (col / 3) + ((row / 3) * 3);

            var digits = rows[row] & cols[col] & boxs[box];
            var index = indexes[pos];
            var digit = Digit[digits][index];

            cells[pos] = digit;

            rows[row] ^= digit;
            cols[col] ^= digit;
            boxs[box] ^= digit;
        }
        return cells;
    }

    /// <summary>Converts <see cref="Cells"/> into a number.</summary>
    [Pure]
    public static UInt128 ToUInt128(Cells cells)
    {
        var num = UInt128.Zero;
        Digits[] rows = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] cols = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] boxs = [.. range(_9).Select(_ => _1_to_9)];

        for (Pos pos = Pos.O; pos <= MaxPos; pos++)
        {
            var (row, col) = pos;
            var box = (col / 3) + ((row / 3) * 3);

            var digit = cells[row, col];
            var digits = rows[row] & cols[col] & boxs[box];

            var factor = Factor[pos];
            var index = Index[digits][digit];

            num *= factor;
            num += index;

            rows[row] ^= digit;
            cols[col] ^= digit;
            boxs[box] ^= digit;
        }
        return num;
    }

    /// <summary>Converts <see cref="Cells"/> into a number.</summary>
    [Pure]
    public static byte[] ToIndexes(Cells cells)
    {
        var indexes = new byte[_9x9];
        Digits[] rows = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] cols = [.. range(_9).Select(_ => _1_to_9)];
        Digits[] boxs = [.. range(_9).Select(_ => _1_to_9)];

        for (Pos pos = Pos.O; pos <= MaxPos; pos++)
        {
            var (row, col) = pos;
            var box = (col / 3) + ((row / 3) * 3);

            var digit = cells[row, col];
            var digits = rows[row] & cols[col] & boxs[box];
            var index = Index[digits][digit];

            indexes[pos] = index;

            rows[row] ^= digit;
            cols[col] ^= digit;
            boxs[box] ^= digit;
        }
        return indexes;
    }

    /// <summary>Gets the conversion factor.</summary>
    /// <remarks>
    /// These factor is equal to the higest possible number of candidates left
    /// when taken into account that the previous cells are all known.
    /// </remarks>
    public static readonly ImmutableArray<byte> Factor =
    [
        9, 8, 7, 6, 5, 4, 3, 2, 1,
        6, 5, 4, 6, 5, 4, 3, 2, 1,
        3, 2, 1, 3, 2, 1, 3, 2, 1,
        6, 6, 6, 6, 5, 4, 3, 2, 1,
        5, 5, 4, 5, 5, 4, 3, 2, 1,
        3, 2, 1, 3, 2, 1, 3, 2, 1,
        3, 3, 3, 3, 3, 3, 1, 1, 1,
        2, 2, 2, 2, 2, 2, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1,
    ];

    public static readonly Pos MaxPos = new(Factor.LastIndexOf(2));

    private static readonly DigitLookup<ImmutableArray<byte>> Index = InitIndex();

    private static readonly DigitLookup<ImmutableArray<byte>> Digit = InitDigit();

    private static DigitLookup<ImmutableArray<byte>> InitIndex()
    {
        var lookup = new DigitLookup<ImmutableArray<byte>>();

        foreach (var digits in Digits.All)
        {
            byte i = 0;
            var index = new byte[10];
            foreach (var digit in digits) index[digit] = i++;

            lookup[digits] = [.. index];
        }
        return lookup;
    }

    private static DigitLookup<ImmutableArray<byte>> InitDigit()
    {
        var lookup = new DigitLookup<ImmutableArray<byte>>();

        foreach (var digits in Digits.All)
        {
            var i = 0;
            var index = new byte[10];
            foreach (var digit in digits) index[i++] = (byte)digit;

            lookup[digits] = [.. index];
        }
        return lookup;
    }
}
