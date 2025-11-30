namespace Puzzles.CrackingTheCryptic;

public sealed class _2018_09_19 : CtcPuzzle
{
    public override string Title => "Hard 2018-09-26";

    public override string? Author => "The New York Times";

    public override Uri? Url => new("https://youtu.be/zCohweFecw0");

    public override O Duration => O.μs100;

    public override bool IsStandard => true;

    public override Clues Clues { get; } = Clues.Parse("""
        6..│.4.│...
        3.1│.7.│.4.
        ..7│6..│.8.
        ───┼───┼───
        918│.6.│...
        ...│.9.│13.
        ...│..4│.6.
        ───┼───┼───
        ...│9..│...
        4..│.3.│...
        .35│...│.1.
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         682│345│791
         391│872│645
         547│619│382
         ───┼───┼───
         918│263│574
         264│597│138
         753│184│269
         ───┼───┼───
         176│958│423
         429│731│856
         835│426│917
         """);
}
