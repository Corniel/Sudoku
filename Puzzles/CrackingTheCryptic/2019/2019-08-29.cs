namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_08_29 : CtcPuzzle
{
    public override string Title => "New York Times Hard Sudoku 2019-08-27";

    public override string? Author => "New York Times";

    public override Uri? Url => new("https://youtu.be/az2M0V9QCXk");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        63.│...│.81
        .2.│..3│...
        ...│.17│43.
        ───┼───┼───
        .96│4..│57.
        ...│762│...
        .8.│...│6..
        ───┼───┼───
        .6.│.2.│...
        3.9│...│.6.
        ...│...│..9
        """);

    public override Cells Solution { get; } = Cells.New("""
        637│254│981
        124│893│756
        958│617│432
        ───┼───┼───
        296│481│573
        543│762│198
        781│935│624
        ───┼───┼───
        465│329│817
        379│148│265
        812│576│349
        """);
}
