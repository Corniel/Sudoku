using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_04_12 : CtcPuzzle
{
    public override string Title => "Magic Square Sudoku";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/hAyZ9K2EBF0");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        384│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│..2
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        843│567│219
        275│913│846
        619│428│375
        ───┼───┼───
        384│672│951
        726│159│483
        951│834│627
        ───┼───┼───
        537│286│194
        462│791│538
        198│345│762
        """);

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + Diagonal.NW_SE
        + Diagonal.NE_SW
        + MagicSquare.SelectMany(line => Group.Select(line, (a, o) => new Cage(a, o, [15])));

    private static readonly PosSet[] MagicSquare =
    [
        [(3, 3), (3, 4), (3, 5)],
        [(4, 3), (4, 4), (4, 5)],
        [(5, 3), (5, 4), (5, 5)],

        [(3, 3), (4, 3), (5, 3)],
        [(3, 4), (4, 4), (5, 4)],
        [(3, 5), (4, 5), (5, 5)],

        [(3, 3), (4, 4), (5, 5)],
        [(3, 5), (4, 4), (5, 3)],
    ];
}
