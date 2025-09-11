using BenchmarkDotNet.Attributes;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;
using SudokuSolver.Solvers;

namespace Benchmarks;

public class Cracking_the_Cryptic
{
    private static readonly _2025_05_21 _2025_05_21 = new();
    private static readonly _2025_08_21 _2025_08_21 = new();

    [Benchmark]
    public Cells Stepped_Themos()
        => DynamicSolver.Solve(_2025_05_21.Clues, _2025_05_21.Constraints);

    [Benchmark]
    public Cells Miracle_Of_Eleven()
        => DynamicSolver.Solve(_2025_08_21.Clues, _2025_08_21.Constraints);
}
