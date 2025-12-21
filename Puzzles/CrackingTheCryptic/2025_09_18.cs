namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_18 : CtcPuzzle
{
    public override string Title => "Diagonality";

    public override string? Author => "The Book Wyrm";

    public override Uri? Url => new("https://youtu.be/OwAtzJLNt0U");

    public override O Duration => O.ms10;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│5..
        ...│...│...
        1..│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│..8
        .7.│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        482│317│569
        736│958│241
        195│624│873
        ───┼───┼───
        317│569│482
        958│241│736
        624│873│195
        ───┼───┼───
        569│482│317
        241│736│958
        873│195│624
        """);

    public override Rules Constraints { get; } =
        Rules.Standard
        + RenbanLines.Parse("""
        ...│..A│...
        ...│.A.│...
        ...│A..│...
        ───┼───┼───
        B..│...│C..
        .B.│...│.C.
        ..B│...│..C
        ───┼───┼───
        ...│..D│...
        ...│.D.│...
        ...│D..│...
        """)
        + Diagonal()

        // As a consquense of the above two
        + RenbanLines.Parse("""
        ...│A..│...
        ...│.A.│...
        ...│..A│...
        ───┼───┼───
        ..B│...│..C
        .B.│...│.C.
        B..│...│C..
        ───┼───┼───
        ...│D..│...
        ...│.D.│...
        ...│..D│...
        """);

    private static IEnumerable<Max3Distinct> Diagonal() => Diagonals.NWSEs.Concat(Diagonals.NESWs)
        .Where(d => d.Count > 3)
        .SelectMany(line => Group.Select(line, (a, o) => new Max3Distinct(a, o)));

    public sealed class Max3Distinct(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var distinct = Digits.New(Others.Select(o => cells[o].Digit));
            return distinct.Count switch
            {
                3 => distinct,
                1 or 2 => Digits._1_to_9,
                _ => Digits.None,
            };
        }
    }
}
