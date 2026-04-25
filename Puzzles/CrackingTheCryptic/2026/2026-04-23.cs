using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_23 : CtcPuzzle
{
    public override string Title => "The Triple Crown";

    public override string? Author => "Kennet's Dad";

    public override Uri? Url => new("https://youtu.be/vVTSGvQZs3k");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.Parse("""
        452│367│198
        697│185│423
        183│492│567
        ───┼───┼───
        725│843│619
        869│571│342
        341│629│875
        ───┼───┼───
        574│236│981
        218│954│736
        936│718│254
        """);

    protected override Rules GetConstraints()
        => Rules.Killer("""
        AA.│...│...
        A..│...│...
        ..B│B..│...
        ───┼───┼───
        ..B│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│..C
        ...│...│.CC
        A=15  B=12  C=15
        """)
        + Disjoint.All
        + Diagonal.NE_SW
        + RenbanLines.Parse("""
        ...│..B│...
        .AA│.B.│C..
        .A.│B..│CC.
        ───┼───┼───
        ..B│...│..E
        .B.│...│.E.
        B..│...│E..
        ───┼───┼───
        ...│..E│..F
        DD.│.E.│.F.
        .D.│E..│F..
        """)
       + SameSums.Parse("""
        ...│...│...
        ...│...│...
        ...│.A.│...
        ───┼───┼───
        ...│.B.│I..
        ..C│B.E│FI.
        ...│.E.│..I
        ───┼───┼───
        ...│GD.│.H.
        ...│.G.│H..
        ...│..G│...
        A=B=C D=E=F G=H=I
        """)
        + KillerCages.Extend;
}
