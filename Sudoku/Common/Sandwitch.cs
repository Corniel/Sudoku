using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Sandwitch : Restriction
{
    public static IEnumerable<Sandwitch> New(PosSet cells, int gap)
        => Group.Select(cells, (a, _) => new Sandwitch(a, [.. cells], gap));

    private Sandwitch(Pos appliesTo, ImmutableArray<Pos> line, int gap)
    {
        Gap = gap;
        Line = line;
        Index = line.IndexOf(appliesTo);
        AppliesTo = appliesTo;
        Links = [.. line];
    }

    public int Gap { get; }

    public int Index { get; }

    public ImmutableArray<Pos> Line { get; }

    /// <inheritdoc />
    public Pos AppliesTo { get; }

    /// <inheritdoc />
    public PosSet Links { get; }

    /// <inheritdoc />
    public Digits Restrict(SudokuCells cells)
    {
        var ones = Indexes.None;
        var nine = Indexes.None;

        for (var i = 0; i < Line.Length; i++)
        {
            var digits = cells[Line[i]].Digits;

            if (digits.Contains(1)) ones |= i;
            if (digits.Contains(9)) nine |= i;
        }

        var d1 = Test(nine, ones, cells);
        var d9 = Test(ones, nine, cells);

        // No targets.
        if (d1.HasNone || d9.HasNone)
            return Digits.None;

        if (d1.HasSingle && d1.First() == Index)
            return One;

        if (d9.HasSingle && d9.First() == Index)
            return Nine;

        var allowed = _2_8;

        if (d1.Contains(Index))
            allowed |= 1;

        if (d9.Contains(Index))
            allowed |= 9;

        return allowed;
    }

    private Indexes Test(Indexes src, Indexes tar, SudokuCells cells)
    {
        var indexes = Indexes.None;

        foreach (var i in src)
        {
            var ix = i + 1;
            var sum = Ints.Zero;

            while (ix < Line.Length && sum.Min() <= Gap)
            {
                if (tar.Contains(ix) && sum.Contains(Gap))
                    indexes |= ix;

                var digits = cells[Line[ix]].Digits & _2_8;
                if (digits.HasAny)
                    sum += digits;

                ix++;
            }
        }

        foreach (var i in src)
        {
            var ix = i - 1;
            var sum = Ints.Zero;

            while (ix >= 0 && sum.Min() <= Gap)
            {
                if (tar.Contains(ix) && sum.Contains(Gap))
                    indexes |= ix;

                var digits = cells[Line[ix]].Digits & _2_8;
                if (digits.HasAny)
                    sum += digits;

                ix--;
            }
        }

        return indexes;
    }

    public override string ToString() => $"Sandwitch, Gap = {Gap}, Cells = {string.Join(',', Line)}";

    private static readonly Digits One = [1];
    private static readonly Digits Nine = [9];
    private static readonly Digits _2_8 = [2, 3, 4, 5, 6, 7, 8];
}
