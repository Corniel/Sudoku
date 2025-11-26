using Sudoku.Generics;
using System.IO;
using System.Numerics;

namespace Puzzles.IO;

/// <summary>Stores a (regualar) Sudoku puzzle in 30 byte.</summary>
public sealed class BinaryPuzzle(Clues clues, Cells solution) : Puzzle, IEquatable<BinaryPuzzle>
{
    public override string Title => nameof(BinaryPuzzle);

    public override Clues Clues { get; } = clues;

    public override Cells Solution { get; } = solution;

    public override bool Equals(object? obj) => obj is BinaryPuzzle other && Equals(other);

    public bool Equals(BinaryPuzzle? other)
        => other is { }
        && Clues.Equals(other.Clues)
        && Solution.Equals(other.Solution);

    public override int GetHashCode()
        => Clues.GetHashCode()
        ^ Solution.GetHashCode();

    public void WriteTo(Stream stream)
    {
        var big = BigInteger.Zero;
        var row = 0;
        var col = 0;
        var buf = Digits._1_to_9;

        foreach (var factor in Factors)
        {
            var digit = Solution[row, col];
            var add = Index[buf][digit];

            big *= factor;
            big += add;
            buf ^= digit;

            if (factor is 2)
            {
                row++;
                col = 0;
                buf = Digits._1_to_9;
            }
            else col++;
        }

        var bytes = big.ToByteArray();

        stream.Write(bytes);
        if (bytes.Length is 18) stream.WriteByte(0);

        Int128 clue = 0;

        foreach (var pos in Clues.Select(c => c.Pos))
            clue |= Int128.One << pos;

        stream.Write(BitConverter.GetBytes(clue).AsSpan(..11));
    }

    public static BinaryPuzzle Load(Stream stream)
    {
        var bytes = new byte[30];
        if (stream.Read(bytes) != bytes.Length) throw new ArgumentOutOfRangeException(nameof(stream));

        var idx = new byte[Factors.Length];
        var big = new BigInteger(bytes.AsSpan(..19));

        for (var i = idx.Length - 1; i >= 0; i--)
        {
            var factor = Factors[i];
            idx[i] = (byte)(big % factor);
            big /= factor;
        }

        var cols = new Digits[_9];
        var solution = Cells.Empty;
        var fac = 0;

        for (var row = 0; row < 8; row++)
        {
            var buf = Digits._1_to_9;
            for (var col = 0; col < 8; col++)
            {
                var index = idx[fac++];
                var digit = Digit[buf][index];
                cols[col] |= digit;
                buf ^= digit;
                solution[row, col] = digit;
            }

            // Resolve last digit
            solution[row, 8] = buf.First();
            cols[8] |= buf.First();
        }

        // Resolve last row
        for (var col = 0; col < _9; col++)
            solution[8, col] = (~cols[col]).First();

        var clue = new PosSet(BitConverter.ToInt128([.. bytes.AsSpan(19..), 0, 0, 0, 0, 0]));

        return new(
            new Clues(clue.Select(p => new Cell(p, solution[p]))),
            solution);
    }

    private static readonly ImmutableArray<byte> Factors =
    [
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
        9, 8, 7, 6, 5, 4, 3, 2,
    ];

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
