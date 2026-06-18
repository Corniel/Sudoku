namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_02_24 : CtcPuzzle
{
    public override string Title => "Confiable";

    public override string? Author => "James Singclair";

    public override Uri? Url => new("https://youtu.be/ZcbWWK_D6_M");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
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

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
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
        + Lines.Arrow("""
        ..A│aa.│...
        ...│...│...
        B..│..F│..G
        ───┼───┼───
        b..│ff.│.g.
        b..│ee.│g..
        ..E│...│..D
        ───┼───┼───
        ...│.h.│..d
        ...│h..│..d
        ..H│..C│cc.
        """)
        + Couples.BlackDots("""
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
