using BenchmarkDotNet.Attributes;
using Sudoku;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;

namespace Benchmark;

public class Digit_Iterator
{
    private static readonly Random Rnd = new(42);

    private static readonly ImmutableArray<Digits> AsDigits =
    [
        .. Enumerable.Range(0, 2000)
            .SelectMany(_ => Digits.All)
            .Take(1_000_000)
            .OrderBy(_ => Rnd.Next())
    ];

    private static readonly ImmutableArray<ImmutableArray<int>> AsImmutable = [.. AsDigits.Select(d => d.ToImmutableArray())];

    private static readonly ImmutableArray<int[]> AsArray = [.. AsDigits.Select(d => d.ToArray())];

    private static readonly ImmutableArray<List<int>> AsList = [.. AsDigits.Select(d => d.ToList())];

    private static readonly ImmutableArray<uint> AsBits = [.. AsDigits.Select(d => d.Bits)];

    [Benchmark(Baseline = true)]
    public int Iterator()
    {
        var sum = 0;

        foreach (var values in AsDigits)
        {
            var iterator = new Digits.Iterator(values.Bits);
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }

    [Benchmark]
    public int Immutable()
    {
        var sum = 0;

        foreach (var values in AsImmutable)
        {
            var iterator = values.GetEnumerator();
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }

    [Benchmark]
    public int List()
    {
        var sum = 0;

        foreach (var values in AsList)
        {
            var iterator = values.GetEnumerator();
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }

    [Benchmark]
    public int Array()
    {
        var sum = 0;

        foreach (var values in AsArray)
        {

            for (var i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
        }
        return sum;
    }

    [Benchmark]
    public int Old_Iterator()
    {
        var sum = 0;

        foreach (var values in AsBits)
        {
            var iterator = new OldIterator(values);
            while (iterator.MoveNext())
            {
                sum += iterator.Current;
            }
        }
        return sum;
    }

    public struct OldIterator(uint bits) : IEnumerator<int>, IEnumerable<int>
    {
        private uint Remaining = bits;

        public int Current { get; private set; } = -1;

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Remaining is 0) return false;

            var trailing = BitOperations.TrailingZeroCount(Remaining) + 1;
            Remaining >>= trailing;
            Current += trailing;
            return true;
        }

        public readonly IEnumerator<int> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();

        public readonly void Dispose() { /* Nothing to dispose. */ }
    }
}
