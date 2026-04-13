using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_25 : CtcPuzzle
{
    public override string Title => "Xmas 2025";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/XuwUs1bRGsk");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
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

    protected override Rules GetConstraints()
        => Rules.Standard
        + Sandwitch.New(Col.All[1].Cells, 2)
        + Sandwitch.New(Col.All[2].Cells, 0)
        + Sandwitch.New(Col.All[3].Cells, 2)
        + Sandwitch.New(Col.All[4].Cells, 5)
        + GermanWhispers.Parse("""
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
        + Quadruples.Parse("""
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

        A = 2479  a = 56  B = 14  b = 24  C = 27 c = 3
        """);
}
