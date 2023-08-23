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

    public class Naked_Singles
    {
        private static readonly Puzzle Puzzle = Puzzle.Parse(@"
            6..|..4|..3
            ..5|7.6|.1.
            .1.|...|7..
            ---+---+---
            .98|32.|6..
            75.|8..|.21
            ..4|1.7|.9.
            ---+---+---
            4..|5..|..7
            .6.|...|1..
            3..|69.|.52"
        );

        private static readonly Locations Singles = Locations.All(Puzzle.Where(c => c.Values.SingleValue()).Select(c => c.Location));
        private static readonly Technique[] Techniques = new[] { new SudokuSolver.Techniques.NakedSingles() };

        [Benchmark]
        public Puzzle Old()
        {
            return Solver.Solve(Puzzle, Techniques).Last().Reduced;
        }

        [Benchmark]
        public Puzzle With_context()
        {
            var context = new Context(Puzzle, Singles);
            SudokuSolver.Techniques2.NakedSingles.Reduce(context);
            return context.Puzzle;
        }
    }
}