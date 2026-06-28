namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_27 : CtcPuzzle
{
    public override string Title => "Proxyimity";

    public override string? Author => "zetamath";

    public override Uri? Url => new("https://youtu.be/w8Ml0ETI5IM");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        745│826│913
        621│397│485
        893│154│267
        ───┼───┼───
        972│468│351
        158│239│746
        436│715│892
        ───┼───┼───
        287│641│539
        369│582│174
        514│973│628
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        .AA│...│...
        .AA│BB.│...
        ───┼───┼───
        ...│BBC│C..
        DD.│..C│C..
        DD.│...│.EE
        ───┼───┼───
        ...│...│.EE
        ...│.FF│..4
        ...│.FF│...
        A:123 B:456 C:789 D:13 E:23 F:78
        """)
        + Lines.Arrow("""
        Aa.│B..│C..
        .aa│b..│cD.
        ...│b..│c.d
        ───┼───┼───
        Eee│b..│c.d
        .f.│...│...
        .f.│...│...
        ───┼───┼───
        gF.│.hi│...
        g..│..h│i..
        G..│...│HI.
        """)
        + Quadruple.Extend;
}
