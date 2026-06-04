namespace Puzzles.CrackingTheCryptic;

public sealed class _2006_01_01 : CtcPuzzle
{
    public override string Title => "World's Hardest Sudoku";

    public override string? Author => "Arto Inkala";

    public override Uri? Url => new("https://youtu.be/pZOcKXQEDi8");

    public override O Duration => O.μs100;

    public override bool IsClassic => true;

    public override Cells Solution { get; } = Cells.New("""
        812│753│649
        943│682│175
        675│491│283
        ───┼───┼───
        154│237│896
        369│845│721
        287│169│534
        ───┼───┼───
        521│974│368
        438│526│917
        796│318│452
        """);

    public override Clues Clues { get; } = Clues.New("""
        8..│...│...
        ..3│6..│...
        .7.│.9.│2..
        ───┼───┼───
        .5.│..7│...
        ...│.45│7..
        ...│1..│.3.
        ───┼───┼───
        ..1│...│.68
        ..8│5..│.1.
        .9.│...│4..
        """);
}
