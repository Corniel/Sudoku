namespace Puzzles.CrackingTheCryptic;

public sealed class _2017_09_18 : CtcPuzzle
{
    public override string Title => "9313 Super Fiendish";

    public override string? Author => "The Times";

    public override Uri? Url => new("https://youtu.be/S3lXNfkFFJo");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        ...│.4.│...
        .15│.3.│47.
        2..│...│..1
        ───┼───┼───
        .7.│8.1│.9.
        ...│2.3│...
        6..│...│..7
        ───┼───┼───
        .6.│...│.4.
        .8.│7.6│.5.
        5.7│...│3.8
        """);

    public override Cells Solution { get; } = Cells.New("""
         796│148│235
         815│932│476
         243│657│981
         ───┼───┼───
         472│861│593
         958│273│614
         631│594│827
         ───┼───┼───
         169│385│742
         384│726│159
         527│419│368
         """);
}
