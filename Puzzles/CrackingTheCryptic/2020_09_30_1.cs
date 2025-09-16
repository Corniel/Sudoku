
namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_09_30_1 : CtcPuzzle
{
    public override string Title => "Classic Sudoku!";
    public override string? Author => "Rimu Takamura";
    public override Uri? Url => new("https://youtu.be/V38qsL1cmFs");

    public override Clues Clues { get; } = Clues.Parse("""
        ...|49.|...
        ..8|.2.|..4
        .6.|5..|.7.
        ---+---+---
        .54|...|6..
        ...|...|...
        ..9|...|58.
        ---+---+---
        .3.|..2|.9.
        5..|.8.|3..
        ...|.73|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        215|497|836
        978|326|154
        463|518|972
        ---+---+---
        754|839|621
        381|265|749
        629|741|583
        ---+---+---
        137|652|498
        592|184|367
        846|973|215
        """);
}
