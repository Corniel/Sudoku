namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_03_13 : CtcPuzzle
{
    public override string Title => "The Trident";

    public override string? Author => "GBPack";

    public override Uri? Url => new("https://youtu.be/sOSrJCXdSCQ");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.New("""
        579│264│813
        863│719│254
        214│583│769
        ───┼───┼───
        347│825│691
        628│391│547
        195│647│382
        ───┼───┼───
        932│456│178
        751│938│426
        486│172│935
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Jigsaw.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ..A│.A.│A..
        ..A│AAA│A..
        ...│.A.│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
       + Couples.WhiteDots("""
        ...│...│...
        ...│...│.AA
        BB.│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .CC│...│...
        ...│...│...
        ...│...│...
        """)
        + Lines.Arrow("""
        .A.│...│...
        ..a│.dD│e..
        ..a│.d.│e..
        ───┼───┼───
        C..│...│.E.
        .c.│...│...
        c.b│...│fF.
        ───┼───┼───
        ..b│.g.│f..
        ..b│.gG│f..
        .B.│...│...
        """);
}
