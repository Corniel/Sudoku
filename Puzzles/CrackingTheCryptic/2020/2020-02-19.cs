namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_02_15 : CtcPuzzle
{
    public override string Title => "Classic Sudoku";

    public override string? Author => "?";

    public override Uri? Url => new("https://youtu.be/9m9t8ie9-EE");

    public override O Duration => O.μs100;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        5..│2..│.4.
        ...│6.3│...
        .3.│..9│..7
        ───┼───┼───
        ..3│..7│...
        ..7│..8│...
        6..│...│.2.
        ───┼───┼───
        .8.│...│..3
        ...│4..│6..
        ...│1..│5..
        """);

    public override Cells Solution { get; } = Cells.New("""
        598│271│346
        742│653│891
        136│849│257
        ───┼───┼───
        813│527│964
        427│968│135
        659│314│728
        ───┼───┼───
        285│796│413
        971│435│682
        364│182│579
        """);
}
