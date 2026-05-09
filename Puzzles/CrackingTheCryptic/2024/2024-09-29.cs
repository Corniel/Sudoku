namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_09_29 : CtcPuzzle
{
    public override string Title => "3 In the Corner";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/x6RrwaOb0Iw");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        594│738│261
        261│495│837
        837│162│594
        ───┼───┼───
        159│627│483
        483│951│726
        726│384│159
        ───┼───┼───
        948│273│615
        615│849│372
        372│516│948
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        3..│...│...
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + AtLeast3s()
        + new Arrow()
        ;

    private static Rules AtLeast3s()
        => Dominos.Ort
        .Where(d => Box.IndexOf(d.A) == Box.IndexOf(d.B))
        .SelectMany(d => DeltaMin.New(d.A, d.B, 3));

    private sealed class Arrow() : Constraint
    {
        public PosSet Cells { get; } = [.. Arrows.SelectMany(l => l)];

        public bool IsSatisfied(SudokuCells cells)
            => Arrows.Any(a => Fits(cells, a));

        private static bool Fits(SudokuCells cells, PosArray arrow)
        {
            var point = cells[arrow[0]].Digits;
            var shaft = Ints.Zero;

            for (var i = 1; i < arrow.Length; i++)
                shaft += cells[arrow[i]].Digits;

            return (point & shaft.Digits).HasAny;
        }
    }

    public static readonly PosArray[] Arrows = [.. Init((0, 0), (0, 1), (1, 0), (1, 1))];

    private static IEnumerable<PosArray> Init(params Pos[] points)
    {
        foreach (var p in points)
        {
            var arrow = new Pos[8];
            arrow[0] = p;

            for (var i = 1; i < 8; i++)
                arrow[i] = (p.Row + i, p.Col + i);

            yield return [.. arrow];
        }
    }
}
