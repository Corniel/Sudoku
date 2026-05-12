namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_14 : CtcPuzzle
{
    public override string Title => "Supersonic Slingshots";

    public override string? Author => "DubiousMobius";

    public override Uri? Url => new("https://youtu.be/VUl7u_2sse0");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        398│572│146
        274│916│853
        156│483│972
        ───┼───┼───
        942│357│681
        567│821│439
        813│649│725
        ───┼───┼───
        429│138│567
        635│794│218
        781│265│394
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Grid.NamedGroups("""
        .AB│C.D│EF.
        aaB│C.D│EF.
        ..B│C.D│.bb
        ───┼───┼───
        ccc│c.D│.dd
        eee│eee│ee.
        ffG│.gg│ggg
        ───┼───┼───
        ..G│.I.│hhh
        iiG│HIJ│.jj
        ..G│HIJ│...
        """)
        .SelectMany(g => Group.Select(g, (o, a) => new SlingShot(o, a)))
        + Couples.BlackDots("""
        ..A│...│..B
        ..A│...│..B
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        CC.│...│...
        ...│...│...
        ...│...│...
        """)
        + Couples.WhiteDots("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        AA.│.BB│...
        ...│...│...
        ───┼───┼───
        ...│...│..C
        ...│...│..C
        ...│...│...
        """)
        + KillerCages.Extend;

    public sealed class SlingShot(Pos appliesTo, PosArray others)
        : Group(appliesTo, others)
        , Summation
    {
        public Ints Sum { get; } = Init(others.Length+1);

        private static Ints Init(int size) =>
        [
            .. Digits.All
                .Where(d => d.Count == size).Select(d => d.Sum())
                .Where(s => s % 9 is 0)
        ];

        public override Digits Restrict(SudokuCells cells)
        {
            var sum = Sum;

            foreach (var other in Others)
                sum -= cells[other].Digits;

            return sum.Digits;
        }
    }
}
