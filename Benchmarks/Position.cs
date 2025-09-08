using BenchmarkDotNet.Attributes;
using SudokuSolver;
using System.Collections.Immutable;
using System.Linq;

namespace Benchmarks;

public class Position
{
    public class Iterate
    {
        private readonly ImmutableArray<Pos>[] array;
        private readonly PosSet[] posst;

        public Iterate()
        {
            array = [.. Houses.Standard.Select(h => h.ToImmutableArray())];
            posst = [.. Houses.Standard];
        }

        [Benchmark]
        public int ImmutableArray()
        {
            var sum = 0;
            foreach (var arr in array)
            {
                foreach (var pos in arr)
                {
                    sum += pos;
                }
            }
            return sum;
        }

        [Benchmark]
        public int PosSets()
        {
            var sum = 0;
            foreach (var arr in posst)
            {
                foreach (var pos in arr)
                {
                    sum += pos;
                }
            }
            return sum;
        }
    }
}
