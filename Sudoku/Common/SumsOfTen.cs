namespace Sudoku.Common;

public sealed class SumsOfTen(Pos appliesTo, ImmutableArray<Pos> line) : Restriction
{
    public Pos AppliesTo { get; } = appliesTo;

    public ImmutableArray<Pos> Line { get; } = line;

    public PosSet Links { get; } = [.. line];

    private readonly Ints Mask = appliesTo == line[^1] ? M10 : M1_10;

    public Digits Restrict(SudokuCells cells)
    {
        var allow = Digits.None;
        var total = Ints.Zero;

        foreach (var pos in Line)
        {
            var digits = cells[pos].Digits;

            if (pos == AppliesTo)
            {
                foreach (var digit in digits.Where(d => ((total + d) & Mask).HasAny))
                    allow |= digit;

                digits = allow;
            }

            total += digits;
            total &= M1_10;

            if (total.HasNone)
                return Digits.None;

            total %= 10;
        }

        // The total is correct.
        return total.First() is 0
            ? allow
            : Digits.None;
    }

    public override string ToString() => $"Sums-of-10[{AppliesTo}]: Length = {Line.Length}, {string.Join(", ", Line)}";

    private static readonly Ints M10 = Ints.New(10);
    private static readonly Ints M1_10 = Ints.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
}
