using BenchmarkDotNet.Attributes;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;

namespace Benchmarks;

public class Cracking_the_Cryptic
{
    private static readonly _2025_08_07 Test = new();
    
    [Benchmark]
    public Cells Puzzle() => Test.Solve();
}
