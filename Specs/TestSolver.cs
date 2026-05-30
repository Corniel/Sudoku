using Puzzles;

namespace Specs;

internal static class TestSolver
{
    private const SolverType Default = SolverType.Jvo;

    public static Cells Solve(Puzzle puzzle, SolverType solver = Default)
        => Solve(puzzle.Clues, puzzle.Constraints, solver);

    public static Cells Solve(Clues clues, RuleSet? rules = null, SolverType solver = Default) => solver switch
    {
        SolverType.Dynamic => DynamicSolver.Solver.Solve(clues, rules ?? RuleSet.Standard),
        SolverType.Dlx => Dlx.DlxSolver.Solve(clues),
        SolverType.Reference => Reference.Solver.Solve(clues),
        SolverType.StrategyBased => StrategyBased.StrategyBasedSolver.Solve(clues),
        SolverType.Jvo => JvoSolver.Solver.Solve(clues),
        _ => throw new ArgumentOutOfRangeException(nameof(solver), $"'{solver}' is an unknown/unsuported solver type"),
    };
}
