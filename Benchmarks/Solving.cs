using BenchmarkDotNet.Attributes;
using Puzzles.PuzzleBank;
using SudokuSolver;
using SudokuSolver.Solvers;
using System.Collections.Immutable;
using System.Linq;

namespace Benchmarks;

public class Solving
{
    public static readonly ImmutableArray<Clues> Diabolical /*.*/ = Set(PuzzleBankPuzzle.Diabolical);
    public static readonly ImmutableArray<Clues> Hard /*.......*/ = Set(PuzzleBankPuzzle.Hard);
    public static readonly ImmutableArray<Clues> Medium /*.....*/ = Set(PuzzleBankPuzzle.Medium);
    public static readonly ImmutableArray<Clues> Easy /*.......*/ = Set(PuzzleBankPuzzle.Easy);

    public ImmutableArray<Clues> Clues => Config == nameof(Diabolical) ? Diabolical : Hard;

    [Params(nameof(Easy), nameof(Medium), nameof(Hard), nameof(Diabolical))]
    public string Config { get; set; } = nameof(Diabolical);

    [Benchmark]
    public int Reference()
    {
        var solved = 0;

        foreach (var clue in Clues)
        {
            solved += Backtracker.Solve(clue)[0, 0];
        }
        return solved;
    }

    [Benchmark(Baseline = true)]
    public int Default()
    {
        var solved = 0;

        foreach (var clue in Clues)
        {
            solved += Solver.Solve(clue, Rules.Standard, ReduceOptions.Default)[Pos.O];
        }
        return solved;
    }

    [Benchmark]
    public int No_pre_processing()
    {
        var solved = 0;

        foreach (var clue in Clues)
        {
            solved += Solver.Solve(clue, Rules.Standard, ReduceOptions.None)[Pos.O];
        }
        return solved;
    }

     [Benchmark]
    public int Naked_and_hidden_singles()
    {
        var solved = 0;

        foreach (var clue in Clues)
        {
            solved += Solver.Solve(clue, Rules.Standard, ReduceOptions.All)[Pos.O];
        }
        return solved;
    }

    private static ImmutableArray<Clues> Set(ImmutableArray<PuzzleBankPuzzle> puzzles, int take = 1000) =>
    [
       .. puzzles
            .OrderByDescending(p => p.Level)
            .Take(take)
            .Select(p => p.Clues)
    ];
}
