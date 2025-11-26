using BenchmarkDotNet.Attributes;
using Generator;
using StrategyBased;
using Sudoku;
using System;
using System.Linq;

namespace Benchmark;

public class Generation
{
    private static readonly Grids grids = new(new Random(42));
    private static readonly PuzzleGenerator generator = new PuzzleGenerator(ReduceOptions.All, new Random(42));

    [Benchmark]
    public int Grids()
    {
        var res = 0;
        var i = 0;
        while (i++ < 1_000_000)
        {
            grids.MoveNext();
            res ^= grids.Current[Pos.O];
        }
        return res;
    }

    [Benchmark]
    public int Puzzles()
    {
        var res = 0;
        var i = 0;
        while (i++ < 1000)
        {
            generator.MoveNext();
            res ^= generator.Current.Clues.First().Digit;
        }
        return res;
    }
}
