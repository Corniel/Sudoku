namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public sealed class Mask(Pos appliesTo, Digits mask) : Restriction
{
    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <inheritdoc />
    public PosSet Links => PosSet.Empty;

    /// <inheritdoc />
    public Digits Restrict(SudokuCells cells) => mask;

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {mask}";
}
