using Sudoku.IO;
using System.IO;

namespace Puzzles.IO;

/// <summary>Stores a (regualar) Sudoku puzzle in 30 byte.</summary>
public sealed class BinaryPuzzle(Clues clues, Cells solution) : Puzzle, IEquatable<BinaryPuzzle>
{
    public BinaryPuzzle(Puzzle other) : this(other.Clues, other.Solution) { }

    /// <summary>The number of bytes needed per puzzle.</summary>
    public static readonly int ByteSize = (100 + 81 + 7) / 8;

    /// <summary>The bit maksk for the cells.</summary>
    public static readonly UInt128 CellsMask = (UInt128.One << 100) - 1;

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
        var bytes = new byte[16];
        var num = Binary.ToUInt128(Solution);
        var clu = UInt128.Zero;

        foreach (var pos in Clues.Select(c => c.Pos))
            clu |= UInt128.One << pos;

        // the cells ande first clue
        BitConverter.TryWriteBytes(bytes, num | (clu << ((13 * 8) - 1)));
        stream.Write(bytes.AsSpan(..13));

        // All except the first clue
        BitConverter.TryWriteBytes(bytes, clu >> 1);
        stream.Write(bytes.AsSpan(..10));
    }

    public static BinaryPuzzle Load(Stream stream)
    {
        var bytes = new byte[ByteSize];
        stream.ReadExactly(bytes);
        var num = BitConverter.ToUInt128(bytes.AsSpan(0..16)) & CellsMask;
        var clu = BitConverter.ToUInt128(bytes.AsSpan(^16..)) >> (128 - 81);

        var solution = Binary.TolCells(num);
        var posset = new PosSet(clu);
        return new(new(posset.Select(p => new Cell(p, solution[p]))), solution);
    }
}
