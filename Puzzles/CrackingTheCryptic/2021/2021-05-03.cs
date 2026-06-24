namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_05_03 : CtcPuzzle
{
    public override string Title => "Spoons";

    public override string? Author => "Phistomefel";

    public override Uri? Url => new("https://youtu.be/xzuRD2TdXts");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        231│456│987
        864│729│351
        975│831│462
        ───┼───┼───
        186│942│573
        342│567│198
        597│183│624
        ───┼───┼───
        618│294│735
        729│315│846
        453│678│219
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Lines.Thermometer("""
        ...│...│...
        .AE│IM.│RVa
        .BF│JN.│SWb
        ───┼───┼───
        .CG│KO.│TXc
        ...│...│...
        e.i│m.r│v..
        ───┼───┼───
        f.j│n.s│w..
        g.k│o.t│x..
        ...│...│...
        """)
        + Lines.Thermometer("""
        ..A│BC.│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ..E│FG.│...
        ...│...│.ae
        ───┼───┼───
        ...│...│.bf
        ...│...│.cg
        ..I│JKO│NM.
        """);
}
