using SudokuSolver.Solvers;

namespace Puzzles.CrackingTheCryptic;

public abstract class CtcPuzzle : Puzzle
{
    public override string ToString()
        => $"{string.Join('-', GetType().Name.Split('_', StringSplitOptions.RemoveEmptyEntries))}: {Title} ({Duration})";

    public override Clues Clues { get; } = Clues.None;

    public override ReduceOptions Options => base.Options with { Log = true };

    public static ImmutableArray<Puzzle> All => Collect(p => p is CtcPuzzle);
}
