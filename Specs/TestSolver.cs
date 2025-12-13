using Puzzles;

namespace Specs;

internal static class TestSolver
{
    public static Cells Solve(Puzzle puzzle, TestSolverType solver = TestSolverType.DynamicSolver)
        => Solve(puzzle.Clues, puzzle.Constraints, solver);

    public static Cells Solve(Clues clues, Rules? rules = null, TestSolverType solver = TestSolverType.DynamicSolver) => solver switch
    {
        TestSolverType.DynamicSolver => DynamicSolver.Solver.Solve(clues, rules ?? Rules.Standard),
        TestSolverType.Dlx => Dlx.DlxSolver.Solve(clues),
        TestSolverType.Reference => Reference.Solver.Solve(clues),
        TestSolverType.StrategyBased => StrategyBased.StrategyBasedSolver.Solve(clues),
        _ => throw new ArgumentOutOfRangeException(nameof(solver), $"'{solver}' is an unknown/unsuported solver type"),
    };
}
