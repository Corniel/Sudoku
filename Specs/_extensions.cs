using Puzzles;

namespace Specs;

internal static class TestToolExtensions
{
    public static Cells Solve(this Puzzle puzzle, Rules? rules = null)
        => Solver.Solve(puzzle.Clues, rules ?? puzzle.Constraints, ReduceOptions.All);
        // => DancingLinks.LinksSolver.Single(puzzle.Clues, rules ?? puzzle.Constraints)
}
