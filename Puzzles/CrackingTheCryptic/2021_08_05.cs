namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_08_05 : CtcPuzzle
{
    public override string Title => "Checkerboard";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/5wtgymz5yjQ");

    public override O Duration => O.oo;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│.9.│1..
        ...│.1.│...
        8..│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        9..│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        628│153│794
        459│786│231
        713│942│586
        ───┼───┼───
        367│294│158
        295│618│473
        841│375│962
        ───┼───┼───
        534│829│617
        182│567│349
        976│431│825
        """);

    public override Rules Constraints { get; } =
        Rules.Standard
        + Thermometers.Parse("""
            T.N│...│Q.W
            .S.│M.P│.V.
            H.B│...│E.K
            ───┼───┼───
            .G.│A.D│.J.
            ...│...│...
            .g.│a.d│.j.
            ───┼───┼───
            h.b│...│e.k
            .s.│m.p│.v.
            t.n│...│q.w
            """);

}
