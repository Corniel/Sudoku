using Puzzles;

namespace Specs;

internal static class TestSolver
{
    private const SolverType Default = SolverType.Dynamic;

    public static Cells Solve(Puzzle puzzle, SolverType solver = Default)
        => Solve(puzzle.Clues, puzzle.Constraints, solver);

    public static Cells Solve(Clues clues, Rules? rules = null, SolverType solver = Default) => solver switch
    {
        SolverType.Dynamic => DynamicSolver.Solver.Solve(clues, rules ?? Rules.Standard),
        SolverType.Dlx => Dlx.DlxSolver.Solve(clues),
        SolverType.Reference => Reference.Solver.Solve(clues),
        SolverType.StrategyBased => StrategyBased.StrategyBasedSolver.Solve(clues),
        _ => throw new ArgumentOutOfRangeException(nameof(solver), $"'{solver}' is an unknown/unsuported solver type"),
    };
}
