using BenchmarkDotNet.Attributes;
using Generator;
using StrategyBased;
using System;
using System.Linq;

namespace Benchmark;

public class Generation
{
    private static readonly PuzzleGenerator generator = new(ReduceOptions.All, new Random(42));

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
