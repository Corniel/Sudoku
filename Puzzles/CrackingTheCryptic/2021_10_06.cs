
using SudokuSolver.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_10_06 : CtcPuzzle
{
    public override string Title => "Dutch Whispers";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/6pAQYHf42Ik");
    public override O Duration => O.oo;

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        519|...|...
        ...|...|...
        ...|...|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        481|627|395
        296|534|817
        375|819|642
        ---+---+---
        843|172|569
        152|986|473
        967|453|281
        ---+---+---
        519|268|734
        634|795|128
        728|341|956
        """);

    public override Rules Constraints { get; } =
        Rules.AntiKing
        + Diagonal.NE_SW
        + Diagonal.NW_SE
        + new DutchWhisper(
        [
            (8, 0), (7, 1), (6, 2), (5, 3), (4, 4), (3, 5), (2, 6), (1, 7), (0, 8),
            (0, 7), (0, 6), (0, 5), (0, 4), (0, 3), (0, 2), (0, 1),
            (0, 0), (1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (6, 6), (7, 7), (8, 8),
        ]);
}
