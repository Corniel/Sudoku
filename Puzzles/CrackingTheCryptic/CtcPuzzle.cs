namespace Puzzles.CrackingTheCryptic;

public abstract class CtcPuzzle : Puzzle
{
    public override string ToString()
        => $"{string.Join('-', GetType().Name.Split('_', StringSplitOptions.RemoveEmptyEntries))}: {Title} ({Duration})";

    public static ImmutableArray<Puzzle> All => Collect(p => p is CtcPuzzle);
}
