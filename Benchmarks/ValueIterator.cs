using BenchmarkDotNet.Attributes;
using SudokuSolver;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Benchmarks;

public class ValueIterator
{
    private static readonly Random Rnd = new(42);

    readonly ImmutableArray<Candidates> All = [ ..Enumerable
        .Range(0, 0b_111_111_111)
        .Select(i => new Candidates((uint)i << 1))
        .OrderBy(_ => Rnd.Next())];

    [Benchmark]
    public int Sum()
    {
        var sum = 0;

        foreach (var values in All)
        {
            var iterator = new Candidates.Iterator(values.Bits);
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }
}
