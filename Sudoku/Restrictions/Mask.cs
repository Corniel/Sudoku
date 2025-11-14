namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public sealed class Mask(Pos appliesTo, Digits mask) : Restriction
{
    public static Mask Even(Pos appliesTo) => new(appliesTo, Digits.Even);

    public static Mask Odd(Pos appliesTo) => new(appliesTo, Digits.Odd);

    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <inheritdoc />
    public PosSet Links => PosSet.Empty;

    /// <inheritdoc />
    public Digits Restrict(SudokuCells cells) => mask;

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {mask}";
}
