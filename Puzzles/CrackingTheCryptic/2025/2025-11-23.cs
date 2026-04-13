namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_23 : CtcPuzzle
{
    public override string Title => "Ice Breaker";

    public override string? Author => "IcyFruit";

    public override Uri? Url => new("https://youtu.be/IeV2kiiTvjk");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.Parse("""
        384│517│296
        576│982│134
        921│634│587
        ───┼───┼───
        418│273│659
        652│198│743
        739│456│812
        ───┼───┼───
        893│761│425
        147│325│968
        265│849│371
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + RenbanLines.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        AA.│.BB│B..
        A..│.B.│...
        ───┼───┼───
        ...│.B.│...
        ..C│...│.DD
        .CC│...│.D.
        """)
        + GermanWhispers.Parse("""
        ...│..A│BC.
        ..G│...│.D.
        ..H│...│.E.
        ───┼───┼───
        ..I│JK.│...
        ...│...│...
        .OP│Q..│...
        ───┼───┼───
        .N.│...│W..
        .M.│...│V..
        ...│.ST│U..
        """)
        + NamedCage.Parse("""
        ...│..X│XX.
        ..X│...│.X.
        ..X│...│XXX
        ───┼───┼───
        ..X│XX.│XXX
        XX.│.XX│X..
        XXX│XX.│...
        ───┼───┼───
        .X.│.X.│X..
        .XX│...│XXX
        .XX│.XX│XX.
        """).SelectMany(c => Group.Select(c.Cells, (a, o) => new Repeat(a, o)));

    private sealed class Repeat(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            Array.Clear(Counts);

            foreach (var c in Others)
            {
                var d = cells[c].Digit;

                // Too many of the specfied digit.
                if (Counts[d]++ >= d && d != 0)
                    return Digits.None;
            }

            var allowed = Digits.None;
            for (var d = 1; d <= _9; d++)

                // Allow the digits with 'space' left.
                if (Counts[d] < d)
                    allowed |= d;
            return allowed;
        }

        private static readonly int[] Counts = new int[_9 + 1];
    }
}
