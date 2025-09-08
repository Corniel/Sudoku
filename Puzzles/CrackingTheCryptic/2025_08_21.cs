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

    public override ImmutableArray<Constraint> Constraints =>
    [
        .. Rules.Standard,
        .. AtMost11s(),
        .. NonConsecutives(),
    ];

    public static IEnumerable<AtMost> AtMost11s()
    {
        for (var r = 1; r < 9; r++)
        {
            for (var c = 0; c < 8; c++)
            {
                Pos one = (r + 0, c + 0);
                Pos two = (r - 1, c + 1);

                yield return new AtMost(one, two, 11);
            }
        }
    }

    private static IEnumerable<NonConsecutive> NonConsecutives()
    {
        foreach(var pos in Pos.All)
        {
            var n = pos.N();
            var w = pos.W();
            if (n.OnBoard) yield return new NonConsecutive(pos, n);
            if (w.OnBoard) yield return new NonConsecutive(pos, w);
        }
    }
}
