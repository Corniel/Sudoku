namespace Puzzles;

public abstract class Puzzle
{
    public abstract string Title { get; }

    public virtual string? Author { get; }

    public virtual Uri? Url { get; }

    public abstract Clues Clues { get; }

    public virtual Rules Rules { get; } = Rules.Standard;

    public virtual Cells Solution { get; } = Cells.Empty;

    public override string ToString() => Title;

    public static ImmutableArray<Puzzle> Collect(Predicate<Puzzle> predicate) => [.. typeof(Puzzle)
        .Assembly
        .GetExportedTypes()
        .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Puzzle)) && t.GetConstructors().Any(c => c.GetParameters().Length is 0))
        .Select(Activator.CreateInstance)
        .OfType<Puzzle>()
        .Where(p => predicate(p))];
}
