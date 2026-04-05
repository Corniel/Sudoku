namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_03_18 : CtcPuzzle
{
    public override string Title => "X-Wing Sudoku";

    public override string? Author => "?";

    public override Uri? Url => new("https://youtu.be/az2M0V9QCXk");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.Parse("""
        6..|.9.|..7
        .4.|..7|1..
        ..2|8..|.5.
        ---+---+---
        8..|...|.9.
        ...|.7.|...
        .3.|...|..8
        ---+---+---
        .5.|..2|3..
        ..4|5..|.2.
        9..|.3.|..4
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         683|195|247
         549|627|183
         712|843|956
         ---+---+---
         865|314|792
         491|278|635
         237|956|418
         ---+---+---
         156|482|379
         374|569|821
         928|731|564
         """);
}
