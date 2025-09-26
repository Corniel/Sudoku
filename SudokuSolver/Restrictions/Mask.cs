namespace SudokuSolver.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public sealed class Mask(Pos appliesTo, Candidates mask) : Restriction
{
    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <inheritdoc />
    public double Bits => 0;

    /// <inheritdoc />
    public Candidates Restrict(Cells cells) => mask;

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {mask}";
}
