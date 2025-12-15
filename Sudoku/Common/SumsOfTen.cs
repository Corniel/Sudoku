namespace Sudoku.Common;

public sealed class SumsOfTen(Pos appliesTo, ImmutableArray<Pos> line) : Restriction
{
    public Pos AppliesTo { get; } = appliesTo;

    public ImmutableArray<Pos> Line { get; } = line;

    public PosSet Links { get; } = [.. line];

    public Digits Restrict(SudokuCells cells)
    {
        Ints total = Ints.Zero;
        Ints check = Ints.Zero;

        foreach (var digits in Line.Select(c => cells[c].Digits))
        {
            total += digits;
            Ints temp = default;

            foreach (var digit in digits)
                temp |= check + digit;

            // Group of 10 rule is broken.
            if ((temp & Mask).HasNone)
                return Digits.None;

            check = temp % 10;
        }

        // The total is not correct.
        if (!(total % 10).Contains(0))
            return Digits.None;

        var other = (total - cells[AppliesTo].Digits) % 10;
        var allowed = Ten - other;
        return allowed.Digits;
    }

    private static readonly Ints Ten = Ints.New(10);
    private static readonly Ints Mask = Ints.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
}
