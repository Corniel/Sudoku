using BenchmarkDotNet.Attributes;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;

namespace Benchmarks;

public class Cracking_the_Cryptic
{
    private static readonly _2024_01_08 Test = new();
    
    [Benchmark]
    public Cells Puzzle() => Test.Solve();
}
