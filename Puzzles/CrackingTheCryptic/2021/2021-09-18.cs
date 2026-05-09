namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_09_18 : CtcPuzzle
{
    public override string Title => "Patto Patto";

    public override string? Author => "shye";

    public override Uri? Url => new("https://youtu.be/SDTtcipqw7M");

    public override O Duration => O.μs100;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        .23│.65│.89
        9..│..4│..5
        5..│9..│...
        ───┼───┼───
        6..│3..│.18
        38.│59.│..2
        ...│.86│3..
        ───┼───┼───
        23.│...│..6
        8.7│.2.│..3
        .96│.53│82.
        """);

    public override Cells Solution { get; } = Cells.New("""
         723│165│489
         961│874│235
         548│932│167
         ───┼───┼───
         652│347│918
         384│591│672
         179│286│354
         ───┼───┼───
         235│418│796
         817│629│543
         496│753│821
         """);
}
