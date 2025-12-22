namespace Puzzles.CrackingTheCryptic;

public abstract class CtcPuzzle : Puzzle
{
    public override string ToString()
        => $"{string.Join('-', GetType().Name.Split('_', StringSplitOptions.RemoveEmptyEntries))}: {Title} ({Duration})";

    public override Clues Clues { get; } = Clues.None;

    public virtual bool IsClassic => false;

    public static ImmutableArray<Puzzle> All => Collect(p => p is CtcPuzzle);

    public static IEnumerable<Puzzle> Classics => All.Where(p => ((CtcPuzzle)p).IsClassic);
}
