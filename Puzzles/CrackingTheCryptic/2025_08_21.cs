using SudokuSolver.Restrictions;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_08_21 : CtcPuzzle
{
    public override string Title => "The Miracle Sudoku Of Eleven";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/OzzuJUU6g84");

    public override Clues Clues => Clues.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|.3.|...
        ...|...|...
        ...|.8.|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """);

    public override Cells Solution => Cells.Parse("""
        752|693|184
        184|275|369
        369|418|527
        ---+---+---
        527|936|841
        841|752|693
        693|184|275
        ---+---+---
        275|369|418
        418|527|936
        936|841|752
        """);

    public override Rules Rules => new()
    {
        Sets = Houses.Standard,
        Restrictions =
        [
            .. AtMosts(),
            .. Pos.All.Select(NonConsecutive)
        ]
    };

    private static IEnumerable<AtMost> AtMosts()
    {
        for (var r = 1; r < 9; r++)
        {
            for (var c = 0; c < 8; c++)
            {
                Pos fst = (r + 0, c + 0);
                Pos sec = (r - 1, c + 1);

                yield return new AtMost(fst, sec, 11);
                yield return new AtMost(sec, fst, 11);
            }
        }
    }

    private static NonConsecutive NonConsecutive(Pos cell)
    {
        Pos[] others =
        [
            cell.N(),
            cell.E(),
            cell.S(),
            cell.W(),
        ];
        return new(cell, [.. others.Where(p => p.OnBoard)]);
    }
}
