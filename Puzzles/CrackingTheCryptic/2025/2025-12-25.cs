namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_25 : CtcPuzzle
{
    public override string Title => "Xmas 2025";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/XuwUs1bRGsk");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        168│435│792
        352│978│641
        794│261│358
        ───┼───┼───
        927│143│865
        516│827│934
        843│596│127
        ───┼───┼───
        481│659│273
        279│314│586
        635│782│419
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Sandwitch.New(Houses.Cols[1].Cells, 2)
        + Sandwitch.New(Houses.Cols[2].Cells, 0)
        + Sandwitch.New(Houses.Cols[3].Cells, 2)
        + Sandwitch.New(Houses.Cols[4].Cells, 5)
        + Lines.GermanWhisper("""
        ...│.c.│...
        ...│b.d│...
        ..a│g.o│e..
        ───┼───┼───
        ..h│...│n..
        .ij│A.K│lm.
        ..B│...│J..
        ───┼───┼───
        .CD│E.G│HI.
        ...│.F.│...
        ...│...│...
        """)
        + Groups.Cages("""
        ...│...│...
        ...│...│...
        .AA│...│aa.
        ───┼───┼───
        .AA│...│aa.
        BB.│...│.bb
        BB.│...│.bb
        ───┼───┼───
        CC.│...│.cc
        CC.│...│.cc
        ...│...│...

        A:2479  a:56  B:14  b:24  C:27 c:3
        """);
}
