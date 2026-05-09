namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_01_15 : CtcPuzzle
{
    public override string Title => "Hard Sudoku";

    public override string? Author => "?";

    public override Uri? Url => new("https://youtu.be/9m9t8ie9-EE");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        ..1│.6.│.59
        ...│..3│.2.
        .6.│.8.│...
        ───┼───┼───
        4..│...│5..
        .2.│...│...
        .7.│2..│48.
        ───┼───┼───
        8..│...│9.5
        7..│6.9│.3.
        ..5│...│.4.
        """);

    public override Cells Solution { get; } = Cells.New("""
        381│462│759
        954│173│826
        267│985│314
        ───┼───┼───
        436│891│572
        128│547│693
        579│236│481
        ───┼───┼───
        813│724│965
        742│659│138
        695│318│247
        """);
}
