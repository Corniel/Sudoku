namespace Sudoku.Restrictions;

public static class BetweenLine
{
    public static Rules New(Line line) => New(line[0], line[^1], line[1..^1]);

    public static Rules New(Pos s, Pos e, PosArray line) =>
    [
        new End(s,e, line),
        new End(e,s,line),
        .. line.Select(p => new Mask(p, Segment.Mask)),
        .. line.Select(p => new Segment(p, s, e)),
    ];

    public sealed class End(Pos appliesTo, Pos other, PosArray line) : Group(appliesTo, [other, .. line])
    {
        public Pos Other { get; } = other;

        public PosArray Line { get; } = line;

        public override Digits Restrict(SudokuCells cells)
        {
            var oth = cells[Other].Digits;
            return Hi(cells, Digits.AtLeast(oth.Min() + 1))
                | Lo(cells, Digits.AtMost(oth.Max() - 1));
        }

        /// <summary>Assumes this end is the high value.</summary>
        public Digits Hi(SudokuCells cells, Digits mask)
        {
            var min = mask.Min();

            foreach (var segement in Line)
            {
                if ((cells[segement].Digits & mask) is { HasAny: true } oth)
                {
                    min = Math.Max(min, oth.Min());
                }
                else return Digits.None;
            }
            return (min + 1)..;
        }

        /// <summary>Assumes this end is the low value.</summary>
        public Digits Lo(SudokuCells cells, Digits mask)
        {
            var max = mask.Max();

            foreach (var segement in Line)
            {
                if ((cells[segement].Digits & mask) is { HasAny: true } oth)
                {
                    max = Math.Min(max, oth.Max());
                }
                else return Digits.None;
            }
            return ..(max - 1);
        }

        public override string ToString()
            => $"BetweenLine.End[{AppliesTo}, {Other}], [{string.Join(", ", Others)}]";
    }

    public sealed class Segment(Pos appliesTo, Pos start, Pos end) : Group(appliesTo, [start, end])
    {
        public static readonly Digits Mask = 2..8;

        public Pos Start { get; } = start;

        public Pos End { get; } = end;

        public override Digits Restrict(SudokuCells cells)
        {
            var st = cells[Start].Digits;
            var ed = cells[End].Digits;
            return Range(st, ed) | Range(ed, st);
        }

        private static Digits Range(Digits s, Digits e)
            => Digits.Between(s.Min() + 1, e.Max() - 1);

        public override string ToString()
            => $"BetweenLine.Segment[{AppliesTo}], Start = {Start}, End = {End}";
    }
}
