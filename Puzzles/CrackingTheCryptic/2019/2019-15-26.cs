namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_05_26 : CtcPuzzle
{
    public override string Title => "Thermo Sudoku";

    public override string? Author => "Jonas Gleim";

    public override Uri? Url => new("https://youtu.be/KTth49YrQVU");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        987│634│125
        621│578│349
        543│219│876
        ───┼───┼───
        432│186│597
        175│923│468
        896│745│213
        ───┼───┼───
        764│851│932
        359│462│781
        218│397│654
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + Thermometers.Parse("""
        GFE│D.I│LM.
        ...│C.J│.N.
        ...│B.Q│PO.
        ───┼───┼───
        ...│A..│...
        ...│.fg│hij
        cdU│...│..n
        ───┼───┼───
        b.T│...│..m
        aWX│...│..l
        ...│...│...
        """)
       + Thermometers.Parse("""
        G..│...│...
        F..│...│...
        E..│...│...
        ───┼───┼───
        DCB│A..│...
        ...│.f.│...
        ...│.g.│...
        ───┼───┼───
        ...│.h.│...
        ...│.i.│..l
        ...│.qp│onm
        """);
}
