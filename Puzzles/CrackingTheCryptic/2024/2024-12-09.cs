namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_12_09 : CtcPuzzle
{
    public override string Title => "Elbow Joint";

    public override string? Author => "Arachno";

    public override Uri? Url => new("https://youtu.be/fhAJVxU0v6Q");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        617│235│498
        824│169│573
        935│874│216
        ───┼───┼───
        469│317│825
        572│698│341
        381│542│967
        ───┼───┼───
        743│981│652
        156│423│789
        298│756│134
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Lines.Thermometer("""
            .A.│..b│a..
            .B.│.c.│...
            .C.│ed.│..m
            ───┼───┼───
            D.f│...│..l
            E..│...│jk.
            ...│..i│...
            ───┼───┼───
            .G.│..h│...
            .HI│...│MNO
            ...│.KL│...
            """)
        ;
}
