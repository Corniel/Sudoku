namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_02_01 : CtcPuzzle
{
    public override string Title => "Hard 2019-01-31";

    public override string? Author => "The New York Times";

    public override Uri? Url => new("https://youtu.be/8dNHOyzH-gc");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.Parse("""
        .2.│...│..5
        ..4│.7.│..1
        ...│.3.│...
        ───┼───┼───
        .7.│.2.│9..
        4..│...│3..
        ...│6..│..8
        ───┼───┼───
        .56│...│.1.
        ...│3..│7.2
        9..│8..│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         729│481│635
         364│579│281
         185│236│479
         ───┼───┼───
         678│123│954
         412│958│367
         593│647│128
         ───┼───┼───
         256│794│813
         841│365│792
         937│812│546
         """);
}
