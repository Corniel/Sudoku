namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_04 : CtcPuzzle
{
    public override string Title => "Coral Corridor";

    public override string? Author => "Sotehr";

    public override Uri? Url => new("https://youtu.be/L5q_FWlkjLQ");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        358│124│769
        927│638│154
        416│597│328
        ───┼───┼───
        273│986│415
        861│345│297
        549│271│683
        ───┼───┼───
        135│469│872
        682│753│941
        794│812│536
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        .A.│..D│EEE
        .A.│..D│EE.
        ───┼───┼───
        ..B│...│...
        ...│C..│.O.
        ..F│GG.│.O.
        ───┼───┼───
        ...│..H│..P
        ...│...│I.P
        ...│...│PPP
        A=B=C D=E F=G=H=I O=P
        """)
        + Lines.Renban("""
        AAA│...│...
        A.B│...│...
        A..│B..│...
        ───┼───┼───
        .A.│.BB│B..
        .A.│..C│...
        ...│...│C..
        ───┼───┼───
        .DD│D..│.C.
        DDD│D..│.C.
        ...│...│...
        """)
        + Couples.BlackDots("""
        ...│..A│...
        ...│..A│...
        B..│...│.C.
        ───┼───┼───
        B..│...│.C.
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Couples.WhiteDots("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        .A.│...│..C
        ───┼───┼───
        .A.│...│..C
        ...│B..│...
        ...│B..│...
        """);
}
