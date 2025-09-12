using BenchmarkDotNet.Attributes;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;

namespace Benchmarks;

public class Cracking_the_Cryptic
{
    private static readonly _2024_12_08 _2024_12_08 = new();
    private static readonly _2025_05_21 _2025_05_21 = new();
    private static readonly _2025_08_19 _2025_08_19 = new();
    private static readonly _2025_08_21 _2025_08_21 = new();
    private static readonly _2025_09_04 _2025_09_04 = new();
    private static readonly _2025_09_09 _2025_09_09 = new();


    [Benchmark]
    public Cells Fortune_Cookie_II() => _2024_12_08.Solve();

    [Benchmark]
    public Cells Stepped_Themos() => _2025_05_21.Solve();

    [Benchmark]
    public Cells Piles_Of_15() => _2025_08_19.Solve();

    [Benchmark]
    public Cells Miracle_Of_Eleven() => _2025_08_21.Solve();

    [Benchmark]
    public Cells Packing_Problem() => _2025_09_04.Solve();

    [Benchmark]
    public Cells Phistomefel() => _2025_09_09.Solve();
}
