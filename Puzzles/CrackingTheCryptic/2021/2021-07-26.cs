namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_07_26 : CtcPuzzle
{
    public override string Title => "Classic Sudoku";

    public override string? Author => "Topy Linkala";

    public override Uri? Url => new("https://youtu.be/uWgu-HOm5to");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|9..
        5..|...|.18
        .28|..7|.4.
        ---+---+---
        ...|1.5|..9
        .9.|.3.|.6.
        4..|9.2|...
        ---+---+---
        .3.|5..|62.
        28.|...|..1
        ..6|...|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        314│856│972
        567│429│318
        928│317│546
        ───┼───┼───
        672│185│439
        891│734│265
        453│962│187
        ───┼───┼───
        139│578│624
        285│643│791
        746│291│853
        """);
}
