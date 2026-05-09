using System.Diagnostics;

namespace Puzzles;

public abstract class Puzzle
{
    public abstract string Title { get; }

    public virtual string? Author { get; }

    public virtual Uri? Url { get; }

    public virtual O Duration { get; } = O.Unknown;

    public abstract Clues Clues { get; }

    public RuleSet Constraints => constraints ??= GetConstraints();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private RuleSet? constraints;

    public virtual Cells Solution { get; } = Cells.Empty;

    protected virtual RuleSet GetConstraints() => RuleSet.Standard;

    public override string ToString() => Title;

    public static ImmutableArray<Puzzle> Collect(Predicate<Puzzle> predicate) => [.. typeof(Puzzle)
        .Assembly
        .GetExportedTypes()
        .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Puzzle)) && t.GetConstructors().Any(c => c.GetParameters().Length is 0))
        .Select(Activator.CreateInstance)
        .OfType<Puzzle>()
        .Where(p => predicate(p))];
}
