namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_01_19 : CtcPuzzle
{
    public override string Title => "Hard 2020-01-19";

    public override string? Author => "The New York Times";

    public override Uri? Url => new("https://youtu.be/UGDnLIFdSkg");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        53.│7..│...
        ...│..4│.52
        ...│...│7..
        ───┼───┼───
        68.│..9│...
        .7.│.52│.3.
        ...│...│...
        ───┼───┼───
        9.3│6..│..4
        ..6│...│...
        ...│8..│9.1
        """);

    public override Cells Solution { get; } = Cells.New("""
         532│716│849
         761│984│352
         498│235│716
         ───┼───┼───
         684│379│125
         179│452│638
         325│168│497
         ───┼───┼───
         913│627│584
         846│591│273
         257│843│961
         """);
}
