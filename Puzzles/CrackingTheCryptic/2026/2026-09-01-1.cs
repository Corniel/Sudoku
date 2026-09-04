namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_01_1 : CtcPuzzle
{
    public override string Title => "Ascents Traversed Evenly";

    public override string? Author => "Kaktuslav";

    public override Uri? Url => new("https://youtu.be/UNqWl3TNa7c");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.New("""
        647│382│915
        821│596│473
        953│417│286
        ───┼───┼───
        482│739│561
        396│251│847
        715│648│392
        ───┼───┼───
        179│825│634
        264│973│158
        538│164│729
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│.A.│...
            ───┼───┼───
            ...│.B.│...
            ...│..B│A..
            ...│...│...
            A=B
            """)
        + pos(1, 0).IsEven
        + pos(7, 8).IsEven
        + Lines.Arrow("""
            ...│...│...
            ..a│...│b.c
            .A.│..B│.C.
            ───┼───┼───
            a.x│.b.│c.y
            .X.│...│.Y.
            x.d│.e.│y.f
            ───┼───┼───
            .D.│E..│.F.
            d.e│...│f..
            ...│...│...
            """)
        + Lines.Arrow("""
            ...│...│...
            ..a│...│bBb
            ...│A..│...
            ───┼───┼───
            ...│.a.│...
            ...│...│...
            ...│.d.│...
            ───┼───┼───
            ...│..D│...
            cCc│...│d..
            ...│...│...
            """);
}
