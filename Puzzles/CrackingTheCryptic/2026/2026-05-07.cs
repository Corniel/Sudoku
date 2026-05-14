namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_07 : CtcPuzzle
{
    public override string Title => "Hotspots";

    public override string? Author => "Ryan W.";

    public override Uri? Url => new("https://youtu.be/KTu-Nu1gjjg");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        857│164│392
        613│928│547
        429│357│861
        ───┼───┼───
        291│576│483
        745│832│619
        386│491│725
        ───┼───┼───
        574│619│238
        938│245│176
        162│783│954
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        .1.│...│...
        ...│..7│...
        ───┼───┼───
        ...│5..│...
        ...│..2│...
        ...│...│...
        ───┼───┼───
        ...│6..│...
        ...│...│...
        ...│.8.│..4
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Pos.All.SelectMany(Hotspots);

    private static Rules Hotspots(Pos p)
    {
        List<Pos> neighbours = [];
        if (p.N() is { } n) neighbours.Add(n);
        if (p.E() is { } e) neighbours.Add(e);
        if (p.S() is { } s) neighbours.Add(s);
        if (p.W() is { } w) neighbours.Add(w);

        PosArray others = [.. neighbours];

        return
        [
            new Hotspot(p, others),
            .. Group.Select(others, (a, o) => new Neighbor(a, p, o)),
        ];
    }

    private sealed class Hotspot(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var sum = Ints.Zero;

            foreach (var other in Others)
                sum += cells[other].Digits;

            return sum.Contains(9)
                ? Digits._1_to_9
                : NotNine;
        }

        public override string ToString() => $"Hotspot = {AppliesTo}, Neighbor = {Others.Length}";
    }

    private sealed class Neighbor(Pos appliesTo, Pos hotspot, PosArray neighbors) : Group(appliesTo, [hotspot, .. neighbors])
    {
        public Pos HotSpot { get; } = hotspot;

        public PosArray Neighbors { get; } = neighbors;

        public override Digits Restrict(SudokuCells cells) 
        {
            if (cells[HotSpot].Digit is not 9) return Digits._1_to_9;

            var sum = Nine;

            foreach (var other in Neighbors)
                sum -= cells[other].Digits;

            return sum.Digits;
        }

        public override string ToString()
            => $"Neighbor = {AppliesTo}, HotSpot = {HotSpot}, Others = {string.Join(", ", Neighbors)}";
    }

    private static readonly Ints Nine = [9];
    private static readonly Digits NotNine = 1..8;
}
