using BenchmarkDotNet.Attributes;
using Generator;
using Puzzles.IO;
using Puzzles.PuzzleBank;
using Sudoku;
using Sudoku.IO;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Benchmark;

public class BinaryConversion
{
    public readonly ImmutableArray<Cells> Cells;
    public readonly ImmutableArray<UInt128> Nums;
    public readonly ImmutableArray<BinaryPuzzle> Puzzles;
    public readonly MemoryStream Read = new();
    public readonly MemoryStream Write = new();

    public BinaryConversion()
    {
        Cells = [.. PuzzleBankPuzzle.Diabolical.Take(1000).Select(p => p.Solution)];
        Nums = [.. Cells.Select(Binary.ToUInt128)];
        Puzzles = [.. PuzzleBankPuzzle.Diabolical.Take(1000).Select(p => new BinaryPuzzle(p))];
        foreach (var puzzle in Puzzles)
            puzzle.WriteTo(Read);
    }

    [Benchmark]
    public UInt128 ToUInt128()
    {
        var zero = UInt128.Zero;

        foreach (var cells in Cells)
            zero |= Binary.ToUInt128(cells);

        return zero;
    }

    [Benchmark]
    public int ToCells()
    {
        var zero = 0;

        foreach (var num in Nums)
            zero |= Binary.TolCells(num)[Pos.O];

        return zero;
    }

    [Benchmark]
    public int Reads()
    {
        Read.Position = 0;
        var index = 0;

        while (Read.Position < Read.Length)
            index ^= BinaryPuzzle.Load(Read).Solution[Pos.O];

        return index;
    }

    [Benchmark]
    public long Writes()
    {
        Write.Position = 0;

        foreach (var puzzle in Puzzles)
            puzzle.WriteTo(Write);

        return Write.Position;
    }
}
