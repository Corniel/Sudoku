namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_08 : CtcPuzzle
{
    public override string Title => "Heavy Is The Crown";

    public override string? Author => "sujoyku";

    public override Uri? Url => new("https://youtu.be/X-ebnjTycsc");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        176│549│823
        982│136│547
        354│782│691
        ───┼───┼───
        743│918│265
        625│374│918
        819│265│734
        ───┼───┼───
        297│453│186
        538│621│479
        461│897│352
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        .A.│...│...
        .A.│...│...
        .A.│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .C.│...│...
        .CC│...│BBB
        ...│...│...
        A = 20  B = 20  C = 20
        """)
        + Lines.Tentropic("""
        ...│...│...
        ..A│...│...
        a.B│C..│...
        ───┼───┼───
        .b.│.DE│...
        .c.│..F│...
        .d.│...│G..
        ───┼───┼───
        ..e│...│HI.
        ...│fgh│...
        ...│...│i..
        """)
        + Groups.Cages("""
        ..A│...│...
        ...│BB.│...
        ...│..B│E..
        ───┼───┼───
        ...│...│C..
        ..X│Y..│.C.
        ...│Y..│.C.
        ───┼───┼───
        ...│.Z.│..D
        ...│...│...
        ...│...│...
        A=B=C=D=E X=Y=Z
        """)
        + KillerCages.Extend;
}
