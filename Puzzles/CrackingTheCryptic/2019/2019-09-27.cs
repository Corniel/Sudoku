namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_09_27 : CtcPuzzle
{
    public override string Title => "Hard 2019-09-26";

    public override string? Author => "The New York Times";

    public override Uri? Url => new("https://youtu.be/QXDrwCCj0oE");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│..1│269
        2..│.5.│..1
        ───┼───┼───
        ...│.86│9..
        .5.│.49│...
        ...│...│.7.
        ───┼───┼───
        .38│.7.│6..
        ..5│...│.97
        .9.│..5│..4
        """);

    public override Cells Solution { get; } = Cells.New("""
         361│492│785
         574│831│269
         289│657│431
         ───┼───┼───
         712│586│943
         853│749│126
         946│213│578
         ───┼───┼───
         138│974│652
         425│168│397
         697│325│814
         """);
}
