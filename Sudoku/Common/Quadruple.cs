using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Quadruple(Pos appliesTo, ImmutableArray<Pos> others, Digits digits) : Group(appliesTo, others)
{
    public Digits Digits { get; } = digits;

    /// <inheritdoc />
    public override Digits Restrict(SudokuCells cells)
    {
        var self = cells[AppliesTo].Digits;
        var others = Others.Select(o => cells[o].Digits).ToArray();

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
