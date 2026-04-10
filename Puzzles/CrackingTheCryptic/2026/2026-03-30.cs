namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_03_30 : CtcPuzzle
{
    public override string Title => "The X and The V Squared";

    public override string? Author => ".proxz14";

    public override Uri? Url => new("https://youtu.be/01KC84S7FJ8");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
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

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + KillerCages.Parse("""
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
        A = 10  B = 10  C = 10  D = 10  E = 10  F = 10  G = 10  H = 10  I = 10
        a = 5   b = 5   c = 5
        """)
        + KillerCages.Parse(
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
        A = 25  B = 25  C = 25  D = 25  E = 25  F = 25
        """,
        isSet: false)
       + KillerCages.Extend;
}
