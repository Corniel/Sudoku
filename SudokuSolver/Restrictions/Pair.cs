namespace SudokuSolver.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Pair(Pos appliesTo, Pos other) : Restriction
{
    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public Pos Other { get; } = other;

    /// <inheritdoc />
    public abstract Candidates Restrict(Cells cells);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Other}";
}
