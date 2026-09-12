namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_11 : CtcPuzzle
{
    public override string Title => "Right Angles Wrong Quads";

    public override string? Author => "Scojo";

    public override Uri? Url => new("https://youtu.be/bOcMD06DNV4");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        951│362│874
        467│518│293
        823│497│615
        ───┼───┼───
        534│821│967
        692│734│581
        718│956│342
        ───┼───┼───
        286│173│459
        149│285│736
        375│649│128
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
            ...│...│...
            .AA│...│...
            .AA│...│...
            ───┼───┼───
            ...│...│...
            ...│.BB│CC.
            ...│.BB│CC.
            ───┼───┼───
            ...│.DD│EE.
            ...│.DD│EE.
            ...│...│...
            A!48 B!1279 C!129 D!1249 E!1269
            """)
        + Lines.Zip("""
            CDE│G..│.U.
            B..│HOP│ST.
            A..│I.Q│...
            ───┼───┼───
            MLK│J..│.WX
            a..│...│..Y
            bc.│..f│g..
            ───┼───┼───
            ...│..e│...
            .k.│m..│..z
            ij.│no.│.xy
            """);
}
