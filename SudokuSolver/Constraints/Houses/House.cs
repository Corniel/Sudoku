namespace SudokuSolver.Constraints;

public abstract class House(int index, PosSet cells) : Constraint
{
    /// <summary>The index of the house.</summary>
    public int Index { get; } = index;

    /// <inheritdoc />
    public sealed override bool IsSet => true;

    /// <inheritdoc />
    public override PosSet Cells { get; } = cells;

    /// <inheritdoc />
    public override ImmutableArray<Restriction> Restrictions => [];

    internal override string DebuggerDisplay => $"[{Index}]";

    internal static IEnumerable<int> range() => Enumerable.Range(0, _9);
}
