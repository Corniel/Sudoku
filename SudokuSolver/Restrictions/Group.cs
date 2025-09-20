namespace SudokuSolver.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Group(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
{
    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public ImmutableArray<Pos> Others { get; } = others;

    /// <inheritdoc />
    public abstract Candidates Restrict(Cells cells);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Others}";
}
