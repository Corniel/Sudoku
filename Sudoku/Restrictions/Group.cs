namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Group(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
{
    /// <inheritdoc />
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public ImmutableArray<Pos> Others { get; } = others;

    /// <inheritdoc />
    public PosSet Links { get; } = [.. others];

    /// <inheritdoc />
    public abstract Digits Restrict(SudokuCells cells);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Others}";
}
