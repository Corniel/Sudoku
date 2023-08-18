using BenchmarkDotNet.Attributes;
using SudokuSolver;

namespace Benchmarks;

public class Solving
{
    private readonly Regions Regions = Regions.Default;
    private readonly IReadOnlyCollection<Puzzle> Puzzles = Specs.Data.Puzzles.Solvable;

    [Benchmark]
    public int Solve()
    {
        var count = 0;
        foreach (var puzzle in Puzzles)
        {
            var solved = Solver.Solve(puzzle, Regions).LastOrDefault();
            count += solved is null ? 0 : 1;
        }

        return count;
    }
}