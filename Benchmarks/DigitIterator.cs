using BenchmarkDotNet.Attributes;
using Sudoku;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Benchmarks;

public class DigitIterator
{
    private static readonly Random Rnd = new(42);

    readonly ImmutableArray<Digits> All = [ ..Enumerable
        .Range(0, 0b_111_111_111)
        .Select(i => new Digits((uint)i << 1))
        .OrderBy(_ => Rnd.Next())];

    [Benchmark]
    public int Sum()
    {
        var sum = 0;

        foreach (var values in All)
        {
            var iterator = new Digits.Iterator(values.Bits);
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }
}
