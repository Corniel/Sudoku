namespace Puzzles.CrackingTheCryptic;

public sealed class _2017_08_31 : CtcPuzzle
{
    public override string Title => "9284 Super Fiendish";

    public override string? Author => "The Times";

    public override Uri? Url => new("https://youtu.be/nrz4rhbaVR0");

    public override O Duration => O.μs10;

    public override bool IsStandard => true;

    public override Clues Clues { get; } = Clues.Parse("""
        ..3│...│...
        ...│.3.│...
        2..│8.6│7..
        ───┼───┼───
        6.4│32.│1..
        ..7│5.1│.4.
        5..│.94│8..
        ───┼───┼───
        ...│.47│..5
        4..│...│...
        .8.│9.3│2..
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         163│479│528
         978│235│461
         245│816│793
         ───┼───┼───
         694│328│157
         837│561│942
         512│794│836
         ───┼───┼───
         329│147│685
         451│682│379
         786│953│214
         """);
}
