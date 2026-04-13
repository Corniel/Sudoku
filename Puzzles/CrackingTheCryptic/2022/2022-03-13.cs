namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_03_13 : CtcPuzzle
{
    public override string Title => "The Trident";

    public override string? Author => "GBPack";

    public override Uri? Url => new("https://youtu.be/sOSrJCXdSCQ");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.Parse("""
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

    protected override Rules GetConstraints() =>
        Rules.Standard
        + Jigsaw.Parse("""
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
       + WhiteDots.Parse("""
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
        + Arrows.Parse("""
        .A.│...│...
        ..B│.GF│L..
        ..C│.H.│K..
        ───┼───┼───
        O..│...│.J.
        .P.│...│...
        Q.d│...│kj.
        ───┼───┼───
        ..c│.h.│l..
        ..b│.gf│m..
        .a.│...│...
        """)
        ;
}
