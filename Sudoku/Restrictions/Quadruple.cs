namespace Sudoku.Restrictions;

/// <summary>
/// Quadruple define for the 4 cells they overlap which digit(s) should at least occur.
/// </summary>
/// Where the quadruple requires 148:
///
/// ...│...│...
/// ...│...│...
/// ...│12.│...
/// ───┼───┼───
/// ...│48.│...
/// ...│...│...
/// ...│...│...
/// ───┼───┼───
/// ...│...│...
/// ...│...│...
/// ...│...│...
/// </example>
public sealed class Quadruple(Pos appliesTo, PosArray others, Digits digits) : Group(appliesTo, others)
{
    public Digits Digits { get; } = digits;

    /// <inheritdoc />
    public override Digits Restrict(SudokuCells cells)
    {
        var missing = Digits;

        foreach (var o in Others)
            missing ^= cells[o].Digits;

        return missing.Count switch
        {
            0 => Digits._1_to_9,
            1 => missing,
            _ => Digits.None,
        };
    }

    /// <inheritdoc />
    public override string ToString() => $"Quadruple[{AppliesTo}]: Contains = {Digits}, Others = {string.Join(", ", Others)}";
}
