
using SudokuSolver.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_10_06 : CtcPuzzle
{
    public override string Title => "Dutch Whispers";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/6pAQYHf42Ik");
    public override O Duration => O.Unknown;

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
        481│627│395
        697│345│218
        325│891│647
        ───┼───┼───
        964│172│853
        153│986│472
        278│453│961
        ───┼───┼───
        519│268│734
        836│714│529
        742│539│186
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
