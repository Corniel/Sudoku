namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_02_24 : CtcPuzzle
{
    public override string Title => "Confiable";

    public override string? Author => "James Singclair";

    public override Uri? Url => new("https://youtu.be/ZcbWWK_D6_M");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        968│352│714
        534│761│982
        721│489│653
        ───┼───┼───
        379│815│426
        486│237│195
        215│694│378
        ───┼───┼───
        657│923│841
        892│146│537
        143│578│269
        """);

    protected override Rules GetConstraints()
        => Rules.Killer("""
        AA.│...│...
        A..│...│...
        ...│...│...
        ───┼───┼───
        ...│BB.│CC.
        ...│BB.│CC.
        ...│...│...
        ───┼───┼───
        ...│DD.│...
        ...│DD.│..E
        ...│...│.EE

        A = 20  B = 14  C = 16  D = 16  E = 22
        """)
        + Arrows.Parse("""
        ..A│BC.│...
        ...│...│...
        a..│..i│..m
        ───┼───┼───
        b..│kj.│.n.
        c..│JK.│o..
        ..I│...│..E
        ───┼───┼───
        ...│.O.│..F
        ...│N..│..G
        ..M│..e│fg.
        """)
        + BlackDots.Parse("""
        .A.│...│...
        .A.│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + KillerCages.Extend;
}
