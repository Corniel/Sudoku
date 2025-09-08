namespace SudokuSolver;

/// <summary>Describes a restriction.</summary>
/// <remarks>
/// <see cref="Houses"/> and other restrictions based on cells having distinct
/// values must be handled by <see cref="Rules.Sets"/>.
/// </remarks>
public abstract record Restriction
{
    /// <summary>The cell that is restricted.</summary>
    public Pos AppliesTo { get; init; }

    public ImmutableArray<Pos> Involved { get; init; } = [];

    /// <summary>The remaining candidates based on the restriction.</summary>
    public abstract Candidates Restrict(Cells cells);
}
