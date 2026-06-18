namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_17 : CtcPuzzle
{
    public override string Title => "Nabnerfel";

    public override string? Author => "Antiknight";

    public override Uri? Url => new("https://youtu.be/z9T9kUTum1M");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        854│697│213
        139│824│657
        267│351│894
        ───┼───┼───
        723│568│149
        945│712│368
        681│943│572
        ───┼───┼───
        498│135│726
        312│476│985
        576│289│431
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Arrow("""
        ...│.Aa│a..
        ..E│...│...
        c..│eee│.H.
        ───┼───┼───
        c.g│...│h..
        C.g│...│h.D
        ..g│...│h.d
        ───┼───┼───
        .G.│fff│..d
        ...│...│F..
        ..b│bB.│...
        """)
        + Couples.WhiteDots("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│AA.│...
        ...│..B│B..
        ...│.C.│...
        ───┼───┼───
        F..│.C.│..D
        F..│...│..D
        ...│...│EE.
        """)
        + Couples.Consecutive((2, 4), (3, 4))
         + Grid.NamedGroups("""
        AA.│...│.BB
        AA.│...│.BB
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        CC.│...│.DD
        CC.│...│.DD
        """).SelectMany(GoldenLine)
        + KillerCages.Extend;

    private static Rules GoldenLine(NamedGroup group) => Dominos
        .RoundRobin([.. group])
        .SelectMany(NonConsecutive.New);
}
