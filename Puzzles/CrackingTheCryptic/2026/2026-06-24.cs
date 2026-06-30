namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_24 : CtcPuzzle
{
    public override string Title => "Auralis";

    public override string? Author => "Rab3acon";

    public override Uri? Url => new("https://youtu.be/rSMMWRaKq8M");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        921│457│836
        378│962│145
        654│138│792
        ───┼───┼───
        189│725│463
        267│349│581
        435│681│279
        ───┼───┼───
        716│294│358
        843│516│927
        592│873│614
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        A..│.E.│G..
        ACC│.EF│G..
        ───┼───┼───
        BBO│DDK│L.I
        ...│PKM│L.I
        TT.│PRR│NNI
        ───┼───┼───
        SUU│VQ.│.HH
        ...│V.X│...
        ...│...│Y..
        A=B=C=D=E F=G H=I X=Y
        K=L=M=N O=P=Q=R S=T=U=V
        """)
        + Couples.GoldenDots("""
        ...│...│...
        AA.│.CD│...
        ...│.CD│...
        ───┼───┼───
        BB.│...│.E.
        ...│...│.E.
        .GG│...│.F.
        ───┼───┼───
        ...│...│.F.
        ...│...│...
        ...│...│...
        """)
        + NonConsecutive.New((6, 7), (6, 8));
}
