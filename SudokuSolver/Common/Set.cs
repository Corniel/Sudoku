namespace SudokuSolver.Common;

public abstract class Set(PosSet cells) : Rule
{
    protected Set(ImmutableArray<Pos> cells) : this(PosSet.New([.. cells])) { }

    /// <inheritdoc />
    public override bool IsSet => true;

    /// <inheritdoc />
    public override PosSet Cells { get; } = cells;

    /// <inheritdoc />
    public override ImmutableArray<Restriction> Restrictions { get; } = [];
}
