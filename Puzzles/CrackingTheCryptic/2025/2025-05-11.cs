namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_11 : CtcPuzzle
{
    public override string Title => "Quadrants";

    public override string? Author => "Supware";

    public override Uri? Url => new("https://youtu.be/IEO4oA2-TTQ");

    public override O Duration => O.ms100;

    public override Clues Clues { get; } = Clues.New("""
        .2.│...│...
        .13│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│7..
        ...│...│.89
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        526│839│174
        813│724│695
        479│156│823
        ───┼───┼───
        635│918│247
        287│345│961
        194│267│358
        ───┼───┼───
        952│483│716
        361│572│489
        748│691│532
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Regions();

    private static Rules Regions()
    {
        PosSet circles = [.. Clues.New("""
            ...│...│...
            ...│.1.│.1.
            ...│...│...
            ───┼───┼───
            ...│...│...
            .1.│.1.│.1.
            ...│...│...
            ───┼───┼───
            ...│...│...
            .1.│.1.│...
            ...│...│..1
            """).Select(c => c.Pos)];

        foreach (var p in Pos.All)
        {
            if (p.N() is { } n && p.W() is { } w)
            {
                PosSet cells = [p, n, w, p - 10];

                if (circles.NotAny(cells.Contains))
                {
                    foreach (var r in Group.Select(cells, (a, o) => new Region(a, o)))
                        yield return r;
                }
            }
        }
    }

    private sealed class Region(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var sum = 0;

            foreach (var val in Others.Select(o => cells[o].Digit))
            {
                if (val is 0) return Digits._1_to_9;
                sum += val;
            }
            return Allowed[sum % 4];
        }

        private static readonly ImmutableArray<Digits> Allowed =
        [
            [4, 8],
            [3, 7],
            [2, 6],
            [1, 5, 9],
        ];
    }
}
