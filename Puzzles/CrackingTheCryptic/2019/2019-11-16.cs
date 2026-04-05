using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_11_16 : CtcPuzzle
{
    public override string Title => "Jigsaw";

    public override string? Author => "Aart van de wetering";

    public override Uri? Url => new("https://youtu.be/f5GWiAIZXGI");

    public override O Duration => O.s;

    public override Clues Clues { get; } = Clues.Parse("""
        . . .│. . .│.│.│.
        ─┐   │     │ └─┘ 
        .│. .│. . .│. . 3
        ─┘   └─┐ ┌─┤     
        . . . 4│.│1│. . .
        ───┬─┬─┤ └─┤ ┌───
        . .│.│.│5 .│.│. .
           └─┘ ├─┬─┴─┘   
        . . . 9│.│. . . .
           ┌─┬─┴─┤ ┌─┐   
        6 .│.│. .│.│.│. .
        ───┘ ├─┐ ├─┴─┴───
        . . .│.│.│. 2 . .
             ├─┘ └─┐   ┌─
        . . .│7 . .│. .│.
         ┌─┐ │     │   └─
        .│.│.│. . .│. . .

        """);

    public override Cells Solution { get; } = Cells.Parse("""
        8 2 1│6 7 3│9│5│4
        ─┐   │     │ └─┘ 
        9│7 5│1 2 4│6 8 3
        ─┘   └─┐ ┌─┤     
        3 6 9 4│8│1│5 2 7
        ───┬─┬─┤ └─┤ ┌───
        2 8│4│3│5 9│1│7 6
           └─┘ ├─┬─┴─┘   
        1 5 7 9│6│2 4 3 8
           ┌─┬─┴─┤ ┌─┐   
        6 4│8│2 3│5│7│1 9
        ───┘ ├─┐ ├─┴─┴───
        4 9 3│8│1│7 2 6 5
             ├─┘ └─┐   ┌─
        5 1 6│7 9 8│3 4│2
         ┌─┐ │     │   └─
        7│3│2│5 4 6│8 9 1
        """);

    public override Rules Constraints { get; }
        = Rules.Jigsaw("""
        a a a│b b b│c│X│c
        ─┐   │     │ └─┘ 
        X│a a│b b b│c c c
        ─┘   └─┐ ┌─┤     
        a a a a│b│X│c c c
        ───┬─┬─┤ └─┤ ┌───
        d d│X│d│b b│c│e e
           └─┘ ├─┬─┴─┘   
        d d d d│X│e e e e
           ┌─┬─┴─┤ ┌─┐   
        d d│f│g g│e│X│e e
        ───┘ ├─┐ ├─┴─┴───
        f f f│X│g│h h h h
             ├─┘ └─┐   ┌─
        f f f│g g g│h h│X
         ┌─┐ │     │   └─
        f│X│f│g g g│h h h
        """)
        + Diagonal.NE_SW
        + Diagonal.NW_SE;
}
