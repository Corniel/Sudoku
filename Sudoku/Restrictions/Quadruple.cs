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
            0 => _1_to_9,
            1 => missing,
            _ => Digits.None,
        };
    }

    /// <inheritdoc />
    public override string ToString() => $"Quadruple[{AppliesTo}]: Contains = {Digits}, Others = {string.Join(", ", Others)}";

    public static RuleSet Extend(RuleSet rules)
    {
        var done = new HashSet<PosSet>();

        var masks = new List<Mask>();

        foreach (var q in rules.OfType<Quadruple>().Where(q => done.Add(q.Cells)))
        {
            var shared = q.Cells;

            foreach (var set in rules.Sets.Where(q.Cells.IsSubsetOf))
                shared |= set;

            shared ^= q.Cells;

            foreach (var c in shared)
                masks.Add(new(c, ~q.Digits));
        }
        return rules + masks;
    }
}
