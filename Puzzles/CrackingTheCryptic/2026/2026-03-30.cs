namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_03_30 : CtcPuzzle
{
    public override string Title => "The X and The V Squared";

    public override string? Author => ".proxz14";

    public override Uri? Url => new("https://youtu.be/01KC84S7FJ8");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        159│467│823
        468│239│751
        732│851│964
        ───┼───┼───
        321│975│648
        985│146│237
        647│382│195
        ───┼───┼───
        573│628│419
        896│514│372
        214│793│586
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Groups.Cages("""
        ...│AA.│...
        CC.│B..│...
        .aa│B..│.DD
        ───┼───┼───
        bb.│...│...
        ...│.FF│.EE
        GG.│...│...
        ───┼───┼───
        ...│.HH│...
        .I.│.cc│...
        .I.│...│...
        A = B = C = D = E = F = G = H = I = 10
        a = b = c = 5
        """)
        + Groups.Cages(
        """
        ...│...│...
        ...│.AA│AAA
        ...│..C│...
        ───┼───┼───
        B..│.C.│C..
        BBB│C..│.C.
        ..C│...│.F.
        ───┼───┼───
        DDD│...│.F.
        D.E│...│FF.
        D..│EEE│F..
        A = B = C = D = E = F = 25
        """,
        isSet: false)
       + KillerCages.Extend
       ;
}
