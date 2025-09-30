namespace SudokuSolver.Common;

public abstract class Set(params ImmutableArray<Pos> cells) : Rule(cells)
{
    /// <inheritdoc />
    public sealed override bool IsSet => true;

    /// <inheritdoc />
    public override ImmutableArray<Restriction> Restrictions { get; } = [];
}
