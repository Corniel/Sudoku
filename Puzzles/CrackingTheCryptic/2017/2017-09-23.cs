namespace Puzzles.CrackingTheCryptic;

public sealed class _2017_09_23 : CtcPuzzle
{
    public override string Title => "Diabolic 22 Sept 2017";

    public override string? Author => "The Daily Telegraph";

    public override Uri? Url => new("https://youtu.be/h4_935wCSFY");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.Parse("""
        .9.│.63│...
        8..│..1│..2
        ...│...│9..
        ───┼───┼───
        27.│.1.│.6.
        ...│8.7│...
        .3.│...│.91
        ───┼───┼───
        ..6│...│...
        ...│6.8│..9
        .1.│79.│.5.
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         794│263│185
         853│971│642
         621│485│937
         ───┼───┼───
         275│319│468
         169│847│523
         438│526│791
         ───┼───┼───
         986│152│374
         547│638│219
         312│794│856
         """);
}
